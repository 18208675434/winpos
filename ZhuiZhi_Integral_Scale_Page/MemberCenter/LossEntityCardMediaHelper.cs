using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{


    public class LossEntityCardMediaHelper
    {

        private static FormLossEntityCardMedia formLossEntityCardMedia = null;
        //<summary>
        //按比例缩放页面及控件
        //</summary>
        private static AutoSizeFormUtil asf = new AutoSizeFormUtil();

        public static void ShowFormLossEntityCardMedia()
        {
            try
            {
                if (formLossEntityCardMedia == null || formLossEntityCardMedia.IsDisposed)
                {
                    formLossEntityCardMedia = new FormLossEntityCardMedia();
                    asf.AutoScaleControlTest(formLossEntityCardMedia, 1180, 760, Screen.AllScreens[1].Bounds.Width, Screen.AllScreens[1].Bounds.Height, true);
                    formLossEntityCardMedia.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width, 0);
                    formLossEntityCardMedia.TopMost = true;
                }              
                formLossEntityCardMedia.Show();

            }
            catch (Exception ex)
            {

            }
        }


        public static void CloseLossEntityCardMedai()
        {
            if (formLossEntityCardMedia != null)
            {
                formLossEntityCardMedia.Close();
            }
        }


        public static void UpdateEntityCardInfo(string phone,string newCardNo)
        {
            formLossEntityCardMedia.UpdateEntityCardInfo(phone,newCardNo);
        }
    }
}
