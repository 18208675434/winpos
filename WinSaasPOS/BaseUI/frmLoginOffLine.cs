using Maticsoft.BLL;
using Maticsoft.Model;
using Newtonsoft.Json;
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

namespace WinSaasPOS
{
    //"∨ ^"
    public partial class frmLoginOffLine : Form
    {

        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();


        AutoSizeFormUtil asf = new AutoSizeFormUtil();


        private SYSTEM_USER_BEANMODEL CurrentUser = new SYSTEM_USER_BEANMODEL();
        HttpUtil httputil = new HttpUtil();
        public frmLoginOffLine()
        {
            InitializeComponent();
            MainModel.IsOffLine = true;
            INIManager.SetIni("System", "IsOffLine", "1", MainModel.IniPath);

        }

        private void frmLogin_Shown(object sender, EventArgs e)
        {

            Application.DoEvents();
            MainModel.ShowTask();
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Log"))
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Log");


            Thread threadItemExedate = new Thread(ThreadUpStart);
            threadItemExedate.IsBackground = true;
            threadItemExedate.Start();

            
            Thread threadIniFormExedate = new Thread(IniForm);
            threadIniFormExedate.IsBackground = true;
            threadIniFormExedate.Start();

            ////客屏初始化
            //MainModel.frmmainmedia = new frmMainMedia();
            //if (Screen.AllScreens.Count() > 1)
            //{
            //    asf.AutoScaleControlTest(MainModel.frmmainmedia, 1020, 760, Screen.AllScreens[1].Bounds.Width, Screen.AllScreens[1].Bounds.Height + 20, true);
            //    MainModel.frmmainmedia.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width, -20);

            //    MainModel.frmmainmedia.Show();
            //    MainModel.frmmainmedia.IniForm(null);

            //}
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            MainModel.IsOffLine = true;
            INIManager.SetIni("System", "IsOffLine", "1", MainModel.IniPath);

            string imgname = "LoginLogo.bmp";
            if (File.Exists(MainModel.MediaPath + imgname))
            {
                picTenantLogo.BackgroundImage = Image.FromFile(MainModel.MediaPath + imgname);

            }
            else
            {

            }
         

            Application.DoEvents();
        }

        private SYSTEM_USER_BEANBLL userbll = new SYSTEM_USER_BEANBLL();

        private void IniForm()
        {

            this.Invoke(new InvokeHandler(delegate()
        {


            ////电脑可能会有多个mac地址，取第一次获取的mac地址为准  同时同步start.exe 获取的mac
            //string currentmac = "";
            //try
            //{
            //    if (File.Exists(MainModel.StartIniPath))
            //    {
            //        currentmac = INIManager.GetIni("System", "DeviceSN", MainModel.StartIniPath);
            //    }
            //    else
            //    {
            //        currentmac = INIManager.GetIni("System", "DeviceSN", MainModel.IniPath);
            //    }
            //}
            //catch { }

            string devicesn = INIManager.GetIni("System", "DeviceSN", MainModel.IniPath);


            //没有网络的时候获取不到MAC地址  ？？？  会被替换
            if (devicesn.Length > 10)
            {
                INIManager.SetIni("System", "DeviceSN", devicesn, MainModel.IniPath);
            }
            MainModel.DeviceSN = devicesn;
            //lblSN.Text = "设备序列号：" + devicesn;
            txtSN.Text = devicesn;
            LoadShopInfo();
            LoadUserLst();



        }));
        }
        private void LoadUserLst()
        {
            try
            {
                dgvUser.Rows.Clear();
                List<SYSTEM_USER_BEANMODEL> lstuser = userbll.GetModelList(" CREATE_URL_IP = '" + MainModel.URL + "'");
                if (lstuser != null && lstuser.Count > 0)
                {
                    //pnlUserList.Visible = true;
                    int usercount = lstuser.Count;

                    if (usercount <= 5)
                    {
                        dgvUser.Height = 60 * usercount;
                    }
                    else
                    {
                        dgvUser.Height = 300;
                    }

                    foreach (SYSTEM_USER_BEANMODEL user in lstuser)
                    {
                        dgvUser.Rows.Add(GetUserImg(user));
                    }
                    dgvUser.ClearSelection();
                    btnAddUser.Top = dgvUser.Height;


                    pnlUserList.Height = dgvUser.Height + dgvUser.Top;

                    pnlRight.Height = dgvUser.Height;
                    pnlRight.Top = dgvUser.Top;
                    pnlRight.Left = dgvUser.Left + dgvUser.Width - pnlRight.Width;
                }
                else
                {
                    pnlUserList.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载员工信息异常"+ex.Message,false);
            }
        }


        #region 手机验证码登录


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

            pnlUserList.Visible = false;
            //CloseOSK();
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {

            try
            {
                this.Dispose();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("清理登录页面资源异常" + ex.Message);
            }
            //MainModel.ShowTask();
            //MainModel.frmmainmedia.Close();
            //MainModel.frmmainmedia = null;
            ////CloseOSK();
        }


        

        private JSON_BEANBLL jsonbll = new JSON_BEANBLL();
        private bool LoadShopInfo()
        {

            try
            {
                
                    JSON_BEANMODEL jsonmodel = jsonbll.GetModel("SHOPINFO");
                    if (jsonmodel != null && jsonmodel.JSON != null)
                    {
                        MainModel.CurrentShopInfo = JsonConvert.DeserializeObject<DeviceShopInfo>(jsonmodel.JSON);

                        MainModel.ShopName = MainModel.CurrentShopInfo.shopname;
                        lblShopName.Text = MainModel.ShopName;
                        return true;
                    }
                    else
                    {
                        return false;
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




        private void picExit_Click(object sender, EventArgs e)
        {
            frmDeleteGood frmdelete = new frmDeleteGood("是否确认退出系统？", "", "");

            if (frmdelete.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            try
            {
                //MainModel.frmmainmedia.Close();
                //MainModel.frmmainmedia = null;
            }
            catch { }
            MainModel.ShowTask();
            System.Environment.Exit(0);
        }


        private void ThreadUpStart()
        {
            try
            {
                File.Copy(MainModel.TempFilePath + "\\WinSaasPOSStart.exe", MainModel.ServerPath + "\\WinSaasPOSStart.exe", true);
            }
            catch (Exception ex)
            {

            }
        }



        private int click = 0;
        private DateTime lastClickTime = DateTime.Now;
        //切换环境
        private void lblSN_Click(object sender, EventArgs e)
        {
            //LogManager.WriteLog(click.ToString());
            //// 两次点击间隔小于100毫秒时，算连续点击
            //if ((DateTime.Now - lastClickTime).TotalMilliseconds <= 2000)
            //{
            //    click++;
            //    if (click >= 3)
            //    {
            //        click = 0;// 连续点击完毕时，清0
            //        frmChangeUrl frmchangeurl = new frmChangeUrl();
            //        frmchangeurl.ShowDialog();
            //    }
            //}
            //else
            //{

            //    click = 1;// 不是连续点击时，清0
            //}
            //lastClickTime = DateTime.Now;
        }


        private void btnWindows_Click(object sender, EventArgs e)
        {
            pnlUserList.Visible = false;
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

        private void ShowForm()
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

        private void button1_Click_1(object sender, EventArgs e)
        {

           
            //userModel usermodel = new userModel();
            //usermodel.nickname = "测试";

            //MainModel.CurrentUser = usermodel;
            //MainModel.IsOffLine = true;
            //if (!LoadShopInfo())
            //{
            //    MainModel.ShowLog("获取门店信息异常", true);
            //    return;
            //}

            //frmMainOffLine frmmainoffline = new frmMainOffLine(this);
            //asf.AutoScaleControlTest(frmmainoffline, 1178, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
            //frmmainoffline.ShowDialog();
        }

        private void lblOffLinePhone_Click(object sender, EventArgs e)
        {
            txtOffLinePhone.Focus();
        }

        //防止最小化 重新展开异常
        private void picSelect_Click(object sender, EventArgs e)
        {

            if(pnlUserList.Visible){
                 pnlUserList.Visible=false;
            }else{
                pnlUserList.Visible=true;
                LoadUserLst();
            }
           // pnlUserList.Visible = !pnlUserList.Visible;

           // if(pnluser)
        }

        private void pnlUserList_VisibleChanged(object sender, EventArgs e)
        {
            if (pnlUserList.Visible)
            {
                picSelect.BackgroundImage = picUP.Image;
            }
            else
            {
                picSelect.BackgroundImage = picDown.Image;
            }
        }


        #endregion

        private void lbtnChangeOnLine_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string LastLoginaccount = INIManager.GetIni("OffLine", "LastLoginaccount", MainModel.IniPath);
                string LastNickName = INIManager.GetIni("OffLine", "LastNickName", MainModel.IniPath);

                if (!string.IsNullOrEmpty(LastNickName))
                {


                    List<SYSTEM_USER_BEANMODEL> lstuser = userbll.GetModelList(" LOGINACCOUNT = '" + LastLoginaccount +"' and  CREATE_URL_IP = '"+MainModel.URL+"'");
                    if (lstuser != null && lstuser.Count > 0)
                    {
                        userModel usermodel = new userModel();
                        usermodel.nickname = lstuser[0].NICKNAME;
                        usermodel.loginaccount = lstuser[0].LOGINACCOUNT;

                        MainModel.CurrentUser = usermodel;
                        MainModel.IsOffLine = true;

                        LoadShopInfo();
                        LoadPicScreen(true);
                        frmConfirmChange frmConfirm = new frmConfirmChange();

                       // asf.AutoScaleControlTest(frmConfirm, 490, 230, Screen.AllScreens[0].Bounds.Width * 30 / 100, Screen.AllScreens[0].Bounds.Height * 25 / 100, true);
                        frmConfirm.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmConfirm.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmConfirm.Height) / 2);

                        frmConfirm.ShowDialog();
                        LoadPicScreen(false);

                        if (frmConfirm.DialogResult != DialogResult.OK)
                        {
                            return;
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("检查是否需要强制离线交班异常"+ex.Message);
            }


            if (MainModel.frmlogin != null)
            {
                try
                {
                    MainModel.frmlogin.Dispose();
                }
                catch { }
            }
            MainModel.frmlogin = new frmLogin();
            MainModel.frmlogin.Show();
            this.Hide();
            //if (MainModel.frmlogin == null)
            //{
            //    MainModel.frmlogin = new frmLogin();
            //    MainModel.frmlogin.Show();
            //    this.Hide();
            //}
            //else
            //{
            //    MainModel.frmlogin.Show();
            //    this.Hide();
            //}
        }

        public void ChangeOnLine()
        {
            if (MainModel.frmlogin == null)
            {
                MainModel.frmlogin = new frmLogin();
                MainModel.frmlogin.Show();
                this.Hide();
            }
            else
            {
                MainModel.frmlogin.Show();
                this.Hide();
            }
        }

        private void txtOffLinePhone_TextChanged(object sender, EventArgs e)
        {

            if (txtOffLinePhone.Text.Length > 0)
            {
                lblOffLinePhone.Visible = false;
            }
            else
            {
                lblOffLinePhone.Visible = true;
            }
        }

        private void panel13_Paint(object sender, PaintEventArgs e)
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

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            try
            {

                LoadPicScreen(true);
                frmAddUser frmadduser = new frmAddUser();

                asf.AutoScaleControlTest(frmadduser, 380, 240, Screen.AllScreens[0].Bounds.Width * 2 / 5, Screen.AllScreens[0].Bounds.Height * 2 / 5, true);
                frmadduser.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmadduser.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmadduser.Height) / 2);

                frmadduser.ShowDialog();
                LoadPicScreen(false);

                if (frmadduser.DialogResult == DialogResult.OK)
                {
                    MainModel.ShowLog("添加成功",false);
                    
                    dgvUser.Rows.Add(GetUserImg(frmadduser.CurrentUser));
                    CurrentUser = frmadduser.CurrentUser;

                    txtOffLinePhone.Text = "  " + CurrentUser.NICKNAME + "  " + CurrentUser.LOGINACCOUNT;

                }

               

                LoadUserLst();
                pnlUserList.Visible = false;
                dgvUser.ClearSelection();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("添加员工页面异常"+ex.Message);
            }
            finally
            {
                LoadPicScreen(false);
            }
        }

        private void dgvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                Bitmap bmp = (Bitmap)dgvUser.Rows[e.RowIndex].Cells[0].Value;

                CurrentUser =(SYSTEM_USER_BEANMODEL) bmp.Tag;

                //if (CurrentUser.NICKNAME != null && CurrentUser.NICKNAME.Length > 0)
                //{
                //    txtOffLinePhone.Text = "  " + CurrentUser.NICKNAME.Substring(0, 1) + "某某" + "  " + CurrentUser.LOGINACCOUNT;
                //}
                //else
                //{
                //    txtOffLinePhone.Text = "  " + CurrentUser.NICKNAME + "  " + CurrentUser.LOGINACCOUNT;
                //}
                txtOffLinePhone.Text = "  " + CurrentUser.NICKNAME + "  " + CurrentUser.LOGINACCOUNT;

                //txtOffLinePhone.Text = CurrentUser.LOGINACCOUNT;

                pnlUserList.Visible = false;
                dgvUser.ClearSelection();

            }
            catch (Exception ex)
            {
                MainModel.ShowLog("选择用户异常"+ex.Message,true);
            }
        }



        private void LoadPicScreen(bool isShown)
        {
            try
            {
                this.Invoke(new InvokeHandler(delegate()
                {
                    if (isShown)
                    {
                        if (!picScreen.Visible)
                        {
                            picScreen.BackgroundImage = MainModel.GetWinformImage(this);
                            // picScreen.BackgroundImage = picCheck.BackgroundImage;
                            picScreen.Size = new System.Drawing.Size(this.Width, this.Height);
                            picScreen.Visible = true;
                        }
                    }
                    else
                    {
                        picScreen.Visible = false;
                    }
                   
                    Application.DoEvents();
                }));
            }
            catch (Exception ex)
            {
                picScreen.Visible = false;
                LogManager.WriteLog("修改主窗体背景图异常：" + ex.Message);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string phone = txtOffLinePhone.Text;

                if (string.IsNullOrEmpty(phone) || CurrentUser == null)
                {
                    MainModel.ShowLog("请选择员工", false);
                    return;
                }

                //if (phone.Length != 11 || phone.Substring(0, 1) != "1")
                //{
                //    MainModel.ShowLog("用户不存在", false);
                //    return;
                //}

                //List<SYSTEM_USER_BEANMODEL> lstuser = userbll.GetModelList(" LOGINACCOUNT = "+phone);
                //if (lstuser == null || lstuser.Count <= 0)
                //{
                //    MainModel.ShowLog("用户不存在",false);
                //    return;
                //}

                userModel usermodel = new userModel();
                usermodel.nickname = CurrentUser.NICKNAME;
                usermodel.loginaccount = CurrentUser.LOGINACCOUNT;

                if (!CheckEnableLogin(CurrentUser.LOGINACCOUNT))
                {
                    return;
                }

                MainModel.CurrentUser = usermodel;
                MainModel.IsOffLine = true;
                if (!LoadShopInfo())
                {
                    MainModel.ShowLog("获取门店信息异常", true);
                    return;
                }

                txtOffLinePhone.Text = "";
                INIManager.SetIni("OffLine", "LastLoginaccount", usermodel.loginaccount, MainModel.IniPath);
                INIManager.SetIni("OffLine", "LastNickName",usermodel.nickname, MainModel.IniPath);
                frmMainOffLine frmmainoffline = new frmMainOffLine(this);
                asf.AutoScaleControlTest(frmmainoffline, 1178, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                frmmainoffline.ShowDialog();
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("离线登录异常"+ex.Message,true);
            }
        }
        //检查是否有未交班状态
        private bool CheckEnableLogin(string phone)
        {
            try
            {
                string LastLoginaccount = INIManager.GetIni("OffLine", "LastLoginaccount", MainModel.IniPath);
                string LastNickName = INIManager.GetIni("OffLine", "LastNickName", MainModel.IniPath);

                if (string.IsNullOrEmpty(LastNickName) || LastLoginaccount == phone)
                {
                    return true;
                }

                List<SYSTEM_USER_BEANMODEL> lstuser = userbll.GetModelList(" LOGINACCOUNT = '" + LastLoginaccount + "' and  CREATE_URL_IP = '" + MainModel.URL + "'");
                if (lstuser == null || lstuser.Count <= 0)
                {
                    return true;
                }
                else
                {
                    MainModel.ShowLog("当前设备未交班，使用人:"+lstuser[0].NICKNAME+"，如需使用，请先交班",false);
                    return false;
                }


            }
            catch (Exception ex)
            {
                LogManager.WriteLog("校验用户交班状态异常"+ex.Message);
                return true;
            }
        }

        private Bitmap GetUserImg(SYSTEM_USER_BEANMODEL user)
        {
            try
            {
                //if (user.NICKNAME != null && user.NICKNAME.Length > 0)
                //{
                //    lblUserInfo.Text = "  " + user.NICKNAME.Substring(0,1)+"某某" + "  " + user.LOGINACCOUNT;
                //}
                //else
                //{
                //    lblUserInfo.Text = "  " + user.NICKNAME + "  " + user.LOGINACCOUNT;
                //}
                lblUserInfo.Text = "  " + user.NICKNAME + "  " + user.LOGINACCOUNT;



                //获取单元格图片内容
                Bitmap b = new Bitmap(pnlUser.Width, pnlUser.Height);
                b.Tag = user;
                pnlUser.DrawToBitmap(b, new Rectangle(0, 0, pnlUser.Width, pnlUser.Height));

                return b;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        private void frmLoginOffLine_Activated(object sender, EventArgs e)
        {
            //LoadUserLst();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string names = "";
            int num = Application.OpenForms.Count;
            for (int i = num-1; i >-1; i--)
            {

              
                if (Application.OpenForms[i].Name != "frmLogin" && Application.OpenForms[i].Name != "frmLoginOffLine")
                {
                  
                    try
                    {
                        Application.OpenForms[i].Close();
                        Application.OpenForms[i].Dispose();

                    }
                    catch(Exception ex)  {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

     

      
    }
}