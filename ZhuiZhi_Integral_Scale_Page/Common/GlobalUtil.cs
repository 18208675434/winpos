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
using ZhuiZhi_Integral_Scale_UncleFruit.MyControl;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Common
{
    public  class GlobalUtil
    {
        #region  获取设备号
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

        #endregion
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


        #region  软键盘打开与关闭

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
                if (IsOskOpen())
                {
                    return;
                }
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


        public static bool IsOskOpen()
        {
            try
            {
                System.Diagnostics.Process[] pro = System.Diagnostics.Process.GetProcesses();

               Process oskpro =  pro.FirstOrDefault(r=> r.ProcessName.Contains("osk"));

              
               return oskpro != null;
            }
            catch
            {
                return false;
            }
        }


        static string keyboardname = "uckeyboard";

        public static void ShowKeyBoard(System.Windows.Forms.Form frm, KeyBorderCharType type = KeyBorderCharType.CHAR)
        {
            Size size = new Size(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height / 3);
            Point point = new System.Drawing.Point(0, Screen.AllScreens[0].Bounds.Height * 2 / 3);
            ShowKeyBoard(frm, size, point, type);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frm">需要加载键盘的窗体</param>
        /// <param name="size">键盘尺寸</param>
        /// <param name="location">键盘位置</param>
        public static void ShowKeyBoard(System.Windows.Forms.Form frm, Size size, Point location, KeyBorderCharType type = KeyBorderCharType.CHAR)
        {
            try
            {
                ZhuiZhi_Integral_Scale_UncleFruit.MyControl.KeyBoardAll uckey;
               

                //如果窗体已存在键盘 显示即可
                foreach (System.Windows.Forms.Control con in frm.Controls)
                {
                    if (con.Name == keyboardname)
                    {
                        uckey = (ZhuiZhi_Integral_Scale_UncleFruit.MyControl.KeyBoardAll)con;
                        uckey.CharType = type;
                        con.Show();
                        return;
                    }
                }
                uckey = new ZhuiZhi_Integral_Scale_UncleFruit.MyControl.KeyBoardAll();
                uckey.Name = keyboardname;
                uckey.Name = keyboardname;
                uckey.CharType = type;
                uckey.Size = size;
                uckey.Location = location;
                uckey.BringToFront();
                frm.Controls.Add(uckey);
                uckey.BringToFront();
            }
            catch { }
        }

        public static void CloseKeyBoard(System.Windows.Forms.Form frm)
        {
            foreach (System.Windows.Forms.Control con in frm.Controls)
            {
                if (con.Name == keyboardname)
                {
                    con.Hide();
                    return;
                }
            }
        }
        #endregion

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


        #region  获取汉字首字母

        /// <summary>
        /// 微软官方类库， 通过ascii码会出现有些字查不到
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ConvertToPinYin(string str)
        {
            try
            {
                string PYstr = "";
                foreach (char item in str.ToCharArray())
                {
                    if (Microsoft.International.Converters.PinYinConverter.ChineseChar.IsValidChar(item))
                    {
                        Microsoft.International.Converters.PinYinConverter.ChineseChar cc = new Microsoft.International.Converters.PinYinConverter.ChineseChar(item);
                        PYstr += cc.Pinyins[0].Substring(0, 1).ToUpper();
                    }
                    else
                    {
                        PYstr += item.ToString().ToUpper();
                    }
                }
                return PYstr;
            }
            catch
            {
                return "";
            }
        }


        public static string ConvertToFirstPinYin(string str)
        {
            try
            {
                string PYstr = "";
                foreach (char item in str.ToCharArray())
                {
                    if (Microsoft.International.Converters.PinYinConverter.ChineseChar.IsValidChar(item))
                    {
                        Microsoft.International.Converters.PinYinConverter.ChineseChar cc = new Microsoft.International.Converters.PinYinConverter.ChineseChar(item);

                        PYstr += cc.Pinyins[0].Substring(0, 1).ToUpper();
                        break;
                    }
                    else
                    {
                        PYstr += item.ToString().ToUpper();
                        break;
                    }
                }
                return PYstr;
            }
            catch
            {
                return "";
            }
        }



        public static string GetAllPinyin(string str)
        {
            try
            {
                string PYstr = "";
                List<List<string>> lstpara = new List<List<string>>();

                foreach (char item in str.ToCharArray())
                {
                    List<string> lst = new List<string>();
                    if (Microsoft.International.Converters.PinYinConverter.ChineseChar.IsValidChar(item))
                    {
                        Microsoft.International.Converters.PinYinConverter.ChineseChar cc = new Microsoft.International.Converters.PinYinConverter.ChineseChar(item);
                        foreach (string strpin in cc.Pinyins)
                        {
                            if (!string.IsNullOrEmpty(strpin))
                            {
                                string first = strpin.Substring(0, 1).ToUpper();
                                if (!lst.Contains(first))
                                {
                                    lst.Add(first);
                                }
                            }

                        }

                        //PYstr += cc.Pinyins[0].Substring(0, 1).ToUpper();
                    }
                    else
                    {
                        lst.Add(item.ToString().ToUpper());
                        // PYstr += item.ToString();
                    }

                    lstpara.Add(lst);
                }

                return getlistpinyin(lstpara);

            }
            catch (Exception ex)
            {
                return str;
            }
            // return PYstr;
        }


        public static string getlistpinyin(List<List<string>> listArray)
        {
            //List<List<string>> listArray = new List<List<string>>()

            //{

            //    new List<string>() { "a","b" },

            //    new List<string>() { "1","2" },

            //   // new List<string>() { 0, 1, 2 },

            //};



            var allCombinations = AllCombinationsOf(listArray);
            string result = "";
            foreach (var combination in allCombinations)
            {

                string str = string.Join("", combination.ToArray());
                result += str + ",";
                // Console.WriteLine(string.Join(", ", combination));

            }

            return result;
        }


        public static List<List<T>> AllCombinationsOf<T>(List<List<T>> sets)
        {

            var combinations = new List<List<T>>();



            foreach (var value in sets[0])

                combinations.Add(new List<T> { value });



            foreach (var set in sets.Skip(1))

                combinations = AddExtraSet(combinations, set);

            return combinations;
        }


        private static List<List<T>> AddExtraSet<T>

             (List<List<T>> combinations, List<T> set)
        {

            var newCombinations = from value in set

                                  from combination in combinations

                                  select new List<T>(combination) { value };


            return newCombinations.ToList();

        }
        # endregion

        public static void Speech(string str)
        {
            System.ComponentModel.BackgroundWorker bk = new System.ComponentModel.BackgroundWorker();

            bk.DoWork += bk_DoWork;
            bk.RunWorkerAsync(str);
        }
        private static void bk_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            string speechstr = e.Argument as string;
            using (System.Speech.Synthesis.SpeechSynthesizer speech = new System.Speech.Synthesis.SpeechSynthesizer())
            {
                //speech.Rate = 0;  //语速
                speech.Volume = 80;  //音量
                speech.Speak(speechstr);
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