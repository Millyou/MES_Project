namespace WinFormsApp2.View
{
    partial class Order
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
            tableLayoutPanel2 = new TableLayoutPanel();
            label4 = new Label();
            label1 = new Label();
            button1 = new Button();
            dateTimePicker1 = new DateTimePicker();
            dateTimePicker2 = new DateTimePicker();
            dataGridView1 = new DataGridView();
            제품코드 = new DataGridViewTextBoxColumn();
            제품명 = new DataGridViewTextBoxColumn();
            Lot = new DataGridViewTextBoxColumn();
            목표수량 = new DataGridViewTextBoxColumn();
            완료수량 = new DataGridViewTextBoxColumn();
            남은수량 = new DataGridViewTextBoxColumn();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.5799007F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(dataGridView1, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 8.917954F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 91.08205F));
            tableLayoutPanel1.Size = new Size(1291, 823);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.ColumnCount = 5;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27.5F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27.5F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel2.Controls.Add(label4, 2, 0);
            tableLayoutPanel2.Controls.Add(label1, 0, 0);
            tableLayoutPanel2.Controls.Add(button1, 4, 0);
            tableLayoutPanel2.Controls.Add(dateTimePicker1, 1, 0);
            tableLayoutPanel2.Controls.Add(dateTimePicker2, 3, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(1285, 67);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = DockStyle.Fill;
            label4.Location = new Point(613, 0);
            label4.Name = "label4";
            label4.Size = new Size(122, 67);
            label4.TabIndex = 6;
            label4.Text = "~";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(251, 67);
            label1.TabIndex = 0;
            label1.Text = "작업지시";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            button1.Dock = DockStyle.Fill;
            button1.Location = new Point(1094, 3);
            button1.Name = "button1";
            button1.Size = new Size(188, 61);
            button1.TabIndex = 1;
            button1.Text = "조회";
            button1.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Anchor = AnchorStyles.None;
            dateTimePicker1.Location = new Point(268, 14);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(330, 39);
            dateTimePicker1.TabIndex = 2;
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.Anchor = AnchorStyles.None;
            dateTimePicker2.Location = new Point(749, 14);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(330, 39);
            dateTimePicker2.TabIndex = 3;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { 제품코드, 제품명, Lot, 목표수량, 완료수량, 남은수량 });
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(3, 76);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 82;
            dataGridView1.Size = new Size(1285, 744);
            dataGridView1.TabIndex = 1;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // 제품코드
            // 
            제품코드.HeaderText = "제품코드";
            제품코드.MinimumWidth = 10;
            제품코드.Name = "제품코드";
            제품코드.Width = 200;
            // 
            // 제품명
            // 
            제품명.HeaderText = "제품명";
            제품명.MinimumWidth = 10;
            제품명.Name = "제품명";
            제품명.Width = 200;
            // 
            // Lot
            // 
            Lot.HeaderText = "Lot";
            Lot.MinimumWidth = 10;
            Lot.Name = "Lot";
            Lot.Width = 200;
            // 
            // 목표수량
            // 
            목표수량.HeaderText = "목표수량";
            목표수량.MinimumWidth = 10;
            목표수량.Name = "목표수량";
            목표수량.Width = 200;
            // 
            // 완료수량
            // 
            완료수량.HeaderText = "완료수량";
            완료수량.MinimumWidth = 10;
            완료수량.Name = "완료수량";
            완료수량.Width = 200;
            // 
            // 남은수량
            // 
            남은수량.HeaderText = "남은수량";
            남은수량.MinimumWidth = 10;
            남은수량.Name = "남은수량";
            남은수량.Width = 200;
            // 
            // Order
            // 
            AutoScaleDimensions = new SizeF(14F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1291, 823);
            Controls.Add(tableLayoutPanel1);
            Name = "Order";
            Text = "Order";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label1;
        private Button button1;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn 제품코드;
        private DataGridViewTextBoxColumn 제품명;
        private DataGridViewTextBoxColumn Lot;
        private DataGridViewTextBoxColumn 목표수량;
        private DataGridViewTextBoxColumn 완료수량;
        private DataGridViewTextBoxColumn 남은수량;
        private Label label4;
        private DateTimePicker dateTimePicker1;
        private DateTimePicker dateTimePicker2;
    }
}