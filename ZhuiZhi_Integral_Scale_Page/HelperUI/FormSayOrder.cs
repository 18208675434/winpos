using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;

namespace ZhuiZhi_Integral_Scale_UncleFruit.HelperUI
{
    public partial class FormSayOrder : Form
    {
        public FormSayOrder(string msg)
        {
            InitializeComponent();

            lblMsg.Text = msg;

            this.Size = new Size(lblMsg.Right + 15, this.Height);
            this.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width - this.Width - 10, 30);

            GlobalUtil.Speech(msg);
        }

        public void UpdateSayMsg(string msg)
        {
            lblMsg.Text = msg;
            
            this.Size = new Size(lblMsg.Width+15,lblMsg.Height);
            this.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width - this.Width-10, 30);

            GlobalUtil.Speech(msg);
        }
    }
}
