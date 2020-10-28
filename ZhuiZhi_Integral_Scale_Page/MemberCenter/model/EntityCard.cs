using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model
{
    public class EntityCard
    {
        public decimal balance { get; set; }
        public decimal depositbalance { get; set; }
        public string id { get; set; }
        public string memberid { get; set; }
        public string outcardid { get; set; }
        public string password { get; set; }
        public decimal rewardbalance { get; set; }
        public string tenantid { get; set; }
    }
}
