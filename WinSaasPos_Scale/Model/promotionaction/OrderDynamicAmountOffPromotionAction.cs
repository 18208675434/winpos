using Maticsoft.Model;
using WinSaasPOS_Scale.Model.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinSaasPOS_Scale.Model.promotionaction
{
   public class OrderDynamicAmountOffPromotionAction : OrderAmountOffPromotionAction {

   // @Override
       protected override Decimal getDiscountValue(EvaluateScopePromotion evaluateScopePromotion, List<Product> products, DBPROMOTION_CACHE_BEANMODEL promotion)
       {
        try {
            Decimal stepThreshold = MoneyUtils.newMoney(promotion.PROMOCONDITIONCONTEXT);
            Decimal stepDisAmount = MoneyUtils.newMoney(promotion.PROMOACTIONCONTEXT);

            Decimal discount = CommonConstant.ZERODECIMAL;


            Decimal scopeMaxDiscount = calculateScopeDiscountAmtAtRuntime(products);

            Decimal scopeSpAmt = evaluateScopePromotion.getPromotionItemTotalAmt();//(Decimal) context.get(PromotionCondition.VAR_ORDER_SCOPE_PAY_AMT);
            Decimal totalCount = evaluateScopePromotion.getPromotionItemTotalCount();// (Decimal) context.get(PromotionCondition.VAR_ORDER_ITEM_COUNT);

            if (MoneyUtils.isFirstBiggerThanSecond(scopeSpAmt, Decimal.Zero)) {
                bool itemCount = promotion.PROMOCONDITIONTYPE == EnumPromotionType.ITEM_COUNT_THRESHOLD;// Objects.equals(promotion.getPromoconditiontype(), EnumPromotionType.ITEM_COUNT_THRESHOLD);
                //考虑MoneyUtils.divide精度问题,使用原生divide向下取整
                //取件数or取金额,threshold含义不变
                Decimal total = itemCount ? totalCount : scopeSpAmt;
                Decimal steps = MoneyUtils.newMoneyFromInt((int)(total/stepThreshold));
                discount = MoneyUtils.multiply(steps, stepDisAmount);
            }

            if (MoneyUtils.isFirstBiggerThanOrEqualToSecond(discount, scopeMaxDiscount)) {
                return PromotionInfoUtils.getFixedScaleAmt(scopeMaxDiscount, getShopDefaultScale());
            }
            return PromotionInfoUtils.getFixedScaleAmt(discount, getShopDefaultScale());
        } catch (Exception exp) {
            //exp.printStackTrace();
        }
        return Decimal.Zero;
    }
}


}
