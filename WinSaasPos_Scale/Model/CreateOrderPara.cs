using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WinSaasPOS_Scale.Model
{
    public class CreateOrderPara
    {
        /// <summary>
        /// 是否自动选择一张最优惠的券（非必填）
        /// </summary>
        public int autocoupon { set; get; }
        /// <summary>
        /// 是否人脸识别 （非必填）
        /// </summary>
        public int facepayoption { set; get; }
        /// <summary>
        /// 订单子类型 收银机传pos
        /// </summary>
        public string ordersubtype { set; get; }
        /// <summary>
        /// 是否使用积分折扣 1为是 ，0或空为否
        /// </summary>
        public int pointpayoption { set; get; }

        /// <summary>
        /// 2019-11-25 是否使用会余额抵扣 1为是 0或空为否（非必填）
        /// </summary>
        public int balancepayoption { get; set; }

        /// <summary>
        /// 2019-11-26 是否使用余额密码
        /// </summary>
        public int paypasswordtype { get; set; }

        /// <summary>
        /// 2019-11-26 余额密码输入RSA加密后的值
        /// </summary>
        public string paypassword { get; set; }



        /// <summary>
        /// 是否使用现金支付 1为是 0或空为否
        /// </summary>
        public int cashpayoption { set; get; }
        /// <summary>
        /// 现金支付金额传入 （非必填）
        /// </summary>
        public decimal cashpayamt { set; get; }
        /// <summary>
        /// 现金券金额传入（非必填）
        /// </summary>
        public decimal cashcouponamt { set; get; }
        /// <summary>
        /// 应付金额
        /// </summary>
        public decimal pricetotal { set; get; }

        /// <summary>
        /// 商品数组
        /// </summary>
        public product[] products { set; get; }
        /// <summary>
        /// 门店ID，当前下单门店号
        /// </summary>
        public string shopid { set; get; }
        /// <summary>
        /// 用户ID，配合usertoken使用，检验当前用户登录是否有效（非必填）
        /// </summary>
        public string uid { set; get; }
        /// <summary>
        /// 用户 收音机登录获取到的token数据，用于下单校验（非必填）
        /// </summary>
        public string usertoken { set; get; }
        /// <summary>
        /// 放重复下单ID
        /// </summary>
        public string orderplaceid { set; get; }

        /// <summary>
        /// 选中的优惠券号，字符数组，目前仅支持一次选中一张优惠券
        /// </summary>
        public string[] selectedcoupons { set; get; }

        /// <summary>
        /// 是否为winpos请求，1为是  0或空为否
        /// </summary>
        public int fromwinpos { set; get; }

        /// <summary>
        /// 修改购物车价钱
        /// </summary>
        public decimal fixpricetotal { set; get; }

    }
}
