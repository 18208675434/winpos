using Maticsoft.Model;
using WinSaasPOS.Common;
using WinSaasPOS.Model.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinSaasPOS.Model.promotionaction
{
   public class OrderStepAmountOffPromotionAction : OrderAmountOffPromotionAction {
    //@Override
       public override Decimal getDiscountValue(EvaluateScopePromotion evaluateScopePromotion, List<Product> products, DBPROMOTION_CACHE_BEANMODEL promotion)
       {
        try {
            Decimal discount = CommonConstant.ZERODECIMAL;

            //解析ctx,得到每个阶梯满减的threshold和discount 如:100:10;200:25;300:40
            String ctx = promotion.PROMOACTIONCONTEXT;
            if (!string.IsNullOrEmpty(ctx)) {
                List<KeyValuePair<Decimal, Decimal>> pairList = PromotionContextConvertUtils.convertActionContext(ctx);

                if (pairList != null && pairList.Count > 0) {
                    Decimal scopeMaxDiscount = calculateScopeDiscountAmtAtRuntime(products);
                    Decimal scopeSpAmt = evaluateScopePromotion.getPromotionItemTotalAmt();
                    Decimal totalCount = evaluateScopePromotion.getPromotionItemTotalCount();

                    //从高到低判断
                    bool itemCount = promotion.PROMOCONDITIONTYPE== EnumPromotionType.ITEM_COUNT_THRESHOLD;
                    Decimal total = itemCount ? totalCount : scopeSpAmt;//取件数or取金额,threshold含义不变


                    foreach (KeyValuePair<Decimal, Decimal> pair in pairList) {
                        if (MoneyUtils.isFirstBiggerThanOrEqualToSecond(total, pair.Key)) {
                            discount = pair.Value;
                            break;
                        }
                    }
                    if (MoneyUtils.isFirstBiggerThanOrEqualToSecond(discount, scopeMaxDiscount)) {
                        return PromotionInfoUtils.getFixedScaleAmt(scopeMaxDiscount, getShopDefaultScale());
                    }
                    return PromotionInfoUtils.getFixedScaleAmt(discount, getShopDefaultScale());
                }
            }
        } catch (Exception exp) {
            LogManager.WriteLog("ERROR", "阶梯满减计算异常:" + exp.Message);

            //exp.printStackTrace();
        }
        return Decimal.Zero;
    }
}


}
