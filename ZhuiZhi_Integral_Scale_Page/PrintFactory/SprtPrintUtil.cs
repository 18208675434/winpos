
// 斯普瑞特打印机 设置打印字体 打印第二次才会生效 所以单据不做打印字体变更

//打印内容每行用回车换行隔开 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.BrokenUI.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.PrintFactory
{
   public class SprtPrintUtil
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
           public static int BodyCharCountOfLine = 30;

           /// <summary>
           /// 58 MM纸张下 一行能够打印的大字符数
           /// </summary>
           public static int HeadCharCountOfLine = 25;

           public static List<string> lstPrintStr = new List<string>();



           private const Int32 POS_PT_COM = 1000;
           private const Int32 POS_PT_LPT = 1001;
           private const Int32 POS_PT_USB = 1002;
           private const Int32 POS_PT_NET = 1003;


           private const Int32 POS_ES_INVALIDPARA = -1; //参数错误
           private const Int32 POS_ES_WRITEFAIL = -2; //写失败
           private const Int32 POS_ES_READFAIL = -3; //读失败
           private const Int32 POS_ES_NONMONOCHROMEBITMAP = -4; //非单色位图
           private const Int32 POS_ES_OVERTIME = -5; //超时/写超时/读超时/打印未完成
           private const Int32 POS_ES_FILEOPENERROR = -6; //文件/图片打开失败
           private const Int32 POS_ES_OTHERERRORS = -100; //其他原因导致的错误

           private const Int32 POS_ES_SUCCESS = 0; //成功/发送成功/状态正常/打印完成


           [DllImport("POS_SDK.dll", CharSet = CharSet.Ansi, EntryPoint = "POS_Port_OpenA")]
           static extern Int32 POS_Port_OpenA(String lpName, Int32 iPort, bool bFile, String path);

           [DllImport("POS_SDK.dll", CharSet = CharSet.Ansi, EntryPoint = "POS_Port_Close")]
           static extern Int32 POS_Port_Close(Int32 printID);

           [DllImport("POS_SDK.dll", CharSet = CharSet.Ansi, EntryPoint = "POS_Output_PrintFontStringA")]
           static extern Int32 POS_Output_PrintFontStringA(Int32 printID, Int32 iFont, Int32 iThick, Int32 iWidth, Int32 iHeight, Int32 iUnderLine, String lpString);


           [DllImport("POS_SDK.dll", CharSet = CharSet.Ansi, EntryPoint = "POS_Control_ReSet")]
           static extern Int32 POS_Control_ReSet(Int32 printID);

           [DllImport("POS_SDK.dll", CharSet = CharSet.Ansi, EntryPoint = "POS_Control_SetPrintFontC")]
           static extern Int32 POS_Control_SetPrintFontC(Int32 printID, bool iDoubleWidth, bool iDoubleHeight, bool iUnderLine);

           [DllImport("POS_SDK.dll", CharSet = CharSet.Ansi, EntryPoint = "POS_Control_CashDraw")]
           static extern Int32 POS_Control_CashDraw(Int32 printID, Int32 iNum, Int32 time1, Int32 time2);


           [DllImport("POS_SDK.dll", CharSet = CharSet.Ansi, EntryPoint = "POS_Control_AlignType")]
           static extern Int32 POS_Control_AlignType(Int32 printID, Int32 iAlignType);

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


                       m_hPrinter = POS_Port_OpenA("LPT1:", POS_PT_LPT, false, "");
                        POS_Control_AlignType(m_hPrinter, 1);  //设置右面对齐
                       POS_Output_PrintFontStringA(m_hPrinter, 0, 0, 0, 1, 0,"欢迎光临"+"\r\n");

                       POS_Output_PrintFontStringA(m_hPrinter, 0, 0, 0, 1, 0, MainModel.CurrentShopInfo.tenantname + "\r\n");

                       POS_Control_ReSet(m_hPrinter);

                       POS_Port_Close(m_hPrinter);  //打印之后必须关闭 否则打印内容多会打印不全
                      
                       //lstPrintStr.Add(MergeStr("欢迎光临", "", BodyCharCountOfLine, PageSize) );
                       //lstPrintStr.Add(MergeStr(MainModel.CurrentShopInfo.tenantname, "", BodyCharCountOfLine, PageSize));
                        lstPrintStr.Add(" ");

                       lstPrintStr.Add("订单号：" + printdetail.orderid );
                       lstPrintStr.Add("门店：" + MainModel.CurrentShopInfo.shopname );
                       lstPrintStr.Add("地址：" + MainModel.CurrentShopInfo.address );
                       lstPrintStr.Add("门店电话：" + MainModel.CurrentShopInfo.tel );
                       //lstPrintStr.Add(MergeStr("收银员：" + MainModel.CurrentUser.nickname, "机：" + MainModel.CurrentShopInfo.deviceid, BodyCharCountOfLine, PageSize));
                       lstPrintStr.Add("订单时间：" + MainModel.GetDateTimeByStamp(printdetail.orderat).ToString("yyyy-MM-dd HH:mm:ss" ));
                       lstPrintStr.Add(getStrLine());

                       foreach (ProductDetail pro in printdetail.products)
                       {
                           lstPrintStr.Add(pro.skucode + "  " + pro.title);
                           if (pro.goodstagid == 0)//标品
                           {
                               string priceandnum = "   " + pro.price.originprice.ToString("f2") + "元" + "  X  " + pro.num + pro.price.unit;

                               lstPrintStr.Add(MergeStr(priceandnum, pro.price.origintotal.ToString("f2"), BodyCharCountOfLine, PageSize - 8));
                           }
                           else if (pro.goodstagid == 1) //散称称重
                           {
                               string priceandnum = "   " + pro.price.originprice.ToString("f2") + "元" + "  X  " + pro.specnum + pro.price.unit;

                               //lstPrintStr.Add(priceandnum + pro.price.total.ToString().PadLeft(length - 2 - priceandnum.Length, ' '));

                               lstPrintStr.Add(MergeStr(priceandnum, pro.price.origintotal.ToString("f2"), BodyCharCountOfLine, PageSize - 8));
                           }
                           else if (pro.goodstagid == 2) //多规格原称重
                           {

                               string priceandnum = "   " + pro.price.originprice.ToString("f2") + "元" + "  X  " + pro.specnum + pro.price.unit;
                               // lstPrintStr.Add(priceandnum + pro.price.total.ToString().PadLeft(length - 2 - priceandnum.Length, ' '));
                               lstPrintStr.Add(MergeStr(priceandnum, pro.price.origintotal.ToString("f2"), BodyCharCountOfLine, PageSize - 8));
                           }

                           if (pro.price.promoamt > 0)
                           {
                               lstPrintStr.Add(MergeStr("", "优惠-" + pro.price.promoamt.ToString("f2"), BodyCharCountOfLine, PageSize - 6));
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

                       IniPrintSize();

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

                       lstPrintStr.Add(MergeStr(receipt.totalamount.title, receipt.totalamount.amount, BodyCharCountOfLine, PageSize));
                       //lstPrintStr.Add(receipt.totalamount.title + receipt.totalamount.amount.PadLeft(28 - Encoding.Default.GetBytes(receipt.totalamount.title).Length, ' '));

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


                       PrintTextByPaperWidth(lstPrintStr);

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

                      // lstPrintStr.Add(MergeStr("报损单", "", BodyCharCountOfLine, PageSize));

                       m_hPrinter = POS_Port_OpenA("LPT1:", POS_PT_LPT, false, "");
                       POS_Control_AlignType(m_hPrinter, 1);  //设置左对齐
                       POS_Output_PrintFontStringA(m_hPrinter, 0, 0, 0, 1, 0, "报损单" + "\r\n");

                       POS_Control_ReSet(m_hPrinter);

                       POS_Port_Close(m_hPrinter);  //打印之后必须关闭 否则打印内容多会打印不全

                       // lstPrintStr.Add(MergeStr("报损单", "", BodyCharCountOfLine, PageSize));
                       lstPrintStr.Add(" ");

                       lstPrintStr.Add("收银员：" + MainModel.CurrentUser.nickname );
                       lstPrintStr.Add("报损时间：" + MainModel.GetDateTimeByStamp(brokenresult.createdat).ToString("yyyy-MM-dd HH:mm:ss") );
                       lstPrintStr.Add("报损门店：" + MainModel.CurrentShopInfo.shopname );

                       lstPrintStr.Add(getStrLine());

                       foreach (Itemlist item in brokenresult.itemlist)
                       {
                           lstPrintStr.Add(item.skucode + "  " + item.skuname);

                           string priceandnum = "   " + item.deliveryquantity.ToString("f2") + item.salesunit + "  X  " + item.deliveryprice.ToString("f2") + "元";

                           lstPrintStr.Add(MergeStr(priceandnum, item.totalamount.ToString("f2") + "元", BodyCharCountOfLine, PageSize - 6));
                       }
                       lstPrintStr.Add(getStrLine());

                       lstPrintStr.Add(MergeStr("总计件数", brokenresult.totalqty.ToString(), BodyCharCountOfLine, PageSize));
                       lstPrintStr.Add(MergeStr("报损金额", brokenresult.totalamt.ToString("f2") + "元", BodyCharCountOfLine, PageSize));


                       PrintTextByPaperWidth(lstPrintStr);


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

           //合并字符串使 用空格填补 以左右对齐
           public static string MergeStr(string str1, string str2, int charlength, int pagesize)
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

           private static HttpUtil httputil = new HttpUtil();
           public static bool PrintTopUp(string depositbillid)
           {
               try
               {
                   string errormsg = "";
                   TopUpPrint printdetail = httputil.GetDepositbill(depositbillid, ref errormsg);

                   if (!string.IsNullOrEmpty(errormsg) || printdetail == null)
                   {
                       LogManager.WriteLog(errormsg);
                       return false;
                   }

                   try { LogManager.WriteLog("打印充值单:" + depositbillid); }
                   catch { }

                   IniPrintSize();

                   //每次打印先清空之前内容
                   lstPrintStr = new List<string>();


                   m_hPrinter = POS_Port_OpenA("LPT1:", POS_PT_LPT, false, "");
                   POS_Control_AlignType(m_hPrinter, 1);  //设置右面对齐
                   POS_Output_PrintFontStringA(m_hPrinter, 0, 0, 0, 1, 0, "欢迎光临" + "\r\n");

                   POS_Output_PrintFontStringA(m_hPrinter, 0, 0, 0, 1, 0, MainModel.CurrentShopInfo.tenantname + "\r\n");

                   POS_Control_ReSet(m_hPrinter);

                   POS_Port_Close(m_hPrinter);  //打印之后必须关闭 否则打印内容多会打印不全

                   lstPrintStr.Add(" ");

                   lstPrintStr.Add("订单号：" + printdetail.id );
                   lstPrintStr.Add("门店：" + MainModel.CurrentShopInfo.shopname );
                   lstPrintStr.Add("地址：" + MainModel.CurrentShopInfo.address );
                   lstPrintStr.Add("门店电话：" + MainModel.CurrentShopInfo.tel);
                   //lstPrintStr.Add(MergeStr("收银员：" + MainModel.CurrentUser.nickname, "机：" + MainModel.CurrentShopInfo.deviceid, BodyCharCountOfLine, PageSize));
                   lstPrintStr.Add("订单时间：" + MainModel.GetDateTimeByStamp(printdetail.createdat).ToString("yyyy-MM-dd HH:mm:ss" ));
                   lstPrintStr.Add(getStrLine());

                   lstPrintStr.Add(MergeStr("充值消费   X1", printdetail.amount.ToString("f2"), BodyCharCountOfLine, PageSize));

                   lstPrintStr.Add(getStrLine());

                   string type = "";
                   if (printdetail.paymode == "0")
                   {
                       type = "现金支付";
                   }
                   else if (printdetail.paymode == "2")
                   {
                       type = "微信支付";
                   }
                   else
                   {
                       type = "支付宝支付";
                   }

                   lstPrintStr.Add(MergeStr(type, printdetail.amount.ToString("f2"), BodyCharCountOfLine, PageSize));

                   lstPrintStr.Add(getStrLine());


                   lstPrintStr.Add("多谢惠顾，欢迎下次光临！");

                   PrintTextByPaperWidth(lstPrintStr);
                   return true;
               }
               catch {
                   return false;
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




           public static void IniPrintSize()
           {
               try
               {
                   //每次使用初始化一次，否则纸张大于58 每次都会放大
                   BodyCharCountOfLine = 30;
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



       /// <summary>
       /// 初始值-1  标识为未开启打印机   开启打印机获取返回值 打印和开钱箱都需要传该值 否则打印报错
       /// </summary>
          private  static  int m_hPrinter = -1;
          public static bool PrintTextByPaperWidth(List<string> lstprints)
           {
               try
               {
                    m_hPrinter = POS_Port_OpenA("LPT1:", POS_PT_LPT, false, "");

                   POS_Control_AlignType(m_hPrinter, 0);  //设置左对齐
                   string strprint = "";

                   foreach (string str in lstprints)
                   {
                       POS_Output_PrintFontStringA(m_hPrinter, 0, 0, 0, 0, 0, str+"\r\n");
                   }

                   //末尾打印空白行走纸
                   string strswhite =  "  \r\n  \r\n  \r\n";
                   POS_Output_PrintFontStringA(m_hPrinter, 0, 0, 0, 0, 0, strswhite + "\r\n");

                
                   POS_Control_ReSet(m_hPrinter);

                   POS_Port_Close(m_hPrinter);  //打印之后必须关闭 否则打印内容多会打印不全
                   return true;
               }
               catch(Exception ex)
               {

                   LogManager.WriteLog("sprt打印异常"+ex.Message);

                   return false;
               }
           }

       /// <summary>
       /// 开钱箱
       /// </summary>
           public static void OpenCashDrawerEx()
           {
               try
               {
                   m_hPrinter = POS_Port_OpenA("LPT1:", POS_PT_LPT, false, "");
                   Int32 ret = POS_Control_CashDraw(m_hPrinter, 1, 100, 100);

                   POS_Port_Close(m_hPrinter);  //打印之后必须关闭 否则打印内容多会打印不全
               }
               catch (Exception ex)
               {
                   LogManager.WriteLog("sprt打开钱箱异常"+ex.Message);
               }
           }
    }
}
