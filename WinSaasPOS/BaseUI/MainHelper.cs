using Maticsoft.BLL;
using Maticsoft.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using WinSaasPOS.Common;
using WinSaasPOS.Model;
using WinSaasPOS.Model.HalfOffLine;
using WinSaasPOS.Model.Promotion;

namespace WinSaasPOS.BaseUI
{
    public class MainHelper
    {
        #region  计算促销
        /// <summary>
        /// 单品促销
        /// </summary>
        private static ImplOfflineSingleCalculateNew impsinglecalculate = new ImplOfflineSingleCalculateNew(MainModel.CurrentShopInfo.tenantid, MainModel.CurrentShopInfo.shopid);

        /// <summary>
        /// 订单级别促销
        /// </summary>
        private static ImplOfflineOrderPromotion impordercalculate = new ImplOfflineOrderPromotion(MainModel.CurrentShopInfo.tenantid, MainModel.CurrentShopInfo.shopid);
        /// <summary>
        /// 优惠券
        /// </summary>
        private static ImplOfflineCouponsCalculate impcouponcalculate = new ImplOfflineCouponsCalculate(MainModel.CurrentShopInfo.tenantid, MainModel.CurrentShopInfo.shopid);
        /// <summary>
        /// 积分
        /// </summary     
        private static CalculateAvailablePointsCommandImpl imppointcalculate = new CalculateAvailablePointsCommandImpl();

        private static ImplApplyPayPromoCalculate impApplyCalculate = new ImplApplyPayPromoCalculate();

        /// <summary>
        /// 计算促销 
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="whethrefix">是否修改金额</param>
        public static void CartPromotion(Cart cart, bool whethrefix)
        {
            try
            {
                DateTime starttime = DateTime.Now;
                if (cart == null || cart.products == null || cart.products.Count == 0)
                {
                    return;
                }
                //计算优惠 先清空缓存资源
                PromotionCache.getInstance().onCreate(MainModel.CurrentShopInfo.tenantid, MainModel.CurrentShopInfo.shopid);
                cart.orderpricedetails = new List<OrderPriceDetail>();
                cart.couponpromoamt = 0;

                if (string.IsNullOrEmpty(MainModel.CurrentCouponCode))
                {
                    cart.selectedcoupons = null;
                }
                else
                {
                    cart.selectedcoupons = new Dictionary<string, Availablecoupon>();
                    cart.selectedcoupons.Add(MainModel.CurrentCouponCode, MainModel.Currentavailabecoupno);
                }

                SingleCalculate(cart);
                OrderCalculate(cart);

                CouponCalculate(cart);
                PointCalculate(cart);

                FixCalculate(cart, whethrefix);

                UpdatePaymentType(cart);

                ApplyPayPromo(cart);
                //if(cart.paymenttypes !=null && cart.paymenttypes.ba)

                Console.WriteLine("本地优惠计算时间" + (DateTime.Now - starttime).Milliseconds);

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("计算优惠异常" + ex.Message);
            }
        }

        public static void UpdatePaymentType(Cart cart)
        {
            try
            {
                //如果没有重新刷新一次接口  防止第一次获取失败
                if (WinSaasPOS.Model.HalfOffLine.HalfOffLineUtil.paymenttypes == null)
                {
                    Thread threadmqtt = new Thread(WinSaasPOS.Model.HalfOffLine.HalfOffLineUtil.GetAvailablePaymentTypes);
                    threadmqtt.IsBackground = true;
                    threadmqtt.Start();
                }
                else
                {

                    Paymenttypes avaiablePayment = new Paymenttypes();
                    avaiablePayment.balancepayenabled = HalfOffLineUtil.paymenttypes.balancepayenabled;
                    avaiablePayment.balancemixpayenabled = HalfOffLineUtil.paymenttypes.balancemixpayenabled;
                    avaiablePayment.cashcouponpayenabled = HalfOffLineUtil.paymenttypes.cashcouponpayenabled;
                    avaiablePayment.cashenabled = HalfOffLineUtil.paymenttypes.cashenabled;
                    avaiablePayment.facepayenabled = HalfOffLineUtil.paymenttypes.facepayenabled;
                    avaiablePayment.onlineenabled = HalfOffLineUtil.paymenttypes.onlineenabled;
                    avaiablePayment.swipecardpayenabled = HalfOffLineUtil.paymenttypes.swipecardpayenabled;

                    if (avaiablePayment.balancepayenabled == 1)
                    {
                        if (avaiablePayment.balancemixpayenabled == 1 || MainModel.CurrentMember == null)
                        {
                            avaiablePayment.balancepayenabled = 1;
                        }
                        else
                        {
                            if (MainModel.CurrentMember.barcoderecognitionresponse.balance > cart.totalpayment)
                            {
                                avaiablePayment.balancepayenabled = 1;
                            }
                            else
                            {
                                avaiablePayment.balancepayenabled = 0;
                            }
                        }
                    }
                    else
                    {
                        avaiablePayment.balancepayenabled = 0;
                    }

                    cart.paymenttypes = avaiablePayment;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("UpdatePaymentType 异常" + ex.Message);
            }
        }

        /// <summary>
        /// 单品促销
        /// </summary>
        /// <param name="pro"></param>
        public static void SingleCalculate(Cart cart)
        {
            IniPorducts(cart.products);


            cart.products = cart.products.OrderBy(r=> r.RowNum).ToList();
            //初始化商品数据
            foreach (Product singlepro in cart.products)
            {
                singlepro.RowNum = 0;
                singlepro.offlinepromos = new List<OffLinePromos>();
               
                if (singlepro.goodstagid == 0)
                {
                    singlepro.price.total = Math.Round(singlepro.num * singlepro.price.saleprice, 2, MidpointRounding.AwayFromZero);
                    singlepro.price.origintotal = Math.Round(singlepro.num * singlepro.price.originprice, 2, MidpointRounding.AwayFromZero);
                    singlepro.PaySubAmt = Math.Round(singlepro.num * singlepro.price.saleprice, 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    singlepro.price.total = Math.Round(singlepro.price.saleprice * singlepro.price.specnum, 2, MidpointRounding.AwayFromZero);
                    singlepro.price.origintotal = Math.Round(singlepro.price.originprice * singlepro.price.specnum, 2, MidpointRounding.AwayFromZero);
                    singlepro.PaySubAmt = Math.Round(singlepro.price.saleprice * singlepro.price.specnum, 2, MidpointRounding.AwayFromZero);
                }

                singlepro.PromoSubAmt = 0;
                singlepro.offlinepromos = new List<OffLinePromos>();
                TripletBeanForCoupon tripletbean = impsinglecalculate.calculate(singlepro);

                if (tripletbean == null)
                {
                    singlepro.pricetag = "";
                    singlepro.pricetagid = 0;
                }
            }

            decimal carttotal = 0;
            decimal cartoriginaltotal = 0;
            decimal cartmemberpromo = 0;
            foreach (Product pro in cart.products)
            {

                if (pro.goodstagid == 0)
                {
                    pro.price.total = Math.Round(pro.num * pro.price.saleprice, 2, MidpointRounding.AwayFromZero);
                    pro.price.origintotal = Math.Round(pro.num * pro.price.originprice, 2, MidpointRounding.AwayFromZero);
                    pro.PaySubAmt = Math.Round(pro.num * pro.price.saleprice, 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    pro.price.total = Math.Round(pro.price.saleprice * pro.price.specnum, 2, MidpointRounding.AwayFromZero);
                    pro.price.origintotal = Math.Round(pro.price.originprice * pro.price.specnum, 2, MidpointRounding.AwayFromZero);
                    pro.PaySubAmt = Math.Round(pro.price.saleprice * pro.price.specnum, 2, MidpointRounding.AwayFromZero);
                }

                carttotal += pro.price.total;
                cartoriginaltotal += pro.price.origintotal;
                if (pro.pricetagid == 1 || pro.pricetagid == 4)
                {
                    cartmemberpromo += Math.Abs(pro.price.origintotal - pro.price.total);
                }
            }

            cart.origintotal = cartoriginaltotal;
            cart.totalpayment = Math.Round(carttotal, 2, MidpointRounding.AwayFromZero);
            cart.payamt = cart.totalpayment;

            cart.memberpromo = cartmemberpromo;

            AddOrderPriceDetail(cart, "商品金额", cart.totalpayment, "");

        }

        /// <summary>
        /// 订单促销
        /// </summary>
        /// <param name="cart"></param>
        public static void OrderCalculate(Cart cart)
        {
            impordercalculate.doAction(cart.products,cart); //订单促销

            decimal cartoriginaltotal = 0;
            decimal carttotal = 0;
            decimal cartpromoamt = 0;
            foreach (Product pro in cart.products)
            {
                cartoriginaltotal += pro.price.origintotal;
                carttotal += pro.price.total;

                if (pro.PromoSubAmt != null && pro.PromoSubAmt > 0)
                {
                    cartpromoamt += pro.PromoSubAmt;
                    carttotal -= pro.PromoSubAmt;
                }
            }

            cart.origintotal = Math.Round(cartoriginaltotal, 2, MidpointRounding.AwayFromZero);
            cart.totalpayment = Math.Round(carttotal, 2, MidpointRounding.AwayFromZero);
            cart.payamt = Math.Round(carttotal, 2, MidpointRounding.AwayFromZero);
            cart.totalpaymentbeforefix = cart.totalpayment;
            cart.promoamt = Math.Round(cartpromoamt, 2, MidpointRounding.AwayFromZero);
            if (cartpromoamt > 0)
            {
                AddOrderPriceDetail(cart, "活动优惠", cartpromoamt, "-");
            }
        }

        /// <summary>
        /// 修改金额
        /// </summary>
        /// <param name="cart"></param>
        public static void FixCalculate(Cart cart, bool whetherfix)
        {
            if (whetherfix)
            {

                cart.fixpricepromoamt = cart.totalpayment - cart.fixpricetotal;
                cart.totalpayment = cart.fixpricetotal;
                if (cart.orderpricedetails == null)
                {
                    cart.orderpricedetails = new List<OrderPriceDetail>();
                }
                OrderPriceDetail temppricedetial = new OrderPriceDetail();
                temppricedetial.title = "整单优惠";
                temppricedetial.amount = "-¥ " + PromotionInfoUtils.getFixedScaleAmt(cart.fixpricepromoamt, 2);
                cart.orderpricedetails.Add(temppricedetial);
            }
            else
            {
                cart.totalpaymentbeforefix = cart.totalpayment;
                cart.fixpricetotal = 0;
            }

            cart.payamt = cart.totalpayment;
        }

        /// <summary>
        /// 优惠券计算
        /// </summary>
        /// <param name="cart"></param>
        public static void CouponCalculate(Cart cart)
        {
            impcouponcalculate.calculate(cart);  //优惠券

            //add 202006-18
            if (cart.couponpromoamt == 0)
            {
                MainModel.CurrentCouponCode = "";
                MainModel.Currentavailabecoupno = null;
            }
        }

        /// <summary>
        /// 积分计算
        /// </summary>
        /// <param name="cart"></param>
        public static void PointCalculate(Cart cart)
        {
            imppointcalculate.execute(cart);  //积分

            cart.totalpayment = cart.payamt;
        }

        /// <summary>
        /// 余额支付立减
        /// </summary>
        /// <param name="cart"></param>
        public static void ApplyPayPromo(Cart cart)
        {
            if (MainModel.CurrentMember != null && cart.paymenttypes != null && cart.paymenttypes.balancemixpayenabled != 1)
            {

           
            cart.balancepaypromoamt = 0;
            impApplyCalculate.applyPayPromo(cart);

            }
        }

        public static void AddOrderPriceDetail(Cart cart, string title, decimal amount, string prefix)
        {
            try
            {
                if (cart.orderpricedetails == null)
                {
                    cart.orderpricedetails = new List<OrderPriceDetail>();
                }
                OrderPriceDetail temppricedetial = new OrderPriceDetail();
                temppricedetial.title = title;
                temppricedetial.amount = prefix + "¥ " + Math.Round(amount, 2, MidpointRounding.AwayFromZero);
                cart.orderpricedetails.Add(temppricedetial);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("AddOrderPriceDetail 异常" + ex.Message);
            }
        }



        public static void IniPorducts(List<Product> lstpro)
        {
            try
            {

                if (lstpro != null && lstpro.Count > 0)
                {

               
                List<string> lstskucodes = new List<string>();
                string skucodes = "";
                foreach (Product pro in lstpro)
                {
                    if(!string.IsNullOrEmpty(pro.skucode)){
                        if (!lstskucodes.Contains(pro.skucode))
                        {
                            skucodes += pro.skucode + ",";
                            lstskucodes.Add(pro.skucode);
                        }
                    }
                   
                }

                List<DBPRODUCT_BEANMODEL> lstdbpro = productbll.GetModelList(" SKUCODE in ("+skucodes.TrimEnd(',')+") "+" and CREATE_URL_IP='" + MainModel.URL + "' ");

                Dictionary<string, DBPRODUCT_BEANMODEL> dicpro = new Dictionary<string, DBPRODUCT_BEANMODEL>();
                foreach (DBPRODUCT_BEANMODEL dbpro in lstdbpro)
                {
                    if (!dicpro.ContainsKey(dbpro.SKUCODE))
                    {
                        dicpro.Add(dbpro.SKUCODE,dbpro);
                    }
                }

                foreach (Product pro in lstpro)
                {
                    DBPRODUCT_BEANMODEL tempdbpro = dicpro[pro.skucode];
                    if (tempdbpro != null)
                    {
                        pro.transitionPriceDetail = null;
                        pro.pricetag = "";
                        pro.pricetagid = -1;
                        pro.canmixcoupon = true;
                        pro.offlinepromos = new List<OffLinePromos>();

                        pro.firstcategoryid = tempdbpro.FIRSTCATEGORYID;
                        pro.firstcategoryname = tempdbpro.FIRSTCATEGORYNAME;
                        pro.secondcategoryid = tempdbpro.SECONDCATEGORYID;

                        pro.categoryid = tempdbpro.CATEGORYID;

                        if (pro.price != null)
                        {
                            pro.price.flag = 0;
                            pro.price.originprice = tempdbpro.SALEPRICE;
                            pro.price.saleprice = tempdbpro.SALEPRICE;
                            pro.price.originpricedesc = "";
                            pro.price.salepricedesc = "";
                        }
                    }
                }

                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("初始化购物车商品异常"+ex.Message);
            }
        }
        #endregion


        #region  查询本地数据

       // private static string strfilter = " and 1=1 and STATUS =1 and SKUTYPE in (1,4)";
        private static DBPRODUCT_BEANBLL productbll = new DBPRODUCT_BEANBLL();
        public static DBPRODUCT_BEANMODEL GetLocalPro(string goodcode)
        {
            try
            {
                DBPRODUCT_BEANMODEL dbpro = null;

                bool isINNERBARCODE = false;
                string skucode = "";
                int pronum = 0;
                if (goodcode.Length == 18 && !checkEanCodeIsError(goodcode, 18) && (goodcode.Substring(0, 2) == "25" || goodcode.Substring(0, 2) == "26"))
                {
                    List<DBPRODUCT_BEANMODEL> lstdbpro = productbll.GetModelList(" INNERBARCODE='" + goodcode.Substring(2, 10) + "' " + " and CREATE_URL_IP='" + MainModel.URL + "' " );
                    if (lstdbpro != null && lstdbpro.Count > 0)
                    {
                        isINNERBARCODE = true;
                        dbpro = lstdbpro[0];
                    }
                    else
                    {
                        isINNERBARCODE = false;


                       

                        lstdbpro = productbll.GetModelList(" BARCODE='" + goodcode + "'" + " and CREATE_URL_IP='" + MainModel.URL + "' ");

                        if (lstdbpro != null && lstdbpro.Count > 0)
                        {
                            dbpro = lstdbpro[0];
                        }
                    }
                }
                else
                {

                    isINNERBARCODE = false;
                    List<DBPRODUCT_BEANMODEL> lstdbpro = null;
                    if (!checkEanCodeIsError(goodcode, 13) && goodcode.Length > 2 && (goodcode.Substring(0, 2) == "25" || goodcode.Substring(0, 2) == "26"))
                    {
                        lstdbpro = productbll.GetModelList(" BARCODE='" + goodcode.Substring(2, 5) + "'" + " and CREATE_URL_IP='" + MainModel.URL + "' ");
                    }
                    else
                    {
                        lstdbpro = productbll.GetModelList(" BARCODE='" + goodcode + "'" + " and CREATE_URL_IP='" + MainModel.URL + "' ");
                    }
                   // List<DBPRODUCT_BEANMODEL> lstdbpro = productbll.GetModelList(" BARCODE='" + goodcode + "'" + " and CREATE_URL_IP='" + MainModel.URL + "' ");
                    if (lstdbpro != null && lstdbpro.Count > 0)
                    {
                        dbpro = lstdbpro[0];
                    }
                }

                if (dbpro != null)
                {


                    if (goodcode.Length == 18 && !checkEanCodeIsError(goodcode, 18))
                    {

                        if (dbpro.WEIGHTFLAG == 1)
                        {
                            if (isINNERBARCODE)
                            {
                                int num = Convert.ToInt32(goodcode.Substring(goodcode.Length - 6, 5));
                                decimal decimalnum = (decimal)num / 1000;

                                dbpro.SPECNUM = decimalnum;
                                dbpro.NUM = 1;
                            }
                            else
                            {
                                return null;
                               // dbpro.NUM = 1;
                            }

                        }
                        else
                        {
                            if (isINNERBARCODE)
                            {
                                int num = Convert.ToInt32(goodcode.Substring(goodcode.Length - 6, 5));

                                dbpro.NUM = num;
                            }
                            else
                            {
                                dbpro.NUM = 1;
                            }

                            dbpro.SPECNUM = 1;
                        }
                    }
                    else
                    {
                        if (dbpro.WEIGHTFLAG == 1)
                        {
                            if (!checkEanCodeIsError(goodcode, 13))
                            {
                                int num = Convert.ToInt32(goodcode.Substring(goodcode.Length - 6, 5));
                                decimal decimalnum = (decimal)num / 1000;

                                dbpro.SPECNUM = decimalnum;
                                dbpro.NUM = 1;
                            }
                            else
                            {
                                return null;
                            }

                            
                        }
                        else
                        {
                            dbpro.SPECNUM = 1;
                            dbpro.NUM = 1;
                        }
                      
                    }

                }

                if (dbpro != null && dbpro.WEIGHTFLAG == 1 && dbpro.SPECNUM == 0)
                {
                    dbpro = null;
                }
                
                return dbpro;


            }
            catch (Exception ex)
            {
                // MainModel.ShowLog("条码验证异常"+ex.Message ,true );
                LogManager.WriteLog("条码验证异常" + ex.Message);
                return null;
            }
        }

        public static scancodememberModel GetScancodeMemberByDbpro(DBPRODUCT_BEANMODEL dbpro)
        {
            try
            {
                scancodememberModel result = new scancodememberModel();
                result.scancodedto = new Scancodedto();
                result.scancodedto.skuname = dbpro.SKUNAME;
                result.scancodedto.skucode = dbpro.SKUCODE;
                result.scancodedto.num = (int)dbpro.NUM;
                result.scancodedto.specnum = dbpro.SPECNUM;
                result.scancodedto.spectype = dbpro.SPECTYPE;
                result.scancodedto.weightflag = dbpro.WEIGHTFLAG == 1 ? true : false;
                result.scancodedto.barcode = dbpro.BARCODE;

                return result;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetScancodeMemberByDbpro 异常" + ex.Message);
                return null;
            }
        }

        public static bool checkEanCodeIsError(String barCode, int num)
        {
            if (barCode.Length != num)
            {
                return true;
            }
            try
            {
                String code = barCode.Substring(0, num - 1);
                String checkBit = barCode.Substring(num - 1);
                int c1 = 0, c2 = 0;
                for (int i = 0; i < code.Length; i += 2)
                {
                    char c = code[i];
                    int n = c - '0';
                    c1 += n;
                }
                for (int i = 1; i < code.Length; i += 2)
                {
                    char c = code[i];
                    int n = c - '0';
                    c2 += n;
                }
                String check = (10 - (c1 + c2 * 3) % 10) % 10 + "";
                if (check == checkBit)
                {
                    return false;
                }
                else
                {
                    if (checkBit.Equals(getMod10CheckDigit(code) + ""))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                return true;
            }
        }

        public static char getMod10CheckDigit(string barcodeString)
        {

            int a = 0, b = 0, c = 0;
            int d = 0;
            char[] charArray = barcodeString.ToCharArray();

            // Step 1: Sum all of the digits in the odd positions together.
            for (int i = barcodeString.Length - 1; i >= 0; i = i - 2)
            {
                if (charArray[i] > '9' || charArray[i] < '0')
                    return (char)0;
                a = a + charArray[i] - 48;
            }

            // Step 2: Mutliply the sum from Step 1 by 3.
            a = a * 3;

            // Step 3: Sum all of the digits in the even positions together.
            for (int i = barcodeString.Length - 2; i >= 0; i = i - 2)
            {
                if (charArray[i] > '9' || charArray[i] < '0')
                    return (char)0;
                b = b + charArray[i] - 48;
            }

            // Step 4: Sum together the results from Step 2 and Step 3.
            c = a + b;

            // Step 5: Subtract the sum from the next highest multiple of 10.
            d = c % 10;
            if (d != 0)
            {
                d = 10 - d;
            }
            return (char)(d + 48);

        }
    }



    public class ScanModelAndDbpro
    {
        public DBPRODUCT_BEANMODEL dbproduct { get; set; }
        public scancodememberModel ScanModel { get; set; }
    }

        #endregion
}
