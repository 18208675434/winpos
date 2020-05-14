using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using WinSaasPOS.Model.Promotion;

namespace WinSaasPOS.Model
{



    [Serializable]
    public class orderpromotion
    {
        public string title { get; set; }
        public string amount { get; set; }
        public int highlight { get; set; }
    }





    [Serializable]
    public class Cart
    {

        public string title { get; set; }

        /// <summary>
        /// 促销计算后的金额
        /// </summary>
        public decimal payamt { get; set; }
        public decimal totalpayment { get; set; }
        public decimal origintotal{ get; set; }
        public List<Product> products { get; set; }
        public List<OrderPriceDetail> orderpricedetails { get; set; }
        public Tenant tenant { get; set; }
        public Pickupinfo pickupinfo { get; set; }
        public string shopid { get; set; }
        public decimal productcount { get; set; }
        public string ordertype { get; set; }
        public int ordersubtypeflag { get; set; }
        public int pointpayoption { get; set; }
        public int facepayoption { get; set; }
        public int cashpayoption { get; set; }


        /// <summary>
        /// 2019-11-25 是否使用会余额抵扣 1为是 0或空为否（非必填）
        /// </summary>
        public int balancepayoption { get; set; }

        /// <summary>
        /// 余额支付金额
        /// </summary>
        public decimal balancepayamt { get; set; }
        public decimal availablebalanceamount { set; get; }


        /// <summary>
        /// 2019-11-26 是否使用余额密码
        /// </summary>
        public int paypasswordtype { get; set; }

        /// <summary>
        /// 2019-11-26 余额密码输入RSA加密后的值
        /// </summary>
        public string paypassword { get; set; }


        public int selfpickenabled { get; set; }
        public List<Availablecoupon> availablecoupons { get; set; }
        public List<Unavailablecoupon> unavailablecoupons { get; set; }
        public Pointinfo pointinfo { get; set; }
        public int selectcouponcount { get; set; }
        public string orderplaceid { get; set; }
        public decimal promoamt { get; set; }
        public decimal producttotalamt { get; set; }
        public decimal couponpromoamt { get; set; }
        public decimal totalpromoamt { get; set; }
        public orderpromotion[] orderpromotions { get; set; }
        public decimal memberpromo { get; set; }
        public decimal membertotalpayment { get; set; }
        public int memberlogin { get; set; }
        public Paymenttypes paymenttypes { get; set; }
        public decimal cashpayamt { get; set; }
        public decimal cashchangeamt { get; set; }
        public decimal changehandleamt { get; set; }
        public decimal payamtbeforecash { get; set; }

        public decimal cashcouponamt { get; set; }


        /// <summary>
        /// 修改购物车价格前价格
        /// </summary>
        public decimal totalpaymentbeforefix { get; set; }

        /// <summary>
        /// 修改购物车价格
        /// </summary>
        public decimal fixpricetotal { get; set; }


        /// <summary>
        /// //指定优惠    =  原来离线订单应付  -  修改后的价格
        /// </summary>
        public decimal fixpricepromoamt { get; set; }


        //面板商品排序
        public SortType sorttype { get; set; }

        /// <summary>
        /// 购物车商品数量
        /// </summary>
        public int goodscount { get; set; }

        public Cart Clone()
        {
            throw new NotImplementedException();
        }

        public Cart qianClone()
        {
            return (Cart)this.MemberwiseClone();
        }

        //ADD 2020-05-11 半离线新增
        //前端计算的最终应付金额
        public decimal poscalculateamt { get; set; }
        //订单级别促销计算前商品总价 计算完单品后
        public decimal postotalbeforeordercalulateamt { get; set; }
        //订单级别促销优惠
        public decimal posorderpromoat { get; set; }
        //优惠券优惠金额
        public decimal poscouponpromoamt { get; set; }
        //积分优惠金额
        public decimal pospointpromoamt { get; set; }
        //使用积分
        public long pospointofuser { get; set; }

        public Dictionary<String, String> selectedcoupons { get; set; }


    }
    [Serializable]
    public class Tenant
    {
        public string tenantid { get; set; }
    }
    [Serializable]
    public class Pickupinfo
    {
        public string shopid { get; set; }
        public string shopname { get; set; }
        public string shopaddress { get; set; }
        public string shoptelephone { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string distance { get; set; }
    }
    [Serializable]
    public class Pointinfo
    {
        public long availablepoints { get; set; }
        public decimal availablepointsamount { get; set; }
        public string availablepointsmsg { get; set; }
        public long totalpoints { get; set; }
    }
    [Serializable]
    public class Paymenttypes
    {
        public int cashenabled { get; set; }
        public int onlineenabled { get; set; }
        public int facepayenabled { get; set; }
        public int balancepayenabled { get; set; }
        public int cashcouponpayenabled { get; set; }
    }
    [Serializable]
    public class Product
    {
        public string skucode { get; set; }
        public string title { get; set; }
        public int num { get; set; }
        public string mainimg { get; set; }
        public int selectstate { get; set; }
        public string specdesc { get; set; }
        public Price price { get; set; }
        public string barcode { get; set; }
        public int goodstagid { get; set; }
        public string goodstag { get; set; }
        public decimal specnum { get; set; }
        public int spectype { get; set; }
        public string liststyle { get; set; }
        public int pricetagid { get; set; }
        public string pricetag { get; set; }
        public int applyreturn { get; set; }
        public int withexchange { get; set; }
        public string[] tags { get; set; }

        //销售单位
        public string saleunit { get; set; }

        //商品名称
        public string skuname { get; set; }

        /// <summary>
        /// 一级分类ID
        /// </summary>
        public string firstcategoryid { get; set; }

        /// <summary>
        /// 一级分类名称
        /// </summary>
        public string firstcategoryname { get; set; }

        /// <summary>
        /// 二级分类ID
        /// </summary>
        public string secondcategoryid { get; set; }

        /// <summary>
        /// 级分类ID
        /// </summary>
        public string categoryid { get; set; }

        public string shopid { get; set; }

        public int isQueryBarcode { get; set; }

        public bool weightflag { get; set; }


        public Product ThisClone()
        {
            return (Product)this.MemberwiseClone();
        }


        /// <summary>
        /// 面板是否加载过（本地缓存防止一直调用接口）
        /// </summary>
        public bool isLoadPanel { get; set; }
        /// <summary>
        /// 面板商品显示图片
        /// </summary>
        public Bitmap panelbmp { get; set; }

        /// <summary>
        /// 面板商品选择数量
        /// </summary>
        public int panelSelectNum { get; set; }

        /// <summary>
        /// 面板商品销量排序用
        /// </summary>
        public int salecount { get; set; }

        /// <summary>
        /// 面板商品上新排序用 （时间戳）
        /// </summary>
        public long createdat { get; set; }


        public List<OffLinePromos> offlinepromos { get; set; }

        public decimal PaySubAmt { get; set; }
        public decimal PromoSubAmt { get; set; }


        public int RowNum = 0;


        //ADD 半离线新增 2020-05-11
        public bool canmixcoupon = true;//是否促销和优惠券可以混合使用

        public int purchaselimit; // >0 为限购 否则不限购


        //add 2020-05-13 半离线新增
        /// <summary>
        /// 原价
        /// </summary>
        public decimal originprice { get; set; }

        public TransitionPriceDetail transitionPriceDetail { get; set; }
    }


    [Serializable]
    public class Availablecoupon
    {
        public string catalog { get; set; }
        public decimal amount { get; set; }
        public string desc { get; set; }
        public string name { get; set; }
        public bool enabled { get; set; }
        public int ordersubtype { get; set; }
        public string promotionrealmtype { get; set; }
        public string sendruletype { get; set; }
        public string shoprealmdetail { get; set; }
        public string districtscope { get; set; }
        public string shopscope { get; set; }
        public string tenantid { get; set; }
        public decimal exchangepoints { get; set; }
        public int selectstate { get; set; }
        public decimal orderminamount { get; set; }
        public string ordersubtypedesc { get; set; }
        public long enabledfrom { get; set; }
        public long enabledto { get; set; }
        public string availableskudesc { get; set; }
        public string availableshopdesc { get; set; }
        public string promotioncode { get; set; }
        public string couponcode { get; set; }

        //ADD 半离线2019-05-12
        public Decimal discountamt = WinSaasPOS.Model.Promotion.CommonConstant.ZERODECIMAL;//券抵扣金额
    }
    [Serializable]
    public class Unavailablecoupon
    {
        public string catalog { get; set; }
        public decimal amount { get; set; }
        public string desc { get; set; }
        public string name { get; set; }
        public bool enabled { get; set; }
        public int ordersubtype { get; set; }
        public string promotionrealmtype { get; set; }
        public string sendruletype { get; set; }
        public string shoprealmdetail { get; set; }
        public string districtscope { get; set; }
        public string shopscope { get; set; }
        public string tenantid { get; set; }
        public decimal exchangepoints { get; set; }
        public string invalidreason { get; set; }
        public decimal orderminamount { get; set; }
        public string ordersubtypedesc { get; set; }
        public long enabledfrom { get; set; }
        public long enabledto { get; set; }
        public string availableskudesc { get; set; }
        public string availableshopdesc { get; set; }
        public string promotioncode { get; set; }
        public string couponcode { get; set; }
    }

        [Serializable]
    public class TransitionPriceDetail
    {
        //售价
            public Decimal originprice { get; set; }
        //促销类型
            public String priceKind { get; set; }

            public int pricetagid { get; set; }
            public String pricetag { get; set; }
            public Decimal pricepromoamt { get; set; }//折上折中间优惠价格
            public String code { get; set; }
            public String outercode { get; set; }
            public String costcenterinfo { get; set; }
            public String promoaction { get; set; }
            public String promosubtype { get; set; }
            public String promotype { get; set; }
    }
}
