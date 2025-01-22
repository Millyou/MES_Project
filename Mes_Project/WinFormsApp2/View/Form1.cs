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
        private bool isAutoMode = true; // ���� ��带 ������ ����
        private CancellationTokenSource _cts;
        public static event Action? LodtUpdated;
        private Task _messageTask;
        public static string? tempstr { get; set; }




        public Form1()
        {
            InitializeComponent();
            _plcModel = new PlcModel(); // Plc ������� ����
            _mainPresenter = new MainPresenter(this); //����ð� �ҷ�����
            _plcFuntion = new PlcFunction(this);
            InverterFunction inverter = new InverterFunction();
            Loginbtn.Text = User.UserName == null ? "�α���" : "�α׾ƿ�";
            tempLbl.Text = "50";
            PopupBtn.Enabled = false;
            button1.Enabled = false;
            

            //
            //Popup.ResetUpdateEvent();
            Popup.SettingsUpdated += () =>
            {
                if (Popup.StationNumber != null && Popup.SelectedPort != null)
                {

                    _plcFuntion.StartReading();
                    Console.WriteLine("PLC ������ �бⰡ ���۵Ǿ����ϴ�.");
                    label4.Text = "ip0" + Popup.StationNumber.ToString();
                    Products.LineId = label4.Text;
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
            Popup.Disconnectevent += () =>
            {

                label4.Text = "-";
                label7.Text = "-";
                Lotnumlbl.Text = "-";
                Userid.Text = "-";
                label13.Text = "-";
                label14.Text = "-";
                label8.Text = "-";
                pvLbl.Text = "-";
                svLbl.Text = "-";

            };

        }

        public void UpdateLocalDateTime(string dateTime)
        {
            localdateLbl.Text = dateTime;
        }

        private void PopupBtn_Click(object sender, EventArgs e)
        {
            var popup = new Popup(_plcModel); // �˾�â ����(station�ʱ�ȭ)
            popup.ShowDialog();
        }

        private void StartBt_Click(object sender, EventArgs e)
        {
            if (Popup.StationNumber != null && Popup.SelectedTempPort != null && Products.PartId !=null)
            {
                if (PlcFunction.pvvalue < PlcFunction.svvalue - 10)
                {
                    MessageBox.Show("����� �µ��� ����� �÷��ֽʽÿ�.");
                    StartBt.Enabled = true;
                }
                else
                {
                    StartBt.BackColor = Color.Red;
                    EndBt.BackColor = Color.FromArgb(82, 108, 137);
                    MessageBox.Show("������ ���۵˴ϴ�.");
                    PlcFunction._plc.SetDevice("M12", Popup.mode);
                    PlcFunction._plc.SetDevice("M10", 1);
                    StartBt.Enabled = false;
                    EndBt.Enabled = true;
                    
                    // 0.5�ʸ��� �ݺ��۾� ����
                    _cts = new CancellationTokenSource();
                    var cancellationToken = _cts.Token;

                    _messageTask = Task.Run(async () =>
                    {
                        while (!cancellationToken.IsCancellationRequested)
                        {
                            try
                            {
                                // �ݺ��۾�
                                PerformRepeatedTask();

                                // 0.5�� ���
                                await Task.Delay(500, cancellationToken);
                            }
                            catch (TaskCanceledException)
                            {
                                break; // �۾��� ��ҵǸ� ���� ����
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"�ݺ� �۾� �� ���� �߻�: {ex.Message}");
                            }
                        }
                    }, cancellationToken);
                }


            }
            else
            {
                MessageBox.Show("PLC ���� ���¸� Ȯ�� ���ּ���.");
            }
        }

        // �ݺ��۾� ����
        private async void PerformRepeatedTask()
        {
            try
            {
                const string serverAddress = "13.125.114.64"; // ���� �ּ�
                const int port = 51900; // ���� ��Ʈ ��ȣ

                // ���� ���� ����
                using var stream = await SocketClient.ConnectToServerAsync(serverAddress, port);

                // InjectionCum ��ü ����
                var injectionCum = SocketClient.CreateInjectionCum();

                // �޽��� ����
                await SocketClient.SendMessageAsync(stream, injectionCum, CancellationToken.None);

                Console.WriteLine("�޽��� ���� �Ϸ�");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"�ݺ� �۾� �� ���� �߻�: {ex.Message}");
            }
        }



        private void EndBt_Click(object sender, EventArgs e)
        {

            if (StartBt.Enabled == false)
            {
                if (Popup.StationNumber != null && Popup.SelectedTempPort != null)
                {


                    StartBt.BackColor = Color.FromArgb(82, 108, 137);
                    EndBt.BackColor = Color.Red;
                    MessageBox.Show("������ ����˴ϴ�.");
                    PlcFunction._plc.SetDevice("M11", 1);
                    EndBt.Enabled = false;
                    StartBt.Enabled = true;

                    // �ݺ��۾� ����
                    _cts?.Cancel();
                }
            }
            else MessageBox.Show("���������� Ȯ�� �ٶ��ϴ�.");
        }


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int tempValue = trackBar1.Value;
            tempLbl.Text = $"{tempValue}";
        }



        private void Loginbtn_Click(object sender, EventArgs e)
        {
            var login = new Login();
            // User.UserName�� NULL���� Ȯ���Ͽ� ���¸� �Ǵ�
            if (User.UserName == null)
            {
                // �α��� ����

                login.ShowDialog();

                // �α��� ���� �� ��ư �ؽ�Ʈ ����
                if (User.UserName != null) // �α��� �������� User.UserName ������
                {
                    Loginbtn.Text = "�α׾ƿ�";

                }
            }
            else
            {
                // �α׾ƿ� ����
                ApiToken.UserId = null;
                ApiToken.UserPw = null;
                ApiToken.Token = null;
                User.UserName = null;

                // �α׾ƿ� �Ϸ� �޽���
                MessageBox.Show("�α׾ƿ� �Ǿ����ϴ�.", "�α׾ƿ�", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // ��ư �ؽ�Ʈ �ʱ�ȭ
                Loginbtn.Text = "�α���";
                PopupBtn.Enabled = false;
                button1.Enabled = false;
                // ��Ÿ UI ������Ʈ (�ɼ�)
                Userid.Text = string.Empty; // Userid ���̺� �ʱ�ȭ

                login.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Popup.SelectedTempPort != null)
            {
                if (tempLbl.Text == "-") { MessageBox.Show("�µ��� �����ϼ���."); }
                else
                {
                    tempstr = tempLbl.Text;
                    _plcFuntion.setTemp(); // setTemp �޼��� ȣ��
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
                PlcFunction._plc.SetDevice("D2", 0);

                using var httpClient = new HttpClient { BaseAddress = new Uri("http://13.125.114.64:5282/api/") };

                try
                {
                    var partId_Date = new { partId = Products.PartId, lineId = Products.LineId, date = Order.todayDate };

                    // JSON ����ȭ
                    string jsonContent = JsonSerializer.Serialize(partId_Date);
                    var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    // API ��û ������
                    HttpResponseMessage response = await httpClient.PostAsync("lot/issue-new", httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        // JSON �Ľ�
                        var responseData = JsonSerializer.Deserialize<JsonDocument>(responseBody);
                        if (responseData != null && responseData.RootElement.TryGetProperty("lotId", out var lotIdElement))
                        {
                            Products.LotNum = lotIdElement.GetString();
                        }
                    }
                    LodtUpdated.Invoke();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"lot ������ ���� �߻�: {ex.Message}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                

            }
            else MessageBox.Show("������ ���� �ٶ��ϴ�.");
            
            

        }
    }
}