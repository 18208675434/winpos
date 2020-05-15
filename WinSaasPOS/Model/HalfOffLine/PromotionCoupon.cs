using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinSaasPOS.Model.promotionaction;

namespace WinSaasPOS.Model.HalfOffLine
{
    /// <summary>
    /// 获取会员标签信息
    /// </summary>
    public class PromotionCoupon {
//"catalog": "Cash",
//        "amount": 3.0,
//        "desc": "自动化测试用，不要动",
//        "name": "自动化测试用长期有效平台优惠券",
//        "enabled": true,
//        "ordersubtype": 8,
//        "promotionrealmtype": "all",
//        "sendruletype": "customer.service",
//        "shoprealmdetail": "全部门店可用",
//        "districtscope": "100,216,209,218,",
//        "shopscope": "",
//        "tenantid": "0210000001",
//        "exchangepoints": 0.0,
//        "ordersubtypedesc": "收银机可用",
//        "enabledfrom": 1580486400000,
//        "enabledto": 1738339200000,
//        "availableskudesc": "全场商品可用",
//        "availableshopdesc": "全部门店可用",
//        "promotioncode": "194042652021235712",
//        "couponcode": "223486320259915776"

    public static  String CASH = "Cash";
    public static  String CASH_REDUCTION = "CashReduction";
    public static  String DISCOUNT_COUPON = "DiscountCoupon";

    public String catalog;
    public decimal amount;
    public String name;
    public bool enabled;
    public int ordersubtype;
    public String promotionrealmtype;
    public int selectstate;
    public String ordersubtypedesc;
    public long enabledfrom;
    public long enabledto;
    public String availableskudesc;
    public String availableshopdesc;
    public String promotioncode;
    public String couponcode;
    public decimal orderminamount;
    public decimal discountlimitation;
    public Decimal discountamt = WinSaasPOS.Model.Promotion.CommonConstant.ZERODECIMAL;//券抵扣金额
    public PromoAction promoAction;

}

}
