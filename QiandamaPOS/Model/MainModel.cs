using QiandamaPOS.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QiandamaPOS.Model
{
    public class MainModel
    {
        /// <summary>
        /// INI目录
        /// </summary>
        public static string IniPath = AppDomain.CurrentDomain.BaseDirectory + "Config.ini";

        /// <summary>
        /// 程序根目录
        /// </summary>
        public static string ServerPath = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// 私钥
        /// </summary>
        public static string PrivateKey = INIManager.GetIni("System", "PrivateKey", MainModel.IniPath);
        /// <summary>
        /// 环境
        /// </summary>
        public static string URL = INIManager.GetIni("System", "URL", MainModel.IniPath);
        
        /// <summary>
        /// POS端 token
        /// </summary>
        public static string Authorization = INIManager.GetIni("System", "POS-Authorization", MainModel.IniPath);
        /// <summary>
        /// 设备号
        /// </summary>
        public static string DeviceSN = INIManager.GetIni("System", "DeviceSN", MainModel.IniPath);
        /// <summary>
        /// 版本号
        /// </summary>
        public static string Version = INIManager.GetIni("System", "Version", MainModel.IniPath);

        ///// <summary>
        ///// 商店ID
        ///// </summary>
        //public static string ShopID = INIManager.GetIni("System", "ShopID", MainModel.IniPath);
        /// <summary>
        /// 商店名称
        /// </summary>
        public static string ShopName = INIManager.GetIni("System", "ShopName", MainModel.IniPath);

        /// <summary>
        /// 当前登录用户信息
        /// </summary>
        public static userModel CurrentUser;

        /// <summary>
        /// 当前登录会员
        /// </summary>
        public static Member CurrentMember = null;

        /// <summary>
        /// 接口返回code 120014 代表用户登录过期 需要重新登录
        /// </summary>
        public static int HttpUserExpired = 120014;
        /// <summary>
        /// 接口返回code 100031 代表会员登录过期 需要重新登录
        /// </summary>
        public static int HttpMemberExpired = 100031;

        public static string CurrentCouponCode ="";

        /// <summary>
        /// 当前店铺信息
        /// </summary>
        public static DeviceShopInfo CurrentShopInfo;


        public static string OrderPath
        {
            get
            {

                //判断默认文件夹是否存在，不存在就创建
                DirectoryInfo logDirectory = new DirectoryInfo(System.Windows.Forms.Application.StartupPath + "\\OrderPath\\");
                if (!logDirectory.Exists)
                    logDirectory.Create();

                return System.Windows.Forms.Application.StartupPath + "\\OrderPath\\";

            }
        }


        /// <summary>
        /// 客屏对象
        /// </summary>
        public static frmMainMedia frmmainmedia = null;
        

        //当前时间戳
        public static  string getStampByDateTime(DateTime datetime)
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

        //键值对排序
        public static Dictionary<string, string> SortDictory(Dictionary<string, string> dictionary)
        {
            System.Collections.ArrayList lst = new System.Collections.ArrayList(dictionary.Keys);
            lst.Sort();
            //lst.Reverse();  //反转排序
            Dictionary<string, string> dicresult = new Dictionary<string, string>();

            foreach (string key in lst)
            {
                dicresult.Add(key, dictionary[key]);
            }

            return dicresult;
        }

        public static AutoSizeFormUtil AuroSizeUtil = new AutoSizeFormUtil();


        public static void ShowLog(string msg, bool iserror)
        {
            

                frmMsg frmmsf = new frmMsg(msg, iserror, 1000);
                frmmsf.Location = new System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width / 2 - frmmsf.Width, SystemInformation.WorkingArea.Height/2-50);
                LogManager.WriteLog(msg);
                frmmsf.ShowDialog();
            
        }


    }
}
