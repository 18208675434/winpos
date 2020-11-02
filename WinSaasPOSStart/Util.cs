using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinSaasPosStart
{

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
                    logPath = AppDomain.CurrentDomain.BaseDirectory + "StartLog\\";
                    if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "StartLog\\"))
                        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "StartLog\\");
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

    ///// <summary>
    ///// Access数据库读写函数
    ///// </summary>
    //public class AccessHelper
    //{
    //    /// <summary>
    //    /// Access数据库读取
    //    /// </summary>
    //    /// <param name="conn">数据库连接</param>
    //    /// <param name="sqlstr">要执行的SQL语句</param>
    //    /// <param name="success">读取成功状态</param>
    //    /// <param name="count">读取成功的行数</param>
    //    /// <returns></returns>
    //    public static DataTable DataReader(OleDbConnection conn, string sqlstr, ref bool success, ref int count)
    //    {
    //        DataTable dt = new DataTable();
    //        try
    //        {
    //            DataRow dr;

    //            //打开连接
    //            if (conn.State != ConnectionState.Open)
    //            {
    //                conn.Open();
    //            }

    //            //建立SQL查询   
    //            OleDbCommand odCommand = conn.CreateCommand();

    //            odCommand.CommandText = sqlstr;

    //            //建立读取   
    //            OleDbDataReader odrReader = odCommand.ExecuteReader();

    //            //查询并显示数据   
    //            int size = odrReader.FieldCount;

    //            for (int i = 0; i < size; i++)
    //            {
    //                DataColumn dc;
    //                dc = new DataColumn(odrReader.GetName(i));
    //                dt.Columns.Add(dc);
    //            }

    //            while (odrReader.Read())
    //            {
    //                dr = dt.NewRow();
    //                for (int i = 0; i < size; i++)
    //                {
    //                    dr[odrReader.GetName(i)] = odrReader[odrReader.GetName(i)].ToString();
    //                }
    //                dt.Rows.Add(dr);
    //                count++;
    //            }

    //            //关闭连接   
    //            odrReader.Close();
    //            success = true;
    //            return dt;
    //        }
    //        catch
    //        {
    //            success = false;
    //            return dt;
    //        }
    //    }

    //    /// <summary>
    //    /// Access数据库读取
    //    /// </summary>
    //    /// <param name="conn">数据库连接</param>
    //    /// <param name="sqlstr">要执行的SQL语句</param>
    //    /// <returns>结果集</returns>
    //    public static DataTable DataReader(OleDbConnection conn, string sqlstr)
    //    {
    //        DataTable dt = new DataTable();

    //        try
    //        {
    //            DataRow dr;

    //            //打开连接
    //            if (conn.State != ConnectionState.Open)
    //            {
    //                conn.Open();
    //            }

    //            //建立SQL查询   
    //            OleDbCommand odCommand = conn.CreateCommand();

    //            odCommand.CommandText = sqlstr;

    //            //建立读取   
    //            OleDbDataReader odrReader = odCommand.ExecuteReader();

    //            //查询并显示数据   
    //            int size = odrReader.FieldCount;

    //            for (int i = 0; i < size; i++)
    //            {
    //                DataColumn dc;
    //                dc = new DataColumn(odrReader.GetName(i));
    //                dt.Columns.Add(dc);
    //            }

    //            while (odrReader.Read())
    //            {
    //                dr = dt.NewRow();
    //                for (int i = 0; i < size; i++)
    //                {
    //                    dr[odrReader.GetName(i)] = odrReader[odrReader.GetName(i)].ToString();
    //                }
    //                dt.Rows.Add(dr);
    //            }

    //            //关闭连接   
    //            odrReader.Close();
    //            return dt;
    //        }
    //        catch
    //        {
    //            return dt;
    //        }
    //    }

    //    /// <summary>
    //    /// 用于返回首行首列的值(仅返回一条数据)
    //    /// </summary>
    //    /// <param name="conn">数据库连接</param>
    //    /// <param name="sqlstr">sql语句</param>
    //    /// <returns>首行首列</returns>
    //    public static object GetTopData(OleDbConnection conn, string sqlstr)
    //    {
    //        object obj = new object();

    //        try
    //        {
    //            //打开连接
    //            if (conn.State != ConnectionState.Open)
    //            {
    //                conn.Open();
    //            }

    //            //建立SQL查询
    //            OleDbCommand odCommand = conn.CreateCommand();

    //            odCommand.CommandText = sqlstr;

    //            obj = odCommand.ExecuteScalar();

    //            //关闭连接
    //            return obj;
    //        }
    //        catch
    //        {
    //            return obj;
    //        }
    //    }

    //    /// <summary>
    //    /// Access增删改
    //    /// </summary>
    //    /// <param name="conn">数据库连接</param>
    //    /// <param name="sqlstr">数据库语句</param>
    //    /// <returns></returns>
    //    public static int ExecuteAccess(OleDbConnection conn, string sqlstr)
    //    {
    //        try
    //        {

    //            if (conn.State != ConnectionState.Open)
    //            {
    //                //防止多线程调用时出现连接已打开问题
    //                try
    //                {
    //                    conn.Open();
    //                }
    //                catch { }
    //            }
    //            OleDbCommand cmd = new OleDbCommand();

    //            int num = 0;//执行后返回的影响行数

    //            cmd.CommandText = sqlstr;

    //            cmd.Connection = conn;

    //            num = cmd.ExecuteNonQuery();

    //            return num;
    //        }
    //        catch (Exception)
    //        {
    //            throw;
    //        }
    //        //finally
    //        //    {
    //        //    if(conn.State==ConnectionState.Open)
    //        //        conn.Close();
    //        //    }

    //    }

    //    /// <summary>
    //    /// 创建加密Access数据库
    //    /// </summary>
    //    /// <param name="mdbPath">数据库路径</param>
    //    /// <returns></returns>
    //    public static bool CreateMDBDataBase(string connString)
    //    {
    //        try
    //        {
    //            CatalogClass cat = new CatalogClass();
    //            cat.Create(connString);
    //            cat = null;
    //            return true;
    //        }
    //        catch
    //        {
    //            return false;
    //        }
    //    }

    //    /// <summary>
    //    /// 新建mdb的表
    //    /// </summary>
    //    /// <param name="mdbPath">数据库路径</param>
    //    /// <param name="tableName">表名</param>
    //    /// <param name="mdbHead">是一个ArrayList，存储的是table表中的具体列名</param>
    //    /// <returns></returns>
    //    public static bool CreateMDBTable(string connString, string mdbPath, string tableName, ArrayList mdbHead)
    //    {
    //        try
    //        {
    //            CatalogClass cat = new CatalogClass();

    //            Connection cn = new Connection();
    //            cn.Open(connString, null, null, -1);
    //            cat.ActiveConnection = cn;

    //            //新建一个表   
    //            TableClass tbl = new TableClass();
    //            tbl.ParentCatalog = cat;
    //            tbl.Name = tableName;

    //            int size = mdbHead.Count;

    //            for (int i = 0; i < size; i++)
    //            {
    //                //增加一个文本字段
    //                ColumnClass col2 = new ColumnClass();
    //                col2.ParentCatalog = cat;
    //                col2.Name = mdbHead[i].ToString();//列的名称
    //                col2.Properties["Jet OLEDB:Allow Zero Length"].Value = false;
    //                tbl.Columns.Append(col2, ADOX.DataTypeEnum.adVarWChar, 500);
    //            }
    //            cat.Tables.Append(tbl);//把表加入数据库(非常重要)
    //            tbl = null;
    //            cat = null;
    //            cn.Close();
    //            return true;
    //        }
    //        catch
    //        {
    //            return false;
    //        }
    //    }

    //    /// <summary>
    //    /// 压缩数据库
    //    /// </summary>
    //    /// <param name="pathMdb">源数据库目录</param>
    //    /// <param name="pathTemp">临时存放目录</param>
    //    public static void CompactAccessDB(string pathMdb, string pathTemp)
    //    {
    //        try
    //        {
    //            string strConnMdb = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + pathMdb + ";Jet OLEDB:Database Password = WinSafe%%304#&305#";
    //            string strConnTemp = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + pathTemp + ";Jet OLEDB:Database Password = WinSafe%%304#&305#";

    //            JetEngine je = new JetEngine();

    //            je.CompactDatabase(strConnMdb, strConnTemp);//压缩

    //            File.Copy(pathTemp, pathMdb, true);//将压缩后的数据库覆盖原数据库
    //            File.Delete(pathTemp);//删除压缩后的临时数据库
    //        }
    //        catch (Exception)
    //        {
    //            throw;
    //        }
    //    }
    //}


    public class ZipFileUtil
    {


        public static string unZipFile(string TargetFile, string fileDir)
        {
            string rootFile = " ";
            try
            {
                //读取压缩文件(zip文件)，准备解压缩
                ZipInputStream s = new ZipInputStream(File.OpenRead(TargetFile.Trim()));
                ZipEntry theEntry;
                string path = fileDir;
                //解压出来的文件保存的路径

                string rootDir = " ";
                //根目录下的第一个子文件夹的名称
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    rootDir = Path.GetDirectoryName(theEntry.Name);
                    //得到根目录下的第一级子文件夹的名称
                    if (rootDir.IndexOf("\\") >= 0)
                    {
                        rootDir = rootDir.Substring(0, rootDir.IndexOf("\\") + 1);
                    }
                    string dir = Path.GetDirectoryName(theEntry.Name);
                    //根目录下的第一级子文件夹的下的文件夹的名称
                    string fileName = Path.GetFileName(theEntry.Name);
                    //根目录下的文件名称
                    //if (dir != " ")
                    ////创建根目录下的子文件夹,不限制级别
                    //{
                    //    if (!Directory.Exists(fileDir + "\\" + dir))
                    //    {
                    //        path = fileDir + "\\" + dir;
                    //        //在指定的路径创建文件夹
                    //        Directory.CreateDirectory(path);
                    //    }
                    //}
                    //else if (dir == " " && fileName != "")
                    ////根目录下的文件
                    //{
                    //    path = fileDir;
                    //    rootFile = fileName;
                    //}
                    //else if (dir != " " && fileName != "")
                    ////根目录下的第一级子文件夹下的文件
                    //{
                    //    if (dir.IndexOf("\\") > 0)
                    //    //指定文件保存的路径
                    //    {
                    //        path = fileDir + "\\" + dir;
                    //    }
                    //}

                    //if (dir == rootDir)
                    ////判断是不是需要保存在根目录下的文件
                    //{
                    //    path = fileDir + "\\" + rootDir;
                    //}

                    //以下为解压缩zip文件的基本步骤
                    //基本思路就是遍历压缩文件里的所有文件，创建一个相同的文件。
                    if (fileName != String.Empty)
                    {
                        FileStream streamWriter = File.Create(path + "\\" + fileName);

                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }
                        streamWriter.Close();
                    }
                }
                s.Close();

                return rootFile;
            }
            catch (Exception ex)
            {
                return "1; " + ex.Message;
            }
        }

        public static void ZipFile(string strFile, string strZip)
        {
            if (strFile[strFile.Length - 1] != Path.DirectorySeparatorChar)
                strFile += Path.DirectorySeparatorChar;
            ZipOutputStream s = new ZipOutputStream(File.Create(strZip));
            s.SetLevel(6); // 0 - store only to 9 - means best compression
            zip(strFile, s, strFile);
            s.Finish();
            s.Close();
        }

        private static void zip(string strFile, ZipOutputStream s, string staticFile)
        {
            if (strFile[strFile.Length - 1] != Path.DirectorySeparatorChar) strFile += Path.DirectorySeparatorChar;
            Crc32 crc = new Crc32();
            string[] filenames = Directory.GetFileSystemEntries(strFile);
            foreach (string file in filenames)
            {
                if (Directory.Exists(file))
                {
                    zip(file, s, staticFile);
                }
                else // 否则直接压缩文件
                {
                    //打开压缩文件
                    FileStream fs = File.OpenRead(file);
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    string tempfile = file.Substring(staticFile.LastIndexOf("\\") + 1);
                    ZipEntry entry = new ZipEntry(tempfile);

                    entry.DateTime = DateTime.Now;
                    entry.Size = fs.Length;
                    fs.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    s.PutNextEntry(entry);

                    s.Write(buffer, 0, buffer.Length);
                }
            }
        }


    }

    /// <summary>
    /// INI文件管理（读、写、创建）
    /// </summary>
    public class INIManager
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">关键字</param>
        /// <param name="val">值</param>
        /// <param name="filePath">ini文件路径</param>
        /// <returns></returns>
        public static int SetIni(string section, string key, string val, string filePath)
        {
            if (!File.Exists(filePath))
            {
                return -1;
            }
            long result = WritePrivateProfileString(section, key, val, filePath);
            return (int)result;
        }

        /// <summary>
        /// 读取ini文件
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">关键字</param>
        /// <param name="val">值</param>
        /// <param name="filePath">ini文件路径</param>
        /// <returns></returns>
        public static string GetIni(string section, string key, string filePath)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }
            StringBuilder result = new StringBuilder(1024);
            GetPrivateProfileString(section, key, "", result, 1024, filePath);
            return result.ToString();
        }

        /// <summary>
        /// 创建INI文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>-1已存在 0 失败 1 成功</returns>
        public static int CreateIni(string filePath)
        {
            if (File.Exists(filePath))
            {
                return -1;
            }
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Create);
                fs.Close();
                return 1;
            }
            catch
            {
                return 0;
            }
        }
    }



    /// <summary>
    /// 其他函数
    /// </summary>
    public class Other
    {
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        /// <summary>
        /// 内存清理
        /// </summary>
        public static void CrearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }



    
    }




}
