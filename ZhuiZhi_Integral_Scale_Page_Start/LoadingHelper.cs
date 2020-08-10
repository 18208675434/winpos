using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit_Start;

namespace ZhuiZhi_Integral_Scale_UncleFruit_Start
{

        public class LoadingHelper
        {

            #region 相关变量定义

            /// <summary>

            /// 定义委托进行窗口关闭

            /// </summary>

            private delegate void CloseDelegate();

            private static frmLoading loadingForm;

            private static readonly Object syncLock = new Object();  //加锁使用



            #endregion
            


            //private LoadingHelper()

            //{



            //}



            /// <summary>

            /// 显示loading框

            /// </summary>

            public static void ShowLoadingScreen()
            {

                try
                {
                    // Make sure it is only launched once.

                    if (loadingForm != null)
                        return;

                  

                    Thread thread = new Thread(new ThreadStart(LoadingHelper.ShowForm));

                    thread.IsBackground = true;

                    thread.SetApartmentState(ApartmentState.STA);

                    thread.Start();
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog("显示等待窗口异常"+ex.Message);
                }

            }

              public static void ShowLoadingScreen(string msg)
            {
                  try{
                      // Make sure it is only launched once.

                if (loadingForm != null)

                    return;

                ParameterizedThreadStart Pts = new ParameterizedThreadStart(LoadingHelper.ShowForm);
                Thread thread = new Thread(Pts);
                thread.IsBackground = true;
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start(msg);

                //Thread thread = new Thread(new ThreadStart(LoadingHelper.ShowForm));

                //thread.IsBackground = true;

                //thread.SetApartmentState(ApartmentState.STA);

                //thread.Start();
                  }
                  catch (Exception ex)
                  {
                      LogManager.WriteLog("显示等待窗口异常" + ex.Message);
                  }

            }

           

            /// <summary>

            /// 显示窗口

            /// </summary>

            private static void ShowForm()
            {
                try
                {
                    if (loadingForm != null)
                    {

                        loadingForm.closeOrder();

                        loadingForm = null;

                    }

                    loadingForm = new frmLoading();
                    loadingForm.Location = new System.Drawing.Point((Screen.PrimaryScreen.Bounds.Width - loadingForm.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - loadingForm.Height) / 2);
                    loadingForm.TopMost = true;
                    loadingForm.Opacity = 0.6d;
                    loadingForm.ShowDialog();
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog("显示等待窗口异常" + ex.Message);
                }

            }


            private static void ShowForm(object obj)
            {
                try { 

                if (loadingForm != null)
                {

                    loadingForm.closeOrder();

                    loadingForm = null;

                }

                loadingForm = new frmLoading(obj.ToString());

                loadingForm.Location = new System.Drawing.Point((Screen.PrimaryScreen.Bounds.Width - loadingForm.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - loadingForm.Height) / 2);
                loadingForm.TopMost = true;
                loadingForm.Opacity = 0.6d;
                loadingForm.ShowDialog();

                }
                catch (Exception ex)
                {
                    LogManager.WriteLog("显示等待窗口异常" + ex.Message);
                }

            }



            /// <summary>

            /// 关闭窗口

            /// </summary>

            public static void CloseForm()
            {
                //try
                //{
                //    loadingForm.Close();
                //}
                //catch
                //{

                //}
                
                Thread threadItemExedate = new Thread(LoadingHelper.CloseFormThread);
                threadItemExedate.IsBackground = true;
                threadItemExedate.Start();

            }



            public static void CloseFormThread()
            {
                try
                {
                    Thread.Sleep(50); //可能到这里线程还未起来，所以进行延时，可以确保线程起来，彻底关闭窗口

                    if (loadingForm != null)
                    {

                        lock (syncLock)
                        {
                            Thread.Sleep(50);

                            if (loadingForm != null)
                            {
                                Thread.Sleep(50);  //通过三次延时，确保可以彻底关闭窗口
                                loadingForm.Invoke(new CloseDelegate(LoadingHelper.CloseFormInternal));
                            }
                        }
                    }
                   
                }
                catch
                {
                    LoadingHelper.CloseFormInternal();
                }

            }



            /// <summary>

            /// 关闭窗口，委托中使用

            /// </summary>

            private static void CloseFormInternal()
            {
                try
                {
                    loadingForm.closeOrder();

                    loadingForm = null;
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog("关闭等待窗口异常" + ex.Message);
                }

            }


    }
}
