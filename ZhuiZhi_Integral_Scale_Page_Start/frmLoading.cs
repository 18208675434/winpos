
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit_Start;

namespace ZhuiZhi_Integral_Scale_UncleFruit_Start
{

    public partial class frmLoading : Form
    {

        public frmLoading()
        {

            InitializeComponent();
            //timerClose.Interval = 2000;
            //timerClose.Enabled = true;
            lblMsg1.Text = "";
            lblMsg2.Text = "";

        }
        public frmLoading(string msg)
        {

            InitializeComponent();

            try
            {
                if (msg.Contains("|"))
                {
                    string[] strs = msg.Split('|');
                    lblMsg1.Text = strs[0];
                    lblMsg2.Text = strs[1];
                }
                else
                {
                    lblMsg1.Text = msg;
                    lblMsg2.Text = "";
                }
              
            }
            catch
            {

            }
           

        }



        /// <summary>

        /// 关闭命令

        /// </summary>

        public void closeOrder()
        {
            try
            {
                if (this.InvokeRequired)
                {

                    //这里利用委托进行窗体的操作，避免跨线程调用时抛异常，后面给出具体定义

                    CONSTANTDEFINE.SetUISomeInfo UIinfo = new CONSTANTDEFINE.SetUISomeInfo(new Action(() =>
                    {

                        while (!this.IsHandleCreated)
                        {

                            ;

                        }

                        if (this.IsDisposed)

                            return;

                        if (!this.IsDisposed)
                        {

                            this.Dispose();

                        }

                    }));

                    this.Invoke(UIinfo);

                }

                else
                {

                    if (this.IsDisposed)

                        return;

                    if (!this.IsDisposed)
                    {

                        this.Dispose();

                    }

                }
            }
            catch (Exception ex)
            {
                try { this.Close(); }
                catch
                {

                }
                
                LogManager.WriteLog("关闭显示窗体页面异常"+ex.Message);
            }

        }



        private void LoaderForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (!this.IsDisposed)
            {

                this.Dispose(true);

            }



        }

        private void frmLoading_SizeChanged(object sender, EventArgs e)
        {
           //asf.ControlAutoSize(this);
        }

        private void timerClose_Tick(object sender, EventArgs e)
        {
            try
            {
                this.Dispose();
                this.Close();

            }catch{}
        }

    }

    class CONSTANTDEFINE
    {

        public delegate void SetUISomeInfo();

    }




}
