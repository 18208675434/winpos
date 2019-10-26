using QiandamaPOS.Common;
using QiandamaPOS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace QiandamaPOS
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


        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();

        public frmOnLinePayResult(string orderid)
        {
            InitializeComponent();
            CurrentOrderID = orderid;
            listener.ScanerEvent += Listener_ScanerEvent;

            listener.Start();
        }

        private void Listener_ScanerEvent(ScanerHook.ScanerCodes codes)
        {

            PayOnLine(codes.Result);
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
                MainModel.ShowLog("订单取消失败" + errormsg, true);
            }
            isrun = false;

            if (DataReceiveHandle != null)
                this.DataReceiveHandle.BeginInvoke(3, "", null, null);
            this.Close();
        }

        private void btnCancle_Click(object sender, EventArgs e)
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
            }
            isrun = false;
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

                if (ErrorMsg != "" || codetrade == null) //商品不存在或异常
                {
                    MainModel.ShowLog(ErrorMsg, false);
                }
                else
                {
                    if (codetrade.status == "SUCCESS")
                    {
                        if (DataReceiveHandle != null)
                            this.DataReceiveHandle.BeginInvoke(1, codetrade.orderid, null, null);
                        isrun = false;
                        this.Close();
                    }
                    else
                    {
                        timerAuthCodeTrade.Enabled = false;
                        CurrentPayID = codetrade.payid;
                        SyncTrade(CurrentOrderID, codetrade.payid);  
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
                pnlWaiting.Visible = true;
                string errormsg = "";
                synctrade sync = httputil.SyncTrade(orderid, payid, ref errormsg);

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
                    //btnCancle.Enabled = true;
                    //lblExit.Enabled = true;
                    pnlWaiting.Visible = false;
                    timerSyncTrade.Enabled = false;
                    MainModel.ShowLog("交易关闭,请重新扫码付款！", false);
                }
                else if (sync.status == "FAIL")
                {
                    pnlWaiting.Visible = false;
                    timerSyncTrade.Enabled = false;
                    MainModel.ShowLog("交易失败,请重新扫码付款！", false);
                }
                else if (sync.status == "SUCCESS")
                {
                    btnCancle.Enabled = true;
                    lblExit.Enabled = true;
                    pnlWaiting.Visible = false;
                    if (DataReceiveHandle != null)
                        this.DataReceiveHandle.BeginInvoke(1,sync.orderid, null, null);
                    isrun = false;
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
                    timerSyncTrade.Enabled = false;
                    isrun = false;
                }


                if (isrun && CurrentPayID != "" && isrun)
                {
                    SyncTrade(CurrentOrderID, CurrentPayID);
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
            MainModel.frmmainmedia.ShowPayInfo("请出示微信/支付宝/银联付款码");
        }


    }
}
