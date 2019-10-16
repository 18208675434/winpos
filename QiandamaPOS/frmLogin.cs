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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QiandamaPOS
{
    public partial class frmLogin : Form
    {
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

           //判断日志文件夹是否存在，不存在则新建
           if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Log"))
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Log");

            try
            {
                LoadingHelper.ShowLoadingScreen("用户/门店信息验证中，请稍候");
            
                if (!LoadUser() || !LoadShopInfo())
                {
                    LoadingHelper.CloseForm();

                    txtUser.Clear();
                    txtPwd.Clear();
                    isReLogin = true;
                    int screenwdith = Screen.PrimaryScreen.Bounds.Width;
                    this.Location = new System.Drawing.Point((screenwdith - this.Width) / 2, 10);

                    lblUser_Click(null, null);
                    lblSN.Text = "设备序列号：" + GlobalUtil.GetPCSN();

                    txtUser.SetWatermark("请输入11位手机号");
                    txtPwd.SetWatermark("请输入密码");

                    txtPhone.SetWatermark("请输入11位手机号");
                    txtCheckCode.SetWatermark("请输入右侧图形验证码");
                    txtPhoneCheckCode.SetWatermark("请输入短信验证码");
                }
                else
                {
           
                    frmMain frmmain = new frmMain();
                    //frmmain.frmMain_SizeChanged(null,null);
                    //frmmain.WindowState = FormWindowState.Maximized;

                    asf.AutoScaleControlTest(frmmain, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height,true);
                    this.Hide();
                    CloseOSK();

                    //asf.AutoScaleControl(frmmain);
                    frmmain.Show();
                }              

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("登录异常："+ex.Message);
            }
            finally
            {
               // LoadingHelper.CloseForm();
            }
            //客屏初始化
            MainModel.frmmainmedia = new frmMainMedia();
            if (Screen.AllScreens.Count() != 1)
            {
                // windowstate设置max 不能再页面设置 否则会显示到第一个屏幕
               // MainModel.frmmainmedia.Size = new System.Drawing.Size(Screen.AllScreens[1].Bounds.Width, Screen.AllScreens[1].Bounds.Height);
                MainModel.frmmainmedia.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width, 0);

                MainModel.frmmainmedia.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                MainModel.frmmainmedia.Show();
            }
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

            if (username == "" || password == "")
            {
                lblMsg.Text = "*用户名密码不能为空";
                return;
            }
            //如果是当前登录用户 不重新获取token
            if(username==INIManager.GetIni("System", "UserName", MainModel.IniPath) && password==INIManager.GetIni("System", "PassWord", MainModel.IniPath) && !isReLogin)
            {
                if (!LoadUser() || !LoadShopInfo())
                {
                    txtUser.Clear();
                    txtPwd.Clear();
                    isReLogin = true;
                    return;
                }

                INIManager.SetIni("System", "UserName", username, MainModel.IniPath);
                INIManager.SetIni("System", "PassWord", password, MainModel.IniPath);
                frmMain frmmain = new frmMain();
                frmmain.WindowState = FormWindowState.Maximized;
                this.Hide();
                CloseOSK();
                frmmain.Show();
            }
            else//重新登录账户
            {
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
                    //MainModel.DeviceSN = Token;
                   

                    INIManager.SetIni("System", "UserName", username, MainModel.IniPath);
                    INIManager.SetIni("System", "PassWord", password, MainModel.IniPath);
                    frmMain frmmain = new frmMain();
                    frmmain.WindowState = FormWindowState.Maximized;
                    this.Hide();
                    CloseOSK();
                    frmmain.Show();
                }
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
            this.picCheckCode.Image = Bitmap.FromStream(validCode.CreateCheckCodeImage());

        }

        private void lblSendCheckCode_Click(object sender, EventArgs e)
        {
            if (validCode.CheckCode != txtCheckCode.Text)
            {
                MessageBox.Show("图形验证码不正确，请重新输入！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (txtPhone.Text.Length != 11)
                {
                    MessageBox.Show("请输入正确的手机号！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    //TODO 调用接口发送手机验证                    

                }
            }
        }

        private void btnLoginByPhone_Click(object sender, EventArgs e)
        {
            //TODO  调用接口验证手机验证码
            frmMain frmmain = new frmMain();
            frmmain.WindowState = FormWindowState.Maximized;
            this.Hide();
            frmmain.Show();
        }
        #endregion

        private void picCheckCode_Click(object sender, EventArgs e)
        {
            //点击图片 刷新验证码
            this.picCheckCode.Image = Bitmap.FromStream(validCode.CreateCheckCodeImage());

        }

        private void OpenOSK()
        {
            try
            {
                System.Diagnostics.Process.Start(@"C:\Windows\System32\osk.exe");
            }catch(Exception ex){}
        }
        private void CloseOSK()
        {
            //程序关闭时，将labelprint.exe一起关闭
            try
            {
                Process[] pro = Process.GetProcesses();
                for (int i = 0; i < pro.Length - 1; i++)
                {
                    if (pro[i].ProcessName=="osk")
                    {
                        pro[i].Kill();
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("小键盘关闭异常：" + ex.Message);
            }
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
                LogManager.WriteLog("获取用户信息异常：" + ex.Message);
                lblMsg.Text = "获取用户信息异常，请重新登录：";
                return false;
            }
        }

        private void frmLogin_SizeChanged(object sender, EventArgs e)
        {
           asf.ControlAutoSize(this);
        }
    }


}
