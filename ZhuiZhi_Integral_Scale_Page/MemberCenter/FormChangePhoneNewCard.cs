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
                MainModel.ShowLog( errormsg , false);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void btnpw1_Click(object sender, EventArgs e)
        {
            txtNewCard.Text += "1";
        }

        private void btnpw2_Click(object sender, EventArgs e)
        {
            txtNewCard.Text += "2";
        }

        private void btnpw3_Click(object sender, EventArgs e)
        {
            txtNewCard.Text += "3";
        }

        private void btnpw4_Click(object sender, EventArgs e)
        {
            txtNewCard.Text += "4";
        }

        private void btnpw5_Click(object sender, EventArgs e)
        {
            txtNewCard.Text += "5";
        }

        private void btnpw6_Click(object sender, EventArgs e)
        {
            txtNewCard.Text += "6";
        }

        private void btnpw7_Click(object sender, EventArgs e)
        {
            txtNewCard.Text += "7";
        }

        private void btnpw8_Click(object sender, EventArgs e)
        {
            txtNewCard.Text += "8";
        }

        private void btnpw9_Click(object sender, EventArgs e)
        {
            txtNewCard.Text += "9";
        }

        private void btnpwd_Click(object sender, EventArgs e)
        {
            txtNewCard.Text += ".";
        }

        private void btnpw0_Click(object sender, EventArgs e)
        {
            txtNewCard.Text += "0";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;

                if (txtNewCard.Text.Length > 0)
                {
                    txtNewCard.Text = txtNewCard.Text.Substring(0, txtNewCard.Text.Length - 1);
                }
            }
            catch { }
        }
    }
}
