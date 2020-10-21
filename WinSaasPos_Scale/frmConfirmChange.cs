using Maticsoft.BLL;
using Maticsoft.Model;
using Newtonsoft.Json;
using WinSaasPOS_Scale.Common;
using WinSaasPOS_Scale.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinSaasPOS_Scale
{
    public partial class frmConfirmChange : Form
    {
        private HttpUtil httputil = new HttpUtil();
        /// <summary>
        /// 本地订单表操作类
        /// </summary>
        private DBORDER_BEANBLL orderbll = new DBORDER_BEANBLL();

        private DBRECEIPT_BEANBLL receiptbll = new DBRECEIPT_BEANBLL(); 

        public frmConfirmChange()
        {
            InitializeComponent();
            if (MainModel.IsOffLine)
            {
                lblConfim.Text = "您确认要将收银机切换为在线模式吗？";
            }
            else
            {
                lblConfim.Text = "您确认要将收银机切换为离线模式吗？";
            }

            this.Size = new System.Drawing.Size(Convert.ToInt32(500 * MainModel.wScale), Convert.ToInt32(200 * MainModel.hScale));
            AutoScaleControl();
            this.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - this.Width) / 2, (Screen.AllScreens[0].Bounds.Height - this.Height) / 2);
        }

        private void lbtnCancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void lbtnOK_Click(object sender, EventArgs e)
        {
            try
            {
                LoadingHelper.ShowLoadingScreen("交班中...");
                if (MainModel.IsOffLine)
                {
                    if (OffLineReceipt())
                    {
                        LoadingHelper.CloseForm();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    LoadingHelper.CloseForm();
                }
                else
                {
                    if (OnLineReceipt())
                    {
                        LoadingHelper.CloseForm();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    LoadingHelper.CloseForm();
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("交班异常"+ex.Message,true);
            }
        }


        private bool OnLineReceipt()
        {
            try
            {
                ReceiptPara receiptpara = new ReceiptPara();
                receiptpara.cancelordercount = Convert.ToInt16(INIManager.GetIni("Receipt", "CancelOrderCount", MainModel.IniPath));
                receiptpara.cancelordertotalmoney = INIManager.GetIni("Receipt", "CancelOrderTotalMoney", MainModel.IniPath);
                receiptpara.cancelsinglecount = Convert.ToInt16(INIManager.GetIni("Receipt", "CancelSingleCount", MainModel.IniPath));
                receiptpara.cancelsingletotalmoney = INIManager.GetIni("Receipt", "CancelSingleTotalMoney", MainModel.IniPath);

                receiptpara.openmoneypacketcount = Convert.ToInt16(INIManager.GetIni("Receipt", "OpenMoneyPacketCount", MainModel.IniPath));
                receiptpara.reprintcount = Convert.ToInt16(INIManager.GetIni("Receipt", "ReprintCount", MainModel.IniPath));
                receiptpara.endtime = MainModel.getStampByDateTime(DateTime.Now);
                receiptpara.shopid = MainModel.CurrentShopInfo.shopid;

                string ErrorMsg = "";
                Receiptdetail receipt = httputil.Receipt(receiptpara, ref ErrorMsg);


                if (ErrorMsg != "" || receipt == null) //商品不存在或异常
                {
                    MainModel.ShowLog("交班失败：" + ErrorMsg, true);
                    return false;
                }
                else
                {
                    string ErrorMsgReceipt = "";
                    bool receiptresult = PrintUtil.ReceiptPrint(receipt, ref ErrorMsgReceipt);

                    if (receiptresult)
                    { }
                    else
                    {
                        MainModel.ShowLog("交班打印失败：" + ErrorMsgReceipt, true);
                       
                    }
                    ReceiptUtil.ClearReceipt();

                    INIManager.SetIni("System", "POS-Authorization", "", MainModel.IniPath);
                    MainModel.Authorization = "";

                    FrmConfirmReceiptBack frmconfirmreceiptback = new FrmConfirmReceiptBack(receipt);
                    frmconfirmreceiptback.Location = new Point(0, 0);
                    frmconfirmreceiptback.ShowDialog();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("交班失败："+ex.Message,true);
                return false;
            }
        }


        private bool OffLineReceipt()
        {
            try
            {
                Receiptdetail receiptdetail =  ReceiptUtil.GetReceiptDetailOffLine();
                FrmConfirmReceiptBack frmconfirmreceiptback = new FrmConfirmReceiptBack(receiptdetail);
                frmconfirmreceiptback.Location = new Point(0, 0);
                frmconfirmreceiptback.ShowDialog();
              
                return true;
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("交班失败：" + ex.Message, true);
                return false;
            }
        }



        private void AutoScaleControl()
        {

            try
            {
                foreach (Control c in this.Controls)
                {
                    c.Left = (int)Math.Ceiling(c.Left * MainModel.wScale);
                    c.Top = (int)Math.Ceiling(c.Top * MainModel.hScale);

                    c.Width = (int)Math.Ceiling(c.Width * MainModel.wScale);
                    c.Height = (int)Math.Ceiling(c.Height * MainModel.hScale);

                    float wSize = c.Font.Size * MainModel.wScale;
                    float hSize = c.Font.Size * MainModel.hScale;



                    c.Font = new Font(c.Font.Name, Math.Min(hSize, wSize), c.Font.Style, c.Font.Unit);


                }
            }
            catch
            {

            }

        }
    }
}
