using Maticsoft.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinSaasPOS.Common;
using WinSaasPOS.Model.Promotion;

namespace WinSaasPOS.Model.promotionaction
{
   public class OrderPaymentPromotionAction : OrderAmountOffPromotionAction {

    public override Decimal getDiscountValue(EvaluateScopePromotion evaluateScopePromotion, List<Product> products, DBPROMOTION_CACHE_BEANMODEL promotion) {
        try {
            //获取折扣信息 1-0.85=0.15
            Decimal scopeMaxDiscount;
            Decimal scopeSpAmt;
            Decimal discount = CommonConstant.ZERODECIMAL;
            String ctx = promotion.PROMOACTIONCONTEXT;//  .getPromoactioncontext();
            scopeMaxDiscount = calculateScopeDiscountAmtAtRuntime(products);
            scopeSpAmt = evaluateScopePromotion.getPromotionItemTotalPayAmt();
            if (promotion.PROMOSUBTYPE== EnumPromotionType.DISCOUNT) {
              //  Decimal discountNum = CommonConstant.ONEDECIMAL.subtract(new Decimal(ctx).divide(Decimal.TEN, RoundingMode.HALF_UP));
                Decimal discountNum = Math.Round(1 - Convert.ToDecimal(ctx) / 10, 4, MidpointRounding.AwayFromZero);
                if (!PromotionInfoUtils.isValidDiscountNum(discountNum)) {
                    return Decimal.Zero;
                }
                discount = MoneyUtils.multiply(scopeSpAmt, discountNum);
                if (MoneyUtils.isFirstBiggerThanOrEqualToSecond(discount, scopeMaxDiscount)) {
                    return scopeMaxDiscount;
                }
            } else {
                discount = MoneyUtils.newMoney(ctx);
                if (MoneyUtils.isFirstBiggerThanSecond(scopeMaxDiscount, discount) && MoneyUtils.isFirstBiggerThanSecond(
                        discount,
                        Decimal.Zero)) {
                    return discount;
                } else if (MoneyUtils.isFirstBiggerThanOrEqualToSecond(discount, scopeMaxDiscount)) {
                    return scopeMaxDiscount;
                }
            }
            return discount;
        } catch (Exception e) {
            LogManager.WriteLog(e.Message);
        }
        return CommonConstant.ZERODECIMAL;
    }
}

}
