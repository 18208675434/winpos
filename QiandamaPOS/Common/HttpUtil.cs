using Newtonsoft.Json;
using QiandamaPOS.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace QiandamaPOS.Common
{
    public class HttpUtil
    {

        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public string Signin(string username, string password,ref string errormsg)
        {
            try
            {
                string url = "/pos/account/signin";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                sort.Add("username", username);
                sort.Add("password", password);

                string testjson = JsonConvert.SerializeObject(sort);

                string json = HttpPOST(url, testjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    string Token = rd.data.ToString();
                    return Token;

                }
                else
                {
                    errormsg = rd.message;
                    return "";
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "验证用户信息异常：" + ex.Message);
                errormsg = "验证用户信息异常：" + ex.Message;
                return "";
            }
        }

     /// <summary>
     /// 获取用户信息
     /// </summary>
     /// <param name="errormsg">返回信息</param>
     /// <returns></returns>
        public userModel GetUser(ref string errormsg)
        {
            try
            {
                string url = "/pos/account/sysuser/item/user";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();

                string json = HttpGET(url, sort);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    userModel currentuser = JsonConvert.DeserializeObject<userModel>(rd.data.ToString());
                    return currentuser;

                }
                else
                {
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取用户信息异常：" + ex.Message);
                errormsg = "获取用户信息异常：" + ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="errormsg">返回信息</param>
        /// <returns></returns>
        public DeviceShopInfo GetShopInfo(string devicesn, ref string errormsg)
        {
            try
            {
                string url = "/pos/product/pos/getdeviceshopinfovo";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                sort.Add("devicesn", devicesn);

                string json = HttpGET(url, sort);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    DeviceShopInfo deviceshop = JsonConvert.DeserializeObject<DeviceShopInfo>(rd.data.ToString());
                    return deviceshop;

                }
                else
                {
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取门店信息异常：" + ex.Source);
                errormsg = "获取门店信息异常：" + ex.Message;
                return null;
            }
        }


        public scancodememberModel GetSkuInfoMember(string scancode, ref string errormsg)
        {
            try
            {
                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                string url = "";

                    url = "/pos/product/scancode/getscancodeskumemberdto";
                    sort.Add("shopid", MainModel.CurrentShopInfo.shopid);
                    sort.Add("barcode", scancode);
               
                    
              
                string json = HttpGET(url, sort);
                //Console.Write("getskuInfobyshortcode:"+ json);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                if (rd.code == 0)
                {
                    scancodememberModel scancodember =new scancodememberModel();

                        scancodember = JsonConvert.DeserializeObject<scancodememberModel>(rd.data.ToString());
                  
                    return scancodember;

                }
                else
                {
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "条码或会员识别异常：" + ex.Message);
                errormsg = "条码或会员识别异常：" + ex.Message;
                return null;
            }
        }


        public scancodememberModel GetSkuInfoByShortCode(string scancode, ref string errormsg)
        {
            try
            {
                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                string url = "";
            
                    url = "/pos/product/scancode/getskuInfobyshortcode";
                    sort.Add("shopid", MainModel.CurrentShopInfo.shopid);
                    sort.Add("shortcode", scancode.PadLeft(5, '0'));

                string json = HttpGET(url, sort);
                //Console.Write("getskuInfobyshortcode:"+ json);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                if (rd.code == 0)
                {
                    scancodememberModel scancodember = new scancodememberModel();
                    
                        Scancodedto scancodedto = JsonConvert.DeserializeObject<Scancodedto>(rd.data.ToString());
                        scancodember.scancodedto = scancodedto;
                    
                    return scancodember;

                }
                else
                {
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "条码或会员识别异常：" + ex.Message);
                errormsg = "条码或会员识别异常：" + ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 购物车接口
        /// </summary>
        /// param name="lstscancodemember"> 0:扫描刷新购物车（cashpayoption=0,cashpayamt=0)  1：抹零（cashpayoption1,cashpayamt=0)  2：现金支付（cashpayoption=1,cashpayamt=支付金额)  3:积分+现金</param>
        /// <param name="lstscancodemember"></param>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public Cart GetCart(int type, List<scancodememberModel> lstscancodemember,decimal cash, ref string errormsg)
        {
            try
            {
                string url = "/pos/order/pos/cart";

                product[] lstpro = new product[lstscancodemember.Count];
                for (int i = 0; i < lstscancodemember.Count; i++)
                {
                    product pro = new product();
                    pro.skucode = lstscancodemember[i].scancodedto.skucode;
                    pro.num = lstscancodemember[i].scancodedto.num;
                    pro.specnum = lstscancodemember[i].scancodedto.specnum.ToString();
                    pro.spectype = lstscancodemember[i].scancodedto.spectype;
                    pro.goodstagid = lstscancodemember[i].scancodedto.weightflag == true ? 1 : 0;

                    pro.barcode = lstscancodemember[i].scancodedto.barcode;

                    lstpro[i] = pro;
                }               
                

                CartPara cart = new CartPara();
                cart.ordersubtype = "pos";
                cart.products = lstpro;
                cart.shopid = MainModel.CurrentShopInfo.shopid;

                if (type == 0)
                {
                    cart.cashpayoption = 0;
                    cart.cashpayamt = 0;
                }
                else if (type == 1)
                {
                    cart.cashpayoption = 1;
                    cart.cashpayamt = 0;
                }
                else if (type == 2)
                {
                    cart.cashpayoption = 1;
                    cart.cashpayamt = cash;
                }
                else if (type == 3)
                {
                    cart.cashpayoption = 1;
                    cart.cashpayamt = 0;
                    cart.pointpayoption = 1;
                 
                }

             
                if (MainModel.CurrentMember != null)
                {
                    cart.uid = MainModel.CurrentMember.memberheaderresponsevo.memberid;
                    cart.usertoken = MainModel.CurrentMember.memberheaderresponsevo.token;

                    if (!string.IsNullOrWhiteSpace(MainModel.CurrentCouponCode))
                    {
                        string[] strs = new string[1];
                        strs[0] = MainModel.CurrentCouponCode;
                        cart.selectedcoupons = strs;
                    }

                }

                string tempjson = JsonConvert.SerializeObject(cart);


               
                string json = HttpPOST(url, tempjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                //Console.Write(json);
                // return;
                if (rd.code == 0)
                {
                    Cart carttemp = JsonConvert.DeserializeObject<Cart>(rd.data.ToString());
                    return carttemp;
                }
                else
                {
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "购物车信息获取异常：" + ex.Message);
                errormsg = "购物车信息获取异常：" + ex.Message;
                return null;

            }
        }



        /// <summary>
        /// 刷新购物车
        /// </summary>
        /// param name="lstscancodemember"> 0:扫描刷新购物车（cashpayoption=0,cashpayamt=0)  1：抹零（cashpayoption1,cashpayamt=0)  2：现金支付（cashpayoption=1,cashpayamt=支付金额)  3:积分+现金</param>
        /// <param name="lstscancodemember"></param>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public Cart RefreshCart(Cart cart, ref string errormsg)
        {
            try
            {
                string url = "/pos/order/pos/cart";


                CartPara cartpara = new CartPara();
                cartpara.ordersubtype = "pos";
                if (cart.products != null)
                {
                    product[] lstpro = new product[cart.products.Count];
                    for (int i = 0; i < cart.products.Count; i++)
                    {
                        product pro = new product();
                        pro.skucode = cart.products[i].skucode;
                        pro.num = cart.products[i].num;
                        pro.specnum = cart.products[i].specnum.ToString();
                        pro.spectype = cart.products[i].spectype;
                        pro.goodstagid = cart.products[i].goodstagid;
                        pro.barcode = cart.products[i].barcode;

                        lstpro[i] = pro;
                    }
                    cartpara.products = lstpro;
                }
                
                cartpara.shopid = MainModel.CurrentShopInfo.shopid;



                    cartpara.cashpayoption = cart.cashpayoption;
                    cartpara.cashpayamt =(decimal) cart.cashpayamt;
                
                    cartpara.pointpayoption = cart.pointpayoption;

                    cartpara.cashcouponamt = cart.cashcouponamt;

                if (MainModel.CurrentMember != null)
                {
                    cartpara.uid = MainModel.CurrentMember.memberheaderresponsevo.memberid;
                    cartpara.usertoken = MainModel.CurrentMember.memberheaderresponsevo.token;

                    if (!string.IsNullOrWhiteSpace(MainModel.CurrentCouponCode))
                    {
                        string[] strs = new string[1];
                        strs[0] = MainModel.CurrentCouponCode;
                        cartpara.selectedcoupons = strs;
                    }

                }

                string tempjson = JsonConvert.SerializeObject(cartpara);


                string json = HttpPOST(url, tempjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                //Console.Write(json);

                // return;
                if (rd.code == 0)
                {
                    Cart carttemp = JsonConvert.DeserializeObject<Cart>(rd.data.ToString());
                    return carttemp;
                }
                else
                {
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "购物车信息获取异常：" + ex.Message);
                errormsg = "购物车信息获取异常：" + ex.Message;
                return null;

            }
        }

        public CreateOrderResult CreateOrder(Cart cart,ref string errormsg)
        {
            try
            {
                string url = "/pos/order/pos/create";

                product[] lstpro = new product[cart.products.Count];
                for (int i = 0; i < cart.products.Count; i++)
                {
                    product pro = new product();
                    pro.skucode = cart.products[i].skucode;
                    pro.num = cart.products[i].num;
                    pro.specnum = cart.products[i].specnum.ToString();
                    pro.spectype = cart.products[i].spectype;
                    pro.goodstagid = cart.products[i].goodstagid;
                    pro.barcode = cart.products[i].barcode;
                    lstpro[i] = pro;
                }
   

                CreateOrderPara order = new CreateOrderPara();

                order.ordersubtype = "pos";
                order.products = lstpro;

                order.shopid = MainModel.CurrentShopInfo.shopid;
                order.orderplaceid = cart.orderplaceid;
                order.cashpayoption = cart.cashpayoption;
                order.pointpayoption = cart.pointpayoption;
                //order.cashcouponamt=cart.cashc

                order.cashcouponamt = cart.cashcouponamt;
                //现金支付
                if(order.cashpayoption == 1)
                {
                    order.cashpayamt = (decimal)cart.cashpayamt;
                    
                }
                if(MainModel.CurrentMember!=null)
                {
                      order.uid =MainModel.CurrentMember.memberheaderresponsevo.memberid;
                      order.usertoken = MainModel.CurrentMember.memberheaderresponsevo.token;

                      if (!string.IsNullOrWhiteSpace(MainModel.CurrentCouponCode))
                      {
                          string[] strs = new string[1];
                          strs[0] = MainModel.CurrentCouponCode;
                          order.selectedcoupons = strs;
                      }

                }
                order.pricetotal = (decimal)cart.totalpayment;
                string tempjson = JsonConvert.SerializeObject(order);

                ////Console.Write(tempjson);

                string json = HttpPOST(url, tempjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);


                // return;
                if (rd.code == 0)
                {
                    CreateOrderResult cr = JsonConvert.DeserializeObject<CreateOrderResult>(rd.data.ToString());
                    return cr;
                }
                else
                {
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "创建订单异常：" + ex.Message);
                errormsg = "创建订单异常：" + ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="reason"></param>
        /// <param name="erromessage"></param>
        /// <returns></returns>
        public bool CancleOrder(string orderid, string reason, ref string erromessage)
        {
            try
            {
                string url = "/pos/order/common/cancel";


                string tempjson = "{\"orderid\":\"" + orderid +"\",\"reason\":\""+orderid+ "\"}";
                string json = HttpPOST(url, tempjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                //TODO

                if (rd.code == 0)
                {
                    return true;

                }
                else
                {
                    erromessage = rd.message;
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", orderid+"取消订单异常：" + ex.Message);
                erromessage =orderid+ "取消订单异常：" + ex.Message;
                return false;
            }
        }


        public int QueryOrderStatus(string orderid,ref string erromessage)
        {
            try
            {
                string url = "/pos/order/pos/querystatus";
     

                string tempjson = JsonConvert.SerializeObject(orderid);
                tempjson = "{\"orderid\":\"" + orderid + "\"}";
                string json = HttpPOST(url, tempjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                                
                if (rd.code == 0)
                {
                    return Convert.ToInt16(rd.data);

                }
                else
                {
                    erromessage = rd.message;
                    return 0;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "查询订单状态异常：" + ex.Message);
                erromessage = "查询订单状态异常：" + ex.Message;
                return 0;
            }
        }

        /// <summary>
        /// 第三方支付接口
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="authcode"></param>
        /// <param name="erromessage"></param>
        public AuthcodeTrade AuthCodeTrade(string orderid,string authcode, ref string erromessage)
        {
            try
            {
                string url = "/pos/pay/trade/authcodetrade";

                tradePara trade = new tradePara();
                trade.orderid = orderid;
                trade.authcode = authcode;
                string tempjson = JsonConvert.SerializeObject(trade);

                string json = HttpPOST(url, tempjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                //TODO

                if (rd.code == 0)
                {
                    AuthcodeTrade codetrade = JsonConvert.DeserializeObject<AuthcodeTrade>(rd.data.ToString());

                    return codetrade;                    

                }
                else
                {
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "调用第三方支付接口异常：" + ex.Message);
                erromessage = "调用第三方支付接口异常：" + ex.Message;
                return null;
            }
        }


        /// <summary>
        /// 第三方支付结果查询
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="payid"></param>
        /// <param name="erromessage"></param>
        public synctrade  SyncTrade(string orderid, string payid, ref string erromessage)
        {
            try
            {
                string url = "/pos/pay/trade/synctrade";

                syncPara trade = new syncPara();
                trade.orderid = orderid;
                trade.payid = payid;
                string tempjson = JsonConvert.SerializeObject(trade);

                string json = HttpPOST(url, tempjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                if (rd.code == 0)
                {
                    synctrade sync = JsonConvert.DeserializeObject<synctrade>(rd.data.ToString());

                    return sync;
                }
                else
                {
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "查询第三方支付状态异常：" + ex.Message);
                erromessage = "查询第三方支付状态异常：" + ex.Message;
                return null;
            }
        }


        /// <summary>
        /// 会员余额支付
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="authcode"></param>
        /// <param name="erromessage"></param>
        public BalanceTrade BalanceTrade(string orderid, string authcode, ref string erromessage)
        {
            try
            {
                string url = "/pos/pay/balancetrade/trade";

                string tempjson = "{\"outtradeno\":\"" + orderid + "\",\"authcode\":\"" + authcode + "\"}";

                string json = HttpPOST(url, tempjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                //TODO
                Console.WriteLine(json);
                if (rd.code == 0)
                {
                    BalanceTrade codetrade = JsonConvert.DeserializeObject<BalanceTrade>(rd.data.ToString());

                    return codetrade;

                }
                else
                {
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "调用第三方支付接口异常：" + ex.Message);
                erromessage = "调用第三方支付接口异常：" + ex.Message;
                return null;
            }
        }


        /// <summary>
        /// 会员余额支付查询
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="payid"></param>
        /// <param name="erromessage"></param>
        public BalanceTrade QueryTrade(string orderid, string payid, ref string erromessage)
        {
            try
            {
                string url = "/pos/pay/balancetrade/querytrade";

                string tempjson = "{\"outtradeno\":\"" + orderid + "\"}";

                string json = HttpPOST(url, tempjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                if (rd.code == 0)
                {
                    BalanceTrade sync = JsonConvert.DeserializeObject<BalanceTrade>(rd.data.ToString());

                    return sync;
                }
                else
                {
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "查询第三方支付状态异常：" + ex.Message);
                erromessage = "查询第三方支付状态异常：" + ex.Message;
                return null;
            }
        }


        /// <summary>
        /// 查询收银机订单详情（打印小票）
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="erromessage"></param>
        /// <returns></returns>
        public PrintDetail GetPrintDetail(string orderid, ref string erromessage)
        {
            try
            {
                string url = "/pos/order/pos/detail";

                string tempjson = JsonConvert.SerializeObject(orderid);
                tempjson = "{\"orderid\":\"" + orderid + "\"}";
                string json = HttpPOST(url, tempjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                
                //Console.Write(json);

                // return;
                if (rd.code == 0)
                {
                    PrintDetail printinfo = JsonConvert.DeserializeObject<PrintDetail>(rd.data.ToString());
                    return printinfo;

                }
                else
                {
                    erromessage = rd.message;
                     return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "查询订单详情异常：" + ex.Message);
                erromessage = "查询订单详情异常：" + ex.Message;
                return null;
            }
        }


        /// <summary>
        /// 订单查询
        /// </summary>
        /// <returns></returns>
        public QueryOrder QueryOrderInfo(QueryOrderPara queryorderpara, ref string erromessage)
        {
            try
            {
                string url = "/pos/order/pos/query";

                string tempjson = JsonConvert.SerializeObject(queryorderpara);

                string json = HttpPOST(url, tempjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                Console.WriteLine("订单查询："+json);

                // return;
                if (rd.code == 0)
                {
                    QueryOrder queryorder = JsonConvert.DeserializeObject<QueryOrder>(rd.data.ToString());
                    return queryorder;

                }
                else
                {
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "查询历史异常：" + ex.Message);
                erromessage = "查询历史订单异常：" + ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 退款
        /// </summary>
        /// <returns></returns>
        public string Refund(string orderid, ref string erromessage)
        {
            try
            {
                string url = "/pos/order/pos/refund";

                string tempjson = JsonConvert.SerializeObject(orderid);
                tempjson = "{\"orderid\":\"" + orderid + "\"}";
                string json = HttpPOST(url, tempjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                // return;
                if (rd.code == 0)
                {
                    erromessage = "";
                    return rd.data.ToString();

                }
                else
                {
                    erromessage = rd.message;
                    return "";
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "查询历史异常：" + ex.Message);
                erromessage = "查询历史订单异常：" + ex.Message;
                return "";
            }
        }


        /// <summary>
        /// 获取客屏信息
        /// </summary>
        /// <returns></returns>
        public MediaList GetPosMedia(ref string erromessage)
        {
            try
            {
                string url = "/pos/product/pos/getposmedialistwithoutlogin";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();


                string json = HttpGET(url, sort);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                // return;
                if (rd.code == 0)
                {
                    erromessage = "";
                    MediaList posmedia = JsonConvert.DeserializeObject<MediaList>(rd.data.ToString());
                    return posmedia;
                }
                else
                {
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取客屏信息异常：" + ex.Message);
                erromessage = "获取客屏信息异常：" + ex.Message;
                return null;
            }
        }


        /// <summary>
        /// 提交交班小票请求
        /// </summary>
        /// <returns></returns>
        public Receiptdetail Receipt(ReceiptPara receiptpara, ref string erromessage)
        {
            try
            {
                string url = "/pos/order/pos/receipt";

                string tempjson = JsonConvert.SerializeObject(receiptpara);

                string json = HttpPOST(url, tempjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);


                // return;
                if (rd.code == 0)
                {
                    erromessage = "";
                    Receiptdetail receipt = JsonConvert.DeserializeObject<Receiptdetail>(rd.data.ToString());
                    return receipt ;

                }
                else
                {
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "提交交班小票请求异常：" + ex.Message);
                erromessage = "提交交班小票请求异常：" + ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 交班查询
        /// </summary>
        /// <returns></returns>
        public List<ReceiptQuery> QueryReceipt(QueryReceiptPara queryreceiptpara, ref string erromessage)
        {
            try
            {
                string url = "/pos/order/pos/queryreceipt";

                string tempjson = JsonConvert.SerializeObject(queryreceiptpara);

                string json = HttpPOST(url, tempjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                // return;
                if (rd.code == 0)
                {
                    erromessage = "";
                    List<ReceiptQuery> receipt = JsonConvert.DeserializeObject<List<ReceiptQuery>>(rd.data.ToString());
                    return receipt;
                }
                else
                {
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "交班查询异常：" + ex.Message);
                erromessage = "交班查询异常：" + ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 会员登录
        /// </summary>
        /// <returns></returns>
        public Member GetMember(string memberinfo, ref string erromessage)
        {
            try
            {
                string url = "/pos/member/recognize/member";

                string tempjson = "{\"memberinfo\":\"" + memberinfo + "\"}";

                string json = HttpPOST(url, tempjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                // return;
                if (rd.code == 0)
                {
                    erromessage = "";
                    Member member = JsonConvert.DeserializeObject<Member>(rd.data.ToString());
                    return member;
                }
                else
                {
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "验证会员信息异常：" + ex.Message);
                erromessage = "验证会员信息异常：" + ex.Message;
                return null;
            }
        }


        /// <summary>
        /// 分页获取门店秤信息
        /// </summary>
        /// <returns></returns>
        public Scale GetScale(int page, int size, ref string erromessage)
        {
            try
            {
                string url = "/pos/product/pos/queryshopscaledtosbypage";
                ScalePara scalepara = new ScalePara();
                scalepara.shopid = MainModel.CurrentShopInfo.shopid;
                scalepara.page = page;
                scalepara.size = size;

                string tempjson = JsonConvert.SerializeObject(scalepara);

                string json = HttpPOST(url, tempjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                // return;
                if (rd.code == 0)
                {
                    erromessage = "";
                    Scale scale = JsonConvert.DeserializeObject<Scale>(rd.data.ToString());
                    return scale;
                }
                else
                {
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取门店秤信息异常：" + ex.Message);
                erromessage = "获取门店秤信息异常：" + ex.Message;
                return null;
            }
        }



        /// <summary>
        /// 收银机可选现金券面值列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetAvailableCashCoupons(ref string erromessage)
        {
            try
            {
                List<string> lstCashCoupon = new List<string>();
                string url = "/pos/order/pos/availablecashcoupons";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();


                string json = HttpGET(url, sort);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                // return;
                if (rd.code == 0)
                {
                    erromessage = "";
                    lstCashCoupon = JsonConvert.DeserializeObject<List<string>>(rd.data.ToString());
                    return lstCashCoupon;

                }
                else
                {
                    erromessage = rd.message;
                    return lstCashCoupon;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取现金券面值异常：" + ex.Message);
                erromessage = "获取现金券面值异常：" + ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 获取项目费用
        /// </summary>
        /// <returns></returns>
        public List<ExpenseType> GetExpenses(ref string erromessage)
        {
            try
            {
                string url = "/pos/product/pos/getallexpensesdto";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();


                string json = HttpGET(url, sort);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                // return;
                if (rd.code == 0)
                {
                    erromessage = "";
                    List<ExpenseType> expensetype = JsonConvert.DeserializeObject<List<ExpenseType>>(rd.data.ToString());
                    return expensetype;

                }
                else
                {
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取费用项目异常：" + ex.Message);
                erromessage = "获取费用项目异常：" + ex.Message;
                return null;
            }
        }


        /// <summary>
        /// 保存营业外支出收入
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="payid"></param>
        /// <param name="erromessage"></param>
        public bool SaveExpense(string expenseid, decimal expensefee,string shopid, ref string erromessage)
        {
            try
            {
                string url = "/pos/order/pos/saveexpenserecord";


                SaveExpensePara savaexepensepara = new SaveExpensePara();
                savaexepensepara.expenseid = expenseid;
                savaexepensepara.expensefee = expensefee;
                savaexepensepara.shopid = shopid;
                string tempjson = JsonConvert.SerializeObject(savaexepensepara);
                //string tempjson = "{\"expenseid\":\"" + expenseid + "\",\"expensefee\":" + expensefee + ",\"shopid\":\"" + shopid + "\"}";



                string json = HttpPOST(url, tempjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                Console.WriteLine(json);
                if (rd.code == 0)
                {
                    return Convert.ToBoolean(rd.data);

                }
                else
                {
                    erromessage = rd.message;
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "保存营业外支出异常：" + ex.Message);
                erromessage = "保存营业外支出异常：" + ex.Message;
                return false;
            }
        }


        /// <summary>
        /// 查询营业外支出收入
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="payid"></param>
        /// <param name="erromessage"></param>
        public Expense[] QueryExpense(int intervaldays, long lastorderid, string shopid, string operatetimestr, ref string erromessage)
        {
            try
            {
                string url = "/pos/order/pos/queryexpenserecords";

                QueryExpensePara qep = new QueryExpensePara();
                qep.intervaldays = intervaldays;
                qep.lastorderid = lastorderid;
                qep.operatetimestr = operatetimestr;

               
                string tempjson = JsonConvert.SerializeObject(qep);
                //string tempjson = "{\"expenseid\":\"" + expenseid + "\",\"expensefee\":" + expensefee + ",\"shopid\":\"" + shopid + "\"}";



                string json = HttpPOST(url, tempjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                Console.WriteLine(json);
                if (rd.code == 0)
                {
                    Expense[] expenses = JsonConvert.DeserializeObject<Expense[]>(rd.data.ToString());
                    return expenses;

                }
                else
                {
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "保存营业外支出异常：" + ex.Message);
                erromessage = "保存营业外支出异常：" + ex.Message;
                return null;
            }
        }





        #region  访问服务端
        public string HttpGET(string Url, SortedDictionary<string,string> sortpara)
        {
            string retString = "";

            string Timestamp = MainModel.getStampByDateTime(DateTime.Now);
            string nonce = MainModel.getNonce();

            sortpara.Add("nonce",nonce);
            //string signstr = "kVl55eO1n3DZhWC8Z7" + "devicesn" + devicesn + "nonce" + nonce + Timestamp;
            string signstr = MainModel.PrivateKey;
            foreach(KeyValuePair<string,string> keyvalue in sortpara)
            {
                signstr += keyvalue.Key + keyvalue.Value;
            }
            signstr += Timestamp;

            //string body = "{\"devicesn\":\"" + devicesn + "\"}";

            string postDataStr = "sign=" + MainModel.GetMD5(signstr);
            //Url += "?" + "devicesn=" + devicesn + "&" + postDataStr;

            Url = MainModel.URL + Url + "?";
            foreach (KeyValuePair<string, string> keyvalue in sortpara)
            {
                Url += keyvalue.Key +"="+ keyvalue.Value +"&";
            }
            Url += postDataStr;
           // //Console.Write(Url);
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);

                try
                {
                    int timeout = 5000;
                    if (timeout > 0)
                        request.Timeout = timeout;
                }
                catch { }

                System.Net.ServicePointManager.Expect100Continue = false;

                if (Url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    request = WebRequest.Create(new Uri(Url)) as HttpWebRequest;
                    request.ProtocolVersion = HttpVersion.Version10;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                }
                else
                {
                    request = WebRequest.Create(new Uri(Url)) as HttpWebRequest;
                }

                request.Method = "GET";

                ////请求头
                request.Headers.Add("v", MainModel.Version);
                request.Headers.Add("X-ZZ-Device-Sn", MainModel.DeviceSN);
                request.Headers.Add("POS-Authorization", MainModel.Authorization);
                request.Headers.Add("X-ZZ-Timestamp", Timestamp);

                request.ContentType = "application/json;charset=UTF-8";

                //byte[] by = Encoding.GetEncoding("utf-8").GetBytes(body);   //请求参数转码
                ////request.ContentType = "application/json;charset=UTF-8";
                //request.ContinueTimeout = 500000;
                //request.ContentLength = by.Length;

                //Stream stw = request.GetRequestStream();     //获取绑定相应流
                //stw.Write(by, 0, by.Length);      //写入流
                //stw.Close();    //关闭流

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.StackTrace);
                 LogManager.WriteLog("访问服务器出错:" + ex.Message);
                MainModel.ShowLog("访问服务器出错,请检查网络连接！", false);

            }

            return retString;
        }


        public string HttpPOST(string Url, string bodyjson)
        {

            string retString = "";

            string Timestamp = MainModel.getStampByDateTime(DateTime.Now);
            string nonce = MainModel.getNonce();
            string signstr = MainModel.PrivateKey + "nonce" + nonce + Timestamp + bodyjson;
            string postDataStr = "nonce=" + nonce + "&sign=" + MainModel.GetMD5(signstr);

            Url = MainModel.URL + Url+"?" + postDataStr;
            Console.WriteLine(Url);
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);

                try
                {
                    int timeout = 5000;
                    if (timeout > 0)
                        request.Timeout = timeout;
                }
                catch { }

                System.Net.ServicePointManager.Expect100Continue = false;

                if (Url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    request = WebRequest.Create(new Uri(Url)) as HttpWebRequest;
                    request.ProtocolVersion = HttpVersion.Version10;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                }
                else
                {
                    request = WebRequest.Create(new Uri(Url)) as HttpWebRequest;
                }

                request.Method = "POST";

                ////请求头
                request.Headers.Add("v", MainModel.Version);
                request.Headers.Add("X-ZZ-Device-Sn", MainModel.DeviceSN);
                request.Headers.Add("POS-Authorization", MainModel.Authorization);
                request.Headers.Add("X-ZZ-Timestamp", Timestamp);


                byte[] by = Encoding.GetEncoding("utf-8").GetBytes(bodyjson);   //请求参数转码
                request.ContentType = "application/json;charset=UTF-8";
                request.ContinueTimeout = 500000;
                request.ContentLength = by.Length;

                Stream stw = request.GetRequestStream();     //获取绑定相应流
                stw.Write(by, 0, by.Length);      //写入流
                stw.Close();    //关闭流

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("访问服务器出错:" + ex.Message);
                MainModel.ShowLog("访问服务器出错,请检查网络连接！",false);
            }

            return retString;
        }

        internal bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            // 总是接受  
            return true;
        }

        ////当前时间戳
        //private string getStampByDateTime(DateTime datetime )
        //{

        //    //DateTime datetime = DateTime.Now;
        //    var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        //    var result = (long)(datetime - startTime).TotalMilliseconds;

        //    return result.ToString();
        //}

        //private DateTime  GetDateTimeByStamp(string stamp)
        //{
        //    try
        //    {
        //        long result = Convert.ToInt64(stamp);
        //        DateTime datetime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        //        datetime = datetime.AddMilliseconds(result);
        //        return datetime;
        //    }
        //    catch (Exception ex)
        //    {
        //        return DateTime.Now;
        //    }
        //}

        ////MD5 加密
        //private string GetMD5(string str)
        //{

        //    byte[] result = Encoding.Default.GetBytes(str);
        //    MD5 md5 = new MD5CryptoServiceProvider();
        //    byte[] output = md5.ComputeHash(result);
        //    string md5str = BitConverter.ToString(output).Replace("-", ""); 
        //    return md5str;
        //}

        ////获取20位随机码 nonce
        //private string getNonce()
        //{
        //    string randomstr = "0,1,2,3,4,5,6,7,8,9,0,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
        //    string[] strRandom = randomstr.Split(',');
        //    Random rd = new Random();
        //    string result = "";
        //    for (int i = 0; i < 20; i++)
        //    {
        //        int num = rd.Next(35);

        //        result += strRandom[num];
        //    }

        //    return result;
        //}

        ////键值对排序
        //private Dictionary<string,string> SortDictory(Dictionary<string,string> dictionary)
        //{
        //    System.Collections.ArrayList lst = new System.Collections.ArrayList(dictionary.Keys);
        //    lst.Sort();
        //    //lst.Reverse();  //反转排序
        //    Dictionary<string, string> dicresult = new Dictionary<string, string>();

        //    foreach (string key in lst)
        //    {
        //        dicresult.Add(key, dictionary[key]);
        //    }

        //    return dicresult;
        //}
        #endregion  
    }
}
