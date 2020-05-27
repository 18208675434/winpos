using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using WinSaasPOS.Common;

namespace WinSaasPOS.HelperUI
{
    public class ToastHelper
    {
        public static FormToast frmtoast = null;

                  
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
                frmtoast.currentmsg = msg;
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

                    frmtoast.Close();
                    frmtoast = null;
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
                frmtoast = null;
                HideFormToast();

                LogManager.WriteLog("AutoToast异常" + ex.Message);
            }
           
        }



    }
}
