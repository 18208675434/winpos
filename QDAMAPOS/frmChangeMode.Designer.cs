namespace QDAMAPOS
{
    partial class frmChangeMode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChangeMode));
            this.label1 = new System.Windows.Forms.Label();
            this.pnlOrdreDetail = new System.Windows.Forms.Panel();
            this.picOnLine = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.picOffLine = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblLastTime = new System.Windows.Forms.Label();
            this.btnLoadScale = new System.Windows.Forms.Button();
            this.timerNow = new System.Windows.Forms.Timer(this.components);
            this.pnlHead = new System.Windows.Forms.Panel();
            this.btnOnLineType = new System.Windows.Forms.Button();
            this.btnMenu = new System.Windows.Forms.Button();
            this.btnWindows = new System.Windows.Forms.Button();
            this.lblShopName = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.btnCancle = new System.Windows.Forms.Button();
            this.picScreen = new System.Windows.Forms.PictureBox();
            this.pnlOrdreDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picOnLine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picOffLine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.pnlHead.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picScreen)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.label1.Location = new System.Drawing.Point(515, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 30);
            this.label1.TabIndex = 2;
            this.label1.Text = "选择收银模式";
            // 
            // pnlOrdreDetail
            // 
            this.pnlOrdreDetail.BackColor = System.Drawing.Color.Transparent;
            this.pnlOrdreDetail.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlOrdreDetail.BackgroundImage")));
            this.pnlOrdreDetail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlOrdreDetail.Controls.Add(this.picOnLine);
            this.pnlOrdreDetail.Controls.Add(this.label5);
            this.pnlOrdreDetail.Controls.Add(this.label4);
            this.pnlOrdreDetail.Controls.Add(this.label2);
            this.pnlOrdreDetail.Controls.Add(this.pictureBox1);
            this.pnlOrdreDetail.Controls.Add(this.label3);
            this.pnlOrdreDetail.Location = new System.Drawing.Point(309, 184);
            this.pnlOrdreDetail.Margin = new System.Windows.Forms.Padding(2);
            this.pnlOrdreDetail.Name = "pnlOrdreDetail";
            this.pnlOrdreDetail.Size = new System.Drawing.Size(240, 298);
            this.pnlOrdreDetail.TabIndex = 5;
            this.pnlOrdreDetail.Click += new System.EventHandler(this.OnLine_Click);
            // 
            // picOnLine
            // 
            this.picOnLine.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picOnLine.BackgroundImage")));
            this.picOnLine.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picOnLine.Location = new System.Drawing.Point(88, 204);
            this.picOnLine.Name = "picOnLine";
            this.picOnLine.Size = new System.Drawing.Size(55, 45);
            this.picOnLine.TabIndex = 7;
            this.picOnLine.TabStop = false;
            this.picOnLine.Click += new System.EventHandler(this.OnLine_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(30, 181);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(189, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "支持第三方支付和现金支付";
            this.label5.Click += new System.EventHandler(this.OnLine_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(74, 159);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "支持会员录入";
            this.label4.Click += new System.EventHandler(this.OnLine_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(49, 137);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "在线识别及录入商品";
            this.label2.Click += new System.EventHandler(this.OnLine_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(88, 45);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(60, 50);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.OnLine_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label3.Location = new System.Drawing.Point(77, 98);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "在线收银";
            this.label3.Click += new System.EventHandler(this.OnLine_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.picOffLine);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.pictureBox4);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Location = new System.Drawing.Point(621, 184);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 298);
            this.panel1.TabIndex = 8;
            this.panel1.Click += new System.EventHandler(this.OffLine_Click);
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // picOffLine
            // 
            this.picOffLine.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picOffLine.BackgroundImage")));
            this.picOffLine.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picOffLine.Location = new System.Drawing.Point(90, 204);
            this.picOffLine.Name = "picOffLine";
            this.picOffLine.Size = new System.Drawing.Size(55, 45);
            this.picOffLine.TabIndex = 7;
            this.picOffLine.TabStop = false;
            this.picOffLine.Visible = false;
            this.picOffLine.Click += new System.EventHandler(this.OffLine_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label6.ForeColor = System.Drawing.Color.DimGray;
            this.label6.Location = new System.Drawing.Point(61, 181);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 20);
            this.label6.TabIndex = 6;
            this.label6.Text = "仅支持现金支付";
            this.label6.Click += new System.EventHandler(this.OffLine_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label7.ForeColor = System.Drawing.Color.DimGray;
            this.label7.Location = new System.Drawing.Point(67, 159);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 20);
            this.label7.TabIndex = 5;
            this.label7.Text = "无法识别会员";
            this.label7.Click += new System.EventHandler(this.OffLine_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label8.ForeColor = System.Drawing.Color.DimGray;
            this.label8.Location = new System.Drawing.Point(49, 137);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(144, 20);
            this.label8.TabIndex = 4;
            this.label8.Text = "本地识别及录入商品";
            this.label8.Click += new System.EventHandler(this.OffLine_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox4.BackgroundImage")));
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox4.Location = new System.Drawing.Point(90, 45);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(60, 50);
            this.pictureBox4.TabIndex = 3;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.OffLine_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.White;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label9.Location = new System.Drawing.Point(79, 98);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 24);
            this.label9.TabIndex = 2;
            this.label9.Text = "离线收银";
            this.label9.Click += new System.EventHandler(this.OffLine_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label10.Location = new System.Drawing.Point(305, 494);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(451, 20);
            this.label10.TabIndex = 8;
            this.label10.Text = "*离线收银模式下商品数据、价格及促销以最近一次更新的数据为准";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label11.Location = new System.Drawing.Point(305, 517);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(144, 20);
            this.label11.TabIndex = 9;
            this.label11.Text = "最后一次更新时间：";
            // 
            // lblLastTime
            // 
            this.lblLastTime.AutoSize = true;
            this.lblLastTime.BackColor = System.Drawing.Color.Transparent;
            this.lblLastTime.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.lblLastTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblLastTime.Location = new System.Drawing.Point(444, 517);
            this.lblLastTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLastTime.Name = "lblLastTime";
            this.lblLastTime.Size = new System.Drawing.Size(21, 20);
            this.lblLastTime.TabIndex = 10;
            this.lblLastTime.Text = "--";
            // 
            // btnLoadScale
            // 
            this.btnLoadScale.BackColor = System.Drawing.Color.Transparent;
            this.btnLoadScale.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLoadScale.BackgroundImage")));
            this.btnLoadScale.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLoadScale.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnLoadScale.FlatAppearance.BorderSize = 0;
            this.btnLoadScale.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnLoadScale.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnLoadScale.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadScale.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnLoadScale.ForeColor = System.Drawing.Color.White;
            this.btnLoadScale.Location = new System.Drawing.Point(773, 510);
            this.btnLoadScale.Name = "btnLoadScale";
            this.btnLoadScale.Size = new System.Drawing.Size(88, 36);
            this.btnLoadScale.TabIndex = 23;
            this.btnLoadScale.Text = "同步数据";
            this.btnLoadScale.UseVisualStyleBackColor = false;
            this.btnLoadScale.Click += new System.EventHandler(this.btnLoadAllProduct_Click);
            // 
            // timerNow
            // 
            this.timerNow.Tick += new System.EventHandler(this.timerNow_Tick);
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
            this.pnlHead.TabIndex = 37;
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
            // 
            // btnMenu
            // 
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
            this.btnMenu.Location = new System.Drawing.Point(1029, 1);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(149, 54);
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
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // picScreen
            // 
            this.picScreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picScreen.Location = new System.Drawing.Point(0, 0);
            this.picScreen.Name = "picScreen";
            this.picScreen.Size = new System.Drawing.Size(10, 10);
            this.picScreen.TabIndex = 38;
            this.picScreen.TabStop = false;
            this.picScreen.Visible = false;
            // 
            // frmChangeMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1178, 760);
            this.ControlBox = false;
            this.Controls.Add(this.picScreen);
            this.Controls.Add(this.pnlHead);
            this.Controls.Add(this.btnLoadScale);
            this.Controls.Add(this.lblLastTime);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlOrdreDetail);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmChangeMode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Shown += new System.EventHandler(this.frmChangeMode_Shown);
            this.pnlOrdreDetail.ResumeLayout(false);
            this.pnlOrdreDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picOnLine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picOffLine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picScreen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlOrdreDetail;
        private System.Windows.Forms.PictureBox picOnLine;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picOffLine;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblLastTime;
        private System.Windows.Forms.Button btnLoadScale;
        private System.Windows.Forms.Timer timerNow;
        private System.Windows.Forms.Panel pnlHead;
        private System.Windows.Forms.Button btnMenu;
        private System.Windows.Forms.Button btnWindows;
        private System.Windows.Forms.Label lblShopName;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.PictureBox picScreen;
        private System.Windows.Forms.Button btnOnLineType;
    }
}