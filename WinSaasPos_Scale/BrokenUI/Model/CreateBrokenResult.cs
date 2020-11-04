using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinSaasPOS_Scale.BrokenUI.Model
{

    public class CreateBrokenResult
    {
        public string createdat { get; set; }
        public List<Itemlist> itemlist { get; set; }
        public decimal totalamt { get; set; }
        public decimal totalqty { get; set; }
        public string username { get; set; }
    }

    public class Itemlist
    {
        public decimal deliveryprice { get; set; }
        public decimal deliveryquantity { get; set; }
        public string salesunit { get; set; }
        public string skucode { get; set; }
        public string skuname { get; set; }
        public decimal totalamount { get; set; }
        public bool weightflag { get; set; }
    }




    public class ParaCreateBroken
    {
        public List<Item> itemlist { get; set; }
        public string shopid { get; set; }
    }

    public class Item
    {
        public decimal deliveryquantity { get; set; }
        public string skucode { get; set; }
    }


}
