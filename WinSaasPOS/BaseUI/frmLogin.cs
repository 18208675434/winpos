using WinSaasPOS.Common;
using WinSaasPOS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading;
using System.Windows.Forms;
using Maticsoft.BLL;
using Newtonsoft.Json;
using Maticsoft.Model;
using System.Runtime.Remoting.Messaging;

namespace WinSaasPOS
{
    public partial class frmLogin : Form
    {

        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();

        //图形验证码
        ValidCode validCode = new ValidCode(4, ValidCode.CodeType.Alphas);

        HttpUtil httputil = new HttpUtil();

        AutoSizeFormUtil asf = new AutoSizeFormUtil();
        public frmLogin()
        {
            InitializeComponent();
            MainModel.IsOffLine = false;
            INIManager.SetIni("System", "IsOffLine", "0", MainModel.IniPath);
        }

        private void frmLogin_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            MainModel.HideTask(); 
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Log"))
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Log");


            //用户控件没有重新绘图 修改大小才会触发roundradius
            rbtnLoginByPhone.RoundRadius = rbtnLoginByPhone.Height;
            rbtnLoginByUser.RoundRadius = rbtnLoginByUser.Height;
            rbtnLoginByUser.Height += 1;
            rbtnLoginByPhone.Height += 1;

            Application.DoEvents();

            Thread threadItemExedate = new Thread(ThreadUpStart);
            threadItemExedate.IsBackground = true;
            threadItemExedate.Start();

            //IniForm();
            Thread threadIniFormExedate = new Thread(IniForm);
            threadIniFormExedate.IsBackground = true;
            threadIniFormExedate.Start();

            //每十分钟更新一次
            timerTen.Interval = 10 * 60 * 1000;
            timerTen.Enabled = true;
          
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            MainModel.IsOffLine = false;
            INIManager.SetIni("System", "IsOffLine", "0", MainModel.IniPath);
            //每次开启页面都要加载，页面关闭值null
            string imgname = "LoginLogo.bmp";
            if (File.Exists(MainModel.MediaPath + imgname))
            {
                picTenantLogo.BackgroundImage = Image.FromFile(MainModel.MediaPath + imgname);

            }
            else
            {

            }
            Application.DoEvents();
            lblLobinByUser_Click(null, null);
        }

        private void IniForm()
        {

            this.Invoke(new InvokeHandler(delegate()
        {

            this.Enabled = false;
            LoadingHelper.ShowLoadingScreen("加载中...");

            //电脑可能会有多个mac地址，取第一次获取的mac地址为准  同时同步start.exe 获取的mac
            string currentmac = "";
            try
            {
                if (File.Exists(MainModel.StartIniPath))
                {
                     currentmac = INIManager.GetIni("System", "DeviceSN", MainModel.StartIniPath);
                }
                else
                {
                     currentmac = INIManager.GetIni("System", "DeviceSN", MainModel.IniPath);
                }
            }
            catch { }

            string devicesn = GlobalUtil.GetMacAddress(currentmac);



            //没有网络的时候获取不到MAC地址  ？？？  会被替换
            if (devicesn.Length > 10)
            {
                INIManager.SetIni("System", "DeviceSN", devicesn, MainModel.IniPath);

            }
            MainModel.DeviceSN = devicesn;
            //lblSN.Text = "设备序列号：" + devicesn;
            txtSN.Text = devicesn;

            try
            {
              //  LoadingHelper.ShowLoadingScreen("用户/门店信息验证中，请稍候");
                //lblMsg.Text = "用户/门店信息验证中，请稍候...";
                Application.DoEvents();
                if (!LoadUser() || !LoadShopInfo())
                {
                    this.Enabled = true;
                    LoadingHelper.CloseForm();
                    //lblMsg.Text = "验证失败，请重新登录";
                    Application.DoEvents();
                    txtUser.Clear();
                    txtPwd.Clear();
                    isReLogin = true;
                    int screenwdith = Screen.AllScreens[0].Bounds.Width;

                    lblLobinByUser_Click(null, null);

                }
                else
                {
                    this.Enabled = true;
                    LoadingHelper.CloseForm();

                    ClearText();
                    frmMainHalfOffLine frmmain = new frmMainHalfOffLine(this);

                    asf.AutoScaleControlTest(frmmain, 1178, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                    picTenantLogo.BackgroundImage = null;
                    frmmain.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("登录异常：" + ex.Message+ex.StackTrace);
            }
            finally
            {
                this.Enabled = true;
                LoadingHelper.CloseForm();
            }
        }));
        }

        #region 账号密码登录

        private void lblLobinByUser_Click(object sender, EventArgs e)
        {
            //lblLoginByUser.ForeColor = Color.Green;
            //lblLoginByPhone.ForeColor = Color.Black;

            lblLoginByUser.Image = Resources.ResourcePos.Line_red;
            lblLoginByUser.Font = new Font(lblLoginByUser.Font.Name,lblLoginByUser.Font.Size,FontStyle.Bold);
            lblLoginByPhone.Image = null;
            lblLoginByPhone.Font = new Font(lblLoginByPhone.Font.Name, lblLoginByPhone.Font.Size, FontStyle.Regular);

            pnlUser.Visible = true;
            pnlPhone.Visible = false;
        }


        bool isReLogin = false;
        private void btnLoginByUser_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtSN.Text.Length < 10)
                {
                    GetDeviceSn();
                }

                lblMsg.Text = "";
                if (!CheckPhone(txtUser.Text))
                {
                    return;
                }

                if (txtPwd.Text.Length == 0)
                {
                    MainModel.ShowLog("密码不能为空",false);
                    return;
                }
            this.Enabled=false;
                LoadingHelper.ShowLoadingScreen();
            //TODO  调用接口 验证用户
            string username = txtUser.Text;
            string password = txtPwd.Text;
           // lblMsg.Text = "";

          
           
                string ErrorMsg = "";
                string Token = httputil.Signin(username,password,ref ErrorMsg);
                if (ErrorMsg != "" || Token=="")
                {
                    lblMsg.Text = ErrorMsg;
                    MainModel.ShowLog(ErrorMsg,false);
                }
                else
                {
                    INIManager.SetIni("System", "POS-Authorization", Token, MainModel.IniPath);
                    INIManager.SetIni("OffLine", "POS-Authorization", Token, MainModel.IniPath);
                    MainModel.Authorization = Token;
                    if (!LoadUser() || !LoadShopInfo())
                    {
                        return;
                    }
                   

                    INIManager.SetIni("System", "UserName", username, MainModel.IniPath);
                    INIManager.SetIni("System", "PassWord", password, MainModel.IniPath);

                    ClearText();
                    frmMainHalfOffLine frmmain = new frmMainHalfOffLine(this);

                    asf.AutoScaleControlTest(frmmain, 1178, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                    LoadingHelper.CloseForm();
                    picTenantLogo.BackgroundImage = null;
                    //this.Hide();
                    //CloseOSK();
                    frmmain.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("登录异常：" + ex.Message+ex.StackTrace, false);
            }
            finally
            {
                LoadingHelper.CloseForm();
                this.Enabled = true;
            }
            
        }
        
        #endregion

        #region 手机验证码登录
        private void lblLoginByPhone_Click(object sender, EventArgs e)
        {
            //lblLoginByUser.ForeColor = Color.Black;
            //lblLoginByPhone.ForeColor = Color.Green;



            lblLoginByUser.Image = null;
            lblLoginByUser.Font = new Font(lblLoginByUser.Font.Name, lblLoginByUser.Font.Size, FontStyle.Regular);
            lblLoginByPhone.Image = Resources.ResourcePos.Line_red;
            lblLoginByPhone.Font = new Font(lblLoginByPhone.Font.Name, lblLoginByPhone.Font.Size, FontStyle.Bold);

            pnlUser.Visible = false;
            pnlPhone.Visible = true;

            Application.DoEvents();
            //手机验证页面 先刷新一次图形验证码
            //this.picCheckCode.Image = Bitmap.FromStream(validCode.CreateCheckCodeImage());

            GetAuthcodeImage();
           
        }

        private void lblSendCheckCode_Click(object sender, EventArgs e)
        {
            if (!CheckPhone(txtPhone.Text))
            {
                return;
            }
                               
                    if (lblSendCheckCode.Text == "发送验证码")
                    {
                        string ErrorMsg = "";
                        if (httputil.SendSmsCode(txtPhone.Text, CurrentImgCodeKey, txtCheckCode.Text, ref ErrorMsg))
                        {
                            lblSendCheckCode.Text = "60";
                            timerNow.Enabled = true;
                        }
                        else
                        {
                            //lblMsg.Text = ErrorMsg;
                            MainModel.ShowLog(ErrorMsg, false);
                        }                        
                    }
        }

        private void btnLoginByPhone_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSN.Text.Length < 10)
                {
                    GetDeviceSn();
                }

                lblMsg.Text = "";
                if (!CheckPhone(txtPhone.Text))
                {
                    return;
                }
                if (txtCheckCode.Text.Length == 0)
                {
                    MainModel.ShowLog("图形验证码不能为空",false);
                    return;
                }

                if (txtPhoneCheckCode.Text.Length == 0)
                {
                    MainModel.ShowLog("短信验证码不能为空", false);
                    return;
                }

                this.Enabled = false;
                LoadingHelper.ShowLoadingScreen();
                SignPara signpara = new SignPara();
                signpara.imgcode = txtCheckCode.Text;
                signpara.imgcodekey = CurrentImgCodeKey;
                signpara.phone = txtPhone.Text;
                signpara.smscode = txtPhoneCheckCode.Text;
               // lblMsg.Text = "";
                string ErrorMsg = "";
                string Token = httputil.SigninWithSmscode(signpara, ref ErrorMsg);
                if (ErrorMsg != "" || Token == "")
                {
                   // lblMsg.Text = ErrorMsg;
                    MainModel.ShowLog(ErrorMsg, false);
                }
                else
                {
                    INIManager.SetIni("System", "POS-Authorization", Token, MainModel.IniPath);
                    INIManager.SetIni("OffLine", "POS-Authorization", Token, MainModel.IniPath);
                    MainModel.Authorization = Token;
                    if (!LoadUser() || !LoadShopInfo())
                    {
                        return;
                    }

                    ClearText();
                    frmMainHalfOffLine frmmain = new frmMainHalfOffLine(this);

                    asf.AutoScaleControlTest(frmmain, 1178, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                    LoadingHelper.CloseForm();
                    picTenantLogo.BackgroundImage = null;
                    //this.Hide();
                    //CloseOSK();

                    frmmain.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("登录异常："+ex.Message+ex.StackTrace,false);
            }
            finally
            {
                LoadingHelper.CloseForm();
                this.Enabled = true;
            }
        }
        #endregion

        private void picCheckCode_Click(object sender, EventArgs e)
        {
            //点击图片 刷新验证码
           // this.picCheckCode.Image = Bitmap.FromStream(validCode.CreateCheckCodeImage());

            GetAuthcodeImage();
        }



        private void frmLogin_Click(object sender, EventArgs e)
        {
            //CloseOSK();
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                try
                {
                    GlobalUtil.CloseOSK();
                    WinSaasPOS.Model.MainModel.ShowTask();
                    this.Dispose();
                }
                catch { }
                
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("清理登录页面资源异常"+ex.Message);
            }
        }


        private bool LoadUser()
        {
            try
            {
                if (string.IsNullOrEmpty(MainModel.Authorization))
                {
                    return false;
                }

                string ErrorMsg = "";
                userModel currentuser = httputil.GetUser(ref ErrorMsg);

                if (ErrorMsg != "" || currentuser == null)
                {
                   // lblMsg.Text = "获取用户信息异常，请重新登录";
                    MainModel.ShowLog(ErrorMsg, false);
                    LogManager.WriteLog("ERROR",ErrorMsg);
                    return false;
                }
                else
                {
                    MainModel.CurrentUser = currentuser;
                    return true;
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("获取用户信息异常：" + ex.Message, false);
                LogManager.WriteLog("获取用户信息异常："+ex.Message);
                //lblMsg.Text = "获取用户信息异常，请重新登录";
                return false;
            }
        }

        private JSON_BEANBLL jsonbll = new JSON_BEANBLL();
        private bool LoadShopInfo()
        {

            try
            {
                string ErrorMsg = "";
                DeviceShopInfo  shopinfo = httputil.GetShopInfo(MainModel.DeviceSN, ref ErrorMsg);

                if (ErrorMsg != "" || shopinfo == null)
                {
                    //lblMsg.Text = "获取门店信息异常，请重新登录";
                    MainModel.ShowLog(ErrorMsg, false);
                    LogManager.WriteLog("ERROR", ErrorMsg);
                    return false;
                }
                else
                {
                    MainModel.CurrentShopInfo = shopinfo;
                    MainModel.ShopName = shopinfo.shopname;


                    jsonbll.Delete("SHOPINFO");
                    JSON_BEANMODEL jsonmodel = new JSON_BEANMODEL();
                    jsonmodel.CONDITION = "SHOPINFO";
                    jsonmodel.CREATE_TIME = DateTime.Now.ToString("yyyyMMddHHmmss");
                    jsonmodel.DEVICESN = MainModel.DeviceSN;
                    jsonmodel.CREATE_URL_IP = MainModel.URL;
                    jsonmodel.JSON = JsonConvert.SerializeObject(shopinfo);
                    jsonbll.Add(jsonmodel);
                  

                    return true;
                }
            }
            catch (Exception ex)
            {

                MainModel.ShowLog("获取门店信息异常：" + ex.Message, false);
                LogManager.WriteLog("获取门店信息异常：" + ex.Message);
                //lblMsg.Text = "获取门店信息异常，请重新登录：";
               
                return false;
            }
        }

        private void frmLogin_SizeChanged(object sender, EventArgs e)
        {
           asf.ControlAutoSize(this);
        }


        private string CurrentImgCodeKey = "";
        private void GetAuthcodeImage()
        {
            try
            {
                string ErrorMsg = "";

                AuthCodeImage authcodeimage = httputil.GetAuthcodeImage(ref ErrorMsg);
                CurrentImgCodeKey = authcodeimage.key;
                string ss = authcodeimage.imagestr;
                int startIndex = ss.IndexOf("base64,");//开始位置

                string inputStr = ss.Substring(startIndex + 7, ss.Length - startIndex - 7);//从开始位置截取一个新的字符串

                byte[] arr = Convert.FromBase64String(inputStr);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);

                picCheckCode.Image = bmp;
                ms.Close();

                Image _image = bmp;
            }
            catch (Exception ex)
            {

            }
        }


        private void Base64StringToImage(string txtFileName)
        {
            try
            {
                //FileStream ifs = new FileStream(txtFileName, FileMode.Open, FileAccess.Read);
                //StreamReader sr = new StreamReader(ifs);

                String inputStr = txtFileName;
                byte[] arr = Convert.FromBase64String(inputStr);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);

                //bmp.Save(txtFileName + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                //bmp.Save(txtFileName + ".bmp", ImageFormat.Bmp);
                //bmp.Save(txtFileName + ".gif", ImageFormat.Gif);
                //bmp.Save(txtFileName + ".png", ImageFormat.Png);
                ms.Close();

                Image _image = bmp;
                if (File.Exists(txtFileName))
                {
                    File.Delete(txtFileName);
                }
                //MessageBox.Show("转换成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Base64StringToImage 转换失败\nException：" + ex.Message);
            }
        }

        private void picExit_Click(object sender, EventArgs e)
        {
            frmDeleteGood frmdelete = new frmDeleteGood("是否确认退出系统？", "", "");
            
            if (frmdelete.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                GlobalUtil.CloseOSK();
                WinSaasPOS.Model.MainModel.ShowTask();
            }
            catch { }
            System.Environment.Exit(0);
        }


        private void ThreadUpStart()
        {
            try 
            {
                File.Copy(MainModel.TempFilePath + "\\锥智收银机.exe", MainModel.ServerPath + "\\锥智收银机.exe", true);
                File.Copy(MainModel.TempFilePath + "\\WinSaasPOSStart.exe", MainModel.ServerPath + "\\WinSaasPOSStart.exe", true);
            }
            catch (Exception ex)
            {

            }
        }

        private void timerNow_Tick(object sender, EventArgs e)
        {
            try
            {

                lblSendCheckCode.Text = (Convert.ToInt16(lblSendCheckCode.Text) - 1).ToString();

                if (lblSendCheckCode.Text == "0")
                {
                    lblSendCheckCode.Text = "发送验证码";
                    timerNow.Enabled = false;
                }
            }
            catch { }
        }

        private int click = 0;
        private DateTime lastClickTime = DateTime.Now;
        //切换环境
        private void lblSN_Click(object sender, EventArgs e)
        {
            try
            {
                string isdebug = INIManager.GetIni("System", "IsDebug", MainModel.StartIniPath);
                if (isdebug == "1")
                {

                }
                LogManager.WriteLog(click.ToString());
                // 两次点击间隔小于100毫秒时，算连续点击
                if ((DateTime.Now - lastClickTime).TotalMilliseconds <= 2000)
                {
                    click++;
                    if (click >= 3)
                    {
                        click = 0;// 连续点击完毕时，清0
                        frmChangeUrl frmchangeurl = new frmChangeUrl();
                        frmchangeurl.ShowDialog();
                    }
                }
                else
                {
                    click = 1;// 不是连续点击时，清0
                }
                lastClickTime = DateTime.Now;
            }
            catch (Exception ex)
            {

            }
        }



        private bool CheckPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                MainModel.ShowLog("手机号不能为空", false);
                return false;
            }

            if (phone.Length != 11)
            {
                MainModel.ShowLog("请输入正确的手机号", false);
                return false;
            }
            if (phone.Substring(0, 1) != "1")
            {
                MainModel.ShowLog("请输入正确的手机号", false);
                return false;
            }

            return true;
        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {
            if (txtUser.Text.Length > 0)
            {
                lblUser.Visible = false;
            }
            else
            {
                lblUser.Visible = true;
            }
        }

        private void lblUser_Click(object sender, EventArgs e)
        {

            GlobalUtil.OpenOSK();

            Delay.Start(100);
            this.Activate();
            txtUser.Focus();
        }

        private void txtPwd_TextChanged(object sender, EventArgs e)
        {
            if (txtPwd.Text.Length > 0)
            {
                lblPwd.Visible = false;
            }
            else
            {
                lblPwd.Visible = true;
            }
        }

        private void lblPwd_Click(object sender, EventArgs e)
        {

            GlobalUtil.OpenOSK();

            Delay.Start(100);
            this.Activate();
            txtPwd.Focus();
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            if (txtPhone.Text.Length > 0)
            {
                lblPhone.Visible = false;
            }
            else
            {
                lblPhone.Visible = true;
            }
        }

        private void lblPhone_Click(object sender, EventArgs e)
        {
            GlobalUtil.OpenOSK();

            Delay.Start(100);
            this.Activate();
            txtPhone.Focus();
        }

        private void txtCheckCode_TextChanged(object sender, EventArgs e)
        {
            if (txtCheckCode.Text.Length > 0)
            {
                lblCheckCode.Visible = false;
            }
            else
            {
                lblCheckCode.Visible = true;
            }
        }

        private void lblCheckCode_Click(object sender, EventArgs e)
        {
            GlobalUtil.OpenOSK();

            Delay.Start(100);
            this.Activate();
            txtCheckCode.Focus();
        }

        private void txtPhoneCheckCode_TextChanged(object sender, EventArgs e)
        {
            if (txtPhoneCheckCode.Text.Length > 0)
            {
                lblPhoneCheckCode.Visible = false;
            }
            else
            {
                lblPhoneCheckCode.Visible = true;
            }
        }

        private void lblPhoneCheckCode_Click(object sender, EventArgs e)
        {

            GlobalUtil.OpenOSK();
            Delay.Start(100);
            this.Activate();
            txtPhoneCheckCode.Focus();
        }


        private void Control_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Control con = (Control)sender;

                Graphics g = e.Graphics;

                LinearGradientBrush myLinearGradientBrush = new LinearGradientBrush(e.ClipRectangle, Color.OrangeRed, Color.OrangeRed, LinearGradientMode.Vertical);
                //填充         

                ////四边圆角
                GraphicsPath gp = new GraphicsPath();
                gp.AddArc(e.ClipRectangle.X, e.ClipRectangle.Y, con.Height, con.Height, 180, 90);
                gp.AddArc(e.ClipRectangle.Width - 2 - con.Height, e.ClipRectangle.Y, con.Height, con.Height, 270, 90);
                gp.AddArc(e.ClipRectangle.Width - 2 - con.Height, e.ClipRectangle.Height - 1 - con.Height, con.Height, con.Height, 0, 90);
                gp.AddArc(e.ClipRectangle.Y, e.ClipRectangle.Height - 1 - con.Height, con.Height, con.Height, 90, 90);
                gp.CloseAllFigures();

                g.FillPath(myLinearGradientBrush, gp);

            }
            catch (Exception ex)
            {

            }
        }

        private void btnWindows_Click(object sender, EventArgs e)
        {
            MainModel.ShowTask();
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadingHelper.ShowLoadingScreen();

            Delay.Start(2000);

            LoadingHelper.CloseForm();
        }

        frmLoading loadingForm;
        private void button2_Click(object sender, EventArgs e)
        {
            //frmLoading frm = new frmLoading();

            //frm.ShowDialog();
            
            Thread thread = new Thread(ShowForm);
            thread.IsBackground = false;           
           // thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

            Delay.Start(3000);

            loadingForm.Close();
        }

        private  void ShowForm()
        {
            try
            {

                    loadingForm = new frmLoading();
                    loadingForm.TopMost = true;

                    loadingForm.ShowDialog();
                    

            }
            catch (Exception ex)
            {
                
            }

        }

        private void frmLogin_Activated(object sender, EventArgs e)
        {
            try
            {
                string imgname = "LoginLogo.bmp";
                if (File.Exists(MainModel.MediaPath + imgname))
                {
                    picTenantLogo.BackgroundImage = Image.FromFile(MainModel.MediaPath + imgname);

                }
                else
                {

                }
            }
            catch { }
        }

        #region 解决闪烁问题
        //protected override void WndProc(ref Message m)
        //{
        //    if (m.Msg == 0x0014) // 禁掉清除背景消息
        //        return;
        //    base.WndProc(ref m);
        //}

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        #endregion


        //控制仅允许录入数字
        private void TextNUMBER_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                TextBox txt = sender as TextBox;
                e.Handled = true;
                char ch = e.KeyChar;

                if (ch >= '0' && ch <= '9')
                    e.Handled = false;

                if (ch == (char)Keys.Back)
                    e.Handled = false;

            }
            catch { }
        }

        private void lbtnChangeOffLine_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MainModel.frmloginoffline != null)
            {
                try { MainModel.frmloginoffline.Dispose(); }
                catch { }
                
            }
            MainModel.frmloginoffline = new frmLoginOffLine();
            MainModel.frmloginoffline.Show();
            this.Hide();
            //if (MainModel.frmloginoffline == null)
            //{
            //    MainModel.frmloginoffline = new frmLoginOffLine();
            //    MainModel.frmloginoffline.Show();
            //    this.Hide();
            //}
            //else
            //{
            //    MainModel.frmloginoffline.Show();
            //    this.Hide();
            //}
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string names = "";
            int num = Application.OpenForms.Count;
            for (int i = 0; i < num; i++)
            {
                
                Form f = Application.OpenForms[i];
                if (f.Name != "frmLogin" && f.Name != "frmLoginOffLine")
                {
                    num = num - 1;
                    i = i - 1;
                    try
                    {
                        f.Dispose();
                      
                    }
                    catch { }
                }
            }
        }


        private void ClearText()
        {
            try
            {
                txtUser.Text = "";
                txtPwd.Text = "";
                txtPhone.Text = "";
                txtCheckCode.Text = "";
                txtPhoneCheckCode.Text = "";
                GlobalUtil.CloseOSK();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("清空登录页面信息异常"+ex.Message);
            }
        }


        private void GetDeviceSn()
        {
            try
            {
                //电脑可能会有多个mac地址，取第一次获取的mac地址为准  同时同步start.exe 获取的mac
                string currentmac = "";
                try
                {
                    if (File.Exists(MainModel.StartIniPath))
                    {
                        currentmac = INIManager.GetIni("System", "DeviceSN", MainModel.StartIniPath);
                    }
                    else
                    {
                        currentmac = INIManager.GetIni("System", "DeviceSN", MainModel.IniPath);
                    }
                }
                catch { }

                string devicesn = GlobalUtil.GetMacAddress(currentmac);



                //没有网络的时候获取不到MAC地址  ？？？  会被替换
                if (devicesn.Length > 10)
                {
                    INIManager.SetIni("System", "DeviceSN", devicesn, MainModel.IniPath);

                }
                MainModel.DeviceSN = devicesn;
                //lblSN.Text = "设备序列号：" + devicesn;
                txtSN.Text = devicesn;
            }
            catch (Exception ex)
            {
            }
        }

        #region 定时器 

        public delegate void deleteTimeTen();
        private void timerTen_Tick(object sender, EventArgs e)
        {
            try
            {
                timerTen.Enabled = false;


                deleteTimeTen operation = new deleteTimeTen(TimerTenAction);

                operation.BeginInvoke(new System.AsyncCallback(GetCallbackHandler), "Async parameter");                

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("定时任务异常" + ex.Message);
                timerTen.Enabled = true;
            }
            
        }

        public void TimerTenAction()
        {
            ServerDataUtil.LoadIncrementProduct();
            ServerDataUtil.LoadTVSkus();
            LogManager.WriteLog("定时10分钟刷新数据完成");
        }

          public void GetCallbackHandler(IAsyncResult iar)
        {
            this.BeginInvoke(new EventHandler(delegate
            {
                timerTen.Enabled = true;

            })); 
        }



        #endregion





          private void txt_OskClick(object sender, EventArgs e)
          {
              try
              {
                  TextBox txt = (TextBox)sender;
                  GlobalUtil.OpenOSK();
                  Delay.Start(100);
                  this.Activate();
                  txt.Focus();
              }
              catch (Exception ex)
              {
                  LogManager.WriteLog("焦点打开键盘异常" + ex.Message);
              }
          }


    }


}
