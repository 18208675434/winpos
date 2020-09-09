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
        public FormChangePhoneNumber()
        {
            InitializeComponent();
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
            lblShopName.Text = MainModel.Titledata + "   " + MainModel.CurrentShopInfo.shopname;
            lblMenu.Text = MainModel.CurrentUser.nickname + ",你好";
        }

        private void btnSmsCodeVerify_Click(object sender, EventArgs e)
        {
            string errormsg = "";
            membercenterhttputil.GetSendvalidateSmsCode(MainModel.CurrentMember.memberid, ref errormsg);
            if (MemberCenterHelper.ShowFormChengPhoneSmsCode())
            {
                ShowChangePhonePage();
            }
        }

        private void btnUserPassWordVerify_Click(object sender, EventArgs e)
        {
            if (MemberCenterHelper.ShowFormChangePhonePayPwd())
            {
                ShowChangePhonePage();
            }
        }

        private void btnOldCardVerify_Click(object sender, EventArgs e)
        {
            if (MemberCenterHelper.ShowFormChangePhonePhysicalCard())
            {
                ShowChangePhonePage();
            }
        }

        private void btnNewCardVerify_Click(object sender, EventArgs e)
        {
            if (MemberCenterHelper.ShowFormChangePhoneNewCard())
            {
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
            if (MemberCenterHelper.ShowFormChengPhoneVerifyNewPhone())
            {
                ShowChangePhonePage();
            }
        }

        private void btnOkChange_Click(object sender, EventArgs e)
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

        private void picChangePhoneOK_Click(object sender, EventArgs e)
        {

        }
    }
}
