using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZhuiZhi_Integral_Scale_UncleFruit.ScaleUI
{
    public partial class FormSending : Form
    {
        public FormSending()
        {
            InitializeComponent();
            //使用委托的话frmmain界面会卡死
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        public FormSending(string msg)
        {
            InitializeComponent();
            //使用委托的话frmmain界面会卡死
            Control.CheckForIllegalCrossThreadCalls = false;

            try
            {
                lblMsg.Text = msg;

                //this.Width = lblMsg.Width + 20;

                
                this.Size = new System.Drawing.Size(lblMsg.Width + 40, this.Height);
                picLoading.Left = (this.Width - picLoading.Width) / 2;
                //this.ClientSize = new System.Drawing.Size(lblMsg.Width + 40, this.Height);
                this.Location = new Point((Screen.AllScreens[0].Bounds.Width - this.Width) / 2, (Screen.AllScreens[0].Bounds.Height - this.Height) / 2);
                Application.DoEvents();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
