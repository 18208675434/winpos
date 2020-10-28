using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Model
{

    public class PrintDetail
    {
        public string title { get; set; }
        public decimal totalpayment { get; set; }
        public ProductDetail[] products { get; set; }
        public OrderPriceDetail[] orderpricedetails { get; set; }
        public string shopid { get; set; }
        public decimal productcount { get; set; }
        public int ordersubtypeflag { get; set; }
        public string orderid { get; set; }
        public string status { get; set; }
        public int statusflag { get; set; }
        public string paymentmethod { get; set; }
        public string ordersubtype { get; set; }
        public string orderat { get; set; }
        public string shopname { get; set; }
        public string earnpoint { get; set; }
        public string customerphone { get; set; }
        public PointInfo[] pointinfo { get; set; }
        public Paydetail[] paydetail { get; set; }
        public Payinfo[] payinfo { get; set; }
        public string cashier { get; set; }
        public string devicecode { get; set; }

        public decimal productamount { get; set; }

        public string shopservicephone { get; set; }
        public string bottommessage { get; set; }

        public List<PointInfo> memberinfo { get; set; }

        public List<OrderPriceDetail> orderbasicinfo { get; set; }
    }

    public class ProductDetail
    {
        public string skucode { get; set; }
        public string title { get; set; }
        public int num { get; set; }
        public string mainimg { get; set; }
        public int selectstate { get; set; }
        public string specdesc { get; set; }
        public Price price { get; set; }
        public string barcode { get; set; }
        public int goodstagid { get; set; }
        public string goodstag { get; set; }
        public decimal specnum { get; set; }
        public int spectype { get; set; }
        public string orderitemid { get; set; }
        public int applyreturn { get; set; }
        public int withexchange { get; set; }
    }

   
    
public class PointInfo
{
public string title { get; set; }
public string amount { get; set; }
public int highlight { get; set; }
}


    public class Paydetail
    {
        public string title { get; set; }
        public string amount { get; set; }
        public int highlight { get; set; }
    }

    public class Payinfo
    {
        public string type { get; set; }
        public decimal amount { get; set; }
    }

}
