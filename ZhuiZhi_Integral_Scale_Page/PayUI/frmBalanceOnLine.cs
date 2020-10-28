using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit
{
    public partial class frmBalanceOnLine : Form
    {

        /// <summary>
        /// 现金收银界面传过来的 抹零后的cartModel
        /// </summary>
        private Cart CurrentCart;


        HttpUtil httputil = new HttpUtil();


        public frmBalanceOnLine()
        {
            InitializeComponent();
        }
        public frmBalanceOnLine( Cart cart)
        {
            InitializeComponent();
            CurrentCart = (Cart)cart.qianClone();
            IniForm(CurrentCart);
        }

        private void IniForm(Cart cart)
        {
            try
            {
                lblPrice.Text = "￥" + cart.payamtbeforecash.ToString("f2");
                lblBalance.Text = "￥" + cart.balancepayamt.ToString("f2");
                lblCash.Text = "￥" + cart.cashpayamt.ToString("f2");
                lblTotalPay.Text = "￥" + cart.totalpayment.ToString("f2");

                Dictionary<string, string> dicdetail = new Dictionary<string, string>();
                dicdetail.Add("应付：", "￥" + cart.payamtbeforecash.ToString("f2"));
                dicdetail.Add("已付现金：", "￥" + cart.cashpayamt.ToString("f2"));

                ZhuiZhi_Integral_Scale_UncleFruit.BaseUI.BaseUIHelper.UpdateDgvOrderDetail(dicdetail, "还需支付：", "￥" + cart.totalpayment.ToString("f2"));
            }
            catch (Exception ex)
            {

            }
        }

        private void frmBalanceOnLine_Shown(object sender, EventArgs e)
        {

        }

        private void frmBalanceOnLine_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void lbtnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPayNext_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            try
            {
                if (keyData == Keys.Enter)
                {
                    btnPayNext_Click(null,null);
                    return false;
                }
                return false;
            }
            catch
            {
                return false;
            }


        }
    }
}
