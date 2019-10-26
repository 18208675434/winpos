using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace QiandamaPOS.Model
{
    public class BalanceTrade
    {
        /// <summary>
        /// 支付单号
        /// </summary>
        public string id { set; get; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string outtradeno { set; get; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal totalamount { set; get; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string subject { set; get; }
        /// <summary>
        /// 门店号
        /// </summary>
        public string shopid { set; get; }
        /// <summary>
        /// 操作员号
        /// </summary>
        public string operatorid { set; get; }
        /// <summary>
        /// 收银机号
        /// </summary>
        public string terminalid { set; get; }
        /// <summary>
        /// 支付状态，SUCCESS:支付成功，FAILED:支付失败，UNKNOWN:支付状态未知
        /// </summary>
        public string status { set; get; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string errormessage { set; get; }
    }
}
