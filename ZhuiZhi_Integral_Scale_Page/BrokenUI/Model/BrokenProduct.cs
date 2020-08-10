using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.BrokenUI.Model
{
    public class BrokenProduct
    {
        public string skucode { get; set; }
        public string skuname { get; set; }
        public string title { get; set; }
        public int num { get; set; }
        public decimal specnum { get; set; }
        public bool weightflag { get; set; }

        public string unit { get; set; }

        public decimal saleprice { get; set; }

        public decimal deliveryprice { get; set; }

        public int RowNum { get; set; }
    }


}
