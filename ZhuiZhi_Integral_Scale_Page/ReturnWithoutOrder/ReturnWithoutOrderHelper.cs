using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.ReturnWithoutOrder
{
    public class ReturnWithoutOrderHelper
    {

        //<summary>               
        //按比例缩放页面及控件
        //</summary>
        private static AutoSizeFormUtil asf = new AutoSizeFormUtil();
        public static void ShowFormReturnWithoutOrder()
        {
            try
            {
                FormReturnWithoutOrder frmreturn = new FormReturnWithoutOrder();
                asf.AutoScaleControlTest(frmreturn, 1178, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                frmreturn.Location = new System.Drawing.Point(0, 0);

                frmreturn.ShowDialog();

                frmreturn.Dispose();

                Application.DoEvents();
            }
            catch { }
        }


        public static bool ShowFormRetunCash(Cart cart,Member member,ReturnType returntype)
        {
            try
            {
                BackHelper.ShowFormBackGround();
                FormReturnCash frmcash = new FormReturnCash(cart,member,returntype);
                asf.AutoScaleControlTest(frmcash, 600, 300, 600 * MainModel.midScale, 300 * MainModel.midScale, true);
                frmcash.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmcash.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmcash.Height) / 2);
                frmcash.TopMost = true;

                frmcash.ShowDialog();

                frmcash.Dispose();
                Application.DoEvents();
                BackHelper.HideFormBackGround();
                return frmcash.DialogResult == DialogResult.OK;
            }
            catch
            {
                BackHelper.HideFormBackGround();
                return false;
            }
        }

        public static bool ShowFormRetunCashOrBalance(Cart cart, Member member)
        {
            try
            {
                BackHelper.ShowFormBackGround();
                FormReturnCashOrBalance frmcash = new FormReturnCashOrBalance(cart,member);
                asf.AutoScaleControlTest(frmcash, 600, 300, 600 * MainModel.midScale, 300 * MainModel.midScale, true);
                frmcash.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmcash.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmcash.Height) / 2);
                frmcash.TopMost = true;

                frmcash.ShowDialog();

                frmcash.Dispose();
                Application.DoEvents();
                BackHelper.HideFormBackGround();
                return frmcash.DialogResult == DialogResult.OK;
            }
            catch
            {
                BackHelper.HideFormBackGround();
                return false;
            }
        }


        public static string ShowFormCheckSmCode(string name,string phone){

            try
            {
              
                FormCheckSmCode frm = new FormCheckSmCode(name,phone);
                asf.AutoScaleControlTest(frm, 600, 300, 600 * MainModel.midScale, 300 * MainModel.midScale, true);
                frm.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frm.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frm.Height) / 2);
                frm.TopMost = true;

                frm.ShowDialog();

                frm.Dispose();
                Application.DoEvents();
                return frm.smscode;
            }
            catch
            {
                return "";
            }

        }



        static FormReturnWithoutOrderMedia frmMedia;
        public static void ShowFormReturnWithoutMedia()
        {
            try
            {
                frmMedia = new FormReturnWithoutOrderMedia();
                asf.AutoScaleControlTest(frmMedia, 1180, 760, Screen.AllScreens[1].Bounds.Width, Screen.AllScreens[1].Bounds.Height, true);
                frmMedia.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width , 0);
                frmMedia.TopMost = true;

                frmMedia.Show();
            }
            catch { }
        }

        public static void CloseFormReturnWithoutMedia()
        {
            try
            {
                frmMedia.Close();
                Application.DoEvents();
            }
            catch { }
        }

        public static void UpdateMediaCart(Cart cart,Member member)
        {
            try
            {
                frmMedia.UpdateForm(cart,member);
            }
            catch { }
        }

    }
}
