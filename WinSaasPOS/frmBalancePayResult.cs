using WinSaasPOS.Common;
using WinSaasPOS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WinSaasPOS
{
    public partial class frmBalancePayResult : Form
    {
        #region  成员变量

        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();

        /// <summary>
        /// 接口访问类
        /// </summary>
        HttpUtil httputil = new HttpUtil();

        /// <summary>
        /// 当前订单ID
        /// </summary>
        string CurrentOrderID = "";


        /// <summary>
        /// USB设备监听
        /// </summary>
        private ScanerHook listener = new ScanerHook();


        bool isrun = true;

        public Cart CurrentCart = new Cart();

        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();


        public string OverOrderId = "";


        private bool IsScan = true;


        /// <summary>
        /// 窗体状态   orderid   errocode
        /// </summary>
        /// <param name="type"></param>
        public delegate void DataRecHandleDelegate(DialogResult dialogresult, string orderid,ExpiredType  expiredtype,Cart cart);

        /// <summary>
        /// 数据接收事件
        /// </summary>
        public event DataRecHandleDelegate DataReceiveHandle;


        #endregion


        #region  页面加载与退出
        public frmBalancePayResult(Cart cart)
        {
            InitializeComponent();
            CurrentCart = cart.qianClone();
            listener.ScanerEvent += Listener_ScanerEvent;

            try
            {
               // this.Size = new System.Drawing.Size(Convert.ToInt32(380 * MainModel.wScale), Convert.ToInt32(480 * MainModel.hScale));

                AutoScaleControl();
                this.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - this.Width) / 2, (Screen.AllScreens[0].Bounds.Height - this.Height) / 2);

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("自适应余额收银页面异常" + ex.Message);
            }

            listener.Start();
        }

        public frmBalancePayResult(string orderid)
        {
            InitializeComponent();
            CurrentOrderID = orderid;
            listener.ScanerEvent += Listener_ScanerEvent;

            listener.Start();
        }



        private void frmOnLinePayResult_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                isrun = false;
                // listener.
                listener.Stop();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ERROR", "余额收银页面关闭异常" + ex.Message);
            }
        }

        private void frmBalancePayResult_Shown(object sender, EventArgs e)
        {
            if (MainModel.CurrentMember == null)
            {
                lblOr.Visible = false;
                btnByBalancePwd.Visible = false;
            }
            else
            {
                lblOr.Visible = true;
                btnByBalancePwd.Visible = true;
            }

            MainModel.frmmainmedia.ShowPayInfo("请出示余额码", false);
        }
        #endregion



        private void lblExit_Click(object sender, EventArgs e)
        {
            if (DataReceiveHandle != null)
                this.DataReceiveHandle.BeginInvoke(this.DialogResult, OverOrderId, ExpiredCode,CurrentCart, null, null);
            this.Close();
        }


        private void Listener_ScanerEvent(ScanerHook.ScanerCodes codes)
        {
            if(this.Enabled)
            {
                PayOnLine(codes.Result);
            }
            else
            {
                LogManager.WriteLog("余额付款屏蔽扫描时间扫描："+codes.Result);
            }

            

        }



        public void PayOnLine(string balancecode)
        {
            try
            {
                IsScan = false;
                this.Enabled = false;
                LoadingHelper.ShowLoadingScreen("支付中...");
                string BalaceErrorMsg="";
                 Member member =   httputil.BalanceCodeRecognition(balancecode,!(MainModel.CurrentMember==null),ref BalaceErrorMsg);

                   //if (BalaceErrorMsg != "" || cart == null) //商品不存在或异常
                   if (BalaceErrorMsg != "" || member==null) //商品不存在或异常
                   {

                       //TODO  刷新会员信息
                       //CheckUserAndMember(ResultCode, ErrorMsgCart);                      

                       MainModel.ShowLog(BalaceErrorMsg,true);
                       IsScan = true;
                       this.Enabled = true;
                       LoadingHelper.CloseForm();

                       return;
                   }
                   else
                   {

                       //if (MainModel.CurrentMember == null)
                       //{
                       //    MainModel.CurrentMember = member;
                       //}


                       if (MainModel.CurrentMember == null)
                       {
                           MainModel.CurrentMember = member;
                       }
                       else
                       {
                           MainModel.CurrentMember.memberheaderresponsevo = member.memberheaderresponsevo;
                       }


                       CurrentCart.balancepayoption = 1;
                       string ErrorMsgCart = "";
                       int ResultCode = 0;

                       Cart cart = httputil.RefreshCart(CurrentCart, ref ErrorMsgCart, ref ResultCode);

                       if (ErrorMsgCart != "" || cart == null) //商品不存在或异常
                       {


                          if(ResultCode==330008)//此代码代表需要输入密码
                           {
                               LoadingHelper.CloseForm();
                               
                               this.Enabled = false;
                               Application.DoEvents();
                               frmBalancePwdMain frmbalancepwd = new frmBalancePwdMain(true);
                               frmbalancepwd.Size = this.Size;
                               frmbalancepwd.Location = this.Location;
                               asf.AutoScaleControlTest(frmbalancepwd, 404, 500, this.Width, this.Height, true);
                               frmbalancepwd.TopMost = true;

                               frmbalancepwd.ShowDialog();

                               CheckUserAndMember(MainModel.BalancePwdErrorCode,"");

                               if (frmbalancepwd.DialogResult == DialogResult.OK)
                               {
                                   CurrentCart.paypasswordtype = 1;

                                   string privatekey = MainModel.URL.Contains("pos.zhuizhikeji.com") ? "MFwwDQYJKoZIhvcNAQEBBQADSwAwSAJBAK9Qd/bThbnosGb7t6qRl3xGQDJb5cu/gpStdDfE9zJaB81CniDaXpR9+8Nap0Naru2vJL0ytOV7L+pjELwBrWcCAwEAAQ==" : "MFwwDQYJKoZIhvcNAQEBBQADSwAwSAJBAN2nINiXBXIzzC6LMqS7/cyXLtEpqa+e2WcyHQoyXytWabBNRH8Vno/d/sDXCZm81LIJJwralJHYUciMMTEkqeMCAwEAAQ==";

                                   string paypassword = MainModel.RSAEncrypt(privatekey, frmbalancepwd.Securitycode + ":" + frmbalancepwd.PassWord);

                                   CurrentCart.paypassword = paypassword;

                                   Cart pwdcart = httputil.RefreshCart(CurrentCart, ref ErrorMsgCart, ref ResultCode);

                                   if (pwdcart == null || !string.IsNullOrEmpty(ErrorMsgCart))
                                   {

                                       if (ResultCode == 100011)
                                       {
                                           MainModel.ShowLog("非当前登录用户的付款码，请确认后重新支付", true);
                                           return;
                                       }
                                       else
                                       {
                                           CheckUserAndMember(ResultCode,ErrorMsgCart);
                                           //MainModel.ShowLog(ErrorMsgCart, true);
                                       }
                                       
                                   }else
                                   {
                                       //回参没有，混合支付需要调用
                                       CurrentCart = pwdcart;

                                       MainModel.frmMainmediaCart = CurrentCart;
                                       MainModel.frmmainmedia.UpdateForm();


                                       CurrentCart.balancepayoption = 1;
                                       CurrentCart.paypasswordtype = 1;
                                       CurrentCart.paypassword = paypassword;

                                       if (pwdcart.totalpayment == 0)
                                       {
                                           pwdcart.paypasswordtype = 1;
                                           pwdcart.paypassword = paypassword;
                                           string ErrorMsg = "";
                                           //int ResultCode = 0;
                                           CreateOrderResult orderresult = httputil.CreateOrder(pwdcart, ref ErrorMsg, ref ResultCode);
                                           this.Enabled = true;
                                           if (ResultCode != 0 || orderresult == null)
                                           {
                                               CheckUserAndMember(ResultCode, ErrorMsg);
                                               //MainModel.ShowLog(ErrorMsg, true);
                                           }
                                           else if (orderresult.continuepay == 1)
                                           {
                                               //TODO  继续支付
                                               MainModel.ShowLog("需要继续支付", true);
                                           }
                                           else
                                           {
                                               OverOrderId = orderresult.orderid;
                                               this.DialogResult = DialogResult.OK;

                                               if (DataReceiveHandle != null)
                                                   this.DataReceiveHandle.BeginInvoke(this.DialogResult, OverOrderId, ExpiredCode, CurrentCart, null, null);
                                               this.Close();
                                           }
                                       }
                                       else
                                       {

                                           pwdcart.paypasswordtype = 1;
                                           pwdcart.paypassword = paypassword;
                                           this.Enabled = true;
                                           //TODO  混合支付
                                           //this.DialogResult=DialogResult.

                                           this.DialogResult = DialogResult.Retry;

                                           if (DataReceiveHandle != null)
                                               this.DataReceiveHandle.BeginInvoke(this.DialogResult, OverOrderId, ExpiredCode, CurrentCart, null, null);
                                           this.Close();
                                       }

                                   }
                               }
                               else
                               {
                                   CheckUserAndMember(MainModel.BalancePwdErrorCode,"");
                               }

                               this.Enabled = true;
                           }
                           else
                           {
                               CheckUserAndMember(ResultCode, ErrorMsgCart);
                               //MainModel.ShowLog(ErrorMsgCart, true);

                           }

                      
                           return;
                       }
                       else
                       {
                           CurrentCart = cart;

                           MainModel.frmMainmediaCart = CurrentCart;
                           MainModel.frmmainmedia.UpdateForm();
                           CurrentCart.balancepayoption = 1;

                           if (cart.totalpayment == 0)
                           {
                               string ErrorMsg = "";
                               //int ResultCode = 0;
                               CreateOrderResult orderresult = httputil.CreateOrder(cart, ref ErrorMsg, ref ResultCode);
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
                                   OverOrderId = orderresult.orderid;
                                   this.DialogResult = DialogResult.OK;

                                   if (DataReceiveHandle != null)
                                       this.DataReceiveHandle.BeginInvoke(this.DialogResult, OverOrderId, ExpiredCode, CurrentCart, null, null);
                                   this.Close();
                               }
                           }
                           else
                           {
                               //TODO  混合支付
                               //this.DialogResult=DialogResult.

                               this.DialogResult = DialogResult.Retry;

                               if (DataReceiveHandle != null)
                                   this.DataReceiveHandle.BeginInvoke(this.DialogResult, OverOrderId, ExpiredCode, CurrentCart, null, null);
                               this.Close();
                           }
                       }
                   }
            }
            catch { }
            finally
            {
                 IsScan = true;
                this.Enabled = true;
                LoadingHelper.CloseForm();
            }
        }


        private void btnByBalancePwd_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                Application.DoEvents();
                frmBalancePwdMain frmbalancepwd = new frmBalancePwdMain(false);
                frmbalancepwd.Size = this.Size;
                frmbalancepwd.Location = this.Location;

                asf.AutoScaleControlTest(frmbalancepwd, 404, 500, this.Width, this.Height,true);

                frmbalancepwd.TopMost = true;

                frmbalancepwd.ShowDialog();

                CheckUserAndMember(MainModel.BalancePwdErrorCode, "");

                if (frmbalancepwd.DialogResult == DialogResult.OK)
                {
                    CurrentCart.paypasswordtype = 1;
                    CurrentCart.balancepayoption = 1;
                    string ErrorMsgCart = "";
                    int ResultCode = 0;

                    string privatekey = MainModel.URL.Contains("pos.zhuizhikeji.com") ? "MFwwDQYJKoZIhvcNAQEBBQADSwAwSAJBAK9Qd/bThbnosGb7t6qRl3xGQDJb5cu/gpStdDfE9zJaB81CniDaXpR9+8Nap0Naru2vJL0ytOV7L+pjELwBrWcCAwEAAQ==" : "MFwwDQYJKoZIhvcNAQEBBQADSwAwSAJBAN2nINiXBXIzzC6LMqS7/cyXLtEpqa+e2WcyHQoyXytWabBNRH8Vno/d/sDXCZm81LIJJwralJHYUciMMTEkqeMCAwEAAQ==";

                    string paypassword = MainModel.RSAEncrypt(privatekey, frmbalancepwd.Securitycode + ":" + frmbalancepwd.PassWord);

                    CurrentCart.paypassword = paypassword;

                    Cart pwdcart = httputil.RefreshCart(CurrentCart, ref ErrorMsgCart, ref ResultCode);
                    if (pwdcart == null || !string.IsNullOrEmpty(ErrorMsgCart))
                    {
                        if (ResultCode == 100011)
                        {
                            MainModel.ShowLog("非当前登录用户的付款码，请确认后重新支付", true);
                            return;
                        }
                        else
                        {
                            CheckUserAndMember(ResultCode,ErrorMsgCart);
                           // MainModel.ShowLog(ErrorMsgCart, true);
                        }
                    }
                    else
                    {
                        CurrentCart = pwdcart;

                        MainModel.frmMainmediaCart = CurrentCart;
                        MainModel.frmmainmedia.UpdateForm();
                        //回参没有， 混合支付的时候需要这些参数
                        CurrentCart.paypasswordtype = 1;
                        CurrentCart.balancepayoption = 1;
                        CurrentCart.paypassword = paypassword;

                        if (pwdcart.totalpayment == 0)
                        {


                            pwdcart.paypasswordtype = 1;
                            pwdcart.paypassword = paypassword;
                            string ErrorMsg = "";
                            //int ResultCode = 0;
                            CreateOrderResult orderresult = httputil.CreateOrder(pwdcart, ref ErrorMsg, ref ResultCode);
                            this.Enabled = true;
                            if (ResultCode != 0 || orderresult == null)
                            {
                                CheckUserAndMember(ResultCode, ErrorMsg);
                                //MainModel.ShowLog(ErrorMsg, true);
                            }
                            else if (orderresult.continuepay == 1)
                            {
                                //TODO  继续支付
                                MainModel.ShowLog("需要继续支付", true);
                            }
                            else
                            {
                                OverOrderId = orderresult.orderid;
                                this.DialogResult = DialogResult.OK;

                                if (DataReceiveHandle != null)
                                    this.DataReceiveHandle.BeginInvoke(this.DialogResult, OverOrderId, ExpiredCode, CurrentCart, null, null);
                                this.Close();
                            }
                        }
                        else
                        {
                            this.Enabled = true;
                            //TODO  混合支付
                            //this.DialogResult=DialogResult.

                            this.DialogResult = DialogResult.Retry;

                            if (DataReceiveHandle != null)
                                this.DataReceiveHandle.BeginInvoke(this.DialogResult, OverOrderId, ExpiredCode, CurrentCart, null, null);
                            this.Close();
                        }
                    }
                }


                this.Enabled = true;
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("使用密码支付异常",true);
            }
            finally
            {
                this.Enabled = true;                
            }
        }

        public ExpiredType ExpiredCode = ExpiredType.None;
        private void CheckUserAndMember(int resultcode, string ErrorMsg)
        {
            try
            {

                if (resultcode == MainModel.HttpUserExpired)
                {

                    this.Enabled = false;
                    MainModel.CurrentMember = null;

                    ExpiredCode = ExpiredType.UserExpired;

                    if (DataReceiveHandle != null)
                        this.DataReceiveHandle.BeginInvoke(this.DialogResult, OverOrderId, ExpiredCode, CurrentCart, null, null);

                    this.Close();

                }
                else if (resultcode == MainModel.HttpMemberExpired)
                {
                    this.Enabled = false;
                    MainModel.CurrentMember = null;

                    ExpiredCode = ExpiredType.MemberExpired;

                    if (DataReceiveHandle != null)
                        this.DataReceiveHandle.BeginInvoke(this.DialogResult, OverOrderId, ExpiredCode, CurrentCart, null, null);

                    this.Close();

                }
                else if(resultcode==MainModel.DifferentMember)   //不是同一个会员 只提示不退出
                {
                    MainModel.ShowLog("非当前登录用户的付款码，请确认后重新支付", true);
                }
                else
                {
                    MainModel.ShowLog(ErrorMsg,false);
                }

            }
            catch (Exception ex)
            {

                this.Enabled = true;

                MainModel.ShowLog("面板商品验证用户/会员异常", true);

            }

        }


        bool CurrentFormStatus = true;
        private void SetFormStatus(bool status)
        {
            if (status == CurrentFormStatus)
            {
                return;
            }
            else
            {
                CurrentFormStatus = status;
                if (status)
                {
                    LoadingHelper.CloseForm();
                    this.Enabled = true;
                }
                else
                {
                    LoadingHelper.ShowLoadingScreen("支付中...");
                    this.Enabled = false;
                }
            }
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
                return false;
            }

            else
                return base.ProcessDialogKey(keyData);
        }


        private void AutoScaleControl()
        {

            try
            {
                float wScale = (float)Screen.AllScreens[0].Bounds.Width /3/this.Width;
                float hScale = (float)Screen.AllScreens[0].Bounds.Height *3/5/ this.Height;

                this.Size = new System.Drawing.Size(Convert.ToInt32(Screen.AllScreens[0].Bounds.Width / 3), Convert.ToInt32(Screen.AllScreens[0].Bounds.Height * 3 / 5));

                

                foreach (Control c in this.Controls)
                {
                    c.Left = (int)Math.Ceiling(c.Left * wScale);
                    c.Top = (int)Math.Ceiling(c.Top * hScale);

                    c.Width = (int)Math.Ceiling(c.Width * wScale);
                    c.Height = (int)Math.Ceiling(c.Height * hScale);

                    float wSize = c.Font.Size * wScale;
                    float hSize = c.Font.Size * hScale;



                    c.Font = new Font(c.Font.Name, Math.Min(hSize, wSize), c.Font.Style, c.Font.Unit);


                }
            }
            catch
            {

            }

        }
     
    }

    public enum BalanceResultType
    {
        /// <summary>
        /// 初始状态
        /// </summary>
        None, 
        /// <summary>
        /// 取消支付
        /// </summary>
        Cancle,
        /// <summary>
        /// 余额支付完成
        /// </summary>
        PaySuccess,
        /// <summary>
        /// 余额不足需要继续支付
        /// </summary>
        NeedPay

    }
}
