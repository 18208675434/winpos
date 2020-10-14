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

namespace ZhuiZhi_Integral_Scale_UncleFruit.ReturnWithoutOrder
{
    public partial class FormReturnCashOrBalance : Form
    {
        private Cart thisCurrentCart = null;
        private Member thisCurrentMember = null;

        private HttpUtil httputil = new HttpUtil();
        public FormReturnCashOrBalance(Cart cart, Member member)
        {
            InitializeComponent();
            thisCurrentCart = cart;
            thisCurrentMember = member;
        }

        private void FormReturnCash_Load(object sender, EventArgs e)
        {
            lblReturnCash.Text = thisCurrentCart.totalpayment.ToString("f2");
            lblProCount.Text = thisCurrentCart.productcount.ToString();
            lblRefundPoint.Text = thisCurrentCart.returnpoint.ToString();
        }

        private void FormReturnCash_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


        private void btnReturnBalance_Click(object sender, EventArgs e)
        {
            if (ReturnWithoutOrderHelper.ShowFormRetunCash(thisCurrentCart, thisCurrentMember, ReturnType.balance))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void btnReturnCash_Click(object sender, EventArgs e)
        {

            thisCurrentCart.cashpayoption = 1;
            RefreshCart();

            if (ReturnWithoutOrderHelper.ShowFormRetunCash(thisCurrentCart, thisCurrentMember, ReturnType.cash))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        //刷新购物车
        private bool RefreshCart()
        {
            try
            {
                this.Enabled = false;
                LoadingHelper.ShowLoadingScreen("加载中...");

                string ErrorMsgCart = "";
                int ResultCode = -1;
                Cart cart = httputil.RefreshCart(thisCurrentCart, ref ErrorMsgCart, ref ResultCode);


                if (ErrorMsgCart != "" || cart == null) //商品不存在或异常
                {
                    this.Enabled = true;
                    LoadingHelper.CloseForm();

                    MainModel.ShowLog(ErrorMsgCart, true);
                    return false;
                }
                else
                {
                    thisCurrentCart = cart;
                    return true;
                }
            }
            catch (Exception ex)
            {

                MainModel.ShowLog("刷新购物车异常" + ex.Message, true);
                return false;
            }
            finally
            {
                this.Enabled = true;
                LoadingHelper.CloseForm();
            }
        }
    }
}
