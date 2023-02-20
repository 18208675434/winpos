using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Common
{
    /// <summary>
 /// fmj
 /// 2023/2/20
 /// 用于界面折扣修改的工具类
 /// </summary>
    public class ChangePriceUtil
    {
        /// <summary>
        /// 价格比较限制
        /// </summary>
        /// <param name="nowprice">比较价格</param>
        /// <param name="totalprice">总价</param>
        /// <param name="lessthen">大于小于限制</param>
        /// <returns></returns>
        public static bool PriceImpose(decimal nowprice, decimal totalprice, Lessthen lessthen)
        {
            switch (lessthen)
            {
                case Lessthen.THEN:
                    return nowprice > ThenPriceRange(totalprice);
                case Lessthen.LESS:
                    return nowprice < ThenPriceRange(totalprice);
            }

            return false;
        }

        /// <summary>
        /// 获取最高可减金额
        /// </summary>
        /// <param name="totalprice"></param>
        /// <returns></returns>
        public static decimal ThenPriceRange(decimal totalprice)
        {
            return totalprice * (MainModel.CurrentShopInfo.posalterorderdiscountrange / 10);
        }
    }

    /// <summary>
    /// 大于小于
    /// </summary>
    public enum Lessthen
    {
        /// <summary>
        /// 小于
        /// </summary>
        LESS,
        /// <summary>
        /// 大于
        /// </summary>
        THEN
    }
}
