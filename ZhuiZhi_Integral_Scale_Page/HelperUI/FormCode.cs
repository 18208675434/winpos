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
using ZhuiZhi_Integral_Scale_UncleFruit.MyControl;

namespace ZhuiZhi_Integral_Scale_UncleFruit.HelperUI
{
    public partial class FormCode : Form
    {

        public string Code = "";
        public FormCode(string title, string waterText, int maxlen = 20)
        {
            InitializeComponent();
            lblTitle.Text = title;
            txtCode.MaxLength = maxlen;
            txtCode.WaterText = waterText;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Code = "";
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FormCode_Shown(object sender, EventArgs e)
        {
            try
            {
                Application.DoEvents();

                txtCode.Text = "";
                timerFocus.Enabled = true;
                Delay.Start(100);
                this.Activate();
                txtCode.Focus();
            }
            catch { }
        }



        public void UpInfo()
        {
            try
            {
                txtCode.Text = "";

                timerFocus.Enabled = true;

                GlobalUtil.OpenOSK();

                Delay.Start(100);
                this.Activate();
                txtCode.Focus();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("刷新" + lblTitle.Text + "窗体异常" + ex.Message);
            }
        }

        private void timerFocus_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!txtCode.Focused)
                {
                    txtCode.Focus();
                }
            }
            catch { }
        }

        private void FormVoucher_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                timerFocus.Enabled = false;
                GlobalUtil.CloseOSK();
            }
            catch { }
        }


        private void btnOK_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtCode.Text))
            {
                MainModel.ShowLog("不能为空",false);
                return;
            }
            //判断是否有特殊字符（数字和字母外）
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtCode.Text, "^[0-9a-zA-Z]+$"))
            {
                //System.Diagnostics.Debug.WriteLine("是符合要求字符");
                MainModel.ShowLog("不能输入特殊字符", false);
                return;
            }
            Code = txtCode.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

    }
}
