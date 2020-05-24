using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinSaasPOS_Scale.BrokenUI.Model
{
    public class SkuMovePrice
    {
        /// <summary>
        /// 价格
        /// </summary>
        public decimal deliveryprice { get; set; }
        /// <summary>
        /// 商品编码
        /// </summary>
        public string skucode { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string skuname { get; set; }
        /// <summary>
        /// 称重标识
        /// </summary>
        public bool weightflag { get; set; }

    }
}