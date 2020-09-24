using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    public partial class FormCustomMoney : Form
    {
        public FormCustomMoney()
        {
            InitializeComponent();
        }
        private static AutoSizeFormUtil asf = new AutoSizeFormUtil();

        private void button1_Click(object sender, EventArgs e)
        {
            BackHelper.ShowFormBackGround();
            //BackHelper.HideFormBackGround();
            FormCustomreCharge charge = new FormCustomreCharge();
            asf.AutoScaleControlTest(charge, 390, 450, 390 * MainModel.midScale, 450 * MainModel.midScale, true);
            charge.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - charge.Width) / 2, (Screen.AllScreens[0].Bounds.Height - charge.Height) / 2);
            charge.TopMost = true;
            charge.ShowDialog();
            charge.Dispose();
            charge.Close();
            this.Close();
        }

        private void pictureCancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            BackHelper.HideFormBackGround();
            MainModel.CoustomMoney = "+";
            MainModel.ZMoney = "自定义金额";
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
