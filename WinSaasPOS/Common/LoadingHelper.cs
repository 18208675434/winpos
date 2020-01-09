using WinSaasPOS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading;
using System.Windows.Forms;

namespace WinSaasPOS.Common
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

                    MainModel.HideTask();
                    ParameterizedThreadStart Pts = new ParameterizedThreadStart(LoadingHelper.ShowForm);
                    Thread thread = new Thread(Pts);
                    thread.IsBackground = true;
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start("加载中...");

                }
                catch (Exception ex)
                {
                    try
                    {
                        CloseFormInternal();
                    }
                    catch { }
                    LogManager.WriteLog("显示等待窗口异常"+ex.Message);
                }
            }

              public static void ShowLoadingScreen(string msg)
            {
                  try{
                      // Make sure it is only launched once.

                if (loadingForm != null)
                    return;
                MainModel.HideTask();
                ParameterizedThreadStart Pts = new ParameterizedThreadStart(LoadingHelper.ShowForm);
                Thread thread = new Thread(Pts);
                thread.IsBackground = true;
                thread.SetApartmentState(ApartmentState.STA);
                //thread.Priority = ThreadPriority.Lowest;
                thread.Start(msg);
                
                  }
                  catch (Exception ex)
                  {
                      try
                      {
                          CloseFormInternal();
                      }
                      catch { }
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
                    loadingForm.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - loadingForm.Width) / 2, (Screen.AllScreens[0].Bounds.Height - loadingForm.Height) / 2);
                    //loadingForm.TopMost = true;
                    loadingForm.Opacity = 0.5d;
                    loadingForm.ShowDialog();
                }
                catch (Exception ex)
                {
                    try
                    {
                        CloseFormInternal();
                    }
                    catch { }
                    LogManager.WriteLog("显示等待窗口异常" + ex.Message);
                }

            }

            private static void ShowForm(object obj)
            {
                try { 

                if (loadingForm != null)
                {
                    return;
                    loadingForm.closeOrder();
                   

                    loadingForm = null;
                }

                loadingForm = new frmLoading(obj.ToString());
                loadingForm.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - loadingForm.Width) / 2, (Screen.AllScreens[0].Bounds.Height - loadingForm.Height) / 2);
                //loadingForm.Size = new System.Drawing.Size(50,50);
                loadingForm.TopMost = true;
                loadingForm.Opacity = 0.5d;
                loadingForm.ShowDialog();
                }
                catch (Exception ex)
                {
                    try
                    {
                        CloseFormInternal();
                    }
                    catch { }
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
                //MainModel.ShowTaskThread();
                Thread threadItemExedate = new Thread(LoadingHelper.CloseFormThread);
                threadItemExedate.IsBackground = true;
                threadItemExedate.Start();
            }


            public static void CloseFormThread()
            {
                try
                {
                  //  Thread.Sleep(50); //可能到这里线程还未起来，所以进行延时，可以确保线程起来，彻底关闭窗口

                    if (loadingForm != null)
                    {

                        lock (syncLock)
                        {
                          //  Thread.Sleep(50);

                            if (loadingForm != null)
                            {
                               // Thread.Sleep(50);  //通过三次延时，确保可以彻底关闭窗口
                                loadingForm.Invoke(new CloseDelegate(LoadingHelper.CloseFormInternal));
                            }
                        }
                    }
                   
                }
                catch
                {
                    Thread.Sleep(50);
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
                    //loadingForm.closeOrder();
                    loadingForm.Close();

                    loadingForm = null;
                }
                catch (Exception ex)
                {
                    try
                    {
                        Delay.Start(150);
                        loadingForm.closeOrder();

                        loadingForm = null;
                    }
                    catch (Exception ex1)
                    {
                        try { Delay.Start(100); loadingForm.Close(); }
                        catch { }
                        LogManager.WriteLog("关闭等待窗口异常1" + ex1.Message);
                    }

                    LogManager.WriteLog("关闭等待窗口异常" + ex.Message);
                }

            }


    }
}
