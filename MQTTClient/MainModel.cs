
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MQTTClient
{
    public class MainModel
    {
        //判断客屏是否播放视屏  是的话把焦点还给主界面
        public static bool IsPlayer = false;

        /// <summary>
        /// INI目录
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


        /// <summary>
        /// 设备号
        /// </summary>
        public static string DeviceSN = "";

        public static string URL = "";


    }


    public class DeviceShopInfo
    {
        /// <summary>
        /// 设备id
        /// </summary>
        public long deviceid { set; get; }
        /// <summary>
        /// 商户编号
        /// </summary>
        public string tenantid { set; get; }
        /// <summary>
        /// 门店编码
        /// </summary>
        public string shopid { set; get; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string shopname { set; get; }
        /// <summary>
        /// 设备sn（全局唯一）
        /// </summary>
        public string devicesn { set; get; }
        /// <summary>
        /// 设备code
        /// </summary>
        public string devicecode { set; get; }
        /// <summary>
        /// 门店地址
        /// </summary>
        public string address { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string allclose { set; get; }

        /// <summary>
        /// 门店电话
        /// </summary>
        public string tel { set; get; }
    }



    
public class JsonPromotion
{
public string allowance { get; set; }
public bool canbecombined { get; set; }
public bool canmixcoupon { get; set; }
public string code { get; set; }
public string costcenterinfo { get; set; }
public long createdat { get; set; }
public string createdby { get; set; }
public string description { get; set; }
public string districtscope { get; set; }
public string eligibilitycondition { get; set; }
public bool enabled { get; set; }
public long enabledfrom { get; set; }
public string enabledtimeinfo { get; set; }
public long enabledto { get; set; }
public bool fromouter { get; set; }
public string name { get; set; }
public int ordersubtype { get; set; }
public string outercode { get; set; }
public string promoaction { get; set; }
public string promoactioncontext { get; set; }
public string promoconditioncontext { get; set; }
public string promoconditiontype { get; set; }
public string promosubtype { get; set; }
public string promotype { get; set; }
public int rank { get; set; }
public int salechannel { get; set; }
public string shopscope { get; set; }
public string tag { get; set; }
public string tenantscope { get; set; }
public long updatedat { get; set; }
public string updatedby { get; set; }
}


}
