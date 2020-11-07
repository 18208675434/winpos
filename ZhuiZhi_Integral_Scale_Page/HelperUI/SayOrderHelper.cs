using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;

namespace ZhuiZhi_Integral_Scale_UncleFruit.HelperUI
{
    public class SayOrderHelper
    {
       // public static FormSayOrder frmsayorder = null;


        public static void IniFormToast()
        {
            //try
            //{
            //    if (frmsayorder != null)
            //    {
            //        frmsayorder.Dispose();
            //    }
            //    frmsayorder = new FormSayOrder();
            //    frmsayorder.TopMost = true;
            //}
            //catch (Exception ex)
            //{
            //    frmsayorder = null;
            //    LogManager.WriteLog("初始订单提示窗异常" + ex.Message);
            //}
        }

        public delegate void deleteAutoToast(string msg);
        public static void ShowFormToast(string msg)
        {


            deleteAutoToast operation = new deleteAutoToast(ShowFormToast_DoWork);
            operation.Invoke(msg); //不能异步执行
        }

        public static void ShowFormToast_DoWork(string msg)
        {
            try
            {

                string saymsg = msg;
                FormSayOrder frmsayorder = new FormSayOrder(saymsg);
                 frmsayorder.TopMost = true;
                 frmsayorder.Show();
                 System.Windows.Forms.Application.DoEvents();
                 Delay.Start(2000);
                 frmsayorder.Close();

            }
            catch { }
        }



    }
}
