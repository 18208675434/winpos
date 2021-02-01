using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    public partial class FormEditMember : Form
    {

        private MemberCenterHttpUtil memberhttp = new MemberCenterHttpUtil();
        private Member CurrentMember = null;

        #region 页面加载与退出
        public FormEditMember(Member member)
        {
            InitializeComponent();
            CurrentMember = member;
        }

        private void FormEditMember_Shown(object sender, EventArgs e)
        {
            if (CurrentMember.memberinformationresponsevo.gender == 1)
            {
                picWoman.BackgroundImage = picSelect.Image;
            }
            else
            {
                picMan.BackgroundImage = picSelect.Image;
            }
            dtStart.Value = DateTime.Now;

            
            MemberCenterMediaHelper.ShowEidtMember();

            txt_DataChanged("");
        }

        private void FormEditMember_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                MemberCenterMediaHelper.ShowMemberInfo();
                ZhuiZhi_Integral_Scale_UncleFruit.Common.GlobalUtil.CloseOSK();
            }
            catch { }
        }
        #endregion



        private void pnlMan_Click(object sender, EventArgs e)
        {
            picMan.BackgroundImage = picSelect.Image;
            picWoman.BackgroundImage = picNotSelect.Image;
            txt_DataChanged("");
        }

        private void pnlWoman_Click(object sender, EventArgs e)
        {
            picMan.BackgroundImage = picNotSelect.Image;
            picWoman.BackgroundImage = picSelect.Image;

            txt_DataChanged("");
        }


        private void txt_DataChanged(string data)
        {
            string name = txtName.Text;
            string birthday = txtBirthday.Text;
            string gender = "";

            if (picMan.BackgroundImage == picSelect.Image)
            {
                gender = "男";
            }
            else if (picWoman.BackgroundImage == picSelect.Image)
            {
                gender = "女";
            }
            MemberCenterMediaHelper.UpdateEditMemberInfo(name,birthday,gender);
            //CreateMemberMediaHelper.UpdateMemberInfo("", name, birthday, gender);
        }

        private void dtStart_MouseDown(object sender, MouseEventArgs e)
        {
            dtStart.Focus();
            SendKeys.Send("{F4}");
        }

        private void dtStart_CloseUp(object sender, EventArgs e)
        {
            txtBirthday.Text = dtStart.Value.ToString("yyyy-MM-dd");
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                CreateMemberPara para = new CreateMemberPara();
                para.memberid = CurrentMember.memberid;
                if (!string.IsNullOrEmpty(txtBirthday.Text))
                {
                    para.birthday = MainModel.getStampByDateTime(dtStart.Value);
                }

                if (picMan.BackgroundImage == picSelect.Image)
                {
                    para.gender = "0";
                }
                else if (picWoman.BackgroundImage == picSelect.Image)
                {
                    para.gender = "1";
                }
                para.nickname = txtName.Text;
                string errormsg = "";

                if (memberhttp.UpdateMember(para, ref errormsg))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MainModel.ShowLog(errormsg, false);
                    return;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtBirthday_Enter(object sender, EventArgs e)
        {
            dtStart.Focus();
            SendKeys.Send("{F4}");
        }

      
    }
}
