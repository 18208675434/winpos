namespace QiandamaPOS
{
    partial class frmOrderHang
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvOrderOnLine = new System.Windows.Forms.DataGridView();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClearOrderHang = new System.Windows.Forms.Button();
            this.picScreen = new System.Windows.Forms.PictureBox();
            this.serialno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hangtime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reprint = new System.Windows.Forms.DataGridViewLinkColumn();
            this.cancle = new System.Windows.Forms.DataGridViewLinkColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderOnLine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picScreen)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvOrderOnLine
            // 
            this.dgvOrderOnLine.AllowUserToAddRows = false;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.LightCyan;
            this.dgvOrderOnLine.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvOrderOnLine.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrderOnLine.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvOrderOnLine.BackgroundColor = System.Drawing.Color.White;
            this.dgvOrderOnLine.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("微软雅黑", 11F);
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvOrderOnLine.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvOrderOnLine.ColumnHeadersHeight = 50;
            this.dgvOrderOnLine.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.serialno,
            this.phone,
            this.title,
            this.hangtime,
            this.reprint,
            this.cancle});
            this.dgvOrderOnLine.GridColor = System.Drawing.Color.White;
            this.dgvOrderOnLine.Location = new System.Drawing.Point(7, 81);
            this.dgvOrderOnLine.Name = "dgvOrderOnLine";
            this.dgvOrderOnLine.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvOrderOnLine.RowHeadersVisible = false;
            this.dgvOrderOnLine.RowHeadersWidth = 40;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.dgvOrderOnLine.RowsDefaultCellStyle = dataGridViewCellStyle18;
            this.dgvOrderOnLine.RowTemplate.Height = 23;
            this.dgvOrderOnLine.Size = new System.Drawing.Size(947, 505);
            this.dgvOrderOnLine.TabIndex = 1;
            this.dgvOrderOnLine.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrderOnLine_CellContentClick);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.LightSalmon;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(863, 24);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(70, 35);
            this.btnExit.TabIndex = 14;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(627, 24);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(70, 35);
            this.btnRefresh.TabIndex = 13;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClearOrderHang
            // 
            this.btnClearOrderHang.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnClearOrderHang.FlatAppearance.BorderSize = 0;
            this.btnClearOrderHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearOrderHang.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnClearOrderHang.ForeColor = System.Drawing.Color.White;
            this.btnClearOrderHang.Location = new System.Drawing.Point(735, 24);
            this.btnClearOrderHang.Name = "btnClearOrderHang";
            this.btnClearOrderHang.Size = new System.Drawing.Size(90, 35);
            this.btnClearOrderHang.TabIndex = 15;
            this.btnClearOrderHang.Text = "清空挂单";
            this.btnClearOrderHang.UseVisualStyleBackColor = false;
            this.btnClearOrderHang.Click += new System.EventHandler(this.btnClearOrderHang_Click);
            // 
            // picScreen
            // 
            this.picScreen.BackColor = System.Drawing.Color.Red;
            this.picScreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picScreen.Location = new System.Drawing.Point(0, 0);
            this.picScreen.Name = "picScreen";
            this.picScreen.Size = new System.Drawing.Size(10, 10);
            this.picScreen.TabIndex = 24;
            this.picScreen.TabStop = false;
            this.picScreen.Visible = false;
            // 
            // serialno
            // 
            this.serialno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.serialno.DefaultCellStyle = dataGridViewCellStyle12;
            this.serialno.FillWeight = 100.195F;
            this.serialno.HeaderText = "序号";
            this.serialno.Name = "serialno";
            this.serialno.Width = 85;
            // 
            // phone
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.phone.DefaultCellStyle = dataGridViewCellStyle13;
            this.phone.FillWeight = 100.195F;
            this.phone.HeaderText = "会员手机号";
            this.phone.Name = "phone";
            // 
            // title
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.title.DefaultCellStyle = dataGridViewCellStyle14;
            this.title.FillWeight = 100.195F;
            this.title.HeaderText = "商品明细";
            this.title.Name = "title";
            // 
            // hangtime
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.hangtime.DefaultCellStyle = dataGridViewCellStyle15;
            this.hangtime.FillWeight = 100.195F;
            this.hangtime.HeaderText = "挂单时间";
            this.hangtime.Name = "hangtime";
            // 
            // reprint
            // 
            this.reprint.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("微软雅黑", 14F);
            dataGridViewCellStyle16.ForeColor = System.Drawing.Color.White;
            this.reprint.DefaultCellStyle = dataGridViewCellStyle16;
            this.reprint.FillWeight = 101.9699F;
            this.reprint.HeaderText = "继续收银";
            this.reprint.Name = "reprint";
            this.reprint.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.reprint.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.reprint.Width = 150;
            // 
            // cancle
            // 
            this.cancle.ActiveLinkColor = System.Drawing.Color.Black;
            this.cancle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("微软雅黑", 14F);
            dataGridViewCellStyle17.ForeColor = System.Drawing.Color.White;
            this.cancle.DefaultCellStyle = dataGridViewCellStyle17;
            this.cancle.FillWeight = 81.21828F;
            this.cancle.HeaderText = "删除挂单";
            this.cancle.Name = "cancle";
            this.cancle.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cancle.Width = 150;
            // 
            // frmOrderHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(960, 591);
            this.Controls.Add(this.picScreen);
            this.Controls.Add(this.btnClearOrderHang);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dgvOrderOnLine);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmOrderHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmOrderHang";
            this.Shown += new System.EventHandler(this.frmOrderHang_Shown);
            this.EnabledChanged += new System.EventHandler(this.frmOrderHang_EnabledChanged);
            this.SizeChanged += new System.EventHandler(this.frmOrderHang_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderOnLine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picScreen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvOrderOnLine;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnClearOrderHang;
        private System.Windows.Forms.PictureBox picScreen;
        private System.Windows.Forms.DataGridViewTextBoxColumn serialno;
        private System.Windows.Forms.DataGridViewTextBoxColumn phone;
        private System.Windows.Forms.DataGridViewTextBoxColumn title;
        private System.Windows.Forms.DataGridViewTextBoxColumn hangtime;
        private System.Windows.Forms.DataGridViewLinkColumn reprint;
        private System.Windows.Forms.DataGridViewLinkColumn cancle;
    }
}