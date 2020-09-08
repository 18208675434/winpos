using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    public partial class FormChangePhoneNewCard : Form
    {
        MemberCenterHttpUtil membercenterhttputil = new MemberCenterHttpUtil();
        public FormChangePhoneNewCard()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormChangePhoneNewCard_Shown(object sender, EventArgs e)
        {
            MemberCenterMediaHelper.ShowChangePhoneNewOldCard();
        }

        private void FormChangePhoneNewCard_FormClosing(object sender, FormClosingEventArgs e)
        {
            MemberCenterMediaHelper.ShowChangePhoneNumber();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            string errormsg = "";
            string newcard = txtNewCard.Text;
            bool resule = membercenterhttputil.GetMactchCardNewCard(newcard, ref errormsg);
            if (resule)
            {
                MainModel.ShowChangePhonePage = 1;
                MainModel.ShowChangePhoneMedia = 1;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MainModel.ShowLog("卡号输入错误请重试", false);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}
