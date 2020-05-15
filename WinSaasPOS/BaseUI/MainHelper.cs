using Maticsoft.BLL;
using Maticsoft.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using WinSaasPOS.Common;
using WinSaasPOS.Model;
using WinSaasPOS.Model.Promotion;

namespace WinSaasPOS.BaseUI
{
    public class MainHelper
    {
        #region  计算促销
        /// <summary>
        /// 单品促销
        /// </summary>
        private static  ImplOfflineSingleCalculateNew impsinglecalculate = new ImplOfflineSingleCalculateNew(MainModel.CurrentShopInfo.tenantid, MainModel.CurrentShopInfo.shopid);

        /// <summary>
        /// 订单级别促销
        /// </summary>
        private static  ImplOfflineOrderPromotion impordercalculate = new ImplOfflineOrderPromotion(MainModel.CurrentShopInfo.tenantid, MainModel.CurrentShopInfo.shopid);
        /// <summary>
        /// 优惠券
        /// </summary>
        private static  ImplOfflineCouponsCalculate impcouponcalculate = new ImplOfflineCouponsCalculate(MainModel.CurrentShopInfo.tenantid, MainModel.CurrentShopInfo.shopid);
        /// <summary>
        /// 积分
        /// </summary     
        private static  CalculateAvailablePointsCommandImpl imppointcalculate = new CalculateAvailablePointsCommandImpl();

        /// <summary>
        /// 计算促销 
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="whethrefix">是否修改金额</param>
        public static void CartPromotion(Cart cart,bool whethrefix)
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

                SingleCalculate(cart);
                OrderCalculate(cart);
                
                CouponCalculate(cart);
                PointCalculate(cart);

                FixCalculate(cart, whethrefix);

                UpdatePaymentType(cart);
                Console.WriteLine("本地优惠计算时间" + (DateTime.Now - starttime).Milliseconds);

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("计算优惠异常"+ex.Message);
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
                    cart.paymenttypes = WinSaasPOS.Model.HalfOffLine.HalfOffLineUtil.paymenttypes;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("UpdatePaymentType 异常"+ex.Message);
            }
        }

        /// <summary>
        /// 单品促销
        /// </summary>
        /// <param name="pro"></param>
        public static void SingleCalculate(Cart cart){

           
            //初始化商品数据
            foreach (Product singlepro in cart.products)
            {

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
                impsinglecalculate.calculate(singlepro);
            }

            decimal carttotal = 0;
            decimal cartoriginaltotal = 0;
            decimal cartmemberpromo = 0;
            foreach (Product pro in cart.products)
            {
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

            AddOrderPriceDetail(cart,"商品金额", cart.totalpayment,"");

        }

        /// <summary>
        /// 订单促销
        /// </summary>
        /// <param name="cart"></param>
        public static void OrderCalculate(Cart cart)
        {
            impordercalculate.doAction(cart.products); //订单促销

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
                AddOrderPriceDetail(cart,"活动优惠",cartpromoamt,"-");
            }
        }

        /// <summary>
        /// 修改金额
        /// </summary>
        /// <param name="cart"></param>
        public static void FixCalculate(Cart cart,bool whetherfix)
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


        public static void AddOrderPriceDetail(Cart cart,string title, decimal amount,string prefix)
        {
            try
            {
                if (cart.orderpricedetails == null)
                {
                    cart.orderpricedetails = new List<OrderPriceDetail>();
                }
                OrderPriceDetail temppricedetial = new OrderPriceDetail();
                temppricedetial.title = title;
                temppricedetial.amount = prefix+"¥ " + Math.Round(amount, 2, MidpointRounding.AwayFromZero);
                cart.orderpricedetails.Add(temppricedetial);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("AddOrderPriceDetail 异常"+ex.Message);
            }
        }
        #endregion


        #region  查询本地数据
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
                    List<DBPRODUCT_BEANMODEL> lstdbpro = productbll.GetModelList(" INNERBARCODE='" + goodcode.Substring(2, 10) + "' " + " and CREATE_URL_IP='" + MainModel.URL + "' ");
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
                    List<DBPRODUCT_BEANMODEL> lstdbpro = productbll.GetModelList(" BARCODE='" + goodcode + "'" + " and CREATE_URL_IP='" + MainModel.URL + "' ");
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
                                dbpro.NUM = 1;
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
                        dbpro.NUM = 1;
                    }

                }

                //防止重量为0的条码  TODO 
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
                LogManager.WriteLog("GetScancodeMemberByDbpro 异常"+ex.Message);
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
                    return true;
                }
            }
            catch (Exception e)
            {
                return true;
            }
        }

    }


    public class ScanModelAndDbpro
    {
        public DBPRODUCT_BEANMODEL dbproduct { get; set; }
        public scancodememberModel ScanModel { get; set; }
    }
#endregion
}
