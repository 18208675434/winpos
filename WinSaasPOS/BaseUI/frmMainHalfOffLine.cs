using Maticsoft.BLL;
using Maticsoft.Model;
using WinSaasPOS.Common;
using WinSaasPOS.Model;
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
using WinSaasPOS.Model.Promotion;
using WinSaasPOS.BaseUI;
using WinSaasPOS.ScaleUI;

namespace WinSaasPOS
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

        //第三方支付页面
        frmOnLinePayResult frmonlinepayresult = null;

        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();

        private Bitmap btnorderhangimage;

        private frmLogin CurrentFrmLogin;

        /// <summary>
        /// this.enable=false; 页面不可用页面不可控；  通过该标志控制页面是否可用
        /// </summary>
        private  bool IsEnable=true;

           //扫描数据处理线程
                Thread threadScanCode;

                //刷新焦点线程  防止客屏播放视频抢走焦点
                Thread threadCheckActivate ;
                //启动全量商品同步线程
                Thread threadLoadAllProduct;

                //启动电视屏服务
                Thread threadServerStart;

                //更新离线数据
                Thread threadUploadOffLineDate;

                private bool IsRun = true;

        /// <summary>
        /// 单品促销
        /// </summary>
                private ImplOfflineSingleCalculateNew singlecalculate = new ImplOfflineSingleCalculateNew(MainModel.CurrentShopInfo.tenantid, MainModel.CurrentShopInfo.shopid);

        /// <summary>
        /// 订单级别促销
        /// </summary>
                private ImplOfflineOrderPromotion ordercalculate = new ImplOfflineOrderPromotion(MainModel.CurrentShopInfo.tenantid, MainModel.CurrentShopInfo.shopid);
        /// <summary>
        /// 优惠券
        /// </summary>
                private ImplOfflineCouponsCalculate couponcalculate = new ImplOfflineCouponsCalculate(MainModel.CurrentShopInfo.tenantid, MainModel.CurrentShopInfo.shopid);
        /// <summary>
        /// 积分
        /// </summary     
        private CalculateAvailablePointsCommandImpl pointcalculate = new CalculateAvailablePointsCommandImpl();
        #endregion

        #region  页面加载
                public frmMainHalfOffLine(frmLogin frmlogin)
        {
            InitializeComponent();

            //使用委托的话frmmain界面会卡死
            Control.CheckForIllegalCrossThreadCalls = false;

            MainModel.wScale = (float)Screen.AllScreens[0].Bounds.Width / this.Width;
            MainModel.hScale = (float)Screen.AllScreens[0].Bounds.Height / this.Height;
            MainModel.midScale = (MainModel.wScale + MainModel.hScale) / 2;


            CurrentFrmLogin = frmlogin;
            
            //防止标品加减框变形
            btnIncrease.Size = new System.Drawing.Size(35, 35);
            btnNum.Size = new System.Drawing.Size(90, 35);
            btnNum.Left = pnlNum.Width - btnNum.Width + 3;

           
        }
        private void frmMain_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();

            ParameterizedThreadStart Pts = new ParameterizedThreadStart(ChangeMQTT);
            Thread threadmqtt = new Thread(Pts);
            threadmqtt.IsBackground = true;
            threadmqtt.Start(true);

            Thread threadpayment = new Thread(WinSaasPOS.Model.HalfOffLine.HalfOffLineUtil.GetAvailablePaymentTypes);
            threadpayment.IsBackground = true;
            threadpayment.Start();

            MainModel.IsOffLine = false;
            CurrentFrmLogin.Hide();
            LoadingHelper.CloseForm();
            LoadCart();
           
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                DateTime starttime = DateTime.Now;
                LoadingHelper.ShowLoadingScreen("页面初始化...");

                SetBtnPayStarus(false);

                timerClearMemory.Interval = 5 * 60 * 1000;
                timerClearMemory.Enabled = true;

                lblShopName.Text = MainModel.CurrentShopInfo.shopname;
                lblTime.Text = MainModel.Titledata;
                btnOnLineType.Left = lblShopName.Left + lblShopName.Width + 10;

                //∨ 从右往左排列 被当成图形   从左向右 右侧间距太大
                btnMenu.Text = MainModel.CurrentUser.nickname + "，你好 ∨";
                btnMenu.Left = Math.Max(pnlHead.Width - btnMenu.Width - 10, btnOrderQuery.Left + btnOrderQuery.Width);
                picBirthday.Visible = false;              

                //扫描数据处理线程
                 threadScanCode = new Thread(ScanCodeThread);
                threadScanCode.IsBackground = true;
                threadScanCode.Start();

                //刷新焦点线程  防止客屏播放视频抢走焦点
                 threadCheckActivate = new Thread(CheckActivate);
                threadCheckActivate.IsBackground = true;
                threadCheckActivate.Start();

                //启动全量商品同步线程
                 threadLoadAllProduct = new Thread(ServerDataUtil.LoadAllProduct);
                threadLoadAllProduct.IsBackground = true;
                threadLoadAllProduct.Start();

                //启动促销商品同步线程
                Thread threadLoadPromotion = new Thread(ServerDataUtil.UpdatePromotion);
                threadLoadPromotion.IsBackground = false;
                threadLoadPromotion.Start();

                ClearHistoryData();
                //启动电子秤同步信息线程
                Thread threadLoadScale = new Thread(ScaleDataHelper.LoadScale);
                threadLoadScale.IsBackground = true;
                threadLoadScale.Start();


                //LoadFormIni();
                //弹窗初始化线程
                Thread threadLoadFrmIni = new Thread(LoadFormIni);
                threadLoadFrmIni.IsBackground = true;
                threadLoadFrmIni.Start();

                //更新离线数据
                 threadUploadOffLineDate = new Thread(UploadOffLineData);
                threadUploadOffLineDate.IsBackground = false;
                threadUploadOffLineDate.Start();

                timerGetIncrementProduct.Enabled = true;

                btnorderhangimage = new Bitmap(btnOrderHang.Image, 10, 10);
                UpdateOrderHang();

                //控制按钮图片大小，防止与按钮文字异常
                try
                {
                    try
                    {
                        //-6是因为控件imagealine属性边缘时会有间距
                        int topsize = Convert.ToInt16(((btnPayByCash.Height-6 - 25 - 20 * Math.Min(MainModel.hScale, MainModel.wScale)))/3);
                        // int topsize = Convert.ToInt16(btnPayByCash.Height * (MainModel.hScale - 1) *3/ 10 / MainModel.hScale);

                        btnPayByCash.Padding = new System.Windows.Forms.Padding(0, topsize, 0, topsize);
                        btnPayByBalance.Padding = new System.Windows.Forms.Padding(0, topsize, 0, topsize);
                        btnPayOnLine.Padding = new System.Windows.Forms.Padding(0, topsize, 0, topsize);
                        btnPayByCoupon.Padding = new System.Windows.Forms.Padding(0, topsize, 0, topsize);
                    }
                    catch { }
                    picLoading.Size = new Size(55, 55);
                    pnlPriceLine.Height = 1;              
                }
                catch { }
               
                CheckPrint();

                //客屏初始化
                MainModel.frmmainmedia = new frmMainMedia();
                if (Screen.AllScreens.Count() > 1)
                {
                    asf.AutoScaleControlTest(MainModel.frmmainmedia, 1020, 760, Screen.AllScreens[1].Bounds.Width, Screen.AllScreens[1].Bounds.Height + 20, true);
                    Application.DoEvents();
                    MainModel.frmmainmedia.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width, -20);

                    MainModel.frmmainmedia.Show();
                    MainModel.frmmainmedia.IniForm(null);
                }    
         
                Console.WriteLine("初始化页面时间" + (DateTime.Now - starttime).Milliseconds);
            }
            catch (Exception ex)
            {
                ShowLog("初始化页面异常" + ex.Message + ex.StackTrace, true);
            }
            this.Activate();
        }



        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {

            IsRun = false;

            ChangeMQTT(false);
            SaveCart();
            try { threadScanCode.Abort(); }
            catch { }

            try { threadCheckActivate.Abort(); }
            catch { }

            try { threadServerStart.Abort(); }
            catch { }

            try { threadServerStart.Abort(); }
            catch { }


            try { MainModel.frmnumber.Dispose(); }
            catch { } MainModel.frmnumber = null;
            try { MainModel.frmcashpay.Dispose(); }
            catch { } MainModel.frmcashpay = null;
            try { MainModel.frmcashpayoffline.Dispose(); }
            catch { } MainModel.frmcashpayoffline = null;
            try { MainModel.frmcashcoupon.Dispose(); }
            catch { } MainModel.frmcashcoupon = null;
            try { MainModel.frmtoolmain.Dispose(); }
            catch { } MainModel.frmtoolmain = null;
            try { MainModel.frmmodifyprice.Dispose(); }
            catch { } MainModel.frmmodifyprice = null;
            try { MainModel.frmprintersetting.Dispose(); }
            catch { } MainModel.frmprintersetting = null;
            try { MainModel.frmloading.Dispose(); }
            catch { } MainModel.frmloading = null;
            
            timerGetIncrementProduct.Enabled = false;

            MainModel.frmmainmedia.Close();
            MainModel.frmmainmedia = null;
            this.Dispose();
        }

        JSON_BEANBLL jsonbll = new JSON_BEANBLL();
        private void SaveCart()
        {
            try
            {
                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                {
                   
                    jsonbll.Delete(ConditionType.CurrentCart);

                    foreach (Product pro in CurrentCart.products)
                    {
                        pro.weightflag = Convert.ToBoolean(pro.goodstagid);
                    }

                    JSON_BEANMODEL jsonmodel = new JSON_BEANMODEL();
                    jsonmodel.CONDITION = ConditionType.CurrentCart;
                    jsonmodel.CREATE_TIME = DateTime.Now.ToString("yyyyMMddHHmmss");
                    jsonmodel.DEVICESN = MainModel.DeviceSN;
                    jsonmodel.CREATE_URL_IP = MainModel.URL;
                    jsonmodel.JSON = JsonConvert.SerializeObject(CurrentCart);
                    jsonbll.Add(jsonmodel);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("保存购物车信息异常"+ex.Message);
            }
        }

        private void LoadCart()
        {
            try
            {
              
                    JSON_BEANMODEL jsonbmodel = jsonbll.GetModel(ConditionType.CurrentCart);
                    jsonbll.Delete(ConditionType.CurrentCart);

                  //测试的时候会出现
                    if (jsonbmodel != null && jsonbmodel.CREATE_URL_IP == MainModel.URL)
                    {

                        Cart lastCart = JsonConvert.DeserializeObject<Cart>(jsonbmodel.JSON);

                        CurrentCart = lastCart;

                        UploadOffLineDgvGoods();
                       // RefreshCart(new List<Product>());
                    }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("加载购物车信息异常" + ex.Message);
            }
        }

        
        bool isshowpic = false;
        //实时时间显示


        //定时清理内存
        private void timerClearMemory_Tick(object sender, EventArgs e)
        {
            try {

                LogManager.WriteLog("开始清理内存");
                Other.CrearMemory();
                LogManager.WriteLog("内存清理完成");
            }
            catch(Exception ex) {
                LogManager.WriteLog("清理内存异常"+ex.Message);
            }
        }


        private void CheckUserAndMember(int resultcode, string ErrorMsg)
        {
            try
            {
                this.Invoke(new InvokeHandler(delegate()
                {
                    if (resultcode == MainModel.HttpUserExpired)
                    {
                        ShowLoading(false);// LoadingHelper.CloseForm();
                        LoadPicScreen(true);
                        MainModel.CurrentMember = null;
                        frmUserExpired frmuserexpired = new frmUserExpired();
                        frmuserexpired.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmuserexpired.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmuserexpired.Height) / 2);
                        frmuserexpired.TopMost = true;
                        frmuserexpired.ShowDialog();

                        INIManager.SetIni("System", "POS-Authorization", "", MainModel.IniPath);

                        CurrentFrmLogin.Show();
                        this.Close();
                        LoadPicScreen(false);
                    }
                    else if (resultcode == MainModel.HttpMemberExpired || resultcode == MainModel.DifferentMember)
                    {
                        ShowLoading(false);// LoadingHelper.CloseForm();
                        MainModel.CurrentMember = null;
                        ClearMember();
                       
                        FrmConfirmBack frmconfirmback = new FrmConfirmBack("会员登录已过期，请重新登录", "", "");

                        frmconfirmback.Location = new Point(0, 0);
                        
                        if (frmconfirmback.ShowDialog() != DialogResult.OK)
                        {
                            IsEnable = true;
                            return;
                        }
                        IsEnable = true;
                        //ClearForm();
                        LoadPicScreen(false);

                        btnLoadPhone_Click(null, null);

                    }
                    //else if (resultcode == MainModel.DifferentMember)   //不是同一个会员 只提示不退出
                    //{
                    //    ShowLog("非当前登录用户的付款码，请确认后重新支付", true);
                    //}
                    else
                    {
                        ShowLog(ErrorMsg, false);
                    }
                }));

            }
            catch (Exception ex)
            {
                
                ShowLog("验证用户/会员异常", true);
            }
            finally
            {
                IsEnable = true;
                
                LoadPicScreen(false );
            }

        }
        #endregion

        #region 菜单按钮



        private void tsmExit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }
              

                FrmConfirmBack frmconfirmback = new FrmConfirmBack("是否确认退出系统？", "", "");

                frmconfirmback.Location = new Point(0, 0);
                
                if (frmconfirmback.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                INIManager.SetIni("System", "POS-Authorization", "", MainModel.IniPath);
                CurrentFrmLogin.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("退出系统异常"+ex.Message);
            }
            finally
            {
                LoadPicScreen(false);
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
                LogManager.WriteLog("开启订单查询页面异常"+ex.Message);
            }
            finally
            {
                IsEnable = true;
            }
        }

        private void btnOrderCancle_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }


                if (dgvGood.Rows.Count <= 0)
                {
                    return;
                }


                FrmConfirmBack frmconfirmback = new FrmConfirmBack("是否确认取消订单？", "", "");

                frmconfirmback.Location = new Point(0, 0);
                
                if (frmconfirmback.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                //可能存在网络中断情况桌面还要清空
                try
                {
                    ReceiptUtil.EditCancelOrder(1, CurrentCart.totalpayment+CurrentCart.totalpromoamt);
                }
                catch (Exception ex) { }

                if (MainModel.CurrentMember != null)
                {
                    ClearMember();
                }

                ClearForm();

              //  Other.CrearMemory();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("取消交易异常" + ex.Message);
            }
            finally
            {
                LoadPicScreen(false);
            }
        }

        //挂单
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

                        FrmConfirmBack frmconfirmback = new FrmConfirmBack("确认挂单？", "", "");

                        frmconfirmback.Location = new Point(0, 0);
                        
                        if (frmconfirmback.ShowDialog() != DialogResult.OK)
                        {
                            return;
                        }

                        SerializeOrder(CurrentCart);

                        if (MainModel.CurrentMember != null)
                        {
                            ClearMember();
                        }

                        ClearForm();
                        ShowLog("挂单完成", false);

                    }
                }
                else if (btnOrderHang.Text == "挂单列表")
                {
                    IsEnable = false;
                    frmOrderHang frmorderhang = new frmOrderHang();
                    //frmorderhang.DataReceiveHandle += FormOrderHang_DataReceiveHandle;
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
                        }

                    if (!string.IsNullOrEmpty(frmorderhang.CurrentPhone))
                    {
                        string ErrorMsgMember = "";
                        Member member = httputil.GetMember(frmorderhang.CurrentPhone, ref ErrorMsgMember);

                        if (ErrorMsgMember != "" || member == null) //会员不存在
                        {

                            ClearMember();
                            ShowLog(ErrorMsgMember, false);
                            UploadOffLineDgvGoods();
                        }
                        else
                        {
                            LoadMember(member);
                        }
                    }
                    else
                    {
                        UploadOffLineDgvGoods();
                    }
                   
                   

                    }

                    UpdateOrderHang();

                }
            }
            catch (Exception ex)
            {
                ShowLog("挂单异常", true);
            }
            finally
            {
                IsEnable = true;
                LoadPicScreen(false);
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


        //更新挂单按钮  购物车没有商品且有挂单信息时 按钮text="挂单列表"   按钮点击事件根据文本判断事件  更新取消交易
        private void UpdateOrderHang()
        {
            try
            {
                btnOrderHang.Text = "挂单列表";
               // btnOrderCancle.Visible = false;
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
            try
            {

            if (!IsEnable)
            {
                return;
            }
            
            if (MainModel.frmtoolmain == null)
            {
                MainModel.frmtoolmain = new frmToolMain();

                asf.AutoScaleControlTest(MainModel.frmtoolmain, 178, 370, Convert.ToInt32(MainModel.wScale * 178), Convert.ToInt32(MainModel.hScale * 370), true);
                MainModel.frmtoolmain.DataReceiveHandle += frmToolMain_DataReceiveHandle;
                MainModel.frmtoolmain.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width - MainModel.frmtoolmain.Width - 15, pnlHead.Height + 10);
                MainModel.frmtoolmain.Show();
            }
            else
            {
                MainModel.frmtoolmain.Show();
            }
           
            }
            catch (Exception ex)
            {
                MainModel.frmtoolmain = null;
                ShowLog("菜单窗体显示异常"+ex.Message,true);
            }
        }

        private void frmToolMain_DataReceiveHandle(ToolType tooltype)
        {
            try
            {
                if (tooltype == ToolType.Receipt)
                {
                    this.Invoke(new InvokeHandler(delegate()
                    {
                        tsmReceipt_Click(null,null);
                    }));
                }
                if (tooltype == ToolType.Exit)
                {
                    this.Invoke(new InvokeHandler(delegate()
                    {
                        tsmExit_Click(null, null);
                    }));
                }
                if (tooltype == ToolType.Expense)
                {
                    this.Invoke(new InvokeHandler(delegate()
                    {
                        tsmExpense_Click(null, null);
                    }));
                }
                if (tooltype == ToolType.PrintSet)
                {
                    this.Invoke(new InvokeHandler(delegate()
                    {
                        tsmPrintSet_Click(null, null);
                    }));
                }
                if (tooltype == ToolType.ReceiptQuery)
                {
                    this.Invoke(new InvokeHandler(delegate()
                    {
                        tsmReceiptQuery_Click(null, null);
                    }));
                }

                if (tooltype == ToolType.Scale)
                {
                    this.Invoke(new InvokeHandler(delegate()
                    {
                        tsmScale_Click(null, null);
                    }));
                }

                if (tooltype == ToolType.ChangeMode)
                {
                    this.Invoke(new InvokeHandler(delegate()
                    {
                        tsmChangeMode_Click(null, null);
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
            try
            {

                if (!IsEnable)
                {
                    return;
                }

                IsEnable = false;
                FrmConfirmBack frmconfirmback = new FrmConfirmBack("确认交班", "点击确认后，收银机将自动打印交班表单", "");

                frmconfirmback.Location = new Point(0, 0);
                

                if (frmconfirmback.ShowDialog() != DialogResult.OK)
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
                
                IsEnable = false;
               ShowLoading(true);
                string ErrorMsg = "";
                Receiptdetail receipt = httputil.Receipt(receiptpara, ref ErrorMsg);

                IsEnable = true;
                ShowLoading(false);// LoadingHelper.CloseForm();
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


                    INIManager.SetIni("System", "POS-Authorization", "", MainModel.IniPath);
                    MainModel.Authorization = "";

                    FrmConfirmReceiptBack frmconfirmreceiptback = new FrmConfirmReceiptBack(receipt);
                    frmconfirmreceiptback.Location = new Point(0, 0);
                    frmconfirmreceiptback.ShowDialog();

                    CurrentFrmLogin.Show();
                    this.Close();

                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("交班出现异常"+ex.Message);
                
            }
            finally
            {
                LoadPicScreen(false);
                IsEnable = true;
                ShowLoading(false);// LoadingHelper.CloseForm();
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
                LogManager.WriteLog("交班查询异常"+ex.Message);
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
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("电子秤管理异常"+ex.Message);
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
            try { 
            if (!IsEnable)
            {
                return;
            }
            frmChangeMode frmchangemode = new frmChangeMode();

            asf.AutoScaleControlTest(frmchangemode, 1178, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);

            frmchangemode.Location = new System.Drawing.Point(0, 0);

            frmchangemode.ShowDialog();
            frmchangemode.Dispose();

            if (frmchangemode.DialogResult == DialogResult.OK)
            {

                INIManager.SetIni("System", "POS-Authorization", "", MainModel.IniPath);
                MainModel.Authorization = "";
               // this.Hide();
             if (MainModel.frmloginoffline != null)
            {
                try { MainModel.frmloginoffline.Dispose(); }
                catch { }                
            }
            MainModel.frmloginoffline = new frmLoginOffLine();
            MainModel.frmloginoffline.Show();

            this.Dispose();
                
            }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("修改模式异常" + ex.Message);
            }
        }

        #endregion

        #region 购物车
        private void btnLoadBarCode_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }

                frmNumberBack frmnumberback = new frmNumberBack("请输入商品条码", NumberType.BarCode,ShowLocation.Right);
                
                frmnumberback.Location = new Point(0, 0);
                frmnumberback.ShowDialog();

                if (frmnumberback.DialogResult == DialogResult.OK)
                {
                    //数字过长会变成 +E 形式
                    //string goodscode = frmnumberback.NumValue.ToString();
                    string goodscode = frmnumberback.strNumValue;

                    QueueScanCode.Enqueue(goodscode);
                   
                }
                else
                {
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("输入商品条码异常" + ex.Message);
            }
            finally
            {
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
                    // pnlPayType1.Enabled = true;
                    SetBtnPayStarus(true);
                    pnlPayType2.Enabled = true;

                    if (MainModel.CurrentMember != null && MainModel.CurrentMember.isUsePoint)
                    {
                        CurrentCart.pointpayoption = 1;
                    }
                    else
                    {
                        CurrentCart.pointpayoption = 0 ;
                    }
                   ShowLoading(true);
                    IsEnable = false;

                    Cart cart = httputil.RefreshCart(CurrentCart, ref ErrorMsgCart, ref ResultCode);

                    Console.WriteLine("在线刷新购物车时间" + (DateTime.Now - starttime).Milliseconds);

                    if (ErrorMsgCart != "" || cart == null) //商品不存在或异常
                    {
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
                            cart.selectedcoupons = null;
                            cart = httputil.RefreshCart(CurrentCart, ref ErrorMsgCart, ref ResultCode);
                            if (ErrorMsgCart != "" || cart == null) //商品不存在或异常
                            {
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

                    UploadDgvGoods(cart);
                    Console.WriteLine("表格加载时间" + (DateTime.Now - starttime).TotalMilliseconds);

                    if (MainModel.CurrentMember!=null && MainModel.CurrentMember.isUsePoint)
                    {
                        if (MainModel.CurrentMember != null && CurrentCart != null && CurrentCart.pointinfo != null)
                        {
                            lblJF.Text = CurrentCart.pointinfo.totalpoints.ToString();

                            btnJFUse.Text = "使用" + CurrentCart.pointinfo.availablepoints + "积分 抵用" + CurrentCart.pointinfo.availablepointsamount + "元";
                            btnJFUse.Visible = true;
                        }
                    }
                    else
                    {
                        btnJFUse.Text = "";
                        btnJFUse.Visible = false;
                    }

                    UpdatePayment();

                    return true;

                }
                else
                {
                    SetBtnPayStarus(false);
                   // pnlPayType2.Enabled = false;

                    ClearForm();
                    return true;
                }

            }
            catch (Exception ex)
            {

                ShowLog("刷新购物车异常：" + ex.Message, true);
                return false;
            }
            finally
            {
                IsEnable = true;
                
                ShowLoading(false);// LoadingHelper.CloseForm();
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
                        IsEnable = false;
                        ShowLoading(true);

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
                        ShowLoading(false);// LoadingHelper.CloseForm();
                        DateTime starttime = DateTime.Now;
                        if (LstScancodemember.Count > 0)
                        {
                            isGoodRefresh = true;
                            FlashSkuCode = LstScancodemember[0].ScanModel.scancodedto.skucode;
                        
                        if (this.IsHandleCreated)
                        {
                            this.Invoke(new InvokeHandler(delegate()
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
                                ShowLog("条码识别错误",false);
                                LogManager.WriteLog("SCAN",logCode);
                            }
                        }

                        Console.WriteLine("离线扫描计算时间" + (DateTime.Now - starttime).TotalMilliseconds);
                        Thread.Sleep(1);
                    }
                    catch (Exception ex)
                    {
                        ShowLoading(false);// LoadingHelper.CloseForm();//关闭
                        LogManager.WriteLog("ERROR", "扫描数据处理异常：" + ex.Message);
                    }
                    finally
                    {
                        IsEnable = true;
                        ShowLoading(false);// LoadingHelper.CloseForm();
                    }
                }

                Thread.Sleep(100);
            }
        }

        private void addcart( List<ScanModelAndDbpro> lstscanmodelandpro)
        {

                try
                {

                    DateTime starttime = DateTime.Now;
                    foreach (ScanModelAndDbpro scancodemember in lstscanmodelandpro)
                    {

                        if (scancodemember.ScanModel.scancodedto.weightflag && scancodemember.ScanModel.scancodedto.specnum == 0)
                    {
                        frmNumberBack frmnumberback = new frmNumberBack(scancodemember.ScanModel.scancodedto.skuname, NumberType.ProWeight, ShowLocation.Center);
                        
                        frmnumberback.Location = new Point(0, 0);
                        frmnumberback.ShowDialog();

                        if (frmnumberback.DialogResult == DialogResult.OK)
                        {
                            scancodemember.ScanModel.scancodedto.specnum = (decimal)frmnumberback.NumValue / 1000;
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

                    if (CurrentCart == null)
                    {
                        CurrentCart = new Cart();
                    }
                    if (CurrentCart.products == null)
                    {
                        CurrentCart.products = new List<Product>();
                    }
                    if (CurrentCart.products != null && !pro.weightflag)
                    {
                        bool newpro = true;
                        foreach (Product exitspro in CurrentCart.products)
                        {
                            if (pro.skucode == exitspro.skucode)
                            { 
                                exitspro.num++;
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
                    }
                    Console.WriteLine("addcart  计算时间" + (DateTime.Now - starttime).TotalMilliseconds);
                    UploadOffLineDgvGoods();
                    Console.WriteLine("addcart  更新页面" + (DateTime.Now - starttime).TotalMilliseconds);
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog("ERROR", "添加购物车商品异常:" + ex.Message);
                }
                finally
                {
                    LoadPicScreen(false);                    
                    ShowLoading(false);// LoadingHelper.CloseForm();

                }

        }



        //TODO  修改样式
        private void ShowLog(string msg, bool iserror)
        {
            ParameterizedThreadStart Pts = new ParameterizedThreadStart(showlogthread);
            Thread threadmqtt = new Thread(Pts);
            threadmqtt.IsBackground = true;
            threadmqtt.Start(msg);
        }

        private void showlogthread(object obj)
        {
            try
            {
                lblToast.Text = obj.ToString();
                if (this.IsHandleCreated)
                {
                    this.Invoke(new InvokeHandler(delegate()
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
                    this.Invoke(new InvokeHandler(delegate()
                    {
                        lblToast.Visible = false ;
                    }));
                }
                else
                {
                    lblToast.Visible = false ;

                }                 
            }
            catch (Exception ex)
            {

            }
        }



        private object thislockDgvGood = new object();

        private void UploadDgvGoods(Cart cart)
        {
            lock (thislockDgvGood)
            {
                try
                {

                   UploadOrderDetail();
                    DateTime starttime = DateTime.Now;

                    if (cart.totalpayment > 0)
                    {
                        btnModifyPrice.BackgroundImage = Resources.ResourcePos.FixEnable;
                        btnModifyPrice.ForeColor = ColorTranslator.FromHtml("#FF0D62B8");
                    }
                    else
                    {

                        btnModifyPrice.BackgroundImage = Resources.ResourcePos.FixDisenable;
                        btnModifyPrice.ForeColor = Color.White;
                    }


                    int oldrowindex = dgvGood.FirstDisplayedScrollingRowIndex;
                    dgvGood.Rows.Clear();
                    if (cart != null && cart.products != null && cart.products.Count > 0)
                    {
                       
                        int count = cart.products.Count;
                        int goodscount = 0;
                        foreach (Product pro in cart.products)
                        {
                            goodscount += pro.num;
                        }
                        CurrentCart.goodscount = goodscount;
                        lblGoodsCount.Text = "(" + goodscount.ToString() + "件商品)";
                        if (count == 0)
                        {
                            pnlWaiting.Show();
                        }
                        else
                        {
                            pnlWaiting.Visible = false;
                            for (int i = 0; i < count; i++)
                            {

                                Product temppro = cart.products[i].ThisClone();

                                //标品之前有的话不会改变位置，所以要记录标品行数 实现动画效果
                                if ( temppro.goodstagid==0 && temppro.skucode == FlashSkuCode)
                                {
                                    FlashIndex = count-i-1; 
                                }

                                List<Bitmap> lstbmp = GetDgvRow(temppro);
                                if (lstbmp != null && lstbmp.Count == 6)
                                {
                                    dgvGood.Rows.Insert(0, new object[] { (1 + i).ToString(), lstbmp[0], lstbmp[1], lstbmp[2], lstbmp[3], lstbmp[4], lstbmp[5] });
                                }
                            }
                            try { dgvGood.FirstDisplayedScrollingRowIndex = oldrowindex; }
                            catch { }

                            Application.DoEvents();

                            ////ShowDgv();
                            Thread threadItemExedate = new Thread(ShowDgv);
                            threadItemExedate.IsBackground = true;
                            threadItemExedate.SetApartmentState(ApartmentState.STA);
                            threadItemExedate.Start();

                            dgvGood.ClearSelection();
                            
                            if ( cart.totalpayment == 0 && cart.products != null && cart.products.Count > 0)
                            {
                                pnlPayType1.Visible = false;
                                pnlPayType2.Visible = true;
                            }
                            else
                            {
                                pnlPayType1.Visible = true;
                                pnlPayType2.Visible = false;
                            }
                        }
                    }
                    else
                    {
                        SetBtnPayStarus(false);
                       // pnlPayType2.Enabled = false;
                    }
                    //Console.WriteLine("积分前" + (DateTime.Now - starttime).TotalMilliseconds);
                    CurrentCart = cart;
                    if (MainModel.CurrentMember != null && MainModel.CurrentMember.isUsePoint && CurrentCart.pointinfo != null)
                    {
                        btnJFUse.Text = "使用" + CurrentCart.pointinfo.availablepoints + "积分 抵用" + CurrentCart.pointinfo.availablepointsamount + "元";
                        btnJFUse.Visible = true;
                    }

                  
                    if (CurrentCart.availablecoupons != null && CurrentCart.availablecoupons.Count > 0 )
                    {

                        lblCoupon.Visible = true;

                        lblCouponStr.Visible = true;

                        if (CurrentCart.couponpromoamt > 0)
                        {
                            lblCoupon.Text = "-￥" + CurrentCart.couponpromoamt + ">";
                        }
                        else
                        {
                            lblCoupon.Text = CurrentCart.availablecoupons.Count + "张可用>";
                        }
                    }
                    else
                    {

                        lblCoupon.Visible = false;
                        lblCouponStr.Visible = false;
                    }

                    Thread threadMember = new Thread(UploadMember);
                    threadMember.IsBackground = true;
                    threadMember.Start();


                    //Console.WriteLine("优惠券后" + (DateTime.Now - starttime).TotalMilliseconds);
                    UpdateOrderHang();
                    // }));


                    MainModel.frmMainmediaCart = CurrentCart;
                    MainModel.frmmainmedia.UpdateForm();
                    this.Activate();
                    // Application.DoEvents();

                    //刷新一次 否则滚动条不显示  否则dgvinsert  要用委托
                   // dgvGood.Refresh();
                }
                catch (Exception ex)
                {
                    dgvGood.Refresh();
                    ShowLoading(false);// LoadingHelper.CloseForm();
                    LogManager.WriteLog("更新显示列表异常" + ex.Message + ex.StackTrace);
                    //ShowLog("更新显示列表异常" + ex.Message+ex.StackTrace, false);
                }
            }
        }

        private object thislockOffLineDgvGood = new object();

        private void UploadOffLineDgvGoods()
        {
            lock (thislockOffLineDgvGood)
            {
                try
                {
                    MainHelper.CartPromotion(CurrentCart,whetherfix);

                    whetherfix = false;

                    UploadOffLineOrderDetail();


                    if (CurrentCart.totalpayment > 0)
                    {
                        btnModifyPrice.BackgroundImage = Resources.ResourcePos.FixEnable;
                        btnModifyPrice.ForeColor = ColorTranslator.FromHtml("#FF0D62B8");
                    }
                    else
                    {

                        btnModifyPrice.BackgroundImage = Resources.ResourcePos.FixDisenable;
                        btnModifyPrice.ForeColor = Color.White;
                    }
                    DateTime starttime = DateTime.Now;

                    int oldrowindex = dgvGood.FirstDisplayedScrollingRowIndex;
                    dgvGood.Rows.Clear();
                    if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                    {

                        int count = CurrentCart.products.Count;
                        int goodscount = 0;
                        foreach (Product pro in CurrentCart.products)
                        {
                            goodscount += pro.num;
                        }
                        CurrentCart.goodscount = goodscount;
                        lblGoodsCount.Text = "(" + goodscount.ToString() + "件商品)";
                        if (count == 0)
                        {
                            pnlWaiting.Show();
                        }
                        else
                        {
                            pnlWaiting.Visible = false;
                            for (int i = 0; i < count; i++)
                            {

                                Product temppro = CurrentCart.products[i].ThisClone();

                                //标品之前有的话不会改变位置，所以要记录标品行数 实现动画效果
                                if (temppro.goodstagid == 0 && temppro.skucode == FlashSkuCode)
                                {
                                    FlashIndex = count - i - 1;
                                }

                                List<Bitmap> lstbmp = GetDgvRow(temppro);
                                if (lstbmp != null && lstbmp.Count == 6)
                                {
                                    dgvGood.Rows.Insert(0, new object[] { (1 + i).ToString(), lstbmp[0], lstbmp[1], lstbmp[2], lstbmp[3], lstbmp[4], lstbmp[5] });
                                }
                            }
                            try { dgvGood.FirstDisplayedScrollingRowIndex = oldrowindex; }
                            catch { }

                            //Application.DoEvents();

                            ////ShowDgv();
                            Thread threadItemExedate = new Thread(ShowDgv);
                            threadItemExedate.IsBackground = true;
                            threadItemExedate.SetApartmentState(ApartmentState.STA);
                            threadItemExedate.Start();

                            dgvGood.ClearSelection();

                            if (CurrentCart.payamt == 0 && CurrentCart.products != null && CurrentCart.products.Count > 0)
                            {
                                pnlPayType1.Visible = false;
                                pnlPayType2.Visible = true;
                            }
                            else
                            {
                                pnlPayType1.Visible = true;
                                pnlPayType2.Visible = false;
                            }
                        }

                        if (CurrentCart.totalpayment > 0)
                        {
                            SetBtnPayStarus(true);
                        }

                        UpdatePayment();
                    }
                    else
                    {
                        pnlWaiting.Show();
                        ClearForm();
                        SetBtnPayStarus(false);
                    }
                    if (MainModel.CurrentMember != null && MainModel.CurrentMember.isUsePoint && CurrentCart.pointinfo != null)
                    {
                        btnJFUse.Text = "使用" + CurrentCart.pointinfo.availablepoints + "积分 抵用" + CurrentCart.pointinfo.availablepointsamount + "元";
                        btnJFUse.Visible = true;
                    }

                    if (CurrentCart.availablecoupons != null && CurrentCart.availablecoupons.Count > 0)
                    {

                        lblCoupon.Visible = true;

                        lblCouponStr.Visible = true;

                        if (CurrentCart.couponpromoamt > 0)
                        {
                            lblCoupon.Text = "-￥" + CurrentCart.couponpromoamt + ">";
                        }
                        else
                        {
                            lblCoupon.Text = CurrentCart.availablecoupons.Count + "张可用>";
                        }
                    }
                    else
                    {

                        lblCoupon.Visible = false;
                        lblCouponStr.Visible = false;
                    }

                    UploadMember();

                    UpdateOrderHang();


                    MainModel.frmMainmediaCart = CurrentCart;
                    MainModel.frmmainmedia.UpdateForm();
                    this.Activate();

                }
                catch (Exception ex)
                {
                    dgvGood.Refresh();
                    ShowLoading(false);
                    LogManager.WriteLog("更新显示列表异常" + ex.Message + ex.StackTrace);
                }
            }
        }

        private object thislockOrderDetail = new object();
        private void UploadOrderDetail()
        {
            lock (thislockOrderDetail)
            {
                try
                {
                    dgvOrderDetail.Rows.Clear();

                    if (CurrentCart != null && CurrentCart.orderpricedetails != null)
                    {
                        foreach (OrderPriceDetail orderprice in CurrentCart.orderpricedetails)
                        {
                            try
                            {
                                // dgvOrderDetail.Rows.Add(orderprice.title, orderprice.amount);
                                if (this.IsHandleCreated)
                                {
                                    this.Invoke(new InvokeHandler(delegate()
                                    {

                                        dgvOrderDetail.Rows.Add(orderprice.title, orderprice.amount);
                                    }));
                                }
                            }
                            catch
                            {
                                dgvOrderDetail.Refresh();
                            }
                        }

                        dgvOrderDetail.ClearSelection();
                    }


                    if (MainModel.CurrentMember != null)
                    {
                        string strmemberpromo = "";
                        if (CurrentCart.memberpromo != null && CurrentCart.memberpromo > 0)
                        {
                            strmemberpromo = "会员已优惠：￥" + CurrentCart.memberpromo.ToString("f2") + "，";
                        }
                        if (CurrentCart.balancepaypromoamt != null && CurrentCart.balancepaypromoamt > 0)
                        {
                            strmemberpromo += "余额支付再减：￥" + CurrentCart.balancepaypromoamt.ToString("f2");
                        }

                        if (string.IsNullOrEmpty(strmemberpromo))
                        {
                            btnMemberPromo.Visible = false;
                        }
                        else
                        {
                            btnMemberPromo.Text = strmemberpromo.TrimEnd('，');
                            btnMemberPromo.Visible = true;
                        }
                       

                    }
                    else
                    {
                        if (CurrentCart.memberpromo != null && CurrentCart.memberpromo > 0)
                        {
                            btnMemberPromo.Text = "会员可优惠:￥" + CurrentCart.memberpromo.ToString("f2");
                            btnMemberPromo.Visible = true;
                        }
                        else
                        {
                            btnMemberPromo.Visible = false;
                        }
                    }

                    lblPrice.Text = "￥" + CurrentCart.totalpayment.ToString("f2");

                    dgvOrderDetail.Height = dgvOrderDetail.RowTemplate.Height * dgvOrderDetail.Rows.Count;

                    pnlPrice.Top = dgvOrderDetail.Location.Y + dgvOrderDetail.Height;

                    if (btnMemberPromo.Visible)
                    {

                        //btnMemberPromo.Top = lblPriceStr.Top + lblPriceStr.Height;
                        pnlPrice.Height = btnMemberPromo.Location.Y + btnMemberPromo.Height + 10;
                    }
                    else
                    {
                        pnlPrice.Height = lblPriceStr.Location.Y + lblPriceStr.Height + 10;
                    }

                    pnlOrdreDetail.Height = pnlPrice.Location.Y + pnlPrice.Height + 5;
                    Application.DoEvents();
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog("更新订单价格详情异常" + ex.Message + ex.StackTrace);
                }
            }
        }

        private object thislockOffLineOrderDetail = new object();
        private void UploadOffLineOrderDetail()
        {
            lock (thislockOffLineOrderDetail)
            {
                try
                {

                    lblPrice.Text = "￥" + CurrentCart.totalpayment.ToString("f2");


                    dgvOrderDetail.Rows.Clear();

                    if (CurrentCart != null && CurrentCart.orderpricedetails != null)
                    {
                        foreach (OrderPriceDetail orderprice in CurrentCart.orderpricedetails)
                        {
                            try
                            {
                                // dgvOrderDetail.Rows.Add(orderprice.title, orderprice.amount);
                                if (this.IsHandleCreated)
                                {
                                    this.Invoke(new InvokeHandler(delegate()
                                    {

                                        dgvOrderDetail.Rows.Add(orderprice.title, orderprice.amount);
                                    }));
                                }
                            }
                            catch
                            {
                                dgvOrderDetail.Refresh();
                            }
                        }

                        dgvOrderDetail.ClearSelection();
                    }

                    dgvOrderDetail.ClearSelection();


                    

                    dgvOrderDetail.Height = dgvOrderDetail.RowTemplate.Height * dgvOrderDetail.Rows.Count;

                    pnlPrice.Top = dgvOrderDetail.Location.Y + dgvOrderDetail.Height;

                    if (MainModel.CurrentMember != null)
                    {
                        string strmemberpromo = "";
                        if (CurrentCart.memberpromo != null && CurrentCart.memberpromo > 0)
                        {
                            strmemberpromo = "会员已优惠：￥" + CurrentCart.memberpromo.ToString("f2") + "，";
                        }
                        if (CurrentCart.balancepaypromoamt != null && CurrentCart.balancepaypromoamt > 0)
                        {
                            strmemberpromo += "余额支付再减：￥" + CurrentCart.balancepaypromoamt.ToString("f2");
                        }

                        if (string.IsNullOrEmpty(strmemberpromo))
                        {
                            btnMemberPromo.Visible = false;
                        }else{
                            btnMemberPromo.Text=strmemberpromo.TrimEnd('，');
                            btnMemberPromo.Visible=true;
                        }
                       
                    }
                    else
                    {
                        if (CurrentCart.memberpromo != null && CurrentCart.memberpromo > 0)
                        {
                            btnMemberPromo.Text = "会员可优惠：￥" + CurrentCart.memberpromo.ToString("f2");
                            btnMemberPromo.Visible = true;
                        }
                        else
                        {
                            btnMemberPromo.Visible = false;
                        }
                    }

                    if (btnMemberPromo.Visible)
                    {

                        //btnMemberPromo.Top = lblPriceStr.Top + lblPriceStr.Height;
                        pnlPrice.Height = btnMemberPromo.Location.Y + btnMemberPromo.Height + 10;
                    }
                    else
                    {
                        pnlPrice.Height = lblPriceStr.Location.Y + lblPriceStr.Height + 10;
                    }

                    pnlOrdreDetail.Height = pnlPrice.Location.Y + pnlPrice.Height + 5;

                    if (MainModel.CurrentMember != null && MainModel.CurrentMember.isUsePoint)
                    {
                        if (MainModel.CurrentMember != null && CurrentCart != null && CurrentCart.pointinfo != null)
                        {
                            lblJF.Text = CurrentCart.pointinfo.totalpoints.ToString();

                            btnJFUse.Text = "使用" + CurrentCart.pointinfo.availablepoints + "积分 抵用" + CurrentCart.pointinfo.availablepointsamount + "元";
                            btnJFUse.Visible = true;
                        }
                    }
                    else
                    {
                        btnJFUse.Text = "";
                        btnJFUse.Visible = false;
                    }


                   
                    Application.DoEvents();
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog("更新订单价格详情异常" + ex.Message + ex.StackTrace);
                }
            }
        }


        private object thislockMember = new object();
        private void UploadMember()
        {
            lock (thislockMember)
            {
                try
                {
                    

                    if (pnlMember.Visible)
                    {

                        if (MainModel.CurrentMember != null && MainModel.CurrentMember.membertenantresponsevo.onbirthday)
                        {
                            pnlMember.Top = pnlWaitingMember.Height+20;
                            picBirthday.Top = pnlMember.Top - picBirthday.Height + 5;

                        }else{
                            pnlMember.Top = pnlWaitingMember.Height;
                        }
                        int JFTOP = lblJFStr.Top;
                        if (lblCoupon.Visible)
                        {
                            JFTOP = lblCoupon.Top + lblCoupon.Height+3;
                        }
                        else
                        {
                            JFTOP = lblWechartNickName.Top + lblWechartNickName.Height+3;
                        }

                        lblJF.Top = JFTOP;
                        lblJFStr.Top = JFTOP;
                        btnCheckJF.Top=JFTOP-3;

                        btnJFUse.Top = JFTOP + lblJF.Height+3;

                        if (btnJFUse.Visible)
                        {
                            pnlMember.Height = btnJFUse.Top + btnJFUse.Height + 15;
                        }
                        else
                        {
                            pnlMember.Height = lblJF.Top + lblJF.Height + 15;
                        }

                        pnlOrdreDetail.Top = pnlMember.Top + pnlMember.Height + 20;

                    }
                    else
                    {
                        pnlOrdreDetail.Top = pnlWaitingMember.Top + pnlWaitingMember.Height + 20;
                    }


                }
                catch (Exception ex)
                {
                    LogManager.WriteLog("更新订单价格详情异常" + ex.Message + ex.StackTrace);
                }
            }
        }

        private void ClearForm()
        {
            try
            {
                CurrentCart = new Cart();

                dgvGood.Rows.Clear();

                ShowLoading(false);

                lblPrice.Text = "￥" + "0.00";
                lblGoodsCount.Text = "(0件商品)";

                btnModifyPrice.BackgroundImage = Resources.ResourcePos.FixDisenable;
                btnModifyPrice.ForeColor = Color.White;

                btnMemberPromo.Visible = false;

                pnlWaiting.Show();

                dgvOrderDetail.Rows.Clear();

                pnlPayType1.Visible = true;
                pnlPayType2.Visible = false;

                SetBtnPayStarus(false);
               // pnlPayType2.Enabled = false;
                
                UpdateOrderHang();

                ShowLoading(false);// LoadingHelper.CloseForm();

                Other.CrearMemory();
                Application.DoEvents();
                MainModel.frmmainmedia.IniForm(null);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("清空主界面异常" + ex.Message);
            }
        }
        #endregion

        #region  会员积分优惠券

        private void btnLoadPhone_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }
                frmNumberBack frmnumberback = new frmNumberBack("输入会员手机号", NumberType.MemberCode, ShowLocation.Right);
                
                frmnumberback.Location = new Point(0, 0);
                frmnumberback.ShowDialog();

                if (frmnumberback.DialogResult == DialogResult.OK)
                {
                    
                    string ErrorMsgMember = "";
                    Member member = httputil.GetMember(frmnumberback.strNumValue, ref ErrorMsgMember);

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
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("会员登录异常" + ex.Message);
            }
            finally
            {
                LoadPicScreen(false);
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

                        string ErrorMsgMember = "";
                        Member member = httputil.GetMember(goodscode, ref ErrorMsgMember);

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

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ERROR", "会员号处理异常：" + ex.Message);
            }
            finally
            {
                LoadPicScreen(false);

                    ShowLoading(false);// LoadingHelper.CloseForm();//关闭
            }
        }

        private object thislockmember = new object();
        private void LoadMember(Member member)
        {
            lock (thislockmember)
            {
                try
                {
                    this.Invoke(new InvokeHandler(delegate()
                    {
                        MainModel.CurrentMember = member;

                        Thread threadloadMember = new Thread(WinSaasPOS.Model.HalfOffLine.HalfOffLineUtil.LoadMemberInfo);
                        threadloadMember.IsBackground = true;
                        threadloadMember.Start();
                        

                        IsEnable = false;
                        pnlWaitingMember.Visible = false;
                        pnlMember.Visible = true;
                        //
                        lblMobil.Text = member.memberheaderresponsevo.mobile;

                        //if (!string.IsNullOrEmpty(member.memberinformationresponsevo.nickname))
                        //{
                        //    lblWechartNickName.Text = member.memberinformationresponsevo.nickname;
                        //}
                        //else
                        //{
                        //    lblWechartNickName.Text = member.memberinformationresponsevo.wechatnickname;
                        //}
                        lblWechartNickName.Text = member.memberinformationresponsevo.wechatnickname;

                        lblJF.Text = member.creditaccountrepvo.availablecredit.ToString();
                        btnCheckJF.BackgroundImage = picUncheck.BackgroundImage;
                        lblCoupon.Visible = false;
                        lblCouponStr.Visible = false;
                        Application.DoEvents();

                        //购物车有商品的话刷新一次
                        if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                        {
                            RefreshCart();
                        }
                        else
                        {
                            //UploadMember();

                            Thread threadMember = new Thread(UploadMember);
                            threadMember.IsBackground = true;
                            threadMember.Start();
                        }
                        IsEnable = true;
                                               
                        Application.DoEvents();

                        if (member.membertenantresponsevo.onbirthday)
                        {
                            picBirthday.Visible = true;
                            // picBirthday.Select();
                        }
                        else
                        {
                            picBirthday.Visible = false;
                        }

                        Application.DoEvents();
                        MainModel.frmmainmedia.LoadMember();
                    }));
                }
                catch (Exception ex)
                {
                    IsEnable = true;
                    ShowLoading(false);// LoadingHelper.CloseForm();

                    LogManager.WriteLog("加载会员信息异常：" + ex.Message + ex.StackTrace);
                }
                finally
                {
                    IsEnable = true;
                    ShowLoading(false);// LoadingHelper.CloseForm();
                }
            }
        }

        //退出会员
        private void lblExitMember_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }

                FrmConfirmBack frmconfirmback = new FrmConfirmBack("是否确认退出会员？", "", "");

                frmconfirmback.Location = new Point(0, 0);
                
                if (frmconfirmback.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                ClearMember();
                UploadOffLineDgvGoods();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("退出会员异常"+ex.Message);
            }
            finally
            {
                LoadPicScreen(false);
            }
        }

        //清空会员信息
        private void ClearMember()
        {
            try
            {
                this.Invoke(new InvokeHandler(delegate()
                {
                    WinSaasPOS.Model.HalfOffLine.HalfOffLineUtil.ClearMemberInfo();
                    //Thread threadloadMember = new Thread(WinSaasPOS.Model.HalfOffLine.HalfOffLineUtil.ClearMemberInfo);
                    //threadloadMember.IsBackground = true;
                    //threadloadMember.Start();

                    lblJF.Text = "0";
                    btnJFUse.Text = "";
                    btnJFUse.Visible = false;
                    MainModel.CurrentMember = null;
                    MainModel.CurrentCouponCode = "";
                    MainModel.Currentavailabecoupno = null;
                    //chkJF.Checked = false;
                    btnCheckJF.BackgroundImage = picUncheck.BackgroundImage;
                    pnlWaitingMember.Visible = true;
                    pnlMember.Visible = false;
                    picBirthday.Visible = false;

                    Thread threadMember = new Thread(UploadMember);
                    threadMember.IsBackground = true;
                    threadMember.Start();

                    Application.DoEvents();
                    MainModel.frmmainmedia.LoadMember();

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
                if (!IsEnable)
                {
                    return;
                }
                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                {
                    MainModel.CurrentMember.isUsePoint = !MainModel.CurrentMember.isUsePoint;
                   
                    btnCheckJF.BackgroundImage = MainModel.CurrentMember.isUsePoint ? picCheck.BackgroundImage:picUncheck.BackgroundImage;

                    UploadOffLineDgvGoods();
                }
                else
                {
                    btnCheckJF.BackgroundImage = picUncheck.BackgroundImage;
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

            try
            {
                if (!IsEnable)
                {
                    return;
                }
                LoadPicScreen(true);
                frmCoupon frmcoupon = new frmCoupon(CurrentCart, MainModel.CurrentCouponCode);
                asf.AutoScaleControlTest(frmcoupon, 380, 480, 380 * MainModel.wScale, 480 * MainModel.hScale, true);
                frmcoupon.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmcoupon.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmcoupon.Height) / 2);
                frmcoupon.TopMost = true;
                frmcoupon.ShowDialog();

                LoadPicScreen(false);
                MainModel.CurrentCouponCode = frmcoupon.SelectCouponCode;
                MainModel.Currentavailabecoupno = frmcoupon.SelectPromotionCode;


                if (string.IsNullOrEmpty(frmcoupon.SelectCouponCode))
                {
                    CurrentCart.selectedcoupons = null;
                }
                else
                {
                    CurrentCart.selectedcoupons = new Dictionary<string, Availablecoupon>();
                    CurrentCart.selectedcoupons.Add(frmcoupon.SelectCouponCode, frmcoupon.SelectPromotionCode);
                }


                UploadOffLineDgvGoods();
                bool RefreshCartOK = true;

                //收银完成
                if (frmcoupon.DialogResult == DialogResult.Yes && RefreshCartOK)
                {
                    if (!RefreshCart()) ;
                    {
                        return;
                    }
                    if (CurrentCart.totalpayment == 0)
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
                        frmPaySuccess frmresult = new frmPaySuccess(orderresult.orderid);
                        frmresult.DataReceiveHandle += FormCashierResult_DataReceiveHandle;
                        frmresult.ShowDialog();
                        ClearForm();
                        ClearMember();
                    }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("选择优惠券异常：" + ex.Message);
            }
            finally
            {
                LoadPicScreen(false);
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
                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0 && CurrentCart.totalpayment > 0)
                {
                    frmModifyPriceBack frmmodefypriceback = new frmModifyPriceBack(CurrentCart);
                    frmmodefypriceback.Location = new Point(0,0);
                    frmmodefypriceback.ShowDialog();

                    this.Enabled = true;
                    Application.DoEvents();

                    if (frmmodefypriceback.DialogResult == DialogResult.OK)
                    {
                        CurrentCart.fixpricetotal = frmmodefypriceback.fixpricetotal;
                        whetherfix = true;
                        UploadOffLineDgvGoods();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowLog("修改订单金额异常：" + ex.Message, true);
            }
            finally
            {
                this.Enabled = true;

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

                            frmPaySuccess frmresult = new frmPaySuccess(orderid);
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

                    // btnUSB.Focus();
                }));
            }
            catch { }

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
        #endregion

        #region 支付
        //在线支付
        private void btnPayOnLine_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }
                if (btnPayOnLine.Tag == null || btnPayOnLine.Tag.ToString() != "1")
                {
                    return;
                }

                if (!RefreshCart())
                {
                    return;
                }

                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                {
                   ShowLoading(true);
                    string ErrorMsgCart = "";
                    int ResultCode = 0;
                    Cart cart = httputil.RefreshCart(CurrentCart, ref ErrorMsgCart, ref ResultCode);

                    if (ErrorMsgCart != "" || cart == null) //商品不存在或异常
                    {

                        ShowLog(ErrorMsgCart, false);
                        CheckUserAndMember(ResultCode, ErrorMsgCart);
                        ShowLoading(false);// LoadingHelper.CloseForm();
                    }
                    else
                    {
                        ShowLoading(false);
                        CurrentCart = cart;
                        string ErrorMsg = "";
                        CreateOrderResult orderresult = httputil.CreateOrder(CurrentCart, ref ErrorMsg, ref ResultCode);
                        if (ResultCode != 0 || orderresult == null)
                        {
                            CheckUserAndMember(ResultCode, ErrorMsg);
                            ShowLoading(false);// LoadingHelper.CloseForm();
                        }
                        else if (orderresult.continuepay == 1)
                        {

                            frmOnLinePayResultBack frmonlinepayresultback = new frmOnLinePayResultBack(orderresult.orderid,CurrentCart);
                            frmonlinepayresultback.Location = new Point(0, 0);
                            frmonlinepayresultback.DataReceiveHandle += FormOnLinePayResult_DataReceiveHandle;
                            frmonlinepayresultback.ShowDialog();
                        }
                    }
                }

                
                Application.DoEvents();

            }
            catch (Exception ex)
            {
                ShowLog("在线收银异常" + ex.Message, true);
            }
            finally
            {
            }
        }

        //现金支付
        private void btnPayByCash_Click(object sender, EventArgs e)
        {
            try
            {

                if (!IsEnable)
                {
                    return;
                }
                if (btnPayByCash.Tag == null || btnPayByCash.Tag.ToString() != "1")
                {
                    return;
                }

                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                {

                    CurrentCart.cashpayoption = 1;
                    if (!RefreshCart())
                    {
                        return;
                    }                  

                    frmCashPayBack frmcashpayback = new frmCashPayBack(CurrentCart);
                    frmcashpayback.ShowDialog();

                    int type = frmcashpayback.cashpaytype;
                    string orderid = frmcashpayback.cashpayorderid;
                    if (type == 0)
                    {
                        this.Invoke(new InvokeHandler(delegate()
                        {
                            frmOnLinePayResultBack frmonlinepayresultback = new frmOnLinePayResultBack(orderid,frmcashpayback.CurrentCart);
                            frmonlinepayresultback.Location = new Point(0, 0);
                            frmonlinepayresultback.DataReceiveHandle += FormOnLinePayResult_DataReceiveHandle;
                            frmonlinepayresultback.ShowDialog();
                        }));
                    }
                    else if (type == 1)
                    {
                        this.Invoke(new InvokeHandler(delegate()
                        {
                            frmPaySuccess frmresult = new frmPaySuccess(orderid);
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

                    RefreshCart();
                }
            }
            catch (Exception ex)
            {
                ShowLog("现金收银异常：" + ex.Message, true);
            }
            finally
            {
                LoadPicScreen(false);                
                
            }
        }

        //余额支付
        private void btnPayByBalance_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }

                if (btnPayByBalance.Tag == null || btnPayByBalance.Tag.ToString() != "1")
                {
                    return;
                }

                if (!RefreshCart())
                {
                    return;
                }

                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                {
                    frmBalancePayResultBack frmbalancepayresultback = new frmBalancePayResultBack(CurrentCart);
                    frmbalancepayresultback.Location = new Point(0,0);
                    frmbalancepayresultback.ShowDialog();

                    if (frmbalancepayresultback.ExpiredCode == ExpiredType.MemberExpired)
                    {
                        CheckUserAndMember(MainModel.HttpMemberExpired, "");
                    }
                    else if (frmbalancepayresultback.ExpiredCode == ExpiredType.UserExpired)
                    {
                        CheckUserAndMember(MainModel.HttpUserExpired, "");
                    }
                    else if (frmbalancepayresultback.ExpiredCode == ExpiredType.DifferentMember)
                    {
                        CheckUserAndMember(MainModel.DifferentMember, "");
                    }

                    else if (frmbalancepayresultback.DialogResult == DialogResult.OK) //订单完成
                    {
                        frmPaySuccess frmresult = new frmPaySuccess(frmbalancepayresultback.OverOrderId);
                        frmresult.DataReceiveHandle += FormCashierResult_DataReceiveHandle;
                        frmresult.ShowDialog();

                        ClearForm();
                        ClearMember();
                    }
                    else if (frmbalancepayresultback.DialogResult == DialogResult.Retry) //需要继续支付  微信/支付宝
                    {
                        LoadPicScreen(true);
                        frmBalanceToMix frmmix = new frmBalanceToMix(frmbalancepayresultback.CurrentCart);
                        asf.AutoScaleControlTest(frmmix, 380, 520, Screen.AllScreens[0].Bounds.Width *36 / 100, this.Height * 70 / 100, true);
                        frmmix.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width - frmmix.Width - 40, this.Height * 15 / 100);
                        frmmix.TopMost = true;
                        frmmix.ShowDialog();

                        if (frmmix.DialogResult == DialogResult.Retry)
                        {
                            this.Invoke(new InvokeHandler(delegate()
                            {
                                frmOnLinePayResultBack frmonlinepayresultback = new frmOnLinePayResultBack(frmmix.CrrentOrderid,frmmix.CurrentCart);
                                frmonlinepayresultback.Location = new Point(0, 0);
                                frmonlinepayresultback.DataReceiveHandle += FormOnLinePayResult_DataReceiveHandle;
                                frmonlinepayresultback.ShowDialog();
                            }));
                        }
                        else if (frmmix.DialogResult == DialogResult.OK)
                        {

                            this.Invoke(new InvokeHandler(delegate()
                            {
                                frmPaySuccess frmresult = new frmPaySuccess(frmmix.CrrentOrderid);
                                frmresult.DataReceiveHandle += FormCashierResult_DataReceiveHandle;
                                frmresult.ShowDialog();

                                ClearForm();
                                ClearMember();
                            }));
                        }
                        else
                        {
                            if (pnlMember.Visible == false && MainModel.CurrentMember != null)
                            {
                                LoadMember(MainModel.CurrentMember);

                            }
                            CheckUserAndMember(frmmix.ErrorCode, "");
                        }
                    }
                    else
                    {
                        if (pnlMember.Visible == false && MainModel.CurrentMember != null)
                        {
                            LoadMember(MainModel.CurrentMember);
                        }
                        RefreshCart();
                    }

                }
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                ShowLog("在线收银异常" + ex.Message, true);
            }
            finally
            {
                LoadPicScreen(false);
            }
        }


        //代金券支付
        private void btnByCoupon_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }


                if (btnPayByCoupon.Tag == null || btnPayByCoupon.Tag.ToString() != "1")
                {
                    return;
                }

                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                {

                    if (!RefreshCart())
                    {
                        return;
                    }

                    string ErrorMsg = "";
                    List<string> lstCashCoupons = httputil.GetAvailableCashCoupons(ref ErrorMsg);

                    if (!string.IsNullOrEmpty(ErrorMsg))
                    {
                        ShowLog(ErrorMsg, false);
                        return;
                    }
                    else if (lstCashCoupons == null || lstCashCoupons.Count == 0)
                    {
                        ShowLog("没有可用的代金券",false);
                        return;
                    }



                    frmCashCouponBack frmcashcouponback = new frmCashCouponBack(CurrentCart,lstCashCoupons);
                    frmcashcouponback.Location = new Point(0, 0);
                    frmcashcouponback.ShowDialog();


                    if (frmcashcouponback.DialogResult == DialogResult.OK)
                    {
                        frmPaySuccess frmresult = new frmPaySuccess(frmcashcouponback.SuccessOrderID);
                        frmresult.DataReceiveHandle += FormCashierResult_DataReceiveHandle;
                        frmresult.ShowDialog();

                        ClearForm();
                        ClearMember();
                    }
                    else
                    {
                        CurrentCart.cashcouponamt = 0;
                        if (pnlMember.Visible == false && MainModel.CurrentMember != null)
                        {
                            LoadMember(MainModel.CurrentMember);
                        }
                        RefreshCart();
                    }

                    CurrentCart.cashcouponamt = 0;
                }
            }
            catch (Exception ex)
            {
                ShowLog("代金券收银异常：" + ex.Message, true);
            }
            finally
            {
                LoadPicScreen(false);

            }

        }


        private void btnPayOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }

                if (!RefreshCart())
                {
                    return;
                }
                //订单离线计算直接完成时  调用一次在线接口复查防止计算出错
                if (CurrentCart == null || CurrentCart.products == null || CurrentCart.products.Count == 0 || CurrentCart.totalpayment != 0)
                {
                    return;
                }

               ShowLoading(true);
                string ErrorMsg = "";
                int ResultCode = 0;
                CreateOrderResult orderresult = httputil.CreateOrder(CurrentCart, ref ErrorMsg, ref ResultCode);
                ShowLoading(false);// LoadingHelper.CloseForm();
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
                    frmPaySuccess frmresult = new frmPaySuccess(orderresult.orderid);
                    frmresult.DataReceiveHandle += FormCashierResult_DataReceiveHandle;
                    frmresult.ShowDialog();
                    ClearForm();
                    ClearMember();
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("完成支付异常"+ex.Message);
            }
        }
        #endregion



        private void dgvGood_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }

                if (e.RowIndex < 0)
                    return;

                //int oldrowindex = dgvGood.FirstDisplayedScrollingRowIndex;
                Bitmap bmp = (Bitmap)dgvGood.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                Product pro = (Product)bmp.Tag;

                if (e.ColumnIndex == 2)
                {

                    if (!string.IsNullOrEmpty(pro.price.purchaselimitsubdesc))
                    {
                        //TODO  温馨提示 知道了
                        FrmConfirmBack frmconfirmback = new FrmConfirmBack("温馨提示", "知道了", "");

                        frmconfirmback.Location = new Point(0, 0);
                        frmconfirmback.ShowDialog();
                        this.Enabled = true;
                    }                   
                    return;
                }


                LastLstPro = new List<Product>();
                foreach (Product ppro in CurrentCart.products)
                {
                    LastLstPro.Add((Product)MainModel.Clone(ppro));
                }
                //增加标品
                if (e.ColumnIndex==4 && pro.goodstagid==0)
                {
                    for (int i = 0; i < CurrentCart.products.Count; i++)
                    {
                        if (CurrentCart.products[i].barcode == pro.barcode)
                        {
                            CurrentCart.products[i].num += 1;
                            break;
                        }
                    }

                    UploadOffLineDgvGoods();
                }
                //减少标品

                if (e.ColumnIndex == 3 && pro.goodstagid == 0)
                {
                    for (int i = 0; i < CurrentCart.products.Count; i++)
                    {
                        if (CurrentCart.products[i].barcode == pro.barcode)
                        {

                            if (CurrentCart.products[i].num == 1)
                            {


                                FrmConfirmBack frmconfirmback = new FrmConfirmBack("确认删除", pro.title, pro.skucode);

                                frmconfirmback.Location = new Point(0, 0);


                                if (frmconfirmback.ShowDialog() == DialogResult.OK)
                                {
                                    ReceiptUtil.EditCancelSingle(1, CurrentCart.products[i].price.origintotal);
                                    CurrentCart.products.RemoveAt(i);
                                }

                            }
                            else
                            {
                                ReceiptUtil.EditCancelSingle(1, CurrentCart.products[i].price.origintotal);
                                CurrentCart.products[i].num -= 1;
                            }
                            break;
                        }
                    }

                    UploadOffLineDgvGoods();
                }

                if (e.ColumnIndex == 6)
                {

                    FrmConfirmBack frmconfirmback = new FrmConfirmBack("确认删除", pro.skuname, pro.skucode);

                    frmconfirmback.Location = new Point(0, 0);

                    if (frmconfirmback.ShowDialog() != DialogResult.OK)
                    {

                        return;
                    }

                    foreach (Product delpro in CurrentCart.products)
                    {
                        if (delpro.skucode == pro.skucode && delpro.specnum==pro.specnum)
                        {
                            ReceiptUtil.EditCancelSingle(delpro.num, delpro.price.origintotal);
                            CurrentCart.products.Remove(delpro);
                            break;
                        }
                    }
                    UploadOffLineDgvGoods();
                }
                //try { dgvGood.FirstDisplayedScrollingRowIndex = oldrowindex; }
                //catch { }

                dgvGood.ClearSelection();
            }
            catch (Exception ex)
            {

                ShowLog("操作购物车商品异常" + ex.Message, true);
            }
            finally
            {
                //Delay.Start(200);
                btnScan.Select();
                LoadPicScreen(false);
            }
        }

        //防止控件占用焦点后  按键无法捕获
        private void dgvGood_Click(object sender, EventArgs e)
        {
            //Delay.Start(200);
            btnScan.Select();
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

        private void LoadPicScreen(bool isShown)
        {
            try
            {
                this.Invoke(new InvokeHandler(delegate()
                {
                if (isShown)
                {
                    if (!picScreen.Visible)
                    {
                        picScreen.BackgroundImage = MainModel.GetWinformImage(this);
                        picScreen.Size = new System.Drawing.Size(this.Width, this.Height);
                       // picScreen.Location = new Point(0,0);
                        picScreen.Visible = true;
                    }                   
                }
                else
                {
                    picScreen.Visible = false;
                }

                //否则不悬浮？
                if (picBirthday.Visible)
                {
                    picBirthday.Visible = false;
                    picBirthday.Visible = true;
                }
                Application.DoEvents();
            }));
            }
            catch (Exception ex)
            {
                picScreen.Visible = false;
               // LogManager.WriteLog("修改主窗体背景图异常：" + ex.Message);
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
              
                        frmPrinterSettingBack frmsettingback = new frmPrinterSettingBack();
                        frmsettingback.ShowDialog();                    
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("打印机设置页面异常"+ex.Message);
            }
           
        }

        private void btnMianban_Click(object sender, EventArgs e)
        {

            if (!IsEnable)
            {
                return;
            }
            //LoadIncrementProduct();
            try
            {
                IsEnable = false;
                frmPanelGoods frmpanel = new frmPanelGoods();
                asf.AutoScaleControlTest(frmpanel,1178,760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height , true);
                frmpanel.Location = new System.Drawing.Point(0, 0);

                frmpanel.ShowDialog();

                Application.DoEvents();

                IsEnable = true;
                

                if (frmpanel.ExpiredCode == ExpiredType.MemberExpired)
                {
                    CheckUserAndMember(MainModel.HttpMemberExpired, "");
                }
                else if (frmpanel.ExpiredCode == ExpiredType.UserExpired)
                {
                    CheckUserAndMember(MainModel.HttpUserExpired, "");
                }
                //IsScan = true;
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
                        LastLstPro = null;
                        CurrentCart.products = frmpanel.CurrentCart.products;
                        UploadOffLineDgvGoods();

                        //RefreshCart(LastLstPro);
                    }
                    else
                    {
                        LastLstPro = new List<Product>();
                        foreach (Product pro in frmpanel.CurrentCart.products)
                        {
                            if (pro.goodstagid!=0)
                            {
                                CurrentCart.products.Add(pro);
                            }
                            else
                            {
                                bool isexits = false;
                                for (int i = 0; i < CurrentCart.products.Count; i++)
                                {
                                    if (CurrentCart.products[i].skucode == pro.skucode)
                                    {
                                        CurrentCart.products[i].num += pro.num; ;
                                        CurrentCart.products[i].price.total = Math.Round(CurrentCart.products[i].num * CurrentCart.products[i].price.saleprice, 2, MidpointRounding.AwayFromZero);
                                        CurrentCart.products[i].price.origintotal = Math.Round(CurrentCart.products[i].num * CurrentCart.products[i].price.originprice, 2, MidpointRounding.AwayFromZero);
                                        // CurrentCart.products[i].PaySubAmt = Math.Round(CurrentCart.products[i].num * CurrentCart.products[i].price.saleprice, 2, MidpointRounding.AwayFromZero);
                                        isexits = true;
                                        break;
                                    }
                                }

                                if (!isexits)
                                {
                                    CurrentCart.products.Add(pro);
                                }
                            }

                            //LastLstPro.Add((Product)MainModel.Clone(ppro));
                        }

                       // CurrentCart.products.AddRange(frmpanel.CurrentCart.products);
                        UploadOffLineDgvGoods();
                       // RefreshCart(LastLstPro);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowLog("加载面板商品异常" + ex.Message, true);
            }
            finally
            {
                IsEnable = true;
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

            }
        }

        private void UpdatePayment()
        {
            try
            {
                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                {
                    //tag=1 允许访问 0 或其他不允许   改变enabled背景色丑
                    if (CurrentCart.paymenttypes.balancepayenabled == 1)
                    {
                        btnPayByBalance.Tag = 1;
                        btnPayByBalance.BackColor = Color.DarkTurquoise;
                    }
                    else
                    {
                        btnPayByBalance.Tag = 0;
                        btnPayByBalance.BackColor = Color.Silver;
                    }

                    if (CurrentCart.paymenttypes.cashenabled == 1)
                    {
                        btnPayByCash.Tag = 1;
                        btnPayByCash.BackColor = Color.DarkOrange;
                    }
                    else
                    {
                        btnPayByCash.Tag = 0;
                        btnPayByCash.BackColor = Color.Silver;
                    }

                    if (CurrentCart.paymenttypes.onlineenabled == 1)
                    {
                        btnPayOnLine.Tag = 1;
                        btnPayOnLine.BackColor = Color.Tomato;
                    }
                    else
                    {
                        btnPayOnLine.Tag = 0;
                        btnPayOnLine.BackColor = Color.Silver;
                    }


                    if (CurrentCart.paymenttypes.cashcouponpayenabled == 1)
                    {

                        btnPayByCoupon.Tag = 1;
                        btnPayByCoupon.BackColor = Color.MediumSeaGreen;
                    }
                    else
                    {
                        btnPayByCoupon.Tag = 0;
                        btnPayByCoupon.BackColor = Color.Silver;
                    }
                }
                else
                {
                    SetBtnPayStarus(false);
                }
            }
            catch (Exception ex)
            {

            }
        }


        StringBuilder scancode = new StringBuilder();
        protected override bool ProcessDialogKey(Keys keyData)
        {
            try
            {
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

        private string FlashSkuCode = "";
        private int FlashIndex = 0;
        //放线程就无效了
        public bool isGoodRefresh = true;
        private void ShowDgv()
        {
            try
            {

                if (dgvGood.Rows.Count >= FlashIndex && isGoodRefresh)
                {
                    isGoodRefresh = false;
                    System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
                    dataGridViewCellStyle1.BackColor = Color.PeachPuff;
                    Color color = dgvGood.Rows[FlashIndex].DefaultCellStyle.BackColor;

                    dgvGood.Rows[FlashIndex].DefaultCellStyle = dataGridViewCellStyle1;

                    Delay.Start(150);
                    dataGridViewCellStyle1.BackColor = color;
                    dgvGood.Rows[FlashIndex].DefaultCellStyle = dataGridViewCellStyle1;


                    FlashIndex = 0;
                    FlashSkuCode = "";
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("突出新增商品异常"+ex.Message);
            }
        }

        #region 商品全量/增量数据同步

        private void timerGetIncrementProduct_Tick(object sender, EventArgs e)
        {
            ////启动促销商品同步线程
            //Thread threadLoadPromotion = new Thread(serverdatautil.UpdatePromotion);
            //threadLoadPromotion.IsBackground = false;
            //threadLoadPromotion.Start();
            ////启动增量商品同步线程
            //Thread threadLoadIncrementProduct = new Thread(LoadIncrementProduct);
            //threadLoadIncrementProduct.IsBackground = true;
            //threadLoadIncrementProduct.Start();
            //if (httputil.CheckScaleUpdate())
            //{
            //    //启动电子秤同步信息线程
            //    Thread threadLoadScale = new Thread(LoadScale);
            //    threadLoadScale.IsBackground = true;
            //    threadLoadScale.Start();

            //    Thread threadSendScale = new Thread(SendScale);
            //    threadSendScale.IsBackground = true;
            //    threadSendScale.Start();
            //}
            //else
            //{
            //    MainModel.LastScaleTimeStamp = MainModel.getStampByDateTime(DateTime.Now);
            //}
        }



        /// <summary>
        /// 清理本地历史数据
        /// </summary>
        private void ClearHistoryData()
        {
            try
            {

                long currentstamp =Convert.ToInt64(MainModel.getStampByDateTime(DateTime.Now.AddDays(-7)));

                string strwhere = " CREATE_TIME <"+currentstamp+" and SYN_TIME>0";
                orderbll.ClearHistory(strwhere);

                receiptbll.ClearHistory(strwhere);


            }
            catch (Exception ex)
            {
                LogManager.WriteLog("清理本地历史数据异常"+ex.Message);
            }
        }
        #endregion





        public void btnWindows_Click(object sender, EventArgs e)
        {
            MainModel.ShowTask();
            this.WindowState = FormWindowState.Minimized;
        }

        private void CheckPrint()
        {
            try
            {
                try
                {
                    bool PrintCheck = Convert.ToBoolean(INIManager.GetIni("Print", "PrintCheck", MainModel.IniPath));

                    if (!PrintCheck)
                    {
                        return;
                    }
                }
                catch { }

                //系统自带打印机集合
                List<string> lstprintname = new List<string>();
                lstprintname.Add("Fax");
                lstprintname.Add("Microsoft XPS Document Writer");
                lstprintname.Add("OneNote");
                lstprintname.Add("Fax");
                lstprintname.Add("Foxit Reader PDF Printer");
                lstprintname.Add("Microsoft Print to PDF");
                lstprintname.Add("发送至 OneNote 2010");

                string checkPrintName = "";
                string PrintName = INIManager.GetIni("Print", "PrintName", MainModel.IniPath);

                bool ExitsPrinter = false;
                //获取安装的打印机列表，并选中默认打印机
                foreach (string print in PrinterSettings.InstalledPrinters)
                {
                    if (print == PrintName)
                    {
                        checkPrintName = PrintName;
                        ExitsPrinter = true;
                    }
                }

                //默认打印机
                PrintDocument pd = new PrintDocument();
                string defaultStr = pd.PrinterSettings.PrinterName;

                if (!ExitsPrinter)
                {
                    checkPrintName = defaultStr;
                    INIManager.SetIni("Print", "PrintName", defaultStr, MainModel.IniPath);
                }

                if (lstprintname.Contains(checkPrintName))
                {
                    LoadPicScreen(true);
                    
                    frmPrinterInfo frmprintinfo = new frmPrinterInfo();

                    //asf.AutoScaleControlTest(frmprintinfo, 480, 190, 480*MainModel.wScale, 190*MainModel.hScale, true);
                    frmprintinfo.TopMost = true;

                    frmprintinfo.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmprintinfo.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmprintinfo.Height) / 2);
                    frmprintinfo.ShowDialog();
                    
                    if (frmprintinfo.DialogResult == DialogResult.OK)
                    {
                        tsmPrintSet_Click(null, null);
                    }

                    LoadPicScreen(false);
                }
                
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("检查打印机异常"+ex.Message);
            }
            finally
            {
                LoadPicScreen(false);
            }
        }

        private void picScreen_Click(object sender, EventArgs e)
        {
            LoadPicScreen(false);
        }

        private void ShowLoading(bool isshow)
        {
            try
            {
                if (this.IsHandleCreated)
                {
                    this.Invoke(new InvokeHandler(delegate()
                     {
                         picLoading.Visible = isshow;
                     }));
                }
                else
                {
                    picLoading.Visible = isshow;
                }                
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("显示等待异常" + ex.Message);
            }
        }

        #region 解决闪烁问题

        //此方法不可重写 禁止擦除背景会极大影响页面加载速度
        //protected override void WndProc(ref Message m)
        //{
        //    if (m.Msg == 0x0014) // 禁掉清除背景消息
        //        return;
        //    base.WndProc(ref m);
        //}


        ///// <summary>
        ///// 改方法设置界面加载完成后显示  屏蔽掉了页面加载过程中的闪烁
        ///// </summary>
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;
        //        return cp;
        //    }
        //}
        #endregion


        /// <summary>
        /// 客屏播放视频会占用焦点，此时主界面按键监听（扫描枪）无效，需要刷新焦点
        /// </summary>
        private void CheckActivate()
        {
            while (IsRun)
            {
                try
                {
                    if ( MainModel.IsPlayer && this.WindowState!=FormWindowState.Minimized && IsEnable)
                    {
                        MainModel.IsPlayer = false;
                        Thread.Sleep(300);
                        this.Activate();
                        Thread.Sleep(300);
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

        //初始化弹窗页面，提前加载 防止加载资源影藏页面  防止加载时速度慢闪屏
        private void LoadFormIni()
        {
            try {
                DateTime starttime = DateTime.Now;
                //数字弹窗初始化
                MainModel.frmnumber = new frmNumber();
                asf.AutoScaleControlTest(MainModel.frmnumber, 380, 540, this.Width * 36 / 100, this.Height * 70 / 100, true);
                MainModel.frmnumber.TopMost = true;

                //现金支付窗体
                MainModel.frmcashpay = new frmCashPay();
                asf.AutoScaleControlTest(MainModel.frmcashpay, 380, 520, Screen.AllScreens[0].Bounds.Width * 36 / 100, Screen.AllScreens[0].Bounds.Height * 70 / 100, true);
                MainModel.frmcashpay.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width - MainModel.frmcashpay.Width - 40, Screen.AllScreens[0].Bounds.Height * 15 / 100);
                MainModel.frmcashpay.TopMost = true;

                //现金券弹窗
                MainModel.frmcashcoupon = new frmCashCoupon();
                asf.AutoScaleControlTest(MainModel.frmcashcoupon, 380, 480, Screen.AllScreens[0].Bounds.Width * 36 / 100, Screen.AllScreens[0].Bounds.Height * 70 / 100, true);
                MainModel.frmcashcoupon.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width - MainModel.frmcashpay.Width - 40, Screen.AllScreens[0].Bounds.Height * 15 / 100);
                MainModel.frmcashcoupon.TopMost = true;
                 
                //修改订单金额弹窗
                MainModel.frmmodifyprice = new frmModifyPrice();
                asf.AutoScaleControlTest(MainModel.frmmodifyprice, 380, 520, Screen.AllScreens[0].Bounds.Width * 36 / 100, Screen.AllScreens[0].Bounds.Height * 70 / 100, true);
                MainModel.frmmodifyprice.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width - MainModel.frmmodifyprice.Width - 40, Screen.AllScreens[0].Bounds.Height * 15 / 100);
                MainModel.frmmodifyprice.TopMost = true;

                //打印机设置窗体
                MainModel.frmprintersetting = new frmPrinterSetting();
                MainModel.frmprintersetting.TopMost = true;
                asf.AutoScaleControlTest(MainModel.frmprintersetting, 536, 243, 536 * MainModel.wScale, 243 * MainModel.hScale, true);
                MainModel.frmprintersetting.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - MainModel.frmprintersetting.Width) / 2, (Screen.AllScreens[0].Bounds.Height - MainModel.frmprintersetting.Height) / 2);

                //菜单栏窗体
                MainModel.frmtoolmain = new frmToolMain();
                asf.AutoScaleControlTest(MainModel.frmtoolmain, 178, 370, Convert.ToInt32(MainModel.wScale * 178), Convert.ToInt32(MainModel.hScale * 370), true);
                MainModel.frmtoolmain.DataReceiveHandle += frmToolMain_DataReceiveHandle;
                MainModel.frmtoolmain.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width - MainModel.frmtoolmain.Width - 15, pnlHead.Height + 10);
             
                Console.WriteLine("页面初始化时间"+(DateTime.Now-starttime).Milliseconds);
            }
            catch
            {
                LogManager.WriteLog("初始化静态页面异常");
            }
        }


        Bitmap bmpbarcode;
        Bitmap bmpPrice;
        Bitmap bmpNum;
        Bitmap bmpAdd;
        Bitmap bmpTotal;
        Bitmap bmpdelete;
        private List<Bitmap> GetDgvRow(Product pro)
        {
            try
            {
                Bitmap add = Resources.ResourcePos.empty;
                lblTitle.Text = pro.title;
                lblSkuCode.Text = pro.skucode;
                //第一行图片
                switch (pro.pricetagid)
                {
                    case 1: lblPriceTag.BackColor = ColorTranslator.FromHtml("#FF7D14"); lblPriceTag.Text = pro.pricetag; break;
                    case 2: lblPriceTag.BackColor = ColorTranslator.FromHtml("#209FD4"); lblPriceTag.Text = pro.pricetag; break;
                    case 3: lblPriceTag.BackColor = ColorTranslator.FromHtml("#D42031"); lblPriceTag.Text = pro.pricetag; break;
                    case 4: lblPriceTag.BackColor = ColorTranslator.FromHtml("#250D05"); lblPriceTag.Text = pro.pricetag; break;
                    default: lblPriceTag.Text = ""; break;
                }

                if (!string.IsNullOrEmpty(pro.price.purchaselimitdesc))
                {
                    btnPurchaseLimit.Width = 10; //自适应大小只会放大不会缩小
                    btnPurchaseLimit.Visible = true;
                    if (!string.IsNullOrEmpty(pro.price.purchaselimitsubdesc))
                    {
                        btnPurchaseLimit.Text = pro.price.purchaselimitdesc + "?";
                    }
                    else
                    {
                        btnPurchaseLimit.Text = pro.price.purchaselimitdesc;
                    }
                }
                else
                {
                    btnPurchaseLimit.Visible = false;
                }
                bmpbarcode = new Bitmap(pnlBarCode.Width, pnlBarCode.Height);
                bmpbarcode.Tag = pro;
                pnlBarCode.DrawToBitmap(bmpbarcode, new Rectangle(0, 0, pnlBarCode.Width, pnlBarCode.Height));

                //第二列图片
                if (pro.price.saleprice == pro.price.originprice)
                {
                    lblSinglePrice.Text = pro.price.saleprice.ToString("f2");

                    lblSinglePrice.Left = (pnlSinglePrice.Width - lblSinglePrice.Width) / 2;

                    lblPriceDesc.Text = "";
                    lblOriginPrice.Text = "";
                }
                else
                {
                    lblPriceDesc.Text = "";
                    lblOriginPrice.Text = "";

                    lblSinglePrice.Text = pro.price.saleprice.ToString("f2");
                    if (!string.IsNullOrEmpty(pro.price.salepricedesc))
                    {
                        lblPriceDesc.Text = pro.price.salepricedesc;
                    }

                    if (pro.price.strikeout == 1)
                    {
                        lblOriginPrice.Text = pro.price.originprice.ToString("f2");
                        lblOriginPrice.Font = new Font(lblOriginPrice.Font.Name, lblOriginPrice.Font.Size, FontStyle.Strikeout);
                    }
                    else
                    {
                        lblOriginPrice.Text = pro.price.originprice.ToString("f2");
                        lblOriginPrice.Font = new Font(lblOriginPrice.Font.Name, lblOriginPrice.Font.Size, FontStyle.Regular);
                    }

                    if (!string.IsNullOrEmpty(pro.price.originpricedesc))
                    {
                        lblOriginPrice.Text += "(" + pro.price.originpricedesc + ")";
                    }

                    lblSinglePrice.Left = (pnlSinglePrice.Width - lblSinglePrice.Width - lblPriceDesc.Width) / 2;
                    lblPriceDesc.Left = lblSinglePrice.Left + lblSinglePrice.Width;
                    lblOriginPrice.Left = (pnlSinglePrice.Width - lblOriginPrice.Width) / 2;
                }
                bmpPrice = new Bitmap(pnlSinglePrice.Width, pnlSinglePrice.Height);
                bmpPrice.Tag = pro;
                pnlSinglePrice.DrawToBitmap(bmpPrice, new Rectangle(0, 0, pnlSinglePrice.Width, pnlSinglePrice.Height));


                //第三 四列图片
                if (pro.goodstagid == 0)  //0是标品  1是称重
                {
                    btnNum.Text =  pro.num.ToString() + "    ";

                    //btnNum.FlatAppearance.BorderSize = 1;

                    //btnIncrease.FlatAppearance.BorderSize = 1;

                    btnNum.BackgroundImage  = Resources.ResourcePos.border_minus;

                    btnIncrease.BackgroundImage = Resources.ResourcePos.border_add;
                    bmpNum = new Bitmap(pnlNum.Width, pnlNum.Height);
                    bmpNum.Tag = pro;
                    pnlNum.DrawToBitmap(bmpNum, new Rectangle(0, 0, pnlNum.Width, pnlNum.Height));

                    bmpAdd = new Bitmap(pnlAdd.Width, pnlAdd.Height);
                    bmpAdd.Tag = pro;
                    pnlAdd.DrawToBitmap(bmpAdd, new Rectangle(0, 0, pnlAdd.Width, pnlAdd.Height));
                }
                else
                {
                    //btnNum.FlatAppearance.BorderSize = 0;

                    //btnNum.FlatAppearance.BorderSize = 0;
                    btnNum.BackgroundImage = Resources.ResourcePos.White;
                    btnIncrease.BackgroundImage = Resources.ResourcePos.White;
                    btnNum.Text = pro.price.specnum + pro.price.unit;
                    bmpNum = new Bitmap(pnlNum.Width, pnlNum.Height);
                    bmpNum.Tag = pro;
                    pnlNum.DrawToBitmap(bmpNum, new Rectangle(0, 0, pnlNum.Width, pnlNum.Height));

                    bmpAdd = Resources.ResourcePos.White;
                }

                if (pro.price.total == pro.price.origintotal)
                {
                    lblTotal.Text = pro.price.total.ToString("f2");
                    lblTotalDesc.Text = "";
                    lblOriginTotal.Text = "";

                    lblTotal.Left = (pnlTotal.Width - lblTotal.Width) / 2;
                }
                else
                {
                    lblTotalDesc.Text = "";
                    lblOriginTotal.Text = "";
                    //total = "￥" + pro.price.total.ToString() + "("+pro.price.salepricedesc+")" + "\r\n" + "￥" + pro.price.origintotal + "("+pro.price.originpricedesc+")";
                    lblTotal.Text = pro.price.total.ToString("f2");

                    if (!string.IsNullOrEmpty(pro.price.salepricedesc))
                    {
                        lblTotalDesc.Text = "(" + pro.price.salepricedesc + ")";
                    }
                    else
                    {
                        lblTotalDesc.Text = "";
                    }

                    if (pro.price.strikeout == 1)
                    {
                        lblOriginTotal.Text = pro.price.origintotal.ToString("f2");
                        lblOriginTotal.Font = new Font(lblOriginTotal.Font.Name, lblOriginTotal.Font.Size, FontStyle.Strikeout);
                    }
                    else
                    {
                        lblOriginTotal.Text = pro.price.origintotal.ToString("f2");
                        lblOriginTotal.Font = new Font(lblOriginTotal.Font.Name, lblOriginTotal.Font.Size, FontStyle.Regular);
                    }

                    if (!string.IsNullOrEmpty(pro.price.originpricedesc))
                    {
                        lblOriginTotal.Text += "(" + pro.price.originpricedesc + ")";
                    }

                    lblTotal.Left = (pnlTotal.Width - lblTotal.Width - lblTotalDesc.Width) / 2;
                    lblTotalDesc.Left = lblTotal.Left + lblTotal.Width;
                    lblOriginTotal.Left = (pnlTotal.Width - lblOriginTotal.Width) / 2;
                }

                bmpTotal = new Bitmap(pnlTotal.Width, pnlTotal.Height);
                bmpTotal.Tag = pro;
                pnlTotal.DrawToBitmap(bmpTotal, new Rectangle(0, 0, pnlTotal.Width, pnlTotal.Height));

                bmpdelete = new Bitmap(picDelete.Image, dgvGood.RowTemplate.Height * 20 / 100, dgvGood.RowTemplate.Height * 20 / 100);
                bmpdelete.Tag = pro;

                bmpbarcode.MakeTransparent(Color.White);
                bmpPrice.MakeTransparent(Color.White);
                bmpNum.MakeTransparent(Color.White);
                bmpAdd.MakeTransparent(Color.White);
                bmpTotal.MakeTransparent(Color.White);


                List<Bitmap> lstbmp = new List<Bitmap>();
                lstbmp.Add(bmpbarcode);
                lstbmp.Add(bmpPrice);
                lstbmp.Add(bmpNum);
                lstbmp.Add(bmpAdd);
                lstbmp.Add(bmpTotal);
                lstbmp.Add(bmpdelete);

                return lstbmp;
            }
            catch (Exception ex)
            {
                ShowLog("解析商品信息异常"+ex.Message,true);
                return null;
            }
        }

        private void dgvGood_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                MainModel.frmmainmedia.UpDgvScorll(dgvGood.FirstDisplayedScrollingRowIndex);
            }
        }

        private void UploadOffLineData()
        {
            try
            {
                UploadPosUser();
                UploadOffLineOrder();
                UploadOffLineRefund();
                UploadOffLineReceiptr();

            }
            catch (Exception ex)
            {

            }
        }

        SYSTEM_USER_BEANBLL userbll = new SYSTEM_USER_BEANBLL();
        private void UploadPosUser()
        {
            try
            {

                string errormsg = "";
                List<OffLineUser> lstresultoffuser = httputil.GetUserForPos(ref errormsg);

                if (lstresultoffuser != null && lstresultoffuser.Count > 0)
                {
                    userbll.DeleteAll();

                    foreach (OffLineUser offuser in lstresultoffuser)
                    {
                        SYSTEM_USER_BEANMODEL adduser = new SYSTEM_USER_BEANMODEL();
                        adduser.NICKNAME = offuser.nickname;
                        adduser.LOGINACCOUNT = offuser.loginaccount;
                        adduser.CREATE_URL_IP = MainModel.URL;

                        userbll.Add(adduser);
                    }
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("添加离线员工信异常" + ex.Message);
            }
        }

        DBORDER_BEANBLL orderbll = new DBORDER_BEANBLL();
        private void UploadOffLineOrder()
        {
            try
            {

                List<DBORDER_BEANMODEL> lstorder = orderbll.GetModelList(" SYN_TIME=null or SYN_TIME=0 " + " and CREATE_URL_IP='" + MainModel.URL + "' ");

                if (lstorder != null && lstorder.Count > 0)
                {
                    foreach (DBORDER_BEANMODEL order in lstorder)
                    {
                        OffLineOrder offlineorder = JsonConvert.DeserializeObject<OffLineOrder>(order.ORDER_JSON);

                        if (order.ORDERSTATUSVALUE == 5)
                        {
                            offlineorder.hasrefunded = 1;
                        }

                        string errormsg = "";
                        bool result = httputil.CreateOffLineOrder(offlineorder, ref errormsg);
                        if (result)
                        {
                            LogManager.WriteLog("离线订单同步完成" + order.OFFLINEORDERID);
                            order.SYN_TIME = Convert.ToInt64(MainModel.getStampByDateTime(DateTime.Now));
                            orderbll.Update(order);
                        }
                        else
                        {
                            LogManager.WriteLog("离线订单同步失败" + order.OFFLINEORDERID + "  " + errormsg);
                        }
                    }
                }
                else
                {
                    //  return;
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("同步离线订单异常" + ex.Message);
            }
        }

        private void UploadOffLineRefund()
        {
            try
            {
                List<DBORDER_BEANMODEL> lstorder = orderbll.GetModelList(" REFUND_TIME> SYN_TIME " + " and CREATE_URL_IP='" + MainModel.URL + "' ");

                if (lstorder != null && lstorder.Count > 0)
                {
                    foreach (DBORDER_BEANMODEL order in lstorder)
                    {
                        if (order.SYN_TIME != null && order.SYN_TIME > 0)
                        {
                            OffLineOrder offlineorder = JsonConvert.DeserializeObject<OffLineOrder>(order.ORDER_JSON);

                            offlineorder.hasrefunded = 1;
                            string errormsg = "";
                            bool result = httputil.OffLineRefund(offlineorder, ref errormsg);

                            order.SYN_TIME = Convert.ToInt64(MainModel.getStampByDateTime(DateTime.Now));
                            orderbll.Update(order);
                        }
                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("同步离线订单异常" + ex.Message);
            }
        }

        DBRECEIPT_BEANBLL receiptbll = new DBRECEIPT_BEANBLL();
        private void UploadOffLineReceiptr()
        {
            try
            {
                List<DBRECEIPT_BEANMODEL> lstreceipt = receiptbll.GetModelList(" SYN_TIME=null or SYN_TIME=0 " + " and CREATE_URL_IP='" + MainModel.URL + "' ");

                if (lstreceipt != null && lstreceipt.Count > 0)
                {
                    foreach (DBRECEIPT_BEANMODEL receipt in lstreceipt)
                    {
                        Receiptdetail receiptdetail = JsonConvert.DeserializeObject<Receiptdetail>(receipt.RECEIPTDETAIL);

                        OffLineReceipt o = new OffLineReceipt();
                        o.id = (int)receipt._id;
                        o.cashier = receipt.CASHIER;
                        o.operatetimestr = receipt.OPERATETIMESTR;
                        o.starttime = receipt.STARTTIME;
                        o.endtime = receipt.ENDTIME;
                        o.netsaleamt = receipt.NETSALEAMT;
                        o.totalpayment = receipt.TOTALPAYMENT;
                        o.cashtotalamt = receipt.TOTALPAYMENT;
                        o.receiptdetail = receiptdetail;
                        o.hasprint = 1;
                        o.onlinemode = 0;
                        o.shopid = receiptdetail.shopid;
                        o.devicecode = receiptdetail.DeviceSN;
                        o.offlinereceiptid = receipt.OFFLINE_RECEIPT_ID;
                        o.createurlip = receipt.CREATE_URL_IP;
                        o.saleclerkphone = receiptdetail.saleclerkphone;
                        string errormsg = "";
                        bool result = httputil.UpdateOffLineReceipt(o, ref errormsg);

                        receipt.SYN_TIME = Convert.ToInt64(MainModel.getStampByDateTime(DateTime.Now));
                        receiptbll.Update(receipt);
                    }
                }
                else
                {
                    //  return;
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("同步离线交班异常" + ex.Message);
            }
        }

        private void ChangeMQTT(object obj)
        {
            try
            {
                bool isstart = (bool)obj;
                if (isstart)
                {
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

        private void button2_Click(object sender, EventArgs e)
        {
            Other.CrearMemory();
        }

        private void frmMainHalfOffLine_Activated(object sender, EventArgs e)
        {
            try
            {
                MainModel.HideTask();
            }
            catch { }
        }


    }
}
