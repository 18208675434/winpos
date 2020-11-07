using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model
{
    public class EntityCardBatchDepositResponse
    {
        public string batchoperatorid { get; set; } //批操作id ,
        public List<EntityCardBatchDepositDetail> depositdetails { get; set; } //充值结果明细
    }
    public class EntityCardBatchDepositDetail
    {
        public decimal capitalamount { get; set; } // 充值本金 ,
        public string cardno { get; set; } //卡号 ,
        public string depositbillid { get; set; } //充值订单id ,
        public string memberid { get; set; } // 会员id ,
        public decimal rewardamount { get; set; } //赠金
    }

    public class EntityCardBatchDepositLine
    {
        public decimal amount { get; set; } //
        public bool autorewardbill { get; set; } //
        public decimal balance { get; set; } //
        public string couponname { get; set; } //
        public string couponsendat { get; set; } //
        public string createdat { get; set; } //
        public string createdby { get; set; } //
        public string customerpaycode { get; set; } //
        public string deposittype { get; set; } //
        public string gateway { get; set; } //
        public string id { get; set; } //
        public decimal incomeamount { get; set; } // 第三方入账金额 ,
        public string memberid { get; set; } //
        public string outerbillnumber { get; set; } //
        public string payexpiredat { get; set; } //
        public string paymode { get; set; } //
        public string paymodeforapi { get; set; } // 支付方式 ,
        public decimal payrate { get; set; } // 第三方支付费率 ,
        public decimal payserviceamount { get; set; } // 支付手续费 ,
        public string phone { get; set; } //
        public decimal rechargerate { get; set; } //储值服务管理费率 ,
        public decimal rechargeserviceamount { get; set; } // 储值服务管理费 ,
        public string remark { get; set; } //
        public decimal rewardamount { get; set; } //
        public string rewardcoupon { get; set; } //
        public string shopid { get; set; } //
        public string status { get; set; } //
        public string tenantid { get; set; } //
    }
}
