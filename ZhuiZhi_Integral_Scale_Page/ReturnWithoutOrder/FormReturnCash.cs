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
    public partial class FormReturnCash : Form
    {
        private Cart thisCurrentCart = null;
        private Member thisCurrentMember = null;

        private HttpUtil httputil = new HttpUtil();

        private ReturnType currentreturntye = ReturnType.cash;
        public FormReturnCash(Cart cart,Member member,ReturnType type)
        {
            InitializeComponent();
            thisCurrentCart = cart;
            thisCurrentMember = member;
            currentreturntye = type;
        }

        private void FormReturnCash_Load(object sender, EventArgs e)
        {
            lblReturnCash.Text = thisCurrentCart.totalpayment.ToString("f2");

            switch (currentreturntye)
            {
                case ReturnType.cash: lblType.Text = "确认退回现金？";break;
                case ReturnType.balance:lblType.Text ="确认退回余额？";break;
                default: break;
            }
        }

        private void FormReturnCash_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

                 if (!RefreshCart())
                {
                    return;
                }

                 string smscode = ReturnWithoutOrderHelper.ShowFormCheckSmCode(thisCurrentCart.shopownername,thisCurrentCart.shopownerphone);

                 if (string.IsNullOrEmpty(smscode))
                 {
                     return;
                 }

            //TODO  门店负责人发送验证码

                         thisCurrentCart.returnwithoutorder = 1;
                         thisCurrentCart.smscode = smscode;
                         if (currentreturntye == ReturnType.cash)
                         {
                             thisCurrentCart.cashpayoption = 1;
                             thisCurrentCart.cashpayamt = thisCurrentCart.totalpayment;

                             thisCurrentCart.balancepayoption = 0;
                         }
                         else if (currentreturntye == ReturnType.balance)
                         {
                             thisCurrentCart.balancepayoption = 1;
                             thisCurrentCart.cashpayoption = 0;
                         }
                         string ErrorMsg = "";
                         int ResultCode = 0;
                         CreateOrderResult orderresult = httputil.CreateOrder(thisCurrentCart, ref ErrorMsg, ref ResultCode);
                         if (ResultCode != 0 || orderresult == null)
                         {
                             MainModel.ShowLog("异常" + ErrorMsg, true);
                         }
                         else if (orderresult.continuepay == 1)
                         {
                             //TODO  继续支付
                             MainModel.ShowLog("需要继续支付", true);
                         }
                         else
                         {
                             ZhuiZhi_Integral_Scale_UncleFruit.PayUI.PayHelper.ShowFormPaySuccess(orderresult.orderid);
                             this.DialogResult = DialogResult.OK;
                             this.Close();

                         }

        }
        private bool RefreshCart()
        {
            try
            {
                this.Enabled = false;
                LoadingHelper.ShowLoadingScreen("加载中...");

                string ErrorMsgCart = "";
                int ResultCode = 0;

                thisCurrentCart.returnwithoutorder = 1;

                if (currentreturntye == ReturnType.cash)
                {
                    thisCurrentCart.cashpayoption = 1;
                    //thisCurrentCart.cashpayamt = thisCurrentCart.totalpayment;

                    thisCurrentCart.balancepayoption = 0;
                    //thisCurrentCart.balancepayamt = 0;
                }
                else if (currentreturntye == ReturnType.balance)
                {
                    thisCurrentCart.balancepayoption = 1;
                    //thisCurrentCart.balancepayamt = thisCurrentCart.totalpayment;
                    thisCurrentCart.cashpayoption = 0;
                    //thisCurrentCart.cashpayamt = 0;

                }
              

                Cart cart = httputil.RefreshCart(thisCurrentCart, ref ErrorMsgCart, ref ResultCode);


                if (ErrorMsgCart != "" || cart == null) //商品不存在或异常
                {
                    this.Enabled = true;
                    LoadingHelper.CloseForm();

                    MainModel.ShowLog(ErrorMsgCart, false);
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

    public enum ReturnType
    {
        balance,//退余额
        cash//退现金
    }
}
