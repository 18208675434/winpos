using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ToledoTest
{
    public partial class FormTaoYu : Form
    {
        public FormTaoYu()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            listResult.Items.Clear();
        }


        [DllImport("SensorDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]

        public static extern bool __Open(string CommName,int BaudRate);
        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                listResult.Items.Add("打开串口" + __Open(txtCom.Text, 9600).ToString());
            }
            catch (Exception ex)
            {
                listResult.Items.Add("打开串口"+"失败"+ex.Message);
            }
        }


        [DllImport("SensorDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]

        public static extern bool __Close();
        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                listResult.Items.Add("关闭串口" + __Close().ToString());
            }
            catch (Exception ex)
            {
                listResult.Items.Add("关闭串口" + "失败" + ex.Message);
            }
        }


        [DllImport("SensorDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]

        public static unsafe extern char* __GetWeight();
        private unsafe void btnGetWeight_Click(object sender, EventArgs e)
        {
            try
            {
               //char* ch = __GetWeight();
               //byte[] bb = ch;
               //listResult.Items.Add(ch+"");
               // //Thread threadTV = new Thread(getweightthread);
               // //threadTV.IsBackground = true;
               // //threadTV.Start();
               //// listResult.Items.Add("读取重量" + __GetWeight().ToString());

               // //__GetWeight(vDest);
               // //listResult.Items.Add(vDest.ToString());
            }
            catch (Exception ex)
            {
                listResult.Items.Add("读取重量" + "失败" + ex.Message);
            }
        }



        private void getweightthread()
        {

           // __GetWeight();
            //listResult.Items.Add("读取重量" + __GetWeight().ToString());
        }

        [DllImport("SensorDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]

        public static extern string __udeTare();
        private void btnTare_Click(object sender, EventArgs e)
        {
            try
            {
              __udeTare();
               // listResult.Items.Add("读取重量" + __udeTare().ToString());
            }
            catch (Exception ex)
            {
                listResult.Items.Add("去皮" + "失败" + ex.Message);
            }
        }


        [DllImport("SensorDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]

        public static extern void __uCleart();
        private void btnZero_Click(object sender, EventArgs e)
        {
            try
            {
                __uCleart();
                // listResult.Items.Add("读取重量" + __udeTare().ToString());
            }
            catch (Exception ex)
            {
                listResult.Items.Add("归零" + "失败" + ex.Message);
            }
        }



        [DllImport("SensorDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]

        public static extern bool __SetUnitValue(string Word, string ID);
        private void button1_Click(object sender, EventArgs e)
        {

            listResult.Items.Add("关闭串口" +  __SetUnitValue("0公斤", "0").ToString());
           
        }
    }
}
