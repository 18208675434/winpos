using ZhuiZhi_Integral_Scale_UncleFruit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Drawing.Printing;
using System.Drawing;
using System.Windows.Forms;
using Maticsoft.BLL;
using Newtonsoft.Json;
using Maticsoft.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.BrokenUI;
using ZhuiZhi_Integral_Scale_UncleFruit.BrokenUI.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.ScaleFactory;
using ZhuiZhi_Integral_Scale_UncleFruit.PrintFactory;
using ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Common
{
    public class PrintUtil
    {
        #region 打印订单
        public static bool PrintOrder(PrintDetail printdetail, bool isRefound, ref string errormsg)
        {
            try
            {
                //string ScaleName = INIManager.GetIni("Scale", "ScaleName", MainModel.IniPath);

                //if (ScaleName == ScaleType.托利多.ToString())
                //{
                //    return ToledoPrintUtil.PrintOrder(printdetail,isRefound,ref errormsg);
                //}
                //else if (ScaleName == ScaleType.易捷通.ToString())
                //{
                //    return SprtPrintUtil.PrintOrder(printdetail, isRefound, ref errormsg);
                //}
                //else
                //{
                //    return YKPrintUtil.PrintOrder(printdetail, isRefound, ref errormsg);
                //}

                //return false;

                //QuestPara para = e.Argument as QuestPara;
                printdetail.isrefund = isRefound;
                System.ComponentModel.BackgroundWorker bk = new System.ComponentModel.BackgroundWorker();
                bk.DoWork += PrintOrder_DoWork;
                bk.RunWorkerAsync(printdetail);

                return true;
            }
            catch {
                return false;
            }
        }

        public static void PrintOrder_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

            try
            {
                string errrormsg="";
                PrintDetail printdetail = e.Argument as PrintDetail;
                string ScaleName = INIManager.GetIni("Scale", "ScaleName", MainModel.IniPath);

                if (ScaleName == ScaleType.托利多.ToString())
                {
                    ToledoPrintUtil.PrintOrder(printdetail, printdetail.isrefund, ref errrormsg);
                }
                else if (ScaleName == ScaleType.易捷通.ToString())
                {
                    SprtPrintUtil.PrintOrder(printdetail, printdetail.isrefund, ref errrormsg);
                }
                else
                {
                    YKPrintUtil.PrintOrder(printdetail, printdetail.isrefund, ref errrormsg);
                }


            }
            catch
            {
                 
            }
            
        }
        #endregion

        #region 打印交班单

        public static bool ReceiptPrint(Receiptdetail receipt, ref string errormsg)
        {
            try
            {
               
                System.ComponentModel.BackgroundWorker bk = new System.ComponentModel.BackgroundWorker();
                bk.DoWork += ReceiptPrint_DoWork;
                bk.RunWorkerAsync(receipt);

                return true;
            }
            catch
            {
                return false;
            }
        }
        public static void ReceiptPrint_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                string errormsg = "";
                Receiptdetail receipt = e.Argument as Receiptdetail;

                string ScaleName = INIManager.GetIni("Scale", "ScaleName", MainModel.IniPath);

                if (ScaleName == ScaleType.托利多.ToString())
                {
                     ToledoPrintUtil.ReceiptPrint(receipt, ref errormsg);
                }
                else if (ScaleName == ScaleType.易捷通.ToString())
                {
                     SprtPrintUtil.ReceiptPrint(receipt, ref errormsg);
                }
                else
                {
                     YKPrintUtil.ReceiptPrint(receipt, ref errormsg);

                }

                return ;
            }
            catch {
                return ;
            }
        }
        #endregion

        #region 打印报损单

        public static bool PrintBroken(CreateBrokenResult brokenresult, ref string errormsg)
        {
            try
            {
                System.ComponentModel.BackgroundWorker bk = new System.ComponentModel.BackgroundWorker();
                bk.DoWork += PrintBroken_DoWork;
                bk.RunWorkerAsync(brokenresult);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void PrintBroken_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                string errormsg = "";
                CreateBrokenResult brokenresult = e.Argument as CreateBrokenResult;
                string ScaleName = INIManager.GetIni("Scale", "ScaleName", MainModel.IniPath);

                if (ScaleName == ScaleType.托利多.ToString())
                {
                     ToledoPrintUtil.PrintBroken(brokenresult, ref errormsg);
                }
                else if (ScaleName == ScaleType.易捷通.ToString())
                {
                     SprtPrintUtil.PrintBroken(brokenresult, ref errormsg);
                }
                else
                {
                     YKPrintUtil.PrintBroken(brokenresult, ref errormsg);

                }
                return;
            }
            catch {
                return;
            }
        }
        #endregion

        #region 开钱箱
        public static void OpenCashDrawerEx()
        {
            try
            {
                System.ComponentModel.BackgroundWorker bk = new System.ComponentModel.BackgroundWorker();
                bk.DoWork += OpenCashDrawerEx_DoWork;
                bk.RunWorkerAsync();

                return;
            }
            catch
            {
                return;
            }
        }

        public static void OpenCashDrawerEx_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                ReceiptUtil.EditOpenMoneyPacketCount(1);


                string ScaleName = INIManager.GetIni("Scale", "ScaleName", MainModel.IniPath);

                if (ScaleName == ScaleType.托利多.ToString())
                {
                    ToledoPrintUtil.OpenCashDrawerEx();
                }
                else if (ScaleName == ScaleType.易捷通.ToString())
                {
                    SprtPrintUtil.OpenCashDrawerEx();
                }
                else
                {
                    YKPrintUtil.OpenCashDrawerEx();
                }
            }
            catch { }
        }
        #endregion


        #region 打印充值单
        public static bool PrintTopUp(ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model.TopUpPrint printdetail, bool isEntityCardBatchSale = false)
        {
            try
            {
                printdetail.isEntityCardBatchSale = isEntityCardBatchSale;
                System.ComponentModel.BackgroundWorker bk = new System.ComponentModel.BackgroundWorker();
                bk.DoWork += PrintTopUp_DoWork;
                bk.RunWorkerAsync(printdetail);

                return true;
            }
            catch
            {
                return false;
            }
        }
        public static void PrintTopUp_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {

                ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model.TopUpPrint printdetail = e.Argument as ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model.TopUpPrint;

                DbJsonUtil.AddBalanceInfo(printdetail.paymodeforapi, printdetail.amount);

                string ScaleName = INIManager.GetIni("Scale", "ScaleName", MainModel.IniPath);

                if (ScaleName == ScaleType.托利多.ToString())
                {
                    ToledoPrintUtil.PrintTopUp(printdetail, printdetail.isEntityCardBatchSale);
                }
                else if (ScaleName == ScaleType.易捷通.ToString())
                {
                    SprtPrintUtil.PrintTopUp(printdetail, printdetail.isEntityCardBatchSale);
                }
                else
                {
                    YKPrintUtil.PrintTopUp(printdetail, printdetail.isEntityCardBatchSale);
                }

                return;
            }
            catch {
                return;
            }
        }
        #endregion

        #region 余额充值退款

        public static bool PrintBalanceRefund(string refundid)
        {
            try
            {
                System.ComponentModel.BackgroundWorker bk = new System.ComponentModel.BackgroundWorker();
                bk.DoWork += PrintBalanceRefund_DoWork;
                bk.RunWorkerAsync(refundid);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static HttpUtil httputil = new HttpUtil();
        public static void PrintBalanceRefund_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                string errormsg = "";
                string refundid = e.Argument as string;
                ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model.GetBalanceDepositRefund printdetail = httputil.GetBalancecodepositrefoundbill(refundid, ref errormsg);

                if (!string.IsNullOrEmpty(errormsg) || printdetail == null)
                {
                    LogManager.WriteLog(errormsg);
                    return;
                }


                string ScaleName = INIManager.GetIni("Scale", "ScaleName", MainModel.IniPath);

                if (ScaleName == ScaleType.托利多.ToString())
                {
                    ToledoPrintUtil.PrintBalanceRefund(printdetail);
                }
                else if (ScaleName == ScaleType.易捷通.ToString())
                {
                    SprtPrintUtil.PrintBalanceRefund(printdetail);
                }
                else
                {
                    YKPrintUtil.PrintBalanceRefund(printdetail);
                }

                return;
            }
            catch
            {
                return;
            }
        }

        #endregion

        #region 打印礼品卡订单
        public static bool PrintGiftCardOrder(ZhuiZhi_Integral_Scale_UncleFruit.GiftCard.Model.GiftCardPrintDetail printdetail, bool isRefound, ref string errormsg)
        {
            try
            {
                printdetail.isRefound = isRefound;
                System.ComponentModel.BackgroundWorker bk = new System.ComponentModel.BackgroundWorker();
                bk.DoWork += PrintGiftCardOrder_DoWork;
                bk.RunWorkerAsync(printdetail);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void PrintGiftCardOrder_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {

                string errormsg = "";
                ZhuiZhi_Integral_Scale_UncleFruit.GiftCard.Model.GiftCardPrintDetail printdetail = e.Argument as ZhuiZhi_Integral_Scale_UncleFruit.GiftCard.Model.GiftCardPrintDetail;

                string ScaleName = INIManager.GetIni("Scale", "ScaleName", MainModel.IniPath);

                if (ScaleName == ScaleType.托利多.ToString())
                {
                     ToledoPrintUtil.PrintGiftCardOrder(printdetail, printdetail.isRefound, ref errormsg);
                }
                else if (ScaleName == ScaleType.易捷通.ToString())
                {
                     SprtPrintUtil.PrintGiftCardOrder(printdetail, printdetail.isRefound, ref errormsg);
                }
                else
                {
                     YKPrintUtil.PrintGiftCardOrder(printdetail, printdetail.isRefound, ref errormsg);
                }

                return;
            }
            catch
            {
                return;
            }
        }
        #endregion


        #region 打印线上订单
        public static bool PrintThirdOrder(PrinterPickOrderInfo printdetail, ref string errormsg)
        {
            try
            {
                System.ComponentModel.BackgroundWorker bk = new System.ComponentModel.BackgroundWorker();
                bk.DoWork += PrintThirdOrder_DoWork;
                bk.RunWorkerAsync(printdetail);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void PrintThirdOrder_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {

                string errormsg = "";
                PrinterPickOrderInfo printdetail = e.Argument as PrinterPickOrderInfo;


                string ScaleName = INIManager.GetIni("Scale", "ScaleName", MainModel.IniPath);

                if (ScaleName == ScaleType.托利多.ToString())
                {
                     ToledoPrintUtil.PrintThirdOrder(printdetail, ref errormsg);
                }
                else if (ScaleName == ScaleType.易捷通.ToString())
                {
                     SprtPrintUtil.PrintThirdOrder(printdetail, ref errormsg);
                }
                else
                {
                     YKPrintUtil.PrintThirdOrder(printdetail, ref errormsg);
                }
                return;
            }
            catch
            {
                return;
            }
        }
      
    }

        #endregion

    public enum PrintType
    {

    }
}
