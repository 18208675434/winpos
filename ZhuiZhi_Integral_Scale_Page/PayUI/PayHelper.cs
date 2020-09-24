using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.PayUI
{
    public class PayHelper
    {
        //<summary>
        //按比例缩放页面及控件
        //</summary>
        private static AutoSizeFormUtil asf = new AutoSizeFormUtil();

        #region  支付页面
        private static FormPay frmpay = null;
        public static void IniFormPay()
        {
            try
            {

                if (frmpay != null)
                {
                    try
                    {
                                            frmpay.Dispose();

                    }
                    catch { }
                }
                frmpay = new FormPay();
                asf.AutoScaleControlTest(frmpay, 380, 520, 380 * MainModel.midScale, 520 * MainModel.midScale, true);
                frmpay.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmpay.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmpay.Height) / 2);
                frmpay.TopMost = true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("初始支付窗体异常" + ex.Message);
            }          
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cart"></param>
        /// <returns>返回结果   -1：初始值  0：返回   1:完成   其他 后端code值</returns>
        public static int ShowFormPay(Cart cart)
        {
            try
            {
                //TODO 测试临时取消
                BackHelper.ShowFormBackGround();
                if (frmpay == null)
                {
                    frmpay = new FormPay();
                    asf.AutoScaleControlTest(frmpay, 380, 520, 380 * MainModel.midScale, 520 * MainModel.midScale, true);
                    frmpay.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmpay.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmpay.Height) / 2);
                    frmpay.TopMost = true;
                }
                frmpay.thisCurrentCart = cart.qianClone();
                frmpay.ShowDialog();
                BackHelper.HideFormBackGround();
                Application.DoEvents();
                return frmpay.PayResult; 

            }
            catch (Exception ex)
            {
                frmpay = null;
                BackHelper.HideFormBackGround();
                LogManager.WriteLog("支付弹窗出现异常" + ex.Message);
                return -1;
            }
        }
        #endregion

        #region  现金支付窗体
        private static FormPayByCash frmpaybycash = null;

        public static void IniFormPayByCash()
        {
            try
            {
                if (frmpaybycash != null)
                {
                      try
                    {
                                              frmpaybycash.Dispose();

                    }
                    catch { }
                }
                frmpaybycash = new FormPayByCash();
                asf.AutoScaleControlTest(frmpaybycash, 380, 520, 380 * MainModel.midScale, 520 * MainModel.midScale, true);
                frmpaybycash.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmpaybycash.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmpaybycash.Height) / 2);
                frmpaybycash.TopMost = true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("初始化现金支付窗体异常" + ex.Message);
            }
        }

        /// <summary>
        /// 现金支付弹窗
        /// </summary>
        /// <param name="cart"></param>
        /// <returns>0:取消   1：支付完成 2：需要继续支付  12004：会员登录失效   100031：店员登录失效</returns>
        public static int ShowFormPayByCash(Cart cart,out string orderid)
        {
            try
            {
                if (frmpaybycash == null)
                {
                    frmpaybycash = new FormPayByCash();
                    asf.AutoScaleControlTest(frmpaybycash, 380, 520, 380 * MainModel.midScale, 520 * MainModel.midScale, true);
                    frmpaybycash.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmpaybycash.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmpaybycash.Height) / 2);
                    frmpaybycash.TopMost = true;
                }
                frmpaybycash.UpInfo(cart);
                frmpaybycash.ShowDialog();
                orderid = frmpaybycash.ReruntOrderId;
                cart = frmpaybycash.thisCurrentCart;
                return frmpaybycash.ReturnResultCode;
            }
            catch (Exception ex)
            {
                frmpaybycash = null;
                LogManager.WriteLog("现金支付弹窗出现异常" + ex.Message);
                orderid = "";
                return -1;
            }
        }
        #endregion

        #region 现金转在线继续收银
        private static FormPayCashToOnLine frmpaycashtoonline = null;

        public static void IniFormPayCashToOnLine()
        {
            try
            {
                if (frmpaycashtoonline != null)
                {
                      try
                    {
                                              frmpaycashtoonline.Dispose();

                    }
                    catch { }
                }
                frmpaycashtoonline = new FormPayCashToOnLine();
                asf.AutoScaleControlTest(frmpaycashtoonline, 380, 520, 380 * MainModel.midScale, 520 * MainModel.midScale, true);
                frmpaycashtoonline.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmpaycashtoonline.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmpaycashtoonline.Height) / 2);
                frmpaycashtoonline.TopMost = true;
            }
            catch (Exception ex)
            {
                frmpaycashtoonline = null;
                LogManager.WriteLog("初始化现金支付窗体异常" + ex.Message);
            }
        }

        /// <summary>
        /// 现金转在线继续收银
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public static bool ShowFormPayCashToOnLine(Cart cart)
        {
            try
            {
                if (frmpaycashtoonline == null)
                {
                    frmpaycashtoonline = new FormPayCashToOnLine();
                    asf.AutoScaleControlTest(frmpaycashtoonline, 380, 520, 380 * MainModel.midScale, 520 * MainModel.midScale, true);
                    frmpaycashtoonline.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmpaycashtoonline.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmpaycashtoonline.Height) / 2);
                    frmpaycashtoonline.TopMost = true;
                }
                frmpaycashtoonline.UpInfo(cart);
                frmpaycashtoonline.ShowDialog();
                return frmpaycashtoonline.DialogResult == DialogResult.OK;
            }
            catch (Exception ex)
            {
                frmpaycashtoonline = null;
                LogManager.WriteLog("现金支付弹窗出现异常" + ex.Message);
                return false;
            }
        }

        #endregion

        #region  在线收银

        private static FormPayByOnLine frmpaybyonline = null;

        //public static void IniFormPayByOnLine()
        //{
        //    try
        //    {
        //        if (frmpaybyonline != null)
        //        {
        //            frmpaybyonline.Dispose();
        //        }
        //        frmpaybyonline = new FormPayByOnLine();
        //        asf.AutoScaleControlTest(frmpaybyonline, 380, 520, 380 * MainModel.wScale, 520 * MainModel.hScale, true);
        //        frmpaybyonline.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmpaybyonline.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmpaybyonline.Height) / 2);
        //        frmpaybyonline.TopMost = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogManager.WriteLog("初始化现金支付窗体异常" + ex.Message);
        //    }
        //}

        /// <summary>
        /// 现金转在线继续收银
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public static bool ShowFormPayByOnLine(string orderid,Cart cart)
        {
            try
            {
               
                    frmpaybyonline = new FormPayByOnLine(orderid,cart);
                    asf.AutoScaleControlTest(frmpaybyonline, 380, 520, 380 * MainModel.midScale, 520 * MainModel.midScale, true);
                    frmpaybyonline.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmpaybyonline.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmpaybyonline.Height) / 2);
                    frmpaybyonline.TopMost = true;
                             
                DialogResult dialog = frmpaybyonline.ShowDialog();
                frmpaybyonline.Dispose();
                Application.DoEvents();
                return dialog == DialogResult.OK;
            }
            catch (Exception ex)
            {
                frmpaybyonline = null;
                LogManager.WriteLog("现金支付弹窗出现异常" + ex.Message);
                return false;
            }
        }

        #endregion

        #region 代金券
        private static FormPayByCashCoupon frmpaybycashcoupon = null;
        public static void IniFormPayByCashCoupon()
        {
            try
            {

                if (frmpaybycashcoupon != null)
                {
                      try
                    {
                                              frmpaybycashcoupon.Dispose();

                    }
                    catch { }
                }
                frmpaybycashcoupon = new FormPayByCashCoupon();
                asf.AutoScaleControlTest(frmpaybycashcoupon, 380, 520, 380 * MainModel.midScale, 520 * MainModel.midScale, true);
                frmpaybycashcoupon.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmpaybycashcoupon.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmpaybycashcoupon.Height) / 2);
                frmpaybycashcoupon.TopMost = true;
            }
            catch (Exception ex)
            {
                frmpaybycashcoupon = null;
                LogManager.WriteLog("初始代金券窗体异常" + ex.Message);

            }
        }

        public static bool ShowFormPayByCashCoupon(Cart cart, List<string> lstcash,out string orderid)
        {
            try
            {
                if (frmpaybycashcoupon == null)
                {
                    frmpaybycashcoupon = new FormPayByCashCoupon();
                    asf.AutoScaleControlTest(frmpaybycashcoupon, 380, 520, 380 * MainModel.midScale, 520 * MainModel.midScale, true);
                    frmpaybycashcoupon.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmpaybycashcoupon.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmpaybycashcoupon.Height) / 2);
                    frmpaybycashcoupon.TopMost = true;
                }

                frmpaybycashcoupon.UpInfo(cart, lstcash);

                frmpaybycashcoupon.ShowDialog();
                
                orderid = frmpaybycashcoupon.SuccessOrderID;
                return frmpaybycashcoupon.DialogResult == DialogResult.OK;

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("显示代金券窗体异常"+ex.Message);
                frmpaybycashcoupon = null;
                orderid = "";
                return false;
            }
        }
        #endregion

        #region 余额

        private static FormPayByBalance frmpaybybalance = null;
        public static void IniFormPayByBalance()
        {
            try
            {
                if (frmpaybybalance != null)
                {
                      try
                    {
                          frmpaybybalance.Dispose();

                    }
                    catch { }
                }

                frmpaybybalance = new FormPayByBalance();
                asf.AutoScaleControlTest(frmpaybybalance, 380, 520, 380 * MainModel.midScale, 520 * MainModel.midScale, true);
                frmpaybybalance.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmpaybybalance.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmpaybybalance.Height) / 2);
                frmpaybybalance.TopMost = true;
            }
            catch (Exception ex)
            {
                frmpaybybalance = null;
                LogManager.WriteLog("初始化余额支付窗体异常"+ex.Message);
            }
        }

        public static int ShowFormPayByBalance(Cart cart,out string orderid,out Cart outcart)
        {
            try
            {
                if (frmpaybybalance == null)
                {
                    IniFormPayByBalance();
                }
                frmpaybybalance.UpInfo(cart);
                frmpaybybalance.ShowDialog();
                orderid = frmpaybybalance.OverOrderId;
                outcart = frmpaybybalance.CurrentCart;
                return frmpaybybalance.ReturnResultCode;
            }
            catch (Exception ex)
            {
                frmpaybybalance = null;
                LogManager.WriteLog("显示余额支付窗体异常"+ex.Message);
                orderid = "";
                outcart = cart;
                return -1;
            }
        }


        private static FormPayBalanceToMix frmpaybalancetomix = null;
        public static void IniFormPayBalacneToMix()
        {
            try
            {
                if (frmpaybalancetomix != null)
                {
                      try
                    {
                                              frmpaybalancetomix.Dispose();

                    }
                    catch { }
                }

                frmpaybalancetomix = new FormPayBalanceToMix();
                asf.AutoScaleControlTest(frmpaybalancetomix, 380, 520, 380 * MainModel.midScale, 520 * MainModel.midScale, true);
                frmpaybalancetomix.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmpaybalancetomix.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmpaybalancetomix.Height) / 2);
                frmpaybalancetomix.TopMost = true;

            }
            catch (Exception ex)
            {
                frmpaybalancetomix = null;
                LogManager.WriteLog("初始化余额混合支付窗异常"+ex.Message);
            }
        }

        public static int ShowFormPayBalanceToMix(Cart cart,out string orderid)
        {
            try
            {
                if (frmpaybalancetomix == null)
                {
                    IniFormPayBalacneToMix();
                }

                frmpaybalancetomix.UpInfo(cart);

                frmpaybalancetomix.ShowDialog();
                orderid = frmpaybalancetomix.CrrentOrderid;
                return frmpaybalancetomix.ReturnResultCode;
            }
            catch (Exception ex)
            {
                frmpaybalancetomix = null;
                LogManager.WriteLog("显示余额混合支付窗体异常"+ex.Message);
                orderid = "";
                return -1;
            }
        }
        #endregion

        #region 支付结果
        private static FormPaySuccess frmpaysuccess = null;
        private static FormPayFail frmpayfail = null;

        //public static void IniFormPaySuccess()
        //{
        //    try
        //    {
        //        if (frmpaysuccess != null)
        //        {
        //            frmpaysuccess.Dispose();
        //        }
        //        frmpaysuccess = new FormPaySuccess();
        //        asf.AutoScaleControlTest(frmpaysuccess, 1180, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
        //            frmpaysuccess.Location = new System.Drawing.Point(0,0); 
        //frmpaysuccess.TopMost = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogManager.WriteLog("初始支付完成窗体异常" + ex.Message);
        //    }
        //}

        public static bool ShowFormPaySuccess(string orderid)
        {
             try
            {

                frmpaysuccess = new FormPaySuccess(orderid);
                asf.AutoScaleControlTest(frmpaysuccess, 1180, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                    frmpaysuccess.Location = new System.Drawing.Point(0,0);
                    frmpaysuccess.TopMost = true;
               
                
                frmpaysuccess.ShowDialog();

                frmpaysuccess.Dispose();
                return frmpaysuccess.DialogResult == DialogResult.OK;
            }
            catch (Exception ex)
            {
                frmpaysuccess = null;
                LogManager.WriteLog("现金支付弹窗出现异常" + ex.Message);
                return false;
            }
        }
        #endregion

        #region  现金找零页面
        private static FormPayCashToChange frmpaycashtochange = null;
        public static void IniFormPayCashToChange()
        {
            try
            {
                 if (frmpaycashtochange != null)
                 {
                       try
                    {
                           frmpaycashtochange.Dispose();
                    }
                    catch { }
                 }
                 frmpaycashtochange = new FormPayCashToChange();

                 asf.AutoScaleControlTest(frmpaycashtochange, 380, 520, 380 * MainModel.midScale, 520 * MainModel.midScale, true);
                 frmpaybycash.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmpaycashtochange.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmpaycashtochange.Height) / 2);

                 frmpaycashtochange.TopMost = true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("初始找零窗体异常" + ex.Message);
            }
        }

        public static bool ShowFormPayCashToChange(Cart cart)
        {
            try
            {
                FormPayCashToChange frmpaycashtochange = new FormPayCashToChange();
                asf.AutoScaleControlTest(frmpaycashtochange, 380, 520, 380 * MainModel.midScale, 520 * MainModel.midScale, true);
                frmpaycashtochange.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmpaycashtochange.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmpaycashtochange.Height) / 2);

                frmpaycashtochange.TopMost = true;

                frmpaycashtochange.UpInfo(cart);
                frmpaycashtochange.ShowDialog();

                frmpaycashtochange.Dispose();
                return frmpaycashtochange.DialogResult == DialogResult.OK;
            }
            catch (Exception ex)
            {
                frmpaycashtochange = null;
                LogManager.WriteLog("找零页面出现异常" + ex.Message);
                return false;
            }
        }
        #endregion


        public static bool ShowFormChangeAndTopup(Cart cart,out string orderid)
        {
            try
            {
                FormChangeAndTopUp frmpaycashtochange = new FormChangeAndTopUp(cart);
                asf.AutoScaleControlTest(frmpaycashtochange, 600, 360, 600 * MainModel.midScale, 360 * MainModel.midScale, true);
                frmpaycashtochange.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmpaycashtochange.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmpaycashtochange.Height) / 2);

                frmpaycashtochange.TopMost = true;
                frmpaycashtochange.ShowDialog();

                frmpaycashtochange.Dispose();
                orderid = frmpaycashtochange.SuccessOrderid;
                return frmpaycashtochange.DialogResult == DialogResult.OK;
            }
            catch (Exception ex)
            {
                orderid = "";
                frmpaycashtochange = null;
                LogManager.WriteLog("找零转存余额页面出现异常" + ex.Message);
                return false;
            }
        }



        #region 兑换券

        private static FormVoucher frmvoucher = null;

        public static void IniFormVoucher()
        {
            try
            {
                if (frmvoucher != null)
                {
                    try
                    {
                        frmvoucher.Dispose();

                    }
                    catch { }
                }
                frmvoucher = new FormVoucher();
                asf.AutoScaleControlTest(frmvoucher, 1180, 760, 1180 * MainModel.wScale, 760 * MainModel.hScale, true);
                frmvoucher.Location = new System.Drawing.Point(0,0);
                frmvoucher.TopMost = true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("兑换券窗体异常" + ex.Message);
            }
        }

        /// <summary>
        /// 会员码 不校验规则和长度
        /// </summary>
        /// <param name="cart"></param>
        /// <returns>0:取消   1：支付完成 2：需要继续支付  12004：会员登录失效   100031：店员登录失效</returns>
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
                LogManager.WriteLog("会员号弹窗异常" + ex.Message);
                return "";
            }
        }

        #endregion

        #region 其他支付方式


        /// <summary>
        /// 现金支付弹窗
        /// </summary>
        /// <param name="cart"></param>
        /// <returns>0:取消   1：支付完成 2：需要继续支付  12004：会员登录失效   100031：店员登录失效</returns>
        public static OtherPayResult ShowFormPayByOther(Cart cart)
        {
            try
            {
                    FormPayByOther frmpaybyother = new FormPayByOther(cart);
                    asf.AutoScaleControlTest(frmpaybyother, 800, 600, 800 * MainModel.midScale, 600 * MainModel.midScale, true);
                    frmpaybyother.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmpaybyother.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmpaybyother.Height) / 2);
                    frmpaybyother.TopMost = true;

                frmpaybyother.ShowDialog();
                frmpaybyother.Dispose();
                Application.DoEvents();

                cart.otherpayinfos = frmpaybyother.LstOtherPayInfos;
                return null;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("其他支付弹窗出现异常" + ex.Message);
                return null;
            }
        }


        public static bool ShowFormCouponPwd(string couponcode,out string password)
        {
            FormBackGround Tempfrmback = new FormBackGround();
            try
            {

               
                Tempfrmback.TopMost = true;
                Tempfrmback.Location = new System.Drawing.Point(0, 0);

                Tempfrmback.Show();

                FormCouponPwd frmnumber = new FormCouponPwd(couponcode);
                asf.AutoScaleControlTest(frmnumber, 380, 480, 380 * MainModel.midScale, 480 * MainModel.midScale, true);
                frmnumber.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmnumber.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmnumber.Height) / 2);
                frmnumber.TopMost = true;

                frmnumber.ShowDialog();

                Tempfrmback.Close();
                frmnumber.Dispose();
                Application.DoEvents();

                if (frmnumber.DialogResult == DialogResult.OK)
                {
                    password = frmnumber.Pasword;
                    return true;
                }
                else
                {
                    password = null;
                    return false;
                }
            }
            catch (Exception ex)
            {
                password = null;
                try
                {
                    Tempfrmback.Close();
                }
                catch { }
                LogManager.WriteLog("验证优惠券异常" + ex.Message);
                return false;
            }
        }

        #endregion
    }
}
