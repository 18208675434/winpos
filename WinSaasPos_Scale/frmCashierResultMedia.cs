using WinSaasPOS_Scale.Common;
using WinSaasPOS_Scale.Model;
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

namespace WinSaasPOS_Scale
{
    public partial class frmCashierResultMedia : Form
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

        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();

        public frmCashierResultMedia(string orderid)
        {
            InitializeComponent();
            CurrentOrderID = orderid;

        }


        private void frmCashierResult_Shown(object sender, EventArgs e)
        {
            GetResult();
            timerClose.Enabled = true;


            lblCashierInfo.Text = CurrentOrderID;
            //Thread threadPrint = new Thread(UpdateInfo);
            //threadPrint.IsBackground = true;
            //threadPrint.Start();
        }

        private void UpdateInfo(object obj)
        {
            string ErrorMsg = "";
            PrintDetail printdetail = httputil.GetPrintDetail(CurrentOrderID, ref ErrorMsg);
            if (ErrorMsg != "" || printdetail == null)
            {
                //获取订单打印详情异常
                //ShowLog(ErrorMsg, true);
            }
            else
            {
                this.Invoke(new InvokeHandler(delegate()
                   {
                       //显示收银详情

                       foreach (Payinfo pay in printdetail.payinfo)
                       {
                           lblCashierInfo.Text += pay.type + " ￥" + pay.amount + "|";
                       }
                       lblCashierInfo.Text = lblCashierInfo.Text.TrimEnd('|');
                   }));
                Application.DoEvents();

            }


        }

        private void GetResult()
        {
            //try
            //{

            //    string ErrorMsg = "";
            //    int status = httputil.RequestStatus(CurrentOrderID, ref ErrorMsg);

            //    if (status == 1)
            //    {
            //        Delay.Start(500);
            //            this.Close();
            //            return;
            //    }
            //    else
            //    {
            //        GetResult();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    LogManager.WriteLog("ERROR", "查询订单状态异常" + ex.Message);
            //}

        }

        private void timerClose_Tick(object sender, EventArgs e)
        {

            lblSecond.Text = (Convert.ToInt16(lblSecond.Text) - 1).ToString();

            if (lblSecond.Text == "0")
            {
                this.Close();
            }

        }

        private void frmCashierResultMedia_SizeChanged(object sender, EventArgs e)
        {
            asf.ControlAutoSize(this);
        }



    }
}
