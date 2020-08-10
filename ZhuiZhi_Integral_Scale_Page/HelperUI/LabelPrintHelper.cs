using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.HelperUI
{
    public class LabelPrintHelper
    {
        private static FormLabelPrint frmlabelprint = null;

        public static bool LabelPrint(Product pro)
        {
            try
            {
                if (frmlabelprint == null || frmlabelprint.IsDisposed)
                {
                    frmlabelprint = new FormLabelPrint();
                }

                return frmlabelprint.PrintLabel(pro);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("打印标签异常"+ex.Message);
                return false;
            }
        }
    }
}
