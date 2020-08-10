using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.HelperUI
{
    public class ScaleHelper
    {
        //<summary>
        //按比例缩放页面及控件
        //</summary>
        private static AutoSizeFormUtil asf = new AutoSizeFormUtil();

        private static FormScale frmscale = null;
        public static void IniFormScale()
        {
            try
            {
                if (frmscale != null)
                {
                    frmscale.Dispose();
                }

                frmscale = new FormScale();
                asf.AutoScaleControlTest(frmscale, 380, 480, 380 * MainModel.midScale, 480 * MainModel.midScale, true);
                frmscale.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmscale.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmscale.Height) / 2);
                frmscale.TopMost = true;

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("初始化确认弹窗异常" + ex.Message);
            }
        }

        public static bool ShowFormScale(Product pro)
        {
            try
            {
                BackHelper.ShowFormBackGround();
                if (frmscale == null || frmscale.IsDisposed)
                {
                    frmscale = new FormScale();
                    asf.AutoScaleControlTest(frmscale, 380, 480, 380 * MainModel.midScale, 480 * MainModel.midScale, true);
                    frmscale.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmscale.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmscale.Height) / 2);
                    frmscale.TopMost = true;

                }
                frmscale.UpInfo(pro);
                frmscale.ShowDialog();
                Application.DoEvents();
                BackHelper.HideFormBackGround();
                return frmscale.DialogResult == DialogResult.OK;
            }
            catch (Exception ex)
            {
                BackHelper.HideFormBackGround();
                frmscale = null;
                LogManager.WriteLog("称重弹窗出现异常" + ex.Message);
                return false;
            }
        }
    }
}
