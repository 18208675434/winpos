using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Model
{


    public class OffLineReceipt
    {
        public string cashier { get; set; }
        public decimal cashtotalamt { get; set; }
        public string createurlip { get; set; }
        public string devicecode { get; set; }
        public long endtime { get; set; }
        public int hasprint { get; set; }
        public int id { get; set; }
        public decimal netsaleamt { get; set; }
        public string offlinereceiptid { get; set; }
        public int onlinemode { get; set; }
        public string operatetimestr { get; set; }
        public Receiptdetail receiptdetail { get; set; }
        public string saleclerkphone { get; set; }
        public string shopid { get; set; }
        public long starttime { get; set; }
        public decimal totalpayment { get; set; }
    }

    //public class Receiptdetail
    //{
    //    public OrderPriceDetail[] basicinfo { get; set; }
    //    public OrderPriceDetail[] bottomdetails { get; set; }
    //    public string cashier { get; set; }
    //    public OrderPriceDetail devicecode { get; set; }
    //    public DateTime endtime { get; set; }
    //    public OrderPriceDetail[] incomedetails { get; set; }
    //    public OrderPriceDetail serial { get; set; }
    //    public OrderPriceDetail shiftcode { get; set; }
    //    public DateTime starttime { get; set; }
    //    public OrderPriceDetail title { get; set; }
    //    public OrderPriceDetail totalamount { get; set; }
    //}

    //public class OrderPriceDetail 
    //{
    //    public string amount { get; set; }
    //    public int highlight { get; set; }
    //    public string subtitle { get; set; }
    //    public string title { get; set; }
    //}


}
