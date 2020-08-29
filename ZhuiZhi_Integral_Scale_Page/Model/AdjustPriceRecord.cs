using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Model
{
    public class AdjustPriceRecord
    {  
        /// <summary>
        /// 
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<AdjustPriceRecordItem> rows { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int size { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int total { get; set; }
    }


    public class AdjustPriceRecordItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string adjustpricedate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal aftermemberprice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal aftersalesprice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal beforememberprice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal beforeprice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal beforesalesprice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal pricetype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string querydate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string skucode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string skuname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int sourcetype { get; set; }
    }


    public class AdjustPricePara
    {
        /// <summary>
        /// 
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int datetype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool needdetail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool pagination { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string querydate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string shopid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int size { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sortdirection { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sorttype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int startIndex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tenantid { get; set; }
    }

}
