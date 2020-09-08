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

        private ListAllTemplate CurrentTemplate = null;

        private List<ListAllTemplate> LstTemplates = new List<ListAllTemplate>();

        bool IsEnable = true;


        //使用密码支付  RSA公钥加密后的值
        public string PayPassWord = "";


        public FormMemberCenterMedia()
        {
            InitializeComponent();

            //使用委托的话frmmain界面会卡死
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        public void UpdatememberInfo(string phone, string memberinfo, string balance, string credit, string creditspec, string coupon)
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
            try
            {

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
                tlpMember.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 100);
                tlpMember.ColumnStyles[3] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[4] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[5] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[6] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[7] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[8] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[9] = new ColumnStyle(SizeType.Percent, 0);
            }
            catch { }
        }

        public void HidePayInfo()
        {
            try
            {
                tlpMember.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100);
                tlpMember.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[3] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[4] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[5] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[6] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[7] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[8] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[9] = new ColumnStyle(SizeType.Percent, 0);
            }
            catch { }
        }
        /// <summary>
        /// 显示修改密码客屏界面
        /// </summary>
        public void ShowServrPassWord()
        {
            try
            {
                tlpMember.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 100);
                tlpMember.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[3] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[4] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[5] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[6] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[7] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[8] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[9] = new ColumnStyle(SizeType.Percent, 0);

            }
            catch { }
        }
        /// <summary>
        /// 显示忘记密码客屏界面
        /// </summary>
        public void ShowForgetPassWord()
        {
            try
            {
                tlpMember.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[3] = new ColumnStyle(SizeType.Percent, 100);
                tlpMember.ColumnStyles[4] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[5] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[6] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[7] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[8] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[9] = new ColumnStyle(SizeType.Percent, 0);
            }
            catch { }
        }

        /// <summary>
        /// 显示修改成功客屏界面
        /// </summary>
        public void ShowChangePassWordOK()
        {
            try
            {
                tlpMember.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[3] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[4] = new ColumnStyle(SizeType.Percent, 100);
                tlpMember.ColumnStyles[5] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[6] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[7] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[8] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[9] = new ColumnStyle(SizeType.Percent, 0);
            }
            catch { }
        }
        /// <summary>
        /// 显示更换手机号码客屏
        /// </summary>
        public void ShowChangePhoneNumber()
        {
            try
            {
                switch (MainModel.ShowChangePhoneMedia)
                {
                    case 0:
                        picTheFirstStep.Visible = true;
                        lblTheStepOne.Visible = true;
                        picFirstStepSuccess.Visible = false;
                        tlpMember.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 0);
                        tlpMember.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0);
                        tlpMember.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 0);
                        tlpMember.ColumnStyles[3] = new ColumnStyle(SizeType.Percent, 0);
                        tlpMember.ColumnStyles[4] = new ColumnStyle(SizeType.Percent, 0);
                        tlpMember.ColumnStyles[5] = new ColumnStyle(SizeType.Percent, 100);
                        tlpMember.ColumnStyles[6] = new ColumnStyle(SizeType.Percent, 0);
                        tlpMember.ColumnStyles[7] = new ColumnStyle(SizeType.Percent, 0);
                        tlpMember.ColumnStyles[8] = new ColumnStyle(SizeType.Percent, 0);
                        tlpMember.ColumnStyles[9] = new ColumnStyle(SizeType.Percent, 0);
                        break;
                    case 1:
                        picTheFirstStep.Visible = false;
                        lblTheStepOne.Visible = false;
                        picFirstStepSuccess.Visible = true;
                        tlpMember.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 0);
                        tlpMember.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0);
                        tlpMember.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 0);
                        tlpMember.ColumnStyles[3] = new ColumnStyle(SizeType.Percent, 0);
                        tlpMember.ColumnStyles[4] = new ColumnStyle(SizeType.Percent, 0);
                        tlpMember.ColumnStyles[5] = new ColumnStyle(SizeType.Percent, 100);
                        tlpMember.ColumnStyles[6] = new ColumnStyle(SizeType.Percent, 0);
                        tlpMember.ColumnStyles[7] = new ColumnStyle(SizeType.Percent, 0);
                        tlpMember.ColumnStyles[8] = new ColumnStyle(SizeType.Percent, 0);
                        tlpMember.ColumnStyles[9] = new ColumnStyle(SizeType.Percent, 0);
                        break;
                    default:
                        break;
                }

            }
            catch { }
        }
        /// <summary>
        /// 显示更换手机号码支付密码验证屏
        /// </summary>
        public void ShowChangePhonePayPwd()
        {
            try
            {
                tlpMember.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[3] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[4] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[5] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[6] = new ColumnStyle(SizeType.Percent, 100);
                tlpMember.ColumnStyles[7] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[8] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[9] = new ColumnStyle(SizeType.Percent, 0);
            }
            catch { }
        }
        /// <summary>
        /// 显示更换手机号码新旧卡验证屏
        /// </summary>
        public void ShowChangePhoneNewOldCard()
        {
            try
            {
                tlpMember.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[3] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[4] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[5] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[6] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[7] = new ColumnStyle(SizeType.Percent, 100);
                tlpMember.ColumnStyles[8] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[9] = new ColumnStyle(SizeType.Percent, 0);
            }
            catch { }
        }
        /// <summary>
        /// 显示更换手机号码验证码验证屏
        /// </summary>
        public void ShowChengPhoneSmsCode()
        {
            try
            {
                tlpMember.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[3] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[4] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[5] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[6] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[7] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[8] = new ColumnStyle(SizeType.Percent, 100);
                tlpMember.ColumnStyles[9] = new ColumnStyle(SizeType.Percent, 0);
            }
            catch { }
        }
        /// <summary>
        /// 显示新手机号码验证屏
        /// </summary>
        public void ChengPhoneVerifyNewPhone()
        {
            try
            {
                tlpMember.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[3] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[4] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[5] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[6] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[7] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[8] = new ColumnStyle(SizeType.Percent, 0);
                tlpMember.ColumnStyles[9] = new ColumnStyle(SizeType.Percent, 100);
            }
            catch { }
        }
        /// <summary>
        /// 密码输入时显示*
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerPassWord_Tick(object sender, EventArgs e)
        {

            switch (MainModel.SevaePwd.Length)
            {
                case 0: btnPassWord1.Text = ""; btnPassWord2.Text = ""; btnPassWord3.Text = ""; btnPassWord4.Text = ""; btnPassWord5.Text = ""; btnPassWord6.Text = ""; break;
                case 1: btnPassWord1.Text = "*"; btnPassWord2.Text = ""; btnPassWord3.Text = ""; btnPassWord4.Text = ""; btnPassWord5.Text = ""; btnPassWord6.Text = ""; break;
                case 2: btnPassWord1.Text = "*"; btnPassWord2.Text = "*"; btnPassWord3.Text = ""; btnPassWord4.Text = ""; btnPassWord5.Text = ""; btnPassWord6.Text = ""; break;
                case 3: btnPassWord1.Text = "*"; btnPassWord2.Text = "*"; btnPassWord3.Text = "*"; ; btnPassWord4.Text = ""; btnPassWord5.Text = ""; btnPassWord6.Text = ""; break;
                case 4: btnPassWord1.Text = "*"; btnPassWord2.Text = "*"; btnPassWord3.Text = "*"; btnPassWord4.Text = "*"; btnPassWord5.Text = ""; btnPassWord6.Text = ""; break;
                case 5: btnPassWord1.Text = "*"; btnPassWord2.Text = "*"; btnPassWord3.Text = "*"; btnPassWord4.Text = "*"; btnPassWord5.Text = "*"; ; btnPassWord6.Text = ""; break;
                case 6: btnPassWord1.Text = "*"; btnPassWord2.Text = "*"; btnPassWord3.Text = "*"; btnPassWord4.Text = "*"; btnPassWord5.Text = "*"; btnPassWord6.Text = "*"; break;

                default: btnPassWord1.Text = ""; btnPassWord2.Text = ""; btnPassWord3.Text = ""; btnPassWord4.Text = ""; btnPassWord5.Text = ""; btnPassWord6.Text = ""; break;
            }
        }

        private void FormMemberCenterMedia_Load(object sender, EventArgs e)
        {
            //调用定时刷新控件   刷新客屏数据
            timerPassWord.Enabled = true;
            timerSmsCode.Enabled = true;
            timerChangePhoneScd.Enabled = true;
            timerChangePhonePwd.Enabled = true;

        }

        private void timerSmsCode_Tick(object sender, EventArgs e)
        {
            if (MainModel.inputimes != 0)
            {
                switch (MainModel.SmsCode.Length)
                {
                    case 0: btnPassY1.Text = ""; btnPassY2.Text = ""; btnPassY3.Text = ""; btnPassY4.Text = ""; btnPassY5.Text = ""; btnPassY6.Text = ""; break;
                    case 1: btnPassY1.Text = "*"; btnPassY2.Text = ""; btnPassY3.Text = ""; btnPassY4.Text = ""; btnPassY5.Text = ""; btnPassY6.Text = ""; break;
                    case 2: btnPassY1.Text = "*"; btnPassY2.Text = "*"; btnPassY3.Text = ""; btnPassY4.Text = ""; btnPassY5.Text = ""; btnPassY6.Text = ""; break;
                    case 3: btnPassY1.Text = "*"; btnPassY2.Text = "*"; btnPassY3.Text = "*"; ; btnPassY4.Text = ""; btnPassY5.Text = ""; btnPassY6.Text = ""; break;
                    case 4: btnPassY1.Text = "*"; btnPassY2.Text = "*"; btnPassY3.Text = "*"; btnPassY4.Text = "*"; btnPassY5.Text = ""; btnPassY6.Text = ""; break;
                    case 5: btnPassY1.Text = "*"; btnPassY2.Text = "*"; btnPassY3.Text = "*"; btnPassY4.Text = "*"; btnPassY5.Text = "*"; ; btnPassY6.Text = ""; break;
                    case 6: btnPassY1.Text = "*"; btnPassY2.Text = "*"; btnPassY3.Text = "*"; btnPassY4.Text = "*"; btnPassY5.Text = "*"; btnPassY6.Text = "*"; break;

                    default: btnPassY1.Text = ""; btnPassY2.Text = ""; btnPassY3.Text = ""; btnPassY4.Text = ""; btnPassY5.Text = ""; btnChangesms6.Text = ""; break;
                }
                if (MainModel.inputimes == 1)
                {
                    lblForgetPwd.Text = "请确认支付密码";
                }
                if (MainModel.inputimes == 2)
                {
                    lblForgetPwd.Text = "请设置支付密码";
                }
                if (MainModel.inputimes == 0)
                {
                    lblForgetPwd.Text = "请输入短信验证码";
                }
            }
            else
            {
                switch (MainModel.SmsCode.Length)
                {
                    case 0: btnPassY1.Text = ""; btnPassY2.Text = ""; btnPassY3.Text = ""; btnPassY4.Text = ""; btnPassY5.Text = ""; btnPassY6.Text = ""; break;
                    case 1: btnPassY1.Text = MainModel.SmsCode.Substring(0, 1); btnPassY2.Text = ""; btnPassY3.Text = ""; btnPassY4.Text = ""; btnPassY5.Text = ""; btnPassY6.Text = ""; break;
                    case 2: btnPassY1.Text = MainModel.SmsCode.Substring(0, 1); btnPassY2.Text = MainModel.SmsCode.Substring(1, 1); btnPassY3.Text = ""; btnPassY4.Text = ""; btnPassY5.Text = ""; btnPassY6.Text = ""; break;
                    case 3: btnPassY1.Text = MainModel.SmsCode.Substring(0, 1); btnPassY2.Text = MainModel.SmsCode.Substring(1, 1); btnPassY3.Text = MainModel.SmsCode.Substring(2, 1); btnPassY4.Text = ""; btnPassY5.Text = ""; btnPassY6.Text = ""; break;
                    case 4: btnPassY1.Text = MainModel.SmsCode.Substring(0, 1); btnPassY2.Text = MainModel.SmsCode.Substring(1, 1); btnPassY3.Text = MainModel.SmsCode.Substring(2, 1); btnPassY4.Text = MainModel.SmsCode.Substring(3, 1); btnPassY5.Text = ""; btnPassY6.Text = ""; break;
                    case 5: btnPassY1.Text = MainModel.SmsCode.Substring(0, 1); btnPassY2.Text = MainModel.SmsCode.Substring(1, 1); btnPassY3.Text = MainModel.SmsCode.Substring(2, 1); btnPassY4.Text = MainModel.SmsCode.Substring(3, 1); btnPassY5.Text = MainModel.SmsCode.Substring(4, 1); ; btnPassY6.Text = ""; break;
                    case 6: btnPassY1.Text = MainModel.SmsCode.Substring(0, 1); btnPassY2.Text = MainModel.SmsCode.Substring(1, 1); btnPassY3.Text = MainModel.SmsCode.Substring(2, 1); btnPassY4.Text = MainModel.SmsCode.Substring(3, 1); btnPassY5.Text = MainModel.SmsCode.Substring(4, 1); btnPassY6.Text = MainModel.SmsCode.Substring(5, 1); break;

                    default: btnPassY1.Text = ""; btnPassY2.Text = ""; btnPassY3.Text = ""; btnPassY4.Text = ""; btnPassY5.Text = ""; btnPassY6.Text = ""; break;
                }
            }

        }

        private void timerChangePhoneScd_Tick(object sender, EventArgs e)
        {
            switch (MainModel.ChangeScd.Length)
            {
                case 0: btnChangesms1.Text = ""; btnChangesms2.Text = ""; btnChangesms3.Text = ""; btnChangesms4.Text = ""; btnChangesms5.Text = ""; btnChangesms6.Text = ""; break;
                case 1: btnChangesms1.Text = MainModel.ChangeScd.Substring(0, 1); btnChangesms2.Text = ""; btnChangesms3.Text = ""; btnChangesms4.Text = ""; btnChangesms5.Text = ""; btnChangesms6.Text = ""; break;
                case 2: btnChangesms1.Text = MainModel.ChangeScd.Substring(0, 1); btnChangesms2.Text = MainModel.ChangeScd.Substring(1, 1); btnChangesms3.Text = ""; btnChangesms4.Text = ""; btnChangesms5.Text = ""; btnChangesms6.Text = ""; break;
                case 3: btnChangesms1.Text = MainModel.ChangeScd.Substring(0, 1); btnChangesms2.Text = MainModel.ChangeScd.Substring(1, 1); btnChangesms3.Text = MainModel.ChangeScd.Substring(2, 1); btnChangesms4.Text = ""; btnChangesms5.Text = ""; btnChangesms6.Text = ""; break;
                case 4: btnChangesms1.Text = MainModel.ChangeScd.Substring(0, 1); btnChangesms2.Text = MainModel.ChangeScd.Substring(1, 1); btnChangesms3.Text = MainModel.ChangeScd.Substring(2, 1); btnChangesms4.Text = MainModel.ChangeScd.Substring(3, 1); btnChangesms5.Text = ""; btnChangesms6.Text = ""; break;
                case 5: btnChangesms1.Text = MainModel.ChangeScd.Substring(0, 1); btnChangesms2.Text = MainModel.ChangeScd.Substring(1, 1); btnChangesms3.Text = MainModel.ChangeScd.Substring(2, 1); btnChangesms4.Text = MainModel.ChangeScd.Substring(3, 1); btnChangesms5.Text = MainModel.ChangeScd.Substring(4, 1); btnChangesms6.Text = ""; break;
                case 6: btnChangesms1.Text = MainModel.ChangeScd.Substring(0, 1); btnChangesms2.Text = MainModel.ChangeScd.Substring(1, 1); btnChangesms3.Text = MainModel.ChangeScd.Substring(2, 1); btnChangesms4.Text = MainModel.ChangeScd.Substring(3, 1); btnChangesms5.Text = MainModel.ChangeScd.Substring(4, 1); btnChangesms6.Text = MainModel.ChangeScd.Substring(5, 1); break;

                default: btnChangesms1.Text = ""; btnChangesms2.Text = ""; btnChangesms3.Text = ""; btnChangesms4.Text = ""; btnChangesms5.Text = ""; btnChangesms6.Text = ""; break;
            }
        }

        private void timerChangePhonePwd_Tick(object sender, EventArgs e)
        {
            switch (MainModel.ChangePwd.Length)
            {
                case 0: btnChangePwd1.Text = ""; btnChangePwd2.Text = ""; btnChangePwd3.Text = ""; btnChangePwd4.Text = ""; btnChangePwd5.Text = ""; btnChangePwd6.Text = ""; break;
                case 1: btnChangePwd1.Text = "*"; btnChangePwd2.Text = ""; btnChangePwd3.Text = ""; btnChangePwd4.Text = ""; btnChangePwd5.Text = ""; btnChangePwd6.Text = ""; break;
                case 2: btnChangePwd1.Text = "*"; btnChangePwd2.Text = "*"; btnChangePwd3.Text = ""; btnChangePwd4.Text = ""; btnChangePwd5.Text = ""; btnChangePwd6.Text = ""; break;
                case 3: btnChangePwd1.Text = "*"; btnChangePwd2.Text = "*"; btnChangePwd3.Text = "*"; ; btnChangePwd4.Text = ""; btnChangePwd5.Text = ""; btnChangePwd6.Text = ""; break;
                case 4: btnChangePwd1.Text = "*"; btnChangePwd2.Text = "*"; btnChangePwd3.Text = "*"; btnChangePwd4.Text = "*"; btnChangePwd5.Text = ""; btnChangePwd6.Text = ""; break;
                case 5: btnChangePwd1.Text = "*"; btnChangePwd2.Text = "*"; btnChangePwd3.Text = "*"; btnChangePwd4.Text = "*"; btnChangePwd5.Text = "*"; ; btnChangePwd6.Text = ""; break;
                case 6: btnChangePwd1.Text = "*"; btnChangePwd2.Text = "*"; btnChangePwd3.Text = "*"; btnChangePwd4.Text = "*"; btnChangePwd5.Text = "*"; btnChangePwd6.Text = "*"; break;

                default: btnChangePwd1.Text = ""; btnChangePwd2.Text = ""; btnPassWord3.Text = ""; btnChangePwd4.Text = ""; btnChangePwd5.Text = ""; btnChangePwd6.Text = ""; break;
            }
        }


    }
}
