using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.GiftCard.Model
{
    public class CartAloneUpdatePara
    {
        /// <summary>
        /// 
        /// </summary>
        public string carttype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long memberid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal pcashpayamt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<CardProduct> requestproducts { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long salechannel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string shopid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tenantid { get; set; }
    }

  
}
