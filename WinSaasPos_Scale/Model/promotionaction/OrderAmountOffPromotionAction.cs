using Maticsoft.Model;
using WinSaasPOS_Scale.Model.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinSaasPOS_Scale.Model.promotionaction
{
  public class OrderAmountOffPromotionAction : PromoAction {

    //试算优惠多少钱的

      protected override Decimal getDiscountValue(EvaluateScopePromotion evaluateScopePromotion, List<Product> products, DBPROMOTION_CACHE_BEANMODEL promotion)
      {
        try {
            Decimal scopeMaxDiscount = evaluateScopePromotion.getPromotionItemTotalAmt();
            Decimal discount = MoneyUtils.newMoney(promotion.PROMOACTIONCONTEXT);
            if (MoneyUtils.isFirstBiggerThanSecond(scopeMaxDiscount, discount) && MoneyUtils.isFirstBiggerThanSecond(
                    discount, Decimal.Zero)) {
                return discount;
            } else if (MoneyUtils.isFirstBiggerThanOrEqualToSecond(discount, scopeMaxDiscount)) {
                return scopeMaxDiscount;
            }
        } catch (Exception exp) {
            //exp.printStackTrace();
        }
        return Decimal.Zero;
    }


    //真正去计算和分摊
   // @Override
    public override void perform(EvaluateScopePromotion evaluateScopePromotion, List<Product> products, DBPROMOTION_CACHE_BEANMODEL promotion) {
        Decimal discount = getDiscountValue(evaluateScopePromotion, products, promotion);
        if (MoneyUtils.isFirstBiggerThanSecond(discount, Decimal.Zero)) {
            List<Product> distributeAmountList = processDistribute(evaluateScopePromotion, products, discount);
            if (CollectionUtils.isNotEmpty(distributeAmountList)) {
                addOrderItemPromotion(promotion, distributeAmountList);
            }
        }
    }

    //这是分摊的逻辑
   // @Override
    protected override List<Product> processDistribute(EvaluateScopePromotion cartBean, List<Product> orderItems, Decimal discount)
    {
//        //取促销适用的商品行记录
//        List<Integer> rows = cartBean.getListScope();
        //取总的金额，用来做分母用的，你可以直接取适用商品汇总的应付金额，不考虑兜底
        Decimal scopeSpAmt = cartBean.getPromotionItemTotalAmt();
        if (!MoneyUtils.isFirstBiggerThanSecond(scopeSpAmt, CommonConstant.ZERODECIMAL)) {
            return null;
        }
         Decimal finalScopeSpAmt = scopeSpAmt;
        //商品行明细
//        List<Product> orderItems = products;
        //底下这几判断可以要
//        Boolean bomChildPromo = (Boolean) context.get(PromotionCondition.VAR_BOM_CHILD_PROMO);
//        if(bomChildPromo != null && bomChildPromo){
//            orderItems = getOrder(context).getOrderItemsWithBom();
//        }

        //遍历商品行，取出适用的商品行的商品数据，并且标识该行当前享受的促销金额默认0，你可以用别的结构存储，可能安卓没有这个数据结构
        List<Product> distributeAmountList = new  List<Product>();
        for (int i = 0; i < orderItems.Count; i++) {
//            if (rows.contains(i)) {
            orderItems[i].PromoSubAmt =(CommonConstant.ZERODECIMAL);
//            ZZPair zzPair = new ZZPair(orderItems.get(i), orderItems.get(i).getpPaySubAmt(), orderItems.get(i).getpPaySubAmt(), CommonConstant.ZERODECIMAL);
            distributeAmountList.Add(orderItems[i]);
//            }
        }

//        List<ZzPair<OrderItemDto, Decimal>> distributeAmountList =
//                orderItems.stream()
//                        .filter(orderItem -> (!CollectionUtils.isNotEmpty(rows)) || rows.contains(orderItem.getRowNum()))
//                        .map(item -> new ZzPair<>(item, CommonConstant.ZERODECIMAL))
//                        .collect(Collectors.toList());
        //分摊逻辑，以上面的这过滤出来的商品信息为基础，然后按 当前遍历商品行的金额/总适用促销商品行的累计金额*折扣值   来分摊，要考虑分摊完整，所以校验最终金额等

        AmountDistributor.AmountDistributionRule amo = new AmountDistributor.AmountDistributionRule();
        
        amo.amountToBeDistribute=discount;
        amo.amountRateDenominator=finalScopeSpAmt;
        

        AmountDistributor.distribute(distributeAmountList,amo,getShopDefaultScale());
       //new AmountDistributor.AmountDistributionRule<ProductBean>() {
       //             @Override
       //             //用于分摊的值，折扣金额
       //             public BigDecimal amountToBeDistribute() {
       //                 return discount;
       //             }

       //             @Override//计算的分母
       //             public BigDecimal amountRateDenominator() {
       //                 return finalScopeSpAmt;
       //             }

       //             @Override//计算分子
       //             public BigDecimal amountRateNumerator(ProductBean t) {
       //                 return t.getpPaySubAmt();
       //             }

       //             @Override//单行最大分配的优惠上限，最大为 这一行的应付金额
       //             public BigDecimal maxDistributeValue(ProductBean t) {
       //                 return t.getpPaySubAmt();
       //             }

       //             @Override//当前商品行的优惠金额
       //             public BigDecimal currentValue(ProductBean t) {
       //                 return t.getpPromoSubAmt();
       //             }

       //             @Override//赋值新优惠金额的逻辑，默认是赋值，有些情况也可以考虑累加。这边默认就是赋值
       //             public void setNewValue(ProductBean t, BigDecimal bd) {
       //                 t.setpPromoSubAmt(bd);
       //             }

       //             @Override//用于标识保留几位精度，默认2位，香港1位
       //             public Integer getDefaultScale() {
       //                 return getShopDefaultScale();
       //             }
       //         });


        return distributeAmountList;
    }

    protected void addOrderItemPromotion(DBPROMOTION_CACHE_BEANMODEL promotion, List<Product> distributeAmountList) {
//        OrderCouponDto orderCoupon = getOrderCoupon(context);
//        fillCouponAction(actionContext, orderCoupon);


        for (int i = 0; i < distributeAmountList.Count; i++) {
            //设置商品行上的金额字段
//            Product Product = distributeAmountList.get(i).getProduct();
//            Decimal itemDiscount = distributeAmountList.get(i).getCurrentValue();

            Product Product = distributeAmountList[i];
            if (Product != null) {
                Product.PaySubAmt = MoneyUtils.substract(Product.PaySubAmt, Product.PromoSubAmt);
//                Product.getPrice().setTotal(Product.getpPaySubAmt().doubleValue());


                if (Product.offlinepromos == null)
                {
                    Product.offlinepromos = new List<OffLinePromos>();
                }
                //                Boolean evaluate = true;
                //记录促销信息，你可以用你 写法
                if (Product.offlinepromos != null)
                {
                    OffLinePromos offlinepromos = new OffLinePromos();

                    offlinepromos.promotioncode = promotion.CODE;
                    offlinepromos.outerpromocode = promotion.OUTERCODE;
                    offlinepromos.costcenterinfo = promotion.COSTCENTERINFO;
                    offlinepromos.promoaction = promotion.PROMOACTION;
                    offlinepromos.promosubtype = promotion.PROMOSUBTYPE;
                    offlinepromos.promotype = promotion.PROMOTYPE;
                    offlinepromos.promoamt = distributeAmountList[i].PromoSubAmt;

                    Product.offlinepromos.Add(offlinepromos);
                }
            }


//            Product.setpPromoSubAmt(distributeAmountList.get(i).getCurrentValue());


            Boolean evaluate = true;
            //记录促销信息，你可以用你 写法


        }

//        distributeAmountList.forEach(p -> {
//            OrderItemDto item = p.getValue0();
//            Decimal itemDiscount = p.getValue1();
//            item.setpPaySubAmt(MoneyUtils.substract(item.getpPaySubAmt(), itemDiscount));
//            item.setpPromoSubAmt(MoneyUtils.add(item.getpPromoSubAmt(), itemDiscount));
//            Boolean evaluate = context.get(PromotionCondition.VAR_CALCULATION_EVALUATE) == null ? null
//                    : (Boolean) context.get(PromotionCondition.VAR_CALCULATION_EVALUATE);
//            orderItemPromotionBuilder.addOrderItemPromotion(
//                    getOrder(context),
//                    item,
//                    itemDiscount,
//                    CommonConstant.ZERODECIMAL,
//                    OrderItemPromotionCategory.RULE,
//                    (PromotionCacheDto) context.get(PromotionCondition.VAR_PROMOTION),
//                    orderCoupon == null ? null : orderCoupon.getCouponCode(),
//                    (String) context.get(PromotionCondition.VAR_OPERATOR_ID),
//                    evaluate, getShopDefaultScale(context));
//        });

//        if (orderCoupon != null) {//This is coupon action.
//            orderCoupon.setPayAmt(discount);
//            orderCoupon.setpPayAmt(discount);
//            orderCoupon.setfPayAmt(CommonConstant.ZERODECIMAL);
//        }

//        header.setpPayAmt(MoneyUtils.substract(header.getpPayAmt(), discount));
//        header.setpPromoAmt(MoneyUtils.add(header.getpPromoAmt(), discount));
    }
}

}
