using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Model
{
    public class PrinterPickOrderInfo
    {
        public String tenantid{set;get;}
        public String tenantname{set;get;}
        public String orderid{set;get;} //订单编号
        public String shopid{set;get;}
        public String shopname{set;get;} //门店名称
        public String address{set;get;}  //配送地址
        public String tel{set;get;}      //用户电话
        public String date{set;get;}     //下单时间
        public String username{set;get;} //用户名称
        public List<PickProduct> productdetaillist { set; get; } //商品信息
       //public PrinterInfo printerInfo{set;get;} //打印机信息
        public String productamt{set;get;} //商品金额"{set;get;}
        public String deliveryamt{set;get;}//配送费
        public String promoamt{set;get;} //活动优惠
        public String couponamt{set;get;} //优惠券抵
        public String pointpayamt{set;get;} //积分抵现"{set;get;}
        public String totalpayment{set;get;} //实付金额"{set;get;}
        public String pickcode{set;get;}//提货码
        public String pickname{set;get;}
        public String remark{set;get;} //备注
        public String expecttimedesc{set;get;} //期望送达时间
        public String serialcode{set;get;} //序号
        public int ordertype{set;get;} //订单类型
        public String createdby{set;get;}
        public String source{set;get;}
        public String remindmsg{set;get;}
        public String saymsg{set;get;}
        public int printstatus{set;get;}
    }

    public class PickProduct
    {
        public List<PickProduct> addiniteminfos { set; get; }

        public List<string> changeinfos { set; get; }
        public string money { set; get; }

        public string num { get; set; }
        public string price { get; set; }
        public string skuname { get; set; }
    }

}
