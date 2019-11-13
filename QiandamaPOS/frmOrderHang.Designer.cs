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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrderHang));
            this.dgvOrderOnLine = new System.Windows.Forms.DataGridView();
            this.serialno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hangtime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reprint = new System.Windows.Forms.DataGridViewLinkColumn();
            this.cancle = new System.Windows.Forms.DataGridViewLinkColumn();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClearOrderHang = new System.Windows.Forms.Button();
            this.picScreen = new System.Windows.Forms.PictureBox();
            this.timerNow = new System.Windows.Forms.Timer(this.components);
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.btnMenu = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.btnCancle = new System.Windows.Forms.ToolStripButton();
            this.lblTime = new System.Windows.Forms.ToolStripLabel();
            this.lblShopName = new System.Windows.Forms.ToolStripLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderOnLine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picScreen)).BeginInit();
            this.toolStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvOrderOnLine
            // 
            this.dgvOrderOnLine.AllowUserToAddRows = false;
            this.dgvOrderOnLine.AllowUserToDeleteRows = false;
            this.dgvOrderOnLine.AllowUserToResizeColumns = false;
            this.dgvOrderOnLine.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            this.dgvOrderOnLine.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvOrderOnLine.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrderOnLine.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvOrderOnLine.BackgroundColor = System.Drawing.Color.White;
            this.dgvOrderOnLine.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvOrderOnLine.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvOrderOnLine.ColumnHeadersHeight = 50;
            this.dgvOrderOnLine.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.serialno,
            this.phone,
            this.title,
            this.hangtime,
            this.reprint,
            this.cancle});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvOrderOnLine.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgvOrderOnLine.GridColor = System.Drawing.Color.White;
            this.dgvOrderOnLine.Location = new System.Drawing.Point(12, 78);
            this.dgvOrderOnLine.Name = "dgvOrderOnLine";
            this.dgvOrderOnLine.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvOrderOnLine.RowHeadersVisible = false;
            this.dgvOrderOnLine.RowHeadersWidth = 40;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.dgvOrderOnLine.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvOrderOnLine.RowTemplate.Height = 40;
            this.dgvOrderOnLine.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrderOnLine.Size = new System.Drawing.Size(1154, 670);
            this.dgvOrderOnLine.TabIndex = 1;
            this.dgvOrderOnLine.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrderOnLine_CellContentClick);
            // 
            // serialno
            // 
            this.serialno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.serialno.DefaultCellStyle = dataGridViewCellStyle3;
            this.serialno.FillWeight = 100.195F;
            this.serialno.HeaderText = "序号";
            this.serialno.Name = "serialno";
            this.serialno.Width = 85;
            // 
            // phone
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.phone.DefaultCellStyle = dataGridViewCellStyle4;
            this.phone.FillWeight = 100.195F;
            this.phone.HeaderText = "会员手机号";
            this.phone.Name = "phone";
            // 
            // title
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.title.DefaultCellStyle = dataGridViewCellStyle5;
            this.title.FillWeight = 100.195F;
            this.title.HeaderText = "商品明细";
            this.title.Name = "title";
            // 
            // hangtime
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.hangtime.DefaultCellStyle = dataGridViewCellStyle6;
            this.hangtime.FillWeight = 100.195F;
            this.hangtime.HeaderText = "挂单时间";
            this.hangtime.Name = "hangtime";
            // 
            // reprint
            // 
            this.reprint.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("微软雅黑", 14F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            this.reprint.DefaultCellStyle = dataGridViewCellStyle7;
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
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("微软雅黑", 14F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            this.cancle.DefaultCellStyle = dataGridViewCellStyle8;
            this.cancle.FillWeight = 81.21828F;
            this.cancle.HeaderText = "删除挂单";
            this.cancle.Name = "cancle";
            this.cancle.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cancle.Width = 150;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.LightSalmon;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(876, 544);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(70, 35);
            this.btnExit.TabIndex = 14;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Visible = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(640, 544);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(70, 35);
            this.btnRefresh.TabIndex = 13;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Visible = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClearOrderHang
            // 
            this.btnClearOrderHang.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnClearOrderHang.FlatAppearance.BorderSize = 0;
            this.btnClearOrderHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearOrderHang.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnClearOrderHang.ForeColor = System.Drawing.Color.White;
            this.btnClearOrderHang.Location = new System.Drawing.Point(748, 544);
            this.btnClearOrderHang.Name = "btnClearOrderHang";
            this.btnClearOrderHang.Size = new System.Drawing.Size(90, 35);
            this.btnClearOrderHang.TabIndex = 15;
            this.btnClearOrderHang.Text = "清空挂单";
            this.btnClearOrderHang.UseVisualStyleBackColor = false;
            this.btnClearOrderHang.Visible = false;
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
            // timerNow
            // 
            this.timerNow.Tick += new System.EventHandler(this.timerNow_Tick);
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
            this.toolStripMain.TabIndex = 25;
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
            // frmOrderHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1178, 760);
            this.Controls.Add(this.picScreen);
            ((System.ComponentModel.ISupportInitialize)(this.picScreen)).EndInit();

            this.Controls.Add(this.toolStripMain);
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
            
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
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
        private System.Windows.Forms.Timer timerNow;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton btnMenu;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton btnCancle;
        private System.Windows.Forms.ToolStripLabel lblTime;
        private System.Windows.Forms.ToolStripLabel lblShopName;
    }
}