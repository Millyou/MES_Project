using System;
using System.Windows.Forms;
using WinFormsApp2.Presenter;
using WinFormsApp2.Model;
using WinFormsApp2.ViewInterface;
using Microsoft.VisualBasic;
using ActUtlTypeLib;
using ActUtlType64Lib;
using WinFormsApp2.View;


namespace WinFormsApp2
{
    public partial class Form1 : Form, IMainView
    {
        private readonly MainPresenter _mainPresenter;
        private readonly PlcModel _plcModel;
        private readonly PlcFunction _plcFuntion;
        private bool isAutoMode = true; // 현재 모드를 저장할 변수
        public static string? tempstr { get; set; }
        public static int mode { get; set; }



        public Form1()
        {
            InitializeComponent();
            _plcModel = new PlcModel(); // Plc 연결로직 생성
            _mainPresenter = new MainPresenter(this); //현재시간 불러오기
            _plcFuntion = new PlcFunction(this);
            InverterFunction inverter = new InverterFunction();
            mode = 0;
            Popup.SettingsUpdated += () =>
            {
                if (Popup.StationNumber != null && Popup.SelectedPort != null)
                {
                    _plcFuntion.StartReading();
                    MessageBox.Show("PLC 데이터 읽기가 시작되었습니다.");

                }


            };
        }

        public void UpdateLocalDateTime(string dateTime)
        {
            localdateLbl.Text = dateTime;
        }

        private void PopupBtn_Click(object sender, EventArgs e)
        {
            var popup = new Popup(_plcModel); // 팝업창 들어가기(station초기화)
            popup.ShowDialog();
        }

        private void StartBt_Click(object sender, EventArgs e)
        {
            if (Popup.StationNumber != null && Popup.SelectedTempPort != null)
            {
                if (PlcFunction.pvvalue > PlcFunction.svvalue - 10)
                {
                    StartBt.BackColor = Color.Red;
                    EndBt.BackColor = Color.FromArgb(224, 224, 224);
                    MessageBox.Show("생산이 시작됩니다.");
                    PlcFunction._plc.SetDevice("M10", 1);
                    StartBt.Enabled = false;
                }
                else MessageBox.Show("사출기 온도를 충분히 올려주십시오.");

            }
            else MessageBox.Show("PLC 연결 상태를 확인 해주세요.");
        }

        private void EndBt_Click(object sender, EventArgs e)
        {
            if (Popup.StationNumber != null && Popup.SelectedTempPort != null)
            {
                StartBt.BackColor = Color.FromArgb(224, 224, 224);
                EndBt.BackColor = Color.Red;
                MessageBox.Show("생산이 종료됩니다.");
                PlcFunction._plc.SetDevice("M11", 1);
                EndBt.Enabled = false;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int tempValue = trackBar1.Value;
            tempLbl.Text = $"{tempValue}";
        }

        private void togglebtn_Click(object sender, EventArgs e)
        {
            // 상태 변경 (토글)
            isAutoMode = !isAutoMode;

            if (Popup.StationNumber != null && Popup.SelectedTempPort != null)
            {

                // 상태에 따라 동작 변경
                if (isAutoMode)
                {
                    togglebtn.Text = "자동 모드"; // 버튼 텍스트 변경
                    togglebtn.BackColor = Color.Green; // 색상 변경
                    MessageBox.Show("자동 모드로 전환되었습니다.");
                    mode = 1;
                    PlcFunction._plc.SetDevice("M12", mode);
                }
                else
                {
                    togglebtn.Text = "수동 모드"; // 버튼 텍스트 변경
                    togglebtn.BackColor = Color.Orange; // 색상 변경
                    MessageBox.Show("수동 모드로 전환되었습니다.");
                    mode = 0;
                    PlcFunction._plc.SetDevice("M12", mode);
                }
            }
            else MessageBox.Show("PLC 연결 상태 및 라인을 정지 후 전환 바랍니다.");
        }

        private void Loginbtn_Click(object sender, EventArgs e)
        {
            var login = new Login(); // 팝업창 들어가기(station초기화)
            login.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Popup.SelectedTempPort != null)
            {
                if (tempLbl.Text == "-") { MessageBox.Show("온도를 지정하세요."); }
                else
                {
                    tempstr = tempLbl.Text;
                    _plcFuntion.setTemp(); // setTemp 메서드 호출
                    trackBar1.Value = 200;
                    tempLbl.Text = "-";
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Order order = new Order();
            order.ShowDialog();
        }
    }
}