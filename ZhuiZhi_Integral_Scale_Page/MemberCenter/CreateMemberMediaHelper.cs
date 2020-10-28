using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{

   
    public class CreateMemberMediaHelper
    {

        private static FormCreateMemberMedia frmmainmedia = null;
        //<summary>
        //按比例缩放页面及控件
        //</summary>
        private static AutoSizeFormUtil asf = new AutoSizeFormUtil();

        public static void ShowFormCreateMedia()
        {
            try
            {
                if (frmmainmedia == null || frmmainmedia.IsDisposed)
                {
                    frmmainmedia = new FormCreateMemberMedia();
                    asf.AutoScaleControlTest(frmmainmedia, 1180, 760, Screen.AllScreens[1].Bounds.Width, Screen.AllScreens[1].Bounds.Height, true);
                    frmmainmedia.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width, 0);
                }

                    frmmainmedia.Show();
   
            }
            catch (Exception ex)
            {

            }
        }


        public static void CloseFormCreateMedai()
        {
            if (frmmainmedia != null)
            {
                frmmainmedia.Close();
            }
        }


        public static void UpdateMemberInfo(string phone, string name, string birthday, string gender)
        {
            frmmainmedia.UpdateMemberInfo(phone,name,birthday,gender);
        }


        public static void UpdateType(int select)
        {
            frmmainmedia.UpdateType(select);
        }


    }
}
