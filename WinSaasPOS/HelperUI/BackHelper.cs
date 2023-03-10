using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinSaasPOS.Common;

namespace WinSaasPOS.HelperUI
{
    
    public class BackHelper
    {

        private static FormBackGround frmback = null;


        public static void IniFormBackGround()
        {
            try
            {
                if (frmback != null)
                {
                    try
                    {
                        frmback.Close();
                        frmback.Dispose();
                    }
                    catch { }
                }

                frmback = new FormBackGround();
                frmback.TopMost = true;
                frmback.Location = new System.Drawing.Point(0, 0);
            }
            catch (Exception ex)
            {
                frmback = null;
                LogManager.WriteLog("初始化灰屏异常" + ex.Message);
            }
        }

        public static void ShowFormBackGround()
        {
            try
            {
                if (frmback == null || frmback.IsDisposed)
                {
                    frmback = new FormBackGround();
                    frmback.TopMost = true;
                    frmback.Location = new System.Drawing.Point(0,0);
                }

                frmback.Show();
            }
            catch (Exception ex)
            {
                frmback = null;
                LogManager.WriteLog("显示灰屏异常" + ex.Message);
            }
        }



        public static void HideFormBackGround()
        {
            try
            {
                if (frmback != null && !frmback.IsDisposed)
                {
                    frmback.Hide();
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("关闭灰屏异常" + ex.Message);
            }
        }
    }
}
