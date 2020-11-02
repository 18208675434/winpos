using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WinSaasPOS_Scale.Model
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
