using Maticsoft.BLL;
using Maticsoft.Model;
using Newtonsoft.Json;
using QDAMAPOS.Common;
using QDAMAPOS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QDAMAPOS
{
    public partial class frmConfirmChange : Form
    {
        private HttpUtil httputil = new HttpUtil();
        /// <summary>
        /// 本地订单表操作类
        /// </summary>
        private DBORDER_BEANBLL orderbll = new DBORDER_BEANBLL();

        private DBRECEIPT_BEANBLL receiptbll = new DBRECEIPT_BEANBLL(); 

        public frmConfirmChange()
        {
            InitializeComponent();
            if (MainModel.IsOffLine)
            {
                lblConfim.Text = " 您确认要将收银机切换为在线模式吗？";
            }
            else
            {
                lblConfim.Text = " 您确认要将收银机切换为离线模式吗？";
            }
        }

        private void lbtnCancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void lbtnOK_Click(object sender, EventArgs e)
        {
            try
            {
                LoadingHelper.ShowLoadingScreen("交班中...");
                if (MainModel.IsOffLine)
                {
                    if (OffLineReceipt())
                    {
                        LoadingHelper.CloseForm();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    LoadingHelper.CloseForm();
                }
                else
                {
                    if (OnLineReceipt())
                    {
                        LoadingHelper.CloseForm();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    LoadingHelper.CloseForm();
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("交班异常"+ex.Message,true);
            }
        }


        private bool OnLineReceipt()
        {
            try
            {
                ReceiptPara receiptpara = new ReceiptPara();
                receiptpara.cancelordercount = Convert.ToInt16(INIManager.GetIni("Receipt", "CancelOrderCount", MainModel.IniPath));
                receiptpara.cancelordertotalmoney = INIManager.GetIni("Receipt", "CancelOrderTotalMoney", MainModel.IniPath);
                receiptpara.cancelsinglecount = Convert.ToInt16(INIManager.GetIni("Receipt", "CancelSingleCount", MainModel.IniPath));
                receiptpara.cancelsingletotalmoney = INIManager.GetIni("Receipt", "CancelSingleTotalMoney", MainModel.IniPath);

                receiptpara.openmoneypacketcount = Convert.ToInt16(INIManager.GetIni("Receipt", "OpenMoneyPacketCount", MainModel.IniPath));
                receiptpara.reprintcount = Convert.ToInt16(INIManager.GetIni("Receipt", "ReprintCount", MainModel.IniPath));
                receiptpara.endtime = MainModel.getStampByDateTime(DateTime.Now);
                receiptpara.shopid = MainModel.CurrentShopInfo.shopid;

                string ErrorMsg = "";
                Receiptdetail receipt = httputil.Receipt(receiptpara, ref ErrorMsg);


                if (ErrorMsg != "" || receipt == null) //商品不存在或异常
                {
                    MainModel.ShowLog("交班失败：" + ErrorMsg, true);
                    return false;
                }
                else
                {
                    string ErrorMsgReceipt = "";
                    bool receiptresult = PrintUtil.ReceiptPrint(receipt, ref ErrorMsgReceipt);

                    if (receiptresult)
                    { }
                    else
                    {
                        MainModel.ShowLog("交班打印失败：" + ErrorMsgReceipt, true);
                       
                    }
                    ReceiptUtil.ClearReceipt();

                    INIManager.SetIni("System", "POS-Authorization", "", MainModel.IniPath);
                    MainModel.Authorization = "";

                    FrmConfirmReceiptBack frmconfirmreceiptback = new FrmConfirmReceiptBack(receipt);
                    frmconfirmreceiptback.Location = new Point(0, 0);
                    frmconfirmreceiptback.ShowDialog();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("交班失败："+ex.Message,true);
                return false;
            }
        }


        private bool OffLineReceipt()
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

      

                decimal totaypayamt = 0;
                long starttime = Convert.ToInt64(MainModel.getStampByDateTime(Convert.ToDateTime(INIManager.GetIni("OffLine", "StartTime", MainModel.IniPath))));
                long endtime = Convert.ToInt64(MainModel.getStampByDateTime(DateTime.Now));

                string strwhere = " ORDERAT >" + starttime + " and ORDERAT < " + endtime;

                List<DBORDER_BEANMODEL> lstorderoffline = orderbll.GetModelList(strwhere);
                string custonNum = "0";
                if (lstorderoffline != null && lstorderoffline.Count > 0)
                {

                    foreach (DBORDER_BEANMODEL order in lstorderoffline)
                    {
                        totaypayamt += order.PRICETOTAL;
                    }
                    custonNum = lstorderoffline.Count.ToString();
                }

                Receiptdetail receiptdetail = new Receiptdetail();
                receiptdetail.shopid = MainModel.CurrentShopInfo.shopid;
                receiptdetail.saleclerkphone = MainModel.CurrentUser.loginaccount;
                receiptdetail.DeviceSN = MainModel.DeviceSN;
                receiptdetail.createurlip = MainModel.URL;

                receiptdetail.cashier = totaypayamt.ToString("f2");

                OrderPriceDetail serial = new OrderPriceDetail();
                try
                {
                    int serialnum = Convert.ToInt32(INIManager.GetIni("OffLine", "Serial", MainModel.IniPath));
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


                receiptdetail.devicecode = getOrderpriceDetail(MainModel.CurrentShopInfo.devicecode, "机", "");



                receiptdetail.shiftcode = getOrderpriceDetail("", "SHIFT账序", "");


                receiptdetail.title = getOrderpriceDetail("", "SHIFT账", "");

                List<OrderPriceDetail> lstorder = new List<OrderPriceDetail>();
                lstorder.Add(getOrderpriceDetail(custonNum, "来客数：", ""));
                lstorder.Add(getOrderpriceDetail(totaypayamt.ToString("f2"), "营业毛额：", ""));
                lstorder.Add(getOrderpriceDetail("0", "-M&M转让：", ""));
                lstorder.Add(getOrderpriceDetail(totaypayamt.ToString("f2"), "=净营业额", ""));
                lstorder.Add(getOrderpriceDetail("0", "+总代售：", "0"));
                lstorder.Add(getOrderpriceDetail("0", "+溢收：", "0"));
                lstorder.Add(getOrderpriceDetail("0", "-营业支出：", ""));
                lstorder.Add(getOrderpriceDetail("0", "+营业收入", ""));
                lstorder.Add(getOrderpriceDetail(totaypayamt.ToString("f2"), "应有总现金：", ""));
                receiptdetail.basicinfo = lstorder;

                receiptdetail.totalamount = getOrderpriceDetail(totaypayamt.ToString("f2"), "支付合计", "");


                List<OrderPriceDetail> lstbo = new List<OrderPriceDetail>();
                lstbo.Add(getOrderpriceDetail("", "退货：", "0"));
                lstbo.Add(getOrderpriceDetail(cancelsingletotalmoney, "指定取消：", cancelsinglecount));
                lstbo.Add(getOrderpriceDetail(cancelordertotalmoney, "整笔取消：", cancelordercount));
                lstbo.Add(getOrderpriceDetail(openmoneypacketcount, "开钱箱：", ""));
                lstbo.Add(getOrderpriceDetail(reprintcount, "小票重印：", ""));
                lstbo.Add(getOrderpriceDetail("0", "会员充值：", "0"));
                lstbo.Add(getOrderpriceDetail("0次", "零钱包：", "0"));
                lstbo.Add(getOrderpriceDetail("0次", "会员赠送积分：", "0"));
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

                receiptbll.Add(d);
                INIManager.SetIni("OffLine", "StartTime", "", MainModel.IniPath);

                string ErrorMsgReceipt = "";
                bool receiptresult = PrintUtil.ReceiptPrint(receiptdetail, ref ErrorMsgReceipt);

                if (receiptresult)
                { }
                else
                {
                    LogManager.WriteLog(ErrorMsgReceipt);
                  //  ShowLog(ErrorMsgReceipt, true);
                }
                ReceiptUtil.ClearReceipt();

                INIManager.SetIni("System", "POS-Authorization", "", MainModel.IniPath);
                INIManager.SetIni("OffLine", "LastLoginaccount", "", MainModel.IniPath);
                INIManager.SetIni("OffLine", "LastNickName", "", MainModel.IniPath);
                INIManager.SetIni("OffLine", "StartTime", "", MainModel.IniPath);

                FrmConfirmReceiptBack frmconfirmreceiptback = new FrmConfirmReceiptBack(receiptdetail);
                frmconfirmreceiptback.Location = new Point(0, 0);
                frmconfirmreceiptback.ShowDialog();
                return true;
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("交班失败：" + ex.Message, true);
                return false;
            }
        }

        private OrderPriceDetail getOrderpriceDetail(string amount, string title, string subtitle)
        {
            OrderPriceDetail orderprice = new OrderPriceDetail();
            orderprice.amount = amount;
            orderprice.title = title;
            orderprice.subtitle = subtitle;

            return orderprice;
        }

        private string GetOffLineReceiptID(Receiptdetail receiptdetail)
        {
            try
            {
                //订单号：取当时时间戳+设备SN+门店ID+登录离线店员手机号+该订单总价+现金支付价+找零价+抹分价+实际订单对象hashcode+订单对象hashcode，生成 后的订单hashcode+4位随机数,生成后的订单号去掉"-"为本次生成的离线订单号

                string strorder = MainModel.getStampByDateTime(DateTime.Now) + MainModel.DeviceSN + MainModel.CurrentUserPhone + receiptdetail.GetHashCode();
                return strorder.GetHashCode().ToString().Replace("-", "") + Getrandom(4);
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("获取离线订单号异常" + ex.Message, true);
            }
            return "";
        }
        private string Getrandom(int num)
        {
            Random rd = new Random();
            string result = "";
            for (int i = 0; i < num; i++)
            {
                result += rd.Next(10).ToString();
            }
            return result;
        }
    }
}
