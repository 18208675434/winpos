using Maticsoft.BLL;
using Maticsoft.Model;
using Newtonsoft.Json;
using QDAMAPOS.Common;
using QDAMAPOS.Model;
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

namespace QDAMAPOS
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
            // MainModel.HideTaskThread();
            MainModel.ShowTask();
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Log"))
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Log");


            Thread threadItemExedate = new Thread(ThreadUpStart);
            threadItemExedate.IsBackground = true;
            threadItemExedate.Start();

            Thread threadIniFormExedate = new Thread(IniForm);
            threadIniFormExedate.IsBackground = true;
            threadIniFormExedate.Start();
            

            //客屏初始化
            MainModel.frmmainmedia = new frmMainMedia();
            if (Screen.AllScreens.Count() > 1)
            {
                asf.AutoScaleControlTest(MainModel.frmmainmedia, 1020, 760, Screen.AllScreens[1].Bounds.Width, Screen.AllScreens[1].Bounds.Height + 20, true);
                MainModel.frmmainmedia.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width, -20);

                MainModel.frmmainmedia.Show();
                MainModel.frmmainmedia.IniForm(null);

            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            Application.DoEvents();
            MainModel.IsOffLine = false;
            INIManager.SetIni("System", "IsOffLine", "0", MainModel.IniPath);
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

                    pnlbtnLoginByUser.Refresh();
                    lblLobinByUser_Click(null, null);

                }
                else
                {
                    this.Enabled = true;
                    LoadingHelper.CloseForm();

                    frmMain frmmain = new frmMain(this);

                    asf.AutoScaleControlTest(frmmain, 1178, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                    //Delay.Start(200);
                    Application.DoEvents();
                    //this.Hide();
                    frmmain.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("登录异常：" + ex.Message + ex.StackTrace);
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

                    frmMain frmmain = new frmMain(this);

                    asf.AutoScaleControlTest(frmmain, 1178, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                    LoadingHelper.CloseForm();
                    //Delay.Start(200);
                    Application.DoEvents();
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


                    frmMain frmmain = new frmMain(this);

                    asf.AutoScaleControlTest(frmmain, 1178, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                    LoadingHelper.CloseForm();
                    //Delay.Start(200);
                    Application.DoEvents();
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

        private void OpenOSK()
        {
            //try
            //{
            //    System.Diagnostics.Process.Start(@"C:\Windows\System32\osk.exe");
            //}catch(Exception ex){}
        }
        private void CloseOSK()
        {
            
            //try
            //{
            //    Process[] pro = Process.GetProcesses();
            //    for (int i = 0; i < pro.Length - 1; i++)
            //    {
            //        if (pro[i].ProcessName=="osk")
            //        {
            //            pro[i].Kill();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    LogManager.WriteLog("小键盘关闭异常：" + ex.Message);
            //}
        }

        private void txt_MouseCaptureChanged(object sender, EventArgs e)
        {
            //TextBox txt = (TextBox)sender;
            //if (txt.Focused)
            //{
            //    OpenOSK();
            //}

            OpenOSK();

        }


        private void frmLogin_Click(object sender, EventArgs e)
        {
            //CloseOSK();
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainModel.ShowTask();
            MainModel.frmmainmedia.Close();
            MainModel.frmmainmedia = null;
            ////CloseOSK();
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
                MainModel.frmmainmedia.Close();
                MainModel.frmmainmedia = null;
            }
            catch { }
            MainModel.ShowTask();
            System.Environment.Exit(0);
        }


        private void ThreadUpStart()
        {
            try 
            {
                File.Copy(MainModel.TempFilePath + "\\QDAMAPOSStart.exe", MainModel.ServerPath + "\\QDAMAPOSStart.exe", true);
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
            txtPhoneCheckCode.Focus();
        }


        private void Control_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Control con = (Control)sender;

                // Draw(e.ClipRectangle, e.Graphics, 100, false, Color.FromArgb(113, 113, 113), Color.FromArgb(0, 0, 0));
                //base.OnPaint(e);
                Graphics g = e.Graphics;
                // g.DrawString("", new Font("微软雅黑", 9, FontStyle.Regular), new SolidBrush(Color.White), new PointF(10, 10));

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


        private void lbtnChangeOffLine_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MainModel.frmloginoffline == null)
            {
                MainModel.frmloginoffline = new frmLoginOffLine();
                MainModel.frmloginoffline.Show();
                this.Hide();
            }
            else
            {
                MainModel.frmloginoffline.Show();
                this.Hide();
            }
        }

        public void ChangeOffLine()
        {
            if (MainModel.frmloginoffline == null)
            {
                MainModel.frmloginoffline = new frmLoginOffLine();
                MainModel.frmloginoffline.Show();
                this.Hide();
            }
            else
            {
                MainModel.frmloginoffline.Show();
                this.Hide();
            }
        }

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

        #region 解决闪烁问题
        //protected override void WndProc(ref Message m)
        //{
        //    if (m.Msg == 0x0014) // 禁掉清除背景消息
        //        return;
        //    base.WndProc(ref m);
        //}

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;
        //        return cp;
        //    }
        //}
        #endregion

    }


}
