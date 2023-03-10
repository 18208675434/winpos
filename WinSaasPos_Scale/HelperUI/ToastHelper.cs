using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using WinSaasPOS_Scale.Common;

namespace WinSaasPOS_Scale.HelperUI
{
    public class ToastHelper
    {
        private static FormToast frmtoast = null;

                  
        public static void IniFormToast()
        {
            try
            {
                if (frmtoast != null)
                {
                    frmtoast.Dispose();                 
                }
                frmtoast = new FormToast();
                frmtoast.TopMost = true;
            }
            catch (Exception ex)
            {
                frmtoast = null;
                LogManager.WriteLog("初始化信息弹窗异常" + ex.Message);
            }
        }


        public static void ShowFormToast(string msg)
        {
            try
            {
                if (frmtoast == null || frmtoast.IsDisposed)
                {
                    IniFormToast();
                }
                frmtoast.UpInfo(msg);
                frmtoast.Show();
            }
            catch (Exception ex)
            {
                frmtoast = null;
                LogManager.WriteLog("显示toast异常" + ex.Message);
            }
        }

        public static void HideFormToast()
        {
             try
            {
                if (frmtoast != null)
                {
                    frmtoast.Hide();
                }
              
            }
            catch (Exception ex)
            {
                frmtoast = null;
                LogManager.WriteLog("隐藏toast异常" + ex.Message);
            }
        }

        public static void AutoToast(string msg, int interval)
        {

            try
            {
                ShowFormToast(msg);
                Delay.Start(interval);
                HideFormToast();
            }
            catch (Exception ex)
            {
                HideFormToast();
            }
           
        }



    }
}
