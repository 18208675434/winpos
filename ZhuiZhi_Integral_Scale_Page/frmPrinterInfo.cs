using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZhuiZhi_Integral_Scale_UncleFruit
{
    public partial class frmPrinterInfo : Form
    {
        public frmPrinterInfo()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void lblNotCheck_Click(object sender, EventArgs e)
        {

            INIManager.SetIni("Print", "PrintCheck", "false", MainModel.IniPath);
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
