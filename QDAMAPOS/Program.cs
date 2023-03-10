using QDAMAPOS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace QDAMAPOS
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
                if (MessageBox.Show("检测到系统已在运行，不允许重复运行系统？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                    != DialogResult.OK)
                    return;
                return;
            }

            //处理未捕获的异常
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            //处理非UI线程异常
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

           // Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string isoffline = "0";
            try
            {
                 isoffline = INIManager.GetIni("System", "IsOffLine", QDAMAPOS.Model.MainModel.IniPath);
            }
            catch { }

            if (isoffline == "1")
            {
                Application.Run(new frmLoginOffLine());
            }
            else
            {
                Application.Run(new frmLogin());
            }
            
            //Application.Run(new frmTest());
        }

        /// <summary>
        /// 是否退出应用程序
        /// </summary>
        static bool glExitApp = true;

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            LogManager.WriteLog("CurrentDomain_UnhandledException");
            LogManager.WriteLog("IsTerminating : " + e.IsTerminating.ToString());
            LogManager.WriteLog(e.ExceptionObject.ToString());

            try {
                QDAMAPOS.Model.MainModel.ShowTask();
            }
            catch { }

            while (true)
            {//循环处理，否则应用程序将会退出
                if (glExitApp)
                {//标志应用程序可以退出，否则程序退出后，进程仍然在运行
                    //MessageBox.Show("异常，系统将自动关闭！");
                    LogManager.WriteLog("ExitApp");
                    return;
                }
                System.Threading.Thread.Sleep(100);
            };
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            LogManager.WriteLog("Application_ThreadException:" +
                e.Exception.Message);

            //throw new NotImplementedException();
        }
    }
}
