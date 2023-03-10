using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QDAMAPOS.Model;
using QDAMAPOS.Common;

namespace QDAMAPOS
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
            btnMenu.Text = MainModel.CurrentUser.nickname + "，你好  ";
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
            timerNow.Interval = 1000;
            timerNow.Enabled = true;

            MainModel.frmmainmedia.ShowPayInfo("请出示微信/支付宝付款码", true);
        }

        private void timerNow_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}
