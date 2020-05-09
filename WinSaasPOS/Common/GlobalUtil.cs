using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WinSaasPOS.Common
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
                string devicesn = INIManager.GetIni("System", "DeviceSN", WinSaasPOS.Model.MainModel.IniPath);
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






        public static void OpenOSK()
        {
            try
            {
                string tabtipfile = @"C:\Program Files\Common Files\microsoft shared\ink\TabTip.exe";
                string oskfile = @"C:\Windows\System32\osk.exe";
                if (File.Exists(tabtipfile))
                {
                    System.Diagnostics.Process.Start(tabtipfile);
                }
                else
                {
                    System.Diagnostics.Process.Start(oskfile);
                }
            }
            catch (Exception ex) { }
        }
        public static void CloseOSK()
        {

            try
            {
                Process[] pro = Process.GetProcesses();
                for (int i = 0; i < pro.Length - 1; i++)
                {
                    if (pro[i].ProcessName == "osk" || pro[i].ProcessName == "TabTip")
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






}