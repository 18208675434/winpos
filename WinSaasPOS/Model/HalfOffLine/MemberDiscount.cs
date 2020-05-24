using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinSaasPOS.Model.HalfOffLine
{
    public class MemberDiscount
    {
        public bool unionpromotion { get; set; }
        public String realmtype { get; set; }
        public List<String> catalogstoinclude { get; set; }
        public List<String> catalogstoexclude { get; set; }
        public List<String> skucodestoexclude { get; set; }
        public List<String> skucodestoinclude { get; set; }
        public float discount { get; set; }

    }
}
