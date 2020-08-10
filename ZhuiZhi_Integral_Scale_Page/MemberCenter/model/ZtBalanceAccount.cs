using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model
{
    public class ZtBalanceAccount
    {
            /// <summary>
            /// 
            /// </summary>
            public decimal balance { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public bool haspaypassword { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public long memberid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public bool needinitpassword { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string tenantid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public decimal totalreward { get; set; }
        

    }
}
