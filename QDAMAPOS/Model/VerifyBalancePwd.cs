using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QDAMAPOS.Model
{
    public class VerifyBalancePwd
    {

        /// <summary>
        /// 1:成功   其他失败
        /// </summary>
        public int success { get; set; }

        /// <summary>
        /// 安全码
        /// </summary>
        public string securitycode { get; set; }

        /// <summary>
        /// 错误次数
        /// </summary>
        public int wrongcount { get; set; }

        /// <summary>
        /// 剩余输入次数
        /// </summary>
        public int remainwrongcount { get; set; }

        /// <summary>
        /// 错误提示
        /// </summary>
        public string hint { get; set; }

    }
}


