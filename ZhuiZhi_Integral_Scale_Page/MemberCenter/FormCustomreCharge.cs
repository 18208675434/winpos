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
    public partial class FormCustomreCharge : Form
    {
        public FormCustomreCharge()
        {
            InitializeComponent();
            
        }

        private void btnpw1_Click(object sender, EventArgs e)
        {
            if (outputmoney.Text.Length == 3)
            {
                outputmoney.Text += "1";
                int zeng = int.Parse(outputmoney.Text);
                int name = zeng / 10;
                label3.Text = "赠送" + name + ".00元";
                label3.Visible = true;

            }
            else if (outputmoney.Text.Length == 2)
            {
                outputmoney.Text += "1";
                int zeng = int.Parse(outputmoney.Text);
                int name = zeng / 10;
                label3.Text = "赠送" + name + ".00元";
                label3.Visible = true;


            }
            else if (outputmoney.Text.Length == 1)
            {
                outputmoney.Text += "1";
                int zeng = int.Parse(outputmoney.Text);
                int name = zeng / 10;
                label3.Text = "赠送" + name + ".00元";
                label3.Visible = true;


            }
            else if (outputmoney.Text =="" )
            {
                outputmoney.Text += "1";
            }
            else if (outputmoney.Text.Length > 3)
            {
                return;
            }
        }

        private void btnpw2_Click(object sender, EventArgs e)
        {
            if (outputmoney.Text.Length == 3)
            {
                outputmoney.Text += "2";
                
                int zeng = int.Parse(outputmoney.Text);
                int name = zeng / 10;
                label3.Text = "赠送" + name + ".00元";
                label3.Visible = true;

            }
            else if (outputmoney.Text.Length == 2)
            {
                outputmoney.Text += "2";

                int zeng = int.Parse(outputmoney.Text);
                int name = zeng / 10;
                label3.Text = "赠送" + name + ".00元";
                label3.Visible = true;


            }
            else if (outputmoney.Text.Length == 1)
            {
                outputmoney.Text += "2";

                int zeng = int.Parse(outputmoney.Text);
                int name = zeng / 10;
                label3.Text = "赠送" + name + ".00元";
                label3.Visible = true;

            }
            else if (outputmoney.Text == "")
            {
                outputmoney.Text += "2";
            }
            else if (outputmoney.Text.Length > 3)
            {
                return;
            }
        }

        private void btnpw3_Click(object sender, EventArgs e)
        {
            if (outputmoney.Text.Length == 3)
            {
                outputmoney.Text += "3";
                int zeng = int.Parse(outputmoney.Text);
                int name = zeng / 10;
                label3.Text = "赠送" + name + ".00元";
                label3.Visible = true;

            }
            else if (outputmoney.Text.Length == 2)
            {
                outputmoney.Text += "3";
                int zeng = int.Parse(outputmoney.Text);
                int name = zeng / 10;
                label3.Text = "赠送" + name + ".00元";
                label3.Visible = true;


            }
            else if (outputmoney.Text.Length == 1)
            {
                outputmoney.Text += "3";
                int zeng = int.Parse(outputmoney.Text);
                int name = zeng / 10;
                label3.Text = "赠送" + name + ".00元";
                label3.Visible = true;


            }
            else if (outputmoney.Text == "")
            {
                outputmoney.Text += "3";
            }
            else if (outputmoney.Text.Length > 3)
            {
                return;
            }
        }

        private void btnpw4_Click(object sender, EventArgs e)
        {
            if (outputmoney.Text.Length == 3)
            {
                outputmoney.Text += "4";

                int zeng = int.Parse(outputmoney.Text);
                int name = zeng / 10;
                label3.Text = "赠送" + name + ".00元";
                label3.Visible = true;

            }
            else if (outputmoney.Text.Length == 2)
            {
                outputmoney.Text += "4";

                int zeng = int.Parse(outputmoney.Text);
                int name = zeng / 10;
                label3.Text = "赠送" + name + ".00元";
                label3.Visible = true;


            }
            else if (outputmoney.Text.Length == 1)
            {
                outputmoney.Text += "4";

                int zeng = int.Parse(outputmoney.Text);
                int name = zeng / 10;
                label3.Text = "赠送" + name + ".00元";
                label3.Visible = true;


            }
            else if (outputmoney.Text == "")
            {
                outputmoney.Text += "4";
            }
            else if (outputmoney.Text.Length > 3)
            {
                return;
            }
        }

        private void btnpw5_Click(object sender, EventArgs e)
        {
            if (outputmoney.Text.Length == 3)
            {
                outputmoney.Text += "5";

                int zeng = int.Parse(outputmoney.Text);
                int name = zeng / 10;
                label3.Text = "赠送" + name + ".00元";
                label3.Visible = true;

            }
            else if (outputmoney.Text.Length == 2)
            {
                outputmoney.Text += "5";

                int zeng = int.Parse(outputmoney.Text);
                int name = zeng / 10;
                label3.Text = "赠送" + name + ".00元";
                label3.Visible = true;

            }
            else if (outputmoney.Text.Length == 1)
            {
                outputmoney.Text += "5";

                int zeng = int.Parse(outputmoney.Text);
                int name = zeng / 10;
                label3.Text = "赠送" + name + ".00元";
                label3.Visible = true;

            }
            else if (outputmoney.Text == "")
            {
                outputmoney.Text += "5";
            }
            else if (outputmoney.Text.Length > 3)
            {
                return;
            }
        }

        private void btnpw6_Click(object sender, EventArgs e)
        {
            if (outputmoney.Text.Length == 3)
            {
                outputmoney.Text += "6";

                int zeng = int.Parse(outputmoney.Text);
                int name = zeng / 10;
                label3.Text = "赠送" + name + ".00元";
                label3.Visible = true;
            }
            else if (outputmoney.Text.Length == 2)
            {
                outputmoney.Text += "6";

                int zeng = int.Parse(outputmoney.Text);
                int name = zeng / 10;
                label3.Text = "赠送" + name + ".00元";
                label3.Visible = true;

            }
            else if (outputmoney.Text.Length == 1)
            {
                outputmoney.Text += "6";

                int zeng = int.Parse(outputmoney.Text);
                int name = zeng / 10;
                label3.Text = "赠送" + name + ".00元";
                label3.Visible = true;

            }
            else if (outputmoney.Text == "")
            {
                outputmoney.Text += "6";
            }
            else if (outputmoney.Text.Length > 3)
            {
                return;
            }
        }

        private void btnpw7_Click(object sender, EventArgs e)
        {
            if (outputmoney.Text.Length == 3)
            {
                outputmoney.Text += "7";

                int zeng = int.Parse(outputmoney.Text);
                int name = zeng / 10;
                label3.Text = "赠送" + name + ".00元";
                label3.Visible = true;
            }
            else if (outputmoney.Text.Length == 2)
            {
                outputmoney.Text += "7";

                int zeng = int.Parse(outputmoney.Text);
                int name = zeng / 10;
                label3.Text = "赠送" + name + ".00元";
                label3.Visible = true;

            }
            else if (outputmoney.Text.Length == 1)
            {
                outputmoney.Text += "7";

                int zeng = int.Parse(outputmoney.Text);
                int name = zeng / 10;
                label3.Text = "赠送" + name + ".00元";
                label3.Visible = true;

            }
            else if (outputmoney.Text == "")
            {
                outputmoney.Text += "7";
            }
            else if (outputmoney.Text.Length > 3)
            {
                return;
            }
        }

        private void btnpw8_Click(object sender, EventArgs e)
        {
            if (outputmoney.Text.Length == 3)
            {
                outputmoney.Text += "8";

                int zeng = int.Parse(outputmoney.Text);
                int name = zeng / 10;
                label3.Text = "赠送" + name + ".00元";
                label3.Visible = true;
            }
            else if (outputmoney.Text.Length == 2)
            {
                outputmoney.Text += "8";

                int zeng = int.Parse(outputmoney.Text);
                int name = zeng / 10;
                label3.Text = "赠送" + name + ".00元";
                label3.Visible = true;
            }
            else if (outputmoney.Text.Length == 1)
            {
                outputmoney.Text += "8";

                int zeng = int.Parse(outputmoney.Text);
                int name = zeng / 10;
                label3.Text = "赠送" + name + ".00元";
                label3.Visible = true;
            }
            else if (outputmoney.Text == "")
            {
                outputmoney.Text += "8";
            }
            else if (outputmoney.Text.Length > 3)
            {
                return;
            }
        }

        private void btnpw9_Click(object sender, EventArgs e)
        {
            if (outputmoney.Text.Length == 3)
            {
                outputmoney.Text += "9";

                int zeng = int.Parse(outputmoney.Text);
                int name = zeng / 10;
                label3.Text = "赠送" + name + ".00元";
                label3.Visible = true;
            }
            else if (outputmoney.Text.Length == 2)
            {
                outputmoney.Text += "9";

                int zeng = int.Parse(outputmoney.Text);
                int name = zeng / 10;
                label3.Text = "赠送" + name + ".00元";
                label3.Visible = true;

            }
            else if (outputmoney.Text.Length == 1)
            {
                outputmoney.Text += "9";

                int zeng = int.Parse(outputmoney.Text);
                int name = zeng / 10;
                label3.Text = "赠送" + name + ".00元";
                label3.Visible = true;

            }
            else if (outputmoney.Text == "")
            {
                outputmoney.Text += "9";
            }
            else if (outputmoney.Text.Length > 3)
            {
                return;
            }
        }
        
        private void btnpwd_Click(object sender, EventArgs e)
        {
            if (outputmoney.Text.Length == 3)
            {
                outputmoney.Text += ".";

                int zeng = int.Parse(outputmoney.Text);
                int name = zeng / 10;
                label3.Text = "赠送" + name + ".00元";
                label3.Visible = true;
            }
            else if (outputmoney.Text.Length == 2)
            {
                outputmoney.Text += ".";

                int zeng = int.Parse(outputmoney.Text);
                int name = zeng / 10;
                label3.Text = "赠送" + name + ".00元";
                label3.Visible = true;

            }
            else if (outputmoney.Text.Length == 1)
            {
                outputmoney.Text += ".";

                int zeng = int.Parse(outputmoney.Text);
                int name = zeng / 10;
                label3.Text = "赠送" + name + ".00元";
                label3.Visible = true;

            }
            else if (outputmoney.Text == "")
            {
                outputmoney.Text += ".";
            }
            else if (outputmoney.Text.Length > 3)
            {
                return;
            }

            
        }
        
        private void btnpw0_Click(object sender, EventArgs e)
        {
            if (outputmoney.Text.Length == 3)
            {
                outputmoney.Text += "0";

                int zeng =int.Parse( outputmoney.Text);
                int name = zeng / 10;
                label3.Text = "赠送" + name + ".00元";
                label3.Visible = true;
            }
            else if (outputmoney.Text.Length == 2)
            {
                outputmoney.Text += "0";

                int zeng = int.Parse(outputmoney.Text);
                int name = zeng / 10;
                label3.Text = "赠送" + name + ".00元";
                label3.Visible = true;

            }
            else if (outputmoney.Text.Length == 1)
            {
                outputmoney.Text += "0";

                int zeng = int.Parse(outputmoney.Text);
                int name = zeng / 10;
                label3.Text = "赠送" + name + ".00元";
                label3.Visible = true;

            }
            else if (outputmoney.Text == "")
            {
                outputmoney.Text += "0";
            }
            else if (outputmoney.Text.Length > 3)
            {
                return;
            }
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                

                if (outputmoney.Text.Length > 0)
                {
                    outputmoney.Text = outputmoney.Text.Substring(0, outputmoney.Text.Length - 1);
                }
                else if(outputmoney.Text == "")
                {
                    label3.Text = "";
                    label3.Visible = false;
                }
                if (outputmoney.Text.Length == 3)
                {
                    int zeng = int.Parse(outputmoney.Text);
                    int name = zeng / 10;
                    label3.Text = "赠送" + name + ".00元";
                    label3.Visible = true;
                }
                else if (outputmoney.Text.Length == 2)
                {
                    int zeng = int.Parse(outputmoney.Text);
                    int name = zeng / 10;
                    label3.Text = "赠送" + name + ".00元";
                    label3.Visible = true;

                }
                else if (outputmoney.Text.Length == 1)
                {
                    int zeng = int.Parse(outputmoney.Text);
                    int name = zeng / 10;
                    label3.Text = "赠送" + name + ".00元";
                    label3.Visible = true;

                }
            }
            catch { }
        }

        private void pictureCancle_Click(object sender, EventArgs e)
        {
            BackHelper.HideFormBackGround();
            this.Close();
        }
        
        private void btnOk_Click(object sender, EventArgs e)
        {
            BackHelper.HideFormBackGround();
            this.Close();
            ListAllTemplate.CustomMoney = outputmoney.Text;
            ListAllTemplate.ZengCustomMoney = label3.Text;
        }
    }
}
