using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;

namespace ZhuiZhi_Integral_Scale_UncleFruit.GiftCard
{
    public partial class FormGiftCardPayFail : Form
    {
        public FormGiftCardPayFail()
        {
            InitializeComponent();
        }


        public FormGiftCardPayFail(string FailInfo)
        {
            InitializeComponent();
            lblInfo.Text = FailInfo;
        }

        private void frmPayFail_Shown(object sender, EventArgs e)
        {
         
            lblShopName.Text = MainModel.Titledata + "   " + MainModel.CurrentShopInfo.shopname;
            lblMenu.Text = MainModel.CurrentUser.nickname + ",你好";
            picMenu.Left = pnlMenu.Width - picMenu.Width - lblMenu.Width;
            lblMenu.Left = picMenu.Right;

            ZhuiZhi_Integral_Scale_UncleFruit.BaseUI.BaseUIHelper.ShowPayInfo("请出示微信/支付宝付款码", true);
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
