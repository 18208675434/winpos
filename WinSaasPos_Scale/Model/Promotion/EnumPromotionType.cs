using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinSaasPOS_Scale.Model.Promotion
{
    public class EnumPromotionType
    {

        //无条件通过
        public static String ALWAYS_PASS = "always.pass";
        //商品ID匹配
        public static String GOODS_ID_MATCH = "goodsid.match";
        //会员等级阈值
        public static String GRADE_THRESHOLD = "grade.threshold";
        //订单金额(适用商品)阈值
        public static String ORDER_AMOUNT_THRESHOLD = "order.amount.threshold";
        //订单金额(所有商品)阈值
        public static String ORDER_TOTAL_AMOUNT_THRESHOLD = "order.total.amount.threshold";
        //订单金额(适用商品)和会员等级阈值
        public static String ORDER_AMOUNT_GRADE_THRESHOLD = "order.amount.threshold.member.grade";
        //订单金额(所有商品)和会员等级阈值
        public static String ORDER_TOTAL_AMOUNT_GRADE_THRESHOLD = "shipping.amount.threshold";
        //订单金额阈值(计算运费优惠)
        public static String SHIPPING_AMOUNT_THRESHOLD = "shipping.amount.threshold";
        // 商品数量条件
        public static String ITEM_COUNT_THRESHOLD = "item.count.threshold";


        //    ALWAYS_PASS("always.pass","无条件通过"),
        //
        //    GOODS_ID_MATCH("goodsid.match","商品ID匹配"),
        //
        //    GRADE_THRESHOLD("grade.threshold","会员等级阈值"),
        //
        //    ORDER_AMOUNT_THRESHOLD("order.amount.threshold","订单金额(适用商品)阈值"),
        //
        //    ORDER_TOTAL_AMOUNT_THRESHOLD("order.total.amount.threshold","订单金额(所有商品)阈值"),
        //
        //    ORDER_AMOUNT_GRADE_THRESHOLD("order.amount.threshold.member.grade","订单金额(适用商品)和会员等级阈值"),
        //
        //    ORDER_TOTAL_AMOUNT_GRADE_THRESHOLD("order.total.amount.grade.threshold","订单金额(所有商品)和会员等级阈值"),
        //
        //    SHIPPING_AMOUNT_THRESHOLD("shipping.amount.threshold","订单金额阈值(计算运费优惠)"),
        //
        //    ITEM_COUNT_THRESHOLD("item.count.threshold","商品数量条件");

        //全场可用
        public static String REALM_ALL = "all";
        //商品分类
        public static String REALM_CATALOG = "catalog";
        //多商品
        public static String REALM_GOODS = "goods";
        //分类和商品混合
        public static String REALM_MIXED = "mixed";
        //
        //    public enum PromotionRealm {
        //
        //
        ////        ALL("all", "全场可用"),
        ////        CATALOG("catalog", "商品分类"),
        ////        GOODS("goods", "多商品"),
        ////        MIXED("mixed", "分类和商品混合");
        //
        //
        //    }


        //    MARKET_PRICE("market", "市场价"),
        //    PROMOTION_PRICE("promotion", "促销价"),
        //    ONLINE_PRICE("member", "会员价"),
        //    MEMBER_DISCOUNT_PRICE("member.discount", "会员折扣价"),
        //    PROMOTION_DISCOUNT_PRICE("promotion.discount", "折扣价"),
        //    SECOND_PRICE("seckill", "秒杀价"),
        //    GROUP_PRICE("group.price", "拼团价"),
        //    MEMBER_GRADE_PRICE("member.grade.price", "会员等级价");

        //市场价
        public static String MARKET_PRICE = "market";
        //促销价
        public static String PROMOTION_PRICE = "promotion";
        //会员价
        public static String ONLINE_PRICE = "member.discount";
        //会员折扣价
        public static String MEMBER_DISCOUNT_PRICE = "member.discount";
        //折扣价
        public static String PROMOTION_DISCOUNT_PRICE = "promotion.discount";
        //秒杀价
        public static String SECOND_PRICE = "seckill";
        //拼团价
        public static String GROUP_PRICE = "group.price";
        //会员等级价
        public static String MEMBER_GRADE_PRICE = "member.grade.price";


        //    FIXED_AMOUNT_OFF("fix.amount.off", "整单满减"),
        //    STEP_AMOUNT_OFF("step.amount.off", "阶梯满减"),
        //    DYNAMIC_AMOUNT_OFF("dynamic.amount.off", "每金额满减"),
        //    PROMOTION_PRICE("promotion.price", "价格直降"),
        //    MEMBER_PRICE("member.price", "会员特价"),
        //    SECKILL_PRICE("seckill.price", "秒杀价格"),
        //    PROMOTION_PRICE_DISCOUNT("promotion.price.discount", "单品促销价折扣"),
        //    MEMBER_PRICE_DISCOUNT("member.price.discount", "单品会员价折扣"),
        //    MEMBER_GRADE_PRICE_DISCOUNT("member.grade.price.discount", "会员等级折扣"),
        //    DISCOUNT("discount", "打折"),
        //    STEP_DISCOUNT_OFF("step.discount.off", "阶梯满折"),
        //    FIXED_DISCOUNT_OFF("fix.discount.off", "整单满折"),
        //    DYNAMIC_DISCOUNT("dynamic.discount", "翻倍循环打折"),
        //    DESIGNATED_DISCOUNT_OFF("designated.discount.off", "第N件N折"),
        //    CYCLE_DESIGNATED_DISCOUNT_OFF("cycle.designated.discount.off", "第N件N折"),
        //    DESIGNATED_PRICE("designated.price", "第N件N元"),
        //    DYNAMIC_DESIGNATED_PRICE("dynamic.designated.price", "翻倍第N件N元"),
        //    ADDITIONAL_BUY("additional.buy", "换购"),
        //    PACKAGE_SELLING("package.selling", "N元N件");

        //整单满减
        public static String FIXED_AMOUNT_OFF_ACTION = "fix.amount.off";
        //阶梯满减
        public static String STEP_AMOUNT_OFF_ACTION = "step.amount.off";
        //每金额满减
        public static String DYNAMIC_AMOUNT_OFF_ACTION = "dynamic.amount.off";
        //价格直降
        public static String PROMOTION_PRICE_ACTION = "promotion.price";
        //会员特价
        public static String MEMBER_PRICE_ACTION = "member.price";
        //秒杀价格
        public static String SECKILL_PRICE_ACTION = "seckill.price";
        //单品促销价折扣
        public static String PROMOTION_PRICE_DISCOUNT_ACTION = "promotion.price.discount";
        //单品会员价折扣
        public static String MEMBER_PRICE_DISCOUNT_ACTION = "member.price.discount";
        //会员等级折扣
        public static String MEMBER_GRADE_PRICE_DISCOUNT_ACTION = "member.grade.price.discount";
        //打折
        public static String DISCOUNT_ACTION = "discount";
        //阶梯满折
        public static String STEP_DISCOUNT_OFF_ACTION = "step.discount.off";
        //整单满折
        public static String FIXED_DISCOUNT_OFF_ACTION = "fix.discount.off";
        //翻倍循环打折
        public static String DYNAMIC_DISCOUNT_ACTION = "dynamic.discount";
        //第N件N折
        public static String DESIGNATED_DISCOUNT_OFF_ACTION = "designated.discount.off";
        //第N件N折
        public static String CYCLE_DESIGNATED_DISCOUNT_OFF_ACTION = "cycle.designated.discount.off";
        //第N件N元
        public static String DESIGNATED_PRICE_ACTION = "designated.price";
        //翻倍第N件N元
        public static String DYNAMIC_DESIGNATED_PRICE_ACTION = "dynamic.designated.price";
        //换购
        public static String ADDITIONAL_BUY_ACTION = "additional.buy";
        //N元N件
        public static String PACKAGE_SELLING_ACTION = "package.selling";

        //订单级别的促销
        public static String PromotionType_ORDER = "O";
        //收银机促销
        public static String orderSubTypeBitValue = "8";

    }

}
