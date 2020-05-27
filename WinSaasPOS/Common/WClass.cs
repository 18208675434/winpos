using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WinSaasPOS.Common
{ /// <summary>
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

    /// <summary>
    /// SQL数据库读写
    /// </summary>
    public abstract class SqlHelper
    {
        /// <summary>
        /// 执行查询无查询结果(增删改)
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">sql命令类型(普通文本或存储过程)</param>
        /// <param name="commandText">sql语句或存储过程名</param>
        /// <param name="commandParameters">数目不定的sql命令参数</param>
        /// <returns>返回受影响的行数</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 执行查询无查询结果(增删改)
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="conn">an existing database connection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) using an existing SQL Transaction 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="trans">一个已存在的事务</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 返回一个数据读取器
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>A SqlDataReader containing the results</returns>
        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);

            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 返回首行首列的值
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        #region SqlTransaction示例
        //SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringProfile);
        //    conn.Open();
        //    SqlTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);

        //    try {
        //        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sqlDelete, parms1);
        //} 
        #endregion

        /// <summary>
        /// Execute a SqlCommand that returns the first column of the first record against an existing database connection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="conn">an existing database connection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 为执行数据库操作准备参数
        /// </summary>
        /// <param name="cmd">Sql命令对象</param>
        /// <param name="conn">Sql连接对象</param>
        /// <param name="trans">Sql事务</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdParms">Sql参数</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        /// <summary>
        /// 数据读取器转换为DataTable
        /// </summary>
        /// <param name="sqlDataReader">需要转换成DataTable的数据读取器</param>
        /// <returns>DataTable</returns>
        public static DataTable DataReader(SqlDataReader sqlDataReader)
        {
            try
            {
                //要返回的Table
                DataTable table = new DataTable();

                //数据读取器列数
                int size = sqlDataReader.FieldCount;

                for (int i = 0; i < size; i++)
                {
                    //定义数据列
                    DataColumn column;

                    //获取数据读取器中的列
                    column = new DataColumn(sqlDataReader.GetName(i));

                    //向数据表中添加列
                    table.Columns.Add(column);
                }

                while (sqlDataReader.Read())//获取数据
                {
                    DataRow row = table.NewRow();//数据表行

                    for (int i = 0; i < size; i++)
                    {
                        //逐行逐列获取数据
                        row[sqlDataReader.GetName(i)] = sqlDataReader[sqlDataReader.GetName(i)].ToString();
                    }
                    table.Rows.Add(row);//添加行
                }

                sqlDataReader.Close();//关闭数据读取器

                return table;
            }
            catch (Exception)
            {
                throw;
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
    /// 串口操作类
    /// </summary>
    public class SerialPortOperation
    {
        /// <summary>
        /// 初始化串口
        /// </summary>
        /// <param name="ComProt">串口名称</param>
        /// <param name="strComNumber">设置串口号</param>
        /// <param name="strBaud">波特率</param>
        /// <param name="strDataBits">数据位</param>
        /// <param name="strStopBits">停止位</param>
        /// <param name="strParity">校验位</param>
        /// <param name="strHWFlow">流控制</param>
        /// <returns>设置成功返回 ＴＲＵＥ</returns>
        public static bool InitSerialPort(SerialPort ComProt, string strComNumber, string strBaud, string strDataBits,
                                   string strStopBits, string strParity, string strHWFlow)
        {

            try
            {
                if (ComProt.IsOpen)
                {
                    ComProt.Close();
                }

                if (strComNumber.ToUpper().Contains("COM"))
                {
                    ComProt.PortName = strComNumber.Trim().ToUpper();
                }
                else
                {
                    ComProt.PortName = "COM" + strComNumber.Trim();
                }

                ComProt.BaudRate = Convert.ToInt32(strBaud);
                ComProt.DataBits = Convert.ToInt32(strDataBits);

                if (strStopBits.Trim() == "2")
                {
                    ComProt.StopBits = StopBits.Two;
                }
                else if (strStopBits.Trim() == "1")
                {
                    ComProt.StopBits = StopBits.One;
                }
                else if (strStopBits.Trim() == "1.5")
                {
                    ComProt.StopBits = StopBits.OnePointFive;
                }
                else
                {
                    ComProt.StopBits = StopBits.None;
                }

                if (strParity.Trim().ToUpper() == "EVEN")
                {
                    ComProt.Parity = Parity.Even;
                }
                else if (strParity.Trim().ToUpper() == "MARK")
                {
                    ComProt.Parity = Parity.Mark;
                }
                else if (strParity.Trim().ToUpper() == "ODD")
                {
                    ComProt.Parity = Parity.Odd;
                }
                else if (strParity.Trim().ToUpper() == "SPACE")
                {
                    ComProt.Parity = Parity.Space;
                }
                else
                {
                    ComProt.Parity = Parity.None;
                }

                if (strHWFlow.ToUpper().Contains("RTS"))
                {
                    ComProt.RtsEnable = true;
                }

                if (strHWFlow.ToUpper().Contains("DTR"))
                {
                    ComProt.DtrEnable = true;
                }

                if (!ComProt.IsOpen)
                {
                    ComProt.Open();
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }

    /// <summary>
    /// 其他函数
    /// </summary>
    public class Other
    {
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);

        public delegate void deleteClear() ;

        public static void CrearMemory()
        {
            deleteClear delete = new deleteClear(CrearMemoryDelete);
            delete.BeginInvoke(null,null);
        }
        /// <summary>
        /// 内存清理
        /// </summary>
        public static void CrearMemoryDelete()
        {
            try
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
                }
            }
            catch { }
        }

        /// <summary>
        /// 获取本机可用Com口
        /// </summary>
        public static string[] GetPCCom()
        {
            try
            {
                string[] sAllPort = SerialPort.GetPortNames();
                return sAllPort;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return null;
            }
        }
    }

    public class WinsafeEQP
    {
        /// <summary>
        /// 根据指定变量名返回相应的命令字符串
        /// </summary>
        /// <param name="name">变量名</param>
        /// <param name="code">变量值</param>
        /// <param name="isDelete">喷印完成后是否删除</param>
        /// <returns></returns>
        public static byte[] G220ICommand(string name, string code, bool isDelete)
        {
            //定义发送至喷码机的16进制指令
            byte[] btsSend = new byte[0];

            try
            {
                List<byte> byteList = new List<byte>();

                byteList.Add(Convert.ToByte(int.Parse("00", NumberStyles.HexNumber)));
                byteList.Add(Convert.ToByte(int.Parse("00", NumberStyles.HexNumber)));
                byteList.Add(Convert.ToByte(int.Parse("00", NumberStyles.HexNumber)));
                byteList.Add(Convert.ToByte(int.Parse("00", NumberStyles.HexNumber)));
                byteList.Add(Convert.ToByte(int.Parse("00", NumberStyles.HexNumber)));

                byteList.Add(Convert.ToByte(int.Parse("27", NumberStyles.HexNumber)));//长度 需要变动

                byteList.Add(Convert.ToByte(int.Parse("01", NumberStyles.HexNumber)));
                byteList.Add(Convert.ToByte(int.Parse("65", NumberStyles.HexNumber)));

                byteList.Add(Convert.ToByte(int.Parse("09", NumberStyles.HexNumber)));
                byteList.Add(Convert.ToByte(int.Parse("00", NumberStyles.HexNumber)));
                byteList.Add(Convert.ToByte(int.Parse("00", NumberStyles.HexNumber)));
                byteList.Add(Convert.ToByte(int.Parse("00", NumberStyles.HexNumber)));
                byteList.Add(Convert.ToByte(int.Parse("01", NumberStyles.HexNumber)));
                byteList.Add(Convert.ToByte(int.Parse("03", NumberStyles.HexNumber)));

                byteList.Add(Convert.ToByte(int.Parse("1E", NumberStyles.HexNumber)));//长度 需要变动

                //变量名
                byte[] bytesTem = Encoding.UTF8.GetBytes(name);//byte[] bytesTem = ASCIIEncoding.ASCII.GetBytes(name);

                for (int i = 0; i < 20; i++)
                {
                    if (i < bytesTem.Length)
                        byteList.Add(bytesTem[i]);
                    else
                        byteList.Add(Convert.ToByte(int.Parse("00", NumberStyles.HexNumber)));
                }

                //喷印完成后是否清除
                if (isDelete)
                {
                    byteList.Add(Convert.ToByte(int.Parse("00", NumberStyles.HexNumber)));
                    byteList.Add(Convert.ToByte(int.Parse("01", NumberStyles.HexNumber)));//喷印完后删除
                }
                else
                {
                    byteList.Add(Convert.ToByte(int.Parse("00", NumberStyles.HexNumber)));
                    byteList.Add(Convert.ToByte(int.Parse("00", NumberStyles.HexNumber)));//喷印完后不删除
                }

                //传给变量的值
                byte[] bts = Encoding.UTF8.GetBytes(code);//中文需要搭配UTF-8编码 //byte[] bts = ASCIIEncoding.ASCII.GetBytes(code);

                // <8 用0补全20位
                if (bts.Length <= 8)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if (i < bts.Length)
                            byteList.Add(bts[i]);
                        else
                            byteList.Add(Convert.ToByte(int.Parse("00", NumberStyles.HexNumber)));
                    }
                }
                // >8 实际长度
                if (bts.Length > 8)
                {
                    foreach (byte bt in bts)
                    {
                        byteList.Add(bt);
                    }
                }

                byteList[5] = Convert.ToByte(int.Parse((byteList.Count - 6).ToString("X2"), NumberStyles.HexNumber));//长度值变化
                byteList[14] = Convert.ToByte(int.Parse((byteList.Count - 15).ToString("X2"), NumberStyles.HexNumber));//长度值变化

                btsSend = new byte[byteList.Count];

                byteList.CopyTo(btsSend);
            }
            catch (Exception ex)
            {
                MessageBox.Show("指令拼接错误：\r\n" + ex.Message);
            }

            return btsSend;
        }

        /// <summary>
        /// 根据变量名清空变量
        /// </summary>
        /// <param name="name">变量名</param>
        /// <returns></returns>
        public static byte[] G220IClearCommand(string name)
        {
            byte[] clearBytes = new byte[0];

            try
            {
                List<byte> byteList = new List<byte>();

                byteList.Add(Convert.ToByte(int.Parse("00", NumberStyles.HexNumber)));
                byteList.Add(Convert.ToByte(int.Parse("00", NumberStyles.HexNumber)));
                byteList.Add(Convert.ToByte(int.Parse("00", NumberStyles.HexNumber)));
                byteList.Add(Convert.ToByte(int.Parse("00", NumberStyles.HexNumber)));
                byteList.Add(Convert.ToByte(int.Parse("00", NumberStyles.HexNumber)));

                byteList.Add(Convert.ToByte(int.Parse("1E", NumberStyles.HexNumber)));//长度 需要变动

                byteList.Add(Convert.ToByte(int.Parse("01", NumberStyles.HexNumber)));
                byteList.Add(Convert.ToByte(int.Parse("65", NumberStyles.HexNumber)));
                byteList.Add(Convert.ToByte(int.Parse("09", NumberStyles.HexNumber)));
                byteList.Add(Convert.ToByte(int.Parse("00", NumberStyles.HexNumber)));
                byteList.Add(Convert.ToByte(int.Parse("00", NumberStyles.HexNumber)));
                byteList.Add(Convert.ToByte(int.Parse("00", NumberStyles.HexNumber)));
                byteList.Add(Convert.ToByte(int.Parse("01", NumberStyles.HexNumber)));
                byteList.Add(Convert.ToByte(int.Parse("08", NumberStyles.HexNumber)));
                byteList.Add(Convert.ToByte(int.Parse("15", NumberStyles.HexNumber)));
                byteList.Add(Convert.ToByte(int.Parse("01", NumberStyles.HexNumber)));

                for (int i = 0; i < 20; i++)
                    byteList.Add(Convert.ToByte(int.Parse("00", NumberStyles.HexNumber)));

                byte[] bytesTem = ASCIIEncoding.ASCII.GetBytes(name);

                for (int i = 0; i < bytesTem.Length; i++)
                {
                    byteList[i + 16] = bytesTem[i];
                }

                byteList[5] = Convert.ToByte(int.Parse((byteList.Count - 6).ToString("X2"), NumberStyles.HexNumber));

                clearBytes = new byte[byteList.Count];

                byteList.CopyTo(clearBytes);
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误：\r\n" + ex.Message);
            }

            return clearBytes;
        }
    }
}
