using Maticsoft.BLL;
using Maticsoft.Model;
using Newtonsoft.Json;
using WinSaasPOS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinSaasPOS.Common
{
   public class OrderUtil
    {
       
       public static bool CreaterOrder(Cart thisCurrentCart,decimal relaycash,  ref string offlineorderid)
       {
           try
           {
               offlineorderid = GetOrderID(thisCurrentCart);

               DBORDER_BEANMODEL order = new DBORDER_BEANMODEL();
               order.OFFLINEORDERID = offlineorderid;
               order.ORDER_JSON = "";
               order.ORDERAT = Convert.ToInt64(MainModel.getStampByDateTime(DateTime.Now));
               order.CUSTOMERPHONE = MainModel.CurrentUserPhone;
               order.TITLE = thisCurrentCart.products[0].title + "等共" + thisCurrentCart.goodscount + "件商品";
               order.PRICETOTAL = thisCurrentCart.totalpayment;
               
               order.ORDERSTATUS = "已完成";
               order.SYN_TIME = 0;
               order.ORDERSTATUSVALUE = 0;
               order.CREATE_TIME = Convert.ToInt64(MainModel.getStampByDateTime(DateTime.Now));
               order.CREATE_SN = MainModel.DeviceSN;
               order.CREATE_URL_IP = MainModel.URL;
               order.VERSION_CODE = 0;

               OffLineOrder ff = new OffLineOrder();

               
               ff.cashchangeamt = thisCurrentCart.cashchangeamt;
               ff.cashpayamt = thisCurrentCart.cashpayamt;
               ff.changehandleamt = thisCurrentCart.totalpayment - relaycash;
               ff.origintotal = thisCurrentCart.origintotal;
               ff.pricetotal = relaycash;
               ff.createurlip = MainModel.URL;
               ff.devicecode = MainModel.DeviceSN;
               ff.fromwinpos = 1;
               ff.hasrefunded = 0;
               ff.offlineorderid = offlineorderid;
               ff.orderat = Convert.ToInt64(MainModel.getStampByDateTime(DateTime.Now));
               ff.payat = Convert.ToInt64(MainModel.getStampByDateTime(DateTime.Now));
               ff.outercode = "";
               ff.products = thisCurrentCart.products;
               ff.promoamt = thisCurrentCart.promoamt;
               ff.promotioncode = "";
               ff.salesclerkphone = MainModel.CurrentUser.loginaccount;
               ff.shopid = MainModel.CurrentShopInfo.shopid;
               ff.goodscount = thisCurrentCart.goodscount;

               if (thisCurrentCart.fixpricetotal != null && thisCurrentCart.fixpricetotal > 0)
               {
                   ff.fixpricetotal = thisCurrentCart.fixpricetotal;
                   ff.fixpricepromoamt = thisCurrentCart.fixpricepromoamt;
               }

               order.ORDER_JSON = JsonConvert.SerializeObject(ff);
                DBORDER_BEANBLL ordrebll = new DBORDER_BEANBLL();
               ordrebll.Add(order);

               return true;
           }
           catch (Exception ex)
           {
               MainModel.ShowLog("创建离线订单异常" + ex.Message, true);
               return false;
           }
       }


       public static string GetOrderID(Cart thisCurrentCart)
       {
           try
           {
               //订单号：取当时时间戳+设备SN+门店ID+登录离线店员手机号+该订单总价+现金支付价+找零价+抹分价+实际订单对象hashcode+订单对象hashcode，生成 后的订单hashcode+4位随机数,生成后的订单号去掉"-"为本次生成的离线订单号

               string strorder = MainModel.getStampByDateTime(DateTime.Now) + MainModel.DeviceSN + MainModel.CurrentUserPhone + thisCurrentCart.GetHashCode();
               return strorder.GetHashCode().ToString().Replace("-", "") + Getrandom(4);
           }
           catch (Exception ex)
           {
               MainModel.ShowLog("获取离线订单号异常" + ex.Message, true);
           }
           return "";
       }

       public static string Getrandom(int num)
       {
           Random rd = new Random();
           string result = "";
           for (int i = 0; i < num; i++)
           {
               result += rd.Next(10).ToString();
           }
           return result;
       }

    }
}
