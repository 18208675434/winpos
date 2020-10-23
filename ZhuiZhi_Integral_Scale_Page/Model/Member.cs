using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Model
{
    public class Member
    {
        /// <summary>
        /// 会员号
        /// </summary>
        public string memberid { get; set; }
        /// <summary>
        /// memberheaderresponsevo 收银机会员查询对象
        /// </summary>
        public Memberheaderresponsevo memberheaderresponsevo { get; set; }
        /// <summary>
        /// memberheaderresponsevo 收银机会员查询对象
        /// </summary>
        public Memberinformationresponsevo memberinformationresponsevo { get; set; }
        public Creditaccountrepvo creditaccountrepvo { get; set; }

        public Barcoderecognitionresponse barcoderecognitionresponse { get; set; }

        public Membertenantresponsevo membertenantresponsevo { get; set; }

        public MemberOrderResponsevo memberorderresponsevo { get; set; }

        public MemberEntityCardResponsevo memberentitycardresponsevo { get; set; }

        public List<OutEntityCardResponseDto> outentitycards { get; set; }

        public MergeMemberRecordResponseVo mergememberrecordresponsevo { get; set; }

        /// <summary>
        /// 是否使用积分  主界面勾选/取消勾选积分修改状态
        /// </summary>
        public bool isUsePoint { get; set; }

        /// <summary>
        /// 登录会员凭证  （用于区分手机号 和实体卡登录）
        /// </summary>
        public string entrancecode { get; set; }
    }

    public class Memberheaderresponsevo
    {
        /// <summary>
        /// 会员号
        /// </summary>
        public string memberid { get; set; }
        /// <summary>
        /// 钱大妈会员号
        /// </summary>
        public string outmemberid { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public long signuptime { get; set; }
        /// <summary>
        /// 外部注册时间
        /// </summary>
        public long outsignuptime { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        public string token { get; set; }
    }

    public class Memberinformationresponsevo
    {
        /// <summary>
        /// 会员号
        /// </summary>
        public string memberid { get; set; }
        /// <summary>
        /// 性别（0:男 1：女)
        /// </summary>
        public int gender { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public long birthday { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string birthdaystr { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// LOGO
        /// </summary>
        public string logo { get; set; }
        /// <summary>
        /// 微信昵称
        /// </summary>
        public string wechatnickname { get; set; }
        /// <summary>
        /// 是否今天生日
        /// </summary>
        public bool onbirthday { get; set; }
    }

    public class Creditaccountrepvo
    {
        public string id { get; set; }
        public string memberid { get; set; }
        public decimal availablecredit { get; set; }
        public decimal totalcredit { get; set; }
        public bool freezable { get; set; }

        public decimal creditworth { get; set; }

    }


    public class Barcoderecognitionresponse
    {
        public string memberid { get; set; }
        public decimal balance { get; set; }
    }

    public class Membertenantresponsevo
    {
        public string memberid { get; set; }
        public string tenantid { get; set; }

        public string shopid { get; set; }
        public int gender { get; set; }
        public long birthday { get; set; }
        public long firstorderat { get; set; }
        public bool onbirthday { get; set; }
    }

    /// <summary>
    /// 2020-08-19 add 显示最后一次消费时间  和最近一个月消费次数
    /// </summary>
    public class MemberOrderResponsevo
    {
        public string lastpayat { get; set; }

        public string paycount { get; set; }
    }

    public class MemberEntityCardResponsevo
    {
        public string memberid { get; set; }
        public string tenantid { get; set; }
        public string cardid { get; set; }
        public string status { get; set; }
    }

    public class OutEntityCardResponseDto
    {
        public decimal balance { get; set; } // 余额
        public string batchnumid { get; set; } // 批量Id
        public string createdat { get; set; } //创建时间
        public string createdby { get; set; } //创建人
        public decimal depositbalance { get; set; } // 本金
        public string devicesn { get; set; } //开卡设备号
        public bool enjoycredit { get; set; } //是否积分
        public bool enjoymemberprice { get; set; } //是否享受会员价
        public string id { get; set; } //id
        public string makecardshopid { get; set; } //制卡门店号
        public string memberid { get; set; } //会员号
        public string mobile { get; set; } // 手机号
        public string outcardid { get; set; } //卡号
        public string password { get; set; } // 密码
        public decimal rewardbalance { get; set; } //赠金
        public string shopid { get; set; } //开卡门店号
        public string status { get; set; } //状态
        public string tenantid { get; set; } //商户号
        public string type { get; set; } //类型
        public string updatedat { get; set; } //更新时间
        public string updatedby { get; set; } //更新人
    }

    public class MergeMemberRecordResponseVo
    {
        public string createdat { get; set; }
        public string createdby { get; set; }
        public string id { get; set; }
        public decimal sourcebalance { get; set; }
        public decimal sourcecredit { get; set; }
        public string sourcememberid { get; set; }
        public string sourcemobile { get; set; }
        public decimal targetbalance { get; set; }
        public decimal targetcredit { get; set; }
        public string targetmemberid { get; set; }
        public string targetmobile { get; set; }
        public string tenantid { get; set; }
        public string updatedat { get; set; }
        public string updatedby { get; set; }
    }
}
