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
using ZhuiZhi_Integral_Scale_UncleFruit.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    public partial class FormChangePhoneNumber : Form
    {
        //<summary>
        //按比例缩放页面及控件
        //</summary>
        private static AutoSizeFormUtil asf = new AutoSizeFormUtil();
        /// <summary>
        /// 调用接口
        /// </summary>
        MemberCenterHttpUtil membercenterhttputil = new MemberCenterHttpUtil();
        Member member;

        int type = 0;//1-手机验证码验证 2-余额支付密码验证 3-老卡验证 4-新卡验证
        bool isnew = true;//新手机 or 合并
        public FormChangePhoneNumber(Member m)
        {
            InitializeComponent();
            member = m;
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            MainModel.ShowChangePhonePage = 0;
            MainModel.ShowChangePhoneMedia = 0;
            DialogResult = DialogResult.Cancel;
            this.Close();

            MemberCenterMediaHelper.HidePayInfo();
        }

        private void FormChangePhoneNumber_Load(object sender, EventArgs e)
        {
            type = 0;
            lblShopName.Text = MainModel.Titledata + "   " + MainModel.CurrentShopInfo.shopname;
            lblMenu.Text = MainModel.CurrentUser.nickname + ",你好";
        }

        private void btnSmsCodeVerify_Click(object sender, EventArgs e)
        {
            BackHelper.ShowFormBackGround();
            string errormsg = "";
            membercenterhttputil.GetSendvalidateSmsCode(MainModel.CurrentMember.memberid, ref errormsg);
            if (MemberCenterHelper.ShowFormChengPhoneSmsCode())
            {
                type = 1;
                ShowChangePhonePage();
                label17.ForeColor = Color.DodgerBlue;
                picStepTwo.BackgroundImage = pictureBox1.BackgroundImage;
                label6.BackColor = Color.DodgerBlue;
                lblMerge.Visible = true;

            }
        }

        private void btnUserPassWordVerify_Click(object sender, EventArgs e)
        {
            if (MemberCenterHelper.ShowFormChangePhonePayPwd(member))
            {
                type = 2;
                ShowChangePhonePage();
            }
        }

        private void btnOldCardVerify_Click(object sender, EventArgs e)
        {
            if (MemberCenterHelper.ShowFormChangePhonePhysicalCard())
            {
                type = 3;
                ShowChangePhonePage();
            }
        }

        private void btnNewCardVerify_Click(object sender, EventArgs e)
        {
            if (MemberCenterHelper.ShowFormChangePhoneNewCard())
            {
                type = 4;
                ShowChangePhonePage();
            }
        }

        private void FormChangePhoneNumber_Shown(object sender, EventArgs e)
        {
            MemberCenterMediaHelper.ShowChangePhoneNumber();
        }

        private void FormChangePhoneNumber_FormClosing(object sender, FormClosingEventArgs e)
        {
            MemberCenterMediaHelper.HidePayInfo();
        }

        private void btnVerifyNewPhone_Click(object sender, EventArgs e)
        {
            VerifyNewPhone();
        }
        public void VerifyNewPhone()
        {
            try
            {
                isnew = true;
                BackHelper.ShowFormBackGround();
                string newphone = MemberCenterHelper.ShowFormChengPhoneVerifyNewPhone();
                if (!string.IsNullOrEmpty(newphone))
                {
                    if (MainModel.CurrentMember.memberheaderresponsevo.mobile == newphone)
                    {
                        MainModel.ShowLog("亲，请输入新的手机号", false);
                        return;
                    }
                    if (MemberCenterHelper.ShowFormChangePhoneNewPhoneSms(newphone))
                    {
                        string name = "验证新手机号";
                        if (name == btnVerifyNewPhone.Text)
                        {
                            ShowChangePhonePage();
                        }

                        label8.ForeColor = Color.DodgerBlue;
                        picStepThree.BackgroundImage = pictureBox1.BackgroundImage;
                        label7.BackColor = Color.DodgerBlue;
                        lblMerge.Visible = false;
                        lblChangePhone.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("验证新手机号异常：" + ex.Message, true);
            }
            finally
            {
                BackHelper.HideFormBackGround();
            }
           
        }

        private void lblMerge_Click(object sender, EventArgs e)
        {
            VerifyMember();
        }

        public void VerifyMember()
        {
            try
            {
                isnew = false;
                string numbervalue = ZhuiZhi_Integral_Scale_UncleFruit.PayUI.PayHelper.ShowFormVoucher();
                if (!string.IsNullOrEmpty(numbervalue))
                {
                    if (MainModel.CurrentMember.memberheaderresponsevo.mobile == numbervalue)
                    {
                         MainModel.ShowLog("亲，请输入新的会员卡号", false);
                        return;
                    }
                    string msg = "";
                    Member member = new HttpUtil().GetMember(numbervalue, ref msg);
                    if (member == null)
                    {
                        MainModel.ShowLog("亲，您还不是会员哦", false);
                        return;
                    }
                    BackHelper.ShowFormBackGround();
                    FormChangePhonePayPwd pwd = new FormChangePhonePayPwd(member);
                    asf.AutoScaleControlTest(pwd, 380, 197, 380 * MainModel.midScale, 197 * MainModel.midScale, true);
                    pwd.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - pwd.Width) / 2, (Screen.AllScreens[0].Bounds.Height - pwd.Height) / 2);
                    pwd.TopMost = true;
                    if (pwd.ShowDialog() == DialogResult.OK)
                    {
                        MainModel.NewPhone = numbervalue;
                        MainModel.ShowChangePhonePage = 2;
                        string name = "验证新手机号";
                        if (name == btnVerifyNewPhone.Text)
                        {
                            ShowChangePhonePage();
                        }

                        label8.ForeColor = Color.DodgerBlue;
                        picStepThree.BackgroundImage = pictureBox1.BackgroundImage;
                        label7.BackColor = Color.DodgerBlue;
                        lblMerge.Visible = false;
                        lblChangePhone.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("合并时验证会员异常：" + ex.Message, true);
            }
            finally
            {
                BackHelper.HideFormBackGround();
            }
        }

        private void lblChangePhone_Click(object sender, EventArgs e)
        {
            if (isnew)//新手机
            {
                VerifyNewPhone();//录入的手机也可能是已存在会员
            }
            else//合并
            {
                VerifyMember();
            }
        }



        private void btnOkChange_Click(object sender, EventArgs e)
        {
            ConfirmChange();

        }
        public void ConfirmChange()
        {
            string errormsg = "";
            bool resule = membercenterhttputil.GetCheckmember(MainModel.NewPhone, ref errormsg);
            if (resule)
            {
                MainModel.IsMemberCenter = true;
                if (MemberCenterHelper.ShowFormChangePhoneConfirm())
                {
                    this.Close();
                }
            }
            else
            {
                MainModel.IsMemberCenter = false;
                if (MemberCenterHelper.ShowFormChangePhoneConfirm())
                {
                    this.Close();
                }
            }
        }

        private void ShowChangePhonePage()
        {
            switch (MainModel.ShowChangePhonePage)
            {
                case 0:
                    btnSmsCodeVerify.Visible = true;
                    btnUserPassWordVerify.Visible = true;
                    btnNewCardVerify.Visible = true;
                    btnOldCardVerify.Visible = true;
                    btnVerifyNewPhone.Visible = false;
                    btnOkChange.Visible = false;
                    picVerifyMemberOK.Visible = false;
                    picVerifyPhoneOK.Visible = false;
                    picChangePhoneOK.Visible = false;
                    lblVerifySuccess.Visible = false;
                    lblNewPhone.Visible = false;
                    break;
                case 1:
                    btnSmsCodeVerify.Visible = false;
                    btnUserPassWordVerify.Visible = false;
                    btnNewCardVerify.Visible = false;
                    btnOldCardVerify.Visible = false;
                    btnVerifyNewPhone.Visible = true;
                    btnOkChange.Visible = false;
                    picVerifyMemberOK.Visible = true;
                    picVerifyPhoneOK.Visible = false;
                    picChangePhoneOK.Visible = false;
                    lblVerifySuccess.Visible = true;
                    lblNewPhone.Visible = false;
                    break;
                case 2:
                    btnSmsCodeVerify.Visible = false;
                    btnUserPassWordVerify.Visible = false;
                    btnNewCardVerify.Visible = false;
                    btnOldCardVerify.Visible = false;
                    btnVerifyNewPhone.Visible = false;
                    btnOkChange.Visible = true;
                    picVerifyMemberOK.Visible = true;
                    picVerifyPhoneOK.Visible = true;
                    picChangePhoneOK.Visible = false;
                    lblVerifySuccess.Visible = true;
                    lblNewPhone.Visible = true;
                    lblNewPhone.Text = MainModel.NewPhone;
                    break;
                default:
                    break;
            }
        }

    }
}
