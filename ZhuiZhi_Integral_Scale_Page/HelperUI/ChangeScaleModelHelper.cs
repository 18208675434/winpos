using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.HelperUI
{
    public class ChangeScaleModelHelper
    {

        //<summary>
        //按比例缩放页面及控件
        //</summary>
        private static AutoSizeFormUtil asf = new AutoSizeFormUtil();
        private static FormChangeScaleModel frmchangescalemodel = null;

        public static void IniFormChangeScaleModel()
        {
            try
            {
                if (frmchangescalemodel != null)
                {
                    try
                    {
                        frmchangescalemodel.Dispose();
                    }
                    catch { }
                }

                frmchangescalemodel = new FormChangeScaleModel();
                asf.AutoScaleControlTest(frmchangescalemodel, 600, 250, 600 * MainModel.midScale, 250 * MainModel.midScale, true);
                frmchangescalemodel.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmchangescalemodel.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmchangescalemodel.Height) / 2);
                frmchangescalemodel.TopMost = true;

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("初始化确认弹窗异常" + ex.Message);
            }
        }

      
        public static bool Confirm(ChangeModel model)
        {
            try
            {
                BackHelper.ShowFormBackGround();
                if (frmchangescalemodel == null || frmchangescalemodel.IsDisposed)
                {
                    IniFormChangeScaleModel();
                }
                frmchangescalemodel.UpInfo(model);
                frmchangescalemodel.ShowDialog();

                BackHelper.HideFormBackGround();
                return frmchangescalemodel.DialogResult == DialogResult.OK;
            }
            catch (Exception ex)
            {
                BackHelper.HideFormBackGround();
                frmchangescalemodel = null;
                LogManager.WriteLog("确认弹窗出现异常" + ex.Message);
                return false;
            }
        }

    }
}
