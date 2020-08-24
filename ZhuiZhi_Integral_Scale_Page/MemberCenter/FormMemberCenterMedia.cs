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
using ZhuiZhi_Integral_Scale_UncleFruit.Model.HalfOffLine;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    public partial class FormMemberCenterMedia : Form
    {
        private HttpUtil httputil = new HttpUtil();

        private Member CurrentMember = null;

        private ListAllTemplate CurrentTemplate =null;

        private List<ListAllTemplate> LstTemplates =new List<ListAllTemplate>();

        bool IsEnable=true;
        public FormMemberCenterMedia()
        {
            InitializeComponent();

            //使用委托的话frmmain界面会卡死
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        public void UpdatememberInfo(string phone,string memberinfo,string balance,string credit,string creditspec,string coupon)
        {
            try
            {
                lblPhone.Text = phone;
                lblMemberInfo.Text = memberinfo;
                lblBalance.Text = balance;
                lblCredit.Text = credit;
                lblCoupon.Text = coupon;
                lblCreditAmount.Text = creditspec;

                lblCreditAmount.Left = lblCredit.Right;
            }
            catch { }
        }

        public void UpdateDgvTemplate(List<Bitmap> lstbmp)
        {
            try {

                dgvTemplate.Rows.Clear();
                int emptycount = 3 - lstbmp.Count % 3;
                for (int i = 0; i < emptycount; i++)
                {
                    lstbmp.Add(Resources.ResourcePos.empty);
                }
                int rowcount = lstbmp.Count / 3;

                for (int i = 0; i < rowcount; i++)
                {
                    dgvTemplate.Rows.Add(lstbmp[i * 3 + 0], lstbmp[i * 3 + 1], lstbmp[i * 3 + 2]);
                }
            }
            catch
            {

            }
        }

        public void ShowPayInfo()
        {
            try
            {
                tlpMember.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 100);
            }
            catch { }
        }

        public void HidePayInfo()
        {
            try
            {
                tlpMember.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100);
                tlpMember.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0);
            }
            catch { }
        }
    }
}
