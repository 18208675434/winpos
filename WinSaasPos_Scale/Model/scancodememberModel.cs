using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WinSaasPOS_Scale.Model
{

    public class scancodememberModel
    {

        public Scancodedto scancodedto { get; set; }

        public Member memberresponsevo { get; set; }
        public bool skubarcodeflag { get; set; }

        /// <summary>
        ///SKU商品    MEMBER 会员
        /// </summary>
        public string type { get; set; }
    }

    public class Scancodedto
    {
        /// <summary>
        /// 商品编码
        /// </summary>
        public string skucode { get; set; }
        /// <summary>
        /// 商店ID
        /// </summary>
        public string shopid { get; set; }
        /// <summary>
        /// 商户ID
        /// </summary>
        public string tenantid { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string skuname { get; set; }
        /// <summary>
        /// 商品条码
        /// </summary>
        public string barcode { get; set; }
        /// <summary>
        /// 散称标识 true为散称
        /// </summary>
        public bool weightflag { get; set; }
        /// <summary>
        /// 份数
        /// </summary>
        public int num { get; set; }
        /// <summary>
        /// 规格数
        /// </summary>
        public decimal specnum { get; set; }
        /// <summary>
        /// 规格转换类型
        /// </summary>
        public int spectype { get; set; }
        public string salesunit { get; set; }
    }

}
