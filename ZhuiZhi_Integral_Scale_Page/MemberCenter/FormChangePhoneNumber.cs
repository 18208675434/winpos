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
                ShowChangePhonePage();
                label17.ForeColor = Color.DodgerBlue;
                picStepTwo.BackgroundImage = pictureBox1.BackgroundImage;
                label6.BackColor = Color.DodgerBlue;
                label9.Visible = true;

            }
        }

        private void btnUserPassWordVerify_Click(object sender, EventArgs e)
        {
            if (MemberCenterHelper.ShowFormChangePhonePayPwd(member))
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

            yanzheng();
        }
        public void yanzheng()
        {
            if (MemberCenterHelper.ShowFormChengPhoneVerifyNewPhone())
            {
                string name = "验证新手机号";

                if (name == btnVerifyNewPhone.Text)
                {
                    ShowChangePhonePage();
                }

                label8.ForeColor = Color.DodgerBlue;
                picStepThree.BackgroundImage = pictureBox1.BackgroundImage;
                label7.BackColor = Color.DodgerBlue;
                label9.Visible = false;
                label11.Visible = true;
            }
        }
        public void yanse()
        {
            label8.ForeColor = Color.DodgerBlue;
            picStepThree.BackgroundImage = pictureBox1.BackgroundImage;
            label7.BackColor = Color.DodgerBlue;
            btnOkChange.Visible = true;

        }

        private void btnOkChange_Click(object sender, EventArgs e)
        {
            queren();

        }
        public void queren()
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

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void lblVerifySuccess_Click(object sender, EventArgs e)
        {

        }

        private void picVerifyMemberOK_Click(object sender, EventArgs e)
        {

        }



        private void label9_Click(object sender, EventArgs e)
        {
            BackHelper.ShowFormBackGround();
            //BackHelper.HideFormBackGround();
            FormMemberRecevice m = new FormMemberRecevice();
            asf.AutoScaleControlTest(m, 380, 197, 380 * MainModel.midScale, 197 * MainModel.midScale, true);
            m.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - m.Width) / 2, (Screen.AllScreens[0].Bounds.Height - m.Height) / 2);
            m.TopMost = true;
            m.ShowDialog();
            m.Dispose();
            //this.Enabled = true;
            
            if (m.DialogResult == DialogResult.OK)
            {
                label8.ForeColor = Color.DodgerBlue;
                picStepThree.BackgroundImage = pictureBox1.BackgroundImage;
                label7.BackColor = Color.DodgerBlue;
                button1.Visible = true;
                btnVerifyNewPhone.Visible = false;
                label9.Visible = false;
                label10.Visible = true;
                label11.Visible = true;
                GetPhone(MainModel.NewPhone);
            }
            else
            {
                m.Close();
                return;
            }
            
            







            this.Refresh();


        }
       
        
        public void GetPhone(string phone)
        {
            label10.Text = phone;
            label10.Font = new System.Drawing.Font("微软雅黑", 18, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            MainModel.NewPhone = label10.Text;
            MainModel.UpdatePhone = label10.Text;



        }

        private void button1_Click(object sender, EventArgs e)
        {
            BackHelper.ShowFormBackGround();
            //BackHelper.HideFormBackGround();
            FormHeBing f = new FormHeBing();
            asf.AutoScaleControlTest(f, 550, 200, 380 * MainModel.midScale, 200 * MainModel.midScale, true);
            f.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - f.Width) / 2, (Screen.AllScreens[0].Bounds.Height - f.Height) / 2);
            f.TopMost = true;

            f.ShowDialog();

            if (f.DialogResult == DialogResult.OK)
            {
                BackHelper.HideFormBackGround();
                this.Close();
            }

        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {
            BackHelper.ShowFormBackGround();
            FormMemberRecevice menre = new FormMemberRecevice();
            asf.AutoScaleControlTest(menre, 380, 197, 380 * MainModel.midScale, 197 * MainModel.midScale, true);
            menre.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - menre.Width) / 2, (Screen.AllScreens[0].Bounds.Height - menre.Height) / 2);
            menre.TopMost = true;
            menre.GetNewPhone();
            menre.ShowDialog();
        }
        //public void empty()
        //{
        //    btnVerifyNewPhone.Visible = true;
        //    label9.Visible = true;
        //    label10.Visible = true;
        //    label11.Visible = true;
        //    picStepThree.Visible = false; ;
        //    label8.Visible = false;
        //    button1.Visible = false;
        //}
    }
}
