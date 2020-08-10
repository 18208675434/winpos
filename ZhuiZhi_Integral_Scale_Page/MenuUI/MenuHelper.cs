using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MenuUI
{
    public class MenuHelper
    {
        //<summary>
        //按比例缩放页面及控件
        //</summary>
        private static AutoSizeFormUtil asf = new AutoSizeFormUtil();


        public delegate void DataRecHandleDelegate(ToolType tooltype);
        /// <summary>
        /// 数据接收事件
        /// </summary>
        public event DataRecHandleDelegate DataReceiveHandle;

        private static FormToolMainScale frmtoolmainscale = null;

        public static void IniFormToolMainScale()
        {
            try
            {
                if (frmtoolmainscale != null)
                {
                    try
                    {
                        frmtoolmainscale.Dispose();
                    }
                    catch { }                  
                }

                frmtoolmainscale = new FormToolMainScale();
                asf.AutoScaleControlTest(frmtoolmainscale, 210, 180, 210 * MainModel.midScale, 480 * MainModel.midScale, true);
                frmtoolmainscale.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width - frmtoolmainscale.Width - 15, Convert.ToInt16(60 * MainModel.midScale) + 10);
                frmtoolmainscale.TopMost = true;

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("初始化单独秤菜单页面异常"+ex.Message);
            }
        }

        public static void ShowFomrToolMainScale()
        {
            try
            {

                if (frmtoolmainscale == null)
                {
                    IniFormToolMainScale();
                }

                frmtoolmainscale.Show();

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("显示单独秤菜单页面异常" + ex.Message);
            }
        }


    }
}
