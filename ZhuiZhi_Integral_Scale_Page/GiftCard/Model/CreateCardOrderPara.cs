using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.GiftCard.Model
{

    public class CreateCardOrderPara
    {
        /// <summary>
        /// 
        /// </summary>
        public string carttype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cashier { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string devicesn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool fromwinpos { get; set; }
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
        public string phone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<CardProduct> products { get; set; }
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
