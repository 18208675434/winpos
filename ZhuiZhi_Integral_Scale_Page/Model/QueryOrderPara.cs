using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Model
{
    public class QueryOrderPara
    {
        /// <summary>
        /// 会员手机号
        /// </summary>
        public string customerphone { set; get; }
        /// <summary>
        /// 间隔天数 如 今天:0 昨天:1 最近 7 天: 7 最近 30天： 30
        /// </summary>
        public int interval { set; get; }

        /// <summary>
        /// 后端返回的 lastorderid， 第一次请求传 0
        /// </summary>
        public string lastorderid { set; get; }

        /// <summary>
        /// 下单结束时间
        /// </summary>
        public string orderatend { set; get; }                        
        /// <summary>
        /// 下单开始时间
        /// </summary>
        public string orderatstart { set; get; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string orderid { set; get; }

        /// <summary>
        /// 门店ID
        /// </summary>
        public string shopid { set; get; }

        /// <summary>
        ///  订单渠道:1(小程序) 2(美团) 3(饿百)
        /// </summary>
        public int source { set; get; }

        public string tenantid { set; get; }
       /// <summary>
       /// member.memberinfo   查询会员订单的时候用
       /// </summary>
        public string customerid { set; get; }
    }
}
