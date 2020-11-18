using Maticsoft.BLL;
using Maticsoft.Model;
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
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.BaseUI;
using ZhuiZhi_Integral_Scale_UncleFruit.BatchSaleCardUI;
using ZhuiZhi_Integral_Scale_UncleFruit.BrokenUI;
using ZhuiZhi_Integral_Scale_UncleFruit.ChangePriceUI;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;
using ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.MyControl;
using ZhuiZhi_Integral_Scale_UncleFruit.PayUI;
using ZhuiZhi_Integral_Scale_UncleFruit.Resources;
//using ZhuiZhi_Integral_Scale_UncleFruit.Resources;
using ZhuiZhi_Integral_Scale_UncleFruit.ScaleFactory;
using ZhuiZhi_Integral_Scale_UncleFruit.ScaleUI;

namespace ZhuiZhi_Integral_Scale_UncleFruit
{
    public partial class frmMainHalfOffLine : Form
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

        private List<Product> LastLstPro = new List<Product>();
        /// <summary>
        /// 接口访问类
        /// </summary>
        HttpUtil httputil = new HttpUtil();

        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();

        private Bitmap btnorderhangimage;

        /// <summary>
        /// this.enable=false; 页面不可用页面不可控；  通过该标志控制页面是否可用
        /// </summary>
        private bool IsEnable = true;


        //扫描数据处理线程
        Thread threadScanCode;

        Thread threadScale;

        //刷新焦点线程  防止客屏播放视频抢走焦点
        Thread threadCheckActivate;

        private bool IsRun = true;

        /// <summary>
        /// 当前展示分类页数
        /// </summary>
        private int CurrentCategoryPage = 1;

        /// <summary>
        /// 当前展示二级分类页数 
        /// </summary>
        private int CurrentSecondCategoryPage = 1;

        /// <summary>
        /// 当前展示商品页数
        /// </summary>
        private int CurrentGoodPage = 1;
        /// <summary>
        /// 软键盘弹出时15个  没有键盘这段30
        /// </summary>
        private int CurrentGoodPageSize = 15;

        /// <summary>
        /// 当前展示购物车页数
        /// </summary>
        private int CurrentCartPage = 1;
        #endregion

        #region 页面加载与退出
        public frmMainHalfOffLine()
        {
            InitializeComponent();
            //使用委托的话frmmain界面会卡死
            Control.CheckForIllegalCrossThreadCalls = false;
            MainModel.wScale = (float)Screen.AllScreens[0].Bounds.Width / this.Width;
            MainModel.hScale = (float)Screen.AllScreens[0].Bounds.Height / this.Height;
            MainModel.midScale = (MainModel.wScale + MainModel.hScale) / 2;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                lblShopName.Text = MainModel.Titledata + "   " + MainModel.CurrentShopInfo.shopname;

                pnlAdjustInfo.Left = lblShopName.Right;
                //lblUrl.Left = pnlAdjustInfo.Right;
                lblMenu.Text = MainModel.CurrentUser.nickname + ",你好 ∨";
                picMenu.Left = pnlMenu.Width - picMenu.Width - lblMenu.Width;
                lblMenu.Left = picMenu.Right;
                pnlLoading.Size = new System.Drawing.Size(60, 60);


                pnlAdjustInfo.Visible = false;
                if (MainModel.URL.Contains("pos-qa"))
                {
                    lblUrl.Visible = true;
                    lblUrl.Text = "测试环境（QA）";
                }
                else if (MainModel.URL.Contains("pos-stage"))
                {
                    lblUrl.Visible = true;
                    lblUrl.Text = "测试环境（stage）";
                }
                else
                {
                    lblUrl.Visible = false;
                }


                if (!MainModel.CurrentTenatnIfno.membersimpleregister)
                {

                    tlpMemberType.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 0);
                    tlpMemberType.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 100);
                }
                else
                {
                    tlpMemberType.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100);
                    tlpMemberType.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("称重收银页面加载异常" + ex.Message);
            }
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            try
            {
                Application.DoEvents();
                LoadBaseInfo();

                ParameterizedThreadStart Pts = new ParameterizedThreadStart(ChangeMQTT);
                Thread threadmqtt = new Thread(Pts);
                threadmqtt.IsBackground = true;
                threadmqtt.Start(true);

                ShowLoading(true, false);

                //扫描数据处理线程
                threadScanCode = new Thread(ScanCodeThread);
                threadScanCode.IsBackground = true;
                threadScanCode.Start();


                //刷新焦点线程  防止客屏播放视频抢走焦点
                threadCheckActivate = new Thread(CheckActivate);
                threadCheckActivate.IsBackground = true;
                threadCheckActivate.Start();

                //启动电子秤同步信息线程
                Thread threadLoadScale = new Thread(ScaleDataHelper.LoadScale);
                threadLoadScale.IsBackground = true;
                threadLoadScale.Start();

                if (string.IsNullOrEmpty(MainModel.TvShowPage1))
                {
                    Thread threadTV = new Thread(ServerDataUtil.LoadTVSkus);
                    threadTV.IsBackground = true;
                    threadTV.Start();
                }

                //启动促销商品同步线程
                Thread threadLoadPromotion = new Thread(ServerDataUtil.UpdatePromotion);
                threadLoadPromotion.IsBackground = false;
                threadLoadPromotion.Start();

                LstAllProduct = CartUtil.LoadAllProduct(true);

                if (LstAllProduct == null || LstAllProduct.Count == 0)
                {
                    ServerDataUtil.LoadAllProduct();
                    LstAllProduct = CartUtil.LoadAllProduct(true);
                }
                else
                {
                    //启动全量商品同步线程
                    Thread threadLoadAllProduct = new Thread(ServerDataUtil.LoadAllProduct);
                    threadLoadAllProduct.IsBackground = true;
                    threadLoadAllProduct.Start();
                }
                IniForm();

                LoadPnlScale();
                ShowLoading(false, true);
                timerTask.Enabled = true;

                try
                {
                    ScaleGlobalHelper.GetWeight();
                }
                catch { }
                threadScale = new Thread(ScaleThread);
                threadScale.IsBackground = true;
                threadScale.Start();

                ZhuiZhi_Integral_Scale_UncleFruit.PrettyCash.PrettyCashHelper.ShowFormPretty();
                //Application.DoEvents();
                this.BeginInvoke(new InvokeHandler(delegate ()
                {
                    BaseUIHelper.ShowFormMainMedia();
                    BaseUIHelper.IniFormMainMedia();
                }));

                try { MainModel.frmlogin.Hide(); LoadingHelper.CloseForm(); }
                catch { }
                txtSearch.Focus();
                this.Activate();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("称重收银页面初始化异常" + ex.Message);
            }
        }

        private void LoadBaseInfo()
        {
            try
            {
                btnorderhangimage = new Bitmap(btnOrderHang.Image, 10, 10);
                UpdateOrderHang();

                keyBoard.Size = new System.Drawing.Size(dgvGood.Width, dgvGood.RowTemplate.Height * 3);
                INIManager.SetIni("System", "MainType", "Main", MainModel.IniPath);

                #region 排序选择

                imgPageUpForCategory = MainModel.GetControlImage(btnPreviousPageForCategoty);
                imgPageDownForCagegory = MainModel.GetControlImage(btnNextPageForCategory);

                imgPageUpForGood = MainModel.GetControlImage(btnPageUpForGood);
                imgPageDownForGood = MainModel.GetControlImage(btnPageDownForGood);
                #endregion

                //picMember.Location = lblTitle.Location;
                //picMember.Size = new System.Drawing.Size(lblSearchShuiyin.Height * 2, lblSearchShuiyin.Height);

                #region  自动加购 打码设置
                MainModel.WhetherAutoCart = INIManager.GetIni("CashierSet", "WhetherAutoCart", MainModel.IniPath) == "1";
                MainModel.WhetherPrint = INIManager.GetIni("CashierSet", "WhetherPrint", MainModel.IniPath) == "1";
                MainModel.WhetherAutoPrint = INIManager.GetIni("CashierSet", "WhetherAutoPrint", MainModel.IniPath) == "1";
                MainModel.WhetherHalfOffLine = INIManager.GetIni("System", "WhetherHalfOffLine", MainModel.IniPath) == "1";
                MainModel.WhetherShowWithJin = INIManager.GetIni("System", "WhetherShowWithJin", MainModel.IniPath) == "1";
                #endregion

                //判断是否支持整单改价
                btnModifyPrice.Visible = MainModel.CurrentShopInfo.posalterorderpriceflag == 1 || MainModel.CurrentShopInfo.posalterorderdiscountflag == 1;

                //判断是否支持单品改价
                btnChangePrice.Visible = MainModel.CurrentShopInfo.posalterskupriceflag == 1;
                btnDiscount.Visible = MainModel.CurrentShopInfo.posalterskupriceflag == 1;


            }
            catch (Exception ex)
            {
                ShowLog("加载基础信息异常", true);
            }
        }



        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {

            IsRun = false;
            try
            {
                ParameterizedThreadStart Pts = new ParameterizedThreadStart(ChangeMQTT);
                Thread threadmqtt = new Thread(Pts);
                threadmqtt.IsBackground = true;
                threadmqtt.Start(false);

                Delay.Start(300);
                timerTask.Enabled = false;
                //ScaleGlobalHelper.Close();
                this.Dispose();

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("释放窗体资源异常" + ex.Message);
            }
        }
        #endregion

        #region 顶部工具栏事件

        private void btnWindows_Click(object sender, EventArgs e)
        {
            MainModel.ShowTask();
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnNetWeight_Click(object sender, EventArgs e)
        {
            FormZero frmzero = new FormZero();
            frmzero.Location = new Point(pnlCategory.Left + pnlScale.Left + lblNetWeight.Left + lblNetWeight.Width / 2 - frmzero.Width / 2, pnlHead.Height + lblNetWeight.Bottom);
            frmzero.Show();
        }

        private void btnTareWeight_Click(object sender, EventArgs e)
        {

            FormTare frmtare = new FormTare();
            frmtare.Location = new Point(pnlCategory.Left + pnlScale.Left + lblTareWeight.Left + lblTareWeight.Width / 2 - frmtare.Width / 2, pnlHead.Height + lblTareWeight.Bottom);
            frmtare.Show();

        }


        private void btnOrderCancle_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }

                if (!ConfirmHelper.Confirm("确认取消交易？"))
                {
                    return;
                }

                //可能存在网络中断情况桌面还要清空
                try
                {
                    ReceiptUtil.EditCancelOrder(1, CurrentCart.totalpayment + CurrentCart.totalpromoamt);
                    AbnormalOrderUtil.WholeCancelOrderLsit(CurrentCart);
                }
                catch (Exception ex) { }

                if (MainModel.CurrentMember != null)
                {
                    ClearMember();
                }

                ClearForm();

                Application.DoEvents();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("取消交易异常" + ex.Message);
            }
        }

        private void btnOrderHang_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }
                if (btnOrderHang.Text == "挂单")
                {
                    if (CurrentCart != null)
                    {

                        if (!ZhuiZhi_Integral_Scale_UncleFruit.HelperUI.ConfirmHelper.Confirm("确认挂单？"))
                        {
                            return;
                        }

                        AbnormalOrderUtil.HookOrderList(CurrentCart);
                        SerializeOrder(CurrentCart);

                        if (MainModel.CurrentMember != null)
                        {
                            ClearMember();
                        }
                        ShowLog("已挂单", false);
                        ClearForm();
                    }
                }
                else if (btnOrderHang.Text == "挂单列表")
                {
                    IsEnable = false;
                    frmOrderHang frmorderhang = new frmOrderHang();
                    asf.AutoScaleControlTest(frmorderhang, 1178, 760, Screen.AllScreens[0].Bounds.Width, this.Height, true);
                    frmorderhang.Location = new System.Drawing.Point(0, 0);
                    frmorderhang.ShowDialog();
                    frmorderhang.Dispose();
                    IsEnable = true;
                    if (frmorderhang.DialogResult == DialogResult.OK)
                    {


                        if (frmorderhang.CurrentCart != null && frmorderhang.CurrentCart.products != null)
                        {
                            CurrentCart = frmorderhang.CurrentCart;

                            foreach (Product pro in frmorderhang.CurrentCart.products)
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

                        }

                        if (!string.IsNullOrEmpty(frmorderhang.CurrentPhone))
                        {
                            string ErrorMsgMember = "";
                            Member member = httputil.GetMember(frmorderhang.CurrentPhone, ref ErrorMsgMember);

                            if (ErrorMsgMember != "" || member == null) //会员不存在
                            {
                                ClearMember(false);
                                ShowLog(ErrorMsgMember, false);
                            }
                            else
                            {
                                LoadMember(member, false);
                            }
                        }
                        else
                        {
                            UploadOffLineDgvCart();
                        }



                    }
                }

                UpdateOrderHang();
            }
            catch (Exception ex)
            {
                ShowLog("挂单异常", true);
            }
            finally
            {
                IsEnable = true;
                BackHelper.HideFormBackGround();
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

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("序列化购物单异常：" + ex.Message);
            }
        }

        private void btnOrderQuery_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }
                IsEnable = false;
                frmOrderQuery frmorderquery = new frmOrderQuery();
                asf.AutoScaleControlTest(frmorderquery, 1178, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                frmorderquery.Location = new System.Drawing.Point(0, 0);
                frmorderquery.ShowDialog();
                frmorderquery.Dispose();
                IsEnable = true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("开启订单查询页面异常" + ex.Message);
            }
            finally
            {
                IsEnable = true;
            }
        }


        private void btnGiftCard_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }

                if (CurrentCart == null || CurrentCart.products == null)
                {
                    BaseUIHelper.UpdaForm(CurrentCart);
                }

                IsEnable = false;
                ZhuiZhi_Integral_Scale_UncleFruit.GiftCard.FormGiftCard frmgiftcard = new ZhuiZhi_Integral_Scale_UncleFruit.GiftCard.FormGiftCard();
                asf.AutoScaleControlTest(frmgiftcard, 1180, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                frmgiftcard.Location = new System.Drawing.Point(0, 0);
                frmgiftcard.ShowDialog();
                frmgiftcard.Dispose();
                Application.DoEvents();
                IsEnable = true;

                if (CurrentCart == null || CurrentCart.products == null)
                {
                    BaseUIHelper.IniFormMainMedia();
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("开启订单查询页面异常" + ex.Message);
            }
            finally
            {
                IsEnable = true;
            }
        }


        private void btnAdjustPrice_Click(object sender, EventArgs e)
        {

            ZhuiZhi_Integral_Scale_UncleFruit.MenuUI.MenuHelper.ShowFormAdjustPrice();
            pnlAdjustInfo.Visible = false;
            btnAdjustPrice.Image = null;
            Application.DoEvents();
        }


        private frmToolMain frmtoolmain = null;
        private void btnMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }

                if (frmtoolmain == null)
                {
                    frmtoolmain = new frmToolMain();

                    asf.AutoScaleControlTest(frmtoolmain, 210, 670, Convert.ToInt32(MainModel.wScale * 210), Convert.ToInt32(Screen.AllScreens[0].Bounds.Height - pnlHead.Height - 20), true);
                    frmtoolmain.DataReceiveHandle += frmToolMain_DataReceiveHandle;
                    frmtoolmain.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width - frmtoolmain.Width - 10, pnlHead.Height);
                    frmtoolmain.TopMost = true;
                    frmtoolmain.Show();
                }
                else
                {
                    frmtoolmain.Show();
                }

            }
            catch (Exception ex)
            {
                frmtoolmain = null;
                ShowLog("菜单窗体显示异常" + ex.Message, true);
            }
        }


        private void frmToolMain_DataReceiveHandle(ToolType tooltype)
        {
            try
            {
                if (tooltype == ToolType.Receipt)
                {
                    this.Invoke(new InvokeHandler(delegate ()
                    {
                        tsmReceipt_Click(null, null);
                    }));
                }
                if (tooltype == ToolType.Exit)
                {
                    this.Invoke(new InvokeHandler(delegate ()
                    {
                        tsmExit_Click(null, null);
                    }));
                }
                if (tooltype == ToolType.Expense)
                {
                    this.Invoke(new InvokeHandler(delegate ()
                    {
                        tsmExpense_Click(null, null);
                    }));
                }
                if (tooltype == ToolType.PrintSet)
                {
                    this.Invoke(new InvokeHandler(delegate ()
                    {
                        tsmPrintSet_Click(null, null);
                    }));

                }
                if (tooltype == ToolType.ReceiptQuery)
                {

                    this.Invoke(new InvokeHandler(delegate ()
                    {
                        tsmReceiptQuery_Click(null, null);
                    }));
                }

                if (tooltype == ToolType.Scale)
                {
                    this.Invoke(new InvokeHandler(delegate ()
                    {
                        tsmScale_Click(null, null);
                    }));
                }

                if (tooltype == ToolType.ChangeMode)
                {
                    this.Invoke(new InvokeHandler(delegate ()
                    {
                        tsmChangeMode_Click(null, null);
                    }));
                }

                if (tooltype == ToolType.ScaleModel)
                {
                    this.Invoke(new InvokeHandler(delegate ()
                    {
                        ChangeScaleModel();
                    }));
                }

                if (tooltype == ToolType.Broken)
                {
                    this.Invoke(new InvokeHandler(delegate ()
                 {
                     Broken();
                 }));
                }

                if (tooltype == ToolType.ReturnWithoutOrder)
                {
                    this.Invoke(new InvokeHandler(delegate ()
                    {
                        ShowLoading(true, false);
                        if (CurrentCart == null || CurrentCart.products == null)
                        {
                            BaseUIHelper.UpdaForm(CurrentCart);
                        }


                        FormReturnWithoutOrder frmreturn = new FormReturnWithoutOrder();
                        asf.AutoScaleControlTest(frmreturn, 1178, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                        frmreturn.Location = new System.Drawing.Point(0, 0);
                        frmreturn.ShowDialog();
                        frmreturn.Dispose();
                        Application.DoEvents();


                        // ZhuiZhi_Integral_Scale_UncleFruit.ReturnWithoutOrder.ReturnWithoutOrderHelper.ShowFormReturnWithoutOrder();

                        if (CurrentCart == null || CurrentCart.products == null)
                        {
                            BaseUIHelper.IniFormMainMedia();
                        }
                        ShowLoading(false, true);
                        this.Activate();
                    }));
                }


                if (tooltype == ToolType.BatchSaleCard)
                {
                    this.Invoke(new InvokeHandler(delegate ()
                    {
                        BatchSaleCard();
                    }));
                }

                if (tooltype == ToolType.RechangeQuery)
                {
                    this.Invoke(new InvokeHandler(delegate ()
                    {
                        RechangeQuery();
                    }));
                }
            }
            catch (Exception ex)
            {
                ShowLoading(false, true);
                LogManager.WriteLog("ERROR", "菜单按钮异常" + ex.Message);
            }
        }

        private void tsmExit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }

                if (!ZhuiZhi_Integral_Scale_UncleFruit.HelperUI.ConfirmHelper.Confirm("退出登录"))
                {
                    return;
                }

                INIManager.SetIni("System", "POS-Authorization", "", MainModel.IniPath);
                try { MainModel.frmlogin.Show(); }
                catch { }
                BaseUIHelper.CloseFormMain();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("退出系统异常" + ex.Message);
            }
        }

        //交班
        private void tsmReceipt_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }

                //if (!ZhuiZhi_Integral_Scale_UncleFruit.HelperUI.ConfirmHelper.Confirm("确认交班","点击确认后，收银机将自动打印交班表单"))
                //{
                //    return;
                //}

                decimal cashactualamt = ZhuiZhi_Integral_Scale_UncleFruit.PrettyCash.PrettyCashHelper.ShowFormGetCashNum();

                if (cashactualamt < 0)
                {
                    IsEnable = true;
                    return;
                }

                ReceiptPara receiptpara = new ReceiptPara();
                receiptpara.cancelordercount = Convert.ToInt16(INIManager.GetIni("Receipt", "CancelOrderCount", MainModel.IniPath));
                receiptpara.cancelordertotalmoney = INIManager.GetIni("Receipt", "CancelOrderTotalMoney", MainModel.IniPath);
                receiptpara.cancelsinglecount = Convert.ToInt16(INIManager.GetIni("Receipt", "CancelSingleCount", MainModel.IniPath));
                receiptpara.cancelsingletotalmoney = INIManager.GetIni("Receipt", "CancelSingleTotalMoney", MainModel.IniPath);

                receiptpara.openmoneypacketcount = Convert.ToInt16(INIManager.GetIni("Receipt", "OpenMoneyPacketCount", MainModel.IniPath));
                receiptpara.reprintcount = Convert.ToInt16(INIManager.GetIni("Receipt", "ReprintCount", MainModel.IniPath));
                //receiptpara.endtime = MainModel.getStampByDateTime(DateTime.Now);
                receiptpara.shopid = MainModel.CurrentShopInfo.shopid;
                receiptpara.depositrefundamt = ReceiptUtil.GetDepositRefundMoney();
                receiptpara.depositrefundcount = ReceiptUtil.GetDepositRefundCount();

                decimal PrettyCash = 0;

                decimal.TryParse(INIManager.GetIni("Receipt", "PrettyCash", MainModel.IniPath), out PrettyCash);

                receiptpara.sparecashamt = PrettyCash;
                receiptpara.cashactualamt = cashactualamt;

                receiptpara.balancedepositinfo = DbJsonUtil.GetBalanceInfo();

                if (receiptpara.balancedepositinfo != null)
                {
                    OrderPriceDetail cashpricedetail = receiptpara.balancedepositinfo.FirstOrDefault(r => r.title == "现金");

                    if (cashpricedetail != null)
                    {
                        receiptpara.balancedepositcashamount = cashpricedetail.amount;
                    }
                }

                IsEnable = false;
                string ErrorMsg = "";
                Receiptdetail receipt = httputil.Receipt(receiptpara, ref ErrorMsg);

                AbnormalOrderUtil.UploadAbnormalOrder();


                IsEnable = true;
                if (ErrorMsg != "" || receipt == null) //商品不存在或异常
                {
                    ShowLog(ErrorMsg, false);
                }
                else
                {
                    string ErrorMsgReceipt = "";
                    bool receiptresult = PrintUtil.ReceiptPrint(receipt, ref ErrorMsgReceipt);

                    if (receiptresult)
                    { }
                    else
                    {
                        ShowLog(ErrorMsgReceipt, true);
                    }
                    ReceiptUtil.ClearReceipt();

                    DbJsonUtil.DeleteBalanceInfo();
                    INIManager.SetIni("System", "POS-Authorization", "", MainModel.IniPath);
                    MainModel.Authorization = "";

                    FrmConfirmReceiptBack frmconfirmreceiptback = new FrmConfirmReceiptBack(receipt);
                    frmconfirmreceiptback.Location = new Point(0, 0);
                    frmconfirmreceiptback.ShowDialog();


                    try { MainModel.frmlogin.Show(); }
                    catch { }
                    BaseUIHelper.CloseFormMain();
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("交班出现异常" + ex.Message);
            }
            finally
            {
                IsEnable = true;
            }
        }

        private void tsmPrintSet_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }
                bool tempwhethershowwithjin = MainModel.WhetherShowWithJin;
                IsEnable = false;
                frmPrinterSetting frmprintset = new frmPrinterSetting();
                asf.AutoScaleControlTest(frmprintset, 1170, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                frmprintset.ShowDialog();

                if (tempwhethershowwithjin != MainModel.WhetherShowWithJin)
                {
                    LstAllProduct.ForEach(r => r.panelbmp = null);

                    sortCartByFirstCategoryid[CurrentFirstCategoryid].products.ForEach(r => r.panelbmp = null);


                    LoadDgvGood(true, true);
                }

                LoadPnlScale();

                IsEnable = true;
            }
            catch (Exception ex)
            {
                IsEnable = true;
                LogManager.WriteLog("打印机设置页面异常" + ex.Message);
            }
        }

        private void tsmReceiptQuery_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }
                frmReceiptQuery frmreceiptquery = new frmReceiptQuery();

                asf.AutoScaleControlTest(frmreceiptquery, 1178, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                frmreceiptquery.Location = new System.Drawing.Point(0, 0);

                frmreceiptquery.ShowDialog();

                frmreceiptquery.Dispose();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("交班查询异常" + ex.Message);
            }
        }


        private void tsmScale_Click(object sender, EventArgs e)
        {
            try
            {
                frmScale frmscal = new frmScale();
                asf.AutoScaleControlTest(frmscal, 1178, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                frmscal.Location = new System.Drawing.Point(0, 0);
                frmscal.ShowDialog();
                frmscal.Dispose();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("电子秤管理异常" + ex.Message);
            }
        }


        private void tsmExpense_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }
                frmExpense frmexpense = new frmExpense();

                asf.AutoScaleControlTest(frmexpense, 1178, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);

                frmexpense.Location = new System.Drawing.Point(0, 0);

                frmexpense.ShowDialog();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("营业外支出异常" + ex.Message);
            }
        }

        private void tsmChangeMode_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }
                frmChangeMode frmchangemode = new frmChangeMode();

                asf.AutoScaleControlTest(frmchangemode, 1178, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);

                frmchangemode.Location = new System.Drawing.Point(0, 0);

                frmchangemode.ShowDialog();
                frmchangemode.Dispose();

                ShowLoading(true, false);
                this.Invoke(new InvokeHandler(delegate ()
                   {
                       LstAllProduct = CartUtil.LoadAllProduct(true);
                       dgvGood.Rows.Clear();
                       IniForm();
                   }));
                ShowLoading(false, true);
            }
            catch (Exception ex)
            {
                ShowLoading(false, true);
                LogManager.WriteLog("修改模式异常" + ex.Message);
            }
        }

        private void ChangeScaleModel()
        {
            try
            {
                if (ChangeScaleModelHelper.Confirm(ChangeModel.WeightAndScale))
                {
                    this.DialogResult = DialogResult.Retry;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ShowLog("切换秤模式异常" + ex.Message, true);
            }
        }

        private void Broken()
        {
            try
            {
                IsEnable = false;
                //BrokenHelper.ShowFormBrokenCreate();
                BrokenHelper.ShowFormBroken();
                IsEnable = true;

                //0726防止会员其他页面过期 本页面会员还在展示
                if (MainModel.CurrentMember == null && tplMember.ColumnStyles[1].Width > 0)
                {
                    ClearMember();
                    UploadOffLineDgvCart();
                }
            }
            catch (Exception ex)
            {
                ShowLog("切换秤模式异常" + ex.Message, true);
            }
        }

        private void BatchSaleCard()
        {
            try
            {
                IsEnable = false;
                MemberCenterHelper.ShowFormEntityCardBatchSale();
                IsEnable = true;
            }
            catch (Exception ex)
            {
                ShowLog("切换批量售卡异常" + ex.Message, true);
            }
        }

        private void RechangeQuery()
        {
            try
            {
                IsEnable = false;
                MemberCenterHelper.ShowFormRechangeQuery();
                IsEnable = true;
            }
            catch (Exception ex)
            {
                ShowLog("切换充值明细异常" + ex.Message, true);
            }
        }


        int Seconds = 0;   //执行秒数  没半分钟轮询一次接口  每秒都检查mqtt是否接收到新数据
        public delegate void deleteTimer();
        private void timerTask_Tick(object sender, EventArgs e)
        {
            try
            {
                timerTask.Enabled = false;
                ////防止异步加载窗体控件 出现红叉
                //if (this.ContainsFocus && IsEnable)
                //{
                Seconds++;

                if (Seconds >= 30 || ConfigUtil.HaveNewOrder())
                {
                    LoadNewOrder();
                }

                MqttChangeType mqtttype = ConfigUtil.GetAdjustPriceChanged();
                bool needAdjustPrice = Seconds >= 30 || mqtttype != MqttChangeType.None;
                bool needLoadIncrementProduct = mqtttype == MqttChangeType.AdjustPrice || mqtttype == MqttChangeType.SkuInsert || mqtttype == MqttChangeType.SkuUpOrDown;

                if (needAdjustPrice || needLoadIncrementProduct)
                {
                    Seconds = 0;
                    UpdateProduct(needAdjustPrice, needLoadIncrementProduct);
                }
                // }


            }
            catch (Exception ex)
            {
                LogManager.WriteLog("线上订单校验异常" + ex.Message);
            }
            finally
            {
                Invoke((new Action(() =>
                {
                    timerTask.Enabled = true;
                })));
            }
        }

        private void UpdateProduct(bool needAdjustPrice, bool needLoadIncrementProduct)
        {
            Thread t = new Thread(new ThreadStart(delegate
            {
                try
                {
                    // timerTask.Enabled = false;
                    if (needAdjustPrice)
                    {
                        string errormsg = "";
                        AdjustPriceDynamic result = httputil.GetAdjustPriceDynamicForPos(ConfigUtil.GetAdjustStartTime(), MainModel.getStampByDateTime(DateTime.Now), true, ref errormsg);

                        if (result != null && result.dynamiccount > 0)
                        {
                            if (result.dynamiccount > 99)
                            {
                                lblAdjustCount.Text = "99+";
                            }
                            else
                            {
                                lblAdjustCount.Text = result.dynamiccount.ToString();
                            }
                            pnlAdjustInfo.Visible = true;
                            pnlAdjustInfo.BringToFront();
                            btnAdjustPrice.Image = btnorderhangimage;
                            needAdjustPrice = true;  //有调价商品时需要更新增量商品

                            //INIManager.SetIni("MQTT", "AdjustStartTime", result.querydate, MainModel.IniPath); //记录登录时间作为调价查询的起始时间
                        }
                    }

                    if (needLoadIncrementProduct)
                    {
                        ServerDataUtil.LoadIncrementProduct();

                        IniAllProduct();
                        Application.DoEvents();
                    }

                }
                catch { }


            }));
            t.Start();

        }
        /// <summary>
        /// 防止重复打印
        /// </summary>
        private List<string> LstPrintOrderids = new List<string>();
        private void LoadNewOrder()
        {
            try
            {
                string errormsg = "";
                PrinterPickOrderInfo orderinfo = httputil.QueryPickPrintInfo(ref errormsg);
                if (orderinfo != null && !string.IsNullOrEmpty(orderinfo.orderid) && !LstPrintOrderids.Contains(orderinfo.orderid))
                {
                    LstPrintOrderids.Add(orderinfo.orderid);

                    this.Invoke(new InvokeHandler(delegate ()
                   {
                   //语音播报 +弹窗提示
                   SayOrderHelper.ShowFormToast(orderinfo.saymsg);
                   }));
                    string msg = "";
                    PrintUtil.PrintThirdOrder(orderinfo, ref msg);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("加载线上订单异常" + ex.Message);
            }
        }

        private void IniAllProduct()
        {
            try
            {

                IsEnable = false;
                this.Invoke(new InvokeHandler(delegate ()
               {
                   LstAllProduct = CartUtil.LoadAllProduct(true);

                   string currentsecondcategoryid = sortCartByFirstCategoryid[CurrentFirstCategoryid].SelectSecondCategoryid;
                   Product pro = LstAllProduct.FirstOrDefault(r => r.secondcategoryid == currentsecondcategoryid);

                   //如果之前二级分类不存在
                   if (pro == null)
                   {
                       DgvGoodRowClear();
                       IniForm();
                       LoadSecondDgvCategory();
                   }
                   else
                   {
                       IniForm();

                       sortCartByFirstCategoryid[CurrentFirstCategoryid].SelectSecondCategoryid = currentsecondcategoryid;  //初始化数据后 赋值刷新前的二级分类名称
                       LoadDgvGood(false, false);
                   }
               }));
                IsEnable = true;
            }
            catch (Exception ex)
            {
                IsEnable = true;
                LogManager.WriteLog("更新收银面板商品异常" + ex.Message);
            }
        }


        #endregion

        #region 会员积分优惠券
        //输入会员手机号
        private void cbtnLoadPhone_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }

                ShowLoading(true, false);
                //string numbervalue = NumberHelper.ShowFormNumber("输入会员手机号", NumberType.MemberCode);

                string numbervalue = PayHelper.ShowFormVoucher();
                if (!string.IsNullOrEmpty(numbervalue))
                {
                    string ErrorMsgMember = "";
                    Member member = httputil.GetMember(numbervalue, ref ErrorMsgMember);

                    if (ErrorMsgMember != "" || member == null) //会员不存在
                    {
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
                }
                ShowLoading(false, true);
            }
            catch (Exception ex)
            {
                ShowLoading(false, true);

                LogManager.WriteLog("会员登录异常" + ex.Message);
            }

        }

        private void btnCreateMember_Click(object sender, EventArgs e)
        {
            if (!IsEnable)
            {
                return;
            }

            if (CurrentCart == null || CurrentCart.products == null)
            {
                BaseUIHelper.UpdaForm(CurrentCart);
            }

            ShowLoading(true, false);

            Member member = MainHelper.CreateMember();

            if (member != null)
            {
                LoadMember(member);
            }

            ShowLoading(false, true);

            if (CurrentCart == null || CurrentCart.products == null)
            {
                BaseUIHelper.IniFormMainMedia();
            }
        }

        /// <summary>
        /// 清空会员
        /// </summary>
        /// <param name="clearadjust">是否清空改价信息   挂单恢复情况不清空</param>
        private void ClearMember(bool clearadjust = true)
        {
            try
            {
                this.Invoke(new InvokeHandler(delegate ()
                {
                    DateTime starttime = DateTime.Now;


                    ZhuiZhi_Integral_Scale_UncleFruit.Model.HalfOffLine.HalfOffLineUtil.ClearMemberInfo();
                    tplMember.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100);
                    tplMember.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0);
                    MainModel.CurrentMember = null;
                    MainModel.CurrentCouponCode = "";
                    MainModel.Currentavailabecoupno = null;
                    lblCoupon.Text = "0张";
                    lblCredit.Visible = false;
                    picCredit.Visible = false;


                    Console.WriteLine("clearmember 页面控件" + (DateTime.Now - starttime).TotalMilliseconds);
                    //Application.DoEvents();
                    this.BeginInvoke(new InvokeHandler(delegate ()
                    {
                        BaseUIHelper.LoadMember();
                    }));

                    Console.WriteLine("clearmember 客屏" + (DateTime.Now - starttime).TotalMilliseconds);
                    // RefreshCart();
                    //购物车有商品的话刷新一次
                    if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                    {
                        if (clearadjust)
                        {
                            //退出会员清空改价信息
                            CurrentCart.products.ForEach(r => r.adjustpriceinfo = null);
                        }

                        RefreshCart();
                    }
                    Console.WriteLine("clearmember 刷新购物车" + (DateTime.Now - starttime).TotalMilliseconds);
                    LstAllProduct.ForEach(r => r.panelbmp = null);

                    LoadDgvGood(true, true);
                    Console.WriteLine("clearmember dgvgood" + (DateTime.Now - starttime).TotalMilliseconds);
                }));
            }
            catch (Exception ex)
            {
                ShowLog("清空会员异常", true);
            }
        }

        /// <summary>
        /// 加载会员信息
        /// </summary>
        /// <param name="member"></param>
        /// <param name="clearAdjust">是否清空改价信息  挂单恢复会员订单不清空改价信息</param>
        private void LoadMember(Member member, bool clearAdjust = true)
        {
            try
            {
                if (this.IsHandleCreated)
                {
                    this.Invoke(new InvokeHandler(delegate ()
                    {

                        ZhuiZhi_Integral_Scale_UncleFruit.Model.HalfOffLine.HalfOffLineUtil.CurrentMemberid = member.memberheaderresponsevo.memberid;
                        ShowLoading(true, false);
                        tplMember.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 0);
                        tplMember.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 100);

                        if (string.IsNullOrEmpty(member.entrancecode))
                        {
                            member.entrancecode = member.memberheaderresponsevo.mobile;
                        }

                        lblMemberPhone.Text = "会员账号：" + member.memberinfo+MainHelper.GetMemberName(member,true);

                        pbtnExitMember.Left = lblMemberPhone.Right + 5;

                        if (pbtnExitMember.Right > btnTopUp.Left)
                        {
                            pbtnExitMember.Left = btnTopUp.Left - pbtnExitMember.Width;
                        }

                        MainModel.CurrentMember = member;


                        //Thread threadloadMember = new Thread(ZhuiZhi_Integral_Scale_UncleFruit.Model.HalfOffLine.HalfOffLineUtil.LoadMemberInfo);
                        //threadloadMember.IsBackground = true;
                        //threadloadMember.Start();

                        lblCreditStr.Text = "积分" + member.creditaccountrepvo.availablecredit.ToString();

                        lblBalance.Text = "￥" + member.barcoderecognitionresponse.balance;

                        if (member.memberorderresponsevo != null && !string.IsNullOrEmpty(member.memberorderresponsevo.lastpayat))
                        {
                            lblMemberRecord.Visible = true;
                            lblMemberRecord.Text = "近一次消费时间:" + MainModel.GetDateTimeByStamp(member.memberorderresponsevo.lastpayat).ToString("yyyy-MM-dd") + " | 近一月消费次数:" + member.memberorderresponsevo.paycount;
                        }
                        else
                        {
                            lblMemberRecord.Visible = false;
                        }

                        if (member.membertenantresponsevo.onbirthday)
                        {
                            picBirthday.Visible = true;

                            lblMemberPhone.Top = lblMemberRecord.Top - lblMemberPhone.Height;
                            pbtnExitMember.Top = lblMemberPhone.Top;

                        }
                        else
                        {
                            picBirthday.Visible = false;

                            lblMemberPhone.Top = (pnlMember.Height - lblMemberRecord.Top - lblMemberPhone.Height) / 2;
                            pbtnExitMember.Top = lblMemberPhone.Top;
                        }

                        // btnTopUp.Top = pbtnExitMember.Top - (btnTopUp.Height - pbtnExitMember.Height)/2;A

                        //Application.DoEvents();

                        //购物车有商品的话刷新一次
                        if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                        {
                            if (clearAdjust)
                            {
                                //登录会员清空改价信息
                                CurrentCart.products.ForEach(r => r.adjustpriceinfo = null);
                            }

                            RefreshCart();
                        }
                        else
                        {
                            CurrentCart.unavailablecoupons = CartUtil.GetAllOrderCoupon();
                            lblCoupon.Text = "共" + CartUtil.GetAllCouponCount(CurrentCart) + "张";
                        }
                        Application.DoEvents();

                        Thread threadloadMember = new Thread(ZhuiZhi_Integral_Scale_UncleFruit.Model.HalfOffLine.HalfOffLineUtil.LoadMemberInfo);
                        threadloadMember.IsBackground = true;
                        threadloadMember.Start();
                        IsEnable = true;


                        ShowLoading(false, true);

                        this.BeginInvoke(new InvokeHandler(delegate ()
                        {
                            BaseUIHelper.LoadMember();
                        }));
                        LstAllProduct.ForEach(r => r.panelbmp = null);

                        sortCartByFirstCategoryid[CurrentFirstCategoryid].products.ForEach(r => r.panelbmp = null);


                        LoadDgvGood(true, true);

                    }));
                }
            }
            catch (Exception ex)
            {
                ShowLog("加载会员异常" + ex.StackTrace, true);
            }
        }

        private void pbtnExitMember_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }

                if (!ZhuiZhi_Integral_Scale_UncleFruit.HelperUI.ConfirmHelper.Confirm("确认退出会员？"))
                {
                    return;
                }

                ShowLoading(true, false);
                ClearMember();
                Application.DoEvents();
                // RefreshCart();
                ShowLoading(false, true);
            }
            catch (Exception ex)
            {
                ShowLoading(false, true);

                LogManager.WriteLog("退出会员异常" + ex.Message);
            }
        }


        private void btnCoupon_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }

                if (CartUtil.GetAllCouponCount(CurrentCart) <= 0)
                {
                    return;
                }
                BackHelper.ShowFormBackGround();

                frmCoupon frmcoupon = new frmCoupon(CurrentCart, MainModel.CurrentCouponCode);
                asf.AutoScaleControlTest(frmcoupon, 380, 480, 380 * MainModel.midScale, 480 * MainModel.midScale, true);
                frmcoupon.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmcoupon.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmcoupon.Height) / 2);
                frmcoupon.TopMost = true;
                frmcoupon.ShowDialog();
                BackHelper.HideFormBackGround();

                MainModel.CurrentCouponCode = frmcoupon.SelectCouponCode;
                MainModel.Currentavailabecoupno = frmcoupon.SelectPromotionCode;


                if (string.IsNullOrEmpty(frmcoupon.SelectCouponCode))
                {
                    CurrentCart.selectedcoupons = null;
                }
                else
                {
                    CurrentCart.selectedcoupons = new Dictionary<string, OrderCouponVo>();
                    CurrentCart.selectedcoupons.Add(frmcoupon.SelectCouponCode, frmcoupon.SelectPromotionCode);
                }

                UploadOffLineDgvCart();
                bool RefreshCartOK = true;


                //收银完成
                if (frmcoupon.DialogResult == DialogResult.Yes && RefreshCartOK)
                {
                    string ErrorMsg = "";
                    int ResultCode = 0;
                    CreateOrderResult orderresult = httputil.CreateOrder(CurrentCart, ref ErrorMsg, ref ResultCode);
                    if (ResultCode != 0 || orderresult == null)
                    {
                        CheckUserAndMember(ResultCode, ErrorMsg);
                    }
                    else if (orderresult.continuepay == 1)
                    {
                        ShowLog("需要继续支付", true);
                    }
                    else
                    {
                        PayHelper.ShowFormPaySuccess(orderresult.orderid);
                        ClearForm();
                        ClearMember();
                    }
                }

                ShowLoading(false, true);
            }
            catch (Exception ex)
            {
                ShowLoading(false, true);
                BackHelper.HideFormBackGround();
                LogManager.WriteLog("选择优惠券异常：" + ex.Message);
            }

        }

        private void btnCredit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }

                //购物车为空或者 没有可用积分 不允许选中
                if (CurrentCart == null || CurrentCart.products == null || CurrentCart.products.Count == 0 || MainModel.CurrentMember == null || CurrentCart.pointinfo == null || CurrentCart.pointinfo.availablepoints == 0)
                {
                    return;
                }

                ShowLoading(true, false);
                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                {
                    MainModel.CurrentMember.isUsePoint = !MainModel.CurrentMember.isUsePoint;

                    picCredit.BackgroundImage = MainModel.CurrentMember.isUsePoint ? picSelectCredit.Image : picNotSelectCredit.Image;

                    UploadOffLineDgvCart();
                }
                else
                {
                    picCredit.BackgroundImage = picNotSelectCredit.Image;
                }
                ShowLoading(false, true);
            }
            catch (Exception ex)
            {
                ShowLoading(false, true);
                ShowLog("积分处理异常" + ex.Message, true);
            }
        }

        #endregion

        #region 面板商品展示

        private List<Product> LstAllProduct = new List<Product>();

        private DBPRODUCT_BEANBLL productbll = new DBPRODUCT_BEANBLL();

        private string CurrentFirstCategoryid = "";

        /// <summary>
        /// 商品分类 上一页
        /// </summary>
        private Image imgPageUpForCategory;
        /// <summary>
        /// 商品分类  下一页
        /// </summary>
        private Image imgPageDownForCagegory;

        /// <summary>
        /// 商品 上一页
        /// </summary>
        private Image imgPageUpForGood;

        /// <summary>
        /// 商品下一页
        /// </summary>
        private Image imgPageDownForGood;


        #region  查询和排序
        private void lblSearchShuiyin_Click(object sender, EventArgs e)
        {
            txtSearch.Focus();
        }


        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {

                lblSearchShuiyin.Visible = string.IsNullOrEmpty(txtSearch.Text);
                if (isfresh)
                {
                    return;
                }

                Thread threadItemExedate = new Thread(Upthread);
                threadItemExedate.IsBackground = true;
                threadItemExedate.Start();

            }
            catch (Exception ex)
            {
                ShowLog("查询面板商品异常" + ex.Message, true);
            }

        }
        bool isfresh = false;
        private void Upthread()
        {
            try
            {
                isfresh = true;
                //搜索延时150毫秒 有回车代表扫描数据 没有则做条件查询
                Delay.Start(150);

                string result = txtSearch.Text;

                if (result.Contains("\r\n"))
                {
                    QueueScanCode.Enqueue(result.ToString().Trim());

                    txtSearch.Clear();
                }
                else
                {

                    if (!IsEnable)
                    {
                        return;
                    }

                    Application.DoEvents();

                    CurrentGoodPage = 1;
                    LoadDgvGood(false, false);
                }
            }
            catch { }
            finally
            {
                isfresh = false;
            }
        }


        /// <summary>
        /// 当前页面购物车 根据firsecategoryid 区分
        /// </summary>
        SortedDictionary<string, Cart> sortCartByFirstCategoryid = new SortedDictionary<string, Cart>();

        public SortType querysorttype = SortType.SaleCount;

        #endregion

        private Dictionary<string, string> sortCategory = new Dictionary<string, string>();
        private void IniForm()
        {
            try
            {
                sortCategory.Clear();
                sortCategory.Add("-1", "全部");

                SortedDictionary<string, string> tempSort = productbll.GetDiatinctCategory("STATUS =1 and CREATE_URL_IP='" + MainModel.URL + "' and SHOPID='" + MainModel.CurrentShopInfo.shopid + "'  order by FIRSTCATEGORYID");

                foreach (KeyValuePair<string, string> kv in tempSort)
                {
                    if (!string.IsNullOrEmpty(kv.Key))
                    {
                        sortCategory.Add(kv.Key, kv.Value);
                    }

                }

                sortCartByFirstCategoryid.Clear();

                foreach (KeyValuePair<string, string> kv in sortCategory)
                {
                    if (kv.Key == "-1")
                    {
                        Cart cart = new Cart();
                        cart.sorttype = SortType.SaleCount;
                        cart.products = LstAllProduct;
                        sortCartByFirstCategoryid.Add(kv.Key, cart);
                    }
                    else if (!string.IsNullOrEmpty(kv.Key))  //过滤脏数据 不存在一级分类值不展示
                    {
                        List<Product> lstpro = LstAllProduct.Where(r => r.firstcategoryid == kv.Key).ToList();
                        if (lstpro != null && lstpro.Count > 0)
                        {
                            Cart cart = new Cart();
                            cart.sorttype = SortType.SaleCount;
                            cart.products = lstpro;
                            sortCartByFirstCategoryid.Add(kv.Key, cart);
                        }
                    }

                }

                CurrentCategoryPage = 1;
                LoadDgvCategory();
            }
            catch (Exception ex)
            {
                ShowLog("初始化商品列表异常" + ex.StackTrace, true);
            }
        }

        private object thislockDgvCategory = new object();
        private void LoadDgvCategory()
        {
            lock (thislockDgvCategory)
            {
                Invoke((new Action(() =>
                {

                    try
                    {
                        int page = CurrentCategoryPage;
                        int startindex = 0;
                        int lastindex = 6;
                        int waitingcount = 0;

                        bool havanextpage = false;
                        bool havepreviousPage = false;
                        if (page == 1)
                        {
                            havepreviousPage = false;
                            startindex = 0;
                            if (sortCategory.Count > 7)
                            {
                                lastindex = 5;
                                havanextpage = true;
                            }
                            else
                            {
                                lastindex = sortCategory.Count - 1;
                                havanextpage = false;
                            }
                        }
                        else
                        {
                            havepreviousPage = true;
                            waitingcount = sortCategory.Count - ((page - 1) * 5 + 1);  //第一页只有下一页  中间页都是上一页下一页 占用两个
                            startindex = (page - 1) * 5 + 1;

                            if (waitingcount > 6)
                            {
                                lastindex = startindex + 4;
                                havanextpage = true;
                            }
                            else
                            {
                                lastindex = sortCategory.Count - 1;
                                havanextpage = false;
                            }
                        }

                        int loadingcount = lastindex - startindex + 1;

                        List<Image> lstshowimg = new List<Image>();
                        if (havepreviousPage)
                        {
                            lstshowimg.Add(imgPageUpForCategory);
                        }

                        int tempcount = 0;
                        foreach (KeyValuePair<string, string> kv in sortCategory)
                        {
                            if (tempcount >= startindex && tempcount <= lastindex)
                            {
                                Image img;
                                if (CurrentFirstCategoryid == kv.Key)
                                {
                                    btnSelect.Text = kv.Value;
                                    img = MainModel.GetControlImage(btnSelect);
                                    img.Tag = kv;
                                }
                                else
                                {
                                    btnNotSelect.Text = kv.Value;
                                    img = MainModel.GetControlImage(btnNotSelect);
                                    img.Tag = kv;
                                }

                                lstshowimg.Add(img);
                            }

                            tempcount++;
                        }

                        if (havanextpage)
                        {
                            lstshowimg.Add(imgPageDownForCagegory);
                        }

                        int emptyimgcount = 7 - loadingcount;

                        for (int i = 0; i < emptyimgcount; i++)
                        {
                            lstshowimg.Add(ResourcePos.empty);
                        }
                        dgvCategory.Rows.Clear();
                        for (int i = 0; i < 1; i++)
                        {
                            int temp = 7 * i;
                            dgvCategory.Rows.Add(lstshowimg[temp + 0], lstshowimg[temp + 1], lstshowimg[temp + 2], lstshowimg[temp + 3], lstshowimg[temp + 4], lstshowimg[temp + 5], lstshowimg[temp + 6]);
                        }

                        IsEnable = true;
                        if (dgvCategory.Rows.Count > 0 && dgvGood.Rows.Count == 0)
                        {
                            dgvCategory_CellClick(null, new DataGridViewCellEventArgs(0, 0));
                        }

                    }
                    catch (Exception ex)
                    {
                        ShowLog("加载分类异常" + ex.StackTrace, true);
                    }

                })));
            }
        }

        private object thislockDgvSecondCategory = new object();
        private void LoadSecondDgvCategory()
        {
            lock (thislockDgvSecondCategory)
            {
                Invoke((new Action(() =>
                   {
                       try
                       {


                           if (CurrentFirstCategoryid == "-1")
                           {
                               //TODO  隐藏二级分类 显示无分类信息
                               dgvSecondCategory.Rows.Clear();
                               CurrentGoodPage = 1;
                               LoadDgvGood(false, false);
                               return;
                           }
                           Cart curCart = null;
                           if (sortCartByFirstCategoryid == null || !sortCartByFirstCategoryid.ContainsKey(CurrentFirstCategoryid))
                           {
                               return;
                           }
                           curCart = sortCartByFirstCategoryid[CurrentFirstCategoryid];
                           List<Product> AllCategoryPro = curCart.products;

                           List<string> list_name = AllCategoryPro.Select(t => t.secondcategoryid).Distinct().ToList();

                           Dictionary<string, string> currentsecond = new Dictionary<string, string>();
                           foreach (string str in list_name)
                           {
                               if (!string.IsNullOrEmpty(str))
                               {
                                   currentsecond.Add(str, AllCategoryPro.FirstOrDefault(r => r.secondcategoryid == str).secondcategoryname);
                               }
                           }

                           int page = CurrentSecondCategoryPage;
                           int startindex = 0;
                           int lastindex = 6;
                           int waitingcount = 0;

                           bool havanextpage = false;
                           bool havepreviousPage = false;
                           if (page == 1)
                           {
                               havepreviousPage = false;
                               startindex = 0;
                               if (currentsecond.Count > 7)
                               {
                                   lastindex = 5;
                                   havanextpage = true;
                               }
                               else
                               {
                                   lastindex = currentsecond.Count - 1;
                                   havanextpage = false;
                               }
                           }
                           else
                           {
                               havepreviousPage = true;
                               waitingcount = currentsecond.Count - ((page - 1) * 5 + 1);  //第一页只有下一页  中间页都是上一页下一页 占用两个
                               startindex = (page - 1) * 5 + 1;

                               if (waitingcount > 6)
                               {
                                   lastindex = startindex + 4;
                                   havanextpage = true;
                               }
                               else
                               {
                                   lastindex = currentsecond.Count - 1;
                                   havanextpage = false;
                               }
                           }

                           int loadingcount = lastindex - startindex + 1;

                           List<Image> lstshowimg = new List<Image>();
                           if (havepreviousPage)
                           {
                               lstshowimg.Add(imgPageUpForCategory);
                           }

                           int tempcount = 0;
                           foreach (KeyValuePair<string, string> kv in currentsecond)
                           {
                               if (tempcount >= startindex && tempcount <= lastindex)
                               {
                                   Image img;
                                   if (curCart.SelectSecondCategoryid == kv.Key)
                                   {
                                       btnSecondSelect.Text = kv.Value;
                                       img = MainModel.GetControlImage(btnSecondSelect);
                                       img.Tag = kv;
                                   }
                                   else
                                   {
                                       btnSecondNotSelect.Text = kv.Value;
                                       img = MainModel.GetControlImage(btnSecondNotSelect);
                                       img.Tag = kv;
                                   }

                                   lstshowimg.Add(img);
                               }

                               tempcount++;
                           }

                           if (havanextpage)
                           {
                               lstshowimg.Add(imgPageDownForCagegory);
                           }

                           int emptyimgcount = 7 - loadingcount;

                           for (int i = 0; i < emptyimgcount; i++)
                           {
                               lstshowimg.Add(ResourcePos.empty);
                           }

                           dgvSecondCategory.Rows.Clear();
                           for (int i = 0; i < 1; i++)
                           {
                               int temp = 7 * i;
                               dgvSecondCategory.Rows.Add(lstshowimg[temp + 0], lstshowimg[temp + 1], lstshowimg[temp + 2], lstshowimg[temp + 3], lstshowimg[temp + 4], lstshowimg[temp + 5], lstshowimg[temp + 6]);
                           }

                           IsEnable = true;
                           if (dgvSecondCategory.Rows.Count > 0 && curCart.SelectSecondCategoryid == "-1")
                           {
                               dgvSecondCategory_CellClick(null, new DataGridViewCellEventArgs(0, 0));
                               // dgvSecondCategory(null, new DataGridViewCellEventArgs(0, 0));
                           }
                           else
                           {
                               CurrentGoodPage = 1;
                               LoadDgvGood(false, false);
                           }


                       }
                       catch (Exception ex)
                       {
                           ShowLog("加载分类异常" + ex.StackTrace, true);
                       }
                   })));

            }
        }


        private void dgvCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }
                if (e.RowIndex < 0)
                    return;

                Other.CrearMemory();
                Image selectimg = (Image)dgvCategory.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                //展开
                if (selectimg == imgPageDownForCagegory)
                {
                    CurrentCategoryPage++;
                    LoadDgvCategory();
                    return;
                }
                //收起
                if (selectimg == imgPageUpForCategory)
                {
                    CurrentCategoryPage--;
                    LoadDgvCategory();
                    return;
                }
                if (selectimg.Tag == null)  //空白单元格（无商品）
                {
                    return;
                }

                //遍历单元格清空之前的选中状态
                for (int i = 0; i < this.dgvCategory.Rows.Count; i++)
                {
                    for (int j = 0; j < this.dgvCategory.Columns.Count; j++)
                    {
                        Image lastimg = (Image)dgvCategory.Rows[i].Cells[j].Value;

                        if (lastimg.Tag != null && ((KeyValuePair<string, string>)lastimg.Tag).Key == CurrentFirstCategoryid)
                        {
                            btnNotSelect.Text = ((KeyValuePair<string, string>)lastimg.Tag).Value;
                            Image tempimg = MainModel.GetControlImage(btnNotSelect);
                            tempimg.Tag = (KeyValuePair<string, string>)lastimg.Tag;

                            dgvCategory.Rows[i].Cells[j].Value = tempimg;
                            // break;
                        }
                    }
                }


                KeyValuePair<string, string> kv = (KeyValuePair<string, string>)selectimg.Tag;

                btnSelect.Text = kv.Value;
                Image img = MainModel.GetControlImage(btnSelect);
                img.Tag = kv;

                dgvCategory.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = img;


                CurrentFirstCategoryid = kv.Key;
                CurrentSecondCategoryPage = 1;
                LoadSecondDgvCategory();
                //dgvGood.Rows.Clear();

                //CurrentGoodPage = 1;
                ////说明是第一次加载
                //if (sender == null)
                //{                   
                //    LoadDgvGood(true, true);
                //}
                //else
                //{
                //    LoadDgvGood(false, false);
                //}
            }
            catch (Exception ex)
            {
                ShowLog("选择分类异常" + ex.StackTrace, true);
            }
            finally
            {
                btnScan.Select();
            }
        }

        bool isnewType = false;

        private object thislockDgvGood = new object();
        private void LoadDgvGood(bool isnew, bool isnewType)
        {
            lock (thislockDgvGood)
            {


                try
                {
                    Other.CrearMemory();
                    List<Product> AllCategoryPro = new List<Product>();

                    if (CurrentFirstCategoryid == "-1")
                    {
                        AllCategoryPro = sortCartByFirstCategoryid[CurrentFirstCategoryid].products;

                    }
                    else
                    {
                        AllCategoryPro = sortCartByFirstCategoryid[CurrentFirstCategoryid].products.Where(r => r.secondcategoryid == sortCartByFirstCategoryid[CurrentFirstCategoryid].SelectSecondCategoryid).ToList();

                    }


                    if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
                    {
                        string strquery = txtSearch.Text.Trim().ToUpper();
                        AllCategoryPro = AllCategoryPro.Where(r => r.AllFirstLetter.Contains(strquery) || r.skucode.Contains(strquery)).ToList();

                    }

                    if (AllCategoryPro == null || AllCategoryPro.Count == 0)
                    {
                        DgvGoodRowClear();
                        return;
                    }


                    Paging paging;
                    if (keyBoard.Visible)
                    {
                        paging = CartUtil.GetPaging(CurrentGoodPage, 15, AllCategoryPro.Count, 5);
                    }
                    else
                    {
                        paging = CartUtil.GetPaging(CurrentGoodPage, 30, AllCategoryPro.Count, 5);
                    }

                    if (!paging.success)
                    {
                        MainModel.ShowLog("分页出现异常，请重试", true);
                        DgvGoodRowClear();
                        CurrentGoodPage = 1;
                        return;
                    }

                    List<Image> lstshowimg = new List<Image>();
                    if (paging.haveuppage)
                    {
                        lstshowimg.Add(imgPageUpForGood);
                    }

                    List<Product> lstLaodingPro = AllCategoryPro.GetRange(paging.startindex, paging.endindex - paging.startindex + 1);


                    List<Product> lstNotHaveprice = lstLaodingPro.Where(r => r.panelbmp == null).ToList();
                    //防止转换json  死循环   bmp.tag 是product
                    lstNotHaveprice.ForEach(r => r.panelbmp = null);
                    if (lstNotHaveprice != null && lstNotHaveprice.Count > 0)
                    {
                        MainHelper.SingleCalculate(lstNotHaveprice);

                    }


                    Invoke((new Action(() =>
                    {

                        dgvGood.Rows.Clear();

                        for (int i = 0; i < lstLaodingPro.Count; i++)
                        {

                            if (lstLaodingPro[i].panelbmp == null)
                            {
                                lstLaodingPro[i].panelbmp = GetItemImg(lstLaodingPro[i]);
                            }
                            lstshowimg.Add(lstLaodingPro[i].panelbmp);

                        }

                        if (paging.havedownpage)
                        {
                            lstshowimg.Add(imgPageDownForGood);
                        }



                        for (int i = 0; i < paging.makeupcount; i++)
                        {
                            lstshowimg.Add(ResourcePos.empty);
                        }

                        int rowcount = lstshowimg.Count / 5;

                        for (int i = 0; i < rowcount; i++)
                        {
                            dgvGood.Rows.Add(lstshowimg[i * 5 + 0], lstshowimg[i * 5 + 1], lstshowimg[i * 5 + 2], lstshowimg[i * 5 + 3], lstshowimg[i * 5 + 4]);
                        }
                    })));
                    IsEnable = true;

                }
                catch (Exception ex)
                {
                    LogManager.WriteLog("ERROR", "加载面板商品异常" + ex.Message);
                }
            }
        }

        private Bitmap GetItemImg(Product pro)
        {
            try
            {

                switch (pro.pricetagid)
                {
                    case 1: lblGoodPricetag.Visible = true; lblGoodPricetag.Text = "会员"; lblGoodPricetag.BackColor = ColorTranslator.FromHtml("#FF7D14"); break;
                    case 2: lblGoodPricetag.Visible = true; lblGoodPricetag.Text = "折扣"; lblGoodPricetag.BackColor = ColorTranslator.FromHtml("#209FD4"); break;
                    case 3: lblGoodPricetag.Visible = true; lblGoodPricetag.Text = "直降"; lblGoodPricetag.BackColor = ColorTranslator.FromHtml("#D42031"); break;
                    case 4: lblGoodPricetag.Visible = true; lblGoodPricetag.Text = "优享"; lblGoodPricetag.BackColor = ColorTranslator.FromHtml("#250D05"); break;
                    default: lblGoodPricetag.Visible = false; break;
                }

                if (lblGoodPricetag.Visible)
                {
                    lblGoodName.Text = "        " + pro.skuname;
                }
                else
                {
                    lblGoodName.Text = pro.skuname;
                }

                lblGoodCode.Text = pro.skucode;



                if (pro.price != null)
                {
                    if (pro.goodstagid != 0 && MainModel.WhetherShowWithJin)
                    {

                        lblPrice.Text = "￥" + Math.Round(pro.price.saleprice / 2, 2, MidpointRounding.AwayFromZero).ToString("f2");
                        lblPriceDetail.Text = "/斤";
                    }
                    else
                    {
                        lblPrice.Text = "￥" + pro.price.saleprice.ToString("f2");
                        lblPriceDetail.Text = "/" + pro.saleunit;
                    }


                }
                else
                {
                }


                lblPriceDetail.Left = lblPrice.Left + lblPrice.Width - 3;

                Bitmap b = (Bitmap)MainModel.GetControlImage(pnlGoodNotSelect);
                b.Tag = pro;
                return b;
            }
            catch
            {
                return ResourcePos.empty;
            }
        }
        #endregion

        #region 公用

        //TODO  修改样式
        private void ShowLog(string msg, bool iserror)
        {
            ParameterizedThreadStart Pts = new ParameterizedThreadStart(showlogthread);
            Thread threadmqtt = new Thread(Pts);
            threadmqtt.IsBackground = true;
            threadmqtt.Start(msg);

            if (iserror)
            {
                LogManager.WriteLog(msg);
            }
        }

        private void showlogthread(object obj)
        {
            try
            {
                lblToast.Text = obj.ToString();

                lblToast.Left = (this.Width - lblToast.Width) / 2;
                if (this.IsHandleCreated)
                {
                    this.Invoke(new InvokeHandler(delegate ()
                    {
                        lblToast.Visible = true;
                    }));
                }
                else
                {
                    lblToast.Visible = true;
                }

                Thread.Sleep(1000);
                if (this.IsHandleCreated)
                {
                    this.Invoke(new InvokeHandler(delegate ()
                    {
                        lblToast.Visible = false;
                    }));
                }
                else
                {
                    lblToast.Visible = false;
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="showloading">显示等待框</param>
        /// <param name="isenable">页面是否可操作</param>
        private void ShowLoading(bool showloading, bool isenable)
        {
            try
            {
                IsEnable = isenable;
                if (this.IsHandleCreated)
                {
                    this.Invoke(new InvokeHandler(delegate ()
                    {
                        pnlLoading.Visible = showloading;

                    }));
                }
                else
                {
                    pnlLoading.Visible = showloading;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("显示等待异常" + ex.Message);
            }

        }

        private void ShowLoadingThread(object obj)
        {
            //try
            //{
            //    bool showloading = (bool)obj;
            //    if (this.IsHandleCreated)
            //    {
            //        this.Invoke(new InvokeHandler(delegate()
            //        {
            //            picLoading.Visible = showloading;
            //            pnlLoading.Visible = showloading;

            //        }));
            //    }
            //    else
            //    {
            //        picLoading.Visible = showloading;
            //        pnlLoading.Visible = showloading;
            //    }

            //}
            //catch (Exception ex)
            //{
            //    LogManager.WriteLog("显示等待异常" + ex.Message);
            //}
        }



        private void CheckUserAndMember(int resultcode, string ErrorMsg)
        {
            try
            {
                this.Invoke(new InvokeHandler(delegate ()
                {
                    if (resultcode == MainModel.HttpUserExpired)
                    {
                        // LoadPicScreen(true);
                        MainModel.CurrentMember = null;
                        frmUserExpired frmuserexpired = new frmUserExpired();
                        frmuserexpired.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmuserexpired.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmuserexpired.Height) / 2);
                        frmuserexpired.TopMost = true;
                        frmuserexpired.ShowDialog();

                        INIManager.SetIni("System", "POS-Authorization", "", MainModel.IniPath);

                        try { MainModel.frmlogin.Show(); }
                        catch { }
                        BaseUIHelper.CloseFormMain();

                    }
                    else if (resultcode == MainModel.HttpMemberExpired)
                    {
                        ClearMember();
                        if (!ZhuiZhi_Integral_Scale_UncleFruit.HelperUI.ConfirmHelper.Confirm("会员登录已过期，请重新登录", "", false))
                        {
                            IsEnable = true;

                            //UploadOffLineDgvCart();
                            return;
                        }

                        cbtnLoadPhone_ButtonClick(null, null);
                        IsEnable = true;
                    }
                    else
                    {
                        // ShowLog(ErrorMsg, false);
                    }
                }));

            }
            catch (Exception ex)
            {

                //ShowLog("验证用户/会员异常", true);
            }
            finally
            {
                IsEnable = true;
            }

        }

        #endregion

        #region 扫描数据处理

        StringBuilder scancode = new StringBuilder();
        protected override bool ProcessDialogKey(Keys keyData)
        {
            try
            {

                if (txtSearch.Focused)
                {
                    return false;
                }
                switch (keyData)
                {
                    //不同键盘数字键值不同
                    case Keys.D0: scancode.Append("0"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.D1: scancode.Append("1"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.D2: scancode.Append("2"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.D3: scancode.Append("3"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.D4: scancode.Append("4"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.D5: scancode.Append("5"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.D6: scancode.Append("6"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.D7: scancode.Append("7"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.D8: scancode.Append("8"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.D9: scancode.Append("9"); return !base.ProcessDialogKey(keyData); break;

                    case Keys.NumPad0: scancode.Append("0"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.NumPad1: scancode.Append("1"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.NumPad2: scancode.Append("2"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.NumPad3: scancode.Append("3"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.NumPad4: scancode.Append("4"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.NumPad5: scancode.Append("5"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.NumPad6: scancode.Append("6"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.NumPad7: scancode.Append("7"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.NumPad8: scancode.Append("8"); return !base.ProcessDialogKey(keyData); break;
                    case Keys.NumPad9: scancode.Append("9"); return !base.ProcessDialogKey(keyData); break;

                        //case Keys.Back: AddNum(0, true); return base.ProcessDialogKey(keyData); break;
                        //case Keys.Enter: QueueScanCode.Enqueue(scancode.ToString()); scancode = new StringBuilder(); return !base.ProcessDialogKey(keyData); break;
                }
                if (keyData == Keys.Enter)
                {
                    QueueScanCode.Enqueue(scancode.ToString().Trim());
                    scancode = new StringBuilder();
                    return false;
                }
                if (keyData == Keys.Space)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("监听键盘事件异常" + ex.Message);
                return false;
            }

        }

        private object thislockScanCode = new object();
        private void ScanCodeThread(object obj)
        {
            while (IsRun)
            {
                if (QueueScanCode.Count > 0 && IsEnable)
                {
                    try
                    {
                        string logCode = "";
                        ShowLoading(true, false);

                        List<string> LstScanCode = new List<string>();

                        List<string> lstNotLocalCode = new List<string>();
                        while (QueueScanCode.Count > 0)
                        {
                            string tempcode = QueueScanCode.Dequeue();
                            logCode += tempcode + " ";
                            if (!string.IsNullOrEmpty(tempcode))
                            {
                                LstScanCode.Add(tempcode);
                            }
                        }
                        List<ScanModelAndDbpro> LstScancodemember = new List<ScanModelAndDbpro>();
                        foreach (string scancode in LstScanCode)
                        {
                            DBPRODUCT_BEANMODEL tempdbpro = MainHelper.GetLocalPro(scancode);
                            if (tempdbpro != null)
                            {

                                if (tempdbpro.STATUS != 1 && (tempdbpro.SKUTYPE != 1 || tempdbpro.SKUTYPE != 4))
                                {
                                    //ShowLog("条码不正确",false);
                                }
                                else
                                {
                                    ScanModelAndDbpro tempsd = new ScanModelAndDbpro();
                                    tempsd.dbproduct = tempdbpro;
                                    tempsd.ScanModel = MainHelper.GetScancodeMemberByDbpro(tempdbpro);
                                    LstScancodemember.Add(tempsd);
                                }

                            }
                            else
                            {
                                lstNotLocalCode.Add(scancode);
                            }
                        }

                        bool ismember = false;
                        lstNotLocalCode = lstNotLocalCode.Distinct().ToList(); //去重复，防止一直扫描会员码
                        foreach (string goodcode in lstNotLocalCode)
                        {
                            //IsScan = false;
                            string ErrorMsg = "";
                            int ResultCode = 0;
                            scancodememberModel scancodemember = httputil.GetSkuInfoMember(goodcode, ref ErrorMsg, ref ResultCode);

                            if (ErrorMsg != "" || scancodemember == null) //商品不存在或异常
                            {
                                //CheckUserAndMember(ResultCode, ErrorMsg);
                            }
                            else
                            {
                                if (scancodemember.type == "MEMBER")
                                {
                                    ismember = true;
                                    LoadMember(scancodemember.memberresponsevo);
                                }
                                //else
                                //{
                                //    ScanModelAndDbpro tempsd = new ScanModelAndDbpro();
                                //tempsd.dbproduct=null;
                                //tempsd.ScanModel=scancodemember;
                                //    LstScancodemember.Add(tempsd);    
                                //}
                            }
                        }
                        ShowLoading(false, true);// LoadingHelper.CloseForm();
                        DateTime starttime = DateTime.Now;
                        if (LstScancodemember.Count > 0)
                        {
                            FlashSkuCode = LstScancodemember[0].ScanModel.scancodedto.skucode;

                            if (this.IsHandleCreated)
                            {
                                this.Invoke(new InvokeHandler(delegate ()
                                {
                                    addcart(LstScancodemember);
                                }));
                            }
                            else
                            {
                                addcart(LstScancodemember);
                            }
                        }
                        else
                        {
                            if (!ismember)
                            {
                                ShowLog("条码识别错误", false);
                                LogManager.WriteLog("SCAN", logCode);
                            }
                        }

                        Console.WriteLine("离线扫描计算时间" + (DateTime.Now - starttime).TotalMilliseconds);
                        Thread.Sleep(1);
                    }
                    catch (Exception ex)
                    {
                        ShowLoading(false, true);// LoadingHelper.CloseForm();//关闭
                        LogManager.WriteLog("ERROR", "扫描数据处理异常：" + ex.Message);
                    }
                    finally
                    {
                        ShowLoading(false, true);// LoadingHelper.CloseForm();
                    }
                }

                Thread.Sleep(100);
            }
        }

        #endregion

        #region 购物车
        private Product SelectProduct = null;
        private string LastSkuCode = "";
        private void dgvGood_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DateTime starttime = DateTime.Now;
                if (!IsEnable)
                {
                    Console.WriteLine("bukeyong");
                    return;
                }

                //IsEnable = false;
                if (e.RowIndex < 0)
                    return;

                Image selectimg = (Image)dgvGood.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;


                if (LastSkuCode != "")
                {
                    //遍历单元格清空之前的选中状态
                    for (int i = 0; i < this.dgvGood.Rows.Count; i++)
                    {
                        for (int j = 0; j < this.dgvGood.Columns.Count; j++)
                        {
                            Image lastimg = (Image)dgvGood.Rows[i].Cells[j].Value;

                            if (lastimg.Tag != null && ((Product)lastimg.Tag).skucode == LastSkuCode)
                            {
                                this.Invoke(new InvokeHandler(delegate ()
                                {
                                    dgvGood.Rows[i].Cells[j].Value = GetItemImg((Product)lastimg.Tag);
                                }));
                                break;
                            }
                        }
                    }
                }

                if (selectimg == imgPageDownForGood)
                {
                    CurrentGoodPage++;
                    LoadDgvGood(false, false);
                    return;
                }

                if (selectimg == imgPageUpForGood)
                {
                    CurrentGoodPage--;
                    LoadDgvGood(false, false);
                    return;
                }


                if (selectimg.Tag == null)  //空白单元格（无商品）
                {
                    return;
                }



                Product pro = CartUtil.GetNewProduct((Product)selectimg.Tag);
                pro.RowNum = 1;
                //pnlGoodNotSelect.BackgroundImage = picGoodSelect.Image;
                pnlGoodNotSelect.BackColor = Color.FromArgb(207, 241, 255);
                this.Invoke(new InvokeHandler(delegate ()
                {
                    dgvGood.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = GetItemImg(pro);
                }));
                // pnlGoodNotSelect.BackgroundImage = picGoodNotSelect.Image;
                pnlGoodNotSelect.BackColor = Color.White;

                Console.WriteLine("刷新dgvgood时间" + (DateTime.Now - starttime).TotalMilliseconds);


                SelectProduct = pro;
                LastSkuCode = pro.skucode;
                isCartRefresh = true;
                FlashSkuCode = pro.skucode;
                if (pro.goodstagid == 0) //标品
                {
                    InsertProductToCart(pro);
                    UploadOffLineDgvCart();


                    if (MainModel.WhetherPrint)
                    {
                        LabelPrintHelper.LabelPrint(pro);
                    }

                    SelectProduct = null;
                }
                else
                {

                    if (!MainModel.WhetherAutoCart)
                    {
                        if (ScaleHelper.ShowFormScale(pro))
                        {
                            InsertProductToCart(pro);

                            UploadOffLineDgvCart();

                            SelectProduct = null;
                        }
                    }

                }
                Console.WriteLine("刷新购物车时间" + (DateTime.Now - starttime).TotalMilliseconds);

                ShowLoading(false, true);
                Console.WriteLine("刷新isenable时间" + (DateTime.Now - starttime).TotalMilliseconds);

            }
            catch (Exception ex)
            {
                ShowLoading(false, true);
                ShowLog("选择商品异常" + ex.StackTrace, true);
            }
            finally
            {
                btnScan.Select();
            }
        }

        private void ClearDgvGoodSelect()
        {
            try
            {
                DataGridViewCell dgc = dgvGood.CurrentCell;
                Image lastimg = (Image)dgc.Value;

                if (lastimg.Tag != null)
                {
                    this.Invoke(new InvokeHandler(delegate ()
                    {
                        dgc.Value = GetItemImg((Product)lastimg.Tag);
                    }));
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("清除商品列表选择项异常" + ex.Message);
            }
        }
        private void addcart(List<ScanModelAndDbpro> lstscanmodelandpro)
        {

            try
            {

                DateTime starttime = DateTime.Now;
                foreach (ScanModelAndDbpro scancodemember in lstscanmodelandpro)
                {

                    if (scancodemember.ScanModel.scancodedto.weightflag && scancodemember.ScanModel.scancodedto.specnum == 0)
                    {
                        string numbervalue = NumberHelper.ShowFormNumber("输入会员手机号", NumberType.MemberCode);
                        if (!string.IsNullOrEmpty(numbervalue))
                        {

                            scancodemember.ScanModel.scancodedto.specnum = Convert.ToDecimal(numbervalue) / 1000;
                            scancodemember.ScanModel.scancodedto.num = 1;
                        }
                        else
                        {
                            Application.DoEvents();
                            return;
                        }
                        Application.DoEvents();
                    }

                    Product pro = new Product();
                    pro.title = scancodemember.ScanModel.scancodedto.skuname;
                    pro.skuname = scancodemember.ScanModel.scancodedto.skuname;
                    pro.skucode = scancodemember.ScanModel.scancodedto.skucode;
                    pro.num = scancodemember.ScanModel.scancodedto.num;
                    pro.specnum = scancodemember.ScanModel.scancodedto.specnum;
                    pro.spectype = scancodemember.ScanModel.scancodedto.spectype;
                    pro.goodstagid = scancodemember.ScanModel.scancodedto.weightflag == true ? 1 : 0;
                    pro.barcode = scancodemember.ScanModel.scancodedto.barcode;
                    pro.weightflag = scancodemember.ScanModel.scancodedto.weightflag;

                    if (scancodemember.dbproduct != null)
                    {
                        pro.firstcategoryid = scancodemember.dbproduct.FIRSTCATEGORYID;
                        pro.secondcategoryid = scancodemember.dbproduct.SECONDCATEGORYID;
                        pro.categoryid = scancodemember.dbproduct.SECONDCATEGORYID;
                        Price price = new Price();
                        price.saleprice = scancodemember.dbproduct.SALEPRICE;
                        price.originprice = scancodemember.dbproduct.ORIGINPRICE;
                        price.specnum = scancodemember.dbproduct.SPECNUM;
                        price.unit = scancodemember.dbproduct.SALESUNIT;
                        pro.price = price;
                    }

                    InsertProductToCart(pro);
                }
                Console.WriteLine("addcart  计算时间" + (DateTime.Now - starttime).TotalMilliseconds);
                UploadOffLineDgvCart();
                Console.WriteLine("addcart  更新页面" + (DateTime.Now - starttime).TotalMilliseconds);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ERROR", "添加购物车商品异常:" + ex.Message);
            }
            finally
            {
                ShowLoading(false, true);// LoadingHelper.CloseForm();

            }

        }

        private bool RefreshCart()
        {
            try
            {
                // isGoodRefresh = false;
                DateTime starttime = DateTime.Now;
                string ErrorMsgCart = "";
                int ResultCode = -1;


                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                {

                    if (MainModel.CurrentMember != null && MainModel.CurrentMember.isUsePoint)
                    {
                        CurrentCart.pointpayoption = 1;
                    }
                    else
                    {
                        CurrentCart.pointpayoption = 0;
                    }


                    Cart cart = httputil.RefreshCart(CurrentCart, ref ErrorMsgCart, ref ResultCode);
                    Console.WriteLine("购物车接口时间" + (DateTime.Now - starttime).TotalMilliseconds);
                    if (ErrorMsgCart != "" || cart == null) //商品不存在或异常
                    {

                        ShowLog(ErrorMsgCart, false);

                        if (ResultCode == MainModel.Code_260058) //商品促销商品发生变化，重置修改价格
                        {
                            CurrentCart.fixpricetotal = 0;
                            cart = httputil.RefreshCart(CurrentCart, ref ErrorMsgCart, ref ResultCode);
                            if (ErrorMsgCart != "" || cart == null) //商品不存在或异常
                            {
                                CheckUserAndMember(ResultCode, ErrorMsgCart);

                                return false;
                            }
                        }
                        else if (ResultCode == MainModel.Code_260011)//优惠券无效清空优惠券
                        {
                            MainModel.CurrentCouponCode = "";
                            MainModel.Currentavailabecoupno = null;
                            cart = httputil.RefreshCart(CurrentCart, ref ErrorMsgCart, ref ResultCode);
                            if (ErrorMsgCart != "" || cart == null) //商品不存在或异常
                            {
                                ShowLog(ErrorMsgCart, false);
                                CheckUserAndMember(ResultCode, ErrorMsgCart);
                                return false;
                            }
                        }

                        else
                        {
                            CheckUserAndMember(ResultCode, ErrorMsgCart);
                            return false;
                        }

                    }



                    CurrentCart = cart;

                    UploadDgvCart(cart);
                    Console.WriteLine("表格加载时间" + (DateTime.Now - starttime).TotalMilliseconds);

                    if (MainModel.CurrentMember != null && MainModel.CurrentMember.isUsePoint)
                    {
                        if (MainModel.CurrentMember != null && CurrentCart != null && CurrentCart.pointinfo != null)
                        {
                            lblCreditStr.Text = "积分" + CurrentCart.pointinfo.totalpoints;

                            lblCredit.Text = "使用" + CurrentCart.pointinfo.availablepoints;
                            picCredit.BackgroundImage = picSelectCredit.Image;
                            lblCredit.Visible = true;
                            picCredit.Visible = true;

                            //有会员登录每次刷新购物车都刷一次优惠券接口
                            Thread threadloadMember = new Thread(ZhuiZhi_Integral_Scale_UncleFruit.Model.HalfOffLine.HalfOffLineUtil.ListMemberCouponAvailable);
                            threadloadMember.IsBackground = true;
                            threadloadMember.Start();

                        }
                    }
                    else
                    {
                        if (MainModel.CurrentMember != null && CurrentCart != null && CurrentCart.pointinfo != null)
                        {
                            lblCreditStr.Text = "积分" + CurrentCart.pointinfo.totalpoints;

                            lblCredit.Text = "可用" + CurrentCart.pointinfo.availablepoints;
                            picCredit.BackgroundImage = picNotSelectCredit.Image;
                            lblCredit.Visible = true;
                            picCredit.Visible = true;
                        }
                        else
                        {
                            lblCredit.Text = "";
                            picCredit.BackgroundImage = picNotSelectCredit.Image;
                            lblCredit.Visible = false;
                            picCredit.Visible = false;
                        }
                    }
                    return true;
                }
                else
                {
                    CurrentCart.unavailablecoupons = CartUtil.GetAllOrderCoupon();
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


        private object thislockDgvCart = new object();
        private void UploadDgvCart(Cart cart)
        {
            lock (thislockDgvCart)
            {
                try
                {

                    UploaddgvCartDetail();
                    if (CurrentCart == null || CurrentCart.products == null || CurrentCart.products.Count == 0)
                    {

                        btnModifyPrice.BackColor = Color.Silver;
                        btnModifyPrice.ForeColor = Color.White;
                        btnModifyPrice.FlatAppearance.BorderColor = Color.Silver;
                        btnOrderCancle.Visible = false;

                        rbtnPay.WhetherEnable = false;
                    }
                    else
                    {

                        btnModifyPrice.BackColor = Color.White;
                        btnModifyPrice.ForeColor = Color.FromArgb(42, 133, 178);
                        btnModifyPrice.FlatAppearance.BorderColor = Color.FromArgb(42, 133, 178);
                        btnOrderCancle.Visible = true;

                        rbtnPay.WhetherEnable = true;
                    }

                    CurrentCartPage = 1;
                    LoadDgvCart();


                    lblCoupon.Text = "共" + CartUtil.GetAllCouponCount(CurrentCart) + "张";
                    if (CurrentCart.availablecoupons != null && CurrentCart.availablecoupons.Count > 0)
                    {

                        if (CurrentCart.couponpromoamt > 0)
                        {
                            string temppromoamt = CurrentCart.couponpromoamt.ToString();

                            if (temppromoamt.Length > 5)
                            {
                                temppromoamt = temppromoamt.Substring(0, 5).TrimEnd('.');  //移除最后一位点防止 10000.1 情况 截取后末尾是.
                            }

                            lblCoupon.Text = "-￥" + temppromoamt;
                        }
                        else
                        {
                            MainModel.CurrentCouponCode = "";
                        }
                    }
                    else
                    {
                        MainModel.CurrentCouponCode = "";
                    }

                    if (cart.totalpayment == 0 && cart.products != null && cart.products.Count > 0)
                    {
                        rbtnPay.ShowText = "完成";
                        rbtnPay.AllBackColor = Color.FromArgb(255, 70, 21);
                    }
                    UpdateOrderHang();
                    BaseUIHelper.UpdaForm(CurrentCart);

                    ClearDgvGoodSelect();
                    this.Activate();

                }
                catch (Exception ex)
                {
                    DgvGoodRefresh();
                    LogManager.WriteLog("更新显示列表异常" + ex.Message + ex.StackTrace);
                }
            }
        }


        private void LoadDgvCart()
        {
            try
            {

                dgvCart.Rows.Clear();
                btnDeletePro.BackColor = Color.FromArgb(200, 200, 200);
                btnChangePrice.BackColor = Color.FromArgb(200, 200, 200);
                btnDiscount.BackColor = Color.FromArgb(200, 200, 200);
                OperationProduct = null;

                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                {

                    int count = CurrentCart.products.Count;
                    int goodscount = 0;
                    foreach (Product pro in CurrentCart.products)
                    {
                        goodscount += pro.num;
                    }
                    CurrentCart.goodscount = goodscount;

                    rbtnPay.ShowText = "结算(" + goodscount + ")";
                    rbtnPay.AllBackColor = Color.FromArgb(194, 52, 49);
                    if (count == 0)
                    {
                        pnlWaiting.Show();
                    }
                    else
                    {
                        pnlWaiting.Visible = false;

                        CurrentCart.products.Reverse();
                        rbtnPageUpForCart.WhetherEnable = CurrentCartPage > 1;


                        int startindex = (CurrentCartPage - 1) * 5;

                        int lastindex = Math.Min(CurrentCart.products.Count - 1, startindex + 4);

                        List<Product> lstLoadingPro = CurrentCart.products.GetRange(startindex, lastindex - startindex + 1);


                        int allcount = CurrentCart.products.Count;

                        int rownum = allcount - (CurrentCartPage - 1) * 5;
                        DateTime starttime = DateTime.Now;
                        foreach (Product por in lstLoadingPro)
                        {

                            dgvCart.Rows.Add(GetDgvRow(por, rownum));
                            rownum--;
                        }
                        dgvCart.ClearSelection();


                        txtSearch.Clear();
                        rbtnPageDownForCart.WhetherEnable = CurrentCart.products.Count > CurrentCartPage * 5;

                        CurrentCart.products.Reverse();
                        Application.DoEvents();

                        Thread threadItemExedate = new Thread(ShowDgv);
                        threadItemExedate.IsBackground = true;
                        threadItemExedate.SetApartmentState(ApartmentState.STA);
                        threadItemExedate.Start();


                    }
                }


            }
            catch (Exception ex)
            {
                LogManager.WriteLog("加载购物车列表异常" + ex.Message);
            }
        }


        private void btnPageUpForCart_Click(object sender, EventArgs e)
        {

            if (!rbtnPageUpForCart.WhetherEnable || !IsEnable)
            {
                return;
            }
            if (CurrentCartPage > 1)
            {
                CurrentCartPage--;
                LoadDgvCart();
            }
        }

        private void btnPageDownForCart_Click(object sender, EventArgs e)
        {

            if (!rbtnPageDownForCart.WhetherEnable || !IsEnable)
            {
                return;
            }
            CurrentCartPage++;
            LoadDgvCart();
        }



        private object thislockOrderDetail = new object();
        private void UploaddgvCartDetail()
        {
            lock (thislockOrderDetail)
            {
                try
                {
                    dgvCartDetail.Rows.Clear();

                    List<string> lstcartdetail = new List<string>();

                    if (CurrentCart != null && CurrentCart.orderpricedetails != null)
                    {
                        foreach (OrderPriceDetail orderprice in CurrentCart.orderpricedetails)
                        {

                            //只显示优惠，不显示
                            if (!orderprice.title.Contains("商品金额"))
                            {
                                lstcartdetail.Add(orderprice.title + ":" + orderprice.amount);

                            }

                        }

                        dgvCartDetail.ClearSelection();
                    }


                    if (MainModel.CurrentMember != null)
                    {
                        if (CurrentCart.memberpromo != null && CurrentCart.memberpromo > 0)
                        {
                            lstcartdetail.Add("会员优惠:-￥" + CurrentCart.memberpromo.ToString("f2"));
                        }

                    }
                    else
                    {
                        if (CurrentCart.memberpromo != null && CurrentCart.memberpromo > 0)
                        {
                            //lstcartdetail.Add("会员可优惠:-￥" + CurrentCart.memberpromo.ToString("f2"));
                        }
                    }

                    lblTotalPay.Text = "￥" + CurrentCart.totalpayment.ToString("f2");

                    if (lstcartdetail.Count != 0)
                    {
                        if (lstcartdetail.Count % 2 == 1)
                        {
                            lstcartdetail.Add("");
                        }

                        int rowscount = lstcartdetail.Count / 2;

                        for (int i = 0; i < rowscount; i++)
                        {
                            dgvCartDetail.Rows.Add(lstcartdetail[i * 2], lstcartdetail[i * 2 + 1]);
                        }

                        dgvCartDetail.Height = rowscount * dgvCartDetail.RowTemplate.Height + 5;

                        dgvCartDetail.Top = lblTotalPay.Top - 10 - dgvCartDetail.Height;

                        rbtnPay.Top = dgvCartDetail.Top;
                        rbtnPay.Height = dgvCartDetail.Height + lblTotalPay.Height + 10;
                    }
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog("更新订单价格详情异常" + ex.Message + ex.StackTrace);
                }
            }
        }

        //更新挂单按钮  购物车没有商品且有挂单信息时 按钮text="挂单列表"   按钮点击事件根据文本判断事件  更新取消交易
        private void UpdateOrderHang()
        {
            try
            {
                btnOrderHang.Text = "挂单列表";
                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                {
                    btnOrderHang.Text = "挂单";
                    btnOrderHang.Image = null;
                }
                else
                {
                    btnOrderHang.Text = "挂单列表";

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

        private string FlashSkuCode = "";
        private int FlashIndex = 0;
        //放线程就无效了
        public bool isCartRefresh = true;
        private void ShowDgv()
        {
            try
            {
                if (dgvCart.Rows.Count >= FlashIndex && isCartRefresh)
                {
                    isCartRefresh = false;
                    System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
                    dataGridViewCellStyle1.BackColor = Color.PeachPuff;
                    Color color = dgvCart.Rows[FlashIndex].DefaultCellStyle.BackColor;

                    dgvCart.Rows[FlashIndex].DefaultCellStyle = dataGridViewCellStyle1;

                    Delay.Start(200);
                    dataGridViewCellStyle1.BackColor = color;
                    dgvCart.Rows[FlashIndex].DefaultCellStyle = dataGridViewCellStyle1;

                    FlashIndex = 0;
                    FlashSkuCode = "";
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("突出新增商品异常" + ex.Message);
            }
        }


        private Bitmap GetDgvRow(Product pro, int rownum)
        {
            try
            {
                Bitmap bmpPro;

                Bitmap add = Resources.ResourcePos.empty;

                lblRowNum.Text = rownum.ToString();
                lblTitle.Text = pro.title;
                //lblSkuCode.Text = pro.skucode;
                //第一行图片
                switch (pro.pricetagid)
                {
                    case 1: lblMember.Visible = true; lblMember.Text = "会员"; lblMember.BackColor = ColorTranslator.FromHtml("#FF7D14"); break;
                    case 2: lblMember.Visible = true; lblMember.Text = "折扣"; lblMember.BackColor = ColorTranslator.FromHtml("#209FD4"); break;
                    case 3: lblMember.Visible = true; lblMember.Text = "直降"; lblMember.BackColor = ColorTranslator.FromHtml("#D42031"); break;
                    case 4: lblMember.Visible = true; lblMember.Text = "优享"; lblMember.BackColor = ColorTranslator.FromHtml("#250D05"); break;
                    default: lblMember.Visible = false; lblMember.Text = ""; break;
                }

                lblTitle.Left = lblMember.Right;

                lblSinglePrice.Text = "￥" + pro.price.saleprice.ToString("f2");
                //第二列图片
                if (pro.price.saleprice == pro.price.originprice)
                {

                    lblOriginPrice.Text = "";
                }
                else
                {
                    lblOriginPrice.Text = "";
                    lblOriginPrice.Text = "(" + "￥" + pro.price.originprice.ToString("f2") + ")";
                    lblOriginPrice.Left = lblSinglePrice.Right;
                }

                //第三 四列图片
                if (pro.goodstagid == 0)  //0是标品  1是称重
                {
                    picAdd.Visible = true;
                    picMinus.Visible = true;

                    lblProNum.Font = new Font("微软雅黑", lblTotal.Font.Size);
                    lblProNum.Text = pro.num.ToString();
                    lblProNum.Left = (picAdd.Left - picMinus.Right - lblProNum.Width) / 2 + picMinus.Right;
                }
                else
                {
                    picAdd.Visible = false;
                    picMinus.Visible = false;
                    lblProNum.Font = new Font("微软雅黑", lblTotal.Font.Size + 1); //散称字体放大一号
                    lblProNum.Text = pro.price.specnum + pro.price.unit;
                    lblProNum.Left = picMinus.Left;
                }

                //单品改价

                if (pro.adjustpriceinfo != null && pro.adjustpricedesc != null)
                {
                    pnlAdjust.Visible = true;
                    lblAdjust.Text = pro.adjustpricedesc.amtdesc;
                    pnlAdjust.Width = lblAdjust.Width + 5;
                    pnlAdjust.Left = pnlCartItem.Width - lblAdjust.Width;

                }
                else
                {
                    pnlAdjust.Visible = false;
                }

                lblTotal.Text = "￥" + pro.price.total.ToString("f2");
                bmpPro = new Bitmap(pnlCartItem.Width, pnlCartItem.Height);
                bmpPro.Tag = pro;
                pnlCartItem.DrawToBitmap(bmpPro, new Rectangle(0, 0, pnlCartItem.Width, pnlCartItem.Height));

                bmpPro.MakeTransparent(Color.White);

                return bmpPro;
            }
            catch (Exception ex)
            {
                ShowLog("解析商品信息异常" + ex.Message, true);
                return null;
            }
        }

        private void ClearForm()
        {
            try
            {
                CurrentCart = new Cart();
                dgvCartDetail.Rows.Clear();
                rbtnPageUpForCart.WhetherEnable = false;
                rbtnPageDownForCart.WhetherEnable = false;

                dgvCart.Rows.Clear();
                btnDeletePro.BackColor = Color.FromArgb(200, 200, 200);
                btnChangePrice.BackColor = Color.FromArgb(200, 200, 200);
                btnDiscount.BackColor = Color.FromArgb(200, 200, 200);
                OperationProduct = null;
                dgvCartDetail.Rows.Clear();
                lblTotalPay.Text = "￥" + "0.00";
                rbtnPay.ShowText = "结算";

                btnOrderCancle.Visible = false;
                btnModifyPrice.BackColor = Color.Silver;
                btnModifyPrice.ForeColor = Color.White;
                btnModifyPrice.FlatAppearance.BorderColor = Color.Silver;

                rbtnPay.WhetherEnable = false;
                pnlWaiting.Show();

                UpdateOrderHang();

                lblCredit.Visible = false;
                picCredit.Visible = false;
                //lblCoupon.Text = "0张";
                Other.CrearMemory();

                BaseUIHelper.IniFormMainMedia();
                Application.DoEvents();

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("清空主界面异常" + ex.Message);
            }
        }


        #endregion

        #region 半离线

        private object thislockOffLineDgvGood = new object();
        private void UploadOffLineDgvCart()
        {
            lock (thislockOffLineDgvGood)
            {

                if (!MainModel.WhetherHalfOffLine)
                {
                    ShowLoading(true, false);

                    try
                    {
                        Product rownumpro = CurrentCart.products[CurrentCart.products.Count - 1];

                        if (rownumpro.goodstagid == 0 && rownumpro.RowNum > 0)
                        {
                            foreach (Product pro in CurrentCart.products)
                            {
                                if (pro.skucode == rownumpro.skucode)
                                {
                                    pro.RowNum = 1;
                                }
                            }
                            CurrentCart.products = CurrentCart.products.OrderBy(r => r.RowNum).ToList();
                        }

                    }
                    catch { }
                    RefreshCart();
                    ShowLoading(false, true);
                    return;
                }

                try
                {
                    MainHelper.CartPromotion(CurrentCart, whetherfix);

                    whetherfix = false;

                    UploaddgvCartDetail();


                    if (CurrentCart == null || CurrentCart.products == null || CurrentCart.products.Count == 0)
                    {
                        btnModifyPrice.BackColor = Color.Silver;
                        btnModifyPrice.ForeColor = Color.White;
                        btnModifyPrice.FlatAppearance.BorderColor = Color.Silver;
                        rbtnPay.WhetherEnable = false;

                        ClearForm();
                    }
                    else
                    {


                        btnModifyPrice.BackColor = Color.White;
                        btnModifyPrice.ForeColor = Color.FromArgb(42, 133, 178);
                        btnModifyPrice.FlatAppearance.BorderColor = Color.FromArgb(42, 133, 178);
                        btnOrderCancle.Visible = true;
                        rbtnPay.WhetherEnable = true;
                    }

                    CurrentCartPage = 1;
                    LoadDgvCart();

                    if (CurrentCart.products == null || CurrentCart.products.Count == 0)
                    {
                        CurrentCart.unavailablecoupons = CartUtil.GetAllOrderCoupon();
                    }

                    lblCoupon.Text = "共" + CartUtil.GetAllCouponCount(CurrentCart) + "张";
                    if (CurrentCart.availablecoupons != null && CurrentCart.availablecoupons.Count > 0)
                    {

                        if (CurrentCart.couponpromoamt > 0)
                        {
                            lblCoupon.Text = "-￥" + CurrentCart.couponpromoamt;
                        }
                        else
                        {
                            MainModel.CurrentCouponCode = "";
                        }
                    }
                    else
                    {
                        MainModel.CurrentCouponCode = "";
                    }

                    if (MainModel.CurrentMember != null && MainModel.CurrentMember.isUsePoint)
                    {
                        if (MainModel.CurrentMember != null && CurrentCart != null && CurrentCart.pointinfo != null)
                        {
                            lblCreditStr.Text = "积分" + CurrentCart.pointinfo.totalpoints;
                            lblCredit.Text = "使用" + CurrentCart.pointinfo.availablepoints;
                            picCredit.BackgroundImage = picSelectCredit.Image;
                            lblCredit.Visible = true;
                            picCredit.Visible = true;

                            //有会员登录每次刷新购物车都刷一次优惠券接口
                            Thread threadloadMember = new Thread(ZhuiZhi_Integral_Scale_UncleFruit.Model.HalfOffLine.HalfOffLineUtil.ListMemberCouponAvailable);
                            threadloadMember.IsBackground = true;
                            threadloadMember.Start();
                        }
                    }
                    else
                    {
                        if (MainModel.CurrentMember != null && CurrentCart != null && CurrentCart.pointinfo != null)
                        {
                            lblCreditStr.Text = "积分" + CurrentCart.pointinfo.totalpoints;
                            lblCredit.Text = "可用" + CurrentCart.pointinfo.availablepoints;
                            picCredit.BackgroundImage = picNotSelectCredit.Image;
                            lblCredit.Visible = true;
                            picCredit.Visible = true;
                        }
                        else
                        {
                            //lblCredit.Text = "";
                            lblCredit.Text = "";
                            picCredit.BackgroundImage = picNotSelectCredit.Image;
                            lblCredit.Visible = false;
                            picCredit.Visible = false;
                        }
                    }

                    if (CurrentCart.totalpayment == 0 && CurrentCart.products != null && CurrentCart.products.Count > 0)
                    {
                        rbtnPay.ShowText = "完成";
                        rbtnPay.AllBackColor = Color.FromArgb(255, 70, 21);
                    }
                    UpdateOrderHang();
                    BaseUIHelper.UpdaForm(CurrentCart);

                    ClearDgvGoodSelect();
                    this.Activate();

                }
                catch (Exception ex)
                {
                    DgvGoodRefresh();
                    LogManager.WriteLog("更新显示列表异常" + ex.Message + ex.StackTrace);
                }
            }
        }

        private bool InsertProductToCart(Product pro)
        {
            try
            {
                if (CurrentCart == null)
                {
                    CurrentCart = new Cart();
                }
                if (CurrentCart.products == null)
                {
                    CurrentCart.products = new List<Product>();
                }
                if (CurrentCart.products != null && pro.goodstagid == 0)
                {
                    bool newpro = true;
                    foreach (Product exitspro in CurrentCart.products)
                    {
                        if (pro.skucode == exitspro.skucode)
                        {
                            exitspro.num += pro.num;
                            exitspro.price.total = Math.Round(exitspro.num * exitspro.price.saleprice, 2, MidpointRounding.AwayFromZero);
                            exitspro.price.origintotal = Math.Round(exitspro.num * exitspro.price.originprice, 2, MidpointRounding.AwayFromZero);
                            exitspro.PaySubAmt = Math.Round(exitspro.num * exitspro.price.saleprice, 2, MidpointRounding.AwayFromZero);
                            exitspro.RowNum = CurrentCart.products.Count + 1;

                            newpro = false;
                            break;
                        }
                    }

                    if (newpro)
                    {
                        pro.price.total = Math.Round(pro.price.saleprice, 2, MidpointRounding.AwayFromZero);
                        pro.price.origintotal = Math.Round(pro.price.originprice, 2, MidpointRounding.AwayFromZero);
                        pro.PaySubAmt = Math.Round(pro.price.saleprice, 2, MidpointRounding.AwayFromZero);
                        CurrentCart.products.Add(pro);
                    }
                }
                else
                {
                    pro.price.total = Math.Round(pro.price.saleprice * pro.price.specnum, 2, MidpointRounding.AwayFromZero);
                    pro.price.origintotal = Math.Round(pro.price.originprice * pro.price.specnum, 2, MidpointRounding.AwayFromZero);
                    pro.PaySubAmt = Math.Round(pro.price.saleprice * pro.price.specnum, 2, MidpointRounding.AwayFromZero);
                    CurrentCart.products.Add(pro);
                }
                return true;
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("购物车添加商品异常" + ex.Message, true);
                return false;
            }
        }
        #endregion


        Product OperationProduct = null;
        private void dgvCart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }

                if (e.RowIndex < 0)
                    return;

                Bitmap bmp = (Bitmap)dgvCart.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                Product pro = (Product)bmp.Tag;

                LastLstPro = new List<Product>();
                foreach (Product ppro in CurrentCart.products)
                {
                    LastLstPro.Add((Product)MainModel.Clone(ppro));
                }


                Point po = GlobalUtil.GetCursorPos();
                //增加标品
                if (pro.goodstagid == 0 && po.X < (dgvCart.Left + picAdd.Right + 10) && po.X > (dgvCart.Left + picAdd.Left))
                {
                    for (int i = 0; i < CurrentCart.products.Count; i++)
                    {
                        if (CurrentCart.products[i].barcode == pro.barcode)
                        {
                            CurrentCart.products[i].num += 1;
                            CurrentCart.products[i].adjustpriceinfo = null; CurrentCart.products[i].adjustpricedesc = null;  //商品数量有变化清空改价信息
                            break;
                        }
                    }

                    UploadOffLineDgvCart();
                }

                else if (pro.goodstagid == 0 && po.X < (dgvCart.Left + picMinus.Right) && po.X > (dgvCart.Left + picMinus.Left - 10))
                {
                    for (int i = 0; i < CurrentCart.products.Count; i++)
                    {
                        if (CurrentCart.products[i].barcode == pro.barcode)
                        {
                            if (CurrentCart.products[i].num == 1)
                            {
                                if (ZhuiZhi_Integral_Scale_UncleFruit.HelperUI.ConfirmHelper.Confirm("确认删除？", pro.title + pro.skucode))
                                {
                                    ReceiptUtil.EditCancelSingle(1, CurrentCart.products[i].price.origintotal);
                                    AbnormalOrderUtil.DeleteSkuList(CurrentCart.products[i]);
                                    CurrentCart.products.RemoveAt(i);
                                }
                            }
                            else
                            {
                                ReceiptUtil.EditCancelSingle(1, CurrentCart.products[i].price.origintotal);
                                AbnormalOrderUtil.DeleteSkuList(CurrentCart.products[i]);

                                CurrentCart.products[i].num -= 1;
                                CurrentCart.products[i].adjustpriceinfo = null; CurrentCart.products[i].adjustpricedesc = null;  //商品数量有变化清空改价信息
                            }
                            break;
                        }
                    }
                    UploadOffLineDgvCart();
                }
                //自定义标品数量
                else if (pro.goodstagid == 0 && po.X > (dgvCart.Left + picMinus.Right) && po.X < (dgvCart.Left + picAdd.Left))
                {
                    string numstr = NumberHelper.ShowFormNumber(pro.skuname, NumberType.ProNum);
                    if (string.IsNullOrEmpty(numstr))
                    {
                        return;
                    }
                    int newnum = Convert.ToInt16(numstr);
                    for (int i = 0; i < CurrentCart.products.Count; i++)
                    {
                        if (CurrentCart.products[i].barcode == pro.barcode)
                        {
                            CurrentCart.products[i].num = newnum;
                            CurrentCart.products[i].adjustpriceinfo = null; CurrentCart.products[i].adjustpricedesc = null;  //商品数量有变化清空改价信息
                            break;
                        }
                    }

                    UploadOffLineDgvCart();
                }
                else if (pro.goodstagid != 0 && po.X < (dgvCart.Left + lblProNum.Right + 10) && po.X > (dgvCart.Left + lblProNum.Left - 10))
                {
                    decimal newweight = BrokenHelper.ShowBrokenNumber(pro.skuname);

                    if (newweight > 0)
                    {
                        for (int i = 0; i < CurrentCart.products.Count; i++)
                        {
                            if (CurrentCart.products[i].skucode == pro.skucode && CurrentCart.products[i].specnum == pro.specnum)
                            {
                                CurrentCart.products[i].specnum = newweight;
                                CurrentCart.products[i].price.specnum = newweight;
                                CurrentCart.products[i].num = 1;
                                break;
                            }
                        }
                    }

                    UploadOffLineDgvCart();

                }
                else
                {
                    OperationProduct = pro;
                    btnDeletePro.BackColor = Color.FromArgb(22, 135, 206);
                    btnChangePrice.BackColor = Color.FromArgb(22, 135, 206);
                    btnDiscount.BackColor = Color.FromArgb(22, 135, 206);
                }

            }
            catch (Exception ex)
            {
                ShowLog("操作购物车商品异常" + ex.Message, true);
            }
            finally
            {
                btnScan.Select();
            }
        }

        private void btnDeletePro_Click(object sender, EventArgs e)
        {
            try
            {
                if (OperationProduct == null) { return; }

                if (!ZhuiZhi_Integral_Scale_UncleFruit.HelperUI.ConfirmHelper.Confirm("确认删除？", OperationProduct.title + OperationProduct.skucode))
                {
                    return;
                }

                foreach (Product delpro in CurrentCart.products)
                {
                    if (delpro.skucode == OperationProduct.skucode && delpro.specnum == OperationProduct.specnum)
                    {
                        ReceiptUtil.EditCancelSingle(delpro.num, delpro.price.origintotal);
                        AbnormalOrderUtil.DeleteSkuList(delpro);

                        CurrentCart.products.Remove(delpro);
                        break;
                    }
                }
                UploadOffLineDgvCart();

            }
            catch (Exception ex)
            {
            }
        }

        private void btnChangePrice_Click(object sender, EventArgs e)
        {
            try
            {
                if (OperationProduct == null) { return; }

                AdjustPriceResult result = ChangePriceHelper.ShowFormPricing(OperationProduct);

                if (result.WhetherAdjust)
                {
                    OperationProduct.adjustpriceinfo = result.adjustpriceinfo;

                    UploadOffLineDgvCart();
                }
                else
                {
                    return;
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {
            try
            {
                if (OperationProduct == null) { return; }

                AdjustPriceResult result = ChangePriceHelper.ShowFormDiscount(OperationProduct);

                if (result.WhetherAdjust)
                {
                    OperationProduct.adjustpriceinfo = result.adjustpriceinfo;

                    UploadOffLineDgvCart();
                }
                else
                {
                    return;
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void rbtnPay_ButtonClick(object sender, EventArgs e)
        {
            try
            {

                if (!IsEnable || !rbtnPay.WhetherEnable)
                {
                    return;
                }
                ShowLoading(true, false);

                if (MainModel.WhetherHalfOffLine)
                {
                    if (!RefreshCart())
                    {
                        ShowLoading(false, true);
                        return;
                    }
                }

                if (rbtnPay.ShowText == "完成")
                {
                    PayOK();
                    ShowLoading(false, true);
                }
                else
                {
                    int resultcode = PayHelper.ShowFormPay(CurrentCart);
                    ShowLoading(false, true);
                    Application.DoEvents();
                    if (resultcode == 1)
                    {
                        ClearForm();
                        ClearMember();

                    }
                    else if (resultcode == 0)
                    {

                        if (MainModel.CurrentMember != null)
                        {
                            LoadMember(MainModel.CurrentMember);
                        }
                        else
                        {
                            RefreshCart();
                        }

                    }
                    else
                    {
                        CheckUserAndMember(resultcode, "");
                    }

                    //0726防止会员其他页面过期 本页面会员还在展示
                    if (MainModel.CurrentMember == null && tplMember.ColumnStyles[1].Width > 0)
                    {
                        ClearMember();
                        UploadOffLineDgvCart();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowLog("结算异常" + ex.Message, true);
            }
        }


        private void PayOK()
        {
            try
            {
                ShowLoading(true, false);
                if (!RefreshCart())
                {
                    ShowLoading(false, true);
                    return;
                }

                string ErrorMsg = "";
                int ResultCode = 0;
                CreateOrderResult orderresult = httputil.CreateOrder(CurrentCart, ref ErrorMsg, ref ResultCode);
                if (ResultCode != 0 || orderresult == null)
                {
                    CheckUserAndMember(ResultCode, ErrorMsg);
                    MainModel.ShowLog(ErrorMsg, true);
                    // ShowLog("异常" + ErrorMsg, true);
                }
                else if (orderresult.continuepay == 1)
                {
                    //TODO  继续支付
                    ShowLog("需要继续支付", true);
                }
                else
                {

                    PayHelper.ShowFormPaySuccess(orderresult.orderid);
                    ClearForm();
                    ClearMember();
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("完成支付异常" + ex.Message);

            }
            finally
            {
                ShowLoading(false, true);
            }
        }

        /// <summary>
        /// 修改金额时 可用  使用过后就置位
        /// </summary>
        private bool whetherfix = false;
        private void btnModifyPrice_Click(object sender, EventArgs e)
        {
            try
            {
                ShowLoading(true, false);
                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0 && CurrentCart.totalpayment > 0)
                {

                    decimal fixpricetaotal = ChangePriceHelper.ShowFormOrderPricing(CurrentCart.totalpaymentbeforefix);
                    // decimal fixpricetaotal = ModifyPriceHelper.ShowForm(CurrentCart.totalpaymentbeforefix);
                    if (fixpricetaotal > 0)
                    {
                        CurrentCart.fixpricetotal = fixpricetaotal;


                        AbnormalOrderUtil.WholeAdjustOrder(CurrentCart);


                        whetherfix = true;
                        UploadOffLineDgvCart();
                    }
                }
                ShowLoading(false, true);
            }
            catch (Exception ex)
            {
                ShowLoading(false, true);
                ShowLog("修改订单金额异常：" + ex.Message, true);
            }
        }

        #region
        private ScaleResult CurrentScaleResult = null;



        private void LoadPnlScale()
        {
            try
            {

                string ScaleName = INIManager.GetIni("Scale", "ScaleName", MainModel.IniPath);

                if (ScaleName == ScaleType.中科英泰.ToString() || ScaleName == ScaleType.托利多.ToString())
                {
                    pnlScale.Width = lblTareWeightStr.Left * 2;
                    pnlScale.Left = pnlCategory.Width - pnlScale.Width - 5;
                }
                else if (ScaleName == ScaleType.爱宝.ToString() || ScaleName == ScaleType.易捷通.ToString() || ScaleName == ScaleType.易衡.ToString())
                {
                    pnlScale.Width = lblTareWeightStr.Left;
                    pnlScale.Left = pnlCategory.Width - pnlScale.Width - 5;
                }

                lblStable.Left = pnlScale.Width - lblStable.Width;
            }
            catch { }
        }
        #endregion

        private void btnScan_Click(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 客屏播放视频会占用焦点，此时主界面按键监听（扫描枪）无效，需要刷新焦点
        /// </summary>
        private void CheckActivate()
        {
            while (IsRun)
            {
                try
                {
                    if (MainModel.IsPlayer && this.WindowState != FormWindowState.Minimized && IsEnable)
                    {
                        MainModel.IsPlayer = false;
                        Delay.Start(500);
                        this.Activate();
                        Delay.Start(500);
                        this.Activate();

                        Console.WriteLine("主屏重新获取焦点");
                        Thread.Sleep(100);
                    }
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog("刷新主界面焦点事件异常" + ex.Message);
                }
                Thread.Sleep(100);
            }
        }


        private void btnKboard_Click(object sender, EventArgs e)
        {
            keyBoard.Visible = !keyBoard.Visible;

            CurrentGoodPage = 1;

            if (keyBoard.Visible)
            {

                keyBoard.Size = new System.Drawing.Size(dgvGood.Width, dgvGood.RowTemplate.Height * 3);
                txtSearch.Focus();


            }
            else
            {
                btnScan.Select();
            }

            LoadDgvGood(false, false);
        }


        string keyInput = "";
        private void keyBoardNew1_Press(object sender, KeyBoardNew.KeyboardArgs e)
        {
            TextBox focusing = txtSearch;
            keyInput = e.KeyCode;

            int startDel = 0;

            //退格
            if (keyInput == keyBoard.KeyDelete)
            {
                if (focusing.SelectedText != "")
                    focusing.SelectedText = "";
                else if (focusing.SelectionStart > 0)
                {
                    startDel = focusing.SelectionStart;
                    focusing.Text = focusing.Text.Substring(0, focusing.SelectionStart - 1) +
                        focusing.Text.Substring(focusing.SelectionStart, focusing.Text.Length - focusing.SelectionStart);
                    focusing.SelectionStart = startDel - 1;
                }
            }
            //按确定，焦点转移
            else if (keyInput == keyBoard.KeyEnter)
            {
                //TOOD querendong
            }
            else if (keyInput == keyBoard.KeyClear)
            {
                focusing.Text = "";
            }
            else if (keyInput == keyBoard.KeyHide)
            {
                keyBoard.Visible = false;
                CurrentGoodPage = 1;
                LoadDgvGood(false, false);
                return;
            }

            //其他键直接输入
            else
            {
                if (focusing.SelectedText != "")
                    focusing.SelectedText = keyInput;
                else
                    focusing.SelectedText += keyInput;
            }

            txtSearch.Focus();
        }


        private void ChangeMQTT(object obj)
        {
            try
            {




                bool isstart = (bool)obj;
                if (isstart)
                {
                    string errormsg = "";
                    MainModel.balanceconfigdetail = httputil.BalanceConfigDetail(ref errormsg);

                    //启用标签打印程序
                    if (File.Exists(MainModel.ServerPath + @"MQTTClient.exe"))
                    {
                        System.Diagnostics.Process.Start(MainModel.ServerPath + @"MQTTClient.exe");
                        LogManager.WriteLog("MQTT 启动");
                    }
                    else
                    {
                        LogManager.WriteLog("缺少MQTT程序");
                    }
                }
                else
                {
                    System.Diagnostics.Process[] pro = System.Diagnostics.Process.GetProcesses();
                    for (int i = 0; i < pro.Length - 1; i++)
                    {
                        if (pro[i].ProcessName == "MQTTClient")
                        {
                            pro[i].Kill();
                            LogManager.WriteLog("MQTT 关闭");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("启动/关闭 MQTT程序异常");
            }
        }

        private void frmMainHalfOffLine_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                MainModel.HideTaskThread();
            }
            else
            {
                MainModel.ShowTaskThread();
            }
        }

        private void dgvCart_Click(object sender, EventArgs e)
        {
            try
            {
                btnScan.Select();
            }
            catch { }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            string msg = "";

            PayHelper.ShowFormPayByOther(null);

            //PayHelper.ShowFormVoucher(null,out msg);
        }

        private void btnTopUp_Click(object sender, EventArgs e)
        {
            try
            {

                if (!IsEnable)
                {
                    return;
                }
                if (CurrentCart == null || CurrentCart.products == null)
                {
                    BaseUIHelper.UpdaForm(CurrentCart);
                }

                MemberCenterHelper.ShowFormMemberCenter(MainModel.CurrentMember);

                if (MainModel.CurrentMember != null)
                {
                    string ErrorMsgMember = "";
                    Member member = httputil.GetMember(MainModel.CurrentMember.memberinfo, ref ErrorMsgMember);

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


                if (CurrentCart == null || CurrentCart.products == null)
                {
                    BaseUIHelper.IniFormMainMedia();
                }
            }
            catch { }
        }


        decimal LastNetWeight = 0;
        //爱宝秤 必须放到线程 否则会影响键盘按钮监听事件
        private void ScaleThread(object obj)
        {
            while (IsRun)
            {
                try
                {
                    //不放进委托 自动加购后点取消交易会卡死？？？？？
                    this.Invoke(new InvokeHandler(delegate ()
                    {
                        CurrentScaleResult = ScaleGlobalHelper.GetWeight();


                        if (CurrentScaleResult != null && CurrentScaleResult.WhetherSuccess)
                        {

                            LastNetWeight = CurrentScaleResult.NetWeight;
                            lblNetWeight.Text = CurrentScaleResult.NetWeight + "";
                            picNetWeight.Left = lblNetWeight.Right;
                            lblTareWeight.Text = CurrentScaleResult.TareWeight + "";
                            picTareWeight.Left = lblTareWeight.Right;
                            lblStable.Visible = CurrentScaleResult.WhetherStable;
                            if (MainModel.WhetherAutoCart && CurrentScaleResult.WhetherStable && CurrentScaleResult.NetWeight > 0 && SelectProduct != null && SelectProduct.goodstagid != 0)
                            {


                                if (CurrentCart == null)
                                {
                                    CurrentCart = new Cart();
                                }
                                if (CurrentCart.products == null)
                                {
                                    List<Product> products = new List<Product>();
                                    CurrentCart.products = products;
                                }

                                LastLstPro = new List<Product>();
                                foreach (Product ppro in CurrentCart.products)
                                {
                                    LastLstPro.Add((Product)MainModel.Clone(ppro));
                                }

                                SelectProduct.specnum = CurrentScaleResult.NetWeight;
                                SelectProduct.num = 1;
                                if (SelectProduct.price == null)
                                {
                                    SelectProduct.price = new Price();
                                }
                                SelectProduct.price.specnum = SelectProduct.specnum;
                                InsertProductToCart(SelectProduct);

                                if (MainModel.WhetherPrint)
                                {
                                    LabelPrintHelper.LabelPrint(SelectProduct);
                                }

                                UploadOffLineDgvCart();
                                SelectProduct = null;


                            }
                        }
                        else
                        {
                            LastNetWeight = 0;
                            lblStable.Visible = false;
                        }
                    }));
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog("SCALE", "获取电子秤重量信息异常" + ex.Message);
                }


                Thread.Sleep(120);

            }
        }

        private void dgvSecondCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }
                if (e.RowIndex < 0)
                    return;

                Other.CrearMemory();
                Image selectimg = (Image)dgvSecondCategory.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                //展开
                if (selectimg == imgPageDownForCagegory)
                {
                    CurrentSecondCategoryPage++;
                    LoadSecondDgvCategory();
                    return;
                }
                //收起
                if (selectimg == imgPageUpForCategory)
                {
                    CurrentSecondCategoryPage--;
                    LoadSecondDgvCategory();
                    return;
                }
                if (selectimg.Tag == null)  //空白单元格（无商品）
                {
                    return;
                }

                //遍历单元格清空之前的选中状态
                for (int i = 0; i < this.dgvSecondCategory.Rows.Count; i++)
                {
                    for (int j = 0; j < this.dgvSecondCategory.Columns.Count; j++)
                    {
                        Image lastimg = (Image)dgvSecondCategory.Rows[i].Cells[j].Value;

                        if (lastimg.Tag != null && ((KeyValuePair<string, string>)lastimg.Tag).Key == CurrentFirstCategoryid)
                        {
                            btnNotSelect.Text = ((KeyValuePair<string, string>)lastimg.Tag).Value;
                            Image tempimg = MainModel.GetControlImage(btnNotSelect);
                            tempimg.Tag = (KeyValuePair<string, string>)lastimg.Tag;

                            dgvSecondCategory.Rows[i].Cells[j].Value = tempimg;
                            // break;
                        }
                    }
                }


                KeyValuePair<string, string> kv = (KeyValuePair<string, string>)selectimg.Tag;

                btnSecondSelect.Text = kv.Value;
                Image img = MainModel.GetControlImage(btnSecondSelect);
                img.Tag = kv;

                dgvSecondCategory.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = img;

                sortCartByFirstCategoryid[CurrentFirstCategoryid].SelectSecondCategoryid = kv.Key;

                LoadSecondDgvCategory();
                DgvGoodRowClear();

                CurrentGoodPage = 1;
                //说明是第一次加载
                if (sender == null)
                {
                    LoadDgvGood(true, true);
                }
                else
                {
                    LoadDgvGood(false, false);
                }
            }
            catch (Exception ex)
            {
                ShowLog("选择分类异常" + ex.StackTrace, true);
            }
            finally
            {
                btnScan.Select();
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            txtSearch.Focus();
        }

        private void DgvGoodRowClear()
        {
            this.Invoke(new InvokeHandler(delegate ()
            {
                dgvGood.Rows.Clear();
            }));
        }

        private void DgvGoodRefresh()
        {
            this.Invoke(new InvokeHandler(delegate ()
            {
                dgvGood.Refresh();
            }));
        }
    }
}
