using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.ScaleFactory;

namespace ZhuiZhi_Integral_Scale_UncleFruit
{
    public partial class FormScale : Form
    {
        private Product ParaProduct;

        private FormLabelPrint frmlabelprint = new FormLabelPrint();

        /// <summary>
        /// 数据处理委托方法
        /// </summary>
        /// <param name="type">0：返回  1：确认数字</param>
        public delegate void DataRecHandleDelegate(int type);
        /// <summary>
        /// 数据接收事件
        /// </summary>
        public event DataRecHandleDelegate DataReceiveHandle;

        public FormScale()
        {
            InitializeComponent();
            //使用委托的话frmmain界面会卡死
            Control.CheckForIllegalCrossThreadCalls = false;
           
        }
        public FormScale(Product pro)
        {
            InitializeComponent();
            ParaProduct=pro;
        }

        private void FormScale_Resize(object sender, EventArgs e)
        {
            SetWindowRegion();
        }

        private void FormScale_Load(object sender, EventArgs e)
        {
            //LoadProInfo();
            //IniForm();

            //UpdateWeight();
            //bgwLoadProInfo.RunWorkerAsync();

            System.Threading.Thread threadmqtt = new System.Threading.Thread(LoadProInfo);
            threadmqtt.IsBackground = true;
            threadmqtt.Start(true);
        }

        public void UpInfo(Product pro)
        {
            CurrentNetWeight = 0;
            CurrentTotalWeight = 0;
            ParaProduct = pro;
            //LoadProInfo(null);
            IniForm();
            timerScale.Enabled = true;
        }


        private void IniForm()
        {
            try
            {
                
                if (MainModel.WhetherPrint && !MainModel.WhetherAutoPrint)//打开打码 关闭自动打码
                {

                    tplBtn.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 15);
                    tplBtn.ColumnStyles[4] = new ColumnStyle(SizeType.Percent, 33);
                    btnOK.ShowText = "确认";
                    //tplBtn.ColumnStyles[]
                }
                else if (MainModel.WhetherPrint && MainModel.WhetherAutoPrint)//打开打码 打开自动打码
                {
                    tplBtn.ColumnStyles[3] = new ColumnStyle(SizeType.Percent, 0);
                    tplBtn.ColumnStyles[4] = new ColumnStyle(SizeType.Percent, 0);

                    btnOK.ShowText = "确认并打码";
                }
                else if (!MainModel.WhetherPrint)//关闭打码
                {
                    tplBtn.ColumnStyles[3] = new ColumnStyle(SizeType.Percent, 0);
                    tplBtn.ColumnStyles[4] = new ColumnStyle(SizeType.Percent, 0);
                    btnOK.ShowText = "确认";
                }

                string ScaleName = INIManager.GetIni("Scale", "ScaleName", MainModel.IniPath);

                if (ScaleName == ScaleType.中科英泰.ToString())
                {
                    pnlTare.Visible = true;
                }
                else if (ScaleName == ScaleType.爱宝.ToString())
                {
                    pnlTare.Visible = false;
                }
                else if (ScaleName == ScaleType.托利多.ToString())
                {
                    pnlTare.Visible = true;
                }
                else if (ScaleName == ScaleType.易捷通.ToString())
                {
                    pnlTare.Visible = false;
                }
                else
                {
                    pnlTare.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("初始化称重页面异常"+ex.Message,true);
            }
        }

        /// <summary>
        /// 设置窗体的Region   画半径为10的圆角
        /// </summary>
        public void SetWindowRegion()
        {
            try
            {
                GraphicsPath FormPath;
                Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
                FormPath = GetRoundedRectPath(rect, 10);
                this.Region = new Region(FormPath);
            }
            catch (Exception ex)
            {

            }

        }
        /// <summary>
        /// 绘制圆角路径
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            try
            {
                int diameter = radius;
                Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
                GraphicsPath path = new GraphicsPath();

                // 左上角
                path.AddArc(arcRect, 180, 90);

                // 右上角
                arcRect.X = rect.Right - diameter;
                path.AddArc(arcRect, 270, 90);

                // 右下角
                arcRect.Y = rect.Bottom - diameter;
                path.AddArc(arcRect, 0, 90);

                // 左下角
                arcRect.X = rect.Left;
                path.AddArc(arcRect, 90, 90);
                path.CloseFigure();//闭合曲线
                return path;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (DataReceiveHandle != null)
            {
                this.DataReceiveHandle.BeginInvoke(0, null, null);
            }

            
            this.Close();
        }

        //去皮
        private void btnTare_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                ScaleResult result = ScaleGlobalHelper.SetTare();

                if (result.WhetherSuccess)
                {
                   // MainModel.ShowLog("去皮完成",false);
                }
                else
                {
                   // MainModel.ShowLog("去皮失败"+result.Message,true);
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("去皮异常" + ex.Message, true);
            }
         
        }

        //置零
        private void btnZero_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                ScaleResult result = ScaleGlobalHelper.SetZero();
                //ScaleResult result = ScaleGlobalHelper.ClearTare();
                if (result.WhetherSuccess)
                {
                   // MainModel.ShowLog("置零完成", false);
                }
                else
                {
                   // MainModel.ShowLog("置零失败" + result.Message, true);
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("置零异常" + ex.Message, true);
            }
         
        }
        //打码
        private void btnPrint_ButtonClick(object sender, EventArgs e)
        {
            try
            {

                //未获取到稳定重量数据无效
                if (CurrentScaleResult == null || !CurrentScaleResult.WhetherSuccess || !CurrentScaleResult.WhetherStable)
                {
                    MainModel.ShowLog("当前称重还没有稳定", false);
                    return;
                }
                //重量不允许为0
                if (CurrentScaleResult.NetWeight <= 0)
                {
                    MainModel.ShowLog("当前没有重量",false);
                    return;
                }
                //打码之后就停止称重
                timerScale.Enabled = false;
                ParaProduct.specnum = CurrentScaleResult.NetWeight;
                ParaProduct.num = 1;

                LabelPrintHelper.LabelPrint(ParaProduct);
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("打码异常"+ex.Message,true);
            }
        }

        //确认
        private void btnOK_ButtonClick(object sender, EventArgs e)
        {
            //未获取到稳定重量数据无效
            if (CurrentScaleResult == null || !CurrentScaleResult.WhetherSuccess || !CurrentScaleResult.WhetherStable)
            {
                MainModel.ShowLog("当前称重还没有稳定", false);
                return;
            }
            //重量不允许为0
            if (CurrentScaleResult.NetWeight <= 0)
            {
                MainModel.ShowLog("当前没有重量", false);
                return;
            }


            ParaProduct.specnum = CurrentScaleResult.NetWeight;
            ParaProduct.num = 1;

            //离线计算用
            if (ParaProduct.price == null)
            {
                ParaProduct.price = new Price();
            }
            ParaProduct.price.specnum = ParaProduct.specnum;


            if (MainModel.WhetherPrint && MainModel.WhetherAutoPrint)
            {
                LabelPrintHelper.LabelPrint(ParaProduct);
            }
            this.DialogResult =DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 当前总重量
        /// </summary>
        private decimal CurrentTotalWeight=0;

        /// <summary>
        /// 当前净重
        /// </summary>
        private decimal CurrentNetWeight = 0;
         
        private void SetWeight(decimal weight)
        {
            try
            {
                CurrentTotalWeight = weight;
               

                UpdateWeight();

            }
            catch (Exception ex)
            {
                MainModel.ShowLog("解析重量信息异常"+ex.Message,true);
            }
        }

        private void UpdateWeight()
        {
            try
            {
                CurrentNetWeight = CurrentTotalWeight - MainModel.CurrentTareWeight;

                lblTotalWeight.Text = CurrentTotalWeight.ToString();
                lblTareWeight.Text = MainModel.CurrentTareWeight.ToString();
                lblNetWeight.Text = CurrentNetWeight.ToString();

                if (CurrentNetWeight <= 0)
                {
                    lblTotalPay.Text ="0.00";
                }
                else {
                    lblTotalPay.Text = Math.Round(CurrentNetWeight * ParaProduct.price.saleprice, 2, MidpointRounding.AwayFromZero) + "";
}

               

                lblTotalUnit.Left = lblTotalWeight.Left + lblTotalWeight.Width;
                lblTareUnit.Left = lblTareWeight.Left + lblTareWeight.Width;
                lblNetUnit.Left = lblNetWeight.Left + lblNetWeight.Width;
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("显示重量信息异常",true);
            }
        }


        //退出页面要关闭获取电子秤数据
        private void FormScale_FormClosed(object sender, FormClosedEventArgs e)
        {
            timerScale.Enabled = false;
        }


        private ScaleResult CurrentScaleResult = null;
        private void timerScale_Tick(object sender, EventArgs e)
        {
            try
            {
                timerScale.Enabled = false;
                CurrentScaleResult = ScaleGlobalHelper.GetWeight();

                if (CurrentScaleResult.WhetherSuccess)
                {
                    lblTotalWeight.Text = CurrentScaleResult.TotalWeight + "";
                    lblTareWeight.Text = CurrentScaleResult.TareWeight + "";
                    lblNetWeight.Text = CurrentScaleResult.NetWeight + "";

                    if (CurrentScaleResult.NetWeight <= 0)
                    {
                        lblTotalPay.Text = "0.00";
                    }
                    else
                    {
                        lblTotalPay.Text = Math.Round(CurrentScaleResult.NetWeight * ParaProduct.price.saleprice, 2, MidpointRounding.AwayFromZero) + "";
                    }

                    lblTotalUnit.Left = lblTotalWeight.Left + lblTotalWeight.Width;
                    lblTareUnit.Left = lblTareWeight.Left + lblTareWeight.Width;
                    lblNetUnit.Left = lblNetWeight.Left + lblNetWeight.Width;                 
                }
                else
                {
                    LogManager.WriteLog("SCALE", "获取电子秤重量信息错误" + CurrentScaleResult.Message);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("SCALE", "获取电子秤重量信息异常" + ex.Message);
            }
            finally
            {
                timerScale.Enabled = true;
            }
        }

        private void 虚拟重量_Click(object sender, EventArgs e)
        {
            timerScale.Enabled = false;
            CurrentScaleResult = new ScaleResult();
            CurrentScaleResult.WhetherSuccess = true;
            CurrentScaleResult.WhetherStable = true;
            CurrentScaleResult.NetWeight = 1;
       
        }

        private void LoadProInfo(object obj)
        {
            try
            {
                lblGoodName.Text = ParaProduct.skuname;
                //lblPromotion.Text = ParaProduct.pricetag;
                string imgurl = ParaProduct.mainimg;
                string imgname = imgurl.Substring(imgurl.LastIndexOf("/") + 1) + ".bmp"; //URL 最后的值

                if (File.Exists(MainModel.ProductPicPath + imgname))
                {
                    pnlpicItem.BackgroundImage = Image.FromFile(MainModel.ProductPicPath + imgname);
                }
                else
                {
                    try
                    {

                        //if (!MainModel.WhetherHalfOffLine)  //半离线状态下不加载图片
                        //{
                        Image _image = Image.FromStream(System.Net.WebRequest.Create(imgurl).GetResponse().GetResponseStream());
                        _image.Save(MainModel.ProductPicPath + imgname);
                        pnlpicItem.BackgroundImage = Image.FromFile(MainModel.ProductPicPath + imgname);
                        //}

                    }
                    catch { }
                }


                switch (ParaProduct.pricetagid)
                {
                    case 1: picGoodPricetag.Visible = true; picGoodPricetag.BackgroundImage = pic会员.Image; break;
                    case 2: picGoodPricetag.Visible = true; picGoodPricetag.BackgroundImage = pic折扣.Image; break;
                    case 3: picGoodPricetag.Visible = true; picGoodPricetag.BackgroundImage = pic直降.Image; break;
                    case 4: picGoodPricetag.Visible = true; picGoodPricetag.BackgroundImage = pic优享.Image; break;
                    default: picGoodPricetag.Visible = false; break;
                }

                if (ParaProduct.price != null)
                {
                    if (ParaProduct.price.saleprice == ParaProduct.price.originprice)
                    {
                        lblPrice.Text = "￥" + ParaProduct.price.saleprice.ToString("f2");
                        lblMemberPrice.Visible = false;
                    }
                    else
                    {
                        lblPrice.Text = "￥" + ParaProduct.price.saleprice.ToString("f2");
                        lblMemberPrice.Visible = true;
                        if (!string.IsNullOrEmpty(ParaProduct.price.salepricedesc))
                        {
                            lblPriceDetail.Text += "(" + ParaProduct.price.salepricedesc + ")";
                        }

                        if (ParaProduct.price.strikeout == 1)
                        {
                            lblMemberPrice.Font = new System.Drawing.Font("微软雅黑", lblMemberPrice.Font.Size, FontStyle.Strikeout);
                            lblMemberPrice.Text = ParaProduct.price.originprice.ToString("f2") + "/" + ParaProduct.saleunit;
                        }
                        else
                        {
                            lblMemberPrice.Font = new System.Drawing.Font("微软雅黑", lblMemberPrice.Font.Size, FontStyle.Regular);
                            lblMemberPrice.Text = ParaProduct.price.originprice.ToString("f2") + "/" + ParaProduct.saleunit;
                        }


                    }
                }
                else
                {

                }
                lblPriceDetail.Text = "/" + ParaProduct.saleunit;
                lblPriceDetail.Left = lblPrice.Left + lblPrice.Width;

                lblMemberPrice.Left = Math.Max(btnCancel.Right - lblMemberPrice.Width, lblPriceDetail.Right);
            }
            catch { }
        }
    }
}
