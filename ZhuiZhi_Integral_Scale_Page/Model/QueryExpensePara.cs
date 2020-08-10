using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Model
{
    public class QueryExpensePara
    {
        /// <summary>
        /// 今天 0 昨天 1 最近 7 天 最近 30 天
        /// </summary>
        public int intervaldays { set; get; }
       /// <summary>
       /// 后端返回的lastorderid，第一次请求传 0
       /// </summary>
        public long lastorderid { set; get; }

        /// <summary>
        /// 门店ID
        /// </summary>
        public string shopid { set; get; }

        /// <summary>
        /// 操作时间 yyyy-MM-dd
        /// </summary>
        public string operatetimestr { set; get; }

    }
}
