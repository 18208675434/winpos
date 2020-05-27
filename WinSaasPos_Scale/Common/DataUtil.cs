using Maticsoft.BLL;
using Maticsoft.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using WinSaasPOS_Scale.Model;

namespace WinSaasPOS_Scale.Common
{
    public class DataUtil
    {
        private static HttpUtil httputil = new HttpUtil();

        private static DBPRODUCT_BEANBLL productbll = new DBPRODUCT_BEANBLL();

        #region 全量商品
        //当天第一次进入更新全局商品
        public static void LoadAllProduct()
        {
            try
            {
                string errormesg = "";

                int i = 0;
                lstproduct.Clear();

                productbll.AddProduct(GetAllProdcut(1, 200), MainModel.URL);

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("更新全部商品异常" + ex.Message);
            }

        }

        private static  List<DBPRODUCT_BEANMODEL> lstproduct = new List<DBPRODUCT_BEANMODEL>();
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

        #region

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
                    MainModel.TvShowPage1 = httputil.GetTVshowpage(MainModel.CurrentShopInfo.shopid,"1",ref ErrorMsg);

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
