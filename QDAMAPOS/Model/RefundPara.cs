using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QDAMAPOS.Model
{
    public class RefundPara
    {
        /// <summary>
        /// 订单完成前退款 1
        /// 订单完成后退货退款 2
        /// 收银机发起的目前均传2 （2019-10-31）
        /// </summary>
        public int aftersaletype { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public long orderid { get; set; }

        /// <summary>
        /// 是否整单退款   true 是    false 否
        /// </summary>
        public bool allorder { get; set; }

        /// <summary>
        /// 选择部分退款的商品，仅在部分退款时需要传
        /// </summary>
        public List<OrderRefunditem> orderrefunditems { get; set; }

    }
    public class OrderRefunditem
    {
        /// <summary>
        /// 退款份数
        /// </summary>
        public decimal refundqty { get; set; }
        
        /// <summary>
        /// 商品明细行id
        /// </summary>
        public long orderitemid { get; set; }
        
        /// <summary>
        /// 商品编码对应列表返回商品对象的skucode
        /// </summary>
        public string goodsid { get; set; }

    }
}
