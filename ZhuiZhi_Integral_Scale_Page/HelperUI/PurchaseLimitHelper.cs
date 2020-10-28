using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.HelperUI
{
    public class PurchaseLimitHelper
    {

        private static AutoSizeFormUtil asf = new AutoSizeFormUtil();
        public static void ShowPucehaseLimit()
        {
            try
            {
                BackHelper.ShowFormBackGround();

                FormPurchaseLimit frmpurch = new FormPurchaseLimit();
                asf.AutoScaleControlTest(frmpurch, 700, 200, 700 * MainModel.midScale, 200 * MainModel.midScale, true);
                frmpurch.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmpurch.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmpurch.Height) / 2);
                frmpurch.TopMost = true;
                frmpurch.ShowDialog();
                frmpurch.Dispose();
                BackHelper.HideFormBackGround();
            }
            catch (Exception ex)
            {
                BackHelper.HideFormBackGround();
                LogManager.WriteLog("限购弹窗出现异常" + ex.Message);
                
            }
        }

    }
}
