using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.HelperUI
{
    public partial class FormLabelPrint : Form
    {
        BarCode128 code128 = new BarCode128();

        private string PrintName = "";
        public FormLabelPrint()
        {
            InitializeComponent();
        }

        public  bool PrintLabel(Product pro)
        {
            try
            {

                PrintName = INIManager.GetIni("Print", "LabelPrintName", MainModel.IniPath);

                lblTenantName.Text = MainModel.CurrentShopInfo.tenantname;
                string barcode = "";
                CartUtil.GetBarCode(pro);
                lblSkuName.Text = pro.skuname;
                lblMarketDate.Text = "上市日期:" + DateTime.Now.ToString("yyyy-MM-dd");
                lblPackingDate.Text = "包装日期:" + DateTime.Now.ToString("yyyy-MM-dd");
                lblExpirationDate.Text = "保质日期:" + DateTime.Now.AddDays(pro.ShelfLife).ToString("yyyy-MM-dd");//TOOD
                lblSalePrice.Text = pro.price.saleprice.ToString("f2");
                lblPriceDesc.Text = "元/" + pro.price.unit;
                if (pro.goodstagid == 0) //标品
                {
                    lblStrNum.Text = "数量(" + pro.price.unit+")";
                    lblNum.Text = pro.num.ToString().PadRight(4,' ');

                    lblTotal.Text = pro.price.saleprice.ToString("f2");
                }
                else
                {
                    lblStrNum.Text = "重量(" + pro.price.unit + ")";

                    lblNum.Text = pro.specnum.ToString().PadRight(4, ' ');
                    lblTotal.Text=Math.Round(pro.specnum * pro.price.saleprice, 2, MidpointRounding.AwayFromZero).ToString("f2");
                }

                //lblTotal.Text = pro.price.total.ToString("f2");
                lblCode.Text = pro.barcode;

                lblSkuName.Left = (pnlLabel.Width - lblSkuName.Width) / 2;
                lblTenantName.Left = (pnlLabel.Width - lblTenantName.Width) / 2;

                lblPriceDesc.Left = lblSalePrice.Left + lblSalePrice.Width-2;
                lblNum.Left = pnlLabel.Width - lblNum.Width - 15;
                lblTotal.Left = pnlLabel.Width - lblTotal.Width - 15;
                PrintDocument pd = new PrintDocument();
            
                //pd.PrinterSettings.DefaultPageSettings.PaperSize.Kind = PaperKind.Custom;
                //pd.PrinterSettings.DefaultPageSettings.PaperSize.Width = 30;
                //pd.PrinterSettings.DefaultPageSettings.PaperSize.Height = 20;

                pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);

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

               // pd.Print();

                return true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("LabelPrint", "解析打印内容异常:" + ex.Message);
                return false;
            }
        }

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

        /// <summary>
        /// 画图
        /// </summary>
        private void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {

                e.Graphics.DrawString(lblSkuName.Text, lblSkuName.Font, Brushes.Black, lblSkuName.Location.X, lblSkuName.Location.Y);
                e.Graphics.DrawString(lblMarketDate.Text, lblMarketDate.Font, Brushes.Black, lblMarketDate.Location.X, lblMarketDate.Location.Y);
                e.Graphics.DrawString(lblPackingDate.Text, lblPackingDate.Font, Brushes.Black, lblPackingDate.Location.X, lblPackingDate.Location.Y);
                e.Graphics.DrawString(lblExpirationDate.Text, lblExpirationDate.Font, Brushes.Black, lblExpirationDate.Location.X, lblExpirationDate.Location.Y);
                e.Graphics.DrawString(lblStrSalePrice.Text, lblStrSalePrice.Font, Brushes.Black, lblStrSalePrice.Location.X, lblStrSalePrice.Location.Y);
                e.Graphics.DrawString(lblSalePrice.Text, lblSalePrice.Font, Brushes.Black, lblSalePrice.Location.X, lblSalePrice.Location.Y);
                e.Graphics.DrawString(lblPriceDesc.Text, lblPriceDesc.Font, Brushes.Black, lblPriceDesc.Location.X, lblPriceDesc.Location.Y);
                e.Graphics.DrawString(lblStrNum.Text, lblStrNum.Font, Brushes.Black, lblStrNum.Location.X, lblStrNum.Location.Y);
                e.Graphics.DrawString(lblNum.Text, lblNum.Font, Brushes.Black, lblNum.Location.X, lblNum.Location.Y);
                e.Graphics.DrawString(lblTotal.Text, lblTotal.Font, Brushes.Black, lblTotal.Location.X, lblTotal.Location.Y);
                e.Graphics.DrawString(lblStrTotal.Text, lblStrTotal.Font, Brushes.Black, lblStrTotal.Location.X, lblStrTotal.Location.Y);
                e.Graphics.DrawString(lblTenantName.Text, lblTenantName.Font, Brushes.Black, lblTenantName.Location.X, lblTenantName.Location.Y);
                e.Graphics.DrawString(lblCode.Text, lblCode.Font, Brushes.Black, lblCode.Location.X, lblCode.Location.Y);



                e.Graphics.DrawImage((Image)code128.EncodeBarcode(lblCode.Text, picCode.Width + 100, picCode.Height, false), picCode.Location.X, picCode.Location.Y, picCode.Width, picCode.Height);


                //Point point1 = new Point(0, (int)(4.3 / 25.4 * 1000));
                //Point point2 = new Point((int)(7.8 / 25.4 * 1000), (int)(4.3 / 25.4 * 1000));
                //Pen blackPen = new Pen(Color.Black, 3);
                //e.Graphics.DrawLine(blackPen, point1, point2);

                //Application.DoEvents();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("LabelPrint","描绘标签内容错误: \r\n" + ex.Message);
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);

            pd.PrinterSettings.DefaultPageSettings.PaperSize.Width = 60;
            pd.PrinterSettings.DefaultPageSettings.PaperSize.Height = 40;

            ////不弹框<正在打印>直接打印
            //PrintController PrintStandard = new StandardPrintController();
            //pd.PrintController = PrintStandard;
            //pd.DocumentName = "rrere";
            pd.Print();

            
        }

    }

    /// <summary>
    /// 画Code128条形码
    /// </summary>
    public class BarCode128
    {
        #region ASCII从32到127对应的条码区,由3个条、3个空、共11个单元构成,符号内含校验码
        private string[] Code128Encoding = new string[] 
            { 
                "11011001100", "11001101100", "11001100110", "10010011000", "10010001100", "10001001100", "10011001000",
                "10011000100", "10001100100", "11001001000", "11001000100", "11000100100", "10110011100", "10011011100", 
                "10011001110", "10111001100", "10011101100", "10011100110", "11001110010", "11001011100", "11001001110", 
                "11011100100", "11001110100", "11101101110", "11101001100", "11100101100", "11100100110", "11101100100", 
                "11100110100", "11100110010", "11011011000", "11011000110", "11000110110", "10100011000", "10001011000", 
                "10001000110", "10110001000", "10001101000", "10001100010", "11010001000", "11000101000", "11000100010", 
                "10110111000", "10110001110", "10001101110", "10111011000", "10111000110", "10001110110", "11101110110", 
                "11010001110", "11000101110", "11011101000", "11011100010", "11011101110", "11101011000", "11101000110", 
                "11100010110", "11101101000", "11101100010", "11100011010", "11101111010", "11001000010", "11110001010", 
                "10100110000", "10100001100", "10010110000", "10010000110", "10000101100", "10000100110", "10110010000", 
                "10110000100", "10011010000", "10011000010", "10000110100", "10000110010", "11000010010", "11001010000", 
                "11110111010", "11000010100", "10001111010", "10100111100", "10010111100", "10010011110", "10111100100", 
                "10011110100", "10011110010", "11110100100", "11110010100", "11110010010", "11011011110", "11011110110", 
                "11110110110", "10101111000", "10100011110", "10001011110", "10111101000", "10111100010", "11110101000", 
                "11110100010", "10111011110", "10111101110", "11101011110", "11110101110", "11010000100", "11010010000", 
                "11010011100" 
            };
        #endregion

        //固定码尾
        private const string Code128Stop = "11000111010", Code128End = "11";

        //变更
        private enum Code128ChangeModes
        {
            CodeA = 101,
            CodeB = 100,
            CodeC = 99
        };

        //各类编码的码头
        private enum Code128StartModes
        {
            CodeUnset = 0,
            CodeA = 103,
            CodeB = 104,
            CodeC = 105
        };

        /// <summary> 
        /// 绘制Code128码(以像素为单位) 
        /// </summary>
        public int EncodeBarcode(string code, System.Drawing.Graphics g, int x, int y, int width, int height, bool showText)
        {
            if (string.IsNullOrEmpty(code))
                new Exception("条码不能为空");

            List<int> encoded = CodetoEncoded(code); //1.拆分转义

            encoded.Add(CheckDigitCode128(encoded)); //2.加入校验码

            string encodestring = EncodeString(encoded); //3.编码

            if (showText) //计算文本的大小,字体占图像的1/4高
            {
                Font font = new System.Drawing.Font("宋体", height / 5F, System.Drawing.FontStyle.Regular, GraphicsUnit.Pixel, ((byte)(0)));
                SizeF size = g.MeasureString(code, font);
                height = height - (int)size.Height;
                g.DrawString(code, font, System.Drawing.Brushes.Black, x, y + height);
                int w = DrawBarCode(g, encodestring, x, y, width, height); //4.绘制

                return ((int)size.Width > w ? (int)size.Width : w);
            }
            else
                return DrawBarCode(g, encodestring, x, y, width, height); //4.绘制

        }

        /// <summary>
        /// 1.检测并将字符串拆分并加入码头
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private List<int> CodetoEncoded(string code)
        {
            List<int> encoded = new List<int>();

            int type = 0;//2:B类,3:C类

            for (int i = 0; code.Length > 0; i++)
            {
                int k = isNumber(code);

                if (k >= 4) //连续偶个数字可优先使用C类(其实并不定要转C类，但能用C类时条码会更短) 
                {
                    if (type == 0)
                        encoded.Add((int)Code128StartModes.CodeC); //加入码头

                    else if (type != 3)
                        encoded.Add((int)(Code128ChangeModes.CodeC)); //转义

                    type = 3;

                    for (int j = 0; j < k; j = j + 2) //两位数字合为一个码身
                    {
                        encoded.Add(Int32.Parse(code.Substring(0, 2)));
                        code = code.Substring(2);
                    }
                }
                else
                {
                    if ((int)code[0] < 32 || (int)code[0] > 126)
                        throw new Exception("字符串必须是数字或字母");

                    if (type == 0)
                        encoded.Add((int)Code128StartModes.CodeB); //加入码头

                    else if (type != 2)
                        encoded.Add((int)(Code128ChangeModes.CodeB)); //转义

                    type = 2;
                    encoded.Add((int)code[0] - 32);//字符串转为ASCII-32 
                    code = code.Substring(1);
                }
            }
            return encoded;
        }

        /// <summary>
        /// 2.校验码
        /// </summary>
        /// <param name="encoded"></param>
        /// <returns></returns>
        private int CheckDigitCode128(List<int> encoded)
        {
            int check = encoded[0];

            for (int i = 1; i < encoded.Count; i++)
                check = check + (encoded[i] * i);

            return (check % 103);
        }

        /// <summary>
        /// 2.编码(对应Code128Encoding数组) 
        /// </summary>
        /// <param name="encoded"></param>
        /// <returns></returns>
        private string EncodeString(List<int> encoded)
        {
            string encodedString = "";
            for (int i = 0; i < encoded.Count; i++)
            {
                encodedString += Code128Encoding[encoded[i]];
            }
            encodedString += Code128Stop + Code128End; // 加入结束码 
            return encodedString;
        }

        /// <summary>
        /// 4.绘制条码(返回实际图像宽度) 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="encodeString"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private int DrawBarCode(System.Drawing.Graphics g, string encodeString, int x, int y, int width, int height)
        {
            int w = width / encodeString.Length;

            for (int i = 0; i < encodeString.Length; i++)
            {
                g.FillRectangle(encodeString[i] == '0' ? System.Drawing.Brushes.White : System.Drawing.Brushes.Black, x, y, w, height);
                x += w;
            }

            return w * (encodeString.Length + 2);
        }

        /// <summary>
        /// 检测是否连续偶个数字,返回连续数字的长度 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private int isNumber(string code)
        {
            int k = 0;

            for (int i = 0; i < code.Length; i++)
            {
                if (char.IsNumber(code[i]))
                    k++;
                else
                    break;
            }
            if (k % 2 != 0) k--;

            return k;
        }

        /// <summary> 
        /// 绘制Code128码到图片 
        /// </summary> 
        public Image EncodeBarcode(string code, int width, int height, bool showText)
        {
            Bitmap image = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(image))
            {
                g.Clear(Color.White);

                int w = EncodeBarcode(code, g, 0, 0, width, height, showText);

                Bitmap image2 = new Bitmap(w, height); //剪切多余的空白; 

                using (Graphics g2 = Graphics.FromImage(image2))
                {
                    g2.DrawImage(image, 0, 0);
                    return image2;
                }
            }
        }

        /// <summary> 
        /// 绘制Code128码到流 
        /// </summary> 
        public byte[] EncodeBarcodeByte(string code, int width, int height, bool showText)
        {
            Image image = EncodeBarcode(code, width, height, showText);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            byte[] byteImage = ms.ToArray();
            ms.Close();
            image.Dispose();
            return byteImage;
        }
    }
}
