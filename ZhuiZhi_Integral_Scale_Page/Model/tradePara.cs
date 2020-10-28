using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Model
{
    public class tradePara
    {
        /// <summary>
        /// 付款码
        /// </summary>
        public string authcode { set; get; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string orderid { set; get; }


        //2020-07-30 修改
        /// <summary>
        /// ORDER(1, "订单支付"),
    //RECHARGE(2, "会员储值卡充值"),
    //TENANT_RECHARGE(4, "商户充值"),
    //MEMBER_RIGHTS_RECHARGE(8, "会员权益充值"),
    //ICP_ORDER(16, "ICP订单支付"),
    //ALONE_ORDER(32, "独立订单支付"),
        /// </summary>
        public int ordertype { set; get; }
    }
    
}
