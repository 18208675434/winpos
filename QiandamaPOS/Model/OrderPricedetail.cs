using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QiandamaPOS.Model
{
    [Serializable]
    public class Orderpricedetail
    {
        public string title { get; set; }
        public string amount { get; set; }
        public int highlight { get; set; }
    }
}
