using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Model.HalfOffLine
{
    /// <summary>
    /// 当前会员等级
    /// </summary>
    public class Gradesettinggetgrade
    {
        public String id { get; set; }
        public String tenantid { get; set; }
        public int grade { get; set; }
        public String name { get; set; }
        public int growth { get; set; }
        public List<Coupons> coupons { get; set; }
        public MemberDiscount discount { get; set; }
        public bool enable { get; set; }
        public Freightfree freightfree { get; set; }

    }


    public class Freightfree
    {
        private bool freightfreeenabled { get; set; }
    }
}
