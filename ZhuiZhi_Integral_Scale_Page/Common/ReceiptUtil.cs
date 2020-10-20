using ZhuiZhi_Integral_Scale_UncleFruit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Maticsoft.Model;
using Maticsoft.BLL;
using Newtonsoft.Json;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Common
{
    public class ReceiptUtil
    {
        #region 成员变量
        /// <summary>
        /// 本地订单表操作类
        /// </summary>
        private static DBORDER_BEANBLL orderbll = new DBORDER_BEANBLL();
        /// <summary>
        /// 本地交班记录表操作类
        /// </summary>
        private static DBRECEIPT_BEANBLL receiptbll = new DBRECEIPT_BEANBLL();
        #endregion

        #region 交班数据记录 config.ini
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
                INIManager.SetIni("Receipt", "CancelOrderTotalMoney", CancelOrderTotalMoney.ToString("f2"), MainModel.IniPath);
                return true;
            }
            catch (Exception ex)
            {

                INIManager.SetIni("Receipt", "CancelOrderCount", n.ToString(), MainModel.IniPath);
                INIManager.SetIni("Receipt", "CancelOrderTotalMoney", money.ToString("f2"), MainModel.IniPath);
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
                INIManager.SetIni("Receipt", "CancelSingleTotalMoney", CancelSingleTotalMoney.ToString("f2"), MainModel.IniPath);
                return true;
            }
            catch (Exception ex)
            {
                INIManager.SetIni("Receipt", "CancelSingleCount", n.ToString(), MainModel.IniPath);
                INIManager.SetIni("Receipt", "CancelSingleTotalMoney", money.ToString("f2"), MainModel.IniPath);
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
        public static bool EditRefund(int n, decimal money)
        {
            try
            {
                int CancelSingleCount = Convert.ToInt16(INIManager.GetIni("Receipt", "RefundCount", MainModel.IniPath)) + n;
                decimal CancelSingleTotalMoney = Convert.ToDecimal(INIManager.GetIni("Receipt", "RefundMoney", MainModel.IniPath)) + Convert.ToDecimal(money);

                INIManager.SetIni("Receipt", "RefundCount", CancelSingleCount.ToString(), MainModel.IniPath);
                INIManager.SetIni("Receipt", "RefundMoney", CancelSingleTotalMoney.ToString("f2"), MainModel.IniPath);
                return true;
            }
            catch (Exception ex)
            {
                INIManager.SetIni("Receipt", "RefundCount", n.ToString(), MainModel.IniPath);
                INIManager.SetIni("Receipt", "RefundMoney", money.ToString("f2"), MainModel.IniPath);
                return false;
            }
            //int  cancelsinglecount = Convert.ToInt16(INIManager.GetIni("Receipt", "CancelSingleCount", MainModel.IniPath));
            //receiptpara.cancelsingletotalmoney = INIManager.GetIni("Receipt", "CancelSingleTotalMoney", MainModel.IniPath);

            //receiptpara.openmoneypacketcount = Convert.ToInt16(INIManager.GetIni("Receipt", "OpenMoneyPacketCount", MainModel.IniPath));
            //receiptpara.reprintcount = Convert.ToInt16(INIManager.GetIni("Receipt", "ReprintCount", MainModel.IniPath));
        }


        /// <summary>
        /// 重打小票次数
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
        /// 开钱箱次数
        /// </summary>
        /// <param name="n">修改的值</param>
        /// <returns></returns>
        public static bool EditOpenMoneyPacketCount(int n)
        {
            try
            {
                AbnormalOrderUtil.OpenBoxList();
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

        public static bool SetPrettyCash(decimal prettycash)
        {
            try
            {

                INIManager.SetIni("Receipt", "PrettyCash", prettycash.ToString(), MainModel.IniPath);
                return true;
            }
            catch (Exception ex)
            {
                INIManager.SetIni("Receipt", "PrettyCash", "", MainModel.IniPath);
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
                INIManager.SetIni("Receipt", "RefundCount","0", MainModel.IniPath);
                INIManager.SetIni("Receipt", "RefundMoney", "0", MainModel.IniPath);
                INIManager.SetIni("Receipt", "PrettyCash", "", MainModel.IniPath);
                return true;
            }
            catch (Exception ex)
            {
               
                return false;
            }
        }

        #endregion

        #region  离线交班数据记录
        public static Receiptdetail GetReceiptDetailOffLine()
        {
            try
            {
                string cancelordercount = INIManager.GetIni("Receipt", "CancelOrderCount", MainModel.IniPath);
                string cancelordertotalmoney = INIManager.GetIni("Receipt", "CancelOrderTotalMoney", MainModel.IniPath);
                string cancelsinglecount = INIManager.GetIni("Receipt", "CancelSingleCount", MainModel.IniPath);
                string cancelsingletotalmoney = INIManager.GetIni("Receipt", "CancelSingleTotalMoney", MainModel.IniPath);

                string openmoneypacketcount = INIManager.GetIni("Receipt", "OpenMoneyPacketCount", MainModel.IniPath);
                string reprintcount = INIManager.GetIni("Receipt", "ReprintCount", MainModel.IniPath);
                string shopid = MainModel.CurrentShopInfo.shopid;


                int custonNum = 0; //来客数
                decimal totaypayamt = 0;//营业净额
                decimal origintotal = 0;//营业毛额
                decimal promototal = 0;//折让

                
                int refundNum = 0; //退款次数
                decimal refundtotaypayamt = 0;//退款金额



                long starttime = Convert.ToInt64(MainModel.getStampByDateTime(Convert.ToDateTime(INIManager.GetIni("OffLine", "StartTime", MainModel.IniPath))));
                long endtime = Convert.ToInt64(MainModel.getStampByDateTime(DateTime.Now));


                //计算 营业额  需要除去退款
                string strwhere = " ORDERAT >" + starttime + " and ORDERAT < " + endtime;

                List<DBORDER_BEANMODEL> lstorderoffline = orderbll.GetModelList(strwhere);
               
                if (lstorderoffline != null && lstorderoffline.Count > 0)
                {

                    foreach (DBORDER_BEANMODEL order in lstorderoffline)
                    {
                        OffLineOrder offlineorder = JsonConvert.DeserializeObject<OffLineOrder>(order.ORDER_JSON);
                        origintotal += offlineorder.origintotal;
                        totaypayamt += offlineorder.pricetotal;
                        promototal += (offlineorder.origintotal - offlineorder.pricetotal);
                    }
                    custonNum = lstorderoffline.Count;
                }

                string strwhererefund = " REFUND_TIME >" + starttime + " and REFUND_TIME < " + endtime;

                List<DBORDER_BEANMODEL> lstorderofflinerefund = orderbll.GetModelList(strwhererefund);
                if (lstorderofflinerefund != null && lstorderofflinerefund.Count > 0)
                {

                    foreach (DBORDER_BEANMODEL order in lstorderofflinerefund)
                    {
                        OffLineOrder offlineorder = JsonConvert.DeserializeObject<OffLineOrder>(order.ORDER_JSON);
                        origintotal -= offlineorder.origintotal;
                        totaypayamt -= offlineorder.pricetotal;
                        promototal -= (offlineorder.origintotal - offlineorder.pricetotal);


                        refundtotaypayamt += offlineorder.pricetotal;
                    }
                   // custonNum -= lstorderofflinerefund.Count;
                    refundNum = lstorderofflinerefund.Count;
                }



                Receiptdetail receiptdetail = new Receiptdetail();
                receiptdetail.shopid = MainModel.CurrentShopInfo.shopid;
                receiptdetail.saleclerkphone = MainModel.CurrentUser.loginaccount;
                receiptdetail.DeviceSN = MainModel.DeviceSN;
                receiptdetail.createurlip = MainModel.URL;

                receiptdetail.cashier = MainModel.CurrentUser.nickname;

                OrderPriceDetail serial = new OrderPriceDetail();
                int serialnum = 0;
                try
                {
                    serialnum = Convert.ToInt32(INIManager.GetIni("OffLine", "Serial", MainModel.IniPath));
                    serialnum += 1;
                    INIManager.SetIni("OffLine", "Serial", serialnum.ToString(), MainModel.IniPath);
                    serial.amount = serialnum.ToString();
                }
                catch
                {
                    INIManager.SetIni("OffLine", "Serial", "1", MainModel.IniPath);
                    serial.amount = "1";
                }
                serial.title = "序";
                receiptdetail.serial = serial;


                receiptdetail.devicecode = getOrderpriceDetail(MainModel.CurrentShopInfo.deviceid + "", "机", "");



                receiptdetail.shiftcode = getOrderpriceDetail(serialnum.ToString(), "SHIFT账序", "");


                receiptdetail.title = getOrderpriceDetail("", "SHIFT账", "");
                receiptdetail.starttime = starttime;
                receiptdetail.endtime = endtime;

                List<OrderPriceDetail> lstorder = new List<OrderPriceDetail>();

                lstorder.Add(getOrderpriceDetail((serialnum - 1).ToString(), "开始序号", ""));
                lstorder.Add(getOrderpriceDetail(serialnum.ToString(), "结束序号", ""));

                lstorder.Add(getOrderpriceDetail(custonNum.ToString(), "来客数", ""));
                lstorder.Add(getOrderpriceDetail(origintotal.ToString("f2"), "营业毛额", ""));
                lstorder.Add(getOrderpriceDetail(promototal.ToString("f2"), "-M&M折让", ""));
                lstorder.Add(getOrderpriceDetail(totaypayamt.ToString("f2"), "=营业净额", ""));
                lstorder.Add(getOrderpriceDetail("0.00", "+总代售", "0"));
                lstorder.Add(getOrderpriceDetail("0.00", "+溢收", ""));
                lstorder.Add(getOrderpriceDetail("0.00", "-营业支出", ""));
                lstorder.Add(getOrderpriceDetail("0.00", "+营业收入", ""));
                lstorder.Add(getOrderpriceDetail(totaypayamt.ToString("f2"), "应有总现金", ""));
                receiptdetail.basicinfo = lstorder;

                receiptdetail.totalamount = getOrderpriceDetail(totaypayamt.ToString("f2"), "支付合计", "");

                receiptdetail.incomedetails = new List<OrderPriceDetail>();
                receiptdetail.incomedetails.Add(getOrderpriceDetail(totaypayamt.ToString("f2"), "现金", custonNum + "次"));

                List<OrderPriceDetail> lstbo = new List<OrderPriceDetail>();
                lstbo.Add(getOrderpriceDetail(refundtotaypayamt.ToString("f2"), "退货", refundNum+"次"));
                lstbo.Add(getOrderpriceDetail(cancelsingletotalmoney, "指定取消", cancelsinglecount + "次"));
                lstbo.Add(getOrderpriceDetail(cancelordertotalmoney, "整笔取消", cancelordercount + "次"));
                lstbo.Add(getOrderpriceDetail(openmoneypacketcount + "次", "开钱箱", ""));
                lstbo.Add(getOrderpriceDetail(reprintcount + "次", "小票重印", ""));
                lstbo.Add(getOrderpriceDetail("0.00", "会员充值", "0次"));
                lstbo.Add(getOrderpriceDetail("0.00", "零钱包", "0次"));
                lstbo.Add(getOrderpriceDetail("0", "会员赠送积分", "0" + "次"));
                receiptdetail.bottomdetails = lstbo;

                DBRECEIPT_BEANMODEL d = new DBRECEIPT_BEANMODEL();
                d.CASHIER = MainModel.CurrentUser.nickname;
                d.CASHTOTALAMT = totaypayamt;
                d.ENDTIME = endtime;
                d.NETSALEAMT = totaypayamt;
                d.OPERATETIMESTR = DateTime.Now.ToString("yyyy-MM-dd");
                d.RECEIPTDETAIL = JsonConvert.SerializeObject(receiptdetail);
                d.STARTTIME = starttime;
                d.TOTALPAYMENT = totaypayamt;
                d.OFFLINE_RECEIPT_ID = GetOffLineReceiptID(receiptdetail);
                d.CREATE_TIME = endtime;
                d.CREATE_SN = MainModel.DeviceSN;
                d.CREATE_URL_IP = MainModel.URL;

                receiptbll.Add(d);
                INIManager.SetIni("OffLine", "StartTime", "", MainModel.IniPath);

                string ErrorMsgReceipt = "";
                bool receiptresult = PrintUtil.ReceiptPrint(receiptdetail, ref ErrorMsgReceipt);

                if (receiptresult)
                { }
                else
                {
                   
                }
                ReceiptUtil.ClearReceipt();

                INIManager.SetIni("System", "POS-Authorization", "", MainModel.IniPath);
                INIManager.SetIni("OffLine", "LastLoginaccount", "", MainModel.IniPath);
                INIManager.SetIni("OffLine", "LastNickName", "", MainModel.IniPath);
                INIManager.SetIni("OffLine", "StartTime", "", MainModel.IniPath);

                return receiptdetail;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("获取离线交班详情异常"+ex.Message);
                return null;
            }
        }


        private static OrderPriceDetail getOrderpriceDetail(string amount, string title, string subtitle)
        {
            OrderPriceDetail orderprice = new OrderPriceDetail();
            orderprice.amount = amount;
            orderprice.title = title;
            orderprice.subtitle = subtitle;

            return orderprice;
        }

        private static string GetOffLineReceiptID(Receiptdetail receiptdetail)
        {
            try
            {
                //订单号：取当时时间戳+设备SN+门店ID+登录离线店员手机号+该订单总价+现金支付价+找零价+抹分价+实际订单对象hashcode+订单对象hashcode，生成 后的订单hashcode+4位随机数,生成后的订单号去掉"-"为本次生成的离线订单号

                string strorder = MainModel.getStampByDateTime(DateTime.Now) + MainModel.DeviceSN + MainModel.CurrentUserPhone + receiptdetail.GetHashCode();
                return strorder.GetHashCode().ToString().Replace("-", "") + Getrandom(4);
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("获取离线交班号异常" + ex.Message, true);
            }
            return "";
        }

        private static string Getrandom(int num)
        {
            Random rd = new Random();
            string result = "";
            for (int i = 0; i < num; i++)
            {
                result += rd.Next(10).ToString();
            }
            return result;
        }

        #endregion
    }
}
