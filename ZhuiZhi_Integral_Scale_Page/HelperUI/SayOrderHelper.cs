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


        public static void ShowFormToast(string msg)
        {

            System.ComponentModel.BackgroundWorker bk = new System.ComponentModel.BackgroundWorker();
            bk.DoWork += ShowFormToast_DoWork;
            bk.RunWorkerAsync(msg);


            //try
            //{
            //    if (frmsayorder == null || frmsayorder.IsDisposed)
            //    {
            //        IniFormToast();
            //    }
            //    frmsayorder.UpdateSayMsg(msg);
            //    frmsayorder.Show();

            //    Delay.Start(1500);

            //    frmsayorder.Hide();
            //}
            //catch (Exception ex)
            //{
            //    frmsayorder = null;
            //    LogManager.WriteLog("显示toast异常" + ex.Message);
            //}
        }

        public static void ShowFormToast_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {

                string saymsg = e.Argument as string;
                FormSayOrder frmsayorder = new FormSayOrder(saymsg);
                 frmsayorder.TopMost = true;
                 frmsayorder.Show();
                 System.Windows.Forms.Application.DoEvents();
                 System.Threading.Thread.Sleep(2000);

                 frmsayorder.Close();

            }
            catch { }
        }



    }
}
