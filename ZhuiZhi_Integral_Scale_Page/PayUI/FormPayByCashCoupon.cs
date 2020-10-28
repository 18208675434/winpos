using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.PayUI;

namespace ZhuiZhi_Integral_Scale_UncleFruit
{
    public partial class FormPayByCashCoupon : Form
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

        public Cart CurrentCart = new Cart();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">0:支付完成   1：在线收银继续支付  2.余额收银 3：取消  12004：会员登录失效   100031：店员登录失效</param>
        /// <param name="orderid"></param>
        public delegate void DataRecHandleDelegate(int type, string orderid);
        /// <summary>
        /// 数据接收事件
        /// </summary>
        public event DataRecHandleDelegate DataReceiveHandle;

        public int ErrorCode = -1;

        public string SuccessOrderID = "";

        private decimal CartTotalPayment = 0;

        public FormPayByCashCoupon()
        {
            InitializeComponent();
            //使用委托的话frmmain界面会卡死
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        public void UpInfo(Cart cart, List<string> lstcash)
        {
            try
            {
                try
                {
                    CartTotalPayment = cart.totalpayment;
                    lblPrice.Text = "￥" + cart.totalpayment.ToString("f2");
                }
                catch { }
                CurrentCart = (Cart)cart.qianClone();
               

                int topsize = Convert.ToInt16(btnPayByCash.Height * (MainModel.hScale - 1) / 3 / MainModel.hScale);

                btnPayByCash.Padding = new System.Windows.Forms.Padding(0, topsize, 0, topsize);
                btnPayByBalance.Padding = new System.Windows.Forms.Padding(0, topsize, 0, topsize);
                btnPayOnLine.Padding = new System.Windows.Forms.Padding(0, topsize, 0, topsize);

                pnlCashCoupons.Controls.Clear();

                CurrentCart = (Cart)cart.qianClone();

                ErrorCode = -1;
                SuccessOrderID = "";
                // CartTotalPayment = 0;

                try
                {
                    CartTotalPayment = cart.totalpayment;
                    lblPrice.Text = "￥" + cart.totalpayment.ToString("f2");
                }
                catch { }

                Application.DoEvents();
                LoadCashCoupon(lstcash);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("刷新现金券窗体异常" + ex.Message);
            }
        }

        private void frmCashCoupon_Shown(object sender, EventArgs e)
        {
            
        }

        public CashCouponType cashcoupontype = CashCouponType.None;
        private void CheckUserAndMember(int resultcode, string ErrorMsg)
        {
            try
            {

                if (resultcode == MainModel.HttpUserExpired)
                {
                    this.Enabled = false;
                    MainModel.CurrentMember = null;

                    cashcoupontype = CashCouponType.UserExpired;

                    if (DataReceiveHandle != null)
                        this.DataReceiveHandle.BeginInvoke(0, "", null, null);

                   this.Close();

                }
                else if (resultcode == MainModel.HttpMemberExpired)
                {
                    this.Enabled = false;
                    MainModel.CurrentMember = null;

                    cashcoupontype = CashCouponType.MemberExpired;
                    if (DataReceiveHandle != null)
                        this.DataReceiveHandle.BeginInvoke(0, "", null, null);
                   this.Close();

                }
                else if (resultcode == MainModel.DifferentMember)   //不是同一个会员 只提示不退出
                {
                    MainModel.ShowLog("非当前登录用户的付款码，请确认后重新支付", true);
                }
                else
                {
                    MainModel.ShowLog(ErrorMsg, false);
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;

                MainModel.ShowLog("面板商品验证用户/会员异常", true);
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

        public void LoadCashCoupon(List<string> lstCashCoupons)
        {
            try
            {
                //string ErrorMsg = "";
                //List<string> lstCashCoupons = httputil.GetAvailableCashCoupons(ref ErrorMsg);

                //if (!string.IsNullOrEmpty(ErrorMsg))
                //{
                //    MainModel.ShowLog(ErrorMsg, false);
                //}
                //加载之前先清空，页面只隐藏 不释放资源
                pnlCashCoupons.Controls.Clear();
                foreach (string casncoupon in lstCashCoupons)
                {
                    AddCashButton(casncoupon);
                }

                if (pnlCashCoupons.Controls.Count > 0)
                {
                    Button btn = (Button)pnlCashCoupons.Controls[0];
                    if (btn.Enabled == true)
                    {
                        btnCash_Click(btn, new EventArgs());
                    }

                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载现金券金额异常"+ex.Message,true);
            }
        }


        private void btnCash_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            decimal casncoupon = Convert.ToDecimal(btn.Tag.ToString());

            if (casncoupon > CartTotalPayment)
            {
                MainModel.ShowLog("代金券金额高于订单金额，无法选择。", false);
            }

            CurrentCart.cashcouponamt = casncoupon;
            CurrentCart.totalpayment=CartTotalPayment-casncoupon;
            //RefreshCart();
            UpdateBtnStatus();

            foreach (Control con in pnlCashCoupons.Controls)
            {
                Button btntemp = (Button)con;
                if (Convert.ToDecimal(btntemp.Tag.ToString()) <= CartTotalPayment)
                {
                    btntemp.BackColor = Color.White;
                }
                else
                {
                    btntemp.BackColor = Color.Silver;

                }
                btntemp.ForeColor = Color.Black;
            }

            btn.BackColor = Color.SkyBlue;
            btn.ForeColor = Color.White;
        }

        bool isfirst = true;
        private void AddCashButton(string casnNum)
        {
            try
            {
                int count = pnlCashCoupons.Controls.Count;
                Button btntemp = new Button();
                btntemp.Font = new System.Drawing.Font("微软雅黑", 15.75F * (Math.Min(MainModel.hScale, MainModel.wScale)), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                //btntemp.Location = new System.Drawing.Point(23, 15);
                btntemp.Name = "button1";

                btntemp.Size = new System.Drawing.Size((pnlCashCoupons.Width - 40) / 2, (pnlCashCoupons.Height-30)/2);
                btntemp.TabIndex = 0;
                btntemp.Text = casnNum;
                btntemp.UseVisualStyleBackColor = true;
                btntemp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                btntemp.FlatAppearance.BorderSize = 1;
                btntemp.FlatAppearance.BorderColor = Color.DarkGray;
                btntemp.BackColor = Color.Silver;
                if (Convert.ToDecimal(casnNum) <= CartTotalPayment)
                {
                    btntemp.BackColor = Color.White;
                }
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
            catch (Exception ex)
            {
                MainModel.ShowLog("初始化现金券异常"+ex.Message,true);
            }
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            if (DataReceiveHandle != null)
                this.DataReceiveHandle.BeginInvoke(0, "", null, null);
           this.Close();
        }

        private void btnPayByCash_Click(object sender, EventArgs e)
        {
      
            if (btnPayByCash.Tag == null || btnPayByCash.Tag.ToString() != "1")
            {
                return;
            }

            try
            {
                CurrentCart.cashpayoption = 1;
                if (!RefreshCart())
                {
                    return;
                }

                string orderid = "";
                int type = PayHelper.ShowFormPayByCash(CurrentCart, out orderid);


                if (type == 2)
                {
                   
                        if (PayHelper.ShowFormPayByOnLine(orderid, CurrentCart))
                        {

                           // PayHelper.ShowFormPaySuccess(orderid);
                            SuccessOrderID = orderid;
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                            return;
                        }
                        else
                        {
                            RefreshCart();
                        }

                }
                else if (type == 1)
                {
                   
                       // PayHelper.ShowFormPaySuccess(orderid);
                        SuccessOrderID = orderid;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        return;
                  
                }
                else if (type == MainModel.HttpUserExpired || type == MainModel.HttpMemberExpired)
                {
                    this.Close();
                    return;
                }

                if (CurrentCart != null)
                {
                    CurrentCart.cashpayoption = 0;
                    CurrentCart.cashpayamt = 0;
                }


                RefreshCart();

                Application.DoEvents();


            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MainModel.ShowLog("现金券>现金收银异常：" + ex.Message, true);
            }

        }

        private void FormCashPay_DataReceiveHandle(int type, string orderid,Cart cart)
        {
            try
            {
                if (type == 0)
                {
                   
                        this.Enabled = false;
                        FormPayByOnLine frmonlinepayresult = new FormPayByOnLine(orderid,cart);
                        frmonlinepayresult.TopMost = true;
                        frmonlinepayresult.frmOnLinePayResult_SizeChanged(null, null);
                        frmonlinepayresult.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmonlinepayresult.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmonlinepayresult.Height) / 2);

                        frmonlinepayresult.ShowDialog();


                        if (frmonlinepayresult.DialogResult == DialogResult.OK)
                        {
                            SuccessOrderID = orderid;
                            this.DialogResult = DialogResult.OK;

                            if (DataReceiveHandle != null)
                                this.DataReceiveHandle.BeginInvoke(1, SuccessOrderID, null, null);
                          this.Close();
                        }
                        else if (frmonlinepayresult.DialogResult == DialogResult.None)
                        {
                            this.DialogResult = DialogResult.Cancel;
                            if (DataReceiveHandle != null)
                                this.DataReceiveHandle.BeginInvoke(0, "", null, null);
                            this.Close();
                        }
                        // ClearForm();
                        this.Enabled = true;
                  
                }
                else if (type == 1)
                {
                    SuccessOrderID = orderid;
                    this.DialogResult = DialogResult.OK;
                    if (DataReceiveHandle != null)
                        this.DataReceiveHandle.BeginInvoke(1, SuccessOrderID, null, null);
                   this.Close();
                }
                else if (type == MainModel.HttpUserExpired || type == MainModel.HttpMemberExpired)
                {
                    CheckUserAndMember(type, "");
                }

            }
            catch (Exception ex)
            {
                this.DialogResult = DialogResult.Cancel;
                if (DataReceiveHandle != null)
                    this.DataReceiveHandle.BeginInvoke(0, "", null, null);
                LogManager.WriteLog("ERROR", "处理确认窗体结果异常" + ex.Message);
               this.Close();
            }

        }




        private void btnPayOnLine_Click_1(object sender, EventArgs e)
        {

            try
            {
                if (btnPayOnLine.Tag == null || btnPayOnLine.Tag.ToString() != "1")
                {
                    return;
                }

                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                {
                    this.Enabled = false;
                    LoadingHelper.ShowLoadingScreen("加载中...");
                    string ErrorMsgCart = "";
                    int ResultCode = 0;
                    Cart cart = httputil.RefreshCart(CurrentCart, ref ErrorMsgCart, ref ResultCode);

                    if (ErrorMsgCart != "" || cart == null) //商品不存在或异常
                    {

                        CheckUserAndMember(ResultCode, ErrorMsgCart);
                        LoadingHelper.CloseForm();
                        this.Enabled = true;
                        //ShowLog(ErrorMsgCart, false);
                    }
                    else
                    {
                        CurrentCart = cart;

                        this.Enabled = false;
                        LoadingHelper.ShowLoadingScreen();
                        string ErrorMsg = "";
                        ResultCode = -1;
                        CreateOrderResult orderresult = httputil.CreateOrder(CurrentCart, ref ErrorMsg, ref ResultCode);
                        if (ResultCode != 0 || orderresult == null)
                        {
                            CheckUserAndMember(ResultCode,ErrorMsg);
                           

                        }
                        else if (orderresult.continuepay == 1)
                        {

                            if (PayHelper.ShowFormPayByOnLine(orderresult.orderid, CurrentCart))
                            {

                                //PayHelper.ShowFormPaySuccess(orderresult.orderid);
                                SuccessOrderID = orderresult.orderid;
                                this.DialogResult = DialogResult.OK;
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

                    this.Enabled = true;
                }

                //btnmianban.focus();
                Application.DoEvents();

            }
            catch (Exception ex)
            {
                this.Enabled = true;
               MainModel.ShowLog("在线收银异常" + ex.Message, true);
            }

        }

        private void btnPayByBalance_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnPayByBalance.Tag == null || btnPayByBalance.Tag.ToString() != "1")
                {
                    return;
                }

                Cart outcart = new Cart();
                string successorderid = "";
                int resultcode = PayHelper.ShowFormPayByBalance(CurrentCart, out successorderid,out outcart);

                if (outcart != null)
                {
                    CurrentCart = outcart;
                }

                if (CheckUserAndMember(resultcode))
                {
                    return;
                }

                if (resultcode == 1) //订单完成
                {
                   // PayHelper.ShowFormPaySuccess(successorderid);
                    SuccessOrderID = successorderid;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    return;
                }
                else if (resultcode == 2) //需要继续支付  微信/支付宝
                {
                    string returnorderid = "";
                    int returnresultcode = PayHelper.ShowFormPayBalanceToMix(CurrentCart, out returnorderid);

                    if (returnresultcode == 2)
                    {
                       
                            if (PayHelper.ShowFormPayByOnLine(returnorderid, CurrentCart))
                            {
                                SuccessOrderID = returnorderid;
                                //PayHelper.ShowFormPaySuccess(returnorderid);
                                this.DialogResult = DialogResult.OK;
                                this.Close();
                                return;
                            }
                            else
                            {
                                RefreshCart();
                            }
                       
                    }
                    else if (returnresultcode == 1)
                    {

                       
                            SuccessOrderID = returnorderid;
                           // PayHelper.ShowFormPaySuccess(returnorderid);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                            return;
                      
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
                this.Enabled = true;
                MainModel.ShowLog("现金券>余额支付异常：" + ex.Message, true);
            }
        }

        private void btnPayOK_Click(object sender, EventArgs e)
        {
            string ErrorMsg = "";
            int ResultCode = 0;
            CreateOrderResult orderresult = httputil.CreateOrder(CurrentCart, ref ErrorMsg, ref ResultCode);
            if (ResultCode != 0 || orderresult == null)
            {
                CheckUserAndMember(ResultCode,ErrorMsg);
                //MainModel.ShowLog("异常" + ErrorMsg, true);
            }
            else if (orderresult.continuepay == 1)
            {
                //TODO  继续支付
                MainModel.ShowLog("需要继续支付", true);
            }
            else
            {

                SuccessOrderID = orderresult.orderid;
                this.DialogResult = DialogResult.OK;
                if (DataReceiveHandle != null)
                    this.DataReceiveHandle.BeginInvoke(1, SuccessOrderID, null, null);
               this.Close();

            }
        }




        private bool RefreshCart()
        {
            try
            {
                string ErrorMsgCart = "";
                int ResultCode = 0;

                if (CurrentCart != null)
                {

                    Cart cart = httputil.RefreshCart(CurrentCart, ref ErrorMsgCart,ref ResultCode);
                    CurrentCart = cart;
                    if (ErrorMsgCart != "" || cart == null) //商品不存在或异常
                    {
                        CheckUserAndMember(ResultCode, ErrorMsgCart);
                        //MainModel.ShowLog(ErrorMsgCart, false);
                        return false;
                    }
                    else
                    {
                        CurrentCart = cart;
                        //lblPrice.Text = "￥" + CurrentCart.producttotalamt.ToString("f2");
                        lblCash.Text = "￥" + CurrentCart.cashcouponamt.ToString("f2");
                        lblNeedCash.Text = "￥" + CurrentCart.totalpayment.ToString("f2");

                        if (CurrentCart.totalpayment == 0)
                        {
                            lblNext.Visible = false;
                            btnPayOK.Visible = true;
                            btnPayByCash.Visible = false;
                            btnPayByBalance.Visible = false;
                            btnPayOnLine.Visible = false;
                        }
                        else
                        {
                            lblNext.Visible = true;
                            btnPayOK.Visible = false;
                            btnPayByCash.Visible = true;
                            btnPayByBalance.Visible = true;
                            btnPayOnLine.Visible = true;
                        }

                        //tag=1 允许访问 0 或其他不允许   改变enabled背景色丑
                        if (CurrentCart.paymenttypes.balancepayenabled == 1)
                        {
                            btnPayByBalance.Tag = 1;
                            btnPayByBalance.BackColor = Color.DarkTurquoise;
                        }
                        else
                        {
                            btnPayByBalance.Tag = 0;
                            btnPayByBalance.BackColor = Color.Silver;
                        }

                        if (CurrentCart.paymenttypes.cashenabled == 1)
                        {
                            btnPayByCash.Tag = 1;
                            btnPayByCash.BackColor = Color.DarkOrange;
                        }
                        else
                        {
                            btnPayByCash.Tag = 0;
                            btnPayByCash.BackColor = Color.Silver;
                        }

                        if (CurrentCart.paymenttypes.onlineenabled == 1)
                        {
                            btnPayOnLine.Tag = 1;
                            btnPayOnLine.BackColor = Color.Tomato;
                        }
                        else
                        {
                            btnPayOnLine.Tag = 0;
                            btnPayOnLine.BackColor = Color.Silver;
                        }

                        ZhuiZhi_Integral_Scale_UncleFruit.BaseUI.BaseUIHelper.UpdaForm(CurrentCart);


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
                MainModel.ShowLog("刷新购物车异常：" + ex.Message, true);
                return false;
            }
        }




     

        private void UpdateBtnStatus()
        {
            try
            {
                lblCash.Text = "￥" +  CurrentCart.cashcouponamt.ToString("f2");
                lblNeedCash.Text = "￥" + CurrentCart.totalpayment.ToString("f2");

                if (CurrentCart.totalpayment == 0)
                {
                    lblNext.Visible = false;
                    btnPayOK.Visible = true;
                    btnPayByCash.Visible = false;
                    btnPayByBalance.Visible = false;
                    btnPayOnLine.Visible = false;
                }
                else
                {
                    lblNext.Visible = true;
                    btnPayOK.Visible = false;
                    btnPayByCash.Visible = true;
                    btnPayByBalance.Visible = true;
                    btnPayOnLine.Visible = true;
                }

                //tag=1 允许访问 0 或其他不允许   改变enabled背景色丑
                if (CurrentCart.paymenttypes.balancepayenabled == 1)
                {
                    btnPayByBalance.Tag = 1;
                    btnPayByBalance.BackColor = Color.DarkTurquoise;
                }
                else
                {
                    if (CurrentCart.availablebalanceamount >= CartTotalPayment)
                    {
                        btnPayByBalance.Tag = 1;
                        btnPayByBalance.BackColor = Color.DarkTurquoise;
                    }
                    else
                    {
                        btnPayByBalance.Tag = 0;
                        btnPayByBalance.BackColor = Color.Silver;
                    }
                   
                }

                if (CurrentCart.paymenttypes.cashenabled == 1)
                {
                    btnPayByCash.Tag = 1;
                    btnPayByCash.BackColor = Color.DarkOrange;
                }
                else
                {
                    btnPayByCash.Tag = 0;
                    btnPayByCash.BackColor = Color.Silver;
                }

                if (CurrentCart.paymenttypes.onlineenabled == 1)
                {
                    btnPayOnLine.Tag = 1;
                    btnPayOnLine.BackColor = Color.Tomato;
                }
                else
                {
                    btnPayOnLine.Tag = 0;
                    btnPayOnLine.BackColor = Color.Silver;
                }


                //MainModel.frmMainmediaCart = CurrentCart;
                //MainModel.frmmainmedia.UpdateForm();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("更新代金券页面按钮状态异常"+ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddCashButton("11");
        }

    }

    public enum CashCouponType
    {
        /// <summary>
        /// 初始
        /// </summary>
        None, 
        /// <summary>
        /// 现金继续支付
        /// </summary>
        Cash,
        /// <summary>
        /// 在线继续支付
        /// </summary>
        Online,
        /// <summary>
        /// 余额继续支付
        /// </summary>
        Balance,
        /// <summary>
        /// 用户过期
        /// </summary>
        UserExpired,
        /// <summary>
        /// 会员登录过期
        /// </summary>
        MemberExpired,
        /// <summary>
        /// 会员不是同一用户
        /// </summary>
        DifferentMember
    }
}
