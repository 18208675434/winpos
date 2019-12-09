using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WinSaasPOS.Model
{
    [Serializable]
    public class Price
    {
        /// <summary>
        /// 原价
        /// </summary>
        public decimal originprice { get; set; }
        /// <summary>
        /// 原价描述
        /// </summary>
        public string originpricedesc { get; set; }
        /// <summary>
        /// 原价小计
        /// </summary>
        public decimal origintotal { get; set; }
        /// <summary>
        /// 优惠金额 含订单优惠和单品优惠
        /// </summary>
        public decimal promoamt { get; set; }
        /// <summary>
        /// 售价
        /// </summary>
        public decimal saleprice { get; set; }
        /// <summary>
        /// 售价描述
        /// </summary>
        public string salepricedesc { get; set; }
        /// <summary>
        /// 规格重量
        /// </summary>
        public decimal specnum { get; set; }
        /// <summary>
        /// 是否需要删除线 1为是 0 为否
        /// </summary>
        public decimal strikeout { get; set; }
        /// <summary>
        /// 售价小计
        /// </summary>
        public decimal total { get; set; }

        /// <summary>
        /// 销售单位
        /// </summary>
        public string unit { get; set; }


        /// <summary>
        /// 限购文案，不为空就显示
        /// </summary>
        public string purchaselimitdesc { get; set; }

        /// <summary>
        /// 限购文案  不为空？  点击显示弹窗温馨提示
        /// 
        /// </summary>
        public string purchaselimitsubdesc { get; set; }

    }
}
