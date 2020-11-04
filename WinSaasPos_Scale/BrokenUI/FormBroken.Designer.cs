namespace WinSaasPOS_Scale.BrokenUI
{
    partial class FormBroken
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBroken));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHead = new System.Windows.Forms.Panel();
            this.btnCancle = new System.Windows.Forms.Button();
            this.btnMenu = new System.Windows.Forms.Button();
            this.btnWindows = new System.Windows.Forms.Button();
            this.lblShopName = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbtnCeate = new WinSaasPOS_Scale.RoundButton();
            this.rbtnQuery = new WinSaasPOS_Scale.RoundButton();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.btnWeek = new System.Windows.Forms.Button();
            this.btnYesterday = new System.Windows.Forms.Button();
            this.btnToday = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dtStart = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlDgvHead = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.dgvBroken = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BrokenData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BrokenDetail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BrokenNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BrokenCash = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BrokenStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Operation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timerNow = new System.Windows.Forms.Timer(this.components);
            this.pnlEmptyOrder = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlHead.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlDgvHead.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBroken)).BeginInit();
            this.pnlEmptyOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHead
            // 
            this.pnlHead.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.pnlHead.Controls.Add(this.btnCancle);
            this.pnlHead.Controls.Add(this.btnMenu);
            this.pnlHead.Controls.Add(this.btnWindows);
            this.pnlHead.Controls.Add(this.lblShopName);
            this.pnlHead.Controls.Add(this.lblTime);
            this.pnlHead.Font = new System.Drawing.Font("宋体", 9F);
            this.pnlHead.Location = new System.Drawing.Point(0, 0);
            this.pnlHead.Name = "pnlHead";
            this.pnlHead.Size = new System.Drawing.Size(1180, 60);
            this.pnlHead.TabIndex = 33;
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
            this.btnCancle.Location = new System.Drawing.Point(887, 18);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(95, 28);
            this.btnCancle.TabIndex = 41;
            this.btnCancle.Text = "返回";
            this.btnCancle.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnCancle.UseVisualStyleBackColor = false;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // btnMenu
            // 
            this.btnMenu.AutoSize = true;
            this.btnMenu.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMenu.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnMenu.FlatAppearance.BorderSize = 0;
            this.btnMenu.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnMenu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenu.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnMenu.ForeColor = System.Drawing.Color.White;
            this.btnMenu.Image = ((System.Drawing.Image)(resources.GetObject("btnMenu.Image")));
            this.btnMenu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMenu.Location = new System.Drawing.Point(1029, 16);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(142, 31);
            this.btnMenu.TabIndex = 40;
            this.btnMenu.Text = " 某某某，你好";
            this.btnMenu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMenu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMenu.UseVisualStyleBackColor = false;
            // 
            // btnWindows
            // 
            this.btnWindows.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnWindows.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnWindows.BackgroundImage")));
            this.btnWindows.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnWindows.FlatAppearance.BorderSize = 0;
            this.btnWindows.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWindows.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnWindows.ForeColor = System.Drawing.Color.White;
            this.btnWindows.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnWindows.Location = new System.Drawing.Point(12, 16);
            this.btnWindows.Name = "btnWindows";
            this.btnWindows.Size = new System.Drawing.Size(37, 31);
            this.btnWindows.TabIndex = 38;
            this.btnWindows.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnWindows.UseVisualStyleBackColor = false;
            // 
            // lblShopName
            // 
            this.lblShopName.AutoSize = true;
            this.lblShopName.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.lblShopName.ForeColor = System.Drawing.Color.White;
            this.lblShopName.Location = new System.Drawing.Point(214, 20);
            this.lblShopName.Name = "lblShopName";
            this.lblShopName.Size = new System.Drawing.Size(39, 20);
            this.lblShopName.TabIndex = 37;
            this.lblShopName.Text = "店铺";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.lblTime.ForeColor = System.Drawing.Color.White;
            this.lblTime.Location = new System.Drawing.Point(51, 20);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(159, 20);
            this.lblTime.TabIndex = 36;
            this.lblTime.Text = "2019-10-10 12:12:39";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.rbtnCeate);
            this.panel1.Controls.Add(this.rbtnQuery);
            this.panel1.Controls.Add(this.dtEnd);
            this.panel1.Controls.Add(this.btnWeek);
            this.panel1.Controls.Add(this.btnYesterday);
            this.panel1.Controls.Add(this.btnToday);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.dtStart);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(12, 79);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1155, 99);
            this.panel1.TabIndex = 34;
            // 
            // rbtnCeate
            // 
            this.rbtnCeate.AllBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(70)))), ((int)(((byte)(21)))));
            this.rbtnCeate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(70)))), ((int)(((byte)(21)))));
            this.rbtnCeate.Image = null;
            this.rbtnCeate.ImageSize = new System.Drawing.Size(0, 0);
            this.rbtnCeate.Location = new System.Drawing.Point(980, 24);
            this.rbtnCeate.Name = "rbtnCeate";
            this.rbtnCeate.PenColor = System.Drawing.Color.Black;
            this.rbtnCeate.PenWidth = 1;
            this.rbtnCeate.RoundRadius = 10;
            this.rbtnCeate.ShowImg = false;
            this.rbtnCeate.ShowText = "新建报损";
            this.rbtnCeate.Size = new System.Drawing.Size(147, 52);
            this.rbtnCeate.TabIndex = 24;
            this.rbtnCeate.TextForeColor = System.Drawing.Color.White;
            this.rbtnCeate.WhetherEnable = true;
            this.rbtnCeate.ButtonClick += new System.EventHandler(this.rbtnCeate_ButtonClick);
            // 
            // rbtnQuery
            // 
            this.rbtnQuery.AllBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.rbtnQuery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.rbtnQuery.Image = null;
            this.rbtnQuery.ImageSize = new System.Drawing.Size(0, 0);
            this.rbtnQuery.Location = new System.Drawing.Point(843, 24);
            this.rbtnQuery.Name = "rbtnQuery";
            this.rbtnQuery.PenColor = System.Drawing.Color.Black;
            this.rbtnQuery.PenWidth = 1;
            this.rbtnQuery.RoundRadius = 10;
            this.rbtnQuery.ShowImg = false;
            this.rbtnQuery.ShowText = "查询";
            this.rbtnQuery.Size = new System.Drawing.Size(120, 52);
            this.rbtnQuery.TabIndex = 23;
            this.rbtnQuery.TextForeColor = System.Drawing.Color.White;
            this.rbtnQuery.WhetherEnable = true;
            this.rbtnQuery.ButtonClick += new System.EventHandler(this.rbtnQuery_ButtonClick);
            // 
            // dtEnd
            // 
            this.dtEnd.CalendarFont = new System.Drawing.Font("微软雅黑", 18F);
            this.dtEnd.CalendarForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dtEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtEnd.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEnd.Location = new System.Drawing.Point(328, 35);
            this.dtEnd.Margin = new System.Windows.Forms.Padding(2);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(185, 29);
            this.dtEnd.TabIndex = 22;
            this.dtEnd.Value = new System.DateTime(2019, 9, 24, 16, 10, 45, 0);
            this.dtEnd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dtEnd_MouseDown);
            // 
            // btnWeek
            // 
            this.btnWeek.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnWeek.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnWeek.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.btnWeek.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnWeek.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnWeek.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWeek.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnWeek.Location = new System.Drawing.Point(671, 34);
            this.btnWeek.Margin = new System.Windows.Forms.Padding(2);
            this.btnWeek.Name = "btnWeek";
            this.btnWeek.Size = new System.Drawing.Size(95, 31);
            this.btnWeek.TabIndex = 21;
            this.btnWeek.Text = "最近7天";
            this.btnWeek.UseVisualStyleBackColor = true;
            this.btnWeek.Click += new System.EventHandler(this.btnWeek_Click);
            // 
            // btnYesterday
            // 
            this.btnYesterday.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnYesterday.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnYesterday.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.btnYesterday.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnYesterday.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnYesterday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYesterday.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnYesterday.Location = new System.Drawing.Point(603, 34);
            this.btnYesterday.Margin = new System.Windows.Forms.Padding(2);
            this.btnYesterday.Name = "btnYesterday";
            this.btnYesterday.Size = new System.Drawing.Size(55, 31);
            this.btnYesterday.TabIndex = 20;
            this.btnYesterday.Text = "昨天";
            this.btnYesterday.UseVisualStyleBackColor = true;
            this.btnYesterday.Click += new System.EventHandler(this.btnYesterday_Click);
            // 
            // btnToday
            // 
            this.btnToday.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnToday.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnToday.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.btnToday.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnToday.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnToday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToday.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnToday.Location = new System.Drawing.Point(533, 34);
            this.btnToday.Margin = new System.Windows.Forms.Padding(2);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(55, 31);
            this.btnToday.TabIndex = 19;
            this.btnToday.Text = "今天";
            this.btnToday.UseVisualStyleBackColor = true;
            this.btnToday.Click += new System.EventHandler(this.btnToday_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label4.Location = new System.Drawing.Point(297, 39);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 20);
            this.label4.TabIndex = 18;
            this.label4.Text = "至";
            // 
            // dtStart
            // 
            this.dtStart.CalendarFont = new System.Drawing.Font("微软雅黑", 18F);
            this.dtStart.CalendarForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dtStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtStart.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.dtStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtStart.Location = new System.Drawing.Point(109, 34);
            this.dtStart.Margin = new System.Windows.Forms.Padding(2);
            this.dtStart.Name = "dtStart";
            this.dtStart.Size = new System.Drawing.Size(185, 29);
            this.dtStart.TabIndex = 17;
            this.dtStart.Value = new System.DateTime(2019, 9, 24, 16, 10, 45, 0);
            this.dtStart.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dtStart_MouseDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label3.Location = new System.Drawing.Point(26, 39);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 21);
            this.label3.TabIndex = 16;
            this.label3.Text = "报损时间:";
            // 
            // pnlDgvHead
            // 
            this.pnlDgvHead.BackColor = System.Drawing.Color.White;
            this.pnlDgvHead.Controls.Add(this.label12);
            this.pnlDgvHead.Controls.Add(this.label5);
            this.pnlDgvHead.Controls.Add(this.label7);
            this.pnlDgvHead.Controls.Add(this.label9);
            this.pnlDgvHead.Controls.Add(this.label10);
            this.pnlDgvHead.Controls.Add(this.label11);
            this.pnlDgvHead.Location = new System.Drawing.Point(12, 192);
            this.pnlDgvHead.Name = "pnlDgvHead";
            this.pnlDgvHead.Size = new System.Drawing.Size(1155, 58);
            this.pnlDgvHead.TabIndex = 36;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(1058, 18);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(44, 23);
            this.label12.TabIndex = 38;
            this.label12.Text = "操作";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(892, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 23);
            this.label5.TabIndex = 36;
            this.label5.Text = "操作人";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(729, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 23);
            this.label7.TabIndex = 35;
            this.label7.Text = "报损金额(元)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(564, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 23);
            this.label9.TabIndex = 34;
            this.label9.Text = "报损种类";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(222, 18);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 23);
            this.label10.TabIndex = 33;
            this.label10.Text = "报损明细";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(20, 18);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 23);
            this.label11.TabIndex = 32;
            this.label11.Text = "报损时间";
            // 
            // dgvBroken
            // 
            this.dgvBroken.AllowUserToAddRows = false;
            this.dgvBroken.AllowUserToDeleteRows = false;
            this.dgvBroken.AllowUserToResizeColumns = false;
            this.dgvBroken.AllowUserToResizeRows = false;
            this.dgvBroken.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBroken.BackgroundColor = System.Drawing.Color.White;
            this.dgvBroken.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBroken.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvBroken.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvBroken.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("微软雅黑", 11F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBroken.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvBroken.ColumnHeadersHeight = 50;
            this.dgvBroken.ColumnHeadersVisible = false;
            this.dgvBroken.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.BrokenData,
            this.BrokenDetail,
            this.BrokenNum,
            this.BrokenCash,
            this.BrokenStatus,
            this.Operation});
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("微软雅黑", 11F);
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBroken.DefaultCellStyle = dataGridViewCellStyle17;
            this.dgvBroken.GridColor = System.Drawing.Color.LightGray;
            this.dgvBroken.Location = new System.Drawing.Point(12, 251);
            this.dgvBroken.Margin = new System.Windows.Forms.Padding(2);
            this.dgvBroken.Name = "dgvBroken";
            this.dgvBroken.ReadOnly = true;
            this.dgvBroken.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvBroken.RowHeadersVisible = false;
            this.dgvBroken.RowHeadersWidth = 40;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.dgvBroken.RowsDefaultCellStyle = dataGridViewCellStyle18;
            this.dgvBroken.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgvBroken.RowTemplate.Height = 80;
            this.dgvBroken.Size = new System.Drawing.Size(1155, 508);
            this.dgvBroken.TabIndex = 35;
            this.dgvBroken.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBroken_CellClick);
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // BrokenData
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("微软雅黑", 10F);
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.Black;
            this.BrokenData.DefaultCellStyle = dataGridViewCellStyle11;
            this.BrokenData.FillWeight = 20F;
            this.BrokenData.HeaderText = "报损时间";
            this.BrokenData.Name = "BrokenData";
            this.BrokenData.ReadOnly = true;
            // 
            // BrokenDetail
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("微软雅黑", 10F);
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Black;
            this.BrokenDetail.DefaultCellStyle = dataGridViewCellStyle12;
            this.BrokenDetail.FillWeight = 30F;
            this.BrokenDetail.HeaderText = "报损明细";
            this.BrokenDetail.Name = "BrokenDetail";
            this.BrokenDetail.ReadOnly = true;
            // 
            // BrokenNum
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("微软雅黑", 10F);
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.Black;
            this.BrokenNum.DefaultCellStyle = dataGridViewCellStyle13;
            this.BrokenNum.FillWeight = 15F;
            this.BrokenNum.HeaderText = "报损数量";
            this.BrokenNum.Name = "BrokenNum";
            this.BrokenNum.ReadOnly = true;
            // 
            // BrokenCash
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("微软雅黑", 10F);
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.Black;
            this.BrokenCash.DefaultCellStyle = dataGridViewCellStyle14;
            this.BrokenCash.FillWeight = 15F;
            this.BrokenCash.HeaderText = "报损金额(元)";
            this.BrokenCash.Name = "BrokenCash";
            this.BrokenCash.ReadOnly = true;
            // 
            // BrokenStatus
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("微软雅黑", 10F);
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.Black;
            this.BrokenStatus.DefaultCellStyle = dataGridViewCellStyle15;
            this.BrokenStatus.FillWeight = 15F;
            this.BrokenStatus.HeaderText = "状态";
            this.BrokenStatus.Name = "BrokenStatus";
            this.BrokenStatus.ReadOnly = true;
            // 
            // Operation
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("微软雅黑", 10F);
            dataGridViewCellStyle16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(140)))), ((int)(((byte)(206)))));
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(140)))), ((int)(((byte)(206)))));
            this.Operation.DefaultCellStyle = dataGridViewCellStyle16;
            this.Operation.FillWeight = 8F;
            this.Operation.HeaderText = "操作";
            this.Operation.Name = "Operation";
            this.Operation.ReadOnly = true;
            this.Operation.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // timerNow
            // 
            this.timerNow.Tick += new System.EventHandler(this.timerNow_Tick);
            // 
            // pnlEmptyOrder
            // 
            this.pnlEmptyOrder.BackColor = System.Drawing.Color.White;
            this.pnlEmptyOrder.Controls.Add(this.label8);
            this.pnlEmptyOrder.Controls.Add(this.pictureBox1);
            this.pnlEmptyOrder.Location = new System.Drawing.Point(414, 456);
            this.pnlEmptyOrder.Margin = new System.Windows.Forms.Padding(2);
            this.pnlEmptyOrder.Name = "pnlEmptyOrder";
            this.pnlEmptyOrder.Size = new System.Drawing.Size(364, 99);
            this.pnlEmptyOrder.TabIndex = 37;
            this.pnlEmptyOrder.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label8.ForeColor = System.Drawing.Color.Gray;
            this.label8.Location = new System.Drawing.Point(88, 39);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(259, 25);
            this.label8.TabIndex = 9;
            this.label8.Text = "没有符合查询条件的报损记录";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(35, 24);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(49, 55);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // FormBroken
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 760);
            this.Controls.Add(this.pnlEmptyOrder);
            this.Controls.Add(this.pnlDgvHead);
            this.Controls.Add(this.dgvBroken);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlHead);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormBroken";
            this.Text = "FormBroken";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormBroken_FormClosing);
            this.Load += new System.EventHandler(this.FormBroken_Load);
            this.Shown += new System.EventHandler(this.FormBroken_Shown);
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlDgvHead.ResumeLayout(false);
            this.pnlDgvHead.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBroken)).EndInit();
            this.pnlEmptyOrder.ResumeLayout(false);
            this.pnlEmptyOrder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHead;
        private System.Windows.Forms.Button btnMenu;
        private System.Windows.Forms.Button btnWindows;
        private System.Windows.Forms.Label lblShopName;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Panel panel1;
        private RoundButton rbtnCeate;
        private RoundButton rbtnQuery;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private System.Windows.Forms.Button btnWeek;
        private System.Windows.Forms.Button btnYesterday;
        private System.Windows.Forms.Button btnToday;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlDgvHead;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView dgvBroken;
        private System.Windows.Forms.Timer timerNow;
        private System.Windows.Forms.Panel pnlEmptyOrder;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn BrokenData;
        private System.Windows.Forms.DataGridViewTextBoxColumn BrokenDetail;
        private System.Windows.Forms.DataGridViewTextBoxColumn BrokenNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn BrokenCash;
        private System.Windows.Forms.DataGridViewTextBoxColumn BrokenStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn Operation;
    }
}