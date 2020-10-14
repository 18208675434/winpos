using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model
{
    public class EntityCardMoveRequest
    {
        public decimal amount { get; set; }
        public bool autoreward { get; set; }
        public string birthday { get; set; }
        public string customerpaycode { get; set; }
        public string memberid { get; set; }
        public string nickname { get; set; }
        public string oldentitycardnumber { get; set; }
        public string paymode { get; set; }
        public string phone { get; set; }
        public string remark { get; set; }
        public decimal rewardamount { get; set; }
        public string shopid { get; set; }
        public string smscode { get; set; }
    }
}
