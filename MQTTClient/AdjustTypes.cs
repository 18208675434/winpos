using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTTClient
{
    public class AdjustTypes
    {
        /// <summary>
        /// 类型 1：上下架    2：调价
        /// </summary>
        public int adjustTypes { get; set; }

        public string shopId { get; set; }

        public string tenantId { get; set; }
    }
}
