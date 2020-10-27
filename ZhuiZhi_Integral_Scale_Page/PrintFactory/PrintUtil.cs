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
                bk.DoWork += HttpAsyncRequest_DoWork;
                bk.RunWorkerAsync(printdetail);

                return true;
            }
            catch {
                return false;
            }
        }

        public static  void HttpAsyncRequest_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
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

        public static bool ReceiptPrint(Receiptdetail receipt, ref string errormsg)
        {
            try
            {
                string ScaleName = INIManager.GetIni("Scale", "ScaleName", MainModel.IniPath);

                if (ScaleName == ScaleType.托利多.ToString())
                {
                    return ToledoPrintUtil.ReceiptPrint(receipt, ref errormsg);
                }
                else if (ScaleName == ScaleType.易捷通.ToString())
                {
                    return SprtPrintUtil.ReceiptPrint(receipt, ref errormsg);
                }
                else
                {
                    return YKPrintUtil.ReceiptPrint(receipt, ref errormsg);

                }

                return false;
            }
            catch {
                return false;
            }
        }

        public static bool PrintBroken(CreateBrokenResult brokenresult, ref string errormsg)
        {
            try
            {
                string ScaleName = INIManager.GetIni("Scale", "ScaleName", MainModel.IniPath);

                if (ScaleName == ScaleType.托利多.ToString())
                {
                    return ToledoPrintUtil.PrintBroken(brokenresult, ref errormsg);
                }
                else if (ScaleName == ScaleType.易捷通.ToString())
                {
                    return SprtPrintUtil.PrintBroken(brokenresult, ref errormsg);
                }
                else
                {
                    return YKPrintUtil.PrintBroken(brokenresult, ref errormsg);

                }
                return false;
            }
            catch {
                return false;
            }
        }


        public static void OpenCashDrawerEx()
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



        private static HttpUtil httputil = new HttpUtil();
        public static bool PrintTopUp(string depositbillid)
        {
            try
            {
                try { LogManager.WriteLog("充值单:" + depositbillid); }
                catch { }
                string errormsg = "";
                ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model.TopUpPrint printdetail = httputil.GetDepositbill(depositbillid, ref errormsg);

                if (!string.IsNullOrEmpty(errormsg) || printdetail == null)
                {
                    LogManager.WriteLog(errormsg);
                    return false;
                }

                DbJsonUtil.AddBalanceInfo(printdetail.paymodeforapi, printdetail.amount);

                string ScaleName = INIManager.GetIni("Scale", "ScaleName", MainModel.IniPath);

                if (ScaleName == ScaleType.托利多.ToString())
                {
                    ToledoPrintUtil.PrintTopUp(printdetail);
                }
                else if (ScaleName == ScaleType.易捷通.ToString())
                {
                    SprtPrintUtil.PrintTopUp(printdetail);
                }
                else
                {
                    YKPrintUtil.PrintTopUp(printdetail);
                }

                return true;
            }
            catch {
                return false;
            }
        }

        public static bool PrintBalanceRefund(string refundid)
        {
            try
            {

                string errormsg = "";
                ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model.GetBalanceDepositRefund printdetail = httputil.GetBalancecodepositrefoundbill(refundid, ref errormsg);

                if (!string.IsNullOrEmpty(errormsg) || printdetail == null)
                {
                    LogManager.WriteLog(errormsg);
                    return false;
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

                return true;
            }
            catch
            {
                return false;
            }
        }



        public static bool PrintGiftCardOrder(ZhuiZhi_Integral_Scale_UncleFruit.GiftCard.Model.GiftCardPrintDetail printdetail, bool isRefound, ref string errormsg)
        {
            try
            {
                string ScaleName = INIManager.GetIni("Scale", "ScaleName", MainModel.IniPath);

                if (ScaleName == ScaleType.托利多.ToString())
                {
                    return ToledoPrintUtil.PrintGiftCardOrder(printdetail, isRefound, ref errormsg);
                }
                else if (ScaleName == ScaleType.易捷通.ToString())
                {
                    return SprtPrintUtil.PrintGiftCardOrder(printdetail, isRefound, ref errormsg);
                }
                else
                {
                    return YKPrintUtil.PrintGiftCardOrder(printdetail, isRefound, ref errormsg);

                }

                return false;
            }
            catch
            {
                return false;
            }
        }



        public static bool PrintThirdOrder(PrinterPickOrderInfo printdetail, ref string errormsg)
        {
            try
            {
                string ScaleName = INIManager.GetIni("Scale", "ScaleName", MainModel.IniPath);

                if (ScaleName == ScaleType.托利多.ToString())
                {
                    return ToledoPrintUtil.PrintThirdOrder(printdetail, ref errormsg);
                }
                else if (ScaleName == ScaleType.易捷通.ToString())
                {
                    return SprtPrintUtil.PrintThirdOrder(printdetail, ref errormsg);
                }
                else
                {
                    return YKPrintUtil.PrintThirdOrder(printdetail, ref errormsg);

                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        static MemberCenterHttpUtil memberCenterHttpUtil=new MemberCenterHttpUtil();
      
        /// <summary>
        /// 批量售卡 小票打印
        /// </summary>
        /// <param name="orderids"></param>
        /// <returns></returns>
        public static bool PrintEntityCardBatchSale(ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model.TopUpPrint printdetail, bool isEntityCardBatchSale=false)
        {
            try
            {
                DbJsonUtil.AddBalanceInfo(printdetail.paymodeforapi, printdetail.amount);
                string ScaleName = INIManager.GetIni("Scale", "ScaleName", MainModel.IniPath);

                if (ScaleName == ScaleType.托利多.ToString())
                {
                    ToledoPrintUtil.PrintTopUp(printdetail,true);
                }
                else if (ScaleName == ScaleType.易捷通.ToString())
                {
                    SprtPrintUtil.PrintTopUp(printdetail, true);
                }
                else
                {
                    YKPrintUtil.PrintTopUp(printdetail, true);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
