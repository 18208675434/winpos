﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    public partial class FormIsokCancle : Form
    {
        public FormIsokCancle()
        {
            InitializeComponent();
        }
        private static AutoSizeFormUtil asf = new AutoSizeFormUtil();

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //BackHelper.ShowFormBackGround();
            BackHelper.HideFormBackGround();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
           
            BackHelper.ShowFormBackGround();
            
            
        }
    }
}
