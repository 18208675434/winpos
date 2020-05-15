using Maticsoft.BLL;
using Maticsoft.Model;
using WinSaasPOS.Common;
using WinSaasPOS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WinSaasPOS
{
    public partial class frmChangeMode : Form
    {

        private DBPRODUCT_BEANBLL productbll = new DBPRODUCT_BEANBLL();
        private HttpUtil httputil = new HttpUtil();

        /// <summary>
        /// this.enable=false; 页面不可用页面不可控；  通过该标志控制页面是否可用
        /// </summary>
        private bool IsEnable = true;

        public frmChangeMode()
        {
            InitializeComponent();
        }

        private void frmChangeMode_Shown(object sender, EventArgs e)
        {

            lblShopName.Text = MainModel.CurrentShopInfo.shopname;
            btnMenu.Text = MainModel.CurrentUser.nickname + "，你好   ";
            timerNow.Interval = 1000;
            timerNow.Enabled = true;
            btnOnLineType.Left = lblShopName.Left + lblShopName.Width + 10;
            if (MainModel.IsOffLine)
            {
                btnOnLineType.BackgroundImage = Resources.ResourcePos.OffLineType; btnOnLineType.Text = "   离线";
            }
            else
            {
                btnOnLineType.BackgroundImage = Resources.ResourcePos.OnLineType; btnOnLineType.Text = "   在线";
            }

            lblLastTime.Text = MainModel.GetDateTimeByStamp(MainModel.LastQuerySkushopAllTimeStamp.ToString()).ToString("yyyy-MM-dd HH:mm:ss");

            if (MainModel.IsOffLine)
            {
                picOffLine.Visible = true;
                picOnLine.Visible = false;

                btnLoadScale.Visible = false;

                 
            }
            else
            {
                picOffLine.Visible = false;
                picOnLine.Visible = true;
                btnLoadScale.Visible = true;
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            if (!IsEnable)
            {
                return;
            }
            this.Close();
        }

        private void timerNow_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }


        ServerDataUtil serverutil = new ServerDataUtil();
        //当天第一次进入更新全局商品
        private void LoadAllProduct()
        {
            try
            {
                IsEnable = false;
                LoadingHelper.ShowLoadingScreen("请稍候...");
                string errormesg = "";

                int i = 0;
                lstproduct.Clear();

                productbll.AddProduct(GetAllProdcut(1, 200),MainModel.URL);

                IsEnable = true;
                
                LoadingHelper.CloseForm();


                MainModel.LastQuerySkushopAllTimeStamp = MainModel.getStampByDateTime(DateTime.Now);

                lblLastTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
               // serverutil.UpdatePromotion();

                MainModel.ShowLog("同步成功",false);
            }
            catch (Exception ex)
            {
                this.Enabled = true;
                LoadingHelper.CloseForm();
                LogManager.WriteLog("手动更新全部商品异常" + ex.Message);
                MainModel.ShowLog("同步失败"+ex.Message,true);
            }
            finally
            {
                IsEnable = true;
            }
        }

        List<DBPRODUCT_BEANMODEL> lstproduct = new List<DBPRODUCT_BEANMODEL>();
        private List<DBPRODUCT_BEANMODEL> GetAllProdcut(int page, int size)
        {
            try
            {
                string errormesg = "";
                AllProduct allproduct = httputil.QuerySkushopAll(MainModel.CurrentShopInfo.shopid, page, size, ref errormesg);

                if (!string.IsNullOrEmpty(errormesg) || allproduct == null)
                {
                    LogManager.WriteLog("更新全部商品异常" + errormesg);
                    return null;
                }
                else
                {
                    MainModel.LastQuerySkushopCrementTimeStamp = allproduct.timestamp.ToString();

                    lstproduct.AddRange(allproduct.rows);
                    if (allproduct.rows.Count >= size)
                    {
                        GetAllProdcut(page + 1, size);
                    }
                }

                return lstproduct;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ERROR", "获取全量商品异常" + ex.Message);
                return null;
            }
        }

        private void btnLoadAllProduct_Click(object sender, EventArgs e)
        {

            if (!IsEnable)
            {
                return;
            }
            //启动全量商品同步线程
            Thread threadLoadAllProduct = new Thread(LoadAllProduct);
            threadLoadAllProduct.IsBackground = true;
            threadLoadAllProduct.Start();
        }


        private void btnWindows_Click(object sender, EventArgs e)
        {
            MainModel.ShowWindows();
        }


        AutoSizeFormUtil asf = new AutoSizeFormUtil();
        private void OnLine_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MainModel.IsOffLine)
                {
                    return;
                }

                LoadPicScreen(true);
                frmConfirmChange frmConfirm = new frmConfirmChange();

                //asf.AutoScaleControlTest(frmConfirm, 490, 230, Screen.AllScreens[0].Bounds.Width * 30 / 100, Screen.AllScreens[0].Bounds.Height * 25 / 100, true);
                frmConfirm.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmConfirm.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmConfirm.Height) / 2);

                frmConfirm.ShowDialog();
                LoadPicScreen(false);

                if (frmConfirm.DialogResult == DialogResult.OK)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }


            }
            catch (Exception ex)
            {
                MainModel.ShowLog("切换在线模式异常"+ex.Message,true);
            }
            finally
            {
                LoadPicScreen(false);
            }
           
        }


        private void OffLine_Click(object sender, EventArgs e)
        {

            try
            {
                if (MainModel.IsOffLine)
                {
                    return;
                }

                LoadPicScreen(true);
                frmConfirmChange frmConfirm = new frmConfirmChange();

                //asf.AutoScaleControlTest(frmConfirm, 490, 230, Screen.AllScreens[0].Bounds.Width * 30 / 100, Screen.AllScreens[0].Bounds.Height * 25 / 100, true);
                frmConfirm.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmConfirm.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmConfirm.Height) / 2);

                frmConfirm.ShowDialog();
                LoadPicScreen(false);

                if (frmConfirm.DialogResult == DialogResult.OK)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }


            }
            catch (Exception ex)
            {
                MainModel.ShowLog("切换离线模式异常" + ex.Message, true);
            }
            finally
            {
                LoadPicScreen(false);
            }
        }

        private void LoadPicScreen(bool isShown)
        {
            try
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
               
            }
            catch (Exception ex)
            {
                picScreen.Visible = false;
                LogManager.WriteLog("修改主窗体背景图异常：" + ex.Message);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmChangeMode_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                timerNow.Enabled = false;
                this.Dispose();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("清理切换窗体资源异常" + ex.Message);
            }
        }

    }
}
