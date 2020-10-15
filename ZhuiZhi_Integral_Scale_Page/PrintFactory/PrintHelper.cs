using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ThoughtWorks.QRCode.Codec;
using ZhuiZhi_Integral_Scale_UncleFruit.BrokenUI.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.GiftCard.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.PrintFactory
{
    public class PrintHelper
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


        /// <summary>
        /// 获取交班打印信息
        /// </summary>
        /// <param name="receipt"></param>
        /// <returns></returns>
        public static List<string> GetOrderPrintInfo(PrintDetail printdetail, bool isRefound)
        {
            List<string> lstPrintStr = new List<string>();
            try
            {

                lstPrintStr.Add("订单号：" + printdetail.orderid);
                lstPrintStr.Add("门店：" + MainModel.CurrentShopInfo.shopname);
                lstPrintStr.Add("地址：" + MainModel.CurrentShopInfo.address);
                lstPrintStr.Add("门店电话：" + MainModel.CurrentShopInfo.tel);
                //lstPrintStr.Add(MergeStr("收银员：" + MainModel.CurrentUser.nickname, "机：" + MainModel.CurrentShopInfo.deviceid, BodyCharCountOfLine, PageSize));
                lstPrintStr.Add("订单时间：" + MainModel.GetDateTimeByStamp(printdetail.orderat).ToString("yyyy-MM-dd HH:mm:ss"));
                lstPrintStr.Add(getStrLine());


                //lstPrintStr.AddRange(PrintHelper.MergeStr("商品", "单价", "重量(kg)", "金额", BodyCharCountOfLine));

                lstPrintStr.Add("商品    单价   重量(kg) 金额");
                foreach (ProductDetail pro in printdetail.products)
                {
                    string num = pro.goodstagid == 0 ? pro.num.ToString() : pro.specnum.ToString("f3");

                    lstPrintStr.AddRange(PrintHelper.MergeStr(pro.title, pro.price.saleprice.ToString("f2"), num, pro.price.total.ToString("f2"), BodyCharCountOfLine));


                    if (pro.price.promoamt > 0)
                    {
                        lstPrintStr.Add(MergeStr("", "原价"+pro.price.origintotal.ToString("f2")+" 已优惠" + pro.price.promoamt.ToString("f2"), BodyCharCountOfLine, PageSize - 6));
                    }
                }
                lstPrintStr.Add(getStrLine());
                if (printdetail.orderbasicinfo != null && printdetail.orderbasicinfo.Count > 0)
                {
                    //汉字占两位 TODO 判断前面汉字和英文数字
                    foreach (OrderPriceDetail pricedetail in printdetail.orderbasicinfo)
                    {
                        string stramount = pricedetail.amount;
                        if (isRefound && pricedetail.title !=null && pricedetail.title != "件数合计" && pricedetail.amount!=null && !pricedetail.amount.Contains("-"))
                        {
                            stramount = "-" + stramount;
                        }

                        if (string.IsNullOrEmpty(stramount))
                        {
                            lstPrintStr.Add(pricedetail.title);
                        }
                        else
                        {
                            lstPrintStr.Add(MergeStr(pricedetail.title, stramount, BodyCharCountOfLine, PageSize));
                        }

                            



                            if (pricedetail.childdetail != null && pricedetail.childdetail.Count > 0)
                            {
                                foreach (OrderPriceDetail child in pricedetail.childdetail)
                                {
                                    string childamount = "";
                                    if (child.amount != null)
                                    {
                                        childamount = child.amount.Contains("-") ? child.amount : "-" + child.amount;
                                    }
                                    lstPrintStr.Add(MergeStr("    "+child.title, childamount, BodyCharCountOfLine, PageSize));
                                }
                             
                            }
                    }

                    lstPrintStr.Add(getStrLine());
                }
               
                foreach (Paydetail paydetail in printdetail.paydetail)
                {

                    string stramount = paydetail.amount;
                    if (isRefound && paydetail.title != "件数合计" && !stramount.Contains("-"))
                    {
                        stramount = "-" + stramount;
                    }

                    lstPrintStr.Add(MergeStr(paydetail.title, stramount, BodyCharCountOfLine, PageSize));
                   
                }

                if (printdetail.memberinfo != null && printdetail.memberinfo.Count > 0)
                {

                    lstPrintStr.Add(getStrLine());
                    foreach (PointInfo memberinfo in printdetail.memberinfo)
                    {
                        string stramount = memberinfo.amount;
                        if (isRefound && (memberinfo.title=="获得积分" || memberinfo.title=="消耗积分"))
                        {
                            stramount = "-" + stramount;
                        }

                        lstPrintStr.Add(MergeStr(memberinfo.title, stramount, BodyCharCountOfLine, PageSize));
                    }
                }

                lstPrintStr.Add(getStrLine());

                lstPrintStr.Add("多谢惠顾，欢迎下次光临！");

                return lstPrintStr;
            }
            catch
            {
                return lstPrintStr;
            }
        }


        /// <summary>
        /// 获取礼品卡订单
        /// </summary>
        /// <param name="receipt"></param>
        /// <returns></returns>
        public static List<string> GetGiftCardOrderPrintInfo(GiftCardPrintDetail printdetail, bool isRefound)
        {
            List<string> lstPrintStr = new List<string>();
            try
            {

                lstPrintStr.Add("订单号：" + printdetail.orderid);
                lstPrintStr.Add("门店：" + MainModel.CurrentShopInfo.shopname);
                lstPrintStr.Add("地址：" + MainModel.CurrentShopInfo.address);
                lstPrintStr.Add("电话：" + MainModel.CurrentShopInfo.tel);
                //lstPrintStr.Add(MergeStr("收银员：" + MainModel.CurrentUser.nickname, "机：" + MainModel.CurrentShopInfo.deviceid, BodyCharCountOfLine, PageSize));
                lstPrintStr.Add(MainModel.GetDateTimeByStamp(printdetail.orderat).ToString("yyyy-MM-dd HH:mm:ss"));
                lstPrintStr.Add(getStrLine());


                //lstPrintStr.AddRange(PrintHelper.MergeStr("商品", "单价", "重量(kg)", "金额", BodyCharCountOfLine));

                foreach (PrintProduct pro in printdetail.products)
                {
                    lstPrintStr.Add(pro.cardno + "  " + pro.title);

                        string priceandnum = "   " + pro.pspamt.ToString("f2") + "  X  " + pro.num;

                        lstPrintStr.Add(MergeStr(priceandnum, pro.pspamt.ToString("f2"), BodyCharCountOfLine, PageSize - 8));
                    
                }
                lstPrintStr.Add(getStrLine());

                lstPrintStr.Add(MergeStr("件数合计:", printdetail.productcount.ToString(), BodyCharCountOfLine, PageSize));

                lstPrintStr.Add(MergeStr("应收金额:", printdetail.pspamt.ToString("f2"), BodyCharCountOfLine, PageSize));
                lstPrintStr.Add(getStrLine());
                foreach (PaydetailItem paydetail in printdetail.paydetail)
                {

                    string stramount = paydetail.amount;

                    lstPrintStr.Add(MergeStr(paydetail.title, stramount, BodyCharCountOfLine, PageSize));

                }


                lstPrintStr.Add(getStrLine());

                lstPrintStr.Add("多谢惠顾，欢迎下次光临！");

                return lstPrintStr;
            }
            catch
            {
                return lstPrintStr;
            }
        }

        /// <summary>
        /// 获取交班打印信息
        /// </summary>
        /// <param name="receipt"></param>
        /// <returns></returns>
        public static List<string> GetReceiptPrintInfo(Receiptdetail receipt)
        {
            List<string> lstPrintStr = new List<string>();
            try
            {

                string Cashier = "收银员：" + receipt.cashier;
                string Serial = receipt.serial.amount + receipt.serial.title;
                lstPrintStr.Add(MergeStr(Cashier, Serial, BodyCharCountOfLine, PageSize));

                lstPrintStr.Add(MergeStr("门店："+receipt.shopname, (receipt.devicecode.amount + receipt.devicecode.title), BodyCharCountOfLine, PageSize));

                lstPrintStr.Add(MergeStr("开始时间：", MainModel.GetDateTimeByStamp(receipt.starttime.ToString()).ToString("yyyy-MM-dd HH:mm:ss"), BodyCharCountOfLine, PageSize));
                lstPrintStr.Add(MergeStr("结束时间：", MainModel.GetDateTimeByStamp(receipt.endtime.ToString()).ToString("yyyy-MM-dd HH:mm:ss"), BodyCharCountOfLine, PageSize));

                lstPrintStr.Add(MergeStr("开始序号：", receipt.startserialcode, BodyCharCountOfLine, PageSize));
                lstPrintStr.Add(MergeStr("结束序号：", receipt.endserialcode, BodyCharCountOfLine, PageSize));

                lstPrintStr.Add(getStrLine());

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


                foreach (OrderPriceDetail paoinfo in receipt.payinfo)
                {
                    try
                    {
                        decimal tempamount = 0;
                        try
                        {
                            tempamount = Convert.ToDecimal(paoinfo.amount);
                        }
                        catch { }
                        if (tempamount != 0)  //不能用大于0判断 退款时会出现负数
                        {
                            lstPrintStr.Add(MergeStr(paoinfo.title, paoinfo.amount, BodyCharCountOfLine, PageSize));

                            if (!string.IsNullOrEmpty(paoinfo.subtitle))
                            {
                                lstPrintStr.Add(MergeStr("", paoinfo.subtitle, BodyCharCountOfLine, PageSize));
                            }
                        }

                    }
                    catch (Exception ex) { }
                }


                lstPrintStr.Add(getStrLine());



                foreach (OrderPriceDetail otherinfo in receipt.otherinfo)
                {
                    try
                    {
                        lstPrintStr.Add(MergeStr(otherinfo.title, otherinfo.amount, BodyCharCountOfLine, PageSize));

                        if (!string.IsNullOrEmpty(otherinfo.subtitle))
                        {
                            lstPrintStr.Add(MergeStr("", otherinfo.subtitle.Trim(), BodyCharCountOfLine, PageSize));
                        }
                    }
                    catch (Exception ex) { }
                }


                if (receipt.balancedepositinfo != null && receipt.balancedepositinfo.Count > 0)
                {
                    lstPrintStr.Add(getStrLine());


                    lstPrintStr.Add("充值明细:");

                    decimal totalamount = 0;
                    foreach (OrderPriceDetail balancedetail in receipt.balancedepositinfo)
                    {
                        try
                        {

                            totalamount += Convert.ToDecimal(balancedetail.amount);
                            lstPrintStr.Add(MergeStr(balancedetail.title, balancedetail.amount, BodyCharCountOfLine, PageSize));

                            if (!string.IsNullOrEmpty(balancedetail.subtitle))
                            {
                                lstPrintStr.Add(MergeStr("", balancedetail.subtitle.Trim(), BodyCharCountOfLine, PageSize));
                            }
                        }
                        catch (Exception ex) { }

                        lstPrintStr.Add(MergeStr("总计", totalamount.ToString("f2"), BodyCharCountOfLine, PageSize));
                    }
                }

                lstPrintStr.Add(getStrLine());
                foreach (OrderPriceDetail shopcashinfo in receipt.shopcashinfo)
                {
                    try
                    {
                        lstPrintStr.Add(MergeStr(shopcashinfo.title, shopcashinfo.amount, BodyCharCountOfLine, PageSize));

                        if (!string.IsNullOrEmpty(shopcashinfo.subtitle))
                        {
                            lstPrintStr.Add(MergeStr("", shopcashinfo.subtitle.Trim(), BodyCharCountOfLine, PageSize));
                        }
                    }
                    catch (Exception ex) { }
                }
                lstPrintStr.Add(getStrLine());

                lstPrintStr.Add("实缴现金：（          ）");
                lstPrintStr.Add("其他费用：（          ）");
                //lstPrintStr.Add("长 短 款：（          ）");
                lstPrintStr.Add("签    字：");

                return lstPrintStr;
            }
            catch {
                return lstPrintStr;
            }
        }

        /// <summary>
        /// 获取报损打印信息
        /// </summary>
        /// <param name="brokenresult"></param>
        /// <returns></returns>
        public static List<string> GetBrokenPrintInfo(CreateBrokenResult brokenresult)
        {
            List<string> lstPrintStr = new List<string>();
            try
            {
               // lstPrintStr.Add("收银员：" + MainModel.CurrentUser.nickname);
                lstPrintStr.Add("报损时间：" + MainModel.GetDateTimeByStamp(brokenresult.createdat).ToString("yyyy-MM-dd HH:mm:ss"));
                lstPrintStr.Add("报损门店：" + MainModel.CurrentShopInfo.shopname);

                lstPrintStr.Add(getStrLine());

                foreach (Itemlist item in brokenresult.itemlist)
                {
                    lstPrintStr.Add(item.skucode + "  " + item.skuname);

                    string priceandnum = "   " + item.deliveryquantity.ToString("f3") + item.salesunit + "  X  " + item.deliveryprice.ToString("f2") + "元";

                    lstPrintStr.Add(MergeStr(priceandnum, item.totalamount.ToString("f2") + "元", BodyCharCountOfLine, PageSize - 6));

                    if (!string.IsNullOrEmpty(item.actiontypedesc))
                    {
                        lstPrintStr.Add(MergeStr( item.actiontypedesc,"", BodyCharCountOfLine, PageSize));
                    }
                }
                lstPrintStr.Add(getStrLine());

                lstPrintStr.Add(MergeStr("总计件数", brokenresult.totalqty.ToString(), BodyCharCountOfLine, PageSize));
                lstPrintStr.Add(MergeStr("报损金额", brokenresult.totalamt.ToString("f2") + "元", BodyCharCountOfLine, PageSize));



                lstPrintStr.Add(getStrLine());

                return lstPrintStr;
            }
            catch
            {
                return lstPrintStr;
            }
        }



        /// <summary>
        /// 获取交班打印信息
        /// </summary>
        /// <param name="receipt"></param>
        /// <returns></returns>
        public static List<string> GetThirdOrderPrintInfo(PrinterPickOrderInfo printdetail)
        {
            List<string> lstPrintStr = new List<string>();
            try
            {
                lstPrintStr.Add("订单号：" + printdetail.orderid);
                lstPrintStr.Add("下单时间：" + printdetail.date);
                lstPrintStr.Add("顾客姓名：" + printdetail.username);
                lstPrintStr.Add("顾客电话：" + printdetail.tel);
                lstPrintStr.Add("配送地址：" + printdetail.address);
                lstPrintStr.Add("备注：");

                lstPrintStr.Add("期望送达时间："+printdetail.expecttimedesc);


                if (!string.IsNullOrEmpty(printdetail.remark))
                {
                    lstPrintStr.Add(printdetail.remark);
                }
                lstPrintStr.Add(getStrLine());

                //lstPrintStr.AddRange(PrintHelper.MergeStr("商品", "单价", "重量(kg)", "金额", BodyCharCountOfLine));

                lstPrintStr.Add("商品    单价   数量    金额");
                foreach (PickProduct pro in printdetail.productdetaillist)
                {

                    lstPrintStr.AddRange(PrintHelper.MergeStr(pro.skuname, pro.price, pro.num, pro.money, BodyCharCountOfLine));
                }
                lstPrintStr.Add(getStrLine());

                lstPrintStr.Add(MergeStr("商品金额：", printdetail.productamt,  BodyCharCountOfLine,PageSize));
                lstPrintStr.Add(MergeStr("配送费：", printdetail.deliveryamt, BodyCharCountOfLine, PageSize));
                lstPrintStr.Add(MergeStr("实付金额：", printdetail.totalpayment, BodyCharCountOfLine, PageSize));
               

                lstPrintStr.Add(getStrLine());

                lstPrintStr.Add("多谢惠顾，欢迎下次光临！");

                return lstPrintStr;
            }
            catch
            {
                return lstPrintStr;
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


        public static List<string> MergeStr(string str1, string str2, string str3, string str4, int charlength)
        {
            try
            {
                //每行分成五份  商品2/5  数量、单价、总额各站1/5    四行数据中间三段空白
                int itemLength = Convert.ToInt16(Math.Round((decimal)charlength / 9));
                int NameWidth = charlength - itemLength * 6;
                List<string> lstname = substr(str1, NameWidth -1);

                str1 = lstname[lstname.Count - 1];
                str2 = GetItemString(str2, itemLength*2);
                str3 = GetItemString(str3, itemLength*2);
                string result = "";

                int spacenum1 = NameWidth - Encoding.Default.GetBytes(str1).Length;
                int spacenum2 = itemLength*2 - Encoding.Default.GetBytes(str2).Length;
                int spacenum3 = itemLength*2 - Encoding.Default.GetBytes(str3).Length;

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

                lstname[lstname.Count - 1] = result;
                return lstname;

                //string result = str1 + str2.PadLeft(length - Encoding.Default.GetBytes(str1+str2).Length, ' ');

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("计算字体异常" + ex.Message + ex.StackTrace);
                 return new List<string>() {str1 + str2+str3+str4};
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
                return new List<string>() { str };
            }
        }


        //二维码
        private static  QRCodeEncoder qrCode = new QRCodeEncoder();

        public static Bitmap GetQrBmp(string qrcode)
        {
            try
            {
                qrCode.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;        //二维码编码(Byte、AlphaNumeric、Numeric)
                qrCode.QRCodeScale = 3;                                          //二维码尺寸
                qrCode.QRCodeVersion = 0;                                        //二维码密集度0-40
                qrCode.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;    //二维码纠错能力(L：7% M：15% Q：25% H：30%)
            }
            catch (Exception ex)
            {
            }

            Bitmap bmp = new Bitmap(qrCode.Encode(qrcode, System.Text.Encoding.Default), 130, 130);

            bmp.Save(MainModel.ServerPath + "orderqrcoe.bmp",System.Drawing.Imaging.ImageFormat.Bmp);
            //    using (Bitmap bmp24 = new Bitmap(bmp.Width, bmp.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb))
            //    using (Graphics g = Graphics.FromImage(bmp24))
            //    {
            //        g.DrawImage(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
            //        bmp24.Save(MainModel.ServerPath + "qrcoe.bmp");
            //    }

            //bmp.Save(MainModel.ServerPath+"qrcoe.bmp");

            return bmp;
        }


        public static Bitmap KiResizeImage(Bitmap bmp, int newW, int newH)
        {

            try
            {

                Bitmap b = new Bitmap(newW, newH);

                Graphics g = Graphics.FromImage(b);



                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;



                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);

                g.Dispose();



                return b;

            }

            catch
            {

                return null;

            }

        } 

    }
}
