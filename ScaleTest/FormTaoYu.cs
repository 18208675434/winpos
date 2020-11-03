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

namespace ScaleTest
{
    public partial class FormTaoYu : Form
    {
        public FormTaoYu()
        {
            InitializeComponent();

            //使用委托的话frmmain界面会卡死
            Control.CheckForIllegalCrossThreadCalls = false;
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

        public static unsafe extern byte __GetWeight();
        private unsafe void btnGetWeight_Click(object sender, EventArgs e)
        {
            try
            {

                listResult.Items.Add("读取重量" + __GetWeight().ToString());

            }
            catch (Exception ex)
            {
                listResult.Items.Add("读取重量" + "失败" + ex.Message);
            }
        }

        //爱宝电子秤未连接成功  或者没有电子秤 读取重量会直接退出  此函数判断是否爱宝电子秤或者是否打开
        private static bool AscalOK()
        {
            try
            {
                if (__GetWeight().ToString() != "37")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
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

        bool isopne = false;
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if(!isopne){

                Invoke((new Action(() =>
              {
                  isopne = __Open(txtCom.Text, 9600);
              })));

                listResult.Items.Add("打开端口" +isopne.ToString());

            }
            
            listResult.Items.Add("读取重量" + __GetWeight().ToString());

            Thread.Sleep(150);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void FormTaoYu_Shown(object sender, EventArgs e)
        {
          //  backgroundWorker1.RunWorkerAsync();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }
    }
}
