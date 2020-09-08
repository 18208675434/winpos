using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.GiftCard.Model
{
    public class GiftCardPaySuccess
    {


        public class CardPaySuccess
        {
            /// <summary>
            /// 余额抵扣金额 ,
            /// </summary>
            public decimal balancepayamt { get; set; }
            /// <summary>
            /// 配送时间提示文案 ,
            /// </summary>
            public string deliverydesc { get; set; }
            /// <summary>
            /// 点餐核销码用于打单 ,
            /// </summary>
            public string foodpickcode { get; set; }
            /// <summary>
            ///  取餐码 
            /// </summary>
            public string foodserialid { get; set; }
            /// <summary>
            /// 聚餐码 桌号信息 ,
            /// </summary>
            public Foodserialinfo foodserialinfo { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long orderid { get; set; }
            /// <summary>
            ///  订单类型区分 扫码购1 收银机2 配送单3 自提4 虚拟单5 堂食6 ,
            /// </summary>
            public int ordersubtypeflag { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string paydetails { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Pickupinfo pickupinfo { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public decimal pointpayamt { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public decimal productcount { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<CardProduct> products { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string producttitle { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Redenvelopordervo redenvelopordervo { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string shop { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int tocompleteordercount { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<TocompleteordersItem> tocompleteorders { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public decimal wechatpayamt { get; set; }
        }

        public class Foodserialinfo
        {
            /// <summary>
            /// 
            /// </summary>
            public string foodserialid { get; set; }
            /// <summary>
            ///  类型 1取餐号 2桌号 3牌号
            /// </summary>
            public int type { get; set; }
        }

        public class Pickupinfo
        {
            /// <summary>
            /// 
            /// </summary>
            public string code { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string desc { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string distance { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string latitude { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string longitude { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string serialid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string shopaddress { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string shopid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string shopname { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string shoptelephone { get; set; }
            /// <summary>
            /// 类型 1:普通门店自提 2:自提点自提
            /// </summary>
            public int type { get; set; }
        }

        public class AddiniteminfosItem
        {
            /// <summary>
            /// 
            /// </summary>
            public int addintype { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public decimal price { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public decimal qty { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string skucode { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string skuname { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string typename { get; set; }
        }

        public class Adjustpricedesc
        {
            /// <summary>
            /// 
            /// </summary>
            public decimal amt { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string amtdesc { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string bannerdesc { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int type { get; set; }
        }

        public class Adjustpriceinfo
        {
            /// <summary>
            /// 
            /// </summary>
            public decimal amt { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public decimal beforeamt { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int type { get; set; }
        }

        public class PacktaginfosItem
        {
            /// <summary>
            /// 
            /// </summary>
            public decimal absentqty { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public decimal diffamtrefundweight { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public decimal packnum { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public decimal packsaleprice { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string packsaleunit { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string packskucode { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string packskuname { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public decimal packspecnum { get; set; }
            /// <summary>
            ///  标签 1已拣 2缺货 3换货
            /// </summary>
            public int packtag { get; set; }
        }


        public class ProductsItem
        {
            /// <summary>
            /// 
            /// </summary>
            public List<AddiniteminfosItem> addiniteminfos { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Adjustpricedesc adjustpricedesc { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Adjustpriceinfo adjustpriceinfo { get; set; }
            /// <summary>
            /// 是否退货商品 1是 0不是 ,
            /// </summary>
            public int applyreturn { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int available { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string barcode { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public decimal displayqty { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string exchangeids { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string firstdisplaycategory { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string goodstag { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int goodstagid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string liststyle { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string mainimg { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public decimal num { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int orderitemid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string originskucode { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int outordertime { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<PacktaginfosItem> packtaginfos { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Price price { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string pricetag { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int pricetagid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public decimal refundnum { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string relatepromocode { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int rootorderitemid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int selectstate { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string skucode { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string specdesc { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string specifiedamountrefunddesc { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int specnum { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int spectype { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string stockdesc { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public decimal stocknum { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string subtitle { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int supportspecifiedamountrefund { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<string> tags { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string title { get; set; }
            /// <summary>
            /// 是否涉及换货 1是 0否 ,
            /// </summary>
            public int withexchange { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string workshopid { get; set; }
        }

        public class Redenvelopordervo
        {
            /// <summary>
            /// 
            /// </summary>
            public decimal biggestamount { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string code { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string desc { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string sharebuoyimg { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string shareimg { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string shareupsimg { get; set; }
        }

        public class ActioninfosItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string actionname { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string actionurl { get; set; }
        }

        public class ChilddetailItem
        {
        }

        public class OrderpricedetailsItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string amount { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<ChilddetailItem> childdetail { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public decimal highlight { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string subtitle { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string title { get; set; }
        }


        public class Price
        {
            /// <summary>
            /// 
            /// </summary>
            public decimal originprice { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string originpricedesc { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public decimal originsaleprice { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int origintotal { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public decimal payamtafterpromo { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public decimal promoamt { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string purchaselimitdesc { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string purchaselimitsubdesc { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public decimal saleprice { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string salepricedesc { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public decimal specnum { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public decimal strikeout { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public decimal total { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public decimal totalwithaddinamt { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string unit { get; set; }
        }

        public class Tenant
        {
            /// <summary>
            /// 
            /// </summary>
            public string tenantid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string tenantname { get; set; }
        }

        public class TocompleteordersItem
        {
            /// <summary>
            /// 
            /// </summary>
            public List<ActioninfosItem> actioninfos { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string activitycode { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int afsorderflag { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int afsorderid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string completeat { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int dinersnumber { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string groupteamcode { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int makesure { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int memberpricepromoamt { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int multigroupbuyorder { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public decimal orderamount { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string orderat { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long orderid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<OrderpricedetailsItem> orderpricedetails { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string ordersubtype { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int ordersubtypeflag { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string ordertype { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string payendat { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string paymentmethod { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Pickupinfo pickupinfo { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int productcount { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<ProductsItem> products { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public decimal productsaleamt { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Redenvelopordervo redenvelopordervo { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string shopid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string shopname { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string status { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int statusflag { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string tablenumber { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Tenant tenant { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string title { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string togroupendat { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public decimal totalamount { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public decimal totalpayment { get; set; }
        }


    }
}
