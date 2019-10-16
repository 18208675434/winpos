using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QiandamaPOS.Common
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
            SendMessage(textBox.Handle, EM_SETCUEBANNER, 0, watermark);
        }
    }

}