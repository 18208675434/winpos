using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    public partial class FormTuikuan : Form
    {
        public FormTuikuan()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnpw1_Click(object sender, EventArgs e)
        {
            if (txttuikuan.Text.Length <= 4)
            {
                txttuikuan.Text += "1";
                Kouchu.Text = "扣除:￥" + txttuikuan.Text+ "+￥1.0(赠送金额)";
                Kouchu.ForeColor = Color.Red;
                Kouchu.Visible = true;

            }
            else
            {
                return;
            }
        }

        private void btnpw2_Click(object sender, EventArgs e)
        {
            if (txttuikuan.Text.Length <= 4)
            {
                txttuikuan.Text += "2";
                Kouchu.Text = "扣除:￥" + txttuikuan.Text + "+￥1.0(赠送金额)";
                Kouchu.ForeColor = Color.Red;
                Kouchu.Visible = true;

            }
            else
            {
                return;
            }
        }

        private void btnpw3_Click(object sender, EventArgs e)
        {
            if (txttuikuan.Text.Length <= 4)
            {
                txttuikuan.Text += "3";
                Kouchu.Text = "扣除:￥" + txttuikuan.Text + "+￥1.0(赠送金额)";
                Kouchu.ForeColor = Color.Red;
                Kouchu.Visible = true;

            }
            else
            {
                return;
            }
        }

        private void btnpw4_Click(object sender, EventArgs e)
        {
            if (txttuikuan.Text.Length <= 4)
            {
                txttuikuan.Text += "4";
                Kouchu.Text = "扣除:￥" + txttuikuan.Text + "+￥1.0(赠送金额)";
                Kouchu.ForeColor = Color.Red;
                Kouchu.Visible = true;

            }
            else
            {
                return;
            }
        }

        private void btnpw5_Click(object sender, EventArgs e)
        {
            if (txttuikuan.Text.Length <= 4)
            {
                txttuikuan.Text += "5";
                Kouchu.Text = "扣除:￥" + txttuikuan.Text + "+￥1.0(赠送金额)";
                Kouchu.ForeColor = Color.Red;
                Kouchu.Visible = true;

            }
            else
            {
                return;
            }
        }

        private void btnpw6_Click(object sender, EventArgs e)
        {
            if (txttuikuan.Text.Length <= 4)
            {
                txttuikuan.Text += "6";
                Kouchu.Text = "扣除:￥" + txttuikuan.Text + "+￥1.0(赠送金额)";
                Kouchu.ForeColor = Color.Red;
                Kouchu.Visible = true;

            }
            else
            {
                return;
            }
        }

        private void btnpw7_Click(object sender, EventArgs e)
        {
            if (txttuikuan.Text.Length <= 4)
            {
                txttuikuan.Text += "7";
                Kouchu.Text = "扣除:￥" + txttuikuan.Text + "+￥1.0(赠送金额)";
                Kouchu.ForeColor = Color.Red;
                Kouchu.Visible = true;

            }
            else
            {
                return;
            }
        }

        private void btnpw8_Click(object sender, EventArgs e)
        {
            if (txttuikuan.Text.Length <= 4)
            {
                txttuikuan.Text += "8";
                Kouchu.Text = "扣除:￥" + txttuikuan.Text + "+￥1.0(赠送金额)";
                Kouchu.ForeColor = Color.Red;
                Kouchu.Visible = true;

            }
            else
            {
                return;
            }
        }

        private void btnpw9_Click(object sender, EventArgs e)
        {
            if (txttuikuan.Text.Length <= 4)
            {
                txttuikuan.Text += "9";
                Kouchu.Text = "扣除:￥" + txttuikuan.Text + "+￥1.0(赠送金额)";
                Kouchu.ForeColor = Color.Red;
                Kouchu.Visible = true;

            }
            else
            {
                return;
            }
        }

        private void btnpwd_Click(object sender, EventArgs e)
        {
            if (txttuikuan.Text.Length <= 4)
            {
                txttuikuan.Text += ".";
                Kouchu.Text = "扣除:￥" + txttuikuan.Text + "+￥1.0(赠送金额)";
                Kouchu.ForeColor = Color.Red;
                Kouchu.Visible = true;

            }
            else
            {
                return;
            }
        }

        private void btnpw0_Click(object sender, EventArgs e)
        {
            if (txttuikuan.Text.Length <= 4)
            {
                txttuikuan.Text += "0";
                Kouchu.Text = "扣除:￥" + txttuikuan.Text + "+￥1.0(赠送金额)";
                Kouchu.ForeColor = Color.Red;
                Kouchu.Visible = true ;

            }
            else
            {
                return;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (txttuikuan.Text.Length > 0)
            {
                txttuikuan.Text = txttuikuan.Text.Substring(0, txttuikuan.Text.Length - 1);
                Kouchu.Text = "扣除:￥" + txttuikuan.Text + "+￥1.0(赠送金额)";
                Kouchu.ForeColor = Color.Red;
            }
            else
            {
                Kouchu.Text = "";
                Kouchu.Visible = false;
            }
            
        }
    }
}
