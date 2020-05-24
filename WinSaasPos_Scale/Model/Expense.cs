using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WinSaasPOS_Scale.Model
{
    public class Expense
    {

            public long createdat { get; set; }
            public string expensename { get; set; }
            public float expensefee { get; set; }
            public string createby { get; set; }

    }
}
