using Maticsoft.BLL;
using Maticsoft.Model;
using Newtonsoft.Json;
using WinSaasPOS.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace WinSaasPOS.Model.Promotion
{
  public class ImplOfflineSingleCalculate {

      private String tenantId;
      private String shopId;
      private DBPROMOTION_CACHE_BEANBLL promotionbll = new DBPROMOTION_CACHE_BEANBLL();

      public ImplOfflineSingleCalculate(String tenantId, String shopId)
      {
          this.tenantId = tenantId;
          this.shopId = shopId;
      }

      //计算价格
      public void calculate(Product productBean)
      {
          try
          {

              TripletBean currentPriceTriplet = null;
              //productBean.ORIGINPRICE(new Decimal(productBean.getPrice().getOriginprice() + ""));

              List<DBPROMOTION_CACHE_BEANMODEL> list = QueryPromotionBySkucode(productBean.skucode, tenantId, shopId);
              TripletBean tripletBean = calcPromotionPrice(list, productBean);
              TripletBean discountPricePair = calcPromotionDiscountPrice(productBean);
              currentPriceTriplet = comparePricePair(tripletBean, discountPricePair);
              if (currentPriceTriplet != null)
              {
                  if (productBean.price != null && currentPriceTriplet.getPromoTriplet() != null && !string.IsNullOrEmpty(currentPriceTriplet.getPromoTriplet().CODE) && !string.IsNullOrEmpty(currentPriceTriplet.getPromoTriplet().COSTCENTERINFO))
                  {
                      productBean.price.saleprice = currentPriceTriplet.getOriginprice();
                      if (!productBean.weightflag)
                      {
                          productBean.price.total = Math.Round(productBean.num * productBean.price.saleprice, 2, MidpointRounding.AwayFromZero);
                          productBean.price.origintotal = Math.Round(productBean.num * productBean.price.originprice, 2, MidpointRounding.AwayFromZero);
                          productBean.PaySubAmt = Math.Round(productBean.num * productBean.price.saleprice, 2, MidpointRounding.AwayFromZero);
                      }
                      else
                      {
                          productBean.price.total = Math.Round(productBean.price.saleprice * productBean.price.specnum, 2, MidpointRounding.AwayFromZero);
                          productBean.price.origintotal = Math.Round(productBean.price.originprice * productBean.price.specnum, 2, MidpointRounding.AwayFromZero);
                          productBean.PaySubAmt = Math.Round(productBean.price.saleprice * productBean.price.specnum, 2, MidpointRounding.AwayFromZero);
                      }

                      //productBean.PaySubAmt = currentPriceTriplet.getOriginprice();
                      productBean.price.Promotioncode = currentPriceTriplet.getPromoTriplet().CODE;
                      productBean.price.Outerpromocode = currentPriceTriplet.getPromoTriplet().OUTERCODE;
                      productBean.price.Pricekind = currentPriceTriplet.getPriceKind();
                      productBean.price.Costcenterinfo = currentPriceTriplet.getPromoTriplet().COSTCENTERINFO;
                      productBean.price.promoamt = productBean.price.origintotal - productBean.price.total;
                      productBean.price.pricepromoamt = productBean.price.origintotal - productBean.price.total;
                      productBean.pricetagid = currentPriceTriplet.getPricetagid();
                      productBean.pricetag = currentPriceTriplet.getPricetag();
                     // productBean.PromoSubAmt = productBean.price.origintotal - productBean.price.total;

                      PerfectPro(productBean,currentPriceTriplet);
                      //  ZzLog.e("productBean--->" + productBean.getSkucode() + "---name-->" + productBean.getSkuname() + "---price-->" + productBean.getPrice().getSaleprice());
                  }
                  else
                  {
                      // ZzLog.e("products.get(i).getPrice()  is null or promon 不合法");
                  }
              }
          }
          catch (Exception ex)
          {
              LogManager.WriteLog("ERROR", "计算单品促销异常:" + ex.Message);
          }
      }

      //比较两个优惠后价格
      private TripletBean comparePricePair(TripletBean tripletBean, TripletBean tripletBean2)
      {
          if (tripletBean == null || tripletBean2 != null && MoneyUtils.isFirstBiggerThanOrEqualToSecond(tripletBean.getOriginprice(), tripletBean2.getOriginprice()))
          {
              return tripletBean2;
          }
          return tripletBean;
      }

    //查找适合的促销对象
    private TripletBean calcPromotionPrice(List<DBPROMOTION_CACHE_BEANMODEL> list, Product productBean) {
        sortPromotion(list);
        for (int n = 0; n < list.Count; n++)
        {
            DBPROMOTION_CACHE_BEANMODEL dbPromotionCacheBean = list[n];
            if (dbPromotionCacheBean != null)
            {
                if (EnumPromotionType.ALWAYS_PASS.Equals(dbPromotionCacheBean.PROMOCONDITIONTYPE))
                {
                    if (evaluateSkuScope(dbPromotionCacheBean, productBean))
                    {
                        //计算逻辑
                        return getNewPriceAndKind(dbPromotionCacheBean, productBean);
                    }
                }
                else if (EnumPromotionType.GOODS_ID_MATCH.Equals(dbPromotionCacheBean.PROMOCONDITIONTYPE))
                {
                    if (evaluateSkuScope(dbPromotionCacheBean, productBean))
                    {
                        if (dbPromotionCacheBean.PROMOCONDITIONCONTEXT.Equals(productBean.skucode))
                        {
                            //计算逻辑
                            return getNewPriceAndKind(dbPromotionCacheBean, productBean);
                        }
                    }
                }
            }
        }
        return null;
    }

    private TripletBean calcPromotionDiscountPrice(Product productBean){
       // ZzLog.e("getFirstcategoryid  ---->" + productBean.getFirstcategoryid());
        //TODO  getFirstcategoryid对应字段
        List<DBPROMOTION_CACHE_BEANMODEL> list = QueryPromotionByCategory(productBean.firstcategoryid, tenantId, shopId); 
       // ZzLog.e("list  ---->" + list.size());
        return calcPromotionPrice(list, productBean);
    }

    //判断是否符合促销逻辑
    private bool evaluateSkuScope(DBPROMOTION_CACHE_BEANMODEL dbPromotionCacheBean, Product productBean) {
        if (!isInTimeRange(dbPromotionCacheBean))
        {
            return false;
        }

        PromotionRealmDetail realmDetail = JsonConvert.DeserializeObject<PromotionRealmDetail>(dbPromotionCacheBean.ELIGIBILITYCONDITION);
        if (realmDetail == null)
        {
            return true;
        }
        if (dbPromotionCacheBean.ORDERSUBTYPE != null && (8 & dbPromotionCacheBean.ORDERSUBTYPE) == 0) {//8
            return false;
        }
        if (EnumPromotionType.REALM_ALL.Equals(dbPromotionCacheBean.PROMOCONDITIONTYPE)) {
            if (realmDetail.skuCodesToExclude != null && realmDetail.skuCodesToExclude.Contains(productBean.skucode)) {
                return false;
            } else {
                return true;
            }

        }
        else if (EnumPromotionType.REALM_GOODS.Equals(dbPromotionCacheBean.PROMOCONDITIONTYPE))
        {
            if (realmDetail.skuCodesToInclude != null && realmDetail.skuCodesToInclude.Contains(productBean.skucode))
            {
                return true;
            }
            return false;
        }
        else if (EnumPromotionType.REALM_CATALOG.Equals(realmDetail.realmType))
        {
            if (realmDetail.skuCodesToExclude != null && realmDetail.skuCodesToExclude.Contains(productBean.skucode))
            {
                return false;
            }
            if (string.IsNullOrEmpty(productBean.firstcategoryid))
            {
                return false;
            }

            if (realmDetail.catalogsToInclude != null && !realmDetail.catalogsToInclude.Contains(productBean.firstcategoryid))
            {
                return true;
            }
            return false;
        }
        else if (EnumPromotionType.REALM_MIXED.Equals(realmDetail.realmType))
        {
            if (realmDetail.skuCodesToExclude != null && realmDetail.skuCodesToExclude.Count > 0 && realmDetail.skuCodesToExclude.Contains(
                    productBean.skucode))
            {
                return false;
            }
            if (string.IsNullOrEmpty(productBean.firstcategoryid))
            {
                if (realmDetail.catalogsToExclude==null
                        && !realmDetail.catalogsToExclude.Contains(productBean.firstcategoryid) )
                {
                    return false;
                }
                if ((realmDetail.catalogsToInclude != null
                        && ! realmDetail.catalogsToInclude.Contains(productBean.firstcategoryid) ))
                {
                    return true;
                }
            }
            if (realmDetail.skuCodesToInclude==null && realmDetail.skuCodesToInclude.Contains(productBean.skucode))
            {
                return true;
            }
            return false;
        }
        return false;
    }

    private TripletBean getNewPriceAndKind(DBPROMOTION_CACHE_BEANMODEL dbPromotionCacheBean, Product productBean) {
        Decimal newPrice = getCalculationPrice(dbPromotionCacheBean, productBean);
        if (MoneyUtils.isFirstBiggerThanOrEqualToSecond(productBean.price.originprice, newPrice)) {
            String priceKind = EnumPromotionType.PROMOTION_PRICE;
            if ( EnumPromotionType.PROMOTION_PRICE_DISCOUNT_ACTION.Equals(dbPromotionCacheBean.PROMOACTION)) {//打折
                priceKind = EnumPromotionType.PROMOTION_DISCOUNT_PRICE;
            }
            if (MoneyUtils.isFirstEqualToSecond(newPrice, productBean.price.originprice,2)) {
                priceKind = EnumPromotionType.MARKET_PRICE;
            }
            int pricetagid = -1;
            String pricetag = "";
            if (EnumPromotionType.PROMOTION_PRICE_DISCOUNT_ACTION.Equals(dbPromotionCacheBean.PROMOACTION)) {
                pricetag = MoneyUtils.newMoney(dbPromotionCacheBean.PROMOACTIONCONTEXT) + "折";
                pricetagid = 2;
            } else {
                pricetag = "直降";
                pricetagid = 3;
            }
           // ZzLog.e("newPrice--->" + newPrice);
            return new TripletBean(newPrice, priceKind, dbPromotionCacheBean, pricetagid, pricetag);
        }
        return null;
    }

    //单品计算逻辑
    private Decimal getCalculationPrice(DBPROMOTION_CACHE_BEANMODEL dbPromotionCacheBean, Product productBean) {
        if (EnumPromotionType.PROMOTION_PRICE_DISCOUNT_ACTION.Equals(dbPromotionCacheBean.PROMOACTION)) {//打折
                    Decimal discount = Math.Round(MoneyUtils.newMoney(dbPromotionCacheBean.PROMOACTIONCONTEXT), 2, MidpointRounding.AwayFromZero);
            Decimal originprice = productBean.price.originprice;
            if (MoneyUtils.isBiggerThanZero(discount)
                    && MoneyUtils.isFirstBiggerThanSecond(Decimal.One, discount)) {
                originprice = MoneyUtils.multiply(originprice, discount);
                return originprice;
            }
            return originprice;
        }
        return MoneyUtils.newMoney(dbPromotionCacheBean.PROMOACTIONCONTEXT);
    }

      //促销排序
      private void sortPromotion(List<DBPROMOTION_CACHE_BEANMODEL> list) {


          list.OrderBy(x => x.FROMOUTER).ThenByDescending(x => x.RANK);
        //  list.Sort();
        //  //TODO
        //if (Build.VERSION.SDK_INT >= 24) {
        //    list.ort((o1, o2) -> {
        //        //fromouter相同时按优先级、时间倒序
        //        if (o1.getFromouter().Equals(o2.getFromouter())) {
        //            if (o2.getRank().Equals(o1.getRank())) {//优先级相同,按时间排序
        //                return o2.getUpdatedat().compareTo(o1.getUpdatedat());
        //            } else {
        //                return o2.getRank().compareTo(o1.getRank());
        //            }
        //        } else { 
        //            //fromouter优先
        //            if (o1.getFromouter()) {
        //                return -1;
        //            } else {
        //                return 1;
        //            }
        //        }
        //    });
        //}
    }

    //判断是否在时间范围内
    private bool isInTimeRange(DBPROMOTION_CACHE_BEANMODEL promotion) {
        //默认适用，当且仅当成对时间范围不为空，且不在该范围内时进行时间范围判断
        bool inTimeRange = true;
        try {
            String enabledTimeInfo = promotion.ENABLEDTIMEINFO;
            if (!string.IsNullOrEmpty(enabledTimeInfo)) {
                ActivityEnabledTimeInfo activityEnabledTimeInfo = JsonConvert.DeserializeObject<ActivityEnabledTimeInfo>(enabledTimeInfo);
                //周循环处理
                if (!string.IsNullOrEmpty(activityEnabledTimeInfo.weekcycleData)) {
                    String[] weekcycleData = activityEnabledTimeInfo.weekcycleData.Split(',');
                    if (1 != (int)DateTime.Now.DayOfWeek) {
                        return false;
                    }
                }
                long currentTime = Convert.ToInt64( MainModel.getStampByDateTime(DateTime.Now)); 
                if (!string.IsNullOrEmpty(activityEnabledTimeInfo.startTime1) && !string.IsNullOrEmpty(activityEnabledTimeInfo.endTime1)) {
                    long start = parseTime(activityEnabledTimeInfo.startTime1);
                    long end = parseTime(activityEnabledTimeInfo.endTime1);
                    inTimeRange = start <= currentTime && currentTime <= end;
                }
                if (!inTimeRange && !string.IsNullOrEmpty(activityEnabledTimeInfo.startTime2) && !string.IsNullOrEmpty(activityEnabledTimeInfo.endTime2)) {
                    long start = parseTime(activityEnabledTimeInfo.startTime2);
                    long end = parseTime(activityEnabledTimeInfo.endTime2);
                    inTimeRange = start <= currentTime && currentTime <= end;
                }
                if (!inTimeRange && !string.IsNullOrEmpty(activityEnabledTimeInfo.startTime3) && !string.IsNullOrEmpty(activityEnabledTimeInfo.endTime3)) {
                    long start = parseTime(activityEnabledTimeInfo.startTime3);
                    long end = parseTime(activityEnabledTimeInfo.endTime3);
                    inTimeRange = start <= currentTime && currentTime <= end;
                }
            }
        } catch (Exception e) {
        }
        return inTimeRange;
    }

    private long parseTime(String time) {
//        String shortPattern = "HH:mm";
//        String longPattern = "HH:mm:ss";
        String[] split = time.Split(':');
        String currentTims = DateTime.Now.ToString("yyyy-MM-dd");
        String pattern = currentTims + " " + time + ":00";
        if (split.Length == 3) {
            pattern = currentTims + " " + time;
        }
        return Convert.ToInt64( MainModel.getStampByDateTime(Convert.ToDateTime(pattern)));
    }




    /// <summary>
    /// 
    /// </summary>
    /// <param name="skucode"></param>
    /// <param name="tenantId"></param>
    /// <param name="shopId"></param>
    /// <returns></returns>
//查询促销过滤
    public List<DBPROMOTION_CACHE_BEANMODEL> QueryPromotionBySkucode(String skucode, String tenantId, String shopId) {


        long currentTime = Convert.ToInt64(MainModel.getStampByDateTime(DateTime.Now));

        string strwhere = " PROMOCONDITIONCONTEXT ='" + skucode + "' and TENANTID='" + tenantId + "' and SHOPID ='" + shopId  + "' and ENABLED =1  and CREATE_URL_IP ='" + MainModel.URL + "' and ENABLEDFROM <" + currentTime + " and ENABLEDTO >" + currentTime;

        List<DBPROMOTION_CACHE_BEANMODEL> lstpro = promotionbll.GetModelList(strwhere);

        return lstpro;
    }

    //查询促销过滤
    public List<DBPROMOTION_CACHE_BEANMODEL> QueryPromotionByCategory(String firstcategoryid, String tenantId, String shopId) {


          long currentTime = Convert.ToInt64(MainModel.getStampByDateTime(DateTime.Now));

        string strwhere = " AVAILABLECATEGORY LIKE '%" + firstcategoryid + "%' and TENANTID='" + tenantId + "' and SHOPID ='" + shopId  + "' and ENABLED =1  and CREATE_URL_IP ='" + MainModel.URL  + "' and ENABLEDFROM <" + currentTime + " and ENABLEDTO >" + currentTime;

        List<DBPROMOTION_CACHE_BEANMODEL> lstpro = promotionbll.GetModelList(strwhere);

        return lstpro;


}

    /**
     * 获取当前日期是星期几<br>
     *
     * @return 当前日期是星期几
     */
    //public int getWeekOfDate() {
    //    // String[] weekDays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
    //    int[] weekDays = {6, 0, 1, 2, 3, 4, 5};
    //    Calendar cal = Calendar.CurrentEra;
    //    cal.setTime(new Date());
    //    int w = cal.get(Calendar.DAY_OF_WEEK) - 1;
    //    if (w < 0)
    //        w = 0;
    //    return weekDays[w];
    //}



    private void PerfectPro(Product pro, TripletBean currentPriceTriplet)
    {
        try
        {

            OffLinePromos offlinepromos = new OffLinePromos();
            //单品记录促销信息
            offlinepromos.promotioncode=currentPriceTriplet.getPromoTriplet().CODE;
            offlinepromos.outerpromocode=currentPriceTriplet.getPromoTriplet().OUTERCODE;
            offlinepromos.costcenterinfo=currentPriceTriplet.getPromoTriplet().COSTCENTERINFO;
            offlinepromos.promoaction=currentPriceTriplet.getPromoTriplet().PROMOACTION;
            offlinepromos.promosubtype=currentPriceTriplet.getPromoTriplet().PROMOSUBTYPE;
            offlinepromos.promotype=currentPriceTriplet.getPromoTriplet().PROMOTYPE;
            offlinepromos.promoamt = pro.price.origintotal - pro.price.total;

            List<OffLinePromos> lstpromos = new List<OffLinePromos>();
            lstpromos.Add(offlinepromos);

            pro.offlinepromos = lstpromos;

        }
        catch (Exception ex)
        {
            LogManager.WriteLog("添加商品促销信息异常"+ex.Message);
        }

    }
}

}
