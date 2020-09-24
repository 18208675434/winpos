﻿
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
using ZhuiZhi_Integral_Scale_UncleFruit.GiftCard.Model;
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
           public static int BodyCharCountOfLine = 32;

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

           // 2D barcode type
           private const Int32 POS_BT_PDF417 = 4100;
           private const Int32 POS_BT_DATAMATRIX = 4101;
           private const Int32 POS_BT_QRCODE = 4102;


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

           [DllImport("POS_SDK.dll", CharSet = CharSet.Ansi, EntryPoint = "POS_Output_PrintTwoDimensionalBarcodeA")]
           static extern Int32 POS_Output_PrintTwoDimensionalBarcodeA(Int32 printID, Int32 iType, Int32 parameter1, Int32 parameter2, Int32 parameter3, String lpString);


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

                        lstPrintStr.Add(" ");

                     


                       PrintTextByPaperWidth(PrintHelper.GetOrderPrintInfo(printdetail,isRefound));

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
           public static object lockgiftcardprinting = new object();
           public static bool PrintGiftCardOrder(GiftCardPrintDetail printdetail, bool isRefound, ref string errormsg)
           {
               lock (lockgiftcardprinting)
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
                       POS_Output_PrintFontStringA(m_hPrinter, 0, 0, 0, 1, 0, "欢迎光临" + "\r\n");

                       POS_Output_PrintFontStringA(m_hPrinter, 0, 0, 0, 1, 0, MainModel.CurrentShopInfo.tenantname + "\r\n");

                       POS_Control_ReSet(m_hPrinter);

                       POS_Port_Close(m_hPrinter);  //打印之后必须关闭 否则打印内容多会打印不全

                       lstPrintStr.Add(" ");




                       PrintTextByPaperWidth(PrintHelper.GetGiftCardOrderPrintInfo(printdetail, isRefound));

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

                      // lstPrintStr.Add(MergeStr("报损单", "", BodyCharCountOfLine, PageSize));

                       m_hPrinter = POS_Port_OpenA("LPT1:", POS_PT_LPT, false, "");
                       POS_Control_AlignType(m_hPrinter, 1);  //设置左对齐
                       POS_Output_PrintFontStringA(m_hPrinter, 0, 0, 0, 1, 0, "报损单" + "\r\n");

                       POS_Control_ReSet(m_hPrinter);

                       POS_Port_Close(m_hPrinter);  //打印之后必须关闭 否则打印内容多会打印不全

                       // lstPrintStr.Add(MergeStr("报损单", "", BodyCharCountOfLine, PageSize));
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

                   lstPrintStr.Add("充值方式：" + "\n");

                   lstPrintStr.Add(MergeStr(printdetail.paymodeforapi, printdetail.amount.ToString("f2"), BodyCharCountOfLine, PageSize));

                   lstPrintStr.Add(getStrLine());

                   lstPrintStr.Add(MergeStr("账户余额", printdetail.balance.ToString("f2"), BodyCharCountOfLine, PageSize));

                   lstPrintStr.Add(getStrLine());
                   lstPrintStr.Add("多谢惠顾，欢迎下次光临！");

                   PrintTextByPaperWidth(lstPrintStr);
                   return true;
               }
               catch {
                   return false;
               }
           }



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


                       m_hPrinter = POS_Port_OpenA("LPT1:", POS_PT_LPT, false, "");
                       POS_Control_AlignType(m_hPrinter, 1);  //设置居中

                       POS_Output_PrintFontStringA(m_hPrinter, 0, 0, 0, 1, 0, MainModel.CurrentShopInfo.tenantname + "\r\n");
                       POS_Output_PrintFontStringA(m_hPrinter, 0, 0, 0, 1, 0, MainModel.CurrentShopInfo.shopname + "\r\n");
                       
                       POS_Control_AlignType(m_hPrinter, 0);  //设置右面对齐
                       POS_Output_PrintFontStringA(m_hPrinter, 0, 0, 0, 1, 0, printdetail.serialcode + "\r\n");
                      POS_Output_PrintFontStringA(m_hPrinter, 0, 0, 0, 0, 0,"订单号：" + printdetail.orderid+"\r\n");
                      POS_Output_PrintFontStringA(m_hPrinter, 0, 0, 0, 0, 0, "下单时间：" + printdetail.date +"\r\n");
                      POS_Output_PrintFontStringA(m_hPrinter, 0, 0, 0, 0, 0, "顾客姓名：" + printdetail.username +"\r\n");
                      POS_Output_PrintFontStringA(m_hPrinter, 0, 0, 0, 0, 0, "顾客电话：" + printdetail.tel +"\r\n");
                      POS_Output_PrintFontStringA(m_hPrinter, 0, 0, 0, 0, 0, "配送地址：" + printdetail.address +"\r\n");
                      POS_Output_PrintFontStringA(m_hPrinter, 0, 0, 0, 0, 0, "备注：" + "\r\n");
                       if (!string.IsNullOrEmpty(printdetail.remark))
                       {
                           POS_Output_PrintFontStringA(m_hPrinter, 0, 0, 0, 1, 0, printdetail.remark + "\r\n");
                       }
                       POS_Output_PrintFontStringA(m_hPrinter, 0, 0, 0, 1, 0, "期望送达时间：" + printdetail.expecttimedesc + "\r\n");



                       POS_Output_PrintFontStringA(m_hPrinter, 0, 0, 0, 0, 0, getStrLine() + "\r\n");

                       //PrintTextRange(PrintHelper.MergeStr("商品", "单价", "重量(kg)", "金额", BodyCharCountOfLine));

                       POS_Output_PrintFontStringA(m_hPrinter, 0, 0, 0, 0, 0, "商品    单价   数量    金额" + "\r\n");
                       foreach (PickProduct pro in printdetail.productdetaillist)
                       {
                           List<string> lstpro = PrintHelper.MergeStr(pro.skuname, pro.price, pro.num, pro.money, BodyCharCountOfLine);

                           foreach (string str in lstpro)
                           {
                               POS_Output_PrintFontStringA(m_hPrinter, 0, 0, 0, 0, 0, str + "\r\n");
                           }

                       }

                       foreach (PickProduct pro in printdetail.productdetaillist)
                       {
                           List<string> lstpro = PrintHelper.MergeStr(pro.skuname, pro.price, pro.num, pro.money, BodyCharCountOfLine);

                           foreach (string str in lstpro)
                           {
                               POS_Output_PrintFontStringA(m_hPrinter, 0, 0, 0, 0, 0, str + "\r\n");
                           }

                       }

                       POS_Output_PrintFontStringA(m_hPrinter, 0, 0, 0, 0, 0, getStrLine() + "\r\n");

                       POS_Output_PrintFontStringA(m_hPrinter, 0, 0, 0, 0, 0, MergeStr("商品金额：", printdetail.productamt, BodyCharCountOfLine, PageSize) + "\r\n");
                       POS_Output_PrintFontStringA(m_hPrinter, 0, 0, 0, 0, 0, MergeStr("配送费：", printdetail.deliveryamt, BodyCharCountOfLine, PageSize) + "\r\n");
                       POS_Output_PrintFontStringA(m_hPrinter, 0, 0, 0, 0, 0, MergeStr("实付金额：", printdetail.totalpayment, BodyCharCountOfLine, PageSize) + "\r\n");

                       if (!string.IsNullOrEmpty(printdetail.pickcode))
                       {
                           POS_Output_PrintFontStringA(m_hPrinter, 0, 0, 0, 0, 0, getStrLine() + "\r\n");
                           POS_Output_PrintFontStringA(m_hPrinter, 0, 0, 0, 0, 0, "请扫描下方二维码取货配送" + "\r\n");
                           POS_Output_PrintFontStringA(m_hPrinter, 0, 0, 0, 0, 0, "  " + "\r\n");
                           POS_Control_AlignType(m_hPrinter, 1);
                           PrintQrCode(printdetail.pickcode);

                           POS_Output_PrintFontStringA(m_hPrinter, 0, 0, 0, 0, 0,printdetail.pickcode + "\r\n");
                       }

                       POS_Output_PrintFontStringA(m_hPrinter, 0, 0, 0, 0, 0, getStrLine() + "\r\n");


                       POS_Output_PrintFontStringA(m_hPrinter, 0, 0, 0, 0, 0, "多谢惠顾，欢迎下次光临！                         " + " \r\n \r\n \r\n \r\n \r\n \r\n");

                       POS_Control_ReSet(m_hPrinter);

                       POS_Port_Close(m_hPrinter);  //打印之后必须关闭 否则打印内容多会打印不全
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


           public static void PrintQrCode(string qrcode)
           {
               try
               {
                   Int32 ret = POS_Output_PrintTwoDimensionalBarcodeA(m_hPrinter, POS_BT_QRCODE, 2, 77, 6, qrcode);
               }
               catch (Exception ex)
               {
                   LogManager.WriteLog("qr" + ex.Message);
               }
           } 
    }
}
