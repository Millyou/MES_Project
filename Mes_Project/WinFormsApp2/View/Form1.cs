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
        private bool isAutoMode = true; // ���� ��带 ������ ����

        public static int mode { get; set; }

        private readonly ActUtlType _plc = new ActUtlType(); // MX Component ��ü
        public Form1()
        {
            InitializeComponent();
            _plcModel = new PlcModel(); // Plc ������� ����
            _mainPresenter = new MainPresenter(this); //����ð� �ҷ�����


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
            if (Popup.StationNumber != null)
            {
                EndBt.Enabled = true;
                StartBt.Enabled = false;
                StartBt.BackColor = Color.Red;
                EndBt.BackColor = Color.FromArgb(224, 224, 224);
                MessageBox.Show("������ ���۵˴ϴ�.");
                _plcFuntion.StartReading(); //������ �ҷ����� ����(���� ������)

            }
            else MessageBox.Show("PLC ���� ���¸� Ȯ�� ���ּ���.");
        }

        private void EndBt_Click(object sender, EventArgs e)
        {
            if (StartBt.Enabled != true)
            {
                stopLbl.Text = DateTime.Now.ToString("HH:mm:ss");
                EndBt.Enabled = false;
                StartBt.Enabled = true;
                StartBt.BackColor = Color.FromArgb(224, 224, 224);
                EndBt.BackColor = Color.Red;
                _plcFuntion.StopReading(); //������ �ҷ����� ����
                MessageBox.Show("������ ����˴ϴ�.");
            }
            else MessageBox.Show("���� ������ ���������� Ȯ�� ���ּ���.");

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int tempValue = trackBar1.Value;
            tempLbl.Text = $"�µ�: {tempValue}��C";
        }

        private void label13_Click(object sender, EventArgs e)
        {
            // TextBox ����
            TextBox textBox = new TextBox
            {
                Text = label13.Text, // ���� �� ���� TextBox�� ����
                Bounds = label13.Bounds, // TextBox�� ��ġ�� ũ�⸦ �󺧰� �����ϰ� ����
                Parent = label13.Parent // ���� �θ� ��Ʈ���� ����
            };

            // TextBox�� ��Ŀ�� ����
            textBox.Focus();

            // TextBox���� ��Ŀ���� ������ ���� �ؽ�Ʈ�� ����
            textBox.Leave += (s, ev) =>
            {
                label13.Text = textBox.Text; // TextBox ���� Label�� ����
                textBox.Dispose(); // TextBox ����
            };

            // Enter Ű�� ������ ���� ���� Label�� ����
            textBox.KeyDown += (s, ev) =>
            {
                if (ev.KeyCode == Keys.Enter)
                {
                    label13.Text = textBox.Text; // TextBox ���� Label�� ����
                    textBox.Dispose(); // TextBox ����
                }
            };
        }

        private void togglebtn_Click(object sender, EventArgs e)
        {
            // ���� ���� (���)
            isAutoMode = !isAutoMode;
            
            if (EndBt.Enabled && Popup.StationNumber != null)
            {

                // ���¿� ���� ���� ����
                if (isAutoMode)
                {
                    togglebtn.Text = "�ڵ� ���"; // ��ư �ؽ�Ʈ ����
                    togglebtn.BackColor = Color.Green; // ���� ����
                    MessageBox.Show("�ڵ� ���� ��ȯ�Ǿ����ϴ�.");
                    mode = 1;
                }
                else
                {
                    togglebtn.Text = "���� ���"; // ��ư �ؽ�Ʈ ����
                    togglebtn.BackColor = Color.Orange; // ���� ����
                    MessageBox.Show("���� ���� ��ȯ�Ǿ����ϴ�.");
                    mode = 0;
                }
            }
            else MessageBox.Show("PLC ���� ���¸� Ȯ�� ���ּ���.");
        }
    }
}