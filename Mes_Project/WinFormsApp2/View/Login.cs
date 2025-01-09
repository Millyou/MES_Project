using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace WinFormsApp2.View
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

            // 이벤트를 코드로 연결
            textBox1.Click += textBox1_Click;
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            // 터치 키보드 실행
            StartTouchKeyboard();
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
    }
}