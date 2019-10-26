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
    public partial class frmMsg : Form
    {


        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();

        public frmMsg(string Msg,bool isrun,int interval)
        {
            InitializeComponent();
            
            lblMsg.Text = Msg;
            this.ClientSize = new System.Drawing.Size(lblMsg.Width+10, this.Size.Height);
            timerClose.Interval = interval;
            timerClose.Enabled = true;
        }

        private void timerClose_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMsg_SizeChanged(object sender, EventArgs e)
        {

            //<summary>
            //按比例缩放页面及控件
            //</summary>
            AutoSizeFormUtil asf = new AutoSizeFormUtil();
        }


    }
}
