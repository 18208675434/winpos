using Maticsoft.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinSaasPOS.Model.Promotion
{
    public class PromotionsInvalidBean
    {
        public List<DBPROMOTION_CACHE_BEANMODEL> promos { get; set; }

        public List<string> invalidcategories { get; set; }

        public List<string> invalidskucodes { get; set; }

    }
}
