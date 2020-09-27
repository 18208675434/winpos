using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model
{
    public class ClassPayment
    {
        public string id { get; set; }
        public string tenantid { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public bool allshopenable { get; set; }
        public bool discountsoverlay { get; set; }
        public bool rechargeenable { get; set; }
        public bool needcouponcode { get; set; }
        public bool locked { get; set; }
        public decimal defaultamt { get; set; }
        public string remark { get; set; }
        public string createdat { get; set; }
        public bool posenable { get; set; }

    }
}
