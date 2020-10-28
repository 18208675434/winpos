using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Model
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
         /// 商户名称
         /// </summary>
         public string tenantname { set; get; }

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


         /// <summary>
         /// 单品改价 0不支持  1支持
         /// </summary>
         public int posalterskupriceflag { set; get; }

         /// <summary>
         /// 整单改价  0不支持 1支持
         /// </summary>
         public int posalterorderpriceflag { set; get; }

        /// <summary>
        /// 最高支持改价金额（整单）
        /// </summary>
         public decimal posalterorderpricerange { set; get; }

        /// <summary>
        /// 是否支持整的那折扣  0不支持 1支持
        /// </summary>
         public int posalterorderdiscountflag { set; get; }

        /// <summary>
        /// 最高支持折扣 （
        /// </summary>
         public decimal posalterorderdiscountrange { set; get; }

    }
}
