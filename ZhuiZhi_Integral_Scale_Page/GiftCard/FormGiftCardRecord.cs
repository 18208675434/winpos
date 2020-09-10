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
using ZhuiZhi_Integral_Scale_UncleFruit.GiftCard.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.GiftCard
{
    public partial class FormGiftCardRecord : Form
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

        private GiftCardHttp giftcardhttp = new GiftCardHttp();

        #endregion


        #region  页面加载与退出
        public FormGiftCardRecord()
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
                dgvRecord.Rows.Clear();

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
                dgvRecord.Rows.Clear();

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
                dgvRecord.Rows.Clear();
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
                   dgvRecord.Rows.Clear();
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

               if (dgvRecord.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == bmpReprint)
                {
                    IsEnable = false;

                    if (!ConfirmHelper.Confirm("确认重打小票？"))
                    {
                        return;
                    }

                    LoadingHelper.ShowLoadingScreen("加载中...");
                    ReceiptUtil.EditReprintCount(1);

                    string orderid = dgvRecord.Rows[e.RowIndex].Cells["orderid"].Value.ToString();
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
                if (e.NewValue + dgvRecord.DisplayedRowCount(false) == dgvRecord.Rows.Count)
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

                dgvRecord.Visible = true;
                pnlDgvHead.Visible = true;
                pnlDgvOffLineHead.Visible = false;

                if (dgvRecord.Rows.Count > 0)
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
                rbtnPageUp.WhetherEnable = CurrentPage > 1;

                GiftCardRecordPara para = new GiftCardRecordPara();

                if (!string.IsNullOrEmpty(txtPhone.Text))
                {
                    para.customerphone = txtPhone.Text;
                }
             
               // para.needdetail = true;
                para.orderatstart =Convert.ToInt64( MainModel.getStampByDateTime(dtStart.Value));
                para.orderatend = Convert.ToInt64(MainModel.getStampByDateTime(dtEnd.Value));
                para.page = CurrentPage;
                para.size = PageSize;
                para.pagination = true;                
                para.shopid = MainModel.CurrentShopInfo.shopid;
                para.tenantid = MainModel.CurrentShopInfo.tenantid;

                string errormsg = "";
                GiftCardRecord record = giftcardhttp.GiftCardQuery(para, ref errormsg);

                
                if (!string.IsNullOrEmpty(errormsg) || record == null)
                {
                    MainModel.ShowLog(errormsg,false);
                    dgvRecord.Rows.Clear();
                    return;
                }
                dgvRecord.Rows.Clear();
                foreach (RowsItem item in record.rows)
                {
                    dgvRecord.Rows.Add(GetDateTimeByStamp(item.orderat.ToString()).ToString("yyyy-MM-dd HH:mm:ss"), item.id, item.customerphone, item.title, item.pspamt.ToString("f2"), bmpReprint);
                }
                                
                dgvRecord.ClearSelection();
                Application.DoEvents();

                rbtnPageDown.WhetherEnable = record.total > CurrentPage * PageSize;
              
                pnlEmptyOrder.Visible = dgvRecord.Rows.Count == 0;            

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

    }
}
