using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinSaasPOS_Scale.Common;
using WinSaasPOS_Scale.Model;

namespace WinSaasPOS_Scale.PayUI
{
    public partial class FormPayByCash : Form
    {

        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();

        /// <summary>
        /// 接口访问类
        /// </summary>
        private HttpUtil httputil = new HttpUtil();

        /// <summary>
        /// 收银主界面传过来的 抹零后的cartModel
        /// </summary>
        public Cart thisCurrentCart = new Cart();

        /// <summary>
        /// 界面初始化录入默认值   修改的话自动清空
        /// </summary>
        bool isfirst = true;

        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();

        /// <summary>
        /// 页面返回类型结果  -1:初始值  0：取消 1：支付完成  2：需要继续支付    errorcode ：异常代码
        /// </summary>
        public int ReturnResultCode = -1;

        /// <summary>
        /// 页面返回订单
        /// </summary>
        public string ReruntOrderId = "";

        public FormPayByCash()
        {
            InitializeComponent();
        }

        public void UpInfo(Cart cart)
        {
            try
            {
                isfirst = true;

                int cashpaytype = 0;

                string cashpayorderid = "";

                thisCurrentCart = cart;
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
                    ReturnResultCode = resultcode;
                    ReruntOrderId = "";
                    this.Close();
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
                ReturnResultCode = 0;
                this.Close();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("关闭现金支付窗体异常" + ex.Message);
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

                ReceiptUtil.EditOpenMoneyPacketCount(1);

                decimal cash = Convert.ToDecimal(txtCash.Text);

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
                            MainModel.ShowLog("异常" + ErrorMsg, true);
                        }
                        else if (orderresult.continuepay == 1)
                        {
                            //TODO  继续支付
                            MainModel.ShowLog("需要继续支付", true);
                        }
                        else
                        {
                            ReturnResultCode = 1;
                            ReruntOrderId = orderresult.orderid;
                            this.Close();

                        }
                    }
                    else//进入找零界面
                    {

                        if (PayHelper.ShowFormPayCashToChange(thisCurrentCart))
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
                                //TODO  继续支付
                                MainModel.ShowLog("需要继续支付", true);
                            }
                            else
                            {
                                ReturnResultCode = 1;
                                ReruntOrderId = orderresult.orderid;
                                this.Close();
                            }
                        }
                        else
                        {
                            RefreshCart(0, true);
                            //找零页面返回  不做处理 
                            WinSaasPOS_Scale.BaseUI.BaseUIHelper.UpdaForm(thisCurrentCart);

                        }
                       
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
                                MainModel.ShowLog("异常" + ErrorMsg, true);
                            }
                            else if (orderresult.continuepay == 1)
                            {
                                ReturnResultCode = 2;
                                ReruntOrderId = orderresult.orderid;
                               this.Close();
                                Application.DoEvents();
                            }

                        }
                        else
                        {
                            RefreshCart(0, true);
                         
                            WinSaasPOS_Scale.BaseUI.BaseUIHelper.UpdaForm(thisCurrentCart);
                        }
                    }
                    else
                    {

                        if (PayHelper.ShowFormPayCashToOnLine(thisCurrentCart))
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
                                MainModel.ShowLog("异常" + ErrorMsg, true);
                            }
                            else if (orderresult.continuepay == 1)
                            {
                                ReturnResultCode = 2;
                                ReruntOrderId = orderresult.orderid;
                                this.Close();
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
                MainModel.ShowLog("现金支付异常：" + ex.StackTrace, false);
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
                    MainModel.ShowLog(ErrorMsgCart, false);
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

                    //Dictionary<string, string> dicdetail = new Dictionary<string, string>();
                    //dicdetail.Add("应付：", "￥" + thisCurrentCart.payamtbeforecash.ToString("f2"));
                    //dicdetail.Add("已付现金：", "￥" + thisCurrentCart.cashpayamt.ToString("f2"));

                    //BaseUIHelper.UpdateDgvOrderDetail(dicdetail, "还需支付：", "￥" + thisCurrentCart.totalpayment.ToString("f2"));

                    //MainModel.frmMainmediaCart = thisCurrentCart;
                    //MainModel.frmmainmedia.UpdateForm();

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
