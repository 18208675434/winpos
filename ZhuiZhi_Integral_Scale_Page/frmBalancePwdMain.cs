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

namespace ZhuiZhi_Integral_Scale_UncleFruit
{
    public partial class frmBalancePwdMain : Form
    {
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord = "";
        /// <summary>
        /// 安全码
        /// </summary>
        public string Securitycode = "";

        public frmBalancePwdMain(bool saveInfo)
        {
            InitializeComponent();

            if (saveInfo)
            {
                this.tableLayoutPanel1.RowStyles[0] = new RowStyle(SizeType.Percent, 40);
                this.tableLayoutPanel1.RowStyles[1] = new RowStyle(SizeType.Percent, 60);
            }
            else
            {
                this.tableLayoutPanel1.RowStyles[0] = new RowStyle(SizeType.Percent, 0);
                this.tableLayoutPanel1.RowStyles[1] = new RowStyle(SizeType.Percent, 100);
            }
         
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timerLoadPwd_Tick(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                if (MainModel.BalanceClose)
                {

                    PassWord = MainModel.BalancePwd;
                    Securitycode = MainModel.BalanceSecuritycode;


                    //MainModel.BalanceClose = false;
                    MainModel.BalanceEnter = false;
                    MainModel.BalancePayPwd = "";
                    MainModel.BalancePwd = "";
                    MainModel.BalanceSecuritycode = "";
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }

                switch (MainModel.BalancePwd.Length)
                {
                    case 0: btnValue1.Text = ""; btnValue2.Text = ""; btnValue3.Text = ""; btnValue4.Text = ""; btnValue5.Text = ""; btnValue6.Text = ""; break;
                    case 1: btnValue1.Text = "*"; btnValue2.Text = ""; btnValue3.Text = ""; btnValue4.Text = ""; btnValue5.Text = ""; btnValue6.Text = ""; break;
                    case 2: btnValue1.Text = "*"; btnValue2.Text = "*"; btnValue3.Text = ""; btnValue4.Text = ""; btnValue5.Text = ""; btnValue6.Text = ""; break;
                    case 3: btnValue1.Text = "*"; btnValue2.Text = "*"; btnValue3.Text = "*"; ; btnValue4.Text = ""; btnValue5.Text = ""; btnValue6.Text = ""; break;
                    case 4: btnValue1.Text = "*"; btnValue2.Text = "*"; btnValue3.Text = "*"; btnValue4.Text = "*"; btnValue5.Text = ""; btnValue6.Text = ""; break;
                    case 5: btnValue1.Text = "*"; btnValue2.Text = "*"; btnValue3.Text = "*"; btnValue4.Text = "*"; btnValue5.Text = "*"; ; btnValue6.Text = ""; break;
                    case 6: btnValue1.Text = "*"; btnValue2.Text = "*"; btnValue3.Text = "*"; btnValue4.Text = "*"; btnValue5.Text = "*"; btnValue6.Text = "*"; break;

                    default: btnValue1.Text = ""; btnValue2.Text = ""; btnValue3.Text = ""; btnValue4.Text = ""; btnValue5.Text = ""; btnValue6.Text = ""; break;
                }
            }
            catch(Exception ex) 
            {
                LogManager.WriteLog("余额密码主界面更新异常"+ex.Message+ex.StackTrace);
            }
        }




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
                case Keys.Enter: MainModel.BalanceEnter = true ; return !base.ProcessDialogKey(keyData); break;
            }

            return base.ProcessDialogKey(keyData);

        }

        private void AddNum(int num, bool isDel)
        {
            if (isDel)
            {
                if (MainModel.BalancePwd.Length > 0)
                {
                    MainModel.BalancePwd = MainModel.BalancePwd.Substring(0,MainModel.BalancePwd.Length-1);
                }
            }
            else
            {
                if (MainModel.BalancePwd.Length < 6)
                {
                    MainModel.BalancePwd += num;
                }
            }
        }


        private void frmBalancePwdMain_Shown(object sender, EventArgs e)
        {
            MainModel.BalanceClose = false;
            MainModel.BalanceEnter = false;
            MainModel.BalancePayPwd = "";
            MainModel.BalancePwd = "";
            MainModel.BalanceSecuritycode = "";
            MainModel.BalancePwdErrorCode = -1;

            ZhuiZhi_Integral_Scale_UncleFruit.BaseUI.BaseUIHelper.ShowBalancePwd(true);
            timerLoadPwd.Interval = 150;
            timerLoadPwd.Enabled = true;
            Application.DoEvents();
            this.Activate();
        }

        private void frmBalancePwdMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                MainModel.BalanceEnter = false;
                //MainModel.BalanceClose = false;
                //MainModel.BalancePayPwd = "";
                MainModel.BalancePwd = "";
                MainModel.BalanceSecuritycode = "";
                ZhuiZhi_Integral_Scale_UncleFruit.BaseUI.BaseUIHelper.ShowBalancePwd(false);
                
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("余额密码主屏界面关闭异常"+ex.Message+ex.StackTrace);
            }
        }
    }
}
