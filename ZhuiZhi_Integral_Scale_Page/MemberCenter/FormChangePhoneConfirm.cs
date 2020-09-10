using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

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

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (MainModel.IsMemberCenter)
            {
                btnOK.Text = "确认合并";
                MainModel.Sourcetoken = MainModel.CurrentMember.memberheaderresponsevo.token;
                string err = "";
                bool resule = memberchttputil.Updatemembermobile(MainModel.NewPhone, ref err);
                string errrormsg = "";
                if (resule)
                {
                    
                    bool mergeresult = memberchttputil.MergeMemberPhonenumber(ref errrormsg);
                    if (mergeresult)
                    {
                        MainModel.ShowLog(errrormsg, false);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MainModel.ShowLog(errrormsg, false);
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                    }
                }
                else
                {
                    MainModel.ShowLog(errrormsg, false);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            }
            else
            {
                string err = "";
                bool resule = memberchttputil.Updatemembermobile(MainModel.NewPhone, ref err);
                if (resule)
                {
                    MainModel.ShowLog(err, false);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MainModel.ShowLog(err, false);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            }

        }

        private void FormChangePhoneConfirm_Load(object sender, EventArgs e)
        {
            if (MainModel.IsMemberCenter)
            {
                lblConfirmChangeNamber.Text = "当前修改号码已是会员，是否合并会员，合并后数据将被转移至新会员";
            }
            else
            {
                lblConfirmChangeNamber.Text = "确认更换手机号码为" + MainModel.NewPhone + "?";
            }

        }
    }
}
