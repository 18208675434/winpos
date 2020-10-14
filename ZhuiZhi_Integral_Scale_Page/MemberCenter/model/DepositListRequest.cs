using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model
{
   public class DepositListRequest
    {
           /// <summary>
           /// 
           /// </summary>
           public string endtime { get; set; }
           /// <summary>
           /// 
           /// </summary>
         //  public long memberid { get; set; }
           /// <summary>
           /// 
           /// </summary>
           public bool needdetail { get; set; }
           /// <summary>
           /// 
           /// </summary>
           public string operatorid { get; set; }
           /// <summary>           
           /// 
           /// </summary>
           public string operatorphone { get; set; }
           /// <summary>
           /// 
           /// </summary>
           public int page { get; set; }
           /// <summary>
           /// 
           /// </summary>
           public string pagination { get; set; }
           /// <summary>
           /// 
           /// </summary>
           public string phone { get; set; }
           /// <summary>
           /// 
           /// </summary>
           public int size { get; set; }
           /// <summary>
           /// 
           /// </summary>
           public string sortdirection { get; set; }
           /// <summary>
           /// 
           /// </summary>
           public string sorttype { get; set; }
           /// <summary>
           /// 
           /// </summary>
           public int startIndex { get; set; }
           /// <summary>
           /// 
           /// </summary>
           public string starttime { get; set; }
           /// <summary>
           /// 
           /// </summary>
           public string tenantid { get; set; }
        /// <summary>
        /// 充值金额
        /// </summary>
        public decimal rewardamount { get; set; }
        /// <summary>
        /// 充值方式
        /// </summary>

        public string fangshi { get; set; }
        /// <summary>
        /// 是否可以退款
        /// </summary>
        public bool refundable { get; set; }
        public string id { get; set; }
    }
}
