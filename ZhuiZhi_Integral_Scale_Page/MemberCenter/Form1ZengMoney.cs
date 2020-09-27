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
    public partial class Form1ZengMoney : Form
    {
        public Form1ZengMoney()
        {
            InitializeComponent();
        }

        private void btnpw1_Click(object sender, EventArgs e)
        {
            if (inputz.Text.Length <= 4)
            {
                input.Text += "1";
            }
            else if(input.Text.Length <= 4)
            {
                input.Text += "1";

            }
            else
            {
                return;
            }
        }

        private void btnpw2_Click(object sender, EventArgs e)
        {
           
        }

        private void btnpw3_Click(object sender, EventArgs e)
        {
            
        }

        private void btnpw4_Click(object sender, EventArgs e)
        {
            
        }

        private void btnpw5_Click(object sender, EventArgs e)
        {
            
        }

        private void btnpw6_Click(object sender, EventArgs e)
        {
           
        }

        private void btnpw7_Click(object sender, EventArgs e)
        {
           
        }

        private void btnpw8_Click(object sender, EventArgs e)
        {
            
        }

        private void btnpw9_Click(object sender, EventArgs e)
        {
          
        }

        private void btnpwd_Click(object sender, EventArgs e)
        {
          
        }

        private void btnpw0_Click(object sender, EventArgs e)
        {
           

        }

        private void button7_Click(object sender, EventArgs e)
        {

            if (input.Text.Length > 0)
            {
                input.Text = input.Text.Substring(0, input.Text.Length - 1);
            }
            if (inputz.Text.Length > 0)
            {
                inputz.Text = inputz.Text.Substring(0, inputz.Text.Length - 1);

            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (input.Text == "")
            {
                MainModel.ShowLog("不能为空", true);
            }
            ListAllTemplate.Money = input.Text;
            if (inputz.Text != "")
            {
                MainModel.GetZsje = int.Parse(inputz.Text);
            }
            BackHelper.HideFormBackGround();

            this.Close();
        }

        private void pictureCancle_Click(object sender, EventArgs e)
        {
            //BackHelper.ShowFormBackGround();
            BackHelper.HideFormBackGround();
            this.Close();
        }

        private void Form1ZengMoney_Load(object sender, EventArgs e)
        {

        }

        

       
        
        

        private void inputc_Click(object sender, EventArgs e)
        {
            
        }

        private void numberTextBox1_Click(object sender, EventArgs e)
        {
            //this.numberTextBox1.Focus();//获取焦点

            //this.numberTextBox1.Select(this.numberTextBox1.TextLength, 0);//光标定位到文本最后
        }

        private void numberTextBox2_Load(object sender, EventArgs e)
        {
               
        }
    }
}
