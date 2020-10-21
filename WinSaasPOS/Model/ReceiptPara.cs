using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WinSaasPOS.Model
{
    public class ReceiptPara
    {
        /// <summary>
        /// 整比订单取消次数
        /// </summary>
        public int cancelordercount { set; get; }
        /// <summary>
        /// 整笔取消次数
        /// </summary>
        public string cancelordertotalmoney { set; get; }
        /// <summary>
        /// 指定取消次数
        /// </summary>
        public int cancelsinglecount { set; get; }
        /// <summary>
        /// 指定取消商品金额
        /// </summary>
        public string cancelsingletotalmoney { set; get; }
        /// <summary>
        /// 结束时间  如果无 取当前时间
        /// </summary>
        public string endtime { set; get; }
        /// <summary>
        /// 打开钱包次数
        /// </summary>
        public int openmoneypacketcount { set; get; }
        /// <summary>
        /// 重打小票次数
        /// </summary>
        public int reprintcount { set; get; }
        /// <summary>
        /// 门店ID
        /// </summary>
        public string shopid { set; get; }
    }
}
