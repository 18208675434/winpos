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
    public partial class Form1ZengMoney : Form
    {
        public Form1ZengMoney()
        {
            InitializeComponent();
        }

        private void btnpw1_Click(object sender, EventArgs e)
        {
            if (inputc.Text.Length > 3 || inputz.Text.Length > 3)
            {
                return;
            }
            else
            {
                inputc.Text += "1";
                inputz.Text += "1";
            }
        }

        private void btnpw2_Click(object sender, EventArgs e)
        {
            if (inputc.Text.Length > 3 || inputz.Text.Length > 3)
            {
                return;
            }
            else
            {
                inputc.Text += "2";
                inputz.Text += "2";
            }
        }

        private void btnpw3_Click(object sender, EventArgs e)
        {
            if (inputc.Text.Length > 3 || inputz.Text.Length > 3)
            {
                return;
            }
            else
            {
                inputc.Text += "3";
                inputz.Text += "3";
            }
        }

        private void btnpw4_Click(object sender, EventArgs e)
        {
            if (inputc.Text.Length > 3 || inputz.Text.Length > 3)
            {
                return;
            }
            else
            {
                inputc.Text += "4";
                inputz.Text += "4";
            }
        }

        private void btnpw5_Click(object sender, EventArgs e)
        {
            if (inputc.Text.Length > 3 || inputz.Text.Length > 3)
            {
                return;
            }
            else
            {
                inputc.Text += "5";
                inputz.Text += "5";
            }
        }

        private void btnpw6_Click(object sender, EventArgs e)
        {
            if (inputc.Text.Length > 3 || inputz.Text.Length > 3)
            {
                return;
            }
            else
            {
                inputc.Text += "6";
                inputz.Text += "6";
            }
        }

        private void btnpw7_Click(object sender, EventArgs e)
        {
            if (inputc.Text.Length > 3 || inputz.Text.Length > 3)
            {
                return;
            }
            else
            {
                inputc.Text += "7";
                inputz.Text += "7";
            }
        }

        private void btnpw8_Click(object sender, EventArgs e)
        {
            if (inputc.Text.Length > 3 || inputz.Text.Length > 3)
            {
                return;
            }
            else
            {
                inputc.Text += "8";
                inputz.Text += "8";
            }
        }

        private void btnpw9_Click(object sender, EventArgs e)
        {
            if (inputc.Text.Length > 3 || inputz.Text.Length > 3)
            {
                return;
            }
            else
            {
                inputc.Text += "9";
                inputz.Text += "9";
            }
        }

        private void btnpwd_Click(object sender, EventArgs e)
        {
            if (inputc.Text.Length > 3 || inputz.Text.Length > 3)
            {
                return;
            }
            else
            {
                inputc.Text += ".";
                inputz.Text += ".";
            }
        }

        private void btnpw0_Click(object sender, EventArgs e)
        {
            if (inputc.Text.Length > 3 || inputz.Text.Length > 3)
            {
                return;
            }
            else
            {
                inputc.Text += "0";
                inputz.Text += "0";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

            if (inputc.Text.Length > 0 || inputz.Text.Length > 0 )
            {
                inputc.Text = inputc.Text.Substring(0, inputc.Text.Length - 1);
                inputz.Text = inputz.Text.Substring(0, inputz.Text.Length - 1);
            }
        }
    }
}
