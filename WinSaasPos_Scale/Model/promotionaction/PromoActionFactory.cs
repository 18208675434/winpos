using Maticsoft.Model;
using WinSaasPOS_Scale.Model.promotionaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinSaasPOS_Scale.Model.Promotion
{
    public class PromoActionFactory
    {
        private AbstractPromoAction abstractPromoAction;

        public PromoActionFactory(DBPROMOTION_CACHE_BEANMODEL DBPROMOTION_CACHE_BEANMODEL)
        {
            if (EnumPromotionType.FIXED_AMOUNT_OFF_ACTION.Equals(DBPROMOTION_CACHE_BEANMODEL.PROMOACTION))
            {
                //普通满减  （fix.amount.off  已测试）
                abstractPromoAction = new OrderAmountOffPromotionAction();
            }
            else if (EnumPromotionType.DYNAMIC_AMOUNT_OFF_ACTION.Equals(DBPROMOTION_CACHE_BEANMODEL.PROMOACTION))
            {
                //无⻔槛 订单满额  订单满件 （dynamic.amount.off 每满多少减多少  已测试）
                abstractPromoAction = new OrderDynamicAmountOffPromotionAction();
            }
            else if (EnumPromotionType.STEP_AMOUNT_OFF_ACTION.Equals(DBPROMOTION_CACHE_BEANMODEL.PROMOACTION))
            {
                // 订单满额  订单满件  阶梯满减  （step.amount.off 已测试）
                abstractPromoAction = new OrderStepAmountOffPromotionAction();
                //订单满额 订单满件
            }
            else if (EnumPromotionType.FIXED_DISCOUNT_OFF_ACTION.Equals(DBPROMOTION_CACHE_BEANMODEL.PROMOACTION) || EnumPromotionType.DISCOUNT_ACTION.Equals(DBPROMOTION_CACHE_BEANMODEL.PROMOACTION))
            {
                //指定金额  指定件数  折扣  普通折扣     （整单满折已测试）
                abstractPromoAction = new OrderDiscountPromotionAction();

            }
            else if (EnumPromotionType.STEP_DISCOUNT_OFF_ACTION.Equals(DBPROMOTION_CACHE_BEANMODEL.PROMOACTION))
            {


                //无⻔槛  订单满额  订单满件 阶梯折扣 (step.discount.off 已测试)

                abstractPromoAction = new StepDiscountOffPromotionAction();


            }
            else if (EnumPromotionType.DESIGNATED_DISCOUNT_OFF_ACTION.Equals(DBPROMOTION_CACHE_BEANMODEL.PROMOACTION))
            {
                //指定金额  //        指定件数  //    第N件M折  //        指定折扣    （designated.discount.off ）
                abstractPromoAction = new DesignatedDiscountOffPromotionAction();
            }
            else if (EnumPromotionType.CYCLE_DESIGNATED_DISCOUNT_OFF_ACTION.Equals(DBPROMOTION_CACHE_BEANMODEL.PROMOACTION))
            {
                //循环折扣   
                abstractPromoAction = new CycleDesignatedDiscountPromotionAction();

            }
            else if (EnumPromotionType.PACKAGE_SELLING_ACTION.Equals(DBPROMOTION_CACHE_BEANMODEL.PROMOACTION))
            {
                //N元任选M件   （package.selling）
                abstractPromoAction = new PackageSellingPromotionAction();
            }
        }

        public void perform(DBPROMOTION_CACHE_BEANMODEL DBPROMOTION_CACHE_BEANMODEL, List<Product> products, EvaluateScopePromotion evaluateScopePromotion)
        {
            if (abstractPromoAction != null)
            {
                abstractPromoAction.perform(evaluateScopePromotion, products, DBPROMOTION_CACHE_BEANMODEL);
            }
        }

    }

}
