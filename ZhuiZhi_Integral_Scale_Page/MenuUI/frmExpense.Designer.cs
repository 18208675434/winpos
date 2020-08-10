namespace ZhuiZhi_Integral_Scale_UncleFruit
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.pnlHead = new System.Windows.Forms.Panel();
            this.lblShopName = new System.Windows.Forms.Label();
            this.btnCancle = new System.Windows.Forms.Button();
            this.pnlDgvHead = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.lblMenu = new System.Windows.Forms.Label();
            this.picMenu = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpense)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picScreen)).BeginInit();
            this.pnlHead.SuspendLayout();
            this.pnlDgvHead.SuspendLayout();
            this.pnlMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvExpense
            // 
            this.dgvExpense.AllowUserToAddRows = false;
            this.dgvExpense.AllowUserToDeleteRows = false;
            this.dgvExpense.AllowUserToResizeColumns = false;
            this.dgvExpense.AllowUserToResizeRows = false;
            this.dgvExpense.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvExpense.BackgroundColor = System.Drawing.Color.White;
            this.dgvExpense.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvExpense.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvExpense.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvExpense.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvExpense.ColumnHeadersHeight = 50;
            this.dgvExpense.ColumnHeadersVisible = false;
            this.dgvExpense.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.createdat,
            this.expensename,
            this.收入支付项目,
            this.createby});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvExpense.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvExpense.GridColor = System.Drawing.Color.LightGray;
            this.dgvExpense.Location = new System.Drawing.Point(11, 208);
            this.dgvExpense.Margin = new System.Windows.Forms.Padding(2);
            this.dgvExpense.Name = "dgvExpense";
            this.dgvExpense.ReadOnly = true;
            this.dgvExpense.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvExpense.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvExpense.RowHeadersVisible = false;
            this.dgvExpense.RowHeadersWidth = 40;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.dgvExpense.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvExpense.RowTemplate.Height = 80;
            this.dgvExpense.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvExpense.Size = new System.Drawing.Size(1155, 537);
            this.dgvExpense.TabIndex = 1;
            // 
            // createdat
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.createdat.DefaultCellStyle = dataGridViewCellStyle2;
            this.createdat.FillWeight = 235F;
            this.createdat.HeaderText = "记录时间";
            this.createdat.Name = "createdat";
            this.createdat.ReadOnly = true;
            // 
            // expensename
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.expensename.DefaultCellStyle = dataGridViewCellStyle3;
            this.expensename.FillWeight = 235F;
            this.expensename.HeaderText = "类型";
            this.expensename.Name = "expensename";
            this.expensename.ReadOnly = true;
            // 
            // 收入支付项目
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.收入支付项目.DefaultCellStyle = dataGridViewCellStyle4;
            this.收入支付项目.FillWeight = 235F;
            this.收入支付项目.HeaderText = "金额";
            this.收入支付项目.Name = "收入支付项目";
            this.收入支付项目.ReadOnly = true;
            // 
            // createby
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.createby.DefaultCellStyle = dataGridViewCellStyle5;
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
            this.btnAdd.Location = new System.Drawing.Point(889, 18);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(110, 55);
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
            this.btnQuery.FlatAppearance.BorderSize = 0;
            this.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuery.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnQuery.ForeColor = System.Drawing.Color.Black;
            this.btnQuery.Location = new System.Drawing.Point(1025, 18);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(2);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(110, 55);
            this.btnQuery.TabIndex = 11;
            this.btnQuery.Text = "查 询";
            this.btnQuery.UseVisualStyleBackColor = false;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnWeek
            // 
            this.btnWeek.AutoSize = true;
            this.btnWeek.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnWeek.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.btnWeek.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnWeek.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnWeek.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWeek.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.btnWeek.Location = new System.Drawing.Point(451, 27);
            this.btnWeek.Margin = new System.Windows.Forms.Padding(2);
            this.btnWeek.Name = "btnWeek";
            this.btnWeek.Size = new System.Drawing.Size(94, 36);
            this.btnWeek.TabIndex = 10;
            this.btnWeek.Text = "最近一周";
            this.btnWeek.UseVisualStyleBackColor = true;
            this.btnWeek.Click += new System.EventHandler(this.btnWeek_Click);
            // 
            // btnYesterday
            // 
            this.btnYesterday.AutoSize = true;
            this.btnYesterday.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnYesterday.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.btnYesterday.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnYesterday.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnYesterday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYesterday.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.btnYesterday.Location = new System.Drawing.Point(381, 27);
            this.btnYesterday.Margin = new System.Windows.Forms.Padding(2);
            this.btnYesterday.Name = "btnYesterday";
            this.btnYesterday.Size = new System.Drawing.Size(60, 36);
            this.btnYesterday.TabIndex = 9;
            this.btnYesterday.Text = "昨天";
            this.btnYesterday.UseVisualStyleBackColor = true;
            this.btnYesterday.Click += new System.EventHandler(this.btnYesterday_Click);
            // 
            // btnToday
            // 
            this.btnToday.AutoSize = true;
            this.btnToday.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnToday.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
            this.btnToday.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnToday.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnToday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToday.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.btnToday.Location = new System.Drawing.Point(311, 27);
            this.btnToday.Margin = new System.Windows.Forms.Padding(2);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(60, 36);
            this.btnToday.TabIndex = 8;
            this.btnToday.Text = "今天";
            this.btnToday.UseVisualStyleBackColor = true;
            this.btnToday.Click += new System.EventHandler(this.btnToday_Click);
            // 
            // dtStart
            // 
            this.dtStart.CalendarFont = new System.Drawing.Font("微软雅黑", 16F);
            this.dtStart.CustomFormat = "yyyy-MM-dd";
            this.dtStart.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.dtStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtStart.Location = new System.Drawing.Point(124, 28);
            this.dtStart.Margin = new System.Windows.Forms.Padding(2);
            this.dtStart.Name = "dtStart";
            this.dtStart.Size = new System.Drawing.Size(169, 32);
            this.dtStart.TabIndex = 5;
            this.dtStart.Value = new System.DateTime(2019, 9, 24, 16, 10, 45, 0);
            this.dtStart.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dtStart_MouseDown);
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
            // pnlHead
            // 
            this.pnlHead.BackColor = System.Drawing.Color.Black;
            this.pnlHead.Controls.Add(this.pnlMenu);
            this.pnlHead.Controls.Add(this.lblShopName);
            this.pnlHead.Controls.Add(this.btnCancle);
            this.pnlHead.Location = new System.Drawing.Point(0, 0);
            this.pnlHead.Name = "pnlHead";
            this.pnlHead.Size = new System.Drawing.Size(1178, 60);
            this.pnlHead.TabIndex = 37;
            // 
            // lblShopName
            // 
            this.lblShopName.AutoSize = true;
            this.lblShopName.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblShopName.ForeColor = System.Drawing.Color.White;
            this.lblShopName.Location = new System.Drawing.Point(20, 20);
            this.lblShopName.Name = "lblShopName";
            this.lblShopName.Size = new System.Drawing.Size(42, 21);
            this.lblShopName.TabIndex = 42;
            this.lblShopName.Text = "店铺";
            // 
            // btnCancle
            // 
            this.btnCancle.BackColor = System.Drawing.Color.Black;
            this.btnCancle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancle.BackgroundImage")));
            this.btnCancle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancle.FlatAppearance.BorderSize = 0;
            this.btnCancle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancle.Font = new System.Drawing.Font("微软雅黑", 11.5F);
            this.btnCancle.ForeColor = System.Drawing.Color.White;
            this.btnCancle.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnCancle.Location = new System.Drawing.Point(880, 14);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(95, 35);
            this.btnCancle.TabIndex = 34;
            this.btnCancle.Text = "返回";
            this.btnCancle.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnCancle.UseVisualStyleBackColor = false;
            this.btnCancle.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // pnlDgvHead
            // 
            this.pnlDgvHead.BackColor = System.Drawing.Color.White;
            this.pnlDgvHead.Controls.Add(this.label6);
            this.pnlDgvHead.Controls.Add(this.label5);
            this.pnlDgvHead.Controls.Add(this.label10);
            this.pnlDgvHead.Controls.Add(this.label11);
            this.pnlDgvHead.Location = new System.Drawing.Point(12, 158);
            this.pnlDgvHead.Name = "pnlDgvHead";
            this.pnlDgvHead.Size = new System.Drawing.Size(1154, 49);
            this.pnlDgvHead.TabIndex = 38;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(966, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 23);
            this.label6.TabIndex = 37;
            this.label6.Text = "操作人";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(697, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 23);
            this.label5.TabIndex = 36;
            this.label5.Text = "金额";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(410, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 23);
            this.label10.TabIndex = 33;
            this.label10.Text = "类型";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(94, 13);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 23);
            this.label11.TabIndex = 32;
            this.label11.Text = "记录时间";
            // 
            // pnlMenu
            // 
            this.pnlMenu.Controls.Add(this.lblMenu);
            this.pnlMenu.Controls.Add(this.picMenu);
            this.pnlMenu.Location = new System.Drawing.Point(1020, 8);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(150, 45);
            this.pnlMenu.TabIndex = 49;
            // 
            // lblMenu
            // 
            this.lblMenu.AutoSize = true;
            this.lblMenu.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblMenu.ForeColor = System.Drawing.Color.White;
            this.lblMenu.Location = new System.Drawing.Point(103, 12);
            this.lblMenu.Name = "lblMenu";
            this.lblMenu.Size = new System.Drawing.Size(42, 21);
            this.lblMenu.TabIndex = 38;
            this.lblMenu.Text = "店铺";
            // 
            // picMenu
            // 
            this.picMenu.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picMenu.BackgroundImage")));
            this.picMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picMenu.Location = new System.Drawing.Point(69, 12);
            this.picMenu.Name = "picMenu";
            this.picMenu.Size = new System.Drawing.Size(21, 21);
            this.picMenu.TabIndex = 0;
            this.picMenu.TabStop = false;
            // 
            // frmExpense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1178, 760);
            this.ControlBox = false;
            this.Controls.Add(this.picScreen);
            this.Controls.Add(this.pnlDgvHead);
            this.Controls.Add(this.pnlHead);
            this.Controls.Add(this.dgvExpense);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmExpense";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmExpense_Load);
            this.Shown += new System.EventHandler(this.frmExpense_Shown);
            this.SizeChanged += new System.EventHandler(this.frmExpense_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpense)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picScreen)).EndInit();
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            this.pnlDgvHead.ResumeLayout(false);
            this.pnlDgvHead.PerformLayout();
            this.pnlMenu.ResumeLayout(false);
            this.pnlMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMenu)).EndInit();
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
        private System.Windows.Forms.DataGridViewTextBoxColumn createdat;
        private System.Windows.Forms.DataGridViewTextBoxColumn expensename;
        private System.Windows.Forms.DataGridViewTextBoxColumn 收入支付项目;
        private System.Windows.Forms.DataGridViewTextBoxColumn createby;
        private System.Windows.Forms.Panel pnlHead;
        private System.Windows.Forms.Label lblShopName;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Panel pnlDgvHead;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Label lblMenu;
        private System.Windows.Forms.PictureBox picMenu;
    }
}