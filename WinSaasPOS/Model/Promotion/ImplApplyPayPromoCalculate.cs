using Maticsoft.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinSaasPOS.Model.Promotion
{
    /// <summary>
    /// 余额支付立减
    /// </summary>
    public class ImplApplyPayPromoCalculate
    {

        public void applyPayPromo(Cart cartBean)
        {
            if (cartBean == null || cartBean.products == null || cartBean.products.Count == 0)
            {
                return;
            }
            if (cartBean.totalpayment <= 0)
            {
                return;
            }
            List<DBPROMOTION_CACHE_BEANMODEL> list = PromotionCache.getInstance().getList(7, EnumPromotionType.PAYMENT_REDUCE);
            if (CollectionUtils.isEmpty(list))
            {
                return;
            }
            for (int i = 0; i < list.Count; i++)
            {
                DBPROMOTION_CACHE_BEANMODEL dbPromotionCacheBean = list[i];
                EvaluateScopePromotion evaluateScopePromotion = new EvaluateScopePromotion();
                if (dbPromotionCacheBean != null && PromotionCache.getInstance().getPromotionConditionUtils() != null && PromotionCache.getInstance().getPromotionConditionUtils().evaluateScope(dbPromotionCacheBean, cartBean.products, evaluateScopePromotion))
                {
                    PromoActionFactory promoActionFactory = PromotionCache.getInstance().getPromotionConditionUtils().getPromoActionFactoryByThreshold(dbPromotionCacheBean, evaluateScopePromotion);
                    if (promoActionFactory != null)
                    {
                        Decimal discountValue = promoActionFactory.getDiscountValue(evaluateScopePromotion, cartBean.products, dbPromotionCacheBean);
                        if (discountValue != null)
                        {
                            //TODO  balancepaypromoamt 用法
                            cartBean.balancepaypromoamt = discountValue;//.setBalancepaypromoamt(discountValue.doubleValue());
                        }
                        break;
                    }
                }
            }
        }
    }
}
