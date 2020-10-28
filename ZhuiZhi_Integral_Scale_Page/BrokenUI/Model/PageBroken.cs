using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.BrokenUI.Model
{
   public  class PageBroken
    {
       public int page { get; set; }
       public List<BrokenInfo> rows { get; set; }
       public int size { get; set; }
       public int total { get; set; }
    }



    public class ParaPageBroken
    {
        public string createdatend { get; set; }
        public string createdatstart { get; set; }
        public bool needdetail { get; set; }
        public int page { get; set; }
        public bool pagination { get; set; }
        public string shopid { get; set; }
        public int size { get; set; }
        public string sortdirection { get; set; }
        public string sorttype { get; set; }
        public int startIndex { get; set; }
        public string tenantid { get; set; }
    }

}
