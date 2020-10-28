using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Model.HalfOffLine
{
    public class MemberrightsItem
    {
        public bool enable;
        public float onemonthfee;
        public Tenantmemberrightsdiscountconfig tenantmemberrightsdiscountconfig;
        public Tenantmemberrightsfreightfreeconfig tenantmemberrightsfreightfreeconfig;

        public bool memberrightsfunctionenable;
    }

    public class Tenantmemberrightsfreightfreeconfig
    {
        public bool freightfreeenabled;
        public String title;
        public String description;
    }
}
