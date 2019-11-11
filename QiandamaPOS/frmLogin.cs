using QiandamaPOS.Common;
using QiandamaPOS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading;
using System.Windows.Forms;

namespace QiandamaPOS
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
           
        }

        private void frmLogin_Shown(object sender, EventArgs e)
        {


          

        //public void QuerySkushopAll(string shopid, int page, int size, ref string erromessage)

           //判断日志文件夹是否存在，不存在则新建
           if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Log"))
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Log");


           Thread threadItemExedate = new Thread(ThreadUpStart);
           threadItemExedate.IsBackground = true;
           threadItemExedate.Start();

           Thread threadIniFormExedate = new Thread(IniForm);
           threadIniFormExedate.IsBackground = true;
           threadIniFormExedate.Start(); 
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            lblUser_Click(null, null);
        }

        private void IniForm()
        {

            this.Invoke(new InvokeHandler(delegate()
        {
            string devicesn = GlobalUtil.GetHardDiskID();

            //MainModel.DeviceSN = MainModel.GetHardDiskID();

            INIManager.SetIni("System", "DeviceSN", devicesn, MainModel.IniPath);
            MainModel.DeviceSN = devicesn;
            lblSN.Text = "设备序列号：" + devicesn;


            try
            {
              //  LoadingHelper.ShowLoadingScreen("用户/门店信息验证中，请稍候");
                lblMsg.Text = "用户/门店信息验证中，请稍候...";
                Application.DoEvents();
                if (!LoadUser() || !LoadShopInfo())
                {
                   // LoadingHelper.CloseForm();

                 
                   // lblMsg.Text = "";

                    txtUser.Clear();
                    txtPwd.Clear();
                    isReLogin = true;
                    int screenwdith = Screen.PrimaryScreen.Bounds.Width;
                    //this.Location = new System.Drawing.Point((screenwdith - this.Width) / 2, 10);

                    lblUser_Click(null, null);
                    //lblSN.Text = "设备序列号：" + GlobalUtil.GetPCSN();

                    //txtUser.SetWatermark("请输入11位手机号");
                    //txtPwd.SetWatermark("请输入密码");

                    //txtPhone.Font = new System.Drawing.Font("微软雅黑", 10F);
                    //txtPhone.SetWatermark("请输入11位手机号");
                    //txtCheckCode.SetWatermark("请输入右侧图形验证码");
                    //txtPhoneCheckCode.SetWatermark("请输入短信验证码");
                }
                else
                {
               

                    frmMain frmmain = new frmMain(this);

                    asf.AutoScaleControlTest(frmmain, Screen.PrimaryScreen.Bounds.Width, SystemInformation.WorkingArea.Height, true);
                    this.Hide();
                    CloseOSK();

                    //asf.AutoScaleControl(frmmain);
                    frmmain.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("登录异常：" + ex.Message);
            }
            finally
            {
                // LoadingHelper.CloseForm();
            }
        }));
        }

        #region 账号密码登录

        private void lblUser_Click(object sender, EventArgs e)
        {
            lblUser.ForeColor = Color.Green;
            lblPhone.ForeColor = Color.Black;

            pnlUser.Visible = true;
            pnlPhone.Visible = false;
        }


        bool isReLogin = false;
        private void btnLoginByUser_Click(object sender, EventArgs e)
        {
          
            //TODO  调用接口 验证用户
            string username = txtUser.Text;
            string password = txtPwd.Text;
            lblMsg.Text = "";

            if (username == "" || password == "")
            {
                lblMsg.Text = "*用户名密码不能为空";
                return;
            }
           
                string ErrorMsg = "";
                string Token = httputil.Signin(username,password,ref ErrorMsg);
                if (ErrorMsg != "" || Token=="")
                {
                    lblMsg.Text = ErrorMsg;
                }
                else
                {
                    INIManager.SetIni("System", "POS-Authorization", Token, MainModel.IniPath);
                    MainModel.Authorization = Token;
                    if (!LoadUser() || !LoadShopInfo())
                    {
                        return;
                    }
                   

                    INIManager.SetIni("System", "UserName", username, MainModel.IniPath);
                    INIManager.SetIni("System", "PassWord", password, MainModel.IniPath);

                    frmMain frmmain = new frmMain(this);

                    asf.AutoScaleControlTest(frmmain, Screen.PrimaryScreen.Bounds.Width, SystemInformation.WorkingArea.Height, true);
                    this.Hide();
                    CloseOSK();
                    frmmain.ShowDialog();
                }
            
            
        }
        
        #endregion

        #region 手机验证码登录
        private void lblPhone_Click(object sender, EventArgs e)
        {
            lblUser.ForeColor = Color.Black;
            lblPhone.ForeColor = Color.Green;

            pnlUser.Visible = false;
            pnlPhone.Visible = true;

            //手机验证页面 先刷新一次图形验证码
            //this.picCheckCode.Image = Bitmap.FromStream(validCode.CreateCheckCodeImage());

            GetAuthcodeImage();
           
        }

        private void lblSendCheckCode_Click(object sender, EventArgs e)
        {
           if (txtPhone.Text.Length != 11)
                {
                    //MessageBox.Show("请输入正确的手机号！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    MainModel.ShowLog("手机号不正确",false);
                    return;
                }
                else
                {
                 
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
                            lblMsg.Text = ErrorMsg;
                        }                        
                    }
                }
            
        }

        private void btnLoginByPhone_Click(object sender, EventArgs e)
        {
            SignPara signpara =new SignPara();
            signpara.imgcode=txtCheckCode.Text;
            signpara.imgcodekey=CurrentImgCodeKey;
            signpara.phone=txtPhone.Text;
            signpara.smscode=txtPhoneCheckCode.Text;
            lblMsg.Text = "";
              string ErrorMsg = "";
                string Token = httputil.SigninWithSmscode(signpara,ref ErrorMsg);
                if (ErrorMsg != "" || Token == "")
                {
                    lblMsg.Text = ErrorMsg;
                }
                else
                {
                    INIManager.SetIni("System", "POS-Authorization", Token, MainModel.IniPath);
                    MainModel.Authorization = Token;
                    if (!LoadUser() || !LoadShopInfo())
                    {
                        return;
                    }


                    frmMain frmmain = new frmMain(this);

                    asf.AutoScaleControlTest(frmmain, Screen.PrimaryScreen.Bounds.Width, SystemInformation.WorkingArea.Height, true);
                    this.Hide();
                    CloseOSK();

                    frmmain.ShowDialog();
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
            CloseOSK();
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseOSK();
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
                    lblMsg.Text = "获取用户信息异常，请重新登录";
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
                LogManager.WriteLog("获取用户信息异常："+ex.Message);
                lblMsg.Text = "获取用户信息异常，请重新登录";
                return false;
            }
        }
        private bool LoadShopInfo()
        {

            try
            {
                string ErrorMsg = "";
                DeviceShopInfo  shopinfo = httputil.GetShopInfo(MainModel.DeviceSN, ref ErrorMsg);

                if (ErrorMsg != "" || shopinfo == null)
                {
                    lblMsg.Text = "获取门店信息异常，请重新登录";
                    LogManager.WriteLog("ERROR", ErrorMsg);
                    return false;
                }
                else
                {
                    MainModel.CurrentShopInfo = shopinfo;
                    MainModel.ShopName = shopinfo.shopname;
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("获取门店信息异常：" + ex.Message);
                lblMsg.Text = "获取门店信息异常，请重新登录：";
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

            System.Environment.Exit(0);
        }


        private void ThreadUpStart()
        {
            try 
            {
                File.Copy(MainModel.TempFilePath + "\\QdamaPOSStart.exe", MainModel.ServerPath + "\\QdamaPOSStart.exe", true);
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
            if ((DateTime.Now - lastClickTime).TotalMilliseconds <= 5000)
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

     

    }


}
