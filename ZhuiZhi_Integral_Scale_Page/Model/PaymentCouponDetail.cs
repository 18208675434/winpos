using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Model
{
    public class PaymentCouponDetail
    {

        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tenantid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string couponid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string couponcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string fromouter { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal amount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string orderid { get; set; }
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
        public List<string> availablecustomerpaycode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool needpassword { get; set; }

    }
}
