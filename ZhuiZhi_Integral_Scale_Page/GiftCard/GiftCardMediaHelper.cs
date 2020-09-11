using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.GiftCard.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.GiftCard
{
    public class GiftCardMediaHelper
    {
       private static  FormGiftCardMedia frmmedia = null;

       //<summary>
       //按比例缩放页面及控件
       //</summary>
       private static AutoSizeFormUtil asf = new AutoSizeFormUtil();

        public static void ShowFormGiftCardMedia()
        {
            try
            {

                if (frmmedia == null || frmmedia.IsDisposed)
                {
                    frmmedia = new FormGiftCardMedia();
                    asf.AutoScaleControlTest(frmmedia, 1180, 760, Screen.AllScreens[1].Bounds.Width, Screen.AllScreens[1].Bounds.Height, true);
                    frmmedia.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width,0);
                }

                frmmedia.TopMost = true;
                frmmedia.Show();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("显示客屏异常" + ex.Message);

            }
        }

        public static void CloseFormGiftCartMedia()
        {
            try
            {
                frmmedia.Close();
            }
            catch { }
        }


        public static  void UpdateCartInfo(CartAloneUpdate cart)
        {
            frmmedia.UpdateCartInfo(cart);
        }

        public static  void LoadMember(Member member)
        {

            frmmedia.LoadMember(member);

        }

        public static void ClearMember()
        {
            frmmedia.ClearMember();
        }

        public static void ShowPayInfo(bool whethershow ){
            frmmedia.ShowPayInfo(whethershow);
        }

    }
}
