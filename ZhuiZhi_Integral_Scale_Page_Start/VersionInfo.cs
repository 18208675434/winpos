using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace ZhuiZhi_Integral_Scale_UncleFruit_Start
{

        public class VersionInfo
        {
            public string[] testsn { get; set; }
            public string devicetype { get; set; }
            public Apkversiondto apkversiondto { get; set; }
            public long createdat { get; set; }
            public string createdby { get; set; }
        }

        public class Apkversiondto
        {
            public string version { get; set; }
            public string url { get; set; }
            public string description { get; set; }
            public string versionmin { get; set; }
            public string winfilestr { get; set; }
        }

    
}
