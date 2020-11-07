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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        [DllImport("pos_ad_dll.dll", CharSet = CharSet.Ansi, CallingConvention =
CallingConvention.StdCall)]
        public static extern int read_standard_stdcall(byte[] str);

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            int li_ret = 0;
            byte[] p = new byte[32];
            string s = "";
            try
            {
                li_ret = read_standard_stdcall(p);
                /*
                * HS_OK 成功
                * -1 欠载
                * -2 过载
                */
                switch (li_ret)
                {
                    case -1:
                        {
                            s = "欠载";
                            break;
                        }
                    case -2:
                        {
                            s = "过载";
                            break;
                        }
                    case 0xF0:
                        {
                            s = "成功-----";
                            //status 的 bit0(第一位)表示是否稳定，如为 1 则表示稳定
                            //status 的 bit1(第二位)表示是否在零位，如为 1 则表示零位
                            //status 的 bit2(第三位)表示是否有皮重，如为 1 则表示有皮重
                            byte status = p[0];
                            if ((status & 0x01) == 0x01)
                            {
                                s = s + "稳定";
                            }
                            else
                            {
                                s = s + "动态";
                            }
                            if ((status & 0x02) == 0x02)
                            {
                                s = s + "零位";
                            }
                            else
                            {
                                s = s + "非零位";
                            }
                            if ((status & 0x04) == 0x04)
                            {
                                s = s + "有皮重";
                            }
                            else
                            {
                                s = s + "无皮重";
                            }
                            s = s + "    "+Encoding.Default.GetString(p, 1, p.Length - 1);
                            break;
                        }
                    default:
                        {
                            s = "失败";
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                s = "异常" + ex.Message.ToString();
            }
            listBox1.Items.Insert(0, s);
            timer1.Enabled = checkBox1.Checked;
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

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

                [DllImport("pos_ad_dll.dll", CharSet = CharSet.Ansi, CallingConvention =
CallingConvention.StdCall)]
        public static extern int send_zero();
        private void btnZero_Click(object sender, EventArgs e)
        {
            try
            {
                int result = send_zero();
                MessageBox.Show(result.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("清零异常" + ex.Message);
            }
        }
                [DllImport("pos_ad_dll.dll", CharSet = CharSet.Ansi, CallingConvention =
CallingConvention.StdCall)]
        public static extern int send_tare(byte[] str);
        private void btnSendTare_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] byteArray = System.Text.Encoding.Default.GetBytes(txtTare.Text);
                int result = send_tare(byteArray);
                MessageBox.Show(result.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("设置皮重异常" + ex.Message);
            }
        }
                [DllImport("pos_ad_dll.dll", CharSet = CharSet.Ansi, CallingConvention =
CallingConvention.StdCall)]
        public static extern int send_pre_tare(byte[] str);
        private void btnPreTare_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] byteArray = System.Text.Encoding.Default.GetBytes(txtPreTare.Text);
                int result = send_pre_tare(byteArray);
                MessageBox.Show(result.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("预置皮重异常" + ex.Message);
            }
        }

                [DllImport("pos_ad_dll.dll", CharSet = CharSet.Ansi, CallingConvention =
CallingConvention.StdCall)]
        public static extern int set_tare_bykey(byte[] str);
        private void btnTareBYkey_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] p = new byte[32];
                int result = set_tare_bykey(p);
                txtTareBykey.Text = Encoding.Default.GetString(p, 1, p.Length - 1);
                MessageBox.Show(result.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("按键去皮异常" + ex.Message);
            }
        }





    }
}
