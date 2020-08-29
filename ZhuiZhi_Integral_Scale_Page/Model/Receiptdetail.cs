using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Model
{


    public class Receiptdetail
    {
        public string cashier { get; set; }
        public OrderPriceDetail devicecode { get; set; }
        public OrderPriceDetail serial { get; set; }
        public OrderPriceDetail shiftcode { get; set; }
        public OrderPriceDetail title { get; set; }
        public OrderPriceDetail totalamount { get; set; }
        public List<OrderPriceDetail> basicinfo { get; set; }
        public List<OrderPriceDetail> incomedetails { get; set; }
        public List<OrderPriceDetail> bottomdetails { get; set; }

        public List<OrderPriceDetail> payinfo { get; set; }
        public List<OrderPriceDetail> otherinfo { get; set; }

        public List<OrderPriceDetail> shopcashinfo { get; set; }
        public long starttime { get; set; }
        public long endtime { get; set; }



        public string saleclerkphone { get; set; }
        public string shopid { get; set; }
        public string DeviceSN { get; set; }
        public string createurlip { get; set; }

        public string shopname { get; set; }

        public string startserialcode { get; set; }

        public string endserialcode { get; set; }
    }

}
