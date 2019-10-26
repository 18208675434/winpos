using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace QiandamaPOS.Model
{
    public class CartPara
    {
        /// <summary>
        /// 现金支付时传入的收取现金金额（非必填）
        /// </summary>
        public decimal cashpayamt { set; get; }
        /// <summary>
        /// 是否使用现金支付  1为是 0为否（非必填）
        /// </summary>
        public int cashpayoption { set; get; }
        /// <summary>
        /// 订单子类型 收音机传pos
        /// </summary>
        public string ordersubtype { set; get; }
        /// <summary>
        /// 是否使用积分抵扣 1为是 0或空为否（非必填）
        /// </summary>
        public int pointpayoption { set; get; }
        /// <summary>
        /// 现金券金额传入（非必填）
        /// </summary>
        public decimal cashcouponamt { set; get; }
        /// <summary>
        /// 请求购物车的商品集合
        /// </summary>
        public product[] products { set; get; }
        /// <summary>
        /// 门店ID，当前下单门店号
        /// </summary>
        public string shopid { set; get; }
        /// <summary>
        /// 用户ID，配合usertoken使用，校验当前用户登录是否有效（非必填）
        /// </summary>
        public string uid { set; get; }
        /// <summary>
        /// 用户收音机登录的获取到的token数据，用于下单校验（非必填）
        /// </summary>
        public string usertoken { set; get; }

        /// <summary>
        /// 是否自动选中优惠券1 自动选择最优优惠券 0 不自动选中
        /// </summary>
        public int autocoupon { set; get; }

        /// <summary>
        /// 选中的优惠券号，字符数组，目前仅支持一次选中一张优惠券
        /// </summary>
        public string[] selectedcoupons { set; get; }
    }

}
