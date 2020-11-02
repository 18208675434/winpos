using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WinSaasPOS.Model
{


    public class CreateOrderResult
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public string orderid { get; set; }
        /// <summary>
        /// 是否需要继续支付 1为需要
        /// </summary>
        public int continuepay { get; set; }

        public int makesure { get; set; }
    }

}
