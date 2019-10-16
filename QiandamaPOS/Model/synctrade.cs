using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QiandamaPOS.Model
{
    /// <summary>
    /// 第三方支付查询
    /// </summary>
    public  class synctrade
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
        /// 第三方支付号
        /// </summary>
        public string transactionid { set; get; }
       
        /// <summary>
        /// 支付状态(REQUEST_SUCCESS:请求成功， REQUEST_CLOSE:交易关闭， FAIL:支付失败， SUCCESS： 支付成功)
        /// </summary>
        public string status { set; get; }
       
    }
}
