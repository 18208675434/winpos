using QiandamaPOS.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace QiandamaPOS
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
            lblMsg.Text = "";

        }
        public frmLoading(string msg)
        {

            InitializeComponent();
            lblMsg.Text = msg;

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

        private void frmLoading_SizeChanged(object sender, EventArgs e)
        {
           asf.ControlAutoSize(this);
        }

    }

    class CONSTANTDEFINE
    {

        public delegate void SetUISomeInfo();

    }

}
