using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WinFormsApp2.Presenter;
using WinFormsApp2.Model;
using WinFormsApp2.ViewInterface;
using System.IO.Ports;
using static System.Resources.ResXFileRef;

namespace WinFormsApp2
{
    public partial class Popup : Form, IPopupView
    {
        public static event Action? SettingsUpdated;
        private readonly PopupPresenter _presenter;
        private string SelectedItem;
        private readonly PlcFunction _plcFuntion;
        public static int? StationNumber { get; set; }
        public static string SelectedPort { get; set; }
        public static bool IsPortComboEnabled { get; set; } = true; // 초기 상태는 true
        public static string SelectedTempPort {  get; set; }
        public Popup(PlcModel plcModel)
        {
            InitializeComponent();
            
            _presenter = new PopupPresenter(this, plcModel);
            _presenter.LoadStations(); // 팝업 창 로드시 Station 로드
            getPort();
            if (!string.IsNullOrEmpty(SelectedPort) && PortCombo.Items.Contains(SelectedPort))
            {
                PortCombo.SelectedItem = SelectedPort;
                
            }
            if (!string.IsNullOrEmpty(SelectedTempPort) && PortCombo2.Items.Contains(SelectedTempPort))
            {
                PortCombo2.SelectedItem = SelectedTempPort;

            }

            PortCombo.Enabled = IsPortComboEnabled;
            PortCombo2.Enabled = IsPortComboEnabled;
            
        }

        public void PopulatePortCombo(List<string> ports)
        {
            PortCombo.Items.Clear();
            foreach (var port in ports)
            {
                PortCombo.Items.Add(port); // Station 리스트를 콤보박스에 추가
            }
        }

        public void SelectPort(string selectedPort)
        {
            if (!string.IsNullOrEmpty(selectedPort) && PortCombo.Items.Contains(selectedPort))
            {
                PortCombo.SelectedItem = selectedPort;

            }
        }

        public void ConnectionBtn_Click(object sender, EventArgs e)
        {
            if (PortCombo.SelectedItem == null || PortCombo2.SelectedItem ==null)
            {
                ShowErrorMessage("포트를 선택하세요.");
                return;
            }

            // 선택된 Station 가져오기
            string selectedStation = PortCombo.SelectedItem.ToString(); // e.g., "Station 1"

            // Station 번호 추출 및 스태틱 속성에 저장
            StationNumber = int.Parse(selectedStation.Replace("Station ", "")); // e.g., 1
            SelectedPort = selectedStation; // 
            MessageBox.Show("연결이 되었습니다.");
            // 선택된 Station 확인
            Console.WriteLine($"선택된 Station 번호: {StationNumber}");

            PortCombo.Enabled = false;
            IsPortComboEnabled = false; // PortCombo 비활성화 상태 저장
            PortCombo2.Enabled = false;
            SelectedTempPort = PortCombo2.Text;
            

        }
        public void DisconnectBtn_Click(object sender, EventArgs e)
        {
            if (StationNumber == null)
            {
                ShowErrorMessage("연결된 PLC가 없습니다.");
                return;
            }
            SelectedPort = null; // 저장된 데이터 삭제
            StationNumber = null;
            MessageBox.Show("연결이 해제 되었습니다.");
            PlcFunction._plc.SetDevice("M11", 1);
            IsPortComboEnabled = true; // PortCombo 활성화 상태 저장
            PortCombo.Enabled = true;
            PortCombo2.Enabled = true;
            _plcFuntion.StopReading();
        }

        public void SetControlsEnabled(bool portComboEnabled,bool portCombo2Enabled, bool connectionBtnEnabled, bool disconnectBtnEnabled)
        {
            PortCombo.Enabled = portComboEnabled;
            PortCombo2.Enabled = portCombo2Enabled;
        }

        public void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowSuccessMessage(string message)
        {
            MessageBox.Show(message, "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SettingsUpdated?.Invoke();
            InverterFunction inverter = new InverterFunction();
            inverter.SetFrequency(6000);
            inverter.Deceleration();
            this.Close(); // 팝업 창 닫기
        }

        public void getPort()
        {
            string[] ports = SerialPort.GetPortNames();
            PortCombo2.Items.AddRange(ports);
   
        }
    }
}