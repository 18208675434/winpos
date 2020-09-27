using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;
using ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    public partial class FormReminder : Form
    {
        public FormReminder()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            BackHelper.HideFormBackGround();
            MainModel.isokcancle = true;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BackHelper.HideFormBackGround();
            MainModel.isokcancle = true;
            this.Close();

        }

        private void FormReminder_Load(object sender, EventArgs e)
        {
            label2.Text = "确认"+ListAllTemplate.zhifu+"支付：￥"+ ListAllTemplate.mount;
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            
            this.Close();
            
        }
      
        
    }
}
