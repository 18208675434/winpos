using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QiandamaPOS.Model
{
    public class AuthcodeTrade
    {
        /// <summary>
        /// 锥智支付系统支付号
        /// </summary>
        public string payid { set; get; }
        /// <summary>
        /// 锥智支付系统支付号
        /// </summary>
        public string orderid { set; get; }
        /// <summary>
        /// 订单类型（参数约定）
        /// </summary>
        public int ordertype { set; get; }
        /// <summary>
        /// 第三方支付号
        /// </summary>
        
        public string transactionid { set; get; }
        /// <summary>
        /// 支付网关（参数约定）
        /// </summary>
        public string gateway { set; get; }
        /// <summary>
        /// 门店号
        /// </summary>
        public string shopcode { set; get; }
        /// <summary>
        /// 收银机机具号
        /// </summary>
        public string terminalid { set; get; }
        /// <summary>
        /// 操作员号
        /// </summary>
        public string operatorid { set; get; }
        /// <summary>
        /// 支付状态(REQUEST_SUCCESS:请求成功， REQUEST_CLOSE:交易关闭， FAIL:支付失败， SUCCESS： 支付成功)
        /// </summary>
        public string status { set; get; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public string totalfee { set; get; }
        /// <summary>
        /// 交易时间
        /// </summary>
        public long tradetime { set; get; }
        /// <summary>
        /// 用户在第三方支付系统中的身份信息， 比如微信系统中的 openid
        /// </summary>
        public string thirdpartyuseridentity { set; get; }
    }
}
