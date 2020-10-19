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
    /// <summary>
    /// 新手机号验证码验证
    /// </summary>
    public partial class FormChangePhoneNewPhoneSms : Form
    {
        string newphone="";
        /// <summary>
        /// 输入验证码
        /// </summary>
        public string smscode = "";
        /// <summary>
        /// 访问接口工厂类
        /// </summary>
        MemberCenterHttpUtil memberhttputil = new MemberCenterHttpUtil();
        public FormChangePhoneNewPhoneSms(string newphone)
        {
            InitializeComponent();
            this.newphone = newphone;
        }

        private void FormChangePhoneNewPhoneSms_Shown(object sender, EventArgs e)
        {
            int result = GetNewPhoneSmscode();
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

        private void FormChangePhoneNewPhoneSms_Load(object sender, EventArgs e)
        {
            timerCountDown.Enabled = true;
        }
        private int GetNewPhoneSmscode()
        {
            string err = "";
            return memberhttputil.ChangeNumberGetSendsmscode(newphone, ref err);
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

                case Keys.Delete: AddNum(0, true); return base.ProcessDialogKey(keyData);
                case Keys.Back: AddNum(0, true); return base.ProcessDialogKey(keyData);
                case Keys.Enter: return !base.ProcessDialogKey(keyData);
            }

            return base.ProcessDialogKey(keyData);
        }

        private void AddNum(int num, bool isDel)
        {

            if (isDel && smscode.Length > 0)
            {
                smscode = smscode.Substring(0, smscode.Length - 1);
                UpdatePassWord();
            }
            else
            {
                if (smscode.Length >= 6)
                {
                    return;
                }
                smscode += num;
                UpdatePassWord();
                if (smscode.Length == 6)
                {
                    string err = "";
                    int result = memberhttputil.ChangeNumberVerifysmscode(newphone,smscode, ref err);
                    if (result == 1)
                    {
                        MainModel.NewPhone = newphone;
                        MainModel.ShowChangePhonePage = 2;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MainModel.ShowLog("验证码验证错误,请重试", false);
                        this.Close();
                    }
                }
            }

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

        private void btnCountDown_Click(object sender, EventArgs e)
        {
            int resule = GetNewPhoneSmscode();
            if (resule == 1)
            {
                btnCountDown.Text = "重新发送(60)";
                timerCountDown.Enabled = true;
            }
        }

        private void linkLabel1_MouseClick(object sender, MouseEventArgs e)
        {
            smscode = "";
            this.Close();
        }
    }
}
