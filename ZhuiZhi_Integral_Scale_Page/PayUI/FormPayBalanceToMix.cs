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
using ZhuiZhi_Integral_Scale_UncleFruit.PayUI;

namespace ZhuiZhi_Integral_Scale_UncleFruit
{
    public partial class FormPayBalanceToMix : Form
    {
        public string CrrentOrderid = "";

        public Cart CurrentCart;

        private HttpUtil httputil = new HttpUtil();

        public int ErrorCode = -1;
        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();

        /// <summary>
        /// 窗体返回结果  0：返回  1：支付完成 2：继续支付   errorcode：会员用户过期等信息
        /// </summary>
        public int ReturnResultCode = -1;
        public FormPayBalanceToMix()
        {
            InitializeComponent();
        }


        //public FormPayBalanceToMix(Cart cart)
        //{
        //    InitializeComponent();
        //}



        public void UpInfo(Cart cart)
        {
            try
            {

                CurrentCart = (Cart)cart.qianClone();

                lblPrice.Text = "￥" + (cart.balancepayamt + cart.totalpayment).ToString("f2");
                lblBalance.Text = "￥" + cart.balancepayamt.ToString("f2");
                lblTotalPay.Text = "￥" + cart.totalpayment.ToString("f2");
            }
            catch (Exception ex)
            {
            }
        }

        private void frmBalanceToMix_Shown(object sender, EventArgs e)
        {
        }

        private void frmBalanceToMix_FormClosing(object sender, FormClosingEventArgs e)
        {

        }


        private void CheckUserAndMember(int resultcode,string errormsg)
        {
            try
            {
                if (resultcode == MainModel.HttpMemberExpired || resultcode == MainModel.HttpUserExpired)
                {
                    ErrorCode = resultcode;
                    ReturnResultCode = resultcode;
                    this.Close();
                }
                else
                {
                    MainModel.ShowLog(errormsg,false);
                }
            }
            catch (Exception ex)
            {
                //ShowLog("验证用户/会员异常", true);
            }
        }

        private void btnOnLine_Click(object sender, EventArgs e)
        {

            string ErrorMsg = "";
             int ResultCode = 0;
            CreateOrderResult orderresult = httputil.CreateOrder(CurrentCart, ref ErrorMsg, ref ResultCode);
            if (ResultCode != 0 || orderresult == null)
            {
                CheckUserAndMember(ResultCode,ErrorMsg);
               // MainModel.ShowLog(ErrorMsg,true);
            }
            else
            {
                CrrentOrderid = orderresult.orderid;
                this.DialogResult = DialogResult.Retry;
                ReturnResultCode = 2;
                this.Close();
            }
        }

        private void btnCash_Click(object sender, EventArgs e)
        {
            try
            {

                //现金支付抹零处理
                CurrentCart.cashpayoption = 1;
                CurrentCart.cashpayamt = 0;
                string ErrorMsgCart = "";
                int ResultCode = 0;


                Cart cart = httputil.RefreshCart(CurrentCart, ref ErrorMsgCart, ref ResultCode);

                if (cart == null || !string.IsNullOrEmpty(ErrorMsgCart))
                {
                    CheckUserAndMember(ResultCode,ErrorMsgCart);
                    //MainModel.ShowLog(ErrorMsgCart, true);
                    return;
                }
                else
                {
                    CurrentCart = cart;
                }

                string orderid = "";
                int type = PayHelper.ShowFormPayByCash(CurrentCart, out orderid);

                if (type == 2)
                {
                    CrrentOrderid = orderid;
                    CurrentCart = cart;
                    this.DialogResult = DialogResult.Retry;
                    ReturnResultCode = 2;
                    this.Close();
                }
                else if (type == 1)
                {
                    CrrentOrderid = orderid;
                    CurrentCart = cart;
                    this.DialogResult = DialogResult.OK;
                    ReturnResultCode = 1;
                    this.Close();
                }
                else if (type == MainModel.HttpUserExpired || type == MainModel.HttpMemberExpired)
                {
                    ErrorCode = type;
                    ReturnResultCode = type;
                    this.Close();
                }
               
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("余额混合现金支付异常"+ex.Message,true);
            }

        }



        private void FormCashPay_DataReceiveHandle(int type, string orderid,Cart cart)
        {
            try
            {
                if (type == 0)
                {
                    CrrentOrderid = orderid;
                    CurrentCart = cart;
                    this.DialogResult = DialogResult.Retry;
                    ReturnResultCode = 2;
                    this.Close();
                }
                else if (type == 1)
                {
                    CrrentOrderid = orderid;
                    CurrentCart = cart;
                    this.DialogResult = DialogResult.OK;
                    ReturnResultCode = 1;
                    this.Close();
                }
                else if (type == MainModel.HttpUserExpired || type == MainModel.HttpMemberExpired)
                {
                    ErrorCode = type;
                    ReturnResultCode = type;
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                ReturnResultCode = 0;
                LogManager.WriteLog("ERROR", "处理确认窗体结果异常" + ex.Message);
                this.Close();
            }
            finally
            {
                MainModel.frmcashpay.DataReceiveHandle -= FormCashPay_DataReceiveHandle;
            }

        }


        private void lbtnCancle_Click(object sender, EventArgs e)
        {
            ReturnResultCode = 0;
            this.Close();
        }

        //屏蔽回车和空格键    return  false true  ???????????????
        protected override bool ProcessDialogKey(Keys keyData)
        {

            if (keyData == Keys.Enter)
            {
                LogManager.WriteLog("检测到回车键");
                return false;
            }
            if (keyData == Keys.Space)
            {
                LogManager.WriteLog("检测到空格键");
                return true;
            }

            else
                return base.ProcessDialogKey(keyData);
        }

    }
}
