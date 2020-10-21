using Maticsoft.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinSaasPOS.Common;

namespace WinSaasPOS.Model.Promotion
{
public class OrderThresholdUtils {

    //商品件数判断
    public static bool ItemCountThresholdevaluate(DBPROMOTION_CACHE_BEANMODEL dbPromotionCacheBean, List<Product> products, EvaluateScopePromotion cartBean) {
        try {
            Decimal threshold = Convert.ToDecimal(dbPromotionCacheBean.PROMOCONDITIONCONTEXT);

            Decimal totalQty = cartBean.getPromotionItemTotalCount();
            bool meetItemCount = MoneyUtils.isFirstBiggerThanOrEqualToSecond(totalQty, threshold);
            return meetItemCount;
        } catch (Exception ex) {
                        LogManager.WriteLog( "商品件数判断异常"+ex.Message);

            return false;
        }
    }

    //商品金额判断
    public static bool OrderAmountThresholdvaluate(DBPROMOTION_CACHE_BEANMODEL dbPromotionCacheBean, List<Product> products, EvaluateScopePromotion evaluateScopePromotion) {
        try {
            Decimal threshold = Convert.ToDecimal(dbPromotionCacheBean.PROMOCONDITIONCONTEXT);
            Decimal scopeSpAmt = evaluateScopePromotion.getPromotionItemTotalPayAmt();
            return MoneyUtils.isFirstBiggerThanOrEqualToSecond(scopeSpAmt,
                    threshold);
        } catch (Exception ex) {
                                    LogManager.WriteLog("商品金额判断异常"+ex.Message);

            return false;
        }
    }

    //组合促销判断条件
    public static bool CombinedThresholdPromotionCondition(DBPROMOTION_CACHE_BEANMODEL dbPromotionCacheBean, List<Product> products, EvaluateScopePromotion evaluateScopePromotion) {
        try {
            // 组合限购套数
            Decimal thresholdAmt = Convert.ToDecimal(dbPromotionCacheBean.PROMOCONDITIONCONTEXT );
            if (MoneyUtils.isBiggerThanZero(thresholdAmt)) {
                // 判断限购组合套数
//                Integer count = 0;//限购信息下发来源combinedDiscountCountCacheManager.get(promotionCacheDto.getCode());
//                if (count == null || count < 1) {
//                    return false;
//                }
            }
            // 优惠力度
            Decimal discountAmt = Convert.ToDecimal(dbPromotionCacheBean.PROMOACTIONCONTEXT);
            String promoSubType = dbPromotionCacheBean.PROMOSUBTYPE;
            // 折扣 or 降价
            bool discountFlag = EnumPromotionType.DISCOUNT.Equals(promoSubType);
            // 组合商品信息
            String eligibilityCondition = dbPromotionCacheBean.ELIGIBILITYCONDITION;
            PromotionRealmDetail realmDetail = null;
            if (!string.IsNullOrEmpty(eligibilityCondition)) {
                realmDetail = JsonConvert.DeserializeObject<PromotionRealmDetail>(eligibilityCondition);
            }
            if (realmDetail == null) {
                return false;
            }

           Dictionary<String, PromotionSku> promotionSkuMap = new Dictionary<string,PromotionSku>();

        foreach(PromotionSku prosku in realmDetail.skuAmtInfo){
            promotionSkuMap.Add(prosku.skuCode,prosku);
        }

            // 组合商品的总原支付金额
            Decimal originalTotalAmt = CommonConstant.ZERODECIMAL;

            foreach (Product orderItemDto in products) {
                // 商品份数
                Decimal qty = Convert.ToDecimal(orderItemDto.num);
                // 商品支付金额
                Decimal paySubAmt = orderItemDto.PaySubAmt;
                // 商品单价
                Decimal unitPrice =(decimal) paySubAmt/qty;

                String skuCode = orderItemDto.skucode;
//                SkuDto skuInfo = fSkuServiceClient.getSkuInfo(promotionCacheDto.getTenantScope(), skuCode);
//                if (skuInfo != null && SkuPrefixType.isMultiSpecSkuCode(skuCode)) {
//                    skuCode = skuCode.substring(skuCode.indexOf("-") + 1, skuCode.lastIndexOf("-"));
//                }
                PromotionSku promotionSku = promotionSkuMap[skuCode]; promotionSkuMap.Remove(skuCode);
                if (promotionSku != null)
                {
                    if (MoneyUtils.isFirstBiggerThanSecond(promotionSku.amt, qty)) {
                        // 此商品购买数量没有达到组合折扣门槛
                        promotionSkuMap.Add(skuCode, promotionSku);
                    } else {
                        originalTotalAmt = MoneyUtils.add(originalTotalAmt, MoneyUtils.multiply(promotionSku.amt, unitPrice));
                    }
                }
            }

            if ((promotionSkuMap==null || promotionSkuMap.Count==0) && !discountFlag) {
                // 满足 且 减额类型
                if (MoneyUtils.isFirstBiggerThanSecond(discountAmt, originalTotalAmt)) {
                    return false;
                }
            }

            if (promotionSkuMap == null || promotionSkuMap.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        } catch (Exception ex) {
           
            return false;
        }
    }
}
}
