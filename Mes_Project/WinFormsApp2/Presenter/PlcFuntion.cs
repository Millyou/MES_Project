using ActUtlType64Lib;
using ActUtlTypeLib;
using MQTTnet.Client;
using MQTTnet;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp2;
using WinFormsApp2.Presenter;
using MQTTnet.Protocol;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using MQTTnet.Server;
using System.IO.Ports;
using NModbus.Device;
using NModbus;
using Modbus.Device;
using System.Diagnostics.Metrics;
using WinFormsApp2;
using System.Net.Sockets;
using MSD.Client.TCP;
using MSD.Crux.Core.Models;

public static class CurrentData
{
    public static int? X20 { get; set; }
    public static int? X21 { get; set; }
    public static int? Y40 { get; set; }
    public static int? D1 { get; set; }
    public static int? D2 { get; set; }
    public static int? Y41 { get; set; }
    public static int? Y42 { get; set; }
    public static int? Y43 { get; set; }
}

public class PlcFunction
{
    public static readonly ActUtlType _plc = new ActUtlType(); // MX Component 객체
    public static CancellationTokenSource _cts = null;
    private Form1 _form;
    public int result;
    private CurrentDataPublisher _dataPublisher;
    public InverterFunction inverter = new InverterFunction();

    // N.PV의 주소: 4x 00002 (Modbus 주소는 1부터 시작)
    private ushort readAddress = 1; // N.PV의 절대 번지 00002는 여기서 1로 설정
    private ushort numRegisters = 2; // 한 번에 읽을 레지스터 개수

    public static int? pvvalue;

    public static int? svvalue;
    public static object stream;

    
    public PlcFunction(Form1 form)
    {
        _form = form;
        _dataPublisher = new CurrentDataPublisher(); // MQTT Publisher 초기화
        

    }
    public async void StartReading()
    {
        
        if (_cts != null)
        {
            Console.WriteLine("PLC 데이터 읽기가 이미 실행 중입니다.");
            return;
        }

        try
        {
            _plc.ActLogicalStationNumber = (int)Popup.StationNumber;
            
            // MQTT 연결 초기화
            await _dataPublisher.InitializeAndPublishAsync();

            int connectionResult = _plc.Open();
            if (connectionResult != 0)
            {
                Console.WriteLine($"{connectionResult}");
                //throw new InvalidOperationException("PLC와 연결되지 않았습니다. 연결 상태를 확인하세요.");
            }


            _cts = new CancellationTokenSource();
            Task.Run(() => ReadPlcData(_cts.Token), _cts.Token);

            serialPort.Open();

            // Modbus Master 생성
            master = Modbus.Device.ModbusSerialMaster.CreateRtu(serialPort);
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"PLC 시작 중 오류 발생: {ex.Message}");
        }
    }

    public async void StopReading()
    {
        
        try
        {
            // 데이터 퍼블리셔 연결 해제
            var dataPublisher = new CurrentDataPublisher();
            try
            {
                await dataPublisher.DisconnectAsync();
                
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"데이터 퍼블리셔 연결 해제 중 오류 발생: {ex.Message}");
            }

            MessageBox.Show("PLC 데이터 읽기가 중지되었습니다.");

            // CancellationTokenSource 취소 및 해제
            if (_cts != null)
            {
                _cts.Cancel();
                _cts = null;
            }

            // PLC 닫기
            if (_plc != null)
            {
                try
                {
                    _plc.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"PLC 닫기 중 오류 발생: {ex.Message}");
                }
            }

            // 시리얼 포트 닫기
            if (serialPort != null && serialPort.IsOpen)
            {
                try
                {
                    serialPort.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"시리얼 포트 닫기 중 오류 발생: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"PLC 정지 중 오류 발생: {ex.Message}");
        }
    }
    
    
    private async Task ReadPlcData(CancellationToken token)
    {
        const string serverAddress = "13.125.114.64";
        const int port = 51900;
        try
        {
            int temp;
            stream = await SocketClient.ConnectToServerAsync(serverAddress, port);

            // Start socket client with the created object



            while (!token.IsCancellationRequested)
            {
                // PLC 데이터 읽기
                _plc.GetDevice("X20", out temp);
                CurrentData.X20 = temp;

                _plc.GetDevice("X21", out temp);
                CurrentData.X21 = temp;

                _plc.GetDevice("Y40", out temp);
                CurrentData.Y40 = temp;

                _plc.GetDevice("D1", out temp);
                CurrentData.D1 = temp;

                _plc.GetDevice("D2", out temp);
                CurrentData.D2 = temp;

                _plc.GetDevice("Y41", out temp);
                CurrentData.Y41 = temp;

                _plc.GetDevice("Y42", out temp);
                CurrentData.Y42 = temp;

                _plc.GetDevice("Y43", out temp);
                CurrentData.Y43 = temp;

                values = master.ReadHoldingRegisters(slaveId, readAddress, numRegisters); // 온도값 가져오기

                // UI 업데이트는 Invoke를 사용
                _form.Invoke((MethodInvoker)(() =>
                {
                    _form.label14.Text = CurrentData.D2?.ToString() ?? "0";

                    if (int.TryParse(_form.label13.Text, out int number))
                    {
                        result = number - (CurrentData.D2 ?? 0);
                        _form.label8.Text = result.ToString();
                    }

                    _form.pvLbl.Text = values[0].ToString();
                    _form.svLbl.Text = values[1].ToString();
                    UpdateTableColors();
                }));

               

                // CurrentData를 MQTT로 전송
                await _dataPublisher.PublishCurrentDataAsync();

                //-----온도 확인후 인버터 작동 및 종료----
                pvvalue = (int)values[0];
                svvalue = (int)values[1];

                if (pvvalue < svvalue - 10) { inverter.Start(); }
                else if (pvvalue >= svvalue) { inverter.Stop(); }
                

                Thread.Sleep(500); // 500ms 대기
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("PLC 데이터 읽기 작업이 취소되었습니다.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"PLC 데이터 읽기 중 오류 발생: {ex.Message}");
        }
    }


    private void UpdateTableColors()
    {
        if (CurrentData.Y41 == 1 && CurrentData.Y42 == 1 && CurrentData.Y43 == 1)
        {
            _form.tableLayoutPanel17.BackColor = Color.DarkSlateGray;
            _form.tableLayoutPanel18.BackColor = Color.DarkSlateGray;
            _form.tableLayoutPanel19.BackColor = Color.DarkSlateGray;
        }
        else if (CurrentData.Y41 == 1)
        {
            _form.tableLayoutPanel17.BackColor = Color.Red;
            _form.tableLayoutPanel18.BackColor = Color.DarkSlateGray;
            _form.tableLayoutPanel19.BackColor = Color.DarkSlateGray;
        }
        else if (CurrentData.Y42 == 1)
        {
            _form.tableLayoutPanel17.BackColor = Color.DarkSlateGray;
            _form.tableLayoutPanel18.BackColor = Color.Red;
            _form.tableLayoutPanel19.BackColor = Color.DarkSlateGray;
        }
        else if (CurrentData.Y43 == 1)
        {
            _form.tableLayoutPanel17.BackColor = Color.DarkSlateGray;
            _form.tableLayoutPanel18.BackColor = Color.DarkSlateGray;
            _form.tableLayoutPanel19.BackColor = Color.Red;
        }
    }

    public void setTemp()
    {

        ushort writeAddress = 301; // SV1의 절대 번지
        ushort writeValue = ushort.Parse(Form1.tempstr); // 설정하고자 하는 값 (예: 300도)
        ushort svnoAddress = 300; // SV1의 절대 번지
        ushort svnoValue = 1; // 설정하고자 하는 값 (예: 1번셋팅값 고정)
        try
        {
            // 시리얼 포트 초기화
            master.WriteSingleRegister(slaveId, writeAddress, writeValue);
            master.WriteSingleRegister(slaveId, svnoAddress, svnoValue);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"오류 발생: {ex.Message}");
        }


    }

    public static int baudRate = 9600;      // 장치에 맞는 BaudRate 설정
    public static Parity parity = Parity.None;
    public static int dataBits = 8;
    public static StopBits stopBits = StopBits.One;
    public static Modbus.Device.IModbusSerialMaster master;
    public static ushort[] values;
    public static byte slaveId = 1; // 변경 필요
    public static string portName = Popup.SelectedTempPort; // 실제 장치 포트로 변경
    public static SerialPort serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
} 
        

    

    
