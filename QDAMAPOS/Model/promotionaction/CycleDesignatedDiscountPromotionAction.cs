using Maticsoft.Model;
using QDAMAPOS.Model.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QDAMAPOS.Model.promotionaction
{
  public class CycleDesignatedDiscountPromotionAction : OrderAmountOffPromotionAction {

   
    protected override Decimal getDiscountValue(EvaluateScopePromotion evaluateScopePromotion, List<Product> products, DBPROMOTION_CACHE_BEANMODEL promotion) {
        try {
            //总商品行数量
            Decimal totalCount = evaluateScopePromotion.getPromotionItemTotalCount();
            //第N件
            Decimal threshold = MoneyUtils.newMoney(promotion.PROMOCONDITIONCONTEXT);
            //获取折扣信息 1-0.85=0.15
            Decimal discountNum =1-  Math.Round( (decimal)Convert.ToDecimal(promotion.PROMOCONDITIONCONTEXT)/10,2);
            //单行的金额
            Decimal singleAmt = 0;
            if (products != null && products.Count > 0) {
                Product Product = products[0];
                if (Product != null) {
                    singleAmt = MoneyUtils.divide(Product.PaySubAmt, Product.num);
                }
            }
            if (!PromotionInfoUtils.isValidDiscountNum(discountNum)) {
                return Decimal.Zero;
            }
            //可参与的第N件N折的数量
            Decimal steps = Math.Ceiling((decimal)totalCount / threshold);
                
                //MoneyUtils.newMoneyFromInt((totalCount.divide(threshold,
                //    0, Decimal.ROUND_DOWN).intValue()));
            //总优惠金额
            return MoneyUtils.multiply(MoneyUtils.multiply(steps, singleAmt), discountNum, 2);
        } catch (Exception exp) {
            //exp.printStackTrace();
            
        }
        return Decimal.Zero;
    }
}


}
