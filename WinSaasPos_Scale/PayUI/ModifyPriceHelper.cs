using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinSaasPOS_Scale.Common;
using WinSaasPOS_Scale.HelperUI;
using WinSaasPOS_Scale.Model;

namespace WinSaasPOS_Scale.PayUI
{
    public class ModifyPriceHelper
    {

        //<summary>
        //按比例缩放页面及控件
        //</summary>
        private static AutoSizeFormUtil asf = new AutoSizeFormUtil();
        private static FormModifyPrice frmmoodifyprice = null;

        private static Cart ParaCart = null;

        public static void IniForm()
        {
            try
            {
                if (frmmoodifyprice != null)
                {
                    try
                    {
                        frmmoodifyprice.Close();
                        frmmoodifyprice.Dispose();
                    }
                    catch (Exception ex)
                    {

                    }

                }

                frmmoodifyprice = new FormModifyPrice();
                asf.AutoScaleControlTest(frmmoodifyprice, 380, 480, 380 * MainModel.wScale, 480 * MainModel.hScale, true);
                frmmoodifyprice.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmmoodifyprice.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmmoodifyprice.Height) / 2);
                frmmoodifyprice.TopMost = true;

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("初始化确认弹窗异常" + ex.Message);
            }
        }

        public static decimal ShowForm(decimal  totalpaymentbeforefix)
        {
            try
            {
                BackHelper.ShowFormBackGround();
                if (frmmoodifyprice == null || frmmoodifyprice.IsDisposed)
                {
                    IniForm();
                }
                frmmoodifyprice.UpInfo(totalpaymentbeforefix);
                frmmoodifyprice.ShowDialog();

                BackHelper.HideFormBackGround();

                return frmmoodifyprice.fixpricetotal;
            }
            catch (Exception ex)
            {
                BackHelper.HideFormBackGround();
                frmmoodifyprice = null;
                LogManager.WriteLog("确认弹窗出现异常" + ex.Message);
                return -1;
            }
        }
    }
}
