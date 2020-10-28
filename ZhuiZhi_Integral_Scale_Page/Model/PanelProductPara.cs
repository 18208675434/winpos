using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Model
{
    public class PanelProductPara
    {
        /// <summary>
        /// 标识用户是否登录；1为登录状态 0为未登录状态
        /// </summary>
        public int memberlogin { get; set; }
        public string shopid { get; set; }
        public List<Product> products { get; set; }
        public string usertoken { get; set; }

    }

    public class panelproduct
    {
        public string mainimg { get; set; }
        public string pricetag { get; set; }
        public int pricetagid { get; set; }
        public string saleunit { get; set; }
        public string skucode { get; set; }
        public string skuname { get; set; }
        public Price price { get; set; }
    }
}
