using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MenuUI
{
    public partial class FormWebOrderDetailBack : Form
    {

        public Order paraOrder = null;
        FormWebOrderDetail frmwebdetial = null;

        //<summary>
        //按比例缩放页面及控件
        //</summary>
        ZhuiZhi_Integral_Scale_UncleFruit.Common.AutoSizeFormUtil asf = new ZhuiZhi_Integral_Scale_UncleFruit.Common.AutoSizeFormUtil();

        public FormWebOrderDetailBack( Order order)
        {
            InitializeComponent();
            paraOrder = order;
        }


        private void Frm_Load(object sender, EventArgs e)
        {
            try
            {
                frmwebdetial = new FormWebOrderDetail(paraOrder);
                frmwebdetial.Location = new Point(0,0);

                asf.AutoScaleControlTest(frmwebdetial, 1100, 760, Screen.AllScreens[0].Bounds.Width*85/100, Screen.AllScreens[0].Bounds.Height, true);
                frmwebdetial.TopMost = true;
                frmwebdetial.DataReceiveHandle += Form_DataReceiveHandle;
                frmwebdetial.Show();


                picClose.Location = new Point(frmwebdetial.Width,frmwebdetial.Height/2);
            }
            catch (Exception ex)
            {
                ZhuiZhi_Integral_Scale_UncleFruit.Model.MainModel.ShowLog("加载确认窗体窗体异常" + ex.Message, true);
                this.Close();
            }
        }



        private void Form_DataReceiveHandle(int type)
        {
            try
            {
                if (type == 0)
                {
                    this.DialogResult = DialogResult.Cancel;
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                }

            }
            catch (Exception ex)
            {
                this.Close();
                ZhuiZhi_Integral_Scale_UncleFruit.Common.LogManager.WriteLog("ERROR", "处理确认窗体结果异常" + ex.Message);
            }
            finally
            {
                this.Close();
            }
        }

        private void FormWebOrderDetailBack_Click(object sender, EventArgs e)
        {
            if (frmwebdetial != null)
            {
                frmwebdetial.Dispose();
            }

            this.Close();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            if (frmwebdetial != null)
            {
                frmwebdetial.Dispose();
            }

            this.Close();
        }


    }
}
