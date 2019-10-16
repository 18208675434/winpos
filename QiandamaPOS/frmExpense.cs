using QiandamaPOS.Common;
using QiandamaPOS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QiandamaPOS
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
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            dtStart.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            CurrentInterval = 0;
            QueryExpense();
        }

        private void btnYesterday_Click(object sender, EventArgs e)
        {
            dtStart.Value = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"));
            CurrentInterval = 1;
            QueryExpense();
        }

        private void btnWeek_Click(object sender, EventArgs e)
        {
            dtStart.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            CurrentInterval = 7;
            QueryExpense();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            QueryExpense();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmExpenseSave frmexpensave = new frmExpenseSave();
            frmexpensave.Opacity = 0.95d;
            frmexpensave.frmExpenseSave_SizeChanged(null,null);
            frmexpensave.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width / 3, Screen.PrimaryScreen.Bounds.Height - 200);
            frmexpensave.Location = new System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width - frmexpensave.Width - 50, 100);
            frmexpensave.ShowDialog();

            if (frmexpensave.DialogResult == DialogResult.OK)
            {
               // todo

            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void QueryExpense()
        {
            try
            {
                LoadingHelper.ShowLoadingScreen();//显示         

                dgvExpense.Rows.Clear();
                string ErrorMsg = "";
               // Expense[] expenses =  httputil.QueryExpense(CurrentInterval,0,MainModel.CurrentShopInfo.shopid,DateTime.Now.ToString("yyyy-MM-dd"), ref ErrorMsg);
                Expense[] expenses = httputil.QueryExpense(CurrentInterval, 0, MainModel.CurrentShopInfo.shopid, dtStart.Value.ToString("yyyy-MM-dd"), ref ErrorMsg);
                if (ErrorMsg != "" || expenses == null)
                {
                    MainModel.ShowLog(ErrorMsg, false);
                }
                else
                {

                    foreach (Expense exp in expenses)
                    {

                        dgvExpense.Rows.Add(MainModel.GetDateTimeByStamp(exp.createdat.ToString()).ToString(),exp.expensename,exp.expensefee,exp.createby);
                    }
                }

                LoadingHelper.CloseForm();//关闭
            }
            catch (Exception ex)
            {
                LoadingHelper.CloseForm();//关闭
                MainModel.ShowLog("查询订单异常：" + ex.Message, true);
            }
        }

        public void frmExpense_SizeChanged(object sender, EventArgs e)
        {
           // asf.ControlAutoSize(this);
        }

        private void frmExpense_Shown(object sender, EventArgs e)
        {
            btnToday_Click(null,null);
            //QueryExpense();
        }
    }
}
