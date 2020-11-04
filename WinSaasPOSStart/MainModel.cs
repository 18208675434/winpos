using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
//using System.Threading.Tasks;

namespace WinSaasPosStart
{
   public  class MainModel
    {


        /// <summary>
        /// POSINI目录
        /// </summary>
        public static string IniPath = AppDomain.CurrentDomain.BaseDirectory + "Config.ini";

        /// <summary>
        /// StartINI目录
        /// </summary>
        public static string StartIniPath = AppDomain.CurrentDomain.BaseDirectory + "StartConfig.ini";

        /// <summary>
        /// 程序根目录
        /// </summary>
        public static string ServerPath = AppDomain.CurrentDomain.BaseDirectory;

        public static string TempFilePath = AppDomain.CurrentDomain.BaseDirectory + "TempFile" + "\\";
        /// <summary>
        /// 私钥
        /// </summary>
        public static string PrivateKey = INIManager.GetIni("System", "PrivateKey", MainModel.StartIniPath);
        /// <summary>
        /// 环境
        /// </summary>
        public static string URL = INIManager.GetIni("System", "URL", MainModel.StartIniPath);

        /// <summary>
        /// POS端 token
        /// </summary>
        public static string Authorization = INIManager.GetIni("System", "POS-Authorization", MainModel.StartIniPath);
        /// <summary>
        /// 设备号
        /// </summary>
        public static string DeviceSN = "";
        /// <summary>
        /// 版本号
        /// </summary>
        public static string Version = INIManager.GetIni("System", "Version", MainModel.StartIniPath);
        



        public static string GetHardDiskID()
        {

            try
            {

                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");

                string strHardDiskID = null;

                foreach (ManagementObject mo in searcher.Get())
                {

                    strHardDiskID = mo["SerialNumber"].ToString().Trim();

                    break;

                }

                return strHardDiskID;

            }

            catch
            {

                return "";

            }

        }


        /// <summary>  
        /// 获取本机MAC地址  
        /// </summary>  
        /// <returns>本机MAC地址</returns>  
        public static string GetMacAddress(string oldmac)
        {
            try
            {
                List<string> lstmac = new List<string>();
                string strMac = string.Empty;
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        strMac = mo["MacAddress"].ToString();
                        strMac = strMac.Replace(":", "");
                        strMac = strMac.Replace("-", "");
                        if (strMac.Length > 10)
                        {
                            lstmac.Add(strMac);
                        }

                    }
                }


                moc = null;
                mc = null;

                if (lstmac.Count > 0)
                {
                    if (lstmac.Contains(oldmac))
                    {
                        return oldmac;
                    }
                    else
                    {
                        return lstmac[0];
                    }

                }
                else
                {
                    return "unknown";
                }


            }
            catch (Exception ex)
            {
                LogManager.WriteLog("获取MAC地址异常：" + ex.Message);
                return "unknown";

            }
        }

        ///// <summary>  
        ///// 获取本机MAC地址  
        ///// </summary>  
        ///// <returns>本机MAC地址</returns>  
        //public static string GetMacAddress()
        //{
        //    try
        //    {
        //        string strMac = string.Empty;
        //        ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
        //        ManagementObjectCollection moc = mc.GetInstances();
        //        foreach (ManagementObject mo in moc)
        //        {
        //            if ((bool)mo["IPEnabled"] == true)
        //            {
        //                strMac = mo["MacAddress"].ToString();
        //                strMac = strMac.Replace(":", "");
        //                strMac = strMac.Replace("-", "");
        //                if (strMac.Length > 10)
        //                    break;
        //            }
        //        }
        //        moc = null;
        //        mc = null;
        //        return strMac;
        //    }
        //    catch(Exception ex)
        //    {
        //        LogManager.WriteLog("获取MAC地址异常："+ex.Message);
        //        return "unknown";

        //    }
        //}

        //当前时间戳
        public static string getStampByDateTime(DateTime datetime)
        {

            //DateTime datetime = DateTime.Now;
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            var result = (long)(datetime - startTime).TotalMilliseconds;

            return result.ToString();
        }

        public static DateTime GetDateTimeByStamp(string stamp)
        {
            try
            {
                long result = Convert.ToInt64(stamp);
                DateTime datetime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                datetime = datetime.AddMilliseconds(result);
                return datetime;
            }
            catch (Exception ex)
            {
                return DateTime.Now;
            }
        }

        //MD5 加密
        public static string GetMD5(string str)
        {

            byte[] result = Encoding.Default.GetBytes(str);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            string md5str = BitConverter.ToString(output).Replace("-", "");
            return md5str;
        }

        //获取20位随机码 nonce
        public static string getNonce()
        {
            string randomstr = "0,1,2,3,4,5,6,7,8,9,0,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
            string[] strRandom = randomstr.Split(',');
            Random rd = new Random();
            string result = "";
            for (int i = 0; i < 20; i++)
            {
                int num = rd.Next(35);

                result += strRandom[num];
            }

            return result;
        }

    }
}
