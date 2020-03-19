using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace QDAMAPOS.Model
{
    public class QueryReceiptPara
    {
        /// <summary>
        /// 交班日期 yyyy-MM-dd
        /// </summary>
        public string operatetimestr { set; get; }
        /// <summary>
        /// 门店号
        /// </summary>
        public string shopid { set; get; }
        /// <summary>
        /// 设备ID
        /// </summary>
        public string deviceid { set; get; }
    }
}
