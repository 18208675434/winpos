using Maticsoft.Model;
using WinSaasPOS.Model.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinSaasPOS.Model.promotionaction
{
    public abstract class AbstractPromoAction
    {

        //试算优惠多少钱的
        public abstract Decimal getDiscountValue(EvaluateScopePromotion evaluateScopePromotion, List<Product> products, DBPROMOTION_CACHE_BEANMODEL promotion);

        //真正去计算和分摊
        public abstract void perform(EvaluateScopePromotion evaluateScopePromotion, List<Product> products, DBPROMOTION_CACHE_BEANMODEL promotion);

        //这是分摊的逻辑
        protected abstract List<Product> processDistribute(EvaluateScopePromotion cartBean, List<Product> products, Decimal discount);

        protected abstract Decimal calculateScopeDiscountAmtAtRuntime(List<Product> products);
    }

}
