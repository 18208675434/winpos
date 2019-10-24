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

        public frmReceiptQuery()
        {
            InitializeComponent();
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            dtReceiptData.Value = DateTime.Now;

            QueryReceipt();
        }

        private void btnYesterday_Click(object sender, EventArgs e)
        {
            dtReceiptData.Value = DateTime.Now.AddDays(-1);

            QueryReceipt();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            QueryReceipt();
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
                string operatertimestr = dtReceiptData.Value.ToString("yyyy-MM-dd");
                string shopid = MainModel.CurrentShopInfo.shopid;
                string deviceid = MainModel.CurrentShopInfo.deviceid.ToString();

                QueryReceiptPara queryreceiptpara = new QueryReceiptPara();
                queryreceiptpara.operatetimestr = operatertimestr;
                queryreceiptpara.shopid = shopid;
                queryreceiptpara.deviceid = deviceid;

                string ErrorMsg = "";
                List<ReceiptQuery> LstReceiptQuery = httputil.QueryReceipt(queryreceiptpara,ref ErrorMsg);

                CurrentLisstReceipt = LstReceiptQuery;

                if (ErrorMsg != "" || LstReceiptQuery == null )
                {
                    ShowLog(ErrorMsg, false);
                }
                else
                {
                    dgvReceipt.Rows.Clear();
                    foreach (ReceiptQuery  receiptquery in LstReceiptQuery)
                    {

                        string ReceiptData = dtReceiptData.Value.ToString("yyy-MM-dd");
                        string ReceiptTime = MainModel.GetDateTimeByStamp(receiptquery.starttime.ToString()).ToString("yyyy-MM-dd HH:mm:ss")+"\r\n" + "至" +"\r\n"+ MainModel.GetDateTimeByStamp(receiptquery.endtime.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        string Cashier = receiptquery.cashier;
                        string NetOperat=receiptquery.netsaleamt.ToString();
                        
                        //foreach(OrderPriceDetail orderprice in receiptquery.receiptdetail.basicinfo)
                        //{
                        //    if(orderprice.title.Contains("营业净额"))
                        //    {
                        //        NetOperat=orderprice.amount;
                        //        break;

                        //    }
                        //}

                        string TotalPay= receiptquery.totalpayment.ToString();

                        string TotalCash=receiptquery.cashtotalamt.ToString();

                        string PrintStatus = receiptquery.hasprint==1 ?"已打印":"未打印";
                        string PosType =receiptquery.hasprint==1 ?"在线模式":"离线模式";

                        dgvReceipt.Rows.Add(ReceiptData,ReceiptTime,Cashier,NetOperat,TotalPay,TotalCash,PrintStatus,PosType,"重打交班单");
                        //dgvOrderOnLine.Rows.Add(GetDateTimeByStamp(order.orderat.ToString()).ToString("yyyy-MM-dd HH:mm:ss"), order.orderid, order.customerphone, order.title, totalpay, order.orderstatus, "", "");
                    }

                    if (dgvReceipt.Rows.Count > 0)
                    {
                        pnlEmptyReceipt.Visible = false;
                    }
                    else
                    {
                        pnlEmptyReceipt.Visible = true;
                    }
                    ShowLog("刷新完成", false);
                }

                
            }
            catch (Exception ex)
            {
                ShowLog("查询交班异常：" + ex.Message, true);
            }
        }


        //TODO  修改样式
        private void ShowLog(string msg, bool iserror)
        {
            this.Invoke(new InvokeHandler(delegate()
            {

                frmMsg frmmsf = new frmMsg(msg, iserror, 1000);
                frmmsf.ShowDialog(); LogManager.WriteLog(msg);
            }));

        }

        private void dgvReceipt_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.RowIndex < 0)
                    return;
                if (dgvReceipt.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "重打交班单")
                {
                    ReceiptQuery receiptquery = CurrentLisstReceipt[e.RowIndex];

                    string ErrorMsgReceipt = "";
                    bool receiptresult = PrintUtil.ReceiptPrint(receiptquery.receiptdetail, ref ErrorMsgReceipt);

                    if (receiptresult)
                    {
                        ShowLog("打印完成",false);
                    }
                    else
                    {
                        ShowLog(ErrorMsgReceipt, true);
                    }

                   
                }
              
            }
            catch (Exception ex)
            {

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
    }
}
