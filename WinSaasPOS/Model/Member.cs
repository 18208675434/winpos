﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WinSaasPOS.Model
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
        public string memberid { get; set; }
        public decimal availablecredit { get; set; }
    }

}
