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
    public partial class FormEH200 : Form
    {
        private Scale_EH200 eh = new Scale_EH200();

        private IntPtr ptr = new IntPtr(1);
        public FormEH200()
        {
            InitializeComponent();
        }

        private void btnGetWeight_Click(object sender, EventArgs e)
        {
            MessageBox.Show(CurWeight(ptr));
           //ScaleResult result = eh.GetScaleWeight();

           //if (result.WhetherSuccess)
           //{
           //    MessageBox.Show(result.Message);
           //}
           //else
           //{
           //    MessageBox.Show("读取失败");
           //}
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            MessageBox.Show(OpenCom(ptr, "COM4").ToString());
            //MessageBox.Show(eh.Open("COM4", 19200).ToString());
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            MessageBox.Show(CloseCom(ptr).ToString());
        }




        [DllImport("PosWeighInterface.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenCom(IntPtr obj, string port);


        [DllImport("PosWeighInterface.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern bool CloseCom(IntPtr obj);


        [DllImport("PosWeighInterface.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern bool SetTare(IntPtr obj);


        [DllImport("PosWeighInterface.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern bool SetZeno(IntPtr obj);


        [DllImport("PosWeighInterface.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern string CurWeight(IntPtr obj);



        /// <summary>
        /// 获取窗体的句柄函数
        /// </summary>
        /// <param name="lpClassName">窗口类名</param>
        /// <param name="lpWindowName">窗口标题名</param>
        /// <returns>返回句柄</returns>
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        private void btnTare_Click(object sender, EventArgs e)
        {
            MessageBox.Show(SetTare(ptr).ToString());
        }

        private void btnZero_Click(object sender, EventArgs e)
        {
            MessageBox.Show(SetZeno(ptr).ToString());
        }
    }
}
