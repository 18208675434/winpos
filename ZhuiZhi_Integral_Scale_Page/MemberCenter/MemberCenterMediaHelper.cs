using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{

    public class MemberCenterMediaHelper
    {
        //static MemberCenterHelper(){
        //    HaveMedia=Screen.AllScreens.Count() > 1;
        //}
        private static FormMemberCenterMedia frmmembermedia = null;

        /// <summary>
        /// 按比例缩放页面及控件
        /// </summary>
        private static AutoSizeFormUtil asf = new AutoSizeFormUtil();

        private static bool HaveMedia = Screen.AllScreens.Count() > 1;

        public static void ShowFormMainMedia()
        {
            try
            {
                if (frmmembermedia == null || frmmembermedia.IsDisposed)
                {
                    frmmembermedia = new FormMemberCenterMedia();
                    asf.AutoScaleControlTest(frmmembermedia, 1180, 760, Screen.AllScreens[1].Bounds.Width, Screen.AllScreens[1].Bounds.Height, true);
                    frmmembermedia.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width, 0);
                    frmmembermedia.TopMost = true;
                }

                if (HaveMedia)
                {
                    frmmembermedia.Show();
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("显示客屏异常" + ex.Message);
            }
        }


        public static void CloseFormMainMedia()
        {
            try
            {
                if (frmmembermedia != null || !frmmembermedia.IsDisposed)
                {
                    frmmembermedia.Close();
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("关闭客屏异常" + ex.Message);
            }
        }

        public static void UpdateDgvTemplate(List<Bitmap> lstbmp)
        {
            try
            {

                if (frmmembermedia != null && HaveMedia)
                {
                    frmmembermedia.UpdateDgvTemplate(lstbmp);
                }


            }
            catch
            {

            }
        }

        #region 客屏提示支付窗体

        private static FormPayOnLineMedia frmonlinemedia = null;

        public static void ShowPayInfo()
        {
            try
            {

                if (Screen.AllScreens.Count() > 1)
                {
                    if (frmonlinemedia == null)
                    {
                        frmonlinemedia = new FormPayOnLineMedia();
                        asf.AutoScaleControlTest(frmonlinemedia, 1180, 760, Screen.AllScreens[1].Bounds.Width, Screen.AllScreens[1].Bounds.Height,true);
                        frmonlinemedia.Location = new Point(Screen.AllScreens[0].Bounds.Width,0);
                        frmonlinemedia.TopMost = true;
                    }

                    frmonlinemedia.Show();
                }
               
            }
            catch { }
        }

        public static void HidePayInfo()
        {
            try
            {

                if (frmonlinemedia != null)
                {
                    frmonlinemedia.Close();
                    frmonlinemedia = null;
                }
                //if (frmmembermedia != null && HaveMedia)
                //{
                //    frmmembermedia.HidePayInfo();
                //}
            }
            catch { }
        }

        //public static void ShowPayInfo()
        //{
        //    try
        //    {
        //        if (frmmembermedia != null && HaveMedia)
        //        {
        //            frmmembermedia.ShowPayInfo();
        //        }
        //    }
        //    catch { }
        //}

        //public static void HidePayInfo()
        //{
        //    try
        //    {
        //        if (frmmembermedia != null && HaveMedia)
        //        {
        //            frmmembermedia.HidePayInfo();
        //        }
        //    }
        //    catch { }
        //}
        #endregion

       
        #region 修改密码
        /// <summary>
        /// 调用显示修改密码客屏界面
        /// </summary>
        public static void ShowServrPassWord()
        {
            try
            {
                if (frmmembermedia != null && HaveMedia)
                {
                    frmmembermedia.ShowServrPassWord();
                }
            }
            catch { }
        }

        /// <summary> 更新修改密码UI 0-原密码 1-新密码 2-确认新密码
        /// </summary>
        public static void UpdatePassWordUpdateUI(int numtype, string smscode)
        {
            frmmembermedia.UpdatePassWordUpdateUI(numtype, smscode);
        }
        #endregion

        #region 忘记密码
        /// <summary>
        /// 调用显示忘记密码客屏界面
        /// </summary>
        public static void ShowForgetPassWord()
        {
            try
            {
                if (frmmembermedia != null && HaveMedia)
                {
                    frmmembermedia.ShowForgetPassWord();
                }
            }
            catch { }
        }

        /// <summary> 更新忘记密码UI
        /// </summary>
        public static void UpdateForgetPassWordUI(int numtype, string smscode)
        {
            frmmembermedia.UpdateForgetPassWordUI(numtype, smscode);
        }


        /// <summary>
        /// 调用显示密码修改成功界面
        /// </summary>
        public static void ShowChangePassWordOK()
        {
            try
            {
                if (frmmembermedia != null && HaveMedia)
                {
                    frmmembermedia.ShowChangePassWordOK();
                }
            }
            catch { }
        }
        #endregion

        #region 更换手机
        /// <summary>
        /// 更换手机-客屏显示 
        /// <param name="ismember">step3 是否是会员</param>
        /// </summary>
        public static void ShowChangePhoneNumber(int step, bool ismember = false)
        {
            try
            {
                if (frmmembermedia != null && HaveMedia)
                {
                    frmmembermedia.ShowChangePhoneNumber(step, ismember);
                }
            }
            catch { }
        }
        #endregion

        #region 弹框显示
        /// <summary> 显示提示框
        /// </summary>
        public static void ShowLog(string msg, bool iserror = false)
        {
            try
            {
                if (frmmembermedia != null && HaveMedia)
                {
                    frmmembermedia.ShowLog(msg, iserror);
                }
            }
            catch { }
        }
        #endregion

        /// <summary> 刷新客屏信息
        /// </summary>
        public static void UpdatememberInfo(string phone, string memberinfo, string balance, string credit, string creditspec, string coupon, string entitycard)
        {
            try
            {
                if (frmmembermedia != null && HaveMedia)
                {
                    frmmembermedia.UpdatememberInfo(phone, memberinfo, balance, credit, creditspec, coupon, entitycard);
                }

            }
            catch { }
        }

        #region 绑卡
        private static FormEntityCardBindMedia formBindEntityCardMedia = null;
        public static void ShowFormBindEntityCardMedia(OutEntityCardResponseDto entityCard)
        {
            try
            {
                if (formBindEntityCardMedia == null || formBindEntityCardMedia.IsDisposed)
                {
                    formBindEntityCardMedia = new FormEntityCardBindMedia();
                    asf.AutoScaleControlTest(formBindEntityCardMedia, 1180, 760, Screen.AllScreens[1].Bounds.Width, Screen.AllScreens[1].Bounds.Height, true);
                    formBindEntityCardMedia.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width, 0);
                    formBindEntityCardMedia.TopMost = true;
                    formBindEntityCardMedia.UpdateEntityCardInfo(entityCard);
                }
                formBindEntityCardMedia.Show();
            }
            catch (Exception ex)
            {

            }
        }

        public static void CloseLossEntityCardMedai()
        {
            if (formBindEntityCardMedia != null)
            {
                formBindEntityCardMedia.Close();
            }
        }
        #endregion

        #region 批量售卡
        private static ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.FormEntityCardBatchSaleMedia formEntityCardBatchSaleMedia = null;
        public static void ShowFormEntityCardBatchSaleMedia()
        {
            try
            {
                if (formEntityCardBatchSaleMedia == null || formEntityCardBatchSaleMedia.IsDisposed)
                {
                    formEntityCardBatchSaleMedia = new ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.FormEntityCardBatchSaleMedia();
                    asf.AutoScaleControlTest(formEntityCardBatchSaleMedia, 1180, 760, Screen.AllScreens[1].Bounds.Width, Screen.AllScreens[1].Bounds.Height, true);
                    formEntityCardBatchSaleMedia.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width, 0);
                    //formEntityCardBatchSaleMedia.TopMost = true;
                }
                formEntityCardBatchSaleMedia.Show();
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary> 刷新客屏信息
        /// </summary>
        public static void UpdateFormEntityCardBatchSaleMedia(string cardSum, string totalRechargeAmount, string totalGiftAmount, string totalRechargeAll, string totalPay, List<ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.RechargeCardInfo> lstCard)
        {
            try
            {
                if (formEntityCardBatchSaleMedia != null && HaveMedia)
                {
                    formEntityCardBatchSaleMedia.UpdateFormEntityCardBatchSaleMedia(cardSum, totalRechargeAmount, totalGiftAmount, totalRechargeAll, totalPay, lstCard);
                }
            }
            catch { }
        }


        public static void CloseFormEntityCardBatchSaleMedia()
        {
            if (formEntityCardBatchSaleMedia != null)
            {
                formEntityCardBatchSaleMedia.Close();
            }
        }
        #endregion
    }
}
