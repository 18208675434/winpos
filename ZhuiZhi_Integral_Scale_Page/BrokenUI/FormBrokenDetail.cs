using Maticsoft.BLL;
using Maticsoft.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;
using ZhuiZhi_Integral_Scale_UncleFruit.BrokenUI.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.BrokenUI;

namespace ZhuiZhi_Integral_Scale_UncleFruit
{
    public partial class FormBrokenDetail : Form
    {

        private HttpUtil httputil = new HttpUtil();

        #region  页面加载

        BrokenInfo brokeninfo = new BrokenInfo();
        public FormBrokenDetail(BrokenInfo bi)
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;

            brokeninfo = bi;
        }
        private void frmMain_Shown(object sender, EventArgs e)
        {
            lblShopName.Text = MainModel.Titledata + "   " + MainModel.CurrentShopInfo.shopname;
            lblMenu.Text = MainModel.CurrentUser.nickname + ",你好";
            picMenu.Left = pnlMenu.Width - picMenu.Width - lblMenu.Width;
            lblMenu.Left = picMenu.Right;
            if (!string.IsNullOrEmpty(brokeninfo.redtype))
            {
                lblRedType.Text = brokeninfo.redtype.ToLower() == "true" ? "已红冲" : "被红冲";
            }
            lblId.Text = brokeninfo.id + "";
            lblDate.Text = MainModel.GetDateTimeByStamp(brokeninfo.createdat).ToString("yyyy-MM-dd HH:mm:ss"); 
            lblUserName.Text = brokeninfo.username;


            lblSkuAmount.Text = brokeninfo.skuamount.ToString();
            lblBrokenAmount.Text = "￥" + brokeninfo.totalamount.ToString("f2");


            Application.DoEvents();

            LoadDgvCart(true);
        }
        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        #endregion


        public void btnWindows_Click(object sender, EventArgs e)
        {
            MainModel.ShowTask();
            this.WindowState = FormWindowState.Minimized;
        }





        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }




        #region 分页

        private PageSku CurrentPageSku = null;
        private int CurrentPage = 1;
        private void rbtnPageUp_ButtonClick(object sender, EventArgs e)
        {
            if (!rbtnPageUp.WhetherEnable)
            {
                return;
            }
            CurrentPage--;
            LoadDgvCart(false);
        }

        private void rbtnPageDown_ButtonClick(object sender, EventArgs e)
        {
            if (!rbtnPageDown.WhetherEnable)
            {
                return;
            }
            CurrentPage++;
            LoadDgvCart(false);
        }


        private void LoadDgvCart(bool needRefresh)
        {

            try
            {

                picLoading.Visible = true;

                dgvGood.Rows.Clear();
                if (CurrentPageSku == null || needRefresh)
                {
                    ParaPageSku para = new ParaPageSku();
                    para.warehouseotherdeliveryid = brokeninfo.id;

                    string errormsg = "";
                    CurrentPageSku = httputil.GetPageBrokenDetail(para, ref errormsg);

                    if (!string.IsNullOrEmpty(errormsg))
                    {
                        MainModel.ShowLog(errormsg, true);
                        return;
                    }
                }

                if (CurrentPageSku == null || CurrentPageSku.rows == null)
                {
                    return;
                }

                rbtnPageUp.WhetherEnable = CurrentPage > 1;

                int startindex = (CurrentPage - 1) * 6;

                int lastindex = Math.Min(CurrentPageSku.rows.Count - 1, startindex + 5);

                List<BrokenSku> lstLoadingSku = CurrentPageSku.rows.GetRange(startindex, lastindex - startindex + 1);

                foreach (BrokenSku sku in lstLoadingSku)
                {
                    string deliveryprice = sku.deliveryprice == 0 ? "--" : sku.deliveryprice.ToString("f2");
                    string quantity = sku.deliveryquantity.ToString();
                    if (sku.weightflag)
                    {
                        //quantity += sku.salesunit;
                        quantity += "KG";
                    }
                    dgvGood.Rows.Add("    " + sku.skuname, deliveryprice, quantity, BrokenHelper.GetBrokenTypeName(sku.actiontype));
                }

                rbtnPageDown.WhetherEnable = CurrentPageSku.rows.Count > CurrentPage * 6;

            }
            catch (Exception ex)
            {
                MainModel.ShowLog("显示报损详细异常" + ex.Message, true);
            }
            finally
            {
                picLoading.Visible = false;
            }
        }

        #endregion

    }
}
