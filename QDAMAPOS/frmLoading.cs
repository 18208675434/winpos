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

    public partial class frmLoading : Form
    {


        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();
        public frmLoading()
        {
            InitializeComponent();
           // this.Size = new System.Drawing.Size(50,50);
            timerClose.Interval =61*1000;
            timerClose.Enabled = true;
            lblMsg1.Text = "";
            lblMsg2.Text = "";
        }
        public frmLoading(string msg)
        {
            InitializeComponent();
           // this.Size = new System.Drawing.Size(50, 50);
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
                try {
                    Delay.Start(50);
                    this.Dispose();
                    this.Close(); }
                catch(Exception ex1)
                {
                    LogManager.WriteLog("二次关闭显示窗体页面异常" + ex1.Message);
                }
                
                LogManager.WriteLog("关闭显示窗体页面异常"+ex.Message);
            }
        }



        private void LoaderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!this.IsDisposed)
                {

                    this.Dispose(true);

                }

            }
            catch { }

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
