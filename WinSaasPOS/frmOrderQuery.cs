using WinSaasPOS.Common;
using WinSaasPOS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        private int CurrentInterval =0;


        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();

        private QueryOrder CurrentQueryOrder = null;


        private string LastOrderid="0";


        private Bitmap bmpReprint;
        private Bitmap bmpRefund;
        private Bitmap bmpWhite;

        public frmOrderQuery()
        {
            InitializeComponent();
           // Application.EnableVisualStyles();
        }

        private void frmOrderQuery_Shown(object sender, EventArgs e)
        {


            LoadBmp();

            btnMenu.Text = MainModel.CurrentUser.nickname + "，你好";
            lblShopName.Text = MainModel.CurrentShopInfo.shopname;
            timerNow.Interval = 1000;
            timerNow.Enabled = true;
            Application.DoEvents();

            btnToday_Click(null,null);
    
            
        }

        private void LoadBmp()
        {
            try
            {

                int height = dgvOrderOnLine.RowTemplate.Height * 55 / 100;
                bmpRefund = new Bitmap(Resources.ResourcePos.Refund, dgvOrderOnLine.Columns["cancle"].Width * 80 / 100, height);
                
                bmpReprint = new Bitmap(Resources.ResourcePos.Reprint, dgvOrderOnLine.Columns["reprint"].Width * 80 / 100, height);
                bmpWhite = Resources.ResourcePos.White;

            }
            catch(Exception ex)
            {
                LogManager.WriteLog("加载页面图片异常"+ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            try
            {
                ShowPicScreen = false; this.Enabled = false;
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
                LogManager.WriteLog("查询当天订单信息异常："+ex.Message);
            }
            finally
            {
                this.Enabled = true;
            }
        }

        private void btnYesterday_Click(object sender, EventArgs e)
        {

            try
            {
                ShowPicScreen = false; this.Enabled = false;
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
                LogManager.WriteLog("查询昨天订单信息异常："+ex.Message);
            }
            finally
            {
                this.Enabled = true;
            }
        }

        private void btnWeek_Click(object sender, EventArgs e)
        {
            try
            {
                ShowPicScreen = false; this.Enabled = false;
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
                LogManager.WriteLog("查询最近一周订单信息异常："+ex.Message);
            }
            finally
            {
                this.Enabled = true;
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
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

            
                DateTime starttime = DateTime.Now;
                LoadingHelper.ShowLoadingScreen();//显示
               

                string phone = txtPhone.Text;
                string orderid = txtOrderID.Text;

                if (phone.Length != 0 && phone.Length != 11)
                {
                    ShowLog("手机号格式不正确！", false);
                    LoadingHelper.CloseForm();//关闭
                    return;
                }

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

                Console.WriteLine("访问接口时间"+(DateTime.Now-starttime).TotalMilliseconds);

                //CurrentQueryOrder = queryorder;
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
               
                if (ErrorMsg != "" || queryorder == null)
                {
                    ShowLog(ErrorMsg, false);
                }
                else
                {
                    //dgvOrderOnLine.Rows.Clear();
                    this.Enabled = true; //很重要  为false 时datagridview 界面滚动条显示不全
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
                            totalpay += "支付宝：" + AliPayAmt+" ";
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

                        if (order.orderstatusvalue==5)
                        {
                            dgvOrderOnLine.Rows.Add(GetDateTimeByStamp(order.orderat.ToString()).ToString("yyyy-MM-dd HH:mm:ss"), order.orderid, order.customerphone, order.title, totalpay, order.orderstatus, bmpWhite, bmpWhite);
                        }
                        else
                        {
                            dgvOrderOnLine.Rows.Add(GetDateTimeByStamp(order.orderat.ToString()).ToString("yyyy-MM-dd HH:mm:ss"), order.orderid, order.customerphone, order.title, totalpay, order.orderstatus, bmpReprint, bmpRefund);
                        }
                        
                    }
                    dgvOrderOnLine.ClearSelection();
                    Console.WriteLine("页面显示时间" + (DateTime.Now - starttime).TotalMilliseconds);
                    Application.DoEvents();

                    if (LastOrderid != "0")
                    {
                        btnFirst.Enabled = true;
                    }
                    else
                    {
                        btnFirst.Enabled = false;
                    }

                    if (queryorder.hasnextpage == 1)
                    {
                        LastOrderid = queryorder.lastorderid;
                        btnNext.Enabled = true;
                        HaveNextPage = true;
                    }
                    else
                    {
                        HaveNextPage = false;
                        btnNext.Enabled = false;
                    }
                 


                    if (dgvOrderOnLine.Rows.Count > 0)
                    {
                        pnlEmptyOrder.Visible = false;
                       
                       // ShowLog("刷新完成", false);
                    }
                    else
                    {
                        pnlEmptyOrder.Visible = true;
                        ShowLog("暂无数据", false);
                    }
                }

                Console.WriteLine("loading开始时间" + (DateTime.Now - starttime).TotalMilliseconds);

                LoadingHelper.CloseForm();//关闭
                Console.WriteLine("loading关闭时间" + (DateTime.Now - starttime).TotalMilliseconds);

                //Application.DoEvents();
                Console.WriteLine("完成时间" + (DateTime.Now - starttime).TotalMilliseconds);
            }
            catch (Exception ex)
            {
                LoadingHelper.CloseForm();//关闭
                ShowLog("查询订单异常："+ex.Message,true);
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



        //TODO  修改样式
        private void ShowLog(string msg, bool iserror)
        {

            MsgHelper.AutoShowForm(msg);
            //this.BeginInvoke(new InvokeHandler(delegate()
            //{

            //    frmMsg frmmsf = new frmMsg(msg, iserror, 1000);
            //    frmmsf.ShowDialog(); LogManager.WriteLog(msg);
            //}));

        }

        private void dgvOrderOnLine_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

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
                //        ShowLog("订单不存在，请刷新", false);
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
                //        ShowLog(ErrorMsg, true);
                //    }
                //    else
                //    {

                //        ShowLog("退款成功", false);

                //    }

                //    this.Enabled = true;
                //    btnQuery_Click(null, null);
                //}

                if (dgvOrderOnLine.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == bmpRefund)
                {

                    string selectorderid = dgvOrderOnLine.Rows[e.RowIndex].Cells["orderid"].Value.ToString();
                    this.Enabled = false;


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
                        ShowLog("订单不存在，请刷新", false);
                        this.Enabled = true;
                        return;
                    }

                    frmRefundSelect frmrefund = new frmRefundSelect(SelectOrder);

                    frmrefund.ShowDialog();

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

                        frmDeleteGood frmdelete = new frmDeleteGood("是否确认退款？", totalpay, "");
                        if (frmdelete.ShowDialog() != DialogResult.OK)
                        {
                            this.Enabled = true;
                            return;
                        }
                        string orderid = dgvOrderOnLine.Rows[e.RowIndex].Cells["orderid"].Value.ToString();
                        string ErrorMsg = "";
                        string resultorderid = httputil.Refund(frmrefund.Refrefundpara, ref ErrorMsg);
                        if (ErrorMsg != "")
                        {
                            ShowLog(ErrorMsg, true);
                        }
                        else
                        {
                             ShowLog("退款成功", false);

                            //ErrorMsg = "";
                            //PrintDetail printdetail = httputil.GetPrintDetail(resultorderid, ref ErrorMsg);
                            //if (ErrorMsg != "" || printdetail == null)
                            //{
                            //    ShowLog(ErrorMsg, true);
                            //}
                            //else
                            //{
                            //    string PrintErrorMsg = "";
                            //    bool printresult = PrintUtil.SEDPrint(printdetail, true, ref PrintErrorMsg);

                            //    if (PrintErrorMsg != "" || !printresult)
                            //    {
                            //        ShowLog(PrintErrorMsg, true);
                            //    }
                            //    else
                            //    {
                            //        //  ShowLog("打印完成", false);
                            //    }

                            //}
                        }
                        btnQuery_Click(null, null);
                    }




                    this.Enabled = true;
                }
                else if (dgvOrderOnLine.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == bmpReprint)
                {
                    ShowPicScreen = true; this.Enabled = false;
                    frmDeleteGood frmdelete = new frmDeleteGood("确认重打小票？", "", "");
                    if (frmdelete.ShowDialog() != DialogResult.OK)
                    {
                        this.Enabled = true;
                        return;
                    }

                    LoadingHelper.ShowLoadingScreen("加载中...");
                    ReceiptUtil.EditReprintCount(1);

                    string orderid = dgvOrderOnLine.Rows[e.RowIndex].Cells["orderid"].Value.ToString();
                    string ErrorMsg = "";
                    PrintDetail printdetail = httputil.GetPrintDetail(orderid, ref ErrorMsg);


                    if (ErrorMsg != "" || printdetail==null)
                    {
                      
                        LoadingHelper.CloseForm();
                        this.Enabled = true;
                        ShowLog(ErrorMsg, true);
                    }
                    else
                    {
                        //SEDPrint(printdetail);
                        string PrintErrorMsg ="";
                       bool printresult= PrintUtil.PrintOrder(printdetail,false, ref PrintErrorMsg);

                       if (PrintErrorMsg != "" || !printresult)
                       {
                           ShowLog(PrintErrorMsg,true);
                       }
                       else
                       {
                           ShowLog("打印完成",false);
                       }
                       
                    }
                    Delay.Start(300);
                    this.Enabled = true;
                    LoadingHelper.CloseForm();
                    btnQuery_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                this.Enabled = true;
                LogManager.WriteLog("ERROR","订单查询操作异常"+ex.Message);
            }
            finally
            {
                LoadingHelper.CloseForm();
                this.Enabled = true;
                
            }
        }

        public void frmOrderQuery_SizeChanged(object sender, EventArgs e)
        {
           // asf.ControlAutoSize(this);
        }

        private bool ShowPicScreen=true;
        private void frmOrderQuery_EnabledChanged(object sender, EventArgs e)
        {
            try
            {


                if (this.Enabled)
                {
                    picScreen.Visible = false;

                }
                else
                {
                    if (ShowPicScreen)
                    {
                        picScreen.BackgroundImage = MainModel.GetWinformImage(this);
                        picScreen.Size = new System.Drawing.Size(this.Width, this.Height);
                        picScreen.Visible = true;
                    }
                    else
                    {
                        ShowPicScreen = true;
                    }

                }

                Application.DoEvents();
            }
            catch (Exception ex)
            {
                picScreen.Visible = false;
                LogManager.WriteLog("修改订单查询窗体背景图异常：" + ex.Message);
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
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
                        MainModel.ShowLog("没有更多数据了",false);
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

    }
}
