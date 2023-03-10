using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Model
{

    public class QueryOrder
    {
        public int hasnextpage { get; set; }
        public string lastorderid { get; set; }
        public List<Order> orders { get; set; }
    }

    public class Order
    {
        /// <summary>
        /// 支付宝支付金额
        /// </summary>
        public decimal alipayamt { get; set; }
        /// <summary>
        /// 余额支付金额
        /// </summary>
        public decimal balanceamt { get; set; }
        /// <summary>
        /// 现金支付金额
        /// </summary>
        public decimal cashpayamt { get; set; }
        /// <summary>
        /// 微信支付金额
        /// </summary>
        public decimal wechatpayamt { get; set; }
        /// <summary>
        /// 银联云闪付金额
        /// </summary>
        public decimal ylpayamt { get; set; }

         /// <summary>
        /// 积分抵扣金额
        /// </summary>
        public decimal pointpayamt { get; set; }


        /// <summary>
        /// 代金券抵扣金额
        /// </summary>
        public decimal cashcouponamt { get; set; }
        

        public long orderat { get; set; }
        public string orderid { get; set; }
        public string title { get; set; }
        public decimal orderamt { get; set; }
        public string orderstatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ordersubtype { get; set; }
        /// <summary>
        /// 订单状态文案  5已退款  1:待支付
        /// </summary>
        public int orderstatusvalue { get; set; }
        public int showprint { get; set; }

        public string customerphone { get; set; }

        public   List<QuereOrderProduct> products {get;set; }

        /// <summary>
        /// 是否支持部分退 1支持 0不支持 ,
        /// </summary>
        public int supportpartrefund { get; set; }

        public decimal otherpayamt { get; set; }

        public string otherpaytypedesc { get; set; }

        /// <summary>
        /// 是否支持指定金额退款  1：支持 0： 不支持
        /// </summary>
        public int supportspecifiedamountrefund { get; set; }

        public List<OtherPayDetailInfo> otherpaydetailinfos { get; set; }

        public string receiverphone { set; get; }

        public string sourceflag { set; get; }


        public string userid { set; get; }

        public string skuname { set; get; }
        public string status { set; get; }
        public string source { set; get; }
        public string errormsg { set; get; }

        public string ordertype { set; get; }

        public string phone { set; get; }
    }

    public class QuereOrderProduct
    {
        public int goodstagid{get;set;}

         public string skucode{get;set;}

         public string title{get;set;}

         public int num{get;set;}

         public decimal specnum{get;set;}

         public long orderitemid{get;set;}

        public Price price{get;set;}

        /// <summary>
        /// 判断本地是否选择部分退款
        /// </summary>
        public bool IsSelect { get; set; }


        /// <summary>
        /// 最大数量  标品部分退修改数量时不允许超过该数量
        /// </summary>
        public decimal maxnum { get; set; }


        /// <summary>
        /// 指定退款金额
        /// </summary>
        public decimal refundamt { get; set; }

        /// <summary>
        /// 指定金额退款说明
        /// </summary>
        public string specifiedamountrefunddesc { get; set; }

        /// <summary>
        /// 订单行是否支持指定金额退款 1：支持 0：不支持
        /// </summary>
        public int supportspecifiedamountrefund { get; set; }

        public decimal totalpayment { get; set; }

    }


    public class OtherPayDetailInfo
    {
        public string type { get; set; }

        public decimal amount { get; set; }
    }

}
