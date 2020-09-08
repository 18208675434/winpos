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
            {
                Regex regex = new Regex(@"^0{0,1}(13[4-9]|15[7-9]|15[0-2]|18[7-8])[0-9]{8}$");
                string PhoneNumber = txtNewPhoneNumber.Text;
                if (txtNewPhoneNumber.Text == "")
                {
                    MainModel.ShowLog("手机号码不能为空", false);
                }
                else if (!regex.IsMatch(txtNewPhoneNumber.Text))
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
    }
}
