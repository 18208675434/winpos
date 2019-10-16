using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QiandamaPOS.Model
{

    public class QueryOrder
    {
        public int hasnextpage { get; set; }
        public string lastorderid { get; set; }
        public Order[] orders { get; set; }
    }

    public class Order
    {
        /// <summary>
        /// 支付宝支付金额
        /// </summary>
        public string alipayamt { get; set; }
        /// <summary>
        /// 余额支付金额
        /// </summary>
        public string balanceamt { get; set; }
        /// <summary>
        /// 现金支付金额
        /// </summary>
        public string cashpayamt { get; set; }
        /// <summary>
        /// 微信支付金额
        /// </summary>
        public string wechatpayamt { get; set; }
        /// <summary>
        /// 银联云闪付金额
        /// </summary>
        public string ylpayamt { get; set; }

         /// <summary>
        /// 积分抵扣金额
        /// </summary>
        public string pointpayamt { get; set; }


        /// <summary>
        /// 代金券抵扣金额
        /// </summary>
        public string cashcouponamt { get; set; }
        

        public long orderat { get; set; }
        public string orderid { get; set; }
        public string title { get; set; }
        public decimal orderamt { get; set; }
        public string orderstatus { get; set; }

        /// <summary>
        /// 订单状态文案  5已退款不显示退款和补打   ！=5 显示退款和补打
        /// </summary>
        public int orderstatusvalue { get; set; }
        public int showprint { get; set; }


        public string customerphone { get; set; }


    }


}
