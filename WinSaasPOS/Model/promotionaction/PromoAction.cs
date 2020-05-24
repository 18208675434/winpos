using Maticsoft.Model;
using WinSaasPOS.Model.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinSaasPOS.Model.promotionaction
{
  public class PromoAction : AbstractPromoAction {

    //@Override
    public override Decimal getDiscountValue(EvaluateScopePromotion evaluateScopePromotion, List<Product> products, DBPROMOTION_CACHE_BEANMODEL promotion) {
        return 0;
    }

    //@Override
    public override void perform(EvaluateScopePromotion evaluateScopePromotion, List<Product> products, DBPROMOTION_CACHE_BEANMODEL promotion) {

    }

    //@Override
    protected override List<Product> processDistribute(EvaluateScopePromotion cartBean, List<Product> products, Decimal discount) {
        return null;
    }

    //用于标识保留几位精度，默认2位，香港1位
    protected int getShopDefaultScale() {
        return 2;
    }

    protected override  Decimal calculateScopeDiscountAmtAtRuntime(List<Product> products) {
        Decimal result = 0;
        foreach (Product item in products) {
            result = MoneyUtils.add(result, item.PaySubAmt);
        }
        return result;
    }
}


}
