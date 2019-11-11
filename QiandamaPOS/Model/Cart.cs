using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace QiandamaPOS.Model
{



    [Serializable]
    public class orderpromotion
{
public string title { get; set; }
public string amount { get; set; }
public int highlight { get; set; }
}





    [Serializable]
    public class Cart 
    {

        public string title { get; set; }
        public decimal totalpayment { get; set; }
        public List<Product> products { get; set; }
        public Orderpricedetail[] orderpricedetails { get; set; }
        public Tenant tenant { get; set; }
        public Pickupinfo pickupinfo { get; set; }
        public string shopid { get; set; }
        public decimal productcount { get; set; }
        public string ordertype { get; set; }
        public int ordersubtypeflag { get; set; }
        public int pointpayoption { get; set; }
        public int facepayoption { get; set; }
        public int cashpayoption { get; set; }
        public int selfpickenabled { get; set; }
        public Availablecoupon[] availablecoupons { get; set; }
        public Unavailablecoupon[] unavailablecoupons { get; set; }
        public Pointinfo pointinfo { get; set; }
        public int selectcouponcount { get; set; }
        public string orderplaceid { get; set; }
        public decimal promoamt { get; set; }
        public decimal producttotalamt { get; set; }
        public int couponpromoamt { get; set; }
        public decimal totalpromoamt { get; set; }
        public orderpromotion[] orderpromotions { get; set; }
        public decimal memberpromo { get; set; }
        public decimal membertotalpayment { get; set; }
        public int memberlogin { get; set; }
        public Paymenttypes paymenttypes { get; set; }
        public decimal cashpayamt { get; set; }
        public decimal cashchangeamt { get; set; }
        public decimal changehandleamt { get; set; }
        public decimal payamtbeforecash { get; set; }

        public decimal cashcouponamt { get; set; }

        public Cart Clone()
        {
            throw new NotImplementedException();
        }

        public Cart qianClone()
        {
            return (Cart)this.MemberwiseClone();
        }
    }
    [Serializable]
    public class Tenant
    {
        public string tenantid { get; set; }
    }
    [Serializable]
    public class Pickupinfo
    {
        public string shopid { get; set; }
        public string shopname { get; set; }
        public string shopaddress { get; set; }
        public string shoptelephone { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string distance { get; set; }
    }
    [Serializable]
    public class Pointinfo
    {
        public string availablepoints { get; set; }
        public decimal availablepointsamount { get; set; }
        public string availablepointsmsg { get; set; }
        public string totalpoints { get; set; }
    }
    [Serializable]
    public class Paymenttypes
    {
        public int cashenabled { get; set; }
        public int onlineenabled { get; set; }
        public int facepayenabled { get; set; }
        public int balancepayenabled { get; set; }
    }
    [Serializable]
    public class Product
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
        public string liststyle { get; set; }
        public int pricetagid { get; set; }
        public string pricetag { get; set; }
        public int applyreturn { get; set; }
        public int withexchange { get; set; }
        public string[] tags { get; set; }

        //销售单位
        public string saleunit { get; set; }

        //商品名称
        public string skuname { get; set; }

        /// <summary>
        /// 一级分类ID
        /// </summary>
        public string firstcategoryid { get; set; }

        /// <summary>
        /// 一级分类名称
        /// </summary>
        public string firstcategoryname { get; set; }

        public string shopid { get; set; }

        public int isQueryBarcode { get; set; }

        public bool weightflag { get; set; }


        public Product ThisClone()
        {
            return (Product)this.MemberwiseClone();
        }
    }



    [Serializable]
    public class Availablecoupon
    {
        public string catalog { get; set; }
        public decimal amount { get; set; }
        public string desc { get; set; }
        public string name { get; set; }
        public bool enabled { get; set; }
        public int ordersubtype { get; set; }
        public string promotionrealmtype { get; set; }
        public string sendruletype { get; set; }
        public string shoprealmdetail { get; set; }
        public string districtscope { get; set; }
        public string shopscope { get; set; }
        public string tenantid { get; set; }
        public decimal exchangepoints { get; set; }
        public int selectstate { get; set; }
        public decimal orderminamount { get; set; }
        public string ordersubtypedesc { get; set; }
        public long enabledfrom { get; set; }
        public long enabledto { get; set; }
        public string availableskudesc { get; set; }
        public string availableshopdesc { get; set; }
        public string promotioncode { get; set; }
        public string couponcode { get; set; }
    }
    [Serializable]
    public class Unavailablecoupon
    {
        public string catalog { get; set; }
        public decimal amount { get; set; }
        public string desc { get; set; }
        public string name { get; set; }
        public bool enabled { get; set; }
        public int ordersubtype { get; set; }
        public string promotionrealmtype { get; set; }
        public string sendruletype { get; set; }
        public string shoprealmdetail { get; set; }
        public string districtscope { get; set; }
        public string shopscope { get; set; }
        public string tenantid { get; set; }
        public decimal exchangepoints { get; set; }
        public string invalidreason { get; set; }
        public decimal orderminamount { get; set; }
        public string ordersubtypedesc { get; set; }
        public long enabledfrom { get; set; }
        public long enabledto { get; set; }
        public string availableskudesc { get; set; }
        public string availableshopdesc { get; set; }
        public string promotioncode { get; set; }
        public string couponcode { get; set; }
    }


}
