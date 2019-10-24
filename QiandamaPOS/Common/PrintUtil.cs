using QiandamaPOS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace QiandamaPOS.Common
{
    public class PrintUtil
    {

        public static bool SEDPrint(PrintDetail printdetail,ref string errormsg)
        {
            try
            {
                string logofilepath = AppDomain.CurrentDomain.BaseDirectory + "\\headlogo.bmp";
                StringBuilder logobuilder = new StringBuilder(logofilepath);
                if (PrintUtil.OpenDevice() == -1)
                {
                    errormsg = "打印机无法连接，请检查驱动和打印纸是否正常！";
                    return false;
                }

                PrintUtil.SelectFond(0);
                PrintUtil.SetInterCharSet(15);

                PrintUtil.SetAlign(1);
                PrintUtil.PrintBitmap(logobuilder, 0);

                PrintUtil.SetAlign(0);
                PrintUtil.SetFontSize(0, 1);
                StringBuilder stringb = new StringBuilder("   欢迎光临[钱大妈]不卖隔夜肉\n");
                PrintUtil.PrintStr(stringb);

                PrintUtil.SetFontSize(0, 0);
                PrintUtil.FeedPaper();
                StringBuilder sb = new StringBuilder();
                sb.Append("订单号：" + printdetail.orderid + "\n");
                sb.Append("门店：" + MainModel.CurrentShopInfo.shopname + "\n");
                sb.Append("地址：" + MainModel.CurrentShopInfo.address + "\n");
                sb.Append("电话：" + MainModel.CurrentShopInfo.tel + "\n");
                sb.Append("收银员：" + MainModel.CurrentUser.nickname + "         机：" + MainModel.CurrentShopInfo.deviceid + "\n");
                sb.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss" + "\n"));
                sb.Append(DateTime.Now.ToString("-----------------------------" + "\n"));
                foreach (ProductDetail pro in printdetail.products)
                {
                    sb.Append(DateTime.Now.ToString(pro.skucode + "  " + pro.title + "\n"));
                    if (pro.goodstagid == 0)//标品
                    {
                        string priceandnum = "   " + pro.num + "  X  " + pro.price.saleprice;

                        sb.Append(DateTime.Now.ToString(priceandnum + pro.price.total.ToString().PadLeft(26 - priceandnum.Length, ' ') + "\n"));

                    }
                    else if (pro.goodstagid == 1) //散称称重
                    {
                        string priceandnum = "   " + pro.specnum + "  X  " + pro.price.saleprice;
                        sb.Append(DateTime.Now.ToString(priceandnum + pro.price.total.ToString().PadLeft(26 - priceandnum.Length, ' ') + "\n"));
                    }
                    else if (pro.goodstagid == 2) //多规格原称重
                    {
                        string priceandnum = "   " + pro.specnum + "  X  " + pro.price.saleprice;
                        sb.Append(DateTime.Now.ToString(priceandnum + pro.price.total.ToString().PadLeft(26 - priceandnum.Length, ' ') + "\n"));
                    }

                }
                sb.Append(DateTime.Now.ToString("-----------------------------" + "\n"));

                //汉字占两位 TODO 判断前面汉字和英文数字
                foreach (Orderpricedetail pricedetail in printdetail.orderpricedetails)
                {
                    sb.Append(pricedetail.title + pricedetail.amount.PadLeft(28 - Encoding.Default.GetBytes(pricedetail.title).Length, ' ') + "\n");

                }
                sb.Append(DateTime.Now.ToString("-----------------------------" + "\n"));

                foreach (Paydetail paydetail in printdetail.paydetail)
                {
                    sb.Append(paydetail.title + paydetail.amount.PadLeft(28 - paydetail.title.Length * 2, ' ') + "\n");

                }

                sb.Append(DateTime.Now.ToString("-----------------------------" + "\n"));
                sb.Append("多谢惠顾，欢迎下次光临！" + "\n");
                sb.Append("钱大妈官网:http://www.qdama.cn" + "\n");
                sb.Append("顾客服务热线: 400-628-5880" + "\n");
                sb.Append("官方会员微信: qdama888" + "\n");
                sb.Append("  加入会员 积分买菜 优惠多多" + "\n");


                PrintUtil.PrintStr(sb);
                //TODO  二维码


                PrintUtil.SetAlign(1);
                string qrcodepath = AppDomain.CurrentDomain.BaseDirectory + "\\QrCode.bmp";
                StringBuilder sbqr = new StringBuilder(qrcodepath);

                PrintUtil.PrintBitmap(sbqr, 3);


                PrintUtil.SetFontSize(0, 1);
                StringBuilder strEnd = new StringBuilder("新鲜·便捷·优选 100%退换货\n\n\n\n\n\n");
                PrintUtil.PrintStr(strEnd);
                PrintUtil.SetFontSize(0, 0);

                PrintUtil.CloseDevice();

                return true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ERROR", "打印出现异常"+ ex.Message);
                errormsg = "打印出现异常" + ex.Message;
                return false;
            }

        }


        public static bool ReceiptPrint(Receiptdetail receipt, ref string errormsg)
        {
            try
            {
                
                if (PrintUtil.OpenDevice() == -1)
                {
                    errormsg = "打印机无法连接，请检查驱动和打印纸是否正常！";
                    return false;
                }

                PrintUtil.SelectFond(0);

                PrintUtil.SetAlign(0);

                PrintUtil.SetFontSize(0, 0);
                StringBuilder sb = new StringBuilder();

                string Cashier = "收银员：" + receipt.cashier;
                string Serial = receipt.serial.amount + receipt.serial.title;
                sb.Append(Cashier+Serial.PadLeft(28-Cashier.Length*2,' ') + "\n");
                sb.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +(receipt.devicecode.amount+receipt.title.amount).PadLeft(4,' ') + "\n");
                sb.Append(receipt.shiftcode.title +receipt.shiftcode.amount.PadLeft(28-receipt.shiftcode.title.Length,' ')+ "\n");

                sb.Append( "          "+receipt.title.title + "\n");

                sb.Append("开始时间：  " + MainModel.GetDateTimeByStamp(receipt.starttime.ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "\n");
                sb.Append("结束时间：  " + MainModel.GetDateTimeByStamp(receipt.endtime.ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "\n");

                foreach (OrderPriceDetail basicinfo in receipt.basicinfo)
                {
                    try
                    {
                        sb.Append(basicinfo.title + basicinfo.amount.PadLeft(28 - Encoding.Default.GetBytes(basicinfo.title).Length, ' ') + "\n");

                        if (!string.IsNullOrWhiteSpace(basicinfo.subtitle))
                        {
                            sb.Append(basicinfo.subtitle.PadLeft(28 - Encoding.Default.GetBytes(basicinfo.subtitle).Length, ' ') + "\n");
                        }
                    }
                    catch (Exception ex) { }
                }

                sb.Append(DateTime.Now.ToString("-----------------------------" + "\n"));

                sb.Append(receipt.totalamount.title + receipt.totalamount.amount.PadLeft(28 - Encoding.Default.GetBytes(receipt.totalamount.title).Length, ' ') + "\n");
                
                sb.Append(DateTime.Now.ToString("-----------------------------" + "\n"));



                foreach (OrderPriceDetail bottomdetail in receipt.bottomdetails)
                {
                    try
                    {
                        int spacenum = 28 - Encoding.Default.GetBytes(bottomdetail.title).Length - Encoding.Default.GetBytes(bottomdetail.amount).Length;
                        sb.Append(bottomdetail.title + "".PadLeft(spacenum, ' ') + bottomdetail.amount + "\n");

                        if (!string.IsNullOrWhiteSpace(bottomdetail.subtitle))
                        {
                            int spacenum2 = 28 - Encoding.Default.GetBytes(bottomdetail.subtitle).Length;

                            sb.Append("".PadLeft(spacenum2, ' ') + bottomdetail.subtitle + "\n");
                        }
                    }
                    catch (Exception ex) { }
                }


                sb.Append("实缴现金：（          ）" + "\n");
                sb.Append("其他费用：（          ）" + "\n");
                sb.Append("长 短 款：（          ）" + "\n");
                sb.Append("签    字：" + "\n\n\n\n\n\n\n");


                PrintUtil.PrintStr(sb);
                //TODO  二维码

                PrintUtil.CloseDevice();

                return true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ERROR", "打印出现异常" + ex.Message);
                errormsg = "打印出现异常" + ex.Message;
                return false;
            }

        }


        //public static bool ReceiptQueryPrint(ReceiptQuery receipt, ref string errormsg)
        //{
        //    try
        //    {

        //        if (PrintUtil.OpenDevice() == -1)
        //        {
        //            errormsg = "打印机无法连接，请检查驱动和打印纸是否正常！";
        //            return false;
        //        }

        //        PrintUtil.SelectFond(0);

        //        PrintUtil.SetAlign(0);

        //        PrintUtil.SetFontSize(0, 0);
        //        StringBuilder sb = new StringBuilder();

        //        string Cashier = "收银员：" + receipt.cashier;

        //        string Serial = receipt.receiptdetail.serial.amount + receipt.receiptdetail.serial.title;
        //        sb.Append(Cashier + Serial.PadLeft(26 - Cashier.Length, ' ') + "\n");
        //        sb.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + (receipt.receiptdetail.devicecode.amount + receipt.receiptdetail.title).PadLeft(8, ' ') + "\n");
        //        sb.Append(receipt.receiptdetail.shiftcode.title + receipt.receiptdetail.shiftcode.amount.PadLeft(26 - receipt.receiptdetail.shiftcode.title.Length, ' ') + "\n");

        //        sb.Append("          " + receipt.receiptdetail.title.title + "\n");

        //        sb.Append("开始时间：  " + MainModel.GetDateTimeByStamp(receipt.starttime.ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "\n");
        //        sb.Append("结束时间：  " + MainModel.GetDateTimeByStamp(receipt.endtime.ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "\n");

        //        foreach (OrderPriceDetail basicinfo in receipt.receiptdetail.basicinfo)
        //        {
        //            sb.Append(basicinfo.title + basicinfo.amount.PadLeft(26 - basicinfo.title.Length, ' ') + "\n");

        //            if (!string.IsNullOrWhiteSpace(basicinfo.subtitle))
        //            {
        //                sb.Append(basicinfo.subtitle.PadLeft(26 - basicinfo.subtitle.Length, ' ') + "\n");
        //            }
        //        }

        //        sb.Append(DateTime.Now.ToString("-----------------------------" + "\n"));

        //        sb.Append(receipt.receiptdetail.totalamount.title + receipt.receiptdetail.totalamount.amount.PadLeft(26 - receipt.receiptdetail.totalamount.title.Length, ' ') + "\n");

        //        sb.Append(DateTime.Now.ToString("-----------------------------" + "\n"));



        //        foreach (OrderPriceDetail bottomdetail in receipt.receiptdetail.bottomdetails)
        //        {
        //            sb.Append(bottomdetail.title + bottomdetail.amount.PadLeft(26 - bottomdetail.title.Length, ' ') + "\n");

        //            if (!string.IsNullOrWhiteSpace(bottomdetail.subtitle))
        //            {
        //                sb.Append(bottomdetail.subtitle.PadLeft(26 - bottomdetail.subtitle.Length, ' ') + "\n");
        //            }
        //        }


        //        sb.Append("实缴现金：（          ）" + "\n");
        //        sb.Append("其他费用：（          ）" + "\n");
        //        sb.Append("长 短 款：（          ）" + "\n");
        //        sb.Append("签    字：" + "\n");


        //        PrintUtil.PrintStr(sb);
        //        //TODO  二维码

        //        PrintUtil.CloseDevice();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogManager.WriteLog("ERROR", "打印出现异常" + ex.Message);
        //        errormsg = "打印出现异常" + ex.Message;
        //        return false;
        //    }

        //}






          ///
        [DllImport("YkPosdll.dll")]
        public static extern int YkOpenDevice(int iport,int baud);
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
        static extern int YkSetCharSize(int m,int n);



        [DllImport("YkPosdll.dll")]
        static extern int YkPrintBarCode(int m,int n,[MarshalAs(UnmanagedType.LPStr)]StringBuilder szBmpFile);


        [DllImport("YkPosdll.dll")]
        static extern int YkSelectFont(int n);


        [DllImport("YkPosdll.dll")]
        static extern int YkSetInterCharSet(int n);

        [DllImport("YkPosdll.dll")]
        static extern int YkSetAlign(int n);

        public static int OpenDevice()
        {
            int i;

            int printport = Convert.ToInt16(INIManager.GetIni("System", "PrintPort", MainModel.IniPath));
            int printbaud = Convert.ToInt16(INIManager.GetIni("System", "PrintBaud", MainModel.IniPath));
            i = YkOpenDevice( printport, printbaud);
            
            return i;
        }

        public static int OpenCashBox()
        {
            int i;
            OpenDevice();
            i = SetCashBoxDriveMode();
            CloseDevice();
            return i;
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

        public static int PrintBitmap([MarshalAs(UnmanagedType.LPStr)]StringBuilder szBmpFile,int m)
        {
            int i;
            i = YkDownloadBitmapAndPrint(szBmpFile, m);
            return i;
        }
        public static int FeedPaper()
        {
            int i;
            i=YkFeedPaper();
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


        public static int PrintBarcode(int m,int n,[MarshalAs(UnmanagedType.LPStr)]StringBuilder szBmpFile)
        {
            YkSetBarCodeHeight(100);

            int i;
            i = YkPrintBarCode(m,n, szBmpFile);
            return i;
        }

        public static int SetFontSize(int m, int n)
        {

            int i;
            i = YkSetCharSize(m,n);
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
            i = YkSetCashBoxDriveMode(m,t1,t2);
            return i;
        }
    }
}
