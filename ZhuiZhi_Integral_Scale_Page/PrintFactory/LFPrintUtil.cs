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
    public class LFPrintUtil
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

                    List<string> lstaddress = PrintHelper.substr("配送地址：" + printdetail.address, BodyCharCountOfLine);
                    foreach (string str in lstaddress)
                    {
                        PrintText(str, 25);
                    }
                    PrintText("备注：");
                    if (!string.IsNullOrEmpty(printdetail.remark))
                    {
                        PrintText(printdetail.remark, 32);
                    }
                    PrintText("期望送达时间：", 32);
                    PrintText(printdetail.expecttimedesc, 32);



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


                    PrintText(PrintHelper.MergeStr("商品金额：", printdetail.productamt.ToString("f2"), BodyCharCountOfLine, PageSize) + "\n", 25);
                    if (printdetail.deliveryamt > 0)
                    {
                        PrintText(PrintHelper.MergeStr("配送费：", printdetail.deliveryamt.ToString("f2"), BodyCharCountOfLine, PageSize) + "\n", 25);
                    }
                    if (printdetail.promoamt > 0)
                    {
                        PrintText(PrintHelper.MergeStr("活动优惠：", printdetail.promoamt.ToString("f2"), BodyCharCountOfLine, PageSize) + "\n", 25);
                    }
                    if (printdetail.couponamt > 0)
                    {
                        PrintText(PrintHelper.MergeStr("优惠券抵：", printdetail.couponamt.ToString("f2"), BodyCharCountOfLine, PageSize) + "\n", 25);
                    }
                    if (printdetail.pointpayamt > 0)
                    {
                        PrintText(PrintHelper.MergeStr("积分抵现：", printdetail.pointpayamt.ToString("f2"), BodyCharCountOfLine, PageSize) + "\n", 25);
                    }
                    if (printdetail.balancepayamt > 0)
                    {
                        PrintText(PrintHelper.MergeStr("余额支付：", printdetail.balancepayamt.ToString("f2"), BodyCharCountOfLine, PageSize) + "\n", 25);
                    }

                    PrintText(PrintHelper.MergeStr("实付金额：", printdetail.totalpayment.ToString("f2"), BodyCharCountOfLine, PageSize), 25);

                    if (!string.IsNullOrEmpty(printdetail.pickcode))
                    {
                        PrintText(PrintHelper.getStrLine(), 25);
                        PrintText("请扫描下方二维码取货配送", 25);
                        PrintText("  ", 25);
                        BeginPrint();
                        PrintHelper.GetQrBmp(printdetail.pickcode);
                        PrintBitmapFile("orderqrcoe.bmp");

                        BeginPrint();
                        PrintText(PrintHelper.MergeStr("取货码：" + printdetail.pickcode, "", BodyCharCountOfLine, PageSize), 25);
                    }

                    PrintText(PrintHelper.getStrLine(), 25);


                    PrintText("多谢惠顾，欢迎下次光临！" + " \r\n \r\n \r\n \r\n \r\n \r\n", 25); 
                    BeginPrint();
                    //BeginPrint(8); //切纸
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
        public static bool PrintTopUp(TopUpPrint printdetail, bool isEntityCardBatchSale = false)
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
                lstPrintStr.Add(MainModel.GetDateTimeByStamp(printdetail.createdat).ToString("yyyy-MM-dd HH:mm:ss" + "\n"));
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
                lstPrintStr.Add(MainModel.GetDateTimeByStamp(printdetail.createdat).ToString("yyyy-MM-dd HH:mm:ss" + "\n"));
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


        #region 打印 method
        public static bool PrintText(string printtext, int fontsize = 32)
        {
            int result = PrintText_stdcall(printtext, fontsize);
            return result==0;
        }

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

                //int result = PrintTextByPaperWidth(System.Text.Encoding.Default.GetBytes(strprint), 25, 60);
                PrintText(strprint);
                BeginPrint();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool PrintBitmapFile(string BmpFileName)
        {
            //LabelAngle  0不旋转
            int result = PrintBitmapFile_stdcall(BmpFileName, 0);
            return true;
        }

        public static bool BeginPrint(bool isautoend = true)
        {
            try
            {
                int flag = BeginPrint_stdcall();
                if (flag != 0)
                {
                    return false;
                }
                if (isautoend)
                {
                    flag = ClosePrinterEx_stdcall();//结束打印 切纸
                    if (flag != 0)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        #region 打印api
        /*功能	初始化打印机
        函数名	int OpenPrinter(void)
        int OpenPrinter_stdcall (void)
        参数	
        返回值	HS_OK
        HS_ERROR
        实例	int st;
        st= OpenPrinter_stdcall (); 
        */
        [DllImport("lf_pos_dll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int OpenPrinter_stdcall();

        /*功能	开始打印
        函数名	int BeginPrint (void)
        int BeginPrint_stdcall (void)
        参数	
        返回值	HS_OK
        HS_ERROR
         */
        [DllImport("lf_pos_dll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int BeginPrint_stdcall();

        /*功能	关闭打印
        函数名	int ClosePrinterEx (void)
        int ClosePrinterEx _stdcall (void)
        参数	
        返回值	HS_OK
        HS_ERROR
         */
        [DllImport("lf_pos_dll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ClosePrinterEx_stdcall();

        /*功能 格式化待打印字符串
        函数名 int PrintText(char* str, int FontSize)
        int PrintText_stdcall(char* str, int FontSize)
        参数 str 长度必须小于 20480
        FontSize 必须介于 12-72 之间,相应字号的高度等于字号除以 8，单位为毫米，如 72 号字为 9mm
        每次打印支持多次调用此函数进行多次格式化，每次调用可传入不同的字号，比如 单据的第一行使用 32 号字，第二行使用 24 号字
        返回值 HS_OK
        HS_ERROR
         */
        [DllImport("lf_pos_dll.dll",EntryPoint = "PrintText", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int PrintText_stdcall(string str, int FontSize);  

        /*功能	开钱箱
        函数名	int OpenCashDrawerEx(void)
        int OpenCashDrawerEx_stdcall (void)
        参数	
        返回值	HS_OK
        HS_ERROR
        实例	 
        备注	
        */
        [DllImport("lf_pos_dll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int OpenCashDrawerEx_stdcall();

        /*暂无*/
        [DllImport("lf_pos_dll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int PrintBitmapFile_stdcall(string BmpFileName, int LabelAngle);
        #endregion
        #endregion

    }
}
