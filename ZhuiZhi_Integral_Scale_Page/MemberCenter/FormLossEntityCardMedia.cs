using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
   
    public partial class FormLossEntityCardMedia : Form
    {

        private HttpUtil httputil = new HttpUtil();
        public FormLossEntityCardMedia()
        {
            InitializeComponent();
          
            //使用委托的话frmmain界面会卡死
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void FormLossEntityCardMedia_Shown(object sender, EventArgs e)
        {
            lblShopName.Text = ZhuiZhi_Integral_Scale_UncleFruit.Model.MainModel.Titledata + "   " + ZhuiZhi_Integral_Scale_UncleFruit.Model.MainModel.CurrentShopInfo.shopname;          
        }
        public void UpdateEntityCardInfo(string phone,string newCardNo)
        {
            lblPhone.Text = phone;
            lblNewCardNo.Text = newCardNo;
        }
    }
}
