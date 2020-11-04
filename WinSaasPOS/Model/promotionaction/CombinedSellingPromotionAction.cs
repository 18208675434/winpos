using Maticsoft.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinSaasPOS.Model.Promotion;

namespace WinSaasPOS.Model.promotionaction
{
public class CombinedSellingPromotionAction : OrderAmountOffPromotionAction {


    public System.Collections.Generic.IEnumerator<PromotionSku> GetEnumerator()
    {
        throw new NotImplementedException();
        yield return default(PromotionSku);
    }


    public override Decimal getDiscountValue(EvaluateScopePromotion evaluateScopePromotion, List<Product> products, DBPROMOTION_CACHE_BEANMODEL promotion) {

        // 优惠力度
        Decimal discountAmt = Convert.ToDecimal(promotion.PROMOACTIONCONTEXT); 
        String promoSubType = promotion.PROMOSUBTYPE; 
        // 折扣 or 降价
        bool discountFlag = EnumPromotionType.DISCOUNT.Equals(promoSubType);
        if (!discountFlag) {
            // 减额
            return discountAmt;
        }
        // 组合商品信息
        String eligibilityCondition = promotion.ELIGIBILITYCONDITION;
        PromotionRealmDetail realmDetail = null;
        if (!string.IsNullOrEmpty(eligibilityCondition)) {
            realmDetail =JsonConvert.DeserializeObject<PromotionRealmDetail>(eligibilityCondition);
        }
        if (realmDetail == null) {
            return Decimal.Zero;
        }

        Dictionary<String, PromotionSku> promotionSkuMap = new Dictionary<string,PromotionSku>();

        foreach(PromotionSku prosku in realmDetail.skuAmtInfo){
            promotionSkuMap.Add(prosku.skuCode,prosku);
        }



//        Map<String, PromotionRealmDetail.PromotionSku> promotionSkuMap = realmDetail.getSkuAmtInfo().stream().collect(Collectors.toMap(PromotionRealmDetail.PromotionSku::getSkuCode, e -> e, (o1, o2) -> o2));

        // 组合套餐商品 原本需要支付的总金额
        Decimal combinedPrimaryTotalAmt = CommonConstant.ZERODECIMAL;

        // 订单商品
        foreach (Product orderItemDto in products) {
            String skuCode = orderItemDto.skucode;

            // 商品份数
            Decimal qty =Convert.ToDecimal( orderItemDto.num);
            // 商品支付金额
            Decimal paySubAmt = orderItemDto.PaySubAmt;
            // 商品单价
            Decimal unitPrice ;
            try {
                unitPrice = MoneyUtils.divide(paySubAmt, qty);

                PromotionSku promotionSku = promotionSkuMap[skuCode]; promotionSkuMap.Remove(skuCode);
                if (promotionSku != null) {
                    if (MoneyUtils.isFirstBiggerThanSecond(promotionSku.amt, qty)) {
                        // 此商品购买数量没有达到组合折扣门槛
                    } else {
                        // 套餐原价
                        Decimal combinedPrimaryAmt = MoneyUtils.multiply(unitPrice, promotionSku.amt);
                        combinedPrimaryTotalAmt = MoneyUtils.add(combinedPrimaryTotalAmt, combinedPrimaryAmt);
                    }
                }
            } catch (Exception e) {
//                return Decimal.ZERO;
            }
        }
        try {
            // 折扣
            Decimal combinedPromoAmt = MoneyUtils.multiply(combinedPrimaryTotalAmt, MoneyUtils.divide(discountAmt, 10));
            return MoneyUtils.substract(combinedPrimaryTotalAmt, combinedPromoAmt);
        } catch (Exception e) {
            return Decimal.Zero;
        }
    }


}


}
