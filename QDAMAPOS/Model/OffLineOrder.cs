using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QDAMAPOS.Model
{


    public class OffLineOrder
    {
        public decimal cashpayamt { get; set; }
        public decimal changehandleamt { get; set; }
        public string createurlip { get; set; }
        public string devicecode { get; set; }
        public int fromwinpos { get; set; }
        public int hasrefunded { get; set; }
        public string offlineorderid { get; set; }
        public long orderat { get; set; }
        public decimal origintotal { get; set; }
        public string outercode { get; set; }
        public decimal  pricetotal { get; set; }
        public List<Product> products { get; set; }
        public decimal  promoamt { get; set; }
        public string promotioncode { get; set; }
        public long refundtime { get; set; }
        public string salesclerkphone { get; set; }
        public string shopid { get; set; }



        public decimal cashchangeamt { get; set; }
    }


}
