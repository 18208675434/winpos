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
    public partial class frmReceiptQuery : Form
    {

        HttpUtil httputil = new HttpUtil();


        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();

        List<ReceiptQuery> CurrentLisstReceipt = new List<ReceiptQuery>();


        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();



        private Bitmap bmpReprint;
        private Bitmap bmpWhite;

        public frmReceiptQuery()
        {
            InitializeComponent();
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            try{
                this.Enabled = false;
            btnToday.FlatAppearance.BorderColor = Color.Red;
            btnYesterday.FlatAppearance.BorderColor = Color.Gray;

            dtReceiptData.Value = DateTime.Now;

           // QueryReceipt();

             }
            catch (Exception ex)
            {
                LogManager.WriteLog("交班查询当天异常"+ex.Message);
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
                this.Enabled = false;
                btnToday.FlatAppearance.BorderColor = Color.Gray;
                btnYesterday.FlatAppearance.BorderColor = Color.Red;

                dtReceiptData.Value = DateTime.Now.AddDays(-1);

              //  QueryReceipt();
                this.Enabled = true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("交班查询昨天异常"+ex.Message);
            }
            finally
            {
                this.Enabled = true;
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        { try
            {
            QueryReceipt();
            }
        catch (Exception ex)
        {
            LogManager.WriteLog("交班查询异常" + ex.Message);
        }
        finally
        {
            this.Enabled = true;
        }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 查询交班
        /// </summary>
        /// <param name="interval"></param>
        private void QueryReceipt()
        {
            try
            {
                LoadingHelper.ShowLoadingScreen("加载中...");
                string operatertimestr = dtReceiptData.Value.ToString("yyyy-MM-dd");
                string shopid = MainModel.CurrentShopInfo.shopid;
                string deviceid = MainModel.CurrentShopInfo.deviceid.ToString();

                QueryReceiptPara queryreceiptpara = new QueryReceiptPara();
                queryreceiptpara.operatetimestr = operatertimestr;
                queryreceiptpara.shopid = shopid;
                queryreceiptpara.deviceid = deviceid;
                //queryreceiptpara.interval=

                string ErrorMsg = "";
                List<ReceiptQuery> LstReceiptQuery = httputil.QueryReceipt(queryreceiptpara,ref ErrorMsg);


                LoadingHelper.CloseForm();
                CurrentLisstReceipt = LstReceiptQuery;

                if (ErrorMsg != "" || LstReceiptQuery == null )
                {
                    MainModel.ShowLog(ErrorMsg, false);
                }
                else
                {
                    dgvReceipt.Rows.Clear();
                    foreach (ReceiptQuery  receiptquery in LstReceiptQuery)
                    {

                        string ReceiptData = dtReceiptData.Value.ToString("yyy-MM-dd");
                        string ReceiptTime = MainModel.GetDateTimeByStamp(receiptquery.starttime.ToString()).ToString("yyyy-MM-dd HH:mm:ss")+"\r\n" + "至" +"\r\n"+ MainModel.GetDateTimeByStamp(receiptquery.endtime.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        string Cashier = receiptquery.cashier;
                        string NetOperat=receiptquery.netsaleamt.ToString("f2");
                        

                        string TotalPay= receiptquery.totalpayment.ToString("f2");

                        string TotalCash=receiptquery.cashtotalamt.ToString("f2");

                        string PrintStatus = receiptquery.hasprint==1 ?"已打印":"未打印";
                        string PosType =receiptquery.hasprint==1 ?"在线模式":"离线模式";

                        dgvReceipt.Rows.Add(ReceiptData,ReceiptTime,Cashier,NetOperat,TotalPay,TotalCash,PrintStatus,PosType,bmpReprint);
                    }

                    if (dgvReceipt.Rows.Count > 0)
                    {
                        pnlEmptyReceipt.Visible = false;
                        MainModel.ShowLog("刷新完成", false);
                    }
                    else
                    {
                        pnlEmptyReceipt.Visible = true;
                        MainModel.ShowLog("暂无数据", false);
                    }                  
                }
                
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("查询交班异常：" + ex.Message, true);
            }
            finally
            {
                LoadingHelper.CloseForm();
            }
        }



        private void dgvReceipt_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.RowIndex < 0)
                    return;
                if (dgvReceipt.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == bmpReprint)
                {

                    this.Enabled = false;
                    LoadingHelper.ShowLoadingScreen("加载中...");
                    ReceiptQuery receiptquery = CurrentLisstReceipt[e.RowIndex];

                    string ErrorMsgReceipt = "";
                    bool receiptresult = PrintUtil.ReceiptPrint(receiptquery.receiptdetail, ref ErrorMsgReceipt);

                    LoadingHelper.CloseForm();
                    this.Enabled = true;
                    if (receiptresult)
                    {
                        MainModel.ShowLog("打印完成", false);
                    }
                    else
                    {
                        MainModel.ShowLog(ErrorMsgReceipt, true);
                    }
                   
                }
                dgvReceipt.ClearSelection();
              
            }
            catch (Exception ex)
            {

            }
            finally
            {
                LoadingHelper.CloseForm();
                this.Enabled = true;
            }
        }

        public void frmReceiptQuery_SizeChanged(object sender, EventArgs e)
        {
            //asf.ControlAutoSize(this);
        }

        private void dtReceiptData_ValueChanged(object sender, EventArgs e)
        {
            QueryReceipt();
        }

        private void frmReceiptQuery_Shown(object sender, EventArgs e)
        {
            btnMenu.Text = MainModel.CurrentUser.nickname + "，你好";
            lblShopName.Text = MainModel.CurrentShopInfo.shopname;
            timerNow.Interval = 1000;
            timerNow.Enabled = true;
            LoadBmp();
            btnToday_Click(null,null);
        }

        private void LoadBmp()
        {
            try
            {

                int height = dgvReceipt.RowTemplate.Height * 55 / 100;
                bmpReprint = new Bitmap(Resources.ResourcePos.ReprintReceipt, dgvReceipt.Columns["Reprint"].Width * 80 / 100, height);
                bmpWhite = Resources.ResourcePos.White;

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("加载页面图片异常" + ex.Message);
            }
        }

        private void timerNow_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
