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
using ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    public partial class FormCustomMoney : Form
    {

        public ListAllTemplate CustomTemplate = null;
        public FormCustomMoney()
        {
            InitializeComponent();
        }
        private static AutoSizeFormUtil asf = new AutoSizeFormUtil();

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            BackHelper.ShowFormBackGround();
            //BackHelper.HideFormBackGround();
            FormCustomreCharge charge = new FormCustomreCharge();
            asf.AutoScaleControlTest(charge, 390, 450, 390 * MainModel.midScale, 450 * MainModel.midScale, true);
            charge.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - charge.Width) / 2, (Screen.AllScreens[0].Bounds.Height - charge.Height) / 2);
            charge.TopMost = true;

            this.Hide();
            charge.ShowDialog();
            charge.Dispose();

            CustomTemplate = charge.CustomTemplate;

            this.Close();

        }

        private void pictureCancle_Click(object sender, EventArgs e)
        {
            CustomTemplate = null;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            BackHelper.ShowFormBackGround();
            //BackHelper.HideFormBackGround();
            FormZsongMoney zeng = new FormZsongMoney();
            asf.AutoScaleControlTest(zeng, 380, 470, 380 * MainModel.midScale, 470 * MainModel.midScale, true);
            zeng.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - zeng.Width) / 2, (Screen.AllScreens[0].Bounds.Height - zeng.Height) / 2);
            zeng.TopMost = true;

            this.Hide();
            zeng.ShowDialog();
            zeng.Dispose();
            CustomTemplate = zeng.CustomTemplate;
            this.Close();
            this.Close();
        }
    }
}
