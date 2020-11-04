using Maticsoft.BLL;
using Maticsoft.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.ScaleUI;

namespace ZhuiZhi_Integral_Scale_UncleFruit
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

            lblShopName.Text = MainModel.Titledata + "   " + MainModel.CurrentShopInfo.shopname;
            lblMenu.Text = MainModel.CurrentUser.nickname + ",你好";
            picMenu.Left = pnlMenu.Width - picMenu.Width - lblMenu.Width;
            lblMenu.Left = picMenu.Right;

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
            //if (!IsEnable)
            //{
            //    return;
            //}
            bgwUpdate.CancelAsync();
            LoadingHelper.CloseForm();
            this.Close();
        }

        private void btnLoadAllProduct_Click(object sender, EventArgs e)
        {
            try
            {
           
            if (!IsEnable)
            {
                return;
            }

            bgwUpdate.RunWorkerAsync("");
          
            }
            catch (Exception ex)
            {
                IsEnable = true;
                LoadingHelper.CloseForm();
                MainModel.ShowLog("同步数据异常"+ex.Message,true);
            }
        }

        private void btnWindows_Click(object sender, EventArgs e)
        {
            MainModel.ShowWindows();
        }


        private void frmChangeMode_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("清理切换窗体资源异常" + ex.Message);
            }
        }

        private void bgwUpdate_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                IsEnable = false;
                LoadingHelper.ShowLoadingScreen("请稍候...");

                ServerDataUtil.LoadAllProduct();


                //检测是否被取消了
                if (bgwUpdate.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                ServerDataUtil.UpdatePromotion();
                //检测是否被取消了
                if (bgwUpdate.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                ScaleDataHelper.LoadScale();
                //检测是否被取消了
                if (bgwUpdate.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                IsEnable = true;
                LoadingHelper.CloseForm();

                //同步余额参数设置
                string errormsg = "";
                MainModel.balanceconfigdetail = httputil.BalanceConfigDetail(ref errormsg);

                //检测是否被取消了
                if (!bgwUpdate.CancellationPending)
                {
                    MainModel.ShowLog("同步成功", false);
                }
                
            }
            catch { }
        }




    }
}
