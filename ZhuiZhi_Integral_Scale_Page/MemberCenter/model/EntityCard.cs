using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model
{
    public class EntityCard
    {
        public decimal balance { get; set; }//余额 ,
        public string batchnumid { get; set; }//批量Id ,
        public string createdat { get; set; }// 创建时间 ,
        public string createdby { get; set; }//创建人 ,
        public decimal depositbalance { get; set; }// 本金 ,
        public string devicesn { get; set; }//开卡设备号 ,
        public string enjoycredit { get; set; }// 是否积分 ,
        public string enjoymemberprice { get; set; }//是否享受会员价 ,
        public string id { get; set; }//
        public string makecardshopid { get; set; }//制卡门店号 ,
        public string memberid { get; set; }//会员号 ,
        public string mobile { get; set; }// 手机号 ,
        public string outcardid { get; set; }// 卡号 ,
        public string password { get; set; }//密码 ,
        public decimal rewardbalance { get; set; }//赠金 ,
        public string shopid { get; set; }//开卡门店号 ,
        public string status { get; set; }//状态 ,
        public string tenantid { get; set; }//商户号 ,
        public string type { get; set; }//类型 ,
        public string updatedat { get; set; }//更新时间 ,
        public string updatedby { get; set; }//更新人
    }
}
