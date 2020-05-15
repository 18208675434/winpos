using Maticsoft.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinSaasPOS.Common;

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
       
    // 基础类，在单品基础上要加上订单类型判断，订单金额汇总，可用商品行标识，件数汇总等
    public bool evaluateScope(DBPROMOTION_CACHE_BEANMODEL dbPromotionCacheBean, List<Product> products, EvaluateScopePromotion evaluateScopePromotion) {
        Decimal promotionItemTotalAmt = new Decimal(0);
        Decimal promotionItemTotalCount = new Decimal(0);
        Decimal promotionItemTotalPayAmt = new Decimal(0);
//        ZzLog.e(dbPromotionCacheBean.getCode() + "--" + dbPromotionCacheBean.getEligibilitycondition());

        // 是否仅会员专享
        bool isOnlyMemberEvaluate = false;
        if (!EnumPromotionType.MEMBER_PRICE_ACTION.Equals(dbPromotionCacheBean.PROMOACTION) && !EnumPromotionType.MEMBER_PRICE_DISCOUNT_ACTION.Equals(dbPromotionCacheBean.PROMOACTION) && !EnumPromotionType.MEMBER_GRADE_PRICE_DISCOUNT_ACTION.Equals(dbPromotionCacheBean.PROMOACTION)) {
            isOnlyMemberEvaluate = true;
        }
        if (isOnlyMemberEvaluate && dbPromotionCacheBean.ONLYMEMBER==1 && !PromotionCache.getInstance().isLoginMember) {
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
                (MoneyUtils.isFirstBiggerThanSecond(orderItem.price.originprice, orderItem.price.total) || MoneyUtils.isBiggerThanZero(orderItem.PromoSubAmt))) {
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
}

}
