using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.GiftCard.Model
{
    public class GiftCardRecord
    {
        /// <summary>
        /// 
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<RowsItem> rows { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int size { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int total { get; set; }
    }

    public class RowsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string cancelat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cashier { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string createdat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string createdby { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long customerid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string customername { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string customerphone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string devicecode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string orderat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ordercategory { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string orderstatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string payat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string paymenttype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal pcashpayamt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal pgivechangeamt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal ppaidamt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal pspamt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string shopid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tenantid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal thirdpartypaycharge { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal thirdpartypayrate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string updatedat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string updatedby { get; set; }
    }

}
