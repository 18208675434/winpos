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
    public partial class FormChengPhoneSmsCode : Form
    {
        /// <summary>
        /// 输入验证码
        /// </summary>
        public string smscode = "";
        /// <summary>
        /// 访问接口工厂类
        /// </summary>
        MemberCenterHttpUtil memberhttputil = new MemberCenterHttpUtil();
        public FormChengPhoneSmsCode()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FormChengPhoneSmsCode_Shown(object sender, EventArgs e)
        {
            MemberCenterMediaHelper.ShowChengPhoneSmsCode();
        }

        private void FormChengPhoneSmsCode_FormClosing(object sender, FormClosingEventArgs e)
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
                if (smscode.Length == 6)
                {
                    string err = "";
                    string result = memberhttputil.GetVerifysmscode(smscode, ref err);
                    
                    if (result == "success")
                    {
                        
                        MainModel.ShowChangePhonePage = 1;
                        MainModel.ShowChangePhoneMedia = 1;
                        this.DialogResult = DialogResult.OK;
                        BackHelper.HideFormBackGround();
                        this.Close();
                        
                    }
                    else
                    {
                        MainModel.ShowLog("验证码输入错误请重试", false);
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                    }
                }

            }
            MainModel.ChangeScd = smscode;
            UpdatePassWord();

        }
        private void UpdatePassWord()
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

        private void timerCountDown_Tick(object sender, EventArgs e)
        {
            try
            {
                int countdown = Convert.ToInt32(btnCountDown.Text.Substring(5, 2));
                countdown--;
                if (countdown < 10)
                {
                    btnCountDown.Enabled = false;
                    btnCountDown.Text = "重新发送(0" + countdown.ToString() + ")";
                }
                else
                {
                    btnCountDown.Enabled = false;
                    btnCountDown.Text = "重新发送(" + countdown.ToString() + ")";
                }
                if (countdown == 0)
                {
                    btnCountDown.Text = "重新发送";
                    timerCountDown.Enabled = false;
                    btnCountDown.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("异常" + ex.Message);
                throw;
            }
        }

        private void FormChengPhoneSmsCode_Load(object sender, EventArgs e)
        {
            //if (MainModel.tick != "")
            //{
            //    BackHelper.ShowFormBackGround();
            //    label1.Text = "找回密码";
            //}
            timerCountDown.Enabled = true;
        }

        private void btnCountDown_Click(object sender, EventArgs e)
        {
            string err = "";
            string resule = memberhttputil.GetSendvalidateSmsCode(MainModel.CurrentMember.memberid, ref err);
            if (resule == "success")
            {
                btnCountDown.Text = "重新发送(60)";
                timerCountDown.Enabled = true;
            }
        }
    }
}
