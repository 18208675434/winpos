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


    public class DbJsonUtil
    {

        private static string balancedepositinfo = "balancedepositinfo"; //充值明细
        public static string EntityCardBatchSale = "entitycardbatchsale"; //批量售卡

        private static string TopUP = "充值明细:";
        private static string TotalAmount = "总计:";
        private static JSON_BEANBLL jsonbll = new JSON_BEANBLL();


        public static List<OrderPriceDetail> GetBalanceInfo()
        {
            try
            {
                JSON_BEANMODEL jsonmodel = jsonbll.GetModel(balancedepositinfo);
                if (jsonmodel != null && jsonmodel.JSON != null)
                {
                    List<OrderPriceDetail> TotalDetail = JsonConvert.DeserializeObject<List<OrderPriceDetail>>(jsonmodel.JSON);

                    jsonbll.Delete(balancedepositinfo);
                    return TotalDetail;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 记录充值明细 交班时提交打印到交班小票
        /// </summary>
        /// <param name="title"></param>
        /// <param name="amount"></param>
        /// <param name="isrefund"></param>
        public static void AddBalanceInfo(string title, decimal amount, bool isrefund = false)
        {

            try
            {

                List<OrderPriceDetail> TotalDetail = null;
                OrderPriceDetail CurrentDetail = null;
                try
                {
                    JSON_BEANMODEL jsonmodel = jsonbll.GetModel(balancedepositinfo);
                    if (jsonmodel != null && jsonmodel.JSON != null)
                    {
                        TotalDetail = JsonConvert.DeserializeObject<List<OrderPriceDetail>>(jsonmodel.JSON);
                        CurrentDetail = TotalDetail.FirstOrDefault(r => r.title == title);
                    }
                }
                catch { }


                if (TotalDetail == null)
                {
                    TotalDetail = new List<OrderPriceDetail>();
                }

                if (CurrentDetail == null)
                {
                    CurrentDetail = new OrderPriceDetail();
                    CurrentDetail.title = title;
                    CurrentDetail.amount = amount.ToString("f2");
                    CurrentDetail.subtitle = "1";
                    TotalDetail.Add(CurrentDetail);
                }
                else
                {

                    //CurrentDetail.amount = (Convert.ToDecimal(CurrentDetail.amount) + amount).ToString("f2");
                    //CurrentDetail.subtitle = (Convert.ToInt16(CurrentDetail.subtitle) + 1).ToString();
                    //充值退款要本地合计
                    if (!isrefund)
                    {
                        CurrentDetail.amount = (Convert.ToDecimal(CurrentDetail.amount) + amount).ToString("f2");
                        CurrentDetail.subtitle = (Convert.ToInt16(CurrentDetail.subtitle) + 1).ToString();
                    }
                    else
                    {
                        CurrentDetail.amount = (Convert.ToDecimal(CurrentDetail.amount) - amount).ToString("f2");
                        CurrentDetail.subtitle = (Convert.ToInt16(CurrentDetail.subtitle) - 1).ToString();
                    }

                }


                // CurrentDetail.childdetail.ad


                jsonbll.Delete(balancedepositinfo);
                JSON_BEANMODEL newjsonmodel = new JSON_BEANMODEL();
                newjsonmodel.CONDITION = balancedepositinfo;
                newjsonmodel.CREATE_TIME = DateTime.Now.ToString("yyyyMMddHHmmss");
                newjsonmodel.DEVICESN = MainModel.DeviceSN;
                newjsonmodel.CREATE_URL_IP = MainModel.URL;
                newjsonmodel.JSON = JsonConvert.SerializeObject(TotalDetail);
                jsonbll.Add(newjsonmodel);



                //List<OrderPriceDetail> TotalDetail = null;
                //OrderPriceDetail CurrentDetail = null;
                //OrderPriceDetail ChildDetail = null;

                //try
                //{
                //    JSON_BEANMODEL jsonmodel = jsonbll.GetModel(balancedepositinfo);
                //    if (jsonmodel != null && jsonmodel.JSON != null)
                //    {
                //        TotalDetail = JsonConvert.DeserializeObject<List<OrderPriceDetail>>(jsonmodel.JSON);

                //        CurrentDetail = TotalDetail.FirstOrDefault(r=> r.title==TopUP);

                //        if(CurrentDetail!=null)
                //        ChildDetail = CurrentDetail.childdetail.FirstOrDefault(r => r.title == title);
                //    }
                //    else
                //    {
                //        TotalDetail = new List<OrderPriceDetail>();

                //        OrderPriceDetail headdetail = new OrderPriceDetail();
                //        headdetail.title = TopUP;
                //        headdetail.amount = "";

                //        TotalDetail.Add(headdetail);
                //    }
                //}
                //catch { }


                //if (CurrentDetail == null)
                //{
                //    CurrentDetail = new OrderPriceDetail();
                //    CurrentDetail.childdetail = new List<OrderPriceDetail>();
                //    CurrentDetail.title = TopUP;
                //}

                //if (ChildDetail == null)
                //{
                //    ChildDetail = new OrderPriceDetail();
                //    ChildDetail.title = title;
                //    ChildDetail.amount = amount.ToString("f2");
                //    ChildDetail.subtitle = "1";
                //    CurrentDetail.childdetail.Add(ChildDetail);
                //}
                //else
                //{
                //    if (!isrefund)
                //    {
                //        ChildDetail.amount = (Convert.ToDecimal(ChildDetail.amount) + amount).ToString("f2");
                //        ChildDetail.subtitle = (Convert.ToInt16(ChildDetail.subtitle) + 1).ToString();
                //    }
                //    else
                //    {
                //        ChildDetail.amount = (Convert.ToDecimal(ChildDetail.amount) - amount).ToString("f2");
                //        ChildDetail.subtitle = (Convert.ToInt16(ChildDetail.subtitle) - 1).ToString();
                //    }

                //}


                //OrderPriceDetail enddetail = CurrentDetail.childdetail.FirstOrDefault(r=> r.title==TotalAmount);

                //if (enddetail != null)
                //{
                //    if (!isrefund)
                //    {
                //        enddetail.amount = (Convert.ToDecimal(enddetail.amount) + amount).ToString("f2");

                //    }
                //    else
                //    {
                //        enddetail.amount = (Convert.ToDecimal(enddetail.amount) - amount).ToString("f2");
                //    }
                //}
                //else
                //{
                //    enddetail = new OrderPriceDetail();
                //    enddetail.title = title;
                //    enddetail.amount = CurrentDetail.childdetail.Sum(r=> Convert.ToDecimal(r.amount)).ToString("f2");
                //}
                //CurrentDetail.childdetail.Add(enddetail);

                //TotalDetail.Add(CurrentDetail);

                //jsonbll.Delete(balancedepositinfo);
                //JSON_BEANMODEL newjsonmodel = new JSON_BEANMODEL();
                //newjsonmodel.CONDITION = balancedepositinfo;
                //newjsonmodel.CREATE_TIME = DateTime.Now.ToString("yyyyMMddHHmmss");
                //newjsonmodel.DEVICESN = MainModel.DeviceSN;
                //newjsonmodel.CREATE_URL_IP = MainModel.URL;
                //newjsonmodel.JSON = JsonConvert.SerializeObject(TotalDetail);
                //jsonbll.Add(newjsonmodel);

            }
            catch { }
        }

        /// <summary>
        /// 获取记录
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetRecord<T>(string key)
        {
            try
            {
                JSON_BEANMODEL model = jsonbll.GetModel(key);
                if (model != null)
                {
                    return JsonConvert.DeserializeObject<T>(model.JSON);
                }               
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("DelRecord->key:" + ex.Message);
            }
            return default(T);
        }

        /// <summary>
        /// 新增或者更新记录
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="jsonData">数据</param>
        public static void AddAndUpdateRecord<T>(string key, T t)
        {
            try
            {
                jsonbll.Delete(key);
                JSON_BEANMODEL newjsonmodel = new JSON_BEANMODEL();
                newjsonmodel.CONDITION = key;
                newjsonmodel.CREATE_TIME = DateTime.Now.ToString("yyyyMMddHHmmss");
                newjsonmodel.DEVICESN = MainModel.DeviceSN;
                newjsonmodel.CREATE_URL_IP = MainModel.URL;
                newjsonmodel.JSON = JsonConvert.SerializeObject(t);
                jsonbll.Add(newjsonmodel);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("DelRecord->key:" + ex.Message);
            }
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="key"></param>
        public static void DelRecord(string key)
        {
            try
            {
                jsonbll.Delete(key);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("DelRecord->key:" + ex.Message);
            }
        }

        public static void DeleteBalanceInfo()
        {
            jsonbll.Delete(balancedepositinfo);
        }

    }
}
