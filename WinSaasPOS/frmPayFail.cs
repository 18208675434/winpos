using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinSaasPOS.Model;
using WinSaasPOS.Common;

namespace WinSaasPOS
{
    public partial class frmPayFail : Form
    {
        public frmPayFail()
        {
            InitializeComponent();
        }


        public frmPayFail(string FailInfo )
        {
            InitializeComponent();
            lblInfo.Text = FailInfo;
        }

        private void frmPayFail_Shown(object sender, EventArgs e)
        {
            try
            {
                lblTime.Text = MainModel.Titledata;
                btnMenu.Text = MainModel.CurrentUser.nickname + "，你好   ";
                btnMenu.Left = Math.Max(pnlHead.Width - btnMenu.Width - 10, btnCancle.Left + btnCancle.Width);
                lblShopName.Text = MainModel.CurrentShopInfo.shopname;
                btnOnLineType.Left = lblShopName.Left + lblShopName.Width + 10;
                if (MainModel.IsOffLine)
                {
                    btnOnLineType.BackgroundImage = Resources.ResourcePos.OffLineType; btnOnLineType.Text = "   离线";
                }
                else
                {
                    btnOnLineType.BackgroundImage = Resources.ResourcePos.OnLineType; btnOnLineType.Text = "   在线";
                }

                MainModel.frmmainmedia.ShowPayInfo("请出示微信/支付宝付款码", true);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("支付失败页加载异常"+ex.Message);
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();

            //this.Dispose();
        }

        private void btnChangePayType_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void btnTryAgain_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
           
            this.Close();
        }

        private void btnWindows_Click(object sender, EventArgs e)
        {
            MainModel.ShowWindows();
        }

        private void frmPayFail_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                this.Dispose();
            }
            catch
            {

            }
        }
    }
}
