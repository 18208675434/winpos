using Maticsoft.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinSaasPOS.Common;
using WinSaasPOS.Model.HalfOffLine;

namespace WinSaasPOS.Model.Promotion
{
   public class PromotionConditionUtils {

    private String tenantId;
    private String shopId;

    //    private final Set<String> NIGHT_TIMED_PROMO_CODES = Sets.newHashSet("201804280012", "201804280013", "201804280014", "201804280015",
//            "201804280016", "201804280017", "201804280018", "201804280019", "201804280020", "201804280021");
    //private List<String> list = new List<string>();
//    private String promotionNight;

    public PromotionConditionUtils(String tenantId, String shopId) {
        this.tenantId = tenantId;
        this.shopId = shopId;

//        promotionNight = SQliteUtils.getInstance().getJson(EnumCondition.CONDITION_OFFLINE_PROMOTIONSWITHFILTERINFO);
    }

    public void setPromotionNight(String promotionNight) {
//        this.promotionNight = promotionNight;
    }

           private bool notValidMemberFlag(DBPROMOTION_CACHE_BEANMODEL promotion) {
//        MemberTenantResponseDto memberTenantResponseDto = memberTenantServiceClient.get(String.valueOf(orderEntity.getOrderHeader().getCustomerId()), orderEntity.getOrderHeader().getTenantId());
//        if (memberTenantResponseDto != null) {
//            context.setMemberFlag(1);
//            if (memberTenantResponseDto.getFirstRechargeAt() != null) {
//                context.setMemberFlag(2);
//            }
//        }


        // 是否仅会员专享
        List<String> memberPriceActions = new List<string>();
        memberPriceActions.Add(EnumPromotionType.MEMBER_PRICE_DISCOUNT_ACTION);
        memberPriceActions.Add(EnumPromotionType.MEMBER_PRICE_ACTION);
        memberPriceActions.Add(EnumPromotionType.PAID_UP_MEMBER_PRICE_DISCOUNT);
//        Lists.newArrayList(PromotionactionType.MEMBER_PRICE_DISCOUNT.getValue(), PromotionActionType.MEMBER_PRICE.getValue(), PromotionActionType.PAID_UP_MEMBER_PRICE_DISCOUNT.getValue());
        List<int> evaluateMemberFlag = new List<int>();
        evaluateMemberFlag.Add(1);
        evaluateMemberFlag.Add(2);
        bool memberFlagEvaluate = (promotion.PROMOACTION == null || !memberPriceActions.Contains(promotion.PROMOACTION))
                && evaluateMemberFlag.Contains((int)promotion.MEMBERFLAG);
        if (memberFlagEvaluate) {
            if (promotion.MEMBERFLAG == 1) {
                return !PromotionCache.getInstance().isLoginMember;
            } else if (promotion.MEMBERFLAG== 2) {
                MemberTenantItem memberTenantResponseDto = PromotionCache.getInstance().membertenantitem;
                int memberFlag = 0;
                if (memberTenantResponseDto != null) {
                    memberFlag = 1;
                    if (memberTenantResponseDto.firstrechargeat != null) {
                        memberFlag = 2;
                    }
                }
//                Integer memberFlag = (Integer) context.get(PromotionCondition.VAR_MEMBER_FLAG);
//                return memberFlag == null || memberFlag < promotion.getMemberflag();
                return memberFlag == 0 || memberFlag < promotion.MEMBERFLAG;
            }
        }
        return false;
    }

    // 基础类，在单品基础上要加上订单类型判断，订单金额汇总，可用商品行标识，件数汇总等
    public bool evaluateScope(DBPROMOTION_CACHE_BEANMODEL dbPromotionCacheBean, List<Product> products, EvaluateScopePromotion evaluateScopePromotion) {
        Decimal promotionItemTotalAmt = new Decimal(0);
        Decimal promotionItemTotalCount = new Decimal(0);
        Decimal promotionItemTotalPayAmt = new Decimal(0);
//        ZzLog.e(dbPromotionCacheBean.getCode() + "--" + dbPromotionCacheBean.getEligibilitycondition());

        // 是否仅会员专享
        //bool isOnlyMemberEvaluate = false;
        //if (!EnumPromotionType.MEMBER_PRICE_ACTION.Equals(dbPromotionCacheBean.PROMOACTION) && !EnumPromotionType.MEMBER_PRICE_DISCOUNT_ACTION.Equals(dbPromotionCacheBean.PROMOACTION) && !EnumPromotionType.MEMBER_GRADE_PRICE_DISCOUNT_ACTION.Equals(dbPromotionCacheBean.PROMOACTION)) {
        //    isOnlyMemberEvaluate = true;
        //}
        //if (isOnlyMemberEvaluate && dbPromotionCacheBean.ONLYMEMBER==1 && !PromotionCache.getInstance().isLoginMember) {
        //    return false;
        //}

        if (notValidMemberFlag(dbPromotionCacheBean))
        {
            return false;
        }

        if (!PromotionInfoUtils.isInTimeRange(dbPromotionCacheBean)) {
            return false;
        }
        if (dbPromotionCacheBean.ORDERSUBTYPE != null && (8 & dbPromotionCacheBean.ORDERSUBTYPE) == 0) {//8
            return false;
        }
        if (PromotionCache.getInstance().validatePromotionMemberTags(dbPromotionCacheBean)) {
            return false;
        }
//        Set<Integer> couponUsageExcludeRows = Sets.newHashSet();
//        List<Integer> listScope = new ArrayList<>();
        List<Product> list = new List<Model.Product>();
        PromotionRealmDetail realmDetail =JsonConvert.DeserializeObject<PromotionRealmDetail>(dbPromotionCacheBean.ELIGIBILITYCONDITION);
        bool eligible = false;
        if (realmDetail == null) {
            if (products != null && products.Count > 0) {
                for (int i = 0; i < products.Count; i++) {
                    Product tempProduct = products[i];
                    if (tempProduct != null) {
                      
                        if (isEligibleRow(dbPromotionCacheBean, products[i])) {
                            list.Add(tempProduct);
//                            listScope.add(i);//我给商品标记的第几行
                            promotionItemTotalAmt = MoneyUtils.add(promotionItemTotalAmt, tempProduct.price.total);//售价合计
                            promotionItemTotalPayAmt = MoneyUtils.add(promotionItemTotalPayAmt, tempProduct.PaySubAmt);//售价-已享受促销之后的应付金额
                            promotionItemTotalCount = MoneyUtils.add(promotionItemTotalCount, new Decimal(tempProduct.num));//购买的份数
                            eligible = true;
                        }
                    }
                }
//                cartBean.setListScope(listScope); //适用的商品行号
                evaluateScopePromotion.setList(list);
                evaluateScopePromotion.setPromotionItemTotalAmt(promotionItemTotalAmt);//这个商品的售价累计
                evaluateScopePromotion.setPromotionItemTotalCount(promotionItemTotalCount); // 商品件数的合计
                evaluateScopePromotion.setPromotionItemTotalPayAmt(promotionItemTotalPayAmt); // 商品件数的合计
                return eligible;
            } else {
                return true;
            }
        }
        if (EnumPromotionType.REALM_ALL.Equals(realmDetail.realmType)) {
            if (products != null && products.Count > 0) {
                for (int i = 0; i < products.Count; i++) {
                    Product tempProduct = products[i];
                    if (tempProduct != null) {
                        if (realmDetail.skuCodesToExclude != null && realmDetail.skuCodesToExclude.Contains(tempProduct.skucode))
                        {
                            continue;
                        }
                        if (isEligibleRow(dbPromotionCacheBean, products[i])) {
//                            listScope.add(i);//我给商品标记的第几行
                            list.Add(tempProduct);
                            promotionItemTotalAmt = MoneyUtils.add(promotionItemTotalAmt, tempProduct.price.total);//售价合计
                            promotionItemTotalPayAmt = MoneyUtils.add(promotionItemTotalPayAmt, tempProduct.PaySubAmt);//售价-已享受促销之后的应付金额
                            promotionItemTotalCount = MoneyUtils.add(promotionItemTotalCount, new Decimal(tempProduct.num));//购买的份数
                            eligible = true;
                        }
                    }
                }
//                cartBean.setListScope(listScope); //适用的商品行号
                evaluateScopePromotion.setList(list);
                evaluateScopePromotion.setPromotionItemTotalAmt(promotionItemTotalAmt);//这个商品的售价累计
                evaluateScopePromotion.setPromotionItemTotalCount(promotionItemTotalCount); // 商品件数的合计
                evaluateScopePromotion.setPromotionItemTotalPayAmt(promotionItemTotalPayAmt); // 商品件数的合计
                return eligible;
            }
        }

        if (EnumPromotionType.REALM_GOODS.Equals(realmDetail.realmType)) {
            if (products != null && products.Count > 0) {
                for (int i = 0; i < products.Count; i++) {
                    Product Product = products[i];
                    if (Product != null) {
                        if (realmDetail.skuCodesToInclude != null && realmDetail.skuCodesToInclude.Contains(Product.skucode) && isEligibleRow(dbPromotionCacheBean, products[i])) {
//                            listScope.add(i);//我给商品标记的第几行
                            list.Add(Product);
                            promotionItemTotalAmt = MoneyUtils.add(promotionItemTotalAmt, Product.price.total);//售价合计
                            promotionItemTotalPayAmt = MoneyUtils.add(promotionItemTotalPayAmt, Product.PaySubAmt);//售价-已享受促销之后的应付金额
                            promotionItemTotalCount = MoneyUtils.add(promotionItemTotalCount, new Decimal(Product.num));//购买的份数
                            eligible = true;
                        }
                    }
                }
//                evaluateScopePromotion.setListScope(listScope); //适用的商品行号
                evaluateScopePromotion.setList(list);
                evaluateScopePromotion.setPromotionItemTotalAmt(promotionItemTotalAmt);//这个商品的售价累计
                evaluateScopePromotion.setPromotionItemTotalCount(promotionItemTotalCount); // 商品件数的合计
                evaluateScopePromotion.setPromotionItemTotalPayAmt(promotionItemTotalPayAmt); // 商品件数的合计
                return eligible;
            }
        }

        if (EnumPromotionType.REALM_CATALOG.Equals(realmDetail.realmType)) {
            if (products != null && products.Count > 0) {
                for (int i = 0; i < products.Count; i++) {
                    Product product = products[i];
                    if (product != null) {
                        if (realmDetail.skuCodesToExclude != null && realmDetail.skuCodesToExclude.Contains(product.skucode)) {
                            continue;
                        }


                        if (PromotionCache.getCategoryIds(product) == null || PromotionCache.getCategoryIds(product).Count == 0) {
                            continue;
                        }

                        if (realmDetail.skuCodesToInclude != null && PromotionCache.getCategoryIds(product) != null && !GlobalUtil.IsArrayIntersection(realmDetail.catalogsToInclude,
                                PromotionCache.getCategoryIds(product)) &&
                                isEligibleRow(dbPromotionCacheBean, products[i])) {
//                            listScope.add(i);//我给商品标记的第几行
                            list.Add(product);
                            promotionItemTotalAmt = MoneyUtils.add(promotionItemTotalAmt, product.price.total);//售价合计
                            promotionItemTotalPayAmt = MoneyUtils.add(promotionItemTotalPayAmt, product.PaySubAmt);//售价-已享受促销之后的应付金额
                            promotionItemTotalCount = MoneyUtils.add(promotionItemTotalCount, new Decimal(product.num));//购买的份数
                            eligible = true;
                        }
                    }
                }
//                cartBean.setListScope(listScope); //适用的商品行号
                evaluateScopePromotion.setList(list);
                evaluateScopePromotion.setPromotionItemTotalAmt(promotionItemTotalAmt);//这个商品的售价累计
                evaluateScopePromotion.setPromotionItemTotalCount(promotionItemTotalCount); // 商品件数的合计
                evaluateScopePromotion.setPromotionItemTotalPayAmt(promotionItemTotalPayAmt); // 商品件数的合计
                return eligible;
            }
        }

        if (EnumPromotionType.REALM_MIXED.Equals(realmDetail.realmType)) {
            if (products != null && products.Count > 0) {
                for (int i = 0; i < products.Count; i++) {
                    Product tempProduct = products[i];
                    if (tempProduct != null) {
                        if (realmDetail.skuCodesToExclude != null && realmDetail.skuCodesToExclude.Contains(tempProduct.skucode)) {
                            continue;
                        }


                        if (PromotionCache.getCategoryIds(tempProduct) == null || PromotionCache.getCategoryIds(tempProduct).Count == 0)
                        {
                            continue;
                        }
                        if (realmDetail.skuCodesToInclude != null && PromotionCache.getCategoryIds(tempProduct) != null && PromotionCache.getCategoryIds(tempProduct).Count > 0 && !GlobalUtil.IsArrayIntersection(realmDetail.catalogsToInclude,
                                PromotionCache.getCategoryIds(tempProduct)))
                        {
                            continue;
                        }
                        if (((realmDetail.catalogsToInclude != null
                                && PromotionCache.getCategoryIds(tempProduct) != null && PromotionCache.getCategoryIds(tempProduct).Count > 0
                                && !GlobalUtil.IsArrayIntersection(realmDetail.catalogsToInclude, PromotionCache.getCategoryIds(tempProduct))) ||
                                (CollectionUtils.isNotEmpty(realmDetail.skuCodesToInclude)
                                        && realmDetail.skuCodesToInclude.Contains(tempProduct.skucode)))
                                && isEligibleRow(dbPromotionCacheBean, products[i])) {
//                            listScope.add(i);//我给商品标记的第几行
                            list.Add(tempProduct);
                            promotionItemTotalAmt = MoneyUtils.add(promotionItemTotalAmt, tempProduct.price.total);//售价合计
                            promotionItemTotalPayAmt = MoneyUtils.add(promotionItemTotalPayAmt, tempProduct.PaySubAmt);//售价-已享受促销之后的应付金额
                            promotionItemTotalCount = MoneyUtils.add(promotionItemTotalCount, new Decimal(tempProduct.num));//购买的份数
                            eligible = true;
                        }
                    }
                }
//                cartBean.setListScope(listScope); //适用的商品行号
                evaluateScopePromotion.setList(list);
                evaluateScopePromotion.setPromotionItemTotalAmt(promotionItemTotalAmt);//这个商品的售价累计
                evaluateScopePromotion.setPromotionItemTotalCount(promotionItemTotalCount); // 商品件数的合计
                evaluateScopePromotion.setPromotionItemTotalPayAmt(promotionItemTotalPayAmt); // 商品件数的合计
                return eligible;
            }
        }
        return false;
    }

    //公共的商品是否适用当前促销的判断
    private bool isEligibleRow(DBPROMOTION_CACHE_BEANMODEL promotion, Product orderItem) {
        if (EnumPromotionType.ORDERCOUPON.Equals(promotion.PROMOTYPE)) {
            //没有限购 && 优惠券不叠加使用
            return orderItem.purchaselimit <= 0 && orderItem.canmixcoupon && canApplySpecificPromo(promotion, orderItem, shopId);
        } else {
            //没有限购
            return orderItem.purchaselimit <= 0 && canApplySpecificPromo(promotion, orderItem, shopId);
        }
    }

    //判断商品是否适用单品促销
    private bool canApplySpecificPromo(DBPROMOTION_CACHE_BEANMODEL promotion, Product orderItem, String shopId) {
        //校验是否仅适用于原价购买的商品
        if (promotion.ONLYUSEINORIGINAL==1 &&
                (MoneyUtils.isFirstBiggerThanSecond(orderItem.price.originprice, orderItem.price.saleprice) || MoneyUtils.isBiggerThanZero(orderItem.PromoSubAmt))) {
            return false;
        }
        //部分分类不参与夜间促销
        if (promotion.FROMOUTER != null && promotion.FROMOUTER==1 && !string.IsNullOrEmpty(promotion.OUTERCODE)
                && !string.IsNullOrEmpty(orderItem.categoryid)) {
//            //部分分类不参与夜间促销
//            if (!TextUtils.isEmpty(promotionNight)) {
//                PromotionsInvalidBean promotionsInvalidBean = GsonUtils.gsonToBean(promotionNight, PromotionsInvalidBean.class);
//                if (promotionsInvalidBean != null) {
//                    if ((promotionsInvalidBean.getInvalidcategories() != null && promotionsInvalidBean.getInvalidcategories().Contains(orderItem.getFirstcategoryid())) || (promotionsInvalidBean.getInvalidskucodes() != null && promotionsInvalidBean.getInvalidskucodes().Contains(orderItem.skucode))) {
//                        return false;
//                    }
//                }
//            }
           
        }
//        if (orderItem.getGoodsBarcode() == null || shopId == null) {
//            return true;
//        }
//        //购物袋不参与任何促销
//        if (Objects.Equals(orderItem.getGoodsFlag(), ItemGoodsFlag.BAG)) {
//            return false;
//        }
        //散称,点餐商品不参与单行促销  //TODO 散称判断
//        if (PromotionContextConvertUtils.isSingleLinePromotion(promotion)
//                && (!Objects.Equals(orderItem.getBomType(),
//                ItemBomType.BOM.getValue()) && posSkuService.getSkuByFreshBarcodeAndShopId(orderItem.getGoodsBarcode(),
//                shopId) != null)) {
//            return false;
//        }

        //判断是否是O或者OrderCoupon类型的促销  商品是否是香烟 商品是否是购物袋
//        if ((PromotionContextConvertUtils.isOrderLevelOrOrderCouponPromotion(promotion) && orderItem.getCategoryId() != null
//                && orderItem.getCategoryId().substring(0, 6).Equals("123301"))) {
//            return false;
//        }
        return true;
    }


    /*** start 订单级别门槛判断及使用***************/
    //判断是否达到促销门槛
    public PromoActionFactory getPromoActionFactoryByThreshold(DBPROMOTION_CACHE_BEANMODEL dbPromotionCacheBean, EvaluateScopePromotion evaluateScopePromotion)
    {
       // ZzLog.e("getCode  ---->" + dbPromotionCacheBean.getCode());
        bool isEligible = false;
        //判断是件数条件判断，走件数判断逻辑
        if (EnumPromotionType.ITEM_COUNT_THRESHOLD.Equals(dbPromotionCacheBean.PROMOCONDITIONTYPE))
        {
            isEligible = OrderThresholdUtils.ItemCountThresholdevaluate(dbPromotionCacheBean, evaluateScopePromotion.getList(), evaluateScopePromotion);
        }
        else if (EnumPromotionType.ORDER_AMOUNT_THRESHOLD.Equals(dbPromotionCacheBean.PROMOCONDITIONTYPE))
        {
            //判断是金额条件判断，走金额判断逻辑
            isEligible = OrderThresholdUtils.OrderAmountThresholdvaluate(dbPromotionCacheBean, evaluateScopePromotion.getList(), evaluateScopePromotion);
        }
        else if (EnumPromotionType.ITEM_COMBINED_THRESHOLD.Equals(dbPromotionCacheBean.PROMOCONDITIONTYPE))
        {
            //组合促销判断条件
            isEligible = OrderThresholdUtils.CombinedThresholdPromotionCondition(dbPromotionCacheBean, evaluateScopePromotion.getList(), evaluateScopePromotion);
        }
        else
        {
            isEligible = true;
        }

        PromoActionFactory promoActionFactory = null;
        //如果满足了，进行促销计算
        if (isEligible)
        {
            if (PromotionContextConvertUtils.isSingleLinePromotion(dbPromotionCacheBean))
            {
                //特殊处理单行优惠
                promoActionFactory = SingleLinePromotioncalculate(dbPromotionCacheBean, evaluateScopePromotion.getList());
            }
            else
            {
                promoActionFactory = promactioncalculate(dbPromotionCacheBean, evaluateScopePromotion.getList(), evaluateScopePromotion);
            }
        }
        //剔除商品
        //                if (evaluateScopePromotion.getList() != null && evaluateScopePromotion.getList().size() > 0) {
        //                    //剔除满足的list商品
        //                    allProducts.removeAll(evaluateScopePromotion.getList());
        //                }
        return promoActionFactory;
    }

    //根据不同的促销方式进行计算
    private PromoActionFactory promactioncalculate(DBPROMOTION_CACHE_BEANMODEL dbPromotionCacheBean, List<Product> products, EvaluateScopePromotion evaluateScopePromotion)
    {
        PromoActionFactory promoActionFactory = new PromoActionFactory(dbPromotionCacheBean);
        return promoActionFactory;
    }

    //根据不同的促销方式进行计算  //特殊处理单行优惠
    private PromoActionFactory SingleLinePromotioncalculate(DBPROMOTION_CACHE_BEANMODEL dbPromotionCacheBean, List<Product> products) {
        if (products != null && products.Count > 0) {
            for (int i = 0; i < products.Count; i++) {
                List<Product> newproducts = new List<Product>();
                newproducts.Add(products[i]);
                EvaluateScopePromotion evaluateScopePromotion = new EvaluateScopePromotion();

                if (PromotionCache.getInstance().getPromotionConditionUtils() != null && PromotionCache.getInstance().getPromotionConditionUtils().evaluateScope(dbPromotionCacheBean, newproducts, evaluateScopePromotion)) {
                    bool isEligible = false;
                    //判断是件数条件判断，走件数判断逻辑
                    if (EnumPromotionType.ITEM_COUNT_THRESHOLD.Equals(dbPromotionCacheBean.PROMOCONDITIONTYPE)) {
                        isEligible = OrderThresholdUtils.ItemCountThresholdevaluate(dbPromotionCacheBean, newproducts, evaluateScopePromotion);
                    } else if (EnumPromotionType.ORDER_AMOUNT_THRESHOLD.Equals(dbPromotionCacheBean.PROMOCONDITIONTYPE)) {
                        //判断是金额条件判断，走金额判断逻辑
                        isEligible = OrderThresholdUtils.OrderAmountThresholdvaluate(dbPromotionCacheBean, newproducts, evaluateScopePromotion);
                    } else {
                        isEligible = true;
                    }
                    if (isEligible) {
                        return promactioncalculate(dbPromotionCacheBean, newproducts, evaluateScopePromotion);
                    }
                }
            }
        }
        return null;
    }
       /*** end 订单级别门槛判断及使用***************/


}

}
