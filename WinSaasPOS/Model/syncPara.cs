﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WinSaasPOS.Model
{
    public class syncPara
    {
        /// <summary>
        /// 付款码
        /// </summary>
        public string payid { set; get; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string orderid { set; get; }
    }
}
