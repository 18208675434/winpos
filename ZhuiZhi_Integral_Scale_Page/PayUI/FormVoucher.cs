using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.MyControl;

namespace ZhuiZhi_Integral_Scale_UncleFruit.PayUI
{
    public partial class FormVoucher : Form
    {

        public string CurrentPhone = "";
        public FormVoucher()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CurrentPhone = "";
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FormVoucher_Shown(object sender, EventArgs e)
        {
            try
            {
                Application.DoEvents();

                txtCode.Text = "";

                timerFocus.Enabled = true;

                GlobalUtil.OpenOSK();

                Delay.Start(100);
                this.Activate();
                txtCode.Focus();
            }
            catch { }

        }



        private void btnOK_ButtonClick(object sender, EventArgs e)
        {

           
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
                LogManager.WriteLog("刷新会员码窗体异常" + ex.Message);
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

        private void lblShuiyin_Click(object sender, EventArgs e)
        {
            try
            {
                txtCode.Select();

                GlobalUtil.OpenOSK();

                Delay.Start(100);
                this.Activate();
                txtCode.Focus();
            }
            catch { }
        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblShuiyin.Visible = string.IsNullOrEmpty(txtCode.Text);
            }
            catch { }
        }

        private void txtCode_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalUtil.OpenOSK();

                Delay.Start(100);
                this.Activate();
                txtCode.Focus();
            }
            catch { }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            CurrentPhone = txtCode.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
