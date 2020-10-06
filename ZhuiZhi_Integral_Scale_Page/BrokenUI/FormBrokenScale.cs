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
    public partial class FormBrokenScale : Form
    {
        private Product ParaProduct;

        public FormBrokenScale(Product pro)
        {
            InitializeComponent();
            ParaProduct = pro;
        }

        private void FormScale_Load(object sender, EventArgs e)
        {
           
        }

        private void FormBrokenScale_Shown(object sender, EventArgs e)
        {
            LoadProInfo();
            IniForm();
            timerScale.Enabled = true;
        }


        private void LoadProInfo()
        {
            try
            {
                UpdateWeight();
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

               
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载商品显示信息异常" + ex.StackTrace, true);
            }
        }

        private void IniForm()
        {
            try
            {
                string ScaleName = INIManager.GetIni("Scale", "ScaleName", MainModel.IniPath);

                if (ScaleName == ScaleType.中科英泰.ToString())
                {
                    pnlTare.Visible = true;
                }
                else if (ScaleName == ScaleType.顶尖.ToString())
                {
                    pnlTare.Visible = false;
                }
                else if (ScaleName == ScaleType.托利多.ToString())
                {
                    pnlTare.Visible = true;
                }
                else if (ScaleName == ScaleType.顶尖PS1X.ToString())
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
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

        private void btnInput_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Retry;
            this.Close();
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

            CurrentNetWeight = CurrentScaleResult.NetWeight;
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
        public decimal CurrentNetWeight = 0;
         

        private void UpdateWeight()
        {
            try
            {
                CurrentNetWeight = CurrentTotalWeight - MainModel.CurrentTareWeight;

                lblTotalWeight.Text = CurrentTotalWeight.ToString();
                lblTareWeight.Text = MainModel.CurrentTareWeight.ToString();
                lblNetWeight.Text = CurrentNetWeight.ToString();


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



       
    }
}
