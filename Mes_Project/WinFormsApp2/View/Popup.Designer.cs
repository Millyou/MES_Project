
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
            modeCombo = new ComboBox();
            label2 = new Label();
            label1 = new Label();
            label3 = new Label();
            PortCombo = new ComboBox();
            PortCombo2 = new ComboBox();
            MachineLbl = new Label();
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
            tableLayoutPanel1.Controls.Add(modeCombo, 1, 2);
            tableLayoutPanel1.Controls.Add(label2, 0, 2);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(label3, 0, 1);
            tableLayoutPanel1.Controls.Add(PortCombo, 1, 0);
            tableLayoutPanel1.Controls.Add(PortCombo2, 1, 1);
            tableLayoutPanel1.Location = new Point(15, 9);
            tableLayoutPanel1.Margin = new Padding(4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Size = new Size(318, 106);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // modeCombo
            // 
            modeCombo.Dock = DockStyle.Fill;
            modeCombo.FormattingEnabled = true;
            modeCombo.Location = new Point(116, 74);
            modeCombo.Margin = new Padding(4);
            modeCombo.Name = "modeCombo";
            modeCombo.Size = new Size(198, 28);
            modeCombo.TabIndex = 7;
            modeCombo.SelectedIndexChanged += modeCombo_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label2.Location = new Point(6, 75);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(100, 25);
            label2.TabIndex = 6;
            label2.Text = "모드선택 :";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label1.Location = new Point(23, 5);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(65, 25);
            label1.TabIndex = 0;
            label1.Text = "PLC : ";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Font = new Font("맑은 고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label3.Location = new Point(6, 40);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(100, 25);
            label3.TabIndex = 2;
            label3.Text = "온도센서 :";
            // 
            // PortCombo
            // 
            PortCombo.Dock = DockStyle.Fill;
            PortCombo.FormattingEnabled = true;
            PortCombo.Location = new Point(116, 4);
            PortCombo.Margin = new Padding(4);
            PortCombo.Name = "PortCombo";
            PortCombo.Size = new Size(198, 28);
            PortCombo.TabIndex = 5;
            // 
            // PortCombo2
            // 
            PortCombo2.Dock = DockStyle.Fill;
            PortCombo2.FormattingEnabled = true;
            PortCombo2.Location = new Point(116, 39);
            PortCombo2.Margin = new Padding(4);
            PortCombo2.Name = "PortCombo2";
            PortCombo2.Size = new Size(198, 28);
            PortCombo2.TabIndex = 5;
            // 
            // MachineLbl
            // 
            MachineLbl.Anchor = AnchorStyles.None;
            MachineLbl.AutoSize = true;
            MachineLbl.Font = new Font("맑은 고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            MachineLbl.ForeColor = Color.Yellow;
            MachineLbl.Location = new Point(215, 40);
            MachineLbl.Margin = new Padding(4, 0, 4, 0);
            MachineLbl.Name = "MachineLbl";
            MachineLbl.Size = new Size(0, 25);
            MachineLbl.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(DisconnectBtn, 1, 0);
            tableLayoutPanel2.Controls.Add(ConnectionBtn, 0, 0);
            tableLayoutPanel2.Location = new Point(13, 141);
            tableLayoutPanel2.Margin = new Padding(4);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(320, 56);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // DisconnectBtn
            // 
            DisconnectBtn.Dock = DockStyle.Fill;
            DisconnectBtn.Location = new Point(164, 4);
            DisconnectBtn.Margin = new Padding(4);
            DisconnectBtn.Name = "DisconnectBtn";
            DisconnectBtn.Size = new Size(152, 48);
            DisconnectBtn.TabIndex = 1;
            DisconnectBtn.Text = "해제";
            DisconnectBtn.UseVisualStyleBackColor = true;
            DisconnectBtn.Click += DisconnectBtn_Click;
            // 
            // ConnectionBtn
            // 
            ConnectionBtn.Dock = DockStyle.Fill;
            ConnectionBtn.Location = new Point(4, 4);
            ConnectionBtn.Margin = new Padding(4);
            ConnectionBtn.Name = "ConnectionBtn";
            ConnectionBtn.Size = new Size(152, 48);
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
            tableLayoutPanel3.Location = new Point(15, 204);
            tableLayoutPanel3.Margin = new Padding(4);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new Size(318, 48);
            tableLayoutPanel3.TabIndex = 2;
            // 
            // button3
            // 
            button3.Dock = DockStyle.Fill;
            button3.Location = new Point(4, 4);
            button3.Margin = new Padding(4);
            button3.Name = "button3";
            button3.Size = new Size(310, 40);
            button3.TabIndex = 0;
            button3.Text = "적용";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // Popup
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(353, 304);
            Controls.Add(tableLayoutPanel3);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(4);
            Name = "Popup";
            Text = "Popup";
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
        private ComboBox PortCombo2;
        private TableLayoutPanel tableLayoutPanel2;
        private Button DisconnectBtn;
        private Button ConnectionBtn;
        private TableLayoutPanel tableLayoutPanel3;
        private Button button3;
        private ComboBox modeCombo;
        private Label label2;
    }
}