using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.PrettyCash
{
    public class PrettyCashHelper
    {
        private static AutoSizeFormUtil asf = new AutoSizeFormUtil();
        public static void ShowFormPretty()
        {
            try
            {

                string currentprettycash = INIManager.GetIni("Receipt", "PrettyCash", MainModel.IniPath);

                //不为空则是已经设置过  
                if (!string.IsNullOrEmpty(currentprettycash))
                {
                    return;
                }


                BackHelper.ShowFormBackGround();

                FormPrettyCash frmprettycash = new FormPrettyCash();
                
                asf.AutoScaleControlTest(frmprettycash, 380, 520, 380 * MainModel.midScale, 520 * MainModel.midScale, true);
                frmprettycash.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmprettycash.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmprettycash.Height) / 2);
                frmprettycash.TopMost = true;
                frmprettycash.ShowDialog();
                frmprettycash.Dispose();

                BackHelper.HideFormBackGround();
                Application.DoEvents();
                
            }
            catch (Exception ex)
            {
                ZhuiZhi_Integral_Scale_UncleFruit.Common.LogManager.WriteLog("设置备用金异常" + ex.Message);
            }
        }

        public static bool ShowFormConfirmPretty(decimal prettycash)
        {
            try
            {
                FormConfirmPrettyCash frmconfirmpretty = new FormConfirmPrettyCash(prettycash);

                asf.AutoScaleControlTest(frmconfirmpretty, 600, 200, 600 * MainModel.midScale, 200 * MainModel.midScale, true);
                frmconfirmpretty.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmconfirmpretty.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmconfirmpretty.Height) / 2);
                frmconfirmpretty.TopMost = true;
                frmconfirmpretty.ShowDialog();
                frmconfirmpretty.Dispose();

                return frmconfirmpretty.DialogResult == DialogResult.OK;
            }
            catch (Exception ex)
            {
                ZhuiZhi_Integral_Scale_UncleFruit.Common.LogManager.WriteLog("确认备用金窗体异常" + ex.Message);
                return false;
            }
        }
    }
}
