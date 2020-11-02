using Maticsoft.BLL;
using Maticsoft.Model;
using Newtonsoft.Json;
using QDAMAPOS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QDAMAPOS.Model.Promotion
{
    public class ImplOfflineOrderPromotion{
        private String tenantId;
        private String shopId;

        private DBPROMOTION_CACHE_BEANBLL promotionbll = new DBPROMOTION_CACHE_BEANBLL();

        private List<String> NIGHT_TIMED_PROMO_CODES = new List<string>(){"201804280012", "201804280013", "201804280014", "201804280015",
            "201804280016", "201804280017", "201804280018", "201804280019", "201804280020", "201804280021"};
        private List<String> list = new List<string>();

        public ImplOfflineOrderPromotion(String tenantId, String shopId)
        {
            this.tenantId = tenantId;
            this.shopId = shopId;
            list.Add("90");
            list.Add("91");
            list.Add("05");
            list.Add("99");
        }

        //查找适合的促销对象
        public void doAction(List<Product> products)
        {
            if (products != null && products.Count > 0)
            {
                //ZzLog.e("products--json--->" + GsonUtils.beanToJson(products));

                //ADD 20200229  否则一个商品满足优惠  新增商品计算分摊会出错

                for (int i = 0; i < products.Count; i++)
                {
                    products[i].PaySubAmt = products[i].price.total;
                    products[i].RowNum = i;
                    
                }
                   
                List<Product> allProducts = new List<Product>();
                allProducts.AddRange(products);

                List<DBPROMOTION_CACHE_BEANMODEL> promotiolist = queryOrderPromotion(tenantId, shopId);
                if (promotiolist != null && promotiolist.Count > 0)
                {
                    // ZzLog.e("promotiolist  ---->" + promotiolist.Count);
                    PromotionInfoUtils.sortPromotion(promotiolist);
                    calcPromotionPrice(promotiolist, allProducts);
                }
            }
        }

        //查询订单级别的促销
        public List<DBPROMOTION_CACHE_BEANMODEL> queryOrderPromotion( String tenantId, String shopId)
        {


            long currentTime = Convert.ToInt64(MainModel.getStampByDateTime(DateTime.Now));

            string strwhere = " PROMOTYPE ='" + EnumPromotionType.PromotionType_ORDER + "' and TENANTID='" + tenantId + "' and SHOPID ='" + shopId + "' and ENABLED =1  and CREATE_URL_IP ='" + MainModel.URL + "' and ENABLEDFROM <" + currentTime + " and ENABLEDTO >" + currentTime;

            List<DBPROMOTION_CACHE_BEANMODEL> lstpro = promotionbll.GetModelList(strwhere);

            return lstpro;
        }
      

////    //为了测试单个促销 排除已经测试过的,做完上线一定要去掉
////    private bool filter(DBPROMOTION_CACHE_BEANMODEL DBPROMOTION_CACHE_BEANMODEL) {
////        //package.selling
////        if ("200379724055191552".equals(DBPROMOTION_CACHE_BEANMODEL.getCode())) {
////            return false;
////        }
////        //cycle.designated.discount.off
////        if ("200379473680408576".equals(DBPROMOTION_CACHE_BEANMODEL.getCode())) {
////            return false;
////        }
////        //designated.discount.off
////        if ("200379178195886080".equals(DBPROMOTION_CACHE_BEANMODEL.getCode())) {
////            return false;
////        }
////
////
//////step.discount.off
////        if ("200378607153979392".equals(DBPROMOTION_CACHE_BEANMODEL.getCode())) {
////            return false;
////        }
//////        if ("200378138197237760".equals(DBPROMOTION_CACHE_BEANMODEL.getCode())) {
//////            // //满额
//////            return false;
//////        }
////
//////discount
////        if ("200377529683419136".equals(DBPROMOTION_CACHE_BEANMODEL.getCode())) {
////            return false;
////        }
////        if ("200377325261430784".equals(DBPROMOTION_CACHE_BEANMODEL.getCode())) {
////            return false;
////        }
////        if ("200376988521734144".equals(DBPROMOTION_CACHE_BEANMODEL.getCode())) {
////            return false;
////        }
////
////        //step.amount.off
////        if ("200376588292857856".equals(DBPROMOTION_CACHE_BEANMODEL.getCode())) {
////            return false;
////        }
////        if ("200376246868123648".equals(DBPROMOTION_CACHE_BEANMODEL.getCode())) {
////            //满额
////            return false;
////        }
////
////        //dynamic.amount.off
////        if ("200375570326888448".equals(DBPROMOTION_CACHE_BEANMODEL.getCode())) {
////            return false;
////        }
////        if ("200375359676358656".equals(DBPROMOTION_CACHE_BEANMODEL.getCode())) {
////            //满额
////            return false;
////        }
////        //fix.amount.off
////        //满件
////        if ("200371992208089088".equals(DBPROMOTION_CACHE_BEANMODEL.getCode())) {
////            return false;
////        }
////        if ("200371697570816000".equals(DBPROMOTION_CACHE_BEANMODEL.getCode())) {
////            //满额
////            return false;
////        }
////        if ("200304156848693248".equals(DBPROMOTION_CACHE_BEANMODEL.getCode())) {
////            //满额
////            return false;
////        }
////        if ("200360286987755520".equals(DBPROMOTION_CACHE_BEANMODEL.getCode())) {
////            //满额
////            return false;
////        }
////        return true;
////    }


     
    //遍历适合的促销对象
    public void calcPromotionPrice(List<DBPROMOTION_CACHE_BEANMODEL> list, List<Product> allProducts)  {
        for (int n = 0; n < list.Count && allProducts.Count > 0; n++) {
            DBPROMOTION_CACHE_BEANMODEL DBPROMOTION_CACHE_BEANMODEL = list[n];
//            if (DBPROMOTION_CACHE_BEANMODEL != null && filter(DBPROMOTION_CACHE_BEANMODEL)) {
            if (DBPROMOTION_CACHE_BEANMODEL != null) {
                EvaluateScopePromotion evaluateScopePromotion = new EvaluateScopePromotion();
                if (evaluateScope(DBPROMOTION_CACHE_BEANMODEL, allProducts, evaluateScopePromotion)) {

                    //ZzLog.e("getCode  ---->" + DBPROMOTION_CACHE_BEANMODEL.getCode());

                    // evaluateScopePromotion.getList() 传入满足的list 商品
                    bool isEligible = false;
                    //判断是件数条件判断，走件数判断逻辑
                    if (EnumPromotionType.ITEM_COUNT_THRESHOLD.Equals(DBPROMOTION_CACHE_BEANMODEL.PROMOCONDITIONTYPE)) {
                        isEligible =  ItemCountThresholdevaluate(DBPROMOTION_CACHE_BEANMODEL, evaluateScopePromotion.getList(), evaluateScopePromotion);
                    } else if (EnumPromotionType.ORDER_AMOUNT_THRESHOLD.Equals(DBPROMOTION_CACHE_BEANMODEL.PROMOCONDITIONTYPE)) {
                        //判断是金额条件判断，走金额判断逻辑
                        isEligible = OrderAmountThresholdvaluate(DBPROMOTION_CACHE_BEANMODEL, evaluateScopePromotion.getList(), evaluateScopePromotion);
                    } else {
                        isEligible = true;
                    }
                    //如果满足了，进行促销计算
                    if (isEligible) {
                        if (PromotionContextConvertUtils.isSingleLinePromotion(DBPROMOTION_CACHE_BEANMODEL)) {
                            //特殊处理单行优惠
                            SingleLinePromotioncalculate(DBPROMOTION_CACHE_BEANMODEL, evaluateScopePromotion.getList());
                        } else {
                            promactioncalculate(DBPROMOTION_CACHE_BEANMODEL, evaluateScopePromotion.getList(), evaluateScopePromotion);
                        }
                    }
                    //剔除商品
                    if (evaluateScopePromotion.getList() != null && evaluateScopePromotion.getList().Count > 0) {
                        //剔除满足的list商品
                        List<Product> lstpro = evaluateScopePromotion.getList(); 
                        foreach(Product pro in lstpro){
                            allProducts.Remove(pro);
                        }
                        //allProducts.RemoveAll(evaluateScopePromotion.getList());
                    }

                   // ZzLog.e("allProducts  after remove---->" + allProducts.Count);
                }
            }
        }
    }
                //商品件数判断

                public bool ItemCountThresholdevaluate(DBPROMOTION_CACHE_BEANMODEL DBPROMOTION_CACHE_BEANMODEL, List<Product> products, EvaluateScopePromotion cartBean)
        {
            try
            {
                Decimal threshold = Convert.ToDecimal(DBPROMOTION_CACHE_BEANMODEL.PROMOCONDITIONCONTEXT );

                Decimal totalQty = Convert.ToDecimal(cartBean.getPromotionItemTotalCount() );
                bool meetItemCount = MoneyUtils.isFirstBiggerThanOrEqualToSecond(totalQty, threshold);
                return meetItemCount;
            }
            catch (Exception ex)
            {
                //ex.printStackTrace();
                return false;
            }
        }
            //商品金额判断
    public bool OrderAmountThresholdvaluate(DBPROMOTION_CACHE_BEANMODEL DBPROMOTION_CACHE_BEANMODEL, List<Product> products, EvaluateScopePromotion evaluateScopePromotion) {
        try {
           Decimal threshold =Convert.ToDecimal(DBPROMOTION_CACHE_BEANMODEL.PROMOCONDITIONCONTEXT );
           Decimal scopeSpAmt =Convert.ToDecimal(evaluateScopePromotion.getPromotionItemTotalPayAmt() );
            return MoneyUtils.isFirstBiggerThanOrEqualToSecond(scopeSpAmt,
                    threshold);
        } catch (Exception ex) {
            //ex.printStackTrace();
            return false;
        }
    }


            //根据不同的促销方式进行计算  //特殊处理单行优惠
        private void SingleLinePromotioncalculate(DBPROMOTION_CACHE_BEANMODEL DBPROMOTION_CACHE_BEANMODEL, List<Product> products) {
        if (products != null && products.Count > 0) {
            for (int i = 0; i < products.Count; i++) {
                List<Product> newproducts = new  List<Model.Product> ();
                newproducts.Add(products[i]);
                EvaluateScopePromotion evaluateScopePromotion = new EvaluateScopePromotion();

                if (evaluateScope(DBPROMOTION_CACHE_BEANMODEL, newproducts, evaluateScopePromotion)) {
                    bool isEligible = false;
                    //判断是件数条件判断，走件数判断逻辑
                    if (EnumPromotionType.ITEM_COUNT_THRESHOLD.Equals(DBPROMOTION_CACHE_BEANMODEL.PROMOCONDITIONTYPE)) {
                        isEligible = ItemCountThresholdevaluate(DBPROMOTION_CACHE_BEANMODEL, newproducts, evaluateScopePromotion);
                    } else if (EnumPromotionType.ORDER_AMOUNT_THRESHOLD.Equals(DBPROMOTION_CACHE_BEANMODEL.PROMOCONDITIONTYPE)) {
                        //判断是金额条件判断，走金额判断逻辑
                        isEligible = OrderAmountThresholdvaluate(DBPROMOTION_CACHE_BEANMODEL, newproducts, evaluateScopePromotion);
                    } else {
                        isEligible = true;
                    }
                    if (isEligible) {
                        promactioncalculate(DBPROMOTION_CACHE_BEANMODEL, newproducts, evaluateScopePromotion);
                    }
                }
            }
        }
    }

            //根据不同的促销方式进行计算
    private void promactioncalculate(DBPROMOTION_CACHE_BEANMODEL DBPROMOTION_CACHE_BEANMODEL, List<Product> products, EvaluateScopePromotion evaluateScopePromotion) {
        PromoActionFactory promoActionFactory = new PromoActionFactory(DBPROMOTION_CACHE_BEANMODEL);
        promoActionFactory.perform(DBPROMOTION_CACHE_BEANMODEL, products, evaluateScopePromotion);
    }


    // 基础类，在单品基础上要加上订单类型判断，订单金额汇总，可用商品行标识，件数汇总等
    public bool evaluateScope(DBPROMOTION_CACHE_BEANMODEL DBPROMOTION_CACHE_BEANMODEL, List<Product> products, EvaluateScopePromotion evaluateScopePromotion) {
       Decimal promotionItemTotalAmt =0;
       Decimal promotionItemTotalCount =0;
       Decimal promotionItemTotalPayAmt =0;
       // ZzLog.e(DBPROMOTION_CACHE_BEANMODEL.getCode() + "--" + DBPROMOTION_CACHE_BEANMODEL.getEligibilitycondition());
        if (!PromotionInfoUtils.isInTimeRange(DBPROMOTION_CACHE_BEANMODEL)) {
            return false;
        }
        if (DBPROMOTION_CACHE_BEANMODEL.ORDERSUBTYPE != null && (8 & DBPROMOTION_CACHE_BEANMODEL.ORDERSUBTYPE) == 0) {//8
            return false;
        }

//        Set<Integer> couponUsageExcludeRows = Sets.newHashSet();
//        List<Integer> listScope = new ArrayList<>();
        List<Product> list = new List<Model.Product>();
        PromotionRealmDetail realmDetail =JsonConvert.DeserializeObject<PromotionRealmDetail>(DBPROMOTION_CACHE_BEANMODEL.ELIGIBILITYCONDITION);
        bool eligible = false;
        if (realmDetail == null) {
            if (products != null && products.Count > 0) {
                for (int i = 0; i < products.Count; i++) {
                    Product Product = products[i];
                    if (Product != null) {
                        if (isEligibleRow(DBPROMOTION_CACHE_BEANMODEL, products[i])) {
                            list.Add(Product);
//                            listScope.add(i);//我给商品标记的第几行
                            promotionItemTotalAmt = MoneyUtils.add(promotionItemTotalAmt, Product.price.total);//售价合计
                            promotionItemTotalPayAmt = MoneyUtils.add(promotionItemTotalPayAmt, Product.PaySubAmt);//售价-已享受促销之后的应付金额
                            promotionItemTotalCount = MoneyUtils.add(promotionItemTotalCount,Product.num);//购买的份数
                            eligible = true;
                        }
                    }
                }
//                cartBean.setListScope(listScope); //适用的商品行号
                evaluateScopePromotion.setList(list);
                evaluateScopePromotion.setPromotionItemTotalAmt(promotionItemTotalAmt);//这个商品的售价累计
                evaluateScopePromotion.setPromotionItemTotalCount(promotionItemTotalCount); // 商品件数的合计
                evaluateScopePromotion.setPromotionItemTotalPayAmt(promotionItemTotalPayAmt); // 商品件数的合计
                return eligible;
            } else {
                return true;
            }
        }
        if (EnumPromotionType.REALM_ALL.Equals(realmDetail.realmType)) {
            if (products != null && products.Count > 0) {
                for (int i = 0; i < products.Count; i++) {
                    Product Product = products[i];
                    if (Product != null) {
                        if (realmDetail.skuCodesToExclude != null && realmDetail.skuCodesToExclude.Contains(Product.skucode)) {
                            continue;
                        }
                        if (isEligibleRow(DBPROMOTION_CACHE_BEANMODEL, products[i])) {
//                            listScope.add(i);//我给商品标记的第几行
                            list.Add(Product);
                            promotionItemTotalAmt = MoneyUtils.add(promotionItemTotalAmt, Product.price.total);//售价合计
                            promotionItemTotalPayAmt = MoneyUtils.add(promotionItemTotalPayAmt, Product.PaySubAmt);//售价-已享受促销之后的应付金额
                            promotionItemTotalCount = MoneyUtils.add(promotionItemTotalCount,Product.num);//购买的份数
                            eligible = true;
                        }
                    }
                }
//                cartBean.setListScope(listScope); //适用的商品行号
                evaluateScopePromotion.setList(list);
                evaluateScopePromotion.setPromotionItemTotalAmt(promotionItemTotalAmt);//这个商品的售价累计
                evaluateScopePromotion.setPromotionItemTotalCount(promotionItemTotalCount); // 商品件数的合计
                evaluateScopePromotion.setPromotionItemTotalPayAmt(promotionItemTotalPayAmt); // 商品件数的合计
                return eligible;
            }
        }

        if (EnumPromotionType.REALM_GOODS.Equals(realmDetail.realmType)) {
            if (products != null && products.Count > 0) {
                for (int i = 0; i < products.Count; i++) {
                    Product Product = products[i];
                    if (Product != null) {
                        if (realmDetail.skuCodesToInclude != null && realmDetail.skuCodesToInclude.Contains(Product.skucode) && isEligibleRow(DBPROMOTION_CACHE_BEANMODEL, products[i])) {
//                            listScope.add(i);//我给商品标记的第几行
                            list.Add(Product);
                            promotionItemTotalAmt = MoneyUtils.add(promotionItemTotalAmt, Product.price.total);//售价合计
                            promotionItemTotalPayAmt = MoneyUtils.add(promotionItemTotalPayAmt, Product.PaySubAmt);//售价-已享受促销之后的应付金额
                            promotionItemTotalCount = MoneyUtils.add(promotionItemTotalCount,Product.num);//购买的份数
                            eligible = true;
                        }
                    }
                }
//                evaluateScopePromotion.setListScope(listScope); //适用的商品行号
                evaluateScopePromotion.setList(list);
                evaluateScopePromotion.setPromotionItemTotalAmt(promotionItemTotalAmt);//这个商品的售价累计
                evaluateScopePromotion.setPromotionItemTotalCount(promotionItemTotalCount); // 商品件数的合计
                evaluateScopePromotion.setPromotionItemTotalPayAmt(promotionItemTotalPayAmt); // 商品件数的合计
                return eligible;
            }
        }

        if (EnumPromotionType.REALM_CATALOG.Equals(realmDetail.realmType)) {
            if (products != null && products.Count > 0) {
                for (int i = 0; i < products.Count; i++) {
                    Product Product = products[i];
                    if (Product != null) {
                        if (realmDetail.skuCodesToExclude != null && realmDetail.skuCodesToExclude.Contains(Product.skucode)) {
                            continue;
                        }

                        //TODO  firs
                        if (Product.firstcategoryid == null || Product.firstcategoryid == "") {
                            continue;
                        }

                        if (realmDetail.skuCodesToInclude != null && Product.firstcategoryid != null && !realmDetail.catalogsToInclude.Contains(
                                Product.firstcategoryid) &&
                                isEligibleRow(DBPROMOTION_CACHE_BEANMODEL, products[i])) {
//                            listScope.add(i);//我给商品标记的第几行
                            list.Add(Product);
                            promotionItemTotalAmt = MoneyUtils.add(promotionItemTotalAmt, Product.price.total);//售价合计
                            promotionItemTotalPayAmt = MoneyUtils.add(promotionItemTotalPayAmt, Product.PaySubAmt);//售价-已享受促销之后的应付金额
                            promotionItemTotalCount = MoneyUtils.add(promotionItemTotalCount,Product.num);//购买的份数
                            eligible = true;
                        }
                    }
                }
//                cartBean.setListScope(listScope); //适用的商品行号
                evaluateScopePromotion.setList(list);
                evaluateScopePromotion.setPromotionItemTotalAmt(promotionItemTotalAmt);//这个商品的售价累计
                evaluateScopePromotion.setPromotionItemTotalCount(promotionItemTotalCount); // 商品件数的合计
                evaluateScopePromotion.setPromotionItemTotalPayAmt(promotionItemTotalPayAmt); // 商品件数的合计
                return eligible;
            }
        }

        if (EnumPromotionType.REALM_MIXED.Equals(realmDetail.realmType)) {
            if (products != null && products.Count > 0) {
                for (int i = 0; i < products.Count; i++) {
                    Product Product = products[i];
                    if (Product != null) {
                        if (realmDetail.skuCodesToExclude != null && realmDetail.skuCodesToExclude.Contains(Product.skucode)) {
                            continue;
                        }


                        if (Product.firstcategoryid == null || Product.firstcategoryid == "") {
                            continue;
                        }

                        if (realmDetail.skuCodesToInclude != null && Product.firstcategoryid != null && !realmDetail.catalogsToInclude.Contains(Product.firstcategoryid)
                                ) {
                            continue;
                        }
                        if (((realmDetail.catalogsToInclude != null
                                && Product.firstcategoryid != null 
                                && !realmDetail.catalogsToInclude.Contains( Product.firstcategoryid)) ||
                                (CollectionUtils.isNotEmpty(realmDetail.skuCodesToInclude)
                                        && realmDetail.skuCodesToInclude.Contains(Product.skucode)))
                                && isEligibleRow(DBPROMOTION_CACHE_BEANMODEL, products[i])) {
//                            listScope.add(i);//我给商品标记的第几行
                            list.Add(Product);
                            promotionItemTotalAmt = MoneyUtils.add(promotionItemTotalAmt, Product.price.total);//售价合计
                            promotionItemTotalPayAmt = MoneyUtils.add(promotionItemTotalPayAmt, Product.PaySubAmt);//售价-已享受促销之后的应付金额
                            promotionItemTotalCount = MoneyUtils.add(promotionItemTotalCount,Product.num);//购买的份数
                            eligible = true;
                        }
                    }
                }
//                cartBean.setListScope(listScope); //适用的商品行号
                evaluateScopePromotion.setList(list);
                evaluateScopePromotion.setPromotionItemTotalAmt(promotionItemTotalAmt);//这个商品的售价累计
                evaluateScopePromotion.setPromotionItemTotalCount(promotionItemTotalCount); // 商品件数的合计
                evaluateScopePromotion.setPromotionItemTotalPayAmt(promotionItemTotalPayAmt); // 商品件数的合计
                return eligible;
            }
        }
        return false;
    }

        //公共的商品是否适用当前促销的判断
        private bool isEligibleRow(DBPROMOTION_CACHE_BEANMODEL promotion, Product orderItem)
        {
            return canApplySpecificPromo(promotion, orderItem, shopId);
        }

        //判断商品是否适用单品促销
        private bool canApplySpecificPromo(DBPROMOTION_CACHE_BEANMODEL promotion, Product orderItem, String shopId)
        {
            //校验是否仅适用于原价购买的商品
            if (promotion.ONLYUSEINORIGINAL==1 &&
                    (MoneyUtils.isFirstBiggerThanSecond(orderItem.price.originprice, orderItem.price.total) || MoneyUtils.isBiggerThanZero(orderItem.PaySubAmt)))
            {
                return false;
            }
            //部分分类不参与夜间促销
            if (promotion.FROMOUTER != null && promotion.FROMOUTER==1 && !string.IsNullOrEmpty(promotion.OUTERCODE)
                    && NIGHT_TIMED_PROMO_CODES.Contains(promotion.OUTERCODE)
                    && !string.IsNullOrEmpty(orderItem.firstcategoryid))
            {
                try
                {
                    if (list != null && list.Count > 0 && !string.IsNullOrEmpty(orderItem.firstcategoryid) && list.Contains(orderItem.firstcategoryid.Substring(0, 2)))
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                  //  e.printStackTrace();
                }
            }
            //        if (orderItem.getGoodsBarcode() == null || shopId == null) {
            //            return true;
            //        }
            //        //购物袋不参与任何促销
            //        if (Objects.equals(orderItem.getGoodsFlag(), ItemGoodsFlag.BAG)) {
            //            return false;
            //        }
            //散称,点餐商品不参与单行促销  //TODO 散称判断
            //        if (PromotionContextConvertUtils.isSingleLinePromotion(promotion)
            //                && (!Objects.equals(orderItem.getBomType(),
            //                ItemBomType.BOM.getValue()) && posSkuService.getSkuByFreshBarcodeAndShopId(orderItem.getGoodsBarcode(),
            //                shopId) != null)) {
            //            return false;
            //        }

            //判断是否是O或者OrderCoupon类型的促销  商品是否是香烟 商品是否是购物袋
            //        if ((PromotionContextConvertUtils.isOrderLevelOrOrderCouponPromotion(promotion) && orderItem.getCategoryId() != null
            //                && orderItem.getCategoryId().substring(0, 6).equals("123301"))) {
            //            return false;
            //        }
            return true;
        }






    

}

}
