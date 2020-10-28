using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Model
{


    public class OffLineOrder
    {
        public decimal cashpayamt { get; set; }
        public decimal changehandleamt { get; set; }
        public string createurlip { get; set; }
        public string devicecode { get; set; }
        public int fromwinpos { get; set; }
        public int hasrefunded { get; set; }
        public string offlineorderid { get; set; }
        public long orderat { get; set; }
        public long payat { get; set; }
        public decimal origintotal { get; set; }
        public string outercode { get; set; }
        public decimal  pricetotal { get; set; }
        public List<Product> products { get; set; }
        public decimal  promoamt { get; set; }
        public string promotioncode { get; set; }
        public long refundtime { get; set; }
        public string salesclerkphone { get; set; }
        public string shopid { get; set; }

        

        public decimal cashchangeamt { get; set; }


        /// <summary>
        /// 购物车商品数量
        /// </summary>
        public int goodscount { get; set; }

        /// <summary>
        /// 修改购物车价格
        /// </summary>
        public decimal fixpricetotal { get; set; }


        /// <summary>
        /// //指定优惠    =  原来离线订单应付  -  修改后的价格
        /// </summary>
        public decimal fixpricepromoamt { get; set; }
    }


}
