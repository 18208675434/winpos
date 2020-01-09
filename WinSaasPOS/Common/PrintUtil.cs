using WinSaasPOS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Drawing.Printing;
using System.Drawing;
using System.Windows.Forms;

namespace WinSaasPOS.Common
{
    public class PrintUtil
    {
        #region Windows 驱动打印

        /// <summary>
        /// 打印机宽度
        /// </summary>
        public static  int PageSize = 58;
        /// <summary>
        /// 便宜量
        /// </summary>
        public static  int LocationX = 0;
        /// <summary>
        /// 打印机名称
        /// </summary>
        public static  string PrintName = "";


        /// <summary>
        /// 58 MM纸张下 一行能够打印的小字符数
        /// </summary>
        public static int BodyCharCountOfLine = 30;

        /// <summary>
        /// 58 MM纸张下 一行能够打印的大字符数
        /// </summary>
        public static int HeadCharCountOfLine = 28;

        /// <summary>
        /// 58 MM纸张下 小字符高度
        /// </summary>
        public static double BodyCharHeightOfLine = 14;

        /// <summary>
        /// 58 MM纸张下 大字符高度
        /// </summary>
        public static double HeadCharHeightOfLine = 16;


        public static Font fontHead;
        //  SizeF sizehead;

        public static Font fontBody;
        //SizeF sizeBody;




        #region Windows 驱动打印
        public static List<string> lstPrintStr = new List<string>();

        public static  object lockprinting = new object();
        public static  bool PrintOrder(PrintDetail printdetail, bool isRefound, ref string errormsg)
        {
            lock (lockprinting)
            {
                try
                {
                    try { LogManager.WriteLog("打印订单:" + printdetail.orderid); }
                    catch { }


                    try
                    {
                        PageSize = Convert.ToInt32(INIManager.GetIni("Print", "PageSize", MainModel.IniPath));

                        LocationX = Convert.ToInt32(INIManager.GetIni("Print", "LocationX", MainModel.IniPath));

                        PrintName = INIManager.GetIni("Print", "PrintName", MainModel.IniPath);
                    }
                    catch { }

                    // PageSize = PageSize - 10;
                    fontHead = new System.Drawing.Font("宋体", 11F * PageSize / 58);
                    //SizeF  sizehead = CreateGraphics().MeasureString("多个字可以减少字符间距误差微软多个字可以减少字符间距误差宋体", fontHead);
                    // HeadCharCountOfLine = HeadCharCountOfLine * 58 / PageSize;
                    HeadCharHeightOfLine = (double)HeadCharHeightOfLine * PageSize / 58;


                    fontBody = new System.Drawing.Font("宋体", 8.5F * PageSize / 58);
                    // SizeF sizeBody = CreateGraphics().MeasureString("多个字可以减少字符间距误差微软多个字可以减少字符间距误差宋体", fontBody);
                    // BodyCharCountOfLine = BodyCharCountOfLine * 58 / PageSize;
                    BodyCharHeightOfLine = (double)BodyCharHeightOfLine * PageSize / 58;
                    // int length = (int)Math.Floor(58 / (0.254 * sizeBody.Width) * 2 * 30) - 8;  //-8 预留两个汉字空白

                    //每次打印先清空之前内容
                    lstPrintStr = new List<string>();


                    lstPrintStr.Add(MergeStr("欢迎光临[" + MainModel.CurrentShopInfo.tenantname + "]", "", BodyCharCountOfLine, PageSize));
                    lstPrintStr.Add(" ");

                    lstPrintStr.Add("订单号：" + printdetail.orderid + "\n");
                    lstPrintStr.Add("门店：" + MainModel.CurrentShopInfo.shopname + "\n");
                    lstPrintStr.Add("地址：" + MainModel.CurrentShopInfo.address + "\n");
                    lstPrintStr.Add("电话：" + MainModel.CurrentShopInfo.tel + "\n");
                    lstPrintStr.Add(MergeStr("收银员：" + MainModel.CurrentUser.nickname, "机：" + MainModel.CurrentShopInfo.deviceid, BodyCharCountOfLine, PageSize));
                    lstPrintStr.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss" + "\n"));
                    lstPrintStr.Add(getStrLine());

                    foreach (ProductDetail pro in printdetail.products)
                    {
                        lstPrintStr.Add(DateTime.Now.ToString(pro.skucode + "  " + pro.title));
                        if (pro.goodstagid == 0)//标品
                        {
                            string priceandnum = "   " + pro.price.saleprice.ToString("f2") + "  X  " + pro.num;

                            //lstPrintStr.Add(priceandnum + pro.price.total.ToString().PadLeft(length - 2 - priceandnum.Length, ' '));

                            lstPrintStr.Add(MergeStr(priceandnum, pro.price.total.ToString("f2"), BodyCharCountOfLine, PageSize - 6));
                        }
                        else if (pro.goodstagid == 1) //散称称重
                        {
                            string priceandnum = "   " + pro.price.saleprice.ToString("f2") + "  X  " + pro.specnum;

                            //lstPrintStr.Add(priceandnum + pro.price.total.ToString().PadLeft(length - 2 - priceandnum.Length, ' '));

                            lstPrintStr.Add(MergeStr(priceandnum, pro.price.total.ToString("f2"), BodyCharCountOfLine, PageSize - 6));
                        }
                        else if (pro.goodstagid == 2) //多规格原称重
                        {

                            string priceandnum = "   " + pro.price.saleprice.ToString("f2") + "  X  " + pro.specnum;
                            // lstPrintStr.Add(priceandnum + pro.price.total.ToString().PadLeft(length - 2 - priceandnum.Length, ' '));
                            lstPrintStr.Add(MergeStr(priceandnum, pro.price.total.ToString("f2"), BodyCharCountOfLine, PageSize - 6));
                        }

                    }
                    lstPrintStr.Add(getStrLine());
                    if (printdetail.orderpricedetails != null && printdetail.orderpricedetails.Length > 0)
                    {


                        //汉字占两位 TODO 判断前面汉字和英文数字
                        foreach (OrderPriceDetail pricedetail in printdetail.orderpricedetails)
                        {
                            if (isRefound)
                            {
                                //lstPrintStr.Add(pricedetail.title.Replace("应收", "退款") + pricedetail.amount.PadLeft(length - Encoding.Default.GetBytes(pricedetail.title).Length, ' '));

                                lstPrintStr.Add(MergeStr(pricedetail.title.Replace("应收", "退款"), pricedetail.amount, BodyCharCountOfLine, PageSize));
                            }
                            else
                            {
                                //lstPrintStr.Add(pricedetail.title + pricedetail.amount.PadLeft(length - Encoding.Default.GetBytes(pricedetail.title).Length, ' '));
                                lstPrintStr.Add(MergeStr(pricedetail.title, pricedetail.amount, BodyCharCountOfLine, PageSize));
                            }

                        }

                        lstPrintStr.Add(getStrLine());
                    }
                    if (isRefound)
                    {
                        lstPrintStr.Add("退款");
                    }

                    foreach (Paydetail paydetail in printdetail.paydetail)
                    {
                        if (isRefound)
                        {
                            //lstPrintStr.Add(paydetail.title.Replace("支付", "退款").Replace("退款宝", "支付宝") + paydetail.amount.PadLeft(28 - paydetail.title.Length * 2, ' '));
                            lstPrintStr.Add(MergeStr(paydetail.title.Replace("支付", "退款").Replace("退款宝", "支付宝"), paydetail.amount, BodyCharCountOfLine, PageSize));
                        }
                        else
                        {
                            // lstPrintStr.Add(paydetail.title + paydetail.amount.PadLeft(length - paydetail.title.Length * 2, ' '));
                            lstPrintStr.Add(MergeStr(paydetail.title, paydetail.amount, BodyCharCountOfLine, PageSize));
                        }
                    }

                    if (printdetail.pointinfo != null && printdetail.pointinfo.Length > 0)
                    {

                        lstPrintStr.Add(getStrLine());
                        foreach (PointInfo pointinfo in printdetail.pointinfo)
                        {

                            //lstPrintStr.Add(pointinfo.title + pointinfo.amount.PadLeft(length - Encoding.Default.GetBytes(pointinfo.title).Length, ' '));
                            lstPrintStr.Add(MergeStr(pointinfo.title, pointinfo.amount, BodyCharCountOfLine, PageSize));
                        }
                    }

                    lstPrintStr.Add(getStrLine());

                    lstPrintStr.Add("多谢惠顾，欢迎下次光临！");
  




                    // StringBuilder strEnd = new StringBuilder("新鲜·便捷·优选 100%退换货\n\n\n\n\n\n");


                    PrintDocument pd = new PrintDocument();

                    pd.PrintPage += new PrintPageEventHandler(pd_OrderPrintPage);


                    ////不弹框<正在打印>直接打印
                    PrintController PrintStandard = new StandardPrintController();
                    pd.PrintController = PrintStandard;

                    if (!string.IsNullOrEmpty(PrintName))
                    {
                        pd.PrinterSettings.PrinterName = PrintName;//选择打印机
                    }

                    pd.Print();

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




        public static object lockreceiptprint = new object();
        public static bool ReceiptPrint(Receiptdetail receipt, ref string errormsg)
        {
            lock (lockreceiptprint)
            {

                try
                {

                    try
                    {
                        PageSize = Convert.ToInt32(INIManager.GetIni("Print", "PageSize", MainModel.IniPath));

                        LocationX = Convert.ToInt32(INIManager.GetIni("Print", "LocationX", MainModel.IniPath));

                        PrintName = INIManager.GetIni("Print", "PrintName", MainModel.IniPath);
                    }
                    catch { }
                    fontHead = new System.Drawing.Font("宋体", 11F * PageSize / 58);
                    //SizeF  sizehead = CreateGraphics().MeasureString("多个字可以减少字符间距误差微软多个字可以减少字符间距误差宋体", fontHead);
                    // HeadCharCountOfLine = HeadCharCountOfLine * 58 / PageSize;
                    HeadCharHeightOfLine = (double)HeadCharHeightOfLine * PageSize / 58;


                    fontBody = new System.Drawing.Font("宋体", 8.5F * PageSize / 58);
                    // SizeF sizeBody = CreateGraphics().MeasureString("多个字可以减少字符间距误差微软多个字可以减少字符间距误差宋体", fontBody);
                    // BodyCharCountOfLine = BodyCharCountOfLine * 58 / PageSize;
                    BodyCharHeightOfLine = (double)BodyCharHeightOfLine * PageSize / 58;
                    // int length = (int)Math.Floor(58 / (0.254 * sizeBody.Width) * 2 * 30) - 8;  //-8 预留两个汉字空白


                    // int length = (int)Math.Floor(58 / (0.254 * sizeBody.Width) * 2 * 30) - 8;  //-8 预留两个汉字空白


                    lstPrintStr = new List<string>();

                    string Cashier = "收银员：" + receipt.cashier;
                    string Serial = receipt.serial.amount + receipt.serial.title;
                    lstPrintStr.Add(MergeStr(Cashier, Serial, BodyCharCountOfLine, PageSize));
                    lstPrintStr.Add(MergeStr(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), (receipt.devicecode.amount + receipt.devicecode.title), BodyCharCountOfLine, PageSize));
                    lstPrintStr.Add(MergeStr(receipt.shiftcode.title, receipt.shiftcode.amount, BodyCharCountOfLine, PageSize));

                    lstPrintStr.Add(MergeStr(receipt.title.title, "", BodyCharCountOfLine, PageSize));

                    lstPrintStr.Add(MergeStr("开始时间：", MainModel.GetDateTimeByStamp(receipt.starttime.ToString()).ToString("yyyy-MM-dd HH:mm:ss"), BodyCharCountOfLine, PageSize));
                    lstPrintStr.Add(MergeStr("结束时间：", MainModel.GetDateTimeByStamp(receipt.endtime.ToString()).ToString("yyyy-MM-dd HH:mm:ss"), BodyCharCountOfLine, PageSize));

                    foreach (OrderPriceDetail basicinfo in receipt.basicinfo)
                    {
                        try
                        {
                            lstPrintStr.Add(MergeStr(basicinfo.title, basicinfo.amount, BodyCharCountOfLine, PageSize));

                            if (!string.IsNullOrEmpty(basicinfo.subtitle))
                            {
                                lstPrintStr.Add(MergeStr("", basicinfo.subtitle, BodyCharCountOfLine, PageSize));
                            }
                        }
                        catch (Exception ex) { }
                    }

                    lstPrintStr.Add(getStrLine());

                    lstPrintStr.Add(receipt.totalamount.title + receipt.totalamount.amount.PadLeft(28 - Encoding.Default.GetBytes(receipt.totalamount.title).Length, ' '));

                    lstPrintStr.Add(getStrLine());



                    foreach (OrderPriceDetail bottomdetail in receipt.bottomdetails)
                    {
                        try
                        {
                            lstPrintStr.Add(MergeStr(bottomdetail.title, bottomdetail.amount, BodyCharCountOfLine, PageSize));

                            if (!string.IsNullOrEmpty(bottomdetail.subtitle))
                            {

                                //lstPrintStr.Add("".PadLeft(spacenum2, ' ') + bottomdetail.subtitle);
                                lstPrintStr.Add(MergeStr("", bottomdetail.subtitle.Trim(), BodyCharCountOfLine, PageSize));
                            }
                        }
                        catch (Exception ex) { }
                    }


                    lstPrintStr.Add("实缴现金：（          ）");
                    lstPrintStr.Add("其他费用：（          ）");
                    lstPrintStr.Add("长 短 款：（          ）");
                    lstPrintStr.Add("签    字：");



                    PrintDocument pd = new PrintDocument();
                    pd.PrintPage += new PrintPageEventHandler(pd_ReceiptPrintPage);


                    ////不弹框<正在打印>直接打印
                    PrintController PrintStandard = new StandardPrintController();
                    pd.PrintController = PrintStandard;

                    if (!string.IsNullOrEmpty(PrintName))
                    {
                        pd.PrinterSettings.PrinterName = PrintName;//选择打印机
                    }
                    pd.Print();

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




        /// <summary>
        /// 画图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void pd_OrderPrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {

                int LoactionY = 10; //标签头部预留10像素空白，防止打印机开头不走纸 打印不全

                for (int i = 0; i < lstPrintStr.Count; i++)
                {
                    string str = lstPrintStr[i];

                    e.Graphics.DrawString(str, fontBody, Brushes.Black, LocationX, LoactionY);
                    LoactionY += (int)BodyCharHeightOfLine;

                }

                //结尾留下20像素空白，防止打印机不走空白纸 撕纸不全    空白不会打印，打印内容不再纸张范围也不会走纸，所以打印一个点用来走纸
                e.Graphics.DrawString(".", fontBody, Brushes.Black, 0, LoactionY + 20);


                Application.DoEvents();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("画打印标签图片异常" + ex.Message);
            }
        }


        /// <summary>
        /// 画图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void pd_ReceiptPrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {

                int LoactionY = 10; //标签头部预留10像素空白，防止打印机开头不走纸 打印不全
                for (int i = 0; i < lstPrintStr.Count; i++)
                {
                    string str = lstPrintStr[i];
                   
                        //右对齐时  左边空格补充  但是会错半个字符？？？？？？
                        if (str.Length > 2 && str.Substring(0, 2) == "  " && PageSize == 58)
                        {
                            e.Graphics.DrawString(str, fontBody, Brushes.Black, LocationX - 3, LoactionY);
                        }
                        else
                        {
                            e.Graphics.DrawString(str, fontBody, Brushes.Black, LocationX, LoactionY);
                        }

                        LoactionY += (int)BodyCharHeightOfLine;

                    
                }

                //结尾留下20像素空白，防止打印机不走空白纸 撕纸不全    空白不会打印，打印内容不再纸张范围也不会走纸，所以打印一个点用来走纸
                e.Graphics.DrawString(".", fontBody, Brushes.Black, 0, LoactionY + 20);


                Application.DoEvents();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("画打印标签图片异常" + ex.Message);
            }
        }

        #endregion


        //合并字符串使 用空格填补 以左右对齐
        public static  string MergeStr(string str1, string str2, int charlength, int pagesize)
        {
            try
            {
                string result = "";


                //一行长度可以放多少个字符                                       
                // int length = (int)Math.Floor(pagesize / (0.254 * sizeftemp.Width) * 2 * 30);  //-6 预留三个汉字空白

                int spacenum = 0;

                //str2为空：str1 居中      str1为空：tr2 右对齐     一行文本左对齐不需要该方法
                if (string.IsNullOrEmpty(str2)) //居中
                {
                    spacenum = (charlength - Encoding.Default.GetBytes(str1).Length) / 2;
                    result = " ".PadLeft(spacenum, ' ') + str1;
                }
                else if (string.IsNullOrEmpty(str1)) //右对齐
                {
                    //右对齐会错位半个字符？？？？？？？？
                    spacenum = charlength - Encoding.Default.GetBytes(str1 + str2).Length;
                    result = str1.PadLeft(spacenum, ' ') + str2;
                }
                else //左右对齐 （中间填充空格）
                {
                    spacenum = charlength - Encoding.Default.GetBytes(str1 + str2).Length;
                    result = str1 + " ".PadLeft(spacenum, ' ') + str2;
                }
                return result;

                //string result = str1 + str2.PadLeft(length - Encoding.Default.GetBytes(str1+str2).Length, ' ');

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("计算字体异常" + ex.Message + ex.StackTrace);
                return str1 + str2;
            }
        }

        public static string getStrLine()
        {
            try
            {
                //SizeF sizeftemp = CreateGraphics().MeasureString("多个字可以减少字符间距误差微软多个字可以减少字符间距误差宋体", fontBody);

                //一行长度可以放多少个字符                                       
                //  int length = (int)Math.Floor(PageSize / (0.254 * sizeBody.Width) * 2 * 30) ;  //-6 预留三个汉字空白

                return "-".PadLeft(BodyCharCountOfLine, '-');
            }
            catch
            {
                return "------------------------------";
            }
        }



        #endregion



        // #region

        //public static bool SEDPrint(PrintDetail printdetail, bool isRefound, ref string errormsg)
        //{
        //    try
        //    {
        //        try { LogManager.WriteLog("打印订单:" + printdetail.orderid); }
        //        catch { }

        //        //test
        //        //return true;
        //        //string logofilepath = AppDomain.CurrentDomain.BaseDirectory + "\\headlogo.bmp";
        //        //StringBuilder logobuilder = new StringBuilder(logofilepath);
        //        if (PrintUtil.OpenDevice() == -1)
        //        {
        //            errormsg = "打印机无法连接，请检查驱动和打印纸是否正常！";
        //            return false;
        //        }

        //        PrintUtil.SelectFond(0);
        //        PrintUtil.SetInterCharSet(15);
        //        // MainModel.CurrentShopInfo
        //        PrintUtil.SetAlign(0);
        //        PrintUtil.SetFontSize(0, 1);
        //        StringBuilder stringb = new StringBuilder("    欢迎光临[" + MainModel.CurrentShopInfo.tenantname + "]\n");
        //        PrintUtil.PrintStr(stringb);
        //        PrintUtil.SelectFond(0);
        //        PrintUtil.SetFontSize(0, 0);
        //        PrintUtil.FeedPaper();
        //        StringBuilder sb = new StringBuilder();
        //        sb.Append("订单号：" + printdetail.orderid + "\n");
        //        sb.Append("门店：" + MainModel.CurrentShopInfo.shopname + "\n");
        //        sb.Append("地址：" + MainModel.CurrentShopInfo.address + "\n");
        //        sb.Append("电话：" + MainModel.CurrentShopInfo.tel + "\n");
        //        sb.Append("收银员：" + MainModel.CurrentUser.nickname + "         机：" + MainModel.CurrentShopInfo.deviceid + "\n");
        //        sb.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss" + "\n"));
        //        sb.Append(DateTime.Now.ToString("-----------------------------" + "\n"));
        //        foreach (ProductDetail pro in printdetail.products)
        //        {
        //            sb.Append(DateTime.Now.ToString(pro.skucode + "  " + pro.title + "\n"));
        //            if (pro.goodstagid == 0)//标品
        //            {
        //                string priceandnum = "   " + pro.num + "  X  " + pro.price.saleprice;

        //                sb.Append(DateTime.Now.ToString(priceandnum + pro.price.total.ToString().PadLeft(26 - priceandnum.Length, ' ') + "\n"));

        //            }
        //            else if (pro.goodstagid == 1) //散称称重
        //            {
        //                string priceandnum = "   " + pro.specnum + "  X  " + pro.price.saleprice;
        //                sb.Append(DateTime.Now.ToString(priceandnum + pro.price.total.ToString().PadLeft(26 - priceandnum.Length, ' ') + "\n"));
        //            }
        //            else if (pro.goodstagid == 2) //多规格原称重
        //            {
        //                string priceandnum = "   " + pro.specnum + "  X  " + pro.price.saleprice;
        //                sb.Append(DateTime.Now.ToString(priceandnum + pro.price.total.ToString().PadLeft(26 - priceandnum.Length, ' ') + "\n"));
        //            }

        //        }
        //        sb.Append(DateTime.Now.ToString("-----------------------------" + "\n"));

        //        //汉字占两位 TODO 判断前面汉字和英文数字
        //        foreach (Orderpricedetail pricedetail in printdetail.orderpricedetails)
        //        {
        //            if (isRefound)
        //            {
        //                sb.Append(pricedetail.title.Replace("应收", "退款") + pricedetail.amount.PadLeft(28 - Encoding.Default.GetBytes(pricedetail.title).Length, ' ') + "\n");

        //            }
        //            else
        //            {
        //                sb.Append(pricedetail.title + pricedetail.amount.PadLeft(28 - Encoding.Default.GetBytes(pricedetail.title).Length, ' ') + "\n");

        //            }

        //        }
        //        sb.Append(DateTime.Now.ToString("-----------------------------" + "\n"));


        //        if (isRefound)
        //        {
        //            sb.Append("退款" + "\n");
        //        }

        //        foreach (Paydetail paydetail in printdetail.paydetail)
        //        {
        //            if (isRefound)
        //            {
        //                sb.Append(paydetail.title.Replace("支付", "退款").Replace("退款宝", "支付宝") + paydetail.amount.PadLeft(28 - paydetail.title.Length * 2, ' ') + "\n");
        //            }
        //            else
        //            {
        //                sb.Append(paydetail.title + paydetail.amount.PadLeft(28 - paydetail.title.Length * 2, ' ') + "\n");
        //            }
        //        }

        //        if (printdetail.pointinfo != null && printdetail.pointinfo.Length > 0)
        //        {

        //            sb.Append(DateTime.Now.ToString("-----------------------------" + "\n"));
        //            foreach (PointInfo pointinfo in printdetail.pointinfo)
        //            {

        //                sb.Append(pointinfo.title + pointinfo.amount.PadLeft(28 - Encoding.Default.GetBytes(pointinfo.title).Length, ' ') + "\n");
        //            }
        //        }

        //        sb.Append(DateTime.Now.ToString("-----------------------------" + "\n"));
        //        sb.Append("    多谢惠顾，欢迎下次光临！" + "\n");
        //        sb.Append("\n\n\n\n\n");


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


        //public static bool ReceiptPrint(Receiptdetail receipt, ref string errormsg)
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
        //        string Serial = receipt.serial.amount + receipt.serial.title;
        //        sb.Append(Cashier+Serial.PadLeft(28-Cashier.Length*2,' ') + "\n");
        //        sb.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +(receipt.devicecode.amount+receipt.title.amount).PadLeft(4,' ') + "\n");
        //        sb.Append(receipt.shiftcode.title +receipt.shiftcode.amount.PadLeft(28-receipt.shiftcode.title.Length,' ')+ "\n");

        //        sb.Append( "          "+receipt.title.title + "\n");

        //        sb.Append("开始时间：  " + MainModel.GetDateTimeByStamp(receipt.starttime.ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "\n");
        //        sb.Append("结束时间：  " + MainModel.GetDateTimeByStamp(receipt.endtime.ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "\n");

        //        foreach (OrderPriceDetail basicinfo in receipt.basicinfo)
        //        {
        //            try
        //            {
        //                sb.Append(basicinfo.title + basicinfo.amount.PadLeft(28 - Encoding.Default.GetBytes(basicinfo.title).Length, ' ') + "\n");

        //                if (!string.IsNullOrEmpty(basicinfo.subtitle))
        //                {
        //                    sb.Append(basicinfo.subtitle.PadLeft(28 - Encoding.Default.GetBytes(basicinfo.subtitle).Length, ' ') + "\n");
        //                }
        //            }
        //            catch (Exception ex) { }
        //        }

        //        sb.Append(DateTime.Now.ToString("-----------------------------" + "\n"));

        //        sb.Append(receipt.totalamount.title + receipt.totalamount.amount.PadLeft(28 - Encoding.Default.GetBytes(receipt.totalamount.title).Length, ' ') + "\n");

        //        sb.Append(DateTime.Now.ToString("-----------------------------" + "\n"));



        //        foreach (OrderPriceDetail bottomdetail in receipt.bottomdetails)
        //        {
        //            try
        //            {
        //                int spacenum = 28 - Encoding.Default.GetBytes(bottomdetail.title).Length - Encoding.Default.GetBytes(bottomdetail.amount).Length;
        //                sb.Append(bottomdetail.title + "".PadLeft(spacenum, ' ') + bottomdetail.amount + "\n");

        //                if (!string.IsNullOrEmpty(bottomdetail.subtitle))
        //                {
        //                    int spacenum2 = 28 - Encoding.Default.GetBytes(bottomdetail.subtitle).Length;

        //                    sb.Append("".PadLeft(spacenum2, ' ') + bottomdetail.subtitle + "\n");
        //                }
        //            }
        //            catch (Exception ex) { }
        //        }


        //        sb.Append("实缴现金：（          ）" + "\n");
        //        sb.Append("其他费用：（          ）" + "\n");
        //        sb.Append("长 短 款：（          ）" + "\n");
        //        sb.Append("签    字：" + "\n\n\n\n\n\n\n");


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




        #region  桑达打印机API

        ///
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

        public static int OpenDevice()
        {
            int i;

            int printport = Convert.ToInt16(INIManager.GetIni("System", "PrintPort", MainModel.IniPath));
            int printbaud = Convert.ToInt16(INIManager.GetIni("System", "PrintBaud", MainModel.IniPath));
            i = YkOpenDevice(printport, printbaud);

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
            i = YkDownloadBitmapAndPrint(szBmpFile, m);
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

        #endregion
    }
}
