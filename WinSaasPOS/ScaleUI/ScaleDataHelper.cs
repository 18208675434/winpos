using Maticsoft.BLL;
using Maticsoft.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinSaasPOS.Common;
using WinSaasPOS.Model;

namespace WinSaasPOS.ScaleUI
{
   public  class ScaleDataHelper
    {

        /// <summary>
        /// 接口访问类
        /// </summary>
      private static  HttpUtil httputil = new HttpUtil();

      /// <summary>
      /// 传秤数据操作类
      /// </summary>
       private static DBSCALE_KEY_BEANBLL scalebll = new DBSCALE_KEY_BEANBLL();

        //当天第一次进入更新全局商品
        public static void LoadScale()
        {
            try
            {
                string errormesg = "";

                DateTime starttime = DateTime.Now;
                int i = 0;
                lstScale.Clear();
                GetScale(1, 100);

                if (lstScale != null && lstScale.Count > 0)
                {
                    scalebll.AddScalse(lstScale, MainModel.URL, MainModel.CurrentShopInfo.shopid);
                }
                else
                {
                    LogManager.WriteLog("无传秤数据更新");
                }


                // LogManager.WriteLog("添加电子秤数据时间" + (DateTime.Now - starttime).TotalMilliseconds);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("更新电子秤数据异常" + ex.Message);
            }
        }

        //如果某页数据访问异常  重新访问一次，（控制次数仅一次，防止网络异常 死循环）
        private static int errorpage = -1;
        private static int errorsize = -1;
        private static List<DBSCALE_KEY_BEANMODEL> lstScale = new List<DBSCALE_KEY_BEANMODEL>();
        public static List<DBSCALE_KEY_BEANMODEL> GetScale(int page, int size)
        {
            try
            {

                string errormesg = "";
                ScaleForSaas scale = httputil.GetScaleForSaas(page, size, ref errormesg);

                if (!string.IsNullOrEmpty(errormesg) || scale == null)
                {
                    MainModel.LastScaleTimeStamp = MainModel.getStampByDateTime(DateTime.Now);
                    if (errorpage != page && errorsize != size)
                    {
                        errorpage = page;
                        errorsize = size;
                        LogManager.WriteLog("获取电子秤分页数据失败，进行第二次访问" + errormesg);
                        GetScale(page, size);
                    }
                    else
                    {
                        LogManager.WriteLog("获取电子秤信息异常" + errormesg);
                        return lstScale;
                    }
                }
                else
                {
                    MainModel.LastScaleTimeStamp = scale.requestedat;
                    if (scale.templist != null && scale.templist.rows != null)
                    {
                        lstScale.AddRange(scale.templist.rows);
                        if (scale.templist.rows.Count >= size)
                        {
                            GetScale(page + 1, size);
                        }
                    }
                }
                return lstScale;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ERROR", "获取电子秤信息异常" + ex.Message);
                return null;
            }
        }

        private static ScaleUtil scaleutil = new ScaleUtil();
        public static void SendScale()
        {
            try
            {
                List<string> lstScaleIP = scalebll.GetDiatinctByScaleIP(" CREATE_URL_IP ='" + MainModel.URL + "'");
                foreach (string scaleip in lstScaleIP)
                {
                    string errormsg = "";
                    scaleutil.SendScaleByScaleIp(scaleip, ref errormsg);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("自动传秤异常" + ex.Message);
            }
        }
    }
}
