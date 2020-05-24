using Maticsoft.BLL;
using Maticsoft.Model;
using Newtonsoft.Json;
using WinSaasPOS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinSaasPOS.Model.Promotion
{
public class ImplOfflineOrderPromotion {
    private String tenantId;
    private String shopId;


    public ImplOfflineOrderPromotion(String tenantId, String shopId) {
        this.tenantId = tenantId;
        this.shopId = shopId;
    }

    //查找适合的促销对象
    public void doAction(List<Product> products,Cart cartBean) {

        try{
        if (products != null && products.Count > 0) {
            List<Product> allProducts = new List<Product>();
            allProducts.AddRange(products);

            if (cartBean != null && cartBean.selectedcoupons != null && cartBean.selectedcoupons.Count > 0)
            {
                foreach (String key in cartBean.selectedcoupons.Keys)
                {
                    try
                    {
                        Availablecoupon couponsBean = cartBean.selectedcoupons[key];// .getSelectedcoupons().get(key);
                        if (couponsBean != null && couponsBean.catalog.Equals("ExchangeCoupon"))
                        {
                            List<DBPROMOTION_CACHE_BEANMODEL> list = PromotionCache.getInstance().getList(5, couponsBean.promotioncode);//SQliteUtils.getInstance().QueryPromotionByCode(code, tenantId, shopId);
                            if (list != null && list.Count > 0)
                            {
                                DBPROMOTION_CACHE_BEANMODEL dbPromotionCacheBean = list[0];
                                if (dbPromotionCacheBean != null)
                                {
                                    EvaluateScopePromotion evaluateScopePromotion = new EvaluateScopePromotion();
                                    if (PromotionCache.getInstance().getPromotionConditionUtils() != null && PromotionCache.getInstance().getPromotionConditionUtils().evaluateScope(dbPromotionCacheBean, allProducts, evaluateScopePromotion))
                                    {
                                        //剔除第0个商品
                                        if (evaluateScopePromotion.getList() != null && evaluateScopePromotion.getList().Count > 0)
                                        {
                                            allProducts.Remove(evaluateScopePromotion.getList()[0]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        //e.printStackTrace();
                    }
                }
            }

            List<DBPROMOTION_CACHE_BEANMODEL> promotiolist = PromotionCache.getInstance().getList(6, EnumPromotionType.PromotionType_ORDER);//SQliteUtils.getInstance().queryOrderPromotion(tenantId, shopId);
            if (promotiolist != null && promotiolist.Count > 0) {
                 LogManager.WriteLog("promotion","promotiolist  ---->" + promotiolist.Count);
                PromotionInfoUtils.sortPromotion(promotiolist);
                calcPromotionPrice(promotiolist, allProducts);
            }
        }
    }catch(Exception ex){
         LogManager.WriteLog("promotion","查找适合的促销对象异常"+ex.Message);
    }
    }

    //遍历适合的促销对象
    private void calcPromotionPrice(List<DBPROMOTION_CACHE_BEANMODEL> list, List<Product> allProducts){
        try
        {
            for (int n = 0; n < list.Count && allProducts.Count > 0; n++)
            {
                DBPROMOTION_CACHE_BEANMODEL dbPromotionCacheBean = list[n];
                //            if (dbPromotionCacheBean != null && filter(dbPromotionCacheBean)) {
                if (dbPromotionCacheBean != null)
                {
                    EvaluateScopePromotion evaluateScopePromotion = new EvaluateScopePromotion();
                    if (PromotionCache.getInstance().getPromotionConditionUtils() != null && PromotionCache.getInstance().getPromotionConditionUtils().evaluateScope(dbPromotionCacheBean, allProducts, evaluateScopePromotion))
                    {

                         LogManager.WriteLog("promotion","getCode  ---->" + dbPromotionCacheBean.CODE);
                        // evaluateScopePromotion.getList() 传入满足的list 商品
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
                        //如果满足了，进行促销计算
                        if (isEligible)
                        {
                            if (PromotionContextConvertUtils.isSingleLinePromotion(dbPromotionCacheBean))
                            {
                                //特殊处理单行优惠
                                SingleLinePromotioncalculate(dbPromotionCacheBean, evaluateScopePromotion.getList());
                            }
                            else
                            {
                                promactioncalculate(dbPromotionCacheBean, evaluateScopePromotion.getList(), evaluateScopePromotion);
                            }
                        }
                        //剔除商品
                        if (evaluateScopePromotion.getList() != null && evaluateScopePromotion.getList().Count > 0)
                        {
                            //剔除满足的list商品
                            List<Product> lstpro = evaluateScopePromotion.getList(); 
                            foreach (Product pro in lstpro)
                            {
                                allProducts.Remove(pro);
                            }
                           // allProducts.RemoveAll(evaluateScopePromotion.getList());
                        }

                        LogManager.WriteLog("promotion","allProducts  after remove---->" + allProducts.Count);
                    }
                }
            }
        }
        catch (Exception ex)
        {
             LogManager.WriteLog("promotion","遍历所有的促销对象异常"+ex.Message);
        }
    }

//    //商品件数判断
//    public boolean ItemCountThresholdevaluate(DBPROMOTION_CACHE_BEANMODEL dbPromotionCacheBean, List<Product> products, EvaluateScopePromotion cartBean) {
//        try {
//            BigDecimal threshold = new BigDecimal(dbPromotionCacheBean.getPromoconditioncontext() + "");
//
//            BigDecimal totalQty = new BigDecimal(cartBean.getPromotionItemTotalCount() + "");
//            boolean meetItemCount = MoneyUtils.isFirstBiggerThanOrEqualToSecond(totalQty, threshold);
//            return meetItemCount;
//        } catch (Exception ex) {
//            ex.printStackTrace();
//            return false;
//        }
//    }
//
//    //商品金额判断
//    public boolean OrderAmountThresholdvaluate(DBPROMOTION_CACHE_BEANMODEL dbPromotionCacheBean, List<Product> products, EvaluateScopePromotion evaluateScopePromotion) {
//        try {
//            BigDecimal threshold = new BigDecimal(dbPromotionCacheBean.getPromoconditioncontext() + "");
//            BigDecimal scopeSpAmt = new BigDecimal(evaluateScopePromotion.getPromotionItemTotalPayAmt() + "");
//            return MoneyUtils.isFirstBiggerThanOrEqualToSecond(scopeSpAmt,
//                    threshold);
//        } catch (Exception ex) {
//            ex.printStackTrace();
//            return false;
//        }
//    }
//
//    //组合促销判断条件
//    private boolean CombinedThresholdPromotionCondition(DBPROMOTION_CACHE_BEANMODEL dbPromotionCacheBean, List<Product> products, EvaluateScopePromotion evaluateScopePromotion) {
//        try {
//            // 组合限购套数
//            BigDecimal thresholdAmt = new BigDecimal(dbPromotionCacheBean.getPromoconditioncontext());
//            if (MoneyUtils.isBiggerThanZero(thresholdAmt)) {
//                // 判断限购组合套数
////                Integer count = 0;//限购信息下发来源combinedDiscountCountCacheManager.get(promotionCacheDto.getCode());
////                if (count == null || count < 1) {
////                    return false;
////                }
//            }
//            // 优惠力度
//            BigDecimal discountAmt = new BigDecimal(dbPromotionCacheBean.getPromoactioncontext());
//            String promoSubType = dbPromotionCacheBean.getPromosubtype();
//            // 折扣 or 降价
//            boolean discountFlag = EnumPromotionType.DISCOUNT.equals(promoSubType);
//            // 组合商品信息
//            String eligibilityCondition = dbPromotionCacheBean.getEligibilitycondition();
//            PromotionRealmDetail realmDetail = null;
//            if (!TextUtils.isEmpty(eligibilityCondition)) {
//                realmDetail = GsonUtils.gsonToBean(eligibilityCondition, PromotionRealmDetail.class);
//            }
//            if (realmDetail == null) {
//                return false;
//            }
//            Map<String, PromotionRealmDetail.PromotionSku> promotionSkuMap = new HashMap<String, PromotionRealmDetail.PromotionSku>();
//            Iterator<PromotionRealmDetail.PromotionSku> iterator = realmDetail.getSkuAmtInfo().iterator();
//            while (iterator.hasNext()) {
//                promotionSkuMap.put(iterator.next().getSkuCode(), iterator.next());
//            }
//
//            // 组合商品的总原支付金额
//            BigDecimal originalTotalAmt = CommonConstant.ZERODECIMAL;
//
//            for (Product orderItemDto : products) {
//                // 商品份数
//                BigDecimal qty = new BigDecimal(Double.toString(orderItemDto.getNum()));
//                // 商品支付金额
//                BigDecimal paySubAmt = orderItemDto.getpPaySubAmt();
//                // 商品单价
//                BigDecimal unitPrice = MoneyUtils.divide(paySubAmt, qty);
//
//                String skuCode = orderItemDto.getSkucode();
////                SkuDto skuInfo = fSkuServiceClient.getSkuInfo(promotionCacheDto.getTenantScope(), skuCode);
////                if (skuInfo != null && SkuPrefixType.isMultiSpecSkuCode(skuCode)) {
////                    skuCode = skuCode.substring(skuCode.indexOf("-") + 1, skuCode.lastIndexOf("-"));
////                }
//                PromotionRealmDetail.PromotionSku promotionSku = promotionSkuMap.remove(orderItemDto.getSkucode());
//                if (promotionSku != null) {
//                    if (MoneyUtils.isFirstBiggerThanSecond(promotionSku.getAmt(), qty)) {
//                        // 此商品购买数量没有达到组合折扣门槛
//                        promotionSkuMap.put(skuCode, promotionSku);
//                    } else {
//                        originalTotalAmt = MoneyUtils.add(originalTotalAmt, MoneyUtils.multiply(promotionSku.getAmt(), unitPrice));
//                    }
//                }
//            }
//
//            if (promotionSkuMap.isEmpty() && !discountFlag) {
//                // 满足 且 减额类型
//                if (MoneyUtils.isFirstBiggerThanSecond(discountAmt, originalTotalAmt)) {
//                    return false;
//                }
//            }
//            return promotionSkuMap.isEmpty();
//        } catch (Exception ex) {
//            ex.printStackTrace();
//            return false;
//        }
//    }

    //根据不同的促销方式进行计算
    private void promactioncalculate(DBPROMOTION_CACHE_BEANMODEL dbPromotionCacheBean, List<Product> products, EvaluateScopePromotion evaluateScopePromotion) {
        PromoActionFactory promoActionFactory = new PromoActionFactory(dbPromotionCacheBean);
        promoActionFactory.perform(dbPromotionCacheBean, products, evaluateScopePromotion);
    }

    //根据不同的促销方式进行计算  //特殊处理单行优惠
    private void SingleLinePromotioncalculate(DBPROMOTION_CACHE_BEANMODEL dbPromotionCacheBean, List<Product> products) {
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
                        promactioncalculate(dbPromotionCacheBean, newproducts, evaluateScopePromotion);
                    }
                }
            }
        }
    }

}

}
