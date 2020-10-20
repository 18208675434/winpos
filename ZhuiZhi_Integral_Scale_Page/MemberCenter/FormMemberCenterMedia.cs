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

        private List<ListAllTemplate> LstTemplates = new List<ListAllTemplate>();
        //使用密码支付  RSA公钥加密后的值
        public string PayPassWord = "";


        public FormMemberCenterMedia()
        {
            InitializeComponent();

            //使用委托的话frmmain界面会卡死
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        public void UpdatememberInfo(string phone, string memberinfo, string balance, string credit, string creditspec, string coupon, string entitycardid = "")
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

                string encCardId = entitycardid;
                if (!string.IsNullOrEmpty(encCardId))
                {
                    if (encCardId.Length > 7)
                    {
                        encCardId = encCardId.Substring(0, 3) + "".PadLeft(encCardId.Length - 7, '*') + encCardId.Substring(encCardId.Length - 4);
                    }
                }
                lblEntityCardNo.Text = encCardId;
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
        public void UpdateEntityCardInfo(string entityCardNo)
        {
            try
            {
                lblEntityCardNo.Text = entityCardNo;
            }
            catch
            {

            }
        }
        public void refresh()
        {
            this.Refresh();
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
            }
            catch { }
        }
        #region 修改密码
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
                UpdatePassWordUpdateUI(0, "");

            }
            catch { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numtype"></param>
        /// <param name="smscode"></param>
        public void UpdatePassWordUpdateUI(int numtype, string smscode)
        {
            if (numtype == 0)
            {
                lblSavePwd.Text = "请输入原支付密码";
            }
            if (numtype == 1)
            {
                lblSavePwd.Text = "请输入支付密码";
            }
            if (numtype == 2)
            {
                lblSavePwd.Text = "请再次输入，确认支付密码";
            }
            switch (smscode.Length)
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
        #endregion

        #region 忘记密码
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
            }
            catch { }
        }

        public void UpdateForgetPassWordUI(int numtype, string smscode)
        {
            if (numtype != 0)
            {
                switch (smscode.Length)
                {
                    case 0: btnPassY1.Text = ""; btnPassY2.Text = ""; btnPassY3.Text = ""; btnPassY4.Text = ""; btnPassY5.Text = ""; btnPassY6.Text = ""; break;
                    case 1: btnPassY1.Text = "*"; btnPassY2.Text = ""; btnPassY3.Text = ""; btnPassY4.Text = ""; btnPassY5.Text = ""; btnPassY6.Text = ""; break;
                    case 2: btnPassY1.Text = "*"; btnPassY2.Text = "*"; btnPassY3.Text = ""; btnPassY4.Text = ""; btnPassY5.Text = ""; btnPassY6.Text = ""; break;
                    case 3: btnPassY1.Text = "*"; btnPassY2.Text = "*"; btnPassY3.Text = "*"; ; btnPassY4.Text = ""; btnPassY5.Text = ""; btnPassY6.Text = ""; break;
                    case 4: btnPassY1.Text = "*"; btnPassY2.Text = "*"; btnPassY3.Text = "*"; btnPassY4.Text = "*"; btnPassY5.Text = ""; btnPassY6.Text = ""; break;
                    case 5: btnPassY1.Text = "*"; btnPassY2.Text = "*"; btnPassY3.Text = "*"; btnPassY4.Text = "*"; btnPassY5.Text = "*"; ; btnPassY6.Text = ""; break;
                    case 6: btnPassY1.Text = "*"; btnPassY2.Text = "*"; btnPassY3.Text = "*"; btnPassY4.Text = "*"; btnPassY5.Text = "*"; btnPassY6.Text = "*"; break;

                    default: btnPassY1.Text = ""; btnPassY2.Text = ""; btnPassY3.Text = ""; btnPassY4.Text = ""; btnPassY5.Text = ""; btnPassY6.Text = ""; break;
                }
                if (numtype == 0)
                {
                    lblForgetPwd.Text = "请输入短信验证码";
                }
                if (numtype == 1)
                {
                    lblForgetPwd.Text = "请输入支付密码";
                }
                if (numtype == 2)
                {
                    lblForgetPwd.Text = "请再次输入，确认支付密码";

                }
            }
            else
            {
                switch (smscode.Length)
                {
                    case 0: btnPassY1.Text = ""; btnPassY2.Text = ""; btnPassY3.Text = ""; btnPassY4.Text = ""; btnPassY5.Text = ""; btnPassY6.Text = ""; break;
                    case 1: btnPassY1.Text = smscode.Substring(0, 1); btnPassY2.Text = ""; btnPassY3.Text = ""; btnPassY4.Text = ""; btnPassY5.Text = ""; btnPassY6.Text = ""; break;
                    case 2: btnPassY1.Text = smscode.Substring(0, 1); btnPassY2.Text = smscode.Substring(1, 1); btnPassY3.Text = ""; btnPassY4.Text = ""; btnPassY5.Text = ""; btnPassY6.Text = ""; break;
                    case 3: btnPassY1.Text = smscode.Substring(0, 1); btnPassY2.Text = smscode.Substring(1, 1); btnPassY3.Text = smscode.Substring(2, 1); btnPassY4.Text = ""; btnPassY5.Text = ""; btnPassY6.Text = ""; break;
                    case 4: btnPassY1.Text = smscode.Substring(0, 1); btnPassY2.Text = smscode.Substring(1, 1); btnPassY3.Text = smscode.Substring(2, 1); btnPassY4.Text = smscode.Substring(3, 1); btnPassY5.Text = ""; btnPassY6.Text = ""; break;
                    case 5: btnPassY1.Text = smscode.Substring(0, 1); btnPassY2.Text = smscode.Substring(1, 1); btnPassY3.Text = smscode.Substring(2, 1); btnPassY4.Text = smscode.Substring(3, 1); btnPassY5.Text = smscode.Substring(4, 1); ; btnPassY6.Text = ""; break;
                    case 6: btnPassY1.Text = smscode.Substring(0, 1); btnPassY2.Text = smscode.Substring(1, 1); btnPassY3.Text = smscode.Substring(2, 1); btnPassY4.Text = smscode.Substring(3, 1); btnPassY5.Text = smscode.Substring(4, 1); btnPassY6.Text = smscode.Substring(5, 1); break;

                    default: btnPassY1.Text = ""; btnPassY2.Text = ""; btnPassY3.Text = ""; btnPassY4.Text = ""; btnPassY5.Text = ""; btnPassY6.Text = ""; break;
                }
            }
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
            }
            catch { }
        }
        #endregion
        #region 更换手机
        /// <summary>
        /// 显示更换手机号码客屏
        /// </summary>
        public void ShowChangePhoneNumber(int step, bool ismember)
        {
            try
            {
                switch (step)
                {
                    case 0:
                        picStepOk.Visible = false;
                        picStep1.Visible = true;
                        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMemberCenterMedia));
                        //picStep1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picStep2.BackgroundImage")));
                        //lblStep1.BackColor = Color.FromArgb(173,173,173);                      
                        pnlLine1.BackColor = Color.FromArgb(238, 238, 238);
                        pnlLine2.BackColor = Color.FromArgb(238, 238, 238);
                        picStep2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picStep2.BackgroundImage")));
                        lblStep2.BackColor = Color.FromArgb(173, 173, 173);
                        picStep3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picStep2.BackgroundImage")));
                        lblStep3.BackColor = Color.FromArgb(173, 173, 173);

                        lblStep3Tip1.Visible = false;
                        lblStep3Tip2.Visible = false;
                        tlpMember.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 0);
                        tlpMember.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0);
                        tlpMember.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 0);
                        tlpMember.ColumnStyles[3] = new ColumnStyle(SizeType.Percent, 0);
                        tlpMember.ColumnStyles[4] = new ColumnStyle(SizeType.Percent, 0);
                        tlpMember.ColumnStyles[5] = new ColumnStyle(SizeType.Percent, 100);
                        break;
                    case 1:
                        pnlLine1.BackColor = Color.FromArgb(52, 147, 255);
                        lblStep2.BackColor = Color.FromArgb(52, 147, 255);
                        picStep2.BackgroundImage = picStep1.BackgroundImage;
                        break;
                    case 2:
                        pnlLine2.BackColor = Color.FromArgb(52, 147, 255);
                        lblStep3.BackColor = Color.FromArgb(52, 147, 255);
                        picStep3.BackgroundImage = picStep1.BackgroundImage;
                        if (ismember)//如果新手机是会员
                        {
                            lblStep3Tip1.Visible = true;
                            lblStep3Tip2.Visible = true;
                        }
                        break;
                    default:
                        break;
                }

            }
            catch { }
        }
        #endregion
    }
}
