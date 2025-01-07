using System;
using System.Windows.Forms;
using WinFormsApp2.Presenter;
using WinFormsApp2.Model;
using WinFormsApp2.ViewInterface;
using Microsoft.VisualBasic;
using ActUtlTypeLib;
using ActUtlType64Lib;

namespace WinFormsApp2
{
    public partial class Form1 : Form, IMainView
    {
        private readonly MainPresenter _mainPresenter;
        private readonly PlcModel _plcModel;
        private readonly PlcFunction _plcFuntion;
        private bool isAutoMode = true; // 현재 모드를 저장할 변수

        public static int mode { get; set; }

        private readonly ActUtlType _plc = new ActUtlType(); // MX Component 객체
        public Form1()
        {
            InitializeComponent();
            _plcModel = new PlcModel(); // Plc 연결로직 생성
            _mainPresenter = new MainPresenter(this); //현재시간 불러오기

            _plcFuntion = new PlcFunction(this);
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
            if (Popup.StationNumber != null)
            {
                //EndBt.Enabled = true;
                //StartBt.Enabled = false;
                StartBt.BackColor = Color.Red;
                EndBt.BackColor = Color.FromArgb(224, 224, 224);
                MessageBox.Show("생산이 시작됩니다.");
                _plcFuntion.StartReading(); //데이터 불러오기 시작(별도 쓰레드)
                startLbl.Text = DateTime.Now.ToString("HH:mm:ss");
                
            }
            else MessageBox.Show("PLC 연결 상태를 확인 해주세요.");
        }

        private void EndBt_Click(object sender, EventArgs e)
        {
            //if (StartBt.Enabled != true)
            //{
                stopLbl.Text = DateTime.Now.ToString("HH:mm:ss");
                //EndBt.Enabled = false;
                //StartBt.Enabled = true;
                StartBt.BackColor = Color.FromArgb(224, 224, 224);
                EndBt.BackColor = Color.Red;
                _plcFuntion.StopReading(); //데이터 불러오기 종료
                MessageBox.Show("생산이 종료됩니다.");
            //}
            //else MessageBox.Show("현재 생산이 진행중인지 확인 해주세요.");

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int tempValue = trackBar1.Value;
            tempLbl.Text = $"온도: {tempValue}°C";
        }

        private void label13_Click(object sender, EventArgs e)
        {
           
        }

        private void togglebtn_Click(object sender, EventArgs e)
        {
            // 상태 변경 (토글)
            isAutoMode = !isAutoMode;
            
            if (Popup.StationNumber != null)
            {

                // 상태에 따라 동작 변경
                if (isAutoMode)
                {
                    togglebtn.Text = "자동 모드"; // 버튼 텍스트 변경
                    togglebtn.BackColor = Color.Green; // 색상 변경
                    MessageBox.Show("자동 모드로 전환되었습니다.");
                    mode = 1;
                }
                else
                {
                    togglebtn.Text = "수동 모드"; // 버튼 텍스트 변경
                    togglebtn.BackColor = Color.Orange; // 색상 변경
                    MessageBox.Show("수동 모드로 전환되었습니다.");
                    mode = 0;
                }
            }
            else MessageBox.Show("PLC 연결 상태 및 라인을 정지 후 전환 바랍니다.");
        }
    }
}