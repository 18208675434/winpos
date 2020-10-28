using Maticsoft.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Model.Promotion
{
public class ImplOfflineSingleCalculateNew {

    private String tenantId;
    private String shopId;

    private Dictionary<String, Product> map = new Dictionary<string,Product>();
    private Dictionary<String, TripletBeanForCoupon> mapTripletBean = new  Dictionary<string,TripletBeanForCoupon>();

    public ImplOfflineSingleCalculateNew(String tenantId, String shopId) {
        this.tenantId = tenantId;
        this.shopId = shopId;

    }

		//单品有没有计算过
    public bool containsKey(String skucode) {
        return map.ContainsKey(skucode);
    }

    //设置单品促销已经计算过的,直接从map 中拿取值
    public TripletBeanForCoupon setCacheValue(Product productBean, Price mPriceBean) {
        TripletBeanForCoupon currentPriceTriplet = null;
        Product productBean1 = getContainsKeyProductBean(productBean.skucode);
        if (productBean1 != null) {
            currentPriceTriplet = getContainsKeyTripletBeann(productBean.skucode);
            if (currentPriceTriplet != null) {
                productBean.pricetagid=currentPriceTriplet.getPricetagid();
                productBean.pricetag=currentPriceTriplet.getPricetag();
                if (productBean1.price != null) {
                    mPriceBean.originpricedesc=productBean1.price.originpricedesc;
                    mPriceBean.strikeout= productBean1.price.strikeout;// .setStrikeout(productBean1.getPrice().getStrikeout());
                    mPriceBean.flag = productBean1.price.flag;//.setFlag(productBean1.getPrice().getFlag());
                }
                if (currentPriceTriplet.getPromoTriplet() != null) {
                    productBean.purchaselimit=(int)currentPriceTriplet.getPromoTriplet().PURCHASELIMIT; //.setPurchaselimit(currentPriceTriplet.getPromoTriplet().getPurchaselimit());
                    productBean.canmixcoupon=currentPriceTriplet.getPromoTriplet().CANMIXCOUPON==1?true:false;///.setCanmixcoupon(currentPriceTriplet.getPromoTriplet().getCanmixcoupon());
                }
                if (EnumPromotionType.MEMBER_PRICE_ACTION.Equals(currentPriceTriplet.getPriceKind()) || EnumPromotionType.MEMBER_PRICE_DISCOUNT_ACTION.Equals(currentPriceTriplet.getPriceKind())) {
                    if (PromotionCache.getInstance().isLoginMember) {
                        mPriceBean.saleprice=currentPriceTriplet.getOriginprice();
                       // mPriceBean.setSaleprice(Double.parseDouble(String.format("%.2f", currentPriceTriplet.getOriginprice())));
                    } else {
                        mPriceBean.originprice=currentPriceTriplet.getOriginprice();
                        //mPriceBean.setOriginprice(Double.parseDouble(String.format("%.2f", currentPriceTriplet.getOriginprice())));
                    }
                } else {
                    mPriceBean.saleprice=currentPriceTriplet.getOriginprice();
                    //mPriceBean.setSaleprice(Double.parseDouble(String.format("%.2f", currentPriceTriplet.getOriginprice())));
                }
            }
        }
        return currentPriceTriplet;
    }



//计算过的商品
    public Product getContainsKeyProductBean(String skucode) {
        return map[skucode];
    }

    public TripletBeanForCoupon getContainsKeyTripletBeann(String skucode) {
        return mapTripletBean[skucode];
    }

    public void onCreate() {
        map.Clear();
        mapTripletBean.Clear();
        PromotionCache.getInstance().onCreate(tenantId, shopId);
    }


    //计算价格
    public TripletBeanForCoupon calculate(Product productBean) {
       
        TripletBeanForCoupon currentPriceTriplet = null;
        productBean.originprice=productBean.price.originprice;

        TripletBeanForCoupon promotionPricePair = calcPromotionPrice(productBean);//促销价直降
        TripletBeanForCoupon discountPricePair = calcPromotionDiscountPrice(productBean);//打折价
        TripletBeanForCoupon memberPricePair = null;//会员价
        //TripletBeanForCoupon memberDiscountPricePair = null;//会员打折

        if (!MainModel.IsOffLine) {

            memberPricePair = getMemberPriceKind(productBean);//会员价促销
            //memberPricePair = calcmemberPrice(productBean);//会员价促销
            //memberDiscountPricePair = calcmemberDiscountPrice(productBean);//会员打折
        }
        TripletBeanForCoupon memberGradePricePair = null;
        DBPROMOTION_CACHE_BEANMODEL memberGradeDiscountPricePromo = null;
        bool gradeMember = false;
        if (PromotionCache.getInstance().isLoginMember) {
            if (PromotionCache.getInstance().getMemberRightsForGradeBean() != null) 
            {
                gradeMember = PromotionCache.getInstance().getMemberRightsForGradeBean().isGradeMember();
                memberGradeDiscountPricePromo = PromotionCache.getInstance().getMemberRightsForGradeBean().getMemberGradeDiscountPricePromo();

                if (memberGradeDiscountPricePromo != null && memberGradeDiscountPricePromo.CANBECOMBINED != 1)
                {
                    memberGradePricePair = calcPaidUpMemberPrice(memberGradeDiscountPricePromo, productBean);
                }
            }
        }

        //会员价与会员折扣价取较低值
        currentPriceTriplet = comparePricePair(memberPricePair, currentPriceTriplet);
        //会员价为空，或促销价 <= 会员价，用促销价
        currentPriceTriplet = comparePricePair(currentPriceTriplet, promotionPricePair);
        //比较折扣价
        currentPriceTriplet = comparePricePair(currentPriceTriplet, discountPricePair);
        //会员等级促销
        if (currentPriceTriplet == null || currentPriceTriplet.getPriceKind() == null || currentPriceTriplet.getPromoTriplet() == null) {
            //会员等级价为空，或会员价 <= 会员等级价，用会员价
            if (memberGradePricePair != null) {
                currentPriceTriplet = comparePricePair(currentPriceTriplet, memberGradePricePair);
            }
        }
        else
        {
            if (currentPriceTriplet != null)
            {
                if (gradeMember && memberGradeDiscountPricePromo != null && memberGradeDiscountPricePromo.CANBECOMBINED != 1)
                {
                    currentPriceTriplet = comparePricePair(currentPriceTriplet, memberGradePricePair);
                }

            }
        }

        TripletBeanForCoupon paidUpDiscountPrice = null;
        //会员等级 折上折
        if (gradeMember && memberGradeDiscountPricePromo != null && memberGradeDiscountPricePromo.CANBECOMBINED==1) {
            Decimal originprice = productBean.originprice;
            if (currentPriceTriplet != null)
            {
                productBean.originprice = currentPriceTriplet.getOriginprice();// .setOriginprice(currentPriceTriplet.getOriginprice());
            }
            
            paidUpDiscountPrice = calcPaidUpMemberPrice(memberGradeDiscountPricePromo, productBean);

            if (currentPriceTriplet != null && currentPriceTriplet.getPriceKind() != EnumPromotionType.MARKET_PRICE)
            {
                //存储中间价格信息
                if (currentPriceTriplet.getPriceKind()!= EnumPromotionType.MARKET_PRICE) {
                    TransitionPriceDetail transitionPriceDetail1 = new TransitionPriceDetail();
                    transitionPriceDetail1.priceKind = currentPriceTriplet.getPriceKind();//.setPriceKind(currentPriceTriplet.getPriceKind());
                    transitionPriceDetail1.originprice = currentPriceTriplet.getOriginprice();//.setOriginprice(currentPriceTriplet.getOriginprice());
                    transitionPriceDetail1.code = currentPriceTriplet.getPromoTriplet().CODE;//.setCode(currentPriceTriplet.getPromoTriplet().CODE);
                    transitionPriceDetail1.pricetag = currentPriceTriplet.getPricetag();//.setPricetag(currentPriceTriplet.getPricetag());
                    transitionPriceDetail1.pricetagid = currentPriceTriplet.getPricetagid();//.setPricetagid(currentPriceTriplet.getPricetagid());
                    transitionPriceDetail1.costcenterinfo = currentPriceTriplet.getPromoTriplet().COSTCENTERINFO;//.setCostcenterinfo(currentPriceTriplet.getPromoTriplet().COSTCENTERINFO);
                    transitionPriceDetail1.outercode = currentPriceTriplet.getPromoTriplet().OUTERCODE;//.setOutercode(currentPriceTriplet.getPromoTriplet().OUTERCODE);
                    transitionPriceDetail1.promoaction = currentPriceTriplet.getPromoTriplet().PROMOACTION;//.setPromoaction(currentPriceTriplet.getPromoTriplet().PROMOACTION);
                    transitionPriceDetail1.promosubtype = currentPriceTriplet.getPromoTriplet().PROMOSUBTYPE;//.setPromosubtype(currentPriceTriplet.getPromoTriplet().PROMOSUBTYPE);
                    transitionPriceDetail1.promotype = currentPriceTriplet.getPromoTriplet().PROMOTYPE;//.setPromotype(currentPriceTriplet.getPromoTriplet().PROMOTYPE);

                    if (productBean.goodstagid == 0) {
                        productBean.price.total=productBean.price.saleprice*productBean.num;
                        productBean.price.origintotal=productBean.originprice*productBean.num;
                        //productBean.price.setTotal(DoubluUtils.setDouble(DoubluUtils.mul(productBean.price.getSaleprice(), productBean.getNum())));
                        //productBean.price.setOrigintotal(DoubluUtils.setDouble(productBean.price.getOriginprice() * productBean.getNum()));
                    } else {
                         productBean.price.total=productBean.price.saleprice*productBean.specnum;
                        productBean.price.origintotal=productBean.originprice*productBean.specnum;
                        //productBean.price.setTotal(DoubluUtils.setDouble(productBean.price.getSaleprice() * productBean.getSpecnum()));
                       // productBean.price.setOrigintotal(DoubluUtils.setDouble(productBean.price.getOriginprice() * productBean.getSpecnum()));
                    }
                    Decimal pricepromoamt = productBean.price.origintotal - productBean.price.total;

                    transitionPriceDetail1.pricepromoamt = pricepromoamt;// .setPricepromoamt(pricepromoamt);
                    productBean.transitionPriceDetail = transitionPriceDetail1;
                    //productBean.setTransitionPriceDetail1(transitionPriceDetail1);
                }
            } else {
                productBean.originprice=originprice;
            }
        }
        currentPriceTriplet = comparePricePair(currentPriceTriplet, paidUpDiscountPrice);

        //付费会员待定
        if (currentPriceTriplet != null) {
            if (productBean.price != null && currentPriceTriplet.getPromoTriplet() != null && !string.IsNullOrEmpty(currentPriceTriplet.getPromoTriplet().CODE) && !string.IsNullOrEmpty(currentPriceTriplet.getPromoTriplet().COSTCENTERINFO)) {
                if (!PromotionCache.getInstance().isOffline && (EnumPromotionType.MEMBER_PRICE_ACTION.Equals(currentPriceTriplet.getPriceKind()) || EnumPromotionType.MEMBER_PRICE_DISCOUNT_ACTION.Equals(currentPriceTriplet.getPriceKind()))) {
                    if (PromotionCache.getInstance().isLoginMember) {
                        productBean.price.saleprice=currentPriceTriplet.getOriginprice();//.setSaleprice(Double.parseDouble(String.format("%.2f", currentPriceTriplet.getOriginprice())));
                        productBean.price.originpricedesc="非会员价";//.setOriginpricedesc("非会员价");
                        productBean.pricetagid=currentPriceTriplet.getPricetagid();//.setPricetagid(currentPriceTriplet.getPricetagid());
                        productBean.pricetag=currentPriceTriplet.getPricetag();//.setPricetag(currentPriceTriplet.getPricetag());
                        if (currentPriceTriplet.getPromoTriplet() != null) {
                            productBean.canmixcoupon=currentPriceTriplet.getPromoTriplet().CANMIXCOUPON==1?true:false;
                        }
                        productBean.price.flag = 2;
                        productBean.purchaselimit=(int)currentPriceTriplet.getPromoTriplet().PURCHASELIMIT;
                    } else {

                        productBean.price.originprice=currentPriceTriplet.getOriginprice();//.setOriginprice(Double.parseDouble(String.format("%.2f", currentPriceTriplet.getOriginprice())));
                        productBean.price.flag = 1;
                        productBean.price.originpricedesc="会员价";//.setOriginpricedesc("会员价");
                        productBean.pricetagid=currentPriceTriplet.getPricetagid();//.setPricetagid(currentPriceTriplet.getPricetagid());
                        productBean.pricetag=currentPriceTriplet.getPricetag();//.setPricetag(currentPriceTriplet.getPricetag());
                    }
                } else {
                    productBean.pricetagid=currentPriceTriplet.getPricetagid();//.setPricetagid(currentPriceTriplet.getPricetagid());
                    productBean.pricetag=currentPriceTriplet.getPricetag();//.setPricetag(currentPriceTriplet.getPricetag());
                    if (currentPriceTriplet.getPromoTriplet() != null) {
                        productBean.canmixcoupon=currentPriceTriplet.getPromoTriplet().CANMIXCOUPON==1?true:false;//.setCanmixcoupon(currentPriceTriplet.getPromoTriplet().getCanmixcoupon());
                    }

                    productBean.price.saleprice=currentPriceTriplet.getOriginprice();//.setSaleprice(Double.parseDouble(String.format("%.2f", currentPriceTriplet.getOriginprice())));
                    productBean.purchaselimit=(int)currentPriceTriplet.getPromoTriplet().PURCHASELIMIT;//.setPurchaselimit(currentPriceTriplet.getPromoTriplet().getPurchaselimit());
                }
                if (productBean.price.originprice != productBean.price.saleprice) {
                    if (productBean.pricetagid == 3) {//直降
                        productBean.price.strikeout=1;
                    } else {
                        productBean.price.strikeout=0;
                    }
                } else {
                    productBean.price.strikeout=0;
                }
                // LogManager.WriteLog("promotion","productBean--->" + productBean.skucode + "---name-->" + productBean.title + "---price-->" + productBean.price.saleprice + "code-->" + currentPriceTriplet.getPromoTriplet().CODE);
            } else {
                // LogManager.WriteLog("promotion","products.get(i).getPrice()  is null or promotion 不合法");
            }
        }
        if (!map.ContainsKey(productBean.skucode)) {
            map.Add(productBean.skucode, productBean);
            mapTripletBean.Add(productBean.skucode, currentPriceTriplet);
        }
        return currentPriceTriplet;
    }



    //果叔会员价格
    private TripletBeanForCoupon getMemberPriceKind(Product productBean)
    {
        decimal newPrice = productBean.memberprice;
        if (MoneyUtils.isBiggerThanZero(newPrice) && MoneyUtils.isFirstBiggerThanSecond(productBean.originprice, newPrice))
        {
            String priceKind = EnumPromotionType.MEMBER_PRICE_ACTION;
            int pricetagid = 1;
            String pricetag = "会员专享";
            DBPROMOTION_CACHE_BEANMODEL dbPromotionCacheBean = new DBPROMOTION_CACHE_BEANMODEL();
            dbPromotionCacheBean.CODE = "-1029924242";
            dbPromotionCacheBean.COSTCENTERINFO = "cvaadf";
            dbPromotionCacheBean.CANBECOMBINED = 1;//.setCanbecombined(true);
            dbPromotionCacheBean.CANMIXCOUPON = 1;// .setCanmixcoupon(true);
            dbPromotionCacheBean.ONLYMEMBER = 1;// .setOnlymember(true);
            return new TripletBeanForCoupon(newPrice, priceKind, dbPromotionCacheBean, pricetagid, pricetag);
        }
        return null;
    }

    //比较两个优惠后价格
    private TripletBeanForCoupon comparePricePair(TripletBeanForCoupon tripletBean, TripletBeanForCoupon tripletBean2) {
        if (tripletBean == null || tripletBean2 != null && MoneyUtils.isFirstBiggerThanOrEqualToSecond(tripletBean.getOriginprice(), tripletBean2.getOriginprice())) {
            return tripletBean2;
        }
        return tripletBean;
    }

    //查找适合的促销对象
    private TripletBeanForCoupon calcPromotionPrice(List<DBPROMOTION_CACHE_BEANMODEL> list, Product productBean){
        if (list==null || list.Count==0) {
            return null;
        }
        PromotionInfoUtils.sortPromotion(list);
        int size = list.Count;
        for (int n = 0; n < size; n++) {
            DBPROMOTION_CACHE_BEANMODEL dbPromotionCacheBean = list[n];
            if (dbPromotionCacheBean != null) {
                bool isEligible = false;
                if (EnumPromotionType.ALWAYS_PASS.Equals(dbPromotionCacheBean.PROMOCONDITIONTYPE)) {
                    if (evaluateSkuScope(dbPromotionCacheBean, productBean)) {
                        // LogManager.WriteLog("promotion","code--single-->" + dbPromotionCacheBean.CODE);
                        isEligible = true;
                    }
                } else if (EnumPromotionType.GOODS_ID_MATCH.Equals(dbPromotionCacheBean.PROMOCONDITIONTYPE)) {
                    if (evaluateSkuScope(dbPromotionCacheBean, productBean)) {
                        if (dbPromotionCacheBean.PROMOCONDITIONCONTEXT.Equals(productBean.skucode)) {
                             //LogManager.WriteLog("promotion","code--single-->" + dbPromotionCacheBean.CODE);
                            isEligible = true;
                        }
                    }
                }
                if (isEligible) {
                    //计算逻辑
                    return getNewPriceAndKind(dbPromotionCacheBean, productBean);
                }
            }
        }

        return null;
    }

    //促销价
    private TripletBeanForCoupon calcPromotionPrice(Product productBean)  {
        List<DBPROMOTION_CACHE_BEANMODEL> list = PromotionCache.getInstance().getList(1, productBean.skucode);//SQliteUtils.getInstance().QueryPromotionBySkucode(productBean.skucode, tenantId, shopId);
//         LogManager.WriteLog("promotion","calcPromotionPrice list  ---->" + list.size());
        return calcPromotionPrice(list, productBean);
    }

    //打折价
    private TripletBeanForCoupon calcPromotionDiscountPrice(Product productBean) {
        List<DBPROMOTION_CACHE_BEANMODEL> list = PromotionCache.getInstance().getList(2, productBean.firstcategoryid);//SQliteUtils.getInstance().QueryPromotionByCategory(productBean.getFirstcategoryid(), tenantId, shopId);
//         LogManager.WriteLog("promotion","calcPromotionDiscountPrice list  ---->" + list.size());
        return calcPromotionPrice(list, productBean);
    }

    //会员价
    private TripletBeanForCoupon calcmemberPrice(Product productBean)  {
        List<DBPROMOTION_CACHE_BEANMODEL> list = PromotionCache.getInstance().getList(3, productBean.skucode);//SQliteUtils.getInstance().QueryPromotionBymember(productBean.skucode, tenantId, shopId);
//         LogManager.WriteLog("promotion","calcmemberPrice--list  ------>" + list.size());
        return calcPromotionPrice(list, productBean);
    }

    //会员折扣价
    private TripletBeanForCoupon calcmemberDiscountPrice(Product productBean) {
        List<DBPROMOTION_CACHE_BEANMODEL> list = PromotionCache.getInstance().getList(4, productBean.firstcategoryid);//SQliteUtils.getInstance().QueryPromotionByMemberDiscount(productBean.getFirstcategoryid(), tenantId, shopId);
//         LogManager.WriteLog("promotion","calcmemberDiscountPrice list  ---->" + list.size());
        return calcPromotionPrice(list, productBean);
    }

    //判断是否符合促销逻辑
    private bool evaluateSkuScope(DBPROMOTION_CACHE_BEANMODEL dbPromotionCacheBean, Product productBean) {
        //// 是否仅会员专享
        //bool isOnlyMemberEvaluate = false;
        //if (!EnumPromotionType.MEMBER_PRICE_ACTION.Equals(dbPromotionCacheBean.PROMOACTION) && !EnumPromotionType.MEMBER_PRICE_DISCOUNT_ACTION.Equals(dbPromotionCacheBean.PROMOACTION) && !EnumPromotionType.MEMBER_GRADE_PRICE_DISCOUNT_ACTION.Equals(dbPromotionCacheBean.PROMOACTION)) {
        //    isOnlyMemberEvaluate = true;
        //}
        //if (isOnlyMemberEvaluate && dbPromotionCacheBean.ONLYMEMBER==1 && !PromotionCache.getInstance().isLoginMember) {
        //    return false;
        //}

        if (!PromotionInfoUtils.isInTimeRange(dbPromotionCacheBean)) {
            return false;
        }
        if (PromotionCache.getInstance().validatePromotionMemberTags(dbPromotionCacheBean)) {
            return false;
        }
        PromotionRealmDetail realmDetail =JsonConvert.DeserializeObject<PromotionRealmDetail>(dbPromotionCacheBean.ELIGIBILITYCONDITION);
        if (realmDetail == null) {
            return true;
        }
        if (dbPromotionCacheBean.ORDERSUBTYPE != null && (8 & dbPromotionCacheBean.ORDERSUBTYPE) == 0) {//8
            return false;
        }
        if (EnumPromotionType.REALM_ALL.Equals(realmDetail.realmType)) {
            if (realmDetail.skuCodesToExclude != null && realmDetail.skuCodesToExclude.Contains(productBean.skucode)) {
                return false;
            } else {
                return true;
            }

        } else if (EnumPromotionType.REALM_GOODS.Equals(realmDetail.realmType)) {
            if (realmDetail.skuCodesToInclude != null && realmDetail.skuCodesToInclude.Contains(productBean.skucode)) {
                return true;
            }
            return false;
        } else if (EnumPromotionType.REALM_CATALOG.Equals(realmDetail.realmType)) {
            if (realmDetail.skuCodesToExclude != null && realmDetail.skuCodesToExclude.Contains(productBean.skucode)) {
                return false;
            }
            if (CollectionUtils.isEmpty(PromotionCache.getCategoryIds(productBean))) {
                return false;
            }
            if (realmDetail.catalogsToInclude != null && !GlobalUtil.IsArrayIntersection(realmDetail.catalogsToInclude,
                    PromotionCache.getCategoryIds(productBean))) {
                return true;
            }
            return false;
        } else if (EnumPromotionType.REALM_MIXED.Equals(realmDetail.realmType)) {
            if (realmDetail.skuCodesToExclude != null && realmDetail.skuCodesToExclude.Count > 0 && realmDetail.skuCodesToExclude.Contains(
                    productBean.skucode)) {
                return false;
            }
            if (CollectionUtils.isNotEmpty(PromotionCache.getCategoryIds(productBean))) {
                if (CollectionUtils.isNotEmpty(realmDetail.catalogsToExclude)
                        && !GlobalUtil.IsArrayIntersection(realmDetail.catalogsToExclude, PromotionCache.getCategoryIds(productBean))) {
                    return false;
                }
                if ((realmDetail.catalogsToInclude != null
                        && !GlobalUtil.IsArrayIntersection(realmDetail.catalogsToInclude, PromotionCache.getCategoryIds(productBean)))) {
                    return true;
                }
            }
            if (CollectionUtils.isNotEmpty(realmDetail.skuCodesToInclude) && realmDetail.skuCodesToInclude.Contains(productBean.skucode)) {
                return true;
            }
            return false;
        }
        return false;
    }

    private TripletBeanForCoupon getNewPriceAndKind(DBPROMOTION_CACHE_BEANMODEL dbPromotionCacheBean, Product productBean) {
        Decimal newPrice = getCalculationPrice(dbPromotionCacheBean, productBean);
        if (MoneyUtils.isFirstBiggerThanSecond(productBean.originprice, newPrice)) {
            String priceKind = EnumPromotionType.PROMOTION_PRICE;
            int pricetagid = -1;
            String pricetag = "";

            if (EnumPromotionType.MEMBER_PRICE_ACTION.Equals(dbPromotionCacheBean.PROMOACTION)) {
                priceKind = EnumPromotionType.MEMBER_PRICE_ACTION;
                pricetagid = 1;
                pricetag = "会员专享";
            } else if (EnumPromotionType.MEMBER_PRICE_DISCOUNT_ACTION.Equals(dbPromotionCacheBean.PROMOACTION)) {
                priceKind = EnumPromotionType.MEMBER_PRICE_DISCOUNT_ACTION;
                pricetagid = 1;
                pricetag = "会员" + MoneyUtils.newMoney(dbPromotionCacheBean.PROMOACTIONCONTEXT).ToString() + "折";
            } else {
                if (EnumPromotionType.PROMOTION_PRICE_DISCOUNT_ACTION.Equals(dbPromotionCacheBean.PROMOACTION)) {//打折
                    priceKind = EnumPromotionType.PROMOTION_DISCOUNT_PRICE;
                }
                if (EnumPromotionType.PROMOTION_PRICE_DISCOUNT_ACTION.Equals(dbPromotionCacheBean.PROMOACTION) ) {
                    pricetag = MoneyUtils.newMoney(dbPromotionCacheBean.PROMOACTIONCONTEXT).ToString() + "折";
                    pricetagid = 2;
                } else {
                    pricetag = "直降";
                    pricetagid = 3;
                }
            }
            if (MoneyUtils.isFirstEqualToSecond(newPrice, productBean.originprice)) {
                priceKind = EnumPromotionType.MARKET_PRICE;
            }
//             LogManager.WriteLog("promotion","newPrice--->" + newPrice);
            return new TripletBeanForCoupon(newPrice, priceKind, dbPromotionCacheBean, pricetagid, pricetag);
        }
        return null;
    }

    //单品计算逻辑
    private Decimal getCalculationPrice(DBPROMOTION_CACHE_BEANMODEL dbPromotionCacheBean, Product productBean){
        if ((EnumPromotionType.PROMOTION_PRICE_DISCOUNT_ACTION.Equals(dbPromotionCacheBean.PROMOACTION) || EnumPromotionType.MEMBER_PRICE_DISCOUNT_ACTION.Equals(dbPromotionCacheBean.PROMOACTION))) {//打折

            Decimal discount = Math.Round(MoneyUtils.newMoney(dbPromotionCacheBean.PROMOACTIONCONTEXT) / 10, 4, MidpointRounding.AwayFromZero);
            Decimal originprice = productBean.price.originprice;
            if (MoneyUtils.isBiggerThanZero(discount) && MoneyUtils.isFirstBiggerThanSecond(Decimal.One, discount)) {
                originprice = MoneyUtils.multiply(originprice, discount);
                return originprice;
            }
            return originprice;
        }
        return MoneyUtils.newMoney(dbPromotionCacheBean.PROMOACTIONCONTEXT);
    }

    public TripletBeanForCoupon calcPaidUpMemberPrice(DBPROMOTION_CACHE_BEANMODEL dbPromotionCacheBean, Product productBean){
        if (evaluateSkuScope(dbPromotionCacheBean, productBean)) {
            if (EnumPromotionType.MEMBER_GRADE_PRICE_DISCOUNT_ACTION.Equals(dbPromotionCacheBean.PROMOACTION)  || EnumPromotionType.PAID_UP_MEMBER_PRICE_DISCOUNT.Equals(dbPromotionCacheBean.PROMOACTION)) {
                int pricetagid = -1;
                String pricetag = "";
                Decimal discount = Math.Round(MoneyUtils.newMoney(dbPromotionCacheBean.PROMOACTIONCONTEXT)/10, 2, MidpointRounding.AwayFromZero);
                Decimal originprice = productBean.price.originprice;

                Decimal newPrice = Decimal.Zero;
                if (MoneyUtils.isBiggerThanZero(discount) && MoneyUtils.isFirstBiggerThanSecond(Decimal.One, discount)) {
                    newPrice = MoneyUtils.multiply(originprice, discount);
                }
                if (MoneyUtils.isBiggerThanZero(newPrice) && MoneyUtils.isFirstBiggerThanOrEqualToSecond(productBean.originprice, newPrice)) {
                    pricetagid = 4;
                    pricetag = "优享会员";
                    String priceKind = EnumPromotionType.MEMBER_GRADE_PRICE;
                    return new TripletBeanForCoupon(newPrice, priceKind, dbPromotionCacheBean, pricetagid, pricetag);
                }
            }
        }
        return null;
    }
}
}
