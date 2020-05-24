using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinSaasPOS_Scale.Common;
using WinSaasPOS_Scale.Model;

namespace WinSaasPOS_Scale.HelperUI
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
