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
        public static int mode { get; set; } = 0;

        public static string? ModeComboValue { get; set; }
        public static int? StationNumber { get; set; }
        public static string? SelectedPort { get; set; }
        public static bool IsPortComboEnabled { get; set; } = true; // 초기 상태는 true
        public static string? SelectedTempPort { get; set; }
        public Popup(PlcModel plcModel)
        {
            InitializeComponent();
            _plcFuntion = new PlcFunction(new Form1());
            _presenter = new PopupPresenter(this, plcModel);
            _presenter.LoadStations(); // 팝업 창 로드시 Station 로드
            modeCombo.Items.Add("수동모드");
            modeCombo.Items.Add("자동모드");
            getPort();
            
            
            
            if (!string.IsNullOrEmpty(SelectedPort) && PortCombo.Items.Contains(SelectedPort))
            {
                PortCombo.SelectedItem = SelectedPort;
            }
            if (!string.IsNullOrEmpty(SelectedTempPort) && PortCombo2.Items.Contains(SelectedTempPort))
            {
                PortCombo2.SelectedItem = SelectedTempPort;
            }
            if (!string.IsNullOrEmpty(ModeComboValue) && modeCombo.Items.Contains(ModeComboValue))
            {
                modeCombo.SelectedItem = ModeComboValue;
            }

            PortCombo.Enabled = IsPortComboEnabled;
            PortCombo2.Enabled = IsPortComboEnabled;
            modeCombo.Enabled = IsPortComboEnabled;

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
            if (PortCombo.SelectedItem == null || PortCombo2.SelectedItem == null)
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
            modeCombo.Enabled = false;
            ModeComboValue = modeCombo.Text; 
            SelectedTempPort = PortCombo2.Text;


        }
        public void DisconnectBtn_Click(object sender, EventArgs e)
        {
            if (StationNumber == null)
            {
                ShowErrorMessage("연결된 PLC가 없습니다.");
                return;
            }
            
            
            _plcFuntion.StopReading();
            SelectedPort = null; // 저장된 데이터 삭제
            StationNumber = null;
            //modeCombo = null;
            MessageBox.Show("연결이 해제 되었습니다.");
            PlcFunction._plc.SetDevice("M11", 1);
            IsPortComboEnabled = true; // PortCombo 활성화 상태 저장
            PortCombo.Enabled = true;
            PortCombo2.Enabled = true;
            modeCombo.Enabled = true;
            InverterFunction inverter = new InverterFunction();
            inverter.Stop();

    }

        public void SetControlsEnabled(bool portComboEnabled, bool portCombo2Enabled,bool modeComboEnable, bool connectionBtnEnabled, bool disconnectBtnEnabled)
        {
            PortCombo.Enabled = portComboEnabled;
            PortCombo2.Enabled = portCombo2Enabled;
            modeCombo.Enabled = modeComboEnable;
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
            inverter.Deceleration();
            inverter.SetFrequency(); //인버터 60hz 적용

            this.Close(); // 팝업 창 닫기
        }
        public static void ResetUpdateEvent()
        {
            SettingsUpdated = null;
        }

        public void getPort()
        {
            try
            {
                // 시스템에서 사용 가능한 포트 이름 가져오기
                string[] ports = SerialPort.GetPortNames();

                // 포트가 비어 있는지 확인
                if (ports.Length > 0)
                {
                    PortCombo2.Items.AddRange(ports); // 포트 목록 추가
                }
                else
                {
                    PortCombo2.Items.Add("null"); // 사용 가능한 포트가 없을 경우 "null" 추가
                }
            }
            catch (Exception e)
            {
                // 예외 처리: 포트를 가져오는 중 문제가 발생했을 때
                MessageBox.Show($"포트를 가져오는 중 오류 발생: {e.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // 기본적으로 "null" 추가
                PortCombo2.Items.Add("null");
            }
        }


        private void modeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (modeCombo.SelectedItem == "수동모드") Popup.mode = 0;

            else if(modeCombo.SelectedItem == "자동모드") Popup.mode = 1;
        }
    }
}