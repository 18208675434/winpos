using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model
{
    public class PageBalanceDepositRefundBill
    {
        /// <summary>
        /// 
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<RowsRefundItem> rows { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int size { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int total { get; set; }
    }

    public class RowsRefundItem
    {
        public string id { get; set; }
        public string createdat { get; set; }
        public string phone { get; set; }
        public string depositbillid { get; set; }
        public decimal refundtotalamount { get; set; }
        public decimal capitalrefundamount { get; set; }
        public decimal rewardrefundamount { get; set; }
        public int refundtype { get; set; }
        public string refundtypeforapi { get; set; }
        [JsonProperty("operator")]
        public string operatorname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string operatorphone { get; set; }
    }
}
