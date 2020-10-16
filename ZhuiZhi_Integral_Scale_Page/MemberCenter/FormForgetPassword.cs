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
        //输入验证码次数
        public int inputtimes = 0;
        //旧密码
        public string oldpassword = "";
        //存储新密码
        public string NowNewPassWord = "";
        //存储验证码
        public string ServeSmscode = "";

        MemberCenterHttpUtil memberhttputil = new MemberCenterHttpUtil();

        public FormForgetPassword()
        {
            InitializeComponent();         
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MainModel.SmsCode = "";
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FormForgetPassword_Shown(object sender, EventArgs e)
        {
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

            if (isDel)
            {
                if (smscode.Length > 0)
                {
                    smscode = smscode.Substring(0, smscode.Length - 1);
                }
            }
            else
            {
                if (smscode.Length < 6)
                {
                    smscode += num;
                }
                if (smscode.Length == 6 && inputtimes == 0)
                {
                    ServeSmscode = smscode;
                    string err = "";
                    string result = memberhttputil.GetVerifysmscode(smscode, ref err);
                    if (result == "success")
                    {
                        smscode = "";
                        inputtimes = 2;
                        MainModel.inputimes = 2;
                        label2.Text = "请输入新的支付密码";
                        btnSend.Visible = false;
                    }
                    else
                    {
                        MainModel.ShowLog("短信验证码错误", false);
                        ShowLog("短信验证码错误", false);
                        this.Close();
                    }
                }
                if (smscode.Length == 6 && inputtimes == 1)
                {
                    if (smscode == NowNewPassWord)
                    {
                        string ErroM = "";
                        string newpassword = smscode;
                        //验证密码类型
                        int resuleCode = 0;
                        //验证成功
                        string result = memberhttputil.ForgetSetPassWord(MainModel.RSAEncrypt(MainModel.RSAPrivateKey, newpassword), 1, ServeSmscode, ref ErroM, ref resuleCode);
                        smscode = "";
                        MainModel.SmsCode = "";
                        if (result == "true")
                        {
                            inputtimes = 0;
                            MainModel.inputimes = 0;
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                    if (smscode != NowNewPassWord)
                    {
                        label2.Text = "两次输入密码不一致，请重新输入";
                        smscode = "";
                        inputtimes = 2;
                        MainModel.inputimes = 2;
                        UpdatePassWord(inputtimes);
                    }

                }
                if (smscode.Length == 6 && inputtimes == 2)
                {
                    //存储到第一次输入的新密码
                    NowNewPassWord = smscode;
                    label2.Text = "请等待用户确认密码";
                    smscode = "";
                    inputtimes = 1;
                    MainModel.inputimes = inputtimes;
                    UpdatePassWord(inputtimes);
                }
            }
            MainModel.SmsCode = smscode;
            UpdatePassWord(inputtimes);

        }
        private void UpdatePassWord(int inputtimes)
        {
            if (inputtimes != 0)
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

        }
        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();
        private void ShowLog(string msg, bool iserror)
        {
            try
            {

                //MsgHelper.AutoShowForm(msg);
                this.BeginInvoke(new InvokeHandler(delegate()
                {                   
                    Delay.Start(1000);
                    this.Activate();
                }));

            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message);
            }

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
