using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace QDAMAPOS.Model
{

        public class ExpenseType
        {
            public string id { get; set; }
            public string expensesid { get; set; }
            public string description { get; set; }
            public string inputflag { get; set; }
            public string visibleflag { get; set; }
            public string type { get; set; }
            public long createdat { get; set; }
            public string createdby { get; set; }
        }

    
}
