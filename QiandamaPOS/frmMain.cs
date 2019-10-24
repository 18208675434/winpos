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
using System.Threading.Tasks;
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
        /// 每次扫描商品调用cart接口 商品集合需要包含订单所以商品
        /// </summary>
        List<scancodememberModel> lstscancode = new List<scancodememberModel>();

        /// <summary>
        /// 接口访问类
        /// </summary>
        HttpUtil httputil = new HttpUtil();

        /// <summary>
        /// USB设备监听
        /// </summary>
        private ScanerHook listener = new ScanerHook();

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

        private Color MainBackColor;
        #endregion


        #region  页面加载
        public frmMain(frmLogin frmlogin)
        {
            InitializeComponent();
            MainModel.wScale = (float)Screen.PrimaryScreen.Bounds.Width / this.Width;
            MainModel.hScale = (float)SystemInformation.WorkingArea.Height / this.Height;
            // Control.CheckForIllegalCrossThreadCalls = false;
            listener.ScanerEvent += Listener_ScanerEvent;
            LoadingHelper.CloseForm();
            CurrentFrmLogin = frmlogin;
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            //string width = Screen.AllScreens[0].Bounds.Width.ToString();
            //string height = Screen.AllScreens[0].Bounds.Height.ToString();
            //MessageBox.Show(width + "  " + height + "  " + Screen.PrimaryScreen.Bounds.Width + "  " + this.Height);

            MainBackColor = this.BackColor;
            listener.Start();

            timerNow.Interval = 1000;
            timerNow.Enabled = true;

            timerClearMemory.Interval = 2 * 60 * 1000;
            timerClearMemory.Enabled = true;

            lblShopName.Text = MainModel.CurrentShopInfo.shopname;
            dpbtnMenu.Text = MainModel.CurrentUser.nickname + ",你好▼";
            btnMenu.Text = MainModel.CurrentUser.nickname + "，你好▼";

            //UpdateOrderHang();

            //启动扫描处理线程
            Thread threadItemExedate = new Thread(ScanCodeExe);
            threadItemExedate.IsBackground = true;
            threadItemExedate.Start();


            //启动扫描处理线程
            Thread threadShortCodeExedate = new Thread(InputShortCodeExe);
            threadShortCodeExedate.IsBackground = true;
            threadShortCodeExedate.Start();

            btnorderhangimage = new Bitmap(btnOrderHang.Image, 10, 10);
            UpdateOrderHang();

            toolStripMain.Focus();

            //客屏初始化
            MainModel.frmmainmedia = new frmMainMedia();
            if (Screen.AllScreens.Count() != 1)
            {
                // windowstate设置max 不能再页面设置 否则会显示到第一个屏幕
                //MainModel.frmmainmedia.Size = new System.Drawing.Size(Screen.AllScreens[1].Bounds.Width, Screen.AllScreens[1].Bounds.Height+20);
                asf.AutoScaleControlTest(MainModel.frmmainmedia, Screen.AllScreens[1].Bounds.Width, Screen.AllScreens[1].Bounds.Height, true);
                MainModel.frmmainmedia.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width, -20);

                //MainModel.frmmainmedia.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                MainModel.frmmainmedia.Show();
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

            if (this.Enabled == true)
            {

                QueueScanCode.Enqueue(codes.Result);
            }

            // textBox1.Text = codes.Result;
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


        private void CheckUserAndMember(int resultcode)
        {
            try
            {
                if (resultcode == MainModel.HttpMemberExpired)
                {
                    frmUserExpired frmuserexpired = new frmUserExpired();
                    frmuserexpired.TopMost = true;
                    frmuserexpired.ShowDialog();

                    INIManager.SetIni("System", "POS-Authorization", "", MainModel.IniPath);

                    CurrentFrmLogin.Show();
                    this.Close();

                }
                else if (resultcode == MainModel.HttpUserExpired)
                {
                    ClearMember();
                    btnLoadPhone_Click(null, null);
                }
            }
            catch (Exception ex)
            {
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
            frmOrderQuery frmorderquery = new frmOrderQuery();

            asf.AutoScaleControlTest(frmorderquery, Screen.PrimaryScreen.Bounds.Width, SystemInformation.WorkingArea.Height - toolStripMain.Height, true);

            frmorderquery.Location = new System.Drawing.Point(0, toolStripMain.Height);

            frmorderquery.ShowDialog();
        }

        private void btnOrderCancle_Click(object sender, EventArgs e)
        {
            if (CurrentCart == null || CurrentCart.products == null || CurrentCart.products.Count <= 0)
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
            this.Enabled = true;
            ReceiptUtil.EditCancelOrder(1, CurrentCart.totalpayment);
            ClearForm();
            ClearMember();
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
                        this.Enabled = true;

                        SerializeOrder(CurrentCart);

                        ShowLog("已挂单", false);
                        ClearForm();
                        ClearMember();

                    }
                }
                else if (btnOrderHang.Text == "挂单列表")
                {

                    //this.IsMdiContainer = true;
                    frmOrderHang frmorderhang = new frmOrderHang();
                    frmorderhang.DataReceiveHandle += FormOrderHang_DataReceiveHandle;
                    //frmorderhang.Height = this.Height - toolStripMain.Height;
                    //frmorderhang.Width = Screen.PrimaryScreen.Bounds.Width;
                    asf.AutoScaleControlTest(frmorderhang, Screen.PrimaryScreen.Bounds.Width, this.Height - toolStripMain.Height, true);
                    frmorderhang.Location = new System.Drawing.Point(0, toolStripMain.Height);
                    //frmorderhang.Parent = this;
                    frmorderhang.ShowDialog();
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
                BinaryFormatter formatter = new BinaryFormatter();
                using (Stream output = File.Create(MainModel.OrderPath + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".order"))
                {
                    formatter.Serialize(output, cart);
                }
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


                //Image _image = btnOrderHang.Image;
                //Bitmap img = new Bitmap(_image ,10,10);

                //btnOrderHang.Image = img;

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
            frmtool.Location = new System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width - frmtool.Width - 20, toolStripMain.Height+15);

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
            frmReceiptQuery frmreceiptquery = new frmReceiptQuery();

            asf.AutoScaleControlTest(frmreceiptquery, Screen.PrimaryScreen.Bounds.Width, this.Height - toolStripMain.Height, true);
            frmreceiptquery.Location = new System.Drawing.Point(0, toolStripMain.Height);

            frmreceiptquery.ShowDialog();
        }





        private void tsmScale_Click(object sender, EventArgs e)
        {
            //frmReceiptQuery frmreceiptquery = new frmReceiptQuery();
            //frmreceiptquery.Height = this.Height - toolStripMain.Height;
            //frmreceiptquery.Width = Screen.PrimaryScreen.Bounds.Width;
            //frmreceiptquery.Location = new System.Drawing.Point(0, toolStripMain.Height);

            //frmreceiptquery.ShowDialog();
        }



        private void tsmExpense_Click(object sender, EventArgs e)
        {
            frmExpense frmexpense = new frmExpense();

            //frmexpense.frmExpense_SizeChanged(null,null);
            //frmexpense.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, SystemInformation.WorkingArea.Height - toolStripMain.Height);

            asf.AutoScaleControlTest(frmexpense, Screen.PrimaryScreen.Bounds.Width, SystemInformation.WorkingArea.Height - toolStripMain.Height, true);

            frmexpense.Location = new System.Drawing.Point(0, toolStripMain.Height);

            frmexpense.ShowDialog();
        }

        #endregion


        #region 购物车
        private void btnLoadBarCode_Click(object sender, EventArgs e)
        {
            try
            {

                this.Enabled = false;
                frmNumber frmnumber = new frmNumber("请输入商品条码", false, false);
                frmnumber.DataReceiveHandle += FormGoodsCode_DataReceiveHandle;
                // frmnumber.Opacity = 0.95d;
                frmnumber.frmNumber_SizeChanged(null, null);
                frmnumber.Size = new System.Drawing.Size(this.Width * 33 / 100, this.Height * 70 / 100);
                frmnumber.Location = new System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width - frmnumber.Width - 40, this.Height * 15 / 100);
                // asf.AutoScaleControlTest(frmnumber,this.Width,this.Height,true);
                frmnumber.ShowDialog();
                picScreen.Visible = false;
                Application.DoEvents();
                this.Enabled = true;
                toolStripMain.Focus();
            }
            catch (Exception ex)
            {
                this.Enabled = true;
                LogManager.WriteLog("输入商品条码异常" + ex.Message);
            }
        }


        private bool RefreshCart()
        {
            try
            {
                string ErrorMsgCart = "";
                int ResultCode = 0;
                toolStripMain.Focus();
                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                {
                    pnlPayType1.Enabled = true;
                    pnlPayType2.Enabled = true;
                    //CurrentCart.pointpayoption = 1;

                    CurrentCart.pointpayoption = btnCheckJF.Text == "√" ? 1 : 0;

                    Cart cart = httputil.RefreshCart(CurrentCart, ref ErrorMsgCart, ref ResultCode);
                    CurrentCart = cart;
                    if (ErrorMsgCart != "" || cart == null) //商品不存在或异常
                    {
                        ShowLog(ErrorMsgCart, false);

                        CheckUserAndMember(ResultCode);
                        return false;
                    }
                    else
                    {

                        UploadDgvGoods(cart);


                        if (btnCheckJF.Text == "√")
                        {
                            if (MainModel.CurrentMember != null && CurrentCart != null && CurrentCart.pointinfo != null)
                            {
                                lblJF.Text = CurrentCart.pointinfo.totalpoints;
                                //lblJFUse.Visible = true;
                                lblJFUse.Text = "使用" + CurrentCart.pointinfo.availablepoints + "积分 抵用" + CurrentCart.pointinfo.availablepointsamount + "元";
                                lblJFUse.BackColor = Color.Moccasin;
                                lblJFUse.Visible = true;

                            }
                        }
                        else
                        {
                            //lblJF.Text = "0";
                            //lblJFUse.Visible = false;
                            lblJFUse.Text = "";
                            lblJFUse.BackColor = Color.White;
                            // lblJFUse.Visible = false;

                        }



                        btnPayByBalance.Enabled = CurrentCart.paymenttypes.balancepayenabled == 1;
                        btnPayByCash.Enabled = CurrentCart.paymenttypes.cashenabled == 1;
                        btnPayOnLine.Enabled = CurrentCart.paymenttypes.onlineenabled == 1;

                        return true;
                    }

                }
                else
                {
                    pnlPayType1.Enabled = false;
                    pnlPayType2.Enabled = false;

                    ClearForm();
                    // ClearMember();
                    return true;
                }


            }
            catch (Exception ex)
            {
                ShowLog("刷新购物车异常：" + ex.Message, true);
                return false;
            }
        }


        private void ScanCodeExe()
        {
            while (true)
            {

                //有数据就处理
                if (QueueScanCode.Count > 0)
                {

                    try
                    {

                        this.Invoke(new InvokeHandler(delegate()
             {
                 LoadingHelper.ShowLoadingScreen();//显示
                 //this.Enabled = false;

                 string goodcode = QueueScanCode.Dequeue();

                 string ErrorMsg = "";
                 int ResultCode = 0;
                 scancodememberModel scancodemember = httputil.GetSkuInfoMember(goodcode, ref ErrorMsg, ref ResultCode);

                 if (ErrorMsg != "" || scancodemember == null) //商品不存在或异常
                 {
                     CheckUserAndMember(ResultCode);
                     ShowLog(ErrorMsg, false);
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
                 Application.DoEvents();
                 RefreshCart();
                 LoadingHelper.CloseForm();//显示
                 this.Enabled = true;
                 toolStripMain.Focus();

             }));
                    }
                    catch (Exception ex)
                    {
                        this.Enabled = true;
                        LoadingHelper.CloseForm();//关闭
                        LogManager.WriteLog("ERROR", "扫描数据处理异常：" + ex.Message);
                    }

                }
                else
                {
                    Thread.Sleep(1);
                }

            }
        }

        private void InputShortCodeExe()
        {
            while (true)
            {

                //有数据就处理
                if (QueueShortCode.Count > 0)
                {

                    try
                    {

                        this.Invoke(new InvokeHandler(delegate()
           {
               LoadingHelper.ShowLoadingScreen();//显示
               //this.Enabled = false;

               string goodcode = QueueShortCode.Dequeue();

               string ErrorMsg = "";
               int ResultCode = 0;
               scancodememberModel scancodemember = httputil.GetSkuInfoByShortCode(goodcode, ref ErrorMsg, ref ResultCode);


               if (ErrorMsg != "" || scancodemember == null) //商品不存在或异常
               {
                   CheckUserAndMember(ResultCode);
                   ShowLog(ErrorMsg, false);
               }
               else
               {
                   addcart(scancodemember);
               }
           }));
                    }
                    catch (Exception ex)
                    {
                        //this.Enabled = true;
                        LoadingHelper.CloseForm();//关闭
                        LogManager.WriteLog("ERROR", "扫描数据处理异常：" + ex.Message);
                    }
                    finally
                    {
                        //this.Enabled = true;
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
            try
            {
                this.Invoke(new InvokeHandler(delegate()
             {
                 if (scancodemember.scancodedto.specnum == 0)
                 {
                     this.Enabled = false;
                     frmNumber frmnumber = new frmNumber(scancodemember.scancodedto.skuname, false, true);
                     frmnumber.Opacity = 0.95d;
                     frmnumber.frmNumber_SizeChanged(null, null);

                     frmnumber.Size = new System.Drawing.Size(this.Width * 33 / 100, this.Height * 70 / 100);

                     //frmnumber.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                     frmnumber.Location = new System.Drawing.Point((Screen.PrimaryScreen.Bounds.Width - frmnumber.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - frmnumber.Height) / 2);
                     frmnumber.ShowDialog();

                     if (frmnumber.DialogResult == DialogResult.OK)
                     {
                         scancodemember.scancodedto.specnum = (decimal)frmnumber.NumValue / 1000;
                         scancodemember.scancodedto.num = 1;
                     }
                     else
                     {
                         this.Enabled = true;
                         Application.DoEvents();
                         return;
                     }

                     this.Enabled = true;
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


                 lstscancode.Add(scancodemember);
                 Cart cart = httputil.RefreshCart(CurrentCart, ref ErrorMsgCart, ref ResultCode);
                 CurrentCart = cart;
                 if (ErrorMsgCart != "" || cart == null) //商品不存在或异常
                 {
                     pnlPayType1.Enabled = false;
                     pnlPayType2.Enabled = false;
                     ShowLog(ErrorMsgCart, false);

                     CheckUserAndMember(ResultCode);
                     ShowLog(ErrorMsgCart, false);
                     lstscancode.Remove(scancodemember);
                 }
                 else
                 {
                     UploadDgvGoods(cart);
                     pnlPayType1.Enabled = true;
                     pnlPayType2.Enabled = true;
                 }
                 Application.DoEvents();
             }));
            }
            catch (Exception ex)
            {
                this.Enabled = true;
                LogManager.WriteLog("ERROR", "添加购物车商品异常:" + ex.Message);
            }
            finally
            {
                this.Enabled = true;
            }

        }



        //TODO  修改样式
        private void ShowLog(string msg, bool iserror)
        {
            MainModel.ShowLog(msg, iserror);
        }


        private void UploadDgvGoods(Cart cart)
        {
            try
            {
                this.Invoke(new InvokeHandler(delegate()
                {
                    dgvOrderDetail.Rows.Clear();
                    dgvGood.Rows.Clear();
                    pnlPayType1.Visible = true;
                    pnlPayType2.Visible = false;
                    if (cart != null && cart.products != null && cart.products.Count > 0)
                    {

                        pnlPayType1.Enabled = true;
                        pnlPayType2.Enabled = true;
                        int orderCount = cart.orderpricedetails.Length;
                        if (orderCount == 0)
                        {

                        }
                        else
                        {
                            for (int i = 0; i < orderCount; i++)
                            {
                                dgvOrderDetail.Rows.Add(cart.orderpricedetails[i].title, cart.orderpricedetails[i].amount);
                                dgvOrderDetail.ClearSelection();
                            }
                        }
                        lblPrice.Text = cart.totalpayment.ToString("f2");

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
                                //TODO 下划线

                                Product pro = cart.products[i];

                                string barcode = "\r\n  " + pro.title + "\r\n  " + pro.barcode;
                                string price = "";
                                string jian = "";

                                string num = "";
                                string add = "";
                                string total = "";
                                switch (pro.pricetagid)
                                {
                                    case 1: barcode = "1"+pro.pricetag + "\r\n  " + pro.title + "\r\n  " + pro.barcode; break;
                                    case 2: barcode = "2" + pro.pricetag + "\r\n  " + pro.title + "\r\n  " + pro.barcode; break;
                                    case 3: barcode = "3" + pro.pricetag + "\r\n  " + pro.title + "\r\n  " + pro.barcode; break;
                                    case 4: barcode = "4" + pro.pricetag + "\r\n  " + pro.title + "\r\n  " + pro.barcode; break;
                                }

                                if (pro.price.saleprice == pro.price.originprice)
                                {
                                    price = pro.price.saleprice.ToString("f2");
                                }
                                else
                                {
                                    //price = "￥" + pro.price.saleprice.ToString() + "("+pro.price.salepricedesc+")" + "\r\n" + "￥" + pro.price.originprice + "("+pro.price.originpricedesc+")";

                                    price = pro.price.saleprice.ToString("f2");
                                    if (!string.IsNullOrWhiteSpace(pro.price.salepricedesc))
                                    {
                                        price += "(" + pro.price.salepricedesc + ")";
                                    }

                                    if (pro.price.strikeout == 1)
                                    {
                                        price += "\r\n" +"strikeout"+ pro.price.originprice.ToString("f2");
                                    }
                                    else
                                    {
                                        price += "\r\n" + pro.price.originprice.ToString("f2");
                                    }
                                   
                                    if (!string.IsNullOrWhiteSpace(pro.price.originpricedesc))
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

                                    if (!string.IsNullOrWhiteSpace(pro.price.salepricedesc))
                                    {
                                        total += "(" + pro.price.salepricedesc + ")";
                                    }
                                    total += "\r\n" + pro.price.origintotal.ToString("f2");
                                    if (!string.IsNullOrWhiteSpace(pro.price.originpricedesc))
                                    {
                                        total += "(" + pro.price.originpricedesc + ")";
                                    }


                                }

                                dgvGood.Rows.Insert(0, new object[] { barcode, price, jian, num, add, total });
                            }
                            dgvGood.ClearSelection();

                            if (cart.pointpayoption == 1 && cart.totalpayment == 0)
                            {
                                pnlPayType1.Visible = false;
                                pnlPayType2.Visible = true;
                            }

                        }
                    }
                    else
                    {
                        pnlPayType1.Enabled = false;
                        pnlPayType2.Enabled = false;
                    }

                    CurrentCart = cart;
                    if (MainModel.CurrentMember != null && btnCheckJF.Text == "√" && CurrentCart.pointinfo != null)
                    {
                        lblJFUse.Text = "使用" + CurrentCart.pointinfo.availablepoints + "积分 抵用" + CurrentCart.pointinfo.availablepointsamount + "元";
                        lblJFUse.BackColor = Color.Moccasin;
                        //lblJFUse.Visible = true;
                    }

                    if (CurrentCart.availablecoupons != null && CurrentCart.availablecoupons.Length > 0 && pnlPayType2.Visible == false)
                    {
                        lblCoupon.Visible = true;
                        lblCouponStr.Visible = true;

                        if (CurrentCart.couponpromoamt > 0)
                        {
                            lblCoupon.Text = "-￥" + CurrentCart.couponpromoamt+">";

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
                    UpdateOrderHang();
                    MainModel.frmmainmedia.UpdateForm(CurrentCart, MainModel.CurrentMember);

                }));


                Application.DoEvents();

            }
            catch (Exception ex)
            {
                ShowLog("更新显示列表异常" + ex.Message, false);
            }
        }

        private void ClearForm()
        {
            //lblTotalPrice.Text = "￥0.00";
            //lblPromoamt.Text = "-￥0.00";

            dgvGood.Rows.Clear();
            lblPrice.Text = "0.00";
            lblGoodsCount.Text = "(0件商品)";

            pnlWaiting.Show();

            lstscancode.Clear();

            //pnlOrderDetail.Controls.Clear();
            //pnlOrderDetail.Controls.Add(pnlOrderIni);
            //pnlOrderIni.Show();

            dgvOrderDetail.Rows.Clear();
            CurrentCart = new Cart();

            //lblJF.Text = "0";
            //lblJFUse.Text = "";
            //MainModel.CurrentMember = null;
            //MainModel.CurrentCouponCode = "";
            //btnCheckJF.Text = "";
            //pnlWaitingMember.Visible = true;
            //pnlMember.Visible = false;


            pnlPayType1.Visible = true;
            pnlPayType2.Visible = false;

            UpdateOrderHang();

            MainModel.frmmainmedia.IniForm();
        }

        #endregion


        #region  会员积分优惠券


        private void btnLoadPhone_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                MainModel.frmmainmedia.ShowNumber();
                frmNumber frmnumber = new frmNumber("请输入会员号", true, false);
                frmnumber.DataReceiveHandle += FormPhone_DataReceiveHandle;
                frmnumber.Opacity = 0.95d;
                frmnumber.frmNumber_SizeChanged(null, null);
                frmnumber.Size = new System.Drawing.Size(this.Width * 33 / 100, this.Height * 70 / 100);
                frmnumber.Location = new System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width - frmnumber.Width - 40, this.Height * 15 / 100);
                // frmnumber.TopMost = true;
                frmnumber.ShowDialog();

                //RefreshCart();
                Application.DoEvents();
                this.Enabled = true;
                toolStripMain.Focus();
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
                            ClearMember();
                            ShowLog(ErrorMsgMember, false);
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
                this.Enabled = true;
                LoadingHelper.CloseForm();//关闭


            }

        }


        private void LoadMember(Member member)
        {
            try
            {
                this.Enabled = false;
                bool testbooo = this.Enabled;
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
                toolStripMain.Focus();
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
                lblJF.Text = "0";
                lblJFUse.Text = "";
                lblJFUse.BackColor = Color.White;
                //lblJFUse.Visible = false;
                MainModel.CurrentMember = null;
                MainModel.CurrentCouponCode = "";
                //chkJF.Checked = false;
                btnCheckJF.Text = "";
                pnlWaitingMember.Visible = true;
                pnlMember.Visible = false;


                //string ErrorMsgCart = "";
                //int ResultCode = 0;
                //toolStripMain.Focus();
                //if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                //{
                //    //CurrentCart.pointpayoption = 1;

                //    CurrentCart.pointpayoption = btnCheckJF.Text == "√" ? 1 : 0;

                //    Cart cart = httputil.RefreshCart(CurrentCart, ref ErrorMsgCart, ref ResultCode);
                //    CurrentCart = cart;
                //    if (ErrorMsgCart != "" || cart == null) //商品不存在或异常
                //    {
                //        ShowLog(ErrorMsgCart, false);

                //        CheckUserAndMember(ResultCode);
                //        //return false;
                //    }
                //    else
                //    {

                //        UploadDgvGoods(cart);


                //        if (btnCheckJF.Text == "√")
                //        {
                //            if (MainModel.CurrentMember != null && CurrentCart != null && CurrentCart.pointinfo != null)
                //            {
                //                lblJF.Text = CurrentCart.pointinfo.totalpoints;
                //                lblJFUse.Visible = true;
                //                lblJFUse.Text = "使用" + CurrentCart.pointinfo.availablepoints + "积分 抵用" + CurrentCart.pointinfo.availablepointsamount + "元";

                //            }
                //        }
                //        else
                //        {
                //            //lblJF.Text = "0";
                //            lblJFUse.Visible = false;
                //            lblJFUse.Text = "";
                //        }



                //        btnPayByBalance.Enabled = CurrentCart.paymenttypes.balancepayenabled == 1;
                //        btnPayByCash.Enabled = CurrentCart.paymenttypes.cashenabled == 1;
                //        btnPayOnLine.Enabled = CurrentCart.paymenttypes.onlineenabled == 1;

                //       // return true;
                //    }

                //}
                //else
                //{
                //    ClearForm();
                //    //ClearMember();
                //    //return true;
                //}


                // RefreshCart();
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

            frmCoupon frmcoupon = new frmCoupon(CurrentCart, MainModel.CurrentCouponCode);
            frmcoupon.Opacity = 0.95d;

            frmcoupon.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            frmcoupon.ShowDialog();

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
                    CheckUserAndMember(ResultCode);
                    ShowLog("异常" + ErrorMsg, true);
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

        }
        #endregion


        #region 页面委托时间

        private void FormOnLinePayResult_DataReceiveHandle(int type, string orderid)
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
                MainModel.frmmainmedia.UpdateForm(CurrentCart, MainModel.CurrentMember);
            }
            // btnUSB.Focus();

        }


        //0 需要微信支付宝继续支付  2、现金支付完成
        private void FormCash_DataReceiveHandle(int type, string orderid)
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
                CheckUserAndMember(type);
            }

            if (CurrentCart != null)
            {
                CurrentCart.cashpayoption = 0;
                CurrentCart.cashpayamt = 0;
            }
            RefreshCart();

        }


        private void FormCashCoupon_DataReceiveHandle(int type, string orderid)
        {
            if (type == MainModel.HttpUserExpired || type == MainModel.HttpMemberExpired)
            {
                CheckUserAndMember(type);
            }

            if (CurrentCart != null)
            {
                CurrentCart.cashcouponamt = 0;
            }
            //RefreshCart();

        }

        private void FormCashierResult_DataReceiveHandle(int type, string payinfo)
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
                    this.Invoke(new InvokeHandler(delegate()
                    {
                        if (goodscode.Length == 13)
                        {
                            QueueScanCode.Enqueue(goodscode);
                        }
                        else
                        {
                            QueueShortCode.Enqueue(goodscode);
                        }
                        
                    }));

                }
            }
            catch (Exception ex)
            {
                //LoadingHelper.CloseForm();//关闭
                LogManager.WriteLog("ERROR", "条码数据处理异常：" + ex.Message);
            }
            finally
            {
                this.Enabled = true;
                toolStripMain.Focus();
                LoadingHelper.CloseForm();//关闭


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


        private void FormOrderHang_DataReceiveHandle(int type, Cart Cart)
        {
            try
            {

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
                    lstscancode.Add(tempscancode);
                    //QueueScanCode.Enqueue(pro.barcode);
                }

                UploadDgvGoods(Cart);

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

                        CheckUserAndMember(ResultCode);
                        ShowLog(ErrorMsgCart, false);
                    }
                    else
                    {
                        CurrentCart = cart;
                        string ErrorMsg = "";
                        // int ResultCode = 0;
                        CreateOrderResult orderresult = httputil.CreateOrder(CurrentCart, ref ErrorMsg, ref ResultCode);
                        if (ResultCode != 0 || orderresult == null)
                        {
                            CheckUserAndMember(ResultCode);
                            ShowLog("异常" + ErrorMsg, true);
                        }
                        else if (orderresult.continuepay == 1)
                        {
                            frmonlinepayresult = new frmOnLinePayResult(orderresult.orderid);

                            frmonlinepayresult.frmOnLinePayResult_SizeChanged(null, null);
                            //frmonlinepayresult.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width / 3, SystemInformation.WorkingArea.Height / 2);
                            frmonlinepayresult.Location = new System.Drawing.Point((Screen.PrimaryScreen.Bounds.Width - frmonlinepayresult.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - frmonlinepayresult.Height) / 2);

                            frmonlinepayresult.DataReceiveHandle += FormOnLinePayResult_DataReceiveHandle;
                            frmonlinepayresult.ShowDialog();
                            //frmonlinepayresult.DataReceiveHandle -= FormOnLinePayResult_DataReceiveHandle;
                            frmonlinepayresult = null;
                        }
                    }

                    listener.Start();
                    this.Enabled = true;
                }

                Application.DoEvents();
                toolStripMain.Focus();
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

                    Application.DoEvents();

                    listener.Start();
                    this.Enabled = true;

                    toolStripMain.Focus();
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

                toolStripMain.Focus();
            }


        }


        //余额支付
        private void btnPayByBalance_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                listener.Stop();
                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                {
                    string ErrorMsgCart = "";
                    int ResultCode = 0;
                    Cart cart = httputil.RefreshCart(CurrentCart, ref ErrorMsgCart, ref ResultCode);
                    CurrentCart = cart;
                    if (ErrorMsgCart != "" || cart == null) //商品不存在或异常
                    {
                        ShowLog(ErrorMsgCart, false);

                        CheckUserAndMember(ResultCode);
                        ShowLog(ErrorMsgCart, false);
                    }
                    else
                    {
                        CurrentCart = cart;
                        string ErrorMsg = "";
                        //int ResultCode = 0;
                        CreateOrderResult orderresult = httputil.CreateOrder(CurrentCart, ref ErrorMsg, ref ResultCode);
                        if (ResultCode != 0 || orderresult == null)
                        {
                            CheckUserAndMember(ResultCode);
                            ShowLog("异常" + ErrorMsg, true);
                        }
                        else if (orderresult.continuepay == 1)
                        {
                            frmBalancePayResult frmbalancepayresult = new frmBalancePayResult(orderresult.orderid);

                            //frmbalancepayresult.frmOnLinePayResult_SizeChanged(null, null);
                            //frmbalancepayresult.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width / 3, SystemInformation.WorkingArea.Height / 2);
                            frmbalancepayresult.Location = new System.Drawing.Point((Screen.PrimaryScreen.Bounds.Width - frmbalancepayresult.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - frmbalancepayresult.Height) / 2);

                            frmbalancepayresult.DataReceiveHandle += FormOnLinePayResult_DataReceiveHandle;
                            frmbalancepayresult.ShowDialog();
                            frmbalancepayresult = null;
                        }
                    }
                }
                Application.DoEvents();

                listener.Start();
                this.Enabled = true;
                toolStripMain.Focus();
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
                toolStripMain.Focus();
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
                    if (frmcashcoupon.DialogResult == DialogResult.OK)
                    {
                        ClearForm();
                        ClearMember();
                    }
                    else
                    {
                        RefreshCart();
                    }
                    Application.DoEvents();

                    toolStripMain.Focus();
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
                toolStripMain.Focus();
            }

        }


        private void btnPayOK_Click(object sender, EventArgs e)
        {
            string ErrorMsg = "";
            int ResultCode = 0;
            CreateOrderResult orderresult = httputil.CreateOrder(CurrentCart, ref ErrorMsg, ref ResultCode);
            if (ResultCode != 0 || orderresult == null)
            {
                CheckUserAndMember(ResultCode);
                ShowLog("异常" + ErrorMsg, true);
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
                string barcode = strs[strs.Length - 1].Trim();
                string proname = strs[strs.Length - 2].Trim();
                //增加标品
                if (dgvGood.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "+")
                {

                    for (int i = 0; i < CurrentCart.products.Count; i++)
                    {
                        if (CurrentCart.products[i].barcode == barcode)
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
                        if (CurrentCart.products[i].barcode == barcode)
                        {

                            if (CurrentCart.products[i].num == 1)
                            {
                                this.Enabled = false;
                                frmDeleteGood frmdelete = new frmDeleteGood("是否确认删除商品？", proname, barcode);
                                this.Enabled = true;
                                if (frmdelete.ShowDialog() == DialogResult.OK)
                                {
                                    CurrentCart.products.RemoveAt(i);
                                }
                                
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
                    frmDeleteGood frmdelete = new frmDeleteGood("是否确认删除商品？", proname, barcode);
                    if (frmdelete.ShowDialog() != DialogResult.OK)
                    {
                        this.Enabled = true;
                        return;
                    }
                    this.Enabled = true;

                    foreach (Product pro in CurrentCart.products)
                    {
                        if (pro.barcode == barcode)
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

        private void frmMain_Click(object sender, EventArgs e)
        {
            toolStripMain.Focus();
        }




        private void control_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Control con = (Control)sender;
                Color co = con.BackColor;
                if (con.Tag == null)
                {
                    con.Tag = co;
                }

                //con.BackColor = MainBackColor;
                Draw(e.ClipRectangle, e.Graphics, Math.Min(Math.Min(con.Width / 5, con.Height / 5), 12), false, Color.DodgerBlue, Color.DodgerBlue);
                //base.OnPaint(e);
                Graphics g = e.Graphics;

                Font fnt2 = con.Font;
                SizeF size2 = this.CreateGraphics().MeasureString(con.Text, fnt2);

                g.DrawString(con.Text, con.Font, new SolidBrush(Color.White), new PointF((con.Width - size2.Width) / 2, (con.Height - size2.Height) / 2));
                // con.Paint -= control_Paint;
                //con.Paint += null;
            }
            catch (Exception ex)
            {

            }
        }


        private void panel_Paint(object sender, PaintEventArgs e)
        {
            Control con = (Control)sender;
            //Color co = con.BackColor;
            //if (con.Tag == null)
            //{
            //    con.Tag = co;
            //}

            //con.BackColor = MainBackColor;
            Draw(e.ClipRectangle, e.Graphics, Math.Min(Math.Min(con.Width / 5, con.Height / 5), 12), false, Color.White, Color.White);
            //base.OnPaint(e);
            Graphics g = e.Graphics;

            Font fnt2 = con.Font;
            SizeF size2 = this.CreateGraphics().MeasureString(con.Text, fnt2);

            g.DrawString(con.Text, con.Font, new SolidBrush(Color.White), new PointF((con.Width - size2.Width) / 2, (con.Height - size2.Height) / 2));
            // con.Paint -= control_Paint;
            //con.Paint += null;
        }

        private void Draw(Rectangle rectangle, Graphics g, int _radius, bool cusp, Color begin_color, Color end_color)
        {
            try
            {
                int span = 2;
                //抗锯齿
                g.SmoothingMode = SmoothingMode.AntiAlias;
                //渐变填充
                LinearGradientBrush myLinearGradientBrush = new LinearGradientBrush(rectangle, begin_color, end_color, LinearGradientMode.Vertical);
                //画尖角
                if (cusp)
                {
                    span = 10;
                    PointF p1 = new PointF(rectangle.Width - 12, rectangle.Y + 10);
                    PointF p2 = new PointF(rectangle.Width - 12, rectangle.Y + 30);
                    PointF p3 = new PointF(rectangle.Width, rectangle.Y + 20);
                    PointF[] ptsArray = { p1, p2, p3 };
                    g.FillPolygon(myLinearGradientBrush, ptsArray);
                }
                //填充
                g.FillPath(myLinearGradientBrush, DrawRoundRect(rectangle.X, rectangle.Y, rectangle.Width - span, rectangle.Height - 1, _radius));

            }
            catch (Exception ex)
            {

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
                    //picScreen.BackgroundImage = TransparentImage(GetWinformImage(), (float)0.15);
                    picScreen.Size = new System.Drawing.Size(this.Width, this.Height);
                    //picScreen.Location = new System.Drawing.Point(0, 0);
                    picScreen.Visible = true;
                    // this.Opacity = 0.9d;
                }
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
            //PrintUtil.OpenCashBox();
        }







        //重绘datagridview单元格
        private void dgvGood_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

            if (e.ColumnIndex == 0 && e.RowIndex >= 0 && e.Value != null)//要进行重绘的单元格
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
                    strLine1 =strLine1.Substring(1,strLine1.Length-1);
                    switch (typecolor)
                    {
                        case "1": titlebackcolor = ColorTranslator.FromHtml("#FF7D14"); break;
                        case "2": titlebackcolor = ColorTranslator.FromHtml("#209FD4"); break;
                        case "3": titlebackcolor = ColorTranslator.FromHtml("#D42031"); break;
                        case "4": titlebackcolor = ColorTranslator.FromHtml("#FF000"); break;
                    }
                }
                //第一行
                TextRenderer.DrawText(e.Graphics, strLine1, fnt1, new Rectangle(intX+10, intY, intWidth, (int)size1.Height),
                    Color.White,titlebackcolor, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);

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
                int y = (e.RowIndex+1) * dgvGood.RowTemplate.Height + dgvGood.ColumnHeadersHeight-1;

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

            if ((e.ColumnIndex == 1 || e.ColumnIndex == 5) && e.RowIndex >= 0 && e.Value != null)//要进行重绘的单元格
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

                   string tempstrline11=strLine1.Substring(0,index);
                   string tempstrline12 = strLine1.Substring(index);

                   SizeF siztemp1 = this.CreateGraphics().MeasureString(tempstrline11, fnt1);
                   SizeF sizetemp2 = this.CreateGraphics().MeasureString(tempstrline12, fnt1);

                   int pianyiX =(int) (e.CellBounds.Width - siztemp1.Width - sizetemp2.Width) / 2;
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
                    
                   TextRenderer.DrawText(e.Graphics, tempstrline12, tempfont2, new Rectangle(intX+(int)siztemp1.Width+pianyiX, intY+5, intWidth, (int)siztemp1.Height),
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
                bool isstrickout=false;
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
                    int index = strLine1.IndexOf("(");

                    string tempstrline21 = strLine2.Substring(0, index);
                    string tempstrline22 = strLine2.Substring(index);

                    SizeF siztemp1 = this.CreateGraphics().MeasureString(tempstrline21, fnt2);
                    SizeF sizetemp2 = this.CreateGraphics().MeasureString(tempstrline22, fnt2);

                    int pianyiX = (int)(e.CellBounds.Width - siztemp1.Width - sizetemp2.Width) / 2;
                    //第一行
                    TextRenderer.DrawText(e.Graphics, tempstrline21, fnt2, new Rectangle(intX + pianyiX, intY, intWidth, (int)siztemp1.Height),
                        Color.Black, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);

                    Font tempfont2 = new System.Drawing.Font("微软雅黑", 10F * Math.Min(MainModel.hScale, MainModel.wScale));

                    TextRenderer.DrawText(e.Graphics, tempstrline22, fnt2, new Rectangle(intX+(int)siztemp1.Width+pianyiX, intY+5, intWidth, (int)size2.Height),
                    Color.DimGray, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);


                }
                else
                {

                    TextRenderer.DrawText(e.Graphics, strLine2, fnt2, new Rectangle(intX+(int) (e.CellBounds.Width - size2.Width)/2, intY, intWidth, (int)size2.Height),
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




    }
}
