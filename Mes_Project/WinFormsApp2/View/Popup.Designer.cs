
namespace WinFormsApp2
{
    partial class Popup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            label4 = new Label();
            userCombo = new ComboBox();
            label2 = new Label();
            label1 = new Label();
            label3 = new Label();
            label5 = new Label();
            MachineLbl = new Label();
            PortCombo = new ComboBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            DisconnectBtn = new Button();
            ConnectionBtn = new Button();
            tableLayoutPanel3 = new TableLayoutPanel();
            button3 = new Button();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35.22267F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 64.77733F));
            tableLayoutPanel1.Controls.Add(label4, 0, 2);
            tableLayoutPanel1.Controls.Add(userCombo, 1, 3);
            tableLayoutPanel1.Controls.Add(label2, 0, 3);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(label3, 0, 1);
            tableLayoutPanel1.Controls.Add(label5, 1, 2);
            tableLayoutPanel1.Controls.Add(MachineLbl, 1, 1);
            tableLayoutPanel1.Controls.Add(PortCombo, 1, 0);
            tableLayoutPanel1.Location = new Point(24, 15);
            tableLayoutPanel1.Margin = new Padding(6);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(494, 243);
            tableLayoutPanel1.TabIndex = 0;
            tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.Font = new Font("맑은 고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label4.Location = new Point(23, 129);
            label4.Margin = new Padding(6, 0, 6, 0);
            label4.Name = "label4";
            label4.Size = new Size(127, 41);
            label4.TabIndex = 9;
            label4.Text = "설비명 :";
            // 
            // userCombo
            // 
            userCombo.Dock = DockStyle.Fill;
            userCombo.FormattingEnabled = true;
            userCombo.Location = new Point(180, 186);
            userCombo.Margin = new Padding(6);
            userCombo.Name = "userCombo";
            userCombo.Size = new Size(308, 40);
            userCombo.TabIndex = 8;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label2.Location = new Point(23, 191);
            label2.Margin = new Padding(6, 0, 6, 0);
            label2.Name = "label2";
            label2.Size = new Size(127, 41);
            label2.TabIndex = 7;
            label2.Text = "작업자 :";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label1.Location = new Point(37, 9);
            label1.Margin = new Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new Size(100, 41);
            label1.TabIndex = 0;
            label1.Text = "PLC : ";
            label1.Click += label1_Click;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Font = new Font("맑은 고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label3.Location = new Point(8, 69);
            label3.Margin = new Padding(6, 0, 6, 0);
            label3.Name = "label3";
            label3.Size = new Size(157, 41);
            label3.TabIndex = 2;
            label3.Text = "온도센서 :";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.AutoSize = true;
            label5.Font = new Font("맑은 고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label5.ForeColor = Color.Yellow;
            label5.Location = new Point(229, 129);
            label5.Margin = new Padding(6, 0, 6, 0);
            label5.Name = "label5";
            label5.Size = new Size(209, 41);
            label5.TabIndex = 10;
            label5.Text = "설비명 바인딩";
            // 
            // MachineLbl
            // 
            MachineLbl.Anchor = AnchorStyles.None;
            MachineLbl.AutoSize = true;
            MachineLbl.Font = new Font("맑은 고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            MachineLbl.ForeColor = Color.Yellow;
            MachineLbl.Location = new Point(334, 69);
            MachineLbl.Margin = new Padding(6, 0, 6, 0);
            MachineLbl.Name = "MachineLbl";
            MachineLbl.Size = new Size(0, 41);
            MachineLbl.TabIndex = 3;
            // 
            // PortCombo
            // 
            PortCombo.Dock = DockStyle.Fill;
            PortCombo.FormattingEnabled = true;
            PortCombo.Location = new Point(180, 6);
            PortCombo.Margin = new Padding(6);
            PortCombo.Name = "PortCombo";
            PortCombo.Size = new Size(308, 40);
            PortCombo.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(DisconnectBtn, 1, 0);
            tableLayoutPanel2.Controls.Add(ConnectionBtn, 0, 0);
            tableLayoutPanel2.Location = new Point(30, 398);
            tableLayoutPanel2.Margin = new Padding(6);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(488, 90);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // DisconnectBtn
            // 
            DisconnectBtn.Dock = DockStyle.Fill;
            DisconnectBtn.Location = new Point(250, 6);
            DisconnectBtn.Margin = new Padding(6);
            DisconnectBtn.Name = "DisconnectBtn";
            DisconnectBtn.Size = new Size(232, 78);
            DisconnectBtn.TabIndex = 1;
            DisconnectBtn.Text = "해제";
            DisconnectBtn.UseVisualStyleBackColor = true;
            DisconnectBtn.Click += DisconnectBtn_Click;
            // 
            // ConnectionBtn
            // 
            ConnectionBtn.Dock = DockStyle.Fill;
            ConnectionBtn.Location = new Point(6, 6);
            ConnectionBtn.Margin = new Padding(6);
            ConnectionBtn.Name = "ConnectionBtn";
            ConnectionBtn.Size = new Size(232, 78);
            ConnectionBtn.TabIndex = 0;
            ConnectionBtn.Text = "연결";
            ConnectionBtn.UseVisualStyleBackColor = true;
            ConnectionBtn.Click += ConnectionBtn_Click;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(button3, 0, 0);
            tableLayoutPanel3.Location = new Point(24, 500);
            tableLayoutPanel3.Margin = new Padding(6);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new Size(494, 76);
            tableLayoutPanel3.TabIndex = 2;
            // 
            // button3
            // 
            button3.Dock = DockStyle.Fill;
            button3.Location = new Point(6, 6);
            button3.Margin = new Padding(6);
            button3.Name = "button3";
            button3.Size = new Size(482, 64);
            button3.TabIndex = 0;
            button3.Text = "적용";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // Popup
            // 
            AutoScaleDimensions = new SizeF(14F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(549, 618);
            Controls.Add(tableLayoutPanel3);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(6);
            Name = "Popup";
            Text = "Popup";
            Load += Popup_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            ResumeLayout(false);
        }



        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Label MachineLbl;
        private Label label3;
        private ComboBox PortCombo;
        private TableLayoutPanel tableLayoutPanel2;
        private Button DisconnectBtn;
        private Button ConnectionBtn;
        private TableLayoutPanel tableLayoutPanel3;
        private Button button3;
        private Label label5;
        private Label label4;
        private ComboBox userCombo;
        private Label label2;
    }
}