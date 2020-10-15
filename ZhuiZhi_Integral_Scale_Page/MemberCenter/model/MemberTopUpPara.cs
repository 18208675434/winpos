using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model
{
    public class MemberTopUpPara
    {
        public decimal rewardamount  { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal amount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string birthday { get; set; }
        /// <summary>
        /// 其他支付方式
        /// </summary>
        public string customerpaycode  { get; set; }
        /// <summary>
        /// 是否自动根据充值模板匹配赠金  自定义传false  非自定义 true
        /// </summary>
        public bool autoreward  { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long memberid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string oldentitycardnumber { get; set; }
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
        public string remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string shopid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string smscode { get; set; }
    }

}
