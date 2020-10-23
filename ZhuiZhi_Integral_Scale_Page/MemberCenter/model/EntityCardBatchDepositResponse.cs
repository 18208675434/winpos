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
}
