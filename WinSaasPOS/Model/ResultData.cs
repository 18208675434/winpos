using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WinSaasPOS.Model
{
    public class ResultData
    {
        /// <summary>
        /// 状态码  接口正常返回时为0
        /// </summary>
        public int code { set; get; }
       /// <summary>
       /// 错误信息
       /// </summary>
        public string message { set; get; }
        /// <summary>
        /// 接口返回具体信息的json
        /// </summary>
        public object data{set;get;}
        /// <summary>
        /// 时间戳
        /// </summary>
        public long now{set;get;}
    }
}
