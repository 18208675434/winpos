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
    public partial class frmOnLinePayResult : Form
    {

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

         Cart CurrentCart = new Cart();

        /// <summary>
        /// 当前付款码
        /// </summary>
        string CurrentAuthCode = "";

        /// <summary>
        /// 当前payid
        /// </summary>
        string CurrentPayID = "";

        /// <summary>
        /// 订单开始时间  轮询结果60秒结束
        /// </summary>
        DateTime StartTime;

        /// <summary>
        /// USB设备监听
        /// </summary>
        private ScanerHook listener = new ScanerHook();

        /// <summary>
        /// 1、交易完成 2、交易失败
        /// </summary>
        /// <param name="type"></param>
        public delegate void DataRecHandleDelegate(int type,string orderid);

        /// <summary>
        /// 数据接收事件
        /// </summary>
        public event DataRecHandleDelegate DataReceiveHandle;

        bool isrun = true;

        private bool IsScan = true;

        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();

        public frmOnLinePayResult(string orderid,Cart cart)
        {
            InitializeComponent();
            CurrentOrderID = orderid;
            CurrentCart =(Cart) cart.qianClone();
            listener.ScanerEvent += Listener_ScanerEvent;

            try
            {
               // this.Size = new System.Drawing.Size(Convert.ToInt32(380 * MainModel.wScale), Convert.ToInt32(480 * MainModel.hScale));
                AutoScaleControl();
                this.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - this.Width) / 2, (Screen.AllScreens[0].Bounds.Height - this.Height) / 2);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("自适应在线收银页面异常"+ex.Message);
            }

            listener.Start();
        }

        private void Listener_ScanerEvent(ScanerHook.ScanerCodes codes)
        {
            if (IsScan)
            {
                PayOnLine(codes.Result);
            }
            else
            {
                LogManager.WriteLog("在线支付扫码枪屏蔽时间"+codes.Result);
            }
           
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            
            string errormsg = "";
            bool result = httputil.CancleOrder(CurrentOrderID, "取消支付", ref errormsg);

            if (result )
            {
                LogManager.WriteLog("取消订单"+CurrentOrderID);
            }
            else
            {
                LogManager.WriteLog("订单取消失败" + errormsg);
               // return;
            }
            isrun = false;
            this.DialogResult = DialogResult.Cancel;
            if (DataReceiveHandle != null)
                this.DataReceiveHandle.BeginInvoke(3, "", null, null);
            this.Close();
        }



        public void PayOnLine(string authcode)
        {
            StartTime = DateTime.Now;
            CurrentAuthCode = authcode;
            AuthCodeTrade();
           // SyncTrade();
           // timerAuthCodeTrade.Enabled = true;
           // timerSyncTrade.Enabled = true;
        }


        private void AuthCodeTrade()
        {
            try
            {
                string ErrorMsg = "";
                AuthcodeTrade codetrade = httputil.AuthCodeTrade(CurrentOrderID, CurrentAuthCode, ref ErrorMsg);
                Console.WriteLine("authcodetrade"+CurrentOrderID+"    "+CurrentAuthCode);
                if (ErrorMsg != "" || codetrade == null)
                {

                    IsScan = false;
                    frmPayFail frmpayfail = new frmPayFail(ErrorMsg);
                    asf.AutoScaleControlTest(frmpayfail,1178,760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                    frmpayfail.Location = new System.Drawing.Point(0, 0);
                    frmpayfail.TopMost = true;
                    frmpayfail.ShowDialog();

                    if (frmpayfail.DialogResult == DialogResult.OK) //重新尝试
                    {


                        string ErrorMsgCart = "";
                        int ResultCodecart = 0;
                        CurrentCart= httputil.RefreshCart(CurrentCart, ref ErrorMsgCart, ref ResultCodecart);




                        string errormsg = "";
                        bool result = httputil.CancleOrder(CurrentOrderID, "取消支付", ref errormsg);

                        string ErrorMsgre = "";
                        int ResultCode = -1;
                        CreateOrderResult orderresult = httputil.CreateOrder(CurrentCart, ref ErrorMsgre, ref ResultCode);
                        if (ResultCode != 0 || orderresult == null)
                        {
                            MainModel.ShowLog("重建订单异常"+ErrorMsgre,true);
                        }
                        else if (orderresult.continuepay == 1)
                        {
                            CurrentOrderID = orderresult.orderid;
                            
                        }

                        IsScan = true;
                        //MainModel.frmmainmedia.ShowPayInfo("请出示微信/支付宝付款码", false);
                    }
                    else
                    {
                        string errormsg = "";
                        bool result = httputil.CancleOrder(CurrentOrderID, "取消支付", ref errormsg);

                        if (result)
                        {
                            LogManager.WriteLog("取消订单" + CurrentOrderID);
                        }
                        else
                        {
                            MainModel.ShowLog("订单取消失败" + errormsg, true);
                            // return;
                        }
                        isrun = false;
                        
                        this.DialogResult = DialogResult.Abort;
                        if (DataReceiveHandle != null)
                            this.DataReceiveHandle.BeginInvoke(3, "", null, null);
                        this.Close();
                    }

                    IsScan = true;
                }
                else
                {
                    if (codetrade.status == "SUCCESS")
                    {
                        if (DataReceiveHandle != null)
                            this.DataReceiveHandle.BeginInvoke(1, codetrade.orderid, null, null);
                        isrun = false;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {

                        LoadingHelper.ShowLoadingScreen("支付中...");
                        timerAuthCodeTrade.Enabled = false;
                        timerSyncTrade.Enabled = true;
                        CurrentPayID = codetrade.payid;
                        SyncTrade(CurrentOrderID, CurrentPayID);  
                    }
                 
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("在线支付异常" + ex.Message);
            }
        }


        //支付状态(REQUEST_SUCCESS:请求成功， REQUEST_CLOSE:交易关闭， FAIL:支付失败， SUCCESS： 支付成功)
        private void SyncTrade(string orderid, string payid)
        {
            try
            {
               

                string errormsg = "";
                string retunerrormsg = "";
                synctrade sync = httputil.SyncTrade(orderid, payid, ref errormsg,ref retunerrormsg);
                Console.WriteLine("synctrade" + orderid + "    " + payid);
                if (errormsg != "" || sync == null)
                {

                    timerSyncTrade.Enabled = false;

                    MainModel.ShowLog(errormsg, true);
                }
                else if (sync.status == "REQUEST_SUCCESS")
                {
                    //btnCancle.Enabled = false;
                    //lblExit.Enabled = false;
                    //pnlWaiting.Visible = true;

                }
                else if (sync.status == "REQUEST_CLOSE")
                {
                    LoadingHelper.CloseForm();
                    timerSyncTrade.Enabled = false;
                    MainModel.ShowLog("交易关闭,请重新扫码付款！", false);
                }
                else if (sync.status == "FAIL")
                {
                    LoadingHelper.CloseForm();
                    timerSyncTrade.Enabled = false;

                    IsScan = false;
                    frmPayFail frmpayfail = new frmPayFail(retunerrormsg);
                    asf.AutoScaleControlTest(frmpayfail,1178,760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                    frmpayfail.Location = new System.Drawing.Point(0, 0);
                    frmpayfail.TopMost = true;
                    frmpayfail.ShowDialog();

                    if (frmpayfail.DialogResult == DialogResult.OK) //重新尝试
                    {

                        string errormsgre = "";
                        bool result = httputil.CancleOrder(CurrentOrderID, "取消支付", ref errormsgre);
                        //if (result)
                        //{
                        //    LogManager.WriteLog("取消订单" + CurrentOrderID);
                        //}
                        //else
                        //{
                        //    MainModel.ShowLog("订单取消失败" + errormsg, true);
                        //    // return;
                        //}

                        string ErrorMsgCart = "";
                        int ResultCodecart = 0;
                       CurrentCart = httputil.RefreshCart(CurrentCart, ref ErrorMsgCart, ref ResultCodecart);

                       
                        string ErrorMsgre = "";
                        int ResultCode = -1;

                        
                        CreateOrderResult orderresult = httputil.CreateOrder(CurrentCart, ref ErrorMsgre, ref ResultCode);
                        if (ResultCode != 0 || orderresult == null)
                        {
                            MainModel.ShowLog("重建订单异常" + ErrorMsgre, true);
                        }
                        else if (orderresult.continuepay == 1)
                        {
                            CurrentOrderID = orderresult.orderid;

                            Console.WriteLine("新订单号"+orderresult.orderid);

                        }
                        IsScan = true;
                    }
                    else
                    {

                        string errormsg1 = "";
                        bool result = httputil.CancleOrder(CurrentOrderID, "取消支付", ref errormsg1);

                        if (result)
                        {
                            LogManager.WriteLog("取消订单" + CurrentOrderID);
                        }
                        else
                        {
                            MainModel.ShowLog("订单取消失败" + errormsg, true);
                            // return;
                        }
                        isrun = false;
                        this.DialogResult = DialogResult.Abort;
                        if (DataReceiveHandle != null)
                            this.DataReceiveHandle.BeginInvoke(3, "", null, null);
                        this.Close();
                        
                    }

                  
                }
                else if (sync.status == "SUCCESS")
                {
                    lblExit.Enabled = true;
                    LoadingHelper.CloseForm();
                    if (DataReceiveHandle != null)
                        this.DataReceiveHandle.BeginInvoke(1,sync.orderid, null, null);
                    isrun = false;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                if (isrun)
                {
                    //Delay.Start(10);
                    //SyncTrade(orderid, payid);
                }
            }
            catch
            {

            }          
           
        }

        ////信息提示
        //private void ShowLog(string msg, bool iserror)
        //{
        //    this.Invoke(new InvokeHandler(delegate()
        //    {
        //        frmMsg frmmsf = new frmMsg(msg, iserror, 1500);
        //        frmmsf.ShowDialog();
        //    }));

        //}


        private void timerAuthCodeTrade_Tick(object sender, EventArgs e)
        {
            //if ((DateTime.Now - StartTime).TotalMinutes > 1 || (DateTime.Now - StartTime).TotalSeconds > 60)
            //{
            //    ShowLog("交易超时失败！",false);
            //    isrun = false;
            //}

            //if (isrun)
            //{
            //    AuthCodeTrade();
            //}
        }

        private void timerSyncTrade_Tick(object sender, EventArgs e)
        {
            if (isrun)
            {

                if ((DateTime.Now - StartTime).TotalSeconds > 60)
                {
                    //ShowLog("交易超时！", false);
                    MainModel.ShowLog("交易超时！", false);

                    LoadingHelper.CloseForm();
                    timerSyncTrade.Enabled = false;
                    this.Activate();
                    isrun = false;
                    this.Hide();
                    FrmPayTimeOut frmpaytimeout = new FrmPayTimeOut();
                    frmpaytimeout.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmpaytimeout.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmpaytimeout.Height) / 2);
                    frmpaytimeout.TopMost = true;
                    frmpaytimeout.ShowDialog();

                    this.DialogResult = DialogResult.Cancel;
                    if (DataReceiveHandle != null)
                        this.DataReceiveHandle.BeginInvoke(3, "", null, null);

                    this.Close();
                   
                }
                else
                {
                    if (isrun && CurrentPayID != "" && isrun)
                    {
                        SyncTrade(CurrentOrderID, CurrentPayID);
                    }
                }


               

            }
        }

        public void frmOnLinePayResult_SizeChanged(object sender, EventArgs e)
        {
           // asf.ControlAutoSize(this);
        }

        private void frmOnLinePayResult_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

            
            isrun = false;

            timerAuthCodeTrade.Enabled = false;
            timerSyncTrade.Enabled = false;

            listener.Stop();


           // MainModel.frmmainmedia.IniForm();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ERROR","在线收银页面关闭异常"+ex.Message);
            }
        }

        private void frmOnLinePayResult_Shown(object sender, EventArgs e)
        {
            MainModel.frmmainmedia.ShowPayInfo("请出示微信/支付宝付款码",false);
        }





        private void AutoScaleControl()
        {

            try
            {
                float wScale = (float)Screen.AllScreens[0].Bounds.Width / 3 / this.Width;
                float hScale = (float)Screen.AllScreens[0].Bounds.Height * 3 / 5 / this.Height;

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
}
