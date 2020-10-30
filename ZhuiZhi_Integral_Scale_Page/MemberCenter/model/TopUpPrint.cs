using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model
{
    public class TopUpPrint
    {

        public string id { get; set; }
        public string tenantid { get; set; }

        public string status { get; set; }

        public string paymode { get; set; }

        public decimal amount { get; set; }

        public string createdat { get; set; }

        /// <summary>
        /// 账户余额
        /// </summary>
        public decimal balance { get; set; }

        /// <summary>
        /// 充值方式
        /// </summary>
        public string paymodeforapi { get; set; }

        public string phone { get; set; }

        /// <summary>
        /// 赠送金额
        /// </summary>
        public decimal rewardamount { get; set; }

        public decimal realCash { get; set; }//实付,打印小票使用

        public bool isEntityCardBatchSale { get; set; }
    }
}
