using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QDAMAPOS.Model
{

    public class TVShowSkus
    {
        public int code { get; set; }
        public List<TVProduct> data { get; set; }
        public string message { get; set; }
    }



    public class PosActivesSku
    {
        public string tenantid { get; set; }
        public string shopid { get; set; }
        public List<TVProduct> posactiveskudetails { get; set; }
    }

    public class TVProduct
    {
        public string categoryid { get; set; }
        public decimal originalprice { get; set; }
        public decimal promotionprice { get; set; }
        public string saleunit { get; set; }
        public string skucode { get; set; }
        public string skuname { get; set; }
        public bool weightflag { get; set; }
    }


}
