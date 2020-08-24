using Maticsoft.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model.HalfOffLine;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Model.Promotion
{
   public class ImplOfflineCouponsCalculate {
    private Dictionary<String, Object> map = new Dictionary<string,object>();

    private String EVALUATESCOPEPROMOTION_KEY = "EVALUATESCOPEPROMOTION_KEY";

    public ImplOfflineCouponsCalculate(String tenantId, String shopId) {
    }

    // 缓存 基础类，在单品基础上要加上订单类型判断，订单金额汇总，可用商品行标识，件数汇总等
    private bool evaluateScope(DBPROMOTION_CACHE_BEANMODEL DBPROMOTION_CACHE_BEANMODEL, List<Product> products, EvaluateScopePromotion evaluateScopePromotion) {
        if (DBPROMOTION_CACHE_BEANMODEL == null) {
            return false;
        }

        if (map.ContainsKey(DBPROMOTION_CACHE_BEANMODEL.CODE)) {
            try {
                
                EvaluateScopePromotion evaluateScopePromotion1 = (EvaluateScopePromotion) map[DBPROMOTION_CACHE_BEANMODEL.CODE + EVALUATESCOPEPROMOTION_KEY];
                if (evaluateScopePromotion1 != null) {
                    evaluateScopePromotion.setList(evaluateScopePromotion1.getList());
                    evaluateScopePromotion.setPromotionItemTotalAmt(evaluateScopePromotion1.getPromotionItemTotalAmt());
                    evaluateScopePromotion.setPromotionItemTotalCount(evaluateScopePromotion1.getPromotionItemTotalCount());
                    evaluateScopePromotion.setPromotionItemTotalPayAmt(evaluateScopePromotion1.getPromotionItemTotalPayAmt());
                    evaluateScopePromotion.setVar_package_selling_item_info(evaluateScopePromotion1.getVar_package_selling_item_info());
                    return true;
                }
            } catch (Exception e) {

            }
        }
        if (PromotionCache.getInstance().getPromotionConditionUtils() != null && PromotionCache.getInstance().getPromotionConditionUtils().evaluateScope(DBPROMOTION_CACHE_BEANMODEL, products, evaluateScopePromotion)) {
            map.Add(DBPROMOTION_CACHE_BEANMODEL.CODE, DBPROMOTION_CACHE_BEANMODEL);
            map.Add(DBPROMOTION_CACHE_BEANMODEL.CODE + EVALUATESCOPEPROMOTION_KEY, evaluateScopePromotion);
            return true;
        }
        return false;
    }

    //查询促销
    private DBPROMOTION_CACHE_BEANMODEL getDBPROMOTION_CACHE_BEANMODEL(String code) {
        if (map.ContainsKey(code)) {
            try {
                return (DBPROMOTION_CACHE_BEANMODEL) map[code];
            } catch (Exception e) {
                List<DBPROMOTION_CACHE_BEANMODEL> list = PromotionCache.getInstance().getList(5, code);//SQliteUtils.getInstance().QueryPromotionByCode(code, tenantId, shopId);
                if (list==null || list.Count==0) {
                    return null;
                }
                return list[0];
            }
        } else {
            List<DBPROMOTION_CACHE_BEANMODEL> list = PromotionCache.getInstance().getList(5, code);//SQliteUtils.getInstance().QueryPromotionByCode(code, tenantId, shopId);
            if (list==null || list.Count==0) {
                return null;
            }
            return list[0];
        }
    }

    public void calculate(Cart cartBean) {
        map.Clear();

        if (cartBean != null)
        {
            cartBean.couponpromoamt = 0;
        }
        if (string.IsNullOrEmpty(MainModel.CurrentCouponCode))
        {
            cartBean.selectedcoupons = null;
        }
        else
        {
            cartBean.selectedcoupons = new Dictionary<string, OrderCouponVo>();
            cartBean.selectedcoupons.Add(MainModel.CurrentCouponCode, MainModel.Currentavailabecoupno);
        }

        List<PromotionCoupon> listcoupon = PromotionCache.getInstance().getListcoupon();//查询出优惠券
        evaluateCoupons(cartBean, listcoupon);

        if (cartBean.totalpayment > 0)
        {
            applyOrderCouponPromo(cartBean, listcoupon);
        }
        map.Clear();
    }

    //结算页优惠券列表排序
    public TripletBeanForCoupon evaluateCoupons(Cart cartBean, List<PromotionCoupon> listcoupon) {
        List<OrderCouponVo> availablecoupons = new List<OrderCouponVo>();
//        是否有券信息
        if (listcoupon==null || listcoupon.Count==0) {
            return null;
        }
            foreach(PromotionCoupon coupon in listcoupon){
//            if (!EnumPromotionType.ORDERCOUPON.equals(coupon.getPromotiontype())) {
//                continue;
//            }
            //从下发数据源里面取
            TripletBeanForCoupon tripletBean = null;
            DBPROMOTION_CACHE_BEANMODEL dbPromotionCacheBean = getDBPROMOTION_CACHE_BEANMODEL(coupon.promotioncode);//SQliteUtils.getInstance().QueryPromotionByCode(coupon.getPromotioncode(), tenantId, shopId);
            if (dbPromotionCacheBean != null)
            {
                tripletBean = new TripletBeanForCoupon();
                tripletBean.setPromoTriplet(dbPromotionCacheBean);
            }
            if (tripletBean == null) {
                continue;
            }

            EvaluateScopePromotion evaluateScopePromotion = new EvaluateScopePromotion();
            if (evaluateScope(tripletBean.getPromoTriplet(), cartBean.products, evaluateScopePromotion))
            {
                PromoActionFactory promoActionFactory = getPromoActionFactoryByThreshold(dbPromotionCacheBean, evaluateScopePromotion);
                if (promoActionFactory != null)
                {
                    Decimal discountamt = promoActionFactory.getDiscountValue(evaluateScopePromotion, evaluateScopePromotion.getList(), tripletBean.getPromoTriplet());
                    coupon.discountamt = discountamt;
                    //if (discountamt != null)
                    //{
                    //    cartBean.couponpromoamt = Convert.ToDecimal(discountamt);
                    //}

                    OrderCouponVo couponsBean = new OrderCouponVo();
                    //set 值
                    couponsBean.catalog = coupon.catalog;
                    couponsBean.enabledfrom = coupon.enabledfrom;
                    couponsBean.enabledto = coupon.enabledto;
                    couponsBean.couponcode = coupon.couponcode;
                    couponsBean.promotioncode = coupon.promotioncode;
                    if (cartBean != null && cartBean.selectedcoupons != null && cartBean.selectedcoupons.ContainsKey(coupon.couponcode))
                    {
                        couponsBean.selectstate = 1;
                    }
                    couponsBean.orderminamount = coupon.orderminamount;
                    couponsBean.discountamt = coupon.discountamt;
                    couponsBean.amount = coupon.amount;
                    couponsBean.availableskudesc = coupon.availableskudesc;
                    couponsBean.exchangeconditioncontext = coupon.exchangeconditioncontext;
                    availablecoupons.Add(couponsBean);
                }
            }
        }
        if (cartBean == null || cartBean.selectedcoupons == null || cartBean.selectedcoupons.Count == 0) {//没有选择优惠券
            cartBean.couponpromoamt=0;
        }

        //以抵扣金额降序,如果金额相同,则以失效时间升序
        if (availablecoupons != null && availablecoupons.Count > 0) {

           // availablecoupons.OrderByDescending(x => x.discountamt).ThenBy(x => x.enabledto);

            availablecoupons.Sort((o1, o2) => {
                    
                        int discAmt = o2.discountamt.CompareTo(o1.discountamt);
                        if (discAmt != 0) {
                            return discAmt;
                        }
                        return o1.discountamt.CompareTo(o2.discountamt);
                    
            }); 

            //Collections.sort(availablecoupons, new Comparator<Availablecoupon>() {
            //    @Override
            //    public int compare(Availablecoupon o1, Availablecoupon o2) {
            //        int discAmt = o2.discountamt.compareTo(o1.discountamt);
            //        if (discAmt != 0) {
            //            return discAmt;
            //        }
            //        return o1.discountamt.compareTo(o2.discountamt);
            //    }
            //});

            cartBean.availablecoupons=availablecoupons;
        }
        else
        {
            if (cartBean != null && cartBean.selectedcoupons != null)
            {
                cartBean.selectedcoupons.Clear();
            }
            cartBean.couponpromoamt=0;

            cartBean.availablecoupons = availablecoupons;
        }
        return null;
    }

    //判断是否达到促销门槛
    private PromoActionFactory getPromoActionFactoryByThreshold(DBPROMOTION_CACHE_BEANMODEL dbPromotionCacheBean, EvaluateScopePromotion evaluateScopePromotion)
    {
        //LogManager.WriteLog("promotion", "getCode  ---->" + dbPromotionCacheBean.CODE);
        bool isEligible = false;
        //判断是件数条件判断，走件数判断逻辑
        if (EnumPromotionType.ITEM_COUNT_THRESHOLD.Equals(dbPromotionCacheBean.PROMOCONDITIONTYPE))
        {
            isEligible = OrderThresholdUtils.ItemCountThresholdevaluate(dbPromotionCacheBean, evaluateScopePromotion.getList(), evaluateScopePromotion);
        }
        else if (EnumPromotionType.ORDER_AMOUNT_THRESHOLD.Equals(dbPromotionCacheBean.PROMOCONDITIONTYPE))
        {
            //判断是金额条件判断，走金额判断逻辑
            isEligible = OrderThresholdUtils.OrderAmountThresholdvaluate(dbPromotionCacheBean, evaluateScopePromotion.getList(), evaluateScopePromotion);
        }
        else if (EnumPromotionType.ITEM_COMBINED_THRESHOLD.Equals(dbPromotionCacheBean.PROMOCONDITIONTYPE ))
        {
            //组合促销判断条件
            isEligible = OrderThresholdUtils.CombinedThresholdPromotionCondition(dbPromotionCacheBean, evaluateScopePromotion.getList(), evaluateScopePromotion);
        }
        else
        {
            isEligible = true;
        }

        PromoActionFactory promoActionFactory = null;
        //如果满足了，进行促销计算
        if (isEligible)
        {
            if (PromotionContextConvertUtils.isSingleLinePromotion(dbPromotionCacheBean))
            {
                //特殊处理单行优惠
                promoActionFactory = SingleLinePromotioncalculate(dbPromotionCacheBean, evaluateScopePromotion.getList());
            }
            else
            {
                promoActionFactory = promactioncalculate(dbPromotionCacheBean, evaluateScopePromotion.getList(), evaluateScopePromotion);
            }
        }
        //剔除商品
        //                if (evaluateScopePromotion.getList() != null && evaluateScopePromotion.getList().size() > 0) {
        //                    //剔除满足的list商品
        //                    allProducts.removeAll(evaluateScopePromotion.getList());
        //                }
        return promoActionFactory;
    }

    //根据不同的促销方式进行计算
    private PromoActionFactory promactioncalculate(DBPROMOTION_CACHE_BEANMODEL dbPromotionCacheBean, List<Product> products, EvaluateScopePromotion evaluateScopePromotion)
    {
        PromoActionFactory promoActionFactory = new PromoActionFactory(dbPromotionCacheBean);
        return promoActionFactory;
    }

    //根据不同的促销方式进行计算  //特殊处理单行优惠
    private PromoActionFactory SingleLinePromotioncalculate(DBPROMOTION_CACHE_BEANMODEL dbPromotionCacheBean, List<Product> products) {
        if (products != null && products.Count > 0) {
            for (int i = 0; i < products.Count; i++) {
                List<Product> newproducts = new List<Product>();
                newproducts.Add(products[i]);
                EvaluateScopePromotion evaluateScopePromotion = new EvaluateScopePromotion();

                if (PromotionCache.getInstance().getPromotionConditionUtils() != null && PromotionCache.getInstance().getPromotionConditionUtils().evaluateScope(dbPromotionCacheBean, newproducts, evaluateScopePromotion)) {
                    bool isEligible = false;
                    //判断是件数条件判断，走件数判断逻辑
                    if (EnumPromotionType.ITEM_COUNT_THRESHOLD.Equals(dbPromotionCacheBean.PROMOCONDITIONTYPE)) {
                        isEligible = OrderThresholdUtils.ItemCountThresholdevaluate(dbPromotionCacheBean, newproducts, evaluateScopePromotion);
                    } else if (EnumPromotionType.ORDER_AMOUNT_THRESHOLD.Equals(dbPromotionCacheBean.PROMOCONDITIONTYPE)) {
                        //判断是金额条件判断，走金额判断逻辑
                        isEligible = OrderThresholdUtils.OrderAmountThresholdvaluate(dbPromotionCacheBean, newproducts, evaluateScopePromotion);
                    } else {
                        isEligible = true;
                    }
                    if (isEligible) {
                        return promactioncalculate(dbPromotionCacheBean, newproducts, evaluateScopePromotion);
                    }
                }
            }
        }
        return null;
    }


    // 订单券应用
    public void applyOrderCouponPromo(Cart cartBean, List<PromotionCoupon> listcoupon) {
        if (listcoupon==null || listcoupon.Count==0) {
            return;
        }
        if (cartBean == null || cartBean.selectedcoupons == null || cartBean.selectedcoupons.Count == 0) {
            return;
        }
        List<List<TripletBeanForCoupon>> newCouponPromoBuckets = couponFilter(cartBean);
        if (newCouponPromoBuckets==null || newCouponPromoBuckets.Count==0) {
            return;
        }
        applyPromotions(newCouponPromoBuckets, cartBean);
    }

    public List<List<TripletBeanForCoupon>> couponFilter(Cart cartBean) {
        List<List<TripletBeanForCoupon>> newCouponPromoBuckets = new  List<List<TripletBeanForCoupon>>();
        if (cartBean.selectedcoupons == null) {
            return newCouponPromoBuckets;
        }
        List<TripletBeanForCoupon> combineList = new  List<TripletBeanForCoupon>();
        foreach(String key in cartBean.selectedcoupons.Keys) {
            TripletBeanForCoupon promoTriplet = null;
//            if ("orderCoupon".equals(type)) {
            String Promotioncode = cartBean.selectedcoupons[key].promotioncode;
            //LogManager.WriteLog("promotion", "--Promotioncode-->" + Promotioncode);
            DBPROMOTION_CACHE_BEANMODEL DBPROMOTION_CACHE_BEANMODEL = getDBPROMOTION_CACHE_BEANMODEL(Promotioncode);//SQliteUtils.getInstance().QueryPromotionByCode(Promotioncode, tenantId, shopId);
            if (DBPROMOTION_CACHE_BEANMODEL != null) {
                promoTriplet = new TripletBeanForCoupon();
                promoTriplet.setPromoTriplet(DBPROMOTION_CACHE_BEANMODEL);
            }
//            }

            if (promoTriplet == null) {
                continue;
            }
            DBPROMOTION_CACHE_BEANMODEL promotion = promoTriplet.getPromoTriplet();
            if (promotion.CANBECOMBINED==1) {
                combineList.Add(promoTriplet);
            } else {
                List<TripletBeanForCoupon> templst = new System.Collections.Generic.List<Promotion.TripletBeanForCoupon>(){promoTriplet};
                newCouponPromoBuckets.Add(templst);
            }
        }
        if (combineList==null || combineList.Count==0) {
            newCouponPromoBuckets.Add(combineList);
        }
        return newCouponPromoBuckets;
    }

    private void applyPromotions( List<List<TripletBeanForCoupon>> promoBuckets,  Cart cartBean) {
        List<KeyValuePair<TripletBeanForCoupon, EvaluateScopePromotion>> bestValue = null;
        Decimal bestDiscountValue = Decimal.Zero;
        long bestMaxRank = 0;
        List<Product> allProducts = new List<Product>();
        foreach ( List<TripletBeanForCoupon> promoBucket in promoBuckets) {
            try {
                if (promoBucket==null || promoBucket.Count==0) {
                    continue;
                }
                List<KeyValuePair<TripletBeanForCoupon, EvaluateScopePromotion>> applicable = new List<KeyValuePair<TripletBeanForCoupon,EvaluateScopePromotion>>();
                Decimal discountValue = Decimal.Zero;
                long maxRank = 0;
                foreach ( TripletBeanForCoupon promo in promoBucket) {
                    TripletBeanForCoupon bucketContext = promo;
                    EvaluateScopePromotion evaluateScopePromotion = new EvaluateScopePromotion();

                    if (evaluateScope(promo.getPromoTriplet(), cartBean.products, evaluateScopePromotion)) {
                         PromoActionFactory promoActionFactory = getPromoActionFactoryByThreshold(promo.getPromoTriplet(), evaluateScopePromotion);
                         if (promoActionFactory != null)
                         {
                             Decimal pdisc = promoActionFactory.getDiscountValue(evaluateScopePromotion, evaluateScopePromotion.getList(), promo.getPromoTriplet());

                             bucketContext.setPromoAction(promoActionFactory.getAbstractPromoAction());

                             if (MoneyUtils.isFirstBiggerThanSecond(pdisc, Decimal.Zero))
                             {
                                 applicable.Add(new KeyValuePair<TripletBeanForCoupon, EvaluateScopePromotion>(bucketContext, evaluateScopePromotion));
                                 discountValue = discountValue + pdisc;
                                 if (promo.getPromoTriplet().RANK > maxRank)
                                 {
                                     maxRank = promo.getPromoTriplet().RANK;
                                 }
                             }
                         }

                         if (evaluateScopePromotion.getList() != null && evaluateScopePromotion.getList().Count > 0)
                         {
                             allProducts.AddRange(evaluateScopePromotion.getList());
                         }

                    }
                }

                if (applicable!=null && applicable.Count!=0  && MoneyUtils.isFirstBiggerThanSecond(discountValue, bestDiscountValue)) {
                    bestDiscountValue = discountValue;
                    bestValue = applicable;
                    bestMaxRank = maxRank;
                } else if (applicable!=null && applicable.Count!=0 && MoneyUtils.isFirstEqualToSecond(discountValue,
                        bestDiscountValue) && maxRank > bestMaxRank) {
                    bestDiscountValue = discountValue;
                    bestValue = applicable;
                    bestMaxRank = maxRank;
                }
            } catch (Exception exp) {
                LogManager.WriteLog("promotion", exp.Message);
                //exp.printStackTrace();
            }
        }

        if (bestValue!=null && bestValue.Count>0) {
            Decimal discount = Decimal.Zero;
            foreach (KeyValuePair<TripletBeanForCoupon, EvaluateScopePromotion> bucketContext in bestValue) {
                if (bucketContext.Key.getPromoAction() != null) {
                    Decimal couponsdiscount = bucketContext.Key.getPromoAction().getDiscountValue(bucketContext.Value, allProducts, bucketContext.Key.getPromoTriplet());
                    discount = MoneyUtils.add(discount, couponsdiscount);
                }
            }
            if (MoneyUtils.isBiggerThanZero(discount)) {
                if (cartBean.orderpricedetails == null) {
                    List<OrderPriceDetail> orderpricedetails = new List<OrderPriceDetail>();
                    cartBean.orderpricedetails = orderpricedetails;//.setOrderpricedetails(orderpricedetails);//购物车商品明细
                }
                if (MoneyUtils.isFirstBiggerThanOrEqualToSecond(discount, cartBean.payamt)) {
                    discount = cartBean.payamt;
                    cartBean.couponpromoamt = discount;
                }

                cartBean.couponpromoamt = discount;
                OrderPriceDetail temppricedetial = new OrderPriceDetail();
                temppricedetial.title = "优惠券抵";
                temppricedetial.amount = "-¥ " + PromotionInfoUtils.getFixedScaleAmt(discount, 2);
                cartBean.orderpricedetails.Add(temppricedetial);

                cartBean.payamt = MoneyUtils.substract(cartBean.payamt, discount);
                cartBean.totalpayment = cartBean.payamt;
                cartBean.poscouponpromoamt = discount;
            }
        }
    }
}

}
