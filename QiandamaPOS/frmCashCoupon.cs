using QiandamaPOS.Common;
using QiandamaPOS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QiandamaPOS
{
    public partial class frmCashCoupon : Form
    {

        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();


        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();

        private HttpUtil httputil = new HttpUtil();

        private Cart CurrentCart = new Cart();

        public frmCashCoupon(Cart cart)
        {
            InitializeComponent();
            CurrentCart = cart;
            LoadCashCoupon();
        }

        private void frmCashCoupon_Shown(object sender, EventArgs e)
        {
            
        }

        private void ShowLog(string msg, bool iserror)
        {
            this.Invoke(new InvokeHandler(delegate()
            {

                frmMsg frmmsf = new frmMsg(msg, iserror, 1000);
                frmmsf.ShowDialog(); LogManager.WriteLog(msg);
            }));

        }

        private void LoadCashCoupon()
        {
            string ErrorMsg = "";
            List<string> lstCashCoupons = httputil.GetAvailableCashCoupons(ref ErrorMsg);

            if (!string.IsNullOrWhiteSpace(ErrorMsg))
            {
                ShowLog(ErrorMsg,false);
            }
            foreach (string casncoupon in lstCashCoupons)
            {
                AddCashButton(casncoupon);
            }

            if (pnlCashCoupons.Controls.Count > 0)
            {
                Button btn =(Button) pnlCashCoupons.Controls[0];
                if (btn.Enabled == true)
                {
                    btnCash_Click(btn, new EventArgs());
                }
                
            }
        }


        private void btnCash_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            decimal casncoupon = Convert.ToDecimal(btn.Tag.ToString());
                string ErrorMsgCart = "";

            CurrentCart.cashcouponamt = casncoupon;
            RefreshCart();
        }

        bool isfirst = true;
        private void AddCashButton(string casnNum)
        {
            int count = pnlCashCoupons.Controls.Count;
            Button btntemp = new Button();
            btntemp.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //btntemp.Location = new System.Drawing.Point(23, 15);
            btntemp.Name = "button1";
            btntemp.Size = new System.Drawing.Size(148, 41);
            btntemp.TabIndex = 0;
            btntemp.Text = casnNum;
            btntemp.UseVisualStyleBackColor = true;
            btntemp.Tag = casnNum;

            if (Convert.ToDecimal(casnNum) > CurrentCart.totalpayment)
            {
                btntemp.Enabled = false;
            }

            btntemp.Click += new System.EventHandler(btnCash_Click);

            int inty = 0;
            int intx = 0;

            if (count > 0)
            {
                inty = count / 2;
                intx = count % 2;
            }

            int left = intx * (pnlCashCoupons.Width / 2) + 5;
            int top = inty * (btntemp.Height + 5);

            pnlCashCoupons.Controls.Add(btntemp);
            btntemp.Location = new System.Drawing.Point(left, top);
            btntemp.Show();

            
        }

        public void frmCashCoupon_SizeChanged(object sender, EventArgs e)
        {
            asf.ControlAutoSize(this);
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPayByCash_Click(object sender, EventArgs e)
        {

            try
            {
                CurrentCart.cashpayoption = 1;
                if (!RefreshCart())
                {
                    return;
                }


                frmCashPay frmcash = new frmCashPay(CurrentCart);
                frmcash.frmCashPay_SizeChanged(null, null);
                frmcash.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width / 3, SystemInformation.WorkingArea.Height- 200);
                frmcash.Location = new System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width - frmcash.Width - 80, 100);

                frmcash.CashPayDataReceiveHandle += FormCash_DataReceiveHandle;
                frmcash.Opacity = 0.95d;
                //frmcash.TopMost = true;
                frmcash.ShowDialog();

                Application.DoEvents();


            }
            catch (Exception ex)
            {
                ShowLog("现金收银异常：" + ex.Message, true);
            }

        }

        //0 需要微信支付宝继续支付  2、现金支付完成
        private void FormCash_DataReceiveHandle(int type, string orderid)
        {
            //返回收银方式按钮 关闭现金收银页面
            if (type == 0)
            {
                this.Invoke(new InvokeHandler(delegate()
                {
                    frmOnLinePayResult frmonlinepayresult = new frmOnLinePayResult(orderid);

                    frmonlinepayresult.frmOnLinePayResult_SizeChanged(null, null);
                    frmonlinepayresult.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width / 3, this.Height - 200);
                    frmonlinepayresult.Location = new System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width - frmonlinepayresult.Width - 50, 100);

                    frmonlinepayresult.DataReceiveHandle += FormOnLinePayResult_DataReceiveHandle;
                    frmonlinepayresult.ShowDialog();
                    frmonlinepayresult.DataReceiveHandle -= FormOnLinePayResult_DataReceiveHandle;
                    frmonlinepayresult = null;

                    // ClearForm();
                }));
            }
            else if (type == 1)
            {
                this.Invoke(new InvokeHandler(delegate()
                {
                    frmCashierResult frmresult = new frmCashierResult(orderid);
                    frmresult.DataReceiveHandle += FormCashierResult_DataReceiveHandle;
                    frmresult.ShowDialog();

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }));
            }
            
        }



        private void FormOnLinePayResult_DataReceiveHandle(int type, string orderid)
        {

            if (type == 1) //交易完成
            {
                this.Invoke(new InvokeHandler(delegate()
                {

                    frmCashierResult frmresult = new frmCashierResult(orderid);
                    frmresult.DataReceiveHandle += FormCashierResult_DataReceiveHandle;
                    frmresult.ShowDialog();

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }));

            }
            else if (type == 2) //交易失败
            {

            }

        }


        private void btnPayOnLine_Click_1(object sender, EventArgs e)
        {

            if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
            {
                string ErrorMsgCart = "";

                Cart cart = httputil.RefreshCart(CurrentCart, ref ErrorMsgCart);
                if (ErrorMsgCart != "" || cart == null) //商品不存在或异常
                {
                    ShowLog(ErrorMsgCart, false);
                }
                else
                {
                    CurrentCart = cart;
                    string ErrorMsg = "";
                    CreateOrderResult orderresult = httputil.CreateOrder(CurrentCart, ref ErrorMsg);
                    if (orderresult == null)
                    {
                        ShowLog("异常" + ErrorMsg, true);
                    }
                    else if (orderresult.continuepay == 1)
                    {
                        frmOnLinePayResult frmonlinepayresult = new frmOnLinePayResult(orderresult.orderid);

                        frmonlinepayresult.frmOnLinePayResult_SizeChanged(null, null);
                        frmonlinepayresult.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width / 3, this.Height - 200);
                        //frmonlinepayresult.TopMost = true;
                        frmonlinepayresult.DataReceiveHandle += FormOnLinePayResult_DataReceiveHandle;
                        frmonlinepayresult.ShowDialog();
                        frmonlinepayresult.DataReceiveHandle -= FormOnLinePayResult_DataReceiveHandle;
                        frmonlinepayresult = null;
                    }
                }
            }
        }

        private void btnPayByBalance_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                {
                    string ErrorMsgCart = "";

                    Cart cart = httputil.RefreshCart(CurrentCart, ref ErrorMsgCart);
                    if (ErrorMsgCart != "" || cart == null) //商品不存在或异常
                    {
                        ShowLog(ErrorMsgCart, false);
                    }
                    else
                    {
                        CurrentCart = cart;
                        string ErrorMsg = "";
                        CreateOrderResult orderresult = httputil.CreateOrder(CurrentCart, ref ErrorMsg);
                        if (orderresult == null)
                        {
                            ShowLog("异常" + ErrorMsg, true);
                        }
                        else if (orderresult.continuepay == 1)
                        {
                            frmBalancePayResult frmbalancepayresult = new frmBalancePayResult(orderresult.orderid);

                            frmbalancepayresult.frmOnLinePayResult_SizeChanged(null, null);
                            frmbalancepayresult.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width / 3, SystemInformation.WorkingArea.Height / 2);
                            //frmonlinepayresult.Location = new System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width - frmbalancepayresult.Width - 50, 100);
                            //frmbalancepayresult.TopMost = true;
                            frmbalancepayresult.DataReceiveHandle += FormOnLinePayResult_DataReceiveHandle;
                            frmbalancepayresult.ShowDialog();
                        }
                    }
                }

                this.Enabled = true;
            }
            catch (Exception ex)
            {
                this.Enabled = true;
                ShowLog("在线收银异常" + ex.Message, true);
            }
        }


        private void btnPayOK_Click(object sender, EventArgs e)
        {
            string ErrorMsg = "";
            CreateOrderResult orderresult = httputil.CreateOrder(CurrentCart, ref ErrorMsg);
            if (orderresult == null)
            {
                ShowLog("异常" + ErrorMsg, true);
            }
            else if (orderresult.continuepay == 1)
            {
                //TODO  继续支付
                ShowLog("需要继续支付", true);
            }
            else
            {

                frmCashierResult frmresult = new frmCashierResult(orderresult.orderid);
                frmresult.DataReceiveHandle += FormCashierResult_DataReceiveHandle;
                frmresult.ShowDialog();

                this.DialogResult = DialogResult.OK;

                this.Close();

            }
        }


        private void FormCashierResult_DataReceiveHandle(int type, string payinfo)
        {
            if (type == 0)
            {
                this.Invoke(new InvokeHandler(delegate()
                {

                }));
            }
            else if (type == 1)
            {
                this.Invoke(new InvokeHandler(delegate()
                {
                    MainModel.frmmainmedia.ShowPayResult(payinfo);
                }));

            }

        }



        private bool RefreshCart()
        {
            try
            {
                string ErrorMsgCart = "";

                if (CurrentCart != null)
                {

                    Cart cart = httputil.RefreshCart(CurrentCart, ref ErrorMsgCart);
                    CurrentCart = cart;
                    if (ErrorMsgCart != "" || cart == null) //商品不存在或异常
                    {
                        ShowLog(ErrorMsgCart, false);
                        return false;
                    }
                    else
                    {
                        lblPrice.Text = "￥" + CurrentCart.producttotalamt;
                        lblCash.Text = "￥" + CurrentCart.cashcouponamt;
                        lblNeedCash.Text ="￥" + CurrentCart.totalpayment;

                        if (CurrentCart.totalpayment == 0)
                        {
                            tabPay.SelectedIndex = 1;
                        }
                        else
                        {
                            tabPay.SelectedIndex = 0;
                        }

                        btnPayByBalance.Enabled = CurrentCart.paymenttypes.balancepayenabled == 1;
                        btnPayByCash.Enabled = CurrentCart.paymenttypes.cashenabled == 1;
                        btnPayOnLine.Enabled = CurrentCart.paymenttypes.onlineenabled == 1;

                        return true;
                    }

                }
                else
                {
                    return true;
                }


            }
            catch (Exception ex)
            {
                ShowLog("刷新购物车异常：" + ex.Message, true);
                return false;
            }
        }

    }
}
