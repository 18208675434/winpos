using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinSaasPOS_Scale.BrokenUI.Model;
using WinSaasPOS_Scale.Common;
using WinSaasPOS_Scale.Model;

namespace WinSaasPOS_Scale.BrokenUI
{
    public partial class FormBroken : Form
    {
        #region 
        bool IsEnable = true;

        HttpUtil httputil = new HttpUtil();

        private List<BrokenInfo> lstBtokenInfo = new List<BrokenInfo>();


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
                btnMenu.Text = MainModel.CurrentUser.nickname + "，你好   ";
                btnMenu.Left = Math.Max(pnlHead.Width - btnMenu.Width - 10, btnCancle.Left + btnCancle.Width);
                lblShopName.Text = MainModel.CurrentShopInfo.shopname;
               lblTime.Text = MainModel.Titledata;
                timerNow.Interval = 1000;
                timerNow.Enabled = true;
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载报损页面异常"+ex.Message,true);
            }
        }

        private void FormBroken_Shown(object sender, EventArgs e)
        {
            btnToday_Click(null,null);
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
                QueryBrokenList();
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
                QueryBrokenList();
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
                QueryBrokenList();
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
                btnToday.FlatAppearance.BorderColor = Color.Red;
                btnYesterday.FlatAppearance.BorderColor = Color.Gray;
                btnWeek.FlatAppearance.BorderColor = Color.Gray;               
                dgvBroken.Rows.Clear();
                QueryBrokenList();
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
                QueryBrokenList();
                IsEnable = true;
            }
            catch (Exception ex)
            {
                IsEnable = true;
                LogManager.WriteLog("新建报损页面异常"+ex.Message);
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timerNow_Tick(object sender, EventArgs e)
        {
           lblTime.Text = MainModel.Titledata;
        }



        private int CurrentPage = 1;

        private void QueryBrokenList()
        {
            try
            {

                LoadingHelper.ShowLoadingScreen();

                LoadingHelper.ShowLoadingScreen();//显示


                ParaPageBroken para = new ParaPageBroken();

                para.createdatend = MainModel.getStampByDateTime(dtEnd.Value);
                para.createdatstart = MainModel.getStampByDateTime(dtStart.Value);
                para.needdetail = true;
                para.pagination=false;
                para.shopid=MainModel.CurrentShopInfo.shopid;
                para.tenantid=MainModel.CurrentShopInfo.tenantid;
                //para.page = CurrentPage;
                //para.size = 20;
                //para.startIndex = 1;
                
                string ErrorMsg = "";

                PageBroken page = httputil.GetPageBroken(para,ref ErrorMsg);


                if (ErrorMsg != "" || page == null)
                {
                    MainModel.ShowLog(ErrorMsg, false);
                }
                else
                {

                    lstBtokenInfo = page.rows;

                    foreach (BrokenInfo  brokeninfo in page.rows)
                    {
                        string createdat = "    "+MainModel.GetDateTimeByStamp(brokeninfo.createdat).ToString("yyyy-MM-dd HH:mm:ss");
                        string detaildesc = brokeninfo.detaildesc;
                        string skuamount = brokeninfo.skuamount.ToString();
                        string totalamount = brokeninfo.totalamount.ToString("f2");
                        string username = brokeninfo.username;
                        string operation = "详细";

                        dgvBroken.Rows.Add(brokeninfo.id, createdat,detaildesc,skuamount,totalamount,username,operation);

                    }
                  
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("查询报损列表数据异常"+ex.Message,true);
            }
            finally
            {
                LoadingHelper.CloseForm();
            }
        }

        private void dgvBroken_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }

                if (e.RowIndex < 0)
                    return; 

                int id =Convert.ToInt32( dgvBroken.Rows[e.RowIndex].Cells[0].Value.ToString());

                BrokenInfo brokeninfo = lstBtokenInfo.Where(r=> r.id==id).ToList()[0];
                if (dgvBroken.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == "详细")
                {
                    FormBrokenDetail frmdetail = new FormBrokenDetail(brokeninfo);
                    asf.AutoScaleControlTest(frmdetail, 1180, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                    frmdetail.Location = new System.Drawing.Point(0, 0);
                    frmdetail.ShowDialog();                  
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("显示报损详情异常"+ex.Message ,true);
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


    }
}
