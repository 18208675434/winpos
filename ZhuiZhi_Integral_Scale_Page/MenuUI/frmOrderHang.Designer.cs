namespace ZhuiZhi_Integral_Scale_UncleFruit
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
        /// Required method for Designer suppfort - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrderHang));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvOrderOnLine = new System.Windows.Forms.DataGridView();
            this.serialno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hangtime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.title = new System.Windows.Forms.DataGridViewImageColumn();
            this.Continue = new System.Windows.Forms.DataGridViewImageColumn();
            this.DelHang = new System.Windows.Forms.DataGridViewImageColumn();
            this.pnlDgvHeadOffLine = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.picScreen = new System.Windows.Forms.PictureBox();
            this.pnlHead = new System.Windows.Forms.Panel();
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.lblMenu = new System.Windows.Forms.Label();
            this.picMenu = new System.Windows.Forms.PictureBox();
            this.lblShopName = new System.Windows.Forms.Label();
            this.btnCancle = new System.Windows.Forms.Button();
            this.btnContinue = new System.Windows.Forms.Button();
            this.btnDelHang = new System.Windows.Forms.Button();
            this.pnlDgvHead = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.rbtnPageDown = new ZhuiZhi_Integral_Scale_UncleFruit.RoundButton();
            this.rbtnPageUp = new ZhuiZhi_Integral_Scale_UncleFruit.RoundButton();
            this.pnlControl = new System.Windows.Forms.Panel();
            this.pnlDetail = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.picTitle = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderOnLine)).BeginInit();
            this.pnlDgvHeadOffLine.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picScreen)).BeginInit();
            this.pnlHead.SuspendLayout();
            this.pnlMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMenu)).BeginInit();
            this.pnlDgvHead.SuspendLayout();
            this.pnlControl.SuspendLayout();
            this.pnlDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTitle)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvOrderOnLine
            // 
            this.dgvOrderOnLine.AllowUserToAddRows = false;
            this.dgvOrderOnLine.AllowUserToDeleteRows = false;
            this.dgvOrderOnLine.AllowUserToResizeColumns = false;
            this.dgvOrderOnLine.AllowUserToResizeRows = false;
            this.dgvOrderOnLine.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrderOnLine.BackgroundColor = System.Drawing.Color.White;
            this.dgvOrderOnLine.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvOrderOnLine.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvOrderOnLine.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvOrderOnLine.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvOrderOnLine.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvOrderOnLine.ColumnHeadersHeight = 50;
            this.dgvOrderOnLine.ColumnHeadersVisible = false;
            this.dgvOrderOnLine.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.serialno,
            this.phone,
            this.hangtime,
            this.title,
            this.Continue,
            this.DelHang});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvOrderOnLine.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvOrderOnLine.GridColor = System.Drawing.Color.LightGray;
            this.dgvOrderOnLine.Location = new System.Drawing.Point(12, 124);
            this.dgvOrderOnLine.Name = "dgvOrderOnLine";
            this.dgvOrderOnLine.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvOrderOnLine.RowHeadersVisible = false;
            this.dgvOrderOnLine.RowHeadersWidth = 40;
            this.dgvOrderOnLine.RowTemplate.Height = 80;
            this.dgvOrderOnLine.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvOrderOnLine.Size = new System.Drawing.Size(1154, 566);
            this.dgvOrderOnLine.TabIndex = 1;
            this.dgvOrderOnLine.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrderOnLine_CellContentClick);
            // 
            // serialno
            // 
            this.serialno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.serialno.DefaultCellStyle = dataGridViewCellStyle2;
            this.serialno.FillWeight = 100.195F;
            this.serialno.HeaderText = "序号";
            this.serialno.Name = "serialno";
            this.serialno.Width = 85;
            // 
            // phone
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 10F);
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.phone.DefaultCellStyle = dataGridViewCellStyle3;
            this.phone.FillWeight = 60F;
            this.phone.HeaderText = "会员手机号";
            this.phone.Name = "phone";
            // 
            // hangtime
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 10F);
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.hangtime.DefaultCellStyle = dataGridViewCellStyle4;
            this.hangtime.FillWeight = 100.195F;
            this.hangtime.HeaderText = "挂单时间";
            this.hangtime.Name = "hangtime";
            this.hangtime.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // title
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 10F);
            dataGridViewCellStyle5.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle5.NullValue")));
            this.title.DefaultCellStyle = dataGridViewCellStyle5;
            this.title.FillWeight = 100.195F;
            this.title.HeaderText = "商品明细";
            this.title.Name = "title";
            this.title.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.title.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Continue
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("微软雅黑", 14F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle6.NullValue")));
            this.Continue.DefaultCellStyle = dataGridViewCellStyle6;
            this.Continue.FillWeight = 30F;
            this.Continue.HeaderText = "继续收银";
            this.Continue.Name = "Continue";
            this.Continue.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Continue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // DelHang
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("微软雅黑", 14F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle7.NullValue")));
            this.DelHang.DefaultCellStyle = dataGridViewCellStyle7;
            this.DelHang.FillWeight = 30F;
            this.DelHang.HeaderText = "删除挂单";
            this.DelHang.Name = "DelHang";
            this.DelHang.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // pnlDgvHeadOffLine
            // 
            this.pnlDgvHeadOffLine.BackColor = System.Drawing.Color.White;
            this.pnlDgvHeadOffLine.Controls.Add(this.label6);
            this.pnlDgvHeadOffLine.Controls.Add(this.label5);
            this.pnlDgvHeadOffLine.Controls.Add(this.label7);
            this.pnlDgvHeadOffLine.Controls.Add(this.label11);
            this.pnlDgvHeadOffLine.Location = new System.Drawing.Point(12, 73);
            this.pnlDgvHeadOffLine.Name = "pnlDgvHeadOffLine";
            this.pnlDgvHeadOffLine.Size = new System.Drawing.Size(1154, 49);
            this.pnlDgvHeadOffLine.TabIndex = 33;
            this.pnlDgvHeadOffLine.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(1047, 13);
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
            this.label5.Location = new System.Drawing.Point(707, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 23);
            this.label5.TabIndex = 36;
            this.label5.Text = "挂单时间";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(254, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 23);
            this.label7.TabIndex = 35;
            this.label7.Text = "商品明细";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(18, 13);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 23);
            this.label11.TabIndex = 32;
            this.label11.Text = "序号";
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
            this.picScreen.Click += new System.EventHandler(this.picScreen_Click);
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
            this.pnlHead.TabIndex = 36;
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
            // btnContinue
            // 
            this.btnContinue.BackColor = System.Drawing.Color.Transparent;
            this.btnContinue.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnContinue.BackgroundImage")));
            this.btnContinue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContinue.Font = new System.Drawing.Font("微软雅黑", 9.5F);
            this.btnContinue.ForeColor = System.Drawing.Color.White;
            this.btnContinue.Location = new System.Drawing.Point(365, 25);
            this.btnContinue.Margin = new System.Windows.Forms.Padding(2);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(83, 35);
            this.btnContinue.TabIndex = 37;
            this.btnContinue.Text = "继续收银";
            this.btnContinue.UseVisualStyleBackColor = false;
            this.btnContinue.Visible = false;
            // 
            // btnDelHang
            // 
            this.btnDelHang.BackColor = System.Drawing.Color.Transparent;
            this.btnDelHang.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDelHang.BackgroundImage")));
            this.btnDelHang.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelHang.Font = new System.Drawing.Font("微软雅黑", 9.5F);
            this.btnDelHang.ForeColor = System.Drawing.Color.White;
            this.btnDelHang.Location = new System.Drawing.Point(452, 25);
            this.btnDelHang.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelHang.Name = "btnDelHang";
            this.btnDelHang.Size = new System.Drawing.Size(83, 35);
            this.btnDelHang.TabIndex = 38;
            this.btnDelHang.Text = "删除挂单";
            this.btnDelHang.UseVisualStyleBackColor = false;
            this.btnDelHang.Visible = false;
            // 
            // pnlDgvHead
            // 
            this.pnlDgvHead.BackColor = System.Drawing.Color.White;
            this.pnlDgvHead.Controls.Add(this.label1);
            this.pnlDgvHead.Controls.Add(this.label2);
            this.pnlDgvHead.Controls.Add(this.label3);
            this.pnlDgvHead.Controls.Add(this.label4);
            this.pnlDgvHead.Controls.Add(this.label8);
            this.pnlDgvHead.Location = new System.Drawing.Point(12, 73);
            this.pnlDgvHead.Name = "pnlDgvHead";
            this.pnlDgvHead.Size = new System.Drawing.Size(1154, 49);
            this.pnlDgvHead.TabIndex = 38;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(1041, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 23);
            this.label1.TabIndex = 37;
            this.label1.Text = "操作";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(288, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 23);
            this.label2.TabIndex = 36;
            this.label2.Text = "挂单时间";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(623, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 23);
            this.label3.TabIndex = 35;
            this.label3.Text = "商品明细";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(130, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 23);
            this.label4.TabIndex = 33;
            this.label4.Text = "会员手机号";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(18, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 23);
            this.label8.TabIndex = 32;
            this.label8.Text = "序号";
            // 
            // rbtnPageDown
            // 
            this.rbtnPageDown.AllBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(160)))), ((int)(((byte)(110)))));
            this.rbtnPageDown.BackColor = System.Drawing.Color.Silver;
            this.rbtnPageDown.Image = null;
            this.rbtnPageDown.ImageSize = new System.Drawing.Size(0, 0);
            this.rbtnPageDown.Location = new System.Drawing.Point(1026, 698);
            this.rbtnPageDown.Name = "rbtnPageDown";
            this.rbtnPageDown.PenColor = System.Drawing.Color.Black;
            this.rbtnPageDown.PenWidth = 1;
            this.rbtnPageDown.RoundRadius = 10;
            this.rbtnPageDown.ShowImg = false;
            this.rbtnPageDown.ShowText = "下一页";
            this.rbtnPageDown.Size = new System.Drawing.Size(140, 50);
            this.rbtnPageDown.TabIndex = 45;
            this.rbtnPageDown.TextForeColor = System.Drawing.Color.White;
            this.rbtnPageDown.WhetherEnable = false;
            this.rbtnPageDown.ButtonClick += new System.EventHandler(this.rbtnPageDown_ButtonClick);
            // 
            // rbtnPageUp
            // 
            this.rbtnPageUp.AllBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(160)))), ((int)(((byte)(110)))));
            this.rbtnPageUp.BackColor = System.Drawing.Color.Silver;
            this.rbtnPageUp.Image = null;
            this.rbtnPageUp.ImageSize = new System.Drawing.Size(0, 0);
            this.rbtnPageUp.Location = new System.Drawing.Point(876, 698);
            this.rbtnPageUp.Name = "rbtnPageUp";
            this.rbtnPageUp.PenColor = System.Drawing.Color.Black;
            this.rbtnPageUp.PenWidth = 1;
            this.rbtnPageUp.RoundRadius = 10;
            this.rbtnPageUp.ShowImg = false;
            this.rbtnPageUp.ShowText = "上一页";
            this.rbtnPageUp.Size = new System.Drawing.Size(144, 50);
            this.rbtnPageUp.TabIndex = 44;
            this.rbtnPageUp.TextForeColor = System.Drawing.Color.White;
            this.rbtnPageUp.WhetherEnable = false;
            this.rbtnPageUp.ButtonClick += new System.EventHandler(this.rbtnPageUp_ButtonClick);
            // 
            // pnlControl
            // 
            this.pnlControl.BackColor = System.Drawing.Color.White;
            this.pnlControl.Controls.Add(this.pnlDetail);
            this.pnlControl.Controls.Add(this.btnDelHang);
            this.pnlControl.Controls.Add(this.btnContinue);
            this.pnlControl.Location = new System.Drawing.Point(335, -528);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Size = new System.Drawing.Size(716, 102);
            this.pnlControl.TabIndex = 46;
            // 
            // pnlDetail
            // 
            this.pnlDetail.Controls.Add(this.lblTitle);
            this.pnlDetail.Controls.Add(this.picTitle);
            this.pnlDetail.Location = new System.Drawing.Point(3, 3);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Size = new System.Drawing.Size(283, 80);
            this.pnlDetail.TabIndex = 40;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblTitle.Location = new System.Drawing.Point(3, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(55, 21);
            this.lblTitle.TabIndex = 40;
            this.lblTitle.Text = "label9";
            // 
            // picTitle
            // 
            this.picTitle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picTitle.BackgroundImage")));
            this.picTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picTitle.Location = new System.Drawing.Point(228, 22);
            this.picTitle.Name = "picTitle";
            this.picTitle.Size = new System.Drawing.Size(35, 35);
            this.picTitle.TabIndex = 39;
            this.picTitle.TabStop = false;
            // 
            // frmOrderHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(1178, 760);
            this.ControlBox = false;
            this.Controls.Add(this.pnlControl);
            this.Controls.Add(this.rbtnPageDown);
            this.Controls.Add(this.rbtnPageUp);
            this.Controls.Add(this.picScreen);
            this.Controls.Add(this.pnlHead);
            this.Controls.Add(this.pnlDgvHead);
            this.Controls.Add(this.pnlDgvHeadOffLine);
            this.Controls.Add(this.dgvOrderOnLine);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmOrderHang";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Load += new System.EventHandler(this.frmOrderHang_Load);
            this.Shown += new System.EventHandler(this.frmOrderHang_Shown);
            this.SizeChanged += new System.EventHandler(this.frmOrderHang_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderOnLine)).EndInit();
            this.pnlDgvHeadOffLine.ResumeLayout(false);
            this.pnlDgvHeadOffLine.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picScreen)).EndInit();
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            this.pnlMenu.ResumeLayout(false);
            this.pnlMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMenu)).EndInit();
            this.pnlDgvHead.ResumeLayout(false);
            this.pnlDgvHead.PerformLayout();
            this.pnlControl.ResumeLayout(false);
            this.pnlDetail.ResumeLayout(false);
            this.pnlDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTitle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvOrderOnLine;
        private System.Windows.Forms.PictureBox picScreen;
        private System.Windows.Forms.Panel pnlDgvHeadOffLine;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel pnlHead;
        private System.Windows.Forms.Label lblShopName;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.Button btnDelHang;
        private System.Windows.Forms.Panel pnlDgvHead;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private RoundButton rbtnPageDown;
        private RoundButton rbtnPageUp;
        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Label lblMenu;
        private System.Windows.Forms.PictureBox picMenu;
        private System.Windows.Forms.Panel pnlControl;
        private System.Windows.Forms.PictureBox picTitle;
        private System.Windows.Forms.Panel pnlDetail;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn serialno;
        private System.Windows.Forms.DataGridViewTextBoxColumn phone;
        private System.Windows.Forms.DataGridViewTextBoxColumn hangtime;
        private System.Windows.Forms.DataGridViewImageColumn title;
        private System.Windows.Forms.DataGridViewImageColumn Continue;
        private System.Windows.Forms.DataGridViewImageColumn DelHang;
    }
}