using Maticsoft.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WinSaasPOS_Scale.Model
{

    public class Scale
    {
        public List<DBSWITCH_KEY_BEANMODEL> rows { get; set; }
        public string total { get; set; }
        public int page { get; set; }
        public int size { get; set; }
    }

    public class Row
    {
        public string id { get; set; }
        public string shopid { get; set; }
        public string skucode { get; set; }
        public string skuname { get; set; }
        public string keyplanid { get; set; }
        public string keyplanname { get; set; }
        public string scaleip { get; set; }
        public string scaletype { get; set; }
        public string scaletypename { get; set; }
        public string keyno { get; set; }
        public string yno { get; set; }
        public string xno { get; set; }
        public string pno { get; set; }
    }





    public class ScaleForSaas
    {
        public string requestedat { get; set; }
        public Templist templist { get; set; }
    }

    public class Templist
    {
        public int page { get; set; }
        public List<DBSCALE_KEY_BEANMODEL> rows { get; set; }
        public int size { get; set; }
        public int total { get; set; }
    }



}
