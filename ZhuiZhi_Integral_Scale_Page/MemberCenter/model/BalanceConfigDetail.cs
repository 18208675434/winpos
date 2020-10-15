using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model
{
    public class BalanceConfigDetail
    {
        public string balanceonlyforpos { get; set; }

        public bool cashrechargeenableforpos { get; set; }

        public bool initdefaultpassword { get; set; }

        public bool littleamountwithoutpasswordpayenabled { get; set; }

        /// <summary>
        /// 现金找零转存余额开关
        /// </summary>
        public bool cashtobalanceenable { get; set; }

        /// <summary>
        /// 是否支持自定义充值
        /// </summary>
        public bool customrechargeenable { get; set; }


    }
}
