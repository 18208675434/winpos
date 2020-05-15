using WinSaasPOS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace WinSaasPOS
{
    static class Program
    {
        ///
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

            changedb();
            string isoffline = "0";
            try
            {
                isoffline = INIManager.GetIni("System", "IsOffLine", WinSaasPOS.Model.MainModel.IniPath);
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

            try
            {
                GlobalUtil.CloseOSK();
                WinSaasPOS.Model.MainModel.ShowTask();
            }
            catch { }
            //Application.Run(new frmLogin());
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

            try
            {
                GlobalUtil.CloseOSK();
                WinSaasPOS.Model.MainModel.ShowTask();
            }
            catch { }

            //等待两秒处理失败强制退出
            int waitingcount = 0;
            while (true)
            {//循环处理，否则应用程序将会退出
                waitingcount++;
                if (glExitApp && waitingcount > 20)
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

        public static void changedb()
        {

            try
            {
                string isexits = "select sql from sqlite_master where type = 'table' and name = 'DBPROMOTION_CACHE_BEAN'";

                object obj = Maticsoft.DBUtility.DbHelperSQLite.GetSingle(isexits);



                System.Collections.ArrayList lststring = new System.Collections.ArrayList();
                bool needadd = false;
                if (!obj.ToString().Contains("ONLYMEMBER"))
                {
                    lststring.Add("ALTER TABLE DBPROMOTION_CACHE_BEAN ADD COLUMN 'ONLYMEMBER' INTEGER");
                    needadd = true;
                }

                if (!obj.ToString().Contains("MEMBERTAGS"))
                {
                    lststring.Add("ALTER TABLE DBPROMOTION_CACHE_BEAN ADD COLUMN 'MEMBERTAGS' TEXT");
                    needadd = true;

                }

                if (!obj.ToString().Contains("PURCHASELIMIT"))
                {
                    lststring.Add("ALTER TABLE DBPROMOTION_CACHE_BEAN ADD COLUMN 'PURCHASELIMIT' INTEGER");
                    needadd = true;

                }
                if (needadd)
                {
                    Maticsoft.DBUtility.DbHelperSQLite.ExecuteSqlTran(lststring);

                }
            }
            catch (Exception ex)
            {

            }
        }


    }
}
