using QiandamaPOS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QiandamaPOS.Common
{
    public class ReceiptUtil
    {
        /// <summary>
        /// 整笔订单取消修改
        /// </summary>
        /// <param name="n">修改的值</param>
        /// <returns></returns>
        public static bool EditCancelOrder(int n,decimal money)
        {
            try
            {
                
                int CancelOrderCount = Convert.ToInt16(INIManager.GetIni("Receipt", "CancelOrderCount", MainModel.IniPath)) + n;
                decimal CancelOrderTotalMoney = Convert.ToDecimal(INIManager.GetIni("Receipt", "CancelOrderTotalMoney", MainModel.IniPath)) + Convert.ToDecimal(money);

                INIManager.SetIni("Receipt", "CancelOrderCount", CancelOrderCount.ToString(), MainModel.IniPath);
                INIManager.SetIni("Receipt", "CancelOrderTotalMoney", CancelOrderTotalMoney.ToString(), MainModel.IniPath);
                return true;
            }
            catch (Exception ex)
            {

                INIManager.SetIni("Receipt", "CancelOrderCount", n.ToString(), MainModel.IniPath);
                INIManager.SetIni("Receipt", "CancelOrderTotalMoney", money.ToString(), MainModel.IniPath);
                return false;
            }           
        }

        /// <summary>
        /// 指定取消修改
        /// </summary>
        /// <param name="n">修改的值</param>
        /// <returns></returns>
        public static bool EditCancelSingle(int n, decimal money)
        {
            try
            {
                int CancelSingleCount = Convert.ToInt16(INIManager.GetIni("Receipt", "CancelSingleCount", MainModel.IniPath)) + n;
                decimal CancelSingleTotalMoney = Convert.ToDecimal(INIManager.GetIni("Receipt", "CancelSingleTotalMoney", MainModel.IniPath)) + Convert.ToDecimal(money);

                INIManager.SetIni("Receipt", "CancelSingleCount", CancelSingleCount.ToString(), MainModel.IniPath);
                INIManager.SetIni("Receipt", "CancelSingleTotalMoney", CancelSingleTotalMoney.ToString(), MainModel.IniPath);
                return true;
            }
            catch (Exception ex)
            {
                INIManager.SetIni("Receipt", "CancelSingleCount", n.ToString(), MainModel.IniPath);
                INIManager.SetIni("Receipt", "CancelSingleTotalMoney", money.ToString(), MainModel.IniPath);
                return false;
            }
            //int  cancelsinglecount = Convert.ToInt16(INIManager.GetIni("Receipt", "CancelSingleCount", MainModel.IniPath));
            //receiptpara.cancelsingletotalmoney = INIManager.GetIni("Receipt", "CancelSingleTotalMoney", MainModel.IniPath);

            //receiptpara.openmoneypacketcount = Convert.ToInt16(INIManager.GetIni("Receipt", "OpenMoneyPacketCount", MainModel.IniPath));
            //receiptpara.reprintcount = Convert.ToInt16(INIManager.GetIni("Receipt", "ReprintCount", MainModel.IniPath));
        }


        /// <summary>
        /// 指定取消修改
        /// </summary>
        /// <param name="n">修改的值</param>
        /// <returns></returns>
        public static bool EditReprintCount(int n)
        {
            try
            {
                int ReprintCount = Convert.ToInt16(INIManager.GetIni("Receipt", "ReprintCount", MainModel.IniPath)) + n;

                INIManager.SetIni("Receipt", "ReprintCount", ReprintCount.ToString(), MainModel.IniPath);
                return true;
            }
            catch (Exception ex)
            {
                INIManager.SetIni("Receipt", "ReprintCount", n.ToString(), MainModel.IniPath);
                return false;
            }
           
        }

        /// <summary>
        /// 指定取消修改
        /// </summary>
        /// <param name="n">修改的值</param>
        /// <returns></returns>
        public static bool EditOpenMoneyPacketCount(int n)
        {
            try
            {
                int OpenMoneyPacketCount = Convert.ToInt16(INIManager.GetIni("Receipt", "OpenMoneyPacketCount", MainModel.IniPath)) + n;

                INIManager.SetIni("Receipt", "OpenMoneyPacketCount", OpenMoneyPacketCount.ToString(), MainModel.IniPath);
                return true;
            }
            catch (Exception ex)
            {
                INIManager.SetIni("Receipt", "OpenMoneyPacketCount", n.ToString(), MainModel.IniPath);
                return false;
            }
        }

        public static bool ClearReceipt()
        {
            try
            {
                INIManager.SetIni("Receipt", "CancelOrderCount", "0", MainModel.IniPath);
                INIManager.SetIni("Receipt", "CancelOrderTotalMoney", "0", MainModel.IniPath);
                INIManager.SetIni("Receipt", "CancelSingleCount", "0", MainModel.IniPath);
                INIManager.SetIni("Receipt", "CancelSingleTotalMoney", "0", MainModel.IniPath);
                INIManager.SetIni("Receipt", "OpenMoneyPacketCount", "0", MainModel.IniPath);
                INIManager.SetIni("Receipt", "ReprintCount", "0", MainModel.IniPath);

                return true;
            }
            catch (Exception ex)
            {
               
                return false;
            }
        }
    }
}
