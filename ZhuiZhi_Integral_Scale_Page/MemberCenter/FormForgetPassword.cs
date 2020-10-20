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

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    public partial class FormForgetPassword : Form
    {
        /// <summary> 
        /// 输入验证码
        /// </summary>
        public string smscode = "";
        /// <summary>  0-验证码录入 1-新密码录入 2-新密码确认
        /// </summary>
        public int numtype = 0;
        //存储新密码
        public string newPassWord = "";
        //存储短信验证码
        public string serveSmscode = "";

        MemberCenterHttpUtil memberhttputil = new MemberCenterHttpUtil();

        public FormForgetPassword()
        {
            InitializeComponent();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FormForgetPassword_Shown(object sender, EventArgs e)
        {
            UpdatePassWord(0,"");//先更改UI   
            btnSend_Click(null, null);
            MemberCenterMediaHelper.ShowForgetPassWord();
        }
        /// <summary>
        /// 关闭界面同时关闭客屏界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormForgetPassword_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {              
                MemberCenterMediaHelper.HidePayInfo();
            }
            catch (Exception ex)
            { }
        }

        /// <summary>
        /// 监听键盘输入
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {

            //char ch =(char) keyData;

            //MessageBox.Show(ch.ToString());

            //*获取按键后使之失效，防止有焦点事件获取按键信息   !base.ProcessDialogKey(keyData)
            switch (keyData)
            {
                //不同键盘数字键值不同
                case Keys.D0: AddNum(0, false); return !base.ProcessDialogKey(keyData); break;
                case Keys.D1: AddNum(1, false); return !base.ProcessDialogKey(keyData); break;
                case Keys.D2: AddNum(2, false); return !base.ProcessDialogKey(keyData); break;
                case Keys.D3: AddNum(3, false); return !base.ProcessDialogKey(keyData); break;
                case Keys.D4: AddNum(4, false); return !base.ProcessDialogKey(keyData); break;
                case Keys.D5: AddNum(5, false); return !base.ProcessDialogKey(keyData); break;
                case Keys.D6: AddNum(6, false); return !base.ProcessDialogKey(keyData); break;
                case Keys.D7: AddNum(7, false); return !base.ProcessDialogKey(keyData); break;
                case Keys.D8: AddNum(8, false); return !base.ProcessDialogKey(keyData); break;
                case Keys.D9: AddNum(9, false); return !base.ProcessDialogKey(keyData); break;

                case Keys.NumPad0: AddNum(0, false); return !base.ProcessDialogKey(keyData); break;
                case Keys.NumPad1: AddNum(1, false); return !base.ProcessDialogKey(keyData); break;
                case Keys.NumPad2: AddNum(2, false); return !base.ProcessDialogKey(keyData); break;
                case Keys.NumPad3: AddNum(3, false); return !base.ProcessDialogKey(keyData); break;
                case Keys.NumPad4: AddNum(4, false); return !base.ProcessDialogKey(keyData); break;
                case Keys.NumPad5: AddNum(5, false); return !base.ProcessDialogKey(keyData); break;
                case Keys.NumPad6: AddNum(6, false); return !base.ProcessDialogKey(keyData); break;
                case Keys.NumPad7: AddNum(7, false); return !base.ProcessDialogKey(keyData); break;
                case Keys.NumPad8: AddNum(8, false); return !base.ProcessDialogKey(keyData); break;
                case Keys.NumPad9: AddNum(9, false); return !base.ProcessDialogKey(keyData); break;

                case Keys.Back: AddNum(0, true); return base.ProcessDialogKey(keyData); break;
                case Keys.Enter: return !base.ProcessDialogKey(keyData); break;
            }

            return base.ProcessDialogKey(keyData);

        }

        private void AddNum(int num, bool isDel)
        {
            if (isDel && smscode.Length > 0)
            {
                smscode = smscode.Substring(0, smscode.Length - 1);
                UpdatePassWord(numtype,smscode);//先更改UI   
            }
            else
            {
                if (smscode.Length > 6)//验证码满足6位，屏蔽录入
                {
                    return;
                }
                smscode += num;
                UpdatePassWord(numtype,smscode);//先更改UI    
            }

            if (smscode.Length == 6 && numtype == 0)
            {
                serveSmscode = smscode;
                string err = "";
                string result = memberhttputil.GetVerifysmscode(MainModel.CurrentMember.memberid,smscode, ref err); ;
                if (result == "success")
                {
                    label2.Text = "请输入新的支付密码";
                    btnSend.Visible = false;
                    UpdatePassWord(1,"");//重置UI
                }
                else
                {
                    this.Close();
                    MainModel.ShowLog("短信验证码错误", false);
                    MemberCenterMediaHelper.ShowLog("短信验证码错误");
                }
            }
            if (smscode.Length == 6 && numtype == 1)
            {
                //存储到第一次输入的新密码
                newPassWord = smscode;
                label2.Text = "请等待用户确认密码";
                UpdatePassWord(2,"");//重置UI
            }
            if (smscode.Length == 6 && numtype == 2)
            {
                if (smscode == newPassWord)
                {
                    string ErroM = "";
                    string newpassword = smscode;
                    //验证密码类型
                    int resuleCode = 0;
                    //验证成功
                    string result = memberhttputil.ForgetSetPassWord(MainModel.RSAEncrypt(MainModel.RSAPrivateKey, newpassword), 1, serveSmscode, ref ErroM, ref resuleCode);
                    if (result == "true")
                    {                      
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }                 
                }
                if (smscode != newPassWord)
                {
                    label2.Text = "两次输入密码不一致，请重新输入";
                    MemberCenterMediaHelper.ShowLog("两次输入密码不一致，请重新输入");
                    UpdatePassWord(1,"");//重置UI
                }
            }
        }
        private void UpdatePassWord(int numtype, string smscode)
        {
            this.numtype = numtype;
            this.smscode = smscode;
            if (numtype != 0)
            {
                switch (smscode.Length)
                {
                    case 0: btnPassW1.Text = ""; btnPassW2.Text = ""; btnPassW3.Text = ""; btnPassW4.Text = ""; btnPassW5.Text = ""; btnPassW6.Text = ""; break;
                    case 1: btnPassW1.Text = "*"; btnPassW2.Text = ""; btnPassW3.Text = ""; btnPassW4.Text = ""; btnPassW5.Text = ""; btnPassW6.Text = ""; break;
                    case 2: btnPassW1.Text = "*"; btnPassW2.Text = "*"; btnPassW3.Text = ""; btnPassW4.Text = ""; btnPassW5.Text = ""; btnPassW6.Text = ""; break;
                    case 3: btnPassW1.Text = "*"; btnPassW2.Text = "*"; btnPassW3.Text = "*"; ; btnPassW4.Text = ""; btnPassW5.Text = ""; btnPassW6.Text = ""; break;
                    case 4: btnPassW1.Text = "*"; btnPassW2.Text = "*"; btnPassW3.Text = "*"; btnPassW4.Text = "*"; btnPassW5.Text = ""; btnPassW6.Text = ""; break;
                    case 5: btnPassW1.Text = "*"; btnPassW2.Text = "*"; btnPassW3.Text = "*"; btnPassW4.Text = "*"; btnPassW5.Text = "*"; ; btnPassW6.Text = ""; break;
                    case 6: btnPassW1.Text = "*"; btnPassW2.Text = "*"; btnPassW3.Text = "*"; btnPassW4.Text = "*"; btnPassW5.Text = "*"; btnPassW6.Text = "*"; break;

                    default: btnPassW1.Text = ""; btnPassW2.Text = ""; btnPassW3.Text = ""; btnPassW4.Text = ""; btnPassW5.Text = ""; btnPassW6.Text = ""; break;
                }
            }
            else
            {
                switch (smscode.Length)
                {
                    case 0: btnPassW1.Text = ""; btnPassW2.Text = ""; btnPassW3.Text = ""; btnPassW4.Text = ""; btnPassW5.Text = ""; btnPassW6.Text = ""; break;
                    case 1: btnPassW1.Text = smscode.Substring(0, 1); btnPassW2.Text = ""; btnPassW3.Text = ""; btnPassW4.Text = ""; btnPassW5.Text = ""; btnPassW6.Text = ""; break;
                    case 2: btnPassW1.Text = smscode.Substring(0, 1); btnPassW2.Text = smscode.Substring(1, 1); btnPassW3.Text = ""; btnPassW4.Text = ""; btnPassW5.Text = ""; btnPassW6.Text = ""; break;
                    case 3: btnPassW1.Text = smscode.Substring(0, 1); btnPassW2.Text = smscode.Substring(1, 1); btnPassW3.Text = smscode.Substring(2, 1); ; btnPassW4.Text = ""; btnPassW5.Text = ""; btnPassW6.Text = ""; break;
                    case 4: btnPassW1.Text = smscode.Substring(0, 1); btnPassW2.Text = smscode.Substring(1, 1); btnPassW3.Text = smscode.Substring(2, 1); btnPassW4.Text = smscode.Substring(3, 1); btnPassW5.Text = ""; btnPassW6.Text = ""; break;
                    case 5: btnPassW1.Text = smscode.Substring(0, 1); btnPassW2.Text = smscode.Substring(1, 1); btnPassW3.Text = smscode.Substring(2, 1); btnPassW4.Text = smscode.Substring(3, 1); btnPassW5.Text = smscode.Substring(4, 1); ; btnPassW6.Text = ""; break;
                    case 6: btnPassW1.Text = smscode.Substring(0, 1); btnPassW2.Text = smscode.Substring(1, 1); btnPassW3.Text = smscode.Substring(2, 1); btnPassW4.Text = smscode.Substring(3, 1); btnPassW5.Text = smscode.Substring(4, 1); btnPassW6.Text = smscode.Substring(5, 1); break;

                    default: btnPassW1.Text = ""; btnPassW2.Text = ""; btnPassW3.Text = ""; btnPassW4.Text = ""; btnPassW5.Text = ""; btnPassW6.Text = ""; break;
                }
            }
            MemberCenterMediaHelper.UpdateForgetPassWordUI(numtype, smscode);
            this.Activate();
        }

        MemberCenterHttpUtil membercenterutil = new MemberCenterHttpUtil();
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (timerSeconds.Enabled || string.IsNullOrEmpty(MainModel.CurrentMember.memberid))
                {
                    return;
                }
                string err = "";
                LoadingHelper.ShowLoadingScreen();
                string smsCodeResult = membercenterutil.GetSendvalidateSmsCode(MainModel.CurrentMember.memberid, ref err);
                LoadingHelper.CloseForm();
                if (!string.IsNullOrEmpty(err))
                {
                    MainModel.ShowLog("发送验证码失败：" + err, true);
                    return;
                }
                timerSeconds.Tag = 60;
                timerSeconds.Enabled = true;
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("发送验证码异常" + ex.Message, true);
            }
        }
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
    }
}
