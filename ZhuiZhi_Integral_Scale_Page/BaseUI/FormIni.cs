using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;

namespace ZhuiZhi_Integral_Scale_UncleFruit.BaseUI
{
    public partial class FormIni : Form
    {

        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();
        public FormIni()
        {
            InitializeComponent();
        }

        private void FormIni_Load(object sender, EventArgs e)
        {
            

        }

        private void FormIni_Shown(object sender, EventArgs e)
        {
            try
            {
                frmLogin frlogin = new frmLogin();
                asf.AutoScaleControlTest(frlogin, 1180, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                frlogin.Show();

                this.Hide();
            }
            catch { }
        }
    }
}
