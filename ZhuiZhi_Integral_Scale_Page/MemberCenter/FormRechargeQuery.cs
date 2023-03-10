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
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;
using ZhuiZhi_Integral_Scale_UncleFruit.MenuUI;
using ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    public partial class FormRechargeQuery : Form
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

        /// <summary>
        /// this.enable=false; 页面不可用页面不可控；  通过该标志控制页面是否可用
        /// </summary>
        private bool IsEnable = true;
        #endregion

        #region  页面加载与退出
        public FormRechargeQuery()
        {
            InitializeComponent();
        }
        private void formRechargeQuery_Load(object sender, EventArgs e)
        {
         
            lblShopName.Text = MainModel.Titledata + "   " + MainModel.CurrentShopInfo.shopname;
            lblMenu.Text = MainModel.CurrentUser.nickname + ",你好";
            picMenu.Left = pnlMenu.Width - picMenu.Width - lblMenu.Width;
            lblMenu.Left = picMenu.Right;

        }
        private void frmRechargeQuery_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();

            btnToday_Click(null, null);
            txtUser.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (!IsEnable)
            {
                return;
            }
            this.Close();
        }


        private void formRechargeQuery_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
        #endregion

        #region 事件
        //充值查询
        private void btnRechargeQuery_Click(object sender, EventArgs e)
        {
            CurrentOrderType = OrderType.recharge;
            lblOrderType.Text = "充值单号:";
            lblOrderId.Text = "请输入充值单号";
            CurrentPage = 1;
            LoadDgvOrder();
        }
        //退款查询
        private void btnRechargeRtnQuery_Click(object sender, EventArgs e)
        {
            CurrentOrderType = OrderType.rtn;
            lblOrderType.Text = "退款单号:";
            lblOrderId.Text = "请输入退款单号";
            CurrentPage = 1;
            LoadDgvOrder();
        }
        #region  查询条件
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
                CurrentPage = 1;
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

                dtStart.Value = Convert.ToDateTime(DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd") + " 00:00:00");
                dtEnd.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                CurrentInterval = 7;
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
                GlobalUtil.CloseKeyBoard(this);
                CurrentInterval = 30;
                CurrentPage = 1;
                LoadDgvOrder();
            }));
        }


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

        private void txtUsr_TextChanged(object sender, EventArgs e)
        {
            if (txtUser.Text.Length > 0)
            {
                lblUserTip.Visible = false;
            }
            else
            {
                lblUserTip.Visible = true;
            }
        }

        private void lblUser_Click(object sender, EventArgs e)
        {
            GlobalUtil.ShowKeyBoard(this, ZhuiZhi_Integral_Scale_UncleFruit.MyControl.KeyBorderCharType.NUMBER);
            Delay.Start(100);
            this.Activate();
            lblUserTip.Focus();
        }

        private void txtOrderId_TextChanged(object sender, EventArgs e)
        {
            if (txtOrderId.Text.Length > 0)
            {

                lblOrderId.Visible = false;
            }
            else
            {
                lblOrderId.Visible = true;
            }
        }

        private void lblOrderId_Click(object sender, EventArgs e)
        {
            GlobalUtil.ShowKeyBoard(this, ZhuiZhi_Integral_Scale_UncleFruit.MyControl.KeyBorderCharType.NUMBER);
            Delay.Start(100);
            this.Activate();
            txtOrderId.Focus();
        }



        private void txt_OskClick(object sender, EventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                GlobalUtil.ShowKeyBoard(this, ZhuiZhi_Integral_Scale_UncleFruit.MyControl.KeyBorderCharType.NUMBER);

                Delay.Start(100);
                this.Activate();
                txt.Focus();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("焦点打开键盘异常" + ex.Message);
            }
        }

        #endregion

        #endregion
      
        #region  方法
        private int CurrentPage = 1;

        private int PageSize = 5;
        //当前类型
        private OrderType CurrentOrderType =OrderType.recharge;

        /// <summary> 加载订单
        /// </summary>
        private void LoadDgvOrder()
        {
            if (CurrentOrderType == OrderType.recharge)
            {
                btnRechargeRtnQuery.BackColor = Color.FromArgb(230, 230, 230);
                btnRechargeQuery.BackColor = Color.White;
                tlpDgvRecharge.Visible = true;
                tlpDgvRtn.Visible = false;
                LoadDgvRechargeOrder();
            }
            else
            {
                btnRechargeRtnQuery.BackColor = Color.White;
                btnRechargeQuery.BackColor = Color.FromArgb(230, 230, 230);
                tlpDgvRecharge.Visible = false;
                tlpDgvRtn.Visible = true;
                LoadDgvRtnOrder();
            }
        }

        /// <summary> 加载充值订单
        /// </summary>
        private void LoadDgvRechargeOrder()
        {
            try
            {
                pnlEmptyOrder.Visible = false;
                LoadingHelper.ShowLoadingScreen();
                IsEnable = false;

                DepositListRequest para = new DepositListRequest();
                para.shopid = MainModel.CurrentShopInfo.shopid;
                para.tenantid = MainModel.CurrentShopInfo.tenantid;
                para.id = txtOrderId.Text;
                para.phone = txtUser.Text;            
                para.starttime = getStampByDateTime(dtStart.Value);
                para.endtime = getStampByDateTime(dtEnd.Value);
                para.size = PageSize;
                para.page = CurrentPage;
                //para.pagination = "true";
                string ErrorMsg = "";
                PageBalanceDepositBill currentBalanceDepos = httputil.ListDepositbill(para, ref ErrorMsg);
                if (ErrorMsg != "" || currentBalanceDepos == null)
                {
                    MainModel.ShowLog(ErrorMsg, false);
                    return;
                }


                dgvRecharge.Rows.Clear();
                rbtnPageUp.WhetherEnable = CurrentPage > 1;

                foreach (RowsItem order in currentBalanceDepos.rows)
                {
                    dgvRecharge.Rows.Add(GetDateTimeByStamp(order.createdat.ToString()).ToString("yyyy-MM-dd HH:mm:ss"), order.id, order.phone, order.amount.ToString("f2") , order.rewardamount.ToString("f2") , order.paymodeforapi, order.operatorname + "\r\n" + order.operatorphone);
                }
                dgvRecharge.ClearSelection();

                Dictionary<String,decimal> dicSum=  httputil.SumDepositByCondition(para, ref ErrorMsg);
                if (ErrorMsg != "" || currentBalanceDepos == null)
                {
                    MainModel.ShowLog(ErrorMsg, false);
                    return;
                }
                if(dicSum.ContainsKey("totalamount"))
                {
                    lbRechargeSum.Text = dicSum["totalamount"].ToString("f2") + "元";
                }
                if (dicSum.ContainsKey("totalrewardamount"))
                {
                    lbRechargeGiftSum.Text = dicSum["totalrewardamount"].ToString("f2") + "元";
                }
                Application.DoEvents();

                pnlEmptyOrder.Visible = dgvRecharge.Rows.Count == 0;
                rbtnPageUp.WhetherEnable = CurrentPage > 1;
                rbtnPageDown.WhetherEnable = currentBalanceDepos.page * currentBalanceDepos.size < currentBalanceDepos.total;

            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载充值列表异常" + ex.Message, true);
            }
            finally
            {
                IsEnable = true;
                LoadingHelper.CloseForm();
            }
        }

        /// <summary> 加载退款订单
        /// </summary>
        private void LoadDgvRtnOrder()
        {
            try
            {
                pnlEmptyOrder.Visible = false;
                LoadingHelper.ShowLoadingScreen();
                IsEnable = false;

                DepositListRequest para = new DepositListRequest();
                para.shopid = MainModel.CurrentShopInfo.shopid;
                para.tenantid = MainModel.CurrentShopInfo.tenantid;
                para.phone = txtUser.Text;
                para.id = txtOrderId.Text;
                para.starttime = getStampByDateTime(dtStart.Value);
                para.endtime = getStampByDateTime(dtEnd.Value);
                para.size = PageSize;
                para.page = CurrentPage;
                para.pagination = "true";
                string ErrorMsg = "";
                PageBalanceDepositRefundBill CurrentBalanceDepos = httputil.ListDepositRefundBillList(para, ref ErrorMsg);
                if (ErrorMsg != "" || CurrentBalanceDepos == null)
                {
                    MainModel.ShowLog(ErrorMsg, false);
                    return;
                }


                dgvRtn.Rows.Clear();
                rbtnPageUp.WhetherEnable = CurrentPage > 1;

                foreach (RowsRefundItem order in CurrentBalanceDepos.rows)
                {
                    dgvRtn.Rows.Add(GetDateTimeByStamp(order.createdat.ToString()).ToString("yyyy-MM-dd HH:mm:ss"), order.id, order.phone, order.depositbillid, order.capitalrefundamount.ToString("f2"), order.rewardrefundamount.ToString("f2"), order.refundtypeforapi, order.operatorname + "\r\n" + order.operatorphone);
                }
                dgvRtn.ClearSelection();

                Dictionary<String, decimal> dicSum = httputil.SumDepositRefundByCondition(para, ref ErrorMsg);
                if (ErrorMsg != "" || CurrentBalanceDepos == null)
                {
                    MainModel.ShowLog(ErrorMsg, false);
                    return;
                }
                if (dicSum.ContainsKey("totalamount"))
                {
                    lblRtnSum.Text = dicSum["totalamount"].ToString("f2") + "元";
                }
                if (dicSum.ContainsKey("totalrewardamount"))
                {
                    lblRtnGiftSum.Text = dicSum["totalrewardamount"].ToString("f2") + "元";
                }
                Application.DoEvents();

                pnlEmptyOrder.Visible = dgvRtn.Rows.Count == 0;

                rbtnPageUp.WhetherEnable = CurrentPage > 1;

                rbtnPageDown.WhetherEnable = CurrentBalanceDepos.page * CurrentBalanceDepos.size < CurrentBalanceDepos.total;

            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载退款列表异常" + ex.Message, true);
            }
            finally
            {
                IsEnable = true;
                LoadingHelper.CloseForm();
            }
        }

      
        #endregion


        #region  公用
        //当前时间戳
        private string getStampByDateTime(DateTime datetime)
        {            //DateTime datetime = DateTime.Now;
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            var result = (long)(datetime - startTime).TotalMilliseconds;

            return result.ToString();
        }
        //时间戳转时间
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

        //控制加载状态
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

     
    }
    /// <summary> 订单类型
    /// </summary>
    public enum OrderType
    {
        /// <summary> 充值
        /// </summary>
        recharge,
        /// <summary> 退款
        /// </summary>
        rtn,
    }
}
