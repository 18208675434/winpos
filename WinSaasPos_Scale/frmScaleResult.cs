using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinSaasPOS_Scale
{
    public partial class frmScaleResult : Form
    {
        public frmScaleResult()
        {
            InitializeComponent();
        }

        public frmScaleResult(bool IsSuccess,string scalename,string errormsg)
        {
            InitializeComponent();

            if (IsSuccess)
            {
                picStatus.BackgroundImage = picSecuss.Image;
                lblScaleName.Text = scalename;
                lblErrorMsg.Text = "下发成功";
                btnAgain.Visible = false;
                btnKnow.Left = (this.Width - btnKnow.Width) / 2;
            }
            else
            {
                picStatus.BackgroundImage = picFaile.Image;
                lblScaleName.Text = scalename;
                lblErrorMsg.Text = errormsg;
                //lblSuccess.Text = "下发失败";
            }
        }

        private void btnKnow_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnAgain_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Retry;
            this.Close();
        }
    }
}
