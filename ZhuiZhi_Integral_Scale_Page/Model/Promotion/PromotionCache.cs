using Maticsoft.BLL;
using Maticsoft.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model.HalfOffLine;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Model.Promotion
{
    public class PromotionCache
    {
         private String tenantId;
    private String shopId;
    private DBPROMOTION_CACHE_BEANBLL promotionbll = new DBPROMOTION_CACHE_BEANBLL();
    private List<DBPROMOTION_CACHE_BEANMODEL> list;
    private List<DBPROMOTION_CACHE_BEANMODEL> listTemp;
    //促销数据是否改变
    private bool isPromotionChange;
    private int listSize = 0;

    private static PromotionCache instance;

    private PromotionConditionUtils promotionConditionUtils;

    public bool isLoginMember;
    public bool isOffline;

    //public bool isEnjoymemberrights;//是否享受会员权益
    //public bool memberRightsEnabled;//是否开通会员权益
    //public MemberrightsItem memberrightsItem;

    public MemberTenantItem membertenantitem;
        /// <summary>
        /// 当前会员标签信息
        /// </summary>
    public static  List<long> listvalidatePromotionMemberTags;

    public static  MemberRightsForGradeBean memberRightsForGradeBean;
    public static  Creditaccountrepvo creditaccountrepvo;

        /// <summary>
        /// 积分规则
        /// </summary>
    public static  TenantCreditConfig tenantCreditConfig = null;//积分规则
       /// <summary>
       /// 优惠券
       /// </summary>
   public static  List<PromotionCoupon> listcoupon = null;//查询出优惠券

   public Paymenttypes availablepaymenttypes;

   public Member memberBean;

    public bool isUsePoint;

       private static object objinstance = new object();
    public static PromotionCache getInstance() {
        if (instance == null) {
            lock(objinstance){
                
                if (instance == null) {
                    instance = new PromotionCache();
            }


            }
        }
        return instance;

       // return null;
    }

    public void init(String tenantId, String shopId) {
        this.tenantId = tenantId;
        this.shopId = shopId;
        if (promotionConditionUtils != null) {
            promotionConditionUtils = null;
        }
        promotionConditionUtils = new PromotionConditionUtils(tenantId, shopId);
        isOffline = MainModel.IsOffLine;

        long currentTime = Convert.ToInt64(MainModel.getStampByDateTime(DateTime.Now));
                string strwhere = " TENANTID='" + tenantId + "' and SHOPID ='" + shopId  + "' and ENABLED =1  and CREATE_URL_IP ='" + MainModel.URL + "' and ENABLEDFROM <" + currentTime + " and ENABLEDTO >" + currentTime;
        if(MainModel.IsOffLine){
            strwhere+="and (( PROMOTYPE = '" + EnumPromotionType.ITEM + "' AND ( PROMOACTION = '" + EnumPromotionType.PROMOTION_PRICE_ACTION + "' OR PROMOACTION = '" + EnumPromotionType.PROMOTION_PRICE_DISCOUNT_ACTION + "')) OR PROMOTYPE = '" + EnumPromotionType.ORDER + "')";
        }
        list=promotionbll.GetModelList(strwhere);


            availablepaymenttypes =HalfOffLineUtil.paymenttypes;//  GsonUtils.gsonToBean(json, Availablepaymenttypes.class);

    }

    //初始化  单品之前就调用
    public void onCreate(String tenantId, String shopId) {


        try
        {
            string ischange = INIManager.GetIni("MQTT", "IsChange", MainModel.IniPath);
            if (ischange == "1")
            {
                refreshPromotion();
                INIManager.SetIni("MQTT", "IsChange", "0", MainModel.IniPath);
            }
        }
        catch (Exception ex)
        {
            LogManager.WriteLog("promotion","判断促销更新异常"+ex.Message);
        }
        if (creditaccountrepvo != null) {
            creditaccountrepvo = null;
        }
        if (memberRightsForGradeBean != null) {
            memberRightsForGradeBean = null;
        }
        if (listvalidatePromotionMemberTags != null) {
            listvalidatePromotionMemberTags = null;
        }
        if (listcoupon != null) {
            listcoupon = null;
        }
        if (tenantCreditConfig != null) {
            tenantCreditConfig = null;
        }
        if (membertenantitem != null)
        {
            membertenantitem = null;
        }
        this.tenantId = tenantId;
        this.shopId = shopId;

        isLoginMember = false;
        isUsePoint = false;

        isOffline = MainModel.IsOffLine;
       // LogManager.WriteLog("promotion","isPromotionChange-->" + isPromotionChange);
        if (isPromotionChange) {
            if (list != null) {
                list.Clear();
                list = null;
            }
            list = listTemp;
            if (list==null || list.Count==0) {
                        long currentTime = Convert.ToInt64(MainModel.getStampByDateTime(DateTime.Now));
                string strwhere = " TENANTID='" + tenantId + "' and SHOPID ='" + shopId  + "' and ENABLED =1  and CREATE_URL_IP ='" + MainModel.URL + "' and ENABLEDFROM <" + currentTime + " and ENABLEDTO >" + currentTime;
        if(MainModel.IsOffLine){
            strwhere+="and (( PROMOTYPE = '" + EnumPromotionType.ITEM + "' AND ( PROMOACTION = '" + EnumPromotionType.PROMOTION_PRICE_ACTION + "' OR PROMOACTION = '" + EnumPromotionType.PROMOTION_PRICE_DISCOUNT_ACTION + "')) OR PROMOTYPE = '" + EnumPromotionType.ORDER + "')";
        }
        list=promotionbll.GetModelList(strwhere);
            }
            isPromotionChange = false;
        }
        listSize = list == null ? 0 : list.Count;
        //LogManager.WriteLog("promotion","可用的促销个数-->" + listSize);


        if(MainModel.CurrentMember!=null){
            Member memberBean = MainModel.CurrentMember;
            isLoginMember=true;
        }
        else
        {
            isLoginMember = false;
        }
        
         
        if (isLoginMember) {

           listvalidatePromotionMemberTags = HalfOffLineUtil.listvalidatePromotionMemberTags;

            setMemberRightsForGradeBean();
            setCreditaccountrepvo();
            isUsePoint = MainModel.CurrentMember.isUsePoint;
            tenantCreditConfig = HalfOffLineUtil.tenantCreditConfig;
            listcoupon = HalfOffLineUtil.listcoupon;


            membertenantitem = HalfOffLineUtil.membertenantitem;
            //isEnjoymemberrights=HalfOffLineUtil.enjoymemberrights;
           
            //memberrightsItem = HalfOffLineUtil.memberrightsitem;
                   
        }
    }

    public DBPROMOTION_CACHE_BEANMODEL getMemberRightsDiscountPricePromo(MemberrightsItem memberrightsItem)
    {
        if (memberrightsItem == null)
        {
            return null;
        }
        Tenantmemberrightsdiscountconfig tenantmemberrightsdiscountconfig = memberrightsItem.tenantmemberrightsdiscountconfig;
        if (tenantmemberrightsdiscountconfig == null)
        {
            return null;
        }
        DBPROMOTION_CACHE_BEANMODEL dbPromotionCacheBean = new DBPROMOTION_CACHE_BEANMODEL();
        formatMemberRightsPromoCommonParams(dbPromotionCacheBean);
        dbPromotionCacheBean.NAME="付费会员折扣活动";
        dbPromotionCacheBean.PROMOTYPE= EnumPromotionType.ITEM;
        dbPromotionCacheBean.PROMOSUBTYPE="";// .setPromosubtype("");
        dbPromotionCacheBean.PROMOCONDITIONTYPE=EnumPromotionType.ALWAYS_PASS;
        dbPromotionCacheBean.CANBECOMBINED=tenantmemberrightsdiscountconfig.unionpromotion ? 1:0;
        dbPromotionCacheBean._id=EnumPromotionType.PAID_UP_MEMBER_RIGHTS_DISCOUNT_PROMO_ID;
        dbPromotionCacheBean.CODE="member_rights_discount_price_001";
        dbPromotionCacheBean.PROMOACTION=EnumPromotionType.PAID_UP_MEMBER_PRICE_DISCOUNT;
        PromotionRealmDetail realmDetail = new PromotionRealmDetail();
        realmDetail.realmType=tenantmemberrightsdiscountconfig.realmtype; 
        realmDetail.catalogsToExclude=tenantmemberrightsdiscountconfig.catalogstoexclude;
        realmDetail.catalogsToInclude=tenantmemberrightsdiscountconfig.catalogstoinclude;
        realmDetail.skuCodesToExclude =tenantmemberrightsdiscountconfig.skucodestoexclude;
        realmDetail.skuCodesToInclude =tenantmemberrightsdiscountconfig.skucodestoinclude;
        dbPromotionCacheBean.ELIGIBILITYCONDITION = JsonConvert.SerializeObject(realmDetail); 
        dbPromotionCacheBean.PROMOCONDITIONCONTEXT="";// .setPromoconditioncontext("");
        dbPromotionCacheBean.PROMOACTIONCONTEXT=tenantmemberrightsdiscountconfig.discount + "";
        return dbPromotionCacheBean;
    }


    //自造会员等级折扣活动促销
    private void setMemberRightsForGradeBean() {
        memberRightsForGradeBean = new MemberRightsForGradeBean();
        MemberrightsItem memberrightsItem = null;
        bool isEnjoymemberrights = false;//是否享受会员权益
        bool memberRightsEnabled = false;//是否开通会员权益
        //会员是否能享受会员权益
      isEnjoymemberrights=HalfOffLineUtil.enjoymemberrights;
        //会员权益配置获取
     memberrightsItem=HalfOffLineUtil.memberrightsitem;

        if(HalfOffLineUtil.memberrightsitem !=null){
            memberRightsEnabled = HalfOffLineUtil.memberrightsitem.memberrightsfunctionenable;
        }

        if (memberRightsEnabled) {
            if (isEnjoymemberrights)
            {
            memberRightsForGradeBean.setMemberGradeDiscountPricePromo(getMemberRightsDiscountPricePromo(memberrightsItem));
            memberRightsForGradeBean.setGradeMember(true);
            }
        } else {
//获取会员等级商户设置

             MemberTenantmembergradeconfig memberTenantmembergradeconfig =HalfOffLineUtil.membertenantmembergradeconfig;
            if (memberTenantmembergradeconfig!=null) {


                if (memberTenantmembergradeconfig != null && memberTenantmembergradeconfig.enable && isLoginMember) {
                    //当前会员等级
                    Gradesettinggetgrade gradesettinggetgrade = HalfOffLineUtil.gradesettinggetgrade;
                    if (gradesettinggetgrade!=null) {

                        if (gradesettinggetgrade != null && gradesettinggetgrade.enable) {
                            memberRightsForGradeBean.setGradeMember(true);
                            if (gradesettinggetgrade.discount != null) {
                                DBPROMOTION_CACHE_BEANMODEL dbPromotionCacheBean = new DBPROMOTION_CACHE_BEANMODEL();

                                formatMemberRightsPromoCommonParams(dbPromotionCacheBean);

                                dbPromotionCacheBean.NAME= "会员等级折扣活动:" + gradesettinggetgrade.name;
                                dbPromotionCacheBean.PROMOTYPE= EnumPromotionType.ITEM;
                                dbPromotionCacheBean.PROMOSUBTYPE="";// .setPromosubtype("");
                                dbPromotionCacheBean.PROMOCONDITIONTYPE=EnumPromotionType.ALWAYS_PASS;
                                //此处的是否可叠加理解为能否与当前的促销价或会员价叠加
                                dbPromotionCacheBean.CANBECOMBINED = gradesettinggetgrade.discount.unionpromotion ? 1:0;// .setCanbecombined(gradesettinggetgrade.getDiscount().isUnionpromotion());

                                dbPromotionCacheBean._id=EnumPromotionType.MEMBER_GRADE_DISCOUNT_PROMO_ID;
                                dbPromotionCacheBean.CODE= "member_grade_rights_discount_price_001";
                                dbPromotionCacheBean.PROMOACTION=EnumPromotionType.MEMBER_GRADE_PRICE_DISCOUNT_ACTION;
                                PromotionRealmDetail realmDetail = new PromotionRealmDetail();
                                realmDetail.realmType = gradesettinggetgrade.discount.realmtype;// .setRealmType(gradesettinggetgrade.getDiscount().getRealmtype());
                                realmDetail.catalogsToExclude = gradesettinggetgrade.discount.catalogstoexclude;//.setCatalogsToExclude(gradesettinggetgrade.getDiscount().getCatalogstoexclude());
                                realmDetail.catalogsToInclude = gradesettinggetgrade.discount.catalogstoinclude;// .setCatalogsToInclude(gradesettinggetgrade.getDiscount().getCatalogstoinclude());
                                realmDetail.skuCodesToExclude = gradesettinggetgrade.discount.skucodestoexclude;//.setSkuCodesToExclude(gradesettinggetgrade.getDiscount().getSkucodestoexclude());
                                realmDetail.skuCodesToInclude = gradesettinggetgrade.discount.skucodestoinclude;//.setSkuCodesToInclude(gradesettinggetgrade.getDiscount().getSkucodestoinclude());
                                dbPromotionCacheBean.ELIGIBILITYCONDITION = JsonConvert.SerializeObject(realmDetail);//.setEligibilitycondition(gson.toJson(realmDetail));
                                dbPromotionCacheBean.PROMOCONDITIONCONTEXT = "";//.setPromoconditioncontext("");
                                dbPromotionCacheBean.PROMOACTIONCONTEXT= gradesettinggetgrade.discount.discount + "";
                                dbPromotionCacheBean.TAG = gradesettinggetgrade.name;//.setTag(gradesettinggetgrade.getName());

                                memberRightsForGradeBean.setMemberGradeDiscountPricePromo(dbPromotionCacheBean);
//                                }
//                            }
                            }
                        }
                    }
                }
            }
        }
    }



   private void formatMemberRightsPromoCommonParams(DBPROMOTION_CACHE_BEANMODEL dbPromotionCacheBean)
   {
       dbPromotionCacheBean.TENANTID = tenantId;//.setTenantid(tenantId);
       dbPromotionCacheBean.SHOPID=shopId;//.setShopid(shopId);
       dbPromotionCacheBean.ENABLED=1;// .setEnabled(true);
       dbPromotionCacheBean.FROMOUTER=0;// .setFromouter(false);
       dbPromotionCacheBean.CANMIXCOUPON=1;// .setCanmixcoupon(true);
       dbPromotionCacheBean.CREATEDAT = Convert.ToInt64(MainModel.getStampByDateTime(Convert.ToDateTime("2019-01-01 01:01:00")));// .setCreatedat(TimeUtils.getTime("2019-01-01 01:01:00"));
       dbPromotionCacheBean.UPDATEDAT = Convert.ToInt64(MainModel.getStampByDateTime(Convert.ToDateTime("2019-01-01 01:01:00")));//setUpdatedat(TimeUtils.getTime("2019-01-01 01:01:00"));
       dbPromotionCacheBean.ENABLEDFROM = Convert.ToInt64(MainModel.getStampByDateTime(Convert.ToDateTime("2019-01-01 00:00:00")));// setEnabledfrom(TimeUtils.getTime("2019-01-01 00:00:00"));
       dbPromotionCacheBean.ENABLEDTO = Convert.ToInt64(MainModel.getStampByDateTime(Convert.ToDateTime("2019-12-31 00:00:00")));// .setEnabledto(TimeUtils.getTime("2019-12-31 00:00:00"));
       dbPromotionCacheBean.CREATEDBY = "System"; //setCreatedby("System");
       dbPromotionCacheBean.UPDATEDBY = "System";// .setUpdatedby("System");
       dbPromotionCacheBean.TAG = "";// setTag("");
       dbPromotionCacheBean.DESCRIPTION = "";// . setDescription("");
       dbPromotionCacheBean.OUTERCODE = "";// .setOutercode("");
       dbPromotionCacheBean.AVAILABLECATEGORY = "";// .setAvailablecategory("");
       dbPromotionCacheBean.RANK = 500;// .setRank(500);
       dbPromotionCacheBean.DISTRICTSCOPE = "";// .setDistrictscope("");
       dbPromotionCacheBean.SHOPSCOPE = ""; //.setShopscope("");
       dbPromotionCacheBean.ENABLEDTIMEINFO = "";// .setEnabledtimeinfo("");
       dbPromotionCacheBean.COSTCENTERINFO = "{\"shop\":1.00}";// .setCostcenterinfo("{\"shop\":1.00}");
       dbPromotionCacheBean.COSTRULECONTEXT = "";// .setCostrulecontext("");
       dbPromotionCacheBean.SALECHANNEL = 8;// .setSalechannel(8);
       dbPromotionCacheBean.ORDERSUBTYPE = 8;// .setOrdersubtype(8);
   }


            // //标识是否是会员等级，用于商详区分提示
    public MemberRightsForGradeBean getMemberRightsForGradeBean() {
        return memberRightsForGradeBean;
    }

    public void setCreditaccountrepvo() {

        if(MainModel.CurrentMember!=null){
            creditaccountrepvo=MainModel.CurrentMember.creditaccountrepvo;
        } 
    }

    //促销变化时
    public void refreshPromotion() {
        if (listTemp != null) {
            listTemp.Clear();
            listTemp = null;
        }
        isPromotionChange = true;
 long currentTime = Convert.ToInt64(MainModel.getStampByDateTime(DateTime.Now));
                string strwhere = " TENANTID='" + tenantId + "' and SHOPID ='" + shopId  + "' and ENABLED =1  and CREATE_URL_IP ='" + MainModel.URL + "' and ENABLEDFROM <" + currentTime + " and ENABLEDTO >" + currentTime;
        if(MainModel.IsOffLine){
            strwhere+="and (( PROMOTYPE = '" + EnumPromotionType.ITEM + "' AND ( PROMOACTION = '" + EnumPromotionType.PROMOTION_PRICE_ACTION + "' OR PROMOACTION = '" + EnumPromotionType.PROMOTION_PRICE_DISCOUNT_ACTION + "')) OR PROMOTYPE = '" + EnumPromotionType.ORDER + "')";
        }
        listTemp=promotionbll.GetModelList(strwhere);   
    }

    /**
     * @param flag 1:单品促销直降 2：单品打折 3:会员 4：会员折扣 5:指定code 6：订单级别促销
     * @return 每次从缓存里面拿出来再过滤掉想要的 比从数据库查询快多了
     */
    public List<DBPROMOTION_CACHE_BEANMODEL> getList(int flag, String condition) {
        List<DBPROMOTION_CACHE_BEANMODEL> newList = new List<DBPROMOTION_CACHE_BEANMODEL>();
        try {
            if (list==null || list.Count==0) {
                  long currentTime = Convert.ToInt64(MainModel.getStampByDateTime(DateTime.Now));
                string strwhere = " TENANTID='" + tenantId + "' and SHOPID ='" + shopId  + "' and ENABLED =1  and CREATE_URL_IP ='" + MainModel.URL + "' and ENABLEDFROM <" + currentTime + " and ENABLEDTO >" + currentTime;
        if(MainModel.IsOffLine){
            strwhere+="and (( PROMOTYPE = '" + EnumPromotionType.ITEM + "' AND ( PROMOACTION = '" + EnumPromotionType.PROMOTION_PRICE_ACTION + "' OR PROMOACTION = '" + EnumPromotionType.PROMOTION_PRICE_DISCOUNT_ACTION + "')) OR PROMOTYPE = '" + EnumPromotionType.ORDER + "')";
        }
        list=promotionbll.GetModelList(strwhere);
            }
            if (list==null || list.Count==0) {
                return newList;
            }
            for (int i = 0; i < listSize; i++) {
                if (flag == 1) {//促销直降
                    if (EnumPromotionType.ITEM.Equals(list[i].PROMOTYPE) && !string.IsNullOrEmpty(condition) && condition.Equals(list[i].PROMOCONDITIONCONTEXT) && EnumPromotionType.PROMOTION_PRICE_ACTION.Equals(list[i].PROMOACTION)) {
                        newList.Add(list[i]);
                    }
                } else if (flag == 2) {//打折
                    if (EnumPromotionType.ITEM.Equals(list[i].PROMOTYPE) && !string.IsNullOrEmpty(list[i].AVAILABLECATEGORY) && list[i].AVAILABLECATEGORY.Contains(condition) && EnumPromotionType.PROMOTION_PRICE_DISCOUNT_ACTION.Equals(list[i].PROMOACTION)) {
                        newList.Add(list[i]);
                    }
                } else if (flag == 3) {//:会员

                   
                    if (EnumPromotionType.ITEM.Equals(list[i].PROMOTYPE) && !string.IsNullOrEmpty(condition) && condition.Equals(list[i].PROMOCONDITIONCONTEXT) && EnumPromotionType.MEMBER_PRICE_ACTION.Equals(list[i].PROMOACTION)) {
                        newList.Add(list[i]);
                    }
                } else if (flag == 4) {//会员折扣
                    if (EnumPromotionType.ITEM.Equals(list[i].PROMOTYPE) && EnumPromotionType.MEMBER_PRICE_DISCOUNT_ACTION.Equals(list[i].PROMOACTION) && !string.IsNullOrEmpty(list[i].AVAILABLECATEGORY) && list[i].AVAILABLECATEGORY.Contains(condition)) {
                        newList.Add(list[i]);
                    }
                } else if (flag == 5) {//指定code
                    if (!string.IsNullOrEmpty(condition) && condition.Equals(list[i].CODE)) {
                        newList.Add(list[i]);
                        break;
                    }
                } else if (flag == 6) {//订单级别促销
                    if (!string.IsNullOrEmpty(condition) && condition.Equals(list[i].PROMOTYPE)) {
                        if (!EnumPromotionType.PAYMENT_REDUCE.Equals(list[i].PROMOACTION))
                        {
                            newList.Add(list[i]);

                        }
                    }
                }
                else if (flag == 7)
                {//支付立减促销
                    if (!string.IsNullOrEmpty(condition) && condition.Equals(list[i].PROMOACTION))
                    {
                        newList.Add(list[i]);
                    }
                }
            }
        } catch (Exception e) {
            LogManager.WriteLog("promotion",e.Message);
        }
        return newList;
    }

    public PromotionConditionUtils getPromotionConditionUtils() {
        return promotionConditionUtils;
    }


    public bool validatePromotionMemberTags(DBPROMOTION_CACHE_BEANMODEL promotion) {
        if (!string.IsNullOrEmpty(promotion.MEMBERTAGS)) {
            String[] split = promotion.MEMBERTAGS.Split(',');
            List<String> promoMemberTags = new List<string>(split); //Arrays.asList(split);// Lists.newArrayList(split);
            if (listvalidatePromotionMemberTags == null || listvalidatePromotionMemberTags.Count == 0) {
                return true;
            }
            List<String> collect = new List<string>();
            for (int i = 0; i < listvalidatePromotionMemberTags.Count; i++) {
                collect.Add(listvalidatePromotionMemberTags[i] + "");
            }


            if (GlobalUtil.IsArrayIntersection(promoMemberTags,collect))
            {
                return true;
            }
        }
        return false;
    }



    public TenantCreditConfig getTenantCreditConfig() {
        return tenantCreditConfig;
    }

    public List<PromotionCoupon> getListcoupon() {
        return listcoupon;
    }

    //支付成功或失败，合适的地方释放掉内存上面的数据
    public  void onDestory() {
        if (list != null) {
            list.Clear();
            list = null;
        }
        if (creditaccountrepvo != null) {
            creditaccountrepvo = null;
        }
        if (memberRightsForGradeBean != null) {
            memberRightsForGradeBean = null;
        }
        if (listvalidatePromotionMemberTags != null) {
            listvalidatePromotionMemberTags = null;
        }
        if (listcoupon != null) {
            listcoupon = null;
        }
        if (tenantCreditConfig != null) {
            tenantCreditConfig = null;
        }

        if (listTemp != null) {
            listTemp.Clear();
            listTemp = null;
        }

        if (membertenantitem != null)
        {
            membertenantitem = null;
        }
        if (availablepaymenttypes != null)
        {
            availablepaymenttypes = null;
        }
        isPromotionChange = true;
    }


        public static List<string> getCategoryIds(Product pro)
        {
            try
            {
                List<string> lstresult = new List<string>();
                if (!string.IsNullOrEmpty(pro.firstcategoryid))
                {
                    lstresult.Add(pro.firstcategoryid);
                }

                if (!string.IsNullOrEmpty(pro.secondcategoryid))
                {
                    lstresult.Add(pro.secondcategoryid);
                }

                if(!string.IsNullOrEmpty(pro.categoryid)){
                    lstresult.Add(pro.categoryid);
                }
                return lstresult;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("promotion","getCategoryIds 异常"+ex.Message);
                return null;
            }
        }
    }
}
