using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinSaasPOS.Model.HalfOffLine
{
    public class MemberrightsItem
    {
        private bool enable;
        private float onemonthfee;
        private Tenantmemberrightsdiscountconfig tenantmemberrightsdiscountconfig;
        private Tenantmemberrightsfreightfreeconfig tenantmemberrightsfreightfreeconfig;

    }

    public class Tenantmemberrightsfreightfreeconfig
    {
        private bool freightfreeenabled;
        private String title;
        private String description;
    }
}
