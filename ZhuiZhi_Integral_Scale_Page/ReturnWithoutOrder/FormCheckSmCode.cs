using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.ReturnWithoutOrder
{
    public partial class FormCheckSmCode : Form
    {
        public string CurrentPhone = "";
        public string smscode = "";
        public string CurrentSmsContent = "";
        /// <summary>
        /// 会员中心访问接口
        /// </summary>
        MemberCenterHttpUtil memberhttputil = new MemberCenterHttpUtil();

        private bool IsEnable =true;

        private ZhuiZhi_Integral_Scale_UncleFruit.Common.HttpUtil httputil = new Common.HttpUtil();
        public FormCheckSmCode(string name,string phone,string smscontent)
        {
            InitializeComponent();

            CurrentPhone = phone;
            CurrentSmsContent = smscontent;
            lblSendUser.Text = "验证码已发送至门店负责人（"+name+"）的手机";

            timerCountDown.Enabled = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 监听键盘输入
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {

            if (!IsEnable)
            {
                return true;
            }
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
            if(!IsEnable ){
                return;
            }

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
                    CheckSmsCode(smscode);
                }
            }

            UpdatePassWord();
        }

        private void CheckSmsCode(string smscode)
        {

            if (string.IsNullOrEmpty(smscode))
            {
                MainModel.ShowLog("验证码不能为空",false);
                return;
            }
            IsEnable = false;


            this.DialogResult = DialogResult.OK;
            this.Close();
            //string err = "";
            //int result = memberhttputil.ChangeNumberVerifysmscode(CurrentPhone,smscode, ref err);
            //if (result == 1)
            //{
            //    this.DialogResult = DialogResult.OK;
            //    this.Close();
            //}
            //else
            //{
            //    MainModel.ShowLog(err,false);
            //    this.DialogResult = DialogResult.Cancel;
            //    this.Close();
            //}


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

        private void FormCheckSmCode_Load(object sender, EventArgs e)
        {
            
        }

        int CurrentCountDown = 60;
        private void timerCountDown_Tick(object sender, EventArgs e)
        {

            CurrentCountDown--;

            btnCountDown.Text = "重新发送("+CurrentCountDown+")";

            if (CurrentCountDown <= 0)
            {
                btnCountDown.Text = "重新发送";
                btnCountDown.ForeColor = Color.White;
                btnCountDown.BackColor = Color.FromArgb(20, 137, 205);
            }
           
        }

        private void FormCheckSmCode_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            IsEnable =false;
            string errormsg="";
            string sendmsg = "";

            if (httputil.SendSmsCodeForReturnWithOutOrder(CurrentPhone, CurrentSmsContent, ref errormsg))
            {
                timerCountDown.Enabled=true;
            }
            IsEnable = true;
        }

        private void btnCountDown_Click(object sender, EventArgs e)
        {

            if (btnCountDown.Text == "重新发送")
            {

                btnCountDown.ForeColor = Color.Gray;
                btnCountDown.BackColor = Color.White;

                CurrentCountDown = 60;
                IsEnable = false;
                string errormsg = "";
                string sendmsg = "";

                if (httputil.SendSmsCodeForReturnWithOutOrder(CurrentPhone, CurrentSmsContent, ref errormsg))
                {
                    timerCountDown.Enabled = true;
                }
                else
                {
                    MainModel.ShowLog(errormsg, false);
                }
                IsEnable = true;
            }
          
        }

    }
}
