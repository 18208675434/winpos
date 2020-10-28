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
    public partial class FormAdjustRecord : Form
    {

        HttpUtil httputil = new HttpUtil();


        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();

        List<ReceiptQuery> CurrentLisstReceipt = new List<ReceiptQuery>();

        List<DBRECEIPT_BEANMODEL> CurrentListReceiptOffLine = new List<DBRECEIPT_BEANMODEL>();


        private long MaxAdjustID = 0;



        /// <summary>
        /// this.enable=false; 页面不可用页面不可控；  通过该标志控制页面是否可用
        /// </summary>
        private bool IsEnable = true;

        private Image bmpNEW;
        private Image bmpWhite;

        private int datetype = 3;

        public FormAdjustRecord()
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
            btnToday.FlatAppearance.BorderColor = Color.Blue;
            btnToday.ForeColor = Color.Blue;
            btnYesterday.FlatAppearance.BorderColor = Color.Black;
            btnYesterday.ForeColor = Color.Black;
            datetype = 0;
            CurrentPage = 1;
            QueryReceipt();

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

                btnToday.FlatAppearance.BorderColor = Color.Black;
                btnToday.ForeColor = Color.Black;
                btnYesterday.FlatAppearance.BorderColor = Color.Blue;
                btnYesterday.ForeColor = Color.Blue;
                datetype = 1;
                CurrentPage = 1;
                QueryReceipt();

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
                IsEnable = false;
                LoadingHelper.ShowLoadingScreen();

                string shopid = MainModel.CurrentShopInfo.shopid;
                string deviceid = MainModel.CurrentShopInfo.deviceid.ToString();

                AdjustPricePara para = new AdjustPricePara();
                para.content = txtSkuCode.Text;
                para.datetype = datetype;
                para.needdetail = true;
                para.pagination = true;
                para.page = CurrentPage;
                para.size = CurrentSize;
                para.shopid = shopid;
                para.tenantid = MainModel.CurrentShopInfo.tenantid;

                string ErrorMsg = "";
                AdjustPriceRecord CurrentAdjustPriceRecord = httputil.GetAdjustPriceRecord(para,ref ErrorMsg);


                LoadingHelper.CloseForm();

                if (ErrorMsg != "" || CurrentAdjustPriceRecord == null)
                {
                    MainModel.ShowLog(ErrorMsg, false);

                    IsEnable = true;
                    LoadingHelper.CloseForm();
                    return;
                }
                else
                {
                    long LastAdjustID = ConfigUtil.GetLastAdjustPriceID();
                    dgvReceipt.Rows.Clear();
                    foreach (AdjustPriceRecordItem item in CurrentAdjustPriceRecord.rows)
                    {

                        MaxAdjustID = Math.Max(MaxAdjustID ,item.id);

                        Bitmap tempbmp;
                        if (item.id > LastAdjustID)
                        {
                            tempbmp = (Bitmap)bmpNEW;
                        }
                        else
                        {
                            tempbmp = Resources.ResourcePos.empty;
                        }

                        dgvReceipt.Rows.Add(tempbmp, MainModel.GetDateTimeByStamp(item.adjustpricedate).ToString("yyyy-MM-dd \r\n HH:mm:ss"), item.skuname + "\r\n" + item.skucode, item.beforesalesprice.ToString(), item.aftersalesprice.ToString(), item.beforememberprice.ToString(), item.aftermemberprice.ToString());
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

                rbtnPageDown.WhetherEnable = CurrentAdjustPriceRecord.total > (CurrentAdjustPriceRecord.page * CurrentAdjustPriceRecord.size);
                rbtnPageUp.WhetherEnable = CurrentAdjustPriceRecord.page > 1;

                IsEnable = true;
                LoadingHelper.CloseForm();
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("查询交班异常：" + ex.Message, true);

                IsEnable = true;
                LoadingHelper.CloseForm();
            }         
        }

        private void frmReceiptQuery_Shown(object sender, EventArgs e)
        {

            MaxAdjustID = ConfigUtil.GetLastAdjustPriceID();

            lblShopName.Text = MainModel.Titledata + "   " + MainModel.CurrentShopInfo.shopname;
            lblMenu.Text = MainModel.CurrentUser.nickname + ",你好";
            picMenu.Left = pnlMenu.Width - picMenu.Width - lblMenu.Width;
            lblMenu.Left = picMenu.Right;
            bmpNEW = MainModel.GetControlImage(lblNEW);
            bmpWhite = Resources.ResourcePos.White;
            Application.DoEvents();
            btnToday_Click(null,null);
            txtSkuCode.Focus();
           

            INIManager.SetIni("MQTT", "AdjustStartTime", MainModel.getStampByDateTime(DateTime.Now), MainModel.IniPath); //记录登录时间作为调价查询的起始时间
        }



        #region  分页
        private int CurrentPage = 1;
        private int CurrentSize = 8;
        private void rbtnPageUp_ButtonClick(object sender, EventArgs e)
        {

            if (!IsEnable || !rbtnPageUp.WhetherEnable)
            {
                return;
            }
            CurrentPage--;
            QueryReceipt();
        }

        private void rbtnPageDown_ButtonClick(object sender, EventArgs e)
        {
            if (!IsEnable || !rbtnPageDown.WhetherEnable)
            {
                return;
            }
            CurrentPage++;
            QueryReceipt();
        }


        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            string errormsg = "";
            AdjustPriceDynamic result = httputil.GetAdjustPriceDynamicForPos(MainModel.getStampByDateTime(DateTime.Now.AddDays(-1)), MainModel.getStampByDateTime(DateTime.Now), true, ref errormsg);

            if (!string.IsNullOrEmpty(errormsg) || result == null)
            {
                MainModel.ShowLog(errormsg,false);
                
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {

            if (!IsEnable)
            {
                return;
            }
            CurrentPage = 1;
            QueryReceipt();
        }

        private void FormAdjustRecord_FormClosing(object sender, FormClosingEventArgs e)
        {
            INIManager.SetIni("MQTT", "LastAdjustPriceID",MaxAdjustID.ToString(), MainModel.IniPath);
        }
    }
}
