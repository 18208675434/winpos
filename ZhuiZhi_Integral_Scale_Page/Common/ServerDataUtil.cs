
using Maticsoft.BLL;
using Maticsoft.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.Model.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ZhuiZhi_Integral_Scale_UncleFruit.Model.Promotion;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Common
{

    public class ServerDataUtil
    {
        private static HttpUtil httputil = new HttpUtil();
        private static JSON_BEANBLL jsonbll = new JSON_BEANBLL();
        private static DBPRODUCT_BEANBLL productbll = new DBPRODUCT_BEANBLL();
        #region  拉去全部促销商品
        private static DBPROMOTION_CACHE_BEANBLL promotionbll = new DBPROMOTION_CACHE_BEANBLL();
        private static List<DBPROMOTION_CACHE_BEANMODEL> lstAllPromotion = new List<DBPROMOTION_CACHE_BEANMODEL>();


        public static void UpdatePromotion()
        {
            try
            {

                lstAllPromotion.Clear();

                List<DBPROMOTION_CACHE_BEANMODEL> lstpromotion = GetAllPromotion(0, 500);

                List<DBPROMOTION_CACHE_BEANMODEL> mlist = new List<DBPROMOTION_CACHE_BEANMODEL>();

                if (lstpromotion != null && lstpromotion.Count > 0)
                {

                    for (int i = 0; i < lstpromotion.Count; i++)
                    {
                        if ("I".Equals(lstpromotion[i].PROMOTYPE))
                        {
                            mlist.Add(lstpromotion[i]);
                        }
                        else if ("O".Equals(lstpromotion[i].PROMOTYPE))
                        {
                            mlist.Add(lstpromotion[i]);
                        }
                        else if (EnumPromotionType.ORDERCOUPON.Equals(lstpromotion[i].PROMOTYPE))
                        {
                            mlist.Add(lstpromotion[i]);
                        }
                    }

                    if (mlist.Count > 0)
                    {
                        promotionbll.AddPromotion(mlist, MainModel.CurrentShopInfo.tenantid, MainModel.CurrentShopInfo.shopid, MainModel.URL);
                    }
                }
                else
                {
                    //没有的话删除所有数据
                    promotionbll.AddPromotion(mlist, MainModel.CurrentShopInfo.tenantid, MainModel.CurrentShopInfo.shopid, MainModel.URL);
                }
               
                //钱大妈用 saas不需要
                //UpdatePromotionFilter();
               // PromotionCache.getInstance().init(MainModel.CurrentShopInfo.tenantid, MainModel.CurrentShopInfo.shopid);
                PromotionCache.getInstance().init(MainModel.CurrentShopInfo.tenantid, MainModel.CurrentShopInfo.shopid);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("加载促销商品异常" + ex.Message);
            }
        }
        public static List<DBPROMOTION_CACHE_BEANMODEL> GetAllPromotion(int page, int size)
        {
            try
            {

                int skip = page * size;
                string errormesg = "";

                List<DBPROMOTION_CACHE_BEANMODEL> lstpromotion = httputil.QueryPromotionAll(MainModel.CurrentShopInfo.shopid, skip, size, ref errormesg);

                if (!string.IsNullOrEmpty(errormesg) || lstpromotion == null)
                {
                    LogManager.WriteLog("更新全部商品异常" + errormesg);
                    return null;
                }
                else
                {

                    lstAllPromotion.AddRange(lstpromotion);
                    if (lstpromotion.Count >= size)
                    {
                        GetAllPromotion(page + 1, size);
                    }
                }

                return lstAllPromotion;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ERROR", "获取全量商品异常" + ex.Message);
                return null;
            }

            return null;
        }

        public void UpdatePromotionFilter()
        {
            try
            {
                string errormesg = "";
                string strjson = httputil.QueryPromotionFilter(MainModel.CurrentShopInfo.shopid, ref errormesg);

                if (errormesg != "" || string.IsNullOrEmpty(strjson))
                {
                    LogManager.WriteLog("ERROR", errormesg);
                }
                else
                {
                    jsonbll.Delete(ConditionType.PromotionsInvalidBean);
                    JSON_BEANMODEL jsonmodel = new JSON_BEANMODEL();
                    jsonmodel.CONDITION = ConditionType.PromotionsInvalidBean;
                    jsonmodel.CREATE_TIME = DateTime.Now.ToString("yyyyMMddHHmmss");
                    jsonmodel.DEVICESN = MainModel.DeviceSN;
                    jsonmodel.CREATE_URL_IP = MainModel.URL;
                    jsonmodel.JSON = strjson;
                    jsonbll.Add(jsonmodel);
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("更新促销过滤条件异常" + ex.Message);
            }
        }
        #endregion




        #region 全量商品
        //当天第一次进入更新全局商品
        public static void LoadAllProduct()
        {
            try
            {
                string errormesg = "";

                int i = 0;
                lstproduct.Clear();

                productbll.ExecuteSql("update DBPRODUCT_BEAN set LOCALSTATUS=-1");
                
                productbll.AddProduct(GetAllProdcut(1, 200), MainModel.URL);

                productbll.ExecuteSql("delete from DBPRODUCT_BEAN where LOCALSTATUS=-1");

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("更新全部商品异常" + ex.Message);
            }

        }

        private static List<DBPRODUCT_BEANMODEL> lstproduct = new List<DBPRODUCT_BEANMODEL>();
        public static List<DBPRODUCT_BEANMODEL> GetAllProdcut(int page, int size)
        {
            try
            {
                string errormesg = "";
                AllProduct allproduct = httputil.QuerySkushopAll(MainModel.CurrentShopInfo.shopid, page, size, ref errormesg);

                if (!string.IsNullOrEmpty(errormesg) || allproduct == null)
                {
                    LogManager.WriteLog("更新全部商品异常" + errormesg);
                    return null;
                }
                else
                {
                    MainModel.LastQuerySkushopCrementTimeStamp = allproduct.timestamp.ToString();
                    MainModel.LastQuerySkushopAllTimeStamp = allproduct.timestamp.ToString();

                    lstproduct.AddRange(allproduct.rows);
                    if (allproduct.rows.Count >= size)
                    {
                        GetAllProdcut(page + 1, size);
                    }
                }

                return lstproduct;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ERROR", "获取全量商品异常" + ex.Message);
                return null;
            }
        }
        #endregion

        #region 增量商品

       // private static Scale_Toledo scaletoledo = new Scale_Toledo();
        public static void LoadIncrementProduct()
        {
            try
            {
                if (MainModel.CurrentShopInfo == null)
                {
                    return;
                }
                string errormesg = "";

                int i = 0;
                lstIncrementproduct.Clear();
                List<DBPRODUCT_BEANMODEL> lstpro = GetIncrementProdcut(1, 100);
                if (lstpro != null && lstpro.Count > 0)
                {
                    productbll.AddProduct(lstpro, MainModel.URL);
                    LogManager.WriteLog("添加增量商品数量：" + lstpro.Count);
                   // scaletoledo.SendIncrementPLU(lstpro);
                }


            }
            catch (Exception ex)
            {
                LogManager.WriteLog("更新增量商品异常" + ex.Message);
            }
        }
        private static List<DBPRODUCT_BEANMODEL> lstIncrementproduct = new List<DBPRODUCT_BEANMODEL>();
        public static List<DBPRODUCT_BEANMODEL> GetIncrementProdcut(int page, int size)
        {
            try
            {
                string errormesg = "";
                int ResultCode = -1;
                AllProduct allproduct = httputil.QuerySkushopIncrement(MainModel.CurrentShopInfo.shopid, page, size, ref errormesg, ref ResultCode);

                if (!string.IsNullOrEmpty(errormesg) || allproduct == null)
                {

                    //代表超过增量更新上限，需要全量更新
                    if (ResultCode == 160046)
                    {
                        Thread threadLoadAllProduct = new Thread(LoadAllProduct);
                        threadLoadAllProduct.IsBackground = true;
                        threadLoadAllProduct.Start();
                    }
                    else
                    {
                        LogManager.WriteLog("更新增量商品异常： " + errormesg + ResultCode);

                    }

                    return null;
                }
                else
                {
                    MainModel.LastQuerySkushopCrementTimeStamp = allproduct.timestamp.ToString();

                    lstIncrementproduct.AddRange(allproduct.rows);
                    if (allproduct.rows.Count >= size)
                    {
                        GetIncrementProdcut(page + 1, size);
                    }
                }

                return lstIncrementproduct;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ERROR", "获取全量商品异常" + ex.Message);
                return null;
            }
        }
        #endregion

        #region 电视屏数据

        public static void LoadTVSkus()
        {
            try
            {
                if (MainModel.CurrentShopInfo == null)
                {
                    return;
                }
                string ErrorMsg = "";
                if (string.IsNullOrEmpty(MainModel.TvShowPage1))
                {
                    MainModel.TvShowPage1 = httputil.GetTVshowpage(MainModel.CurrentShopInfo.shopid, "1", ref ErrorMsg);
                }

                if (string.IsNullOrEmpty(MainModel.TvShowPage2))
                {
                    MainModel.TvShowPage2 = httputil.GetTVshowpage(MainModel.CurrentShopInfo.shopid, "2", ref ErrorMsg);
                }
                ErrorMsg = "";
                PosActivesSku tempactivessku1 = httputil.GetActiveSkus(MainModel.CurrentShopInfo.shopid, "1", ref ErrorMsg);
                if (tempactivessku1 != null && string.IsNullOrEmpty(ErrorMsg))
                {
                    MainModel.TVActivesSku1 = tempactivessku1;
                }

                ErrorMsg = "";
                PosActivesSku tempactivessku2 = httputil.GetActiveSkus(MainModel.CurrentShopInfo.shopid, "2", ref ErrorMsg);
                if (tempactivessku2 != null && string.IsNullOrEmpty(ErrorMsg))
                {
                    MainModel.TVActivesSku2 = tempactivessku2;
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("加载电视屏数据异常" + ex.Message);
            }
        }
        #endregion 
    }
}
