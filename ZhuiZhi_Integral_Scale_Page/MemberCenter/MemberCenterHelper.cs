using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.BaseUI;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;
using ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.Model.HalfOffLine;
using ZhuiZhi_Integral_Scale_UncleFruit.PayUI;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    public class MemberCenterHelper
    {
        //<summary>
        //按比例缩放页面及控件
        //</summary>
        private static AutoSizeFormUtil asf = new AutoSizeFormUtil();

        public static void ShowFormMemberCenter(Member member)
        {
            try
            {
                FormMemberCenter frmcenter = new FormMemberCenter(member);

                asf.AutoScaleControlTest(frmcenter, 1180, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                frmcenter.Location = new System.Drawing.Point(0, 0);

                frmcenter.ShowDialog();
                frmcenter.Dispose();
                Application.DoEvents();

            }
            catch (Exception ex)
            {
                MainModel.ShowLog("显示会员中心页面异常" + ex.Message, true);
            }
        }


        public static bool ShowFormTopUpByCash(decimal amount)
        {
            try
            {
                BackHelper.ShowFormBackGround();

                FormTopUpByCash frmpaybycash = new FormTopUpByCash(amount);
                asf.AutoScaleControlTest(frmpaybycash, 380, 520, 380 * MainModel.midScale, 520 * MainModel.midScale, true);
                frmpaybycash.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmpaybycash.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmpaybycash.Height) / 2);
                frmpaybycash.TopMost = true;
                frmpaybycash.ShowDialog();
                frmpaybycash.Dispose();


                BackHelper.HideFormBackGround();
                Application.DoEvents();
                return frmpaybycash.DialogResult == DialogResult.OK;

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("余额现金充值异常" + ex.Message);
                return false;
            }
        }

        public static bool ShowFormTopUpByOnline(long orderid, string mobile)
        {
            try
            {
                BackHelper.ShowFormBackGround();

                FormTopUpByOnLine frmpaybycash = new FormTopUpByOnLine(orderid.ToString(), mobile);
                asf.AutoScaleControlTest(frmpaybycash, 380, 520, 380 * MainModel.midScale, 520 * MainModel.midScale, true);
                frmpaybycash.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmpaybycash.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmpaybycash.Height) / 2);
                frmpaybycash.TopMost = true;
                frmpaybycash.ShowDialog();
                frmpaybycash.Dispose();

                BackHelper.HideFormBackGround();
                Application.DoEvents();
                return frmpaybycash.DialogResult == DialogResult.OK;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("余额现金充值异常" + ex.Message);
                return false;
            }
        }



        public static void ShowFormAllCoupon(List<PromotionCoupon> LstCoupon)
        {
            try
            {
                BackHelper.ShowFormBackGround();

                FormAllCoupon frmcoupon = new FormAllCoupon(LstCoupon);
                asf.AutoScaleControlTest(frmcoupon, 380, 480, 380 * MainModel.midScale, 480 * MainModel.midScale, true);
                frmcoupon.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmcoupon.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmcoupon.Height) / 2);
                frmcoupon.TopMost = true;
                frmcoupon.ShowDialog();
                BackHelper.HideFormBackGround();

                frmcoupon.Dispose();
                Application.DoEvents();

            }
            catch (Exception ex)
            {
                BackHelper.HideFormBackGround();
                LogManager.WriteLog("显示会员优惠券列表异常" + ex.Message);
            }
        }


        public static void ShowFormTopUPQuery()
        {
            try
            {

                FormTopUpQuery frmorderquery = new FormTopUpQuery();
                asf.AutoScaleControlTest(frmorderquery, 1178, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                frmorderquery.Location = new System.Drawing.Point(0, 0);
                frmorderquery.ShowDialog();
                frmorderquery.Dispose();
                Application.DoEvents();

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("显示会员充值列表异常" + ex.Message);
            }

        }


        public static bool ShowFormNoPayPwd()
        {
            try
            {
                BackHelper.ShowFormBackGround();

                FormNoPayPwd frmconfirm = new FormNoPayPwd();
                asf.AutoScaleControlTest(frmconfirm, 600, 200, 600 * MainModel.midScale, 200 * MainModel.midScale, true);
                frmconfirm.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmconfirm.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmconfirm.Height) / 2);
                frmconfirm.TopMost = true;

                frmconfirm.ShowDialog();
                Application.DoEvents();
                BackHelper.HideFormBackGround();
                return frmconfirm.DialogResult == DialogResult.OK;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 显示用户修改密码输入窗口
        /// </summary>
        /// <returns></returns>
        public static bool ShowFormSeavePassword(Member mm)
        {
            try
            {
                BackHelper.ShowFormBackGround();

                FormSeavePassword frmseavepassword = new FormSeavePassword(mm);
                asf.AutoScaleControlTest(frmseavepassword, 370, 200, 370 * MainModel.midScale, 200 * MainModel.midScale, true);
                frmseavepassword.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmseavepassword.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmseavepassword.Height) / 2);
                frmseavepassword.TopMost = true;

                frmseavepassword.ShowDialog();
                if (frmseavepassword.DialogResult == DialogResult.OK)
                {
                    ShowFormChangePassWordOK();
                }
                Application.DoEvents();
                BackHelper.HideFormBackGround();
                return frmseavepassword.DialogResult == DialogResult.OK;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        /// <summary>
        /// 显示用户找回密码输入验证码窗口
        /// </summary>
        /// <returns></returns>
        public static bool ShowFormForgetPassword()
        {
            try
            {
                BackHelper.ShowFormBackGround();

                FormForgetPassword frmforgetpassword = new FormForgetPassword();
                asf.AutoScaleControlTest(frmforgetpassword, 370, 200, 370 * MainModel.midScale, 200 * MainModel.midScale, true);
                frmforgetpassword.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmforgetpassword.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmforgetpassword.Height) / 2);
                frmforgetpassword.TopMost = true;

                frmforgetpassword.ShowDialog();
                if (frmforgetpassword.DialogResult == DialogResult.OK)
                {
                    ShowFormChangePassWordOK();
                }
                Application.DoEvents();
                BackHelper.HideFormBackGround();
                return frmforgetpassword.DialogResult == DialogResult.OK;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        /// <summary>
        /// 显示用户找修改成功窗口
        /// </summary>
        /// <returns></returns>
        public static bool ShowFormChangePassWordOK()
        {
            try
            {
                BackHelper.ShowFormBackGround();

                FormSevePassWoedOK frmchangepasswordok = new FormSevePassWoedOK();
                asf.AutoScaleControlTest(frmchangepasswordok, 600, 200, 600 * MainModel.midScale, 200 * MainModel.midScale, true);
                frmchangepasswordok.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmchangepasswordok.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmchangepasswordok.Height) / 2);
                frmchangepasswordok.TopMost = true;

                frmchangepasswordok.ShowDialog();
                Application.DoEvents();
                BackHelper.HideFormBackGround();
                return frmchangepasswordok.DialogResult == DialogResult.OK;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        /// <summary>
        /// 显示修改手机号码界面
        /// </summary>
        public static void ShowFormChangePhoneNumber(Member member)
        {
            try
            {

                FormChangePhoneNumber frmchangephonenumber = new FormChangePhoneNumber(member);
                asf.AutoScaleControlTest(frmchangephonenumber, 1180, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                frmchangephonenumber.Location = new System.Drawing.Point(0, 0);
                frmchangephonenumber.TopMost = true;
                frmchangephonenumber.ShowDialog();
                frmchangephonenumber.Dispose();
                Application.DoEvents();

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("显示修改手机号码界面异常" + ex.Message);
            }

        }
        /// <summary>
        /// 显示修改手机号码-验证码验证窗口
        /// </summary>
        /// <returns></returns>
        public static bool ShowFormChengPhoneSmsCode()
        {
            try
            {
                BackHelper.ShowFormBackGround();
                FormChengPhoneSmsCode frmchangephonesmscode = new FormChengPhoneSmsCode();
                asf.AutoScaleControlTest(frmchangephonesmscode, 370, 200, 370 * MainModel.midScale, 200 * MainModel.midScale, true);
                frmchangephonesmscode.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmchangephonesmscode.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmchangephonesmscode.Height) / 2);
                frmchangephonesmscode.TopMost = true;

                frmchangephonesmscode.ShowDialog();

                Application.DoEvents();
                BackHelper.HideFormBackGround();
                return frmchangephonesmscode.DialogResult == DialogResult.OK;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        /// <summary>
        /// 显示修改手机号码-支付密码验证窗口
        /// </summary>
        /// <returns></returns>
        public static bool ShowFormChangePhonePayPwd(Member m )
        {
            try
            {
                BackHelper.ShowFormBackGround();
                FormChangePhonePayPwd frmchangephonepaypwd = new FormChangePhonePayPwd(m);
                asf.AutoScaleControlTest(frmchangephonepaypwd, 370, 200, 370 * MainModel.midScale, 200 * MainModel.midScale, true);
                frmchangephonepaypwd.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmchangephonepaypwd.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmchangephonepaypwd.Height) / 2);
                frmchangephonepaypwd.TopMost = true;

                frmchangephonepaypwd.ShowDialog();

                Application.DoEvents();
                BackHelper.HideFormBackGround();
                return frmchangephonepaypwd.DialogResult == DialogResult.OK;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        /// <summary>
        /// 显示修改手机号码-旧卡验证窗口
        /// </summary>
        /// <returns></returns>
        public static bool ShowFormChangePhonePhysicalCard()
        {
            try
            {
               
                BackHelper.ShowFormBackGround();

                FormChangePhonePhysicalCard frmchangephoneoldcard = new FormChangePhonePhysicalCard();
                asf.AutoScaleControlTest(frmchangephoneoldcard, 380, 197, 380 * MainModel.midScale, 197 * MainModel.midScale, true);
                frmchangephoneoldcard.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmchangephoneoldcard.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmchangephoneoldcard.Height) / 2);
                frmchangephoneoldcard.TopMost = true;

                frmchangephoneoldcard.ShowDialog();

                Application.DoEvents();
                BackHelper.HideFormBackGround();
                return frmchangephoneoldcard.DialogResult == DialogResult.OK;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        /// <summary>
        /// 显示修改手机号码-新卡验证窗口
        /// </summary>
        /// <returns></returns>
        public static bool ShowFormChangePhoneNewCard()
        {
            try
            {
                BackHelper.ShowFormBackGround();

                FormChangePhoneNewCard frmchangephonenewcard = new FormChangePhoneNewCard();
                asf.AutoScaleControlTest(frmchangephonenewcard, 380, 450, 380 * MainModel.midScale, 450 * MainModel.midScale, true);
                frmchangephonenewcard.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmchangephonenewcard.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmchangephonenewcard.Height) / 2);
                frmchangephonenewcard.TopMost = true;
                frmchangephonenewcard.ShowDialog();

                frmchangephonenewcard.Dispose();


                BackHelper.HideFormBackGround();
                Application.DoEvents();
                return frmchangephonenewcard.DialogResult == DialogResult.OK;

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("更换手机号码新卡验证异常" + ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 显示修改手机号码-新手机获取验证码窗口
        /// </summary>
        /// <returns></returns>
        public static bool ShowFormChangePhoneNewPhoneSms()
        {
            try
            {
                BackHelper.ShowFormBackGround();

                FormChangePhoneNewPhoneSms frmforgetpassword = new FormChangePhoneNewPhoneSms();
                asf.AutoScaleControlTest(frmforgetpassword, 380, 200, 380 * MainModel.midScale, 200 * MainModel.midScale, true);
                frmforgetpassword.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmforgetpassword.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmforgetpassword.Height) / 2);
                frmforgetpassword.TopMost = true;

                frmforgetpassword.ShowDialog();

                BackHelper.HideFormBackGround();
                Application.DoEvents();

                return frmforgetpassword.DialogResult == DialogResult.OK;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        /// <summary>
        /// 显示修改手机号码-新手机号码验证窗口
        /// </summary>
        /// <returns></returns>
        public static bool ShowFormChengPhoneVerifyNewPhone()
        {
            try
            {
                BackHelper.ShowFormBackGround();

                FormChengPhoneVerifyNewPhone frmverifynewphone = new FormChengPhoneVerifyNewPhone();

                asf.AutoScaleControlTest(frmverifynewphone, 380, 450, 380 * MainModel.midScale, 450 * MainModel.midScale, true);
                frmverifynewphone.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmverifynewphone.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmverifynewphone.Height) / 2);
                frmverifynewphone.TopMost = true;

                frmverifynewphone.ShowDialog();
                if (frmverifynewphone.DialogResult == DialogResult.OK)
                {
                    ShowFormChangePhoneNewPhoneSms();
                }
                frmverifynewphone.Dispose();
                BackHelper.HideFormBackGround();
                Application.DoEvents();
                return frmverifynewphone.DialogResult == DialogResult.OK;

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("新手机号码验证窗口异常" + ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 修改手机号码-确认修改窗口
        /// </summary>
        /// <returns></returns>
        public static bool ShowFormChangePhoneConfirm()
        {
            try
            {
                BackHelper.ShowFormBackGround();

                FormChangePhoneConfirm frmconfirm = new FormChangePhoneConfirm();
                asf.AutoScaleControlTest(frmconfirm, 600, 200, 600 * MainModel.midScale, 200 * MainModel.midScale, true);
                frmconfirm.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmconfirm.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmconfirm.Height) / 2);
                frmconfirm.TopMost = true;

                frmconfirm.ShowDialog();

                Application.DoEvents();
                BackHelper.HideFormBackGround();
                return frmconfirm.DialogResult == DialogResult.OK;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}