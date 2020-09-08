using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    public partial class FormSevePassWoedOK : Form
    {
        public FormSevePassWoedOK()
        {
            InitializeComponent();
        }

        private void FormSevePassWoedOK_Shown(object sender, EventArgs e)
        {
            MemberCenterMediaHelper.ShowChangePassWordOK();
        }

        private void FormSevePassWoedOK_FormClosing(object sender, FormClosingEventArgs e)
        {
            MemberCenterMediaHelper.HidePayInfo();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
