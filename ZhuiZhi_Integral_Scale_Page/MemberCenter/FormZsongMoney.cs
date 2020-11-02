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
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    public partial class FormZsongMoney : Form
    {
        public ListAllTemplate CustomTemplate = null;

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
            CustomTemplate = null;
            this.DialogResult = DialogResult.Cancel;
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
            try {
                if (string.IsNullOrEmpty(inputc.Text))
                {
                    return;
                }
                decimal cash = Convert.ToDecimal(inputc.Text);

                decimal custommoney = 0;
                decimal.TryParse(inputz.Text,out custommoney);

                if (MainModel.balanceconfigdetail != null && cash < MainModel.balanceconfigdetail.customrechargeamt)
                {
                    MainModel.ShowLog("数值不能小于"+MainModel.balanceconfigdetail.customrechargeamt.ToString("f2"),false);
                    return;
                }
                //允许充值金额为0  赠送金额不为0 的情况
                if (cash <= 0 && custommoney==0)
                {
                    MainModel.ShowLog("充值金额不正确", false);
                    return;
                }
                CustomTemplate = new ListAllTemplate();
                CustomTemplate.id = -1;
                CustomTemplate.amount = cash;
                CustomTemplate.customAndreward = true;
                if (!string.IsNullOrEmpty(inputz.Text))
                {
                    CustomTemplate.rewardamount = Convert.ToDecimal(inputz.Text);
                }
                // CustomTemplate.rewardamount = 0;

                this.Close();
            }
            catch
            {

            }
           
        }

        private void inputc_Enter(object sender, EventArgs e)
        {
            isok = 1;

        }

        private void inputz_Enter(object sender, EventArgs e)
        {
            isok = 2;

        }

        private void FormZsongMoney_Shown(object sender, EventArgs e)
        {
            inputc.Focus();
        }
    }
}
