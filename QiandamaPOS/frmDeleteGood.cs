﻿using QiandamaPOS.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            lblMsgStr.Text=msgstr;
            lblMsg.Text = proname + "  " + barcode;
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmDeleteGood_SizeChanged(object sender, EventArgs e)
        {
           asf.ControlAutoSize(this);
        }
    }
}
