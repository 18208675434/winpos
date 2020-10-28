using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.MyControl;

namespace ZhuiZhi_Integral_Scale_UncleFruit.PrettyCash
{
    public partial class FormGetCashNum : Form
    {

        public decimal ResultNum = 0;
        public FormGetCashNum()
        {
            InitializeComponent();
        }


        string keyInput = "";
        private void MiniKeyboardHandler(object sender, MyControl.NumberBoard.KeyboardArgs e)
        {
            NumberTextBox focusing = txtCash;
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
                decimal prettycash = 0;
                if (!string.IsNullOrEmpty(txtCash.Text))
                {
                    prettycash = Convert.ToDecimal(txtCash.Text);
                }
                else
                {
                    MainModel.ShowLog("请输入实际总金额",false);
                    return;
                }

                this.DialogResult = DialogResult.OK;
                ResultNum = prettycash;

                this.Close();

            }
            catch (Exception ex)
            {
                ZhuiZhi_Integral_Scale_UncleFruit.Common.LogManager.WriteLog("确认备用金异常" + ex.Message);
            }
        }

        private void FormPrettyCash_Shown(object sender, EventArgs e)
        {
            

            txtCash.Focus();
            numBoard.Size = new Size(this.Width, btnOK.Top - numBoard.Top - 10);
            Application.DoEvents();
            //txtCash.Width += 1;
        }


        private void FormPrettyCash_Load(object sender, EventArgs e)
        {
            Application.DoEvents();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


    }
}
