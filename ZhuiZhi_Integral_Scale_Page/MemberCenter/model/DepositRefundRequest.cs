using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model
{
    public class DepositRefundRequest
    {
        /// <summary>
        /// 充值订单id
        /// </summary>
        public string depositbillid { get; set; }
        /// <summary>
        /// 会员id
        /// </summary>
        public string memberid { get; set; }
        /// <summary>
        /// 操作人id
        /// </summary>
        public string operatorid { get; set; }
        /// <summary>
        /// 退回本金
        /// </summary>
        public decimal refundcapital { get; set; }
        /// <summary>
        /// 退回方式1：原路返回2：线下退款
        /// </summary>
        public long refundtype { get; set; }
        /// <summary>
        /// 商店Id
        /// </summary>
        public string shopid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tenantid { get; set; }
    }
}
