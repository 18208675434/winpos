using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model
{
    public class CreateMemberPara
    {
        public string birthday { get; set; }
        public string gender { get; set; }

       [System.ComponentModel.DefaultValue(null)]
        public string mobile { get; set; }

        [System.ComponentModel.DefaultValue(null)]
        public string nickname { get; set; }

        //public string oldcardid { get; set; }

        //public List<long> tagids { get; set; }

        //public string newcardid { get; set; }

        public string memberid { get; set; }

    }
}
