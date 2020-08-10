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
                MainModel.ShowLog("显示会员中心页面异常"+ex.Message,true);
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
                LogManager.WriteLog("余额现金充值异常"+ex.Message);
                return false;
            }
        }

        public static bool ShowFormTopUpByOnline(long orderid,string mobile)
        {
            try
            {
                BackHelper.ShowFormBackGround();

                FormTopUpByOnLine frmpaybycash = new FormTopUpByOnLine(orderid.ToString(),mobile);
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

    }
}