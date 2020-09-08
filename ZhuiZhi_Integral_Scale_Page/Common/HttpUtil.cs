using Maticsoft.Model;
using Newtonsoft.Json;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using ZhuiZhi_Integral_Scale_UncleFruit.BrokenUI.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.Model.HalfOffLine;
using ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Common
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
        public string Signin(string username, string password, ref string errormsg)
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
                    try { LogManager.WriteLog("Error", "signin:" + json); }catch { }
                    errormsg = rd.message;
                    return "";
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "验证用户信息异常：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
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
                    try { LogManager.WriteLog("Error", "GetUser:" + json); }catch { }
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取用户信息异常：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return null;
            }
        }

        /// <summary>
        /// 获取门店设备信息
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
                    try { LogManager.WriteLog("Error", "getdeviceshopinfovo:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取门店信息异常：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return null;
            }
        }


        public scancodememberModel GetSkuInfoMember(string scancode, ref string errormsg, ref int resultcode)
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
                resultcode = rd.code;
                if (rd.code == 0)
                {
                    scancodememberModel scancodember = new scancodememberModel();

                    scancodember = JsonConvert.DeserializeObject<scancodememberModel>(rd.data.ToString());

                    return scancodember;

                }
                else
                {
                    try { LogManager.WriteLog("Error", "getscancodeskumemberdto:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "条码或会员识别异常：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return null;
            }
        }


        public scancodememberModel GetSkuInfoByShortCode(string scancode, ref string errormsg, ref int resultcode)
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

                resultcode = rd.code;
                if (rd.code == 0)
                {
                    scancodememberModel scancodember = new scancodememberModel();

                    Scancodedto scancodedto = JsonConvert.DeserializeObject<Scancodedto>(rd.data.ToString());
                    scancodember.scancodedto = scancodedto;

                    return scancodember;

                }
                else
                {
                    try { LogManager.WriteLog("Error", "getskuInfobyshortcode:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "短码识别异常：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
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
        public Cart GetCart(int type, List<scancodememberModel> lstscancodemember, decimal cash, ref string errormsg)
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
                    cart.uid = MainModel.CurrentMember.memberid;
                    cart.usertoken = MainModel.CurrentMember.memberheaderresponsevo.token;

                    if (!string.IsNullOrEmpty(MainModel.CurrentCouponCode))
                    {
                        string[] strs = new string[1];
                        strs[0] = MainModel.CurrentCouponCode;
                        cart.selectedcoupons = strs;
                    }

                }

                string tempjson = JsonConvert.SerializeObject(cart);


                string json = HttpPOST(url, tempjson);

                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                // return;
                if (rd.code == 0)
                {
                    Cart carttemp = JsonConvert.DeserializeObject<Cart>(rd.data.ToString());
                    return carttemp;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "cart:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "购物车信息获取异常：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
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
        public Cart RefreshCart(Cart cart, ref string errormsg, ref int resultcode)
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
                         
                        pro.adjustpriceinfo = cart.products[i].adjustpriceinfo;
                        lstpro[i] = pro;
                    }
                    cartpara.products = lstpro;
                }

                cartpara.shopid = MainModel.CurrentShopInfo.shopid;

                cartpara.cashpayoption = cart.cashpayoption;
                cartpara.cashpayamt = (decimal)cart.cashpayamt;

                cartpara.pointpayoption = cart.pointpayoption;

                cartpara.cashcouponamt = cart.cashcouponamt;
                cartpara.fixpricetotal = cart.fixpricetotal;

                cartpara.otherpayinfos = cart.otherpayinfos;
                //cartpara.otherpayamt = cart.otherpayamt;
                //cartpara.otherpaycouponcode = cart.otherpaycouponcode;
                //cartpara.otherpaytype = cart.otherpaytype;

                if (MainModel.CurrentMember != null)
                {
                    cartpara.uid = MainModel.CurrentMember.memberid;
                    cartpara.usertoken = MainModel.CurrentMember.memberheaderresponsevo.token;

                    if (!string.IsNullOrEmpty(MainModel.CurrentCouponCode))
                    {
                        string[] strs = new string[1];
                        strs[0] = MainModel.CurrentCouponCode;
                        cartpara.selectedcoupons = strs;
                    }

                }

                cartpara.balancepayoption = cart.balancepayoption;

                if (cart.paypasswordtype == 1)
                {
                    cartpara.paypasswordtype = 1;
                    cartpara.paypassword = cart.paypassword;
                }

                string tempjson = JsonConvert.SerializeObject(cartpara);

                if (cart.fixpricetotal == 0)
                {

                    tempjson = tempjson.Replace(",\"fixpricetotal\":0.0", "");
                    tempjson = tempjson.Replace(",\"fixpricepromoamt\":0.0", "");
                }

                string balancepaypassword = null;
                int balancepaypasswordtype = 0;
                //回参没有，需要加
                try
                {
                    balancepaypassword = cart.paypassword;
                    balancepaypasswordtype = cart.paypasswordtype;
                }
                catch (Exception ex)
                {

                }

                string json = HttpPOST(url, tempjson);

                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                // return;
                resultcode = rd.code;
                if (rd.code == 0)
                {
                    errormsg = "";
                    Cart carttemp = JsonConvert.DeserializeObject<Cart>(rd.data.ToString());
                    carttemp.paypassword = balancepaypassword;
                    carttemp.paypasswordtype = balancepaypasswordtype;
                    return carttemp;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "cart:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "购物车信息获取异常：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return null;

            }
        }

        public CreateOrderResult CreateOrder(Cart cart, ref string errormsg, ref int resultcode)
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

                    pro.adjustpriceinfo = cart.products[i].adjustpriceinfo;
                    lstpro[i] = pro;
                }


                CreateOrderPara order = new CreateOrderPara();

                //winpos收银 固定为1
                order.fromwinpos = 1;

                order.ordersubtype = "pos";
                order.products = lstpro;

                order.shopid = MainModel.CurrentShopInfo.shopid;
                order.orderplaceid = cart.orderplaceid;
                order.cashpayoption = cart.cashpayoption;
                order.pointpayoption = cart.pointpayoption;
                //order.cashcouponamt=cart.cashc
                order.cashcouponamt = cart.cashcouponamt;


                order.otherpayinfos = cart.otherpayinfos;
                //order.otherpayamt = cart.otherpayamt;
                //order.otherpaycouponcode = cart.otherpaycouponcode;
                //order.otherpaytype = cart.otherpaytype;

                //现金支付
                if (order.cashpayoption == 1)
                {
                    order.cashpayamt = (decimal)cart.cashpayamt;

                }
                if (MainModel.CurrentMember != null)
                {
                    order.uid = MainModel.CurrentMember.memberid;
                    order.usertoken = MainModel.CurrentMember.memberheaderresponsevo.token;

                    if (!string.IsNullOrEmpty(MainModel.CurrentCouponCode))
                    {
                        string[] strs = new string[1];
                        strs[0] = MainModel.CurrentCouponCode;
                        order.selectedcoupons = strs;
                    }

                }
                order.pricetotal = (decimal)cart.totalpayment;

                order.fixpricetotal = cart.fixpricetotal;

                order.balancepayoption = cart.balancepayoption;

                if (cart.paypasswordtype == 1)
                {
                    order.paypasswordtype = 1;
                    order.paypassword = cart.paypassword;
                }
                string tempjson = JsonConvert.SerializeObject(order);

                if (cart.fixpricetotal == 0)
                {


                        tempjson = tempjson.Replace(",\"fixpricetotal\":0.0", "");
                        tempjson = tempjson.Replace(",\"fixpricepromoamt\":0.0", "");
                    
                }

                ////Console.Write(tempjson);

                string json = HttpPOST(url, tempjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                resultcode = rd.code;
                // return;
                if (rd.code == 0)
                {
                    CreateOrderResult cr = JsonConvert.DeserializeObject<CreateOrderResult>(rd.data.ToString());
                    return cr;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "create:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "创建订单异常：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
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


                string tempjson = "{\"orderid\":\"" + orderid + "\",\"reason\":\"" + orderid + "\"}";
                string json = HttpPOST(url, tempjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                //TODO

                if (rd.code == 0)
                {
                    return true;

                }
                else
                {
                    try { LogManager.WriteLog("Error", "cancel:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", orderid + "取消订单异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return false;
            }
        }

      

        public int QueryOrderStatus(string orderid, ref string erromessage)
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
                    try { LogManager.WriteLog("Error", "querystatus:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return 0;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "查询订单状态异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return 0;
            }
        }

        /// <summary>
        /// 第三方支付接口
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="authcode"></param>
        /// <param name="erromessage"></param>
        public AuthcodeTrade AuthCodeTrade(string orderid, string authcode, ref string erromessage)
        {
            try
            {
                string url = "/pos/pay/trade/authcodetrade";

                SortedDictionary<string, object> sort = new SortedDictionary<string, object>();
                sort.Add("orderid", orderid);
                sort.Add("authcode", authcode);
                string tempjson = JsonConvert.SerializeObject(sort);
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
                    try { LogManager.WriteLog("Error", "authcodetrade:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "调用第三方支付接口异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return null;
            }
        }


        /// <summary>
        /// 第三方支付接口  会员充值用
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="authcode"></param>
        /// <param name="erromessage"></param>
        public AuthcodeTrade AuthCodeTrade(tradePara trade, ref string erromessage)
        {
            try
            {
                string url = "/pos/pay/trade/authcodetrade";

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
                    try { LogManager.WriteLog("Error", "authcodetrade:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "调用第三方支付接口异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return null;
            }
        }


        /// <summary>
        /// 第三方支付结果查询
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="payid"></param>
        /// <param name="erromessage"></param>
        public synctrade SyncTrade(string orderid, string payid, ref string erromessage, ref string returnerrormsg)
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
                    try
                    {
                        returnerrormsg = rd.message;
                    }
                    catch { }
                    return sync;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "synctrade:" + json); }
                    catch { }
                    returnerrormsg = rd.message;
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "查询第三方支付状态异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
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


                if (rd.code == 0)
                {
                    BalanceTrade codetrade = JsonConvert.DeserializeObject<BalanceTrade>(rd.data.ToString());

                    return codetrade;

                }
                else
                {
                    try { LogManager.WriteLog("Error", "trade:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "调用第三方支付接口异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
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
                    try { LogManager.WriteLog("Error", "querytrade:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "查询第三方支付状态异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return null;
            }
        }


        /// <summary>
        /// 会员余额识别
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="authcode"></param>
        /// <param name="erromessage"></param>
        public Member BalanceCodeRecognition(string blancecode, bool needtoken, ref string erromessage)
        {
            try
            {
                string url = "/pos/member/balance/barcoderecognition";
                SortedDictionary<string, object> sort = new SortedDictionary<string, object>();
                sort.Add("barcode", blancecode);
                //sort.Add("needtoken", needtoken);

                ////有会员的时候不需要token   不允许登录会员和支付余额码的会员不同

                //if (MainModel.CurrentMember != null && MainModel.CurrentMember.memberheaderresponsevo != null && !string.IsNullOrEmpty(MainModel.CurrentMember.memberheaderresponsevo.token))
                //{
                //    sort.Add("needtoken", false);
                //}
                //else
                //{
                //    sort.Add("needtoken", true);
                //}
                sort.Add("needtoken", true);
                sort.Add("needmemberinfodetail", true);
                sort.Add("sncode", MainModel.DeviceSN);
                string tempjson = JsonConvert.SerializeObject(sort);
                string json = HttpPOST(url, tempjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                if (rd.code == 0)
                {
                    Member member = JsonConvert.DeserializeObject<Member>(rd.data.ToString());

                    return member;

                }
                else
                {
                    try { LogManager.WriteLog("Error", "barcoderecognition:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "调用第三方支付接口异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return null;
            }
        }



        /// <summary>
        /// 余额密码验证
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="authcode"></param>
        /// <param name="erromessage"></param>
        public VerifyBalancePwd VerifyBalancePwd(string paypassword, ref string erromessage, ref int resultcode)
        {
            try
            {


                string url = "/pos/member/balance/verifypassword";

                SortedDictionary<string, object> sort = new SortedDictionary<string, object>();
                sort.Add("paypasswordtype", 1);
                sort.Add("paypassword", paypassword);
                sort.Add("deviceid", MainModel.DeviceSN);
                sort.Add("authtoken", MainModel.CurrentMember.memberheaderresponsevo.token);
                sort.Add("memberid", MainModel.CurrentMember.memberid);
                string tempjson = JsonConvert.SerializeObject(sort);


                string json = HttpPOST(url, tempjson);

                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                resultcode = rd.code;

                if (rd.code == 0)
                {
                    VerifyBalancePwd verifyresult = JsonConvert.DeserializeObject<VerifyBalancePwd>(rd.data.ToString());

                    return verifyresult;

                }
                else
                {
                    try { LogManager.WriteLog("Error", "verifypassword:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "调用第三方支付接口异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
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
                string url = "/pos/order/pos/detail/v1";

                string tempjson = JsonConvert.SerializeObject(orderid);
                tempjson = "{\"orderid\":\"" + orderid + "\"}";
                string json = HttpPOST(url, tempjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                LogManager.WriteLog("DEBUG",json);

                // return;
                if (rd.code == 0)
                {
                    PrintDetail printinfo = JsonConvert.DeserializeObject<PrintDetail>(rd.data.ToString());
                    return printinfo;

                }
                else
                {
                    try { LogManager.WriteLog("Error", "detail:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "查询订单详情异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
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
                LogManager.WriteLog("DEBUG", json);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                if (rd.code == 0)
                {
                    QueryOrder queryorder = JsonConvert.DeserializeObject<QueryOrder>(rd.data.ToString());
                    return queryorder;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "query:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "查询历史异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
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
                    try { LogManager.WriteLog("Error", "refund:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return "";
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "查询历史异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return "";
            }
        }

        /// <summary>
        /// 退款
        /// </summary>
        /// <returns></returns>
        public string Refund(RefundPara refundpara, ref string erromessage)
        {
            try
            {
                string url = "/pos/order/pos/refund";

                string tempjson = JsonConvert.SerializeObject(refundpara);
                // tempjson = "{\"orderid\":\"" + orderid + "\"}";
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
                    try { LogManager.WriteLog("Error", "refund:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return "";
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "查询历史异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return "";
            }
        }

        /// <summary>
        /// 退款试算
        /// </summary>
        /// <returns></returns>
        public Order RefundValuate(RefundPara refundpara, ref string erromessage)
        {
            try
            {
                string url = "/pos/order/pos/refundvaluate";

                string tempjson = JsonConvert.SerializeObject(refundpara);
                // tempjson = "{\"orderid\":\"" + orderid + "\"}";
                string json = HttpPOST(url, tempjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                // return;
                if (rd.code == 0)
                {
                    erromessage = "";
                    Order order = JsonConvert.DeserializeObject<Order>(rd.data.ToString());
                    return order;

                }
                else
                {
                    try { LogManager.WriteLog("Error", "refundvaluate:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "查询历史异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return null;
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
                    try { LogManager.WriteLog("Error", "getposmedialistwithoutlogin:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取客屏信息异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
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
                string url = "/pos/order/pos/receipt/v1";

                string tempjson = JsonConvert.SerializeObject(receiptpara);

                // LogManager.WriteLog("交班参数"+tempjson);
                string json = HttpPOST(url, tempjson);
                LogManager.WriteLog("DEBUG","交班结果" + json);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);


                // return;
                if (rd.code == 0)
                {
                    erromessage = "";
                    Receiptdetail receipt = JsonConvert.DeserializeObject<Receiptdetail>(rd.data.ToString());
                    return receipt;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "receipt:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "提交交班小票请求异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
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
                    try { LogManager.WriteLog("Error", "queryreceipt:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "交班查询异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
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
                    try { LogManager.WriteLog("Error", "member:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "验证会员信息异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
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
                    try { LogManager.WriteLog("Error", "queryshopscaledtosbypage:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取门店秤信息异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return null;
            }
        }

        /// <summary>
        /// 分页获取门店秤信息
        /// </summary>
        /// <returns></returns>
        public ScaleForSaas GetScaleForSaas(int page, int size, ref string erromessage)
        {
            try
            {
                string url = "/pos/product/scalestemp/gettempforshoppaginglist"; 
                              

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
                    ScaleForSaas scale = JsonConvert.DeserializeObject<ScaleForSaas>(rd.data.ToString());
                    return scale;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "queryshopscaledtosbypage:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取门店秤信息异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return null;
            }
        }


        /// <summary>
        /// 分页获取门店秤信息
        /// </summary>
        /// <returns></returns>
        public bool CheckScaleUpdate()
        {
            try
            {
                string url = "/pos/product/scalestemp/checkshoptempupdated";

                string tempjson = "{\"lastrequestat\":\"" + MainModel.LastScaleTimeStamp + "\",\"shopid\":\"" + MainModel.CurrentShopInfo.shopid + "\"}";
                string json = HttpPOST(url, tempjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                // return;
                if (rd.code == 0)
                {
                    bool result = Convert.ToBoolean(rd.data);
                    return result;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "checkshoptempupdated:" + json); }
                    catch { }
                    return false ;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取门店秤信息异常：" + ex.Message);
                return false ;
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
                sort.Add("shopid",MainModel.CurrentShopInfo.shopid);

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
                    try { LogManager.WriteLog("Error", "availablecashcoupons:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return lstCashCoupon;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取现金券面值异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
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
                    try { LogManager.WriteLog("Error", "getallexpensesdto:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取费用项目异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return null;
            }
        }

        /// <summary>
        /// 保存营业外支出收入
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="payid"></param>
        /// <param name="erromessage"></param>
        public bool SaveExpense(string expenseid, decimal expensefee, string shopid, ref string erromessage)
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

                if (rd.code == 0)
                {
                    return Convert.ToBoolean(rd.data);

                }
                else
                {
                    try { LogManager.WriteLog("Error", "saveexpenserecord:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "保存营业外支出异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
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

                if (intervaldays <= 7)
                {
                    qep.intervaldays = intervaldays;
                }
                else
                {
                    qep.operatetimestr = operatetimestr;
                }

                qep.lastorderid = lastorderid;

                qep.shopid = MainModel.CurrentShopInfo.shopid;

                string tempjson = JsonConvert.SerializeObject(qep);
                //string tempjson = "{\"expenseid\":\"" + expenseid + "\",\"expensefee\":" + expensefee + ",\"shopid\":\"" + shopid + "\"}";


                string json = HttpPOST(url, tempjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                if (rd.code == 0)
                {
                    Expense[] expenses = JsonConvert.DeserializeObject<Expense[]>(rd.data.ToString());
                    return expenses;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "queryexpenserecords:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "保存营业外支出异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return null;
            }
        }


        /// <summary>
        /// 获取客屏太阳码
        /// </summary>
        /// <returns></returns>
        public string GetMemberCard(ref string erromessage)
        {
            try
            {
                string url = "/pos/common/wechat/suncode/membercard";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();


                string json = HttpGET(url, sort);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                // return;
                if (rd.code == 0)
                {
                    erromessage = "";

                    return rd.data.ToString();
                }
                else
                {
                    try { LogManager.WriteLog("Error", "membercard:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取客屏太阳码异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return null;
            }
        }


        /// <summary>
        /// 获取图形验证码
        /// </summary>
        /// <returns></returns>
        public AuthCodeImage GetAuthcodeImage(ref string erromessage)
        {
            try
            {
                string url = "/pos/account/getauthcodeimage";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();


                string json = HttpGET(url, sort);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                // return;
                if (rd.code == 0)
                {
                    erromessage = "";

                    AuthCodeImage authcodeimage = JsonConvert.DeserializeObject<AuthCodeImage>(rd.data.ToString());
                    return authcodeimage;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "getauthcodeimage:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取图形验证码异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return null;
            }
        }

        /// <summary>
        /// 获取短信验证码
        /// </summary>
        /// <returns></returns>
        public bool SendSmsCode(string phone, string imgcodekey, string imgcode, ref string erromessage)
        {
            try
            {
                string url = "/pos/account/sendsmscode";


                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();

                sort.Add("phone", phone);
                sort.Add("imgcodekey", imgcodekey);
                sort.Add("imgcode", imgcode);

                string json = HttpGET(url, sort);

                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                // return;
                if (rd.code == 0)
                {
                    erromessage = "";

                    //bool smcSuccess = Convert.ToBoolean(rd.data.ToString());
                    return true;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "sendsmscode:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取图形验证码异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return false;
            }
        }



        /// <summary>
        /// 手机验证码登录
        /// </summary>
        /// <returns></returns>
        public string SigninWithSmscode(SignPara signpara, ref string erromessage)
        {
            try
            {
                string url = "/pos/account/signinwithsmscode";


                string tempjson = JsonConvert.SerializeObject(signpara);

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
                    try { LogManager.WriteLog("Error", "signinwithsmscode:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return "";
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取图形验证码异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return "";
            }
        }




        /// <summary>
        /// 升级winpos
        /// </summary>
        /// <returns></returns>
        public string GetWinPos(ref string erromessage)
        {
            try
            {
                string url = "/appversion/winposdetail";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();


                string json = HttpGET(url, sort);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                // return;
                if (rd.code == 0)
                {
                    erromessage = "";

                    return rd.data.ToString();
                }
                else
                {
                    try { LogManager.WriteLog("Error", "winposdetail:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取winpos异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return null;
            }
        }


        /// <summary>
        /// 外部优惠券
        /// </summary>
        /// <returns></returns>
        public PaymentCouponDetail GetPaymentCouponDetail(string couponcode, ref string erromessage)
        {
            try
            {
                string url = "/pos/activity/payment/coupon/getpaymentcoupondetailbyquery";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                sort.Add("couponcode", couponcode);
                sort.Add("shopid", MainModel.CurrentShopInfo.shopid);
                string parajson = JsonConvert.SerializeObject(sort);

                string json = HttpPOST(url,parajson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                // return;
                if (rd.code == 0)
                {
                    erromessage = "";
                    PaymentCouponDetail result = JsonConvert.DeserializeObject<PaymentCouponDetail>(rd.data.ToString());
                    return result;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "getpaymentcoupondetailbyquery:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取getpaymentcoupondetailbyquery异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return null;
            }
        }

        /// <summary>
        /// 验证外部券 密码
        /// </summary>
        /// <returns></returns>
        public bool ValidateOuterCoupon(string couponcode,string password, ref string erromessage)
        {
            try
            {
                string url = "/pos/activity/payment/coupon/validatepassword";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                sort.Add("couponcode", couponcode);
                sort.Add("password", password);

                string json = HttpGET(url, sort);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                // return;
                if (rd.code == 0)
                {
                    erromessage = "";
                    return Convert.ToBoolean(rd.data);
                }
                else
                {
                    try { LogManager.WriteLog("Error", "validateoutercoupon:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取validateoutercoupon异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return false ;
            }
        }

        /// <summary>
        /// 东方福利网外部优惠券
        /// </summary>
        /// <returns></returns>
        public ValidateOuterCoupon GetValidateOuterCoupon(string couponcode, ref string erromessage)
        {
            try
            {
                string url = "/pos/activity/coupon/activity/validateoutercoupon";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                sort.Add("code",couponcode);

                string json = HttpGET(url, sort);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                // return;
                if (rd.code == 0)
                {
                    erromessage = "";
                    ValidateOuterCoupon result = JsonConvert.DeserializeObject<ValidateOuterCoupon>(rd.data.ToString());
                    return result;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "validateoutercoupon:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取validateoutercoupon异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return null;
            }
        }



        /// <summary>
        /// POS机全量数据同步接口
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="payid"></param>
        /// <param name="erromessage"></param>
        public AllProduct QuerySkushopAll(string shopid, int page, int size, ref string erromessage)
        {
            try
            {
                string url = "/pos/product/pos/queryskushopofflinedtobypageforpossync";

                string tempjson = "{\"shopid\":\"" + shopid + "\",\"page\":" + page + ",\"size\":" + size + "}";

                string json = HttpPOST(url, tempjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                if (rd.code == 0)
                {
                    //string datajsong = rd.data.ToString().Replace("'","''");
                    string datajsong = rd.data.ToString();

                    AllProduct allproduct = JsonConvert.DeserializeObject<AllProduct>(datajsong);
                    return allproduct;

                }
                else
                {
                    try { LogManager.WriteLog("Error", "queryskushopofflinedtobypageforpossync:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "POS机全量数据同步异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return null;
            }
        }

        /// <summary>
        /// POS机增量数据同步接口
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="payid"></param>
        /// <param name="erromessage"></param>
        public AllProduct QuerySkushopIncrement(string shopid, int page, int size, ref string erromessage, ref int resultcode)
        {
            try
            {
                string url = "/pos/product/pos/getincrementskushopofflinedtoforpossync";

                string tempjson = "{\"shopid\":\"" + shopid + "\",\"page\":" + page + ",\"size\":" + size + ",\"timestamp\":\"" + MainModel.LastQuerySkushopCrementTimeStamp + "\"}";

                string json = HttpPOST(url, tempjson);

                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                resultcode = rd.code;
                if (rd.code == 0)
                {
                    AllProduct allproduct = JsonConvert.DeserializeObject<AllProduct>(rd.data.ToString());
                    return allproduct;

                }
                else
                {
                    try { LogManager.WriteLog("Error", "getincrementskushopofflinedtoforpossync:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "POS机增量数据同步异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return null;
            }
        }


        /// <summary>
        /// 获取面板商品价格
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="payid"></param>
        /// <param name="erromessage"></param>
        public List<Product> GetPanelProductPrice(PanelProductPara panelpropara, ref string erromessage, ref int resultcode)
        {
            try
            {
                string url = "/pos/order/pos/panelproduct/prices";


                //panelproduct ppro = new panelproduct();
                //ppro

                string tempjson = JsonConvert.SerializeObject(panelpropara);


                //tempjson = "{\"memberlogin\":0,\"products\":[{\"barcode\":\"20042639\",\"goodstagid\":1,\"isQueryBarcode\":0,\"mainimg\":\"https://pic.qdama.cn/Fszmtkf7p-rpK9wxTT5zcCAZdm9d\",\"num\":1.0,\"pricetagid\":0,\"saleunit\":\"kg\",\"shopid\":\"108888\",\"skucode\":\"20042639\",\"skuname\":\"百香果\",\"specnum\":0.0,\"spectype\":0,\"title\":\"百香果\",\"weightflag\":true},{\"barcode\":\"20048471\",\"goodstagid\":1,\"isQueryBarcode\":0,\"num\":1.0,\"pricetagid\":0,\"saleunit\":\"kg\",\"shopid\":\"108888\",\"skucode\":\"20048471\",\"skuname\":\"百香果W\",\"specnum\":0.0,\"spectype\":0,\"title\":\"百香果W\",\"weightflag\":true}],\"shopid\":\"108888\",\"usertoken\":\"\"}";
                //tempjson = "{\"memberlogin\":0,\"products\":[{\"mainimg\":\"https://pic.qdama.cn/FsoTxQ-fRfS5EUCEaaQOKlJSaVDd\",\"pricetag\":\"\",\"pricetagid\":0,\"saleunit\":\"kg\",\"skucode\":\"20000011\",\"skuname\":\"油麦菜\"}],\"shopid\":\"501001\",\"usertoken\":\"\"}";

                //tempjson = "{\"memberlogin\":0,\"products\":[{\"mainimg\":\"https://pic.qdama.cn/FsoTxQ-fRfS5EUCEaaQOKlJSaVDd\",\"pricetagid\":0,\"saleunit\":\"kg\",\"skucode\":\"20000011\"}],\"shopid\":\"501001\",\"usertoken\":\"\"}";

                //tempjson = MainModel.GB2312ToUTF8(tempjson);
                string json = HttpPOST(url, tempjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                resultcode = rd.code;
                if (rd.code == 0)
                {
                    List<Product> lstproduct = JsonConvert.DeserializeObject<List<Product>>(rd.data.ToString());
                    return lstproduct;

                }
                else
                {
                    try { LogManager.WriteLog("Error", "prices:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取面板商品异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return null;
            }
        }


        #region  电视屏接口

        #region saas
        public string GetTVshowpage(string shopid,string lednum, ref string erromessage)
        {
            string json = "";
            try
            {
                string url = "/pos/common/led/gettvshowpage";


                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                sort.Add("shopid", shopid);
                sort.Add("lednum", lednum);

                 json = HttpGET(url, sort);
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
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取电视屏模板异常：" + json);
                erromessage = "网络连接异常，请检查网络连接";
                return null;
            }
        }

        public PosActivesSku GetActiveSkus(string shopid, string lednum, ref string erromessage)
        {
            try
            {
                string url = "/pos/common/led/getactiveskus";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                sort.Add("shopid", shopid);
                sort.Add("lednum", lednum);

                string json = HttpGET(url, sort);

                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                // return;
                if (rd.code == 0)
                {
                    erromessage = "";
                    PosActivesSku posactivessku = JsonConvert.DeserializeObject<PosActivesSku>(rd.data.ToString());


                    return posactivessku;
                }
                else
                {
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取畅销商品异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return null;
            }
        }


        #endregion

        #region 钱大妈
        /// <summary>
        /// 获取电视屏模板  蔬菜
        /// </summary>
        /// <returns></returns>
        public string GetTVshowpageForPromotion(ref string erromessage)
        {
            try
            {
                string url = "/pos/common/led/gettvshowpageforpromotion";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();

                string json = HttpGET(url, sort);
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
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取电视屏模板（蔬菜）异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return null;
            }
        }

        /// <summary>
        /// 获取电视屏模板  猪肉
        /// </summary>
        /// <returns></returns>
        public string GetTVshowpageForPork(ref string erromessage)
        {
            try
            {
                string url = "/pos/common/led/gettvshowpageforpork";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();

                string json = HttpGET(url, sort);
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
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取电视屏模板（猪肉）异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return null;
            }
        }


        /// <summary>
        /// 畅销商品
        /// </summary>
        /// <returns></returns>
        public PosActivesSku GetActiveSkus(ref string erromessage)
        {
            try
            {
                string url = "/pos/common/led/getactiveskus";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                sort.Add("shopid", MainModel.CurrentShopInfo.shopid);

                string json = HttpGET(url, sort);

                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                // return;
                if (rd.code == 0)
                {
                    erromessage = "";
                    PosActivesSku posactivessku = JsonConvert.DeserializeObject<PosActivesSku>(rd.data.ToString());


                    return posactivessku;
                }
                else
                {
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取畅销商品异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return null;
            }
        }

        /// <summary>
        /// 促销商品
        /// </summary>
        /// <returns></returns>
        public PosActivesSku GetPromotionSkus(ref string erromessage)
        {
            try
            {
                string url = "/pos/common/led/getpromotionskus";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                sort.Add("shopid", MainModel.CurrentShopInfo.shopid);

                string json = HttpGET(url, sort);
                //return json;
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                // return;
                if (rd.code == 0)
                {
                    erromessage = "";

                    PosActivesSku posactivessku = JsonConvert.DeserializeObject<PosActivesSku>(rd.data.ToString());

                    return posactivessku;
                }
                else
                {
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取促销商品异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return null;
            }
        }
        #endregion

        #endregion

        #region 离线接口
        /// <summary>
        /// 添加员工
        /// </summary>
        /// <param name="lstuser"></param>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public List<OffLineUser> PosUser(List<OffLineUser> lstuser, ref string errormsg)
        {
            try
            {

                string url = "/pos/account/sysuser/upload/posuser";


              
                string testjson = JsonConvert.SerializeObject(lstuser);

                string json = HttpPOST(url, testjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    string Token = rd.data.ToString();

                    List<OffLineUser> lstuserresult = JsonConvert.DeserializeObject<List<OffLineUser>>(Token);
                    return lstuserresult;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "posuser:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "添加离线用户异常 ：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return null;
            }
        }


        /// <summary>
        /// 获取员工列表
        /// </summary>
        /// <param name="lstuser"></param>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public List<OffLineUser> GetUserForPos(ref string errormsg)
        {
            try
            {

                string url = "/pos/account/sysuser/list/userforpos";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();

                string testjson = JsonConvert.SerializeObject(sort);

                string json = HttpGET(url,sort);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    string Token = rd.data.ToString();

                    List<OffLineUser> lstuserresult = JsonConvert.DeserializeObject<List<OffLineUser>>(Token);
                    return lstuserresult;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "userforpos:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "添加离线用户异常 ：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return null;
            }
        }

        /// <summary>
        /// 添加离线订单
        /// </summary>
        /// <param name="offlineorder"></param>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public bool CreateOffLineOrder(OffLineOrder offlineorder, ref string errormsg)
        {
            try
            {

                string url = "/pos/order/pos/offline/create";

                //正式环境需要校验
                if (offlineorder.createurlip.Contains("pos.zhuizhikeji"))
                {
                    offlineorder.createurlip = "pos.zhuizhikeji";
                }

                string tempjson = JsonConvert.SerializeObject(offlineorder);

                if (offlineorder.fixpricetotal == 0)
                {
                    
                    tempjson = tempjson.Replace(",\"fixpricetotal\":0.0", "");
                    tempjson = tempjson.Replace(",\"fixpricepromoamt\":0.0", "");
                }

                string json = HttpPOST(url, tempjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    return Convert.ToBoolean(rd.data.ToString().Contains("上传成功"));


                }
                else
                {
                    try { LogManager.WriteLog("Error", "create:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "提交离线订单异常：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return false;
            }
        }

        /// <summary>
        /// 离线订单退款
        /// </summary>
        /// <param name="offlineorder"></param>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public bool OffLineRefund(OffLineOrder offlineorder, ref string errormsg)
        {
            try
            {

                string url = "/pos/order/pos/offline/refund";

                //正式环境需要校验
                if (offlineorder.createurlip.Contains("pos.zhuizhikeji"))
                {
                    offlineorder.createurlip = "pos.zhuizhikeji";
                }

                string tempjson = JsonConvert.SerializeObject(offlineorder);
                if (offlineorder.fixpricetotal == 0)
                {

                    tempjson = tempjson.Replace(",\"fixpricetotal\":0.0", "");
                    tempjson = tempjson.Replace(",\"fixpricepromoamt\":0.0", "");
                }
                string json = HttpPOST(url, tempjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    //TODO
                    return Convert.ToBoolean(rd.data.ToString().Contains("成功"));

                }
                else
                {
                    try { LogManager.WriteLog("Error", "create:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "提交离线订单异常：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return false;
            }
        }

        /// <summary>
        /// 提交离线交班信息
        /// </summary>
        /// <param name="receipt"></param>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public bool UpdateOffLineReceipt(OffLineReceipt receipt, ref string errormsg)
        {
            try
            {

                string url = "/pos/order/pos/offline/receipt/upload";


                //正式环境需要校验
                if (receipt.createurlip.Contains("pos.zhuizhikeji"))
                {
                    receipt.createurlip = "pos.zhuizhikeji";
                }

                string testjson = JsonConvert.SerializeObject(receipt);

                string json = HttpPOST(url, testjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    return Convert.ToBoolean(rd.data.ToString());

                }
                else
                {
                    try { LogManager.WriteLog("Error", "upload:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "提交离线交班异常：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return false;
            }
        }

        /// <summary>
        /// POS机全量cuxiao同步接口
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="payid"></param>
        /// <param name="erromessage"></param>
        public List<DBPROMOTION_CACHE_BEANMODEL> QueryPromotionAll(string shopid, int skip, int size, ref string erromessage)
        {
            try
            {
                string url = "/pos/activity/promotion/listbyshop";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();

                sort.Add("shopid", shopid);
                sort.Add("skip", skip + "");
                sort.Add("size", size + "");

                string json = HttpGET(url, sort);
                if (string.IsNullOrEmpty(json))
                {
                    erromessage = "网络连接异常，请检查网络连接";
                    return null;
                }
                //string json = HttpPOST(url, tempjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                if (rd.code == 0)
                {

                    List<DBPROMOTION_CACHE_BEANMODEL> lstpromotion = JsonConvert.DeserializeObject<List<DBPROMOTION_CACHE_BEANMODEL>>(rd.data.ToString());

                    return lstpromotion;

                }
                else
                {
                    try { LogManager.WriteLog("Error", "listbyshop:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "POS机促销商品拉取异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return null;
            }
        }
        /// <summary>
        /// POS机过滤促销数据同步接口
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="payid"></param>
        /// <param name="erromessage"></param>
        public string QueryPromotionFilter(string shopid, ref string erromessage)
        {
            try
            {
                string url = "/pos/order/pos/offline/promotionswithfilterinfo";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();

                sort.Add("shopid", shopid);
                //sort.Add("skip", skip + "");
                //sort.Add("size", size + "");

                string json = HttpGET(url, sort);

                //string tempjson = "{\"shopid\":\"" + shopid + "\",\"skip\":" + skip + ",\"size\":" + size + "}";


                //string json = HttpPOST(url, tempjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                if (rd.code == 0)
                {

                    return rd.data.ToString();

                }
                else
                {
                    try { LogManager.WriteLog("Error", "promotionswithfilterinfo:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "POS机促销过滤拉取异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return null;
            }
        }
        #endregion

        #region 半离线会员信息 及设置

        /// <summary>
        /// 获取会员标签信息
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="errormsg"></param>
        public Memberoperationitem MemberOperationItem(string memberid, ref string errormsg)
        {
            try
            {

                string url = "/pos/member/memberoperation/item";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                sort.Add("memberid", memberid);
                string testjson = JsonConvert.SerializeObject(sort);

                string json = HttpGET(url, sort);

                if (string.IsNullOrEmpty(json))
                {
                    errormsg = "网络连接异常，请检查网络连接";
                    return null;
                }

                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    string strdata = rd.data.ToString();

                    Memberoperationitem lstuserresult = JsonConvert.DeserializeObject<Memberoperationitem>(strdata);
                    return lstuserresult;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "item:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取会员标签信息异常 ：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return null;
            }
        }

        /// <summary>
        /// 当前会员等级
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="errormsg"></param>
        public Gradesettinggetgrade GradesttingGetGrade(string memberid, ref string errormsg)
        {
            try
            {

                string url = "/pos/member/gradesetting/getgrade";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                sort.Add("memberid", memberid);
                string testjson = JsonConvert.SerializeObject(sort);

                string json = HttpGET(url, sort);

                if (string.IsNullOrEmpty(json))
                {
                    errormsg = "网络连接异常，请检查网络连接";
                    return null;
                }
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    string strdata = rd.data.ToString();

                    Gradesettinggetgrade lstuserresult = JsonConvert.DeserializeObject<Gradesettinggetgrade>(strdata);
                    return lstuserresult;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "getgrade:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取当前会员等级异常 ：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return null;
            }
        }

        /// <summary>
        /// 当前会员等级商户设置
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="errormsg"></param>
        public MemberTenantmembergradeconfig GetTenantMembergradeConfig(ref string errormsg)
        {
            try
            {

                string url = "/pos/member/gradesetting/gettenantmembergradeconfig";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                string testjson = JsonConvert.SerializeObject(sort);

                string json = HttpGET(url, sort);

                if (string.IsNullOrEmpty(json))
                {
                    errormsg = "网络连接异常，请检查网络连接";
                    return null;
                }
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    string strdata = rd.data.ToString();

                    MemberTenantmembergradeconfig lstuserresult = JsonConvert.DeserializeObject<MemberTenantmembergradeconfig>(strdata);
                    return lstuserresult;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "gettenantmembergradeconfig:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取当前会员等级商户设置异常 ：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return null;
            }
        }

        /// <summary>
        /// 获取积分规则
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="errormsg"></param>
        public TenantCreditConfig GetTenantCreditConfig(ref string errormsg)
        {
            try
            {

                string url = "/pos/member/credit/gettenantcreditconfig";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                string testjson = JsonConvert.SerializeObject(sort);

                string json = HttpGET(url, sort);

                if (string.IsNullOrEmpty(json))
                {
                    errormsg = "网络连接异常，请检查网络连接";
                    return null;
                }
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    string strdata = rd.data.ToString();

                    TenantCreditConfig lstuserresult = JsonConvert.DeserializeObject<TenantCreditConfig>(strdata);
                    return lstuserresult;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "gettenantcreditconfig:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取积分规则异常 ：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return null;
            }
        }

        /// <summary>
        /// 用户可用券列表
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="errormsg"></param>
        public List<PromotionCoupon> ListMemberCouponAvailable(string memberid, ref string errormsg)
        {
            try
            {

                string url = "/pos/activity/membercoupon/listmembercouponavailable";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                sort.Add("memberid", memberid);
                string testjson = JsonConvert.SerializeObject(sort);

                string json = HttpGET(url, sort);

                if (string.IsNullOrEmpty(json))
                {
                    errormsg = "网络连接异常，请检查网络连接";
                    return null;
                }

                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    string strdata = rd.data.ToString();
                    List<PromotionCoupon> lstuserresult = JsonConvert.DeserializeObject<List<PromotionCoupon>>(strdata);
                    return lstuserresult;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "listmembercouponavailable:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取用户可用券列表异常：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return null;
            }
        }


        /// <summary>
        /// 会员是否能享受会员权益
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="errormsg"></param>
        public bool EnjoyMemberRights(string memberid, ref string errormsg)
        {
            try
            {

                string url = "/pos/member/memberrights/enjoymemberrights";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                sort.Add("memberid", memberid);
                string testjson = JsonConvert.SerializeObject(sort);

                string json = HttpGET(url, sort);

                if (string.IsNullOrEmpty(json))
                {
                    errormsg = "网络连接异常，请检查网络连接";
                    return false;
                }
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    string strdata = rd.data.ToString();

                    return Convert.ToBoolean(rd.data.ToString());
                }
                else
                {
                    try { LogManager.WriteLog("Error", "enjoymemberrights:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "enjoymemberrights异常：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return false;
            }
        }

        /// <summary>
        /// 会员权益配置获取
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="errormsg"></param>
        public MemberrightsItem GetTenantMemberRightsConfigUsing(string memberid, ref string errormsg)
        {
            try
            {

                string url = "/pos/member/memberrights/item";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                sort.Add("memberid", memberid);
                string testjson = JsonConvert.SerializeObject(sort);

                string json = HttpGET(url, sort);

                if (string.IsNullOrEmpty(json))
                {
                    errormsg = "网络连接异常，请检查网络连接";
                    return null;
                }
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    string strdata = rd.data.ToString();
                    MemberrightsItem result = JsonConvert.DeserializeObject<MemberrightsItem>(strdata);
                    return result;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "memberrights/item:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "memberrights/item异常：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return null;
            }
        }

        /// <summary>
        /// 获取商户可用支付类型
        /// </summary>
        /// <param name="shopid"></param>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public Paymenttypes GetAvailablePaymentTypes(string shopid, ref string errormsg)
        {
            try
            {

                string url = "/pos/order/pos/availablepaymenttypes";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                sort.Add("shopid", shopid);
                string testjson = JsonConvert.SerializeObject(sort);

                string json = HttpGET(url, sort);

                if (string.IsNullOrEmpty(json))
                {
                    errormsg = "网络连接异常，请检查网络连接";
                    return null;
                }
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    string strdata = rd.data.ToString();
                    Paymenttypes lstuserresult = JsonConvert.DeserializeObject<Paymenttypes>(strdata);
                    return lstuserresult;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "availablepaymenttypes:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取可用支付类型异常：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return null;
            }
        }


        /// <summary>
        /// 获取商户会员信息
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="errormsg"></param>
        public MemberTenantItem GetmemberTenantItem(string memberid, ref string errormsg)
        {
            try
            {
                string url = "/pos/member/membertenant/item";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                sort.Add("memberid", memberid);
                string testjson = JsonConvert.SerializeObject(sort);

                string json = HttpGET(url, sort);

                if (string.IsNullOrEmpty(json))
                {
                    errormsg = "网络连接异常，请检查网络连接";
                    return null;
                }

                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    string strdata = rd.data.ToString();
                    MemberTenantItem result = JsonConvert.DeserializeObject<MemberTenantItem>(strdata);
                    return result;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "membertenant/item" + json); }
                    catch { }
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "membertenant/item异常：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return null;
            }
        }

        #endregion

        #region  报损接口
        /// <summary>
        /// 新建报损页 获取商品价格
        /// </summary>
        /// <param name="shopid"></param>
        /// <param name="lstcode"></param>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public List<SkuMovePrice> GetSkuMovePrice( List<string> lstcode, ref string errormsg)
        {
            try
            {

                string url = "/pos/common/warehousedelivery/getskumovepriceforpos";

                SortedDictionary<string, object> sort = new SortedDictionary<string, object>();
                sort.Add("shopid",MainModel.CurrentShopInfo.shopid);
                sort.Add("skucodes", lstcode);

                string testjson = JsonConvert.SerializeObject(sort);

                string json = HttpPOST(url, testjson);

                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    string Token = rd.data.ToString();

                    List<SkuMovePrice> lstuserresult = JsonConvert.DeserializeObject<List<SkuMovePrice>>(Token);
                    return lstuserresult;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "getskumovepriceforpos:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取报损商品价格异常 ：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return null;
            }
        }


        /// <summary>
        /// 新建报损
        /// </summary>
        /// <param name="parabroken"></param>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public CreateBrokenResult CreateBroken(ParaCreateBroken parabroken, ref string errormsg)
        {
            try
            {

                string url = "/pos/common/warehousedelivery/createwarehouseotherdeliverybypos";

                string testjson = JsonConvert.SerializeObject(parabroken);

                string json = HttpPOST(url, testjson);
                LogManager.WriteLog("DEBUG", "新建报损" + json);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    string Token = rd.data.ToString();

                    CreateBrokenResult objresult = JsonConvert.DeserializeObject<CreateBrokenResult>(Token);
                    return objresult;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "createwarehouseotherdeliverybypos:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "新建报损异常 ：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return null;
            }
        }
        
        /// <summary>
        /// 报损单据列表
        /// </summary>
        /// <param name="parapagebroken"></param>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public PageBroken GetPageBroken(ParaPageBroken parapagebroken, ref string errormsg)
        {
            try
            {

                string url = "/pos/common/warehousedelivery/querywarehouseotherdeliveryforposbypage";

                string testjson = JsonConvert.SerializeObject(parapagebroken);

                string json = HttpPOST(url, testjson);

                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    string Token = rd.data.ToString();

                    PageBroken objresult = JsonConvert.DeserializeObject<PageBroken>(Token);
                    return objresult;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "querywarehouseotherdeliveryforposbypage:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "查询报损列表异常 ：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return null;
            }
        }



        /// <summary>
        /// 报损 详情
        /// </summary>
        /// <param name="parapagesku"></param>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public PageSku GetPageBrokenDetail(ParaPageSku parapagesku, ref string errormsg)
        {
            try
            {

                string url = "/pos/common/warehousedelivery/querywarehouseotherdeliveryitemdtobypage";

                string testjson = JsonConvert.SerializeObject(parapagesku);

                string json = HttpPOST(url, testjson);

                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    string Token = rd.data.ToString();

                    PageSku objresult = JsonConvert.DeserializeObject<PageSku>(Token);
                    return objresult;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "querywarehouseotherdeliveryitemdtobypage:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "查询报损详情异常 ：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return null;
            }
        }

        public List<BrokenType> ListSubType(ref string erromessage)
        {
            try
            {
                string url = "/pos/common/warehousedelivery/listsubtype";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();

                string json = HttpGET(url, sort);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                // return;
                if (rd.code == 0)
                {
                    erromessage = "";
                    List<BrokenType> result = JsonConvert.DeserializeObject<List<BrokenType>>(rd.data.ToString());
                    return result;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "listsubtype:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取listsubtype异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return null;
            }
        }

        #endregion



        #region 会员中心
        /// <summary>
        /// 商户所有 充值面额设置
        /// </summary>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public List<ListAllTemplate> ListAllTemplate(ref string errormsg)
        {
            try
            {
                string url = "/pos/member/balance/listalltemplate";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();

                string json = HttpGET(url, sort);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    List<ListAllTemplate> resultobj = JsonConvert.DeserializeObject<List<ListAllTemplate>>(rd.data.ToString());
                    return resultobj;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "listalltemplate:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取用户信息异常：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return null;
            }
        }

        /// <summary>
        /// 商户充值设置详情
        /// </summary>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public BalanceConfigDetail BalanceConfigDetail(ref string errormsg)
        {
            try
            {
                string url = "/pos/member/balance/balanceconfigdetail";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();

                string json = HttpGET(url, sort);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    BalanceConfigDetail currentuser = JsonConvert.DeserializeObject<BalanceConfigDetail>(rd.data.ToString());
                    return currentuser;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "listalltemplate:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取用户信息异常：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return null;
            }
        }

        /// <summary>
        /// 会员充值
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public long MemberTopUp(MemberTopUpPara topuppara, ref string errormsg)
        {
            try
            {
                string url = "/pos/member/balance/deposit/member";

                string testjson = JsonConvert.SerializeObject(topuppara);

                string json = HttpPOST(url, testjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    return Convert.ToInt64(rd.data);
                }
                else
                {
                    try { LogManager.WriteLog("Error", "depositmember:" + json); }
                    catch { }
                    return -1;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "会员充值异常：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return -1;
            }
        }


        /// <summary>
        /// 商户所有 充值面额设置
        /// </summary>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public ZtBalanceAccount ZtBalanceAccount(string memberid,ref string errormsg)
        {
            try
            {
                string url = "/pos/member/balance/ztbalanceaccount";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                sort.Add("memberid",memberid);
                string json = HttpGET(url, sort);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    ZtBalanceAccount resultobj = JsonConvert.DeserializeObject<ZtBalanceAccount>(rd.data.ToString());
                    return resultobj;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "ztbalanceaccount:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "查询会员余额账户异常：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
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
        public bool CancleBalanceOrder(string orderid, string mobile, ref string erromessage)
        {
            try
            {
                string url = "/pos/member/balance/deposit/cancelforpos";
                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                sort.Add("id", orderid);
                sort.Add("mobile", mobile);
                string json = HttpGET(url, sort);

                Console.WriteLine(json);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                //TODO

                if (rd.code == 0)
                {
                    return true;

                }
                else
                {
                    try { LogManager.WriteLog("Error", "cancel:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", orderid + "取消订单异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return false;
            }
        }


        /// <summary>
        /// 会员充值列表
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public PageBalanceDepositBill ListDepositbill(DepositListRequest para, ref string errormsg)
        {
            try
            {
                string url = "/pos/member/balance/listdepositbill";

                string testjson = JsonConvert.SerializeObject(para);

                string json = HttpPOST(url, testjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {

                    PageBalanceDepositBill resultobj = JsonConvert.DeserializeObject<PageBalanceDepositBill>(rd.data.ToString());
                    return resultobj;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "listdepositbill:" + json); }
                    catch { }
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "充值列表异常：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return null;
            }
        }



        /// <summary>
        /// 充值详情
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="reason"></param>
        /// <param name="erromessage"></param>
        /// <returns></returns>
        public TopUpPrint GetDepositbill(string orderid, ref string erromessage)
        {
            try
            {
                string url = "/pos/member/balance/getdepositbill";
                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                sort.Add("depositbillid", orderid);

                string json = HttpGET(url, sort);

               LogManager.WriteLog(json);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                //TODO

                if (rd.code == 0){

                    TopUpPrint resultobj = JsonConvert.DeserializeObject<TopUpPrint>(rd.data.ToString());
                    return resultobj;

                }
                else
                {
                    try { LogManager.WriteLog("Error", "cancel:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", orderid + "取消订单异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return null;
            }
        }
        #endregion


        #region 调价通知
        /// <summary>
        /// 是否有调价记录
        /// </summary>
        /// <param name="parapagebroken"></param>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public AdjustPriceDynamic GetAdjustPriceDynamicForPos(string startdate, string enddate, bool nowtime, ref string errormsg)
        {
            try
            {

                string url = "/pos/product/pos/getadjustpricedynamicforpos";

                SortedDictionary<string, object> sort = new SortedDictionary<string, object>();

                sort.Add("startdate", startdate);
                sort.Add("enddate", enddate);
                sort.Add("nowtime", nowtime);

                string parajson = JsonConvert.SerializeObject(sort);

                string json = HttpPOST(url, parajson);

                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    string Token = rd.data.ToString();

                    AdjustPriceDynamic objresult = JsonConvert.DeserializeObject<AdjustPriceDynamic>(Token);
                    return objresult;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "getadjustpricedynamicforpos:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "查询调价异常 ：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return null;
            }
        }

        /// <summary>
        /// 查询调价记录列表
        /// </summary>
        /// <param name="parapagebroken"></param>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public AdjustPriceRecord GetAdjustPriceRecord(AdjustPricePara adjustpara, ref string errormsg)
        {
            try
            {

                string url = "/pos/product/pos/getadjustpricerecord";

              
                string parajson = JsonConvert.SerializeObject(adjustpara);

                string json = HttpPOST(url, parajson);

                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    string Token = rd.data.ToString();

                    AdjustPriceRecord objresult = JsonConvert.DeserializeObject<AdjustPriceRecord>(Token);
                    return objresult;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "getadjustpricedynamicforpos:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "查询调价异常 ：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return null;
            }
        }

        #endregion

        #region  访问服务端
        private HttpRequest httprequest = new HttpRequest();

        static object lockhttpget = new object();
        public string HttpGET(string Url, SortedDictionary<string, string> sortpara)
        {
                Other.CrearMemory();
                return httprequest.HttpGetJson(Url, sortpara);
        }

        static object lockhttppost = new object();
        public string HttpPOST(string Url, string bodyjson)
        {
                Other.CrearMemory();
                return httprequest.HttpPostJson(Url, bodyjson);      
        }



        //public string HttpGET(string Url, SortedDictionary<string, string> sortpara)
        //{
        //    System.GC.Collect();
        //    string retString = "";

        //    string Timestamp = MainModel.getStampByDateTime(DateTime.Now);
        //    string nonce = MainModel.getNonce();

        //    sortpara.Add("nonce", nonce);
        //    //string signstr = "kVl55eO1n3DZhWC8Z7" + "devicesn" + devicesn + "nonce" + nonce + Timestamp;
        //    string signstr = MainModel.PrivateKey;
        //    foreach (KeyValuePair<string, string> keyvalue in sortpara)
        //    {
        //        signstr += keyvalue.Key + keyvalue.Value;
        //    }
        //    signstr += Timestamp;

        //    //string body = "{\"devicesn\":\"" + devicesn + "\"}";

        //    string postDataStr = "sign=" + MainModel.GetMD5(signstr);
        //    //Url += "?" + "devicesn=" + devicesn + "&" + postDataStr;

        //    Url = MainModel.URL + Url + "?";
        //    foreach (KeyValuePair<string, string> keyvalue in sortpara)
        //    {
        //        Url += keyvalue.Key + "=" + keyvalue.Value + "&";
        //    }
        //    Url += postDataStr;
        //    // //Console.Write(Url);
        //    try
        //    {
        //        ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);

        //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);

        //        request.Timeout = 60 * 1000;

        //        System.Net.ServicePointManager.Expect100Continue = false;

        //        if (Url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
        //        {
        //            //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
        //            request = WebRequest.Create(new Uri(Url)) as HttpWebRequest;
        //            request.ProtocolVersion = HttpVersion.Version10;
        //            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;
        //        }
        //        else
        //        {
        //            request = WebRequest.Create(new Uri(Url)) as HttpWebRequest;
        //        }


        //        request.Method = "GET";

        //        ////请求头
        //        request.Headers.Add("v", MainModel.Version);
        //        request.Headers.Add("X-ZZ-Device-Sn", MainModel.DeviceSN);
        //        request.Headers.Add("POS-Authorization", MainModel.Authorization);
        //        request.Headers.Add("X-ZZ-Timestamp", Timestamp);

        //        request.ContentType = "application/json;charset=UTF-8";

        //        request.KeepAlive = false;
        //        request.UseDefaultCredentials = true;
        //        request.ServicePoint.Expect100Continue = false;//important


        //        System.Net.ServicePointManager.DefaultConnectionLimit = 100;
        //        request.Timeout = 60 * 1000; //3秒钟无响应 网络有问题

        //        ParameterizedThreadStart Pts = new ParameterizedThreadStart(GetResponseResult);
        //        Thread thread = new Thread(Pts);
        //        thread.IsBackground = true;
        //        thread.Start(request);

        //        while (thread.IsAlive)
        //        {
        //            Delay.Start(100);
        //        }

        //        int id = thread.ManagedThreadId;

        //        try
        //        {
        //            retString = dicresult[id];
        //            dicresult.Remove(id);
        //        }
        //        catch (Exception ex)
        //        {
        //            LogManager.WriteLog("线程返回异常" + ex.Message);
        //        }

        //        try
        //        {
        //            request.Abort();
        //        }
        //        catch { }
        //        //if (CheckNet())
        //        //{
        //        //    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        //        //    {
        //        //        request.Timeout = 60 * 1000;
        //        //        Stream myResponseStream = response.GetResponseStream();
        //        //        StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
        //        //        retString = myStreamReader.ReadToEnd();

        //        //        myStreamReader.Close();
        //        //        myResponseStream.Close();

        //        //        try
        //        //        {
        //        //            response.Close();
        //        //        }
        //        //        catch { }
        //        //    }
        //        //    try
        //        //    {
        //        //        request.Abort();
        //        //    }
        //        //    catch { }
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show(ex.StackTrace);
        //        LogManager.WriteLog("访问服务器出错:" + ex.Message);
        //        MainModel.ShowLog("访问服务器出错,请检查网络连接！", false);

        //    }

        //    return retString;
        //}


        //public string HttpPOST(string Url, string bodyjson)
        //{

        //    string retString = "";

        //    string Timestamp = MainModel.getStampByDateTime(DateTime.Now);
        //    string nonce = MainModel.getNonce();
        //    string signstr = MainModel.PrivateKey + "nonce" + nonce + Timestamp + bodyjson;
        //    string postDataStr = "nonce=" + nonce + "&sign=" + MainModel.GetMD5(signstr);

        //    Url = MainModel.URL + Url + "?" + postDataStr;
        //    try
        //    {
        //        ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);

        //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);

        //        System.Net.ServicePointManager.Expect100Continue = false;

        //        if (Url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
        //        {
        //            //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
        //            request = WebRequest.Create(new Uri(Url)) as HttpWebRequest;
        //            request.ProtocolVersion = HttpVersion.Version10;
        //            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;
        //        }
        //        else
        //        {
        //            request = WebRequest.Create(new Uri(Url)) as HttpWebRequest;
        //        }

        //        request.Method = "POST";

        //        ////请求头
        //        request.Headers.Add("v", MainModel.Version);
        //        request.Headers.Add("X-ZZ-Device-Sn", MainModel.DeviceSN);
        //        request.Headers.Add("POS-Authorization", MainModel.Authorization);
        //        request.Headers.Add("X-ZZ-Timestamp", Timestamp);


        //        byte[] by = Encoding.GetEncoding("utf-8").GetBytes(bodyjson);   //请求参数转码
        //        request.ContentType = "application/json;charset=UTF-8";
        //        request.Timeout = 1000 * 60;
        //        request.ContentLength = by.Length;

        //        Stream stw = request.GetRequestStream();     //获取绑定相应流
        //        stw.Write(by, 0, by.Length);      //写入流
        //        stw.Close();    //关闭流

        //        System.Net.ServicePointManager.DefaultConnectionLimit = 100;

        //        ParameterizedThreadStart Pts = new ParameterizedThreadStart(GetResponseResult);
        //        Thread thread = new Thread(Pts);
        //        thread.IsBackground = true;
        //        thread.Start(request);

        //        while (thread.IsAlive)
        //        {
        //            Delay.Start(100);
        //        }

        //        int id = thread.ManagedThreadId;

        //        try
        //        {
        //            retString = dicresult[id];
        //            dicresult.Remove(id);
        //        }
        //        catch (Exception ex)
        //        {
        //            LogManager.WriteLog("线程返回异常" + ex.Message);
        //        }

        //        //request.Timeout = 60 * 1000;
        //        //using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        //        //{
        //        //    request.Timeout = 60 * 1000;
        //        //    Stream myResponseStream = response.GetResponseStream();
        //        //    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
        //        //    retString = myStreamReader.ReadToEnd();

        //        //    myStreamReader.Close();
        //        //    myResponseStream.Close();

        //        //    try
        //        //    {
        //        //        response.Close();
        //        //    }
        //        //    catch { }
        //        //}


        //        try
        //        {
        //            request.Abort();
        //        }
        //        catch { }


        //    }
        //    catch (Exception ex)
        //    {
        //        LogManager.WriteLog("访问服务器出错:" + ex.Message);
        //        //MainModel.ShowLog("访问服务器出错,请检查网络连接！", false);
        //    }

        //    return retString;
        //}

        internal bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            // 总是接受  
            return true;
        }

        //检测IP连接
        bool CheckNet()
        {
            return true;
            bool var = false;

            try
            {
                string ip = "www.baidu.com";
                Ping pingSender = new Ping();

                PingOptions pingOption = new PingOptions();
                pingOption.DontFragment = true;
                string data = "0";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 1000;
                PingReply reply = pingSender.Send(ip, timeout, buffer);
                if (reply.Status == IPStatus.Success)
                    var = true;
                else
                    var = false;
            }
            catch (Exception ex)
            {

                return true;
                // ShowLog("无法检测网络连接是否正常-" + ex.Message, true);
            }

            return var;
        }

        public Dictionary<int, string> dicresult = new Dictionary<int, string>();
        private void GetResponseResult(object obj)
        {
            try
            {
                WebRequest request = (WebRequest)obj;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    request.Timeout = 60 * 1000;
                    Stream myResponseStream = response.GetResponseStream();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                    string result = myStreamReader.ReadToEnd();

                    dicresult.Add(Thread.CurrentThread.ManagedThreadId, result);
                    myStreamReader.Close();
                    myResponseStream.Close();

                    try
                    {
                        response.Close();
                    }
                    catch { }

                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("接口访问异常" + ex.Message + ex.StackTrace);
            }
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
