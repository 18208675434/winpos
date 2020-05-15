using Maticsoft.Model;
using WinSaasPOS_Scale.Model.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinSaasPOS_Scale.Model.promotionaction
{
 public class DesignatedDiscountOffPromotionAction : OrderAmountOffPromotionAction {


     protected override Decimal getDiscountValue(EvaluateScopePromotion evaluateScopePromotion, List<Product> products, DBPROMOTION_CACHE_BEANMODEL promotion)
     {
        try {
            Decimal scopeMaxDiscount = CommonConstant.ZERODECIMAL;
            //获取折扣信息 1-0.85=0.15
            Decimal discountNum = 1 - Math.Round((decimal)Convert.ToDecimal(promotion.PROMOACTIONCONTEXT) / 10, 2, MidpointRounding.AwayFromZero);
            Decimal scopeSpAmt = CommonConstant.ZERODECIMAL;
            if (products != null && products.Count > 0) {
                Product Product = products[0];
                if (Product.price.total != null && Product.PaySubAmt != null) {
                    scopeMaxDiscount = MoneyUtils.divide(Product.PaySubAmt, new Decimal(Product.num));
                    scopeSpAmt = MoneyUtils.divide(Product.PaySubAmt, new Decimal(Product.num));
                }

                if (!PromotionInfoUtils.isValidDiscountNum(discountNum)) {
                    return Decimal.Zero;
                }
                Decimal discount = MoneyUtils.multiply(scopeSpAmt, discountNum, 2);
                if (MoneyUtils.isFirstBiggerThanOrEqualToSecond(discount, scopeMaxDiscount)) {
                    return PromotionInfoUtils.getFixedScaleAmt(scopeMaxDiscount, 2);
                }
                return discount;
            }

        } catch (Exception exp) {
           // exp.printStackTrace();
        }
        return Decimal.Zero;
    }
}


}
