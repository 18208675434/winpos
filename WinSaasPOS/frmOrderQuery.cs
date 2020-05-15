using Maticsoft.BLL;
using Maticsoft.Model;
using Newtonsoft.Json;
using WinSaasPOS.Common;
using WinSaasPOS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading;
using System.Windows.Forms;

namespace WinSaasPOS
{
    public partial class frmOrderQuery : Form
    {
        HttpUtil httputil = new HttpUtil();


        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();

        //间隔天数
        private int CurrentInterval = 0;


        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();

        private QueryOrder CurrentQueryOrder = null;


        private string LastOrderid = "0";


        private Bitmap bmpReprint;
        private Bitmap bmpRefund;
        private Bitmap bmpNotRefund;
        private Bitmap bmpWhite;

        /// <summary>
        /// this.enable=false; 页面不可用页面不可控；  通过该标志控制页面是否可用
        /// </summary>
        private bool IsEnable = true;

        /// <summary>
        /// 本地订单表操作类
        /// </summary>
        private DBORDER_BEANBLL orderbll = new DBORDER_BEANBLL();

        private bool thisisoffline = false;

        public frmOrderQuery()
        {
            InitializeComponent();
           
          
            // Application.EnableVisualStyles();
        }
        private void frmOrderQuery_Load(object sender, EventArgs e)
        {

            btnMenu.Text = MainModel.CurrentUser.nickname + "，你好   ";
            btnMenu.Left = Math.Max(pnlHead.Width - btnMenu.Width-10, btnCancle.Left + btnCancle.Width);
            lblShopName.Text = MainModel.CurrentShopInfo.shopname;
            btnOnLineType.Left = lblShopName.Left + lblShopName.Width + 10;
            if (MainModel.IsOffLine)
            {
                btnOnLineType.BackgroundImage = Resources.ResourcePos.OffLineType; btnOnLineType.Text = "   离线";
            }
            else
            {
                btnOnLineType.BackgroundImage = Resources.ResourcePos.OnLineType; btnOnLineType.Text = "   在线";
            }
            lblTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            timerNow.Interval = 1000;
            timerNow.Enabled = true;
            Application.DoEvents();

            if (MainModel.IsOffLine)
            {
                lblPhone.Visible = false;
                txtPhone.Visible = false;
                lblPhoneShuiyin.Visible = false;
                lblOrderID.Location = lblPhone.Location;
                txtOrderID.Location = txtPhone.Location;
                lblOrderIDShuiyin.Location = lblPhoneShuiyin.Location;

                pnlDgvOffLineHead.Top = btnQueryOffLine.Top;
                dgvOrderOffLine.Top = pnlDgvOffLineHead.Top + pnlDgvOffLineHead.Height + 2;
                dgvOrderOffLine.Height = this.Height - pnlDgvOffLineHead.Top - pnlDgvOffLineHead.Height;
                btnQueryOffLine.PerformClick();
            }

        }
        private void frmOrderQuery_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            LoadBmp();

            btnToday_Click(null, null);
        }

        private void LoadBmp()
        {
            try
            {
                bmpRefund = (Bitmap)MainModel.GetControlImage(btnRefundPic);
                bmpReprint = (Bitmap)MainModel.GetControlImage(btnReprintPic);
                bmpNotRefund = (Bitmap)MainModel.GetControlImage(btnNotRefundPic);


                //int height = dgvOrderOnLine.RowTemplate.Height * 55 / 100;
                //bmpRefund = new Bitmap(Resources.ResourcePos.Refund, dgvOrderOnLine.Columns["cancle"].Width * 80 / 100, height);

                //bmpReprint = new Bitmap(Resources.ResourcePos.Reprint, dgvOrderOnLine.Columns["reprint"].Width * 80 / 100, height);
                bmpWhite = Resources.ResourcePos.White;

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("加载页面图片异常" + ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (!IsEnable)
            {
                return;
            }
            this.Close();
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            try
            {

                if (!IsEnable)
                {
                    return;
                }

                IsEnable = false;
                btnToday.FlatAppearance.BorderColor = Color.Red;
                btnYesterday.FlatAppearance.BorderColor = Color.Gray;
                btnWeek.FlatAppearance.BorderColor = Color.Gray;
                dtStart.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dtEnd.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                CurrentInterval = 0;
                LastOrderid = "0";
                dgvOrderOnLine.Rows.Clear();
                QueryOrder();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("查询当天订单信息异常：" + ex.Message);
            }
            finally
            {
                IsEnable = true;
            }
        }

        private void btnYesterday_Click(object sender, EventArgs e)
        {

            try
            {

                if (!IsEnable)
                {
                    return;
                }

                IsEnable = false;
                btnToday.FlatAppearance.BorderColor = Color.Gray;
                btnYesterday.FlatAppearance.BorderColor = Color.Red;
                btnWeek.FlatAppearance.BorderColor = Color.Gray;

                dtStart.Value = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 00:00:00");
                dtEnd.Value = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 23:59:59");
                CurrentInterval = 1;
                LastOrderid = "0";
                dgvOrderOnLine.Rows.Clear();

                QueryOrder();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("查询昨天订单信息异常：" + ex.Message);
            }
            finally
            {
                IsEnable = true;
            }
        }

        private void btnWeek_Click(object sender, EventArgs e)
        {
            try
            {

                if (!IsEnable)
                {
                    return;
                }

                IsEnable = false;
                btnToday.FlatAppearance.BorderColor = Color.Gray;
                btnYesterday.FlatAppearance.BorderColor = Color.Gray;
                btnWeek.FlatAppearance.BorderColor = Color.Red;

                dtStart.Value = Convert.ToDateTime(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + " 00:00:00");
                dtEnd.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                CurrentInterval = 7;
                LastOrderid = "0";
                dgvOrderOnLine.Rows.Clear();
                QueryOrder();

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("查询最近一周订单信息异常：" + ex.Message);
            }
            finally
            {
                IsEnable = true;
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (!IsEnable)
            {
                return;
            }

            this.Invoke(new InvokeHandler(delegate()
               {
                   LastOrderid = "0";
                   dgvOrderOnLine.Rows.Clear();
                   QueryOrder();
               }));
        }


        private bool HaveNextPage = true;

        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="interval"></param>
        private void QueryOrder()
        {
            try
            {
                string phone = txtPhone.Text;
                string orderid = txtOrderID.Text;

                if (phone.Length != 0 && phone.Length != 11)
                {
                    MainModel.ShowLog("手机号格式不正确！", false);
                    LoadingHelper.CloseForm();//关闭
                    return;
                }

                if ( thisisoffline)
                {
                    long endtime =Convert.ToInt64(getStampByDateTime(dtEnd.Value));
                    long starttime =Convert.ToInt64( getStampByDateTime(dtStart.Value));

                    string strwhere = " ORDERAT >" + starttime + " and ORDERAT < "+endtime +" and CREATE_URL_IP='" + MainModel.URL+"' ";
                    if (!string.IsNullOrEmpty(phone))
                    {
                        strwhere += " and CUSTOMERPHONE = '"+phone+"' ";

                    }
                    if (!string.IsNullOrEmpty(orderid))
                    {
                        strwhere += " and OFFLINEORDERID = '" + orderid + "' ";
                    }
                    strwhere += " order by CREATE_TIME desc";
                    List<DBORDER_BEANMODEL> lstorderoffline =  orderbll.GetModelList(strwhere);


                    dgvOrderOffLine.Rows.Clear();
                    if (lstorderoffline != null && lstorderoffline.Count > 0)
                    {
                        foreach (DBORDER_BEANMODEL order in lstorderoffline)
                        {

                            if (order.ORDERSTATUSVALUE == 5)//已退款
                            {
                                if (order.SYN_TIME == null || order.SYN_TIME == 0)
                                {
                                    dgvOrderOffLine.Rows.Add(GetDateTimeByStamp(order.ORDERAT.ToString()).ToString("yyyy-MM-dd HH:mm:ss"), order.OFFLINEORDERID, order.CUSTOMERPHONE, order.TITLE, "现金："+order.PRICETOTAL.ToString("f2"), order.ORDERSTATUS,"未同步", bmpWhite, bmpWhite);

                                }
                                else
                                {
                                    dgvOrderOffLine.Rows.Add(GetDateTimeByStamp(order.ORDERAT.ToString()).ToString("yyyy-MM-dd HH:mm:ss"), order.OFFLINEORDERID, order.CUSTOMERPHONE, order.TITLE, "现金：" + order.PRICETOTAL.ToString("f2"), order.ORDERSTATUS, "已同步", bmpWhite, bmpWhite);
                                }
                            }
                            else
                            {
                                if (order.SYN_TIME == null || order.SYN_TIME == 0)
                                {
                                    if (MainModel.IsOffLine)
                                    {
                                        dgvOrderOffLine.Rows.Add(GetDateTimeByStamp(order.ORDERAT.ToString()).ToString("yyyy-MM-dd HH:mm:ss"), order.OFFLINEORDERID, order.CUSTOMERPHONE, order.TITLE, "现金：" + order.PRICETOTAL.ToString("f2"), order.ORDERSTATUS, "未同步", bmpReprint, bmpRefund);

                                    }
                                    else
                                    {
                                        dgvOrderOffLine.Rows.Add(GetDateTimeByStamp(order.ORDERAT.ToString()).ToString("yyyy-MM-dd HH:mm:ss"), order.OFFLINEORDERID, order.CUSTOMERPHONE, order.TITLE, "现金：" + order.PRICETOTAL.ToString("f2"), order.ORDERSTATUS, "未同步", bmpReprint, bmpNotRefund);

                                    }

                                }
                                else
                                {
                                    dgvOrderOffLine.Rows.Add(GetDateTimeByStamp(order.ORDERAT.ToString()).ToString("yyyy-MM-dd HH:mm:ss"), order.OFFLINEORDERID, order.CUSTOMERPHONE, order.TITLE, "现金：" + order.PRICETOTAL.ToString("f2"), order.ORDERSTATUS, "已同步", bmpReprint, bmpRefund);

                                }

                            }
                        }
                    }


                    if (dgvOrderOffLine.Rows.Count > 0)
                    {
                        pnlEmptyOrder.Visible = false;

                        //  MainModel.ShowLog("刷新完成", false);
                    }
                    else
                    {
                        pnlEmptyOrder.Visible = true;
                        //  MainModel.ShowLog("暂无数据", false);
                    }
                }
                if(!thisisoffline && !MainModel.IsOffLine)
                {
                    LoadingHelper.ShowLoadingScreen();//显示

                QueryOrderPara queryorderpara = new QueryOrderPara();
                queryorderpara.customerphone = txtPhone.Text;
                queryorderpara.interval = CurrentInterval;
                queryorderpara.shopid = MainModel.CurrentShopInfo.shopid;
                queryorderpara.orderatend = getStampByDateTime(dtEnd.Value);
                queryorderpara.orderatstart = getStampByDateTime(dtStart.Value);
                queryorderpara.orderid = txtOrderID.Text;
                queryorderpara.lastorderid = LastOrderid;

                string ErrorMsg = "";
                QueryOrder queryorder = httputil.QueryOrderInfo(queryorderpara, ref ErrorMsg);

                //CurrentQueryOrder = queryorder;
            

                if (ErrorMsg != "" || queryorder == null)
                {
                     MainModel.ShowLog(ErrorMsg, false);
                }
                else
                {

                    if (LastOrderid == "0")
                    {
                        CurrentQueryOrder = queryorder;
                    }
                    else
                    {
                        foreach (Order order in queryorder.orders)
                        {
                            CurrentQueryOrder.orders.Add(order);
                        }
                    }

                    HaveNextPage = queryorder.hasnextpage == 1 ? true : false;

                    foreach (Order order in queryorder.orders)
                    {
                        string AliPayAmt = order.alipayamt;
                        string BalanceAmt = order.balanceamt;
                        string CashPayAmt = order.cashpayamt;
                        string WechatPayAmt = order.wechatpayamt;
                        string YLPayAmt = order.ylpayamt;
                        string PointPayAmt = order.pointpayamt;
                        string CashCouponAmt = order.cashcouponamt;

                        string totalpay = "";
                        if (!string.IsNullOrEmpty(AliPayAmt))
                        {
                            totalpay += "支付宝：" + AliPayAmt + " ";
                        }
                        if (!string.IsNullOrEmpty(BalanceAmt))
                        {
                            totalpay += "余额：" + BalanceAmt + " ";
                        }
                        if (!string.IsNullOrEmpty(CashPayAmt))
                        {
                            totalpay += "现金：" + CashPayAmt + " ";
                        }
                        if (!string.IsNullOrEmpty(WechatPayAmt))
                        {
                            totalpay += "微信：" + WechatPayAmt + " ";
                        }
                        if (!string.IsNullOrEmpty(YLPayAmt))
                        {
                            totalpay += "银联：" + YLPayAmt + " ";
                        }

                        if (!string.IsNullOrEmpty(PointPayAmt))
                        {
                            totalpay += "积分：" + PointPayAmt + " ";
                        }

                        if (!string.IsNullOrEmpty(CashCouponAmt))
                        {
                            totalpay += "代金券：" + CashCouponAmt + " ";
                        }

                        if (order.orderstatusvalue == 5)
                        {
                            dgvOrderOnLine.Rows.Add(GetDateTimeByStamp(order.orderat.ToString()).ToString("yyyy-MM-dd HH:mm:ss"), order.orderid, order.customerphone, order.title, totalpay, order.orderstatus, bmpWhite, bmpWhite);
                        }
                        else
                        {
                            dgvOrderOnLine.Rows.Add(GetDateTimeByStamp(order.orderat.ToString()).ToString("yyyy-MM-dd HH:mm:ss"), order.orderid, order.customerphone, order.title, totalpay, order.orderstatus, bmpReprint, bmpRefund);
                        }

                    }
                    dgvOrderOnLine.ClearSelection();
                    Application.DoEvents();


                    if (dgvOrderOnLine.Rows.Count > 0)
                    {
                        pnlEmptyOrder.Visible = false;

                        //  MainModel.ShowLog("刷新完成", false);
                    }
                    else
                    {
                        pnlEmptyOrder.Visible = true;
                        //  MainModel.ShowLog("暂无数据", false);
                    }
                }
                LoadingHelper.CloseForm();//关闭

                //Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                LoadingHelper.CloseForm();//关闭
                 MainModel.ShowLog("查询订单异常：" + ex.Message, true);
            }
            finally
            {
                LoadingHelper.CloseForm();//关闭
            }
        }


        //当前时间戳
        private string getStampByDateTime(DateTime datetime)
        {

            //DateTime datetime = DateTime.Now;
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            var result = (long)(datetime - startTime).TotalMilliseconds;

            return result.ToString();
        }

        private DateTime GetDateTimeByStamp(string stamp)
        {
            try
            {
                long result = Convert.ToInt64(stamp);
                DateTime datetime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                datetime = datetime.AddMilliseconds(result);
                return datetime;
            }
            catch (Exception ex)
            {
                return DateTime.Now;
            }
        }



        private void dgvOrderOnLine_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }

                if (e.RowIndex < 0)
                    return;
                //if (dgvOrderOnLine.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == bmpRefund)
                //{
                //    string selectorderid = dgvOrderOnLine.Rows[e.RowIndex].Cells["orderid"].Value.ToString();
                //    this.Enabled = false;


                //    // Order SelectOrder =(Order) CurrentQueryOrder.orders.Where(r => r.orderid == selectorderid).ToList()[0];
                //    Order SelectOrder = null;
                //    for (int i = 0; i < CurrentQueryOrder.orders.Count; i++)
                //    {
                //        if (CurrentQueryOrder.orders[i].orderid == selectorderid)
                //        {
                //            SelectOrder = CurrentQueryOrder.orders[i];
                //            break;
                //        }
                //    }

                //    if (SelectOrder == null)
                //    {
                //         MainModel.ShowLog("订单不存在，请刷新", false);
                //        this.Enabled = true;
                //        return;
                //    }

                //    string orderid = dgvOrderOnLine.Rows[e.RowIndex].Cells["orderid"].Value.ToString();

                //    string strinfo = dgvOrderOnLine.Rows[e.RowIndex].Cells["paytype"].Value.ToString();
                //    frmDeleteGood frmdelete = new frmDeleteGood("是否确认退款？", "应退" + strinfo, "");
                //    if (frmdelete.ShowDialog() != DialogResult.OK)
                //    {
                //        this.Enabled = true;
                //        return;
                //    }

                //    string ErrorMsg = "";

                //    RefundPara refundpara = new RefundPara();
                //    refundpara.aftersaletype = 2;
                //    refundpara.orderid = Convert.ToInt64(orderid);
                //    refundpara.allorder = true;


                //    string resultorderid = httputil.Refund(refundpara, ref ErrorMsg);
                //    if (ErrorMsg != "")
                //    {
                //         MainModel.ShowLog(ErrorMsg, true);
                //    }
                //    else
                //    {

                //         MainModel.ShowLog("退款成功", false);

                //    }

                //    this.Enabled = true;
                //    btnQuery_Click(null, null);
                //}

                if (dgvOrderOnLine.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == bmpRefund)
                {

                    string selectorderid = dgvOrderOnLine.Rows[e.RowIndex].Cells["orderid"].Value.ToString();
                    IsEnable = false;

                    // Order SelectOrder =(Order) CurrentQueryOrder.orders.Where(r => r.orderid == selectorderid).ToList()[0];
                    Order SelectOrder = null;
                    for (int i = 0; i < CurrentQueryOrder.orders.Count; i++)
                    {
                        if (CurrentQueryOrder.orders[i].orderid == selectorderid)
                        {
                            SelectOrder = CurrentQueryOrder.orders[i];
                            break;
                        }
                    }

                    if (SelectOrder == null)
                    {
                         MainModel.ShowLog("订单不存在，请刷新", false);
                        
                        return;
                    }
                    LoadPicScreen(true);

                    frmRefundSelect frmrefund = new frmRefundSelect(SelectOrder);
                    this.Invoke(new InvokeHandler(delegate()
             {
                

                 frmrefund.ShowDialog();
             }));
                     LoadPicScreen(false);
                    if (frmrefund.DialogResult == DialogResult.OK)
                    {

                        string AliPayAmt = frmrefund.Reforder.alipayamt;
                        string BalanceAmt = frmrefund.Reforder.balanceamt;
                        string CashPayAmt = frmrefund.Reforder.cashpayamt;
                        string WechatPayAmt = frmrefund.Reforder.wechatpayamt;
                        string YLPayAmt = frmrefund.Reforder.ylpayamt;
                        string PointPayAmt = frmrefund.Reforder.pointpayamt;
                        string CashCouponAmt = frmrefund.Reforder.cashcouponamt;

                        string totalpay = "";
                        if (!string.IsNullOrEmpty(AliPayAmt))
                        {
                            totalpay += "支付宝：" + AliPayAmt + " ";
                        }
                        if (!string.IsNullOrEmpty(BalanceAmt))
                        {
                            totalpay += "余额：" + BalanceAmt + " ";
                        }
                        if (!string.IsNullOrEmpty(CashPayAmt))
                        {
                            totalpay += "现金：" + CashPayAmt + " ";
                        }
                        if (!string.IsNullOrEmpty(WechatPayAmt))
                        {
                            totalpay += "微信：" + WechatPayAmt + " ";
                        }
                        if (!string.IsNullOrEmpty(YLPayAmt))
                        {
                            totalpay += "银联：" + YLPayAmt + " ";
                        }

                        if (!string.IsNullOrEmpty(PointPayAmt))
                        {
                            totalpay += "积分：" + PointPayAmt + " ";
                        }

                        if (!string.IsNullOrEmpty(CashCouponAmt))
                        {
                            totalpay += "代金券：" + CashCouponAmt + " ";
                        }


                        FrmConfirmBack frmconfirmback = new FrmConfirmBack("确认退款？", "应退 "+ totalpay, "");
                        frmconfirmback.Location = new Point(0, 0);
                        if (frmconfirmback.ShowDialog() != DialogResult.OK)
                        {
                            return;
                        }
                        string orderid = dgvOrderOnLine.Rows[e.RowIndex].Cells["orderid"].Value.ToString();
                        string ErrorMsg = "";
                        string resultorderid = httputil.Refund(frmrefund.Refrefundpara, ref ErrorMsg);
                        if (ErrorMsg != "")
                        {
                             MainModel.ShowLog(ErrorMsg, true);
                        }
                        else
                        {
                             MainModel.ShowLog("退款成功", false);

                            //ErrorMsg = "";
                            //PrintDetail printdetail = httputil.GetPrintDetail(resultorderid, ref ErrorMsg);
                            //if (ErrorMsg != "" || printdetail == null)
                            //{
                            //     MainModel.ShowLog(ErrorMsg, true);
                            //}
                            //else
                            //{
                            //    string PrintErrorMsg = "";
                            //    bool printresult = PrintUtil.PrintOrder(printdetail, false, ref PrintErrorMsg);

                            //    if (PrintErrorMsg != "" || !printresult)
                            //    {
                            //         MainModel.ShowLog(PrintErrorMsg, true);
                            //    }
                            //    else
                            //    {
                            //        //   MainModel.ShowLog("打印完成", false);
                            //    }

                            //}
                        }
                        IsEnable = true;
                        //后端订单信息更新需要时间，延时刷新
                        Delay.Start(300);
                        btnQuery_Click(null, null);
                    }


                }
                else if (dgvOrderOnLine.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == bmpReprint)
                {
                    IsEnable = false;

                    FrmConfirmBack frmconfirmback = new FrmConfirmBack("确认重打小票？", "", "");
                    frmconfirmback.Location = new Point(0, 0);
                    if (frmconfirmback.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }

                    LoadingHelper.ShowLoadingScreen("加载中...");
                    ReceiptUtil.EditReprintCount(1);

                    string orderid = dgvOrderOnLine.Rows[e.RowIndex].Cells["orderid"].Value.ToString();
                    string ErrorMsg = "";
                    PrintDetail printdetail = httputil.GetPrintDetail(orderid, ref ErrorMsg);

                    if (ErrorMsg != "" || printdetail == null)
                    {
                        LoadingHelper.CloseForm();
                         MainModel.ShowLog(ErrorMsg, true);
                    }
                    else
                    {
                        //SEDPrint(printdetail);
                        string PrintErrorMsg = "";
                        bool printresult = PrintUtil.PrintOrder(printdetail, false, ref PrintErrorMsg);

                        if (PrintErrorMsg != "" || !printresult)
                        {
                             MainModel.ShowLog(PrintErrorMsg, true);
                        }
                        else
                        {
                             MainModel.ShowLog("打印完成", false);
                        }

                    }
                    Delay.Start(300);
                    IsEnable = true;
                    LoadingHelper.CloseForm();
                    btnQuery_Click(null, null);
                }
            }
            catch (Exception ex)
            {

                LogManager.WriteLog("ERROR", "订单查询操作异常" + ex.Message);
            }
            finally
            {
                LoadingHelper.CloseForm();
                IsEnable = true;
                LoadPicScreen(false);

            }
        }

        public void frmOrderQuery_SizeChanged(object sender, EventArgs e)
        {
            // asf.ControlAutoSize(this);
        }


        private void LoadPicScreen(bool isShown)
        {
            try
            {
                if (isShown)
                {
                    picScreen.BackgroundImage = MainModel.GetWinformImage(this);
                    picScreen.Size = new System.Drawing.Size(this.Width, this.Height);
                    picScreen.Visible = true;
                }
                else
                {
                    //picScreen.Size = new System.Drawing.Size(0, 0);
                    picScreen.Visible = false;
                }

                Application.DoEvents();
            }
            catch (Exception ex)
            {
                picScreen.Visible = false;
                LogManager.WriteLog("修改订单查询窗体背景图异常：" + ex.Message);
            }
        }

        private void picScreen_Click(object sender, EventArgs e)
        {
            LoadPicScreen(false);
        }



        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (!IsEnable)
            {
                return;
            }
            LastOrderid = "0";
            dgvOrderOnLine.Rows.Clear();
            QueryOrder();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            dgvOrderOnLine.Rows.Clear();
            QueryOrder();
        }

        private void timerNow_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void dgvOrderOnLine_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (e.NewValue + dgvOrderOnLine.DisplayedRowCount(false) == dgvOrderOnLine.Rows.Count)
                {

                    if (HaveNextPage)
                    {
                        QueryOrder();
                    }
                    else
                    {
                        MainModel.ShowLog("没有更多数据了", false);
                    }

                    //if (sortProductByFirstCategoryid[CurrentFirstCategoryid].Count > 0 && sortProductByFirstCategoryid[CurrentFirstCategoryid].Count <= dgvGoodPic.Rows.Count * 4)
                    //{
                    //    lblOver.Visible = true;
                    //}
                }
                else
                {
                    //lblOver.Visible = false;
                }
            }
        }


        public void btnWindows_Click(object sender, EventArgs e)
        {
            MainModel.ShowWindows();
        }

        private void btnQueryOnline_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }


                thisisoffline = false;

                btnQueryOnline.BackColor = Color.White;
                btnQueryOffLine.BackColor = Color.LightSteelBlue;

                dgvOrderOnLine.Visible = true;
                pnlDgvHead.Visible = true;
                dgvOrderOffLine.Visible = false;
                pnlDgvOffLineHead.Visible = false;


                if (dgvOrderOnLine.Rows.Count > 0)
                {
                    pnlEmptyOrder.Visible = false;
                }
                else
                {
                    pnlEmptyOrder.Visible = false;
                }

                btnToday.PerformClick();
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("切换订单查询模式异常" + ex.Message, true);
            }
        }

        private void btnQueryOffLine_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }
                thisisoffline = true;
                btnQueryOnline.BackColor = Color.LightSteelBlue;
                btnQueryOffLine.BackColor = Color.White;

                dgvOrderOnLine.Visible = false;
                pnlDgvHead.Visible = false;

                dgvOrderOffLine.Visible = true;              
                pnlDgvOffLineHead.Visible = true;

                if (dgvOrderOffLine.Rows.Count > 0)
                {
                    pnlEmptyOrder.Visible = false;
                }
                else
                {
                    pnlEmptyOrder.Visible = false;
                }
                Application.DoEvents();
                btnToday.PerformClick();
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("切换订单查询模式异常" + ex.Message, true);
            }
        }

        private void dgvOrderOffLine_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }

                if (e.RowIndex < 0)
                    return;
              

                if (dgvOrderOffLine.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == bmpRefund)
                {
                    
                    string selectorderid = dgvOrderOffLine.Rows[e.RowIndex].Cells["OffLineOrderID"].Value.ToString();
                    IsEnable = false;

                   DBORDER_BEANMODEL order = orderbll.GetModel(selectorderid);


                   if (order == null)
                    {
                         MainModel.ShowLog("订单不存在，请刷新", false);  

                        return;
                    }

                   FrmConfirmBack frmconfirmback = new FrmConfirmBack("确认退款？", "应退 现金：" + order.PRICETOTAL.ToString("f2"), "");
                   frmconfirmback.Location = new Point(0, 0);
                   if (frmconfirmback.ShowDialog() != DialogResult.OK)
                   {
                       return;
                   }

                  
                       LoadingHelper.ShowLoadingScreen("加载中...");
                       OffLineOrder offlineorder = JsonConvert.DeserializeObject<OffLineOrder>(order.ORDER_JSON);

                      
                        offlineorder.hasrefunded = 1;
                       


                       string errormsg = "";
                       bool result = true;
                       if (!MainModel.IsOffLine)
                       {
                           result = httputil.OffLineRefund(offlineorder, ref errormsg);
                       }
                        
                       LoadingHelper.CloseForm();

                       if (result)
                       {
                           order.ORDERSTATUSVALUE = 5;
                           order.ORDERSTATUS = "已退款";

                           long timenow = Convert.ToInt64(MainModel.getStampByDateTime(DateTime.Now));
                           order.REFUND_TIME = timenow;

                           if (!MainModel.IsOffLine) //在线情况下已调用退款接口，更新和退款时间一致防止再上传
                           {
                               order.SYN_TIME = timenow;
                           }
                           
                           orderbll.Update(order);

                           ReceiptUtil.EditRefund(1, order.PRICETOTAL);
                            MainModel.ShowLog("退款成功", false);
                       }
                       else
                       {
                            MainModel.ShowLog("退款失败"+errormsg, false);
                       }
                   
                  
                                IsEnable = true;
                        //后端订单信息更新需要时间，延时刷新
                        Delay.Start(300);
                        btnQuery_Click(null, null);
                    


                }

                else if (dgvOrderOffLine.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == bmpNotRefund)
                {

                    MainModel.ShowLog("在线收银模式下，未同步的订单不可以进行退款操作",false);


                }
                else if (dgvOrderOffLine.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == bmpReprint)
                {
                    IsEnable = false;

                    FrmConfirmBack frmconfirmback = new FrmConfirmBack("确认重打小票？", "", "");
                    frmconfirmback.Location = new Point(0, 0);
                    if (frmconfirmback.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }

                    LoadingHelper.ShowLoadingScreen("加载中...");
                    ReceiptUtil.EditReprintCount(1);

                    string orderid = dgvOrderOffLine.Rows[e.RowIndex].Cells["OffLineOrderID"].Value.ToString();

                    DBORDER_BEANMODEL offlineorder= orderbll.GetModel(orderid);

                   
                        
                        string PrintErrorMsg = "";
                        bool printresult = PrintUtil.PrintOrder(orderid, false, ref PrintErrorMsg);

                        if (PrintErrorMsg != "" || !printresult)
                        {
                             MainModel.ShowLog(PrintErrorMsg, true);
                        }
                        else
                        {
                             MainModel.ShowLog("打印完成", false);
                        }

                        IsEnable = true;
                    Delay.Start(300);
                    LoadingHelper.CloseForm();
                    btnQuery_Click(null, null);
                }
            }
            catch (Exception ex)
            {

                LogManager.WriteLog("ERROR", "订单查询操作异常" + ex.Message);
            }
            finally
            {
                LoadingHelper.CloseForm();
                IsEnable = true;
                LoadPicScreen(false);

            }
        }

        //控制仅允许录入数字
        private void TextNUMBER_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                TextBox txt = sender as TextBox;
                e.Handled = true;
                char ch = e.KeyChar;

                if (ch >= '0' && ch <= '9')
                    e.Handled = false;

                if (ch == (char)Keys.Back)
                    e.Handled = false;

            }
            catch { }
        }

        private void dtEnd_MouseDown(object sender, MouseEventArgs e)
        {
            dtEnd.Focus();
            SendKeys.Send("{F4}");
        }

        private void dtStart_MouseDown(object sender, MouseEventArgs e)
        {
            dtStart.Focus();
            SendKeys.Send("{F4}");
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            if (txtPhone.Text.Length > 0)
            {
                lblPhoneShuiyin.Visible = false;
            }
            else
            {
                lblPhoneShuiyin.Visible = true;
            }
        }

        private void lblPhoneShuiyin_Click(object sender, EventArgs e)
        {
            txtPhone.Focus();
        }

        private void txtOrderID_TextChanged(object sender, EventArgs e)
        {
            if (txtOrderID.Text.Length > 0)
            {
                
                lblOrderIDShuiyin.Visible = false;
            }
            else
            {
                lblOrderIDShuiyin.Visible = true;
            }
        }

        private void lblOrderIDShuiyin_Click(object sender, EventArgs e)
        {
            txtOrderID.Focus();
        }

        private void txt_Enter(object sender, EventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                GlobalUtil.OpenOSK();
                txt.Focus();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("焦点打开键盘异常" + ex.Message);
            }
        }

        private void txt_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!txtPhone.Focused && !txtOrderID.Focused)
                {
                    GlobalUtil.CloseOSK();
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("失去焦点关闭键盘异常" + ex.Message);
            }
        }

        private void frmOrderQuery_FormClosed(object sender, FormClosedEventArgs e)
        {
            GlobalUtil.CloseOSK();
        }

    }
}
