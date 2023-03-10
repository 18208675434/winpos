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
using ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter;

namespace ZhuiZhi_Integral_Scale_UncleFruit
{
    public partial class FormTopUpByOnLine : Form
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

        string CurrentMobile = "";


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

        bool isrun = true;
        tradePara trade = null;// 批量售卡使用
        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();

        public FormTopUpByOnLine(string orderid, string mobile = "", tradePara trade = null)
        {
            InitializeComponent();
            this.trade = trade;
            CurrentOrderID = orderid;
            CurrentMobile = mobile;
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            if (trade == null)
            {
                string errormsg = "";
                bool result = httputil.CancleBalanceOrder(CurrentOrderID, CurrentMobile, ref errormsg);

                if (result)
                {
                    LogManager.WriteLog("取消订单" + CurrentOrderID);
                }
                else
                {
                    LogManager.WriteLog("订单取消失败" + errormsg);
                    // return;
                }
            }
            isrun = false;
            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }



        public void PayOnLine(string authcode)
        {
            try
            {
                scancode = new StringBuilder();
                if (isScan)
                {
                    isScan = false;
                    StartTime = DateTime.Now;
                    CurrentAuthCode = authcode;
                    AuthCodeTrade();
                }

            }
            catch (Exception ex)
            {

            }
        }


        private void AuthCodeTrade()
        {
            try
            {
                string ErrorMsg = "";

                if (trade==null)
                {
                    trade = new tradePara();
                    trade.orderid = CurrentOrderID;
                    trade.ordertype = 2;
                }
               
                trade.authcode = CurrentAuthCode;

                AuthcodeTrade codetrade = httputil.AuthCodeTrade(trade, ref ErrorMsg);
                Console.WriteLine("authcodetrade" + CurrentOrderID + "    " + CurrentAuthCode);
                if (ErrorMsg != "" || codetrade == null)
                {

                    MainModel.ShowLog(ErrorMsg, false);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();



                }
                else
                {
                    //if (codetrade.status == "SUCCESS")
                    //{
                    //    this.DialogResult = DialogResult.OK;
                    //    this.Close();
                    //}
                    //else
                    //{

                        pnlLoading.Visible = true;
                        timerSyncTrade.Enabled = true;
                        CurrentPayID = codetrade.payid;
                        SyncTrade(CurrentOrderID, CurrentPayID);
                  //  }

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
                synctrade sync = httputil.SyncTrade(orderid, payid, ref errormsg, ref retunerrormsg);
                Console.WriteLine("synctrade" + orderid + "    " + payid);
                if (errormsg != "" || sync == null)
                {

                    timerSyncTrade.Enabled = false;

                    MainModel.ShowLog(errormsg, true);
                    isScan = true;
                }
                else if (sync.status == "REQUEST_SUCCESS")
                {
                    //btnCancle.Enabled = false;
                    //lblExit.Enabled = false;
                    //pnlWaiting.Visible = true;

                }
                else if (sync.status == "REQUEST_CLOSE")
                {
                    pnlLoading.Visible = false;
                    timerSyncTrade.Enabled = false;
                    MainModel.ShowLog("交易关闭,请重新扫码付款！", false);
                    isScan = true;
                }
                else if (sync.status == "FAIL")
                {
                    MainModel.ShowLog("支付失败", false);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();

                }
                else if (sync.status == "SUCCESS")
                {
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



        private void timerSyncTrade_Tick(object sender, EventArgs e)
        {
            if (isrun)
            {

                if ((DateTime.Now - StartTime).TotalSeconds > 60)
                {
                    //ShowLog("交易超时！", false);
                    MainModel.ShowLog("交易超时！", false);

                    pnlLoading.Visible = false;
                    timerSyncTrade.Enabled = false;
                    this.Activate();
                    isrun = false;
                    this.Hide();
                    FrmPayTimeOut frmpaytimeout = new FrmPayTimeOut();
                    frmpaytimeout.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmpaytimeout.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmpaytimeout.Height) / 2);
                    frmpaytimeout.TopMost = true;
                    frmpaytimeout.ShowDialog();

                    this.DialogResult = DialogResult.Cancel;

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

                pnlLoading.Visible = false;
                isrun = false;

                timerSyncTrade.Enabled = false;
                MemberCenterMediaHelper.HidePayInfo();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ERROR", "在线收银页面关闭异常" + ex.Message);
            }
        }

        private void frmOnLinePayResult_Shown(object sender, EventArgs e)
        {

            MemberCenterMediaHelper.ShowPayInfo();

            //焦点在客屏会导致扫码无响应   加载完毕 刷新一次焦点
            this.Activate();
        }

        //数据处理过程不接受新数据
        bool isScan = true;
        StringBuilder scancode = new StringBuilder();

        protected override bool ProcessDialogKey(Keys keyData)
        {
            try
            {
                switch (keyData)
                {
                    //不同键盘数字键值不同
                    case Keys.D0: scancode.Append("0"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.D1: scancode.Append("1"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.D2: scancode.Append("2"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.D3: scancode.Append("3"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.D4: scancode.Append("4"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.D5: scancode.Append("5"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.D6: scancode.Append("6"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.D7: scancode.Append("7"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.D8: scancode.Append("8"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.D9: scancode.Append("9"); return !base.ProcessDialogKey(keyData); break;

                    case Keys.NumPad0: scancode.Append("0"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.NumPad1: scancode.Append("1"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.NumPad2: scancode.Append("2"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.NumPad3: scancode.Append("3"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.NumPad4: scancode.Append("4"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.NumPad5: scancode.Append("5"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.NumPad6: scancode.Append("6"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.NumPad7: scancode.Append("7"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.NumPad8: scancode.Append("8"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.NumPad9: scancode.Append("9"); return !base.ProcessDialogKey(keyData); break;

                    //case Keys.Back: AddNum(0, true); return base.ProcessDialogKey(keyData); break;
                    //case Keys.Enter: QueueScanCode.Enqueue(scancode.ToString()); scancode = new StringBuilder(); return !base.ProcessDialogKey(keyData); break;
                }
                if (keyData == Keys.Enter)
                {
                    PayOnLine(scancode.ToString());
                    scancode = new StringBuilder();
                    return false;
                }
                if (keyData == Keys.Space)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("监听键盘事件异常" + ex.Message);
                return false;
            }

        }

        private void FormTopUpByOnLine_Leave(object sender, EventArgs e)
        {
            this.Activate();
        }

    }
}
