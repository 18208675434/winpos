using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Model.HalfOffLine
{
    public class Memberoperationitem
    {
        public String id { get; set; }
        public String memberid { get; set; }
        public long createdat { get; set; }
        public List<long> tagids { get; set; }
    }
}
