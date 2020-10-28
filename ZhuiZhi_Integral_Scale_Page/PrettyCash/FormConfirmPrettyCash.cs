using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZhuiZhi_Integral_Scale_UncleFruit.PrettyCash
{
    public partial class FormConfirmPrettyCash : Form
    {

        private decimal CurrentPrettyCash = 0;
        public FormConfirmPrettyCash(decimal prettycash)
        {
            InitializeComponent();
            CurrentPrettyCash = prettycash;

            lblPrettyCash.Text = "￥" + prettycash.ToString("f2");
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();

        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            ZhuiZhi_Integral_Scale_UncleFruit.Common.ReceiptUtil.SetPrettyCash(CurrentPrettyCash);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
