using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScaleTest
{
    /// <summary>
    /// 日志管理
    /// </summary>
    public class LogManager
    {
        private static string logPath = string.Empty;

        /// <summary>
        /// 属性：保存日志的文件夹
        /// </summary>
        public static string LogPath
        {
            get
            {
                if (logPath == string.Empty)
                {
                    logPath = AppDomain.CurrentDomain.BaseDirectory + "Log\\";
                }
                return logPath;
            }
            set { logPath = value; }
        }

        private static string logFielPrefix = string.Empty;

        /// <summary>
        /// 属性：日志文件前缀
        /// </summary>
        public static string LogFielPrefix
        {
            get { return logFielPrefix; }
            set { logFielPrefix = value; }
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="logFile">日志类型</param>
        /// <param name="msg">日志内容</param>
        public static void WriteLog(string logFile, string msg)
        {
            try
            {
                StreamWriter sw = File.AppendText(LogPath + LogFielPrefix + logFile + DateTime.Now.ToString("yyyyMMdd") + ".Log");

                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + msg);

                sw.Close();
            }
            catch
            { }
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        public static void WriteLog(string msg)
        {
            try
            {
                StreamWriter sw = File.AppendText(LogPath + LogFielPrefix + DateTime.Now.ToString("yyyyMMdd") + ".Log");

                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + msg);

                sw.Close();
            }
            catch
            { }
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="logFile">日志类型</param>
        /// <param name="msg">日志内容</param>
        public static void WriteLog(LogFile logFile, string msg)
        {
            WriteLog(logFile.ToString(), msg);
        }
    }

    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogFile
    {
        Trace,
        Warning,
        Error,
        SQL
    }

    /// <summary>
    /// 系统延时
    /// </summary>
    public class Delay
    {
        public static bool bDelay = true;

        /// <summary>
        /// 开始延时
        /// </summary>
        /// <param name="tms">延时时间（毫秒）</param>
        public static void Start(int tms)
        {
            long beginTime = DateTime.Now.Ticks;

            while (bDelay)
            {
                long endTime = DateTime.Now.Ticks;

                TimeSpan elapsedSpan = new TimeSpan(endTime - beginTime);

                if ((elapsedSpan.TotalMilliseconds) > tms)
                    break;

                Application.DoEvents();
            }
        }

        /// <summary>
        /// 停止延时
        /// </summary>
        public static void Stop()
        {
            bDelay = false;
        }
    }
}
