using Maticsoft.BLL;
using Maticsoft.Model;
using WinSaasPOS_Scale.Common;
using WinSaasPOS_Scale.Model;
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
using WinSaasPOS_Scale.HelperUI;
using WinSaasPOS_Scale.BrokenUI.Model;
using WinSaasPOS_Scale.BrokenUI;

namespace WinSaasPOS_Scale
{
    public partial class FormBrokenDetail : Form
    {

        private HttpUtil httputil = new HttpUtil();

        #region  页面加载

        BrokenInfo brokeninfo = new BrokenInfo();
           public FormBrokenDetail(BrokenInfo  bi)
        {
            InitializeComponent();
            brokeninfo = bi;
        }
        private void frmMain_Shown(object sender, EventArgs e)
        {
            try
            {

                lblSkuAmount.Text = brokeninfo.skuamount.ToString();
                lblBrokenAmount.Text ="￥"+ brokeninfo.totalamount.ToString("f2");
                lblUserName.Text = brokeninfo.username;
                Application.DoEvents();

                LoadingHelper.ShowLoadingScreen();

                ParaPageSku para = new ParaPageSku();
                para.warehouseotherdeliveryid = brokeninfo.id;

                string errormsg = "";
                PageSku page = httputil.GetPageBrokenDetail(para, ref errormsg);


                if (page == null || !string.IsNullOrEmpty(errormsg))
                {
                    MainModel.ShowLog(errormsg,false);
                }
                else
                {
                    if (page.rows != null && page.rows.Count > 0)
                    {
                        foreach (BrokenSku sku in page.rows)
                        {
                            string deliveryprice = sku.deliveryprice == 0 ? "--" : sku.deliveryprice.ToString("f2");
                            dgvGood.Rows.Add("    " + sku.skuname, deliveryprice, sku.deliveryquantity);
                        }
                    }
                }
                LoadingHelper.CloseForm();
                
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("显示报损详细异常"+ex.Message,true);
            }
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


    }
}
