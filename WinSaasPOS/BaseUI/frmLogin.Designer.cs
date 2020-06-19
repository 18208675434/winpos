namespace WinSaasPOS
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
            this.lblShopName = new System.Windows.Forms.Label();
            this.lblLoginByUser = new System.Windows.Forms.Label();
            this.lblLoginByPhone = new System.Windows.Forms.Label();
            this.pnlUser = new System.Windows.Forms.Panel();
            this.rbtnLoginByUser = new WinSaasPOS.RoundButton();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.lblPwd = new System.Windows.Forms.Label();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.chkAutoLoginUser = new System.Windows.Forms.CheckBox();
            this.lblSendCheckCode = new System.Windows.Forms.Label();
            this.chkAutoLoginPhone = new System.Windows.Forms.CheckBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.timerNow = new System.Windows.Forms.Timer(this.components);
            this.txtSN = new System.Windows.Forms.TextBox();
            this.lblSN = new System.Windows.Forms.Label();
            this.pnlPhone = new System.Windows.Forms.Panel();
            this.rbtnLoginByPhone = new WinSaasPOS.RoundButton();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblPhoneCheckCode = new System.Windows.Forms.Label();
            this.lblCheckCode = new System.Windows.Forms.Label();
            this.txtPhoneCheckCode = new System.Windows.Forms.TextBox();
            this.picCheckCode = new System.Windows.Forms.PictureBox();
            this.txtCheckCode = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.picExit = new System.Windows.Forms.PictureBox();
            this.picTenantLogo = new System.Windows.Forms.PictureBox();
            this.btnWindows = new System.Windows.Forms.Button();
            this.lbtnChangeOffLine = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.timerTen = new System.Windows.Forms.Timer(this.components);
            this.pnlUser.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel8.SuspendLayout();
            this.pnlPhone.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCheckCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTenantLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblShopName
            // 
            this.lblShopName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblShopName.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.lblShopName.Location = new System.Drawing.Point(507, 37);
            this.lblShopName.Name = "lblShopName";
            this.lblShopName.Size = new System.Drawing.Size(162, 25);
            this.lblShopName.TabIndex = 2;
            this.lblShopName.Text = "登录";
            this.lblShopName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLoginByUser
            // 
            this.lblLoginByUser.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblLoginByUser.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblLoginByUser.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.lblLoginByUser.Location = new System.Drawing.Point(463, 188);
            this.lblLoginByUser.Name = "lblLoginByUser";
            this.lblLoginByUser.Size = new System.Drawing.Size(128, 29);
            this.lblLoginByUser.TabIndex = 3;
            this.lblLoginByUser.Text = "账号密码登录";
            this.lblLoginByUser.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblLoginByUser.Click += new System.EventHandler(this.lblLobinByUser_Click);
            // 
            // lblLoginByPhone
            // 
            this.lblLoginByPhone.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblLoginByPhone.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblLoginByPhone.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.lblLoginByPhone.Location = new System.Drawing.Point(597, 188);
            this.lblLoginByPhone.Name = "lblLoginByPhone";
            this.lblLoginByPhone.Size = new System.Drawing.Size(143, 29);
            this.lblLoginByPhone.TabIndex = 4;
            this.lblLoginByPhone.Text = "手机验证码登录";
            this.lblLoginByPhone.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblLoginByPhone.Click += new System.EventHandler(this.lblLoginByPhone_Click);
            // 
            // pnlUser
            // 
            this.pnlUser.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnlUser.Controls.Add(this.rbtnLoginByUser);
            this.pnlUser.Controls.Add(this.panel10);
            this.pnlUser.Controls.Add(this.panel8);
            this.pnlUser.Controls.Add(this.lblPwd);
            this.pnlUser.Controls.Add(this.txtPwd);
            this.pnlUser.Controls.Add(this.lblUser);
            this.pnlUser.Controls.Add(this.txtUser);
            this.pnlUser.Location = new System.Drawing.Point(368, 236);
            this.pnlUser.Name = "pnlUser";
            this.pnlUser.Size = new System.Drawing.Size(440, 247);
            this.pnlUser.TabIndex = 6;
            // 
            // rbtnLoginByUser
            // 
            this.rbtnLoginByUser.AllBackColor = System.Drawing.Color.OrangeRed;
            this.rbtnLoginByUser.BackColor = System.Drawing.Color.OrangeRed;
            this.rbtnLoginByUser.Image = null;
            this.rbtnLoginByUser.ImageSize = new System.Drawing.Size(0, 0);
            this.rbtnLoginByUser.Location = new System.Drawing.Point(42, 171);
            this.rbtnLoginByUser.Name = "rbtnLoginByUser";
            this.rbtnLoginByUser.PenColor = System.Drawing.Color.Black;
            this.rbtnLoginByUser.PenWidth = 1;
            this.rbtnLoginByUser.RoundRadius = 45;
            this.rbtnLoginByUser.ShowImg = false;
            this.rbtnLoginByUser.ShowText = "登录";
            this.rbtnLoginByUser.Size = new System.Drawing.Size(353, 46);
            this.rbtnLoginByUser.TabIndex = 33;
            this.rbtnLoginByUser.TextForeColor = System.Drawing.Color.White;
            this.rbtnLoginByUser.WhetherEnable = true;
            this.rbtnLoginByUser.ButtonClick += new System.EventHandler(this.btnLoginByUser_Click);
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.panel11);
            this.panel10.Location = new System.Drawing.Point(42, 123);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(354, 13);
            this.panel10.TabIndex = 29;
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.DarkGray;
            this.panel11.Location = new System.Drawing.Point(0, 6);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(354, 1);
            this.panel11.TabIndex = 0;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.panel9);
            this.panel8.Location = new System.Drawing.Point(42, 63);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(354, 13);
            this.panel8.TabIndex = 29;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.DarkGray;
            this.panel9.Location = new System.Drawing.Point(0, 6);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(354, 1);
            this.panel9.TabIndex = 0;
            // 
            // lblPwd
            // 
            this.lblPwd.AutoSize = true;
            this.lblPwd.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblPwd.ForeColor = System.Drawing.Color.DarkGray;
            this.lblPwd.Location = new System.Drawing.Point(43, 91);
            this.lblPwd.Name = "lblPwd";
            this.lblPwd.Size = new System.Drawing.Size(90, 21);
            this.lblPwd.TabIndex = 23;
            this.lblPwd.Text = "请输入密码";
            this.lblPwd.Click += new System.EventHandler(this.lblPwd_Click);
            // 
            // txtPwd
            // 
            this.txtPwd.BackColor = System.Drawing.Color.Gainsboro;
            this.txtPwd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPwd.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.txtPwd.Location = new System.Drawing.Point(42, 87);
            this.txtPwd.MaxLength = 18;
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(347, 29);
            this.txtPwd.TabIndex = 21;
            this.txtPwd.Click += new System.EventHandler(this.txt_OskClick);
            this.txtPwd.TextChanged += new System.EventHandler(this.txtPwd_TextChanged);
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblUser.ForeColor = System.Drawing.Color.DarkGray;
            this.lblUser.Location = new System.Drawing.Point(43, 29);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(140, 21);
            this.lblUser.TabIndex = 20;
            this.lblUser.Text = "请输入11位手机号";
            this.lblUser.Click += new System.EventHandler(this.lblUser_Click);
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.Color.Gainsboro;
            this.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUser.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.txtUser.Location = new System.Drawing.Point(42, 24);
            this.txtUser.MaxLength = 11;
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(347, 29);
            this.txtUser.TabIndex = 18;
            this.txtUser.Click += new System.EventHandler(this.txt_OskClick);
            this.txtUser.TextChanged += new System.EventHandler(this.txtUser_TextChanged);
            this.txtUser.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextNUMBER_KeyPress);
            // 
            // chkAutoLoginUser
            // 
            this.chkAutoLoginUser.AutoSize = true;
            this.chkAutoLoginUser.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkAutoLoginUser.Location = new System.Drawing.Point(10, 603);
            this.chkAutoLoginUser.Name = "chkAutoLoginUser";
            this.chkAutoLoginUser.Size = new System.Drawing.Size(134, 25);
            this.chkAutoLoginUser.TabIndex = 7;
            this.chkAutoLoginUser.Text = "近7天自动登录";
            this.chkAutoLoginUser.UseVisualStyleBackColor = true;
            this.chkAutoLoginUser.Visible = false;
            // 
            // lblSendCheckCode
            // 
            this.lblSendCheckCode.AutoSize = true;
            this.lblSendCheckCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSendCheckCode.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblSendCheckCode.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblSendCheckCode.Location = new System.Drawing.Point(310, 143);
            this.lblSendCheckCode.Name = "lblSendCheckCode";
            this.lblSendCheckCode.Size = new System.Drawing.Size(79, 20);
            this.lblSendCheckCode.TabIndex = 10;
            this.lblSendCheckCode.Text = "发送验证码";
            this.lblSendCheckCode.Click += new System.EventHandler(this.lblSendCheckCode_Click);
            // 
            // chkAutoLoginPhone
            // 
            this.chkAutoLoginPhone.AutoSize = true;
            this.chkAutoLoginPhone.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkAutoLoginPhone.Location = new System.Drawing.Point(244, 603);
            this.chkAutoLoginPhone.Name = "chkAutoLoginPhone";
            this.chkAutoLoginPhone.Size = new System.Drawing.Size(134, 25);
            this.chkAutoLoginPhone.TabIndex = 7;
            this.chkAutoLoginPhone.Text = "近7天自动登录";
            this.chkAutoLoginPhone.UseVisualStyleBackColor = true;
            this.chkAutoLoginPhone.Visible = false;
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.BackColor = System.Drawing.Color.Gainsboro;
            this.lblMsg.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblMsg.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblMsg.Location = new System.Drawing.Point(364, 212);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(21, 20);
            this.lblMsg.TabIndex = 11;
            this.lblMsg.Text = "--";
            this.lblMsg.Visible = false;
            // 
            // timerNow
            // 
            this.timerNow.Interval = 1000;
            this.timerNow.Tick += new System.EventHandler(this.timerNow_Tick);
            // 
            // txtSN
            // 
            this.txtSN.BackColor = System.Drawing.Color.Gainsboro;
            this.txtSN.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSN.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.txtSN.Location = new System.Drawing.Point(112, 9);
            this.txtSN.Name = "txtSN";
            this.txtSN.ReadOnly = true;
            this.txtSN.Size = new System.Drawing.Size(180, 18);
            this.txtSN.TabIndex = 17;
            // 
            // lblSN
            // 
            this.lblSN.AutoSize = true;
            this.lblSN.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblSN.Location = new System.Drawing.Point(54, 9);
            this.lblSN.Name = "lblSN";
            this.lblSN.Size = new System.Drawing.Size(65, 20);
            this.lblSN.TabIndex = 16;
            this.lblSN.Text = "设备号：";
            this.lblSN.Click += new System.EventHandler(this.lblSN_Click);
            // 
            // pnlPhone
            // 
            this.pnlPhone.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnlPhone.Controls.Add(this.rbtnLoginByPhone);
            this.pnlPhone.Controls.Add(this.panel6);
            this.pnlPhone.Controls.Add(this.panel4);
            this.pnlPhone.Controls.Add(this.panel2);
            this.pnlPhone.Controls.Add(this.lblSendCheckCode);
            this.pnlPhone.Controls.Add(this.panel1);
            this.pnlPhone.Controls.Add(this.lblPhoneCheckCode);
            this.pnlPhone.Controls.Add(this.lblCheckCode);
            this.pnlPhone.Controls.Add(this.txtPhoneCheckCode);
            this.pnlPhone.Controls.Add(this.picCheckCode);
            this.pnlPhone.Controls.Add(this.txtCheckCode);
            this.pnlPhone.Controls.Add(this.lblPhone);
            this.pnlPhone.Controls.Add(this.txtPhone);
            this.pnlPhone.Location = new System.Drawing.Point(368, 236);
            this.pnlPhone.Name = "pnlPhone";
            this.pnlPhone.Size = new System.Drawing.Size(440, 296);
            this.pnlPhone.TabIndex = 24;
            this.pnlPhone.Visible = false;
            // 
            // rbtnLoginByPhone
            // 
            this.rbtnLoginByPhone.AllBackColor = System.Drawing.Color.OrangeRed;
            this.rbtnLoginByPhone.BackColor = System.Drawing.Color.OrangeRed;
            this.rbtnLoginByPhone.Image = null;
            this.rbtnLoginByPhone.ImageSize = new System.Drawing.Size(0, 0);
            this.rbtnLoginByPhone.Location = new System.Drawing.Point(41, 214);
            this.rbtnLoginByPhone.Name = "rbtnLoginByPhone";
            this.rbtnLoginByPhone.PenColor = System.Drawing.Color.Black;
            this.rbtnLoginByPhone.PenWidth = 1;
            this.rbtnLoginByPhone.RoundRadius = 45;
            this.rbtnLoginByPhone.ShowImg = false;
            this.rbtnLoginByPhone.ShowText = "登录";
            this.rbtnLoginByPhone.Size = new System.Drawing.Size(354, 46);
            this.rbtnLoginByPhone.TabIndex = 34;
            this.rbtnLoginByPhone.TextForeColor = System.Drawing.Color.White;
            this.rbtnLoginByPhone.WhetherEnable = true;
            this.rbtnLoginByPhone.ButtonClick += new System.EventHandler(this.btnLoginByPhone_Click);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.panel7);
            this.panel6.Location = new System.Drawing.Point(41, 174);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(354, 13);
            this.panel6.TabIndex = 30;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.DarkGray;
            this.panel7.Location = new System.Drawing.Point(0, 6);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(354, 1);
            this.panel7.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Location = new System.Drawing.Point(41, 122);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(354, 13);
            this.panel4.TabIndex = 29;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.DarkGray;
            this.panel5.Location = new System.Drawing.Point(0, 6);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(354, 1);
            this.panel5.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(41, 59);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(354, 13);
            this.panel2.TabIndex = 28;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DarkGray;
            this.panel3.Location = new System.Drawing.Point(0, 6);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(354, 1);
            this.panel3.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkGray;
            this.panel1.Location = new System.Drawing.Point(300, 137);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1, 30);
            this.panel1.TabIndex = 27;
            // 
            // lblPhoneCheckCode
            // 
            this.lblPhoneCheckCode.AutoSize = true;
            this.lblPhoneCheckCode.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblPhoneCheckCode.ForeColor = System.Drawing.Color.DarkGray;
            this.lblPhoneCheckCode.Location = new System.Drawing.Point(43, 144);
            this.lblPhoneCheckCode.Name = "lblPhoneCheckCode";
            this.lblPhoneCheckCode.Size = new System.Drawing.Size(106, 21);
            this.lblPhoneCheckCode.TabIndex = 26;
            this.lblPhoneCheckCode.Text = "请输入验证码";
            this.lblPhoneCheckCode.Click += new System.EventHandler(this.lblPhoneCheckCode_Click);
            // 
            // lblCheckCode
            // 
            this.lblCheckCode.AutoSize = true;
            this.lblCheckCode.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblCheckCode.ForeColor = System.Drawing.Color.DarkGray;
            this.lblCheckCode.Location = new System.Drawing.Point(38, 92);
            this.lblCheckCode.Name = "lblCheckCode";
            this.lblCheckCode.Size = new System.Drawing.Size(170, 21);
            this.lblCheckCode.TabIndex = 23;
            this.lblCheckCode.Text = "请输入右侧图形验证码";
            this.lblCheckCode.Click += new System.EventHandler(this.lblCheckCode_Click);
            // 
            // txtPhoneCheckCode
            // 
            this.txtPhoneCheckCode.BackColor = System.Drawing.Color.Gainsboro;
            this.txtPhoneCheckCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPhoneCheckCode.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.txtPhoneCheckCode.Location = new System.Drawing.Point(42, 139);
            this.txtPhoneCheckCode.MaxLength = 8;
            this.txtPhoneCheckCode.Name = "txtPhoneCheckCode";
            this.txtPhoneCheckCode.Size = new System.Drawing.Size(249, 29);
            this.txtPhoneCheckCode.TabIndex = 24;
            this.txtPhoneCheckCode.Click += new System.EventHandler(this.txt_OskClick);
            this.txtPhoneCheckCode.TextChanged += new System.EventHandler(this.txtPhoneCheckCode_TextChanged);
            this.txtPhoneCheckCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextNUMBER_KeyPress);
            // 
            // picCheckCode
            // 
            this.picCheckCode.BackColor = System.Drawing.Color.Gainsboro;
            this.picCheckCode.Location = new System.Drawing.Point(312, 80);
            this.picCheckCode.Name = "picCheckCode";
            this.picCheckCode.Size = new System.Drawing.Size(80, 35);
            this.picCheckCode.TabIndex = 9;
            this.picCheckCode.TabStop = false;
            this.picCheckCode.Click += new System.EventHandler(this.picCheckCode_Click);
            // 
            // txtCheckCode
            // 
            this.txtCheckCode.BackColor = System.Drawing.Color.Gainsboro;
            this.txtCheckCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCheckCode.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.txtCheckCode.Location = new System.Drawing.Point(37, 87);
            this.txtCheckCode.Name = "txtCheckCode";
            this.txtCheckCode.Size = new System.Drawing.Size(264, 29);
            this.txtCheckCode.TabIndex = 21;
            this.txtCheckCode.Click += new System.EventHandler(this.txt_OskClick);
            this.txtCheckCode.TextChanged += new System.EventHandler(this.txtCheckCode_TextChanged);
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblPhone.ForeColor = System.Drawing.Color.DarkGray;
            this.lblPhone.Location = new System.Drawing.Point(43, 29);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(140, 21);
            this.lblPhone.TabIndex = 20;
            this.lblPhone.Text = "请输入11位手机号";
            this.lblPhone.Click += new System.EventHandler(this.lblPhone_Click);
            // 
            // txtPhone
            // 
            this.txtPhone.BackColor = System.Drawing.Color.Gainsboro;
            this.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPhone.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.txtPhone.Location = new System.Drawing.Point(42, 24);
            this.txtPhone.MaxLength = 11;
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(347, 29);
            this.txtPhone.TabIndex = 18;
            this.txtPhone.Click += new System.EventHandler(this.txt_OskClick);
            this.txtPhone.TextChanged += new System.EventHandler(this.txtPhone_TextChanged);
            this.txtPhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextNUMBER_KeyPress);
            // 
            // picExit
            // 
            this.picExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picExit.BackColor = System.Drawing.Color.Transparent;
            this.picExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picExit.BackgroundImage")));
            this.picExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picExit.Location = new System.Drawing.Point(1143, 8);
            this.picExit.Name = "picExit";
            this.picExit.Size = new System.Drawing.Size(28, 26);
            this.picExit.TabIndex = 12;
            this.picExit.TabStop = false;
            this.picExit.Click += new System.EventHandler(this.picExit_Click);
            // 
            // picTenantLogo
            // 
            this.picTenantLogo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.picTenantLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picTenantLogo.Location = new System.Drawing.Point(532, 65);
            this.picTenantLogo.Name = "picTenantLogo";
            this.picTenantLogo.Size = new System.Drawing.Size(110, 110);
            this.picTenantLogo.TabIndex = 10;
            this.picTenantLogo.TabStop = false;
            // 
            // btnWindows
            // 
            this.btnWindows.BackColor = System.Drawing.Color.Gainsboro;
            this.btnWindows.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnWindows.BackgroundImage")));
            this.btnWindows.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnWindows.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnWindows.FlatAppearance.BorderSize = 0;
            this.btnWindows.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWindows.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnWindows.ForeColor = System.Drawing.Color.White;
            this.btnWindows.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnWindows.Location = new System.Drawing.Point(6, 6);
            this.btnWindows.Name = "btnWindows";
            this.btnWindows.Size = new System.Drawing.Size(32, 26);
            this.btnWindows.TabIndex = 39;
            this.btnWindows.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnWindows.UseVisualStyleBackColor = false;
            this.btnWindows.Click += new System.EventHandler(this.btnWindows_Click);
            // 
            // lbtnChangeOffLine
            // 
            this.lbtnChangeOffLine.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbtnChangeOffLine.AutoSize = true;
            this.lbtnChangeOffLine.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbtnChangeOffLine.LinkColor = System.Drawing.Color.SteelBlue;
            this.lbtnChangeOffLine.Location = new System.Drawing.Point(587, 556);
            this.lbtnChangeOffLine.Name = "lbtnChangeOffLine";
            this.lbtnChangeOffLine.Size = new System.Drawing.Size(106, 21);
            this.lbtnChangeOffLine.TabIndex = 46;
            this.lbtnChangeOffLine.TabStop = true;
            this.lbtnChangeOffLine.Text = "离线收银模式";
            this.lbtnChangeOffLine.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbtnChangeOffLine_LinkClicked);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(450, 556);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 21);
            this.label2.TabIndex = 45;
            this.label2.Text = "网络异常？切换为";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(128, 302);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 69);
            this.button1.TabIndex = 47;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // timerTen
            // 
            this.timerTen.Tick += new System.EventHandler(this.timerTen_Tick);
            // 
            // frmLogin
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1180, 760);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbtnChangeOffLine);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnWindows);
            this.Controls.Add(this.pnlUser);
            this.Controls.Add(this.pnlPhone);
            this.Controls.Add(this.txtSN);
            this.Controls.Add(this.lblSN);
            this.Controls.Add(this.picExit);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.picTenantLogo);
            this.Controls.Add(this.chkAutoLoginPhone);
            this.Controls.Add(this.chkAutoLoginUser);
            this.Controls.Add(this.lblLoginByPhone);
            this.Controls.Add(this.lblLoginByUser);
            this.Controls.Add(this.lblShopName);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.frmLogin_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLogin_FormClosing);
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.Shown += new System.EventHandler(this.frmLogin_Shown);
            this.SizeChanged += new System.EventHandler(this.frmLogin_SizeChanged);
            this.Click += new System.EventHandler(this.frmLogin_Click);
            this.pnlUser.ResumeLayout(false);
            this.pnlUser.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.pnlPhone.ResumeLayout(false);
            this.pnlPhone.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCheckCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTenantLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblShopName;
        private System.Windows.Forms.Label lblLoginByUser;
        private System.Windows.Forms.Label lblLoginByPhone;
        private System.Windows.Forms.Panel pnlUser;
        private System.Windows.Forms.CheckBox chkAutoLoginUser;
        private System.Windows.Forms.Label lblSendCheckCode;
        private System.Windows.Forms.PictureBox picCheckCode;
        private System.Windows.Forms.CheckBox chkAutoLoginPhone;
        private System.Windows.Forms.PictureBox picTenantLogo;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.PictureBox picExit;
        private System.Windows.Forms.Timer timerNow;
        private System.Windows.Forms.TextBox txtSN;
        private System.Windows.Forms.Label lblSN;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label lblPwd;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Panel pnlPhone;
        private System.Windows.Forms.Label lblCheckCode;
        private System.Windows.Forms.TextBox txtCheckCode;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblPhoneCheckCode;
        private System.Windows.Forms.TextBox txtPhoneCheckCode;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnWindows;
        private System.Windows.Forms.LinkLabel lbtnChangeOffLine;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timerTen;
        private RoundButton rbtnLoginByUser;
        private RoundButton rbtnLoginByPhone;
    }
}

