using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Common
{
    public class ConfigUtil
    {
        #region MQTT 

        public static long GetLastAdjustPriceID(){
            try
            {
                string count = INIManager.GetIni("MQTT", "LastAdjustPriceID", MainModel.IniPath);

                if (string.IsNullOrEmpty(count))
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt64(count);
                }

            }
            catch
            {
                return 0;
            }
    }

        //{"adjustTypes":2,"shopId":"110001","tenantId":"0210000114"}//adjustTypes 2:调价
        //{"adjustTypes":1,"shopId":"110001","tenantId":"0210000114"}//adjustTypes 1：上下架
        /// <summary>
        /// 判断是否有调价 上下架 及商品修改
        /// </summary>
        /// <returns></returns>
        public static MqttChangeType GetAdjustPriceChanged()
        {
            
                string AdjustType = INIManager.GetIni("MQTT", "ChangeType", MainModel.IniPath);

                MqttChangeType result = MqttChangeType.None;
                switch (AdjustType)
                {
                    case "1": result = MqttChangeType.SkuUpOrDown; break;
                    case "2": result = MqttChangeType.AdjustPrice; break;
                    case "3": result = MqttChangeType.SkuInsert; break;
                    default: result = MqttChangeType.None; break;

                }

                INIManager.SetIni("MQTT", "ChangeType", "0",MainModel.IniPath);
                return result;
        }

        /// <summary>
        /// 获取最后一次调价时间  没有的话获取当天最早时间
        /// </summary>
        /// <returns></returns>
        public static string GetAdjustStartTime()
        {

            string starttime = INIManager.GetIni("MQTT", "AdjustStartTime",MainModel.IniPath);

            if (string.IsNullOrEmpty(starttime)) {

                return MainModel.getStampByDateTime(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00"));
            }
            else
            {
                return starttime;
            }
                
                
              
           
        }


        public static bool HaveNewOrder()
        {
            
            string neworder = INIManager.GetIni("MQTT", "NewOrder", MainModel.IniPath);

            if (!string.IsNullOrEmpty(neworder) && neworder == "1")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        #endregion
    }

    public enum MqttChangeType
    {

        None, //空
        AdjustPrice, //调价   2
        SkuUpOrDown,//商品上下架  1
        SkuInsert //增量商品  3
    }
}





