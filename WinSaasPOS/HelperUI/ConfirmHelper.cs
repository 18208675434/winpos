using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinSaasPOS.Common;
using WinSaasPOS.Model;

namespace WinSaasPOS.HelperUI
{
   
    public class ConfirmHelper
    {
        //<summary>
        //按比例缩放页面及控件
        //</summary>
        private static AutoSizeFormUtil asf = new AutoSizeFormUtil();
        private static FormConfirm frmconfirm = null;

        public static void IniFormConfirm()
        {
            try
            {
                if (frmconfirm != null)
                {
                    frmconfirm.Dispose();
                }

                frmconfirm = new FormConfirm();
                asf.AutoScaleControlTest(frmconfirm, 700, 200, 700 * MainModel.midScale, 200 * MainModel.midScale, true);
                frmconfirm.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmconfirm.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmconfirm.Height) / 2);
                frmconfirm.TopMost = true;

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("初始化确认弹窗异常"+ex.Message);
            }
        }

        public static bool Confirm(string title)
        {
            return Confirm(title, "", true);
        }
        public static bool Confirm(string title, string msg)
        {
            return Confirm(title, msg, true);
        }
        public static bool Confirm(string title, string msg, bool needcancel)
        {
            try
            {
                BackHelper.ShowFormBackGround();
                if (frmconfirm == null)
                {
                    frmconfirm = new FormConfirm();
                    asf.AutoScaleControlTest(frmconfirm, 700, 200, 700 * MainModel.midScale, 200 * MainModel.midScale, true);
                    frmconfirm.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmconfirm.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmconfirm.Height) / 2);
                    frmconfirm.TopMost = true;

                }
                frmconfirm.UpInfo(title,msg,needcancel);
                frmconfirm.ShowDialog();

                BackHelper.HideFormBackGround();
                return frmconfirm.DialogResult == DialogResult.OK;
            }
            catch (Exception ex)
            {
                BackHelper.HideFormBackGround();
                frmconfirm = null;
                LogManager.WriteLog("确认弹窗出现异常"+ex.Message);
                return false;
            }
        }

    }
}
