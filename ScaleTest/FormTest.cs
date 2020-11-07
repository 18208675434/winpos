using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ScaleTest
{
    public partial class FormTest : Form
    {
        public FormTest()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                ToledoResult result = ToledoUtil.ReadStdcall();

                if (result.WhetherSuccess)
                {
                    listBox1.Items.Add("总重量"+result.TotalWeight  +"  净重"+result.NetWeight +"  皮重"+result.TareWeight);
                }
                else
                {
                    listBox1.Items.Add("失败"+result.Message);
                }
            }
            catch (Exception ex)
            {
                listBox1.Items.Add(ex.Message);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                timer1.Interval = Convert.ToInt32(textBox1.Text);
            }
            catch { }
            timer1.Enabled = checkBox1.Checked;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                timer1.Interval = Convert.ToInt32(textBox1.Text);
            }
            catch { }
        }

        private void btnZero_Click(object sender, EventArgs e)
        {
            try {
                ToledoResult result = ToledoUtil.SendZero();

                MessageBox.Show(result.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("清零异常"+ex.Message);
            }
        }

        private void btnPreTare_Click(object sender, EventArgs e)
        {
            try
            {
                ToledoResult result = ToledoUtil.SendPreTare(Convert.ToInt32(txtPreTare.Text));
                MessageBox.Show(result.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("预置去皮异常"+ex.Message);
            }
        }

        private void btnTareBYkey_Click(object sender, EventArgs e)
        {
            try
            {
                ToledoResult result = ToledoUtil.SendTareByKey();
                MessageBox.Show(result.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("按键去皮异常" + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }



        [DllImport("pos_ad_dll.dll", CharSet = CharSet.Ansi, CallingConvention =
CallingConvention.StdCall)]
        public static extern int PrintText(byte[] str,int FontSize);
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
              int result =   PrintText( System.Text.Encoding.Default.GetBytes(txtPrintText.Text),Convert.ToInt32(txtFontSize.Text));
              MessageBox.Show(result.ToString());
            }
            catch(Exception ex) 
            {
                MessageBox.Show("格式化待打印字符串异常"+ex.Message);
            }
        }


        [DllImport("pos_ad_dll.dll", CharSet = CharSet.Ansi, CallingConvention =CallingConvention.StdCall)]
        public static extern int BeginPrint(int PrintType);

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int result = BeginPrint(Convert.ToInt32(txtprinttype.Text));
                MessageBox.Show(result.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("d打印异常" + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
        }

        
    }
}
