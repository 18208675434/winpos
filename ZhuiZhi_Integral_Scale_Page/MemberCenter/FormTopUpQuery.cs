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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;
using ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model;
using System.IO;
using ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter;

namespace ZhuiZhi_Integral_Scale_UncleFruit
{
    public partial class FormTopUpQuery : Form
    {

        #region 成员变量
        HttpUtil httputil = new HttpUtil();

        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();

        //间隔天数
        private int CurrentInterval = 0;


        private PageBalanceDepositBill CurrentBalanceDepos = null;
        private GetBalanceDepositRefund CurrentRefund = null;
        private PageGetRefund refundre = null;

        private string LastOrderid = "0";


        private Bitmap bmpReprint;
        private Bitmap bmpRefund;
        private Bitmap bmpNotRefund;
        private Bitmap bmpWhite;
        private Bitmap caozuo;
        private Bitmap btncancle;

        /// <summary>
        /// this.enable=false; 页面不可用页面不可控；  通过该标志控制页面是否可用
        /// </summary>
        private bool IsEnable = true;

        /// <summary>
        /// 本地订单表操作类
        /// </summary>
        private DBORDER_BEANBLL orderbll = new DBORDER_BEANBLL();

        private bool thisisoffline = false;

        #endregion


        #region  页面加载与退出
        public FormTopUpQuery()
        {
            InitializeComponent();
            // Application.EnableVisualStyles();
        }
        private void frmOrderQuery_Load(object sender, EventArgs e)
        {

            lblShopName.Text = MainModel.Titledata + "   " + MainModel.CurrentShopInfo.shopname;
            lblMenu.Text = MainModel.CurrentUser.nickname + ",你好";
            picMenu.Left = pnlMenu.Width - picMenu.Width - lblMenu.Width;
            lblMenu.Left = picMenu.Right;

            //Application.DoEvents();



        }
        private void frmOrderQuery_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();

            btnToday_Click(null, null);

            txtPhone.Focus();
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            if (!IsEnable)
            {
                return;
            }
            this.Close();
        }
        #endregion

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
                CurrentInterval = 0;
                LastOrderid = "0";
                dgvOrderOnLine.Rows.Clear();

                CurrentBalanceDepos = null;
                CurrentPage = 1;
                LoadDgvOrder();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("查询当天订单信息异常：" + ex.Message);
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
                btnWeek.FlatAppearance.BorderColor = Color.Gray;

                dtStart.Value = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 00:00:00");
                dtEnd.Value = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 23:59:59");
                CurrentInterval = 1;
                LastOrderid = "0";
                dgvOrderOnLine.Rows.Clear();

                // QueryOrder();
                CurrentPage = 1;
                CurrentBalanceDepos = null;
                LoadDgvOrder();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("查询昨天订单信息异常：" + ex.Message);
            }
            finally
            {
                IsEnable = true;
            }
        }

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
                CurrentInterval = 7;
                LastOrderid = "0";
                dgvOrderOnLine.Rows.Clear();
                // QueryOrder();
                CurrentBalanceDepos = null;
                CurrentPage = 1;
                LoadDgvOrder();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("查询最近一周订单信息异常：" + ex.Message);
            }
            finally
            {
                IsEnable = true;
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (!IsEnable)
            {
                return;
            }

            this.Invoke(new InvokeHandler(delegate()
               {
                   CurrentInterval = 30;
                   LastOrderid = "0";
                   dgvOrderOnLine.Rows.Clear();
                   //QueryOrder();
                   CurrentBalanceDepos = null;
                   CurrentPage = 1;
                   LoadDgvOrder();
               }));
        }


        private bool HaveNextPage = true;



        #region 公用



        //当前时间戳
        private string getStampByDateTime(DateTime datetime)
        {            //DateTime datetime = DateTime.Now;
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            var result = (long)(datetime - startTime).TotalMilliseconds;

            return result.ToString();
        }

        private DateTime GetDateTimeByStamp(string stamp)
        {
            try
            {
                long result = Convert.ToInt64(stamp);
                DateTime datetime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                datetime = datetime.AddMilliseconds(result);
                return datetime;
            }
            catch (Exception ex)
            {
                return DateTime.Now;
            }
        }


        #endregion



        //控制仅允许录入数字
        private void TextNUMBER_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                TextBox txt = sender as TextBox;
                e.Handled = true;
                char ch = e.KeyChar;

                if (ch >= '0' && ch <= '9')
                    e.Handled = false;

                if (ch == (char)Keys.Back)
                    e.Handled = false;

            }
            catch { }
        }

        private void dtEnd_MouseDown(object sender, MouseEventArgs e)
        {
            dtEnd.Focus();
            SendKeys.Send("{F4}");
        }

        private void dtStart_MouseDown(object sender, MouseEventArgs e)
        {
            dtStart.Focus();
            SendKeys.Send("{F4}");
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void lblPhoneShuiyin_Click(object sender, EventArgs e)
        {

            GlobalUtil.OpenOSK();
            Delay.Start(100);
            this.Activate();
            
        }


        private void frmOrderQuery_FormClosed(object sender, FormClosedEventArgs e)
        {
            GlobalUtil.CloseOSK();
        }


        #region  公用

        public void frmOrderQuery_SizeChanged(object sender, EventArgs e)
        {
            // asf.ControlAutoSize(this);
        }


        private void LoadPicScreen(bool isShown)
        {
            try
            {
                if (isShown)
                {
                    picScreen.BackgroundImage = MainModel.GetWinformImage(this);
                    picScreen.Size = new System.Drawing.Size(this.Width, this.Height);
                    picScreen.Visible = true;
                }
                else
                {
                    //picScreen.Size = new System.Drawing.Size(0, 0);
                    picScreen.Visible = false;
                }

                Application.DoEvents();
            }
            catch (Exception ex)
            {
                picScreen.Visible = false;
                LogManager.WriteLog("修改订单查询窗体背景图异常：" + ex.Message);
            }
        }

        private void picScreen_Click(object sender, EventArgs e)
        {
            LoadPicScreen(false);
        }



        #endregion




        #region 分页

        private int CurrentPage = 1;

        private int PageSize = 6;

        public void load()
        {
            try
            {

                //int height = dgvOrderOnLine.RowTemplate.Height * 55 / 100;
                //bmpContinue = new Bitmap(Resources.ResourcePos.Continue, dgvOrderOnLine.Columns["Continue"].Width * 80 / 100, height);

                //bmpDelHang = new Bitmap(Resources.ResourcePos.DelHang, dgvOrderOnLine.Columns["DelHang"].Width * 80 / 100, height);

                caozuo = (Bitmap)MainModel.GetControlImage(btnOperation);




            }
            catch (Exception ex)
            {
                LogManager.WriteLog("加载页面图片异常" + ex.Message);
            }
        }
        public void btn()
        {
            try
            {

                //int height = dgvOrderOnLine.RowTemplate.Height * 55 / 100;
                //bmpContinue = new Bitmap(Resources.ResourcePos.Continue, dgvOrderOnLine.Columns["Continue"].Width * 80 / 100, height);

                //bmpDelHang = new Bitmap(Resources.ResourcePos.DelHang, dgvOrderOnLine.Columns["DelHang"].Width * 80 / 100, height);
                btncan.ForeColor = Color.White;
                btncancle = (Bitmap)MainModel.GetControlImage(btncan);




            }
            catch (Exception ex)
            {
                LogManager.WriteLog("加载页面图片异常" + ex.Message);
            }
        }
        private void LoadDgvOrder()
        {
            try
            {


                LoadingHelper.ShowLoadingScreen();
                IsEnable = false;
                xiadanshijian.Visible = true;
                dingdanhao.Visible = true;
                chongzhijine.Visible = true;
                zengsongjine.Visible = true;
                chongzhifangshi.Visible = true;
                label2.Visible = true;

                tuikuanriqi.Visible = false;
                tuikuandanhao.Visible = false;
                guanliandanhao.Visible = false;
                tuikuanjine.Visible = false;
                tuikuansongjine.Visible = false;
                tuikuanfangshi.Visible = false;
                DepositListRequest para = new DepositListRequest();
                para.id = textBox1.Text;
                txtPhone.Text = MainModel.GetPhone;
                para.starttime = getStampByDateTime(dtStart.Value);
                para.endtime = getStampByDateTime(dtEnd.Value);
                //para.refundable = MainModel.refundquest;
                para.page = CurrentPage;
                para.size = 6;

                string ErrorMsg = "";
                CurrentBalanceDepos = httputil.ListDepositbill(para, ref ErrorMsg);
                



                if (ErrorMsg != "" || CurrentBalanceDepos == null)
                {
                    MainModel.ShowLog(ErrorMsg, false);
                    return;
                }


                dgvOrderOnLine.Rows.Clear();
                rbtnPageUp.WhetherEnable = CurrentPage > 1;






                foreach (RowsItem order in CurrentBalanceDepos.rows)
                {

                    //order.refundable = MainModel.refundquest;
                    if (order.refundable == false)
                    {
                        btn();
                        dgvOrderOnLine.Rows.Add(GetDateTimeByStamp(order.createdat.ToString()).ToString("yyyy-MM-dd HH:mm:ss"), order.id, order.amount.ToString("f2") + "元", order.rewardamount, order.paymodeforapi, btncancle);

                    }
                    else
                    {
                        load();
                        dgvOrderOnLine.Rows.Add(GetDateTimeByStamp(order.createdat.ToString()).ToString("yyyy-MM-dd HH:mm:ss"), order.id, order.amount.ToString("f2") + "元", order.rewardamount, order.paymodeforapi, caozuo);

                    }





                    MainModel.MemberId = order.memberid;
                    MainModel.ShopId = order.shopid;
                    MainModel.Tenantid = order.tenantid;
                    MainModel.Id = order.id;
                }

                dgvOrderOnLine.ClearSelection();
                Application.DoEvents();

                pnlEmptyOrder.Visible = dgvOrderOnLine.Rows.Count == 0;

                rbtnPageUp.WhetherEnable = CurrentPage > 1;

                rbtnPageDown.WhetherEnable = CurrentBalanceDepos.page * CurrentBalanceDepos.size < CurrentBalanceDepos.total;

            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载订单列表异常" + ex.Message, true);
            }
            finally
            {
                IsEnable = true;
                LoadingHelper.CloseForm();
            }
        }
        #endregion

        private void rbtnPageUp_ButtonClick(object sender, EventArgs e)
        {

            if (!rbtnPageUp.WhetherEnable || !IsEnable)
            {
                return;
            }
            CurrentPage--;
            LoadDgvOrder();
        }

        private void rbtnPageDown_ButtonClick(object sender, EventArgs e)
        {
            if (!IsEnable || !rbtnPageDown.WhetherEnable)
            {
                return;
            }
            CurrentPage++;
            LoadDgvOrder();
        }



        private void txt_OskClick(object sender, EventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                GlobalUtil.OpenOSK();


                Delay.Start(100);
                this.Activate();
                txt.Focus();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("焦点打开键盘异常" + ex.Message);
            }
        }
        private static AutoSizeFormUtil asf = new AutoSizeFormUtil();


        private void dgvOrderOnLine_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                //if (MainModel.refundquest != true)
                //{
                //    return;
                //}
                if (!IsEnable)
                {
                    return;
                }
                if (e.RowIndex < 0)
                    return;


                IsEnable = false;

                string numbers = dgvOrderOnLine.Rows[e.RowIndex].Cells["orderid"].Value.ToString();
                string orderate = Convert.ToDateTime(dgvOrderOnLine.Rows[e.RowIndex].Cells["orderat"].Value.ToString()).ToString("yyyyMMddHHmmss") + "-" + numbers + ".order";
                string money = dgvOrderOnLine.Rows[e.RowIndex].Cells["title"].Value.ToString();
                string fang = dgvOrderOnLine.Rows[e.RowIndex].Cells["fangshi"].Value.ToString();
                string give = dgvOrderOnLine.Rows[e.RowIndex].Cells["give"].Value.ToString();
                MainModel.fangshi = fang;
                MainModel.Depostid = numbers;
                MainModel.RechargeAmount = money;
                MainModel.give = give;
                string BasePath = "";
                if (MainModel.IsOffLine)
                {
                    BasePath = MainModel.OffLineOrderPath + "\\" + orderate;
                }
                else
                {
                    BasePath = MainModel.OrderPath + "\\" + orderate;
                }


                BackHelper.ShowFormBackGround();
                //BackHelper.HideFormBackGround();
                FormTuikuan tuikuan = new FormTuikuan();
                asf.AutoScaleControlTest(tuikuan, 420, 560, 420 * MainModel.midScale, 560 * MainModel.midScale, true);
                tuikuan.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - tuikuan.Width) / 2, (Screen.AllScreens[0].Bounds.Height - tuikuan.Height) / 2);
                tuikuan.TopMost = true;
                tuikuan.ShowDialog();
                tuikuan.Dispose();
                tuikuan.Close();



                LoadDgvOrder();



            }
            catch (Exception ex)
            {
                MainModel.ShowLog("退款操作异常！" + ex.Message, true);
            }
            finally
            {
                IsEnable = true;
                LoadPicScreen(false);
            }
        }

        private void czmx_Click(object sender, EventArgs e)
        {
            label5.Text = "充值交易单号";

            czmx.BackColor = Color.White;
            cztkmx.BackColor = Color.FromArgb(200, 200, 200);
            LoadDgvOrder();
        }

        private void cztkmx_Click(object sender, EventArgs e)
        {
            try
            {
                label5.Text = "退款交易单号";

                cztkmx.BackColor = Color.White;
                czmx.BackColor = Color.FromArgb(200, 200, 200);
                tuikuanriqi.Visible = true;
                tuikuandanhao.Visible = true;
                guanliandanhao.Visible = true;
                tuikuanjine.Visible = true;
                tuikuansongjine.Visible = true;
                tuikuanfangshi.Visible = true;

                xiadanshijian.Visible = false;
                dingdanhao.Visible = false;
                chongzhijine.Visible = false;
                zengsongjine.Visible = false;
                chongzhifangshi.Visible = false;
                label2.Visible = false;
                LoadingHelper.ShowLoadingScreen();
                IsEnable = false;
                string ErrorMsg = "";
                
                DepositListRequest para = new DepositListRequest();

                
                para.starttime = getStampByDateTime(dtStart.Value);
                para.endtime = getStampByDateTime(dtEnd.Value);
                para.refundable = MainModel.refundquest;
                para.page = CurrentPage;
                para.size = 6;

                CurrentBalanceDepos = httputil.ListDepositbill(para, ref ErrorMsg);



                dgvOrderOnLine.Rows.Clear();
                rbtnPageUp.WhetherEnable = CurrentPage > 1;
                    foreach (RowsItem order1 in CurrentBalanceDepos.rows)
                    {
                        if (order1.refundable == false)
                        {
                            CurrentRefund = httputil.GetBalancecodepositrefoundbill(MainModel.idm.ToString(), ref ErrorMsg);
                            if (ErrorMsg != "" || CurrentRefund == null)
                            {
                                MainModel.ShowLog(ErrorMsg, false);
                                return;
                            }


                            

                            dgvOrderOnLine.Rows.Add(GetDateTimeByStamp(CurrentRefund.createdat.ToString()).ToString("yyyy-MM-dd HH:mm:ss"), CurrentRefund.id, CurrentRefund.depositbillid, CurrentRefund.capitalrefundamount, CurrentRefund.rewardrefundamount, CurrentRefund.refundtypeforapi);

                        }
                        else
                        {
                            return;
                        }
                    }  
                        
                

                dgvOrderOnLine.ClearSelection();
                Application.DoEvents();

                pnlEmptyOrder.Visible = dgvOrderOnLine.Rows.Count == 0;

                rbtnPageUp.WhetherEnable = CurrentPage > 1;

                //rbtnPageDown.WhetherEnable = CurrentRefund.page * CurrentRefund.size < CurrentRefund.total;

            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载订单列表异常" + ex.Message, true);
            }
            finally
            {
                IsEnable = true;
                LoadingHelper.CloseForm();
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }
    }
}
