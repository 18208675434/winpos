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

        //使用密码支付  RSA公钥加密后的值
        public string PayPassWord = "";
        //输入密码次数
        public int inputtimes = 0;
        //旧密码
        public string oldpassword = "";
        //存储新密码
        public string NowNewPassWord = "";
        Member member;

        MemberCenterHttpUtil McHttpUtil = new MemberCenterHttpUtil();

        public FormSeavePassword(Member m)
        {
            InitializeComponent();
            member = m;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MainModel.SevaePwd = "";
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
                if (password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                }
            }
            else
            {
                if (password.Length < 6)
                {
                    password += num;
                }
                if (password.Length == 6 && inputtimes == 0)
                {

                    try
                    {
                        this.Enabled = false;

                        LoadingHelper.ShowLoadingScreen("密码验证中...");

                        //RSA加密
                        PayPassWord = MainModel.RSAEncrypt(MainModel.RSAPrivateKey, password);

                        string ErrorMsg = "";
                        int ResultCode = 0;
                        VerifyBalancePwd verifyresult = McHttpUtil.VerifyBalancePwd(PayPassWord, ref ErrorMsg, ref ResultCode,member);

                        if (ErrorMsg != "" || verifyresult == null)
                        {

                            this.Enabled = true;
                            LoadingHelper.CloseForm();
                            CheckUserAndMember(ResultCode, ErrorMsg);
                        }
                        else
                        {
                            MainModel.BalancePwdErrorCode = -1;
                            if (verifyresult.success == 1)
                            {
                                //校验成功
                                lblTipsText.Text = "请等待用户输入支付密码";
                                oldpassword = password;
                                inputtimes = 2;
                                password = "";
                                UpdatePassWord();
                            }
                            else if (verifyresult.remainwrongcount != null && verifyresult.remainwrongcount > 0)
                            {
                                MainModel.SevaePwd = "";
                                password = "";
                                string showerrormsg = verifyresult.hint + verifyresult.wrongcount + "次，剩余" + verifyresult.remainwrongcount + "次";
                                MainModel.ShowLog(showerrormsg, false);
                                ShowLog(showerrormsg, false);
                                this.DialogResult = DialogResult.Cancel;
                                this.Close();
                            }
                            else
                            {
                                MainModel.SevaePwd = "";
                                password = "";
                                MainModel.ShowLog(verifyresult.hint, true);
                                ShowLog(verifyresult.hint, true);
                            }
                        }
                        this.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        MainModel.ShowLog("验证余额密码异常" + ex.Message, true);
                    }
                    finally
                    {
                        this.Enabled = true;
                        LoadingHelper.CloseForm();
                        this.Activate();
                    }
                }
                if (password.Length == 6 && inputtimes == 1)
                {
                    if (password == NowNewPassWord)
                    {
                        string ErroM = "";
                        string newpassword = password;
                        //验证密码类型
                        int resuleCode = 0;
                        //验证成功
                        string result = McHttpUtil.UpdatePassWord(MainModel.RSAEncrypt(MainModel.RSAPrivateKey, newpassword), MainModel.RSAEncrypt(MainModel.RSAPrivateKey, oldpassword), 2, ref ErroM, ref resuleCode);
                        password = "";
                        MainModel.SevaePwd = "";
                        if (result == "true")
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                    if (password != NowNewPassWord)
                    {
                        lblTipsText.Text = "两次输入密码不一致，请重新输入";
                        password = "";
                        inputtimes = 2;
                        UpdatePassWord();
                    }

                }
                if (password.Length == 6 && inputtimes == 2)
                {
                    //存储到第一次输入的新密码
                    NowNewPassWord = password;
                    lblTipsText.Text = "请等待用户确认密码";
                    password = "";
                    inputtimes = 1;
                    UpdatePassWord();
                }
            }
            MainModel.SevaePwd = password;
            UpdatePassWord();
        }

        private void UpdatePassWord()
        {
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
        }

        /// <summary>
        /// 验证支付密码是否正确
        /// </summary>
        /// <param name="resultcode"></param>
        /// <param name="ErrorMsg"></param>
        private void CheckUserAndMember(int resultcode, string ErrorMsg)
        {
            try
            {

                if (resultcode == MainModel.HttpUserExpired || resultcode == MainModel.HttpMemberExpired || resultcode == MainModel.DifferentMember)
                {

                    this.Enabled = false;
                    MainModel.CurrentMember = null;
                    MainModel.BalancePwdErrorCode = resultcode;

                }
                else
                {
                    MainModel.ShowLog(ErrorMsg, true);
                    ShowLog(ErrorMsg, true);
                }
            }
            catch (Exception ex)
            {

                this.Enabled = true;

                MainModel.ShowLog("密码验证错误码异常", true);

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
    }
}
