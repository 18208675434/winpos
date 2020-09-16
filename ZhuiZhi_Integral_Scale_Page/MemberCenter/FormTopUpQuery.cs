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
using ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model;
using System.IO;

namespace ZhuiZhi_Integral_Scale_UncleFruit
{
    public partial class FormTopUpQuery : Form
    {

        #region 成员变量
        HttpUtil httputil = new HttpUtil();

        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();
        
        //间隔天数
        private int CurrentInterval = 0;


        private PageBalanceDepositBill CurrentBalanceDepos = null;


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

        #endregion


        #region  页面加载与退出
        public FormTopUpQuery()
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

            btnToday_Click(null, null);

            txtPhone.Focus();
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

                CurrentBalanceDepos = null;
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
                CurrentBalanceDepos = null;
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
                CurrentBalanceDepos = null;
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
                   CurrentBalanceDepos = null;
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
               

                    LoadingHelper.ShowLoadingScreen();
                    IsEnable = false;


                    DepositListRequest para = new DepositListRequest();

                    para.phone = txtPhone.Text;
                    para.starttime = getStampByDateTime(dtStart.Value);
                    para.endtime = getStampByDateTime(dtEnd.Value);

                    para.page = CurrentPage;
                    para.size = 6;

                    string ErrorMsg = "";
                    CurrentBalanceDepos = httputil.ListDepositbill(para, ref ErrorMsg);




                    if (ErrorMsg != "" || CurrentBalanceDepos == null)
                {
                    MainModel.ShowLog(ErrorMsg, false);
                    return;
                }


                dgvOrderOnLine.Rows.Clear();
                rbtnPageUp.WhetherEnable = CurrentPage > 1;
                
                
                foreach (RowsItem order in CurrentBalanceDepos.rows)
                {
                    
                    
                    dgvOrderOnLine.Rows.Add(GetDateTimeByStamp(order.createdat.ToString()).ToString("yyyy-MM-dd HH:mm:ss"), order.id, order.phone, order.amount.ToString("f2")+"元",order.Refound);
                }
                dgvOrderOnLine.ClearSelection();
                Application.DoEvents();
            
                    pnlEmptyOrder.Visible = dgvOrderOnLine.Rows.Count == 0;

                    rbtnPageUp.WhetherEnable = CurrentPage > 1;

                    rbtnPageDown.WhetherEnable = CurrentBalanceDepos.page * CurrentBalanceDepos.size < CurrentBalanceDepos.total;

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

        private void dgvOrderOnLine_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
