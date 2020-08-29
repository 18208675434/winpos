using Maticsoft.BLL;
using Maticsoft.Model;
using Newtonsoft.Json;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;
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
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;
using ZhuiZhi_Integral_Scale_UncleFruit.MenuUI;

namespace ZhuiZhi_Integral_Scale_UncleFruit
{
    public partial class frmOrderQuery : Form
    {

        #region 成员变量
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

        private Bitmap bmpRefundByAmt;
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

        #endregion


        #region  页面加载与退出
        public frmOrderQuery()
        {
            InitializeComponent();
            // Application.EnableVisualStyles();
        }
        private void frmOrderQuery_Load(object sender, EventArgs e)
        {
         
            lblShopName.Text = MainModel.Titledata + "   " + MainModel.CurrentShopInfo.shopname;
            lblMenu.Text = MainModel.CurrentUser.nickname + ",你好";
            picMenu.Left = pnlMenu.Width - picMenu.Width - lblMenu.Width;
            lblMenu.Left = picMenu.Right;
            
            //Application.DoEvents();

           

        }
        private void frmOrderQuery_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            LoadBmp();

            btnToday_Click(null, null);

            txtPhone.Focus();
        }

        private void LoadBmp()
        {
            try
            {
                bmpRefund = (Bitmap)MainModel.GetControlImage(btnRefundPic);
                bmpReprint = (Bitmap)MainModel.GetControlImage(btnReprintPic);
                bmpNotRefund = (Bitmap)MainModel.GetControlImage(btnNotRefundPic);
                bmpRefundByAmt = (Bitmap)MainModel.GetControlImage(btnRefundByAmt);

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
        #endregion

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

                CurrentQueryOrder = null;
                CurrentPage = 1;
                LoadDgvOrder();
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

               // QueryOrder();
                CurrentPage = 1;
                CurrentQueryOrder = null;
                LoadDgvOrder();
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
               // QueryOrder();
                CurrentQueryOrder = null;
                CurrentPage = 1;
                LoadDgvOrder();
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
                   CurrentInterval = 30;
                   LastOrderid = "0";
                   dgvOrderOnLine.Rows.Clear();
                   //QueryOrder();
                   CurrentQueryOrder = null;
                   CurrentPage = 1;
                   LoadDgvOrder();
               }));
        }


        private bool HaveNextPage = true;



        #region 公用

      

        //当前时间戳
        private string getStampByDateTime(DateTime datetime)
        {            //DateTime datetime = DateTime.Now;
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


        #endregion
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
                        string totalpay = MenuHelper.GetTotalPayInfo(frmrefund.Reforder);

                        if (!ConfirmHelper.Confirm("确认退款？", "应退 " + totalpay))
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
                        IsEnable = true;
                        //后端订单信息更新需要时间，延时刷新
                        Delay.Start(300);
                        btnQuery_Click(null, null);
                    }


                }
                else if (dgvOrderOnLine.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == bmpReprint)
                {
                    IsEnable = false;

                    if (!ConfirmHelper.Confirm("确认重打小票？"))
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
                    Delay.Start(300);
                    IsEnable = true;
                    LoadingHelper.CloseForm();
                    btnQuery_Click(null, null);
                }
                else if (dgvOrderOnLine.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == bmpRefundByAmt)
                {
                    string selectorderid = dgvOrderOnLine.Rows[e.RowIndex].Cells["orderid"].Value.ToString();
                    IsEnable = false;

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

                    MenuHelper.ShowFormRefundByAmt(SelectOrder);

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



        private void dgvOrderOnLine_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (e.NewValue + dgvOrderOnLine.DisplayedRowCount(false) == dgvOrderOnLine.Rows.Count)
                {

                    if (HaveNextPage)
                    {
                       // QueryOrder();
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

                dgvOrderOnLine.Visible = true;
                pnlDgvHead.Visible = true;
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

            GlobalUtil.OpenOSK();
            Delay.Start(100);
            this.Activate();
            lblPhoneShuiyin.Focus();
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

            GlobalUtil.OpenOSK();
            Delay.Start(100);
            this.Activate();
            txtOrderID.Focus();
        }



        private void txt_Enter(object sender, EventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                GlobalUtil.OpenOSK();
                Delay.Start(100);
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


        #region  公用

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



        #endregion




        #region 分页

        private int CurrentPage = 1;

        private int PageSize = 6;

        private void LoadDgvOrder()
        {
            try
            {
                if (CurrentQueryOrder == null || (CurrentQueryOrder.orders.Count <= CurrentPage * PageSize && HaveNextPage))
                {

                    LoadingHelper.ShowLoadingScreen();
                    IsEnable = false;
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
                    LastOrderid = queryorder.lastorderid;
                    HaveNextPage = queryorder.hasnextpage == 1 ? true : false;
                }

                }


                rbtnPageUp.WhetherEnable = CurrentPage > 1;
               
                int startindex = (CurrentPage - 1) * 6;

                int lastindex = Math.Min(CurrentQueryOrder.orders.Count - 1, startindex + 5);

                dgvOrderOnLine.Rows.Clear();
                List<Order> lstOrder= CurrentQueryOrder.orders.GetRange(startindex, lastindex - startindex + 1);
                foreach (Order order in lstOrder)
                {
                    string totalpay = MenuHelper.GetTotalPayInfo(order);

                    if (order.orderstatusvalue == 5)
                    {
                        dgvOrderOnLine.Rows.Add(GetDateTimeByStamp(order.orderat.ToString()).ToString("yyyy-MM-dd HH:mm:ss"), order.orderid, order.customerphone, order.title, totalpay, order.orderstatus, bmpWhite, bmpWhite,bmpWhite);
                    }
                    else
                    {

                        Bitmap tempbmprefund = order.supportspecifiedamountrefund == 1 ? bmpRefundByAmt : bmpWhite;
                        dgvOrderOnLine.Rows.Add(GetDateTimeByStamp(order.orderat.ToString()).ToString("yyyy-MM-dd HH:mm:ss"), order.orderid, order.customerphone, order.title, totalpay, order.orderstatus, bmpReprint, bmpRefund,tempbmprefund);
                    }

                }
                dgvOrderOnLine.ClearSelection();
                Application.DoEvents();

                //在线接口每页20个 防止本地分页和接口分页最小积数  例 6*10 = 20*3
                if (CurrentQueryOrder.orders.Count > CurrentPage * 6 || HaveNextPage)
                {
                    rbtnPageDown.WhetherEnable = true;
                }
                else
                {
                    rbtnPageDown.WhetherEnable = false;
                }


                    pnlEmptyOrder.Visible = dgvOrderOnLine.Rows.Count == 0;
             

            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载订单列表异常"+ex.Message,true);
            }
            finally
            {
                IsEnable = true;
                LoadingHelper.CloseForm();
            }
        }
        #endregion

        private void rbtnPageUp_ButtonClick(object sender, EventArgs e)
        {

            if (!rbtnPageUp.WhetherEnable || !IsEnable)
            {
                return;
            }
            CurrentPage--;
            LoadDgvOrder();
        }

        private void rbtnPageDown_ButtonClick(object sender, EventArgs e)
        {
            if (!IsEnable || !rbtnPageDown.WhetherEnable)
            {
                return;
            }
            CurrentPage++;
            LoadDgvOrder();
        }



        private void txt_OskClick(object sender, EventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                GlobalUtil.OpenOSK();


                Delay.Start(100);
                this.Activate();
                txt.Focus();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("焦点打开键盘异常" + ex.Message);
            }
        }
    }
}
