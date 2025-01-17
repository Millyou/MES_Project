using System;
using System.Diagnostics;
using System.Text.Json;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp2.Presenter;
using MSD.Crux.Common;
using MSD.Crux.Core.Models;
using System.Security.Claims;
using WinFormsApp2;

namespace WinFormsApp2.View
{
    public partial class Login : Form
    {
        public static string id { get; set; }
        public static string pw { get; set; }
        public static event Action? UserUpdated;
        Form1 mainview = new Form1();

        public Login()
        {
            InitializeComponent();

            // 이벤트를 코드로 연결
            //UserId.Click += textBox1_Click;
            UserId.Text = "string";
            UserPw.Text = "string";
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            // 터치 키보드 실행
            //StartTouchKeyboard();
        }

        private void StartTouchKeyboard()
        {

            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = @"C:\Program Files\Common Files\Microsoft Shared\ink\tabtip.exe",
                    UseShellExecute = true,
                    Verb = "runas" // 관리자 권한으로 실행
                };
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"터치 키보드를 실행할 수 없습니다: {ex.Message}");
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // ID와 비밀번호 가져오기
            ApiToken.UserId = UserId.Text;
            ApiToken.UserPw = UserPw.Text;

            using var httpClient = new HttpClient { BaseAddress = new Uri("http://13.125.114.64:5282/api/") };

            try
            {
                var id_pw = new { loginId = ApiToken.UserId, loginPw = ApiToken.UserPw };

                // JSON 직렬화
                string jsonContent = JsonSerializer.Serialize(id_pw);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // API 요청 보내기
                HttpResponseMessage response = await httpClient.PostAsync("login", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // JSON 파싱
                    var responseData = JsonSerializer.Deserialize<JsonDocument>(responseBody);
                    if (responseData != null && responseData.RootElement.TryGetProperty("jwtToken", out var tokenElement))
                    {
                        ApiToken.Token = tokenElement.GetString();
                        Console.WriteLine($"Token: {ApiToken.Token}");
                        //MessageBox.Show(ApiToken.Token, "토큰 정보", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    }
                    if (responseData != null && responseData.RootElement.TryGetProperty("jwtPublicKey", out var publicKeyElement))
                    {
                        User.publicKey = publicKeyElement.GetString();

                    }
                    if (responseData != null && responseData.RootElement.TryGetProperty("shift", out var shiftElement))
                    {
                        ApiToken.Shift = shiftElement.GetString();

                    }
                    if (responseData != null && responseData.RootElement.TryGetProperty("employeeNumber", out var EmployeeNumberElement))
                    {
                        ApiToken.EmployeeNumber = EmployeeNumberElement.GetInt32();
                    }
                    if (responseData != null && responseData.RootElement.TryGetProperty("name", out var UserName))
                    {
                        User.UserName = UserName.GetString();


                        // UI 업데이트
                        //MessageBox.Show("로그인 성공!", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        id = UserId.Text;
                        pw = UserPw.Text;

                        UserId.Enabled = false;
                        UserPw.Enabled = false;

                        Debug.WriteLine(ApiToken.Token);


                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("아이디 또는 비밀번호를 확인하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show($"로그인 실패: {response.StatusCode}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"로그인 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            //------------------------------------------------------------------------------------
            // JWT 유효성 검증
            ClaimsPrincipal? principal = JwtHelper.ValidateToken(ApiToken.Token, User.publicKey);
            if (principal is null)
            {
                Console.WriteLine($"[JWT] 토큰이 유효하지 않습니다. 잘못된 토큰 또는 만료");
                return;
            }

            var cruxClaim = CruxClaim.FromClaims(principal.Claims);
            ApiToken.role = cruxClaim.Roles;

            //---------------------------------------------------------------------------------------

            UserUpdated?.Invoke();
        }
    }
}
