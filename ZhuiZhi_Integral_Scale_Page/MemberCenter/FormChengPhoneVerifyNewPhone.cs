using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    public partial class FormChengPhoneVerifyNewPhone : Form
    {
        public FormChengPhoneVerifyNewPhone()
        {
            InitializeComponent();
            

        }

        private void FormChengPhoneVerifyNewPhone_Shown(object sender, EventArgs e)
        {
            MemberCenterMediaHelper.ShowChengPhoneVerifyNewPhone();
        }

        private void FormChengPhoneVerifyNewPhone_FormClosing(object sender, FormClosingEventArgs e)
        {
            MemberCenterMediaHelper.ShowChangePhoneNumber();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
           
            try
            {   //^0{0,1}(13[4-9]|15[7-9]|15[0-2]|18[7-8])[0-9]{8}$
                string s = @"^(1[0-9]|15[0|3|6|8|9])\d{9}$";
                
                string PhoneNumber = txtNewPhoneNumber.Text;
                if (txtNewPhoneNumber.Text == "")
                {
                    MainModel.ShowLog("手机号码不能为空", false);
                }
                else if (!Regex.IsMatch(txtNewPhoneNumber.Text, s))
                {
                    MainModel.ShowLog("手机号码格式不正确", false);
                }
                else
                {
                    MainModel.NewPhone = txtNewPhoneNumber.Text;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnpw1_Click(object sender, EventArgs e)
        {
            txtNewPhoneNumber.Text += "1";
        }

        private void btnpw2_Click(object sender, EventArgs e)
        {
            txtNewPhoneNumber.Text += "2";
            

        }

        private void btnpw3_Click(object sender, EventArgs e)
        {
            txtNewPhoneNumber.Text += "3";
           

        }

        private void btnpw4_Click(object sender, EventArgs e)
        {
            txtNewPhoneNumber.Text += "4";      

        }

        private void btnpw5_Click(object sender, EventArgs e)
        {
            txtNewPhoneNumber.Text += "5";

        }

        private void btnpw6_Click(object sender, EventArgs e)
        {
            txtNewPhoneNumber.Text += "6";

        }

        private void btnpw7_Click(object sender, EventArgs e)
        {
            txtNewPhoneNumber.Text += "7";

        }

        private void btnpw8_Click(object sender, EventArgs e)
        {
            txtNewPhoneNumber.Text += "8";

        }

        private void btnpw9_Click(object sender, EventArgs e)
        {
            txtNewPhoneNumber.Text += "9";

        }

        private void btnpwd_Click(object sender, EventArgs e)
        {
            txtNewPhoneNumber.Text += "0";

        }

        private void btnpw0_Click(object sender, EventArgs e)
        {
            txtNewPhoneNumber.Text += "00";

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;

                if (txtNewPhoneNumber.Text.Length > 0)
                {
                    txtNewPhoneNumber.Text = txtNewPhoneNumber.Text.Substring(0, txtNewPhoneNumber.Text.Length - 1);
                }
            }
            catch { }
        }

        private void txtNewPhoneNumber_Load(object sender, EventArgs e)
        {
            
        }

    }
}
