using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.GiftCard.Model
{
    public class GiftCardPrintDetail
    {
        /// <summary>
        /// 果叔台北路店
        /// </summary>
        public string shop { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string orderid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string memberid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string customerphone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string orderat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal productcount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<PrintProduct> products { get; set; }
        /// <summary>
        /// 礼品卡已自动激活，不支持退款
        /// </summary>
        public string paydetails { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal pspamt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cashier { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string devicecode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<PayinfoItem> payinfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<PaydetailItem> paydetail { get; set; }

        public bool isRefound { get; set; }
    }

    public class PayinfoItem
    {
        /// <summary>
        /// 现金支付
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal amount { get; set; }
    }

    public class PaydetailItem
    {
        /// <summary>
        /// 现金支付
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string amount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int highlight { get; set; }
    }

    public class PrintProduct
    {
        /// <summary>
        /// 
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cardno { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal pspamt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal num { get; set; } 
    }
 
}
