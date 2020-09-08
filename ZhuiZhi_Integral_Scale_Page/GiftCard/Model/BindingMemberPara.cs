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
        public List<long> cardnoes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long depositmemberid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool fromb2b { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long memberid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string @operator { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tenantid { get; set; }


    }
}
