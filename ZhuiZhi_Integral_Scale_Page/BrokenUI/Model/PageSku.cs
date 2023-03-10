using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.BrokenUI.Model
{
    public class PageSku
    {
        public int page { get; set; }
        public int size { get; set; }
        public int total { get; set; }
        public List<BrokenSku> rows { get; set; }
    }


    public class BrokenSku
    {
        public  int actiontype { get; set; }
        public string categoryid { get; set; }
        public string categoryname { get; set; }
        public string createdat { get; set; }
        public string createdby { get; set; }
        public decimal deliveryprice { get; set; }
        public decimal deliveryquantity { get; set; }
        public int id { get; set; }
        public string salesunit { get; set; }
        public string skucode { get; set; }
        public string skuname { get; set; }
        public int skutype { get; set; }
        public string spec { get; set; }
        public string tenantid { get; set; }
        public string updatedat { get; set; }
        public string updatedby { get; set; }
        public int warehouseotherdeliveryid { get; set; }
        public bool weightflag { get; set; }
    }


    public class ParaPageSku
    {
        public string categoryid { get; set; }
        public bool needdetail { get; set; }
        public int page { get; set; }
        public bool pagination { get; set; }
        public int size { get; set; }
        public string skucode { get; set; }
        public List<string> skucodes { get; set; }
        public string skuname { get; set; }
        public string sortdirection { get; set; }
        public string sorttype { get; set; }
        public int startIndex { get; set; }
        public string tenantid { get; set; }
        public int warehouseotherdeliveryid { get; set; }
    }

}
