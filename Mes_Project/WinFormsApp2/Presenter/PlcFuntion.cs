using ActUtlType64Lib;
using ActUtlTypeLib;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp2;

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
    private readonly ActUtlType _plc = new ActUtlType(); // MX Component 객체
    private CancellationTokenSource _cts;
    private Form1 _form;
    public int result;
    public PlcFunction(Form1 form)
    {
        _form = form;
    }
    public void StartReading()
    {
        
            if (_cts != null)
            {
                throw new InvalidOperationException("PLC 데이터 읽기가 이미 실행 중입니다.");
            }

            _cts = new CancellationTokenSource();
            Task.Run(() => ReadPlcData(_cts.Token), _cts.Token);
        
    }

    public void StopReading()
    {
        //_plc.SetDevice("M10", 0);
        _plc.SetDevice("M12", Form1.mode);
        _plc.SetDevice("M11", 1);
        _cts.Cancel(); // 작업 취소 요청
        _cts = null;   // 작업 상태 초기화
        
    }

    private void ReadPlcData(CancellationToken token)
    {
        try
        {
            // Logical Station Number 설정
            _plc.ActLogicalStationNumber = (int)Popup.StationNumber;

            // PLC 연결
            int result = _plc.Open();
            if (result != 0) // PLC 연결 상태 확인
            {
                throw new InvalidOperationException("PLC와 연결되지 않았습니다. 연결 상태를 확인하세요.");
            }

            int temp;
            _plc.SetDevice("M10", 1);
            //_plc.SetDevice("M11", 0);

            // 데이터 읽기 루프
            while (!token.IsCancellationRequested)
            {
                // 데이터 읽기 및 static 속성 설정
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


                _form.label14.Text = CurrentData.D2.ToString();

                if (int.TryParse(_form.label13.Text, out int number))
                {
                    if (CurrentData.D2.HasValue)
                    {
                        result = number - CurrentData.D2.Value;
                        _form.label8.Text = result.ToString(); // 결과를 label14에 출력
                    }
                    else
                    {
                        result = 0 - CurrentData.D2.Value;
                        _form.label8.Text = result.ToString(); // 결과를 label14에 출력
                    }
                }

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






                Console.WriteLine("PLC 데이터가 업데이트되었습니다.");
                // 1초 대기
                Thread.Sleep(1000);

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
        finally
        {
           
        }
    }
}