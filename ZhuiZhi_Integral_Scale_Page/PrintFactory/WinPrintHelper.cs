
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;
/*
* win 驱动打印
* 
* 
* */
namespace ZhuiZhi_Integral_Scale_UncleFruit.PrintFactory
{
    /// <summary>
    /// 适配win驱动打印，未完成，后续如用到需完善
    /// </summary>
    public class WinPrintHelper
    {
        #region 业务单据
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

                    //每次打印先清空之前内容
                    lstPrintStr = new List<Line>();
                    lstPrintStr.Add(new Line(PrintHelper.MergeStr("欢迎光临", "", HeadCharCountOfLine, PageSize), fontHead));
                    lstPrintStr.Add(new Line(PrintHelper.MergeStr(MainModel.CurrentShopInfo.tenantname, "", HeadCharCountOfLine, PageSize), fontHead));
                    lstPrintStr.Add(new Line(" ", fontBody));
                    foreach (var line in PrintHelper.GetOrderPrintInfo(printdetail, isRefound))
                    {
                        lstPrintStr.Add(new Line(line, fontBody));
                    }
                    PrintDocument pd = new PrintDocument();
                    pd.PrintPage += new PrintPageEventHandler(pd_ReceiptPrintPage);

                    ////不弹框<正在打印>直接打印
                    PrintController PrintStandard = new StandardPrintController();
                    pd.PrintController = PrintStandard;

                    if (!string.IsNullOrEmpty(PrintName))
                    {
                        pd.PrinterSettings.PrinterName = PrintName;//选择打印机
                    }

                    ParameterizedThreadStart Pts = new ParameterizedThreadStart(PrintThread);
                    Thread threadprint = new Thread(Pts);
                    threadprint.IsBackground = true;
                    threadprint.Start(pd);
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

        #region Windows 驱动打印
        public static List<Line> lstPrintStr = new List<Line>();

        /// <summary>
        /// 打印机宽度
        /// </summary>
        public static int PageSize = 58;
        /// <summary>
        /// 便移量
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

        public static void IniPrintSize()
        {
            try
            {
                //每次使用初始化一次，否则纸张大于58 每次都会放大
                BodyCharCountOfLine = 30;
                HeadCharCountOfLine = 28;
                BodyCharHeightOfLine = 14;
                HeadCharHeightOfLine = 16;

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
                return str1 + str2;
            }
        }

        /// <summary>
        ///  获取横线字符串
        /// </summary>
        /// <returns></returns>
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
                    Line line = lstPrintStr[i];
                    if (string.IsNullOrEmpty(line.printStr))
                    {
                        Font font = line.printFont;
                        //右对齐时  左边空格补充  但是会错半个字符？？？？？？
                        if (line.printStr.Length > 2 && line.printStr.Substring(0, 2) == "  " && PageSize == 58)
                        {
                            e.Graphics.DrawString(line.printStr, font, Brushes.Black, LocationX - 3, LoactionY);
                        }
                        else
                        {
                            e.Graphics.DrawString(line.printStr, font, Brushes.Black, LocationX, LoactionY);
                        }
                    }
                    if (line.printImage != null)
                    {
                        e.Graphics.DrawImage(line.printImage, 0, 0);
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

        /// <summary>
        /// 异步打印
        /// </summary>
        /// <param name="obj"></param>
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
        #endregion
    }

    public class Line
    {
        public Line(string printStr)
        {

        }
        public Line(string printStr, Font fontSize)
        {

        }
        public string printStr { get; set; }
        public Image printImage { get; set; }
        public Font printFont { get; set; }
    }

}
