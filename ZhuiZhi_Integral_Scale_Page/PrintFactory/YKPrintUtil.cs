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
    public class YKPrintUtil
    {


        
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

                     if (OpenDevice() == -1)
                {
                    errormsg = "打印机无法连接，请检查驱动和打印纸是否正常！";
                    return false;
                }

                    //每次打印先清空之前内容
                    lstPrintStr = new List<string>();
                    SetAlign(1);
                SetFontSize(0, 1);
                PrintStr(new StringBuilder("欢迎光临" + "\n"));
                PrintStr(new StringBuilder(MainModel.CurrentShopInfo.tenantname + "\n"));
                    lstPrintStr.Add(" ");
                    SetAlign(0);
                    lstPrintStr.AddRange(PrintHelper.GetOrderPrintInfo(printdetail, isRefound));

                    PrintTextByPaperWidth(lstPrintStr,true);
                    YkPrnAndFeedLine(4);
                    CloseDevice();
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

                    if (OpenDevice() == -1)
                    {
                        errormsg = "打印机无法连接，请检查驱动和打印纸是否正常！";
                        return false;
                    }


                    //每次打印先清空之前内容
                    lstPrintStr = new List<string>();



                    SetAlign(1);
                        SetFontSize(0, 1);
                        PrintStr(new StringBuilder("欢迎光临" + "\n"));
                        PrintStr(new StringBuilder(MainModel.CurrentShopInfo.tenantname + "\n"));
                    lstPrintStr.Add(" ");
                    SetAlign(0);
                    lstPrintStr.AddRange(PrintHelper.GetGiftCardOrderPrintInfo(printdetail, isRefound));

                    PrintTextByPaperWidth(lstPrintStr);

                    YkPrnAndFeedLine(4); 
                    CloseDevice();
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

                    IniPrintSize();

                    if (OpenDevice() == -1)
                    {
                        errormsg = "打印机无法连接，请检查驱动和打印纸是否正常！";
                        return false;
                    }

                    PrintTextByPaperWidth(PrintHelper.GetReceiptPrintInfo(receipt));

                    YkPrnAndFeedLine(4);
                    CloseDevice();
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

                    if (OpenDevice() == -1)
                    {
                        errormsg = "打印机无法连接，请检查驱动和打印纸是否正常！";
                        return false;
                    }

                    //每次打印先清空之前内容
                    lstPrintStr = new List<string>();

                    PrintStr(new StringBuilder(PrintHelper.MergeStr("报损单", "", HeadCharCountOfLine, PageSize) + "\n"));
                    lstPrintStr.Add(" ");

                    PrintTextByPaperWidth(PrintHelper.GetBrokenPrintInfo(brokenresult));
                    YkPrnAndFeedLine(4);
                    CloseDevice();
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
                    if (OpenDevice() == -1)
                    {
                        errormsg = "打印机无法连接，请检查驱动和打印纸是否正常！";
                        return false;
                    }

                    SetFontSize(0, 1);
                    PrintStr(PrintHelper.MergeStr(MainModel.CurrentShopInfo.tenantname + "\n", "", HeadCharCountOfLine, PageSize));
                    PrintStr(PrintHelper.MergeStr( MainModel.CurrentShopInfo.shopname + "\n", "", HeadCharCountOfLine, PageSize));

                   
                    PrintStr(printdetail.serialcode + "\n");
                    SetFontSize(0, 0);
                    PrintStr("订单号：" + printdetail.orderid + "\n");
                    PrintStr("下单时间：" + printdetail.date + "\n");
                    PrintStr("顾客姓名：" + printdetail.username + "\n");
                    PrintStr("顾客电话：" + printdetail.tel + "\n");
                    List<string> lstaddress = PrintHelper.substr("配送地址：" + printdetail.address, BodyCharCountOfLine);
                    foreach (string str in lstaddress)
                    {
                        PrintStr(str+"\n");
                    }
                    //PrintStr("配送地址：" + printdetail.address + "\n");
                    PrintStr("备注：" + "\n");

                    SetFontSize(0, 1);
                    if (!string.IsNullOrEmpty(printdetail.remark))
                    {
                        PrintStr( printdetail.remark + "\n");
                    }
                    PrintStr( "期望送达时间："  + "\n");
                    PrintStr( printdetail.expecttimedesc + "\n");


                    SetFontSize(0, 0);
                    PrintStr(PrintHelper.getStrLine() + "\n");

                    //PrintTextRange(PrintHelper.PrintHelper.MergeStr("商品", "单价", "重量(kg)", "金额", BodyCharCountOfLine));

                    PrintStr("商品    单价   数量    金额" + "\n");
                    foreach (PickProduct pro in printdetail.productdetaillist)
                    {
                        List<string> lstpro = PrintHelper.MergeStr(pro.skuname, pro.price, pro.num, pro.money, BodyCharCountOfLine);

                        foreach (string str in lstpro)
                        {
                            PrintStr(str + "\n");
                        }

                    }


                    PrintStr(PrintHelper.getStrLine() + "\n");

                    PrintStr(PrintHelper.MergeStr("商品金额：", printdetail.productamt.ToString("f2"), BodyCharCountOfLine, PageSize) + "\n");
                    if (printdetail.deliveryamt > 0)
                    {
                        PrintStr(PrintHelper.MergeStr("配送费：", printdetail.deliveryamt.ToString("f2"), BodyCharCountOfLine, PageSize) + "\n");
                    }
                    if (printdetail.promoamt > 0)
                    {
                        PrintStr(PrintHelper.MergeStr("活动优惠：", printdetail.promoamt.ToString("f2"), BodyCharCountOfLine, PageSize) + "\n");
                    }
                    if (printdetail.couponamt > 0)
                    {
                        PrintStr(PrintHelper.MergeStr("优惠券抵：", printdetail.couponamt.ToString("f2"), BodyCharCountOfLine, PageSize) + "\n");
                    }
                    if (printdetail.pointpayamt > 0)
                    {
                        PrintStr(PrintHelper.MergeStr("积分抵现：", printdetail.pointpayamt.ToString("f2"), BodyCharCountOfLine, PageSize) + "\n");
                    }
                    if (printdetail.balancepayamt > 0)
                    {
                        PrintStr(PrintHelper.MergeStr("余额支付：", printdetail.balancepayamt.ToString("f2"), BodyCharCountOfLine, PageSize) + "\n");
                    }
                    
                    PrintStr(PrintHelper.MergeStr("实付金额：", printdetail.totalpayment.ToString("f2"), BodyCharCountOfLine, PageSize) + "\n");

                    if (!string.IsNullOrEmpty(printdetail.pickcode))
                    {
                        PrintStr(PrintHelper.getStrLine());
                        PrintStr("请扫描下方二维码取货配送" + "\n");
                        //PrintStr("  " + "\n");
                        PrintHelper.GetQrBmp(printdetail.pickcode);

                        SetAlign(1);
                        StringBuilder logobuilder = new StringBuilder(AppDomain.CurrentDomain.BaseDirectory + "\\orderqrcoe.bmp" );
                        PrintBitmap(logobuilder, 33);

                        PrintStr("取货码："+printdetail.pickcode +"\n");
                        SetAlign(0);
                    }

                    PrintStr(PrintHelper.getStrLine() + "\n");

                    PrintStr("多谢惠顾，欢迎下次光临！                         " + " \n \n \n \n \n \n.");
                    CloseDevice();
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

        public static bool PrintTopUp(TopUpPrint printdetail, bool isEntityCardBatchSale = false)
        {
            try
            {
                IniPrintSize();

                if (OpenDevice() == -1)
                {
                    return false;
                }

                //每次打印先清空之前内容
                lstPrintStr = new List<string>();

                SetAlign(1);
                SetFontSize(0, 1);
                PrintStr(new StringBuilder("欢迎光临" + "\n"));
                PrintStr(new StringBuilder(MainModel.CurrentShopInfo.tenantname + "\n"));
                //lstPrintStr.Add(PrintHelper.MergeStr(MainModel.CurrentShopInfo.tenantname, "", BodyCharCountOfLine, PageSize));
                lstPrintStr.Add(" ");
                SetAlign(0);
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

                YkPrnAndFeedLine(4);
                CloseDevice();
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

                if (OpenDevice() == -1)
                {
                    return false;
                }

                //每次打印先清空之前内容
                lstPrintStr = new List<string>();

                SetAlign(1);
                SetFontSize(0, 1);
                PrintStr(new StringBuilder("欢迎光临" + "\n"));
                PrintStr(new StringBuilder(MainModel.CurrentShopInfo.tenantname + "\n"));
                //lstPrintStr.Add(PrintHelper.MergeStr(MainModel.CurrentShopInfo.tenantname, "", BodyCharCountOfLine, PageSize));
                lstPrintStr.Add(" ");
                SetAlign(0);
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

                YkPrnAndFeedLine(4);
                CloseDevice();
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
                SelectFond(0);
                SetInterCharSet(15);

                SetAlign(0);
                SetFontSize(0, 0);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("初始化打印尺寸异常" + ex.Message);
            }
        }



        public static bool PrintTextByPaperWidth(List<string> lstprints, bool needsuncode = false )
        {
            try
            {
                SetFontSize(0, 0);
                SetAlign(0);
                StringBuilder sb =new StringBuilder();
                foreach (string str in lstprints)
                {
                    sb.Append( str + "\n");
                }
              

                 PrintStr(sb);

                if (needsuncode)
                {
                    if (System.IO.File.Exists(MainModel.ServerPath + "\\" + PrintHelper.SunCodeName) && MainModel.WhetherPrintSunCode)
                    {
                        PrintStr(new StringBuilder(PrintHelper.getStrLine() + "\n"));
                        SetAlign(1);
                        StringBuilder logobuilder = new StringBuilder(MainModel.ServerPath + "\\" + PrintHelper.SunCodeName);
                        PrintBitmap(logobuilder, 33);

                        PrintStr(" " + "\n");
                        PrintStr("上果叔到家，抢实惠好物！" + "\n");
                        SetAlign(0);
                    }
                    PrintStr(new StringBuilder(PrintHelper.getStrLine() + "\n"));
                    PrintStr(new StringBuilder("多谢惠顾，欢迎下次光临！" + "\n"));
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="iport"> 1-10:COM1-COM10  11:LPT1  12:LPT2  13:USB  14:net</param>
        /// <param name="baud"></param>
        /// <returns></returns>
        [DllImport("YkPosdll.dll")]
        public static extern int YkOpenDevice(int iport, int baud);
        [DllImport("YkPosdll.dll")]
        static extern int YkCloseDevice();
        [DllImport("YkPosdll.dll")]
        static extern int YkIsConnected();
        [DllImport("YkPosdll.dll")]
        static extern int YkInitPrinter();
        [DllImport("YkPosdll.dll", CharSet = CharSet.Ansi, EntryPoint = "YkPrintStr", CallingConvention = CallingConvention.StdCall)]
        static extern int YkPrintStr([MarshalAs(UnmanagedType.LPStr)]StringBuilder pstr);
        [DllImport("YkPosdll.dll")]
        static extern int YkFeedPaper();
        [DllImport("YkPosdll.dll")]
        static extern int YkGetStatus(byte n);

        [DllImport("YkPosdll.dll")]
        static extern int YkDownloadBitmapAndPrint([MarshalAs(UnmanagedType.LPStr)]StringBuilder szBmpFile, int m);

        [DllImport("YkPosdll.dll")]
        static extern int YkTabMove();

        [DllImport("YkPosdll.dll")]
        static extern int YkSetHRICharStyle(int n);

        [DllImport("YkPosdll.dll")]
        static extern int YkSetBarCodeHeight(int n);

        [DllImport("YkPosdll.dll")]
        static extern int YkSetCashBoxDriveMode(int m, int t1, int t2);



        [DllImport("YkPosdll.dll")]
        static extern int YkSetCharSize(int m, int n);



        [DllImport("YkPosdll.dll")]
        static extern int YkPrintBarCode(int m, int n, [MarshalAs(UnmanagedType.LPStr)]StringBuilder szBmpFile);


        [DllImport("YkPosdll.dll")]
        static extern int YkSelectFont(int n);


        [DllImport("YkPosdll.dll")]
        static extern int YkSetInterCharSet(int n);

        [DllImport("YkPosdll.dll")]
        static extern int YkSetAlign(int n);

        [DllImport("YkPosdll.dll")]
        static extern int YkPrintBitmap([MarshalAs(UnmanagedType.LPStr)]StringBuilder szBmpFile, int m);

        /// <summary>
        /// 打印后走纸行数
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        [DllImport("YkPosdll.dll")]
        static extern int YkPrnAndFeedLine(int n);

        public static int OpenDevice()
        {
            int i;
         
            i = YkOpenDevice(GetYKComNo(), GetYKBaud());

            return i;
        }

        public static void OpenCashBox()
        {
            try
            {
                int i;
                OpenDevice();
                i = SetCashBoxDriveMode();
                CloseDevice();
                // return i;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("钱箱打开失败" + ex.Message);
            }
        }
        public static int CloseDevice()
        {

            
            int i;
            i = YkCloseDevice();
            return i;
        }
        public static int IsConnected()
        {
            int i;
            i = YkIsConnected();
            return i;
        }
        public static int InitPrinter()
        {
            int i;
            i = YkInitPrinter();
            return i;
        }

        public static int PrintStr(string str)
        {
            StringBuilder sb = new StringBuilder(str);
            return PrintStr(sb);
        }
        public static int PrintStr([MarshalAs(UnmanagedType.LPStr)]StringBuilder pstr)
        {
            int i;
            i = YkPrintStr(pstr);
            return i;
        }

        //    m	效果	垂直密度(dpi)	水平密度(dpi)
        //0	普通	180			180
        //1	倍宽	180			90
        //2	倍高	90			180
        //3	四倍	90			90
        //一般 m=0

        public static int PrintBitmap([MarshalAs(UnmanagedType.LPStr)]StringBuilder szBmpFile, int m)
        {
            int i;

           // string ScaleName = INIManager.GetIni("Scale", "ScaleName", MainModel.IniPath);

                i = YkPrintBitmap(szBmpFile, m);
            
            
            return i;
        }
        public static int FeedPaper()
        {
            int i;
            i = YkFeedPaper();
            return i;
        }
        public static int GetStatus()
        {
            int i;
            i = YkGetStatus(0);
            return i;
        }

        public static int TabMove()
        {
            int i;
            i = YkTabMove();
            return i;
        }


        public static int PrintBarcode(int m, int n, [MarshalAs(UnmanagedType.LPStr)]StringBuilder szBmpFile)
        {
            YkSetBarCodeHeight(100);

            int i;
            i = YkPrintBarCode(m, n, szBmpFile);
            return i;
        }

        public static int SetFontSize(int m, int n)
        {

            int i;
            i = YkSetCharSize(m, n);
            return i;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n">n=0  选择字型 A (12×24); n=1 选择字型 B (9×17)</param>
        /// <returns></returns>
        public static int SelectFond(int n)
        {

            int i;
            i = YkSelectFont(n);
            return i;
        }



        public static int SetInterCharSet(int n)
        {

            int i;
            i = YkSetInterCharSet(n);
            return i;
        }

        /// <summary>
        /// 设置打印时的对齐方式
        /// </summary>
        /// <param name="n">n=0 左对齐    n=1 居中  n=2	右对齐</param>
        /// <returns></returns>
        public static int SetAlign(int n)
        {

            int i;
            i = YkSetAlign(n);

            if (i != 0)
            {
                LogManager.WriteLog("设置研科打印机对齐方式异常");
            }
            return i;
        }


        /// <summary>
        /// 设置钱箱驱动方式
        /// </summary>
        /// <param name="m">m=0 m=0  2脚  m=1  5脚</param>
        /// <returns></returns>
        public static int SetCashBoxDriveMode()
        {
            int i;
            int m = 1;
            int t1 = 150;
            int t2 = 250;
            i = YkSetCashBoxDriveMode(m, t1, t2);
            return i;
        }

        public static void OpenCashDrawerEx()
        {
            int i;
            OpenDevice();
            i = SetCashBoxDriveMode();
            CloseDevice();
        }





        public static int GetYKComNo()
        {
            try
            {
               string comno = INIManager.GetIni("Scale", "PrintComNo", MainModel.IniPath);

               return Convert.ToInt16(comno);
            }
            catch {
                return 11;
            }
        }


        public static int GetYKBaud()
        {
            try
            {
                string comno = INIManager.GetIni("Scale", "PrintBaud", MainModel.IniPath);

                if (string.IsNullOrEmpty(comno))
                {
                    return 9600;
                }
                else
                {
                    return Convert.ToInt32(comno);
                }
                
            }
            catch
            {
                return 9600;
            }
        }


    }
}
