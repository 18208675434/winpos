using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
   
    public partial class FormEntityCardBindMedia : Form
    {
        public FormEntityCardBindMedia()
        {
            InitializeComponent();          
        }

        private void FormLossEntityCardMedia_Shown(object sender, EventArgs e)
        {
            lblShopName.Text = ZhuiZhi_Integral_Scale_UncleFruit.Model.MainModel.Titledata + "   " + ZhuiZhi_Integral_Scale_UncleFruit.Model.MainModel.CurrentShopInfo.shopname;          
        }
        public void UpdateEntityCardInfo(OutEntityCardResponseDto entityCard)
        {
            lblEntityCardNo.Text = entityCard.outcardid;
            lblMemberId.Text = MainModel.CurrentMember.memberheaderresponsevo.mobile;
            lblBalance.Text = "￥" + entityCard.balance.ToString("f2");
        }
    }
}
