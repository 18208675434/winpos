using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinSaasPOS.Model.HalfOffLine
{
    /// <summary>
    /// 获取积分规则
    /// 
    /// </summary>
    public class TenantCreditConfig
    {
        /**
         * 是否开通了积分服务-默认不开通
         */
        public bool creditenable { get; set; }

        // 单笔订单积分抵扣数量上限--每个用户每笔订单最多使用多少积分
        public Decimal pointmaxamount { get; set; }

        // 单笔订单积分抵扣最低限度--用户积分低于多少分时，不可使用积分抵扣
        public Decimal pointminamount { get; set; }

        // 积分兑换金额比例--多少积分 = 1元
        public Decimal pointperrmb { get; set; }

        // 积分抵扣订单金额最大比例--每笔订单可使用积分抵扣订单金额的百分之多少
        public Decimal pointmaxratio { get; set; }

        // 消费金额获取积分比例--1块钱获取积分数
        public Decimal earnpointperrmb { get; set; }

    }

}
