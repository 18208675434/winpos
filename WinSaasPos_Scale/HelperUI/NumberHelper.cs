using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinSaasPOS_Scale.Common;
using WinSaasPOS_Scale.Model;

namespace WinSaasPOS_Scale.HelperUI
{
    public class NumberHelper
    {
        //<summary>
        //按比例缩放页面及控件
        //</summary>
        private static AutoSizeFormUtil asf = new AutoSizeFormUtil();
        private static FormNumber frmnumber = null;

        public static void IniFormNumber()
        {
            try
            {
                if (frmnumber != null)
                {
                    try
                    {
                        frmnumber.Close();
                        frmnumber.Dispose();
                    }
                    catch (Exception ex)
                    {

                    }                   
                }

                frmnumber = new FormNumber();
                asf.AutoScaleControlTest(frmnumber, 380, 480, 380 * MainModel.midScale, 480 * MainModel.midScale, true);
                frmnumber.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmnumber.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmnumber.Height) / 2);
                frmnumber.TopMost = true;

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("初始化确认弹窗异常" + ex.Message);
            }
        }


        public static string ShowFormNumber(string title,NumberType numbertype)
        {
              try
            {
                BackHelper.ShowFormBackGround();
                if (frmnumber == null || frmnumber.IsDisposed)
                {
                    IniFormNumber();
                }
                frmnumber.UpInfo(title,numbertype);
                frmnumber.ShowDialog();

                BackHelper.HideFormBackGround();
                return frmnumber.NumberValue;
            }
            catch (Exception ex)
            {
                BackHelper.HideFormBackGround();
                frmnumber = null;
                LogManager.WriteLog("数字弹窗出现异常"+ex.Message);
                return "";
            }
        }
    }


    public enum NumberType
    {
        None,
        BarCode,
        MemberCode,
        ProWeight,
        TareWeight
    }
}
