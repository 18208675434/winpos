using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Model
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

        /// <summary>
        /// 备用金
        /// </summary>
        public decimal sparecashamt { set; get; }

        /// <summary>
        /// 现有金
        /// </summary>
        public decimal cashactualamt { set; get; }

        public List<OrderPriceDetail> balancedepositinfo { set; get; }

        /// <summary>
        /// 会员充值 现金充值金额
        /// </summary>
        public string balancedepositcashamount { set; get; }
        /// <summary>
        /// 会员充值 充值退款订单金额
        /// </summary>
        public decimal depositrefundamt { set; get; }
        /// <summary>
        /// 会员充值 充值退款订单数量
        /// </summary>
        public int depositrefundcount { set; get; }
    }
}
