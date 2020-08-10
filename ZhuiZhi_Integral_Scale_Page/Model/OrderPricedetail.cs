using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Model
{
    [Serializable]
    public class OrderPriceDetail
    {

        public string title { get; set; }
        public string amount { get; set; }
        public int highlight { get; set; }
        public string subtitle { get; set; }
    }
}
