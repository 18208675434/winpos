using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.PrettyCash
{
    public partial class FormPrettyCash : Form
    {
        public FormPrettyCash()
        {
            InitializeComponent();
        }




        string keyInput = "";
        private void MiniKeyboardHandler(object sender, MyControl.NumberBoard.KeyboardArgs e)
        {
            ZhuiZhi_Integral_Scale_UncleFruit.MyControl.NumberTextBox focusing = txtCash;
            keyInput = e.KeyCode;

            int startDel = 0;

            //退格
            if (keyInput == numBoard.KeyDelete)
            {
                if (focusing.Text.Length > 0)
                {
                    focusing.Text = focusing.Text.Substring(0, focusing.Text.Length - 1);
                }
            }
            else if (keyInput == numBoard.KeyDot)
            {
                if (!focusing.Text.Contains(".") && !string.IsNullOrEmpty(focusing.Text))
                {
                    focusing.Text += ".";
                }
            }

            //其他键直接输入
            else
            {
                focusing.Text += keyInput;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {

                try
                {
                    ZhuiZhi_Integral_Scale_UncleFruit.Common.PrintUtil.OpenCashDrawerEx();
                }
                catch
                {
                }


                decimal prettycash = 0;
                if (!string.IsNullOrEmpty(txtCash.Text))
                {
                    prettycash = Convert.ToDecimal(txtCash.Text);
                }
                this.Hide();
                if (PrettyCashHelper.ShowFormConfirmPretty(prettycash))
                {
                    this.Close();
                }
                else
                {
                    this.Show();
                }

            }
            catch (Exception ex)
            {
                ZhuiZhi_Integral_Scale_UncleFruit.Common.LogManager.WriteLog("确认备用金异常" + ex.Message);
            }
        }

        private void FormPrettyCash_Shown(object sender, EventArgs e)
        {
         
            txtCash.Focus();
            txtCash.SelectAll();
            numBoard.Size = new Size(this.Width, btnOK.Top - numBoard.Top - 10);

            Application.DoEvents();
        }

        private void txtCash_Leave(object sender, EventArgs e)
        {
            txtCash.Select();
            
        }

        private void FormPrettyCash_Load(object sender, EventArgs e)
        {
            Application.DoEvents();
        }


    }
}
