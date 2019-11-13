namespace QiandamaPOS
{
    partial class frmExpense
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExpense));
            this.dgvExpense = new System.Windows.Forms.DataGridView();
            this.createdat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expensename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.收入支付项目 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createby = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnWeek = new System.Windows.Forms.Button();
            this.btnYesterday = new System.Windows.Forms.Button();
            this.btnToday = new System.Windows.Forms.Button();
            this.dtStart = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.picScreen = new System.Windows.Forms.PictureBox();
            this.timerNow = new System.Windows.Forms.Timer(this.components);
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.btnMenu = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.btnCancle = new System.Windows.Forms.ToolStripButton();
            this.lblTime = new System.Windows.Forms.ToolStripLabel();
            this.lblShopName = new System.Windows.Forms.ToolStripLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpense)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picScreen)).BeginInit();
            this.toolStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvExpense
            // 
            this.dgvExpense.AllowUserToAddRows = false;
            this.dgvExpense.AllowUserToDeleteRows = false;
            this.dgvExpense.AllowUserToResizeColumns = false;
            this.dgvExpense.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            this.dgvExpense.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvExpense.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvExpense.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvExpense.BackgroundColor = System.Drawing.Color.White;
            this.dgvExpense.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvExpense.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvExpense.ColumnHeadersHeight = 50;
            this.dgvExpense.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.createdat,
            this.expensename,
            this.收入支付项目,
            this.createby});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvExpense.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvExpense.GridColor = System.Drawing.Color.White;
            this.dgvExpense.Location = new System.Drawing.Point(11, 159);
            this.dgvExpense.Margin = new System.Windows.Forms.Padding(2);
            this.dgvExpense.Name = "dgvExpense";
            this.dgvExpense.ReadOnly = true;
            this.dgvExpense.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvExpense.RowHeadersVisible = false;
            this.dgvExpense.RowHeadersWidth = 40;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.dgvExpense.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvExpense.RowTemplate.Height = 40;
            this.dgvExpense.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvExpense.Size = new System.Drawing.Size(1155, 590);
            this.dgvExpense.TabIndex = 1;
            // 
            // createdat
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.createdat.DefaultCellStyle = dataGridViewCellStyle3;
            this.createdat.FillWeight = 235F;
            this.createdat.HeaderText = "记录时间";
            this.createdat.Name = "createdat";
            this.createdat.ReadOnly = true;
            // 
            // expensename
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.expensename.DefaultCellStyle = dataGridViewCellStyle4;
            this.expensename.FillWeight = 235F;
            this.expensename.HeaderText = "类型";
            this.expensename.Name = "expensename";
            this.expensename.ReadOnly = true;
            // 
            // 收入支付项目
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.收入支付项目.DefaultCellStyle = dataGridViewCellStyle5;
            this.收入支付项目.FillWeight = 235F;
            this.收入支付项目.HeaderText = "金额";
            this.收入支付项目.Name = "收入支付项目";
            this.收入支付项目.ReadOnly = true;
            // 
            // createby
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.createby.DefaultCellStyle = dataGridViewCellStyle6;
            this.createby.FillWeight = 235F;
            this.createby.HeaderText = "操作人";
            this.createby.Name = "createby";
            this.createby.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.btnQuery);
            this.panel1.Controls.Add(this.btnWeek);
            this.panel1.Controls.Add(this.btnYesterday);
            this.panel1.Controls.Add(this.btnToday);
            this.panel1.Controls.Add(this.dtStart);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(12, 63);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1154, 91);
            this.panel1.TabIndex = 2;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Transparent;
            this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(914, 20);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 45);
            this.btnAdd.TabIndex = 13;
            this.btnAdd.Text = "新 增";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.BackColor = System.Drawing.Color.Transparent;
            this.btnQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.BackgroundImage")));
            this.btnQuery.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuery.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnQuery.ForeColor = System.Drawing.Color.White;
            this.btnQuery.Location = new System.Drawing.Point(1036, 20);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(2);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(100, 45);
            this.btnQuery.TabIndex = 11;
            this.btnQuery.Text = "查 询";
            this.btnQuery.UseVisualStyleBackColor = false;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnWeek
            // 
            this.btnWeek.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnWeek.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnWeek.Location = new System.Drawing.Point(439, 27);
            this.btnWeek.Margin = new System.Windows.Forms.Padding(2);
            this.btnWeek.Name = "btnWeek";
            this.btnWeek.Size = new System.Drawing.Size(90, 34);
            this.btnWeek.TabIndex = 10;
            this.btnWeek.Text = "最近一周";
            this.btnWeek.UseVisualStyleBackColor = true;
            this.btnWeek.Click += new System.EventHandler(this.btnWeek_Click);
            // 
            // btnYesterday
            // 
            this.btnYesterday.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnYesterday.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnYesterday.Location = new System.Drawing.Point(375, 27);
            this.btnYesterday.Margin = new System.Windows.Forms.Padding(2);
            this.btnYesterday.Name = "btnYesterday";
            this.btnYesterday.Size = new System.Drawing.Size(60, 33);
            this.btnYesterday.TabIndex = 9;
            this.btnYesterday.Text = "昨天";
            this.btnYesterday.UseVisualStyleBackColor = true;
            this.btnYesterday.Click += new System.EventHandler(this.btnYesterday_Click);
            // 
            // btnToday
            // 
            this.btnToday.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnToday.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnToday.Location = new System.Drawing.Point(311, 27);
            this.btnToday.Margin = new System.Windows.Forms.Padding(2);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(60, 33);
            this.btnToday.TabIndex = 8;
            this.btnToday.Text = "今天";
            this.btnToday.UseVisualStyleBackColor = true;
            this.btnToday.Click += new System.EventHandler(this.btnToday_Click);
            // 
            // dtStart
            // 
            this.dtStart.CalendarFont = new System.Drawing.Font("微软雅黑", 16F);
            this.dtStart.CustomFormat = "yyyy-MM-dd";
            this.dtStart.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.dtStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtStart.Location = new System.Drawing.Point(124, 28);
            this.dtStart.Margin = new System.Windows.Forms.Padding(2);
            this.dtStart.Name = "dtStart";
            this.dtStart.Size = new System.Drawing.Size(169, 30);
            this.dtStart.TabIndex = 5;
            this.dtStart.Value = new System.DateTime(2019, 9, 24, 16, 10, 45, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label3.Location = new System.Drawing.Point(22, 31);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "记录时间：";
            // 
            // picScreen
            // 
            this.picScreen.BackColor = System.Drawing.Color.Red;
            this.picScreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picScreen.Location = new System.Drawing.Point(0, 0);
            this.picScreen.Name = "picScreen";
            this.picScreen.Size = new System.Drawing.Size(10, 10);
            this.picScreen.TabIndex = 26;
            this.picScreen.TabStop = false;
            this.picScreen.Visible = false;
            this.picScreen.EnabledChanged += new System.EventHandler(this.picScreen_EnabledChanged);
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
            this.toolStripMain.TabIndex = 27;
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
            // frmExpense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1178, 760);
            this.Controls.Add(this.picScreen);
            ((System.ComponentModel.ISupportInitialize)(this.picScreen)).EndInit();
            this.Controls.Add(this.toolStripMain);
            
            this.Controls.Add(this.dgvExpense);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmExpense";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmExpense";
            this.Shown += new System.EventHandler(this.frmExpense_Shown);
            this.SizeChanged += new System.EventHandler(this.frmExpense_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpense)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
           
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvExpense;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnWeek;
        private System.Windows.Forms.Button btnYesterday;
        private System.Windows.Forms.Button btnToday;
        private System.Windows.Forms.DateTimePicker dtStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.PictureBox picScreen;
        private System.Windows.Forms.Timer timerNow;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton btnMenu;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton btnCancle;
        private System.Windows.Forms.ToolStripLabel lblTime;
        private System.Windows.Forms.ToolStripLabel lblShopName;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdat;
        private System.Windows.Forms.DataGridViewTextBoxColumn expensename;
        private System.Windows.Forms.DataGridViewTextBoxColumn 收入支付项目;
        private System.Windows.Forms.DataGridViewTextBoxColumn createby;
    }
}