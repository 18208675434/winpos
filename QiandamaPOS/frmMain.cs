using Maticsoft.BLL;
using Maticsoft.Model;
using QiandamaPOS.Common;
using QiandamaPOS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading;
using System.Windows.Forms;

namespace QiandamaPOS
{
    public partial class frmMain : Form
    {

        #region 成员变量
        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();

        /// <summary>
        /// 扫描到数据直接扔进来，单独开线程处理
        /// </summary>
        Queue<string> QueueScanCode = new Queue<string>();

        /// <summary>
        /// 扫描到数据直接扔进来，单独开线程处理
        /// </summary>
        Queue<string> QueueShortCode = new Queue<string>();

        /// <summary>
        /// 当前购物车对象
        /// </summary>
        private Cart CurrentCart = new Cart();


        /// <summary>
        /// 接口访问类
        /// </summary>
        HttpUtil httputil = new HttpUtil();

        /// <summary>
        /// USB设备监听
        /// </summary>
        public static ScanerHook listener = new ScanerHook();

        //第三方支付页面
        frmOnLinePayResult frmonlinepayresult = null;

        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();

        ///// <summary>
        ///// 页面宽度缩放比例
        ///// </summary>
        //float wScale = 1;
        ///// <summary>
        ///// 页面高度缩放比例
        ///// </summary>
        //float hScale = 1;

        private Bitmap btnorderhangimage;

        private frmLogin CurrentFrmLogin;

        private bool IsScan = true;
        #endregion


        #region  页面加载
        public frmMain(frmLogin frmlogin)
        {
            InitializeComponent();

            //使用委托的话frmmain界面会卡死
            Control.CheckForIllegalCrossThreadCalls = false;

            MainModel.wScale = (float)Screen.PrimaryScreen.Bounds.Width / this.Width;
            MainModel.hScale = (float)SystemInformation.WorkingArea.Height / this.Height;
            // Control.CheckForIllegalCrossThreadCalls = false;

            LoadingHelper.CloseForm();

            listener.ScanerEvent += Listener_ScanerEvent;
            CurrentFrmLogin = frmlogin;
            // Application.DoEvents();
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            try
            {
                SetBtnPayStarus(false);

                listener.Start();

                timerNow.Interval = 1000;
                timerNow.Enabled = true;

                timerClearMemory.Interval = 2 * 60 * 1000;
                timerClearMemory.Enabled = true;

                lblShopName.Text = MainModel.CurrentShopInfo.shopname;
                dpbtnMenu.Text = MainModel.CurrentUser.nickname + ",你好▼";
                btnMenu.Text = MainModel.CurrentUser.nickname + "，你好▼";

                //UpdateOrderHang();

                // //启动扫描处理线程
                // Thread threadItemExedate = new Thread(ScanCodeExe);
                // threadItemExedate.IsBackground = true;
                // threadItemExedate.Start();
                // //ThreadPool.QueueUserWorkItem(new WaitCallback(ScanCodeExe));

                // //启动扫描处理线程
                // Thread threadShortCodeExedate = new Thread(InputShortCodeExe);
                // threadShortCodeExedate.IsBackground = true;
                // threadShortCodeExedate.Start();
                //// ThreadPool.QueueUserWorkItem(new WaitCallback(InputShortCodeExe));

                //启动全量商品同步线程
                Thread threadLoadAllProduct = new Thread(LoadAllProduct);
                threadLoadAllProduct.IsBackground = true;
                threadLoadAllProduct.Start();

                //启动电子秤同步信息线程
                Thread threadLoadScale = new Thread(LoadScale);
                threadLoadScale.IsBackground = true;
                threadLoadScale.Start();

                timerGetIncrementProduct.Enabled = true;

                btnorderhangimage = new Bitmap(btnOrderHang.Image, 10, 10);
                UpdateOrderHang();

                toolStripMain.Focus();
                ////btnmianban.focus();

                Application.DoEvents();
                //客屏初始化
                MainModel.frmmainmedia = new frmMainMedia();
                if (Screen.AllScreens.Count() != 1)
                {
                    // windowstate设置max 不能再页面设置 否则会显示到第一个屏幕
                    //MainModel.frmmainmedia.Size = new System.Drawing.Size(Screen.AllScreens[1].Bounds.Width, Screen.AllScreens[1].Bounds.Height+20);
                    asf.AutoScaleControlTest(MainModel.frmmainmedia, Screen.AllScreens[1].Bounds.Width, Screen.AllScreens[1].Bounds.Height + 20, true);
                    MainModel.frmmainmedia.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width, -20);

                    //MainModel.frmmainmedia.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    MainModel.frmmainmedia.Show();

                    MainModel.frmmainmedia.IniForm(null);
                    //MainModel.frmmainmedia.LoadMember();
                    //ThreadPool.QueueUserWorkItem(new WaitCallback(MainModel.frmmainmedia.IniForm));
                }
                toolStripMain.Focus();
                ////btnmianban.focus();

                Application.DoEvents();
            }
            catch (Exception ex)
            {
                ShowLog("初始化页面异常"+ex.Message +ex.StackTrace, true);
            }

        }


        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainModel.frmmainmedia.Close();
            MainModel.frmmainmedia = null;
            CurrentFrmLogin.Show();
        }
        private void Listener_ScanerEvent(ScanerHook.ScanerCodes codes)
        {

            //if (this.Enabled == true && IsScan)
            //{

            //    QueueScanCode.Enqueue(codes.Result);
            //}

            //ThreadPool.QueueUserWorkItem(new WaitCallback(ScanCodeThread), codes.Result);

            if (IsScan)
            {

           
            ParameterizedThreadStart Pts = new ParameterizedThreadStart(ScanCodeThread);
            Thread thread = new Thread(Pts);
            thread.IsBackground = true;
            thread.Start(codes.Result);
            }
            else
            {
                LogManager.WriteLog("扫描禁用时间扫描数据："+codes.Result);
            }
        }

        public void frmMain_SizeChanged(object sender, EventArgs e)
        {
            // asf.ControlAutoSize(this);
        }

        //实时时间显示
        private void timerNow_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        //定时清理内存
        private void timerClearMemory_Tick(object sender, EventArgs e)
        {
            try { Other.CrearMemory(); }
            catch { }
        }


        private void CheckUserAndMember(int resultcode, string ErrorMsg)
        {
            try
            {
                this.Invoke(new InvokeHandler(delegate()
                {
                    if (resultcode == MainModel.HttpUserExpired)
                    {
                        LoadingHelper.CloseForm();
                        this.Enabled = false;
                        MainModel.CurrentMember = null;
                        frmUserExpired frmuserexpired = new frmUserExpired();
                        frmuserexpired.Location = new System.Drawing.Point((Screen.PrimaryScreen.Bounds.Width - frmuserexpired.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - frmuserexpired.Height) / 2);
                        frmuserexpired.TopMost = true;
                        frmuserexpired.ShowDialog();
                        this.Enabled = true;

                        INIManager.SetIni("System", "POS-Authorization", "", MainModel.IniPath);

                        CurrentFrmLogin.Show();
                        this.Close();

                    }
                    else if (resultcode == MainModel.HttpMemberExpired)
                    {
                        LoadingHelper.CloseForm();
                        //Application.DoEvents();
                        this.Enabled = false;
                        MainModel.CurrentMember = null;
                        ClearMember();

                        frmDeleteGood frmdelete = new frmDeleteGood("会员登录已过期，请重新登录", "", "");
                        if (frmdelete.ShowDialog() != DialogResult.OK)
                        {
                            this.Enabled = true;
                            return;
                        }
                        this.Enabled = true;
                        ClearForm();



                        btnLoadPhone_Click(null, null);


                    }
                    else
                    {
                        ShowLog(ErrorMsg, false);
                    }
                }));

            }
            catch (Exception ex)
            {
                this.Invoke(new InvokeHandler(delegate()
                {
                    this.Enabled = true;
                }));
                ShowLog("验证用户/会员异常", true);
            }

        }
        #endregion


        #region 菜单按钮



        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            frmDeleteGood frmdelete = new frmDeleteGood("是否确认退出系统？", "", "");
            if (frmdelete.ShowDialog() != DialogResult.OK)
            {
                this.Enabled = true;
                return;
            }
            this.Enabled = true;
            INIManager.SetIni("System", "POS-Authorization", "", MainModel.IniPath);
            CurrentFrmLogin.Show();
            this.Close();
            //System.Environment.Exit(0);
        }

        private void btnOrderQuery_Click(object sender, EventArgs e)
        {
            IsScan = false;
            frmOrderQuery frmorderquery = new frmOrderQuery();
            asf.AutoScaleControlTest(frmorderquery, Screen.PrimaryScreen.Bounds.Width, SystemInformation.WorkingArea.Height, true);
            frmorderquery.Location = new System.Drawing.Point(0, 0);
            btnOrderCancle.Visible = false;
            btnOrderHang.Visible = false;
            btnOrderQuery.Visible = false;
            frmorderquery.ShowDialog();

            IsScan = true;
            btnOrderHang.Visible = true;
            btnOrderQuery.Visible = true;
            if (dgvGood.Rows.Count > 0)
            {
                btnOrderCancle.Visible = true;
            }
        }

        private void btnOrderCancle_Click(object sender, EventArgs e)
        {
            try
            {
                //if (CurrentCart == null || CurrentCart.products == null || CurrentCart.products.Count <= 0)
                //{
                //    return;
                //}

                if (dgvGood.Rows.Count <= 0)
                {
                    return;
                }
                this.Enabled = false;
                frmDeleteGood frmdelete = new frmDeleteGood("是否确认取消订单？", "", "");
                if (frmdelete.ShowDialog() != DialogResult.OK)
                {
                    this.Enabled = true;
                    return;
                }
                // DateTime starttime = DateTime.Now;
                this.Enabled = true;
                // DateTime starttime = DateTime.Now;
                //可能存在网络中断情况桌面还要清空
                try
                {
                    ReceiptUtil.EditCancelOrder(1, CurrentCart.totalpayment);
                }
                catch (Exception ex) { }

                if (MainModel.CurrentMember != null)
                {
                    ClearMember();
                }
               

                ClearForm();
                // Console.WriteLine("清空页面时间"+(DateTime.Now-starttime).TotalMilliseconds);
             
                //Console.WriteLine("清空会员时间" + (DateTime.Now - starttime).TotalMilliseconds);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("取消交易异常" + ex.Message);
            }
        }

        //挂单
        private void btnOrderHang_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnOrderHang.Text == "挂单")
                {
                    if (CurrentCart != null)
                    {
                        this.Enabled = false;
                        frmDeleteGood frmdelete = new frmDeleteGood("确认挂单？", "", "");
                        if (frmdelete.ShowDialog() != DialogResult.OK)
                        {
                            this.Enabled = true;
                            return;
                        }


                        SerializeOrder(CurrentCart);

                        DateTime starttime = DateTime.Now;

                        if (MainModel.CurrentMember != null)
                        {
                            ClearMember();
                        }
                        //Application.DoEvents();
                        this.Enabled = true;
                        ShowLog("已挂单", false);

                        ClearForm();




                    }
                }
                else if (btnOrderHang.Text == "挂单列表")
                {

                    IsScan = false;
                    frmOrderHang frmorderhang = new frmOrderHang();
                    frmorderhang.DataReceiveHandle += FormOrderHang_DataReceiveHandle;
                    asf.AutoScaleControlTest(frmorderhang, Screen.PrimaryScreen.Bounds.Width, this.Height , true);
                    frmorderhang.Location = new System.Drawing.Point(0, 0);
                    frmorderhang.ShowDialog();
                    IsScan = true;
                    UpdateOrderHang();
                }
            }
            catch (Exception ex)
            {
                ShowLog("挂单异常", true);
            }
        }

        /// <summary>
        /// 序列化购物单
        /// </summary>
        /// <param name="order"></param>
        public void SerializeOrder(Cart cart)
        {
            try
            {
                this.BeginInvoke(new InvokeHandler(delegate()
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    string orderpath = "";
                    if (MainModel.CurrentMember != null)
                    {
                        //cartpara.uid = MainModel.CurrentMember.memberheaderresponsevo.memberid;
                        orderpath = MainModel.OrderPath + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + MainModel.CurrentMember.memberheaderresponsevo.mobile + ".order";
                    }
                    else
                    {
                        orderpath = MainModel.OrderPath + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + "" + ".order";
                    }
                    using (Stream output = File.Create(orderpath))
                    {
                        formatter.Serialize(output, cart);
                    }
                }));
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("序列化购物单异常：" + ex.Message);
            }
        }


        //更新挂单按钮  购物车没有商品且有挂单信息时 按钮text="挂单列表"   按钮点击事件根据文本判断事件  更新取消交易
        private void UpdateOrderHang()
        {
            try
            {
                btnOrderHang.Text = "挂单列表";
                btnOrderCancle.Visible = false;
                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                {
                    btnOrderHang.Text = "挂单";
                    btnOrderHang.Image = null;
                    btnOrderCancle.Visible = true;
                }
                else
                {
                    btnOrderHang.Text = "挂单列表";
                    btnOrderCancle.Visible = false;

                    btnOrderHang.Image = null;
                    BinaryFormatter formatter = new BinaryFormatter();
                    DirectoryInfo di = new DirectoryInfo(MainModel.OrderPath);
                    List<FileInfo> fList = di.GetFiles().ToList();
                    for (int i = 0; i < fList.Count; i++)
                    {
                        if (fList[i].Name.Contains(".order"))
                        {
                            btnOrderHang.Image = btnorderhangimage;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("更新挂单按钮异常" + ex.Message);
            }
        }


        private void btnMenu_Click(object sender, EventArgs e)
        {
            frmToolMain frmtool = new frmToolMain();

            frmtool.DataReceiveHandle += frmToolMain_DataReceiveHandle;
            frmtool.Location = new System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width - frmtool.Width - 5, toolStripMain.Height + 10);

            frmtool.Show();
        }

        private void frmToolMain_DataReceiveHandle(ToolType tooltype)
        {
            try
            {
                if (tooltype == ToolType.Receipt)
                {
                    this.Invoke(new InvokeHandler(delegate()
                    {
                        tsmReceipt.PerformClick();
                    }));
                }
                if (tooltype == ToolType.Exit)
                {
                    this.Invoke(new InvokeHandler(delegate()
                    {
                        tsmExit.PerformClick();
                    }));
                }
                if (tooltype == ToolType.Expense)
                {
                    this.Invoke(new InvokeHandler(delegate()
                    {
                        tsmExpense.PerformClick();
                    }));
                }
                if (tooltype == ToolType.PrintSet)
                {
                    this.Invoke(new InvokeHandler(delegate()
                    {
                        tsmPrintSet.PerformClick();
                    }));
                }
                if (tooltype == ToolType.ReceiptQuery)
                {
                    this.Invoke(new InvokeHandler(delegate()
                    {
                        tsmReceiptQuery.PerformClick();
                    }));
                }

                if (tooltype == ToolType.Scale)
                {
                    this.Invoke(new InvokeHandler(delegate()
                    {
                        tsmScale.PerformClick();
                    }));
                }

                if (tooltype == ToolType.ChangeMode)
                {
                    this.Invoke(new InvokeHandler(delegate()
                    {
                        tsmChangeMode.PerformClick();
                    }));
                }


            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ERROR", "菜单按钮异常" + ex.Message);
            }
        }


        //交班
        private void tsmReceipt_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            frmDeleteGood frmdelete = new frmDeleteGood("确认交班", "点击确认后，收银机将自动打印交班表单", "");
            if (frmdelete.ShowDialog() != DialogResult.OK)
            {
                this.Enabled = true;
                return;
            }
            this.Enabled = true;
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
                ShowLog(ErrorMsg, false);
            }
            else
            {
                string ErrorMsgReceipt = "";
                bool receiptresult = PrintUtil.ReceiptPrint(receipt, ref ErrorMsgReceipt);

                if (receiptresult)
                {
                    ReceiptUtil.ClearReceipt();

                    INIManager.SetIni("System", "POS-Authorization", "", MainModel.IniPath);
                    CurrentFrmLogin.Show();
                    this.Close();
                    //System.Environment.Exit(0);
                    //this.Close();
                }
                else
                {
                    ShowLog(ErrorMsgReceipt, true);
                }
            }
        }


        private void tsmReceiptQuery_Click(object sender, EventArgs e)
        {

            IsScan = false;
            frmReceiptQuery frmreceiptquery = new frmReceiptQuery();

            asf.AutoScaleControlTest(frmreceiptquery, Screen.PrimaryScreen.Bounds.Width, SystemInformation.WorkingArea.Height, true);
            frmreceiptquery.Location = new System.Drawing.Point(0, 0);

            frmreceiptquery.ShowDialog();
            IsScan = true;
        }


        private void tsmScale_Click(object sender, EventArgs e)
        {
            IsScan = false;
            frmScale frmscal = new frmScale();

            asf.AutoScaleControlTest(frmscal, Screen.PrimaryScreen.Bounds.Width, SystemInformation.WorkingArea.Height, true);
            frmscal.Location = new System.Drawing.Point(0, 0);

            frmscal.ShowDialog();
            IsScan = true;
        }



        private void tsmExpense_Click(object sender, EventArgs e)
        {
            IsScan = false;
            frmExpense frmexpense = new frmExpense();

            asf.AutoScaleControlTest(frmexpense, Screen.PrimaryScreen.Bounds.Width, SystemInformation.WorkingArea.Height , true);

            frmexpense.Location = new System.Drawing.Point(0, 0);

            frmexpense.ShowDialog();
            IsScan = true;
        }

        private void tsmChangeMode_Click(object sender, EventArgs e)
        {
            IsScan = false;
            frmChangeMode frmchangemode = new frmChangeMode();

            asf.AutoScaleControlTest(frmchangemode, Screen.PrimaryScreen.Bounds.Width, SystemInformation.WorkingArea.Height, true);

            frmchangemode.Location = new System.Drawing.Point(0, 0);

            frmchangemode.ShowDialog();
            IsScan = true;
        }

        #endregion


        #region 购物车
        private void btnLoadBarCode_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                IsScan = false;
                frmNumber frmnumber = new frmNumber("请输入商品条码", false, false);
                asf.AutoScaleControlTest(frmnumber, this.Width * 33 / 100, this.Height * 70 / 100, true);
                frmnumber.TopMost = true;
                frmnumber.Location = new System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width - frmnumber.Width - 40, this.Height * 15 / 100);
                frmnumber.ShowDialog();

                if (frmnumber.DialogResult == DialogResult.OK)
                {
                    string goodscode = frmnumber.NumValue.ToString();
                    if (goodscode.Length == 13)
                    {
                        //ThreadPool.QueueUserWorkItem(new WaitCallback(ScanCodeThread), goodscode);
                        ParameterizedThreadStart Pts = new ParameterizedThreadStart(ScanCodeThread);
                        Thread thread = new Thread(Pts);
                        thread.IsBackground = true;
                        thread.Start(goodscode);
                        // QueueScanCode.Enqueue(goodscode);
                    }
                    else
                    {
                        //ThreadPool.QueueUserWorkItem(new WaitCallback(InputShortCodeThread), goodscode);
                        ParameterizedThreadStart Pts = new ParameterizedThreadStart(InputShortCodeThread);
                        Thread thread = new Thread(Pts);
                        thread.IsBackground = true;
                        thread.Start(goodscode);
                        // QueueShortCode.Enqueue(goodscode);
                    }
                }
                else
                {
                    IsScan = true;
                    this.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                IsScan = true;
                this.Enabled = true;
                LogManager.WriteLog("输入商品条码异常" + ex.Message);
            }
        }


        private bool RefreshCart()
        {
            try
            {

                DateTime starttime = DateTime.Now;
                string ErrorMsgCart = "";
                int ResultCode = 0;
                //btnmianban.focus();
                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                {
                    // pnlPayType1.Enabled = true;
                    SetBtnPayStarus(true);
                    pnlPayType2.Enabled = true;
                    //CurrentCart.pointpayoption = 1;

                    CurrentCart.pointpayoption = btnCheckJF.Text == "√" ? 1 : 0;

                    Cart cart = httputil.RefreshCart(CurrentCart, ref ErrorMsgCart, ref ResultCode);
                    Console.WriteLine("购物车访问时间" + (DateTime.Now - starttime).TotalMilliseconds);

                    if (ErrorMsgCart != "" || cart == null) //商品不存在或异常
                    {
                        CheckUserAndMember(ResultCode, ErrorMsgCart);
                        return false;
                    }
                    else
                    {
                        CurrentCart = cart;
                        UploadDgvGoods(cart);
                        Console.WriteLine("表格加载时间" + (DateTime.Now - starttime).TotalMilliseconds);

                        if (btnCheckJF.Text == "√")
                        {
                            if (MainModel.CurrentMember != null && CurrentCart != null && CurrentCart.pointinfo != null)
                            {
                                lblJF.Text = CurrentCart.pointinfo.totalpoints;
                                //lblJFUse.Visible = true;
                                lblJFUse.Text = "使用" + CurrentCart.pointinfo.availablepoints + "积分 抵用" + CurrentCart.pointinfo.availablepointsamount + "元";
                                panel2.Visible = true;
                            }
                        }
                        else
                        {
                            lblJFUse.Text = "";
                            panel2.Visible = false;
                        }

                        btnPayByBalance.Enabled = CurrentCart.paymenttypes.balancepayenabled == 1;
                        btnPayByCash.Enabled = CurrentCart.paymenttypes.cashenabled == 1;
                        btnPayOnLine.Enabled = CurrentCart.paymenttypes.onlineenabled == 1;

                        return true;
                    }
                }
                else
                {
                    SetBtnPayStarus(false);
                    pnlPayType2.Enabled = false;

                    ClearForm();
                    return true;
                }

            }
            catch (Exception ex)
            {
                ShowLog("刷新购物车异常：" + ex.Message, true);
                return false;
            }
        }

        private object thislockScanCode = new object();
        private void ScanCodeThread(object obj)
        {
            lock (thislockScanCode)
            {

           
            try
            {
                LoadingHelper.ShowLoadingScreen();//显示
                string goodcode = obj.ToString();
                IsScan = false;
                LogManager.WriteLog(goodcode);
                string ErrorMsg = "";
                int ResultCode = 0;
                scancodememberModel scancodemember = httputil.GetSkuInfoMember(goodcode, ref ErrorMsg, ref ResultCode);

                if (ErrorMsg != "" || scancodemember == null) //商品不存在或异常
                {
                    CheckUserAndMember(ResultCode, ErrorMsg);
                    //ShowLog(ErrorMsg, false);
                }
                else
                {
                    if (scancodemember.type == "MEMBER")
                    {
                        this.Invoke(new InvokeHandler(delegate()
                        {
                            LoadMember(scancodemember.memberresponsevo);
                        }));
                    }
                    else
                    {
                        addcart(scancodemember);
                    }
                }

                IsScan = true;
                LoadingHelper.CloseForm();//关闭
                Thread.Sleep(1);
            }
            catch (Exception ex)
            {
                // listener.Start();
                LoadingHelper.CloseForm();//关闭
                LogManager.WriteLog("ERROR", "扫描数据处理异常：" + ex.Message);
            }
            finally
            {

                IsScan = true;
                Application.DoEvents();
                LoadingHelper.CloseForm();//关闭
            }

            }
        }

        private object thislockShortCode = new object();
        private void InputShortCodeThread(object obj)
        {
            lock (thislockShortCode)
            {

            
            try
            {
                LoadingHelper.ShowLoadingScreen();//显示

                string goodcode = obj.ToString();

                string ErrorMsg = "";
                int ResultCode = 0;

                DBPRODUCT_BEANMODEL dbpro = productbll.GetModelByGoodsID(goodcode.PadLeft(5,'0'));
                if (dbpro == null)
                {
                    scancodememberModel scancodemember = httputil.GetSkuInfoByShortCode(goodcode, ref ErrorMsg, ref ResultCode);

                    if (ErrorMsg != "" || scancodemember == null) //商品不存在或异常
                    {
                        CheckUserAndMember(ResultCode, ErrorMsg);
                        //ShowLog(ErrorMsg, false);
                    }
                    else
                    {
                        addcart(scancodemember);
                    }
                }
                else
                {
                    scancodememberModel scancodemember = new scancodememberModel();
                    scancodemember.scancodedto = new Scancodedto();
                    scancodemember.memberresponsevo = new Member();


                    scancodemember.scancodedto.skucode = dbpro.SKUCODE;
                    scancodemember.scancodedto.num =(int) dbpro.NUM;
                    scancodemember.scancodedto.specnum = dbpro.SPECNUM;
                    scancodemember.scancodedto.spectype=dbpro.SPECTYPE;
                    scancodemember.scancodedto.weightflag = dbpro.WEIGHTFLAG==1 ? true : false;

                    scancodemember.scancodedto.barcode = dbpro.BARCODE;
                    //3443
                    addcart(scancodemember);
                }

               

                //LoadingHelper.CloseForm();//关闭
                IsScan = true;
                this.Enabled = true;
            }
            catch (Exception ex)
            {
                IsScan = true;
                // LoadingHelper.CloseForm();//关闭
                LogManager.WriteLog("ERROR", "扫描数据处理异常：" + ex.Message);
            }
            finally
            {
                LoadingHelper.CloseForm();//关闭
            }
            }
        }


        private void ScanCodeExe(object obj)
        {
            while (true)
            {

                //有数据就处理
                if (QueueScanCode.Count > 0)
                {
                    try
                    {
                        LoadingHelper.ShowLoadingScreen();//显示
                        string goodcode = QueueScanCode.Dequeue();
                        IsScan = false;
                        LogManager.WriteLog(goodcode);
                        string ErrorMsg = "";
                        int ResultCode = 0;
                        scancodememberModel scancodemember = httputil.GetSkuInfoMember(goodcode, ref ErrorMsg, ref ResultCode);

                        //if (true) //商品不存在或异常
                        //{
                        //    CheckUserAndMember(MainModel.HttpMemberExpired);
                        //    ShowLog(ErrorMsg, false);
                        //}

                        if (ErrorMsg != "" || scancodemember == null) //商品不存在或异常
                        {
                            CheckUserAndMember(ResultCode, ErrorMsg);
                            //ShowLog(ErrorMsg, false);
                        }
                        else
                        {
                            if (scancodemember.type == "MEMBER")
                            {
                                this.Invoke(new InvokeHandler(delegate()
                                {
                                    LoadMember(scancodemember.memberresponsevo);
                                }));
                            }
                            else
                            {

                                addcart(scancodemember);
                            }
                        }

                        IsScan = true;
                        LoadingHelper.CloseForm();//关闭
                        Thread.Sleep(1);
                    }
                    catch (Exception ex)
                    {
                        // listener.Start();
                        LoadingHelper.CloseForm();//关闭
                        LogManager.WriteLog("ERROR", "扫描数据处理异常：" + ex.Message);
                    }
                    finally
                    {

                        IsScan = true;
                        Application.DoEvents();
                        LoadingHelper.CloseForm();//关闭
                    }
                }
                else
                {
                    Thread.Sleep(1);
                }

            }
        }

        private void InputShortCodeExe(object obj)
        {
            while (true)
            {

                //有数据就处理
                if (QueueShortCode.Count > 0)
                {

                    try
                    {


                        LoadingHelper.ShowLoadingScreen();//显示

                        string goodcode = QueueShortCode.Dequeue();

                        string ErrorMsg = "";
                        int ResultCode = 0;
                        scancodememberModel scancodemember = httputil.GetSkuInfoByShortCode(goodcode, ref ErrorMsg, ref ResultCode);


                        //if (true) //商品不存在或异常
                        //{
                        //    CheckUserAndMember(MainModel.HttpMemberExpired, ErrorMsg);
                        //    //ShowLog(ErrorMsg, false);
                        //}

                        if (ErrorMsg != "" || scancodemember == null) //商品不存在或异常
                        {
                            CheckUserAndMember(ResultCode, ErrorMsg);
                            //ShowLog(ErrorMsg, false);
                        }
                        else
                        {
                            addcart(scancodemember);
                        }

                        //LoadingHelper.CloseForm();//关闭

                    }
                    catch (Exception ex)
                    {
                        // LoadingHelper.CloseForm();//关闭
                        LogManager.WriteLog("ERROR", "扫描数据处理异常：" + ex.Message);
                    }
                    finally
                    {
                        LoadingHelper.CloseForm();//关闭
                    }
                }
                else
                {
                    Thread.Sleep(1);
                }
            }
        }

        private void addcart(scancodememberModel scancodemember)
        {


            //this.Invoke(new InvokeHandler(delegate()
            //{
                try
                {

                    if (scancodemember.scancodedto.weightflag && scancodemember.scancodedto.specnum == 0)
                    {
                        this.Enabled = false;
                        IsScan =false;
                        frmNumber frmnumber = new frmNumber(scancodemember.scancodedto.skuname, false, true);
                        asf.AutoScaleControlTest(frmnumber, this.Width * 33 / 100, this.Height * 70 / 100, true);
                        frmnumber.TopMost = true;
                      
                        frmnumber.Location = new System.Drawing.Point((Screen.PrimaryScreen.Bounds.Width - frmnumber.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - frmnumber.Height) / 2);
                        frmnumber.ShowDialog();

                        if (frmnumber.DialogResult == DialogResult.OK)
                        {
                            scancodemember.scancodedto.specnum = (decimal)frmnumber.NumValue / 1000;
                            scancodemember.scancodedto.num = 1;
                        }
                        else
                        {
                            IsScan = true;
                            this.Enabled = true;
                            Application.DoEvents();
                            return;
                        }
                        IsScan = true;
                        this.Enabled = true;
                        Application.DoEvents();
                    }

                    Product pro = new Product();
                    pro.skucode = scancodemember.scancodedto.skucode;
                    pro.num = scancodemember.scancodedto.num;
                    pro.specnum = scancodemember.scancodedto.specnum;
                    pro.spectype = scancodemember.scancodedto.spectype;
                    pro.goodstagid = scancodemember.scancodedto.weightflag == true ? 1 : 0;

                    pro.barcode = scancodemember.scancodedto.barcode;

                    if (CurrentCart == null)
                    {
                        CurrentCart = new Cart();
                    }
                    if (CurrentCart.products == null)
                    {
                        List<Product> products = new List<Product>();
                        products.Add(pro);
                        CurrentCart.products = products;
                    }
                    else
                    {
                        CurrentCart.products.Add(pro);
                    }

                    string ErrorMsgCart = "";
                    int ResultCode = 0;
                    bool IsExits = false;
                    DateTime starttime = DateTime.Now;
                    RefreshCart();
                    Console.WriteLine("刷新购物车时间：" + (DateTime.Now - starttime).TotalMilliseconds);


                    this.Enabled = true;
                    this.Enabled = true;
                }
                catch (Exception ex)
                {
                    this.Enabled = true;
                    LogManager.WriteLog("ERROR", "添加购物车商品异常:" + ex.Message);
                }
                finally
                {
                    IsScan = true;
                    this.Enabled = true;
                }

           // }));
        }



        //TODO  修改样式
        private void ShowLog(string msg, bool iserror)
        {
            try
            {
                this.BeginInvoke(new InvokeHandler(delegate()
                {
                    MainModel.ShowLog(msg, iserror);
                }));
            }
            catch (Exception ex)
            {

            }
        }


        private void UploadDgvGoods(Cart cart)
        {
            try
            {
                //this.Invoke(new InvokeHandler(delegate()
                //{

                    DateTime starttime = DateTime.Now;
                    dgvOrderDetail.Rows.Clear();
                    dgvGood.Rows.Clear();
                    pnlPayType1.Visible = true;
                    pnlPayType2.Visible = false;
                    //Console.WriteLine("清空时间"+(DateTime.Now-starttime).TotalMilliseconds);
                    if (cart != null && cart.products != null && cart.products.Count > 0)
                    {
                        int orderCount = cart.orderpricedetails.Length;
                        if (orderCount == 0)
                        {

                        }
                        else
                        {
                            for (int i = 0; i < orderCount; i++)
                            {
                                dgvOrderDetail.Rows.Add(cart.orderpricedetails[i].title, cart.orderpricedetails[i].amount);
                            }
                            dgvOrderDetail.ClearSelection();
                        }

                        if (MainModel.CurrentMember != null)
                        {
                            if (CurrentCart.memberpromo != null && CurrentCart.memberpromo > 0)
                            {
                                lblMemberPromo.Text = "会员已优惠:￥" + CurrentCart.memberpromo.ToString("f2");

                                pnlMemberPromo.Visible = true;
                            }

                        }
                        else
                        {
                            if (CurrentCart.memberpromo != null && CurrentCart.memberpromo > 0)
                            {
                                lblMemberPromo.Text = "会员可优惠:￥" + CurrentCart.memberpromo.ToString("f2");
                                pnlMemberPromo.Visible = true;
                            }
                        }

                        lblPrice.Text = "￥" + cart.totalpayment.ToString("f2");

                        int count = cart.products.Count;
                        lblGoodsCount.Text = "(" + count.ToString() + "件商品)";
                        if (count == 0)
                        {
                            pnlWaiting.Show();
                            //picWaiting.Visible = true;
                            //lblWaiting.Visible = true;
                        }
                        else
                        {
                            pnlWaiting.Visible = false;
                            for (int i = 0; i < count; i++)
                            {

                                Product pro = cart.products[i];

                                string barcode = "\r\n  " + pro.title + "\r\n  " + pro.skucode;
                                string price = "";
                                string jian = "";

                                string num = "";
                                string add = "";
                                string total = "";
                                switch (pro.pricetagid)
                                {
                                    case 1: barcode = "1" + pro.pricetag + "\r\n  " + pro.title + "\r\n  " + pro.skucode; break;
                                    case 2: barcode = "2" + pro.pricetag + "\r\n  " + pro.title + "\r\n  " + pro.skucode; break;
                                    case 3: barcode = "3" + pro.pricetag + "\r\n  " + pro.title + "\r\n  " + pro.skucode; break;
                                    case 4: barcode = "4" + pro.pricetag + "\r\n  " + pro.title + "\r\n  " + pro.skucode; break;
                                }

                                if (pro.price.saleprice == pro.price.originprice)
                                {
                                    price = pro.price.saleprice.ToString("f2");
                                }
                                else
                                {
                                    //price = "￥" + pro.price.saleprice.ToString() + "("+pro.price.salepricedesc+")" + "\r\n" + "￥" + pro.price.originprice + "("+pro.price.originpricedesc+")";

                                    price = pro.price.saleprice.ToString("f2");
                                    if (!string.IsNullOrEmpty(pro.price.salepricedesc))
                                    {
                                        price += "(" + pro.price.salepricedesc + ")";
                                    }

                                    if (pro.price.strikeout == 1)
                                    {
                                        price += "\r\n" + "strikeout" + pro.price.originprice.ToString("f2");
                                    }
                                    else
                                    {
                                        price += "\r\n" + pro.price.originprice.ToString("f2");
                                    }

                                    if (!string.IsNullOrEmpty(pro.price.originpricedesc))
                                    {
                                        price += "(" + pro.price.originpricedesc + ")";
                                    }
                                }

                                if (pro.goodstagid == 0)  //0是标品  1是称重
                                {
                                    add = "+";
                                    jian = "-";
                                    num = pro.num.ToString();
                                }
                                else
                                {
                                    add = "";
                                    jian = "";
                                    num = pro.price.specnum + pro.price.unit;
                                }

                                if (pro.price.total == pro.price.origintotal)
                                {
                                    total = pro.price.total.ToString("f2");
                                }
                                else
                                {
                                    //total = "￥" + pro.price.total.ToString() + "("+pro.price.salepricedesc+")" + "\r\n" + "￥" + pro.price.origintotal + "("+pro.price.originpricedesc+")";

                                    total = pro.price.total.ToString("f2");

                                    if (!string.IsNullOrEmpty(pro.price.salepricedesc))
                                    {
                                        total += "(" + pro.price.salepricedesc + ")";
                                    }
                                    total += "\r\n" + pro.price.origintotal.ToString("f2");
                                    if (!string.IsNullOrEmpty(pro.price.originpricedesc))
                                    {
                                        total += "(" + pro.price.originpricedesc + ")";
                                    }


                                }
                                //Console.WriteLine("增加数据前" + (DateTime.Now - starttime).TotalMilliseconds);
                                //isCellPainting = true;
                                dgvGood.Rows.Insert(0, new object[] { barcode, price, jian, num, add, total });

                                //Console.WriteLine("增加数据后" + (DateTime.Now - starttime).TotalMilliseconds);
                                //Delay.Start(50);
                                //isCellPainting = false;
                            }
                            Thread threadItemExedate = new Thread(ShowDgv);
                            threadItemExedate.IsBackground = true;
                            threadItemExedate.Start();

                            dgvGood.ClearSelection();
                            Application.DoEvents();
                            if (cart.pointpayoption == 1 && cart.totalpayment == 0)
                            {
                                pnlPayType1.Visible = false;
                                pnlPayType2.Visible = true;
                            }

                        }
                    }
                    else
                    {
                        SetBtnPayStarus(false);
                        pnlPayType2.Enabled = false;
                    }
                    //Console.WriteLine("积分前" + (DateTime.Now - starttime).TotalMilliseconds);
                    CurrentCart = cart;
                    if (MainModel.CurrentMember != null && btnCheckJF.Text == "√" && CurrentCart.pointinfo != null)
                    {
                        lblJFUse.Text = "使用" + CurrentCart.pointinfo.availablepoints + "积分 抵用" + CurrentCart.pointinfo.availablepointsamount + "元";
                        panel2.Visible = true;

                    }

                    if (CurrentCart.availablecoupons != null && CurrentCart.availablecoupons.Length > 0 && pnlPayType2.Visible == false)
                    {
                        lblCoupon.Visible = true;
                        lblCouponStr.Visible = true;

                        if (CurrentCart.couponpromoamt > 0)
                        {
                            lblCoupon.Text = "-￥" + CurrentCart.couponpromoamt + ">";

                        }
                        else
                        {
                            lblCoupon.Text = CurrentCart.availablecoupons.Length + "张可用>";
                        }
                    }
                    else
                    {
                        lblCoupon.Visible = false;
                        lblCouponStr.Visible = false;
                    }
                    //Console.WriteLine("优惠券后" + (DateTime.Now - starttime).TotalMilliseconds);
                    UpdateOrderHang();
               // }));
                //Console.WriteLine("hang" + (DateTime.Now - starttime).TotalMilliseconds);
                //btnmianban.focus();
                // Application.DoEvents();

                MainModel.frmMainmediaCart = CurrentCart;
                MainModel.frmmainmedia.UpdateForm();

                // Application.DoEvents();

            }
            catch (Exception ex)
            {
                ShowLog("更新显示列表异常" + ex.Message+ex.StackTrace, false);
            }
        }


        private void ClearForm()
        {
            try
            {
                CurrentCart = new Cart();

                isCellPainting = false;
                dgvGood.Rows.Clear();

                Application.DoEvents();
                isCellPainting = true;

                lblPrice.Text = "￥" + "0.00";
                lblGoodsCount.Text = "(0件商品)";

                pnlMemberPromo.Visible = false;

                pnlWaiting.Show();

                //Application.DoEvents();

                dgvOrderDetail.Rows.Clear();


                // pnlPayType1.Enabled = false;
                SetBtnPayStarus(false);
                pnlPayType2.Enabled = false;

                UpdateOrderHang();

                // MainModel.frmmainmedia.IniForm();
                Application.DoEvents();

                MainModel.frmmainmedia.IniForm(null);
                //ThreadPool.QueueUserWorkItem(new WaitCallback(MainModel.frmmainmedia.IniForm));
            }
            catch (Exception ex)
            {

            }
        }

        #endregion


        #region  会员积分优惠券


        private void btnLoadPhone_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                // MainModel.frmmainmedia.ShowNumber();
                frmNumber frmnumber = new frmNumber("请输入会员号", false, false);
                asf.AutoScaleControlTest(frmnumber, this.Width * 33 / 100, this.Height * 70 / 100, true);
                frmnumber.TopMost = true;
                frmnumber.DataReceiveHandle += FormPhone_DataReceiveHandle;
                //frmnumber.Opacity = 0.95d;
                //frmnumber.frmNumber_SizeChanged(null, null);
                //frmnumber.Size = new System.Drawing.Size(this.Width * 33 / 100, this.Height * 70 / 100);
                frmnumber.Location = new System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width - frmnumber.Width - 40, this.Height * 15 / 100);
                // frmnumber.TopMost = true;
                frmnumber.ShowDialog();

                //RefreshCart();
                //Application.DoEvents();
                this.Enabled = true;
                //btnmianban.focus();
            }
            catch (Exception ex)
            {
                this.Enabled = true;
                LogManager.WriteLog("会员登录异常" + ex.Message);
            }

        }


        private void FormPhone_DataReceiveHandle(int type, string goodscode)
        {
            try
            {
                if (type == 0)
                {

                }
                else if (type == 1)
                {
                    this.Invoke(new InvokeHandler(delegate()
                    {

                        string ErrorMsgMember = "";
                        Member member = httputil.GetMember(goodscode, ref ErrorMsgMember);

                        if (ErrorMsgMember != "" || member == null) //会员不存在
                        {
                            //pnlWaitingMember.Visible = true;
                            //pnlMember.Visible = false;
                            //MainModel.CurrentMember = null;
                            ShowLog(ErrorMsgMember, false);

                            if (MainModel.CurrentMember != null)
                            {
                                ClearMember();
                            }
                        }
                        else
                        {
                            LoadMember(member);
                        }
                    }));
                }

                // RefreshCart();
            }
            catch (Exception ex)
            {
                //LoadingHelper.CloseForm();//关闭
                LogManager.WriteLog("ERROR", "会员号处理异常：" + ex.Message);
            }
            finally
            {
                this.Invoke(new InvokeHandler(delegate()
                {
                    this.Enabled = true;
                    LoadingHelper.CloseForm();//关闭
                }));

            }

        }


        private void LoadMember(Member member)
        {
            try
            {
                this.Invoke(new InvokeHandler(delegate()
                {
                    this.Enabled = false;

                    pnlWaitingMember.Visible = false;
                    pnlMember.Visible = true;
                    //this.Enabled = true;
                    lblMobil.Text = member.memberheaderresponsevo.mobile;
                    lblWechartNickName.Text = member.memberinformationresponsevo.wechatnickname;

                    MainModel.CurrentMember = member;

                    lblJF.Text = member.creditaccountrepvo.availablecredit.ToString();
                    //chkJF.Checked = false;
                    btnCheckJF.Text = "";
                    lblCoupon.Visible = false;
                    lblCouponStr.Visible = false;

                    if (member.memberinformationresponsevo.onbirthday)
                    {
                        picBirthday.Visible = true;
                    }
                    else
                    {
                        picBirthday.Visible = false;
                    }
                    //RefreshCart();
                    //购物车有商品的话刷新一次
                    if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                    {
                        RefreshCart();
                    }
                    this.Enabled = true;
                    //btnmianban.focus();
                    Application.DoEvents();

                    MainModel.frmmainmedia.LoadMember();

                }));
            }
            catch (Exception ex)
            {
                this.Enabled = true;
                ShowLog("加载会员信息异常：" + ex.Message, true);
            }

        }

        //退出会员
        private void lblExitMember_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            frmDeleteGood frmdelete = new frmDeleteGood("是否确认退出会员？", "", "");
            if (frmdelete.ShowDialog() != DialogResult.OK)
            {
                this.Enabled = true;
                return;
            }
            this.Enabled = true;
            ClearMember();
            RefreshCart();

        }

        //清空会员信息
        private void ClearMember()
        {
            try
            {
                this.Invoke(new InvokeHandler(delegate()
                {
                    lblJF.Text = "0";
                    lblJFUse.Text = "";
                    panel2.Visible = false;
                    MainModel.CurrentMember = null;
                    MainModel.CurrentCouponCode = "";
                    //chkJF.Checked = false;
                    btnCheckJF.Text = "";
                    pnlWaitingMember.Visible = true;
                    pnlMember.Visible = false;
                    picBirthday.Visible = false;


                    Application.DoEvents();
                    MainModel.frmmainmedia.LoadMember();

                    // RefreshCart();
                }));
            }
            catch (Exception ex)
            {

            }
        }

        private void btnCheckJF_Click(object sender, EventArgs e)
        {

            try
            {

                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                {
                    if (btnCheckJF.Text == "√")
                    {
                        btnCheckJF.Text = "";
                    }
                    else
                    {
                        btnCheckJF.Text = "√";
                    }

                    RefreshCart();
                }
                else
                {
                    //chkJF.Checked = false;
                    btnCheckJF.Text = "";
                }

            }
            catch (Exception ex)
            {
                ShowLog("积分处理异常" + ex.Message, true);
            }
        }



        //选择优惠券
        private void lblCoupon_Click(object sender, EventArgs e)
        {


            this.Enabled = false;
            frmCoupon frmcoupon = new frmCoupon(CurrentCart, MainModel.CurrentCouponCode);
            //frmcoupon.Opacity = 0.95d;

            frmcoupon.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            frmcoupon.ShowDialog();
            this.Enabled = true;
            //if (frmcoupon.DialogResult == DialogResult.OK)
            //{
            //    string[] couponcodes= new string[1];
            //    couponcodes[0] = frmcoupon.SelectCouponCode;

            //}


            MainModel.CurrentCouponCode = frmcoupon.SelectCouponCode;
            bool RefreshCartOK = RefreshCart();


            //收银完成
            if (frmcoupon.DialogResult == DialogResult.Yes && RefreshCartOK)
            {
                string ErrorMsg = "";
                int ResultCode = 0;
                CreateOrderResult orderresult = httputil.CreateOrder(CurrentCart, ref ErrorMsg, ref ResultCode);
                if (ResultCode != 0 || orderresult == null)
                {
                    CheckUserAndMember(ResultCode, ErrorMsg);
                    //ShowLog("异常" + ErrorMsg, true);
                }
                else if (orderresult.continuepay == 1)
                {
                    ShowLog("需要继续支付", true);
                }
                else
                {
                    frmCashierResult frmresult = new frmCashierResult(orderresult.orderid);
                    frmresult.DataReceiveHandle += FormCashierResult_DataReceiveHandle;
                    frmresult.ShowDialog();
                    ClearForm();
                    ClearMember();
                }
            }

        }
        #endregion


        #region 页面委托时间

        private void FormOnLinePayResult_DataReceiveHandle(int type, string orderid)
        {
            try
            {
                this.Invoke(new InvokeHandler(delegate()
                {
                    if (type == 1) //交易完成
                    {
                        this.Invoke(new InvokeHandler(delegate()
                        {

                            frmCashierResult frmresult = new frmCashierResult(orderid);
                            frmresult.DataReceiveHandle += FormCashierResult_DataReceiveHandle;
                            frmresult.ShowDialog();

                            ClearForm();
                            ClearMember();
                        }));

                    }
                    else
                    {

                        MainModel.frmMainmediaCart = CurrentCart;
                        MainModel.frmmainmedia.UpdateForm();
                    }

                    this.Enabled = true;
                    // btnUSB.Focus();
                }));
            }
            catch { }

        }


        //0 需要微信支付宝继续支付  2、现金支付完成
        private void FormCash_DataReceiveHandle(int type, string orderid)
        {
            try
            {
                this.Invoke(new InvokeHandler(delegate()
                {
                    //返回收银方式按钮 关闭现金收银页面
                    if (type == 0)
                    {
                        this.Invoke(new InvokeHandler(delegate()
                        {
                            frmonlinepayresult = new frmOnLinePayResult(orderid);

                            frmonlinepayresult.frmOnLinePayResult_SizeChanged(null, null);
                            //frmonlinepayresult.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width / 3, this.Height - 200);
                            frmonlinepayresult.Location = new System.Drawing.Point((Screen.PrimaryScreen.Bounds.Width - frmonlinepayresult.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - frmonlinepayresult.Height) / 2);

                            frmonlinepayresult.DataReceiveHandle += FormOnLinePayResult_DataReceiveHandle;
                            frmonlinepayresult.ShowDialog();
                            frmonlinepayresult.DataReceiveHandle -= FormOnLinePayResult_DataReceiveHandle;
                            frmonlinepayresult = null;

                            // ClearForm();
                        }));
                    }
                    else if (type == 1)
                    {
                        this.Invoke(new InvokeHandler(delegate()
                        {
                            frmCashierResult frmresult = new frmCashierResult(orderid);
                            frmresult.DataReceiveHandle += FormCashierResult_DataReceiveHandle;
                            frmresult.ShowDialog();

                            ClearForm();
                            ClearMember();
                        }));
                    }
                    else if (type == MainModel.HttpUserExpired || type == MainModel.HttpMemberExpired)
                    {
                        CheckUserAndMember(type, "");
                    }

                    if (CurrentCart != null)
                    {
                        CurrentCart.cashpayoption = 0;
                        CurrentCart.cashpayamt = 0;
                    }

                    this.Enabled = true;
                    RefreshCart();

                }));
            }
            catch (Exception ex)
            {
                this.Invoke(new InvokeHandler(delegate()
                {
                    this.Enabled = true;
                }));
            }



        }


        private void FormCashCoupon_DataReceiveHandle(int type, string orderid)
        {
            try
            {
                this.Invoke(new InvokeHandler(delegate()
                {
                    if (type == MainModel.HttpUserExpired || type == MainModel.HttpMemberExpired)
                    {
                        CheckUserAndMember(type, "");
                    }
                    if (type == 1)
                    {
                        this.Invoke(new InvokeHandler(delegate()
                        {
                            frmonlinepayresult = new frmOnLinePayResult(orderid);

                            frmonlinepayresult.frmOnLinePayResult_SizeChanged(null, null);
                            frmonlinepayresult.Location = new System.Drawing.Point((Screen.PrimaryScreen.Bounds.Width - frmonlinepayresult.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - frmonlinepayresult.Height) / 2);

                            frmonlinepayresult.DataReceiveHandle += FormOnLinePayResult_DataReceiveHandle;
                            frmonlinepayresult.ShowDialog();
                            frmonlinepayresult = null;
                        }));
                    }
                    else if (type == 2)
                    {
                        this.Invoke(new InvokeHandler(delegate()
                        {
                            frmBalancePayResult frmbalancepayresult = new frmBalancePayResult(orderid);

                            frmbalancepayresult.Location = new System.Drawing.Point((Screen.PrimaryScreen.Bounds.Width - frmbalancepayresult.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - frmbalancepayresult.Height) / 2);

                            frmbalancepayresult.DataReceiveHandle += FormOnLinePayResult_DataReceiveHandle;
                            frmbalancepayresult.ShowDialog();
                            frmbalancepayresult = null;
                        }));
                    }

                    if (CurrentCart != null)
                    {
                        CurrentCart.cashcouponamt = 0;
                    }
                    //RefreshCart();

                    this.Enabled = true;
                }));
            }
            catch (Exception ex)
            {
                this.Invoke(new InvokeHandler(delegate()
                {
                    this.Enabled = true;
                }));
            }

        }

        private void FormCashierResult_DataReceiveHandle(int type, string payinfo)
        {
            try
            {
                this.Invoke(new InvokeHandler(delegate()
                {

                    if (type == 0)
                    {
                        this.Invoke(new InvokeHandler(delegate()
                        {

                        }));
                    }
                    else if (type == 1)
                    {
                        this.Invoke(new InvokeHandler(delegate()
                        {
                            MainModel.frmmainmedia.ShowPayResult(payinfo);
                        }));

                    }
                }));
            }
            catch (Exception ex)
            {

            }

        }


        private void FormGoodsCode_DataReceiveHandle(int type, string goodscode)
        {
            try
            {

               
                    if (type == 0)
                    {

                    }
                    else if (type == 1)
                    {
                       
                            if (goodscode.Length == 13)
                            {
                                ThreadPool.QueueUserWorkItem(new WaitCallback(ScanCodeThread), goodscode);
                                //ParameterizedThreadStart Pts = new ParameterizedThreadStart(ScanCodeThread);
                                //Thread thread = new Thread(Pts);
                                //thread.IsBackground = true;
                                //thread.Start(goodscode);
                                // QueueScanCode.Enqueue(goodscode);
                            }
                            else
                            {
                                ThreadPool.QueueUserWorkItem(new WaitCallback(InputShortCodeThread), goodscode);
                                //ParameterizedThreadStart Pts = new ParameterizedThreadStart(InputShortCodeThread);
                                //Thread thread = new Thread(Pts);
                                //thread.IsBackground = true;
                                //thread.Start(goodscode);
                                // QueueShortCode.Enqueue(goodscode);
                            }

                       // }));

                    }
                    this.Enabled = true;

                    //btnmianban.focus();
                //}));
            }
            catch (Exception ex)
            {
                //LoadingHelper.CloseForm();//关闭
                LogManager.WriteLog("ERROR", "条码数据处理异常：" + ex.Message);
            }
            finally
            {
                this.Invoke(new InvokeHandler(delegate()
                {
                    //btnmianban.focus();
                    this.Enabled = true;
                    LoadingHelper.CloseForm();//关闭
                }));



            }

        }

        private void FormGoodWeight_DataReceiveHandle(int type, string goodscode)
        {
            if (type == 0)
            {

            }
            else if (type == 1)
            {
                this.Invoke(new InvokeHandler(delegate()
                {


                    QueueScanCode.Enqueue(goodscode);
                }));

            }

        }


        private void FormOrderHang_DataReceiveHandle(int type, Cart Cart, string phone)
        {
            try
            {

                this.Invoke(new InvokeHandler(delegate()
                {
                    IsScan = true;
                    if (!string.IsNullOrEmpty(phone))
                    {
                        string ErrorMsgMember = "";
                        Member member = httputil.GetMember(phone, ref ErrorMsgMember);

                        if (ErrorMsgMember != "" || member == null) //会员不存在
                        {

                            ClearMember();
                            ShowLog(ErrorMsgMember, false);
                        }
                        else
                        {
                            LoadMember(member);
                        }
                    }

                    foreach (Product pro in Cart.products)
                    {
                        scancodememberModel tempscancode = new scancodememberModel();

                        Scancodedto tempscancodedto = new Scancodedto();

                        tempscancodedto.skucode = pro.skucode;
                        tempscancodedto.num = pro.num;
                        tempscancodedto.specnum = pro.specnum;
                        tempscancodedto.spectype = pro.spectype;
                        tempscancodedto.weightflag = Convert.ToBoolean(pro.goodstagid);
                        tempscancodedto.barcode = pro.barcode;

                        tempscancode.scancodedto = tempscancodedto;
                        //QueueScanCode.Enqueue(pro.barcode);
                    }

                    UploadDgvGoods(Cart);
                }));

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ERROR", "加载挂单异常" + ex.Message);
            }
        }

        #endregion

        #region 支付
        //在线支付
        private void btnPayOnLine_Click(object sender, EventArgs e)
        {
            try
            {

                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                {
                    this.Enabled = false;
                    listener.Stop();
                    string ErrorMsgCart = "";
                    int ResultCode = 0;
                    Cart cart = httputil.RefreshCart(CurrentCart, ref ErrorMsgCart, ref ResultCode);
                    CurrentCart = cart;
                    if (ErrorMsgCart != "" || cart == null) //商品不存在或异常
                    {
                        ShowLog(ErrorMsgCart, false);

                        CheckUserAndMember(ResultCode, ErrorMsgCart);
                        //ShowLog(ErrorMsgCart, false);
                    }
                    else
                    {
                        CurrentCart = cart;
                        string ErrorMsg = "";
                        // int ResultCode = 0;
                        CreateOrderResult orderresult = httputil.CreateOrder(CurrentCart, ref ErrorMsg, ref ResultCode);
                        if (ResultCode != 0 || orderresult == null)
                        {
                            CheckUserAndMember(ResultCode, ErrorMsg);
                            //ShowLog("异常" + ErrorMsg, true);
                        }
                        else if (orderresult.continuepay == 1)
                        {
                            frmonlinepayresult = new frmOnLinePayResult(orderresult.orderid);

                            frmonlinepayresult.frmOnLinePayResult_SizeChanged(null, null);
                            frmonlinepayresult.Location = new System.Drawing.Point((Screen.PrimaryScreen.Bounds.Width - frmonlinepayresult.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - frmonlinepayresult.Height) / 2);

                            frmonlinepayresult.DataReceiveHandle += FormOnLinePayResult_DataReceiveHandle;
                            frmonlinepayresult.ShowDialog();
                            frmonlinepayresult = null;
                        }
                    }

                    listener.Start();
                    this.Enabled = true;
                }

                //btnmianban.focus();
                Application.DoEvents();

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                ShowLog("在线收银异常" + ex.Message, true);
            }
        }

        //现金支付
        private void btnPayByCash_Click(object sender, EventArgs e)
        {
            try
            {

                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                {


                    CurrentCart.cashpayoption = 1;
                    if (!RefreshCart())
                    {
                        return;
                    }

                    this.Enabled = false;
                    listener.Stop();



                    //MemberwiseClone(CurrentCart);
                    frmCashPay frmcash = new frmCashPay(CurrentCart);
                    frmcash.frmCashPay_SizeChanged(null, null);
                    frmcash.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width * 33 / 100, this.Height * 70 / 100);
                    frmcash.Location = new System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width - frmcash.Width - 40, this.Height * 15 / 100);

                    frmcash.CashPayDataReceiveHandle += FormCash_DataReceiveHandle;
                    //frmcash.Opacity = 0.95d;
                    frmcash.ShowDialog();


                    this.Enabled = true;
                    Application.DoEvents();

                    listener.Start();


                    //btnmianban.focus();
                    //btnUSB.Focus();
                }
            }
            catch (Exception ex)
            {
                ShowLog("现金收银异常：" + ex.Message, true);
            }
            finally
            {
                listener.Start();
                this.Enabled = true;

                //btnmianban.focus();
            }


        }


        //余额支付
        private void btnPayByBalance_Click(object sender, EventArgs e)
        {
            try
            {

                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                {
                    this.Enabled = false;
                    listener.Stop();

                    string ErrorMsgCart = "";
                    int ResultCode = 0;
                    Cart cart = httputil.RefreshCart(CurrentCart, ref ErrorMsgCart, ref ResultCode);
                    //CurrentCart = cart;
                    if (ErrorMsgCart != "" || cart == null) //商品不存在或异常
                    {
                        ShowLog(ErrorMsgCart, false);

                        CheckUserAndMember(ResultCode, ErrorMsgCart);
                        //ShowLog(ErrorMsgCart, false);
                    }
                    else
                    {
                        CurrentCart = cart;
                        string ErrorMsg = "";
                        //int ResultCode = 0;
                        CreateOrderResult orderresult = httputil.CreateOrder(CurrentCart, ref ErrorMsg, ref ResultCode);
                        if (ResultCode != 0 || orderresult == null)
                        {
                            CheckUserAndMember(ResultCode, ErrorMsg);
                            //ShowLog("异常" + ErrorMsg, true);
                        }
                        else if (orderresult.continuepay == 1)
                        {
                            frmBalancePayResult frmbalancepayresult = new frmBalancePayResult(orderresult.orderid);

                            frmbalancepayresult.Location = new System.Drawing.Point((Screen.PrimaryScreen.Bounds.Width - frmbalancepayresult.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - frmbalancepayresult.Height) / 2);

                            frmbalancepayresult.DataReceiveHandle += FormOnLinePayResult_DataReceiveHandle;
                            frmbalancepayresult.ShowDialog();
                            frmbalancepayresult = null;
                        }
                    }

                    listener.Start();
                    this.Enabled = true;
                }
                Application.DoEvents();


                //btnmianban.focus();
            }
            catch (Exception ex)
            {
                this.Enabled = true;
                ShowLog("在线收银异常" + ex.Message, true);
            }
            finally
            {
                listener.Start();
                this.Enabled = true;
                //btnmianban.focus();
            }
        }

        //代金券支付
        private void btnByCoupon_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                {

                    if (!RefreshCart())
                    {
                        return;
                    }
                    this.Enabled = false;
                    listener.Stop();
                    frmCashCoupon frmcashcoupon = new frmCashCoupon(CurrentCart);
                    frmcashcoupon.DataReceiveHandle += FormCashCoupon_DataReceiveHandle;
                    frmcashcoupon.frmCashCoupon_SizeChanged(null, null);
                    frmcashcoupon.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width / 3, this.Height - 200);
                    frmcashcoupon.Location = new System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width - frmcashcoupon.Width - 80, 100);
                    //frmcashcoupon.TopMost = true;
                    frmcashcoupon.Opacity = 0.95d;
                    frmcashcoupon.ShowDialog();

                    //TODO  数据为什么会变为1？？？？？？？？？？？ 引用类型 傻不傻！！！
                    CurrentCart.cashcouponamt = 0;
                    //if (frmcashcoupon.DialogResult == DialogResult.OK)
                    //{
                    //    ClearForm();
                    //    ClearMember();
                    //}
                    //else
                    //{
                    //    RefreshCart();
                    //}
                    Application.DoEvents();

                    //btnmianban.focus();
                }
            }
            catch (Exception ex)
            {
                ShowLog("代金券收银异常：" + ex.Message, true);
            }
            finally
            {
                listener.Start();
                this.Enabled = true;
                //btnmianban.focus();
            }

        }


        private void btnPayOK_Click(object sender, EventArgs e)
        {
            string ErrorMsg = "";
            int ResultCode = 0;
            CreateOrderResult orderresult = httputil.CreateOrder(CurrentCart, ref ErrorMsg, ref ResultCode);
            if (ResultCode != 0 || orderresult == null)
            {
                CheckUserAndMember(ResultCode, ErrorMsg);
                // ShowLog("异常" + ErrorMsg, true);
            }
            else if (orderresult.continuepay == 1)
            {
                //TODO  继续支付
                ShowLog("需要继续支付", true);
            }
            else
            {
                frmCashierResult frmresult = new frmCashierResult(orderresult.orderid);
                frmresult.DataReceiveHandle += FormCashierResult_DataReceiveHandle;
                frmresult.ShowDialog();
                ClearForm();
                ClearMember();
            }
        }


        #endregion



        private void dgvGood_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.RowIndex < 0)
                    return;

                string proinfo = dgvGood.Rows[e.RowIndex].Cells["barcode"].Value.ToString();
                string[] strs = proinfo.Replace("\r\n", "*").Split('*');
                string skucode = strs[strs.Length - 1].Trim();
                string proname = strs[strs.Length - 2].Trim();

                string pronum = dgvGood.Rows[e.RowIndex].Cells["num"].Value.ToString();
                //增加标品
                if (dgvGood.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "+")
                {

                    for (int i = 0; i < CurrentCart.products.Count; i++)
                    {
                        if (CurrentCart.products[i].skucode == skucode)
                        {
                            CurrentCart.products[i].num += 1;
                            break;
                        }
                    }

                    RefreshCart();
                    //UploadDgvGoods(CurrentCart);
                }
                //减少标品
                else if (dgvGood.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "-")
                {
                    for (int i = 0; i < CurrentCart.products.Count; i++)
                    {
                        if (CurrentCart.products[i].skucode == skucode)
                        {

                            if (CurrentCart.products[i].num == 1)
                            {
                                this.Enabled = false;
                                frmDeleteGood frmdelete = new frmDeleteGood("是否确认删除商品？", proname, skucode);

                                if (frmdelete.ShowDialog() == DialogResult.OK)
                                {
                                    this.Enabled = true;
                                    CurrentCart.products.RemoveAt(i);
                                }
                                this.Enabled = true;

                            }
                            else
                            {
                                CurrentCart.products[i].num -= 1;
                            }
                            break;
                        }
                    }
                    RefreshCart();
                }

                if (e.ColumnIndex == 6)
                {
                    this.Enabled = false;
                    frmDeleteGood frmdelete = new frmDeleteGood("是否确认删除商品？", proname, skucode);
                    if (frmdelete.ShowDialog() != DialogResult.OK)
                    {
                        this.Enabled = true;
                        return;
                    }
                    this.Enabled = true;

                    foreach (Product pro in CurrentCart.products)
                    {
                        if (pro.skucode == skucode && (pro.num.ToString() == pronum || (pro.price.specnum + pro.price.unit) == pronum))
                        {
                            CurrentCart.products.Remove(pro);
                            break;
                        }
                    }

                    RefreshCart();
                }

                dgvGood.ClearSelection();
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("操作购物车商品异常" + ex.Message, true);
            }

        }


        public static GraphicsPath DrawRoundRect(int x, int y, int width, int height, int radius)
        {
            //四边圆角
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(x, y, radius, radius, 180, 90);
            gp.AddArc(width - radius, y, radius, radius, 270, 90);
            gp.AddArc(width - radius, height - radius, radius, radius, 0, 90);
            gp.AddArc(x, height - radius, radius, radius, 90, 90);
            gp.CloseAllFigures();
            return gp;
        }




        private void frmMain_EnabledChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.Enabled)
                {
                    picScreen.Visible = false;

                }
                else
                {
                    picScreen.BackgroundImage = MainModel.GetWinformImage(this);
                    picScreen.Size = new System.Drawing.Size(this.Width, this.Height);
                    picScreen.Visible = true;

                }

                Application.DoEvents();
            }
            catch (Exception ex)
            {
                picScreen.Visible = false;
                LogManager.WriteLog("修改主窗体背景图异常：" + ex.Message);
            }
        }



        private Image GetWinformImage()
        {
            //获取当前屏幕的图像
            Bitmap b = new Bitmap(this.Width, this.Height);
            this.DrawToBitmap(b, new Rectangle(0, 0, this.Width, this.Height));
            //b.Save(yourFileName);
            return b;
        }


        private Image TransparentImage(Image srcImage, float opacity)
        {
            float[][] nArray ={ new float[] {1, 0, 0, 0, 0},
                  new float[] {0, 1, 0, 0, 0},
                  new float[] {0, 0, 1, 0, 0},
                  new float[] {0, 0, 0, opacity, 0},
                  new float[] {0, 0, 0, 0, 1}};
            ColorMatrix matrix = new ColorMatrix(nArray);
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            Bitmap resultImage = new Bitmap(srcImage.Width, srcImage.Height);
            Graphics g = Graphics.FromImage(resultImage);
            g.DrawImage(srcImage, new Rectangle(0, 0, srcImage.Width, srcImage.Height), 0, 0, srcImage.Width, srcImage.Height, GraphicsUnit.Pixel, attributes);

            return resultImage;
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            frmPrinterSetting frmprint = new frmPrinterSetting();
            frmprint.StartPosition = FormStartPosition.CenterScreen;
            frmprint.ShowDialog();
            this.Enabled = true;
        }

        private void btnMianban_Click(object sender, EventArgs e)
        {

            //LoadIncrementProduct();
            try
            {
                IsScan = false;
                frmPanelGoods frmpanel = new frmPanelGoods();
                asf.AutoScaleControlTest(frmpanel, Screen.PrimaryScreen.Bounds.Width, SystemInformation.WorkingArea.Height , true);
                frmpanel.Location = new System.Drawing.Point(0, 0);
                btnOrderCancle.Visible = false;
                btnOrderHang.Visible = false;
                btnOrderQuery.Visible = false;
                frmpanel.ShowDialog();

                IsScan = true;
                btnOrderHang.Visible = true;
                btnOrderQuery.Visible = true;
                if (frmpanel.DialogResult == DialogResult.OK)
                {


                    if (dgvGood.Rows.Count > 0)
                    {
                        btnOrderCancle.Visible = true;
                    }

                    if (CurrentCart == null)
                    {
                        CurrentCart = new Cart();
                    }
                    if (CurrentCart.products == null)
                    {
                        CurrentCart.products = frmpanel.SelectProducts;
                        RefreshCart();
                    }
                    else
                    {
                        CurrentCart.products.AddRange(frmpanel.SelectProducts);
                        RefreshCart();
                    }
                }
            }
            catch (Exception ex)
            {

                ShowLog("加载面板商品异常" + ex.Message, true);
            }

        }



        bool isCellPainting = true;
        //重绘datagridview单元格
        private void dgvGood_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

            try
            {
                if (e.ColumnIndex == 0 && e.RowIndex >= 0 && e.Value != null && isCellPainting)//要进行重绘的单元格
                {

                    Graphics gpcEventArgs = e.Graphics;
                    Color clrBack = e.CellStyle.BackColor;
                    //Font fntText = e.CellStyle.Font;//获取单元格字体
                    //先使用北京颜色重画一遍背景
                    gpcEventArgs.FillRectangle(new SolidBrush(clrBack), e.CellBounds);
                    //设置字体的颜色
                    Color oneFore = System.Drawing.Color.Black;
                    Color secFore = System.Drawing.Color.Red;
                    //string strFirstLine = "黑色内容";
                    //string strSecondLine = "红色内容";

                    if (!e.Value.ToString().Contains("\r\n"))
                    {
                        return;
                    }

                    string tempstr = e.Value.ToString().Replace("\r\n", "*");
                    string strLine1 = "";
                    string strLine2 = "";
                    string strLine3 = "";

                    strLine1 = tempstr.Split('*')[0];
                    strLine2 = tempstr.Split('*')[1];
                    strLine3 = tempstr.Split('*')[2];
                    string[] sts = tempstr.Split('*');
                    //Size sizText = TextRenderer.MeasureText(e.Graphics, strFirstLine, fntText);
                    int intX = e.CellBounds.Left + e.CellStyle.Padding.Left;
                    int intY = e.CellBounds.Top + e.CellStyle.Padding.Top + 10;
                    int intWidth = e.CellBounds.Width - (e.CellStyle.Padding.Left + e.CellStyle.Padding.Right);
                    //int intHeight = sizText.Height + (e.CellStyle.Padding.Top + e.CellStyle.Padding.Bottom);


                    Font fnt1 = new System.Drawing.Font("微软雅黑", 10F * Math.Min(MainModel.hScale, MainModel.wScale));
                    //Graphics g = this.CreateGraphics(); //this是指所有control派生出来的类，这里是个form

                    SizeF size1 = this.CreateGraphics().MeasureString(strLine1, fnt1);
                    Color titlebackcolor = Color.Black;
                    if (strLine1.Length > 0)
                    {

                        string typecolor = strLine1.Substring(0, 1);
                        strLine1 = strLine1.Substring(1, strLine1.Length - 1);
                        switch (typecolor)
                        {
                            case "1": titlebackcolor = ColorTranslator.FromHtml("#FF7D14"); break;
                            case "2": titlebackcolor = ColorTranslator.FromHtml("#209FD4"); break;
                            case "3": titlebackcolor = ColorTranslator.FromHtml("#D42031"); break;
                            case "4": titlebackcolor = ColorTranslator.FromHtml("#FF000"); break;
                        }
                    }
                    //第一行
                    TextRenderer.DrawText(e.Graphics, strLine1, fnt1, new Rectangle(intX + 10, intY, intWidth, (int)size1.Height),
                        Color.White, titlebackcolor, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);

                    //另起一行
                    Font fnt2 = new System.Drawing.Font("微软雅黑", 12F * Math.Min(MainModel.hScale, MainModel.wScale));
                    SizeF size2 = this.CreateGraphics().MeasureString(strLine2, fnt2);

                    intY = intY + (int)size1.Height;
                    TextRenderer.DrawText(e.Graphics, strLine2, fnt2, new Rectangle(intX, intY, intWidth, (int)size2.Height),
                        Color.Black, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);

                    //Font fnt20 = new System.Drawing.Font("微软雅黑", 9F, FontStyle.Strikeout);
                    //TextRenderer.DrawText(e.Graphics, strLine2, fnt20, new Rectangle(intX + (int)size2.Width, intY, intWidth, (int)size2.Height),
                    //    Color.Green, Color.Red, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);


                    Font fnt3 = new System.Drawing.Font("微软雅黑", 12F * Math.Min(MainModel.hScale, MainModel.wScale));
                    intY = intY + (int)size2.Height;

                    TextRenderer.DrawText(e.Graphics, strLine3, fnt3, new Rectangle(intX, intY, intWidth, (int)size2.Height), Color.Black, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);


                    //int y = intY + (int)size2.Height + e.CellStyle.Padding.Top + e.CellStyle.Padding.Bottom + dgvGood.RowTemplate.Height;
                    int y = (e.RowIndex + 1) * dgvGood.RowTemplate.Height + dgvGood.ColumnHeadersHeight - 1;

                    //Point point1 = new Point(0, y);
                    //Point point2 = new Point(e.CellBounds.Width, y);
                    //Pen blackPen = new Pen(Color.Black, 1);
                    //e.Graphics.DrawLine(blackPen, point1, point2);

                    //Point point21 = new Point(10, 0);
                    //Point point22 = new Point(10, intY + (int)size2.Height + e.CellStyle.Padding.Top + e.CellStyle.Padding.Bottom);
                    //Pen blackPen2 = new Pen(Color.Black, 10);
                    //e.Graphics.DrawLine(blackPen2, point21, point21);
                    // e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    // dgv2.Rows[e.RowIndex].Height = (int)size1.Height+(int)size2.Height*2 + e.CellStyle.Padding.Top + e.CellStyle.Padding.Bottom+1;
                    e.Handled = true;

                    dgvGood.ClearSelection();
                }

                if ((e.ColumnIndex == 1 || e.ColumnIndex == 5) && e.RowIndex >= 0 && e.Value != null && isCellPainting)//要进行重绘的单元格
                {

                    Graphics gpcEventArgs = e.Graphics;
                    Color clrBack = e.CellStyle.BackColor;
                    //Font fntText = e.CellStyle.Font;//获取单元格字体
                    //先使用北京颜色重画一遍背景
                    gpcEventArgs.FillRectangle(new SolidBrush(clrBack), e.CellBounds);
                    //设置字体的颜色
                    Color oneFore = System.Drawing.Color.Black;
                    Color secFore = System.Drawing.Color.Red;
                    //string strFirstLine = "黑色内容";
                    //string strSecondLine = "红色内容";

                    if (!e.Value.ToString().Contains("\r\n"))
                    {
                        return;
                    }

                    string tempstr = e.Value.ToString().Replace("\r\n", "*");
                    string strLine1 = "";
                    string strLine2 = "";


                    strLine1 = tempstr.Split('*')[0];
                    strLine2 = tempstr.Split('*')[1];

                    string[] sts = tempstr.Split('*');
                    //Size sizText = TextRenderer.MeasureText(e.Graphics, strFirstLine, fntText);
                    int intX = e.CellBounds.Left + e.CellStyle.Padding.Left;
                    int intY = e.CellBounds.Top + e.CellStyle.Padding.Top + 30;
                    int intWidth = e.CellBounds.Width - (e.CellStyle.Padding.Left + e.CellStyle.Padding.Right);
                    //int intHeight = sizText.Height + (e.CellStyle.Padding.Top + e.CellStyle.Padding.Bottom);


                    Font fnt1 = new System.Drawing.Font("微软雅黑", 12F * Math.Min(MainModel.hScale, MainModel.wScale));
                    //Graphics g = this.CreateGraphics(); //this是指所有control派生出来的类，这里是个form
                    SizeF size1 = this.CreateGraphics().MeasureString(strLine1, fnt1);

                    if (strLine1.Contains("("))
                    {
                        int index = strLine1.IndexOf("(");

                        string tempstrline11 = strLine1.Substring(0, index);
                        string tempstrline12 = strLine1.Substring(index);

                        SizeF siztemp1 = this.CreateGraphics().MeasureString(tempstrline11, fnt1);
                        SizeF sizetemp2 = this.CreateGraphics().MeasureString(tempstrline12, fnt1);

                        int pianyiX = (int)(e.CellBounds.Width - siztemp1.Width - sizetemp2.Width) / 2;
                        if (e.ColumnIndex == 5)
                        {
                            TextRenderer.DrawText(e.Graphics, tempstrline11, fnt1, new Rectangle(intX + pianyiX, intY, intWidth, (int)siztemp1.Height),
                                Color.OrangeRed, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);

                        }
                        else
                        {
                            TextRenderer.DrawText(e.Graphics, tempstrline11, fnt1, new Rectangle(intX + pianyiX, intY, intWidth, (int)siztemp1.Height),
                                Color.Black, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);

                        }

                        Font tempfont2 = new System.Drawing.Font("微软雅黑", 10F * Math.Min(MainModel.hScale, MainModel.wScale));

                        TextRenderer.DrawText(e.Graphics, tempstrline12, tempfont2, new Rectangle(intX + (int)siztemp1.Width + pianyiX, intY, intWidth, (int)siztemp1.Height),
                          Color.DimGray, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);

                    }
                    else
                    {
                        if (e.ColumnIndex == 5)
                        {
                            //第一行
                            TextRenderer.DrawText(e.Graphics, strLine1, fnt1, new Rectangle(intX + (int)(e.CellBounds.Width - size1.Width) / 2, intY, intWidth, (int)size1.Height),
                                Color.OrangeRed, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);
                        }
                        else
                        {
                            //第一行
                            TextRenderer.DrawText(e.Graphics, strLine1, fnt1, new Rectangle(intX + (int)(e.CellBounds.Width - size1.Width) / 2, intY, intWidth, (int)size1.Height),
                                Color.Black, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);
                        }

                    }


                    //第二行
                    Font fnt2 = new System.Drawing.Font("微软雅黑", 10F * Math.Min(MainModel.hScale, MainModel.wScale));
                    bool isstrickout = false;
                    if (strLine2.Contains("strikeout"))
                    {
                        isstrickout = true;
                        fnt2 = new System.Drawing.Font("微软雅黑", 10F * Math.Min(MainModel.hScale, MainModel.wScale), FontStyle.Strikeout);
                        strLine2 = strLine2.Replace("strikeout", "");
                    }
                    SizeF size2 = this.CreateGraphics().MeasureString(strLine2, fnt2);
                    intY = intY + (int)size1.Height;

                    if (strLine2.Contains("("))
                    {
                        int index = strLine2.IndexOf("(");

                        string tempstrline21 = strLine2.Substring(0, index);
                        string tempstrline22 = strLine2.Substring(index);

                        SizeF siztemp1 = this.CreateGraphics().MeasureString(tempstrline21, fnt2);
                        SizeF sizetemp2 = this.CreateGraphics().MeasureString(tempstrline22, fnt2);

                        int pianyiX = (int)(e.CellBounds.Width - siztemp1.Width - sizetemp2.Width) / 2;
                        //第一行
                        TextRenderer.DrawText(e.Graphics, tempstrline21, fnt2, new Rectangle(intX + pianyiX, intY, intWidth, (int)siztemp1.Height),
                            Color.DimGray, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);

                        Font tempfont2 = new System.Drawing.Font("微软雅黑", 10F * Math.Min(MainModel.hScale, MainModel.wScale));

                        TextRenderer.DrawText(e.Graphics, tempstrline22, fnt2, new Rectangle(intX + (int)siztemp1.Width + pianyiX, intY, intWidth, (int)size2.Height),
                        Color.DimGray, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);

                    }
                    else
                    {
                        TextRenderer.DrawText(e.Graphics, strLine2, fnt2, new Rectangle(intX + (int)(e.CellBounds.Width - size2.Width) / 2, intY, intWidth, (int)size2.Height),
                        Color.DimGray, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);
                    }


                    //int y = intY + (int)size2.Height + e.CellStyle.Padding.Top + e.CellStyle.Padding.Bottom + dgvGood.RowTemplate.Height;
                    int y = (e.RowIndex + 1) * dgvGood.RowTemplate.Height + dgvGood.ColumnHeadersHeight - 1;

                    //Point point1 = new Point(0, y);
                    ////Point point2 = new Point(e.CellBounds.Width, y);
                    //Point point2 = new Point(dgvGood.Width, y);
                    //Pen blackPen = new Pen(Color.Black, 1);
                    //e.Graphics.DrawLine(blackPen, point1, point2);


                    // dgv2.Rows[e.RowIndex].Height = (int)size1.Height+(int)size2.Height*2 + e.CellStyle.Padding.Top + e.CellStyle.Padding.Bottom+1;
                    e.Handled = true;

                    dgvGood.ClearSelection();
                }

            }
            catch (Exception ex)
            {

            }
        }


        private void SetBtnPayStarus(bool isEnable)
        {
            if (isEnable)
            {

                btnPayOnLine.BackColor = Color.Tomato;
                btnPayByCash.BackColor = Color.DarkOrange;
                btnPayByBalance.BackColor = Color.DarkTurquoise;
                btnPayByCoupon.BackColor = Color.MediumSeaGreen;

            }
            else
            {
                btnPayOnLine.BackColor = Color.Silver;
                btnPayByCash.BackColor = Color.Silver;
                btnPayByBalance.BackColor = Color.Silver;
                btnPayByCoupon.BackColor = Color.Silver;

                //btnPayOnLine.ForeColor = Color.White;
                //btnPayByCash.ForeColor = Color.White;
                //btnPayByBalance.ForeColor = Color.White;
                //btnPayByCoupon.ForeColor = Color.White;

            }
        }

        private void pnlPayType1_EnabledChanged(object sender, EventArgs e)
        {

            //if(pnlPayType1.Enabled==false){
            //    btnPayOnLine.BackColor = Color.Silver;
            //    btnPayByCash.BackColor = Color.Silver;
            //    btnPayByBalance.BackColor = Color.Silver;
            //    btnPayByCoupon.BackColor = Color.Silver;

            //    btnPayOnLine.ForeColor = Color.White;
            //    btnPayByCash.ForeColor = Color.White;
            //    btnPayByBalance.ForeColor = Color.White;
            //    btnPayByCoupon.ForeColor = Color.White;

            //}else{

            //    btnPayOnLine.BackColor = Color.Tomato;
            //    btnPayByCash.BackColor = Color.DarkOrange;
            //    btnPayByBalance.BackColor = Color.DarkTurquoise;
            //    btnPayByCoupon.BackColor = Color.MediumSeaGreen;

            //}

        }


        //[System.Runtime.InteropServices.DllImport("user32.dll ")]
        //public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int wndproc);
        //[System.Runtime.InteropServices.DllImport("user32.dll ")]
        //public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        //public const int GWL_STYLE = -16;
        //public const int WS_DISABLED = 0x8000000;

        //public static void SetControlEnabled(Control c, bool enabled)
        //{
        //    if (enabled)
        //    { SetWindowLong(c.Handle, GWL_STYLE, (~WS_DISABLED) & GetWindowLong(c.Handle, GWL_STYLE)); }
        //    else
        //    { SetWindowLong(c.Handle, GWL_STYLE, WS_DISABLED + GetWindowLong(c.Handle, GWL_STYLE)); }
        //}



        //屏蔽回车和空格键
        protected override bool ProcessDialogKey(Keys keyData)
        {

            if (keyData == Keys.Enter || keyData == Keys.Space)
            {
                LogManager.WriteLog("检测到回车空格键");
                return false;
            }

            else
                return base.ProcessDialogKey(keyData);
        }



        private void ShowDgv()
        {
            try
            {
                if (dgvGood.Rows.Count > 0)
                {
                    System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
                    dataGridViewCellStyle1.BackColor = Color.SkyBlue;
                    Color color = dgvGood.Rows[0].DefaultCellStyle.BackColor;

                    dgvGood.Rows[0].DefaultCellStyle = dataGridViewCellStyle1;

                    Delay.Start(200);
                    dataGridViewCellStyle1.BackColor = color;
                    dgvGood.Rows[0].DefaultCellStyle = dataGridViewCellStyle1;

                }

            }
            catch (Exception ex)
            {

            }
        }

        #region 商品全量/增量数据同步

        DBPRODUCT_BEANBLL productbll = new DBPRODUCT_BEANBLL();

        //当天第一次进入更新全局商品
        private void LoadAllProduct()
        {
            try
            {
                string errormesg = "";

                int i = 0;
                lstproduct.Clear();

                productbll.AddProduct(GetAllProdcut(1, 200));


            }
            catch (Exception ex)
            {
                LogManager.WriteLog("更新全部商品异常" + ex.Message);
            }

        }
        
        List<DBPRODUCT_BEANMODEL> lstproduct = new List<DBPRODUCT_BEANMODEL>();
        private List<DBPRODUCT_BEANMODEL> GetAllProdcut(int page, int size)
        {
            try
            {
                string errormesg = "";
                AllProduct allproduct = httputil.QuerySkushopAll(MainModel.CurrentShopInfo.shopid, page, size, ref errormesg);

                if (!string.IsNullOrEmpty(errormesg) || allproduct == null)
                {
                    LogManager.WriteLog("更新全部商品异常" + errormesg);
                    return null;
                }
                else
                {
                    MainModel.LastQuerySkushopCrementTimeStamp = allproduct.timestamp.ToString();
                    MainModel.LastQuerySkushopAllTimeStamp = allproduct.timestamp.ToString();

                    lstproduct.AddRange(allproduct.rows);
                    if (allproduct.rows.Count >= size)
                    {
                        GetAllProdcut(page + 1, size);
                    }
                }

                return lstproduct;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ERROR", "获取全量商品异常" + ex.Message);
                return null;
            }
        }

        private void timerGetIncrementProduct_Tick(object sender, EventArgs e)
        {

            //启动全量商品同步线程
            Thread threadLoadIncrementProduct = new Thread(LoadIncrementProduct);
            threadLoadIncrementProduct.IsBackground = true;
            threadLoadIncrementProduct.Start();
        }


        //每10分钟同步一个增量商品
        private void LoadIncrementProduct()
        {
            try
            {
                string errormesg = "";


                DateTime starttime = DateTime.Now;
                int i = 0;
                lstIncrementproduct.Clear();
                //productbll.Add(allproduct.rows[0]);
                productbll.AddProduct(GetIncrementProdcut(1, 100));

                LogManager.WriteLog("添加增量 数据时间" + (DateTime.Now - starttime).TotalMilliseconds);


            }
            catch (Exception ex)
            {
                LogManager.WriteLog("更新全部商品异常" + ex.Message);
            }

        }

        List<DBPRODUCT_BEANMODEL> lstIncrementproduct = new List<DBPRODUCT_BEANMODEL>();
        private List<DBPRODUCT_BEANMODEL> GetIncrementProdcut(int page, int size)
        {
            try
            {
                string errormesg = "";
                AllProduct allproduct = httputil.QuerySkushopIncrement(MainModel.CurrentShopInfo.shopid, page, size, ref errormesg);

                if (!string.IsNullOrEmpty(errormesg) || allproduct == null)
                {
                    LogManager.WriteLog("更新全部商品异常" + errormesg);
                    return null;
                }
                else
                {
                    MainModel.LastQuerySkushopCrementTimeStamp = allproduct.timestamp.ToString();

                    lstIncrementproduct.AddRange(allproduct.rows);
                    if (allproduct.rows.Count >= size)
                    {
                        GetIncrementProdcut(page + 1, size);
                    }
                }

                return lstIncrementproduct;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ERROR", "获取全量商品异常" + ex.Message);
                return null;
            }
        }

        #endregion



        DBSWITCH_KEY_BEANBLL scalebll = new DBSWITCH_KEY_BEANBLL();

        //当天第一次进入更新全局商品
        private void LoadScale()
        {
            try
            {
                string errormesg = "";

                DateTime starttime = DateTime.Now;
                int i = 0;
                lstScale.Clear();
                scalebll.AddScalse(GetScale(1, 100));

                LogManager.WriteLog("添加电子秤数据时间" + (DateTime.Now - starttime).TotalMilliseconds);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("更新电子秤数据异常" + ex.Message);
            }

        }

        List<DBSWITCH_KEY_BEANMODEL> lstScale = new List<DBSWITCH_KEY_BEANMODEL>();
        private List<DBSWITCH_KEY_BEANMODEL> GetScale(int page, int size)
        {
            try
            {
                string errormesg = "";
                Scale scale = httputil.GetScale(page, size, ref errormesg);

                if (!string.IsNullOrEmpty(errormesg) || scale == null)
                {
                    LogManager.WriteLog("获取电子秤信息异常" + errormesg);
                    return null;
                }
                else
                {
                    lstScale.AddRange(scale.rows);
                    if (scale.rows.Count >= size)
                    {
                        GetScale(page + 1, size);
                    }
                }

                return lstScale;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ERROR", "获取电子秤信息异常" + ex.Message);
                return null;
            }
        }




    }
}
