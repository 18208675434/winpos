using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace QiandamaPOS.Model
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
    }
    
}
