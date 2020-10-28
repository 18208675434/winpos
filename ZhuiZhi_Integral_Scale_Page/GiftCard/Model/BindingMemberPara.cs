using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.GiftCard.Model
{
    public class BindingMemberPara
    {
        /// <summary>
        /// 
        /// </summary>
        public long batchid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> cardnoes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long depositmemberid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool fromb2b { get; set; }
        /// <summary>
        /// 购买人会员id
        /// </summary>
        public string memberid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string @operator { get; set; }
        /// <summary>
        /// 充值手机号
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tenantid { get; set; }


    }
}
