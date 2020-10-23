using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model
{
    public class EntityCardBatchDepositRequest
    {
        public string customerpaycode { get; set; } //其他支付方式code ,
        public string operatorid { get; set; } //
        public string paymode { get; set; } //支付方式 0现金 1其他支付 2微信 或 支付宝 
        public List<EntityCardBatchDepositRequestDetail> requestdetails { get; set; } //支付明细
        public string shopid { get; set; } //门店id ,
        public string tenantid { get; set; } //
    }


    public class EntityCardBatchDepositRequestDetail
    {
        public bool autoreward { get; set; } //是否自动根据充值模板匹配赠金 ,
        public decimal capitalamount { get; set; } //本金 ,
        public string cardno { get; set; } // 卡号 ,
        public decimal rewardamount { get; set; } // 赠金
    }
}
