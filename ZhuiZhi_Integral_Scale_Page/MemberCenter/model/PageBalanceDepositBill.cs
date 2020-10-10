using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model
{
    public class PageBalanceDepositBill
    {
        /// <summary>
        /// 
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<RowsItem> rows { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int size { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int total { get; set; }
    }

    public class RowsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public decimal amount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string couponname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string createdat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string createdby { get; set; }
   
        /// <summary>
        /// 
        /// </summary>
        public string deposittype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gateway { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string memberid { get; set; }
        [JsonProperty("operator")]
        public string operatorname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string operatorphone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string paymode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal rewardamount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rewardcoupon { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string shopid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string shopname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tenantid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tradedate { get; set; }
    }





}
