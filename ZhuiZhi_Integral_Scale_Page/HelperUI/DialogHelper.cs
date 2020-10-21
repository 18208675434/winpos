using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.HelperUI
{
    public class DialogHelper
    {
        //<summary>
        //按比例缩放页面及控件
        //</summary>
        private static AutoSizeFormUtil asf = new AutoSizeFormUtil();
        #region 编码录入弹框

        /// <summary>
        /// 编码录入弹框
        /// </summary>
        public static string ShowFormCode(string title, string waterText, int maxlen = 20)
        {
            try
            {
                BackHelper.ShowFormBackGround();
                FormCode formCode = new FormCode(title, waterText, maxlen);
                asf.AutoScaleControlTest(formCode, 1180, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                formCode.Location = new System.Drawing.Point(0, 0);
                formCode.TopMost = true;

                formCode.ShowDialog();
                formCode.Dispose();
                Application.DoEvents();
                return formCode.Code;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(title + "会员号弹窗异常" + ex.Message);
                return "";
            }
            finally
            {
                BackHelper.HideFormBackGround();
            }
        }

        #endregion
    }
}
