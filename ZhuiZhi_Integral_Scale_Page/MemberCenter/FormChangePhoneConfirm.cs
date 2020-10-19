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
using ZhuiZhi_Integral_Scale_UncleFruit.Model.HalfOffLine;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    public partial class FormChangePhoneConfirm : Form
    {
        MemberCenterHttpUtil memberchttputil = new MemberCenterHttpUtil();
        public FormChangePhoneConfirm()
        {
            InitializeComponent();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private Member CurrentMember = null;
        MemberCenterHttpUtil membercenterhttputil = new MemberCenterHttpUtil();
        private List<PromotionCoupon> CurrentLstCoupon = null;

        private void FormChangePhoneConfirm_Load(object sender, EventArgs e)
        {
            if (MainModel.IsMemberCenter)
            {
                lblConfirmChangeNamber.Text = "手机号" + MainModel.NewPhone + "已注册为会员，是否合并账户？\r\n";
                lblConfirmChangeNamber.Text = lblConfirmChangeNamber.Text + "合并后本账户的积分和余额将迁移到新手机号的账户中。\r\n订单数据将不会迁移。";
                btnCancle.Text = "不合并";
                btnOK.Text = "合并账户";
            }
            else
            {
                lblConfirmChangeNamber.Text = "确认更换手机号码为" + MainModel.NewPhone + "?";
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {       
            try
            {
                bool result = false;
                string err = "";
                HttpUtil httputil = new HttpUtil();
                LoadingHelper.ShowLoadingScreen();
                if (MainModel.IsMemberCenter)
                {
                    Member mermber = httputil.GetMember(MainModel.NewPhone, ref err);
                    if (mermber == null)
                    {
                        return;
                    }
                    result = memberchttputil.MergeMemberPhonenumber(MainModel.CurrentMember.memberheaderresponsevo.token, mermber.memberheaderresponsevo.token, ref err);
                }
                else
                {
                    result = memberchttputil.Updatemembermobile(MainModel.NewPhone, ref err);                   
                }              
                if (result)
                {
                    MainModel.ShowLog("手机号更换成功", false);
                    this.DialogResult = DialogResult.OK;
                    this.Close();                                                
                }
                else
                {
                    MainModel.ShowLog(err, false);
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog(ex.Message, true);
            }
            finally
            {
                LoadingHelper.CloseForm();
            }
        }
    }
}
