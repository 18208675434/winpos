using Maticsoft.BLL;
using Maticsoft.Model;
using Newtonsoft.Json;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;
using ZhuiZhi_Integral_Scale_UncleFruit.MenuUI;
using ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    /// <summary>
    /// 挂失
    /// </summary>
    public partial class FormLossEntityCard : Form
    {

        #region 成员变量
        MemberCenterHttpUtil memberCenterHttpUtil = new MemberCenterHttpUtil();
        HttpUtil httpUtil = new HttpUtil();
        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();

        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();

        #endregion

        #region  页面加载与退出
        public FormLossEntityCard(string phone)
        {
            InitializeComponent();
            lblPhone.Text = phone;

        }
        private void FormLossEntityCard_Load(object sender, EventArgs e)
        {
            lblShopName.Text = MainModel.Titledata + "   " + MainModel.CurrentShopInfo.shopname;
            lblMenu.Text = MainModel.CurrentUser.nickname + ",你好";
            picMenu.Left = pnlMenu.Width - picMenu.Width - lblMenu.Width;
            lblMenu.Left = picMenu.Right;


        }
        private void FormLossEntityCard_Shown(object sender, EventArgs e)
        {           
            Application.DoEvents();
            LossEntityCardMediaHelper.ShowFormLossEntityCardMedia();
            LossEntityCardMediaHelper.UpdateEntityCardInfo(lblPhone.Text, "");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void formRechargeQuery_FormClosed(object sender, FormClosedEventArgs e)
        {
            timerSeconds.Enabled = false;         
          
            LossEntityCardMediaHelper.CloseLossEntityCardMedai();
        }
        #endregion


        #region 事件
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (timerSeconds.Enabled || string.IsNullOrEmpty(lblPhone.Text))
                {
                    return;
                }
                string msg = "";
                LoadingHelper.ShowLoadingScreen();
                bool flag = memberCenterHttpUtil.LossEntityCardGetSendsmscode(lblPhone.Text, ref msg);               
                if (!flag)
                {                   
                    MainModel.ShowLog("发送验证码失败：" + msg, true);
                    return;
                }
                timerSeconds.Tag = 60;
                timerSeconds.Enabled = true;
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("发送验证码异常" + ex.Message, true);
            }
            finally
            {
                LoadingHelper.CloseForm();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                if (string.IsNullOrEmpty(txtNewCardNo.Text))
                {
                    txtNewCardNo.Focus();
                    MainModel.ShowLog("请输入新卡卡号", true);
                    return;
                }
                if (string.IsNullOrEmpty(txtSmsCode.Text))
                {
                    txtSmsCode.Focus();
                    MainModel.ShowLog("请输入验证码", true);
                    return;
                }
                string err = "";
                LoadingHelper.ShowLoadingScreen();
                bool flag = memberCenterHttpUtil.LossEntityCard(lblPhone.Text, txtNewCardNo.Text.Trim(), txtSmsCode.Text, ref err);
                if (!flag)
                {
                    MainModel.ShowLog("挂失失败：" + err, true);
                    return;
                }
                try
                {
                    Member member = httpUtil.GetMember(lblPhone.Text, ref err);
                    MainModel.CurrentMember = member;
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog("ERROR", "获取会员信息异常:" + ex.Message);
                    MainModel.CurrentMember.memberentitycardresponsevo.cardid = txtNewCardNo.Text;
                }
                MainModel.ShowLog("挂失成功", true);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ERROR", "挂失异常:" + ex.Message);
                MainModel.ShowLog("挂失异常:" + ex.Message, true);
            }
            finally
            {               
                LoadingHelper.CloseForm();
                this.Enabled = true;
            }

        }

        #region 录入控制
        private void txt_OskClick(object sender, EventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                GlobalUtil.ShowKeyBoard(this, ZhuiZhi_Integral_Scale_UncleFruit.MyControl.KeyBorderCharType.NUMBER);

                Delay.Start(100);
                this.Activate();
                txt.Focus();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("焦点打开键盘异常" + ex.Message);
            }
        }

        //控制仅允许录入数字
        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                TextBox txt = sender as TextBox;
                e.Handled = true;
                char ch = e.KeyChar;

                if (ch >= '0' && ch <= '9')
                    e.Handled = false;

                if (ch == (char)Keys.Back)
                    e.Handled = false;

            }
            catch { }
        }

        private void txtNewCardNo_TextChanged(object sender, EventArgs e)
        {
            txtNewCardNo.Focus();
            if (txtNewCardNo.Text.Length > 0)
            {

                lblNewCardNo.Visible = false;
            }
            else
            {
                lblNewCardNo.Visible = true;
            }
            LossEntityCardMediaHelper.UpdateEntityCardInfo(lblPhone.Text,txtNewCardNo.Text);
        }

        private void txtSmsCode_TextChanged(object sender, EventArgs e)
        {
            txtSmsCode.Focus();
            if (txtSmsCode.Text.Length > 0)
            {
                lblSmsCode.Visible = false;
                if (txtSmsCode.Text.Length == 6)
                {
                    GlobalUtil.CloseKeyBoard(this);
                }
            }
            else
            {
                lblSmsCode.Visible = true;
            }

        }

        private void lblNewCardNo_Click(object sender, EventArgs e)
        {
            GlobalUtil.ShowKeyBoard(this, ZhuiZhi_Integral_Scale_UncleFruit.MyControl.KeyBorderCharType.NUMBER);
            Delay.Start(100);
            this.Activate();
            txtNewCardNo.Focus();
        }

        private void lblSmsCode_Click(object sender, EventArgs e)
        {
            GlobalUtil.ShowKeyBoard(this, ZhuiZhi_Integral_Scale_UncleFruit.MyControl.KeyBorderCharType.NUMBER);
            Delay.Start(100);
            this.Activate();
            txtSmsCode.Focus();
        }
        #endregion

        private void timerSeconds_Tick(object sender, EventArgs e)
        {
            timerSeconds.Interval = 1000;
            int left = (int)timerSeconds.Tag;
            left--;
            if (left <= 0)
            {
                timerSeconds.Enabled = false;
                btnSend.BackColor = Color.FromArgb(20, 158, 255);
                btnSend.Text = "重新发送";               
            }
            else
            {
                btnSend.BackColor = Color.Gray;
                btnSend.Text = "重新发送(" + left + ")";
                timerSeconds.Tag = left;
            }
        }
        #endregion

    }
}
