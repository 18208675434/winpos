using Maticsoft.BLL;
using Maticsoft.Model;
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
using System.Windows.Forms;

namespace WinSaasPOS
{
    public partial class frmExpense : Form
    {
        /// <summary>
        /// 按比例缩放页面及控件
        /// </summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();

        //间隔天数
        private int CurrentInterval = 0;

        /// <summary>
        /// 接口访问类
        /// </summary>
        private HttpUtil httputil = new HttpUtil();
        public frmExpense()
        {
            InitializeComponent();

            btnMenu.Text = MainModel.CurrentUser.nickname + "，你好   ";
            lblShopName.Text = MainModel.CurrentShopInfo.shopname;
            lblTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            btnOnLineType.Left = lblShopName.Left + lblShopName.Width + 10;
            if (MainModel.IsOffLine)
            {
                btnOnLineType.BackgroundImage = Resources.ResourcePos.OffLineType; btnOnLineType.Text = "   离线";
            }
            else
            {
                btnOnLineType.BackgroundImage = Resources.ResourcePos.OnLineType; btnOnLineType.Text = "   在线";
            }
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            try
            {
                ShowPicScreen = false; this.Enabled = false;
                btnToday.FlatAppearance.BorderColor = Color.Red;
                btnYesterday.FlatAppearance.BorderColor = Color.Gray;
                btnWeek.FlatAppearance.BorderColor = Color.Gray;

                dtStart.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                CurrentInterval = 0;
                QueryExpense();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("查询当天交班信息异常：" + ex.Message);
            }finally{
                this.Enabled=true;
            }
        }

        private void btnYesterday_Click(object sender, EventArgs e)
        {
            try { 
            btnToday.FlatAppearance.BorderColor = Color.Gray;
            btnYesterday.FlatAppearance.BorderColor = Color.Red;
            btnWeek.FlatAppearance.BorderColor = Color.Gray;

            dtStart.Value = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"));
            CurrentInterval = 1;
            QueryExpense();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("查询昨天交班信息异常：" + ex.Message);
            }
            finally
            {
                this.Enabled = true;
            }
        }

        private void btnWeek_Click(object sender, EventArgs e)
        {
            try {

            btnToday.FlatAppearance.BorderColor = Color.Gray;
            btnYesterday.FlatAppearance.BorderColor = Color.Gray;
            btnWeek.FlatAppearance.BorderColor = Color.Red;

            dtStart.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            CurrentInterval = 7;
            QueryExpense();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("查询一周交班信息异常：" + ex.Message);
            }
            finally
            {
                this.Enabled = true;
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try { 
            //CurrentInterval = 30;
            CurrentInterval = 10;
            QueryExpense();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("查询交班信息异常：" + ex.Message);
            }
            finally
            {
                this.Enabled = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            frmExpenseSave frmexpensave = new frmExpenseSave();
            frmexpensave.Opacity = 0.95d;
            //frmexpensave.frmExpenseSave_SizeChanged(null,null);
           // frmexpensave.Size = new System.Drawing.Size(Screen.AllScreens[0].Bounds.Width / 3, Screen.AllScreens[0].Bounds.Height - 200);
            asf.AutoScaleControlTest(frmexpensave, 374, 567, Screen.AllScreens[0].Bounds.Width / 3, Screen.AllScreens[0].Bounds.Height - 200, true);

            frmexpensave.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width - frmexpensave.Width - 50, 100);
            frmexpensave.ShowDialog();
            
            this.Enabled = true;
            if (frmexpensave.DialogResult == DialogResult.OK)
            {
                QueryExpense();

            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private DBEXPENSE_BEANBLL expensebll = new DBEXPENSE_BEANBLL();
       
        private void QueryExpense()
        {
            try
            {
                      

                dgvExpense.Rows.Clear();

                if (MainModel.IsOffLine)
                {
                    DateTime EndTime = Convert.ToDateTime(dtStart.Value.ToString("yyyy-MM-dd") + " 23:59:59");
                    long endtime = Convert.ToInt64(MainModel.getStampByDateTime(EndTime));

                    DateTime StartTime = Convert.ToDateTime(DateTime.Now.AddDays(-CurrentInterval).ToString("yyyy-MM-dd") + " 00:00:00");
                    long starttime = Convert.ToInt64(MainModel.getStampByDateTime(StartTime));

                    string strwhere = " CREATEAT >" + starttime + " and CREATEAT < "+endtime ;
                  

                    List<DBEXPENSE_BEANMODEL> lstexpense =expensebll.GetModelList(strwhere);

                    if (lstexpense != null && lstexpense.Count > 0)
                    {
                        foreach (DBEXPENSE_BEANMODEL expense in lstexpense)
                        {
                            dgvExpense.Rows.Add(MainModel.GetDateTimeByStamp(expense.CREATEAT.ToString()).ToString(), expense.EXPENSENAME, expense.EXPENSEFEE, expense.CREATEBY);

                        }
                    }
                   
                    dgvExpense.ClearSelection();

                    if (dgvExpense.Rows.Count > 0)
                    {
                        MainModel.ShowLog("刷新完成", false);
                    }
                    else
                    {
                        // ShowLog("暂无数据", false);
                    }

                }
                else
                {

                    LoadingHelper.ShowLoadingScreen();//显示   

                    string ErrorMsg = "";
                    Expense[] expenses = httputil.QueryExpense(CurrentInterval, 0, MainModel.CurrentShopInfo.shopid, DateTime.Now.ToString("yyyy-MM-dd"), ref ErrorMsg);
                    //Expense[] expenses = httputil.QueryExpense(CurrentInterval, 0, MainModel.CurrentShopInfo.shopid, DateTime.Now.ToString("yyyy-MM-dd"), ref ErrorMsg);
                    if (ErrorMsg != "" || expenses == null)
                    {
                        MainModel.ShowLog(ErrorMsg, false);
                    }
                    else
                    {

                        foreach (Expense exp in expenses)
                        {

                            dgvExpense.Rows.Add(MainModel.GetDateTimeByStamp(exp.createdat.ToString()).ToString(), exp.expensename, exp.expensefee, exp.createby);
                        }
                    }
                    dgvExpense.ClearSelection();

                    if (dgvExpense.Rows.Count > 0)
                    {
                        MainModel.ShowLog("刷新完成", false);
                    }
                    else
                    {
                        // ShowLog("暂无数据", false);
                    }

                }
             

                LoadingHelper.CloseForm();//关闭
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

        public void frmExpense_SizeChanged(object sender, EventArgs e)
        {
           // asf.ControlAutoSize(this);
        }

        private void frmExpense_Shown(object sender, EventArgs e)
        {
            //btnMenu.Text = MainModel.CurrentUser.nickname + "，你好   ";
            //lblShopName.Text = MainModel.CurrentShopInfo.shopname;
            //timerNow.Interval = 1000;
            //timerNow.Enabled = true;

            //btnToday_Click(null,null);
            //QueryExpense();
        }
        private void frmExpense_Load(object sender, EventArgs e)
        {
            btnMenu.Text = MainModel.CurrentUser.nickname + "，你好   ";
            lblShopName.Text = MainModel.CurrentShopInfo.shopname;
            timerNow.Interval = 1000;
            timerNow.Enabled = true;
            Application.DoEvents();
            btnToday_Click(null, null);
        }

        private bool ShowPicScreen = true;
        private void picScreen_EnabledChanged(object sender, EventArgs e)
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
                LogManager.WriteLog("修改营业外支出窗体背景图异常：" + ex.Message);
            }
        }



        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();
    

        private void timerNow_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        
        }

        private void btnWindows_Click(object sender, EventArgs e)
        {
            MainModel.ShowWindows();
        }

        private void dtStart_MouseDown(object sender, MouseEventArgs e)
        {
            dtStart.Focus();
            SendKeys.Send("{F4}");
        }



    }
}
