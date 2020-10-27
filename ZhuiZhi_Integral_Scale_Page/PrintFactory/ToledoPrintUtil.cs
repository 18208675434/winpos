using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.BrokenUI.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.GiftCard.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.PrintFactory
{
    public class ToledoPrintUtil
    {

        #region Windows打印

        /// <summary>
        /// 打印机宽度
        /// </summary>
        public static int PageSize = 58;
        /// <summary>
        /// 便宜量
        /// </summary>
        public static int LocationX = 0;
        /// <summary>
        /// 打印机名称
        /// </summary>
        public static string PrintName = "";

        /// <summary>
        /// 58 MM纸张下 一行能够打印的小字符数
        /// </summary>
        public static int BodyCharCountOfLine = 32;

        /// <summary>
        /// 58 MM纸张下 一行能够打印的大字符数
        /// </summary>
        public static int HeadCharCountOfLine = 25;

        public static List<string> lstPrintStr = new List<string>();

        #region  打印订单
        public static object lockprinting = new object();
        public static bool PrintOrder(PrintDetail printdetail, bool isRefound, ref string errormsg)
        {
            lock (lockprinting)
            {
                try
                {
                    try { LogManager.WriteLog("打印订单:" + printdetail.orderid); }
                    catch { }


                    IniPrintSize();

                    //每次打印先清空之前内容
                    lstPrintStr = new List<string>();

                    // lstPrintStr.Add(PrintHelper.MergeStr("欢迎光临", "", HeadCharCountOfLine, PageSize));

                    PrintText(PrintHelper.MergeStr("欢迎光临", "", HeadCharCountOfLine, PageSize));
                    PrintText(PrintHelper.MergeStr(MainModel.CurrentShopInfo.tenantname, "", HeadCharCountOfLine, PageSize));
                    //lstPrintStr.Add(PrintHelper.MergeStr(MainModel.CurrentShopInfo.tenantname, "", BodyCharCountOfLine, PageSize));
                    lstPrintStr.Add(" ");

                    lstPrintStr.AddRange(PrintHelper.GetOrderPrintInfo(printdetail, isRefound));

                    PrintTextByPaperWidth(lstPrintStr);

                    Application.DoEvents();
                    return true;

                }
                catch (Exception ex)
                {
                    LogManager.WriteLog("ERROR", "打印订单小票出现异常" + ex.Message + ex.StackTrace);
                    errormsg = "打印订单小票出现异常" + ex.Message;
                    return false;
                }
            }
        }

        #endregion

        #region  打印礼品卡订单
        public static object lockGIftCardprinting = new object();
        public static bool PrintGiftCardOrder(GiftCardPrintDetail printdetail, bool isRefound, ref string errormsg)
        {
            lock (lockGIftCardprinting)
            {
                try
                {
                    try { LogManager.WriteLog("打印订单:" + printdetail.orderid); }
                    catch { }


                    IniPrintSize();

                    //每次打印先清空之前内容
                    lstPrintStr = new List<string>();



                    // lstPrintStr.Add(PrintHelper.MergeStr("欢迎光临", "", HeadCharCountOfLine, PageSize));

                    PrintText(PrintHelper.MergeStr("欢迎光临", "", HeadCharCountOfLine, PageSize));
                    PrintText(PrintHelper.MergeStr(MainModel.CurrentShopInfo.tenantname, "", HeadCharCountOfLine, PageSize));
                    //lstPrintStr.Add(PrintHelper.MergeStr(MainModel.CurrentShopInfo.tenantname, "", BodyCharCountOfLine, PageSize));
                    lstPrintStr.Add(" ");

                    lstPrintStr.AddRange(PrintHelper.GetGiftCardOrderPrintInfo(printdetail, isRefound));

                    PrintTextByPaperWidth(lstPrintStr);

                    Application.DoEvents();
                    return true;

                }
                catch (Exception ex)
                {
                    LogManager.WriteLog("ERROR", "打印订单小票出现异常" + ex.Message + ex.StackTrace);
                    errormsg = "打印订单小票出现异常" + ex.Message;
                    return false;
                }
            }
        }

        #endregion

        #region 打印交班单
        public static object lockreceiptprint = new object();
        public static bool ReceiptPrint(Receiptdetail receipt, ref string errormsg)
        {
            lock (lockreceiptprint)
            {

                try
                {

                    PrintTextByPaperWidth(PrintHelper.GetReceiptPrintInfo(receipt));

                    return true;
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog("ERROR", "打印交班小票出现异常" + ex.Message + ex.StackTrace);
                    errormsg = "打印交班小票出现异常" + ex.Message;
                    return false;
                }
            }
        }


        #endregion

        #region  打印报损单
        public static object lockprintingBroken = new object();
        public static bool PrintBroken(CreateBrokenResult brokenresult, ref string errormsg)
        {
            lock (lockprintingBroken)
            {
                try
                {
                    try { LogManager.WriteLog("打印报损订单:"); }
                    catch { }

                    IniPrintSize();


                    //每次打印先清空之前内容
                    lstPrintStr = new List<string>();

                    PrintText(PrintHelper.MergeStr("报损单", "", HeadCharCountOfLine, PageSize));
                    lstPrintStr.Add(" ");

                    PrintTextByPaperWidth(PrintHelper.GetBrokenPrintInfo(brokenresult));


                    Application.DoEvents();
                    return true;
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog("ERROR", "打印报损小票出现异常" + ex.Message + ex.StackTrace);
                    errormsg = "打印报损小票出现异常" + ex.Message;
                    return false;
                }
            }
        }

        #endregion



        #region  打印第三方订单
        public static object lockthirdprinting = new object();
        public static bool PrintThirdOrder(PrinterPickOrderInfo printdetail, ref string errormsg)
        {
            lock (lockthirdprinting)
            {
                try
                {
                    try { LogManager.WriteLog("打印第三那方订单:" + printdetail.orderid); }
                    catch { }


                    IniPrintSize();

                    //每次打印先清空之前内容
                    lstPrintStr = new List<string>();


                    PrintText(PrintHelper.MergeStr(MainModel.CurrentShopInfo.tenantname, "", HeadCharCountOfLine, PageSize), 32);
                    PrintText(" ", 32);
                    PrintText(PrintHelper.MergeStr(MainModel.CurrentShopInfo.shopname, "", HeadCharCountOfLine, PageSize), 32);
                    PrintText(printdetail.serialcode, 40);

                    PrintText("订单号：" + printdetail.orderid, 25);
                    PrintText("下单时间：" + printdetail.date, 25);
                    PrintText("顾客姓名：" + printdetail.username, 25);
                    PrintText("顾客电话：" + printdetail.tel, 25);

                    List<string> lstaddress = PrintHelper.substr("配送地址：" + printdetail.address,BodyCharCountOfLine);
                    foreach (string str in lstaddress)
                    {
                        PrintText(str,25);
                    }
                    PrintText("备注：");
                    if (!string.IsNullOrEmpty(printdetail.remark))
                    {
                        PrintText(printdetail.remark, 32);
                    }
                    PrintText("期望送达时间：" + printdetail.expecttimedesc, 30);



                    PrintText(PrintHelper.getStrLine(), 25);

                    //PrintTextRange(PrintHelper.PrintHelper.MergeStr("商品", "单价", "重量(kg)", "金额", BodyCharCountOfLine));

                    PrintText("商品    单价   数量    金额", 25);
                    foreach (PickProduct pro in printdetail.productdetaillist)
                    {
                        List<string> lstpro = PrintHelper.MergeStr(pro.skuname, pro.price, pro.num, pro.money, BodyCharCountOfLine);

                        foreach (string str in lstpro)
                        {
                            PrintText(str, 25);
                        }

                    }
                    PrintText(PrintHelper.getStrLine(), 25);

                    PrintText(PrintHelper.MergeStr("商品金额：", printdetail.productamt, BodyCharCountOfLine, PageSize), 25);
                    if (Convert.ToDecimal( printdetail.deliveryamt) > 0)
                    {
                        PrintText(PrintHelper.MergeStr("配送费：", printdetail.deliveryamt, BodyCharCountOfLine, PageSize), 25);
                    }
                    
                    PrintText(PrintHelper.MergeStr("实付金额：", printdetail.totalpayment, BodyCharCountOfLine, PageSize), 25);

                    if (!string.IsNullOrEmpty(printdetail.pickcode))
                    {
                        PrintText(PrintHelper.getStrLine(), 25);
                        PrintText("请扫描下方二维码取货配送", 25);
                        PrintText("  ", 25);
                        BeginPrint(0);
                        PrintHelper.GetQrBmp(printdetail.pickcode);
                        PrintBitmapFile("orderqrcoe.bmp");

                        BeginPrint(7);
                        PrintText(PrintHelper.MergeStr("取货码："+printdetail.pickcode, "", BodyCharCountOfLine, PageSize), 25);
                    }

                    PrintText(PrintHelper.getStrLine(), 25);


                    PrintTextByPaperWidth(System.Text.Encoding.Default.GetBytes("多谢惠顾，欢迎下次光临！" + " \r\n \r\n \r\n \r\n \r\n \r\n"), 25, 60);
                    BeginPrint(0);
                    BeginPrint(8); //切纸
                    Application.DoEvents();
                    return true;

                }
                catch (Exception ex)
                {
                    LogManager.WriteLog("ERROR", "打印订单小票出现异常" + ex.Message + ex.StackTrace);
                    errormsg = "打印订单小票出现异常" + ex.Message;
                    return false;
                }
            }
        }

        #endregion


        #endregion

        private static HttpUtil httputil = new HttpUtil();
        public static bool PrintTopUp(TopUpPrint printdetail,bool isEntityCardBatchSale=false)
        {
            try
            {
             
                IniPrintSize();

                //每次打印先清空之前内容
                lstPrintStr = new List<string>();

                // lstPrintStr.Add(PrintHelper.MergeStr("欢迎光临", "", HeadCharCountOfLine, PageSize));

                PrintText(PrintHelper.MergeStr("欢迎光临", "", HeadCharCountOfLine, PageSize));
                PrintText(PrintHelper.MergeStr(MainModel.CurrentShopInfo.tenantname, "", HeadCharCountOfLine, PageSize));
                //lstPrintStr.Add(PrintHelper.MergeStr(MainModel.CurrentShopInfo.tenantname, "", BodyCharCountOfLine, PageSize));
                lstPrintStr.Add(" ");

                lstPrintStr.Add("订单号：" + printdetail.id + "\n");
                lstPrintStr.Add("门店：" + MainModel.CurrentShopInfo.shopname + "\n");
                lstPrintStr.Add("地址：" + MainModel.CurrentShopInfo.address + "\n");
                lstPrintStr.Add("电话：" + MainModel.CurrentShopInfo.tel + "\n");
                //lstPrintStr.Add(PrintHelper.MergeStr("收银员：" + MainModel.CurrentUser.nickname, "机：" + MainModel.CurrentShopInfo.deviceid, BodyCharCountOfLine, PageSize));
                lstPrintStr.Add( MainModel.GetDateTimeByStamp(printdetail.createdat).ToString("yyyy-MM-dd HH:mm:ss" + "\n"));
                lstPrintStr.Add(PrintHelper.getStrLine());

                lstPrintStr.Add(PrintHelper.MergeStr("充值消费   X1", printdetail.amount.ToString("f2"), BodyCharCountOfLine, PageSize));
                if (printdetail.rewardamount > 0)
                {
                    lstPrintStr.Add(PrintHelper.MergeStr("    赠送金额", printdetail.rewardamount.ToString("f2"), BodyCharCountOfLine, PageSize));
                }

                lstPrintStr.Add(PrintHelper.getStrLine());

                lstPrintStr.Add("充值方式：" + "\n");
                lstPrintStr.Add(PrintHelper.MergeStr(printdetail.paymodeforapi, printdetail.amount.ToString("f2"), BodyCharCountOfLine, PageSize));


                if (!isEntityCardBatchSale)//批量售卡不打印会员号和账户余额
                {
                    lstPrintStr.Add(PrintHelper.getStrLine());
                    string phone = printdetail.phone;
                    if (printdetail.phone.Length > 7)
                    {
                        phone = printdetail.phone.Substring(0, 3) + "****" + printdetail.phone.Substring(printdetail.phone.Length - 4);
                    }

                    lstPrintStr.Add(PrintHelper.MergeStr("会员号", phone, BodyCharCountOfLine, PageSize));
                    lstPrintStr.Add(PrintHelper.MergeStr("账户余额", printdetail.balance.ToString("f2"), BodyCharCountOfLine, PageSize));
                }

                lstPrintStr.Add(PrintHelper.getStrLine());
                lstPrintStr.Add("多谢惠顾，欢迎下次光临！");

                PrintTextByPaperWidth(lstPrintStr);

                Application.DoEvents();
                return true;

            }
            catch
            {
                return false;
            }
        }




        public static bool PrintBalanceRefund(GetBalanceDepositRefund printdetail)
        {
            try
            {
             
                IniPrintSize();

                //每次打印先清空之前内容
                lstPrintStr = new List<string>();

                // lstPrintStr.Add(PrintHelper.MergeStr("欢迎光临", "", HeadCharCountOfLine, PageSize));

                PrintText(PrintHelper.MergeStr("欢迎光临", "", HeadCharCountOfLine, PageSize));
                PrintText(PrintHelper.MergeStr(MainModel.CurrentShopInfo.tenantname, "", HeadCharCountOfLine, PageSize));
                //lstPrintStr.Add(PrintHelper.MergeStr(MainModel.CurrentShopInfo.tenantname, "", BodyCharCountOfLine, PageSize));
                lstPrintStr.Add(" ");

                lstPrintStr.Add("订单号：" + printdetail.id + "\n");
                lstPrintStr.Add("门店：" + MainModel.CurrentShopInfo.shopname + "\n");
                lstPrintStr.Add("地址：" + MainModel.CurrentShopInfo.address + "\n");
                lstPrintStr.Add("电话：" + MainModel.CurrentShopInfo.tel + "\n");
                //lstPrintStr.Add(PrintHelper.MergeStr("收银员：" + MainModel.CurrentUser.nickname, "机：" + MainModel.CurrentShopInfo.deviceid, BodyCharCountOfLine, PageSize));
                lstPrintStr.Add( MainModel.GetDateTimeByStamp(printdetail.createdat).ToString("yyyy-MM-dd HH:mm:ss" + "\n"));
                lstPrintStr.Add(PrintHelper.getStrLine());


                lstPrintStr.Add(PrintHelper.MergeStr("充值退款   X1", "-" + printdetail.capitalrefundamount.ToString("f2"), BodyCharCountOfLine, PageSize));

                if (printdetail.rewardrefundamount > 0)
                {
                    lstPrintStr.Add(PrintHelper.MergeStr("    赠送金额退款", "-" + printdetail.rewardrefundamount.ToString("f2"), BodyCharCountOfLine, PageSize));
                }
                lstPrintStr.Add(PrintHelper.getStrLine());

                lstPrintStr.Add("退款方式：" + "\n");
                lstPrintStr.Add(PrintHelper.MergeStr(printdetail.refundtypeforapi, "-" + printdetail.capitalrefundamount.ToString("f2"), BodyCharCountOfLine, PageSize));


             
                lstPrintStr.Add(PrintHelper.getStrLine());
                string phone = printdetail.phone;
                if (printdetail.phone.Length > 7)
                {
                    phone = printdetail.phone.Substring(0, 3) + "****" + printdetail.phone.Substring(printdetail.phone.Length - 4);
                }

                lstPrintStr.Add(PrintHelper.MergeStr("会员号", phone, BodyCharCountOfLine, PageSize));
                lstPrintStr.Add(PrintHelper.MergeStr("账户余额", printdetail.balance.ToString("f2"), BodyCharCountOfLine, PageSize));

                lstPrintStr.Add(PrintHelper.getStrLine());
                lstPrintStr.Add("多谢惠顾，欢迎下次光临！");

                PrintTextByPaperWidth(lstPrintStr);

                Application.DoEvents();
                return true;

            }
            catch
            {
                return false;
            }
        }
        public static void IniPrintSize()
        {
            try
            {
                //每次使用初始化一次，否则纸张大于58 每次都会放大
                BodyCharCountOfLine = 32;
                HeadCharCountOfLine = 25;

                PageSize = 58;
                LocationX = 0;
                PrintName = "";

                PageSize = Convert.ToInt32(INIManager.GetIni("Print", "PageSize", MainModel.IniPath));

                LocationX = Convert.ToInt32(INIManager.GetIni("Print", "LocationX", MainModel.IniPath));

                PrintName = INIManager.GetIni("Print", "PrintName", MainModel.IniPath);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("初始化打印尺寸异常" + ex.Message);
            }
        }

        [DllImport("pos_ad_dll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BeginPrint(int PrintType);

        public static bool BeginPrint()
        {

            try
            {

                int result = BeginPrint(0);



                BeginPrint(8);//切纸
                if (result == 240)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        [DllImport("pos_ad_dll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int PrintText(byte[] str, int FontSize);
        public static bool PrintText(string printtext, int fontsize = 32)
        {
            int result = PrintText(System.Text.Encoding.Default.GetBytes(printtext), fontsize);
            return true;
        }

        [DllImport("pos_ad_dll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int PrintTextByPaperWidth(byte[] str, int FontSize, int PaperWidth);
        public static bool PrintTextByPaperWidth(List<string> lstprints)
        {
            try
            {
                string strprint = "";

                foreach (string str in lstprints)
                {

                    strprint += str + "\r\n"; //每行前面加两个空格 防止纸张偏打印不全 两边都留间距

                }

                //末尾打印空白行走纸
                strprint += "  \r\n  \r\n  \r\n  \r\n  \r\n  \r\n ";

                int result = PrintTextByPaperWidth(System.Text.Encoding.Default.GetBytes(strprint), 25, 60);

                BeginPrint();



                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        [DllImport("pos_ad_dll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int OpenCashDrawerEx();



        [DllImport("pos_ad_dll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int PrintBitmapFile(string BmpFileName, int LabelAngle);
        public static bool PrintBitmapFile(string BmpFileName)
        {
            //LabelAngle  0不旋转
            int result = PrintBitmapFile(BmpFileName, 0);
            return true;
        }
    }
}
