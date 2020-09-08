using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    public partial class FormChangePhonePayPwd : Form
    {

        /// <summary>
        /// 输入密码
        /// </summary>
        public string password = "";
        //使用密码支付  RSA公钥加密后的值
        public string PayPassWord = "";

        MemberCenterHttpUtil memberhttputil = new MemberCenterHttpUtil();
        public FormChangePhonePayPwd()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            DialogResult = DialogResult.Cancel;
        }

        private void FormChangePhonePayPwd_Shown(object sender, EventArgs e)
        {
            MemberCenterMediaHelper.ShowChangePhonePayPwd();
        }

        private void FormChangePhonePayPwd_FormClosing(object sender, FormClosingEventArgs e)
        {
            MemberCenterMediaHelper.ShowChangePhoneNumber();
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
                if (password.Length == 6)
                {
                    string ErrorMsg = "";
                    int ResultCode = 0;

                    this.Enabled = false;
                    LoadingHelper.ShowLoadingScreen("密码验证中...");

                    //RSA加密
                    PayPassWord = MainModel.RSAEncrypt(MainModel.RSAPrivateKey, password);
                    VerifyBalancePwd verifyresult = memberhttputil.VerifyBalancePwd(PayPassWord, ref ErrorMsg, ref ResultCode);
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
                            password = "";
                            MainModel.ChangePwd = "";
                            MainModel.ShowChangePhonePage = 1;
                            MainModel.ShowChangePhoneMedia = 1;
                            LoadingHelper.CloseForm();
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else if (verifyresult.remainwrongcount != null && verifyresult.remainwrongcount > 0)
                        {
                            MainModel.ChangePwd = "";
                            password = "";
                            UpdatePassWord();
                            string showerrormsg = verifyresult.hint + verifyresult.wrongcount + "次，剩余" + verifyresult.remainwrongcount + "次";
                            MainModel.ShowLog(showerrormsg, false);
                            ShowLog(showerrormsg, false);

                            LoadingHelper.CloseForm();
                            this.Close();
                            this.DialogResult = DialogResult.Cancel;
                        }
                        else
                        {
                            MainModel.ChangePwd = "";
                            password = "";
                            MainModel.ShowLog(verifyresult.hint, true);
                            ShowLog(verifyresult.hint, true);
                        }
                    }
                    this.Enabled = true;
                }
            }
            MainModel.ChangePwd = password;
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
