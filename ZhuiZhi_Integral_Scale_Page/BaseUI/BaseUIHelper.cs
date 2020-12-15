using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.BaseUI
{
    public class BaseUIHelper
    {
        //<summary>
        //按比例缩放页面及控件
        //</summary>
        private static AutoSizeFormUtil asf = new AutoSizeFormUtil();

        #region 客屏
        /// <summary>
        /// 是否有客屏
        /// </summary>
        private static bool HaveMedia = Screen.AllScreens.Count() > 1;
        private static FormMainMedia frmmainmedia = null;
        public static void IniFormMainMedia()
        {            
            try
            {
                if (frmmainmedia != null || frmmainmedia.IsDisposed)
                {
                    frmmainmedia.IniForm(null);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("初始客屏异常" + ex.Message);
            }
        }


        public static void ShowFormMainMedia()
        {
            try
            {

                if (HaveMedia)
                {
                    if (frmmainmedia == null || frmmainmedia.IsDisposed)
                    {
                        frmmainmedia = new FormMainMedia();
                        asf.AutoScaleControlTest(frmmainmedia, 1020, 760, Screen.AllScreens[1].Bounds.Width, Screen.AllScreens[1].Bounds.Height, true);
                        frmmainmedia.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width, 0);
                    }
                    frmmainmedia.Show();
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
                if (frmmainmedia != null || !frmmainmedia.IsDisposed)
                {
                    frmmainmedia.Dispose();
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("关闭客屏异常" + ex.Message);
            }
        }

        public static void UpdaForm(Cart cart)
        {
            try
            {
                if (frmmainmedia != null && HaveMedia)
                {
                    MainModel.frmMainmediaCart = cart;
                    frmmainmedia.UpdateForm();
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("跟新客屏购物车异常"+ex.Message);
            }
        }

        public static void UpdateDgvOrderDetail(Dictionary<string,string> orderdetail, string pricecontent,string price)
        {
            try
            {

                if (frmmainmedia != null && HaveMedia)
                {
                    frmmainmedia.UpdateDgvOrderDetail(orderdetail,pricecontent,price);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("跟新客屏购物车异常" + ex.Message);
            }
        }

        public static void ShowPayInfo(string lblinfo,bool isError)
        {
            try
            {

                if (frmmainmedia != null && HaveMedia)
                {
                    frmmainmedia.ShowPayInfo(lblinfo,isError);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("跟新客屏购物车异常" + ex.Message);
            }
        }


        public static void ShowPayResult(object payinfo)
        {
            try
            {
                if (frmmainmedia != null && HaveMedia)
                {
                    frmmainmedia.ShowPayResult(payinfo);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("跟新客屏购物车异常" + ex.Message);
            }
        }


        public static void ShowBalancePwd(bool showorclose)
        {
            try
            {

                if (frmmainmedia != null && HaveMedia)
                {
                    frmmainmedia.ShowBalancePwd(showorclose);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("跟新客屏购物车异常" + ex.Message);
            }
        }

        public static void ShowBalancePwdLog(string msg)
        {
            try
            {
                if (frmmainmedia != null && HaveMedia)
                {
                    frmmainmedia.ShowBalancePwdLog(msg);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("跟新客屏购物车异常" + ex.Message);
            }
        }



        public static void LoadMember()
        {
            try
            {
                if (frmmainmedia != null && HaveMedia)
                {
                    frmmainmedia.LoadMember();
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("加载客屏会员信息异常" + ex.Message);
            }
        }


        public static void UpDgvScorll(int value)
        {
            try
            {
                if (frmmainmedia != null && HaveMedia)
                {
                    frmmainmedia.UpDgvScorll(value);
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("同步客屏购物车滚动条异常" + ex.Message);
            }
        }
        #endregion


        #region 称重收银
        private static frmMainHalfOffLine formmain = null;
        public static void IniFormMain(){
            try{

                if (formmain != null)
                {
                    try
                    {
                        formmain.Close();
                    }
                    catch { }
                    try
                    {
                        formmain.Dispose();
                    }
                    catch { }
                }

                formmain = new frmMainHalfOffLine();
                asf.AutoScaleControlTest(formmain, 1178, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                formmain.Location = new System.Drawing.Point(0,0);

            }catch(Exception ex){
                LogManager.WriteLog("初始化称重收银页面异常"+ex.Message);
            }
        }

        public static void ShowFormMain()
        {
            try
            {
                if (formmain == null || formmain.IsDisposed)
                {
                    IniFormMain(); 
                }
                formmain.ShowDialog();             
                
                if (formmain.DialogResult == DialogResult.Retry)
                {
                    formmain = null;
                    ShowFormMainScale();
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("显示称重收银页面异常"+ex.Message+ex.StackTrace);
            }
        }

        public static void CloseFormMain()
        {
            try
            {
                try
                {
                    formmain.Close();
                    formmain.Dispose();
                }
                catch { }
                formmain = null;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("关闭称重收银页面异常"+ex.Message);
            }
        }
        #endregion

        #region 单独秤
        private static FormMainScale formmainscale = null;
        public static void IniFormMainScale()
        {
            try
            {
                if (formmainscale != null)
                {
                    try
                    {
                        formmainscale.Close();
                    }
                    catch { }
                    try
                    {
                        formmainscale.Dispose();
                    }
                    catch { }
                }

                formmainscale = new FormMainScale();
                asf.AutoScaleControlTest(formmainscale, 1178, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                formmainscale.Location = new System.Drawing.Point(0, 0);

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("初始化单独秤页面异常" + ex.Message);
            }
        }

        public static void ShowFormMainScale()
        {
            try
            {
                if (formmainscale == null || frmmainmedia.IsDisposed)
                {
                    IniFormMainScale();
                }
                formmainscale.ShowDialog();
                
                if (formmainscale.DialogResult == DialogResult.Retry)
                {
                    formmainscale = null;
                    ShowFormMain();
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("显示单独秤页面异常" + ex.Message);
            }
        }

        public static void CloseFormMainScale()
        {
            try
            {
                try
                {
                    //formmainscale.Close();
                    formmainscale.Dispose();
                }
                catch { }
                formmainscale = null;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("关闭单独秤页面异常" + ex.Message);
            }
        }
        #endregion
    }
}
