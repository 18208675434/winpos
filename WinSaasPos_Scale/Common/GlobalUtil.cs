using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WinSaasPOS_Scale.Common
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
        //    var pcsn = "";
        //    try
        //    {
        //        var search = new System.Management.ManagementObjectSearcher("SELECT * FROM Win32_BIOS");
        //        var mobos = search.Get();
        //        foreach (var temp in mobos)
        //        {
        //            object serial = temp["SerialNumber"]; // ProcessorID if you use Win32_CPU
        //            pcsn = serial.ToString();
        //            //Console.WriteLine(pcsn);
        //            //BFEBFBFF000806EB
        //            if
        //            (
        //                !string.IsNullOrEmpty(pcsn)
        //                && pcsn != "To be filled by O.E.M" //没有找到
        //                && !pcsn.Contains("O.E.M")
        //                && !pcsn.Contains("OEM")
        //                && !pcsn.Contains("Default")
        //            )
        //            {
        //                break;
        //            }
        //            else
        //            {
        //                pcsn = "";
        //               // Console.WriteLine("默认值");
        //            }
        //        }

        //        return pcsn.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("获取cpu序号过程发生异常");
        //        return "";
        //        //Debug.WriteLine(e);
        //        // 无法处理
        //    }
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
                string devicesn = INIManager.GetIni("System", "DeviceSN", WinSaasPOS_Scale.Model.MainModel.IniPath);
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
                        week = "（星期日）";
                        break;
                    case 1:
                        week = "（星期一）";
                        break;
                    case 2:
                        week = "（星期二）";
                        break;
                    case 3:
                        week = "（星期三）";
                        break;
                    case 4:
                        week = "（星期四）";
                        break;
                    case 5:
                        week = "（星期五）";
                        break;
                    default:
                        week = "（星期六）";
                        break;
                }
                return week;
            }
            catch
            {
                return "";
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