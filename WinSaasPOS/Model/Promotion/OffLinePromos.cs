using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinSaasPOS.Model.Promotion
{
    [Serializable]
   public  class OffLinePromos
    {
        public String costcenterinfo{get;set;}//": "string",促销成本分摊
        public String outerpromocode { get; set; }//":"string",外部促销编号 可为空
        public String promoaction { get; set; }//":"string",促销应用类型
        public Decimal promoamt { get; set; }//":0,促销产生的优惠金额
        public String promosubtype { get; set; }//":"string",促销子类
        public String promotioncode { get; set; }//":"string",促销编号
        public String promotype { get; set; }//":"string"促销大类  
    }
}
