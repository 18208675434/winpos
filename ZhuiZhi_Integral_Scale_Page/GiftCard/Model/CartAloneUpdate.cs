using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.GiftCard.Model
{

    public class CartAloneUpdate
    {
        /// <summary>
        /// 
        /// </summary>
        public string carttype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal givechangeamt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string memberid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal pcashpayamt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal ppaidamt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<CardProduct> products { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal pspamt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<CardProduct> requestproducts { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int salechannel { get; set; }
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
