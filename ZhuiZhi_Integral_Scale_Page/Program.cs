using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.BaseUI;
using System.IO;

namespace ZhuiZhi_Integral_Scale_UncleFruit
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            //获取当前进程的一个伪句柄
            System.Diagnostics.Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();

            //获取包含当前进程的一个列表
            System.Diagnostics.Process[] processList = System.Diagnostics.Process.GetProcessesByName(currentProcess.ProcessName);

            //如果前进程已经存在
            if (processList.Length > 1)
            {
                if (MessageBox.Show("检测到系统已在运行，点击确定将重新启动？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                    != DialogResult.OK)
                {
                    return;
                }
                else
                {
                    //是否当前 进程， 如果之前有允许 则排名靠后
                    bool iscurrent = true;
                    foreach (System.Diagnostics.Process process in processList)
                    {
                        if (process.ProcessName == currentProcess.ProcessName)
                        {
                            if (iscurrent)
                            {
                                iscurrent = false;
                            }
                            else
                            {
                                process.Kill();
                            }
                            
                        }

                    }
                }
                    
            }

            try
            {
                File.Copy(@"C:\iSmartSystem\pos_ad_dll.dll", AppDomain.CurrentDomain.BaseDirectory + "\\pos_ad_dll.dll", true);
            }
            catch { }

            //处理未捕获的异常
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            //处理非UI线程异常
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            //Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            UpdateSqlLite();
            string isoffline = "0";
            try
            {
                isoffline = INIManager.GetIni("System", "IsOffLine", ZhuiZhi_Integral_Scale_UncleFruit.Model.MainModel.IniPath);
            }
            catch { }

            
            Application.Run(new FormIni());


            //Application.Run(new ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.FormTestHttp());
        }

        /// <summary>
        /// 是否退出应用程序
        /// </summary>
        static bool glExitApp = false;

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            LogManager.WriteLog("CurrentDomain_UnhandledException" );
            LogManager.WriteLog("IsTerminating : " + e.IsTerminating.ToString());
            LogManager.WriteLog(e.ExceptionObject.ToString());

            try {
                ZhuiZhi_Integral_Scale_UncleFruit.Model.MainModel.ShowTask();
            }
            catch { }

            //等待两秒处理失败强制退出
            int waitingcount = 0;

            while (true)
            {//循环处理，否则应用程序将会退出
                waitingcount++;
                if (glExitApp && waitingcount > 50)
                {
                    //标志应用程序可以退出，否则程序退出后，进程仍然在运行
                    LogManager.WriteLog("ExitApp");
                    return;
                }
                System.Threading.Thread.Sleep(100);
            };

            //while (true)
            //{//循环处理，否则应用程序将会退出
            //    waitingcount++;
            //    if (glExitApp && waitingcount>20)
            //    {
            //        //标志应用程序可以退出，否则程序退出后，进程仍然在运行
            //        LogManager.WriteLog("ExitApp");
            //        return;
            //    }
            //    System.Threading.Thread.Sleep(100);
            //};
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            LogManager.WriteLog("Application_ThreadException:" +
                e.Exception.Message +"  "+e.Exception.StackTrace);

            //throw new NotImplementedException();
        }

        static Maticsoft.BLL.GlobalBLL globalbll = new Maticsoft.BLL.GlobalBLL();
        static void UpdateSqlLite()
        {
            try
            {
                globalbll.UpdateDbProduct();

            }
            catch
            {

            }

        }
    }
}
