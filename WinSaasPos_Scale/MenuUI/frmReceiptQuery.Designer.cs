namespace WinSaasPOS_Scale
{
    partial class frmReceiptQuery
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReceiptQuery));
            this.btnYesterday = new System.Windows.Forms.Button();
            this.btnToday = new System.Windows.Forms.Button();
            this.dtReceiptData = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvReceipt = new System.Windows.Forms.DataGridView();
            this.ReceiptData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReceiptTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cashier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NetOperat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalCash = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrintStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PosType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Reprint = new System.Windows.Forms.DataGridViewImageColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnReprintPic = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlEmptyReceipt = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlDgvHead = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.pnlHead = new System.Windows.Forms.Panel();
            this.btnOnLineType = new System.Windows.Forms.Button();
            this.btnMenu = new System.Windows.Forms.Button();
            this.btnWindows = new System.Windows.Forms.Button();
            this.lblShopName = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.btnCancle = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceipt)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlEmptyReceipt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlDgvHead.SuspendLayout();
            this.pnlHead.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnYesterday
            // 
            this.btnYesterday.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnYesterday.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.btnYesterday.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnYesterday.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnYesterday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYesterday.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnYesterday.Location = new System.Drawing.Point(242, 28);
            this.btnYesterday.Margin = new System.Windows.Forms.Padding(2);
            this.btnYesterday.Name = "btnYesterday";
            this.btnYesterday.Size = new System.Drawing.Size(60, 30);
            this.btnYesterday.TabIndex = 9;
            this.btnYesterday.Text = "昨天";
            this.btnYesterday.UseVisualStyleBackColor = true;
            this.btnYesterday.Click += new System.EventHandler(this.btnYesterday_Click);
            // 
            // btnToday
            // 
            this.btnToday.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnToday.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.btnToday.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnToday.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnToday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToday.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnToday.Location = new System.Drawing.Point(306, 28);
            this.btnToday.Margin = new System.Windows.Forms.Padding(2);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(60, 30);
            this.btnToday.TabIndex = 8;
            this.btnToday.Text = "今天";
            this.btnToday.UseVisualStyleBackColor = true;
            this.btnToday.Click += new System.EventHandler(this.btnToday_Click);
            // 
            // dtReceiptData
            // 
            this.dtReceiptData.CalendarFont = new System.Drawing.Font("微软雅黑", 18F);
            this.dtReceiptData.CustomFormat = "yyyy-MM-dd";
            this.dtReceiptData.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.dtReceiptData.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtReceiptData.Location = new System.Drawing.Point(107, 29);
            this.dtReceiptData.Margin = new System.Windows.Forms.Padding(2);
            this.dtReceiptData.Name = "dtReceiptData";
            this.dtReceiptData.Size = new System.Drawing.Size(129, 30);
            this.dtReceiptData.TabIndex = 5;
            this.dtReceiptData.Value = new System.DateTime(2019, 10, 25, 0, 0, 0, 0);
            this.dtReceiptData.CloseUp += new System.EventHandler(this.dtReceiptData_CloseUp);
            this.dtReceiptData.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dtReceiptData_MouseDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label3.Location = new System.Drawing.Point(8, 32);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "交班日期：";
            // 
            // dgvReceipt
            // 
            this.dgvReceipt.AllowUserToAddRows = false;
            this.dgvReceipt.AllowUserToDeleteRows = false;
            this.dgvReceipt.AllowUserToResizeColumns = false;
            this.dgvReceipt.AllowUserToResizeRows = false;
            this.dgvReceipt.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReceipt.BackgroundColor = System.Drawing.Color.White;
            this.dgvReceipt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvReceipt.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvReceipt.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvReceipt.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 11F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvReceipt.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvReceipt.ColumnHeadersHeight = 50;
            this.dgvReceipt.ColumnHeadersVisible = false;
            this.dgvReceipt.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ReceiptData,
            this.ReceiptTime,
            this.Cashier,
            this.NetOperat,
            this.TotalAmount,
            this.TotalCash,
            this.PrintStatus,
            this.PosType,
            this.Reprint});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("微软雅黑", 11F);
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvReceipt.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgvReceipt.GridColor = System.Drawing.Color.LightGray;
            this.dgvReceipt.Location = new System.Drawing.Point(13, 223);
            this.dgvReceipt.Margin = new System.Windows.Forms.Padding(2);
            this.dgvReceipt.Name = "dgvReceipt";
            this.dgvReceipt.ReadOnly = true;
            this.dgvReceipt.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvReceipt.RowHeadersVisible = false;
            this.dgvReceipt.RowHeadersWidth = 40;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.dgvReceipt.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvReceipt.RowTemplate.Height = 80;
            this.dgvReceipt.Size = new System.Drawing.Size(1152, 539);
            this.dgvReceipt.TabIndex = 0;
            this.dgvReceipt.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReceipt_CellContentClick);
            // 
            // ReceiptData
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.ReceiptData.DefaultCellStyle = dataGridViewCellStyle2;
            this.ReceiptData.FillWeight = 95F;
            this.ReceiptData.HeaderText = "交班日期";
            this.ReceiptData.Name = "ReceiptData";
            this.ReceiptData.ReadOnly = true;
            // 
            // ReceiptTime
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 8.5F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ReceiptTime.DefaultCellStyle = dataGridViewCellStyle3;
            this.ReceiptTime.FillWeight = 125F;
            this.ReceiptTime.HeaderText = "工作时间";
            this.ReceiptTime.Name = "ReceiptTime";
            this.ReceiptTime.ReadOnly = true;
            // 
            // Cashier
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 10F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.Cashier.DefaultCellStyle = dataGridViewCellStyle4;
            this.Cashier.FillWeight = 85F;
            this.Cashier.HeaderText = "收银员";
            this.Cashier.Name = "Cashier";
            this.Cashier.ReadOnly = true;
            // 
            // NetOperat
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 10F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            this.NetOperat.DefaultCellStyle = dataGridViewCellStyle5;
            this.NetOperat.FillWeight = 95F;
            this.NetOperat.HeaderText = "营业净额";
            this.NetOperat.Name = "NetOperat";
            this.NetOperat.ReadOnly = true;
            // 
            // TotalAmount
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("微软雅黑", 10F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            this.TotalAmount.DefaultCellStyle = dataGridViewCellStyle6;
            this.TotalAmount.FillWeight = 95F;
            this.TotalAmount.HeaderText = "支付合计";
            this.TotalAmount.Name = "TotalAmount";
            this.TotalAmount.ReadOnly = true;
            // 
            // TotalCash
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("微软雅黑", 10F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            this.TotalCash.DefaultCellStyle = dataGridViewCellStyle7;
            this.TotalCash.FillWeight = 103F;
            this.TotalCash.HeaderText = "应有总现金";
            this.TotalCash.Name = "TotalCash";
            this.TotalCash.ReadOnly = true;
            // 
            // PrintStatus
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("微软雅黑", 10F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            this.PrintStatus.DefaultCellStyle = dataGridViewCellStyle8;
            this.PrintStatus.FillWeight = 90F;
            this.PrintStatus.HeaderText = "打印状态";
            this.PrintStatus.Name = "PrintStatus";
            this.PrintStatus.ReadOnly = true;
            this.PrintStatus.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // PosType
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("微软雅黑", 10F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            this.PosType.DefaultCellStyle = dataGridViewCellStyle9;
            this.PosType.FillWeight = 95F;
            this.PosType.HeaderText = "收银模式";
            this.PosType.Name = "PosType";
            this.PosType.ReadOnly = true;
            // 
            // Reprint
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("微软雅黑", 12F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle10.NullValue")));
            this.Reprint.DefaultCellStyle = dataGridViewCellStyle10;
            this.Reprint.FillWeight = 110F;
            this.Reprint.HeaderText = "操作";
            this.Reprint.Name = "Reprint";
            this.Reprint.ReadOnly = true;
            this.Reprint.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btnReprintPic);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnYesterday);
            this.panel1.Controls.Add(this.btnToday);
            this.panel1.Controls.Add(this.dtReceiptData);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(13, 79);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1152, 85);
            this.panel1.TabIndex = 2;
            // 
            // btnReprintPic
            // 
            this.btnReprintPic.BackColor = System.Drawing.Color.Transparent;
            this.btnReprintPic.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReprintPic.BackgroundImage")));
            this.btnReprintPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReprintPic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReprintPic.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnReprintPic.ForeColor = System.Drawing.Color.White;
            this.btnReprintPic.Location = new System.Drawing.Point(844, 30);
            this.btnReprintPic.Margin = new System.Windows.Forms.Padding(2);
            this.btnReprintPic.Name = "btnReprintPic";
            this.btnReprintPic.Size = new System.Drawing.Size(120, 40);
            this.btnReprintPic.TabIndex = 15;
            this.btnReprintPic.Text = "重打交班单";
            this.btnReprintPic.UseVisualStyleBackColor = false;
            this.btnReprintPic.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(371, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 20);
            this.label1.TabIndex = 13;
            this.label1.Text = "仅支持查询最近7天的交班记录";
            // 
            // pnlEmptyReceipt
            // 
            this.pnlEmptyReceipt.BackColor = System.Drawing.Color.White;
            this.pnlEmptyReceipt.Controls.Add(this.label8);
            this.pnlEmptyReceipt.Controls.Add(this.pictureBox1);
            this.pnlEmptyReceipt.Location = new System.Drawing.Point(379, 398);
            this.pnlEmptyReceipt.Margin = new System.Windows.Forms.Padding(2);
            this.pnlEmptyReceipt.Name = "pnlEmptyReceipt";
            this.pnlEmptyReceipt.Size = new System.Drawing.Size(353, 96);
            this.pnlEmptyReceipt.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label8.ForeColor = System.Drawing.Color.Gray;
            this.label8.Location = new System.Drawing.Point(88, 39);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(244, 24);
            this.label8.TabIndex = 9;
            this.label8.Text = "没有符合查询条件的交班记录";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(35, 21);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(49, 55);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // pnlDgvHead
            // 
            this.pnlDgvHead.BackColor = System.Drawing.Color.White;
            this.pnlDgvHead.Controls.Add(this.label4);
            this.pnlDgvHead.Controls.Add(this.label2);
            this.pnlDgvHead.Controls.Add(this.label12);
            this.pnlDgvHead.Controls.Add(this.label6);
            this.pnlDgvHead.Controls.Add(this.label5);
            this.pnlDgvHead.Controls.Add(this.label7);
            this.pnlDgvHead.Controls.Add(this.label9);
            this.pnlDgvHead.Controls.Add(this.label10);
            this.pnlDgvHead.Controls.Add(this.label11);
            this.pnlDgvHead.Location = new System.Drawing.Point(13, 164);
            this.pnlDgvHead.Name = "pnlDgvHead";
            this.pnlDgvHead.Size = new System.Drawing.Size(1152, 58);
            this.pnlDgvHead.TabIndex = 33;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(916, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 23);
            this.label4.TabIndex = 40;
            this.label4.Text = "收银模式";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(797, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 23);
            this.label2.TabIndex = 39;
            this.label2.Text = "打印状态";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(666, 24);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(95, 23);
            this.label12.TabIndex = 38;
            this.label12.Text = "应有总现金";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(1057, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 23);
            this.label6.TabIndex = 37;
            this.label6.Text = "操作";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(541, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 23);
            this.label5.TabIndex = 36;
            this.label5.Text = "支付合计";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(418, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 23);
            this.label7.TabIndex = 35;
            this.label7.Text = "营业净额";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(311, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 23);
            this.label9.TabIndex = 34;
            this.label9.Text = "收银员";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(166, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 23);
            this.label10.TabIndex = 33;
            this.label10.Text = "工作时间";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(20, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 23);
            this.label11.TabIndex = 32;
            this.label11.Text = "交班日期";
            // 
            // pnlHead
            // 
            this.pnlHead.BackColor = System.Drawing.Color.Black;
            this.pnlHead.Controls.Add(this.btnOnLineType);
            this.pnlHead.Controls.Add(this.btnMenu);
            this.pnlHead.Controls.Add(this.btnWindows);
            this.pnlHead.Controls.Add(this.lblShopName);
            this.pnlHead.Controls.Add(this.lblTime);
            this.pnlHead.Controls.Add(this.btnCancle);
            this.pnlHead.Location = new System.Drawing.Point(0, 0);
            this.pnlHead.Name = "pnlHead";
            this.pnlHead.Size = new System.Drawing.Size(1178, 60);
            this.pnlHead.TabIndex = 36;
            // 
            // btnOnLineType
            // 
            this.btnOnLineType.BackColor = System.Drawing.Color.Black;
            this.btnOnLineType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOnLineType.BackgroundImage")));
            this.btnOnLineType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOnLineType.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnOnLineType.FlatAppearance.BorderSize = 0;
            this.btnOnLineType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOnLineType.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnOnLineType.ForeColor = System.Drawing.Color.White;
            this.btnOnLineType.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnOnLineType.Location = new System.Drawing.Point(268, 17);
            this.btnOnLineType.Name = "btnOnLineType";
            this.btnOnLineType.Size = new System.Drawing.Size(60, 25);
            this.btnOnLineType.TabIndex = 45;
            this.btnOnLineType.Text = "   在线";
            this.btnOnLineType.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnOnLineType.UseVisualStyleBackColor = false;
            this.btnOnLineType.Visible = false;
            // 
            // btnMenu
            // 
            this.btnMenu.AutoSize = true;
            this.btnMenu.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMenu.BackColor = System.Drawing.Color.Black;
            this.btnMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMenu.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnMenu.FlatAppearance.BorderSize = 0;
            this.btnMenu.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.btnMenu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.btnMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenu.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnMenu.ForeColor = System.Drawing.Color.White;
            this.btnMenu.Image = ((System.Drawing.Image)(resources.GetObject("btnMenu.Image")));
            this.btnMenu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMenu.Location = new System.Drawing.Point(1033, 15);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(142, 30);
            this.btnMenu.TabIndex = 44;
            this.btnMenu.Text = "某某某，你好  ∨";
            this.btnMenu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMenu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMenu.UseVisualStyleBackColor = false;
            // 
            // btnWindows
            // 
            this.btnWindows.BackColor = System.Drawing.Color.Black;
            this.btnWindows.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnWindows.BackgroundImage")));
            this.btnWindows.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnWindows.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnWindows.FlatAppearance.BorderSize = 0;
            this.btnWindows.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWindows.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnWindows.ForeColor = System.Drawing.Color.White;
            this.btnWindows.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnWindows.Location = new System.Drawing.Point(8, 13);
            this.btnWindows.Name = "btnWindows";
            this.btnWindows.Size = new System.Drawing.Size(37, 31);
            this.btnWindows.TabIndex = 43;
            this.btnWindows.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnWindows.UseVisualStyleBackColor = false;
            this.btnWindows.Visible = false;
            this.btnWindows.Click += new System.EventHandler(this.btnWindows_Click);
            // 
            // lblShopName
            // 
            this.lblShopName.AutoSize = true;
            this.lblShopName.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblShopName.ForeColor = System.Drawing.Color.White;
            this.lblShopName.Location = new System.Drawing.Point(220, 19);
            this.lblShopName.Name = "lblShopName";
            this.lblShopName.Size = new System.Drawing.Size(42, 21);
            this.lblShopName.TabIndex = 42;
            this.lblShopName.Text = "店铺";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblTime.ForeColor = System.Drawing.Color.White;
            this.lblTime.Location = new System.Drawing.Point(51, 19);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(163, 21);
            this.lblTime.TabIndex = 41;
            this.lblTime.Text = "2019-10-10 12:12:39";
            // 
            // btnCancle
            // 
            this.btnCancle.BackColor = System.Drawing.Color.Black;
            this.btnCancle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancle.BackgroundImage")));
            this.btnCancle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancle.FlatAppearance.BorderSize = 0;
            this.btnCancle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancle.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btnCancle.ForeColor = System.Drawing.Color.White;
            this.btnCancle.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnCancle.Location = new System.Drawing.Point(901, 16);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(95, 28);
            this.btnCancle.TabIndex = 34;
            this.btnCancle.Text = "返回";
            this.btnCancle.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnCancle.UseVisualStyleBackColor = false;
            this.btnCancle.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmReceiptQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1178, 760);
            this.Controls.Add(this.pnlHead);
            this.Controls.Add(this.pnlEmptyReceipt);
            this.Controls.Add(this.pnlDgvHead);
            this.Controls.Add(this.dgvReceipt);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "frmReceiptQuery";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmReceiptQuery";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.frmReceiptQuery_Shown);
            this.SizeChanged += new System.EventHandler(this.frmReceiptQuery_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceipt)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlEmptyReceipt.ResumeLayout(false);
            this.pnlEmptyReceipt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlDgvHead.ResumeLayout(false);
            this.pnlDgvHead.PerformLayout();
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnYesterday;
        private System.Windows.Forms.Button btnToday;
        private System.Windows.Forms.DateTimePicker dtReceiptData;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvReceipt;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlEmptyReceipt;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel pnlDgvHead;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel pnlHead;
        private System.Windows.Forms.Button btnMenu;
        private System.Windows.Forms.Button btnWindows;
        private System.Windows.Forms.Label lblShopName;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Button btnReprintPic;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceiptData;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceiptTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cashier;
        private System.Windows.Forms.DataGridViewTextBoxColumn NetOperat;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalCash;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrintStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn PosType;
        private System.Windows.Forms.DataGridViewImageColumn Reprint;
        private System.Windows.Forms.Button btnOnLineType;
    }
}