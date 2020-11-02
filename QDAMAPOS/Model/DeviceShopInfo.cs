using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace QDAMAPOS.Model
{
    public class DeviceShopInfo
    {
        /// <summary>
        /// 设备id
        /// </summary>
         public long deviceid{set;get;}
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
}
