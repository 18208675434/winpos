using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Model.Promotion
{
 public class AmountDistributor {

    public class AmountDistributionRule {
        public Decimal amountToBeDistribute { get; set; }

        public Decimal amountRateDenominator { get; set; }//分母

        public Decimal amountRateNumerator { get; set; }//分子

        public Decimal currentValue { get; set; }//当前值

       public Decimal maxDistributeValue { get; set; }//最大该行可分配值

      // public void setNewValue { get; set; }

       public int getDefaultScale { get; set; }
    }

    //amo.distribute(
    //        distributeAmountList,
    //        new AmountDistributor.AmountDistributionRule<Product>(){


    //            //@Override
    //            //用于分摊的值，折扣金额
    //            public override Decimal amountToBeDistribute() {
    //                return discount;
    //            }

    //            //@Override//计算的分母
    //            public override Decimal amountRateDenominator() {
    //                return finalScopeSpAmt;
    //            }

    //            //@Override//计算分子
    //            public override Decimal amountRateNumerator(Product t) {
    //                return t.getpPaySubAmt();
    //            }

    //          //  @Override//单行最大分配的优惠上限，最大为 这一行的应付金额
    //            public override Decimal maxDistributeValue(Product t) {
    //                return t.getpPaySubAmt();
    //            }

    //           // @Override//当前商品行的优惠金额
    //            public override Decimal currentValue(Product t) {
    //                return t.getpPromoSubAmt();
    //            }

    //           // @Override//赋值新优惠金额的逻辑，默认是赋值，有些情况也可以考虑累加。这边默认就是赋值
    //            public override void setNewValue(Product t, Decimal bd) {
    //                t.setpPromoSubAmt(bd);
    //            }

    //           // @Override//用于标识保留几位精度，默认2位，香港1位
    //            public override int getDefaultScale() {
    //                return getShopDefaultScale();
    //            }
    //        });
     //For OrderAmountOffPromotionAction
    public static void distribute(List<Product> tripletList, AmountDistributionRule factor, int DefaultScale)
    {
        Decimal distributedAmt = CommonConstant.ZERODECIMAL;

        foreach (Product item in tripletList) {
            try {
                factor.amountRateNumerator = item.PaySubAmt;
                factor.maxDistributeValue = item.PaySubAmt;
                factor.currentValue = item.PromoSubAmt;

                Decimal itemDiscount = MoneyUtils.isFirstBiggerThanSecond(factor.amountRateDenominator,
                        CommonConstant.ZERODECIMAL)
                        ? MoneyUtils.divide(MoneyUtils.multiply(factor.amountToBeDistribute,
                        factor.amountRateNumerator), factor.amountRateDenominator, DefaultScale)
                        : CommonConstant.ZERODECIMAL;

                //不能超过
                itemDiscount = factor.maxDistributeValue == null
                        ? itemDiscount
                        : (MoneyUtils.isFirstBiggerThanSecond(itemDiscount,
                        factor.maxDistributeValue) ? factor.maxDistributeValue : itemDiscount);

                distributedAmt = distributedAmt+itemDiscount;

                item.PromoSubAmt = MoneyUtils.add(factor.currentValue == null ? CommonConstant.ZERODECIMAL : factor.currentValue, itemDiscount);
               

            } catch (Exception e) {
                //e.printStackTrace();
            }
        }


        //计算金额是不是少
        if (!MoneyUtils.isFirstEqualToSecond(distributedAmt, factor.amountToBeDistribute)) {
            Decimal diff = factor.amountToBeDistribute-distributedAmt;
            bool addFlag = MoneyUtils.isBiggerThanZero(diff); //是否在行级别增加或者减少金额
            diff = Math.Abs(diff); //diff.abs(); // 变正数来操作
            foreach (Product item in tripletList) {
                Decimal maxAdjustValue = addFlag
                        ? (factor.maxDistributeValue == null ? diff : MoneyUtils.substract(factor.maxDistributeValue, item.PromoSubAmt)) //如果无限增加，则最大增加值就是diff本身
                        : item.PromoSubAmt;
                if (MoneyUtils.isBiggerThanZero(maxAdjustValue)) {
                    Decimal toAdjust = MoneyUtils.isFirstBiggerThanSecond(diff,
                            maxAdjustValue) ? maxAdjustValue : diff;
                    diff = MoneyUtils.substract(diff, toAdjust);

                    item.PromoSubAmt = addFlag ? MoneyUtils.add(item.PromoSubAmt, toAdjust) : MoneyUtils.substract(item.PromoSubAmt, toAdjust);                  
                }
                if (MoneyUtils.isZero(diff)) {
                    break;
                }
            }

            if (!MoneyUtils.isZero(diff)) {
                //diff没有分摊均衡
               // ZzLog.e("cannot distribute value in the order items " + GsonUtils.beanToJson(tripletList));
               // ZzLog.e("distribute " + distributedAmt.toString() + " and diff " + diff.toString());
//                throw new BaseUnknownException(ErrorConstant.ERRORCODE_CANNOT_DISTRIBUTE_ORDER_ITEMS,
//                        ErrorConstant.ERRORMESSAGE_CANNOT_DISTRIBUTE_ORDER_ITEMS);
            }
        }
    }

    //For PackageSellingPromotionAction
    public static void distribute(List<Product> tripletList, AmountDistributionRule factor, int DefaultScale, Dictionary<int, KeyValuePair<Product, Decimal>> packageSellingInfo)
    {
        Decimal distributedAmt = CommonConstant.ZERODECIMAL;

        foreach (Product item in tripletList)
        {
            try
            {
                factor.amountRateNumerator = MoneyUtils.multiply(item.PaySubAmt, packageSellingInfo[item.RowNum].Value);
                factor.maxDistributeValue = MoneyUtils.multiply(item.PaySubAmt, packageSellingInfo[item.RowNum].Value);
                factor.currentValue = item.PromoSubAmt;

                Decimal itemDiscount = MoneyUtils.isFirstBiggerThanSecond(factor.amountRateDenominator,
                        CommonConstant.ZERODECIMAL)
                        ? MoneyUtils.divide(MoneyUtils.multiply(factor.amountToBeDistribute,
                        factor.amountRateNumerator), factor.amountRateDenominator, DefaultScale)
                        : CommonConstant.ZERODECIMAL;

                //不能超过
                itemDiscount = factor.maxDistributeValue == null
                        ? itemDiscount
                        : (MoneyUtils.isFirstBiggerThanSecond(itemDiscount,
                        factor.maxDistributeValue) ? factor.maxDistributeValue : itemDiscount);

                distributedAmt = distributedAmt + itemDiscount;

                item.PromoSubAmt = MoneyUtils.add(factor.currentValue == null ? CommonConstant.ZERODECIMAL : factor.currentValue, itemDiscount);


            }
            catch (Exception e)
            {
                //e.printStackTrace();
            }
        }


        //计算金额是不是少
        if (!MoneyUtils.isFirstEqualToSecond(distributedAmt, factor.amountToBeDistribute))
        {
            Decimal diff = factor.amountToBeDistribute - distributedAmt;
            bool addFlag = MoneyUtils.isBiggerThanZero(diff); //是否在行级别增加或者减少金额
            diff = Math.Abs(diff); //diff.abs(); // 变正数来操作
            foreach (Product item in tripletList)
            {
                Decimal maxAdjustValue = addFlag
                        ? (factor.maxDistributeValue == null ? diff : MoneyUtils.substract(factor.maxDistributeValue, item.PromoSubAmt)) //如果无限增加，则最大增加值就是diff本身
                        : item.PromoSubAmt;
                if (MoneyUtils.isBiggerThanZero(maxAdjustValue))
                {
                    Decimal toAdjust = MoneyUtils.isFirstBiggerThanSecond(diff,
                            maxAdjustValue) ? maxAdjustValue : diff;
                    diff = MoneyUtils.substract(diff, toAdjust);

                    item.PromoSubAmt = addFlag ? MoneyUtils.add(item.PromoSubAmt, toAdjust) : MoneyUtils.substract(item.PromoSubAmt, toAdjust);
                }
                if (MoneyUtils.isZero(diff))
                {
                    break;
                }
            }

            if (!MoneyUtils.isZero(diff))
            {
                //diff没有分摊均衡
                // ZzLog.e("cannot distribute value in the order items " + GsonUtils.beanToJson(tripletList));
                // ZzLog.e("distribute " + distributedAmt.toString() + " and diff " + diff.toString());
                //                throw new BaseUnknownException(ErrorConstant.ERRORCODE_CANNOT_DISTRIBUTE_ORDER_ITEMS,
                //                        ErrorConstant.ERRORMESSAGE_CANNOT_DISTRIBUTE_ORDER_ITEMS);
            }
        }
    }
}


}
