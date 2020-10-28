using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit
{
    public partial class FormScaleBack : Form
    {

        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();

        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();

        private Product ParaProduct;


        public FormScaleBack(Product pro)
        {
            InitializeComponent();
            ParaProduct = pro;
        }

        private void FormScaleBack_Load(object sender, EventArgs e)
        {

        }

        private void FormScaleBack_Shown(object sender, EventArgs e)
        {
            try
            {

                if (MainModel.frmscale == null)
                {
                    MainModel.frmscale = new FormScale(ParaProduct);

                    
                    asf.AutoScaleControlTest(MainModel.frmscale, 380, 480, Convert.ToInt32(MainModel.midScale * 380), Convert.ToInt32(MainModel.midScale * 480), true);
                    MainModel.frmscale.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - MainModel.frmscale.Width) / 2, (Screen.AllScreens[0].Bounds.Height - MainModel.frmscale.Height) / 2);
                    MainModel.frmscale.TopMost = true;
                    MainModel.frmscale.DataReceiveHandle += FormScale_DataReceiveHandle;
                    MainModel.frmscale.Show();
                }
                else
                {
                    MainModel.frmscale.DataReceiveHandle += FormScale_DataReceiveHandle;
                    MainModel.frmscale.Show();
                    MainModel.frmscale.UpInfo(ParaProduct);
                }
              
            }
            catch (Exception ex)
            {
                MainModel.frmscale = null;
                this.Close();
            }
        }




        private void FormScale_DataReceiveHandle(int type)
        {
            try
            {
                if (type == 1)
                {
                    this.Invoke(new InvokeHandler(delegate()
                    {
                        this.DialogResult = DialogResult.OK;


                    }));
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;  
                }
                MainModel.frmscale.DataReceiveHandle -= FormScale_DataReceiveHandle;
                this.Close();
            }
            catch (Exception ex)
            {
                this.Close();
                LogManager.WriteLog("ERROR", "处理数字窗体数据异常" + ex.Message);
            }
        }



    }
}
