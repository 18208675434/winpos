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
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ZhuiZhi_Integral_Scale_UncleFruit
{
    public partial class frmReceiptQuery : Form
    {

        HttpUtil httputil = new HttpUtil();


        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();

        List<ReceiptQuery> CurrentLisstReceipt = new List<ReceiptQuery>();

        List<DBRECEIPT_BEANMODEL> CurrentListReceiptOffLine = new List<DBRECEIPT_BEANMODEL>();


        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();

        /// <summary>
        /// this.enable=false; 页面不可用页面不可控；  通过该标志控制页面是否可用
        /// </summary>
        private bool IsEnable = true;

        private Bitmap bmpReprint;
        private Bitmap bmpWhite;

        public frmReceiptQuery()
        {
            InitializeComponent();
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            try{


                if (!IsEnable)
                {
                    return;
                }
                IsEnable = false;
            btnToday.FlatAppearance.BorderColor = Color.Red;
            btnYesterday.FlatAppearance.BorderColor = Color.Gray;

            dtReceiptData.Value = DateTime.Now;

           // QueryReceipt();
            LoadDgvReceipt(true);

             }
            catch (Exception ex)
            {
                LogManager.WriteLog("交班查询当天异常"+ex.Message);
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

                dtReceiptData.Value = DateTime.Now.AddDays(-1);
               // QueryReceipt();

                LoadDgvReceipt(true);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("交班查询昨天异常"+ex.Message);
            }
            finally
            {
                IsEnable = true;
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


        private DBRECEIPT_BEANBLL receiptbll = new DBRECEIPT_BEANBLL();
        /// <summary>
        /// 查询交班
        /// </summary>
        /// <param name="interval"></param>
        private void QueryReceipt()
        {
            try
            {
                if (MainModel.IsOffLine)
                {
                    string operatertimestr = dtReceiptData.Value.ToString("yyyy-MM-dd");
                    string strwhere = " OPERATETIMESTR = '" + operatertimestr + "' and CREATE_URL_IP='" + MainModel.URL+"' order by CREATE_TIME desc";

                    CurrentListReceiptOffLine = receiptbll.GetModelList(strwhere);
                    dgvReceipt.Rows.Clear();
                    if (CurrentListReceiptOffLine != null && CurrentListReceiptOffLine.Count > 0)
                    {
                        foreach (DBRECEIPT_BEANMODEL receipt in CurrentListReceiptOffLine)
                        {
                            //receipt.RECEIPTDETAIL;

                            string ReceiptData = dtReceiptData.Value.ToString("yyy-MM-dd");
                            string ReceiptTime = MainModel.GetDateTimeByStamp(receipt.STARTTIME.ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "\r\n" + "至" + "\r\n" + MainModel.GetDateTimeByStamp(receipt.ENDTIME.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                            string Cashier = receipt.CASHIER;
                            string NetOperat = receipt.NETSALEAMT.ToString("f2");

                            string TotalPay = receipt.TOTALPAYMENT.ToString("f2");

                            string TotalCash = receipt.CASHTOTALAMT.ToString("f2"); 

                            string PrintStatus =  "已打印" ;
                            string PosType = "离线模式";

                            dgvReceipt.Rows.Add(ReceiptData, ReceiptTime, Cashier, NetOperat, TotalPay, TotalCash, PrintStatus, PosType, bmpReprint);
                        }
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
                else
                {

               
               // LoadingHelper.ShowLoadingScreen("加载中...");
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
                if (!IsEnable)
                {
                    return;
                }
                if (e.RowIndex < 0)
                    return;
                if (dgvReceipt.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == bmpReprint)
                {

                    if (MainModel.IsOffLine)
                    {
                        IsEnable = false;
                        //LoadingHelper.ShowLoadingScreen("加载中...");
                        DBRECEIPT_BEANMODEL receipt = CurrentListReceiptOffLine[e.RowIndex];

                        Receiptdetail receiptdetail = JsonConvert.DeserializeObject<Receiptdetail>(receipt.RECEIPTDETAIL);
                        string ErrorMsgReceipt = "";
                        bool receiptresult = PrintUtil.ReceiptPrint(receiptdetail, ref ErrorMsgReceipt);

                       // LoadingHelper.CloseForm();

                        if (receiptresult)
                        {
                            MainModel.ShowLog("打印完成", false);
                        }
                        else
                        {
                            MainModel.ShowLog(ErrorMsgReceipt, true);
                        }
                    }
                    else
                    {
                        IsEnable = false;
                        LoadingHelper.ShowLoadingScreen("加载中...");
                        ReceiptQuery receiptquery = CurrentLisstReceipt[e.RowIndex];

                        string ErrorMsgReceipt = "";
                        bool receiptresult = PrintUtil.ReceiptPrint(receiptquery.receiptinfo, ref ErrorMsgReceipt);

                        LoadingHelper.CloseForm();

                        if (receiptresult)
                        {
                            MainModel.ShowLog("打印完成", false);
                        }
                        else
                        {
                            MainModel.ShowLog(ErrorMsgReceipt, true);
                        }
                    }

                  
                   
                }
                dgvReceipt.ClearSelection();
              
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("重打交班单异常"+ex.Message,true);
            }
            finally
            {
                LoadingHelper.CloseForm();
                IsEnable = true;
            }
        }

        public void frmReceiptQuery_SizeChanged(object sender, EventArgs e)
        {
            //asf.ControlAutoSize(this);
        }


        private void frmReceiptQuery_Shown(object sender, EventArgs e)
        {
           
            lblShopName.Text = MainModel.Titledata + "   " + MainModel.CurrentShopInfo.shopname;
            lblMenu.Text = MainModel.CurrentUser.nickname + ",你好";
            picMenu.Left = pnlMenu.Width - picMenu.Width - lblMenu.Width;
            lblMenu.Left = picMenu.Right;
          

            LoadBmp();
            Application.DoEvents();
            btnToday_Click(null,null);
        }

        private void LoadBmp()
        {
            try
            {
                //int height = dgvReceipt.RowTemplate.Height * 55 / 100;
                //bmpReprint = new Bitmap(Resources.ResourcePos.ReprintReceipt, dgvReceipt.Columns["Reprint"].Width * 80 / 100, height);

                bmpReprint = (Bitmap)MainModel.GetControlImage(btnReprintPic);
                bmpWhite = Resources.ResourcePos.White;

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("加载页面图片异常" + ex.Message);
            }
        }

        private void btnWindows_Click(object sender, EventArgs e)
        {
            MainModel.ShowWindows();
        }

        private void dtReceiptData_CloseUp(object sender, EventArgs e)
        {
            if (!IsEnable)
            {
                return;
            }
            IsEnable = false;


           // QueryReceipt();

            LoadDgvReceipt(true);
            IsEnable = true;
        }


        private void dtReceiptData_MouseDown(object sender, MouseEventArgs e)
        {
            dtReceiptData.Focus();
            SendKeys.Send("{F4}");
        }


        #region  分页
        private int CurrentPage = 1;
        private void rbtnPageUp_ButtonClick(object sender, EventArgs e)
        {

            if (!IsEnable || !rbtnPageUp.WhetherEnable)
            {
                return;
            }
            CurrentPage--;
            LoadDgvReceipt(false);
        }

        private void rbtnPageDown_ButtonClick(object sender, EventArgs e)
        {
            if (!IsEnable || !rbtnPageDown.WhetherEnable)
            {
                return;
            }
            CurrentPage++;
            LoadDgvReceipt(false);
        }


        private void LoadDgvReceipt(bool needRefresh)
        {
            try
            {
                if (needRefresh)
                {

               
                
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

                if (ErrorMsg != "" || LstReceiptQuery == null)
                {
                    MainModel.ShowLog(ErrorMsg, false);
                    return;
                }
                }

                if (CurrentLisstReceipt == null)
                {
                    return;
                }

                if (CurrentPage > 1)
                {
                    rbtnPageUp.WhetherEnable = true;
                }
                else
                {
                    rbtnPageUp.WhetherEnable = false;
                }
                int startindex = (CurrentPage - 1) * 6;

                int lastindex = Math.Min(CurrentLisstReceipt.Count - 1, startindex + 5);


                dgvReceipt.Rows.Clear();

                List<ReceiptQuery> LstLoadingReceipt = CurrentLisstReceipt.GetRange(startindex, lastindex - startindex + 1);
                foreach (ReceiptQuery receiptquery in LstLoadingReceipt)
                {

                    string ReceiptData = dtReceiptData.Value.ToString("yyy-MM-dd");
                    string ReceiptTime = MainModel.GetDateTimeByStamp(receiptquery.starttime.ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "\r\n" + "至" + "\r\n" + MainModel.GetDateTimeByStamp(receiptquery.endtime.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    string Cashier = receiptquery.cashier;
                    string NetOperat = receiptquery.netsaleamt.ToString("f2");

                    string TotalPay = receiptquery.totalpayment.ToString("f2");

                    string TotalCash = receiptquery.cashtotalamt.ToString("f2");

                    string PrintStatus = receiptquery.hasprint == 1 ? "已打印" : "未打印";
                    string PosType = receiptquery.hasprint == 1 ? "在线模式" : "离线模式";

                    dgvReceipt.Rows.Add(ReceiptData, ReceiptTime, Cashier, NetOperat, TotalPay, TotalCash, PrintStatus, PosType, bmpReprint);
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
                Application.DoEvents();

                //在线接口每页20个 防止本地分页和接口分页最小积数  例 6*10 = 20*3
                if (CurrentLisstReceipt.Count > CurrentPage * 6)
                {
                    rbtnPageDown.WhetherEnable = true;
                }
                else
                {
                    rbtnPageDown.WhetherEnable = false;
                }


                pnlEmptyReceipt.Visible = dgvReceipt.Rows.Count == 0;

                //rbtnPageDown.Enabled = CurrentQueryOrder.orders.Count > CurrentCartPage * 6;

            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载交班列表异常" + ex.Message, true);
            }
        }
        #endregion
    }
}
