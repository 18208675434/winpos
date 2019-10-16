using QiandamaPOS.Common;
using QiandamaPOS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        /// <summary>
        /// 页面宽度缩放比例
        /// </summary>
        float wScale = 1;
        /// <summary>
        /// 页面高度缩放比例
        /// </summary>
        float hScale = 1;
        #endregion


        #region  页面加载
        public frmMain()
        {
            InitializeComponent();
            wScale =(float) Screen.PrimaryScreen.Bounds.Width / this.Width;
            hScale=(float)SystemInformation.WorkingArea.Height/this.Height;
            Control.CheckForIllegalCrossThreadCalls = false;
            listener.ScanerEvent += Listener_ScanerEvent;
            LoadingHelper.CloseForm();
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            //string width = Screen.AllScreens[0].Bounds.Width.ToString();
            //string height = Screen.AllScreens[0].Bounds.Height.ToString();
            //MessageBox.Show(width + "  " + height + "  " + Screen.PrimaryScreen.Bounds.Width + "  " + this.Height);

            listener.Start();

            timerNow.Interval = 1000;
            timerNow.Enabled = true;

            timerClearMemory.Interval = 2 * 60 * 1000;
            timerClearMemory.Enabled = true;

            lblShopName.Text = MainModel.CurrentShopInfo.shopname;
            dpbtnMenu.Text = MainModel.CurrentUser.nickname + ",你好▼";

            //UpdateOrderHang();

            //启动扫描处理线程
            Thread threadItemExedate = new Thread(ScanCodeExe);
            threadItemExedate.IsBackground = true;
            threadItemExedate.Start();


            //启动扫描处理线程
            Thread threadShortCodeExedate = new Thread(InputShortCodeExe);
            threadShortCodeExedate.IsBackground = true;
            threadShortCodeExedate.Start();

            toolStripMain.Focus();
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
        #endregion


        #region 菜单按钮



        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

            frmDeleteGood frmdelete = new frmDeleteGood("是否确认退出系统？", "", "");
            if (frmdelete.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            System.Environment.Exit(0);
        }

        private void btnOrderQuery_Click(object sender, EventArgs e)
        {
            frmOrderQuery frmorderquery = new frmOrderQuery();

            asf.AutoScaleControlTest(frmorderquery, Screen.PrimaryScreen.Bounds.Width, SystemInformation.WorkingArea.Height - toolStripMain.Height,true);
            //frmorderquery.frmOrderQuery_SizeChanged(null, null);
            //frmorderquery.Size = new System.Drawing.Size( Screen.PrimaryScreen.Bounds.Width,SystemInformation.WorkingArea.Height - toolStripMain.Height);

            frmorderquery.Location = new System.Drawing.Point(0, toolStripMain.Height);

            frmorderquery.ShowDialog();
        }

        private void btnOrderCancle_Click(object sender, EventArgs e)
        {
            if (CurrentCart == null || CurrentCart.products == null || CurrentCart.products.Count <= 0)
            {
                return;
            }

            frmDeleteGood frmdelete = new frmDeleteGood("是否确认取消订单？", "", "");
            if (frmdelete.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            ReceiptUtil.EditCancelOrder(1, CurrentCart.totalpayment);
            ClearForm();
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
                        frmDeleteGood frmdelete = new frmDeleteGood("确认挂单？", "", "");
                        if (frmdelete.ShowDialog() != DialogResult.OK)
                        {
                            return;
                        }

                        SerializeOrder(CurrentCart);

                        ShowLog("挂单成功", false);
                        ClearForm();

                    }
                }
                else if (btnOrderHang.Text == "挂单列表")
                {
                    frmOrderHang frmorderhang = new frmOrderHang();
                    frmorderhang.DataReceiveHandle += FormOrderHang_DataReceiveHandle;
                    frmorderhang.Height = this.Height - toolStripMain.Height;
                    frmorderhang.Width = Screen.PrimaryScreen.Bounds.Width;
                    frmorderhang.Location = new System.Drawing.Point(0, toolStripMain.Height);

                    frmorderhang.ShowDialog();
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


        //更新挂单按钮  购物车没有商品且有挂单信息时 按钮text="挂单列表"   按钮点击事件根据文本判断事件
        private void UpdateOrderHang()
        {
            try
            {
                btnOrderHang.Text = "挂单列表";
                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                {
                    btnOrderHang.Text = "挂单";
                    //BinaryFormatter formatter = new BinaryFormatter();
                    //DirectoryInfo di = new DirectoryInfo(MainModel.OrderPath);
                    //List<FileInfo> fList = di.GetFiles().ToList();
                    //for (int i = 0; i < fList.Count; i++)
                    //{
                    //    if (fList[i].Name.Contains(".order"))
                    //    {
                    //        btnOrderHang.Text = "挂单";
                    //        break;
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("更新挂单按钮异常" + ex.Message);
            }
        }

        //交班
        private void tsmReceipt_Click(object sender, EventArgs e)
        {
            frmDeleteGood frmdelete = new frmDeleteGood("确认交班", "点击确认后，收银机将自动打印交班表单", "");
            if (frmdelete.ShowDialog() != DialogResult.OK)
            {
                return;
            }

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

                    System.Environment.Exit(0);
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
            frmreceiptquery.Height = this.Height - toolStripMain.Height;
            frmreceiptquery.Width = Screen.PrimaryScreen.Bounds.Width;
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

            asf.AutoScaleControlTest(frmexpense, Screen.PrimaryScreen.Bounds.Width, SystemInformation.WorkingArea.Height - toolStripMain.Height,true);

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
                frmNumber frmnumber = new frmNumber("请输入商品条码", false);
                frmnumber.DataReceiveHandle += FormGoodsCode_DataReceiveHandle;
                frmnumber.Opacity = 0.95d;
                frmnumber.frmNumber_SizeChanged(null, null);
                frmnumber.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width / 3, this.Height - 200);
                frmnumber.Location = new System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width - frmnumber.Width - 50, 100);
                frmnumber.Show();

                Application.DoEvents();

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
                toolStripMain.Focus();
                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                {
                    //CurrentCart.pointpayoption = 1;

                    CurrentCart.pointpayoption = chkJF.Checked ? 1 : 0;

                    Cart cart = httputil.RefreshCart(CurrentCart, ref ErrorMsgCart);
                    CurrentCart = cart;
                    if (ErrorMsgCart != "" || cart == null) //商品不存在或异常
                    {
                        ShowLog(ErrorMsgCart, false);
                        return false;
                    }
                    else
                    {

                        UploadDgvGoods(cart);


                        if (chkJF.Checked)
                        {
                            if (MainModel.CurrentMember != null && CurrentCart != null && CurrentCart.pointinfo != null)
                            {
                                lblJF.Text = CurrentCart.pointinfo.totalpoints;
                                lblJFUse.Visible = true;
                                lblJFUse.Text = "使用" + CurrentCart.pointinfo.availablepoints + "积分 抵用" + CurrentCart.pointinfo.availablepointsamount + "元";

                            }
                        }
                        else
                        {
                            //lblJF.Text = "0";
                            lblJFUse.Visible = false;
                            lblJFUse.Text = "";
                        }



                        btnPayByBalance.Enabled = CurrentCart.paymenttypes.balancepayenabled == 1;
                        btnPayByCash.Enabled = CurrentCart.paymenttypes.cashenabled == 1;
                        btnPayOnLine.Enabled = CurrentCart.paymenttypes.onlineenabled == 1;
                        
                        return true;
                    }

                }
                else
                {
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


        private void ScanCodeExe()
        {
            while (true)
            {

                //有数据就处理
                if (QueueScanCode.Count > 0)
                {
                    try
                    {
                        LoadingHelper.ShowLoadingScreen();//显示
                        this.Enabled = false;

                        string goodcode = QueueScanCode.Dequeue();

                        string ErrorMsg = "";
                        scancodememberModel scancodemember = httputil.GetSkuInfoMember(goodcode, ref ErrorMsg);

                        if (ErrorMsg != "" || scancodemember == null) //商品不存在或异常
                        {
                            ShowLog(ErrorMsg, false);
                        }
                        else
                        {
                            if (scancodemember.type == "MEMBER")
                            {
                                //LoadMember(scancodemember.memberresponsevo);
                            }
                            else
                            {
                                addcart(scancodemember);
                            }
                            
                        }

                    }
                    catch (Exception ex)
                    {
                        //LoadingHelper.CloseForm();//关闭
                        LogManager.WriteLog("ERROR", "扫描数据处理异常：" + ex.Message);
                    }
                    finally
                    {
                        this.Enabled = true;
                        LoadingHelper.CloseForm();//关闭
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
                if (QueueShortCode .Count > 0)
                {
                    try
                    {
                        LoadingHelper.ShowLoadingScreen();//显示
                        this.Enabled = false;

                        string goodcode = QueueShortCode.Dequeue();

                        string ErrorMsg = "";
                        scancodememberModel scancodemember = httputil.GetSkuInfoByShortCode(goodcode, ref ErrorMsg);

                        if (ErrorMsg != "" || scancodemember == null) //商品不存在或异常
                        {
                            ShowLog(ErrorMsg, false);
                        }
                        else
                        {                            
                                addcart(scancodemember);
                        }

                    }
                    catch (Exception ex)
                    {
                        //LoadingHelper.CloseForm();//关闭
                        LogManager.WriteLog("ERROR", "扫描数据处理异常：" + ex.Message);
                    }
                    finally
                    {
                        this.Enabled = true;
                        LoadingHelper.CloseForm();//关闭
                    }
                }
                else
                {
                    Thread.Sleep(1);
                }

            }
        }

        private void addcart(scancodememberModel scancodemember )
        {
            if (true)
            //if (scancodemember.scancodedto.weightflag) //称重商品
            {
                if (scancodemember.scancodedto.specnum == 0)
                {
                    frmNumber frmnumber = new frmNumber(scancodemember.scancodedto.skuname, false);
                    frmnumber.Opacity = 0.95d;
                    frmnumber.frmNumber_SizeChanged(null, null);
                    frmnumber.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width / 3, this.Height - 200);
                    frmnumber.Location = new System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width - frmnumber.Width - 50, 100);

                    frmnumber.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                    frmnumber.ShowDialog();

                    if (frmnumber.DialogResult == DialogResult.OK)
                    {
                        scancodemember.scancodedto.specnum = (decimal)frmnumber.NumValue / 1000;
                        scancodemember.scancodedto.num = 1;
                    }
                    else
                    {
                        return;
                    }
                }

                Product pro = new Product();
                pro.skucode = scancodemember.scancodedto.skucode;
                pro.num = scancodemember.scancodedto.num;
                pro.specnum = scancodemember.scancodedto.specnum;
                pro.spectype = scancodemember.scancodedto.spectype;
                pro.goodstagid = scancodemember.scancodedto.weightflag == true ? 1 : 0;

                pro.barcode = scancodemember.scancodedto.barcode;

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

                bool IsExits = false;


                lstscancode.Add(scancodemember);
                Cart cart = httputil.RefreshCart(CurrentCart, ref ErrorMsgCart);
                if (ErrorMsgCart != "" || cart == null) //商品不存在或异常
                {
                    ShowLog(ErrorMsgCart, false);
                    lstscancode.Remove(scancodemember);
                }
                else
                {
                    UploadDgvGoods(cart);
                }
            }
        }



        //TODO  修改样式
        private void ShowLog(string msg, bool iserror)
        {
            MainModel.ShowLog(msg,iserror);
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
                    if (cart != null && cart.products!=null && cart.products.Count>0)
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
                                dgvOrderDetail.ClearSelection();
                            }
                        }
                        lblPrice.Text = cart.totalpayment.ToString("f2");

                        int count = cart.products.Count;
                        lblGoodsCount.Text = count.ToString();
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

                                string barcode = pro.title + "\r\n" + pro.barcode;
                                string price = "";
                               string jian="";

                               string num = "";
                                string add = "";
                                string total = "";
                                switch (pro.pricetagid)
                                {
                                    case 1: barcode = "会员价" + "\r\n" + pro.title + "\r\n" + pro.barcode; break;
                                    case 2: barcode = "折扣" + "\r\n" + pro.title + "\r\n" + pro.barcode; break;
                                    case 3: barcode = "直降" + "\r\n" + pro.title + "\r\n" + pro.barcode; break;
                                }

                                if (pro.price.saleprice == pro.price.originprice)
                                {
                                    price ="￥"+ pro.price.saleprice.ToString();
                                }
                                else
                                {
                                    price = "￥" + pro.price.saleprice.ToString() + pro.price.salepricedesc + "\r\n" + "￥" + pro.price.originprice + pro.price.originpricedesc;
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
                                    total = "￥" + pro.price.total.ToString();
                                }
                                else
                                {
                                    price = "￥" + pro.price.total.ToString() + pro.price.salepricedesc + "\r\n" + "￥" + pro.price.origintotal + pro.price.originpricedesc;
                                }

                                dgvGood.Rows.Add(barcode, price, jian , num, add, total);

                                dgvGood.ClearSelection();

                            }

                            if (cart.pointpayoption == 1 && cart.totalpayment == 0)
                            {
                                pnlPayType1.Visible = false;
                                pnlPayType2.Visible = true;
                            }

                        }

                    }
                    CurrentCart = cart;
                    if (MainModel.CurrentMember != null && chkJF.Checked && CurrentCart.pointinfo != null)
                    {
                        lblJFUse.Text = "使用" + CurrentCart.pointinfo.availablepoints + "积分 抵用" + CurrentCart.pointinfo.availablepointsamount + "元";
                    }

                    if (CurrentCart.availablecoupons != null && CurrentCart.availablecoupons.Length > 0)
                    {
                        lblCoupon.Visible = true;
                        lblCouponStr.Visible = true;

                        if (CurrentCart.couponpromoamt > 0)
                        {
                            lblCoupon.Text = "-￥" + CurrentCart.couponpromoamt;

                        }
                        else
                        {
                            lblCoupon.Text = CurrentCart.availablecoupons.Length + "张可用>>";

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
            lblGoodsCount.Text = "0";

            pnlWaiting.Show();

            lstscancode.Clear();

            //pnlOrderDetail.Controls.Clear();
            //pnlOrderDetail.Controls.Add(pnlOrderIni);
            //pnlOrderIni.Show();

            dgvOrderDetail.Rows.Clear();
            CurrentCart = new Cart();

            lblJF.Text = "0";
            lblJFUse.Text = "";
            MainModel.CurrentMember = null;
            MainModel.CurrentCouponCode = "";
            chkJF.Checked = false;
            pnlWaitingMember.Visible = true;
            pnlMember.Visible = false;

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
                frmNumber frmnumber = new frmNumber("请输入会员号", true);
                frmnumber.DataReceiveHandle += FormPhone_DataReceiveHandle;
                frmnumber.Opacity = 0.95d;
                frmnumber.frmNumber_SizeChanged(null, null);
                frmnumber.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width / 3, SystemInformation.WorkingArea.Height - 200);
                frmnumber.Location = new System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width - frmnumber.Width - 50, 100);
               // frmnumber.TopMost = true;
                frmnumber.ShowDialog();

                Application.DoEvents();
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

                            pnlWaitingMember.Visible = true;
                            pnlMember.Visible = false;
                            MainModel.CurrentMember = null;
                            ShowLog(ErrorMsgMember, false);
                        }
                        else
                        {
                            LoadMember(member);


                        }

                    }));

                }
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
                pnlWaitingMember.Visible = false;
                pnlMember.Visible = true;

                lblMobil.Text = member.memberheaderresponsevo.mobile;
                lblWechartNickName.Text = member.memberinformationresponsevo.wechatnickname;

                MainModel.CurrentMember = member;

                lblJF.Text = member.creditaccountrepvo.availablecredit.ToString();
                chkJF.Checked = false;

                lblCoupon.Visible = false;
                lblCouponStr.Visible = false;

                //购物车有商品的话刷新一次
                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                {
                    RefreshCart();
                }
            }
            catch (Exception ex)
            {
                ShowLog("加载会员信息异常："+ex.Message,true);
            }
        }

        //退出会员
        private void lblExitMember_Click(object sender, EventArgs e)
        {
            frmDeleteGood frmdelete = new frmDeleteGood("是否确认退出会员？", "", "");
            if (frmdelete.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            lblJF.Text = "0";
            lblJFUse.Text = "";
            MainModel.CurrentMember = null;
            chkJF.Checked = false;
            pnlWaitingMember.Visible = true;
            pnlMember.Visible = false;
        }


        //是否使用积分抵扣
        private void chkJF_CheckedChanged(object sender, EventArgs e)
        {


            //TODO 刷新购物车接口

            try
            {

                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                {
                    RefreshCart();
                }
                else
                {
                    chkJF.Checked = false;
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
            bool RefreshCartOK= RefreshCart();


            //收银完成
            if (frmcoupon.DialogResult == DialogResult.Yes && RefreshCartOK)
            {
                string ErrorMsg = "";
                CreateOrderResult orderresult = httputil.CreateOrder(CurrentCart, ref ErrorMsg);
                if (orderresult == null)
                {
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

                }
            }

        }
        #endregion


        #region 页面委托时间

        private void FormGoodDel_DataReceiveHandle(int type, Product pro)
        {
            try
            {

                string barcode = pro.barcode;
                string proname = pro.title;


                if (type == 2)
                {

                    //if (MessageBox.Show("是否确认删除商品\r\n" + proname+barcode, "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                    //  != DialogResult.OK)
                    //    return;

                    frmDeleteGood frmdelete = new frmDeleteGood("是否确认删除商品？", proname, barcode);
                    if (frmdelete.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }

                    ReceiptUtil.EditCancelSingle(1, pro.price.total);

                    for (int i = 0; i < lstscancode.Count; i++)
                    {
                        if (lstscancode[i].scancodedto.barcode == barcode)
                        {
                            lstscancode.RemoveAt(i);
                            break;  //非标商品会多行同一个条码
                        }
                    }

                    string ErrorMsgCart = "";
                    Cart cart = httputil.RefreshCart(CurrentCart, ref ErrorMsgCart);
                    if (ErrorMsgCart != "" || cart == null) //商品不存在或异常
                    {
                        ShowLog(ErrorMsgCart, false);
                    }
                    else
                    {
                        UploadDgvGoods(cart);

                    }

                }
                else
                {
                    for (int i = 0; i < lstscancode.Count; i++)
                    {
                        if (lstscancode[i].scancodedto.barcode == barcode)
                        {
                            lstscancode[i].scancodedto.num = pro.num;
                            break;  //非标商品会多行同一个条码
                        }
                    }

                    string ErrorMsgCart = "";
                    Cart cart = httputil.RefreshCart(CurrentCart, ref ErrorMsgCart);
                    if (ErrorMsgCart != "" || cart == null) //商品不存在或异常
                    {
                        ShowLog(ErrorMsgCart, false);
                    }
                    else
                    {
                        UploadDgvGoods(cart);

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

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
                }));

            }
            else
            {
                MainModel.frmmainmedia.UpdateForm(CurrentCart,MainModel.CurrentMember);
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
                    frmonlinepayresult.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width / 3, this.Height - 200);
                    frmonlinepayresult.Location = new System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width - frmonlinepayresult.Width - 50, 100);

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
                }));
            }

            if (CurrentCart != null)
            {
                CurrentCart.cashpayoption = 0;
                CurrentCart.cashpayamt = 0;
            }
            RefreshCart();

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
                        QueueShortCode.Enqueue(goodscode);
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

                    Cart cart = httputil.RefreshCart(CurrentCart, ref ErrorMsgCart);
                    if (ErrorMsgCart != "" || cart == null) //商品不存在或异常
                    {
                        ShowLog(ErrorMsgCart, false);
                    }
                    else
                    {
                        CurrentCart = cart;
                        string ErrorMsg = "";
                        CreateOrderResult orderresult = httputil.CreateOrder(CurrentCart, ref ErrorMsg);
                        if (orderresult == null)
                        {
                            ShowLog("异常" + ErrorMsg, true);
                        }
                        else if (orderresult.continuepay == 1)
                        {
                            frmonlinepayresult = new frmOnLinePayResult(orderresult.orderid);

                            frmonlinepayresult.frmOnLinePayResult_SizeChanged(null, null);
                            frmonlinepayresult.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width / 3, SystemInformation.WorkingArea.Height/2);
                            //frmonlinepayresult.Location = new System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width - frmnumber.Width - 50, 100);

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
                ShowLog("在线收银异常"+ex.Message,true);
            }
        }

        //现金支付
        private void btnPayByCash_Click(object sender, EventArgs e)
        {
            try
            {
              
                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                {
                    this.Enabled = false;
                    listener.Stop();

                    CurrentCart.cashpayoption = 1;
                    if (!RefreshCart())
                    {
                        return;
                    }


                    frmCashPay frmcash = new frmCashPay(CurrentCart);
                    frmcash.frmCashPay_SizeChanged(null, null);
                    frmcash.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width / 3, this.Height - 200);
                    frmcash.Location = new System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width - frmcash.Width - 80, 100);

                    frmcash.CashPayDataReceiveHandle += FormCash_DataReceiveHandle;
                    frmcash.Opacity = 0.95d;
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

                    Cart cart = httputil.RefreshCart(CurrentCart,ref ErrorMsgCart);
                    if (ErrorMsgCart != "" || cart == null) //商品不存在或异常
                    {
                        ShowLog(ErrorMsgCart, false);
                    }
                    else
                    {
                        CurrentCart = cart;
                        string ErrorMsg = "";
                        CreateOrderResult orderresult = httputil.CreateOrder(CurrentCart, ref ErrorMsg);
                        if (orderresult == null)
                        {
                            ShowLog("异常" + ErrorMsg, true);
                        }
                        else if (orderresult.continuepay == 1)
                        {
                            frmBalancePayResult frmbalancepayresult = new frmBalancePayResult(orderresult.orderid);

                            frmbalancepayresult.frmOnLinePayResult_SizeChanged(null, null);
                            frmbalancepayresult.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width / 3, SystemInformation.WorkingArea.Height/2);
                            //frmonlinepayresult.Location = new System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width - frmbalancepayresult.Width - 50, 100);

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
                    frmCashCoupon frmcashcoupon = new frmCashCoupon(CurrentCart);
                    frmcashcoupon.frmCashCoupon_SizeChanged(null, null);
                    frmcashcoupon.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width / 3, this.Height - 200);
                    frmcashcoupon.Location = new System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width - frmcashcoupon.Width - 80, 100);
                    frmcashcoupon.TopMost = true;
                    frmcashcoupon.Opacity = 0.95d;
                    frmcashcoupon.ShowDialog();

                    //TODO  数据为什么会变为1？？？？？？？？？？？
                    CurrentCart.cashcouponamt = 0;
                    if (frmcashcoupon.DialogResult == DialogResult.OK)
                    {
                        ClearForm();
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

        }


        private void btnPayOK_Click(object sender, EventArgs e)
        {
            string ErrorMsg = "";
            CreateOrderResult orderresult = httputil.CreateOrder(CurrentCart, ref ErrorMsg);
            if (orderresult == null)
            {
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

            }
        }


        #endregion


        //重绘datagridview单元格
        private void dgvGood_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0 && e.Value != null)//要进行重绘的单元格
            {
                DataGridView dgvtemp = (DataGridView)sender;
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
                int intY = e.CellBounds.Top + e.CellStyle.Padding.Top;
                int intWidth = e.CellBounds.Width - (e.CellStyle.Padding.Left + e.CellStyle.Padding.Right);
                //int intHeight = sizText.Height + (e.CellStyle.Padding.Top + e.CellStyle.Padding.Bottom);


                ColorTranslator.FromHtml("#CE76D1");
                Font fnt1 = new System.Drawing.Font("微软雅黑", Math.Min(13F*wScale,13F*hScale));
                Color line1color = Color.White;
                switch (strLine1)
                {
                    case "会员价": line1color = ColorTranslator.FromHtml("#FF7D14"); break;
                    case "折扣": line1color = ColorTranslator.FromHtml("#209FD4"); break;
                    case "直降": line1color = ColorTranslator.FromHtml("#D42031"); break;
                        
                }

                SizeF size1 = this.CreateGraphics().MeasureString("test", fnt1);
                //第一行
                TextRenderer.DrawText(e.Graphics, strLine1, fnt1, new Rectangle(intX, intY, (int)size1.Width+5, (int)size1.Height),
                    Color.White,ColorTranslator.FromHtml("#CE76D1"), TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);

                //另起一行
                Font fnt2 = new System.Drawing.Font("微软雅黑", Math.Min(16F * wScale, 16F * hScale));
                SizeF size2 = this.CreateGraphics().MeasureString(strLine2, fnt2);

                intY = intY + (int)size1.Height;
                TextRenderer.DrawText(e.Graphics, strLine2, fnt2, new Rectangle(intX, intY, intWidth, (int)size2.Height),
                    Color.Black, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);

                Font fnt3 = new System.Drawing.Font("微软雅黑", Math.Min(16F * wScale, 16F * hScale));
                intY = intY + (int)size2.Height;

                TextRenderer.DrawText(e.Graphics, strLine3, fnt3, new Rectangle(intX, intY, intWidth, (int)size2.Height),Color.Black, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);


                // dgv2.Rows[e.RowIndex].Height = intY;
                Point point1 = new Point(0, dgvGood.RowTemplate.Height * (e.RowIndex + 1));
                //Point point2 = new Point(e.CellBounds.Width, intY + (int)size2.Height + e.CellStyle.Padding.Top + e.CellStyle.Padding.Bottom);
                Point point2 = new Point(e.CellBounds.Width, dgvGood.RowTemplate.Height * (e.RowIndex + 1));
                Pen blackPen = new Pen(Color.Black, 1);
                e.Graphics.DrawLine(blackPen, point1, point2);

                dgvtemp.Columns[e.ColumnIndex].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
                //dgvGood.Rows[e.RowIndex].Height = (int)size1.Height + (int)size2.Height * 2 + e.CellStyle.Padding.Top + e.CellStyle.Padding.Bottom + 1;
                




                e.Handled = true;

                dgvGood.ClearSelection();
            }
        }

        private void dgvGood_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try{

            
            if (e.RowIndex < 0)
                return;


            string proinfo = dgvGood.Rows[e.RowIndex].Cells["barcode"].Value.ToString();
             string barcode = proinfo.Replace("\r\n", "*").Split('*')[2];
             string proname = proinfo.Replace("\r\n", "*").Split('*')[1];
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
                            frmDeleteGood frmdelete = new frmDeleteGood("是否确认删除商品？", proname, barcode);
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
                frmDeleteGood frmdelete = new frmDeleteGood("是否确认删除商品？", proname, barcode);
                if (frmdelete.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

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

            }
            catch (Exception ex)
            {
                MainModel.ShowLog("操作购物车商品异常"+ex.Message,true);
            }

        }

    }
}
