using QDAMAPOS.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace QDAMAPOS
{
    public partial class frmMsg : Form
    {


        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();

        
        public frmMsg()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
       
        }

        public frmMsg(string Msg,bool isrun,int interval)
        {
            InitializeComponent();

            try
            {
                lblMsg.Text = Msg;

                timerClose.Interval = interval;
                timerClose.Enabled = true;

                this.ClientSize = new System.Drawing.Size(lblMsg.Width+10, this.Size.Height+5);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("初始化消息页面异常"+ex.Message);
            }
         
        }

          public frmMsg(string Msg)
        {
            InitializeComponent();
            try
            {
                lblMsg.Text = Msg;

                timerClose.Interval = 1600;
                timerClose.Enabled = true;
              
                this.ClientSize = new System.Drawing.Size(lblMsg.Width+10, this.Size.Height+5);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("初始化消息页面异常"+ex.Message);
            }
         
        }

        private void timerClose_Tick(object sender, EventArgs e)
        {
            try
            {
                this.Dispose();
                this.Close();
            }
            catch (Exception ex)
            {
                try
                {
                    this.Dispose();
                    this.Close();
                }
                catch { }
                LogManager.WriteLog("页面关闭异常"+ex.Message);
                this.Close();
            }
        }

        private void frmMsg_SizeChanged(object sender, EventArgs e)
        {

            //<summary>
            //按比例缩放页面及控件
            //</summary>
           // AutoSizeFormUtil asf = new AutoSizeFormUtil();
        }

        
        /// <summary>

        /// 关闭命令

        /// </summary>

        public void closeOrder()
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



        private void LoaderForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (!this.IsDisposed)
            {

                this.Dispose(true);

            }
        }


    }






    
}
