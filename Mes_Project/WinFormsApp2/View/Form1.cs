using System;
using System.Windows.Forms;
using WinFormsApp2.Presenter;
using WinFormsApp2.Model;
using WinFormsApp2.ViewInterface;
using Microsoft.VisualBasic;
using ActUtlTypeLib;
using ActUtlType64Lib;
using WinFormsApp2.View;
using Microsoft.VisualBasic.Logging;
using System.Text.Json;
using System.Text;


namespace WinFormsApp2
{
    public partial class Form1 : Form, IMainView
    {
        private readonly MainPresenter _mainPresenter;
        private readonly PlcModel _plcModel;
        private readonly PlcFunction _plcFuntion;
        private bool isAutoMode = true; // 현재 모드를 저장할 변수
        private CancellationTokenSource _cts;
        public static event Action? LodtUpdated;
        private Task _messageTask;
        public static string? tempstr { get; set; }




        public Form1()
        {
            InitializeComponent();
            _plcModel = new PlcModel(); // Plc 연결로직 생성
            _mainPresenter = new MainPresenter(this); //현재시간 불러오기
            _plcFuntion = new PlcFunction(this);
            InverterFunction inverter = new InverterFunction();
            Loginbtn.Text = User.UserName == null ? "로그인" : "로그아웃";
            tempLbl.Text = "50";
            PopupBtn.Enabled = false;
            button1.Enabled = false;
            //Popup.ResetUpdateEvent();
            Popup.SettingsUpdated += () =>
            {
                if (Popup.StationNumber != null && Popup.SelectedPort != null)
                {

                    _plcFuntion.StartReading();
                    PlcFunction._plc.SetDevice("M11", 1);
                    Console.WriteLine("PLC 데이터 읽기가 시작되었습니다.");
                    label4.Text = "ip0" + Popup.StationNumber.ToString();
                    EndBt.Enabled = false;
                    
                }
            };
            Login.UserUpdated += () =>
            {
                if (Login.id != null)
                {
                    Userid.Text = User.UserName;


                }
                if (ApiToken.role == "systemAdmin" || ApiToken.role == "supervise")
                {
                    PopupBtn.Enabled = true;
                    button1.Enabled = true;
                }
                else { PopupBtn.Enabled = false; button1.Enabled = false; }
            };
            Order.OderUpdated += () =>
            {
                label7.Text = Products.PartId;
                label13.Text = Products.PlannQty.ToString();
                Lotnumlbl.Text = Products.LotNum;
                
            };
            LodtUpdated += () =>
            {
                
                Lotnumlbl.Text = Products.LotNum;

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
                if (PlcFunction.pvvalue < PlcFunction.svvalue - 10)
                {
                    MessageBox.Show("사출기 온도를 충분히 올려주십시오.");
                    StartBt.Enabled = true;
                }
                else
                {
                    StartBt.BackColor = Color.Red;
                    EndBt.BackColor = Color.FromArgb(224, 224, 224);
                    MessageBox.Show("생산이 시작됩니다.");
                    PlcFunction._plc.SetDevice("M12", Popup.mode);
                    PlcFunction._plc.SetDevice("M10", 1);
                    StartBt.Enabled = false;
                    EndBt.Enabled = true;

                    // 0.5초마다 반복작업 실행
                    _cts = new CancellationTokenSource();
                    var cancellationToken = _cts.Token;

                    _messageTask = Task.Run(async () =>
                    {
                        while (!cancellationToken.IsCancellationRequested)
                        {
                            try
                            {
                                // 반복작업
                                PerformRepeatedTask();

                                // 0.5초 대기
                                await Task.Delay(500, cancellationToken);
                            }
                            catch (TaskCanceledException)
                            {
                                break; // 작업이 취소되면 루프 종료
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"반복 작업 중 에러 발생: {ex.Message}");
                            }
                        }
                    }, cancellationToken);
                }


            }
            else
            {
                MessageBox.Show("PLC 연결 상태를 확인 해주세요.");
            }
        }

        // 반복작업 정의
        private async void PerformRepeatedTask()
        {
            try
            {
                const string serverAddress = "13.125.114.64"; // 서버 주소
                const int port = 51900; // 서버 포트 번호

                // 소켓 서버 연결
                using var stream = await SocketClient.ConnectToServerAsync(serverAddress, port);

                // InjectionCum 객체 생성
                var injectionCum = SocketClient.CreateInjectionCum();

                // 메시지 전송
                await SocketClient.SendMessageAsync(stream, injectionCum, CancellationToken.None);

                Console.WriteLine("메시지 전송 완료");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"반복 작업 중 에러 발생: {ex.Message}");
            }
        }



        private void EndBt_Click(object sender, EventArgs e)
        {

            if (StartBt.Enabled == false)
            {
                if (Popup.StationNumber != null && Popup.SelectedTempPort != null)
                {


                    StartBt.BackColor = Color.FromArgb(224, 224, 224);
                    EndBt.BackColor = Color.Red;
                    MessageBox.Show("생산이 종료됩니다.");
                    PlcFunction._plc.SetDevice("M11", 1);
                    EndBt.Enabled = false;
                    StartBt.Enabled = true;

                    // 반복작업 종료
                    _cts?.Cancel();
                }
            }
            else MessageBox.Show("시작중인지 확인 바랍니다.");
        }


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int tempValue = trackBar1.Value;
            tempLbl.Text = $"{tempValue}";
        }



        private void Loginbtn_Click(object sender, EventArgs e)
        {
            var login = new Login();
            // User.UserName이 NULL인지 확인하여 상태를 판단
            if (User.UserName == null)
            {
                // 로그인 로직

                login.ShowDialog();

                // 로그인 성공 시 버튼 텍스트 변경
                if (User.UserName != null) // 로그인 성공으로 User.UserName 설정됨
                {
                    Loginbtn.Text = "로그아웃";

                }
            }
            else
            {
                // 로그아웃 로직
                ApiToken.UserId = null;
                ApiToken.UserPw = null;
                ApiToken.Token = null;
                User.UserName = null;

                // 로그아웃 완료 메시지
                MessageBox.Show("로그아웃 되었습니다.", "로그아웃", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 버튼 텍스트 초기화
                Loginbtn.Text = "로그인";
                PopupBtn.Enabled = false;
                button1.Enabled = false;
                // 기타 UI 업데이트 (옵션)
                Userid.Text = string.Empty; // Userid 레이블 초기화

                login.ShowDialog();
            }
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
                    trackBar1.Value = 50;
                    tempLbl.Text = "50";
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var order = new Order();
            order.ShowDialog();
        }

        private async void togglebtn_Click(object sender, EventArgs e)
        {
            if (StartBt.Enabled == true)
            { 
                //PlcFunction._plc.SetDevice("D2", 0);

                using var httpClient = new HttpClient { BaseAddress = new Uri("http://13.125.114.64:5282/api/") };

                try
                {
                    var partId_Date = new { partId = Products.PartId, date = Order.todayDate };

                    // JSON 직렬화
                    string jsonContent = JsonSerializer.Serialize(partId_Date);
                    var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    // API 요청 보내기
                    HttpResponseMessage response = await httpClient.PostAsync("lot/issue-new", httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        // JSON 파싱
                        var responseData = JsonSerializer.Deserialize<JsonDocument>(responseBody);
                        if (responseData != null && responseData.RootElement.TryGetProperty("lotId", out var lotIdElement))
                        {
                            Products.LotNum = lotIdElement.GetString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"lot 생성중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                LodtUpdated.Invoke();

            }
            else MessageBox.Show("정지후 수정 바랍니다.");

            

        }
    }
}