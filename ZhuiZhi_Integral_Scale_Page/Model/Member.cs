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
        /// <summary>
        /// 是否使用积分  主界面勾选/取消勾选积分修改状态
        /// </summary>
        public bool isUsePoint { get; set; }
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
}
