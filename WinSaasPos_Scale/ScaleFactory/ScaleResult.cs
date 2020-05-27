using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinSaasPOS_Scale.ScaleFactory
{
    public class ScaleResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool WhetherSuccess { get; set; }

        /// <summary>
        /// 是否稳定
        /// </summary>
        public bool WhetherStable { get; set; }
        /// <summary>
        /// 是否零位
        /// </summary>
        public bool WhetherZero { get; set; }
        /// <summary>
        /// 是否有皮重
        /// </summary>
        public bool WhetherTare { get; set; }
        /// <summary>
        /// 返回code
        /// </summary>
        public int ResultCode { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 总重量
        /// </summary>
        public decimal TotalWeight { get; set; }
        /// <summary>
        /// 皮重
        /// </summary>
        public decimal TareWeight { get; set; }
        /// <summary>
        /// 净重
        /// </summary>
        public decimal NetWeight { get; set; }
    }
}
