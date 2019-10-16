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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
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
            this.Reprint = new System.Windows.Forms.DataGridViewLinkColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceipt)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.LightSalmon;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(718, 36);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(99, 43);
            this.btnExit.TabIndex = 12;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnQuery.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.btnQuery.ForeColor = System.Drawing.Color.White;
            this.btnQuery.Location = new System.Drawing.Point(595, 36);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(2);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(99, 43);
            this.btnQuery.TabIndex = 11;
            this.btnQuery.Text = "查 询";
            this.btnQuery.UseVisualStyleBackColor = false;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnYesterday
            // 
            this.btnYesterday.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnYesterday.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnYesterday.Location = new System.Drawing.Point(479, 37);
            this.btnYesterday.Margin = new System.Windows.Forms.Padding(2);
            this.btnYesterday.Name = "btnYesterday";
            this.btnYesterday.Size = new System.Drawing.Size(63, 33);
            this.btnYesterday.TabIndex = 9;
            this.btnYesterday.Text = "昨天";
            this.btnYesterday.UseVisualStyleBackColor = true;
            this.btnYesterday.Click += new System.EventHandler(this.btnYesterday_Click);
            // 
            // btnToday
            // 
            this.btnToday.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnToday.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnToday.Location = new System.Drawing.Point(390, 37);
            this.btnToday.Margin = new System.Windows.Forms.Padding(2);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(63, 33);
            this.btnToday.TabIndex = 8;
            this.btnToday.Text = "今天";
            this.btnToday.UseVisualStyleBackColor = true;
            this.btnToday.Click += new System.EventHandler(this.btnToday_Click);
            // 
            // dtReceiptData
            // 
            this.dtReceiptData.CalendarFont = new System.Drawing.Font("微软雅黑", 16F);
            this.dtReceiptData.CustomFormat = "yyyy-MM-dd";
            this.dtReceiptData.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.dtReceiptData.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtReceiptData.Location = new System.Drawing.Point(121, 40);
            this.dtReceiptData.Margin = new System.Windows.Forms.Padding(2);
            this.dtReceiptData.Name = "dtReceiptData";
            this.dtReceiptData.Size = new System.Drawing.Size(209, 32);
            this.dtReceiptData.TabIndex = 5;
            this.dtReceiptData.Value = new System.DateTime(2019, 9, 24, 16, 10, 45, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label3.Location = new System.Drawing.Point(22, 43);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "交班时间：";
            // 
            // dgvReceipt
            // 
            this.dgvReceipt.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.dgvReceipt.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvReceipt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvReceipt.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvReceipt.BackgroundColor = System.Drawing.Color.White;
            this.dgvReceipt.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
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
            this.dgvReceipt.GridColor = System.Drawing.Color.White;
            this.dgvReceipt.Location = new System.Drawing.Point(10, 122);
            this.dgvReceipt.Margin = new System.Windows.Forms.Padding(2);
            this.dgvReceipt.Name = "dgvReceipt";
            this.dgvReceipt.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvReceipt.RowHeadersWidth = 40;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.dgvReceipt.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvReceipt.RowTemplate.Height = 23;
            this.dgvReceipt.Size = new System.Drawing.Size(847, 591);
            this.dgvReceipt.TabIndex = 0;
            this.dgvReceipt.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReceipt_CellContentClick);
            // 
            // ReceiptData
            // 
            this.ReceiptData.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ReceiptData.FillWeight = 100.195F;
            this.ReceiptData.HeaderText = "交班日期";
            this.ReceiptData.Name = "ReceiptData";
            this.ReceiptData.Width = 95;
            // 
            // ReceiptTime
            // 
            this.ReceiptTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.ReceiptTime.DefaultCellStyle = dataGridViewCellStyle2;
            this.ReceiptTime.FillWeight = 100.195F;
            this.ReceiptTime.HeaderText = "工作时间";
            this.ReceiptTime.Name = "ReceiptTime";
            // 
            // Cashier
            // 
            this.Cashier.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Cashier.FillWeight = 100.195F;
            this.Cashier.HeaderText = "收银员";
            this.Cashier.Name = "Cashier";
            this.Cashier.Width = 85;
            // 
            // NetOperat
            // 
            this.NetOperat.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.NetOperat.FillWeight = 100.195F;
            this.NetOperat.HeaderText = "营业净额";
            this.NetOperat.Name = "NetOperat";
            // 
            // TotalAmount
            // 
            this.TotalAmount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TotalAmount.FillWeight = 100.195F;
            this.TotalAmount.HeaderText = "支付合计";
            this.TotalAmount.Name = "TotalAmount";
            // 
            // TotalCash
            // 
            this.TotalCash.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TotalCash.FillWeight = 115.8369F;
            this.TotalCash.HeaderText = "应有总现金";
            this.TotalCash.Name = "TotalCash";
            this.TotalCash.Width = 110;
            // 
            // PrintStatus
            // 
            this.PrintStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 16F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            this.PrintStatus.DefaultCellStyle = dataGridViewCellStyle3;
            this.PrintStatus.FillWeight = 101.9699F;
            this.PrintStatus.HeaderText = "打印状态";
            this.PrintStatus.Name = "PrintStatus";
            this.PrintStatus.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // PosType
            // 
            this.PosType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.PosType.HeaderText = "收银模式";
            this.PosType.Name = "PosType";
            // 
            // Reprint
            // 
            this.Reprint.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 16F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            this.Reprint.DefaultCellStyle = dataGridViewCellStyle4;
            this.Reprint.FillWeight = 81.21828F;
            this.Reprint.HeaderText = "操作";
            this.Reprint.Name = "Reprint";
            this.Reprint.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Reprint.Width = 130;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this.btnQuery);
            this.panel1.Controls.Add(this.btnYesterday);
            this.panel1.Controls.Add(this.btnToday);
            this.panel1.Controls.Add(this.dtReceiptData);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(10, 12);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(847, 102);
            this.panel1.TabIndex = 2;
            // 
            // frmReceiptQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 724);
            this.Controls.Add(this.dgvReceipt);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "frmReceiptQuery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmReceiptQuery";
            this.SizeChanged += new System.EventHandler(this.frmReceiptQuery_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceipt)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnYesterday;
        private System.Windows.Forms.Button btnToday;
        private System.Windows.Forms.DateTimePicker dtReceiptData;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvReceipt;
        private System.Windows.Forms.Panel panel1;
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