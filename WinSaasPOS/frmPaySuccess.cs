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
using System.Threading;
using System.Windows.Forms;

namespace WinSaasPOS
{
    public partial class frmPaySuccess : Form
    {
        HttpUtil httputil = new HttpUtil();
        bool isrun = true;

        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();
        /// <summary>
        /// 当前执行的订单号
        /// </summary>
        private string CurrentOrderID = "";

        /// <summary>
        /// 订单状态完成标志
        /// </summary>
        bool OrderResult = false;
        /// <summary>
        /// 打印完成标志
        /// </summary>
        bool PrintResult = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">0:关闭  1：成功</param>
        /// <param name="orderid"></param>
        public delegate void DataRecHandleDelegate(int type, string orderid);
        /// <summary>
        /// 数据接收事件
        /// </summary>
        public event DataRecHandleDelegate DataReceiveHandle;


        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();

        public frmPaySuccess(string orderid)
        {
            InitializeComponent();
            CurrentOrderID = orderid;

        }


        private void frmCashierResult_Shown(object sender, EventArgs e)
        {

            timerClose.Enabled = true;

            Thread threadOrderStatus = new Thread(GetResult);
            threadOrderStatus.IsBackground = true;
            threadOrderStatus.Start();
            Delay.Start(1000);

            RetryPrintCount = 0;
            Thread threadPrint = new Thread(SEDPrint);
            threadPrint.IsBackground = true;
            threadPrint.Start();
            //SEDPrint();
        }

        /// <summary>
        /// 打印重试次数，防止订单完成小票打印详情获取不到 最多获取三次
        /// </summary>
        int RetryPrintCount = 0;
        private void SEDPrint()
        {
            if (isrun)
            {
                
                string ErrorMsg = "";
                PrintDetail printdetail = httputil.GetPrintDetail(CurrentOrderID, ref ErrorMsg);
                if (ErrorMsg != "" || printdetail == null)
                {
                    LogManager.WriteLog("获取打印小票信息异常：" + ErrorMsg);
                    Delay.Start(100);
                    RetryPrintCount++;
                    if (RetryPrintCount < 3)
                    {
                        SEDPrint();
                    }
                   
                }
                else
                {                    
                           //显示收银详情
                           foreach (Payinfo pay in printdetail.payinfo)
                           {
                               lblCashierInfo.Text += pay.type + " ￥" + pay.amount + "|";
                           }
                           lblCashierInfo.Text = lblCashierInfo.Text.TrimEnd('|');

                           if (DataReceiveHandle != null)
                               this.DataReceiveHandle.BeginInvoke(1, lblCashierInfo.Text, null, null);

                       
                    Application.DoEvents();
                    string PrintErrorMsg = "";
                    bool printresult = PrintUtil.PrintOrder(printdetail,false, ref PrintErrorMsg);


                    if (PrintErrorMsg != "" || !printresult)
                    {
                        LogManager.WriteLog("打印小票异常" + PrintErrorMsg);
                        //ShowLog(PrintErrorMsg,true);
                    }
                    else
                    {
                        LogManager.WriteLog(DateTime.Now.ToString() + printresult.ToString() + OrderResult.ToString());
                        printresult = true;
                        if (OrderResult)
                        {
                            this.Close();
                        }
                    }

                }
            }

        }

        private void GetResult()
        {
            if (isrun)
            {
                try
                {


                    string ErrorMsg = "";
                    int status = httputil.QueryOrderStatus(CurrentOrderID, ref ErrorMsg);

                    if (status == 1)
                    {
                        LogManager.WriteLog(CurrentOrderID + "完成");
                        OrderResult = true;

                        LogManager.WriteLog(DateTime.Now.ToString() + PrintResult.ToString() + OrderResult.ToString());
                        if (PrintResult)
                        {
                            this.Invoke(new InvokeHandler(delegate()
                            {
                                this.Close();
                            }));
                        }
                        return;
                    }
                    else
                    {
                        GetResult();
                    }
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog("ERROR", "查询订单状态异常" + ex.Message);
                }
            }

        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timerClose_Tick(object sender, EventArgs e)
        {
            lblSecond.Text = (Convert.ToInt16(lblSecond.Text) - 1).ToString();

            if (lblSecond.Text == "0")
            {
                this.Close();
            }
        }

        private void frmCashierResult_SizeChanged(object sender, EventArgs e)
        {
           // asf.ControlAutoSize(this);
        }

        private void frmCashierResult_FormClosing(object sender, FormClosingEventArgs e)
        {
            isrun = false;
            this.Dispose();
        }



    }
}
