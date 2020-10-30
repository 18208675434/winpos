using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    public partial class FormRechargeSuccessMedia : Form
    {

        public FormRechargeSuccessMedia()
        {
            InitializeComponent();
        }

        private void frmCashierResult_Shown(object sender, EventArgs e)
        {
            lblSecond.Text = "2";
            timerClose.Enabled = true;
        }


        private void timerClose_Tick(object sender, EventArgs e)
        {
            timerClose.Interval = 500;
            int left = Convert.ToInt16(lblSecond.Text) - 1;
            if (left < 0) left = 0;
            lblSecond.Text = left.ToString();
            if (left ==0)
            {
                this.Close();
            }
        }
    }
}
