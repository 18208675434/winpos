using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MenuUI
{
    public partial class FormWebOrderDetail : Form
    {
        #region 成员变量

        public Order CurrentOrder = null;

        private WebOrderDetail CurrentOrderDetail = null;
        private ZhuiZhi_Integral_Scale_UncleFruit.Common.HttpUtil httputil = new Common.HttpUtil();

        private bool IsEnable = true;

        /// <summary>
        /// 数据处理委托方法
        /// </summary>
        /// <param name="type">0：返回  1：确认</param>
        public delegate void DataRecHandleDelegate(int result);
        /// <summary>
        /// 数据接收事件
        /// </summary>
        public event DataRecHandleDelegate DataReceiveHandle;

        #endregion

        #region 页面加载与退出
        public FormWebOrderDetail(Order order)
        {
            InitializeComponent();

            CurrentOrder = order;
        }

        private void FormWebOrderDetail_Shown(object sender, EventArgs e)
        {
            lblOrderStatus.Text = CurrentOrder.orderstatus;
            LoadBtnStatus();
            Application.DoEvents();
            LoadOrderInfo();

           
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            CloseForm();
        }
        private void CloseForm()
        {
            if (DataReceiveHandle != null)
                this.DataReceiveHandle.BeginInvoke(1, null, null);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }


        private void LoadOrderInfo(){
            try{
                string error = "";
                CurrentOrderDetail = httputil.GetWebOrderDetail(CurrentOrder.orderid, ref error);

                if (CurrentOrderDetail == null)
                {
                    MainModel.ShowLog(error,false);
                    
                    return;
                }

                dgvBaseInfo.Rows.Add("订单类型：" + CurrentOrderDetail.ordersubtype, "用户姓名：" + CurrentOrderDetail.customername, "下单用户：" + CurrentOrderDetail.registerphone);
                dgvBaseInfo.Rows.Add("订单号：" + CurrentOrderDetail.orderid, "下单时间：" + MainModel.GetDateTimeByStamp(CurrentOrderDetail.orderat.ToString()).ToString("yyyy-MM-dd HH:mm:ss"), "支付时间：" + MainModel.GetDateTimeByStamp(CurrentOrderDetail.payat.ToString()).ToString("yyyy-MM-dd HH:mm:ss"));


                dgvPayInfo.Rows.Add("订单金额(元)："+CurrentOrderDetail.orderamt.ToString("f2"), "商品金额(元)："+CurrentOrderDetail.productamt.ToString("f2"), "优惠金额(元)："+CurrentOrderDetail.promoamt.ToString("f2"));
                dgvPayInfo.Rows.Add("门店优惠(元)：" + CurrentOrderDetail.pshoppromoamt.ToString("f2"), "平台优惠(元)：" + CurrentOrderDetail.pplatformpromoamt.ToString("f2"), "抹零金额(元)：" + CurrentOrderDetail.givechangeamt.ToString("f2"));
                dgvPayInfo.Rows.Add("应收金额(元)：" + CurrentOrderDetail.payamtafterpromo.ToString("f2"), "现金支付(元)：" + CurrentOrderDetail.cashpayamt.ToString("f2"), "");


                LoadDgvOrderItems();

            }catch(Exception ex){

            }
        }


        private void LoadBtnStatus()
        {
            btnCancelOrder.Visible = CurrentOrder.orderstatusvalue==1;

            if(CurrentOrder.orderstatusvalue==5 || CurrentOrder.orderstatusvalue==1){
                btnReprint.BackColor=Color.FromArgb(220,220,220);
                btnRefund.BackColor=Color.FromArgb(220,220,220);
                btnRefundByAmt.BackColor =Color.FromArgb(220,220,220);
            }
            if (CurrentOrder.supportspecifiedamountrefund != 1)
            {
                btnRefundByAmt.BackColor = Color.FromArgb(220, 220, 220);
            }
        }
        #endregion

        #region 商品明细分页
        private int CurrentPage = 1;
        private void rbtnPageUp_ButtonClick(object sender, EventArgs e)
        {
            if (!rbtnPageUp.WhetherEnable || !IsEnable)
            {
                return;
            }
            CurrentPage--;
            LoadDgvOrderItems();
        }

        private void rbtnPageDown_ButtonClick(object sender, EventArgs e)
        {
            if (!IsEnable || !rbtnPageDown.WhetherEnable)
            {
                return;
            }
            CurrentPage++;
            LoadDgvOrderItems();
        }

        private void LoadDgvOrderItems()
        {
            try
            {

                rbtnPageUp.WhetherEnable = CurrentPage > 1;

                int startindex = (CurrentPage - 1) * 6;

                int lastindex = Math.Min(CurrentOrderDetail.orderitems.Count - 1, startindex + 5);

                dgvOrderItems.Rows.Clear();
                List<Orderitems> lstOrder = CurrentOrderDetail.orderitems.GetRange(startindex, lastindex - startindex + 1);



                foreach (Orderitems item in lstOrder)
                {
                    dgvOrderItems.Rows.Add(item.goodsname, item.bulk == 1 ? "散称" : "标品", item.listprice.ToString("f2"), item.saleprice.ToString("f2"), item.qty.ToString(), item.productamt.ToString("f2"));
                }

                rbtnPageDown.WhetherEnable = CurrentOrderDetail.orderitems.Count > CurrentPage * 6;

            }
            catch
            {

            }
        }

        #endregion


        #region  订单操作事件
        private void btnReprint_Click(object sender, EventArgs e)
        {
            if (!IsEnable || CurrentOrder.orderstatusvalue == 5)
            {
                return;
            }

            IsEnable = false;

            if (!ConfirmHelper.Confirm("确认重打小票？"))
            {
                IsEnable = true;
                return;
            }

            LoadingHelper.ShowLoadingScreen("加载中...");
            ReceiptUtil.EditReprintCount(1);

            string ErrorMsg = "";
            PrintDetail printdetail = httputil.GetPrintDetail(CurrentOrder.orderid, ref ErrorMsg);

            if (ErrorMsg != "" || printdetail == null)
            {
                LoadingHelper.CloseForm();
                MainModel.ShowLog(ErrorMsg, true);
            }
            else
            {
                //SEDPrint(printdetail);
                string PrintErrorMsg = "";
                bool printresult = PrintUtil.PrintOrder(printdetail, false, ref PrintErrorMsg); //PrintUtil.PrintOrder(printdetail, false, ref PrintErrorMsg);

                if (PrintErrorMsg != "" || !printresult)
                {
                    MainModel.ShowLog(PrintErrorMsg, true);
                }
                else
                {
                    MainModel.ShowLog("打印完成", false);
                }

            }
            IsEnable = true;
            CloseForm();

        }

        private void btnRefund_Click(object sender, EventArgs e)
        {
            if (!IsEnable || CurrentOrder.orderstatusvalue == 5)
            {
                return;
            }
            try
            {

                IsEnable = false;

                BackHelper.ShowFormBackGround();
                frmRefundSelect frmrefund = new frmRefundSelect(CurrentOrder);

                frmrefund.ShowDialog();

                BackHelper.HideFormBackGround();

                if (frmrefund.DialogResult == DialogResult.OK)
                {
                    string totalpay = MenuHelper.GetTotalPayInfo(frmrefund.Reforder);

                    if (!ConfirmHelper.Confirm("确认退款？", "应退 " + totalpay))
                    {
                        return;
                    }


                    string orderid = CurrentOrder.orderid;
                    string ErrorMsg = "";
                    string resultorderid = httputil.Refund(frmrefund.Refrefundpara, ref ErrorMsg);
                    if (ErrorMsg != "")
                    {
                        MainModel.ShowLog(ErrorMsg, true);
                    }
                    else
                    {

                        AbnormalOrderUtil.RefundOrderList(resultorderid, frmrefund.Refrefundpara);
                        PrintDetail printdetail = httputil.GetPrintDetail(resultorderid, ref ErrorMsg);

                        if (ErrorMsg != "" || printdetail == null)
                        {
                            LoadingHelper.CloseForm();
                            MainModel.ShowLog(ErrorMsg, true);
                        }
                        else
                        {
                            //SEDPrint(printdetail);
                            string PrintErrorMsg = "";
                            bool printresult = PrintUtil.PrintOrder(printdetail, true, ref PrintErrorMsg); //PrintUtil.PrintOrder(printdetail, false, ref PrintErrorMsg);

                            if (PrintErrorMsg != "" || !printresult)
                            {
                                MainModel.ShowLog(PrintErrorMsg, true);
                            }
                            else
                            {
                                // MainModel.ShowLog("打印完成", false);
                            }

                        }

                        MainModel.ShowLog("退款成功", false);
                        try { PrintUtil.OpenCashDrawerEx(); }
                        catch { }
                    }

                    CloseForm();

                }
                IsEnable = true;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsEnable = true;
            }
                    
        }

        private void btnRefundByAmt_Click(object sender, EventArgs e)
        {
            if (!IsEnable || CurrentOrder.supportspecifiedamountrefund!=1)
            {
                return;
            }

            if (MenuHelper.ShowFormRefundByAmt(CurrentOrder))
            {
                CloseForm();
            }

        }

        private void btnCancelOrder_Click(object sender, EventArgs e)
        {
            if (!IsEnable || CurrentOrder.orderstatusvalue != 1)
            {
                return;
            }

            if (!ConfirmHelper.Confirm("确认取消订单？"))
            {
                return;
            }

            string errormsg = "";
            if (httputil.CancleOrder(CurrentOrder.orderid, "", ref errormsg))
            {
                Delay.Start(300);
                IsEnable = true;
                LoadingHelper.CloseForm();
                MainModel.ShowLog("订单取消成功", false);
            }
            else
            {
                IsEnable = true;
                LoadingHelper.CloseForm();
                MainModel.ShowLog("订单取消失败", false);
            }
            CloseForm();
        }
        #endregion



    }
}
