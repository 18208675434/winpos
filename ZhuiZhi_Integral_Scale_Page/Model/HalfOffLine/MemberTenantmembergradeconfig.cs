using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Model.HalfOffLine
{
    /// <summary>
    /// 获取会员等级商户设置
    /// 
    /// </summary>
    public class MemberTenantmembergradeconfig
    {
        public bool enable { get; set; }
        public int pointpergrowth { get; set; }
        public String cleangrowthmonth { get; set; }
        public String cleangrowthdate { get; set; }
        public bool cleanall { get; set; }

    
    }

}
