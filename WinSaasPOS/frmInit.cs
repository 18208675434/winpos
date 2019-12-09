using WinSaasPOS.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WinSaasPOS
{
    public partial class frmInit : Form
    {


        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();
        public frmInit()
        {
            InitializeComponent();
        }


        public void ShowMsg(string msg)
        {
            txtMsg.Text = msg;
        }

        private void frmInit_SizeChanged(object sender, EventArgs e)
        {
           asf.ControlAutoSize(this);
        }
    }
}
