using Maticsoft.BLL;
using Maticsoft.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.Model.HalfOffLine;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Common
{
    public class CartUtil
    {
        private static DBPRODUCT_BEANBLL productbll = new DBPRODUCT_BEANBLL();

        /// <summary>
        /// 加载本地数据库所有商品
        /// </summary>
        /// <param name="NeesLocalPrice"></param>
        /// <returns></returns>
        public static List<Product> LoadAllProduct(bool NeesLocalPrice)
        {
            List<Product> LstAllProduct = new List<Product>();
            try
            {
               // List<DBPRODUCT_BEANMODEL> lstpro = productbll.GetModelList(" STATUS =1 and CREATE_URL_IP='" + MainModel.URL + "' and SHOPID='" + MainModel.CurrentShopInfo.shopid + "' order by FIRSTCATEGORYID");
                List<DBPRODUCT_BEANMODEL> lstpro = productbll.GetModelList(" STATUS =1 and CREATE_URL_IP='" + MainModel.URL + "' and SHOPID='" + MainModel.CurrentShopInfo.shopid + "' order by SALECOUNT");
                foreach (DBPRODUCT_BEANMODEL pro in lstpro)
                {                   
                    Product product = new Product();
                    product.mainimg = pro.MAINIMG;
                    product.price = null;
                    product.pricetag = pro.PRICETAG;
                    product.pricetagid = (int)pro.PRICETAGID;
                    product.saleunit = pro.SALESUNIT;
                    product.skucode = pro.SKUCODE;
                    product.skuname = pro.SKUNAME;
                    product.title = pro.SKUNAME;
                    product.firstcategoryid = pro.FIRSTCATEGORYID;
                    product.firstcategoryname = pro.FIRSTCATEGORYNAME;
                    product.barcode = pro.SKUCODE;

                    product.secondcategoryid = pro.SECONDCATEGORYID;
                    product.secondcategoryname = pro.SECONDCATEGORYNAME;
                    //product.isQueryBarcode = 0;
                    product.weightflag = Convert.ToBoolean(pro.WEIGHTFLAG);
                    product.shopid = pro.SHOPID;
                    product.goodstagid = (int)pro.WEIGHTFLAG;
                    product.num = 1;

                    product.salecount = (int)pro.SALECOUNT;
                    product.createdat = pro.CREATEDAT;
                    product.AllFirstLetter = pro.ALL_FIRST_LETTER;

                    product.InnerBarcode = pro.INNERBARCODE;
                    product.ShelfLife = pro.SHELFLIFE;

                    product.memberprice = pro.MEMBERPRICE;

                    if (NeesLocalPrice)
                    {
                        Price price = new Price();
                        price.saleprice = pro.SALEPRICE;
                        price.originprice = pro.SALEPRICE;
                        price.specnum = pro.SPECNUM;
                        price.unit = pro.SALESUNIT;

                        product.price = price;
                        //product.pricetagid = "";
                        product.pricetag = "";
                        //product.isLoadPanel = true;
                        //product.panelbmp = GetItemImg(product);

                        //singlecalculate.calculate(product);
                    }
                    //product.price = new Price();
                    //过滤脏数据
                    if(!string.IsNullOrEmpty( product.firstcategoryid)){
                        LstAllProduct.Add(product);
                    }
                    
                }

                return LstAllProduct;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("加载商品数据异常" + ex.StackTrace);
                return LstAllProduct;
            }

        }

        /// <summary>
        /// 校验条码校验位
        /// </summary>
        /// <param name="barCode"></param>
        /// <param name="num"></param>
        /// <returns></returns>
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


        /// <summary>
        /// 获取商品条码  打印标签用
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static bool GetBarCode(Product pro)
        {
            try
            {
                string tempcode = "";
                  if (pro.goodstagid == 0) //标品
                {

                    tempcode = "26" + pro.InnerBarcode + pro.num.ToString().PadLeft(5,'0');
                    string checkeancode = GetcheckEanCode(tempcode);

                    pro.barcode = tempcode + checkeancode;
                }
                else
                {
                    tempcode = "25" + pro.InnerBarcode + (Convert.ToInt32(pro.specnum * 1000)).ToString().PadLeft(5, '0');
                    string checkeancode = GetcheckEanCode(tempcode);

                    pro.barcode = tempcode + checkeancode;
                }
                  return true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("获取barcode异常"+ex.Message);
                return false;
            }
        }

        /// <summary>
        ///获取条码校验位
        /// </summary>
        /// <param name="barCode">条码字符串</param>
        /// <param name="num">条码长度</param>
        /// <returns></returns>
        public static string GetcheckEanCode(String code)
        {
           
            try
            {
                   
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
                return check;
            }
            catch (Exception e)
            {
                return null ;
            }
        }


        /// <summary>
        /// 赋值新对象
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static Product GetNewProduct(Product pro)
        {
            try
            {

                Product newpro = new Product();
                newpro.title = pro.skuname;
                newpro.skuname = pro.skuname;
                newpro.skucode = pro.skucode;
                newpro.num = pro.num;
                newpro.specnum = pro.specnum;
                newpro.spectype = pro.spectype;
                newpro.goodstagid = pro.weightflag == true ? 1 : 0;
                newpro.barcode = pro.barcode;
                newpro.weightflag = pro.weightflag;
                newpro.mainimg = pro.mainimg;
                newpro.panelbmp = pro.panelbmp;
                newpro.saleunit = pro.saleunit;
                newpro.InnerBarcode = pro.InnerBarcode;
                newpro.AllFirstLetter = pro.AllFirstLetter;
                newpro.goodstagid = pro.goodstagid;
                newpro.weightflag = pro.weightflag;
                newpro.pricetagid = pro.pricetagid;
                newpro.pricetag = pro.pricetag;

                newpro.firstcategoryid = pro.firstcategoryid;
                newpro.secondcategoryid = pro.secondcategoryid;
                newpro.secondcategoryname = pro.secondcategoryname;
                newpro.categoryid = pro.categoryid;
                    Price price = null;

                    if (pro.price != null)
                    {
                        price = new Price();

                        price.saleprice = pro.price.saleprice;
                        price.originprice = pro.price.originprice;
                        price.specnum = pro.price.specnum;
                        price.unit = pro.price.unit;
                    }

                    newpro.price = price;
                

                return newpro;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取购物车所有优惠券
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public static int GetAllCouponCount(Cart cart)
        {
            try
            {
                int result =0;

                if (cart == null)
                {
                    return 0;
                }

                if (cart.availablecoupons != null)
                {
                    result += cart.availablecoupons.Count;
                }

                if (cart.unavailablecoupons != null)
                {
                    result += cart.unavailablecoupons.Count;
                }

                return result;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public static Cart InsertProductToCart(Cart cart, Product pro,bool needmerge)
        {

                try
                {
                    if (cart == null)
                    {
                        cart = new Cart();
                    }
                    if (cart.products == null)
                    {
                        cart.products = new List<Product>();
                    }
                    if (cart.products != null && pro.goodstagid == 0)
                    {
                        bool newpro = true;
                        foreach (Product exitspro in cart.products)
                        {
                            if (pro.skucode == exitspro.skucode)
                            {
                                exitspro.num += pro.num;
                                exitspro.price.total = Math.Round(exitspro.num * exitspro.price.saleprice, 2, MidpointRounding.AwayFromZero);
                                exitspro.price.origintotal = Math.Round(exitspro.num * exitspro.price.originprice, 2, MidpointRounding.AwayFromZero);
                                exitspro.PaySubAmt = Math.Round(exitspro.num * exitspro.price.saleprice, 2, MidpointRounding.AwayFromZero);
                                exitspro.RowNum = cart.products.Count + 1;

                                newpro = false;
                                break;
                            }
                        }

                        if (newpro)
                        {
                            pro.price.total = Math.Round(pro.price.saleprice, 2, MidpointRounding.AwayFromZero);
                            pro.price.origintotal = Math.Round(pro.price.originprice, 2, MidpointRounding.AwayFromZero);
                            pro.PaySubAmt = Math.Round(pro.price.saleprice, 2, MidpointRounding.AwayFromZero);
                            cart.products.Add(pro);
                        }

                    }
                    else
                    {

                        if (needmerge)
                        {
                            bool newpro = true;
                            foreach (Product exitspro in cart.products)
                            {
                                if (pro.skucode == exitspro.skucode)
                                {
                                    exitspro.specnum += pro.specnum;
                                    exitspro.price.total = Math.Round(exitspro.num * exitspro.price.saleprice, 2, MidpointRounding.AwayFromZero);
                                    exitspro.price.origintotal = Math.Round(exitspro.num * exitspro.price.originprice, 2, MidpointRounding.AwayFromZero);
                                    exitspro.PaySubAmt = Math.Round(exitspro.num * exitspro.price.saleprice, 2, MidpointRounding.AwayFromZero);
                                    exitspro.RowNum = cart.products.Count + 1;

                                    newpro = false;
                                    break;
                                }
                            }

                            if (newpro)
                            {
                                pro.price.total = Math.Round(pro.price.saleprice * pro.price.specnum, 2, MidpointRounding.AwayFromZero);
                                pro.price.origintotal = Math.Round(pro.price.originprice * pro.price.specnum, 2, MidpointRounding.AwayFromZero);
                                pro.PaySubAmt = Math.Round(pro.price.saleprice * pro.price.specnum, 2, MidpointRounding.AwayFromZero);
                                cart.products.Add(pro);
                            }
                        }
                        else
                        {
                            pro.price.total = Math.Round(pro.price.saleprice * pro.price.specnum, 2, MidpointRounding.AwayFromZero);
                            pro.price.origintotal = Math.Round(pro.price.originprice * pro.price.specnum, 2, MidpointRounding.AwayFromZero);
                            pro.PaySubAmt = Math.Round(pro.price.saleprice * pro.price.specnum, 2, MidpointRounding.AwayFromZero);
                            cart.products.Add(pro);
                        }
                    }
                    return cart;
                }
                catch (Exception ex)
                {
                    MainModel.ShowLog("购物车添加商品异常" + ex.Message, true);
                    return null ;
                }          
        }

        /// <summary>
        /// 获取所有优惠券  仅在购物车商品为空时调用
        /// </summary>
        /// <param name="listcoupon"></param>
        /// <returns></returns>
        public static List<OrderCouponVo> GetAllOrderCoupon()
        {
            try
            {
               // List<PromotionCoupon> listcoupon = ZhuiZhi_Integral_Scale_UncleFruit.Model.Promotion.PromotionCache.getInstance().getListcoupon();//查询出优惠券

                if (HalfOffLineUtil.listcoupon == null)
                {
                    HalfOffLineUtil.ListMemberCouponAvailable();
                }
                List<PromotionCoupon> listcoupon = HalfOffLineUtil.listcoupon;
                List<OrderCouponVo> availablecoupons = new List<OrderCouponVo>();
                if (listcoupon == null || listcoupon.Count == 0)
                {
                    return null;
                }

                foreach (ZhuiZhi_Integral_Scale_UncleFruit.Model.HalfOffLine.PromotionCoupon coupon in listcoupon)
                {
                    OrderCouponVo couponsBean = new OrderCouponVo();
                    //set 值
                    couponsBean.catalog = coupon.catalog;
                    couponsBean.enabledfrom = coupon.enabledfrom;
                    couponsBean.enabledto = coupon.enabledto;
                    couponsBean.couponcode = coupon.couponcode;
                    couponsBean.promotioncode = coupon.promotioncode;

                    couponsBean.orderminamount = coupon.orderminamount;
                    couponsBean.discountamt = coupon.discountamt;
                    couponsBean.amount = coupon.amount;
                    couponsBean.availableskudesc = coupon.availableskudesc;
                    couponsBean.exchangeconditioncontext = coupon.exchangeconditioncontext;
                    couponsBean.enabled = false;
                    availablecoupons.Add(couponsBean);
                }

                return availablecoupons;
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// 获取分页参数  (仅适用于表格形式 且 左上角单元格和右下角单元格作为 翻页按钮)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="total"></param>
        /// <param name="rowcount"></param>
        public static Paging GetPaging(int page,int pagesize,int total,int rowcount )
        {
            Paging paging = new Paging();
            paging.success = false;
            try
            {

                int startindex = 0;
                int lastindex = pagesize-1;
                int waitingcount = 0;

                bool havanextpage = false;
                bool havepreviousPage = false;
                if (page == 1)
                {
                    havepreviousPage = false;
                    startindex = 0;
                    if (total > pagesize)
                    {

                        lastindex = pagesize-2;
                        havanextpage = true;
                    }
                    else
                    {
                        lastindex = total - 1;
                        havanextpage = false;
                    }
                }
                else
                {
                    havepreviousPage = true;
                    waitingcount = total - ((page - 1) * (pagesize-2) + 1);  //第一页只有下一页  中间页都是上一页下一页 占用两个
                    startindex = (page - 1) * (pagesize - 2) + 1;

                    if (waitingcount > (pagesize-1))
                    {
                        lastindex = startindex + (pagesize-3);
                        havanextpage = true;
                    }
                    else
                    {
                        lastindex = total - 1;
                        havanextpage = false;
                    }
                }

                paging.success = true;
                paging.startindex = startindex;
                paging.endindex = lastindex;
                paging.haveuppage = havepreviousPage;
                paging.havedownpage = havanextpage;
                paging.makeupcount = rowcount- (lastindex - startindex + 1) % rowcount;

                return paging;

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("分页异常"+ex.Message);
                return paging;
            }
        }
    }

    public class Paging
    {
        /// <summary>
        /// 分页成功标识
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// 页数
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// 开始位置
        /// </summary>
        public int startindex { get; set; }
        /// <summary>
        /// 结束位置
        /// </summary>
        public int endindex { get; set; }
        /// <summary>
        /// 是否有上一页
        /// </summary>
        public bool haveuppage { get; set; }
        /// <summary>
        /// 是否有下一页
        /// </summary>
        public bool havedownpage { get; set; }
        /// <summary>
        /// 需要补充的空白数量 （不足一行需要补充）
        /// </summary>
        public int makeupcount { get; set; }
    }
}
