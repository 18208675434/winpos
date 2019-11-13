namespace QiandamaPOS
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReceiptQuery));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnYesterday = new System.Windows.Forms.Button();
            this.btnToday = new System.Windows.Forms.Button();
            this.dtReceiptData = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvReceipt = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlEmptyReceipt = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.btnMenu = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.btnCancle = new System.Windows.Forms.ToolStripButton();
            this.lblTime = new System.Windows.Forms.ToolStripLabel();
            this.lblShopName = new System.Windows.Forms.ToolStripLabel();
            this.timerNow = new System.Windows.Forms.Timer(this.components);
            this.ReceiptData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReceiptTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cashier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NetOperat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalCash = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrintStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PosType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Reprint = new System.Windows.Forms.DataGridViewLinkColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceipt)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlEmptyReceipt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.toolStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.LightSalmon;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(1014, 25);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(99, 43);
            this.btnExit.TabIndex = 12;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Visible = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnYesterday
            // 
            this.btnYesterday.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
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
            this.btnToday.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
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
            this.dtReceiptData.ValueChanged += new System.EventHandler(this.dtReceiptData_ValueChanged);
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
            this.label3.Text = "交班时间：";
            // 
            // dgvReceipt
            // 
            this.dgvReceipt.AllowUserToAddRows = false;
            this.dgvReceipt.AllowUserToDeleteRows = false;
            this.dgvReceipt.AllowUserToResizeColumns = false;
            this.dgvReceipt.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            this.dgvReceipt.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvReceipt.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReceipt.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvReceipt.BackgroundColor = System.Drawing.Color.White;
            this.dgvReceipt.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 11F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvReceipt.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvReceipt.ColumnHeadersHeight = 50;
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
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("微软雅黑", 11F);
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvReceipt.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgvReceipt.GridColor = System.Drawing.Color.White;
            this.dgvReceipt.Location = new System.Drawing.Point(13, 172);
            this.dgvReceipt.Margin = new System.Windows.Forms.Padding(2);
            this.dgvReceipt.Name = "dgvReceipt";
            this.dgvReceipt.ReadOnly = true;
            this.dgvReceipt.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvReceipt.RowHeadersVisible = false;
            this.dgvReceipt.RowHeadersWidth = 40;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.dgvReceipt.RowsDefaultCellStyle = dataGridViewCellStyle13;
            this.dgvReceipt.RowTemplate.Height = 23;
            this.dgvReceipt.Size = new System.Drawing.Size(1152, 577);
            this.dgvReceipt.TabIndex = 0;
            this.dgvReceipt.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReceipt_CellContentClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnExit);
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
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(35, 21);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(49, 55);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // toolStripMain
            // 
            this.toolStripMain.AutoSize = false;
            this.toolStripMain.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.toolStripMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnMenu,
            this.toolStripLabel1,
            this.btnCancle,
            this.lblTime,
            this.lblShopName});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(1178, 60);
            this.toolStripMain.TabIndex = 15;
            this.toolStripMain.Text = "取消交易";
            // 
            // btnMenu
            // 
            this.btnMenu.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnMenu.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnMenu.ForeColor = System.Drawing.Color.White;
            this.btnMenu.Image = ((System.Drawing.Image)(resources.GetObject("btnMenu.Image")));
            this.btnMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(127, 57);
            this.btnMenu.Text = "某某某，你好";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(32, 57);
            this.toolStripLabel1.Text = "      ";
            this.toolStripLabel1.ToolTipText = "54435435";
            // 
            // btnCancle
            // 
            this.btnCancle.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnCancle.AutoSize = false;
            this.btnCancle.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnCancle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancle.BackgroundImage")));
            this.btnCancle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnCancle.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnCancle.ForeColor = System.Drawing.Color.White;
            this.btnCancle.Image = ((System.Drawing.Image)(resources.GetObject("btnCancle.Image")));
            this.btnCancle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancle.Margin = new System.Windows.Forms.Padding(0, 1, 15, 2);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(78, 35);
            this.btnCancle.Text = "返回";
            this.btnCancle.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblTime
            // 
            this.lblTime.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblTime.ForeColor = System.Drawing.Color.White;
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(163, 57);
            this.lblTime.Text = "2019-10-10 12:12:39";
            // 
            // lblShopName
            // 
            this.lblShopName.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblShopName.ForeColor = System.Drawing.Color.White;
            this.lblShopName.Name = "lblShopName";
            this.lblShopName.Size = new System.Drawing.Size(90, 57);
            this.lblShopName.Text = "天河东路店";
            // 
            // timerNow
            // 
            this.timerNow.Tick += new System.EventHandler(this.timerNow_Tick);
            // 
            // ReceiptData
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ReceiptData.DefaultCellStyle = dataGridViewCellStyle3;
            this.ReceiptData.FillWeight = 95F;
            this.ReceiptData.HeaderText = "交班日期";
            this.ReceiptData.Name = "ReceiptData";
            this.ReceiptData.ReadOnly = true;
            // 
            // ReceiptTime
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 10F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ReceiptTime.DefaultCellStyle = dataGridViewCellStyle4;
            this.ReceiptTime.FillWeight = 120F;
            this.ReceiptTime.HeaderText = "工作时间";
            this.ReceiptTime.Name = "ReceiptTime";
            this.ReceiptTime.ReadOnly = true;
            // 
            // Cashier
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.Cashier.DefaultCellStyle = dataGridViewCellStyle5;
            this.Cashier.FillWeight = 85F;
            this.Cashier.HeaderText = "收银员";
            this.Cashier.Name = "Cashier";
            this.Cashier.ReadOnly = true;
            // 
            // NetOperat
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.NetOperat.DefaultCellStyle = dataGridViewCellStyle6;
            this.NetOperat.FillWeight = 95F;
            this.NetOperat.HeaderText = "营业净额";
            this.NetOperat.Name = "NetOperat";
            this.NetOperat.ReadOnly = true;
            // 
            // TotalAmount
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.TotalAmount.DefaultCellStyle = dataGridViewCellStyle7;
            this.TotalAmount.FillWeight = 95F;
            this.TotalAmount.HeaderText = "支付合计";
            this.TotalAmount.Name = "TotalAmount";
            this.TotalAmount.ReadOnly = true;
            // 
            // TotalCash
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.TotalCash.DefaultCellStyle = dataGridViewCellStyle8;
            this.TotalCash.FillWeight = 103F;
            this.TotalCash.HeaderText = "应有总现金";
            this.TotalCash.Name = "TotalCash";
            this.TotalCash.ReadOnly = true;
            // 
            // PrintStatus
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("微软雅黑", 10F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            this.PrintStatus.DefaultCellStyle = dataGridViewCellStyle9;
            this.PrintStatus.FillWeight = 95F;
            this.PrintStatus.HeaderText = "打印状态";
            this.PrintStatus.Name = "PrintStatus";
            this.PrintStatus.ReadOnly = true;
            this.PrintStatus.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // PosType
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.PosType.DefaultCellStyle = dataGridViewCellStyle10;
            this.PosType.FillWeight = 95F;
            this.PosType.HeaderText = "收银模式";
            this.PosType.Name = "PosType";
            this.PosType.ReadOnly = true;
            // 
            // Reprint
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("微软雅黑", 12F);
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.White;
            this.Reprint.DefaultCellStyle = dataGridViewCellStyle11;
            this.Reprint.FillWeight = 110F;
            this.Reprint.HeaderText = "操作";
            this.Reprint.Name = "Reprint";
            this.Reprint.ReadOnly = true;
            this.Reprint.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // frmReceiptQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1178, 760);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.pnlEmptyReceipt);
            this.Controls.Add(this.dgvReceipt);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "frmReceiptQuery";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmReceiptQuery";
            this.Shown += new System.EventHandler(this.frmReceiptQuery_Shown);
            this.SizeChanged += new System.EventHandler(this.frmReceiptQuery_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceipt)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlEmptyReceipt.ResumeLayout(false);
            this.pnlEmptyReceipt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
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
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton btnMenu;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton btnCancle;
        private System.Windows.Forms.ToolStripLabel lblTime;
        private System.Windows.Forms.ToolStripLabel lblShopName;
        private System.Windows.Forms.Timer timerNow;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceiptData;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceiptTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cashier;
        private System.Windows.Forms.DataGridViewTextBoxColumn NetOperat;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalCash;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrintStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn PosType;
        private System.Windows.Forms.DataGridViewLinkColumn Reprint;
    }
}