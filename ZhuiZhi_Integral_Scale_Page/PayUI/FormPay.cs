using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.BaseUI;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.PayUI
{
    public partial class FormPay : Form
    {
        /// <summary>
        /// 判断页面是否允许操作 （this.enable 页面灰化  很丑）
        /// </summary>
        private bool IsEnable = true;

        public Cart thisCurrentCart = new Cart();

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
            //使用委托的话frmmain界面会卡死
            Control.CheckForIllegalCrossThreadCalls = false;
            pnlLine.Height = 1;
        }

        private void FormPay_Activated(object sender, EventArgs e)
        {
            try
            {
                UpdateDetail();
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("初始化支付页面异常" + ex.Message);
            }
        }

        private void UpdateDetail(bool needRefresh =false)
        {

            try
            {
                dgvCartDetail.Rows.Clear();

                if (needRefresh)
                {


                    if (thisCurrentCart.otherpayinfos != null && thisCurrentCart.otherpayinfos.Count > 0)
                    {
                        foreach (OtherPayInfoEntity otherpay in thisCurrentCart.otherpayinfos)
                        {
                            OrderPriceDetail detail = new OrderPriceDetail();
                            detail.title = otherpay.payname;
                            detail.amount = "￥" + otherpay.payamt.ToString("f2");
                            thisCurrentCart.orderpricedetails.Add(detail);
                        }
                    }

                    if (thisCurrentCart.cashpayamt != null && thisCurrentCart.cashpayamt > 0)
                    {
                        OrderPriceDetail detail = new OrderPriceDetail();
                        detail.title = "实收现金";
                        detail.amount = "￥" + thisCurrentCart.cashpayamt.ToString("f2");
                        thisCurrentCart.orderpricedetails.Add(detail);
                       // dgvCartDetail.Rows.Add("实收现金:", "￥" + thisCurrentCart.cashpayamt.ToString("f2"));
                    }
                }

                foreach (OrderPriceDetail orderprice in thisCurrentCart.orderpricedetails)
                {
                    dgvCartDetail.Rows.Add(orderprice.title, orderprice.amount);
                }

                //if (thisCurrentCart.otherpayinfos != null && thisCurrentCart.otherpayinfos.Count > 0)
                //{
                //    foreach (OtherPayInfoEntity otherpay in thisCurrentCart.otherpayinfos)
                //    {
                //        dgvCartDetail.Rows.Add(otherpay.payname + ":", "￥" + otherpay.payamt.ToString("f2"));
                //    }
                //}

                //if (thisCurrentCart.cashpayamt != null && thisCurrentCart.cashpayamt > 0)
                //{
                //    dgvCartDetail.Rows.Add("实收现金:", "￥" + thisCurrentCart.cashpayamt.ToString("f2"));                      
                //}

                if (thisCurrentCart.otherpayinfos!=null && thisCurrentCart.otherpayinfos.Count > 0)
                {
                    lblTitle.Text = "继续支付";
                    lblTotalInfo.Text = "还需支付:";
                  
                }
                else
                {
                    lblTitle.Text = "结算";
                    lblTotalInfo.Text = "应收:";
                }

                lblTotalPay.Text = "￥" + thisCurrentCart.totalpayment.ToString("f2");

                if (thisCurrentCart.balancepaypromoamt != null && thisCurrentCart.balancepaypromoamt > 0)
                {
                    btnMemberPromo.Visible = true;
                    btnMemberPromo.Text = "余额支付再减：￥" + thisCurrentCart.balancepaypromoamt.ToString("f2");
                    pnlTotalPay.Height = btnMemberPromo.Bottom;
                }
                else
                {
                    btnMemberPromo.Visible = false;
                    pnlTotalPay.Height = lblTotalPay.Bottom;
                }
               

                dgvCartDetail.Height = dgvCartDetail.Rows.Count * dgvCartDetail.RowTemplate.Height + 5;

                pnlTotalPay.Top = dgvCartDetail.Bottom;

                pnlPayType.Top = pnlTotalPay.Bottom;

                this.Height = pnlPayType.Bottom;

                this.Top = (Screen.AllScreens[0].Bounds.Height - this.Height) / 2;
                UpdatePaymentTypes();
            }
            catch (Exception ex)
            {

            }

        }


        private void btnCancle_Click(object sender, EventArgs e)
        {
            PayResult = 0;
            this.Close();
        }






        private void pnlPayByOnLine_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable || pnlPayByOnLine.Tag==null || pnlPayByOnLine.Tag.ToString()=="0" )
                {
                    return;
                }

                IsEnable = false;
                LoadingHelper.ShowLoadingScreen();
                string ErrorMsg = "";
                int ResultCode = -1;
                CreateOrderResult orderresult = httputil.CreateOrder(thisCurrentCart, ref ErrorMsg, ref ResultCode);
                this.Activate(); //任务栏占用焦点，强制刷新
                if (ResultCode != 0 || orderresult == null)
                {
                    if (CheckUserAndMember(ResultCode))
                    {
                        return;
                    }
                    else
                    {
                        MainModel.ShowLog(ErrorMsg, true);
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
                this.Activate();
            }
            catch (Exception ex)
            {
                //ShowLog("在线收银异常" + ex.Message, true);
            }
            finally
            {
                LoadingHelper.CloseForm();
                IsEnable = true;
            }
         
        }

        private void pnlPayByCash_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable || pnlPayByCash.Tag == null || pnlPayByCash.Tag.ToString() == "0")
                {
                    return;
                }
                IsEnable = false;

                thisCurrentCart.cashpayoption = 1;
                if (!RefreshCart())
                {
                    return;
                }

                string orderid = "";
                int type = PayHelper.ShowFormPayByCash(thisCurrentCart, out orderid);
               
                 if (type == 1) //支付完成
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
                    //if (thisCurrentCart != null)
                    //{
                    //    thisCurrentCart.cashpayoption = 0;
                    //    thisCurrentCart.cashpayamt = 0;
                    //}

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
                IsEnable = true;
            }
        }


        //判断进入会员支付没有会员  且 退出时有会员则退出该页面
        bool needexit = true;
        /// <summary>
        /// 如果之前没有会员 扫码会员 但不支付需要退出该页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlPayByBalance_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable || pnlPayByBalance.Tag == null || pnlPayByBalance.Tag.ToString() == "0")
                {
                    return;
                }

                needexit = MainModel.CurrentMember == null;
                IsEnable = false;

                Cart outcart = new Cart();

                thisCurrentCart.cashprepriority = thisCurrentCart.cashpayamt > 0 ? 1 : 0;
                string successorderid = "";
                int resultcode = PayHelper.ShowFormPayByBalance(thisCurrentCart, out successorderid, out outcart);
                if (outcart != null)
                {
                    thisCurrentCart = outcart;
                }

                if (CheckUserAndMember(resultcode))
                {
                    return;
                }

                if (resultcode == 1) //订单完成
                {
                    PayHelper.ShowFormPaySuccess(successorderid);
                    this.DialogResult = DialogResult.OK;
                    PayResult = 1;
                    this.Close();
                    return;
                }               
                else
                {

                    if (thisCurrentCart.balancepayamt == 0)
                    {
                        thisCurrentCart.balancepayoption = 0;
                    }

                    //扫余额码等会员  且取消支付 返回收银页面
                    if (needexit && MainModel.CurrentMember != null && thisCurrentCart.balancepayamt==0)
                    {
                        PayResult = 0;
                        this.Close();
                    }
                    else
                    {
                        RefreshCart();
                    }
                    
                }

                Application.DoEvents();
            }
            catch (Exception ex)
            {
                //ShowLog("在线收银异常" + ex.Message, true);
            }
            finally
            {
                IsEnable = true;
            }
        }

        private void pnlPayByOther_Click(object sender, EventArgs e)
        {
            try
            {

                if (!IsEnable || pnlPayByOther.Tag == null || pnlPayByOther.Tag.ToString() == "0")
                {
                    return;
                }

                ShowPayOther();
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("选择其他支付方式异常" + ex.Message, true);
            }
            finally
            {
                IsEnable = true;
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
                    UpdateDetail(true);
                    UpdatePaymentTypes();
                    BaseUIHelper.UpdaForm(thisCurrentCart);
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
                if (thisCurrentCart.paymenttypes.cashenabled == 1)
                {
                    pnlPayByCash.Tag = 1;
                    pnlPayByCash.BackColor = Color.FromArgb(255, 146, 27);
                }
                else
                {
                    pnlPayByCash.Tag = 0;
                    pnlPayByCash.BackColor = Color.Silver;
                }

                if (thisCurrentCart.paymenttypes.onlineenabled == 1)
                {
                    pnlPayByOnLine.Tag = 1;
                    pnlPayByOnLine.BackColor = Color.FromArgb(255, 70, 21);
                }
                else
                {
                    pnlPayByOnLine.Tag = 0;
                    pnlPayByOnLine.BackColor = Color.Silver;
                }

                if (thisCurrentCart.balancepayamt <=0)
                {
                    pnlPayByBalance.Tag = 1;
                    pnlPayByBalance.BackColor = Color.FromArgb(31, 178, 191);
                }
                else
                {
                    pnlPayByBalance.Tag = 0;
                    pnlPayByBalance.BackColor = Color.Silver;
                }

                if (thisCurrentCart.otherpayamt > 0)
                {
                    pnlPayByOther.Tag = 0;
                    pnlPayByOther.BackColor = Color.Silver;
                }
                else
                {
                    pnlPayByOther.Tag = 1;
                    pnlPayByOther.BackColor = Color.FromArgb(68, 147, 225);
                  
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("跟新收银页 支付方式状态异常"+ex.Message);
            }
        }

        private void rbtnPayByOther_ButtonClick(object sender, EventArgs e)
        {
           
        }

        public void ShowPayOther()
        {
            try
            {

                IsEnable = false;
                decimal otherpayamt = 0;
                OtherPayResult otherpayresult = PayHelper.ShowFormPayByOther(thisCurrentCart);

                if (otherpayresult != null && otherpayresult.otherpayamt > 0)
                {
                    thisCurrentCart.otherpayamt = otherpayresult.otherpayamt;
                    thisCurrentCart.otherpaycouponcode = otherpayresult.otherpaycouponcode;
                    thisCurrentCart.otherpaytype = otherpayresult.otherpaytype;
                }
                else
                {
                    thisCurrentCart.otherpayamt = 0;
                    thisCurrentCart.otherpaycouponcode = "";
                    thisCurrentCart.otherpaytype = "";
                }

                if (RefreshCart())
                {
                    //其他支付完成收银
                    if (thisCurrentCart.totalpayment == 0)
                    {
                        string ErrorMsg = "";
                        int ResultCode = 0;
                        CreateOrderResult orderresult = httputil.CreateOrder(thisCurrentCart, ref ErrorMsg, ref ResultCode);
                        if (ResultCode != 0 || orderresult == null)
                        {
                            CheckUserAndMember(ResultCode);
                            MainModel.ShowLog("异常" + ErrorMsg, true);
                        }
                        else if (orderresult.continuepay == 1)
                        {
                            //MainModel.ShowLog("需要继续支付", true);
                        }
                        else
                        {
                            PayHelper.ShowFormPaySuccess(orderresult.orderid);
                            this.DialogResult = DialogResult.OK;
                            PayResult = 1;
                            this.Close();
                            return;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("选择其他支付方式异常" + ex.Message, true);
            }
            finally
            {
                IsEnable = true;
            }
        }

        private void lbtnCancle_Click(object sender, EventArgs e)
        {
            ShowPayOther();
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
