using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.GiftCard.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.GiftCard
{
    public class GiftCardHttp
    {

        /// <summary>
        /// 查询礼品卡 信息
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public GiftCardDetail GetPrepaiidCardDetail(string cardno, ref string errormsg,ref int resultcode)
        {
            try
            {
                string url = "/pos/order/common/order/alone/getprepaidcarddetail";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                sort.Add("cardno", cardno);


                string json = HttpGET(url, sort);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                resultcode = rd.code;
                if (rd.code == 0)
                {
                    GiftCardDetail objresult = JsonConvert.DeserializeObject<GiftCardDetail>(rd.data.ToString());
       
                    return objresult;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "getprepaidcarddetail:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取礼品卡信息异常：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return null;
            }
        }

        /// <summary>
        /// 校验卡密
        /// </summary>
        /// <param name="cardno"></param>
        /// <param name="secret"></param>
        /// <param name="erromessage"></param>
        /// <returns></returns>
        public bool ValidatePrepaidCard(string cardno, string secret, ref string erromessage)
        {
            try
            {
                string url = "/pos/order/common/order/alone/validateprepaidcard";
                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                sort.Add("cardno", cardno);
                sort.Add("secret", secret);
                sort.Add("tenantid", MainModel.CurrentShopInfo.tenantid);

                string parajson = JsonConvert.SerializeObject(sort);
                string json = HttpPOST(url, parajson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                if (rd.code == 0)
                {
                    ValidateGiftCard valid = JsonConvert.DeserializeObject<ValidateGiftCard>(rd.data.ToString());
                    if (!valid.success)
                    {
                        erromessage = rd.message;
                    }
                    return valid.success;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "validateprepaidcard:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error",  "礼品卡密码校验异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return false;
            }
        }

        /// <summary>
        /// 单独订单购物车接口
        /// </summary>
        /// <param name="cardno"></param>
        /// <param name="secret"></param>
        /// <param name="erromessage"></param>
        /// <returns></returns>
        public CartAloneUpdate CartAloneUpdate(CartAloneUpdate cartpara, ref string erromessage, ref int resultcode)
        {
            try
            {

                cartpara.carttype = "gift.card";
                string url = "/pos/order/common/cart/alone/update";

                string parajson = JsonConvert.SerializeObject(cartpara);
                string json = HttpPOST(url, parajson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                resultcode = rd.code;
                if (rd.code == 0)
                {
                    CartAloneUpdate objresult = JsonConvert.DeserializeObject<CartAloneUpdate>(rd.data.ToString());
                    return objresult;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "CartAloneUpdate:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "礼品卡购物车异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return null;
            }
        }


        public BindingResult BindingMember(BindingMemberPara bindingpara, ref string erromessage)
        {
            try
            {
                string url = "/pos/order/common/order/alone/bindingmember";

                string parajson = JsonConvert.SerializeObject(bindingpara);
                string json = HttpPOST(url, parajson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                if (rd.code == 0)
                {
                    BindingResult objresult = JsonConvert.DeserializeObject<BindingResult>(rd.data.ToString());
                    return objresult;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "bindingmember:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "礼品卡绑定会员异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return null;
            }
        }

        /// <summary>
        /// 创建礼品卡订单
        /// </summary>
        /// <param name="createpara"></param>
        /// <param name="erromessage"></param>
        /// <returns></returns>
        public CreateCardOrder CreateCardOrder(CreateCardOrderPara createpara, ref string erromessage)
        {
            try
            {
                string url = "/pos/order/common/order/alone/create";

                string parajson = JsonConvert.SerializeObject(createpara);
                string json = HttpPOST(url, parajson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                if (rd.code == 0)
                {
                    CreateCardOrder objresult = JsonConvert.DeserializeObject<CreateCardOrder>(rd.data.ToString());
                    return objresult;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "alone/create:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "创建礼品卡订单异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return null;
            }
        }


        public bool CancelCardOrder(string orderid, ref string errormsg)
        {
            try
            {
                string url = "/pos/order/common/order/alone/cancel";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                sort.Add("orderid", orderid);


                string json = HttpGET(url, sort);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    return Convert.ToBoolean(rd.data);
                }
                else
                {
                    try { LogManager.WriteLog("Error", "alone/cancel:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "取消礼品卡订单异常：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return false;
            }
        }


        public GiftCardPaySuccess CardPaySuccess(string orderid, ref string errormsg)
        {
            try
            {
                string url = "/pos/order/common/order/alone/paysuccess";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                sort.Add("orderid", orderid);


                string json = HttpGET(url, sort);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    GiftCardPaySuccess objresult = JsonConvert.DeserializeObject<GiftCardPaySuccess>(rd.data.ToString());
                    return objresult;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "paysuccess:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "礼品卡支付成功异常：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return null;
            }
        }


        /// <summary>
        /// 查询礼品卡购买记录
        /// </summary>
        /// <param name="createpara"></param>
        /// <param name="erromessage"></param>
        /// <returns></returns>
        public GifrCardRecord GiftCardQuery(GiftCardRecordPara createpara, ref string erromessage)
        {
            try
            {
                string url = "/pos/order/pos/giftcard/query";

                string parajson = JsonConvert.SerializeObject(createpara);
                string json = HttpPOST(url, parajson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                if (rd.code == 0)
                {
                    GifrCardRecord objresult = JsonConvert.DeserializeObject<GifrCardRecord>(rd.data.ToString());
                    return objresult;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "giftcard/query:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "查询礼品卡购买记录异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return null;
            }
        }

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

        #endregion

    }
}
