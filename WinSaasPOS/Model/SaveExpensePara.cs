using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WinSaasPOS.Model
{
    public  class SaveExpensePara
    {
        public string expenseid { set; get; }

        public decimal expensefee { set; get; }

        public string shopid { set; get; }
    }
}
