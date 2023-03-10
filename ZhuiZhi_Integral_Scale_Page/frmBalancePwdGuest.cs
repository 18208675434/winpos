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

namespace ZhuiZhi_Integral_Scale_UncleFruit
{
    public partial class frmBalancePwdGuest : Form
    {
        //当前输入密码
        private string PassWord = "";


        //使用密码支付  RSA公钥加密后的值
        public string PayPassWord = "";

        /// <summary>
        /// 后端接口访问类
        /// </summary>
        private HttpUtil httputil = new HttpUtil();
        public frmBalancePwdGuest()
        {
            InitializeComponent();
        }

        private void frmBalancePwdGuest_Shown(object sender, EventArgs e)
        {
            timerLoadPwd.Interval=150;
            timerLoadPwd.Enabled = true;
        }

        private void frmBalancePwdGuest_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerLoadPwd.Enabled = false;
        }


        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;


            string thisnum = btn.Name.Replace("btn", "");
            if (PassWord.Length + thisnum.Length > 6)
            {
                return;
            }
            else
            {
                PassWord += thisnum;

                MainModel.BalancePwd = PassWord;
                UpdatePassWord();
            }

        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (PassWord.Length > 0)
            {
                PassWord = PassWord.Substring(0, PassWord.Length - 1);
            }

            MainModel.BalancePwd = PassWord;
            UpdatePassWord();
        }

        private void UpdatePassWord()
        {
            if (PassWord.Length == 6)
            {
                btnOK.BackColor = Color.OrangeRed;
            }else
            {
                btnOK.BackColor = Color.Silver;
            }

            switch (PassWord.Length)
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


        protected override bool ProcessDialogKey(Keys keyData)
        {

            //*获取按键后使之失效，防止有焦点事件获取按键信息   !base.ProcessDialogKey(keyData)
            switch (keyData)
            {
                case Keys.D0: btn0.PerformClick(); return !base.ProcessDialogKey(keyData); break;
                case Keys.D1: btn1.PerformClick(); return !base.ProcessDialogKey(keyData); break;
                case Keys.D2: btn2.PerformClick(); return !base.ProcessDialogKey(keyData); break;
                case Keys.D3: btn3.PerformClick(); return !base.ProcessDialogKey(keyData); break;
                case Keys.D4: btn4.PerformClick(); return !base.ProcessDialogKey(keyData); break;
                case Keys.D5: btn5.PerformClick(); return !base.ProcessDialogKey(keyData); break;
                case Keys.D6: btn6.PerformClick(); return !base.ProcessDialogKey(keyData); break;
                case Keys.D7: btn7.PerformClick(); return !base.ProcessDialogKey(keyData); break;
                case Keys.D8: btn8.PerformClick(); return !base.ProcessDialogKey(keyData); break;
                case Keys.D9: btn9.PerformClick(); return !base.ProcessDialogKey(keyData); break;

                case Keys.NumPad0: btn0.PerformClick(); return !base.ProcessDialogKey(keyData); break;
                case Keys.NumPad1: btn1.PerformClick(); return !base.ProcessDialogKey(keyData); break;
                case Keys.NumPad2: btn2.PerformClick(); return !base.ProcessDialogKey(keyData); break;
                case Keys.NumPad3: btn3.PerformClick(); return !base.ProcessDialogKey(keyData); break;
                case Keys.NumPad4: btn4.PerformClick(); return !base.ProcessDialogKey(keyData); break;
                case Keys.NumPad5: btn5.PerformClick(); return !base.ProcessDialogKey(keyData); break;
                case Keys.NumPad6: btn6.PerformClick(); return !base.ProcessDialogKey(keyData); break;
                case Keys.NumPad7: btn7.PerformClick(); return !base.ProcessDialogKey(keyData); break;
                case Keys.NumPad8: btn8.PerformClick(); return !base.ProcessDialogKey(keyData); break;
                case Keys.NumPad9: btn9.PerformClick(); return !base.ProcessDialogKey(keyData); break;

                case Keys.Back: btnDel.PerformClick(); return base.ProcessDialogKey(keyData); break;
                case Keys.Enter: btnOK_Click(null,null); return !base.ProcessDialogKey(keyData); break;
            }

            return base.ProcessDialogKey(keyData);

        }
        

        private void btnOK_Click(object sender, EventArgs e)
        {

            MainModel.BalanceEnter = true;
            //try
            //{
            //    if (PassWord.Length == 0)
            //    {
            //        MainModel.ShowLog("请输入密码", false);
            //        ShowLog("请输入密码", false);
            //        return;
            //    }

            //    if (PassWord.Length != 6)
            //    {
            //        MainModel.ShowLog("密码位数不够", false);
            //        ShowLog("密码位数不够", false);
            //        return;
            //    }
            //    this.Enabled = false;

            //    LoadingHelper.ShowLoadingScreen("密码验证中...");


            //    PayPassWord = MainModel.RSAEncrypt(MainModel.RSAPrivateKey, PassWord);


            //    string ErrorMsg = "";
            //    int ResultCode = 0;
            //    VerifyBalancePwd verifyresult = httputil.VerifyBalancePwd(PayPassWord, ref ErrorMsg, ref ResultCode);

            //    if (ErrorMsg != "" || verifyresult == null)
            //    {
            //        LoadingHelper.CloseForm();
            //        this.Enabled = true;

            //        CheckUserAndMember(ResultCode,ErrorMsg);
            //        // if(ResultCode==)
                   
                   
            //        //MainModel.ShowLog(ErrorMsg, true);
            //        //ShowLog(ErrorMsg, true);
            //    }
            //    else
            //    {
            //        MainModel.BalancePwdErrorCode = -1;
            //        if (verifyresult.success == 1)
            //        {

            //            MainModel.BalancePayPwd = PayPassWord;
            //            MainModel.BalanceClose = true;
            //            MainModel.BalanceSecuritycode = verifyresult.securitycode;
            //            this.DialogResult = DialogResult.OK;
            //            this.Close();
            //        }
            //        else if (verifyresult.remainwrongcount != null && verifyresult.remainwrongcount > 0)
            //        {
            //            MainModel.BalancePwd = "";
            //            PassWord = "";
            //            UpdatePassWord();
            //            string showerrormsg = verifyresult.hint + verifyresult.wrongcount + "次，剩余" + verifyresult.remainwrongcount + "次";
            //            MainModel.ShowLog(showerrormsg, false);
            //            ShowLog(showerrormsg, false);
            //        }
            //        else
            //        {
            //            MainModel.BalancePwd = "";
            //            PassWord = "";
            //            MainModel.ShowLog(verifyresult.hint, true);
            //            ShowLog(verifyresult.hint, true);
            //        }
            //    }

            //    this.Enabled = true;
            //}
            //catch (Exception ex)
            //{
            //    MainModel.ShowLog("验证余额密码异常"+ex.Message,true);
            //}
            //finally
            //{
            //    this.Enabled = true;
            //    LoadingHelper.CloseForm();
            //    //this.Activate();
            //}

        }

        private void timerLoadPwd_Tick(object sender, EventArgs e)
        {
            try
            {
                PassWord = MainModel.BalancePwd;
                UpdatePassWord();

                ////回车按键使用后失效
                //if (MainModel.BalanceEnter)
                //{
                //    MainModel.BalanceEnter = false;
                //    btnOK.PerformClick();
                //}
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("余额密码客屏界面更新异常" + ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();
        public void ShowLog(string msg, bool iserror)
        {
            try
            {
                
                //MsgHelper.AutoShowForm(msg);
                    this.BeginInvoke(new InvokeHandler(delegate()
                    {
                        lblToast.Text = msg;
                        lblToast.Visible = true;
                        Delay.Start(1000);
                        lblToast.Visible = false;

                        //this.Activate();
                    }));
                    
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message);
            }

        }

    }
}
