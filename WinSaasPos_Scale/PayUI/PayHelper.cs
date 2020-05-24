using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinSaasPOS_Scale.Common;
using WinSaasPOS_Scale.HelperUI;
using WinSaasPOS_Scale.Model;

namespace WinSaasPOS_Scale.PayUI
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
                asf.AutoScaleControlTest(frmpay, 380, 480, 380 * MainModel.wScale, 480 * MainModel.hScale, true);
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
                    asf.AutoScaleControlTest(frmpay, 380, 480, 380 * MainModel.wScale, 480 * MainModel.hScale, true);
                    frmpay.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmpay.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmpay.Height) / 2);
                    frmpay.TopMost = true;
                }
                frmpay.UpInfo(cart.qianClone());
                frmpay.ShowDialog();
                BackHelper.HideFormBackGround();
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
                asf.AutoScaleControlTest(frmpaybycash, 380, 480, 380 * MainModel.wScale, 480 * MainModel.hScale, true);
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
                    asf.AutoScaleControlTest(frmpaybycash, 380, 480, 380 * MainModel.wScale, 480 * MainModel.hScale, true);
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
                asf.AutoScaleControlTest(frmpaycashtoonline, 380, 480, 380 * MainModel.wScale, 480 * MainModel.hScale, true);
                frmpaycashtoonline.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmpaycashtoonline.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmpaycashtoonline.Height) / 2);
                frmpaycashtoonline.TopMost = true;
            }
            catch (Exception ex)
            {
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
                    asf.AutoScaleControlTest(frmpaycashtoonline, 380, 480, 380 * MainModel.wScale, 480 * MainModel.hScale, true);
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
        //        asf.AutoScaleControlTest(frmpaybyonline, 380, 480, 380 * MainModel.wScale, 480 * MainModel.hScale, true);
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
                    asf.AutoScaleControlTest(frmpaybyonline, 380, 480, 380 * MainModel.wScale, 480 * MainModel.hScale, true);
                    frmpaybyonline.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmpaybyonline.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmpaybyonline.Height) / 2);
                    frmpaybyonline.TopMost = true;
                             
                frmpaybyonline.ShowDialog();

                frmpaybyonline.Dispose();
                return frmpaybyonline.DialogResult == DialogResult.OK;
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
                asf.AutoScaleControlTest(frmpaybycashcoupon, 380, 480, 380 * MainModel.wScale, 480 * MainModel.hScale, true);
                frmpaybycashcoupon.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmpaybycashcoupon.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmpaybycashcoupon.Height) / 2);
                frmpaybycashcoupon.TopMost = true;
            }
            catch (Exception ex)
            {
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
                    asf.AutoScaleControlTest(frmpaybycashcoupon, 380, 480, 380 * MainModel.wScale, 480 * MainModel.hScale, true);
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
                asf.AutoScaleControlTest(frmpaybybalance, 380, 480, 380 * MainModel.wScale, 480 * MainModel.hScale, true);
                frmpaybybalance.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmpaybybalance.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmpaybybalance.Height) / 2);
                frmpaybybalance.TopMost = true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("初始化余额支付窗体异常"+ex.Message);
            }
        }

        public static int ShowFormPayByBalance(Cart cart,out string orderid)
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
                return frmpaybybalance.ReturnResultCode;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("显示余额支付窗体异常"+ex.Message);
                orderid = "";
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
                asf.AutoScaleControlTest(frmpaybalancetomix, 380, 480, 380 * MainModel.wScale, 480 * MainModel.hScale, true);
                frmpaybalancetomix.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmpaybalancetomix.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmpaybalancetomix.Height) / 2);
                frmpaybalancetomix.TopMost = true;

            }
            catch (Exception ex)
            {
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
                 asf.AutoScaleControlTest(frmpaycashtochange, 1180, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                 frmpaycashtochange.Location = new System.Drawing.Point(0, 0);
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

                frmpaycashtochange = new FormPayCashToChange();
                asf.AutoScaleControlTest(frmpaycashtochange, 1180, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                frmpaycashtochange.Location = new System.Drawing.Point(0, 0);
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
    }
}
