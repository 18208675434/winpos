using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{

    public class MemberCenterMediaHelper
    {
        private static FormMemberCenterMedia frmmembermedia = null;

        /// <summary>
        /// 按比例缩放页面及控件
        /// </summary>
        private static  AutoSizeFormUtil asf = new AutoSizeFormUtil();

        private static bool HaveMedia = false;

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


                if (Screen.AllScreens.Count() > 1)
                {
                    HaveMedia = true;
                    //frmmembermedia.Show();
                }
                else
                {
                    HaveMedia = false;
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

        public static  void ShowPayInfo()
        {
            try
            {
                if (frmmembermedia != null && HaveMedia)
                {
                    frmmembermedia.ShowPayInfo();
                }
            }
            catch { }
        }

        public static void HidePayInfo()
        {
            try
            {
                if (frmmembermedia != null && HaveMedia)
                {
                    frmmembermedia.HidePayInfo() ;
                }
            }
            catch { }
        }
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
        /// <summary>
        /// 调用显示更换手机号码屏
        /// </summary>
        public static void ShowChangePhoneNumber()
        {
            try
            {
                if (frmmembermedia != null && HaveMedia)
                {
                    frmmembermedia.ShowChangePhoneNumber();
                }
            }
            catch { }
        }
        /// <summary>
        /// 调用更换手机号码输入支付密码屏
        /// </summary>
        public static void ShowChangePhonePayPwd()
        {
            try
            {
                if (frmmembermedia != null && HaveMedia)
                {
                    frmmembermedia.ShowChangePhonePayPwd();
                }
            }
            catch { }
        }

        /// <summary> 更换手机-支付密码
        /// </summary>
        public static void UpdateChangePhonePwdUI(string pwd)
        {
            frmmembermedia.UpdateChangePhonePwdUI(pwd);
        }


        /// <summary>
        /// 调用更换手机号码新旧实体卡屏
        /// </summary>
        public static void ShowChangePhoneNewOldCard()
        {
            try
            {
                if (frmmembermedia != null && HaveMedia)
                {
                    frmmembermedia.ShowChangePhoneNewOldCard();
                }
            }
            catch { }
        }
        /// <summary>
        /// 调用更换手机号码新旧实体卡屏
        /// </summary>
        public static void ShowChengPhoneSmsCode()
        {
            try
            {
                if (frmmembermedia != null && HaveMedia)
                {
                    frmmembermedia.ShowChengPhoneSmsCode();
                }
            }
            catch { }
        }

        /// <summary> 更换手机-手机验证码
        /// </summary>
        public static void UpdatePhoneScdUI(string smscode)
        {
            frmmembermedia.UpdatePhoneScdUI(smscode);
        }


        /// <summary>
        /// 调用更换手机号码新旧实体卡屏
        /// </summary>
        public static void ShowChengPhoneVerifyNewPhone()
        {
            try
            {
                if (frmmembermedia != null && HaveMedia)
                {
                    frmmembermedia.ChengPhoneVerifyNewPhone();
                }
            }
            catch { }
        }
         
        public static void UpdatememberInfo(string phone, string memberinfo, string balance, string credit, string creditspec, string coupon,string entitycardid="")
        {
            try
            {
                if (frmmembermedia != null && HaveMedia)
                {
                    frmmembermedia.UpdatememberInfo(phone, memberinfo, balance, credit, creditspec, coupon, entitycardid);
                }

            }
            catch { }
        }

        public static void UpdateEntityCardInfo(string entityCardNo)
        {
            if (frmmembermedia != null && HaveMedia)
            {
                frmmembermedia.UpdateEntityCardInfo(entityCardNo);
            }
        }

    }
}
