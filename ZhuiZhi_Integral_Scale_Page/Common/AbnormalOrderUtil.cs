using Maticsoft.BLL;
using Maticsoft.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Common
{
    public class AbnormalOrderUtil
    {
        private static JSON_BEANBLL jsonbll = new JSON_BEANBLL();

        /// <summary>
        /// 记录打开钱箱次数
        /// </summary>
        public static void OpenBoxList()
        {
            try
            {
                AbnormalOrderItemVo aoi = new AbnormalOrderItemVo();
                aoi.date = MainModel.getStampByDateTime(DateTime.Now);
                aoi.phone = MainModel.CurrentUser.loginaccount;
                aoi.type = 1;

                InsertToDbJson(aoi);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("记录打开钱箱异常" + ex.Message);
            }
        }
        /// <summary>
        /// 记录挂单
        /// </summary>
        public static void HookOrderList(Cart cart)
        {
            try
            {
                AbnormalOrderItemVo aoi = new AbnormalOrderItemVo();
                aoi.date = MainModel.getStampByDateTime(DateTime.Now);
                aoi.phone = MainModel.CurrentUser.loginaccount;
                aoi.type = 2;

                aoi.amt = cart.totalpayment;
                aoi.skucodes = cart.products.Select(r => r.skucode).ToList();

                InsertToDbJson(aoi);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("记录挂单异常" + ex.Message);
            }
        }
        /// <summary>
        /// 记录整单取消
        /// </summary>
        public static void WholeCancelOrderLsit(Cart cart)
        {
            try
            {
                AbnormalOrderItemVo aoi = new AbnormalOrderItemVo();
                aoi.date = MainModel.getStampByDateTime(DateTime.Now);
                aoi.phone = MainModel.CurrentUser.loginaccount;
                aoi.type = 3;

                aoi.amt = cart.totalpayment;
                aoi.skucodes = cart.products.Select(r => r.skucode).ToList();

                InsertToDbJson(aoi);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("记录整单取消异常" + ex.Message);
            }
        }
        /// <summary>
        /// 记录删除商品
        /// </summary>
        public static void DeleteSkuList(Product pro)
        {
            try
            {
                AbnormalOrderItemVo aoi = new AbnormalOrderItemVo();
                aoi.date = MainModel.getStampByDateTime(DateTime.Now);
                aoi.phone = MainModel.CurrentUser.loginaccount;
                aoi.type = 4;

                aoi.amt = pro.price.total;
                aoi.skucodes = new List<string>() { pro.skucode };

                InsertToDbJson(aoi);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("记录删除商品异常" + ex.Message);
            }
        }

        /// <summary>
        /// 记录退款
        /// </summary>
        public static void RefundOrderList(string orderid, RefundPara refund)
        {
            try
            {
                AbnormalOrderItemVo aoi = new AbnormalOrderItemVo();
                aoi.date = MainModel.getStampByDateTime(DateTime.Now);
                aoi.phone = MainModel.CurrentUser.loginaccount;
                aoi.type = 5;
                aoi.orderid = orderid;
                aoi.amt = refund.orderrefunditems.Sum(r => r.refundamt);
                aoi.skucodes = refund.orderrefunditems.Select(r => r.goodsid).ToList();

                InsertToDbJson(aoi);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("记录退款异常" + ex.Message);
            }
        }
        /// <summary>
        /// 记录退差价
        /// </summary>
        public static void RefundDiffList(string orderid, RefundPara refund)
        {
            try
            {
                AbnormalOrderItemVo aoi = new AbnormalOrderItemVo();
                aoi.date = MainModel.getStampByDateTime(DateTime.Now);
                aoi.phone = MainModel.CurrentUser.loginaccount;
                aoi.type = 6;
                aoi.orderid = orderid;
                aoi.amt = refund.orderrefunditems.Sum(r => r.refundamt);
                aoi.skucodes = refund.orderrefunditems.Select(r => r.goodsid).ToList();

                InsertToDbJson(aoi);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("记录退差价异常" + ex.Message);
            }
        }
        /// <summary>
        /// 记录无单退款
        /// </summary>
        public static void RefundNoOrderList(string orderid, Cart cart)
        {
            try
            {
                AbnormalOrderItemVo aoi = new AbnormalOrderItemVo();
                aoi.date = MainModel.getStampByDateTime(DateTime.Now);
                aoi.phone = MainModel.CurrentUser.loginaccount;
                aoi.type = 7;
                aoi.orderid = orderid;
                aoi.amt = cart.totalpayment;
                aoi.skucodes = cart.products.Select(r => r.skucode).ToList();

                InsertToDbJson(aoi);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("记录无单退款异常" + ex.Message);
            }
        }
       
        /// <summary>
        /// 记录单品改价
        /// </summary>
        /// <param name="pro"></param>
        /// <param name="amt">调整金额</param>
        public static void SingleAdjustPrice(Product pro,decimal amt)
        {
            try
            {
                AbnormalOrderItemVo aoi = new AbnormalOrderItemVo();
                aoi.date = MainModel.getStampByDateTime(DateTime.Now);
                aoi.phone = MainModel.CurrentUser.loginaccount;
                aoi.type = 8;

                aoi.amt = amt;
                aoi.skucodes = new List<string>() { pro.skucode };

                InsertToDbJson(aoi);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("记录单品改价异常" + ex.Message);
            }
        } 

        /// <summary>
        /// 整单改价
        /// </summary>
        /// <param name="cart"></param>
        public static void WholeAdjustOrder(Cart cart)
        {
            try
            {
                AbnormalOrderItemVo aoi = new AbnormalOrderItemVo();
                aoi.date = MainModel.getStampByDateTime(DateTime.Now);
                aoi.phone = MainModel.CurrentUser.loginaccount;
                aoi.type = 9;

                aoi.amt = cart.totalpayment;
                aoi.skucodes = cart.products.Select(r => r.skucode).ToList();

                InsertToDbJson(aoi);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("记录整单改价异常" + ex.Message);
            }
        }


        private static void InsertToDbJson(AbnormalOrderItemVo aoi)
        {
            JSON_BEANMODEL jsonmodel = new JSON_BEANMODEL();
            jsonmodel.CONDITION = aoi.type.ToString();
            jsonmodel.CREATE_TIME = DateTime.Now.ToString("yyyyMMddHHmmss");
            jsonmodel.DEVICESN = MainModel.DeviceSN;
            jsonmodel.CREATE_URL_IP = MainModel.URL;
            jsonmodel.JSON = JsonConvert.SerializeObject(aoi);
            jsonbll.Add(jsonmodel);
        }



        public static AbnormalOrder GetAbnormalOrder()
        {
            try
            {

                List<JSON_BEANMODEL> jsonmodels = jsonbll.GetModelList(" CONDITION in('1','2','3','4','5','6','7','8','9')");

                if (jsonmodels == null || jsonmodels.Count == 0)
                {
                    return null;
                }

                List<AbnormalOrderItemVo> lstaoi = new List<AbnormalOrderItemVo>();
                foreach (JSON_BEANMODEL jsonmodel in jsonmodels)
                {
                    lstaoi.Add(JsonConvert.DeserializeObject<AbnormalOrderItemVo>(jsonmodel.JSON));
                }

                AbnormalOrder ao = new AbnormalOrder();
                ao.openboxlist = lstaoi.Where(r => r.type == 1).ToList();
                ao.hookorderlist = lstaoi.Where(r => r.type == 2).ToList();
                ao.wholecancelorderlist = lstaoi.Where(r => r.type == 13).ToList();
                ao.deleteskulist = lstaoi.Where(r => r.type == 4).ToList();
                ao.refundorderlist = lstaoi.Where(r => r.type == 5).ToList(); ;
                ao.refunddifflist = lstaoi.Where(r => r.type == 6).ToList();
                ao.refundnoorderlist = lstaoi.Where(r => r.type == 7).ToList();
                ao.singleadjustpricelist = lstaoi.Where(r => r.type == 8).ToList();
                ao.wholeadjustorderlist = lstaoi.Where(r => r.type == 9).ToList();

                ao.shopid = MainModel.CurrentShopInfo.shopid;
                return ao;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("获取异常订单异常" + ex.Message);
                return null;
            }
        }

        private static HttpUtil httputil = new HttpUtil();

        public static void UploadAbnormalOrder()
        {
            try
            {

           
            AbnormalOrder ao = GetAbnormalOrder();
            if (ao != null)
            {
                string errormsg = "";
                if (httputil.AbnormalOrder(ao, ref errormsg))
                {
                    jsonbll.GetSingle("delete from JSON_BEAN where CONDITION in('1','2','3','4','5','6','7','8','9') ");
                }
                else
                {
                    LogManager.WriteLog("上传异常订单失败"+errormsg);
                }
            }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("上传异常订单异常" + ex.Message);
            }

        }
        //private static List<AbnormalOrderItemVo> GetLstByType(List<AbnormalOrderItemVo> lstaoi,string type)


        public class AbnormalOrderItemVo
        {
            /// <summary>
            /// 
            /// </summary>
            public decimal amt { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string date { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string orderid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string phone { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<string> skucodes { get; set; }
            /// <summary>
            /// 异常类型 1(打开钱箱) 2(挂单) 3(整单取消) 4(删除商品) 5(退款) 6(退差价) 7(无单退款) 8(单品改价) 9(整单改价)
            /// </summary>
            public int type { get; set; }
        }


        public class AbnormalOrder
        {
            /// <summary>
            /// 
            /// </summary>
            public List<AbnormalOrderItemVo> deleteskulist { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<AbnormalOrderItemVo> hookorderlist { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<AbnormalOrderItemVo> openboxlist { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<AbnormalOrderItemVo> refunddifflist { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<AbnormalOrderItemVo> refundnoorderlist { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<AbnormalOrderItemVo> refundorderlist { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string shopid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<AbnormalOrderItemVo> singleadjustpricelist { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<AbnormalOrderItemVo> wholeadjustorderlist { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<AbnormalOrderItemVo> wholecancelorderlist { get; set; }
        }

    }
}
