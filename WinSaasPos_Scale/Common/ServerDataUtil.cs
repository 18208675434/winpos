
using Maticsoft.BLL;
using Maticsoft.Model;
using WinSaasPOS_Scale.Model;
using WinSaasPOS_Scale.Model.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinSaasPOS_Scale.Common
{

    public class ServerDataUtil
    {
        private HttpUtil httputil = new HttpUtil();
        private JSON_BEANBLL jsonbll = new JSON_BEANBLL();
        #region  拉去全部促销商品
        private DBPROMOTION_CACHE_BEANBLL promotionbll = new DBPROMOTION_CACHE_BEANBLL();
        List<DBPROMOTION_CACHE_BEANMODEL> lstAllPromotion = new List<DBPROMOTION_CACHE_BEANMODEL>();


        public void UpdatePromotion()
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
                            if (EnumPromotionType.PROMOTION_PRICE_ACTION.Equals(lstpromotion[i].PROMOACTION) || EnumPromotionType.PROMOTION_PRICE_DISCOUNT_ACTION.Equals(lstpromotion[i].PROMOACTION))
                            {
                                mlist.Add(lstpromotion[i]);
                            }
                        }
                        else if ("O".Equals(lstpromotion[i].PROMOTYPE))
                        {
                            mlist.Add(lstpromotion[i]);
                        }
                    }

                    if (mlist.Count > 0)
                    {
                        promotionbll.AddPromotion(mlist, MainModel.CurrentShopInfo.tenantid, MainModel.CurrentShopInfo.shopid, MainModel.URL);
                    }

                }

                UpdatePromotionFilter();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("加载促销商品异常" + ex.Message);
            }
        }
        public List<DBPROMOTION_CACHE_BEANMODEL> GetAllPromotion(int page, int size)
        {
            try
            {

                //string refstr = "";
                //List<DBPROMOTION_CACHE_BEANMODEL> lsttest = httputil.QueryPromotionAll(MainModel.CurrentShopInfo.shopid, 0, 50, ref refstr);

                //promotionbll.AddPromotion(lsttest, MainModel.URL);

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
    }
}
