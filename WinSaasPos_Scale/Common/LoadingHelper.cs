using WinSaasPOS_Scale.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading;
using System.Windows.Forms;

namespace WinSaasPOS_Scale.Common
{
        public class LoadingHelper
        {

            #region 相关变量定义

            /// <summary>

            /// 定义委托进行窗口关闭

            /// </summary>

            private delegate void CloseDelegate();

            private static frmLoading loadingForm;

            private static readonly Object syncLock = new Object();  //加锁使用



            #endregion


            public static void IniFormToast()
            {
                try
                {
                    if (loadingForm != null)
                    {
                        loadingForm.Dispose();

                    }
                    loadingForm = new frmLoading();
                    loadingForm.TopMost = true ;
                    loadingForm.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - loadingForm.Width) / 2, (Screen.AllScreens[0].Bounds.Height - loadingForm.Height) / 2);
                }
                catch (Exception ex)
                {
                    loadingForm = null;
                    LogManager.WriteLog("初始化信息弹窗异常" + ex.Message);
                }
            }

            public static void ShowLoadingScreen()
            {
                ShowLoadingScreen("加载中...");
            }

            public static void ShowLoadingScreen(string msg)
            {
                try
                {
                    if (loadingForm == null)
                    {
                        IniFormToast();
                    }
                    loadingForm.UpInfo(msg);
                    loadingForm.Show();
                }
                catch (Exception ex)
                {
                    loadingForm = null;
                    LogManager.WriteLog("显示toast异常" + ex.Message);
                }
            }

            public static void CloseForm()
            {
                try
                {
                    if (loadingForm != null)
                    {
                        //loadingForm.Close();
                        loadingForm.Hide();
                    }

                }
                catch (Exception ex)
                {
                    loadingForm = null;
                    LogManager.WriteLog("隐藏toast异常" + ex.Message);
                }
            }


    }
}
