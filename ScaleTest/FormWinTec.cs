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
    public partial class FormWinTec : Form
    {
        public FormWinTec()
        {
            InitializeComponent();
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

        private void btnOpen_Click(object sender, EventArgs e)
        {
            MessageBox.Show(WinTecUtil.OpenADCom(Convert.ToInt16(txtcom.Text),Convert.ToInt16(txtbaud.Text)).ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                ToledoResult result = WinTecUtil.ReadStdcall();

                if (result.WhetherSuccess)
                {
                    listBox1.Items.Add("成功  "+ result.TotalWeight +"  "+result.NetWeight+"  "+result.TareWeight +"  "+result.WhetherStable.ToString());
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

        private void btnZero_Click(object sender, EventArgs e)
        {
            try
            {
                ToledoResult result = WinTecUtil.SendZero();

                
                if (result.WhetherSuccess)
                {
                    MessageBox.Show("清零完成");
                }
                else
                {
                    MessageBox.Show(result.Message);
                }
            }
            catch (Exception ex)
            {
                listBox1.Items.Add(ex.Message);
            }
        }

        private void btnPreTare_Click(object sender, EventArgs e)
        {
            ToledoResult result = WinTecUtil.SendPreTare(Convert.ToInt32(txtPreTare.Text));
            MessageBox.Show(result.Message);
        }

        private void btnTareBYkey_Click(object sender, EventArgs e)
        {
            try
            {
                ToledoResult result = WinTecUtil.SendTareByKey();
                MessageBox.Show(result.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("按键去皮异常" + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string tempweight = ((decimal)30 / 1000).ToString("f3").PadLeft(6, '0');
            MessageBox.Show(tempweight);
        }


        [DllImport("pos_ad_dll.dll", CharSet = CharSet.Ansi, CallingConvention =
CallingConvention.StdCall)]
        public static extern int PrintText(byte[] str, int FontSize);
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int result = PrintText(System.Text.Encoding.Default.GetBytes(txtPrintText.Text), Convert.ToInt32(txtFontSize.Text));
                MessageBox.Show(result.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("格式化待打印字符串异常" + ex.Message);
            }
        }


        [DllImport("pos_ad_dll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
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

           [DllImport("pos_ad_dll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CutPaper_stdcall(int PrintType);
        

        private void button5_Click(object sender, EventArgs e)
        {
            CutPaper_stdcall(1);
        }
    }
}
