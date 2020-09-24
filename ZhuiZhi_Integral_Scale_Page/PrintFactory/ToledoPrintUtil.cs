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
   public  class ToledoPrintUtil
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



                       // lstPrintStr.Add(MergeStr("欢迎光临", "", HeadCharCountOfLine, PageSize));

                       PrintText(MergeStr("欢迎光临", "", HeadCharCountOfLine, PageSize));
                       PrintText(MergeStr(MainModel.CurrentShopInfo.tenantname, "", HeadCharCountOfLine, PageSize));
                       //lstPrintStr.Add(MergeStr(MainModel.CurrentShopInfo.tenantname, "", BodyCharCountOfLine, PageSize));
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



                       // lstPrintStr.Add(MergeStr("欢迎光临", "", HeadCharCountOfLine, PageSize));

                       PrintText(MergeStr("欢迎光临", "", HeadCharCountOfLine, PageSize));
                       PrintText(MergeStr(MainModel.CurrentShopInfo.tenantname, "", HeadCharCountOfLine, PageSize));
                       //lstPrintStr.Add(MergeStr(MainModel.CurrentShopInfo.tenantname, "", BodyCharCountOfLine, PageSize));
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

                       PrintText(MergeStr("报损单", "", HeadCharCountOfLine, PageSize));
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

                      
                       PrintText(MergeStr(MainModel.CurrentShopInfo.tenantname, "", HeadCharCountOfLine, PageSize),32);
                       PrintText(" ",32);
                       PrintText(MergeStr(MainModel.CurrentShopInfo.shopname, "", HeadCharCountOfLine, PageSize),32);
                       PrintText(printdetail.serialcode,40);

                       PrintText("订单号：" + printdetail.orderid,22);
                       PrintText("下单时间：" + printdetail.date, 22);
                       PrintText("顾客姓名：" + printdetail.username, 22);
                       PrintText("顾客电话：" + printdetail.tel, 22);
                       PrintText("配送地址：" + printdetail.address, 22);
                       PrintText("备注：");
                       if(!string.IsNullOrEmpty( printdetail.remark)){
                           PrintText(printdetail.remark,32);
                       }
                       PrintText("期望送达时间：" + printdetail.expecttimedesc,32);


                     
                       PrintText(getStrLine(),22);

                       //PrintTextRange(PrintHelper.MergeStr("商品", "单价", "重量(kg)", "金额", BodyCharCountOfLine));

                       PrintText("商品    单价   数量    金额",22);
                       foreach (PickProduct pro in printdetail.productdetaillist)
                       {
                           List<string> lstpro = PrintHelper.MergeStr(pro.skuname, pro.price, pro.num, pro.money, BodyCharCountOfLine);

                           foreach (string str in lstpro)
                           {
                               PrintText(str, 22);
                           }
                           
                       }
                       PrintText(getStrLine(),22);

                       PrintText(MergeStr("商品金额：", printdetail.productamt, BodyCharCountOfLine, PageSize),22);
                       PrintText(MergeStr("配送费：", printdetail.deliveryamt, BodyCharCountOfLine, PageSize),22);
                       PrintText(MergeStr("实付金额：", printdetail.totalpayment, BodyCharCountOfLine, PageSize),22);

                       if (!string.IsNullOrEmpty(printdetail.pickcode))
                       {
                           PrintText(getStrLine(), 22);
                           PrintText("请扫描下方二维码取货配送", 22);
                           PrintText("  ", 22);
                           BeginPrint(0);
                           PrintHelper.GetQrBmp(printdetail.pickcode);
                           PrintBitmapFile("orderqrcoe.bmp");
                           
                           BeginPrint(7);
                           PrintText(MergeStr(printdetail.pickcode, "", BodyCharCountOfLine, PageSize), 22);
                       }

                       PrintText(getStrLine(),22);


                       PrintTextByPaperWidth(System.Text.Encoding.Default.GetBytes("多谢惠顾，欢迎下次光临！" + " \r\n \r\n \r\n \r\n \r\n \r\n"), 22, 60);
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

           //合并字符串使 用空格填补 以左右对齐
           public static string MergeStr(string str1, string str2, int charlength, int pagesize)
           {
               try
               {
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

                   return "-".PadLeft(BodyCharCountOfLine, '-');
               }
               catch
               {
                   return "------------------------------";
               }
           }

           #endregion

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

                           // lstPrintStr.Add(MergeStr("欢迎光临", "", HeadCharCountOfLine, PageSize));

                           PrintText(MergeStr("欢迎光临", "", HeadCharCountOfLine, PageSize));
                           PrintText(MergeStr(MainModel.CurrentShopInfo.tenantname, "", HeadCharCountOfLine, PageSize));
                           //lstPrintStr.Add(MergeStr(MainModel.CurrentShopInfo.tenantname, "", BodyCharCountOfLine, PageSize));
                           lstPrintStr.Add(" ");

                           lstPrintStr.Add("订单号：" + printdetail.id + "\n");
                           lstPrintStr.Add("门店：" + MainModel.CurrentShopInfo.shopname + "\n");
                           lstPrintStr.Add("地址：" + MainModel.CurrentShopInfo.address + "\n");
                           lstPrintStr.Add("门店电话：" + MainModel.CurrentShopInfo.tel + "\n");
                           //lstPrintStr.Add(MergeStr("收银员：" + MainModel.CurrentUser.nickname, "机：" + MainModel.CurrentShopInfo.deviceid, BodyCharCountOfLine, PageSize));
                           lstPrintStr.Add("订单时间：" + MainModel.GetDateTimeByStamp(printdetail.createdat).ToString("yyyy-MM-dd HH:mm:ss" + "\n"));
                           lstPrintStr.Add(getStrLine());

                           lstPrintStr.Add(MergeStr("充值消费   X1", printdetail.amount.ToString("f2"), BodyCharCountOfLine, PageSize));

                           lstPrintStr.Add(getStrLine());

                           lstPrintStr.Add("充值方式："+ "\n");


                   lstPrintStr.Add(MergeStr(printdetail.paymodeforapi, printdetail.amount.ToString("f2"), BodyCharCountOfLine, PageSize));

                           lstPrintStr.Add(getStrLine());

                           lstPrintStr.Add(MergeStr("账户余额", printdetail.balance.ToString("f2"), BodyCharCountOfLine, PageSize));

                           lstPrintStr.Add(getStrLine());
                           lstPrintStr.Add("多谢惠顾，欢迎下次光临！");

                           PrintTextByPaperWidth(lstPrintStr);

                           Application.DoEvents();
                           return true;

               }
               catch {
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
           public static bool PrintText(string printtext,int fontsize =32)
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

                       strprint +=  str + "\r\n"; //每行前面加两个空格 防止纸张偏打印不全 两边都留间距

                   }

                   //末尾打印空白行走纸
                   strprint += "  \r\n  \r\n  \r\n  \r\n  \r\n  \r\n ";

                   int result = PrintTextByPaperWidth(System.Text.Encoding.Default.GetBytes(strprint), 22, 60);

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
