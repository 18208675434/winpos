using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model.HalfOffLine;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Model.Promotion
{
    public class CalculateAvailablePointsCommandImpl
    {

        public void execute(Cart cartBean)
        {
            if (cartBean == null || CollectionUtils.isEmpty(cartBean.products))
            {
                return;
            }
            if (!PromotionCache.getInstance().isLoginMember)
            {
                return; 
            }
            //if (!PromotionCache.getInstance().isUsePoint)
            //{
            //    return;
            //}
            Decimal availableCredit = CommonConstant.ZERODECIMAL;

            TenantCreditConfig tenantCreditConfig = PromotionCache.getInstance().getTenantCreditConfig();
            if (tenantCreditConfig == null || !tenantCreditConfig.creditenable)
            {
                return;
            }
            Creditaccountrepvo creditaccountrepvo = PromotionCache.creditaccountrepvo;
            if (creditaccountrepvo != null)
            {
                availableCredit = creditaccountrepvo.availablecredit;
            }
            Pointinfo pointinfo = new Pointinfo();
            cartBean.pointinfo=pointinfo;// .setPointinfo(pointinfo);
            if (availableCredit != null)
            {
                pointinfo.totalpoints = Convert.ToInt32(availableCredit);
            }
            if (MoneyUtils.isBiggerThanZero(availableCredit))
            {
                PointValue pointValue = calculate(availableCredit, cartBean.payamt, 2, tenantCreditConfig);
                
                if (pointValue != null)
                {
                    if (MoneyUtils.isBiggerThanZero(pointValue.PointVal))
                    {
                        pointinfo.availablepoints = pointValue.Point;
                        pointinfo.availablepointsamount = Convert.ToDecimal(pointValue.PointVal);


                        
                        if (!PromotionCache.getInstance().isUsePoint)
                        {
                            cartBean.pointinfo = pointinfo;
                            return;
                        }

                        if (cartBean.orderpricedetails == null)
                        {
                            List<OrderPriceDetail> orderpricedetails = new List<OrderPriceDetail>();
                            cartBean.orderpricedetails=orderpricedetails;// //购物车商品明细
                        }
                        Decimal pointv = pointValue.PointVal;
                        if (MoneyUtils.isFirstBiggerThanOrEqualToSecond(pointv, cartBean.payamt))
                        {
                            pointv = cartBean.payamt;
                        }
                        cartBean.pospointpromoamt = Convert.ToDecimal(pointv);
                        cartBean.pospointofuser = pointValue.Point;
                        OrderPriceDetail temppricedetail = new OrderPriceDetail();
                        temppricedetail.title="积分抵现";
                        temppricedetail.amount = "-¥ " + pointv.ToString("f2");
                        cartBean.orderpricedetails.Add(temppricedetail);

                        cartBean.pointinfo = pointinfo;
                        cartBean.payamt = cartBean.payamt - pointv;
                    }
                }
            }
        }

        //积分可抵扣金额和对应积分数
        private PointValue calculate(Decimal point, Decimal orderAmount, int defaultScale, TenantCreditConfig tenantCreditConfig) {
        try {

            PointValue pointvalue = new PointValue();
            //可用积分金额
            Decimal pointValue = calculate(point, defaultScale, tenantCreditConfig);
            //订单金额
            Decimal ratio = tenantCreditConfig.pointmaxratio;
            Decimal maxPointValue = MoneyUtils.multiply(orderAmount, ratio, defaultScale);

            Decimal calculatedPointValue = MoneyUtils.isFirstBiggerThanSecond(maxPointValue, pointValue)
                    ? pointValue : maxPointValue;

            long calculatePoint = Convert.ToInt64(Math.Ceiling(MoneyUtils.multiply(calculatedPointValue, tenantCreditConfig.pointperrmb)));
          

            pointvalue.PointVal=calculatedPointValue;
            pointvalue.Point=calculatePoint;
           // return new Pair<>(calculatedPointValue, calculatePoint);
            return pointvalue;
        } catch (Exception e) {
            LogManager.WriteLog(e.Message);

        }
        return null;
    }


        private Decimal calculate(Decimal point, int defaultScale, TenantCreditConfig tenantCreditConfig)
        {
            if (MoneyUtils.isBiggerThanZero(point))
            {
                Decimal maxAmount = tenantCreditConfig.pointmaxamount;
                Decimal minAmount = tenantCreditConfig.pointminamount;
                if (!MoneyUtils.isFirstBiggerThanOrEqualToSecond(point, minAmount))
                {//低于最低抵扣金额时，直接认为当前可用为0
                    return CommonConstant.ZERODECIMAL;
                }
                Decimal availablePointValue = MoneyUtils.isFirstBiggerThanSecond(point, maxAmount) ? maxAmount : point;
                decimal temp = availablePointValue / tenantCreditConfig.pointperrmb;
                return Math.Floor(temp);
                //return availablePointValue.divide(tenantCreditConfig.getPointperrmb(), defaultScale, Decimal.ROUND_DOWN);
            }
            return CommonConstant.ZERODECIMAL;
        }
    }

    public class PointValue
    {
        public decimal PointVal { set; get; }

        public long Point { set; get; }
    }
}
