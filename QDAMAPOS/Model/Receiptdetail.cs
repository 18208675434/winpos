using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace QDAMAPOS.Model
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
        public long starttime { get; set; }
        public long endtime { get; set; }


        public string saleclerkphone { get; set; }
        public string shopid { get; set; }
        public string DeviceSN { get; set; }
        public string createurlip { get; set; }
    }


    //public class OrderPriceDetail
    //{
    //    public string title { get; set; }
    //    public string amount { get; set; }
    //    public int highlight { get; set; }
    //    public string subtitle { get; set; }
    //}

    //public class Devicecode
    //{
    //    public string title { get; set; }
    //    public string amount { get; set; }
    //    public int highlight { get; set; }
    //}

    //public class Serial
    //{
    //    public string title { get; set; }
    //    public string amount { get; set; }
    //    public int highlight { get; set; }
    //}

    //public class Shiftcode
    //{
    //    public string title { get; set; }
    //    public string amount { get; set; }
    //    public int highlight { get; set; }
    //}

    //public class Title
    //{
    //    public string title { get; set; }
    //    public string amount { get; set; }
    //    public int highlight { get; set; }
    //}

    //public class Totalamount
    //{
    //    public string title { get; set; }
    //    public string amount { get; set; }
    //    public int highlight { get; set; }
    //}

    //public class Basicinfo
    //{
    //    public string title { get; set; }
    //    public string amount { get; set; }
    //    public int highlight { get; set; }
    //    public string subtitle { get; set; }
    //}

    //public class Incomedetail
    //{
    //    public string title { get; set; }
    //    public string amount { get; set; }
    //    public int highlight { get; set; }
    //    public string subtitle { get; set; }
    //}

    //public class Bottomdetail
    //{
    //    public string title { get; set; }
    //    public string amount { get; set; }
    //    public int highlight { get; set; }
    //    public string subtitle { get; set; }
    //}

}
