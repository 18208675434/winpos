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
    public partial class frmDeleteGood : Form
    {


        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();
        public frmDeleteGood(string msgstr,string proname,string barcode)
        {
            InitializeComponent();
            this.Location = new System.Drawing.Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);
            lblMsgStr.Text=msgstr;
            lblMsg.Text = proname + "  " + barcode;
        }

        private void frmDeleteGood_SizeChanged(object sender, EventArgs e)
        {
           asf.ControlAutoSize(this);
        }

        private void lbtnCancle_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void lbtnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
