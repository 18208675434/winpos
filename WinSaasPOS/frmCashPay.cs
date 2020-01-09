using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WinSaasPOS.Common;
using WinSaasPOS.Model;

namespace WinSaasPOS
{
    public partial class frmCashPay : Form
    {

        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">0:支付完成   1：在线收银继续支付 3：取消  12004：会员登录失效   100031：店员登录失效</param>
        /// <param name="orderid"></param>
        public delegate void DataRecHandleDelegate(int type, string orderid);
        /// <summary>
        /// 数据接收事件
        /// </summary>
        public event DataRecHandleDelegate DataReceiveHandle;

        /// <summary>
        /// 接口访问类
        /// </summary>
        private HttpUtil httputil = new HttpUtil();

        /// <summary>
        /// 收银主界面传过来的 抹零后的cartModel
        /// </summary>
        private Cart thisCurrentCart = new Cart();


        /// <summary>
        /// 界面初始化录入默认值   修改的话自动清空
        /// </summary>
        bool isfirst = true;

        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();

        /// <summary>
        /// 页面返回类型结果
        /// </summary>
        public int cashpaytype = 0;

        /// <summary>
        /// 页面返回订单
        /// </summary>
        public string cashpayorderid = "";
        public frmCashPay()
        {
            InitializeComponent();

        }

        public frmCashPay(Cart cart)
        {
            InitializeComponent();

            thisCurrentCart = (Cart)cart.qianClone();
            btnNext.Focus();
        }



        private void frmCashPay_Load(object sender, EventArgs e)
        {
            try
            {
                txtCash.Text = thisCurrentCart.totalpayment.ToString("f2");
                btnNext.Focus();

                lblPrice.Text = "￥" + thisCurrentCart.totalpayment.ToString("f2");

                txtCash.TextChanged += txtNum_TextChanged;
                lblShuiyin.Click += lblShuiyin_Click;
                lbtnCancle.Click += btnCancle_Click;
                btnDot.Click += btnDot_Click;
                btnDel.Click += btnDel_Click;
                btnNext.Click += btnNext_Click;
                btn0.Click += btn_Click;
                btn1.Click += btn_Click;
                btn2.Click += btn_Click;
                btn3.Click += btn_Click;
                btn4.Click += btn_Click;
                btn5.Click += btn_Click;
                btn6.Click += btn_Click;
                btn7.Click += btn_Click;
                btn8.Click += btn_Click;
                btn9.Click += btn_Click;
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("现金页面初始化异常" + ex.Message, true);
            }
        }

        private void frmCash_Shown(object sender, EventArgs e)
        {


        }


        public void UpInfo(Cart cart)
        {
            try
            {

                isfirst = true;



                int cashpaytype = 0;


                string cashpayorderid = "";

                thisCurrentCart = (Cart)cart.qianClone();
                txtCash.Text = thisCurrentCart.totalpayment.ToString("f2");
                btnNext.Focus();

                lblPrice.Text = "￥" + thisCurrentCart.totalpayment.ToString("f2");

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("刷新数字窗体异常" + ex.Message);
            }
        }

        private void CheckUserAndMember(int resultcode)
        {
            try
            {
                if (resultcode == MainModel.HttpMemberExpired || resultcode == MainModel.HttpUserExpired)
                {
                    if (DataReceiveHandle != null)
                        this.DataReceiveHandle.BeginInvoke(resultcode, "", null, null);
                    //this.DialogResult = DialogResult.OK;
                    cashpaytype = resultcode;
                    cashpayorderid = "";
                    this.Hide();    //this.close();
                }
            }
            catch (Exception ex)
            {
                //ShowLog("验证用户/会员异常", true);
            }

        }


        private void btnCancle_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataReceiveHandle != null)
                    this.DataReceiveHandle.BeginInvoke(3, "", null, null);

                //this.DialogResult = DialogResult.OK;
                cashpaytype = 3;
                cashpayorderid = "";
                this.Hide();    //this.close();
            }
            catch (Exception ex)
            {

            }
        }

        //下一步需要判断实收现金是否足够 通过cart接口返回值判断
        private void btnNext_Click(object sender, EventArgs e)
        {

            try
            {
                try
                {
                    double doublenum = Convert.ToDouble(txtCash.Text);

                    if (doublenum <= 0 && thisCurrentCart.payamtbeforecash > 0)
                    {
                        return;
                    }
                }
                catch
                {
                    return;
                }

                //Thread threadItemExedate = new Thread(PrintUtil.OpenCashBox);
                //threadItemExedate.IsBackground = true;
                //threadItemExedate.Start();


                ReceiptUtil.EditOpenMoneyPacketCount(1);

                decimal cash = Convert.ToDecimal(txtCash.Text);
                //string ErrorMsgCart = "";
                //int ResultCode = 0;
                //thisCurrentCart.cashpayoption = 1;
                //thisCurrentCart.cashpayamt = cash;

                //Cart cart = httputil.RefreshCart(thisCurrentCart,ref ErrorMsgCart,ref ResultCode);
                if (!RefreshCart(cash, false))
                {
                    return;
                }


                if (thisCurrentCart.totalpayment == 0)
                {
                    if (thisCurrentCart.cashchangeamt == 0) //找零为0 创建订单反馈完成后结束
                    {
                        string ErrorMsg = "";
                        int ResultCode = 0;
                        CreateOrderResult orderresult = httputil.CreateOrder(thisCurrentCart, ref ErrorMsg, ref ResultCode);
                        if (ResultCode != 0 || orderresult == null)
                        {
                            CheckUserAndMember(ResultCode);
                            ShowLog("异常" + ErrorMsg, true);
                        }
                        else if (orderresult.continuepay == 1)
                        {
                            //TODO  继续支付
                            ShowLog("需要继续支付", true);
                        }
                        else
                        {

                            if (DataReceiveHandle != null)
                                this.DataReceiveHandle.BeginInvoke(1, orderresult.orderid, null, null);
                            this.DialogResult = DialogResult.OK;
                            cashpaytype = 1;
                            cashpayorderid = orderresult.orderid;
                            this.Hide();    //this.close();

                        }
                    }
                    else//进入找零界面
                    {
                        //MainModel.

                        MainModel.frmMainmediaCart = thisCurrentCart;
                        MainModel.frmmainmedia.UpdateForm();

                        frmCashChange frmcashchange = new frmCashChange(thisCurrentCart);

                        frmcashchange.frmCashChange_SizeChanged(null, null);
                        asf.AutoScaleControlTest(frmcashchange, 380, 540, this.Width, this.Height, true);
                        frmcashchange.Location = this.Location;
                        frmcashchange.DataReceiveHandle += FormCashChange_DataReceiveHandle;
                        frmcashchange.TopMost = true;
                        frmcashchange.ShowDialog();
                        Application.DoEvents();
                    }
                }
                else
                {

                    // string ErrorMsg = "";
                    //// int ResultCode = 0;
                    // CreateOrderResult orderresult = httputil.CreateOrder(cart, ref ErrorMsg, ref ResultCode);
                    // if (ResultCode != 0 || orderresult == null)
                    // {
                    //     CheckUserAndMember(ResultCode);
                    //     ShowLog("异常" + ErrorMsg, true);
                    // }
                    // else
                    // {
                    //通过是否有余额值判断
                    if (thisCurrentCart.balancepayamt != null && thisCurrentCart.balancepayamt > 0)
                    {
                        frmBalanceOnLine frmbalanceonline = new frmBalanceOnLine(thisCurrentCart);
                        //frmonline.Size = new System.Drawing.Size(this.Width, this.Height);
                        asf.AutoScaleControlTest(frmbalanceonline, 380, 540, this.Width, this.Height, true);
                        frmbalanceonline.Location = this.Location;
                        frmbalanceonline.TopMost = true;
                        frmbalanceonline.ShowDialog();

                        if (frmbalanceonline.DialogResult == DialogResult.OK)
                        {
                            string ErrorMsg = "";
                            int ResultCode = 0;
                            int BalanceResultCode = 0;
                            CreateOrderResult orderresult = httputil.CreateOrder(thisCurrentCart, ref ErrorMsg, ref BalanceResultCode);
                            if (ResultCode != 0 || orderresult == null)
                            {
                                CheckUserAndMember(BalanceResultCode);
                                ShowLog("异常" + ErrorMsg, true);
                            }
                            else if (orderresult.continuepay == 1)
                            {

                                if (DataReceiveHandle != null)
                                    this.DataReceiveHandle.BeginInvoke(0, orderresult.orderid, null, null);
                                this.DialogResult = DialogResult.OK;
                                cashpaytype = 0;
                                cashpayorderid = orderresult.orderid;
                                this.Hide();    //this.close();
                                Application.DoEvents();
                            }

                        }
                        else
                        {
                            RefreshCart(0, true);
                        }
                    }
                    else
                    {
                        frmOnLine frmonline = new frmOnLine(thisCurrentCart);
                        //frmonline.Size = new System.Drawing.Size(this.Width, this.Height);
                        asf.AutoScaleControlTest(frmonline, 380, 540, this.Width, this.Height, true);
                        frmonline.Location = this.Location;
                        // frmonline.DataReceiveHandle += FormOnline_DataReceiveHandle;
                        frmonline.TopMost = true;
                        frmonline.ShowDialog();

                        if (frmonline.DialogResult == DialogResult.OK)
                        {
                            this.Hide();
                            string ErrorMsg = "";
                            int ResultCode = 0;
                            int OnlineResultCode = 0;
                            CreateOrderResult orderresult = httputil.CreateOrder(thisCurrentCart, ref ErrorMsg, ref OnlineResultCode);
                            if (ResultCode != 0 || orderresult == null)
                            {
                                this.Show();
                                CheckUserAndMember(OnlineResultCode);
                                ShowLog("异常" + ErrorMsg, true);
                            }
                            else if (orderresult.continuepay == 1)
                            {

                                if (DataReceiveHandle != null)
                                    this.DataReceiveHandle.BeginInvoke(0, orderresult.orderid, null, null);
                                this.DialogResult = DialogResult.OK;
                                cashpaytype = 0;
                                cashpayorderid = orderresult.orderid;
                                this.Hide();    //this.close();
                                //Application.DoEvents();
                            }
                        }
                        else
                        {
                            RefreshCart(0, true);
                        }
                    }

                    Application.DoEvents();
                    // }

                }




            }
            catch (Exception ex)
            {
                ShowLog("现金支付异常：" + ex.Message, false);
            }

            //if (DataReceiveHandle != null)
            //    this.DataReceiveHandle.BeginInvoke(1,Convert.ToDouble(txtCash.Text), null, null);

            //this.Hide();    //this.close();
        }


        private void FormCashChange_DataReceiveHandle(int type)
        {

            if (type == 1)
            {
                string ErrorMsg = "";
                int ResultCode = 0;
                CreateOrderResult orderresult = httputil.CreateOrder(thisCurrentCart, ref ErrorMsg, ref ResultCode);
                if (ResultCode != 0 || orderresult == null)
                {
                    CheckUserAndMember(ResultCode);
                    ShowLog("异常" + ErrorMsg, true);
                }
                else if (orderresult.continuepay == 1)
                {
                    //TODO  继续支付
                    ShowLog("需要继续支付", true);
                }
                else
                {

                    if (DataReceiveHandle != null)
                        this.DataReceiveHandle.BeginInvoke(1, orderresult.orderid, null, null);

                    this.DialogResult = DialogResult.OK;
                    cashpaytype = 1;
                    cashpayorderid = orderresult.orderid;
                    this.Hide();    //this.close();
                }
            }
            else
            {
                RefreshCart(0, true);
                //找零页面返回  不做处理 
            }
        }


        private bool RefreshCart(decimal cash, bool updateCash)
        {
            try
            {
                this.Enabled = false;
                LoadingHelper.ShowLoadingScreen("加载中...");

                string ErrorMsgCart = "";
                int ResultCode = 0;
                thisCurrentCart.cashpayoption = 1;
                thisCurrentCart.cashpayamt = cash;

                Cart cart = httputil.RefreshCart(thisCurrentCart, ref ErrorMsgCart, ref ResultCode);


                if (ErrorMsgCart != "" || cart == null) //商品不存在或异常
                {
                    this.Enabled = true;
                    LoadingHelper.CloseForm();
                    //fd
                    CheckUserAndMember(ResultCode);
                    ShowLog(ErrorMsgCart, false);
                    return false;
                }
                else
                {
                    thisCurrentCart = cart;

                    if (updateCash)
                    {
                        lblPrice.Text = "￥" + thisCurrentCart.totalpayment.ToString("f2");
                        txtCash.Text = thisCurrentCart.totalpayment.ToString("f2");
                        isfirst = true;
                    }



                    MainModel.frmMainmediaCart = thisCurrentCart;
                    MainModel.frmmainmedia.UpdateForm();

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

        //TODO  修改样式
        private void ShowLog(string msg, bool iserror)
        {

            MsgHelper.AutoShowForm(msg);
            //this.Invoke(new InvokeHandler(delegate()
            //{

            //    frmMsg frmmsf = new frmMsg(msg, iserror, 1000);
            //    frmmsf.ShowDialog(); LogManager.WriteLog(msg);
            //}));

        }

        #region  小键盘按键
        private void btn_Click(object sender, EventArgs e)
        {
            if (isfirst)
            {
                txtCash.Clear();
                isfirst = false;
            }
            Button btn = (Button)sender;
            try
            {
                //小数点后允许一位  现金抹零后不允许再输入零
                if (txtCash.Text.Length > 2 && txtCash.Text.Substring(txtCash.Text.Length - 2, 1) == ".")
                {
                    return;
                }
                //限制金额不超过100000

                //第一位是0 后面只能输入.
                if (txtCash.Text == "0")
                {
                    return;
                }

                decimal CheckDecimal = Convert.ToDecimal(txtCash.Text + btn.Name.Replace("btn", ""));

                if (CheckDecimal > 100000)
                {
                    return;
                }
                txtCash.Text += btn.Name.Replace("btn", "");
            }
            catch
            {

            }



        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            if (isfirst)
            {
                txtCash.Clear();
                isfirst = false;
            }


            if (txtCash.Text.Length > 0 && !txtCash.Text.Contains("."))
            {
                txtCash.Text += ".";
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (isfirst)
            {
                txtCash.Clear();
                isfirst = false;
            }


            if (txtCash.Text.Length > 0)
            {
                txtCash.Text = txtCash.Text.Substring(0, txtCash.Text.Length - 1);
            }
        }


        //控制仅允许录入数字
        private void TextNUMBER_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        #endregion


        /// <summary>
        /// 实收金额是否合理
        /// </summary>
        /// <param name="hj">应收现金</param>
        /// <param name="zl">找零</param>
        /// <returns></returns>
        public static bool Check(decimal hj, decimal zl)
        {
            bool ret = true;

            //实收现金
            decimal cash = hj + zl;


            int s100, s50, s20, s10, s5, s1, z100, z50, z20, z10, z5, z1, t;

            if (zl >= 0 && zl < 100)
            {
                //实收中100的数量
                s100 = (int)(cash / 100);
                t = s100 * 100;
                //实收中50的数量
                s50 = (int)((cash - t) / 50);
                t += s50 * 50;
                //实收中20的数量
                s20 = (int)((cash - t) / 20);
                t += s20 * 20;
                //实收中10的数量
                s10 = (int)((cash - t) / 10);
                t += s10 * 10;
                //实收中5的数量
                s5 = (int)((cash - t) / 5);
                t += s5 * 5;
                //实收中1的数量
                s1 = (int)((cash - t) / 1);

                //找零中100的数量
                z100 = (int)(zl / 100);
                t = z100 * 100;
                //实收中50的数量
                z50 = (int)((zl - t) / 50);
                t += z50 * 50;
                //实收中20的数量
                z20 = (int)((zl - t) / 20);
                t += z20 * 20;
                //实收中10的数量
                z10 = (int)((zl - t) / 10);
                t += z10 * 10;
                //实收中5的数量
                z5 = (int)((zl - t) / 5);
                t += z5 * 5;
                //实收中1的数量
                z1 = (int)((zl - t) / 1);

                //实收和找零出现相同的票面就不合理了
                if (z100 > 0)
                    ret = false;
                if (s50 > 0 && z50 > 0)
                    ret = false;
                if (s20 > 0 && z20 > 0)
                    ret = false;
                if (s10 > 0 && z10 > 0)
                    ret = false;
                if (s5 > 0 && z5 > 0)
                    ret = false;
                if (s1 > 0 && z1 > 0)
                    ret = false;
            }
            else
                ret = false;

            return ret;
        }

        public void frmCashPay_SizeChanged(object sender, EventArgs e)
        {
            //asf.ControlAutoSize(this);
        }

        private void frmCashPay_FormClosing(object sender, FormClosingEventArgs e)
        {
            //thisCurrentCart.cashpayamt = 0;
            //thisCurrentCart.cashpayoption = 0;
        }

        //屏蔽回车和空格键
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter || keyData == Keys.Space)
                return false;
            else
                return base.ProcessDialogKey(keyData);
        }

        private void frmCashPay_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)            
            //{               
            //    e.Handled = false;   
            //    //将Handled设置为true，指示已经处理过KeyPress事件                        
            //}

        }

        private void txtNum_TextChanged(object sender, EventArgs e)
        {
            if (txtCash.Text.Length > 0)
            {
                lblShuiyin.Visible = false;
            }
            else
            {
                lblShuiyin.Visible = true;
            }

            try
            {
                double doublenum = Convert.ToDouble(txtCash.Text);

                if (doublenum > 0)
                {
                    btnNext.BackColor = Color.OrangeRed;
                }
                else
                {
                    btnNext.BackColor = Color.Silver;
                }
            }
            catch
            {
                btnNext.BackColor = Color.Silver;
            }
        }

        private void lblShuiyin_Click(object sender, EventArgs e)
        {
            txtCash.Focus();
        }




    }
}
