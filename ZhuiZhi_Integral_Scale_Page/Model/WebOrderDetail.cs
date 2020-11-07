using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Model
{

    public class WebOrderDetail
    {

        public decimal givechangeamt { set; get; }
        public String orderid{set;get;}
        public String shopid{set;get;}
        public String shopname{set;get;}
        public long orderat{set;get;}
        public String flag{set;get;}
        public String ordersubtype{set;get;}
        public String ordersubtypevalue{set;get;}
        public String ordertypevalue{set;get;}
        public String orderstatus{set;get;}
        public String orderstatusvalue{set;get;}
        public String paytypedesc{set;get;}
        public decimal ppromoamt{set;get;}
        public decimal mealboxamt{set;get;}
        public decimal fpromoamt{set;get;}
        public decimal balancecapitalpayamt{set;get;}
        public decimal balancerewardpayamt{set;get;}
        public String earnpointamount{set;get;}
        public long payat{set;get;}
        public decimal originorderamt{set;get;}
        public decimal orderamt{set;get;}
        public decimal productamt{set;get;}
        public decimal freightamt{set;get;}
        public decimal promoamt{set;get;}
        public decimal changeamt{set;get;}
        public decimal payamtafterpromo{set;get;}
        public decimal pshoppromoamt{set;get;}
        public decimal pplatformpromoamt{set;get;}
        public decimal pointpayamt{set;get;}
        public decimal balancepayamt{set;get;}
        public decimal payamt{set;get;}
        public decimal cashpayamt{set;get;}
        public decimal alipayamt{set;get;}
        public decimal wechatpayamt{set;get;}
        public decimal ylpayamt{set;get;}
        public String customername{set;get;}
        public String registerphone{set;get;}
        public String receiverphone{set;get;}
        public String receiveraddress{set;get;}
        public String receiverdistrict{set;get;}
        public String deliverytype{set;get;}
        public String deliverystatus{set;get;}
        public String orderflag{set;get;}
        public String orderflagvalue{set;get;}
        public int supportspecifiedamountrefund{set;get;}
        public List<Orderitems> orderitems{set;get;}
        public List<PayDetailInfo> otherpaydetailinfos{set;get;}

    }
    public class Orderitems
    {
        public String orderitemid{set;get;}
        public String goodsid{set;get;}
        public String goodsname{set;get;}
        public decimal qty{set;get;}
        public String spec{set;get;}
        public decimal bulk{set;get;}
        public decimal withexchange{set;get;}
        public decimal listprice{set;get;}
        public decimal saleprice{set;get;}
        public decimal productamt{set;get;}
        public decimal ppromosubamt{set;get;}
        public decimal pskupromosubamt{set;get;}
        public decimal paysubamt{set;get;}
        public decimal ppointpayamt{set;get;}
        public int supportspecifiedamountrefund{set;get;}
        public decimal listpricetotal{set;get;}

    }

    public class PayDetailInfo
    {
        public decimal amount{set;get;}
        public String type{set;get;}
    }

    //public class ActionhistoriesItem
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string actiondesc { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string actiontime { get{set;get;} set{set;get;} }
    //}

    //public class TracesItem
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string acceptstation { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string accepttime { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string remark { get{set;get;} set{set;get;} }
    //}

    //public class LogisticsItem
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int id { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string logisticcode { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int orderid { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string ordertype { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string reason { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string shippercode { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string state { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string tenantid { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public List<TracesItem> traces { get{set;get;} set{set;get;} }
    //}

    //public class AddiniteminfosItem
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int addintype { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal price { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal qty { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string skucode { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string skuname { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string typename { get{set;get;} set{set;get;} }
    //}

    //public class OrderitempromotionsItem
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string costcenter { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string couponmode { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal fpromoamt { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal ppromoamt { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string promotioncode { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal promotioncost { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string promotype { get{set;get;} set{set;get;} }
    //}

    //public class SkuitempromotionsItem
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string costcenter { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string couponmode { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal fpromoamt { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal ppromoamt { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string promotioncode { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal promotioncost { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string promotype { get{set;get;} set{set;get;} }
    //}

    //public class OrderitemsItem
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public List<AddiniteminfosItem> addiniteminfos { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int bulk { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string goodsid { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string goodsname { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal listprice { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal listpricetotal { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int orderitemid { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public List<OrderitempromotionsItem> orderitempromotions { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal paysubamt { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal ppointpayamt { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal ppromosubamt { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal productamt { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal pskupromosubamt { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int qty { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int saleprice { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public List<SkuitempromotionsItem> skuitempromotions { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string spec { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string specifiedamountrefunddesc { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int supportspecifiedamountrefund { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int withexchange { get{set;get;} set{set;get;} }
    //}

    //public class Ordersettlementinfovo
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal accountamt { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal paychargeamt { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string payrate { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal thirdpartypaidamt { get{set;get;} set{set;get;} }
    //}

    //public class OtherpaydetailinfosItem
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal amount { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string type { get{set;get;} set{set;get;} }
    //}

    //public class WebOrderDetail
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public List<ActionhistoriesItem> actionhistories { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public long afsorderid { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal alipayamt { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal balancecapitalpayamt { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal balancepayamt { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal balancerewardpayamt { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string carriercompany { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string carriername { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string carrierphone { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string carriertype { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal cashpayamt { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal changeamt { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public List<int> childrenorderids { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal coupondeductionamt { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string customername { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string deliverystatus { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string deliverytype { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int earnpointamount { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string expecttime { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string flag { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal fpromoamt { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal freightamt { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal givechangeamt { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public List<LogisticsItem> logistics { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal mealboxamt { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal orderamt { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string orderat { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string orderflag { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int orderflagvalue { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int orderid { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public List<OrderitemsItem> orderitems { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public Ordersettlementinfovo ordersettlementinfovo { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string orderstatus { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string orderstatusvalue { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string ordersubtype { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string ordersubtypevalue { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string ordertypevalue { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal originorderamt { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public List<OtherpaydetailinfosItem> otherpaydetailinfos { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string outerorderid { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int parentorderid { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal payamt { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal payamtafterpromo { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string payat { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string paytypedesc { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal pointpayamt { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal pplatformpromoamt { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal ppromoamt { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal productamt { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal promoamt { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal pshoppromoamt { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string receiveraddress { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string receiverdistrict { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string receivername { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string receiverphone { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string refundreason { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string registerphone { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string remark { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string selfpickaddrdetail { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string selfpickaddrname { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string shopid { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string shopname { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int showexpressinput { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int supportspecifiedamountrefund { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal swipecardamt { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string tablenumber { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal thirdpartyfreight { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal thirdpartyplatformpayamt { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal wechatpayamt { get{set;get;} set{set;get;} }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public decimal ylpayamt { get{set;get;} set{set;get;} }
    //}


}
