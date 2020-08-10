using Maticsoft.BLL;
using Maticsoft.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.BrokenUI.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Common
{
    public class BunnyPrintUtil
    {
        private static string bottommessage = "";

        public static void PrintThread(object obj)
        {
            try
            {
                PrintDocument pd = (PrintDocument)obj;
                pd.Print();
                pd.Dispose();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("打印异常" + ex.Message);
            }
        }


        #region Windows 驱动打印

        /// <summary>
        /// 打印机宽度
        /// </summary>
        public static int PageSize = 80;
        /// <summary>
        /// 便宜量
        /// </summary>
        public static int LocationX = 0;
        /// <summary>
        /// 打印机名称
        /// </summary>
        public static string PrintName = "";

        /// <summary>
        /// 80 MM纸张下 一行能够打印的小字符数
        /// </summary>
        public static int BodyCharCountOfLine = 44;

        /// <summary>
        /// 80 MM纸张下 一行能够打印的大字符数
        /// </summary>
        public static int HeadCharCountOfLine = 38;

        /// <summary>
        /// 80 MM纸张下 小字符高度
        /// </summary>
        public static double BodyCharHeightOfLine = 16;

        /// <summary>
        /// 80 MM纸张下 大字符高度
        /// </summary>
        public static double HeadCharHeightOfLine = 20;


        public static Font fontHead;
        //  SizeF sizehead;

        public static Font fontBody;
        //SizeF sizeBody;

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

                    bottommessage = printdetail.bottommessage;
                    IniPrintSize();

                    // PageSize = PageSize - 10;
                    fontHead = new System.Drawing.Font("黑体", 11F * PageSize / 80);
                    HeadCharHeightOfLine = (double)HeadCharHeightOfLine * PageSize / 80;


                    fontBody = new System.Drawing.Font("黑体", 9F * PageSize / 80);

                    BodyCharHeightOfLine = (double)BodyCharHeightOfLine * PageSize / 80;

                    //每次打印先清空之前内容
                    lstPrintStr = new List<string>();

                    lstPrintStr.Add(MergeStr("欢迎光临[" + MainModel.CurrentShopInfo.tenantname + "]", "", HeadCharCountOfLine, PageSize));
                    lstPrintStr.Add(" ");

                    lstPrintStr.Add("小兔单号：" + printdetail.orderid + "\n");
                    lstPrintStr.Add("门店：" + MainModel.CurrentShopInfo.shopname + "\n");
                    lstPrintStr.Add("地址：" + MainModel.CurrentShopInfo.address + "\n");
                    //lstPrintStr.Add("电话：" + MainModel.CurrentShopInfo.tel + "\n");
                    lstPrintStr.Add("收银员：" + MainModel.CurrentUser.nickname);
                    lstPrintStr.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss" + "\n"));

                    lstPrintStr.Add(getStrLine("商品"));

                    lstPrintStr.AddRange(MergeStr("商品", "数量", "原价", "售价","实付金额", BodyCharCountOfLine));

                    foreach (ProductDetail pro in printdetail.products)
                    {
                        string num = pro.goodstagid == 0 ? pro.num.ToString() : pro.specnum.ToString();

                        lstPrintStr.AddRange (MergeStr(pro.title, num,pro.price.originprice.ToString("f2"), pro.price.saleprice.ToString("f2"), pro.price.total.ToString("f2"), BodyCharCountOfLine));
                    }
                    lstPrintStr.Add(getStrLine(""));
                    lstPrintStr.Add("件数：" + printdetail.productcount);

                    lstPrintStr.Add(MergeStr("", "", "原价合计", printdetail.productamount.ToString("f2"), BodyCharCountOfLine));
                    if (printdetail.orderpricedetails != null && printdetail.orderpricedetails.Length > 0)
                    {
                        foreach (OrderPriceDetail pointinfo in printdetail.orderpricedetails)
                            {
                                lstPrintStr.Add(MergeStr("","",pointinfo.title,pointinfo.amount,BodyCharCountOfLine));
                            }
                    }
                    lstPrintStr.Add("支付方式");
                    if (printdetail.paydetail != null && printdetail.paydetail.Length > 0)
                    {
                        foreach (Paydetail paydetail in printdetail.paydetail)
                        {
                            lstPrintStr.Add(MergeStr("", "", paydetail.title, paydetail.amount, BodyCharCountOfLine));
                        }
                    }

                    lstPrintStr.Add(getStrLine(""));

                    lstPrintStr.Add("多谢惠顾，欢迎下次光临！");
                    lstPrintStr.Add("客服电话："+printdetail.shopservicephone);
                    lstPrintStr.Add("*凭小票退货");



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

                    //pd.Print();

                    ParameterizedThreadStart Pts = new ParameterizedThreadStart(PrintThread);
                    Thread threadprint = new Thread(Pts);
                    threadprint.IsBackground = true;
                    threadprint.Start(pd);

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


        public static object lockprintingOffLine = new object();
        public static bool PrintOrder(string offlineorderid, bool isRefound, ref string errormsg)
        {
            lock (lockprintingOffLine)
            {
                try
                {
                    try { LogManager.WriteLog("打印离线订单:" + offlineorderid); }
                    catch { }

                    DBORDER_BEANBLL orderbll = new DBORDER_BEANBLL();
                    DBORDER_BEANMODEL order = orderbll.GetModel(offlineorderid);
                    if (order == null)
                    {
                        errormsg = "离线订单不存在" + offlineorderid;
                        return false;
                    }
                    OffLineOrder offlineorder = JsonConvert.DeserializeObject<OffLineOrder>(order.ORDER_JSON);


                    IniPrintSize();

                    fontHead = new System.Drawing.Font("黑体", 11F * PageSize / 80);
                    HeadCharHeightOfLine = (double)HeadCharHeightOfLine * PageSize / 80;


                    fontBody = new System.Drawing.Font("黑体", 9F * PageSize / 80);
                    BodyCharHeightOfLine = (double)BodyCharHeightOfLine * PageSize / 80;

                    //每次打印先清空之前内容
                    lstPrintStr = new List<string>();


                    lstPrintStr.Add(MergeStr("欢迎光临[" + MainModel.CurrentShopInfo.tenantname + "]", "", BodyCharCountOfLine, PageSize));
                    lstPrintStr.Add(" ");

                    lstPrintStr.Add("订单号：" + offlineorderid + "\n");
                    lstPrintStr.Add("门店：" + MainModel.CurrentShopInfo.shopname + "\n");
                    lstPrintStr.Add("地址：" + MainModel.CurrentShopInfo.address + "\n");
                    lstPrintStr.Add("电话：" + MainModel.CurrentShopInfo.tel + "\n");
                    lstPrintStr.Add(MergeStr("收银员：" + MainModel.CurrentUser.nickname, "机：" + MainModel.CurrentShopInfo.deviceid, BodyCharCountOfLine, PageSize));
                    lstPrintStr.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss" + "\n"));
                    lstPrintStr.Add(getStrLine(""));

                    foreach (Product pro in offlineorder.products)
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
                    lstPrintStr.Add(getStrLine(""));


                    if (isRefound)
                    {

                        lstPrintStr.Add(MergeStr("件数合计", offlineorder.goodscount.ToString(), BodyCharCountOfLine, PageSize));
                        lstPrintStr.Add(MergeStr("原价合计", offlineorder.origintotal.ToString("f2"), BodyCharCountOfLine, PageSize));
                        if (offlineorder.promoamt > 0)
                        {
                            lstPrintStr.Add(MergeStr("优惠金额", "-" + offlineorder.promoamt.ToString("f2"), BodyCharCountOfLine, PageSize));
                        }
                        if (offlineorder.fixpricepromoamt > 0)
                        {
                            lstPrintStr.Add(MergeStr("整单优惠", "-" + offlineorder.fixpricepromoamt.ToString("f2"), BodyCharCountOfLine, PageSize));
                        }

                        lstPrintStr.Add(MergeStr("退款金额", offlineorder.pricetotal.ToString("f2"), BodyCharCountOfLine, PageSize));
                    }
                    else
                    {
                        lstPrintStr.Add(MergeStr("件数合计", offlineorder.goodscount.ToString(), BodyCharCountOfLine, PageSize));
                        lstPrintStr.Add(MergeStr("原价合计", offlineorder.origintotal.ToString("f2"), BodyCharCountOfLine, PageSize));

                        if (offlineorder.promoamt > 0)
                        {
                            lstPrintStr.Add(MergeStr("优惠金额", "-" + offlineorder.promoamt.ToString("f2"), BodyCharCountOfLine, PageSize));
                        }
                        if (offlineorder.fixpricepromoamt > 0)
                        {
                            lstPrintStr.Add(MergeStr("整单优惠", "-" + offlineorder.fixpricepromoamt.ToString("f2"), BodyCharCountOfLine, PageSize));
                        }
                        lstPrintStr.Add(MergeStr("应收金额", offlineorder.pricetotal.ToString("f2"), BodyCharCountOfLine, PageSize));
                    }

                    lstPrintStr.Add(getStrLine(""));

                    if (isRefound)
                    {
                        lstPrintStr.Add("退款");
                    }


                    if (isRefound)
                    {
                        lstPrintStr.Add(MergeStr("现金退款", offlineorder.cashpayamt.ToString("f2"), BodyCharCountOfLine, PageSize));
                        lstPrintStr.Add(MergeStr("找零", offlineorder.cashchangeamt.ToString("f2"), BodyCharCountOfLine, PageSize));

                    }
                    else
                    {
                        lstPrintStr.Add(MergeStr("现金收银", offlineorder.cashpayamt.ToString("f2"), BodyCharCountOfLine, PageSize));
                        lstPrintStr.Add(MergeStr("找零", offlineorder.cashchangeamt.ToString("f2"), BodyCharCountOfLine, PageSize));
                    }

                    lstPrintStr.Add(getStrLine(""));

                    lstPrintStr.Add("多谢惠顾，欢迎下次光临！");

                    lstPrintStr.Add(MergeStr("  ", "", BodyCharCountOfLine, PageSize));

                    PrintDocument pd = new PrintDocument();

                    pd.PrintPage += new PrintPageEventHandler(pd_OrderPrintPage);


                    ////不弹框<正在打印>直接打印
                    PrintController PrintStandard = new StandardPrintController();
                    pd.PrintController = PrintStandard;

                    if (!string.IsNullOrEmpty(PrintName))
                    {
                        pd.PrinterSettings.PrinterName = PrintName;//选择打印机
                    }

                    //pd.Print();

                    ParameterizedThreadStart Pts = new ParameterizedThreadStart(PrintThread);
                    Thread threadprint = new Thread(Pts);
                    threadprint.IsBackground = true;
                    threadprint.Start(pd);

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

                    if (i == 0)
                    {
                        
                        e.Graphics.DrawString(str, fontHead, Brushes.Black, LocationX, LoactionY);

                    }
                    else
                    {
                        e.Graphics.DrawString(str, fontBody, Brushes.Black, LocationX, LoactionY);
                    }
                    LoactionY += (int)BodyCharHeightOfLine;

                }

                int picQrWidth = (int)Math.Round(PageSize / 0.254 * 50 / 100);

                //int picQrHeight = picHeadWidth * 2 / 5;
                int picLocationQr = (int)Math.Round(PageSize / 0.254 * 20 / 100);
                e.Graphics.DrawImage(Resources.ResourcePos.qrlogo, picLocationQr, LoactionY, picQrWidth, picQrWidth);
                LoactionY += picQrWidth;


                e.Graphics.DrawString(MergeStr(bottommessage,"",BodyCharCountOfLine,PageSize), fontBody, Brushes.Black, LocationX, LoactionY);
                LoactionY += (int)BodyCharHeightOfLine;

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

        #region 打印交班单
        public static object lockreceiptprint = new object();
        public static bool ReceiptPrint(Receiptdetail receipt, ref string errormsg)
        {
            lock (lockreceiptprint)
            {

                try
                {

                    IniPrintSize();
                    fontHead = new System.Drawing.Font("黑体", 11F * PageSize / 80,FontStyle.Bold);
                    //SizeF  sizehead = CreateGraphics().MeasureString("多个字可以减少字符间距误差微软多个字可以减少字符间距误差黑体", fontHead);
                    // HeadCharCountOfLine = HeadCharCountOfLine * 80 / PageSize;
                    HeadCharHeightOfLine = (double)HeadCharHeightOfLine * PageSize / 80;


                    fontBody = new System.Drawing.Font("黑体", 9F * PageSize / 80);
                    BodyCharHeightOfLine = (double)BodyCharHeightOfLine * PageSize / 80;


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

                    lstPrintStr.Add(getStrLine(""));

                    lstPrintStr.Add(MergeStr(receipt.totalamount.title, receipt.totalamount.amount, BodyCharCountOfLine, PageSize));
                    //lstPrintStr.Add(receipt.totalamount.title + receipt.totalamount.amount.PadLeft(39 - Encoding.Default.GetBytes(receipt.totalamount.title).Length, ' '));

                    foreach (OrderPriceDetail incomedetail in receipt.incomedetails)
                    {
                        try
                        {
                            decimal tempamount = 0;
                            try
                            {
                                tempamount = Convert.ToDecimal(incomedetail.amount);
                            }
                            catch { }
                            if (tempamount > 0)
                            {
                                lstPrintStr.Add(MergeStr(incomedetail.title, incomedetail.amount, BodyCharCountOfLine, PageSize));

                                if (!string.IsNullOrEmpty(incomedetail.subtitle))
                                {
                                    lstPrintStr.Add(MergeStr("", incomedetail.subtitle, BodyCharCountOfLine, PageSize));
                                }
                            }

                        }
                        catch (Exception ex) { }
                    }


                    lstPrintStr.Add(getStrLine(""));



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
                    // pd.Print();

                    ParameterizedThreadStart Pts = new ParameterizedThreadStart(PrintThread);
                    Thread threadprint = new Thread(Pts);
                    threadprint.IsBackground = true;
                    threadprint.Start(pd);

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
        public static void pd_ReceiptPrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {

                int LoactionY = 10; //标签头部预留10像素空白，防止打印机开头不走纸 打印不全
                for (int i = 0; i < lstPrintStr.Count; i++)
                {
                    string str = lstPrintStr[i];

                    //右对齐时  左边空格补充  但是会错半个字符？？？？？？
                    if (str.Length > 2 && str.Substring(0, 2) == "  " && PageSize == 80)
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

                    fontHead = new System.Drawing.Font("黑体", 11F * PageSize / 80,FontStyle.Bold);
                    HeadCharHeightOfLine = (double)HeadCharHeightOfLine * PageSize / 80;


                    fontBody = new System.Drawing.Font("黑体", 9F * PageSize / 80);
                    BodyCharHeightOfLine = (double)BodyCharHeightOfLine * PageSize / 80;
                    //每次打印先清空之前内容
                    lstPrintStr = new List<string>();

                    lstPrintStr.Add(MergeStr("报损单", "", BodyCharCountOfLine, PageSize));
                    lstPrintStr.Add(" ");

                    lstPrintStr.Add("收银员：" + MainModel.CurrentUser.nickname + "\n");
                    lstPrintStr.Add("报损时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n");
                    lstPrintStr.Add("报损门店：" + MainModel.CurrentShopInfo.tenantname + "\n");

                    lstPrintStr.Add(getStrLine(""));

                    foreach (Itemlist item in brokenresult.itemlist)
                    {
                        lstPrintStr.Add(DateTime.Now.ToString(item.skucode + "  " + item.skuname));

                        string priceandnum = "   " + item.deliveryprice.ToString("f2") + "  X  " + item.deliveryquantity;

                        lstPrintStr.Add(MergeStr(priceandnum, item.totalamount.ToString("f2") + "元", BodyCharCountOfLine, PageSize - 6));
                    }
                    lstPrintStr.Add(getStrLine(""));

                    lstPrintStr.Add(MergeStr("总计件数", brokenresult.totalqty.ToString(), BodyCharCountOfLine, PageSize));
                    lstPrintStr.Add(MergeStr("报损金额", brokenresult.totalamt.ToString("f2") + "元", BodyCharCountOfLine, PageSize));

                    PrintDocument pd = new PrintDocument();

                    pd.PrintPage += new PrintPageEventHandler(pd_BrokenPrintPage);

                    ////不弹框<正在打印>直接打印
                    PrintController PrintStandard = new StandardPrintController();
                    pd.PrintController = PrintStandard;

                    if (!string.IsNullOrEmpty(PrintName))
                    {
                        pd.PrinterSettings.PrinterName = PrintName;//选择打印机
                    }

                    //pd.Print();

                    ParameterizedThreadStart Pts = new ParameterizedThreadStart(PrintThread);
                    Thread threadprint = new Thread(Pts);
                    threadprint.IsBackground = true;
                    threadprint.Start(pd);

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

        /// <summary>
        /// 画图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void pd_BrokenPrintPage(object sender, PrintPageEventArgs e)
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


        #endregion

        //合并字符串使 用空格填补 以左右对齐
        public static string MergeStr(string str1, string str2, int charlength, int pagesize)
        {
            try
            {
                charlength = charlength - 2;
                string result = "";
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
                    result = " ".PadLeft(spacenum, ' ') + str2;
                }
                else //左右对齐 （中间填充空格）
                {
                    spacenum = charlength - Encoding.Default.GetBytes(str1 + str2).Length;
                    result = str1 + " ".PadLeft(spacenum, ' ') + str2;
                }
                return result;

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("计算字体异常" + ex.Message + ex.StackTrace);
                return str1 + str2;
            }
        }


        public static string getStrLine(string str)
        {
            try
            {

                  int spacenu = (BodyCharCountOfLine - Encoding.Default.GetBytes(str).Length)/2;
                  string shortline = "-".PadLeft(spacenu,'-');


                  return shortline + str + shortline;
            }
            catch
            {
                return "------------------------------";
            }
        }

        #endregion
        public static void IniPrintSize()
        {
            try
            {
                //每次使用初始化一次，否则纸张大于80 每次都会放大
                BodyCharCountOfLine = 44;
                HeadCharCountOfLine = 38;
                BodyCharHeightOfLine = 16;
                HeadCharHeightOfLine = 20;

                PageSize = 80;
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


        public static string MergeStr(string str1, string str2, string str3, string str4, int charlength)
        {
            try
            {
               
                //每行分成五份  商品2/5  数量、单价、总额各站1/5    四行数据中间三段空白
                int itemLength =Convert.ToInt16( Math.Round((decimal)charlength/5));

                str1 = GetItemString(str1, itemLength*2);
                str2 = GetItemString(str2, itemLength);
                str3 = GetItemString(str3, itemLength);
                string result = "";


                int spacenum1 = itemLength*2 - Encoding.Default.GetBytes(str1).Length;
                int spacenum2 = itemLength - Encoding.Default.GetBytes(str2).Length;
                int spacenum3 = itemLength - Encoding.Default.GetBytes(str3).Length;


                result = str1;
                if (spacenum1 > 0)
                {
                    result += " ".PadLeft(spacenum1, ' ');
                }

                result += str2;
                if (spacenum2 > 0)
                {
                    result += " ".PadLeft(spacenum2, ' ');
                }
                result += str3;
                if (spacenum3 > 0)
                {
                    result += " ".PadLeft(spacenum3, ' ');
                }

                result += str4;
                return result;

                //string result = str1 + str2.PadLeft(length - Encoding.Default.GetBytes(str1+str2).Length, ' ');

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("计算字体异常" + ex.Message + ex.StackTrace);
                return str1 + str2;
            }
        }

        public static List<string> MergeStr(string str1, string str2, string str3, string str4,string str5, int charlength)
        {
            try
            {

                //每行分成五份  商品2/5  数量、单价、总额各站1/5    四行数据中间三段空白
                int itemWidth = Convert.ToInt16(Math.Floor((decimal)charlength / 5));
                int NameWidth = charlength - itemWidth * 4;

                List<string> lstname = substr(str1,NameWidth-3);
                string tempstr1 = lstname[lstname.Count - 1];
                string tempstr2 = GetItemString(str2, itemWidth);
                string tempstr3 = GetItemString(str3, itemWidth);
                string tempstr4 = GetItemString(str4, itemWidth);
                string result = "";


                int spacenum1 = NameWidth - Encoding.Default.GetBytes(tempstr1).Length;
                int spacenum2 = itemWidth - Encoding.Default.GetBytes(tempstr2).Length;
                int spacenum3 = itemWidth - Encoding.Default.GetBytes(tempstr3).Length;
                int spacenum4 = itemWidth - Encoding.Default.GetBytes(tempstr4).Length;


                result = tempstr1;
                if (spacenum1 > 0)
                {
                    result += " ".PadLeft(spacenum1, ' ');
                }

                result += tempstr2;
                if (spacenum2 > 0)
                {
                    result += " ".PadLeft(spacenum2, ' ');
                }
                result += tempstr3;
                if (spacenum3 > 0)
                {
                    result += " ".PadLeft(spacenum3, ' ');
                }

                result += tempstr4;
                if (spacenum4 > 0)
                {
                    result += " ".PadLeft(spacenum3, ' ');
                }

                result += str5;

                lstname[lstname.Count - 1] = result;
                return lstname;

                //string result = str1 + str2.PadLeft(length - Encoding.Default.GetBytes(str1+str2).Length, ' ');

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("计算字体异常" + ex.Message + ex.StackTrace);
                return new List<string>() {str1 + str2+str3+str4+str5};
            }
        }


        public static string GetItemString(string str, int charlength)
        {
            try
            {
                string result = "";
                int strlength = Encoding.Default.GetBytes(str).Length;

                if (strlength >= charlength)
                {
                    result = str.Substring(0, charlength / 2);
                }
                else
                {
                    result += str.PadRight(charlength - strlength, ' ');
                }

                return result;
            }
            catch (Exception ex)
            {
                return str;
            }
        }

        private static List<string> substr(string str, int linelength)
        {
            try
            {
                string laststr = "";
                List<string> lstresult = new List<string>();
                int lastindex = 0;
                for (int i = 1; i <= str.Length; i++)
                {
                    string temp = str.Substring(lastindex, i - lastindex);
                    int templength = Encoding.Default.GetBytes(temp).Length;
                    if (templength >= linelength && (templength % linelength == 0 || templength % linelength == 1))
                    {
                        laststr = str.Substring(lastindex, i - lastindex);
                        lstresult.Add(laststr);
                        lastindex = i;
                    }
                }

                if (lastindex < str.Length)
                {
                    laststr = str.Substring(lastindex);
                    lstresult.Add(laststr);
                }

                return lstresult;

            }
            catch (Exception ex)
            {
                return new List<string>() { str};
            }
        }

    }
}
