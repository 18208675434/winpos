using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.GiftCard.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.GiftCard
{
    public partial class FormGiftCardPaySuccess : Form
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

        private GiftCardHttp gifthttputil = new GiftCardHttp();
        public FormGiftCardPaySuccess(string orderid)
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

            Thread threadPrint = new Thread(SEDPrint);
            threadPrint.IsBackground = true;
            threadPrint.Start();
            //SEDPrint();
        }

        private void SEDPrint()
        {
            try
            {
                if (isrun)
                {
                    string errorsuccess = "";
                    GiftCardPrintDetail printdetail = gifthttputil.CardPaySuccess(CurrentOrderID, ref errorsuccess);


                    if (errorsuccess != "" || printdetail == null)
                        {
                            LogManager.WriteLog("获取礼品卡打印小票信息异常：" + errorsuccess);
                            Delay.Start(100);
                            SEDPrint();
                        }
                        else
                        {

                            lblPayDetails.Text = printdetail.paydetails;

                            //显示收银详情
                            foreach (PayinfoItem pay in printdetail.payinfo)
                            {
                                lblCashierInfo.Text += pay.type + " ￥" + pay.amount + "|";
                            }
                            lblCashierInfo.Text = lblCashierInfo.Text.TrimEnd('|');

                            if (DataReceiveHandle != null)
                                this.DataReceiveHandle.BeginInvoke(1, lblCashierInfo.Text, null, null);


                            Application.DoEvents();
                            string PrintErrorMsg = "";
                            bool printresult = PrintUtil.PrintGiftCardOrder(printdetail, false, ref PrintErrorMsg);
                                //PrintUtil.PrintOrder(printdetail, false, ref PrintErrorMsg);


                            if (PrintErrorMsg != "" || !printresult)
                            {
                                LogManager.WriteLog("打印小票异常" + PrintErrorMsg);
                            }
                            else
                            {
                                LogManager.WriteLog(DateTime.Now.ToString() + printresult.ToString() + OrderResult.ToString());

                                this.Close();

                            }

                        }
                    

                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("获取订单打印详情异常"+ex.Message,true);
            }

        }

        private void GetResult()
        {
            if (isrun)
            {
                try
                {
                    if (MainModel.IsOffLine)
                    {
                        OrderResult = true;
                        return;
                    }

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

        private void FormPaySuccess_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                isrun = false;
                timerClose.Enabled = false;
            }
            catch { }
        }


    }
}
