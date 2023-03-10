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
        Member member = null;
        MemberCenterHttpUtil memberhttputil = new MemberCenterHttpUtil();

        public FormChangePhonePayPwd(Member member)
        {

            InitializeComponent();
            this.member = member;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            BackHelper.HideFormBackGround();

            this.Close();
            DialogResult = DialogResult.Cancel;
        }

        private void FormChangePhonePayPwd_Shown(object sender, EventArgs e)
        {
           
        }

        private void FormChangePhonePayPwd_FormClosing(object sender, FormClosingEventArgs e)
        {
            BackHelper.HideFormBackGround();
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
        private static AutoSizeFormUtil asf = new AutoSizeFormUtil();

        public void AddNum(int num, bool isDel)
        {

            try
            {
                if (isDel && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    UpdatePassWord(password);
                }
                else
                {
                    if (password.Length >= 6)
                    {
                        return;
                    }
                    password += num;
                    UpdatePassWord(password);
                    if (password.Length == 6)
                    {
                        string errorMsg = "";
                        int resultCode = 0;

                        this.Enabled = false;
                        LoadingHelper.ShowLoadingScreen("密码验证中...");
                        //RSA加密
                        PayPassWord = MainModel.RSAEncrypt(MainModel.RSAPrivateKey, password);
                        VerifyBalancePwd verifyresult = memberhttputil.VerifyBalancePwd(PayPassWord, ref errorMsg, ref resultCode, member);
                        if (errorMsg != "" || verifyresult == null)
                        {
                            this.Enabled = true;
                            MainModel.ShowLog("会员信息异常：" + errorMsg + "(" + resultCode + ")", true);
                            LoadingHelper.CloseForm();
                        }
                        else
                        {
                            MainModel.BalancePwdErrorCode = -1;
                            if (verifyresult.success == 1)
                            {
                                //校验成功
                                LoadingHelper.CloseForm();
                                this.DialogResult = DialogResult.OK;
                                this.Close();
                                BackHelper.HideFormBackGround();
                            }
                            else if (verifyresult.remainwrongcount > 0)
                            {
                                string showerrormsg = verifyresult.hint + verifyresult.wrongcount + "次，剩余" + verifyresult.remainwrongcount + "次";
                                MainModel.ShowLog(showerrormsg, false);
                                MemberCenterMediaHelper.ShowLog(showerrormsg);
                                LoadingHelper.CloseForm();
                                this.Close();
                                this.DialogResult = DialogResult.Cancel;
                                return;
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
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {            
                LoadingHelper.CloseForm();
            }
            

        }
        private void UpdatePassWord(string password)
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
            this.Activate();
        }
    }
}
