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

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    public partial class FormChangePhonePhysicalCard : Form
    {
        MemberCenterHttpUtil membercenterhttputil = new MemberCenterHttpUtil();
        
        public FormChangePhonePhysicalCard()
        {
            
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            GlobalUtil.CloseOSK();
            this.Close();
        }

        private void FormChangePhonePhysicalCard_Shown(object sender, EventArgs e)
        {
        }

        private void FormChangePhonePhysicalCard_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            string errormsg = "";
            string oldcard = txtOldCardNumber.Text;
            bool result = membercenterhttputil.GetMactchCardOldCard(oldcard, ref errormsg);
            if (result)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MainModel.ShowLog(errormsg, false);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void txtOldCardNumber_Load(object sender, EventArgs e)
        {

        }
    }
}
