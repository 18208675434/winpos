using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinSaasPOS_Scale.Common;
using WinSaasPOS_Scale.Model;

namespace WinSaasPOS_Scale.PayUI
{
    public partial class FormPay : Form
    {
        /// <summary>
        /// 判断页面是否允许操作 （this.enable 页面灰化  很丑）
        /// </summary>
        private bool IsEnable = true;

        private Cart thisCurrentCart = new Cart();

        private HttpUtil httputil = new HttpUtil();

        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();

        /// <summary>
        /// 返回结果   -1：初始值  0：返回   1:完成   其他 后端code值
        /// </summary>
        public int PayResult = -1;
        public FormPay()
        {
            InitializeComponent();
        }

        public void UpInfo(Cart cart)
        {
            try
            {
                thisCurrentCart = cart;

                dgvCartDetail.Rows.Clear();
                foreach (OrderPriceDetail orderprice in cart.orderpricedetails)
                {

                     dgvCartDetail.Rows.Add(orderprice.title, orderprice.amount);                           
                }

                lblTotalPay.Text = "￥" + cart.totalpayment.ToString("f2");

                UpdatePaymentTypes();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("初始化支付页面异常"+ex.Message);
            }
        }
        private void FormPay_Resize(object sender, EventArgs e)
        {
            SetWindowRegion();
        }

        /// <summary>
        /// 设置窗体的Region   画半径为10的圆角
        /// </summary>
        public void SetWindowRegion()
        {
            try
            {
                GraphicsPath FormPath;
                Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
                FormPath = GetRoundedRectPath(rect, 10);
                this.Region = new Region(FormPath);
            }
            catch (Exception ex)
            {

            }

        }
        /// <summary>
        /// 绘制圆角路径
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            try
            {
                int diameter = radius;
                Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
                GraphicsPath path = new GraphicsPath();

                // 左上角
                path.AddArc(arcRect, 180, 90);

                // 右上角
                arcRect.X = rect.Right - diameter;
                path.AddArc(arcRect, 270, 90);

                // 右下角
                arcRect.Y = rect.Bottom - diameter;
                path.AddArc(arcRect, 0, 90);

                // 左下角
                arcRect.X = rect.Left;
                path.AddArc(arcRect, 90, 90);
                path.CloseFigure();//闭合曲线
                return path;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            PayResult = 0;
            this.Close();
        }

        //现金支付
        private void rbtnByCash_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable || !rbtnByCash.WhetherEnable)
                {
                    return;
                }

                    thisCurrentCart.cashpayoption = 1;
                    if (!RefreshCart())
                    {
                        return;
                    }

                    string orderid = "";
                    int type = PayHelper.ShowFormPayByCash(thisCurrentCart,out orderid);
                    
                    if (type == 2) //需要继续支付
                    {
                       
                            if (PayHelper.ShowFormPayByOnLine(orderid, thisCurrentCart))
                            {

                                PayHelper.ShowFormPaySuccess(orderid);
                                this.DialogResult = DialogResult.OK;
                                PayResult = 1;
                                this.Close();
                                return;
                            }
                            else
                            {
                                RefreshCart();
                            }
                    }
                    else if (type == 1) //支付完成
                    {
                        
                            PayHelper.ShowFormPaySuccess(orderid);
                            this.DialogResult = DialogResult.OK;
                            PayResult = 1;
                            this.Close();
                            return;
                       
                    }
                    else if (type == MainModel.HttpUserExpired || type == MainModel.HttpMemberExpired)
                    {
                        PayResult = type;
                        this.Close();
                        return;
                    }
                    else
                    {
                        if (thisCurrentCart != null)
                        {
                            thisCurrentCart.cashpayoption = 0;
                            thisCurrentCart.cashpayamt = 0;
                        }

                        RefreshCart();
                    }

                 
                
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("现金收银异常：" + ex.Message, true);
            }
            finally
            {
                LoadingHelper.CloseForm();

            }
        }

        //微信支付宝支付
        private void rbtnPayOnLine_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable || !rbtnPayOnLine.WhetherEnable)
                {
                    return;
                }

                this.Enabled = false;
                LoadingHelper.ShowLoadingScreen();
                        string ErrorMsg = "";
                        int ResultCode = -1;
                        CreateOrderResult orderresult = httputil.CreateOrder(thisCurrentCart, ref ErrorMsg, ref ResultCode);
                        if (ResultCode != 0 || orderresult == null)
                        {
                            if (CheckUserAndMember(ResultCode))
                            {
                                return;
                            }
                            else
                            {
                                MainModel.ShowLog(ErrorMsg,true);
                            }
                         
                        }
                        else if (orderresult.continuepay == 1)
                        {

                            if (PayHelper.ShowFormPayByOnLine(orderresult.orderid, thisCurrentCart))
                            {

                                PayHelper.ShowFormPaySuccess(orderresult.orderid);
                                this.DialogResult = DialogResult.OK;
                                PayResult = 1;
                                this.Close();
                                return;
                            }
                            else
                            {
                                RefreshCart();
                            }
                        }
                        this.Enabled = true;
                Application.DoEvents();

            }
            catch (Exception ex)
            {
                //ShowLog("在线收银异常" + ex.Message, true);
            }
         
        }

        //余额支付
        private void rbtnPayByBalance_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable || !rbtnPayByBalance.WhetherEnable)
                {
                    return;
                }
                //if (btnPayByBalance.Tag == null || btnPayByBalance.Tag.ToString() != "1")
                //{
                //    return;
                //}

                string successorderid = "";
                int resultcode = PayHelper.ShowFormPayByBalance(thisCurrentCart, out successorderid);


                if (CheckUserAndMember(resultcode))
                {
                    return;
                }

                if (resultcode==1) //订单完成
                {
                    PayHelper.ShowFormPaySuccess(successorderid);
                    this.DialogResult = DialogResult.OK;
                    PayResult = 1;
                    this.Close();
                    return;
                }
                else if (resultcode==2) //需要继续支付  微信/支付宝
                {
                    string returnorderid = "";
                    int returnresultcode = PayHelper.ShowFormPayBalanceToMix(thisCurrentCart,out returnorderid);

                    if (returnresultcode == 2)
                    {
                        this.Invoke(new InvokeHandler(delegate()
                        {
                            if (PayHelper.ShowFormPayByOnLine(returnorderid, thisCurrentCart))
                            {

                                PayHelper.ShowFormPaySuccess(returnorderid);
                                this.DialogResult = DialogResult.OK;
                                PayResult = 1;
                                this.Close();
                                return;
                            }
                            else
                            {
                                RefreshCart();
                            }
                        }));
                    }
                    else if (returnresultcode==1)
                    {

                        this.Invoke(new InvokeHandler(delegate()
                        {
                            PayHelper.ShowFormPaySuccess(returnorderid);
                            this.DialogResult = DialogResult.OK;
                            PayResult = 1;
                            this.Close();
                            return;
                        }));
                    }
                    else
                    {
                        if (CheckUserAndMember(returnresultcode))
                        {
                            return;
                        }
                        RefreshCart();
                    }
                }
                else
                {                  
                    RefreshCart();
                }

                
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                //ShowLog("在线收银异常" + ex.Message, true);
            }
           
        }

        //代金券支付
        private void rbtnPayByCoupon_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable || !rbtnPayByCoupon.WhetherEnable)
                {
                    return;
                }

                if (!RefreshCart())
                {
                    return;
                }

                string ErrorMsg = "";
                List<string> lstCashCoupons = httputil.GetAvailableCashCoupons(ref ErrorMsg);

                if (!string.IsNullOrEmpty(ErrorMsg))
                {
                    MainModel.ShowLog(ErrorMsg, false);
                    return;
                }
                else if (lstCashCoupons == null || lstCashCoupons.Count == 0)
                {
                    MainModel.ShowLog("没有可用的代金券", false);
                    return;
                }

                string successorderid = "";
                if(PayHelper.ShowFormPayByCashCoupon(thisCurrentCart, lstCashCoupons,out successorderid))
                {
                    PayHelper.ShowFormPaySuccess(successorderid);
                    this.DialogResult = DialogResult.OK;
                    PayResult = 1;
                    this.Close();
                    return;
                }
                else
                {
                    thisCurrentCart.cashcouponamt = 0;
                   
                    RefreshCart();
                }

                thisCurrentCart.cashcouponamt = 0;
               
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("代金券收银异常：" + ex.Message, true);
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

                    MainModel.ShowLog(ErrorMsgCart,true);
                    return false;
                }
                else
                {
                    thisCurrentCart = cart;
                    UpdatePaymentTypes();
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




        /// <summary>
        /// 检查是否会员过期或 用户登录过期 返回true  需要退出
        /// </summary>
        /// <param name="resultcode"></param>
        /// <returns></returns>
        private bool CheckUserAndMember(int resultcode)
        {
            try
            {
                if (resultcode == MainModel.HttpMemberExpired || resultcode == MainModel.HttpUserExpired)
                {
                    resultcode = resultcode;
                    this.Close();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        private void UpdatePaymentTypes()
        {
            try
            {
                rbtnByCash.WhetherEnable = thisCurrentCart.paymenttypes.cashenabled == 1;
                rbtnPayOnLine.WhetherEnable = thisCurrentCart.paymenttypes.onlineenabled == 1;
                rbtnPayByBalance.WhetherEnable = thisCurrentCart.paymenttypes.balancepayenabled == 1;
                rbtnPayByCoupon.WhetherEnable = thisCurrentCart.paymenttypes.cashcouponpayenabled == 1;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("跟新收银页 支付方式状态异常"+ex.Message);
            }
        }

        ////屏蔽回车和空格键
        //protected override bool ProcessDialogKey(Keys keyData)
        //{
        //    if (keyData == Keys.Enter || keyData == Keys.Space)
        //        return false;
        //    else
        //        return base.ProcessDialogKey(keyData);
        //}

    }
}
