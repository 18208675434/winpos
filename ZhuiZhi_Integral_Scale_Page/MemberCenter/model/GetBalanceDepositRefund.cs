using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model
{
    public class GetBalanceDepositRefund
    {
        /// <summary>
        /// 
        /// </summary>
        public string balance { get; set; }
        /// <summary>
        /// 退款本金
        /// </summary>
        public string capitalrefundamount { get; set; }
        /// <summary>
        /// 退款日期
        /// </summary>
        public string createdat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string createdby { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string customerpaycode { get; set; }
        /// <summary>
        /// 关联充值单id
        /// </summary>
        public string depositbillid { get; set; }
        /// <summary>
        /// 退款单号
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string memberid { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 操作人手机号
        /// </summary>
        public string operatorphone { get; set; }
        public string paymode { get; set; }
        public string phone { get; set; }
        /// <summary>
        /// 总退款金额
        /// </summary>rewardrefundamout
        public string refundtotalamount { get; set; }
        /// <summary>
        /// 1.原路返回，2线下转账
        /// </summary>
        public string refundtype { get; set; }
        /// <summary>
        /// 退款方式
        /// </summary>
        public string refundtypeforapi { get; set; }
        /// <summary>
        /// 退增送金额
        /// </summary>
        public string rewardrefundamount { get; set; }
        public string shopid { get; set; }
        public string shopname { get; set; }
        public string status { get; set; }
        public string tenantid { get; set; }
        public string updatedat { get; set; }
        public string updateby { get; set; }

    }
}
