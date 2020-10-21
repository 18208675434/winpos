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
        /// <summary>
        /// 手机号新值
        /// </summary>
        public static Member member = new Member();
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

        public static bool ShowFormEntityCardList(List<OutEntityCardResponseDto> outentitycards)
        {
            try
            {
                BackHelper.ShowFormBackGround();

                FormEntityCardList formEntityCardList = new FormEntityCardList(outentitycards);
                asf.AutoScaleControlTest(formEntityCardList, 800, 600, 800 * MainModel.midScale, 600 * MainModel.midScale, true);
                formEntityCardList.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - formEntityCardList.Width) / 2, (Screen.AllScreens[0].Bounds.Height - formEntityCardList.Height) / 2);
                formEntityCardList.TopMost = true;
                DialogResult dialog= formEntityCardList.ShowDialog();
                BackHelper.HideFormBackGround();

                formEntityCardList.Dispose();
                Application.DoEvents();
                return dialog == DialogResult.OK;
            }
            catch (Exception ex)
            {
                BackHelper.HideFormBackGround();
                LogManager.WriteLog("显示会员优惠券列表异常" + ex.Message);
            }
            return false;
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
        public static bool ShowFormChangePhoneNumber(Member member)
        {
            try
            {

                FormChangePhoneNumber frmchangephonenumber = new FormChangePhoneNumber(member);
                asf.AutoScaleControlTest(frmchangephonenumber, 1180, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                frmchangephonenumber.Location = new System.Drawing.Point(0, 0);
                frmchangephonenumber.TopMost = true;
                bool flag = frmchangephonenumber.ShowDialog() == DialogResult.OK;
                frmchangephonenumber.Dispose();
                Application.DoEvents();
                return flag;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("显示修改手机号码界面异常" + ex.Message);
            }
            return false;

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
        public static bool ShowFormChangePhonePayPwd(string newphone)
        {
            try
            {
                BackHelper.ShowFormBackGround();
                FormChangePhonePayPwd frmchangephonepaypwd = new FormChangePhonePayPwd(newphone);
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
                FormChangePhoneNewCard frmchangephonenewcard = new FormChangePhoneNewCard();
                asf.AutoScaleControlTest(frmchangephonenewcard, 380, 450, 380 * MainModel.midScale, 450 * MainModel.midScale, true);
                frmchangephonenewcard.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmchangephonenewcard.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmchangephonenewcard.Height) / 2);
                frmchangephonenewcard.TopMost = true;
                frmchangephonenewcard.ShowDialog();
                frmchangephonenewcard.Dispose();
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
        public static bool ShowFormChangePhoneNewPhoneSms(string newphone)
        {
            try
            {
                FormChangePhoneNewPhoneSms formChangePhoneNewPhoneSms = new FormChangePhoneNewPhoneSms(newphone);
                asf.AutoScaleControlTest(formChangePhoneNewPhoneSms, 380, 200, 380 * MainModel.midScale, 200 * MainModel.midScale, true);
                formChangePhoneNewPhoneSms.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - formChangePhoneNewPhoneSms.Width) / 2, (Screen.AllScreens[0].Bounds.Height - formChangePhoneNewPhoneSms.Height) / 2);
                formChangePhoneNewPhoneSms.TopMost = true;

                DialogResult dialog = formChangePhoneNewPhoneSms.ShowDialog();
                return dialog == DialogResult.OK;
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
        public static string ShowFormChengPhoneVerifyNewPhone()
        {
            try
            {
                FormChengPhoneVerifyNewPhone frmverifynewphone = new FormChengPhoneVerifyNewPhone();
                asf.AutoScaleControlTest(frmverifynewphone, 380, 450, 380 * MainModel.midScale, 450 * MainModel.midScale, true);
                frmverifynewphone.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmverifynewphone.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmverifynewphone.Height) / 2);
                frmverifynewphone.TopMost = true;
                DialogResult dialog = frmverifynewphone.ShowDialog();

                if (frmverifynewphone.DialogResult == DialogResult.OK)
                {
                    return frmverifynewphone.newphone;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("新手机号码验证窗口异常" + ex.Message);
            }
            return "";
        }
        /// <summary>
        /// 修改手机号码-确认修改窗口
        /// </summary>
        /// <returns></returns>
        public static bool ShowFormChangePhoneConfirm(string newphone, bool isMember)
        {
            try
            {
                BackHelper.ShowFormBackGround();

                FormChangePhoneConfirm frmconfirm = new FormChangePhoneConfirm(newphone, isMember);
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


        #region 批量售卡
        private static FormBatchSaleCardCreate formBatchSaleCardCreate = null;

        public static void IniFormBatchSaleCardCreate()
        {
            try
            {
                if (formBatchSaleCardCreate != null)
                {
                    try
                    {
                        formBatchSaleCardCreate.Dispose();
                    }
                    catch { }
                }
                formBatchSaleCardCreate = new FormBatchSaleCardCreate();
                asf.AutoScaleControlTest(formBatchSaleCardCreate, 1180, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                formBatchSaleCardCreate.Location = new System.Drawing.Point(0, 0);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("初始化报损页面异常" + ex.Message);
            }
        }

        public static void ShowFormBatchSaleCardCreate()
        {
            try
            {
                if (formBatchSaleCardCreate == null || formBatchSaleCardCreate.IsDisposed)
                {
                    IniFormBatchSaleCardCreate();
                }

                formBatchSaleCardCreate.ShowDialog();
                formBatchSaleCardCreate.Dispose();
            }
            catch (Exception ex)
            {
                formBatchSaleCardCreate = null;
                LogManager.WriteLog("显示批量售卡页面异常" + ex.Message);
            }
        }
        #endregion

        #region 实体卡卡号录入
        /// <summary>
        /// 实体卡卡号 不校验规则和长度
        /// </summary>
        /// <param name="cart"></param>
        /// <returns>返回录入的实体卡卡号</returns>
        public static string ShowFormVoucher()
        {
            try
            {
                BackHelper.ShowFormBackGround();

                FormVoucher frmvoucher = new FormVoucher();
                asf.AutoScaleControlTest(frmvoucher, 430, 240, 430 * MainModel.midScale, 240 * MainModel.midScale, true);
                frmvoucher.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmvoucher.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmvoucher.Height) / 3);
                frmvoucher.TopMost = true;

                frmvoucher.ShowDialog();

                BackHelper.HideFormBackGround();
                frmvoucher.Dispose();

                Application.DoEvents();
                return frmvoucher.CurrentPhone;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("实体卡号弹窗异常" + ex.Message);
                return "";
            }
        }
        #endregion

        #region 充值明细
        private static FormRechargeQuery formRechangeQuery = null;
        public static void IniFormRechangeQuery()
        {
            try
            {
                if (formRechangeQuery != null)
                {
                    try
                    {
                        formRechangeQuery.Dispose();
                    }
                    catch { }
                }
                formRechangeQuery = new FormRechargeQuery();
                asf.AutoScaleControlTest(formRechangeQuery, 1180, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                formRechangeQuery.Location = new System.Drawing.Point(0, 0);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("初始化充值明细页面异常" + ex.Message);
            }
        }

        public static void ShowFormRechangeQuery()
        {
            try
            {
                if (formRechangeQuery == null || formRechangeQuery.IsDisposed)
                {
                    IniFormRechangeQuery();
                }

                formRechangeQuery.ShowDialog();
                formRechangeQuery.Dispose();
            }
            catch (Exception ex)
            {
                formBatchSaleCardCreate = null;
                LogManager.WriteLog("显示充值明细页面异常" + ex.Message);
            }
        }
        #endregion

        #region 单用户充值明细
        private static FormRechargeSingleUserQuery formSingleUserRechargeQuery = null;
        public static void IniFormSingleUserRechangeQuery(string phone)
        {
            try
            {
                if (formSingleUserRechargeQuery != null)
                {
                    try
                    {
                        formRechangeQuery.Dispose();
                    }
                    catch { }
                }
                formSingleUserRechargeQuery = new FormRechargeSingleUserQuery(phone);
                asf.AutoScaleControlTest(formSingleUserRechargeQuery, 1180, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                formSingleUserRechargeQuery.Location = new System.Drawing.Point(0, 0);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("初始化单用户充值明细页面异常" + ex.Message);
            }
        }

        public static void ShowFormSingleUserRechangeQuery(string phone)
        {
            try
            {
                if (formSingleUserRechargeQuery == null || formSingleUserRechargeQuery.IsDisposed)
                {
                    IniFormSingleUserRechangeQuery(phone);
                }

                formSingleUserRechargeQuery.ShowDialog();
                formSingleUserRechargeQuery.Dispose();
            }
            catch (Exception ex)
            {
                formBatchSaleCardCreate = null;
                LogManager.WriteLog("显示单用户充值明细页面异常" + ex.Message);
            }
        }
        #endregion

        /// <summary> 绑定实体卡
        /// </summary>
        public static bool ShowFormBindEntityCard(EntityCard entityCard)
        {
            try
            {
                BackHelper.ShowFormBackGround();

                FormEntityCardBind formBindEntityCard = new FormEntityCardBind(entityCard);
                asf.AutoScaleControlTest(formBindEntityCard, 1180, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                formBindEntityCard.Location = new System.Drawing.Point(0, 0);
                formBindEntityCard.TopMost = true;
                formBindEntityCard.ShowDialog();
                Application.DoEvents();
                BackHelper.HideFormBackGround();
                return formBindEntityCard.DialogResult == DialogResult.OK;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("绑定实体卡页面异常" + ex.Message);
                return false;
            }
        }



        /// <summary>
        /// 显示页面 自定义充值金额
        /// </summary>
        /// <returns></returns>
        public static ListAllTemplate ShowFormCustomerChange()
        {
            try
            {
                FormCustomMoney money = new FormCustomMoney();
                asf.AutoScaleControlTest(money, 420, 197, 420 * MainModel.midScale, 197 * MainModel.midScale, true);
                money.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - money.Width) / 2, (Screen.AllScreens[0].Bounds.Height - money.Height) / 2);
                money.TopMost = true;
                BackHelper.ShowFormBackGround();
                money.ShowDialog();
                money.Dispose();
                BackHelper.HideFormBackGround();

                return money.CustomTemplate;

            }
            catch (Exception ex)
            {
                BackHelper.HideFormBackGround();
                LogManager.WriteLog("加载自定义充值金额页面异常" + ex);
                return null;
            }
        }

        public static ClassPayment ShowFormOtherMethord(List<ClassPayment> payments, decimal amount)
        {
            try
            {
                FormOtherMethod pay = new FormOtherMethod(payments, amount);
                asf.AutoScaleControlTest(pay, 500, 300, 500 * MainModel.midScale, 300 * MainModel.midScale, true);
                pay.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - pay.Width) / 2, (Screen.AllScreens[0].Bounds.Height - pay.Height) / 2);
                pay.TopMost = true;
                BackHelper.ShowFormBackGround();

                pay.ShowDialog();
                pay.Dispose();
                BackHelper.HideFormBackGround();

                return pay.SelectPayMent;

            }
            catch (Exception ex)
            {
                BackHelper.HideFormBackGround();
                LogManager.WriteLog("加载充值其他支付方式页面异常" + ex);
                return null;
            }
        }

    }
}