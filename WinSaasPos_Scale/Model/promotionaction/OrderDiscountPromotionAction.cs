using Maticsoft.Model;
using WinSaasPOS_Scale.Model.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinSaasPOS_Scale.Model.promotionaction
{
  public class OrderDiscountPromotionAction : OrderAmountOffPromotionAction {

    //@Override
      protected override Decimal getDiscountValue(EvaluateScopePromotion evaluateScopePromotion, List<Product> products, DBPROMOTION_CACHE_BEANMODEL promotion)
      {
        try {
            //打折券
            bool isDiscountCoupon = false;//CouponCatalog.DISCOUNT_COUPON.getValue().equals(promotion.getPromosubtype());
            //获取折扣信息 1-0.85=0.15 
            Decimal scopeMaxDiscount = CommonConstant.ZERODECIMAL;
            Decimal scopeSpAmt = CommonConstant.ZERODECIMAL;
            Decimal discountNum = CommonConstant.ZERODECIMAL;

            scopeMaxDiscount = calculateScopeDiscountAmtAtRuntime(products);
            //打折券,需要考虑折扣金额上限
            if (isDiscountCoupon) {
                string temp = promotion.PROMOACTIONCONTEXT.Replace("\\|","*");
                String[] split = temp.Split('*');


                discountNum = Math.Round(1 - Convert.ToDecimal(split[0]) / 10, 4, MidpointRounding.AwayFromZero);
                //CommonConstant.ONEDECIMAL.subtract(new Decimal(split[0]).divide(Decimal.TEN, 4,
                //        RoundingMode.HALF_UP));
                if (split.Length == 2) {
                    Decimal limitDiscountAmt = MoneyUtils.newMoney(split[1]);
                    if (scopeMaxDiscount == null || MoneyUtils.isFirstBiggerThanSecond(scopeMaxDiscount,
                            limitDiscountAmt)) {
                        scopeMaxDiscount = limitDiscountAmt;
                    }
                }
            } else {
                discountNum = Math.Round(1 - Convert.ToDecimal(promotion.PROMOACTIONCONTEXT) / 10, 4, MidpointRounding.AwayFromZero);
                    //CommonConstant.ONEDECIMAL.subtract(new Decimal(promotion.getPromoactioncontext()).divide(Decimal.TEN, 4,
                    //    RoundingMode.HALF_UP));
            }
            scopeSpAmt = evaluateScopePromotion.getPromotionItemTotalAmt();//(Decimal) context.get(PromotionCondition.VAR_ORDER_SCOPE_PAY_AMT);
            if (!PromotionInfoUtils.isValidDiscountNum(discountNum)) {
                return Decimal.Zero;
            }
            Decimal discount = MoneyUtils.multiply(scopeSpAmt, discountNum, getShopDefaultScale());
            if (MoneyUtils.isFirstBiggerThanOrEqualToSecond(discount, scopeMaxDiscount)) {
                return PromotionInfoUtils.getFixedScaleAmt(scopeMaxDiscount, getShopDefaultScale());
            }
            return discount;
        } catch (Exception exp) {
            //exp.printStackTrace();
        }
        return Decimal.Zero;
    }
}


}
