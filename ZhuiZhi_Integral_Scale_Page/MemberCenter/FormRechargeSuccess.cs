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
using ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    public partial class FormRechargeSuccess : Form
    {

        MemberCenterHttpUtil memberCenterHttpUtil = new MemberCenterHttpUtil();
        HttpUtil httputil = new HttpUtil();
        bool isrun = true;

        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();
        /// <summary>
        /// 当前执行的订单号
        /// </summary>
        string batchoperatorid;
        List<string> orderids;


        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();
        public FormRechargeSuccess(string batchoperatorid, List<string> orderids)
        {
            InitializeComponent();
            this.batchoperatorid = batchoperatorid;
            this.orderids = orderids;
        }


        private void frmCashierResult_Shown(object sender, EventArgs e)
        {
            MemberCenterMediaHelper.ShowFormRechargeSuccessMedia();
            lblType.Text = "充值成功";
            timerClose.Enabled = true;

            Thread threadPrint = new Thread(SEDPrint);
            threadPrint.IsBackground = true;
            threadPrint.Start();
        }

        private void SEDPrint()
        {
            try
            {
                if (isrun)
                {
                    PrintEntityCardBatchSale();
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("获取订单打印详情异常" + ex.Message, true);
            }

        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
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

        public bool PrintEntityCardBatchSale()
        {
            string errormsg = "";
            var result = memberCenterHttpUtil.GetDepositBillByIds(orderids, ref errormsg);
            if (!string.IsNullOrEmpty(errormsg) || result == null || result.Count == 0)
            {
                MainModel.ShowLog(errormsg, true);
                return false;
            }
            ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model.TopUpPrint printdetail = new ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model.TopUpPrint();

            decimal amount = 0;
            decimal rewardAmount = 0;

            foreach (var item in result)
            {
                amount += item.amount;
                rewardAmount += item.rewardamount;
            }
            printdetail.id = batchoperatorid;
            printdetail.amount = amount;
            printdetail.rewardamount = rewardAmount;
            printdetail.paymodeforapi = result[0].paymodeforapi;
            printdetail.paymode = result[0].paymode;
            printdetail.createdat = MainModel.getStampByDateTime(DateTime.Now);
            return PrintUtil.PrintTopUp(printdetail,true);
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
