using WinSaasPOS_Scale.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WinSaasPOS_Scale.Common
{
    public class MsgHelper
    {

        #region 相关变量定义

        /// <summary>

        /// 定义委托进行窗口关闭

        /// </summary>

        private delegate void CloseDelegate();

        private static frmMsg loadingMsg;

        private static readonly Object syncLock = new Object();  //加锁使用



        #endregion




        public static void ShowForm(object obj)
        {

            if (loadingMsg != null)
            {

                loadingMsg.closeOrder();

                loadingMsg = null;

            }

            loadingMsg = new frmMsg(obj.ToString());

            loadingMsg.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - loadingMsg.Width) / 2, (Screen.AllScreens[0].Bounds.Height - loadingMsg.Height) / 2);
            loadingMsg.TopMost = true;
            loadingMsg.Opacity = 0.8d;
            loadingMsg.Show();
        }

        public static void AutoShowForm(object obj)
        {
            MainModel.HideTask();

            ParameterizedThreadStart Pts = new ParameterizedThreadStart(AutoShowFormThread);
            Thread thread = new Thread(Pts);
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start(obj);

        }
        public static void AutoShowFormThread(object obj)
        {
            try
            {
                ShowForm(obj);
                Delay.Start(5000);
                CloseForm();
            }
            catch { }
            finally { CloseForm(); }
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>

        public static void CloseForm()
        {
            Thread threadItemExedate = new Thread(MsgHelper.CloseFormThread);
            threadItemExedate.IsBackground = true;
            threadItemExedate.Start();

        }



        public static void CloseFormThread()
        {
            try
            {
                // Thread.Sleep(50); //可能到这里线程还未起来，所以进行延时，可以确保线程起来，彻底关闭窗口

                if (loadingMsg != null)
                {

                    lock (syncLock)
                    {

                        // Thread.Sleep(50);

                        if (loadingMsg != null)
                        {

                            Thread.Sleep(50);  //通过三次延时，确保可以彻底关闭窗口
                            MsgHelper.CloseFormInternal();
                            // loadingMsg.Invoke(new CloseDelegate(MsgHelper.CloseFormInternal));

                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Thread.Sleep(50);
                MsgHelper.CloseFormInternal();
            }

        }



        /// <summary>

        /// 关闭窗口，委托中使用

        /// </summary>

        private static void CloseFormInternal()
        {
            try
            {
                loadingMsg.closeOrder();

                loadingMsg = null;
            }
            catch (Exception ex)
            {

                try
                {
                    Delay.Start(150);
                    loadingMsg.closeOrder();

                    loadingMsg = null;
                }
                catch
                {
                    try { Delay.Start(100); loadingMsg.Close(); }
                    catch { }
                }
               // LogManager.WriteLog("关闭提示窗体异常" + ex.Message);
            }
        }


    }
}
