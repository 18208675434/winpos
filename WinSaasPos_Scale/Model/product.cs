using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WinSaasPOS_Scale.Model
{
    public class product
    {
        /// <summary>
        /// 商品编码
        /// </summary>
        public string skucode { set; get; }
        /// <summary>
        /// 购买数量
        /// </summary>
        public decimal num { set; get; }
        /// <summary>
        /// 规格参数
        /// </summary>
        public int spectype { set; get; }
        /// <summary>
        /// 规格数量
        /// </summary>
        public string specnum { set; get; }
        /// <summary>
        /// 商品标识 0为标品 1为散称
        /// </summary>
        public int goodstagid { set; get; }
        /// <summary>
        /// 散称
        /// </summary>
        public string barcode { set; get; }

    }
}
