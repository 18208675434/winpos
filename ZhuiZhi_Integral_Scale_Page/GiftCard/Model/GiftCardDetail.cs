using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.GiftCard.Model
{
    public class GiftCardDetail
    {


        /// <summary>
        /// 面值
        /// </summary>
        public decimal amount { get; set; }
        /// <summary>
        /// 剩余金额（仅用于代金卡）
        /// </summary>
        public decimal availableamount { get; set; }
        /// <summary>
        /// pici
        /// </summary>
        public long batchid { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public long cardno { get; set; }
        /// <summary>
        /// 预付卡类型 1：充值卡 2：代金卡
        /// </summary>
        public int cardtype { get; set; }
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
        public string enablefrom { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string enableto { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 所属会员id
        /// </summary>
        public long memberid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tenantid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string updatedat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string updatedby { get; set; }
    }
}
