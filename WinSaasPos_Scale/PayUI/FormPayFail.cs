using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinSaasPOS_Scale.Model;
using WinSaasPOS_Scale.Common;

namespace WinSaasPOS_Scale
{
    public partial class FormPayFail : Form
    {
        public FormPayFail()
        {
            InitializeComponent();
        }


        public FormPayFail(string FailInfo )
        {
            InitializeComponent();
            lblInfo.Text = FailInfo;
        }

        private void frmPayFail_Shown(object sender, EventArgs e)
        {
            btnMenu.Text = MainModel.CurrentUser.nickname + "，你好   ";
            btnMenu.Left = Math.Max(pnlHead.Width - btnMenu.Width-10, btnCancle.Left + btnCancle.Width);
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

            WinSaasPOS_Scale.BaseUI.BaseUIHelper.ShowPayInfo("请出示微信/支付宝付款码", true);
        }

        private void timerNow_Tick(object sender, EventArgs e)
        {
           lblTime.Text = MainModel.Titledata;
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
           // this.Close();

            this.Dispose();
        }

        private void btnChangePayType_Click(object sender, EventArgs e)
        {
            this.Dispose();
            //this.Close();
        }

        private void btnTryAgain_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
            //this.Close();
        }

        private void btnWindows_Click(object sender, EventArgs e)
        {
            MainModel.ShowWindows();
        }
    }
}
