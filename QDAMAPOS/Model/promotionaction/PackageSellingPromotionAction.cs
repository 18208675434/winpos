using Maticsoft.Model;
using QDAMAPOS.Model;
using QDAMAPOS.Model.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QDAMAPOS.Model.promotionaction
{
  public class PackageSellingPromotionAction : OrderAmountOffPromotionAction {

    ///@Override
      protected override Decimal getDiscountValue(EvaluateScopePromotion evaluateScopePromotion, List<Product> products, DBPROMOTION_CACHE_BEANMODEL promotion)
      {
        try {
            return getPackageSellingDiscount(evaluateScopePromotion, products, promotion);
        } catch (Exception e) {
           // e.printStackTrace();
        }
        return CommonConstant.ZERODECIMAL;
    }

    //用于获取满足任选促销 N元N件促销金额,当促销金额>0时往ctx中存放促销商品行信息
    public Decimal getPackageSellingDiscount(EvaluateScopePromotion evaluateScopePromotion, List<Product> products, DBPROMOTION_CACHE_BEANMODEL promotion) {
        //对现在满足条件的item按价格从高到低排序
        try {
//        List<Integer> rowNums = (List<Integer>) context.get(PromotionCondition.VAR_ORDER_SCOPE);
//            List<OrderItemDto> items = ((PromotionOrderEntity) context.get(PromotionCondition.VAR_ORDER)).getOrderItemsWithBom();
//            PromotionCacheDto promotion = (PromotionCacheDto) context.get(PromotionCondition.VAR_PROMOTION);
            Decimal discount;//按商品金额从高到低排序 pair(件数,元)
//            List<ZzPair<OrderItemDto, Decimal>> itemForCalculate = items.stream()
//                    .filter(item -> CollectionUtils.isNotEmpty(rowNums) && rowNums.contains(item.getRowNum()))
//                    .sorted((o1, o2) -> MoneyUtils.divide(o2.getpPaySubAmt(), o2.getQty())
//                            .compareTo(MoneyUtils.divide(o1.getpPaySubAmt(), o1.getQty())))
//                    .map(item -> new ZzPair<>(item, item.getQty())).collect(Collectors.toList());

           // Collections.sort(products, new ImplComparator());
            products.Sort();
            List<KeyValuePair<Product, Decimal>> itemForCalculate = new List<KeyValuePair<Product,decimal>>();
            for (int i = 0; i < products.Count; i++) {
                itemForCalculate.Add(new KeyValuePair<Product, Decimal>(products[i], products[i].num));
            }


            Decimal totalCount = evaluateScopePromotion.getPromotionItemTotalCount();//(Decimal) context.get(PromotionCondition.VAR_ORDER_ITEM_COUNT);

            List<KeyValuePair<Decimal, Decimal>> zzPairs = PromotionContextConvertUtils.convertActionContext(promotion.PROMOACTIONCONTEXT);
            if (zzPairs != null && zzPairs.Count > 0) {
                Decimal limitAmt = CommonConstant.ZERODECIMAL;
                Decimal limitCount = CommonConstant.ZERODECIMAL;
                foreach (KeyValuePair<Decimal, Decimal> zzPair in zzPairs) {
                    if (MoneyUtils.isFirstBiggerThanOrEqualToSecond(totalCount, zzPair.Key)) {
                        limitAmt = zzPair.Value;
                        limitCount = zzPair.Key;
                        break;
                    }
                }
                if (!MoneyUtils.isBiggerThanZero(limitAmt) || !MoneyUtils.isBiggerThanZero(limitCount)) {
                    return CommonConstant.ZERODECIMAL;
                }


//            List<ZzPair<Decimal, Decimal>> zzPairs = PromotionContextConvertUtils.convertActionContext(promotion.getPromoActionContext());
//            Decimal limitAmt = null;
//            Decimal limitCount = null;
//            for (ZzPair<Decimal, Decimal> zzPair : zzPairs) {
//                if (MoneyUtils.isFirstBiggerThanOrEqualToSecond(totalCount, zzPair.getValue0())) {
//                    limitAmt = zzPair.getValue1();
//                    limitCount = zzPair.getValue0();
//                    break;
//                }
//            }
//            if (!MoneyUtils.isBiggerThanZero(limitAmt) || !MoneyUtils.isBiggerThanZero(limitCount)) {
//                return CommonConstant.ZERODECIMAL;
//            }
                //计算优惠金额
                Decimal nowCount = CommonConstant.ZERODECIMAL;
                Decimal nowAmt = CommonConstant.ZERODECIMAL;
                //任选时,实际应用促销的商品行和件数
                Dictionary<int,KeyValuePair<Product, Decimal>> promoItemMap =new Dictionary<int,KeyValuePair<Product,decimal>>();
               // Map<Integer, Pair<Product, Decimal>> promoItemMap = new HashMap<>();
                foreach (KeyValuePair<Product, Decimal> pair in itemForCalculate) {
                    //这个商品项的数量
                    Decimal count = pair.Value;
                    //如果累加商品数量不够,则继续添加
                    if (MoneyUtils.isFirstBiggerThanSecond(limitCount, MoneyUtils.add(nowCount, count))) {
                        nowCount = MoneyUtils.add(nowCount, count);
                        nowAmt = MoneyUtils.add(nowAmt, pair.Key.PaySubAmt);
                         nowAmt = MoneyUtils.add(nowAmt, pair.Key.PaySubAmt);

                        promoItemMap.Add(pair.Key.RowNum,new KeyValuePair<Product, Decimal>(pair.Key, CommonConstant.ONEDECIMAL)); 
                    } else {
                         //如果已经超出(或刚好),则取当前这个商品项的数量为两者差值 nowCount=LimitCount
                        Product item = pair.Key;
                        nowAmt = MoneyUtils.add(nowAmt, MoneyUtils.multiply(
                                //单件
                                MoneyUtils.divide(item.PaySubAmt, new Decimal(item.num)),
                                //差件数
                                MoneyUtils.substract(limitCount, nowCount)
                        ));
                        promoItemMap.Add(pair.Key.RowNum, new KeyValuePair<Product, Decimal>(pair.Key, MoneyUtils.divide(MoneyUtils.substract(limitCount, nowCount), new Decimal(item.num))));
                       
//                        promoItemMap.put(pair.getValue0().getRowNum(),
//                                new ZzPair<>(pair.getValue0(),
//                                        MoneyUtils.divide(MoneyUtils.substract(limitCount, nowCount), item.getQty())));
                        break;//如果已经满足或超出,则结束循环
                    }
                }
                //优惠金额
                discount = MoneyUtils.substract(nowAmt, limitAmt);
                if (MoneyUtils.isBiggerThanZero(discount)) {
                    evaluateScopePromotion.setVar_package_selling_item_info(promoItemMap);
                    return discount;
                }
            }
        } catch (Exception e) {
           // e.printStackTrace();
        }
        return CommonConstant.ZERODECIMAL;
    }

   // @Override
    protected override List<Product> processDistribute(EvaluateScopePromotion evaluateScopePromotion, List<Product> products, Decimal discount)
    {
        Dictionary<int, KeyValuePair<Product, Decimal>> packageSellingInfo = evaluateScopePromotion.getVar_package_selling_item_info();
        if (packageSellingInfo == null || packageSellingInfo.Count == 0) {
            return null;
        }
        Decimal scopeSpAmt = CommonConstant.ZERODECIMAL;

        List<Product> distributeAmountList = new List<Product>();
        foreach (KeyValuePair<int, KeyValuePair<Product, Decimal>> entry in packageSellingInfo) {
            try {
                scopeSpAmt = MoneyUtils.add(scopeSpAmt, MoneyUtils.multiply(entry.Value.Key.PaySubAmt, entry.Value.Value));
                entry.Value.Key.PromoSubAmt = (CommonConstant.ZERODECIMAL);
//                ZZPair zzPair = new ZZPair(entry.getValue().first, entry.getValue().first.getpPaySubAmt(), entry.getValue().first.getpPaySubAmt(), CommonConstant.ZERODECIMAL);
                distributeAmountList.Add(entry.Value.Key);
            } catch (Exception e) {
               // e.printStackTrace();
            }


//            if (rows.contains(i)) {
//            orderItems.get(i).setpPromoSubAmt(CommonConstant.ZERODECIMAL);
//            ZZPair zzPair = new ZZPair(orderItems.get(i), orderItems.get(i).getpPaySubAmt(), orderItems.get(i).getpPaySubAmt(), CommonConstant.ZERODECIMAL);
//            distributeAmountList.add(zzPair);
//            }
        }
        if (!MoneyUtils.isBiggerThanZero(scopeSpAmt)) {
            return null;
        }
         Decimal finalScopeSpAmt = scopeSpAmt;

         AmountDistributor.AmountDistributionRule amo = new AmountDistributor.AmountDistributionRule();

         amo.amountToBeDistribute = discount;
         amo.amountRateDenominator = finalScopeSpAmt;


         AmountDistributor.distribute(distributeAmountList, amo, getShopDefaultScale(), packageSellingInfo);
        //AmountDistributor.distribute(
        //        distributeAmountList,
        //        new AmountDistributor.AmountDistributionRule<Product>() {
        //           // @Override
        //            //用于分摊的值，折扣金额
         //            public override Decimal amountToBeDistribute() {
        //                return discount;
        //            }

        //           // @Override//计算的分母
         //            public override Decimal amountRateDenominator() {
        //                return finalScopeSpAmt;
        //            }

        //          //  @Override//计算分子
         //            public override Decimal amountRateNumerator(Product t) {
        //                try {
        //                    return MoneyUtils.multiply(t.getpPaySubAmt(), packageSellingInfo.get(t.getRowNum()).second);
        //                } catch (Exception e) {
        //                    return new Decimal("1");
        //                }
        //            }

        //           // @Override//单行最大分配的优惠上限，最大为 这一行的应付金额
         //            public override Decimal maxDistributeValue(Product t) {
        //                try {
        //                    return MoneyUtils.multiply(t.getpPaySubAmt(), packageSellingInfo.get(t.getRowNum()).second);
        //                } catch (Exception e) {
        //                    return 1;
        //                }
        //            }

        //           // @Override//当前商品行的优惠金额
         //            public override Decimal currentValue(Product t) {
        //                return t.getpPromoSubAmt();
        //            }

        //           // @Override//赋值新优惠金额的逻辑，默认是赋值，有些情况也可以考虑累加。这边默认就是赋值
         //            public override void setNewValue(Product t, Decimal bd) {
        //                t.setpPromoSubAmt(bd);
        //            }

        //           // @Override//用于标识保留几位精度，默认2位，香港1位
         //            public override int getDefaultScale() {
        //                return getShopDefaultScale();
        //            }
        //        });
        return distributeAmountList;


//        List<ZzPair<OrderItemDto, Decimal>> distributeAmountList = Lists.newArrayList();
//        for (Map.Entry<Integer, ZzPair<OrderItemDto, Decimal>> entry : packageSellingInfo.entrySet()) {
//            scopeSpAmt = MoneyUtils.add(scopeSpAmt,
//                    MoneyUtils.multiply(entry.getValue().getValue0().getpPaySubAmt(), entry.getValue().getValue1()));
//            distributeAmountList.add(new ZzPair<>(entry.getValue().getValue0(), CommonConstant.ZERODECIMAL));
//        }
//        if (!MoneyUtils.isBiggerThanZero(scopeSpAmt)) {
//            scopeSpAmt = header.getpPayAmt();
//        }
//        if (!MoneyUtils.isBiggerThanZero(scopeSpAmt)) {
//            return null;
//        }
//
//        final Decimal finalScopeSpAmt = scopeSpAmt;
//
//        AmountDistributor.distribute(
//                distributeAmountList,
//                new AmountDistributor.AmountDistributionRule<ZzPair<OrderItemDto, Decimal>>() {
//                    @Override
//                    public Decimal amountToBeDistribute() {
//                        return discount;
//                    }
//
//                    @Override
//                    public Decimal amountRateDenominator() {
//                        return finalScopeSpAmt;
//                    }
//
//                    @Override
//                    public Decimal amountRateNumerator(ZzPair<OrderItemDto, Decimal> t) {
//                        return MoneyUtils.multiply(t.getValue0().getpPaySubAmt(),
//                                packageSellingInfo.get(t.getValue0().getRowNum()).getValue1());
//                    }
//
//                    @Override
//                    public Decimal maxDistributeValue(ZzPair<OrderItemDto, Decimal> t) {
//                        return MoneyUtils.multiply(t.getValue0().getpPaySubAmt(),
//                                packageSellingInfo.get(t.getValue0().getRowNum()).getValue1());
//                    }
//
//                    @Override
//                    public Decimal currentValue(ZzPair<OrderItemDto, Decimal> t) {
//                        return t.getValue1();
//                    }
//
//                    @Override
//                    public void setNewValue(ZzPair<OrderItemDto, Decimal> t, Decimal bd) {
//                        t.setValue1(bd);
//                    }
//
//                    @Override
//                    public Integer getDefaultScale() {
//                        return getShopDefaultScale(context);
//                    }
//                });
//        return distributeAmountList;
    }

    public class ImplComparator{

        //@Override
        public  int compare(Product o1, Product o2)
        {
            try {
                return MoneyUtils.divide(o2.PaySubAmt, o2.num).CompareTo(MoneyUtils.divide(o1.PaySubAmt, o1.num));
            } catch (Exception e) {
                return 0;
            }

        }
    }

}


}
