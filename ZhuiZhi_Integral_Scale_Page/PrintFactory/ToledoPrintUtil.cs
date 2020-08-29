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
   public  class ToledoPrintUtil
    {

           #region Windows 驱动打印

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
                   //string type ="";
                   //if(printdetail.paymode=="0"){
                   //    type="现金";
                   //}
                   //else if (printdetail.paymode == "2")
                   //{
                   //    type="微信";
                   //}else{
                   //    type="支付宝";
                   //}



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
           public static bool PrintText(string printtext)
           {
               int result = PrintText(System.Text.Encoding.Default.GetBytes(printtext), 32);
               //MessageBox.Show(result.ToString());
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

                       strprint += " " + str + "\r\n"; //每行前面加两个空格 防止纸张偏打印不全 两边都留间距

                   }

                   //末尾打印空白行走纸
                   strprint += "  \r\n  \r\n  \r\n  \r\n  \r\n  \r\n ";

                   int result = PrintTextByPaperWidth(System.Text.Encoding.Default.GetBytes(strprint), 22, 60);

                   return BeginPrint();


               }
               catch (Exception ex)
               {
                   return false;
               }
           }


           [DllImport("pos_ad_dll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
           public static extern int OpenCashDrawerEx();

       
    }
}
