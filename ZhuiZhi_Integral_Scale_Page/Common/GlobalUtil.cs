using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Common
{
    public  class GlobalUtil
    {
        //获取CPU序号
        public static string GetPCSN()
        {
            string cpuInfo = "";//cpu序列号 
            ManagementClass cimobject = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = cimobject.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                return cpuInfo.ToString();

            }
            return "";
        }



        /// <summary>

        /// 获取第一分区硬盘编号

        /// </summary>

        /// <returns>返回一个字符串类型</returns>

        public static string GetHardDiskID()
        {

            try
            {
                bool isfirst = true;
                //test
                //return "WL1VJT4T";
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");

                string strHardDiskID = "";
                string devicesn = INIManager.GetIni("System", "DeviceSN", ZhuiZhi_Integral_Scale_UncleFruit.Model.MainModel.IniPath);
                foreach (ManagementObject mo in searcher.Get())
                {
                    try
                    {
                        string tempstrHardDiskID = mo["SerialNumber"].ToString().Trim();
                        if (isfirst && !string.IsNullOrEmpty(tempstrHardDiskID))
                        {
                            strHardDiskID = tempstrHardDiskID;
                            isfirst = false;
                        }

                        if (tempstrHardDiskID == devicesn)
                        {
                            strHardDiskID = tempstrHardDiskID;
                            break;
                        }
                    }catch{}
                   
                   // break;

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
                    //判断网卡mac只要存在就可以一直使用  第一次取正在使用的网卡地址
                    try
                    {
                        string tempstrMac = mo["MacAddress"].ToString();
                        tempstrMac = strMac.Replace(":", "");
                        tempstrMac = strMac.Replace("-", "");

                        if (!string.IsNullOrEmpty(oldmac) && oldmac == tempstrMac)
                        {
                            return tempstrMac;
                        }
                    }
                    catch { }

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


        /// <summary>
        /// 获取当前星期几
        /// </summary>
        /// <returns></returns>
        public static string GetWeek()
        {
            try
            {
                string week = string.Empty;
                switch ((int)DateTime.Now.DayOfWeek)
                {
                    case 0:
                        week = "(星期日)";
                        break;
                    case 1:
                        week = "(星期一)";
                        break;
                    case 2:
                        week = "(星期二)";
                        break;
                    case 3:
                        week = "(星期三)";
                        break;
                    case 4:
                        week = "(星期四)";
                        break;
                    case 5:
                        week = "(星期五)";
                        break;
                    default:
                        week = "(星期六)";
                        break;
                }
                return week;
            }
            catch
            {
                return "";
            }
        }




        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);

        public static string boardpath = AppDomain.CurrentDomain.BaseDirectory + "SoftBoard.exe";


        /// <summary>
        /// 打开需要判断系统X86  和X64
        /// </summary>
        public static void OpenOSK()
        {
            try
            {
               
                IntPtr ptr = new IntPtr();
                bool isWow64FsRedirectionDisabled = Wow64DisableWow64FsRedirection(ref ptr);
                if (isWow64FsRedirectionDisabled)
                {
                    Process.Start(@"C:\WINDOWS\system32\osk.exe");
                    bool isWow64FsRedirectionReverted = Wow64RevertWow64FsRedirection(ptr);
                }
                else
                {
                    Process.Start(@"C:\WINDOWS\system32\osk.exe");
                }

                //判断软键盘是否进程是否已经存在，如果不存在进行调用   有可能被防火墙拦截
                Process[] pro = Process.GetProcessesByName("osk");
                //说明已经存在，不再进行调用
                if (pro != null && pro.Length > 0)
                    return;

                Process.Start(boardpath);
            }
            catch (Exception ex)
            {
                try
                {
                    Process.Start(boardpath);//兜底方案：系统没有键盘时启动程序包小键盘程序
                }
                catch { }
                LogManager.WriteLog("开启键盘异常" + ex.Message);
            }
        }
        public static void CloseOSK()
        {

            try
            {
                Process[] pro = Process.GetProcesses();
                for (int i = 0; i < pro.Length - 1; i++)
                {
                    if (pro[i].ProcessName == "osk" || pro[i].ProcessName == "TabTip" || pro[i].ProcessName == "SoftBoard")
                    {
                        pro[i].Kill();
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("小键盘关闭异常：" + ex.Message);
            }
        }



        /// <summary>
        /// 判断是否有交集  有交集返回false  没有交集返回true  2020 06-29  类似Collections.disjoint
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        /// <returns></returns>
        public static bool IsArrayIntersection<T>(List<T> list1, List<T> list2)
        {
            try
            {
                List<T> t = list1.Distinct().ToList();

                var exceptArr = t.Except(list2).ToList();

                if (exceptArr.Count < t.Count)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("判断集合是否有交集异常" + ex.Message);
                return true;
            }

        }



        //检测IP连接
        public static bool CheckIP(string ip)
        {
            bool var = false;

            try
            {
                Ping pingSender = new Ping();

                PingOptions pingOption = new PingOptions();
                pingOption.DontFragment = true;
                string data = "0";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 2000;
                PingReply reply = pingSender.Send(ip, timeout, buffer);
                if (reply.Status == IPStatus.Success)
                    var = true;
                else
                    var = false;
            }
            catch (Exception ex)
            {

                return true;
                // ShowLog("无法检测网络连接是否正常-" + ex.Message, true);
            }

            return var;
        }



        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetCursorPos(out Point pt);
        public static Point GetCursorPos()
        {
            try
            {
                Point currentPosition = new Point();
                GetCursorPos(out currentPosition);

                return currentPosition;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("获取屏幕焦点异常" + ex.Message);
                return new Point(0, 0);
            }
        }


    }





    public static class Shuiyin
    {
        private const int EM_SETCUEBANNER = 0x1501;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage
         (IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        /// <summary>
        /// 为TextBox设置水印文字
        /// </summary>
        /// <param name="textBox">TextBox</param>
        /// <param name="watermark">水印文字</param>
        public static void SetWatermark(this TextBox textBox, string watermark)
        {
            
            SendMessage(textBox.Handle, EM_SETCUEBANNER, 2, watermark);
        }





#region  获取汉字首字母

    /// <summary>
        /// 这个办法是用来获得一个字符串的每个字的拼音首字母构成所需的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetChineseSpell(string strText)
        {
            try
            {
                int len = strText.Length;
                string myStr = "";
                for (int i = 0; i < len; i++)
                {
                    myStr += getSpell(strText.Substring(i, 1));
                }
                return myStr;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("获取字符串首字母异常"+ex.Message);
                return "";
            }
        }


        /// <summary>
        /// 用来获得一个字的拼音首字母
        /// </summary>
        /// <param name="cnChar"></param>
        /// <returns></returns>
        public static string getSpell(string cnChar)
        {
            try
            {
                //将汉字转化为ASNI码,二进制序列
                byte[] arrCN = Encoding.Default.GetBytes(cnChar);
                if (arrCN.Length > 1)
                {
                    int area = (short)arrCN[0];
                    int pos = (short)arrCN[1];
                    int code = (area << 8) + pos;
                    int[] areacode = {45217,45253,45761,46318,46826,47010,47297,47614,48119,48119,49062,
                49324,49896,50371,50614,50622,50906,51387,51446,52218,52698,52698,52698,52980,53689,54481};
                    for (int i = 0; i < 26; i++)
                    {
                        int max = 55290;
                        if (i != 25)
                        {
                            max = areacode[i + 1];
                        }
                        if (areacode[i] <= code && code < max)
                        {
                            return Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                        }
                    }
                    return "*";
                }
                else
                {
                    return cnChar;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("获取汉字首字母异常"+ex.Message);
                return "";
            }
        }
# endregion



        

}

    public enum SortType
    {
        SaleCount,
        CreateDate,
        SalePriceAsc,
        SalePriceDesc
    }


    public enum ExpiredType
    {
        None,
        UserExpired,
        MemberExpired,
        DifferentMember
    }



          
}