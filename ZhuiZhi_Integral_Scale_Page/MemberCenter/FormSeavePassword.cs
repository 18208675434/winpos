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
    public partial class FormSeavePassword : Form
    {
        /// <summary>
        /// 输入密码
        /// </summary>
        public string password = "";

        // 0-原密码录入 1-新密码录入 2-新密码确认
        public int numtype = 0;
        //旧密码
        public string oldpassword = "";
        //存储新密码
        public string nowNewPassWord = "";
        Member member;

        MemberCenterHttpUtil mcHttpUtil = new MemberCenterHttpUtil();

        public FormSeavePassword(Member m)
        {
            InitializeComponent();
            member = m;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FormSeavePassword_Shown(object sender, EventArgs e)
        {
            MemberCenterMediaHelper.ShowServrPassWord();
        }

        private void FormSeavePassword_FormClosing(object sender, FormClosingEventArgs e)
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
                case Keys.D0: AddNum(0, false); return !base.ProcessDialogKey(keyData);
                case Keys.D1: AddNum(1, false); return !base.ProcessDialogKey(keyData);
                case Keys.D2: AddNum(2, false); return !base.ProcessDialogKey(keyData);
                case Keys.D3: AddNum(3, false); return !base.ProcessDialogKey(keyData);
                case Keys.D4: AddNum(4, false); return !base.ProcessDialogKey(keyData);
                case Keys.D5: AddNum(5, false); return !base.ProcessDialogKey(keyData);
                case Keys.D6: AddNum(6, false); return !base.ProcessDialogKey(keyData);
                case Keys.D7: AddNum(7, false); return !base.ProcessDialogKey(keyData);
                case Keys.D8: AddNum(8, false); return !base.ProcessDialogKey(keyData);
                case Keys.D9: AddNum(9, false); return !base.ProcessDialogKey(keyData);

                case Keys.NumPad0: AddNum(0, false); return !base.ProcessDialogKey(keyData);
                case Keys.NumPad1: AddNum(1, false); return !base.ProcessDialogKey(keyData);
                case Keys.NumPad2: AddNum(2, false); return !base.ProcessDialogKey(keyData);
                case Keys.NumPad3: AddNum(3, false); return !base.ProcessDialogKey(keyData);
                case Keys.NumPad4: AddNum(4, false); return !base.ProcessDialogKey(keyData);
                case Keys.NumPad5: AddNum(5, false); return !base.ProcessDialogKey(keyData);
                case Keys.NumPad6: AddNum(6, false); return !base.ProcessDialogKey(keyData);
                case Keys.NumPad7: AddNum(7, false); return !base.ProcessDialogKey(keyData);
                case Keys.NumPad8: AddNum(8, false); return !base.ProcessDialogKey(keyData);
                case Keys.NumPad9: AddNum(9, false); return !base.ProcessDialogKey(keyData);

                case Keys.Back: AddNum(0, true); return base.ProcessDialogKey(keyData);
                case Keys.Enter: return !base.ProcessDialogKey(keyData);
            }

            return base.ProcessDialogKey(keyData);

        }

        private void AddNum(int num, bool isDel)
        {

            if (isDel && password.Length > 0)
            {
                password = password.Substring(0, password.Length - 1);
                UpdatePassWord();
            }
            else
            {
                if (password.Length > 6)
                {
                    return;
                }
                password += num;
                UpdatePassWord();
                if (password.Length == 6 && numtype == 0)
                {
                    try
                    {
                        this.Enabled = false;
                        LoadingHelper.ShowLoadingScreen("密码验证中...");
                        //使用密码支付  RSA公钥加密后的值
                        string payPassWord = MainModel.RSAEncrypt(MainModel.RSAPrivateKey, password);
                        string errorMsg = "";
                        int resultCode = 0;
                        VerifyBalancePwd verifyresult = mcHttpUtil.VerifyBalancePwd(payPassWord, ref errorMsg, ref resultCode, member);
                        if (errorMsg != "" || verifyresult == null)
                        {
                            this.Enabled = true;
                            LoadingHelper.CloseForm();
                            MainModel.ShowLog("会员信息异常：" + errorMsg + "(" + resultCode + ")", true);
                            MemberCenterMediaHelper.ShowLog("会员信息异常：" + errorMsg+"("+resultCode+")");
                        }
                        else
                        {
                            MainModel.BalancePwdErrorCode = -1;
                            if (verifyresult.success == 1)
                            {
                                oldpassword = password;
                                numtype = 1;
                                password = "";
                                UpdatePassWord();
                            }
                            else if (verifyresult.remainwrongcount > 0)
                            {
                                password = "";
                                string showerrormsg = verifyresult.hint + verifyresult.wrongcount + "次，剩余" + verifyresult.remainwrongcount + "次";
                                MainModel.ShowLog(showerrormsg, false);
                                MemberCenterMediaHelper.ShowLog(showerrormsg);
                                this.DialogResult = DialogResult.Cancel;
                                this.Close();
                            }
                            else
                            {
                                password = "";
                                MainModel.ShowLog(verifyresult.hint, true);
                                MemberCenterMediaHelper.ShowLog(verifyresult.hint);
                            }
                        }
                        this.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        MainModel.ShowLog("验证余额支付密码异常" + ex.Message, true);
                        MemberCenterMediaHelper.ShowLog("验证余额支付密码异常" + ex);
                    }
                    finally
                    {
                        this.Enabled = true;
                        LoadingHelper.CloseForm();
                        this.Activate();
                    }
                }
                if (password.Length == 6 && numtype == 1)
                {
                    //存储到第一次输入的新密码
                    nowNewPassWord = password;
                    password = "";
                    numtype = 2;
                    UpdatePassWord();
                }
                if (password.Length == 6 && numtype == 2)
                {
                    if (password != nowNewPassWord)
                    {
                        MainModel.ShowLog("两次输入密码不一致，请重新输入", false);
                        MemberCenterMediaHelper.ShowLog("两次输入密码不一致，请重新输入");
                        password = "";
                        numtype = 1;
                        UpdatePassWord();
                    }
                    else if (password == nowNewPassWord)
                    {
                        string ErroM = "";
                        string newpassword = password;
                        //验证密码类型
                        int resuleCode = 0;
                        //验证成功
                        string result = mcHttpUtil.UpdatePassWord(MainModel.RSAEncrypt(MainModel.RSAPrivateKey, newpassword), MainModel.RSAEncrypt(MainModel.RSAPrivateKey, oldpassword), 2, ref ErroM, ref resuleCode);
                        password = "";
                        if (result == "true")
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }

                }
            }
        }

        private void UpdatePassWord()
        {
            if (numtype == 0)
            {
                lblTipsText.Text = "请等待用户输入原支付密码";
            }
            else if (numtype == 1)
            {
                lblTipsText.Text = "请等待用户输入支付密码";
            }
            else if (numtype == 2)
            {
                lblTipsText.Text = "请等待用户确认密码";
            }
            switch (password.Length)
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
            MemberCenterMediaHelper.UpdatePassWordUpdateUI(numtype, password);
            this.Activate();
        }
    }
}
