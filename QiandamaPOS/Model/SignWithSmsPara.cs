using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace QiandamaPOS.Model
{
    public class SignPara
    {

            public string imgcode { get; set; }
            public string imgcodekey { get; set; }
            public string password { get; set; }
            public string phone { get; set; }
            public Poslastonlineuserinfo poslastonlineuserinfo { get; set; }
            public bool rememberme { get; set; }
            public string smscode { get; set; }
            public string token { get; set; }
            public string type { get; set; }
            public string username { get; set; }
        }

        public class Poslastonlineuserinfo
        {
            public string devicesn { get; set; }
            public DateTime endat { get; set; }
            public string mobile { get; set; }
            public string receiptinfo { get; set; }
        }

    
}
