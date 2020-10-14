using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;
using ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    public partial class FormZsongMoney : Form
    {
        public FormZsongMoney()
        {
            InitializeComponent();
        }
        public static int isok;
        private void btnpw1_Click(object sender, EventArgs e)
        {
            if (isok == 1 && inputc.Text.Length < 4)
            {
                inputc.Text += "1";

            }
            else if (isok == 2 && inputz.Text.Length < 4)
            {
                inputz.Text += "1";

            }
            else
            {
                return;
            }
        }

        private void pictureCancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            BackHelper.HideFormBackGround();
            this.Close();
        }






        private void inputc_Click(object sender, EventArgs e)
        {
        }

        private void inputz_Click(object sender, EventArgs e)
        {
            

        }

        private void btnpw2_Click(object sender, EventArgs e)
        {
            if (isok == 1 && inputc.Text.Length < 4)
            {
                inputc.Text += "2";

            }
            else if (isok == 2 && inputz.Text.Length < 4)
            {
                inputz.Text += "2";

            }
            else
            {
                return;
            }
        }

        private void btnpw3_Click(object sender, EventArgs e)
        {
            if (isok == 1 && inputc.Text.Length < 4)
            {
                inputc.Text += "3";

            }
            else if (isok == 2 && inputz.Text.Length < 4)
            {
                inputz.Text += "3";

            }
            else
            {
                return;
            }
        }

        private void btnpw4_Click(object sender, EventArgs e)
        {
            if (isok == 1 && inputc.Text.Length < 4)
            {
                inputc.Text += "4";

            }
            else if (isok == 2 && inputz.Text.Length < 4)
            {
                inputz.Text += "4";

            }
            else
            {
                return;
            }
        }

        private void btnpw5_Click(object sender, EventArgs e)
        {
            if (isok == 1 && inputc.Text.Length < 4)
            {
                inputc.Text += "5";

            }
            else if (isok == 2 && inputz.Text.Length < 4)
            {
                inputz.Text += "5";

            }
            else
            {
                return;
            }
        }

        private void btnpw6_Click(object sender, EventArgs e)
        {
            if (isok == 1 && inputc.Text.Length < 4)
            {
                inputc.Text += "6";

            }
            else if (isok == 2 && inputz.Text.Length < 4)
            {
                inputz.Text += "6";

            }
            else
            {
                return;
            }
        }

        private void btnpw7_Click(object sender, EventArgs e)
        {
            if (isok == 1 && inputc.Text.Length < 4)
            {
                inputc.Text += "7";

            }
            else if (isok == 2 && inputz.Text.Length < 4)
            {
                inputz.Text += "7";

            }
            else
            {
                return;
            }
        }

        private void btnpw8_Click(object sender, EventArgs e)
        {
            if (isok == 1 && inputc.Text.Length < 4)
            {
                inputc.Text += "8";

            }
            else if (isok == 2 && inputz.Text.Length < 4)
            {
                inputz.Text += "8";

            }
            else
            {
                return;
            }
        }

        private void btnpw9_Click(object sender, EventArgs e)
        {
            if (isok == 1 && inputc.Text.Length < 4)
            {
                inputc.Text += "9";

            }
            else if (isok == 2 && inputz.Text.Length < 4)
            {
                inputz.Text += "9";

            }
            else
            {
                return;
            }
        }

        private void btnpwd_Click(object sender, EventArgs e)
        {
            if (isok == 1 && inputc.Text.Length < 4)
            {
                inputc.Text += ".";

            }
            else if (isok == 2 && inputz.Text.Length < 4)
            {
                inputz.Text += ".";

            }
            else
            {
                return;
            }
        }

        private void btnpw0_Click(object sender, EventArgs e)
        {
            if (isok == 1 && inputc.Text.Length < 4)
            {
                inputc.Text += "0";

            }
            else if (isok == 2 && inputz.Text.Length < 4)
            {
                inputz.Text += "0";

            }
            else
            {
                return;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                if (isok == 1 && inputc.Text.Length > 0)
                {

                    inputc.Text = inputc.Text.Substring(0, inputc.Text.Length - 1);

                }
                else if (isok == 2  && inputz.Text.Length > 0 )
                {
                    inputz.Text = inputz.Text.Substring(0, inputz.Text.Length - 1);
                    
                }

            }
            catch { }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
             ListAllTemplate.CustomMoney= int.Parse(inputc.Text);
             ListAllTemplate.ZengCustomMoney = "赠" + inputz.Text + "元";
            BackHelper.HideFormBackGround();

            this.Close();
        }

        private void inputc_Enter(object sender, EventArgs e)
        {
            isok = 1;

        }

        private void inputz_Enter(object sender, EventArgs e)
        {
            isok = 2;

        }
    }
}
