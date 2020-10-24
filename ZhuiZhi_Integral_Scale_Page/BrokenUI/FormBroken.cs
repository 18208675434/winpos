using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.BrokenUI.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.BrokenUI
{
    public partial class FormBroken : Form
    {
        #region
        bool IsEnable = true;

        HttpUtil httputil = new HttpUtil();

        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();

        public FormBroken()
        {
            InitializeComponent();
        }

        private void FormBroken_Load(object sender, EventArgs e)
        {
            try
            {

                lblShopName.Text = MainModel.Titledata + "   " + MainModel.CurrentShopInfo.shopname;
                lblMenu.Text = MainModel.CurrentUser.nickname + ",你好";
                picMenu.Left = pnlMenu.Width - picMenu.Width - lblMenu.Width;
                lblMenu.Left = picMenu.Right;
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载报损页面异常" + ex.Message, true);
            }
        }

        private void FormBroken_Shown(object sender, EventArgs e)
        {

            btnToday_Click(null, null);

            Application.DoEvents();

            //每次进报损页面刷新一次报损类型
            BrokenHelper.GetBrokenType(true);
        }

        private void FormBroken_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        #endregion

        //查询今天
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
                dgvBroken.Rows.Clear();

                //QueryBrokenList();

                CurrentPage = 1;
                LoadDgvBroken(true);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("查询当天报损单信息异常：" + ex.Message);
            }
            finally
            {
                IsEnable = true;
            }
        }

        //查询昨天
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
                dgvBroken.Rows.Clear();
                //QueryBrokenList();

                CurrentPage = 1;
                LoadDgvBroken(true);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("查询昨天报损单信息异常：" + ex.Message);
            }
            finally
            {
                IsEnable = true;
            }
        }

        //查询最近7天
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
                dgvBroken.Rows.Clear();
                //QueryBrokenList();

                CurrentPage = 1;
                LoadDgvBroken(true);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("查询最近一周报损单信息异常：" + ex.Message);
            }
            finally
            {
                IsEnable = true;
            }
        }

        //查询
        private void rbtnQuery_ButtonClick(object sender, EventArgs e)
        {
            try
            {

                if (!IsEnable)
                {
                    return;
                }

                IsEnable = false;
                //btnToday.FlatAppearance.BorderColor = Color.Red;
                //btnYesterday.FlatAppearance.BorderColor = Color.Gray;
                //btnWeek.FlatAppearance.BorderColor = Color.Gray;               
                dgvBroken.Rows.Clear();
                //QueryBrokenList();

                CurrentPage = 1;
                LoadDgvBroken(true);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("查询报损单信息异常：" + ex.Message);
            }
            finally
            {
                IsEnable = true;
            }
        }
        //<summary>
        //按比例缩放页面及控件
        //</summary>
        private static AutoSizeFormUtil asf = new AutoSizeFormUtil();
        //新建报损
        private void rbtnCeate_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                IsEnable = false;
                BrokenHelper.ShowFormBrokenCreate();
                dgvBroken.Rows.Clear();
                //QueryBrokenList();

                CurrentPage = 1;
                LoadDgvBroken(true);
                IsEnable = true;
            }
            catch (Exception ex)
            {
                IsEnable = true;
                LogManager.WriteLog("新建报损页面异常" + ex.Message);
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }


         private void dgvBroken_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }

                if (e.RowIndex < 0)
                    return;

                int id = Convert.ToInt32(dgvBroken.Rows[e.RowIndex].Cells["ID"].Value.ToString());

                BrokenInfo brokeninfo = CurrentPageBroken.rows.Where(r => r.id == id).ToList()[0];
                if (dgvBroken.Columns[e.ColumnIndex].Name == "redop")
                {
                    if (ConfirmHelper.Confirm("提示", "确认红冲此报损单？\r\n确认后将会生成一张商品但金额为负的报损单抵消原单。", true, false))
                    {
                        LoadingHelper.ShowLoadingScreen();
                        string err = "";
                        if (!httputil.CreateredWarehouseotherdeliveryByPos(id + "", ref err))
                        {
                            MainModel.ShowLog(err);
                        }
                        LoadDgvBroken(true);

                    }
                }
                else if (dgvBroken.Columns[e.ColumnIndex].Name== "op")
                {
                    FormBrokenDetail frmdetail = new FormBrokenDetail(brokeninfo);
                    asf.AutoScaleControlTest(frmdetail, 1180, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                    frmdetail.Location = new System.Drawing.Point(0, 0);
                    frmdetail.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("显示报损详情异常" + ex.Message, true);
            }
            finally
            {
                LoadingHelper.CloseForm();
            }
        }

        private void dtStart_MouseDown(object sender, MouseEventArgs e)
        {
            dtStart.Focus();
            SendKeys.Send("{F4}");
        }

        private void dtEnd_MouseDown(object sender, MouseEventArgs e)
        {
            dtEnd.Focus();
            SendKeys.Send("{F4}");
        }




        #region 分页

        private PageBroken CurrentPageBroken = null;

        private int CurrentPage = 1;

        private int PageSize = 5;

        private void LoadDgvBroken(bool needRefresh)
        {
            try
            {
                dgvBroken.Rows.Clear();
                if (CurrentPageBroken == null || needRefresh)
                {
                    LoadingHelper.ShowLoadingScreen();

                    ParaPageBroken para = new ParaPageBroken();

                    para.createdatend = MainModel.getStampByDateTime(dtEnd.Value);
                    para.createdatstart = MainModel.getStampByDateTime(dtStart.Value);
                    para.needdetail = true;
                    para.pagination = false;
                    para.shopid = MainModel.CurrentShopInfo.shopid;
                    para.tenantid = MainModel.CurrentShopInfo.tenantid;
                    //para.page = CurrentPage;
                    //para.size = PageSize;
                    //para.startIndex = 1;

                    string ErrorMsg = "";

                    CurrentPageBroken = httputil.GetPageBroken(para, ref ErrorMsg);


                    if (ErrorMsg != "" || CurrentPageBroken == null)
                    {
                        MainModel.ShowLog(ErrorMsg, false);
                        return;
                    }

                }


                rbtnPageUp.WhetherEnable = CurrentPage > 1;

                int startindex = (CurrentPage - 1) * 6;

                int lastindex = Math.Min(CurrentPageBroken.rows.Count - 1, startindex + 5);

                List<BrokenInfo> lstLoading = CurrentPageBroken.rows.GetRange(startindex, lastindex - startindex + 1);

                foreach (BrokenInfo brokeninfo in lstLoading)
                {
                    string createdat = "    " + MainModel.GetDateTimeByStamp(brokeninfo.createdat).ToString("yyyy-MM-dd HH:mm:ss");
                    string detaildesc = brokeninfo.detaildesc;
                    string skuamount = brokeninfo.skuamount.ToString();
                    string totalamount = brokeninfo.totalamount.ToString("f2");
                    string username = brokeninfo.username;
                    string operation = "详细";
                    string remark = brokeninfo.remark;
                    string status = brokeninfo.redtype ? "已红冲" : "已提交";
                    string redoperation = brokeninfo.redtype ? "" : "红冲";
                    dgvBroken.Rows.Add(createdat, brokeninfo.id, detaildesc, skuamount, totalamount, remark, username, status, redoperation, operation);
                }

                rbtnPageDown.WhetherEnable = CurrentPageBroken.rows.Count > CurrentPage * 6;


            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载报损列表异常" + ex.Message, true);
            }
            finally
            {
                LoadingHelper.CloseForm();
            }
        }
        #endregion

        private void rbtnPageUp_ButtonClick(object sender, EventArgs e)
        {
            if (!IsEnable || !rbtnPageUp.WhetherEnable)
            {
                return;
            }
            CurrentPage--;
            LoadDgvBroken(false);
        }

        private void rbtnPageDown_ButtonClick(object sender, EventArgs e)
        {
            if (!IsEnable || !rbtnPageDown.WhetherEnable)
            {
                return;
            }
            CurrentPage++;
            LoadDgvBroken(false);
        }

      

    }
}
