using Maticsoft.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinSaasPOS.Model.Promotion;

namespace WinSaasPOS.Model.promotionaction
{
public class FixedTradeOffOrderAmountOffPromotionAction : OrderAmountOffPromotionAction {

    public override Decimal getDiscountValue(EvaluateScopePromotion evaluateScopePromotion, List<Product> products, DBPROMOTION_CACHE_BEANMODEL promotion) {
        try {
            Decimal scopeMaxDiscount = thiscalculateScopeDiscountAmtAtRuntime(products);
            Decimal tempAmount = MoneyUtils.newMoney(promotion.PROMOACTIONCONTEXT);
            if (MoneyUtils.isFirstBiggerThanSecond(scopeMaxDiscount, tempAmount)) {
                return MoneyUtils.substract(scopeMaxDiscount, tempAmount);
            }
        } catch (Exception exp) {
           // exp.printStackTrace();
        }
        return Decimal.Zero;
    }


    protected  Decimal thiscalculateScopeDiscountAmtAtRuntime(List<Product> products)
    {  
        Decimal result = 0;
        foreach (Product item in products)
        {
            result = MoneyUtils.add(result, item.PaySubAmt);
            if (item.goodstagid == 0)
            {
                try
                {
                    decimal scaleprice = MoneyUtils.divide(result,item.num);
                    result = scaleprice;
                }
                catch { }
            }
            else
            {

            }

            break;
        }
        return result;
    }
}
}
