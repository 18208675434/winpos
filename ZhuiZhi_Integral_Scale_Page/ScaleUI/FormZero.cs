using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.ScaleFactory;

namespace ZhuiZhi_Integral_Scale_UncleFruit.ScaleUI
{
    public partial class FormZero : Form
    {
        public FormZero()
        {
            InitializeComponent();
        }

        private void lblZero_Click(object sender, EventArgs e)
        {
            try
            {

                //ToledoResult result = ToledoUtil.SendZero();

                ScaleResult result = ScaleGlobalHelper.SetZero();
                if (!result.WhetherSuccess)
                {
                    MainModel.ShowLog(result.Message, false);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("置零异常" + ex.Message, true);

            }
           
        }

        private void FormZero_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
