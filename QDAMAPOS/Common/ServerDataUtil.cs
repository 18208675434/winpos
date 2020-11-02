
using Maticsoft.BLL;
using Maticsoft.Model;
using QDAMAPOS.Model;
using QDAMAPOS.Model.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QDAMAPOS.Common
{

    public class ServerDataUtil
    {
        private HttpUtil httputil = new HttpUtil();

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
                        promotionbll.AddPromotion(mlist,MainModel.CurrentShopInfo.tenantid,MainModel.CurrentShopInfo.shopid,  MainModel.URL);
                    }
                    
                }
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
        #endregion
    }
}
