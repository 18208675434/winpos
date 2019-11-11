namespace QiandamaPOS
{
    partial class frmLogin
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.lblSN = new System.Windows.Forms.Label();
            this.lblShopName = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.pnlUser = new System.Windows.Forms.Panel();
            this.txtPwd = new QiandamaPOS.UserControl.WaterTextBox();
            this.txtUser = new QiandamaPOS.UserControl.WaterTextBox();
            this.btnLoginByUser = new System.Windows.Forms.Button();
            this.chkAutoLoginUser = new System.Windows.Forms.CheckBox();
            this.pnlPhone = new System.Windows.Forms.Panel();
            this.txtPhoneCheckCode = new QiandamaPOS.UserControl.WaterTextBox();
            this.txtCheckCode = new QiandamaPOS.UserControl.WaterTextBox();
            this.txtPhone = new QiandamaPOS.UserControl.WaterTextBox();
            this.lblSendCheckCode = new System.Windows.Forms.Label();
            this.picCheckCode = new System.Windows.Forms.PictureBox();
            this.btnLoginByPhone = new System.Windows.Forms.Button();
            this.chkAutoLoginPhone = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.picExit = new System.Windows.Forms.PictureBox();
            this.timerNow = new System.Windows.Forms.Timer(this.components);
            this.pnlUser.SuspendLayout();
            this.pnlPhone.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCheckCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picExit)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSN
            // 
            this.lblSN.AutoSize = true;
            this.lblSN.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSN.Location = new System.Drawing.Point(12, 9);
            this.lblSN.Name = "lblSN";
            this.lblSN.Size = new System.Drawing.Size(164, 21);
            this.lblSN.TabIndex = 1;
            this.lblSN.Text = "设备号：1234567890";
            this.lblSN.Click += new System.EventHandler(this.lblSN_Click);
            // 
            // lblShopName
            // 
            this.lblShopName.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblShopName.Location = new System.Drawing.Point(312, 29);
            this.lblShopName.Name = "lblShopName";
            this.lblShopName.Size = new System.Drawing.Size(150, 25);
            this.lblShopName.TabIndex = 2;
            this.lblShopName.Text = "登录";
            this.lblShopName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.lblUser.Location = new System.Drawing.Point(203, 210);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(145, 30);
            this.lblUser.TabIndex = 3;
            this.lblUser.Text = "账号密码登录";
            this.lblUser.Click += new System.EventHandler(this.lblUser_Click);
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.lblPhone.Location = new System.Drawing.Point(397, 210);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(167, 30);
            this.lblPhone.TabIndex = 4;
            this.lblPhone.Text = "手机验证码登录";
            this.lblPhone.Click += new System.EventHandler(this.lblPhone_Click);
            // 
            // pnlUser
            // 
            this.pnlUser.Controls.Add(this.txtPwd);
            this.pnlUser.Controls.Add(this.txtUser);
            this.pnlUser.Controls.Add(this.btnLoginByUser);
            this.pnlUser.Controls.Add(this.chkAutoLoginUser);
            this.pnlUser.Location = new System.Drawing.Point(157, 279);
            this.pnlUser.Name = "pnlUser";
            this.pnlUser.Size = new System.Drawing.Size(441, 247);
            this.pnlUser.TabIndex = 6;
            // 
            // txtPwd
            // 
            this.txtPwd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPwd.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.txtPwd.Location = new System.Drawing.Point(31, 76);
            this.txtPwd.MaxLength = 18;
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(380, 36);
            this.txtPwd.TabIndex = 17;
            this.txtPwd.WaterText = "输入密码";
            // 
            // txtUser
            // 
            this.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUser.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.txtUser.Location = new System.Drawing.Point(31, 17);
            this.txtUser.MaxLength = 18;
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(380, 36);
            this.txtUser.TabIndex = 16;
            this.txtUser.WaterText = "输入11位手机号";
            // 
            // btnLoginByUser
            // 
            this.btnLoginByUser.BackColor = System.Drawing.Color.Transparent;
            this.btnLoginByUser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLoginByUser.BackgroundImage")));
            this.btnLoginByUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLoginByUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoginByUser.FlatAppearance.BorderSize = 0;
            this.btnLoginByUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoginByUser.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold);
            this.btnLoginByUser.ForeColor = System.Drawing.Color.White;
            this.btnLoginByUser.Location = new System.Drawing.Point(26, 175);
            this.btnLoginByUser.Name = "btnLoginByUser";
            this.btnLoginByUser.Size = new System.Drawing.Size(390, 60);
            this.btnLoginByUser.TabIndex = 8;
            this.btnLoginByUser.Text = "登  录";
            this.btnLoginByUser.UseVisualStyleBackColor = false;
            this.btnLoginByUser.Click += new System.EventHandler(this.btnLoginByUser_Click);
            // 
            // chkAutoLoginUser
            // 
            this.chkAutoLoginUser.AutoSize = true;
            this.chkAutoLoginUser.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkAutoLoginUser.Location = new System.Drawing.Point(32, 126);
            this.chkAutoLoginUser.Name = "chkAutoLoginUser";
            this.chkAutoLoginUser.Size = new System.Drawing.Size(134, 25);
            this.chkAutoLoginUser.TabIndex = 7;
            this.chkAutoLoginUser.Text = "近7天自动登录";
            this.chkAutoLoginUser.UseVisualStyleBackColor = true;
            // 
            // pnlPhone
            // 
            this.pnlPhone.Controls.Add(this.txtPhoneCheckCode);
            this.pnlPhone.Controls.Add(this.txtCheckCode);
            this.pnlPhone.Controls.Add(this.txtPhone);
            this.pnlPhone.Controls.Add(this.lblSendCheckCode);
            this.pnlPhone.Controls.Add(this.picCheckCode);
            this.pnlPhone.Controls.Add(this.btnLoginByPhone);
            this.pnlPhone.Controls.Add(this.chkAutoLoginPhone);
            this.pnlPhone.Location = new System.Drawing.Point(157, 280);
            this.pnlPhone.Name = "pnlPhone";
            this.pnlPhone.Size = new System.Drawing.Size(441, 279);
            this.pnlPhone.TabIndex = 9;
            // 
            // txtPhoneCheckCode
            // 
            this.txtPhoneCheckCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPhoneCheckCode.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.txtPhoneCheckCode.Location = new System.Drawing.Point(26, 123);
            this.txtPhoneCheckCode.MaxLength = 18;
            this.txtPhoneCheckCode.Name = "txtPhoneCheckCode";
            this.txtPhoneCheckCode.Size = new System.Drawing.Size(241, 36);
            this.txtPhoneCheckCode.TabIndex = 15;
            this.txtPhoneCheckCode.WaterText = "输入短信验证码";
            // 
            // txtCheckCode
            // 
            this.txtCheckCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCheckCode.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.txtCheckCode.Location = new System.Drawing.Point(26, 69);
            this.txtCheckCode.MaxLength = 18;
            this.txtCheckCode.Name = "txtCheckCode";
            this.txtCheckCode.Size = new System.Drawing.Size(241, 36);
            this.txtCheckCode.TabIndex = 14;
            this.txtCheckCode.WaterText = "输入右侧图形验证码";
            // 
            // txtPhone
            // 
            this.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPhone.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.txtPhone.Location = new System.Drawing.Point(27, 17);
            this.txtPhone.MaxLength = 18;
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(381, 36);
            this.txtPhone.TabIndex = 13;
            this.txtPhone.WaterText = "输入11位手机号";
            // 
            // lblSendCheckCode
            // 
            this.lblSendCheckCode.AutoSize = true;
            this.lblSendCheckCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSendCheckCode.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblSendCheckCode.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblSendCheckCode.Location = new System.Drawing.Point(283, 131);
            this.lblSendCheckCode.Name = "lblSendCheckCode";
            this.lblSendCheckCode.Size = new System.Drawing.Size(107, 25);
            this.lblSendCheckCode.TabIndex = 10;
            this.lblSendCheckCode.Text = "发送验证码";
            this.lblSendCheckCode.Click += new System.EventHandler(this.lblSendCheckCode_Click);
            // 
            // picCheckCode
            // 
            this.picCheckCode.Location = new System.Drawing.Point(288, 73);
            this.picCheckCode.Name = "picCheckCode";
            this.picCheckCode.Size = new System.Drawing.Size(120, 39);
            this.picCheckCode.TabIndex = 9;
            this.picCheckCode.TabStop = false;
            this.picCheckCode.Click += new System.EventHandler(this.picCheckCode_Click);
            // 
            // btnLoginByPhone
            // 
            this.btnLoginByPhone.BackColor = System.Drawing.Color.Transparent;
            this.btnLoginByPhone.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLoginByPhone.BackgroundImage")));
            this.btnLoginByPhone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLoginByPhone.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoginByPhone.FlatAppearance.BorderSize = 0;
            this.btnLoginByPhone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoginByPhone.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold);
            this.btnLoginByPhone.ForeColor = System.Drawing.Color.White;
            this.btnLoginByPhone.Location = new System.Drawing.Point(27, 202);
            this.btnLoginByPhone.Name = "btnLoginByPhone";
            this.btnLoginByPhone.Size = new System.Drawing.Size(390, 60);
            this.btnLoginByPhone.TabIndex = 8;
            this.btnLoginByPhone.Text = "登  录";
            this.btnLoginByPhone.UseVisualStyleBackColor = false;
            this.btnLoginByPhone.Click += new System.EventHandler(this.btnLoginByPhone_Click);
            // 
            // chkAutoLoginPhone
            // 
            this.chkAutoLoginPhone.AutoSize = true;
            this.chkAutoLoginPhone.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkAutoLoginPhone.Location = new System.Drawing.Point(27, 175);
            this.chkAutoLoginPhone.Name = "chkAutoLoginPhone";
            this.chkAutoLoginPhone.Size = new System.Drawing.Size(134, 25);
            this.chkAutoLoginPhone.TabIndex = 7;
            this.chkAutoLoginPhone.Text = "近7天自动登录";
            this.chkAutoLoginPhone.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(312, 57);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 150);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.BackColor = System.Drawing.Color.Gainsboro;
            this.lblMsg.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblMsg.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblMsg.Location = new System.Drawing.Point(170, 252);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 20);
            this.lblMsg.TabIndex = 11;
            // 
            // picExit
            // 
            this.picExit.BackColor = System.Drawing.Color.Transparent;
            this.picExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picExit.BackgroundImage")));
            this.picExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picExit.Location = new System.Drawing.Point(761, 15);
            this.picExit.Name = "picExit";
            this.picExit.Size = new System.Drawing.Size(30, 30);
            this.picExit.TabIndex = 12;
            this.picExit.TabStop = false;
            this.picExit.Click += new System.EventHandler(this.picExit_Click);
            // 
            // timerNow
            // 
            this.timerNow.Interval = 1000;
            this.timerNow.Tick += new System.EventHandler(this.timerNow_Tick);
            // 
            // frmLogin
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(810, 583);
            this.Controls.Add(this.picExit);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pnlPhone);
            this.Controls.Add(this.pnlUser);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.lblShopName);
            this.Controls.Add(this.lblSN);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLogin_FormClosing);
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.Shown += new System.EventHandler(this.frmLogin_Shown);
            this.SizeChanged += new System.EventHandler(this.frmLogin_SizeChanged);
            this.Click += new System.EventHandler(this.frmLogin_Click);
            this.pnlUser.ResumeLayout(false);
            this.pnlUser.PerformLayout();
            this.pnlPhone.ResumeLayout(false);
            this.pnlPhone.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCheckCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picExit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSN;
        private System.Windows.Forms.Label lblShopName;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Panel pnlUser;
        private System.Windows.Forms.CheckBox chkAutoLoginUser;
        private System.Windows.Forms.Button btnLoginByUser;
        private System.Windows.Forms.Panel pnlPhone;
        private System.Windows.Forms.Label lblSendCheckCode;
        private System.Windows.Forms.PictureBox picCheckCode;
        private System.Windows.Forms.Button btnLoginByPhone;
        private System.Windows.Forms.CheckBox chkAutoLoginPhone;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.PictureBox picExit;
        private UserControl.WaterTextBox txtPhone;
        private UserControl.WaterTextBox txtPwd;
        private UserControl.WaterTextBox txtUser;
        private UserControl.WaterTextBox txtPhoneCheckCode;
        private UserControl.WaterTextBox txtCheckCode;
        private System.Windows.Forms.Timer timerNow;
    }
}

