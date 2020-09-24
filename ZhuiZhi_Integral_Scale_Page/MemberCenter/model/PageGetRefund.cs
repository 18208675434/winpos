using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model
{
    public class PageGetRefund
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
    public class RowsItems
    {
        public int balance { get; set; }
        public int capitalrefundamount { get; set; }
        public string createdat { get; set; }
        public string createdby { get; set; }
        public string customerpaycod { get; set; }
        public int depositbillid { get; set; }
        public int id { get; set; }
        public int memberid { get; set; }
        public string Operator { get; set; }
        public string operatorphone { get; set; }
        public string paymode { get; set; }
        public string phone { get; set; }
        public int refundtotalamount { get; set; }
        public int refundtype { get; set; }
        public string refundtypeorapi { get; set; }
        public string rewardrefundamout { get; set; }
        public string shopid { get; set; }
        public string shopname { get; set; }
        public string status { get; set; }
        public string tenanid { get; set; }
        public string updatedat { get; set; }
        public string updatedby { get; set; }
    }
}
