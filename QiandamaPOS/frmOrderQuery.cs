using QiandamaPOS.Common;
using QiandamaPOS.Model;
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

namespace QiandamaPOS
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

        public frmOrderQuery()
        {
            InitializeComponent();
        }

        private void frmOrderQuery_Shown(object sender, EventArgs e)
        {

            btnMenu.Text = MainModel.CurrentUser.nickname + "，你好";
            timerNow.Interval = 1000;
            timerNow.Enabled = true;
            Application.DoEvents();
            //picScreen.BackgroundImage = MainModel.GetWinformImage(this);
            ////启动扫描处理线程
            //Thread threadItemExedate = new Thread(btnToday.PerformClick);
            //threadItemExedate.IsBackground = true;
            //threadItemExedate.Start();
            btnToday_Click(null,null);
    
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnToday_Click(object sender, EventArgs e)
        {

            this.Invoke(new InvokeHandler(delegate()
               {
                   dtStart.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                   dtEnd.Value = DateTime.Now;
                   CurrentInterval = 0;
                   LastOrderid = "0";
                   dgvOrderOnLine.Rows.Clear();
               }));
            QueryOrder();
        }

        private void btnYesterday_Click(object sender, EventArgs e)
        {
            this.Invoke(new InvokeHandler(delegate()
               {
                   dtStart.Value = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 00:00:00");
                   dtEnd.Value = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 23:59:59");
                   CurrentInterval = 1;
                   LastOrderid = "0";
                   dgvOrderOnLine.Rows.Clear();
               }));
            QueryOrder();
        }

        private void btnWeek_Click(object sender, EventArgs e)
        {
            this.Invoke(new InvokeHandler(delegate()
               {
                   dtStart.Value = Convert.ToDateTime(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + " 00:00:00");
                   dtEnd.Value = DateTime.Now;
                   CurrentInterval = 7;
                   LastOrderid = "0";
                   dgvOrderOnLine.Rows.Clear();
               }));
            QueryOrder();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.Invoke(new InvokeHandler(delegate()
               {
                   dgvOrderOnLine.Rows.Clear();
                   QueryOrder();
               }));
        }

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

                CurrentQueryOrder = queryorder;
                //if (LastOrderid == "0")
                //{
                //    CurrentQueryOrder = queryorder;
                //}
                //else
                //{
                //    foreach (Order order in queryorder.orders)
                //    {
                //        CurrentQueryOrder.orders.Add(order);
                //    }
                //}
               
                if (ErrorMsg != "" || queryorder == null)
                {
                    ShowLog(ErrorMsg, false);
                }
                else
                {
                    //dgvOrderOnLine.Rows.Clear();

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
                            dgvOrderOnLine.Rows.Add(GetDateTimeByStamp(order.orderat.ToString()).ToString("yyyy-MM-dd HH:mm:ss"), order.orderid, order.customerphone, order.title, totalpay, order.orderstatus, "", "");
                        }
                        else
                        {
                            dgvOrderOnLine.Rows.Add(GetDateTimeByStamp(order.orderat.ToString()).ToString("yyyy-MM-dd HH:mm:ss"), order.orderid, order.customerphone, order.title, totalpay, order.orderstatus, "重打小票", "退款");
                        }

                        
                       
                    }
                    dgvOrderOnLine.ClearSelection();
                    Console.WriteLine("页面显示时间" + (DateTime.Now - starttime).TotalMilliseconds);


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
                    }
                    else
                    {
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
            this.BeginInvoke(new InvokeHandler(delegate()
            {

                frmMsg frmmsf = new frmMsg(msg, iserror, 1000);
                frmmsf.ShowDialog(); LogManager.WriteLog(msg);
            }));

        }

        private void dgvOrderOnLine_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.RowIndex < 0)
                    return;
                if (dgvOrderOnLine.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "退款")
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
                        ShowLog("订单不存在，请刷新",false);
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

                          //  ShowLog("退款成功", false);



                            ErrorMsg = "";
                            PrintDetail printdetail = httputil.GetPrintDetail(resultorderid, ref ErrorMsg);
                            if (ErrorMsg != "" || printdetail == null)
                            {
                                ShowLog(ErrorMsg, true);
                            }
                            else
                            {
                                string PrintErrorMsg = "";
                                bool printresult = PrintUtil.SEDPrint(printdetail, true, ref PrintErrorMsg);

                                if (PrintErrorMsg != "" || !printresult)
                                {
                                    ShowLog(PrintErrorMsg, true);
                                }
                                else
                                {
                                  //  ShowLog("打印完成", false);
                                }

                            }
                        }
                        btnQuery_Click(null, null);
                    }
                  
                  


                    this.Enabled = true;
                }
                else if (dgvOrderOnLine.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "重打小票")
                {

                    ReceiptUtil.EditReprintCount(1);

                    string orderid = dgvOrderOnLine.Rows[e.RowIndex].Cells["orderid"].Value.ToString();
                    string ErrorMsg = "";
                    PrintDetail printdetail = httputil.GetPrintDetail(orderid, ref ErrorMsg);
                    if (ErrorMsg != "" || printdetail==null)
                    {
                        ShowLog(ErrorMsg, true);
                    }
                    else
                    {

                        

                        //SEDPrint(printdetail);
                        string PrintErrorMsg ="";
                       bool printresult= PrintUtil.SEDPrint(printdetail,false, ref PrintErrorMsg);

                       if (PrintErrorMsg != "" || !printresult)
                       {
                           ShowLog(PrintErrorMsg,true);
                       }
                       else
                       {
                           ShowLog("打印完成",false);
                       }
                        //ShowLog(result, false);

                        //dgvOrderOnLine.Rows.RemoveAt(e.RowIndex);
                    }
                    btnQuery_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                this.Enabled = true;
                LogManager.WriteLog("ERROR","订单查询操作异常"+ex.Message);
            }
        }

        public void frmOrderQuery_SizeChanged(object sender, EventArgs e)
        {
           // asf.ControlAutoSize(this);
        }

        private void frmOrderQuery_EnabledChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.Enabled)
                {
                    picScreen.Visible = false;
                    Application.DoEvents();
                }
                else
                {
                    DateTime starttime = DateTime.Now;
                    picScreen.BackgroundImage = MainModel.GetWinformImage(this);
                    Console.WriteLine("获取图片时间" + (DateTime.Now - starttime).TotalMilliseconds);

                    picScreen.Size = new System.Drawing.Size(this.Width, this.Height);
                    //picScreen.Location = new System.Drawing.Point(0, 0);
                    picScreen.Visible = true;
                    // this.Opacity = 0.9d;

                    Console.WriteLine("页面灰度设置时间"+(DateTime.Now-starttime).TotalMilliseconds);
                }
            }
            catch (Exception ex)
            {
                picScreen.Visible = false;
                LogManager.WriteLog("修改主窗体背景图异常：" + ex.Message);
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

        //private void SEDPrint(PrintDetail printdetail)
        //{
        //    try
        //    {
        //        string logofilepath = AppDomain.CurrentDomain.BaseDirectory + "\\Picture\\headlogo.bmp";
        //        StringBuilder logobuilder = new StringBuilder(logofilepath);
        //        PrintUtil.OpenDevice();

        //        PrintUtil.SelectFond(0);
        //        PrintUtil.SetInterCharSet(15);

        //        PrintUtil.SetAlign(1);
        //        PrintUtil.PrintBitmap(logobuilder,0);

        //        PrintUtil.SetAlign(0);
        //        PrintUtil.SetFontSize(0, 1);
        //        StringBuilder stringb = new StringBuilder("   欢迎光临[钱大妈]不卖隔夜肉\n");
        //        PrintUtil.PrintStr(stringb);

        //        PrintUtil.SetFontSize(0, 0);
        //        PrintUtil.FeedPaper();
        //        StringBuilder sb = new StringBuilder();
        //        sb.Append("订单号：" + printdetail.orderid+"\n");
        //        sb.Append("门店：" + MainModel.CurrentShopInfo.shopname + "\n");
        //        sb.Append("地址：" + MainModel.CurrentShopInfo.address + "\n");
        //        sb.Append("电话：" + MainModel.CurrentShopInfo.tel + "\n");
        //        sb.Append("收银员：" + MainModel.CurrentUser.nickname + "         机：" + MainModel.CurrentShopInfo.deviceid + "\n");
        //        sb.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"+"\n"));
        //        sb.Append(DateTime.Now.ToString("-----------------------------" + "\n"));
        //        foreach (ProductDetail pro in printdetail.products)
        //        {
        //            sb.Append(DateTime.Now.ToString(pro.skucode + "  " + pro.title + "\n"));
        //            if (pro.goodstagid == 0)//标品
        //            {
        //                string priceandnum = "   " + pro.num + "  X  " + pro.price.saleprice;
                        
        //                sb.Append(DateTime.Now.ToString(priceandnum+ pro.price.total.ToString().PadLeft(26-priceandnum.Length,' ') + "\n"));

        //            }
        //            else if (pro.goodstagid == 1) //散称称重
        //            {
        //                string priceandnum = "   " + pro.specnum + "  X  " + pro.price.saleprice;
        //                sb.Append(DateTime.Now.ToString(priceandnum + pro.price.total.ToString().PadLeft(26 - priceandnum.Length, ' ') + "\n"));
        //            }
        //            else if (pro.goodstagid == 2) //多规格原称重
        //            {
        //                string priceandnum = "   " + pro.specnum + "  X  " + pro.price.saleprice;
        //                sb.Append(DateTime.Now.ToString(priceandnum + pro.price.total.ToString().PadLeft(26 - priceandnum.Length, ' ') + "\n"));
        //            }

        //        }
        //        sb.Append(DateTime.Now.ToString("-----------------------------" + "\n"));

        //        //汉字占两位 TODO 判断前面汉字和英文数字
        //        foreach (Orderpricedetail pricedetail in printdetail.orderpricedetails)
        //        {
        //            sb.Append(pricedetail.title + pricedetail.amount.PadLeft(28 - pricedetail.title.Length*2, ' ') + "\n");

        //        }
        //        sb.Append(DateTime.Now.ToString("-----------------------------" + "\n"));

        //        foreach (Paydetail paydetail in printdetail.paydetail)
        //        {
        //            sb.Append(paydetail.title + paydetail.amount.PadLeft(28 - paydetail.title.Length*2, ' ') + "\n");

        //        }

        //        sb.Append(DateTime.Now.ToString("-----------------------------" + "\n"));
        //        sb.Append("多谢惠顾，欢迎下次光临！" + "\n");
        //        sb.Append("钱大妈官网:http://www.qdama.cn" + "\n");
        //        sb.Append("顾客服务热线: 400-628-5880" + "\n");
        //        sb.Append("官方会员微信: qdama888" + "\n");
        //        sb.Append("  加入会员 积分买菜 优惠多多" + "\n");


        //        PrintUtil.PrintStr(sb);
        //        //TODO  二维码


        //        PrintUtil.SetAlign(1);
        //        string qrcodepath = AppDomain.CurrentDomain.BaseDirectory + "\\Picture\\QrCode.bmp";
        //        StringBuilder sbqr = new StringBuilder(qrcodepath);

        //        PrintUtil.PrintBitmap(sbqr,3);


        //        PrintUtil.SetFontSize(0,1);
        //        StringBuilder strEnd = new StringBuilder("新鲜·便捷·优选 100%退换货\n\n\n\n\n\n");
        //        PrintUtil.PrintStr(strEnd);
        //        PrintUtil.SetFontSize(0, 0);

        //        PrintUtil.CloseDevice();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("打印出现异常",ex.Message);
        //    }
            
        //}
        
           

        //List<A> list = new List<A>();......List<a> newList = list.Select(x => x.a).ToList();
    }
}
