using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
                    frmmembermedia.Show();
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

        public static void UpdatememberInfo(string phone, string memberinfo, string balance, string credit, string creditspec, string coupon)
        {
            try
            {
                if (frmmembermedia != null && HaveMedia)
                {
                    frmmembermedia.UpdatememberInfo(phone,memberinfo,balance,credit,creditspec,coupon);
                }

            }
            catch { }
        }

    }
}
