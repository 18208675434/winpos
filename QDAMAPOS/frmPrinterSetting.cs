using QDAMAPOS.Common;
using QDAMAPOS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace QDAMAPOS
{

    //    ///串口
    //#define COM1	1
    //#define COM2	2
    //#define COM3	3
    //#define COM4	4
    //#define COM5	5
    //#define COM6	6
    //#define COM7	7
    //#define COM8	8
    //#define COM9	9
    //#define COM10	10
    /////并口
    //#define LPT1	11
    //#define LPT2	12

    /////USB 口
    //#define USB		13

    /////(有线或无线WIFI)网络 打印机
    //#define NET		14

    public partial class frmPrinterSetting : Form
    {
        /// <summary>
        /// 打印机宽度
        /// </summary>
        private int PageSize = 58;
        /// <summary>
        /// 便宜量
        /// </summary>
        private int LocationX = 0;
        /// <summary>
        /// 打印机名称
        /// </summary>
        private string PrintName = "";


        /// <summary>
        /// 58 MM纸张下 一行能够打印的小字符数
        /// </summary>
        private int BodyCharCountOfLine = 30;

        /// <summary>
        /// 58 MM纸张下 一行能够打印的大字符数
        /// </summary>
        private int HeadCharCountOfLine = 28;

        /// <summary>
        /// 58 MM纸张下 小字符高度
        /// </summary>
        private double BodyCharHeightOfLine = 14;

        /// <summary>
        /// 58 MM纸张下 大字符高度
        /// </summary>
        private double HeadCharHeightOfLine = 16;


        Font fontHead;
      //  SizeF sizehead;

        Font fontBody;
        //SizeF sizeBody;

        public frmPrinterSetting()
        {
            InitializeComponent();
        }

        private void frmPrinterSetting_Shown(object sender, EventArgs e)
        {
            UpdatePrint();
        }

        private void UpdatePrint()
        {
            try
            {
                this.Enabled = false;
                LoadingHelper.ShowLoadingScreen("加载中...");
                //第一次没有这些值
                try
                {
                    PageSize = Convert.ToInt32(INIManager.GetIni("Print", "PageSize", MainModel.IniPath));

                    LocationX = Convert.ToInt32(INIManager.GetIni("Print", "LocationX", MainModel.IniPath));

                    PrintName = INIManager.GetIni("Print", "PrintName", MainModel.IniPath);
                }
                catch
                {

                }


                bool ExitsPrinter = false;
                cbxPrint.Items.Clear();
                //获取安装的打印机列表，并选中默认打印机
                foreach (string print in PrinterSettings.InstalledPrinters)
                {
                    cbxPrint.Items.Add(print);
                    if (print == PrintName)
                    {
                        ExitsPrinter = true;
                    }
                }

                //默认打印机
                PrintDocument pd = new PrintDocument();
                string defaultStr = pd.PrinterSettings.PrinterName;

                if (ExitsPrinter)
                {
                    cbxPrint.SelectedItem = PrintName;
                }
                else
                {
                    INIManager.SetIni("Print", "PrintName", "", MainModel.IniPath);
                    cbxPrint.SelectedItem = defaultStr;
                }

                txtPaperSize.Text = PageSize.ToString();

                txtLocationX.Text = LocationX.ToString();
                this.Enabled = true;
                LoadingHelper.CloseForm();

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("加载打印机参数异常" + ex.Message);
            }
            finally
            {
                this.Enabled = true;
                LoadingHelper.CloseForm();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string printname = cbxPrint.Text;
            string pagesize = txtPaperSize.Text;
            string locationx = txtLocationX.Text;

            if (string.IsNullOrEmpty(printname))
            {
                MainModel.ShowLog("请选择打印机", false);
                return;
            }

            if (string.IsNullOrEmpty(pagesize))
            {
                MainModel.ShowLog("请输入纸张尺寸", false);
                return;
            }
            else
            {
                try
                {
                    Convert.ToDecimal(pagesize);
                }
                catch
                {
                    MainModel.ShowLog("请输入正确的纸张尺寸", false);
                    return;
                }
            }

            if (string.IsNullOrEmpty(locationx))
            {
                MainModel.ShowLog("请填写偏移量", false);
                return;
            }
            else
            {
                try
                {
                    Convert.ToDecimal(locationx);
                }
                catch
                {
                    MainModel.ShowLog("请输入正确的偏移量数值", false);
                    return;
                }
            }

            INIManager.SetIni("Print", "PageSize", txtPaperSize.Text, MainModel.IniPath);
            INIManager.SetIni("Print", "LocationX", txtLocationX.Text, MainModel.IniPath);
            INIManager.SetIni("Print", "PrintName", cbxPrint.Text, MainModel.IniPath);

            MainModel.ShowLog("保存完成",false);
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();

        }



        #region Windows 驱动打印
        public List<string> lstPrintStr = new List<string>();

        public object lockprinting = new object();
        public bool PrintOrder(PrintDetail printdetail, bool isRefound, ref string errormsg)
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
                    HeadCharHeightOfLine =(double) HeadCharHeightOfLine * PageSize / 58;


                    fontBody = new System.Drawing.Font("宋体", 8.5F * PageSize / 58);
                   // SizeF sizeBody = CreateGraphics().MeasureString("多个字可以减少字符间距误差微软多个字可以减少字符间距误差宋体", fontBody);
                   // BodyCharCountOfLine = BodyCharCountOfLine * 58 / PageSize;
                    BodyCharHeightOfLine = (double)BodyCharHeightOfLine * PageSize / 58;
                    // int length = (int)Math.Floor(58 / (0.254 * sizeBody.Width) * 2 * 30) - 8;  //-8 预留两个汉字空白

                    //每次打印先清空之前内容
                    lstPrintStr = new List<string>();


                    lstPrintStr.Add(MergeStr("欢迎光临[钱大妈]不卖隔夜肉", "",  BodyCharCountOfLine, PageSize));
                    lstPrintStr.Add(" ");

                    lstPrintStr.Add("订单号：" + printdetail.orderid + "\n");
                    lstPrintStr.Add("门店：" + MainModel.CurrentShopInfo.shopname + "\n");
                    lstPrintStr.Add("地址：" + MainModel.CurrentShopInfo.address + "\n");
                    lstPrintStr.Add("电话：" + MainModel.CurrentShopInfo.tel + "\n");
                    lstPrintStr.Add(MergeStr("收银员：" + MainModel.CurrentUser.nickname, "机：" + MainModel.CurrentShopInfo.deviceid,  BodyCharCountOfLine, PageSize));
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
                    if (printdetail.orderpricedetails != null && printdetail.orderpricedetails.Count > 0)
                    {


                        //汉字占两位 TODO 判断前面汉字和英文数字
                        foreach (OrderPriceDetail pricedetail in printdetail.orderpricedetails)
                        {
                            if (isRefound)
                            {
                                //lstPrintStr.Add(pricedetail.title.Replace("应收", "退款") + pricedetail.amount.PadLeft(length - Encoding.Default.GetBytes(pricedetail.title).Length, ' '));

                                lstPrintStr.Add(MergeStr(pricedetail.title.Replace("应收", "退款"), pricedetail.amount,  BodyCharCountOfLine, PageSize));
                            }
                            else
                            {
                                //lstPrintStr.Add(pricedetail.title + pricedetail.amount.PadLeft(length - Encoding.Default.GetBytes(pricedetail.title).Length, ' '));
                                lstPrintStr.Add(MergeStr(pricedetail.title, pricedetail.amount,  BodyCharCountOfLine, PageSize));
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
                            lstPrintStr.Add(MergeStr(paydetail.title.Replace("支付", "退款").Replace("退款宝", "支付宝"), paydetail.amount,  BodyCharCountOfLine, PageSize));
                        }
                        else
                        {
                            // lstPrintStr.Add(paydetail.title + paydetail.amount.PadLeft(length - paydetail.title.Length * 2, ' '));
                            lstPrintStr.Add(MergeStr(paydetail.title, paydetail.amount,  BodyCharCountOfLine, PageSize));
                        }
                    }

                    if (printdetail.pointinfo != null && printdetail.pointinfo.Length > 0)
                    {

                        lstPrintStr.Add(getStrLine());
                        foreach (PointInfo pointinfo in printdetail.pointinfo)
                        {

                            //lstPrintStr.Add(pointinfo.title + pointinfo.amount.PadLeft(length - Encoding.Default.GetBytes(pointinfo.title).Length, ' '));
                            lstPrintStr.Add(MergeStr(pointinfo.title, pointinfo.amount,  BodyCharCountOfLine, PageSize));
                        }
                    }

                    lstPrintStr.Add(getStrLine());

                    lstPrintStr.Add("多谢惠顾，欢迎下次光临！");
                    lstPrintStr.Add("钱大妈官网:http://www.qdama.cn");
                    lstPrintStr.Add("顾客服务热线: 400-628-5880");
                    lstPrintStr.Add("官方会员微信: qdama888");
                    lstPrintStr.Add(MergeStr("加入会员 积分买菜 优惠多多", "", BodyCharCountOfLine, PageSize));
                    lstPrintStr.Add(MergeStr("  ", "",  BodyCharCountOfLine, PageSize));



                
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




        public object lockreceiptprint = new object();
        public bool ReceiptPrint(Receiptdetail receipt, ref string errormsg)
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
                lstPrintStr.Add(MergeStr(Cashier, Serial,  BodyCharCountOfLine, PageSize));
                lstPrintStr.Add(MergeStr(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), (receipt.devicecode.amount + receipt.devicecode.title),  BodyCharCountOfLine, PageSize));
                lstPrintStr.Add(MergeStr(receipt.shiftcode.title, receipt.shiftcode.amount,  BodyCharCountOfLine, PageSize));

                lstPrintStr.Add(MergeStr(receipt.title.title, "",  BodyCharCountOfLine, PageSize));

                lstPrintStr.Add(MergeStr("开始时间：", MainModel.GetDateTimeByStamp(receipt.starttime.ToString()).ToString("yyyy-MM-dd HH:mm:ss"),  BodyCharCountOfLine, PageSize));
                lstPrintStr.Add(MergeStr("结束时间：", MainModel.GetDateTimeByStamp(receipt.endtime.ToString()).ToString("yyyy-MM-dd HH:mm:ss"),  BodyCharCountOfLine, PageSize));

                foreach (OrderPriceDetail basicinfo in receipt.basicinfo)
                {
                    try
                    {
                        lstPrintStr.Add(MergeStr(basicinfo.title, basicinfo.amount,  BodyCharCountOfLine, PageSize));

                        if (!string.IsNullOrEmpty(basicinfo.subtitle))
                        {
                            lstPrintStr.Add(MergeStr("", basicinfo.subtitle,  BodyCharCountOfLine, PageSize));
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
                        lstPrintStr.Add(MergeStr(bottomdetail.title, bottomdetail.amount,  BodyCharCountOfLine, PageSize));

                        if (!string.IsNullOrEmpty(bottomdetail.subtitle))
                        {

                            //lstPrintStr.Add("".PadLeft(spacenum2, ' ') + bottomdetail.subtitle);
                            lstPrintStr.Add(MergeStr("", bottomdetail.subtitle.Trim(),  BodyCharCountOfLine, PageSize));
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
                LogManager.WriteLog("ERROR", "打印交班小票出现异常" + ex.Message+ex.StackTrace);
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
        public void pd_OrderPrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {

                int LoactionY = 10; //标签头部预留10像素空白，防止打印机开头不走纸 打印不全

                int picHeadWidth =(int)Math.Round( PageSize / 0.254 * 40 / 100);

                int picHeadHeight = picHeadWidth * 2 / 5;
                int picLocationX = (int)Math.Round( PageSize / 0.254 * 20 / 100);
                e.Graphics.DrawImage(picHead.Image, picLocationX, LoactionY, picHeadWidth, picHeadHeight);

                LoactionY += picHeadHeight;
                for (int i = 0; i < lstPrintStr.Count; i++)
                {
                    string str = lstPrintStr[i];
                  
                        e.Graphics.DrawString(str, fontBody, Brushes.Black, LocationX, LoactionY);
                        LoactionY +=(int) BodyCharHeightOfLine;
                    
                }



                int picQrWidth = (int)Math.Round(PageSize / 0.254 * 30 / 100);

                //int picQrHeight = picHeadWidth * 2 / 5;
                int picLocationQr = (int)Math.Round(PageSize / 0.254 * 25 / 100);
                e.Graphics.DrawImage(picQR.Image, picLocationQr, LoactionY, picQrWidth, picQrWidth);
                LoactionY += picQrWidth;

                e.Graphics.DrawString(MergeStr("新鲜·便捷·优选 100%退换货", "",  BodyCharCountOfLine, PageSize), fontBody, Brushes.Black, LocationX, LoactionY);

               

                //结尾留下20像素空白，防止打印机不走空白纸 撕纸不全    空白不会打印，打印内容不再纸张范围也不会走纸，所以打印一个点用来走纸
                e.Graphics.DrawString(".", fontBody, Brushes.Black,0, LoactionY + 20);
                

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
        public void pd_ReceiptPrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {

                int LoactionY = 10; //标签头部预留10像素空白，防止打印机开头不走纸 打印不全
                for (int i = 0; i < lstPrintStr.Count; i++)
                {
                    string str = lstPrintStr[i];
                    //小票第一行要放大字体
                    if (i == 0 && str.Contains("欢迎光临"))  //小票第一行字体放大居中
                    {
                        int spacenum = 0;
                        try
                        {
                            double headwidth = CreateGraphics().MeasureString(str, fontHead).Width;
                            //居中左右空格数量
                            //spacenum = (int)Math.Floor(((PageSize / 0.254 - headwidth) * 0.254 - 8) / 4);
                        }
                        catch { }
                        e.Graphics.DrawString("".PadLeft(spacenum, ' ') + str, fontHead, Brushes.Black, LocationX, LoactionY);
                        LoactionY += (int)HeadCharHeightOfLine;
                    }
                    else
                    {
                        //右对齐时  左边空格补充  但是会错半个字符？？？？？？
                        if (str.Length > 2 && str.Substring(0, 2) == "  " && PageSize==58)
                        {
                            e.Graphics.DrawString(str, fontBody, Brushes.Black, LocationX-3, LoactionY);
                        }
                        else
                        {
                            e.Graphics.DrawString(str, fontBody, Brushes.Black, LocationX, LoactionY);
                        }
                        
                        LoactionY +=(int) BodyCharHeightOfLine;

                    }
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
        private string MergeStr(string str1, string str2, int charlength, int pagesize)
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
                    result = " ".PadLeft(spacenum, ' ')+str1;
                }
                else if (string.IsNullOrEmpty(str1)) //右对齐
                {
                    //右对齐会错位半个字符？？？？？？？？
                    spacenum = charlength - Encoding.Default.GetBytes(str1 + str2).Length;
                    result =str1.PadLeft(spacenum, ' ')+ str2;
                }
                else //左右对齐 （中间填充空格）
                {
                    spacenum = charlength - Encoding.Default.GetBytes(str1 + str2).Length;
                    result = str1 + " ".PadLeft(spacenum,' ') + str2;
                }
                return result;

                //string result = str1 + str2.PadLeft(length - Encoding.Default.GetBytes(str1+str2).Length, ' ');

            }
            catch(Exception ex)
            {
                LogManager.WriteLog("计算字体异常"+ex.Message +ex.StackTrace);
                return str1 + str2;
            }
        }

        private string getStrLine()
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            UpdatePrint();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();

        }

    }

}

