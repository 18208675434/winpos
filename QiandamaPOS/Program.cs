using QiandamaPOS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QiandamaPOS
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

            ////处理未捕获的异常
            //Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            ////处理UI线程异常
            //Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            ////处理非UI线程异常
            //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin());
            //Application.Run(new frmTest());

        }

        /// <summary>
        /// 是否退出应用程序
        /// </summary>
        static bool glExitApp = false;

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            LogManager.WriteLog("CurrentDomain_UnhandledException");
            LogManager.WriteLog("IsTerminating : " + e.IsTerminating.ToString());
            LogManager.WriteLog(e.ExceptionObject.ToString());

            while (true)
            {//循环处理，否则应用程序将会退出
                if (glExitApp)
                {//标志应用程序可以退出，否则程序退出后，进程仍然在运行
                    LogManager.WriteLog("ExitApp");
                    return;
                }
                System.Threading.Thread.Sleep(2 * 1000);
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
