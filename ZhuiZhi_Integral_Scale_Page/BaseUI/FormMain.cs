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
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;
using ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.MyControl;
using ZhuiZhi_Integral_Scale_UncleFruit.PayUI;
using ZhuiZhi_Integral_Scale_UncleFruit.Resources;
using ZhuiZhi_Integral_Scale_UncleFruit.ScaleFactory;
using ZhuiZhi_Integral_Scale_UncleFruit.ScaleUI;

namespace ZhuiZhi_Integral_Scale_UncleFruit
{
    public partial class FormMain : Form
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

        //刷新焦点线程  防止客屏播放视频抢走焦点
        Thread threadCheckActivate;

        private bool IsRun = true;

        /// <summary>
        /// 当前展示分类页数
        /// </summary>
        private int CurrentCategoryPage = 1;

        /// <summary>
        /// 当前展示商品页数
        /// </summary>
        private int CurrentGoodPage = 1;
        /// <summary>
        /// 当前查询商品页数
        /// </summary>
        private int CurrentGoodQueryPage = 1;

        /// <summary>
        /// 当前展示购物车页数
        /// </summary>
        private int CurrentCartPage = 1;

      
        #endregion

        #region 页面加载与退出
        public FormMain() 
        {
            InitializeComponent();
            //使用委托的话frmmain界面会卡死
            Control.CheckForIllegalCrossThreadCalls = false;

            MainModel.wScale = (float)Screen.AllScreens[0].Bounds.Width / this.Width;
            MainModel.hScale = (float)Screen.AllScreens[0].Bounds.Height / this.Height;
            MainModel.midScale = (MainModel.wScale + MainModel.hScale) / 2;
        }

        int TopLblGoodName = 0;
        int HeightLblGoodName = 0;
        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("称重收银页面加载异常" + ex.Message);

            }
        }

       // bool showloading = false;
        private void button1_Click(object sender, EventArgs e)
        {
            //showloading = !showloading;
            //ShowLoading(showloading, true);
            //showlogthread("43434");
            //ShowLog("ceshi",false);
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            try
            {

               // LoadingHelper.CloseForm();
                Application.DoEvents();
                LoadBaseInfo();

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

                LstAllProduct = CartUtil.LoadAllProduct(false);

                if (LstAllProduct == null || LstAllProduct.Count == 0)
                {

                    ServerDataUtil.LoadAllProduct();
                    LstAllProduct = CartUtil.LoadAllProduct(false);
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
                timerScale.Enabled = true;

               
                Application.DoEvents();
                BaseUIHelper.ShowFormMainMedia();
                BaseUIHelper.IniFormMainMedia();
                try { MainModel.frmlogin.Hide(); LoadingHelper.CloseForm(); }
                catch { }
                this.Activate();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("称重收银页面初始化异常"+ex.Message);
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {

            IsRun = false;
            try
            {
                this.Dispose();

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("释放窗体资源异常" + ex.Message);
            }
        }
        #endregion


        #region 菜单栏功能按钮

        private void btnWindows_Click(object sender, EventArgs e)
        {
            MainModel.ShowTask();
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnNetWeight_Click(object sender, EventArgs e)
        {
            FormZero frmzero = new FormZero();
            frmzero.Location = new Point(pnlCategory.Left + pnlScale.Left + btnNetWeight.Left + btnNetWeight.Width / 2 - frmzero.Width / 2, pnlHead.Height + btnNetWeight.Bottom);
            frmzero.Show();
        }

        private void btnTareWeight_Click(object sender, EventArgs e)
        {

            FormTare frmtare = new FormTare();
            frmtare.Location = new Point(pnlCategory.Left + pnlScale.Left + btnTareWeight.Left + btnTareWeight.Width / 2 - frmtare.Width / 2, pnlHead.Height + btnTareWeight.Bottom);
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


                if (dgvGood.Rows.Count <= 0)
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
                }
                catch (Exception ex) { }

                if (MainModel.CurrentMember != null)
                {
                    ClearMember();
                }

                ClearForm();

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

                        if (!string.IsNullOrEmpty(frmorderhang.CurrentPhone))
                        {
                            string ErrorMsgMember = "";
                            Member member = httputil.GetMember(frmorderhang.CurrentPhone, ref ErrorMsgMember);

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
                        if (frmorderhang.CurrentCart != null && frmorderhang.CurrentCart.products != null)
                        {
                            CurrentCart = frmorderhang.CurrentCart; ;

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
                        RefreshCart(null);

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

                    asf.AutoScaleControlTest(frmtoolmain, 210, 470, Convert.ToInt32(MainModel.wScale * 210), Convert.ToInt32(MainModel.hScale * 470), true);
                    frmtoolmain.DataReceiveHandle += frmToolMain_DataReceiveHandle;
                    frmtoolmain.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width - frmtoolmain.Width - 15, pnlHead.Height + 10);
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
                    this.Invoke(new InvokeHandler(delegate()
                    {
                        tsmReceipt_Click(null, null);
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

                if (tooltype == ToolType.ScaleModel)
                {
                    this.Invoke(new InvokeHandler(delegate()
                    {
                        ChangeScaleModel();
                    }));
                        
                }

                if (tooltype == ToolType.Broken)
                {
                    this.Invoke(new InvokeHandler(delegate()
                 {
                     Broken();
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
                //this.Close();
                //this.Dispose();
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


                if (!ZhuiZhi_Integral_Scale_UncleFruit.HelperUI.ConfirmHelper.Confirm("确认交班","点击确认后，收银机将自动打印交班表单"))
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
                //receiptpara.endtime = MainModel.getStampByDateTime(DateTime.Now);
                receiptpara.shopid = MainModel.CurrentShopInfo.shopid;

                IsEnable = false;
                string ErrorMsg = "";
                Receiptdetail receipt = httputil.Receipt(receiptpara, ref ErrorMsg);

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


                    INIManager.SetIni("System", "POS-Authorization", "", MainModel.IniPath);
                    MainModel.Authorization = "";

                    FrmConfirmReceiptBack frmconfirmreceiptback = new FrmConfirmReceiptBack(receipt);
                    frmconfirmreceiptback.Location = new Point(0, 0);
                    frmconfirmreceiptback.ShowDialog();


                    try { MainModel.frmlogin.Show(); }
                    catch { }
                   // BaseUIHelper.CloseFormMain();
                    BaseUIHelper.CloseFormMain();
                    //this.Close();
                    //this.Dispose();
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
                frmPrinterSetting frmprintset = new frmPrinterSetting();
                asf.AutoScaleControlTest(frmprintset, 1170, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                frmprintset.ShowDialog();

                LoadPnlScale();
                //frmPrinterSettingBack frmsettingback = new frmPrinterSettingBack();
                //frmsettingback.ShowDialog();
            }
            catch (Exception ex)
            {
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
                //ShowLog("暂未开通", false);
                frmScale frmscal = new frmScale();

                asf.AutoScaleControlTest(frmscal, 1178, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                frmscal.Location = new System.Drawing.Point(0, 0);

                frmscal.ShowDialog();
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
                this.Invoke(new InvokeHandler(delegate()
                   {
                       LstAllProduct = CartUtil.LoadAllProduct(false);
                       IniForm();
                   }));
                ShowLoading(false,true);

                //if (frmchangemode.DialogResult == DialogResult.OK)
                //{

                //    INIManager.SetIni("System", "POS-Authorization", "", MainModel.IniPath);
                //    MainModel.Authorization = "";
                //    // this.Hide();
                //    if (MainModel.frmloginoffline != null)
                //    {
                //        try { MainModel.frmloginoffline.Dispose(); }
                //        catch { }
                //    }
                //    MainModel.frmloginoffline = new frmLoginOffLine();
                //    MainModel.frmloginoffline.Show();

                //    this.Dispose();

                //}
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
                   // //this.Close();
                   // ShowLoading(true,false);

                   // //BaseUIHelper.ShowFormMainScale();
                   // this.Close();
                   //// this.Dispose();
                   // FormMainScale frmmainscale = new FormMainScale();
                   // asf.AutoScaleControlTest(frmmainscale, 1180, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                   // frmmainscale.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ShowLog("切换秤模式异常"+ex.Message,true);
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
                MemberCenterHelper.ShowFormBatchSaleCardCreate();
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
                
                ShowLoading(true,false);
                string numbervalue = NumberHelper.ShowFormNumber("输入会员手机号", NumberType.MemberCode);
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
                 ShowLoading(false,true);
            }
            catch (Exception ex)
            {
                ShowLoading(false, true);

                LogManager.WriteLog("会员登录异常" + ex.Message);
            }
           
        }

        private void ClearMember()
        {
            try
            {

                this.Invoke(new InvokeHandler(delegate()
                {
                    tplMember.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100);
                    tplMember.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0);
                    WhetherCredit = false;
                    MainModel.CurrentMember = null;
                    MainModel.CurrentCouponCode = "";
                    btnCoupon.Text = "0张";
                    btnCredit.Visible = false;

                    LstAllProduct.ForEach(r => r.price = null);
                    LstAllProduct.ForEach(r => r.panelbmp = null);

                    if (keyBoard.Visible)
                    {
                        LoadDgvQuery();
                    }
                    else
                    {
                        LoadDgvGood(true, true);
                    }

                    BaseUIHelper.LoadMember();
                }));
            }
            catch (Exception ex)
            {
                ShowLog("清空会员异常",true);
            }
        }
        private void LoadMember(Member member)
        {
            try
            {
                if (this.IsHandleCreated)
                {
                    this.Invoke(new InvokeHandler(delegate()
                    {
                        ShowLoading(true,false);
                        tplMember.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 0);
                        tplMember.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 100);

                        lblMemberPhone.Text = "手机号：" + member.memberheaderresponsevo.mobile;
                        //if (member.memberinformationresponsevo != null)
                        //{
                        //    lblMemberPhone.Text += string.IsNullOrEmpty(member.memberinformationresponsevo.nickname) ? "(" + member.memberinformationresponsevo.wechatnickname + ")" : "(" + member.memberinformationresponsevo.nickname + ")";
                        //}

                        //if (member.barcoderecognitionresponse != null)
                        //{
                        //    lblMemberPhone.Text += "        余额:￥" + member.barcoderecognitionresponse.balance;
                        //}

                        MainModel.CurrentMember = member;
                        lblCredit.Text = "积分" + member.creditaccountrepvo.availablecredit.ToString();

                        if (member.membertenantresponsevo.onbirthday)
                        {
                            picBirthday.Visible = true;

                            lblMemberPhone.Top = picBirthday.Bottom;
                            pbtnExitMember.Top = picBirthday.Bottom;

                            pnlCoupon.Top = lblMemberPhone.Bottom+(pnlMember.Height - lblMemberPhone.Bottom - pnlCoupon.Height) / 2;
                            pnlCredit.Top = pnlCoupon.Top;
                        }
                        else
                        {
                            picBirthday.Visible = false;

                           

                            pnlCoupon.Top = pnlMember.Height - pnlCoupon.Height - 10;
                            pnlCredit.Top = pnlCoupon.Top;

                            lblMemberPhone.Top = (pnlMember.Height - pnlCoupon.Top - lblMemberPhone.Height) / 2;
                            pbtnExitMember.Top = lblMemberPhone.Top;
                        }


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

                        BaseUIHelper.LoadMember();
                        Application.DoEvents();
                        ShowLoading(false , true);

                        LstAllProduct.ForEach(r => r.price = null);
                        LstAllProduct.ForEach(r => r.panelbmp = null);

                        if (keyBoard.Visible)
                        {
                            LoadDgvQuery();
                        }
                        else
                        {
                            LoadDgvGood(true, true);
                        }
                        
                    }));
                }
            }
            catch (Exception ex)
            {
                ShowLog("加载会员异常"+ex.StackTrace,true);
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

                ShowLoading(true,false);
                ClearMember();
                RefreshCart();
                ShowLoading(false,true);
            }
            catch (Exception ex)
            {
                ShowLoading(false, true);

                LogManager.WriteLog("退出会员异常" + ex.Message);
            }
          
        }

        private void cbtnTopUp_ButtonClick(object sender, EventArgs e)
        {

        }

        private void btnCoupon_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }

                if (CurrentCart.availablecoupons.Count <= 0)
                {
                    return;
                }
                BackHelper.ShowFormBackGround();

                frmCoupon frmcoupon = new frmCoupon(CurrentCart, MainModel.CurrentCouponCode);
                asf.AutoScaleControlTest(frmcoupon, 380, 480, 380 * MainModel.wScale, 480 * MainModel.hScale, true);
                frmcoupon.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmcoupon.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmcoupon.Height) / 2);
                frmcoupon.TopMost = true;
                frmcoupon.ShowDialog();
                BackHelper.HideFormBackGround();

                MainModel.CurrentCouponCode = frmcoupon.SelectCouponCode;
                ShowLoading(true,false);
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
                        FormPaySuccess frmresult = new FormPaySuccess(orderresult.orderid);
                        frmresult.DataReceiveHandle += FormCashierResult_DataReceiveHandle;
                        frmresult.ShowDialog();
                        ClearForm();
                        ClearMember();
                    }
                }

                ShowLoading(false,true);
            }
            catch (Exception ex)
            {
                ShowLoading(false, true);
                BackHelper.HideFormBackGround();
                LogManager.WriteLog("选择优惠券异常：" + ex.Message);
            }
           
        }

        /// <summary>
        /// 是否选择积分
        /// </summary>
        private bool WhetherCredit = false;
        private void btnCredit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }

                //购物车为空或者 没有可用积分 不允许选中
                if (CurrentCart == null || CurrentCart.products == null || CurrentCart.products.Count == 0 || MainModel.CurrentMember == null || CurrentCart.pointinfo == null || CurrentCart.pointinfo.availablepoints==0)
                {

                    return;
                }

                ShowLoading(true,false);
                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                {
                    WhetherCredit = !WhetherCredit;
                    if (WhetherCredit)
                    {
                        btnCredit.Image = picNotSelectCredit.Image;
                    }
                    else
                    {
                        btnCredit.Image = picSelectCredit.Image;
                    }   

                    RefreshCart();
                }
                else
                {
                    btnCredit.Image = picNotSelectCredit.Image;
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

        private Image imgSelect;
        private Image imgNotSelect;

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
            try{
            if (txtSearch.Text.Length == 0)
            {
                lblSearchShuiyin.Visible = true;
            }
            else
            {
                lblSearchShuiyin.Visible = false;
            }
           
            if (!IsEnable)
            {
                return;
            }

            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                btnScan.Select();
                //ShowLog("请输入商品名称或商品条码", false);
            }
            else
            {
                ShowLoading(true,false);
                Application.DoEvents();

                CurrentGoodQueryPage = 1;
                UpdateDgvGoodByQuery();

                
                ShowLoading(false, true);
            }
            }catch(Exception ex){
                ShowLoading(false, true);
                ShowLog("查询面板商品异常"+ex.Message,true);
            }

        }


        private void UpdateDgvGoodByQuery()
        {
            try
            {

                ShowLoading(true,false);

                LoadDgvQuery();

                ShowLoading(false,true);

            }
            catch (Exception ex)
            {

                LogManager.WriteLog("ERROR", "加载面板商品异常" + ex.Message);
            }
            finally
            {
                ShowLoading(false,true);
            }
        }

        private void LoadDgvQuery()
        {
            try
            {
                Other.CrearMemory();

                string strquery = txtSearch.Text.ToUpper();
                List<Product> AllQueryPro = LstAllProduct.Where(r => r.AllFirstLetter.Contains(strquery) || r.skucode.Contains(strquery)).ToList();

                if (AllQueryPro == null || AllQueryPro.Count == 0)
                {
                    dgvGoodQuery.Rows.Clear();
                    return;
                }

                int page = CurrentGoodQueryPage;
                int startindex = 0;
                int lastindex = 23;
                int waitingcount = 0;

                bool havanextpage = false;
                bool havepreviousPage = false;
                if (page == 1)
                {
                    havepreviousPage = false;
                    startindex = 0;
                    if (AllQueryPro.Count > 24)
                    {

                        lastindex = 22;
                        havanextpage = true;
                    }
                    else
                    {
                        lastindex = AllQueryPro.Count - 1;
                        havanextpage = false;
                    }
                }
                else
                {
                    havepreviousPage = true;
                    waitingcount = AllQueryPro.Count - ((page - 1) * 22 + 1);  //第一页只有下一页  中间页都是上一页下一页 占用两个
                    startindex = (page - 1) * 22 + 1;

                    if (waitingcount > 23)
                    {
                        lastindex = startindex + 21;
                        havanextpage = true;
                    }
                    else
                    {
                        lastindex = AllQueryPro.Count - 1;
                        havanextpage = false;
                    }
                }

                int loadingcount = lastindex - startindex + 1;


                DateTime starttime = DateTime.Now;
                List<Image> lstshowimg = new List<Image>();
                if (havepreviousPage)
                {
                    lstshowimg.Add(imgPageUpForGood);
                }


                List<Product> lstLaodingPro = AllQueryPro.GetRange(startindex, lastindex - startindex + 1);

                List<Product> lstNotHaveprice = lstLaodingPro.Where(r => r.price == null).ToList();
                //防止转换json  死循环   bmp.tag 是product
                lstNotHaveprice.ForEach(r => r.panelbmp = null);
                //lstNotHaveprice = lstpro.Where(r => r.panelbmp == null).ToList();
                if (lstNotHaveprice != null && lstNotHaveprice.Count > 0)
                {
                    PanelProductPara panelpara = new PanelProductPara();
                    if (MainModel.CurrentMember != null)
                    {
                        panelpara.memberlogin = 1;
                        panelpara.usertoken = MainModel.CurrentMember.memberheaderresponsevo.token;
                    }
                    else
                    {
                        panelpara.memberlogin = 0;
                        panelpara.usertoken = "";
                    }
                    panelpara.shopid = MainModel.CurrentShopInfo.shopid;

                    panelpara.products = lstNotHaveprice;
                    string ErrorMsg = "";
                    int resultcode = -1;

                    Console.WriteLine("Good接口开始时间" + (DateTime.Now - starttime).TotalMilliseconds);

                    List<Product> templstpro = httputil.GetPanelProductPrice(panelpara, ref ErrorMsg, ref resultcode);

                    Console.WriteLine("Good接口结束时间" + (DateTime.Now - starttime).TotalMilliseconds);
                    if (!string.IsNullOrEmpty(ErrorMsg) || templstpro == null)
                    {
                        CheckUserAndMember(resultcode, ErrorMsg);
                        // ShowLog(ErrorMsg, true);
                        return;
                    }
                    else
                    {
                        foreach (Product temppro in templstpro)
                        {
                            lstNotHaveprice.Where(r => r.skucode == temppro.skucode).ToList()[0].price = temppro.price;
                            lstNotHaveprice.Where(r => r.skucode == temppro.skucode).ToList()[0].price = temppro.price;
                            lstNotHaveprice.Where(r => r.skucode == temppro.skucode).ToList()[0].pricetagid = temppro.pricetagid;
                            lstNotHaveprice.Where(r => r.skucode == temppro.skucode).ToList()[0].pricetag = temppro.pricetag;
                            lstNotHaveprice.Where(r => r.skucode == temppro.skucode).ToList()[0].isLoadPanel = true;
                            lstNotHaveprice.Where(r => r.skucode == temppro.skucode).ToList()[0].panelbmp = GetItemImg(lstNotHaveprice.Where(r => r.skucode == temppro.skucode).ToList()[0]);

                        }

                    }
                }



                dgvGoodQuery.Rows.Clear();

                for (int i = 0; i < lstLaodingPro.Count; i++)
                {

                    if (lstLaodingPro[i].panelbmp == null)
                    {
                        lstLaodingPro[i].panelbmp = GetItemImg(lstLaodingPro[i]);
                    }
                    lstshowimg.Add(lstLaodingPro[i].panelbmp);

                }

                if (havanextpage)
                {
                    lstshowimg.Add(imgPageDownForGood);
                }


               
                int emptycount = 6- lstshowimg.Count % 6;


                for (int i = 0; i < emptycount; i++)
                {
                    lstshowimg.Add(ResourcePos.empty);
                }

                int rowcount = lstshowimg.Count / 6;

                for (int i = 0; i < rowcount; i++)
                {
                    dgvGoodQuery.Rows.Add(lstshowimg[i * 6 + 0], lstshowimg[i * 6 + 1], lstshowimg[i * 6 + 2], lstshowimg[i * 6 + 3], lstshowimg[i * 6 + 4], lstshowimg[i * 6 + 5]);
                }

                Console.WriteLine("Good画图时间" + (DateTime.Now - starttime).TotalMilliseconds);
                IsEnable = true;

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ERROR", "加载面板商品异常" + ex.Message);
            }
        }




        /// <summary>
        /// 当前页面购物车 根据firsecategoryid 区分
        /// </summary>
        SortedDictionary<string, Cart> sortCartByFirstCategoryid = new SortedDictionary<string, Cart>();


        public SortType querysorttype = SortType.SaleCount;
        private void btnOrderBySaleCount_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }

                SortType thissorttype = SortType.SaleCount;

                //已选择该排序，不需要再刷新
                if (btnOrderBySaleCount.BackgroundImage == imgSelect)
                {
                    return;
                }
                ShowLoading(true,false);
                if (dgvGood.Visible)
                {
                    sortCartByFirstCategoryid[CurrentFirstCategoryid].sorttype = thissorttype;

                    LoadDgvGood(false, true);
                }
                else
                {
                    querysorttype = thissorttype;
                    UpdateDgvGoodByQuery();
                }

                LoadBtnSortStatus(thissorttype);
                ShowLoading(false ,true);
            }
            catch (Exception ex)
            {
                ShowLoading(false, true);
                LogManager.WriteLog("销量排序异常" + ex.Message);
            }
        }

        private void btnOrderByCreateDate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }
                SortType thissorttype = SortType.CreateDate;
                //已选择该排序，不需要再刷新
                if (btnOrderByCreateDate.BackgroundImage == imgSelect)
                {
                    return;
                }

                ShowLoading(true,false);
                if (dgvGood.Visible)
                {
                    sortCartByFirstCategoryid[CurrentFirstCategoryid].sorttype = thissorttype;
                    LoadDgvGood(false, true);
                }
                else
                {
                    querysorttype = thissorttype;
                    UpdateDgvGoodByQuery();
                }
               
                 
              

                LoadBtnSortStatus(thissorttype);
                ShowLoading(false, true);
            }
            catch (Exception ex)
            {
                ShowLoading(false, true);
                LogManager.WriteLog("上新排序异常" + ex.Message);
            }
        }

        private void btnOrderBySalePrice_Click(object sender, EventArgs e)
        {
            try
            {

                if (!IsEnable)
                {
                    return;
                }

                ShowLoading(true,false);
                SortType thissorttype;
                if (btnOrderBySalePrice.Text == "价格↓")
                {
                    thissorttype = SortType.SalePriceAsc;
                }
                else
                {
                    thissorttype = SortType.SalePriceDesc;
                }

                if (dgvGood.Visible)
                {

                    sortCartByFirstCategoryid[CurrentFirstCategoryid].sorttype = thissorttype;
                    LoadDgvGood(false, false);
                }
                else
                {
                    querysorttype = thissorttype;
                    UpdateDgvGoodByQuery();
                }              
               
                LoadBtnSortStatus(thissorttype);
                ShowLoading(false, true);
            }
            catch (Exception ex)
            {
                ShowLoading(false, true);
                LogManager.WriteLog("价格排序异常" + ex.Message);
            }
        }

        private void LoadBtnSortStatus(SortType sorttype)
        {
            btnOrderBySaleCount.BackgroundImage = imgNotSelect;
            btnOrderBySaleCount.ForeColor = Color.Black;
            btnOrderByCreateDate.BackgroundImage = imgNotSelect;
            btnOrderByCreateDate.ForeColor = Color.Black;
            btnOrderBySalePrice.BackgroundImage = imgNotSelect;
            btnOrderBySalePrice.ForeColor = Color.Black;

            btnOrderBySalePrice.Text = "价格";

            switch (sorttype)
            {
                case SortType.SaleCount: btnOrderBySaleCount.BackgroundImage = imgSelect; btnOrderBySaleCount.ForeColor = Color.DeepSkyBlue; break;
                case SortType.CreateDate: btnOrderByCreateDate.BackgroundImage = imgSelect; btnOrderByCreateDate.ForeColor = Color.DeepSkyBlue; break;
                case SortType.SalePriceAsc: btnOrderBySalePrice.BackgroundImage = imgSelect; btnOrderBySalePrice.ForeColor = Color.DeepSkyBlue; btnOrderBySalePrice.Text = "价格↑"; break;
                case SortType.SalePriceDesc: btnOrderBySalePrice.BackgroundImage = imgSelect; btnOrderBySalePrice.ForeColor = Color.DeepSkyBlue; btnOrderBySalePrice.Text = "价格↓"; break;
                //default: templstprodcut.OrderBy(r => r.salecount); break;
            }
        }
        #endregion

        private SortedDictionary<string, string> sortCategory =new SortedDictionary<string,string>();
        private void IniForm()
        {
            try
            {
                sortCategory.Clear();
                sortCategory.Add("-1","全部");

                SortedDictionary<string, string> tempSort = productbll.GetDiatinctCategory("STATUS =1 and CREATE_URL_IP='" + MainModel.URL + "' and SHOPID='" + MainModel.CurrentShopInfo.shopid + "'  order by FIRSTCATEGORYID");

                foreach (KeyValuePair<string, string> kv in tempSort)
                {
                    sortCategory.Add(kv.Key,kv.Value);
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
                    else
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


        private void LoadDgvCategory()
        {
            try
            {
                int page = CurrentCategoryPage;
                int startindex = 0;
                int lastindex = 13;
                int waitingcount = 0;

                bool havanextpage = false;
                bool havepreviousPage = false;
                if(page==1){
                    havepreviousPage = false;
                    startindex = 0;
                    if (sortCategory.Count > 14)
                    {
                       
                        lastindex = 12;
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
                    waitingcount = sortCategory.Count - ((page - 1) * 12 + 1);  //第一页只有下一页  中间页都是上一页下一页 占用两个
                    startindex = (page - 1) * 12 + 1;

                    if (waitingcount > 13)
                    {
                        lastindex =startindex+ 11;
                        havanextpage = true;
                    }
                    else
                    {
                        lastindex = sortCategory.Count-1;
                        havanextpage = false;
                    }
                }

                int loadingcount = lastindex - startindex + 1;

                List<Image> lstshowimg = new List<Image>();
                if (havepreviousPage)
                {
                    lstshowimg.Add(imgPageUpForCategory);
                }

                int tempcount=0;
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

                int emptyimgcount = 14 - loadingcount;

                for (int i = 0; i < emptyimgcount; i++)
                {
                    lstshowimg.Add(ResourcePos.empty);
                }
                dgvCategory.Rows.Clear();
                for (int i = 0; i < 2; i++)
                {
                    int temp = 7 * i;
                    dgvCategory.Rows.Add(lstshowimg[temp + 0], lstshowimg[temp + 1], lstshowimg[temp + 2], lstshowimg[temp + 3], lstshowimg[temp + 4], lstshowimg[temp + 5], lstshowimg[temp + 6]);
                }

                IsEnable = true;
                if (dgvCategory.Rows.Count > 0 && dgvGood.Rows.Count==0)
                {
                    dgvCategory_CellClick(null, new DataGridViewCellEventArgs(0, 0));
                }


            }
            catch (Exception ex)
            {
                ShowLog("加载分类异常" + ex.StackTrace, true);
            }
        }


        private void dgvCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!IsEnable || keyBoard.Visible)
                {
                    return;
                }
                if (e.RowIndex < 0)
                    return;

                
                ShowLoading(true, false);

                Other.CrearMemory();
                Image selectimg = (Image)dgvCategory.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                //展开
                if (selectimg == imgPageDownForCagegory)
                {
                    CurrentCategoryPage++;
                    LoadDgvCategory();
                    ShowLoading(false, true);
                    return;
                }
                //收起
                if (selectimg == imgPageUpForCategory)
                {
                    CurrentCategoryPage--;
                    LoadDgvCategory();
                    ShowLoading(false, true);
                    return;
                }
                if (selectimg.Tag == null)  //空白单元格（无商品）
                {
                    ShowLoading(false, true);
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
                dgvGood.Rows.Clear();

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
                ShowLoading(false, true);
            }
            catch (Exception ex)
            {
                ShowLoading(false, true);
                ShowLog("选择分类异常"+ex.StackTrace,true);
            }
            finally
            {              
                btnScan.Select();
            }
        }

        bool isnewType = false;
        private void LoadDgvGood(bool isnew, bool isnewType)
        {
            try
            {
                Other.CrearMemory();
                List<Product> AllCategoryPro = sortCartByFirstCategoryid[CurrentFirstCategoryid].products;

                if (AllCategoryPro == null || AllCategoryPro.Count == 0)
                {
                    dgvGood.Rows.Clear();
                    return;
                }

                int page = CurrentGoodPage;
                int startindex = 0;
                int lastindex = 41;
                int waitingcount = 0;

                bool havanextpage = false;
                bool havepreviousPage = false;
                if (page == 1)
                {
                    havepreviousPage = false;
                    startindex = 0;
                    if (AllCategoryPro.Count > 42)
                    {

                        lastindex = 40;
                        havanextpage = true;
                    }
                    else
                    {
                        lastindex = AllCategoryPro.Count - 1;
                        havanextpage = false;
                    }
                }
                else
                {
                    havepreviousPage = true;
                    waitingcount = AllCategoryPro.Count - ((page - 1) * 40 + 1);  //第一页只有下一页  中间页都是上一页下一页 占用两个
                    startindex = (page - 1) * 40+1;

                    if (waitingcount > 41)
                    {
                        lastindex = startindex + 39;
                        havanextpage = true;
                    }
                    else
                    {
                        lastindex = AllCategoryPro.Count - 1;
                        havanextpage = false;
                    }
                }

                int loadingcount = lastindex - startindex + 1;


                DateTime starttime = DateTime.Now;
                List<Image> lstshowimg = new List<Image>();
                if (havepreviousPage)
                {
                    lstshowimg.Add(imgPageUpForGood);
                }


                List<Product> lstLaodingPro = AllCategoryPro.GetRange(startindex ,lastindex-startindex+1);

                List<Product> lstNotHaveprice = lstLaodingPro.Where(r => r.price == null).ToList();
                //防止转换json  死循环   bmp.tag 是product
                lstNotHaveprice.ForEach(r => r.panelbmp = null);
                //lstNotHaveprice = lstpro.Where(r => r.panelbmp == null).ToList();
                if (lstNotHaveprice != null && lstNotHaveprice.Count > 0)
                {
                    PanelProductPara panelpara = new PanelProductPara();
                    if (MainModel.CurrentMember != null)
                    {
                        panelpara.memberlogin = 1;
                        panelpara.usertoken = MainModel.CurrentMember.memberheaderresponsevo.token;
                    }
                    else
                    {
                        panelpara.memberlogin = 0;
                        panelpara.usertoken = "";
                    }
                    panelpara.shopid = MainModel.CurrentShopInfo.shopid;

                    panelpara.products = lstNotHaveprice;
                    string ErrorMsg = "";
                    int resultcode = -1;
                    Console.WriteLine("Good开始结束时间" + (DateTime.Now - starttime).TotalMilliseconds);

                    List<Product> templstpro = httputil.GetPanelProductPrice(panelpara, ref ErrorMsg, ref resultcode);

                    Console.WriteLine("Good接口结束时间" + (DateTime.Now - starttime).TotalMilliseconds);
                    if (!string.IsNullOrEmpty(ErrorMsg) || templstpro == null)
                    {
                        CheckUserAndMember(resultcode, ErrorMsg);
                        // ShowLog(ErrorMsg, true);
                        return;
                    }
                    else
                    {
                        foreach (Product temppro in templstpro)
                        {
                            lstNotHaveprice.Where(r => r.skucode == temppro.skucode).ToList()[0].price = temppro.price;
                            lstNotHaveprice.Where(r => r.skucode == temppro.skucode).ToList()[0].price = temppro.price;
                            lstNotHaveprice.Where(r => r.skucode == temppro.skucode).ToList()[0].pricetagid = temppro.pricetagid;
                            lstNotHaveprice.Where(r => r.skucode == temppro.skucode).ToList()[0].pricetag = temppro.pricetag;
                            lstNotHaveprice.Where(r => r.skucode == temppro.skucode).ToList()[0].isLoadPanel = true;
                            lstNotHaveprice.Where(r => r.skucode == temppro.skucode).ToList()[0].panelbmp = GetItemImg(lstNotHaveprice.Where(r => r.skucode == temppro.skucode).ToList()[0]);

                        }

                    }
                }



                dgvGood.Rows.Clear();
                
                for (int i = 0; i < lstLaodingPro.Count; i++)
                {

                    if (lstLaodingPro[i].panelbmp == null)
                        {
                            lstLaodingPro[i].panelbmp = GetItemImg(lstLaodingPro[i]);
                        }
                    lstshowimg.Add(lstLaodingPro[i].panelbmp);
                    
                }
                
                if (havanextpage)
                {
                    lstshowimg.Add(imgPageDownForGood);
                }


                int emptycount =6- lstshowimg.Count % 6;


                for (int i = 0; i < emptycount; i++)
                {
                    lstshowimg.Add(ResourcePos.empty);
                }

                int rowcount = lstshowimg.Count / 6;

                for (int i = 0; i < rowcount; i++)
                {
                    dgvGood.Rows.Add(lstshowimg[i * 6 + 0], lstshowimg[i * 6 + 1], lstshowimg[i * 6 + 2], lstshowimg[i * 6 + 3], lstshowimg[i * 6 + 4], lstshowimg[i * 6 + 5]);
                }

                Console.WriteLine("Good画图时间" + (DateTime.Now - starttime).TotalMilliseconds);
                    IsEnable = true;
             
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ERROR", "加载面板商品异常" + ex.Message);
            }
           
        }

        private Bitmap GetItemImg(Product pro)
        {
            switch (pro.pricetagid)
            {
                case 1: picGoodPricetag.Visible = true; picGoodPricetag.BackgroundImage = pic会员.Image; break;
                case 2: picGoodPricetag.Visible = true; picGoodPricetag.BackgroundImage = pic折扣.Image; break;
                case 3: picGoodPricetag.Visible = true; picGoodPricetag.BackgroundImage = pic直降.Image; break;
                case 4: picGoodPricetag.Visible = true; picGoodPricetag.BackgroundImage = pic优享.Image; break;
                default: picGoodPricetag.Visible = false; break;
            }           
            
            if (picGoodPricetag.Visible)
            {
                lblGoodName.Text = "       " + pro.skuname;
            }
            else
            {
                lblGoodName.Text = pro.skuname;
            }

            lblPriceDetail.Text = "/" + pro.saleunit;


            if (pro.price != null)
            {
                if (pro.price.saleprice == pro.price.originprice)
                {
                    lblPrice.Text = pro.price.saleprice.ToString("f2");
                    lblMemberPrice.Visible = false;
                }
                else
                {
                    lblPrice.Text = pro.price.saleprice.ToString("f2");
                    lblMemberPrice.Visible = true;

                    //新分页样式 空间不足以显示文案
                    //if (!string.IsNullOrEmpty(pro.price.salepricedesc))
                    //{
                    //    lblPriceDetail.Text += "(" + pro.price.salepricedesc + ")";
                    //}

                    if (pro.price.strikeout == 1)
                    {

                        lblMemberPrice.Font = new System.Drawing.Font("微软雅黑", lblMemberPrice.Font.Size, FontStyle.Strikeout);
                        lblMemberPrice.Text = pro.price.originprice.ToString("f2") + "/" + pro.saleunit;
                    }
                    else
                    {
                        lblMemberPrice.Font = new System.Drawing.Font("微软雅黑", lblMemberPrice.Font.Size, FontStyle.Regular);
                        lblMemberPrice.Text = pro.price.originprice.ToString("f2") + "/" + pro.saleunit;
                    }

                    //if (!string.IsNullOrEmpty(pro.price.originpricedesc))
                    //{
                    //    lblMemberPrice.Text += "(" + pro.price.originpricedesc + ")";
                    //}
                }
            }
            else
            {

            }

            if (pro.price != null && pro.price.saleprice == pro.price.originprice)
            {
                lblPrice.Text = pro.price.saleprice.ToString("f2");
            }
            else
            {
            }

            lblPriceDetail.Left = lblPrice.Left + lblPrice.Width - 3;


            Bitmap b =(Bitmap) MainModel.GetControlImage(pnlGoodNotSelect);
            b.Tag = pro;
            return b;
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
        private void ShowLoading(bool showloading,bool isenable)
        {
            try
            {
                //if (Model.MainModel.NewPhone != "")
                //{
                //    cbtnLoadPhone.ShowText = Model.MainModel.NewPhone;
                //}
                IsEnable = isenable;
                if (this.IsHandleCreated)
                {
                    this.Invoke(new InvokeHandler(delegate()
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

        private void ShowLoadingThread( object obj)
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
                this.Invoke(new InvokeHandler(delegate()
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
                        if (!ZhuiZhi_Integral_Scale_UncleFruit.HelperUI.ConfirmHelper.Confirm("会员登录已过期，请重新登录","",false))
                        {
                            IsEnable = true;
                            return;
                        }

                        cbtnLoadPhone_ButtonClick(null,null);
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
                       
                        ShowLoading(true,false);

                        List<string> LstScanCode = new List<string>();

                        List<string> lstNotLocalCode = new List<string>();
                        while (QueueScanCode.Count > 0)
                        {
                            string tempcode = QueueScanCode.Dequeue();
                            if (!string.IsNullOrEmpty(tempcode))
                            {
                                LstScanCode.Add(tempcode);
                            }
                        }

                        List<scancodememberModel> LstScancodemember = new List<scancodememberModel>();
                        foreach (string scancode in LstScanCode)
                        {
                            scancodememberModel scancodemember = GetLocalPro(scancode);
                            if (scancodemember != null)
                            {
                                LstScancodemember.Add(scancodemember);
                                //LstScanCode.Remove(scancode);
                            }
                            else
                            {
                                lstNotLocalCode.Add(scancode);
                            }
                        }

                        bool ismember = false;
                        foreach (string goodcode in lstNotLocalCode)
                        {
                            //IsScan = false;
                            string ErrorMsg = "";
                            int ResultCode = 0;
                            scancodememberModel scancodemember = httputil.GetSkuInfoMember(goodcode, ref ErrorMsg, ref ResultCode);


                            if (ErrorMsg != "" || scancodemember == null) //商品不存在或异常
                            {
                                CheckUserAndMember(ResultCode, ErrorMsg);
                            }
                            else
                            {
                                if (scancodemember.type == "MEMBER")
                                {
                                    ismember = true;
                                    LoadMember(scancodemember.memberresponsevo);
                                }
                                else
                                {
                                    LstScancodemember.Add(scancodemember);
                                }
                            }
                        }
                        ShowLoading(false,true);

                        if (LstScancodemember.Count > 0)
                        {
                            isCartRefresh = true;
                            FlashSkuCode = LstScancodemember[0].scancodedto.skucode;

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
                            if(!ismember)
                            ShowLog("条码识别错误",true);
                        }
                        Thread.Sleep(100);
                    }
                    //}
                    catch (Exception ex)
                    {
                        ShowLoading(false,true);
                        LogManager.WriteLog("ERROR", "扫描数据处理异常：" + ex.Message);
                    }
                    finally
                    {
                        IsEnable = true;
                        ShowLoading(false,true);
                        //Application.DoEvents();
                    }
                }
                Thread.Sleep(100);
            }
        }

        private scancodememberModel GetLocalPro(string goodcode)
        {
            try
            {
                DBPRODUCT_BEANMODEL dbpro = null;

                bool isINNERBARCODE = false;

                if (goodcode.Length == 18 && ! CartUtil.checkEanCodeIsError(goodcode, 18) && (goodcode.Substring(0, 2) == "25" || goodcode.Substring(0, 2) == "26"))
                {
                    List<DBPRODUCT_BEANMODEL> lstdbpro = productbll.GetModelList(" INNERBARCODE='" + goodcode.Substring(2, 10) + "'" + " and CREATE_URL_IP='" + MainModel.URL + "' ");
                    if (lstdbpro != null && lstdbpro.Count > 0)
                    {
                        isINNERBARCODE = true;
                        dbpro = lstdbpro[0];
                    }
                    else
                    {
                        isINNERBARCODE = false;
                        lstdbpro = productbll.GetModelList(" BARCODE='" + goodcode + "'" + " and CREATE_URL_IP='" + MainModel.URL + "' ");
                        if (lstdbpro != null && lstdbpro.Count > 0)
                        {
                            dbpro = lstdbpro[0];
                        }
                    }
                }
                else if (!CartUtil.checkEanCodeIsError(goodcode, 18) && goodcode.Length > 2 && goodcode.Substring(0, 2).Contains("22"))
                {
                    isINNERBARCODE = false;
                    List<DBPRODUCT_BEANMODEL> lstdbpro = productbll.GetModelList(" BARCODE='" + goodcode.Substring(2,5) + "'" + " and CREATE_URL_IP='" + MainModel.URL + "' " + " and SHOPID='" + MainModel.CurrentShopInfo.shopid + "' ");
                    if (lstdbpro != null && lstdbpro.Count > 0)
                    {
                        dbpro = lstdbpro[0];
                    }
                }
                else
                {

                    isINNERBARCODE = false;
                    List<DBPRODUCT_BEANMODEL> lstdbpro = null;
                    if (!CartUtil.checkEanCodeIsError(goodcode, 13) && goodcode.Length > 2 && (goodcode.Substring(0, 2) == "25" || goodcode.Substring(0, 2) == "26"))
                    {
                        lstdbpro = productbll.GetModelList(" BARCODE='" + goodcode.Substring(2, 5) + "'" + " and CREATE_URL_IP='" + MainModel.URL + "' ");
                    }
                    else
                    {
                        lstdbpro = productbll.GetModelList(" BARCODE='" + goodcode + "'" + " and CREATE_URL_IP='" + MainModel.URL + "' ");
                    }
                    // List<DBPRODUCT_BEANMODEL> lstdbpro = productbll.GetModelList(" BARCODE='" + goodcode + "'" + " and CREATE_URL_IP='" + MainModel.URL + "' ");
                    if (lstdbpro != null && lstdbpro.Count > 0)
                    {
                        dbpro = lstdbpro[0];
                    }
                }

                if (dbpro == null)
                {
                    return null;

                }
                else
                {
                    scancodememberModel scancodemember = new scancodememberModel();
                    scancodemember.scancodedto = new Scancodedto();
                    scancodemember.memberresponsevo = new Member();


                    scancodemember.scancodedto.skucode = dbpro.SKUCODE;
                    //scancodemember.scancodedto.num = (int)dbpro.NUM;
                    //scancodemember.scancodedto.specnum = dbpro.SPECNUM;
                    scancodemember.scancodedto.spectype = (int)dbpro.SPECTYPE;
                    scancodemember.scancodedto.weightflag = dbpro.WEIGHTFLAG == 1 ? true : false;
                    scancodemember.scancodedto.barcode = dbpro.BARCODE;


                    if (scancodemember.scancodedto.weightflag)
                    {
                        if (isINNERBARCODE)
                        {
                            int num = Convert.ToInt32(goodcode.Substring(goodcode.Length - 6, 5));
                            decimal decimalnum = (decimal)num / 1000;

                            scancodemember.scancodedto.specnum = decimalnum;
                            scancodemember.scancodedto.num = 1;
                        }
                        else
                        {
                            scancodemember.scancodedto.num = 1;
                        }

                    }
                    else
                    {
                        if (isINNERBARCODE)
                        {
                            int num = Convert.ToInt32(goodcode.Substring(goodcode.Length - 6, 5));

                            scancodemember.scancodedto.num = num;
                        }
                        else
                        {
                            scancodemember.scancodedto.num = 1;
                        }

                        scancodemember.scancodedto.specnum = 1;
                    }
                    return scancodemember;
                }

            }
            catch (Exception ex)
            {

                LogManager.WriteLog("条码验证异常" + ex.Message);
                return null;
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

                ShowLoading(true, false);

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
                                dgvGood.Rows[i].Cells[j].Value = GetItemImg((Product)lastimg.Tag);
                                break;
                            }
                        }
                    }
                }

                if (selectimg == imgPageDownForGood)
                {
                    CurrentGoodPage++;
                    LoadDgvGood(false, false);
                    ShowLoading(false, true);
                    return;
                }

                if (selectimg == imgPageUpForGood)
                {
                    CurrentGoodPage--;
                    LoadDgvGood(false, false);
                    ShowLoading(false, true);
                    return;
                }


                if (selectimg.Tag == null)  //空白单元格（无商品）
                {
                    ShowLoading(false, true);
                    return;
                }

              

                Product pro = (Product)selectimg.Tag;
                pro.RowNum = 1;
                pnlGoodNotSelect.BackgroundImage = picGoodSelect.Image;

                dgvGood.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = GetItemImg(pro);
                pnlGoodNotSelect.BackgroundImage = picGoodNotSelect.Image;

                if (CurrentCart == null)
                {
                    CurrentCart = new Cart();
                }
                if (CurrentCart.products == null)
                {
                    List<Product> products = new List<Product>();
                    CurrentCart.products = products;
                }


                Console.WriteLine("刷新dgvgood时间"+(DateTime.Now-starttime).TotalMilliseconds);

                LastLstPro = new List<Product>();
                foreach (Product ppro in CurrentCart.products)
                {
                    LastLstPro.Add((Product)MainModel.Clone(ppro));
                }

                SelectProduct = pro;
                LastSkuCode = pro.skucode;
                isCartRefresh = true;
                FlashSkuCode = pro.skucode;
                if (pro.goodstagid == 0) //标品
                {
                   pro.num = 1;
                   pro.specnum = 1;
                   CurrentCart.products.Add(pro);
                   RefreshCart(LastLstPro);

                 

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
                        timerScale.Enabled = false;
                        if (ScaleHelper.ShowFormScale(pro))
                        {
                            CurrentCart.products.Add(pro);

                            RefreshCart(LastLstPro);

                            SelectProduct = null;
                        }

                        timerScale.Enabled = true;
                    }
                   
                }
                Console.WriteLine("刷新购物车时间" + (DateTime.Now - starttime).TotalMilliseconds);

                ShowLoading(false, true);
                Console.WriteLine("刷新isenable时间" + (DateTime.Now - starttime).TotalMilliseconds);

            }
            catch (Exception ex)
            {
                ShowLoading(false, true);
                ShowLog("选择商品异常"+ex.StackTrace,true);
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
                    dgc.Value = GetItemImg((Product)lastimg.Tag);
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("清除商品列表选择项异常" + ex.Message);
            }
        }

        private void addcart(List<scancodememberModel> lstscancodemember)
        {
            try
            {
                foreach (scancodememberModel scancodemember in lstscancodemember)
                {

                    if (scancodemember.scancodedto.weightflag && scancodemember.scancodedto.specnum == 0)
                    {
                        string numbervalue = NumberHelper.ShowFormNumber(scancodemember.scancodedto.skuname, NumberType.ProWeight);
                        if (!string.IsNullOrEmpty(numbervalue))
                        {
                            scancodemember.scancodedto.specnum = Convert.ToDecimal(numbervalue)/ 1000;
                            scancodemember.scancodedto.num = 1;
                        }
                        else
                        {
                            Application.DoEvents();
                            return;
                        } 
                        Application.DoEvents();                  
                    }

                    Product pro = new Product();
                    pro.skucode = scancodemember.scancodedto.skucode;
                    pro.num = scancodemember.scancodedto.num;
                    pro.specnum = scancodemember.scancodedto.specnum;
                    pro.spectype = scancodemember.scancodedto.spectype;
                    pro.goodstagid = scancodemember.scancodedto.weightflag == true ? 1 : 0;

                    pro.barcode = scancodemember.scancodedto.barcode;

                    pro.RowNum = 1;

                    if (CurrentCart == null)
                    {
                        CurrentCart = new Cart();
                    }
                    if (CurrentCart.products == null)
                    {
                        List<Product> products = new List<Product>();
                        products.Add(pro);
                        CurrentCart.products = products;

                        LastLstPro = null;
                    }
                    else
                    {
                        LastLstPro = new List<Product>();
                        foreach (Product ppro in CurrentCart.products)
                        {
                            LastLstPro.Add((Product)MainModel.Clone(ppro));
                        }
                        CurrentCart.products.Add(pro);
                    }
                    FlashSkuCode = pro.skucode;
                }

                string ErrorMsgCart = "";
                int ResultCode = 0;
                bool IsExits = false;
                DateTime starttime = DateTime.Now;

                isCartRefresh = true;
               
                RefreshCart(LastLstPro);

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ERROR", "添加购物车商品异常:" + ex.Message);
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

                    CurrentCart.pointpayoption = WhetherCredit ? 1 : 0;
                    Cart cart = httputil.RefreshCart(CurrentCart, ref ErrorMsgCart, ref ResultCode);

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

                    if (WhetherCredit)
                    {
                        if (MainModel.CurrentMember != null && CurrentCart != null && CurrentCart.pointinfo != null)
                        {
                            lblCredit.Text = "积分" + CurrentCart.pointinfo.totalpoints;

                            btnCredit.Text = "使用" + CurrentCart.pointinfo.availablepoints;
                            btnCredit.Image = picSelectCredit.Image;
                            btnCredit.Visible = true;
                            //btnJFUse.Text = "使用" + CurrentCart.pointinfo.availablepoints + "积分 抵用" + CurrentCart.pointinfo.availablepointsamount + "元";
                            //btnJFUse.Visible = true;
                        }
                    }
                    else
                    {
                        if (MainModel.CurrentMember != null && CurrentCart != null && CurrentCart.pointinfo != null)
                        {
                            lblCredit.Text = "积分" + CurrentCart.pointinfo.totalpoints;

                            btnCredit.Text = "可用" + CurrentCart.pointinfo.availablepoints;
                            btnCredit.Image = picNotSelectCredit.Image;
                            btnCredit.Visible = true;
                        }
                        else
                        {
                            lblCredit.Text = "";
                            btnCredit.Text = "";
                            btnCredit.Image = picNotSelectCredit.Image;
                            btnCredit.Visible = false;
                        }
                    }

                    return true;

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
            finally
            {
            }
        }

        private bool RefreshCart(List<Product> lstpro)
        {

            try
            {

                try
                {
                   Product rownumpro = CurrentCart.products[CurrentCart.products.Count-1];

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


                DateTime starttime = DateTime.Now;
                IsEnable = false;
                string ErrorMsgCart = "";
                int ResultCode = 0;
                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                {

                    CurrentCart.pointpayoption = WhetherCredit ? 1 : 0;
                    //增加或删除商品整单优惠重置
                    CurrentCart.fixpricetotal = 0;

                    Cart cart = httputil.RefreshCart(CurrentCart, ref ErrorMsgCart, ref ResultCode);

                    Console.WriteLine("购物车接口时间" + (DateTime.Now - starttime).TotalMilliseconds);

                    if (ErrorMsgCart != "" || cart == null) //商品不存在或异常
                    {
                        ShowLog(ErrorMsgCart,false);
                        CurrentCart.products = lstpro;
                        CheckUserAndMember(ResultCode, ErrorMsgCart);
                        return false;
                    }
                    else
                    {
                        CurrentCart = cart;
                        //有商品增加或减少   fixpricetotal 置0
                        CurrentCart.fixpricetotal = 0;
                        UploadDgvCart(cart);

                        Console.WriteLine("dgvcart刷新时间" + (DateTime.Now - starttime).TotalMilliseconds);

                        if (WhetherCredit)
                        {
                            if (MainModel.CurrentMember != null && CurrentCart != null && CurrentCart.pointinfo != null)
                            {
                                lblCredit.Text = "积分" + CurrentCart.pointinfo.totalpoints;

                                btnCredit.Text = "使用" + CurrentCart.pointinfo.availablepoints;
                                btnCredit.Image = picSelectCredit.Image;
                                btnCredit.Visible = true;
                                //btnJFUse.Text = "使用" + CurrentCart.pointinfo.availablepoints + "积分 抵用" + CurrentCart.pointinfo.availablepointsamount + "元";
                                //btnJFUse.Visible = true;
                            }
                        }
                        else
                        {
                            if (MainModel.CurrentMember != null && CurrentCart != null && CurrentCart.pointinfo != null)
                            {
                                lblCredit.Text = "积分" + CurrentCart.pointinfo.totalpoints;

                                btnCredit.Text = "可用" + CurrentCart.pointinfo.availablepoints;
                                btnCredit.Image = picNotSelectCredit.Image;
                                btnCredit.Visible = true;
                            }
                            else
                            {
                                lblCredit.Text = "";
                                btnCredit.Text = "";
                                btnCredit.Image = picNotSelectCredit.Image;
                                btnCredit.Visible = false;
                            }
                        }
                        Console.WriteLine("购物车全部时间" + (DateTime.Now - starttime).TotalMilliseconds);

                        return true;
                    }
                }
                else
                {
                    
                    ClearForm();
                    Console.WriteLine("购物车全部时间" + (DateTime.Now - starttime).TotalMilliseconds);

                    return true;
                }



            }
            catch (Exception ex)
            {
                IsEnable = true;

                ShowLog("刷新购物车异常：" + ex.StackTrace, true);

                return false;
            }
            finally
            {
                IsEnable = true;

            }

        }

        private object thislockDgvGood = new object();
        private void UploadDgvCart(Cart cart)
        {
            lock (thislockDgvGood)
            {
                try
                {

                    UploaddgvCartDetail();
                    if (CurrentCart == null || CurrentCart.products == null || CurrentCart.products.Count == 0)
                    {
                        btnModifyPrice.BackgroundImage = Resources.ResourcePos.FixDisenable;
                        btnModifyPrice.ForeColor = Color.White;
                        btnOrderCancle.Visible = false;

                        rbtnPay.WhetherEnable = false;
                    }
                    else
                    {
                        btnModifyPrice.BackgroundImage = Resources.ResourcePos.FixEnable;
                        btnModifyPrice.ForeColor = ColorTranslator.FromHtml("#FF0D62B8");
                        btnOrderCancle.Visible = true;

                        rbtnPay.WhetherEnable = true;
                    }

                    CurrentCartPage = 1;
                    LoadDgvCart();

                    if (CurrentCart.availablecoupons != null && CurrentCart.availablecoupons.Count > 0)
                    {

                        btnCoupon.Visible = true;

                        if (CurrentCart.couponpromoamt > 0)
                        {
                            btnCoupon.Text = "-￥" + CurrentCart.couponpromoamt;
                        }
                        else
                        {
                            MainModel.CurrentCouponCode = "";
                            btnCoupon.Text = CurrentCart.availablecoupons.Count + "张";
                        }
                    }
                    else
                    {
                        MainModel.CurrentCouponCode = "";
                        btnCoupon.Text = "0张";
                    }

                    if (cart.totalpayment == 0 && cart.products != null && cart.products.Count > 0)
                    {
                        rbtnPay.ShowText = "完成";
                        rbtnPay.AllBackColor = Color.FromArgb(255, 70, 21);
                    }
                   

                    Thread threadMember = new Thread(UploadMember);
                    threadMember.IsBackground = true;
                    threadMember.Start();

                    UpdateOrderHang();

                    BaseUIHelper.UpdaForm(CurrentCart);

                    ClearDgvGoodSelect();
                    this.Activate();

                }
                catch (Exception ex)
                {
                    dgvGood.Refresh();
                    LogManager.WriteLog("更新显示列表异常" + ex.Message + ex.StackTrace);
                }
            }
        }

        private void LoadDgvCart()
        {
            try {

                dgvCart.Rows.Clear();
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
                          

                            int startindex = (CurrentCartPage - 1) * 6;

                            int lastindex = Math.Min(CurrentCart.products.Count - 1, startindex+5);

                            List<Product> lstLoadingPro = CurrentCart.products.GetRange(startindex,lastindex-startindex+1);
                            
                            foreach(Product por in lstLoadingPro){
                                List<Bitmap> lstbmp = GetDgvRow(por);
                                if (lstbmp != null && lstbmp.Count == 6)
                                {
                                    dgvCart.Rows.Add( lstbmp[0], lstbmp[1], lstbmp[2], lstbmp[3], lstbmp[4], lstbmp[5] );
                                }
                            }

                        rbtnPageDownForCart.WhetherEnable = CurrentCart.products.Count > CurrentCartPage * 6;

                        CurrentCart.products.Reverse();
                        Application.DoEvents();

                        Thread threadItemExedate = new Thread(ShowDgv);
                        threadItemExedate.IsBackground = true;
                        threadItemExedate.SetApartmentState(ApartmentState.STA);
                        threadItemExedate.Start();

                        dgvCart.ClearSelection();

                    }
                }

               
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("加载购物车列表异常"+ex.Message);
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

                           // lstcartdetail.Add(orderprice.title + ":" + orderprice.amount);
                        }

                        dgvCartDetail.ClearSelection();
                    }


                    if (MainModel.CurrentMember != null)
                    {
                        if (CurrentCart.memberpromo != null && CurrentCart.memberpromo > 0)
                        {
                            lstcartdetail.Add("会员已优惠:￥-" + CurrentCart.memberpromo.ToString("f2"));
                        }
                    }
                    else
                    {
                        if (CurrentCart.memberpromo != null && CurrentCart.memberpromo > 0)
                        {
                            lstcartdetail.Add("会员可优惠:￥-" + CurrentCart.memberpromo.ToString("f2"));
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
                            dgvCartDetail.Rows.Add(lstcartdetail[i*2],lstcartdetail[i*2+1]);
                        }

                        dgvCartDetail.Height = rowscount * dgvCartDetail.RowTemplate.Height+5;

                        dgvCartDetail.Top = lblTotalPay.Top - 10 - dgvCartDetail.Height;

                        rbtnPay.Top = dgvCartDetail.Top;
                        rbtnPay.Height = dgvCartDetail.Height + lblTotalPay.Height + 10;
                    }

                    
                    //dgvCartDetail.Height = dgvCartDetail.RowTemplate.Height * dgvCartDetail.Rows.Count;

                    //pnlPrice.Top = dgvCartDetail.Location.Y + dgvCartDetail.Height;

                    //if (btnMemberPromo.Visible)
                    //{

                    //    //btnMemberPromo.Top = lblPriceStr.Top + lblPriceStr.Height;
                    //    pnlPrice.Height = btnMemberPromo.Location.Y + btnMemberPromo.Height + 10;
                    //}
                    //else
                    //{
                    //    pnlPrice.Height = lblPriceStr.Location.Y + lblPriceStr.Height + 10;
                    //}

                    //pnlOrdreDetail.Height = pnlPrice.Location.Y + pnlPrice.Height + 5;
                    Application.DoEvents();
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


        private object thislockMember = new object();
        private void UploadMember()
        {
            lock (thislockMember)
            {
                try
                {


                    //if (pnlMember.Visible)
                    //{
                    //    int JFTOP = lblJFStr.Top;
                    //    if (lblCoupon.Visible)
                    //    {
                    //        JFTOP = lblCoupon.Top + lblCoupon.Height + 3;
                    //    }
                    //    else
                    //    {
                    //        JFTOP = lblWechartNickName.Top + lblWechartNickName.Height + 3;
                    //    }

                    //    lblJF.Top = JFTOP;
                    //    lblJFStr.Top = JFTOP;
                    //    btnCheckJF.Top = JFTOP - 3;

                    //    btnJFUse.Top = JFTOP + lblJF.Height + 3;

                    //    if (btnJFUse.Visible)
                    //    {
                    //        pnlMember.Height = btnJFUse.Top + btnJFUse.Height + 15;
                    //    }
                    //    else
                    //    {
                    //        pnlMember.Height = lblJF.Top + lblJF.Height + 15;
                    //    }

                    //    pnlOrdreDetail.Top = pnlMember.Top + pnlMember.Height + 20;

                    //}
                    //else
                    //{
                    //    pnlOrdreDetail.Top = pnlWaitingMember.Top + pnlWaitingMember.Height + 20;
                    //}


                }
                catch (Exception ex)
                {
                    LogManager.WriteLog("更新订单价格详情异常" + ex.Message + ex.StackTrace);
                }
            }
        }

        private List<Bitmap> GetDgvRow(Product pro)
        {
            try
            {
                Bitmap bmpbarcode;
                Bitmap bmpPrice;
                Bitmap bmpNum;
                Bitmap bmpAdd;
                Bitmap bmpTotal;
                Bitmap bmpdelete;


                Bitmap add = Resources.ResourcePos.empty;
                lblTitle.Text = pro.title;
                //lblSkuCode.Text = pro.skucode;
                //第一行图片
                switch (pro.pricetagid)
                {
                    case 1: picMember.Visible = true; picMember.BackgroundImage = pic会员.Image; picMember.Location = lblTitle.Location; lblTitle.Text = "          " + lblTitle.Text; break;
                    case 2: picMember.Visible = true; picMember.BackgroundImage = pic折扣.Image; ; picMember.Location = lblTitle.Location; lblTitle.Text = "          " + lblTitle.Text; break;
                    case 3: picMember.Visible = true; picMember.BackgroundImage = pic直降.Image; picMember.Location = lblTitle.Location; lblTitle.Text = "          " + lblTitle.Text; break;
                    case 4: picMember.Visible = true; picMember.BackgroundImage = pic优享.Image; picMember.Location = lblTitle.Location; lblTitle.Text = "          " + lblTitle.Text; break;
                    default: picMember.Visible=false; break;
                }
               
                if (!string.IsNullOrEmpty(pro.price.purchaselimitdesc))
                {
                   // btnPurchaseLimit.Width = 10; //自适应大小只会放大不会缩小
                    lblPurchaseLimit.Visible = true;
                    if (!string.IsNullOrEmpty(pro.price.purchaselimitsubdesc))
                    {
                        lblPurchaseLimit.Text = pro.price.purchaselimitdesc.Split('，')[0] + "?";
                    }
                    else
                    {
                        lblPurchaseLimit.Text = pro.price.purchaselimitdesc.Split('，')[0];
                    }

                    lblTitle.Top = 5;
                    picMember.Location = lblTitle.Location;
                }
                else
                {
                    lblTitle.Top = btnNum.Top;
                    picMember.Location = lblTitle.Location;
                   
                    lblPurchaseLimit.Visible = false;
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

                    
                    lblSinglePrice.Left =Math.Max( (pnlSinglePrice.Width - lblSinglePrice.Width - lblPriceDesc.Width) / 2,0);
                    lblPriceDesc.Left = lblSinglePrice.Left + lblSinglePrice.Width;
                    lblOriginPrice.Left =Math.Max( (pnlSinglePrice.Width - lblOriginPrice.Width) / 2,0);
                }
                bmpPrice = new Bitmap(pnlSinglePrice.Width, pnlSinglePrice.Height);
                bmpPrice.Tag = pro;
                pnlSinglePrice.DrawToBitmap(bmpPrice, new Rectangle(0, 0, pnlSinglePrice.Width, pnlSinglePrice.Height));


                //第三 四列图片
                if (pro.goodstagid == 0)  //0是标品  1是称重
                {
                    btnNum.Text = pro.num.ToString().PadRight(4,' ');

                    //btnNum.FlatAppearance.BorderSize = 1;

                    //btnIncrease.FlatAppearance.BorderSize = 1;

                    btnNum.BackgroundImage = Resources.ResourcePos.border_minus;

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
                    lblTotal.Text ="￥"+ pro.price.total.ToString("f2");

                    lblTotalDesc.Text = "";
                    lblOriginTotal.Text = "";

                    lblTotal.Left = (pnlTotal.Width - lblTotal.Width) / 2;
                }
                else
                {
                    lblTotalDesc.Text = "";
                    lblOriginTotal.Text = "";
                    //total = "￥" + pro.price.total.ToString() + "("+pro.price.salepricedesc+")" + "\r\n" + "￥" + pro.price.origintotal + "("+pro.price.originpricedesc+")";

                    lblTotal.Text = "￥" + pro.price.total.ToString("f2");

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

                    lblTotal.Left = Math.Max( (pnlTotal.Width - lblTotal.Width - lblTotalDesc.Width) / 2,0);
                    lblTotalDesc.Left = lblTotal.Left + lblTotal.Width;
                    lblOriginTotal.Left =Math.Max( (pnlTotal.Width - lblOriginTotal.Width) / 2,0);

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
                //return null;
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

                dgvCart.Rows.Clear();
                dgvCartDetail.Rows.Clear();
                lblTotalPay.Text = "￥" + "0.00";
                rbtnPay.ShowText = "结算";

                btnOrderCancle.Visible = false;
                btnModifyPrice.BackgroundImage = Resources.ResourcePos.FixDisenable;
                btnModifyPrice.ForeColor = Color.White;

                rbtnPay.WhetherEnable = false;

                pnlWaiting.Show();

                UpdateOrderHang();

                btnCredit.Visible = false;
                btnCoupon.Text = "0张";
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
                           // MainModel.frmmainmedia.ShowPayResult(payinfo);
                        }));
                    }
                }));
            }
            catch (Exception ex)
            {

            }
        }
      
        private void LoadBaseInfo()
        {
            try
            {
                btnorderhangimage = new Bitmap(btnOrderHang.Image, 10, 10);
                UpdateOrderHang();

                TopLblGoodName = lblGoodName.Top;
                HeightLblGoodName = lblGoodName.Height;

                lblShopName.Text =MainModel.Titledata+"   "+ MainModel.CurrentShopInfo.shopname;
                btnMenu.Text = MainModel.CurrentUser.nickname + ",你好 ∨";
                btnMenu.Left = Math.Max(pnlHead.Width - btnMenu.Width - 10, btnOrderQuery.Right);

                //picLoading.Size = new Size(55,55);

                pnlLoading.Size = new System.Drawing.Size(60,60);

                keyBoard.Size = new System.Drawing.Size(dgvGood.Width,dgvGood.RowTemplate.Height*3);
                INIManager.SetIni("System", "MainType", "Main", MainModel.IniPath); 

                #region 排序选择
                btnSortNortSelect.RoundRadius = btnSortNortSelect.Height;
                btnSortSelect.RoundRadius = btnSortSelect.Height;

                imgSelect = MainModel.GetControlImage(btnSortSelect);
                imgNotSelect = MainModel.GetControlImage(btnSortNortSelect);
                btnOrderBySaleCount.BackgroundImage = imgSelect;

                rbtnPreviousPageForCategoty.Size = new System.Drawing.Size(rbtnPreviousPageForCategoty.Width + 1, rbtnPreviousPageForCategoty.Height + 1);
                rbtnNextPageForCategory.Size = new System.Drawing.Size(rbtnNextPageForCategory.Width + 1, rbtnNextPageForCategory.Height + 1);
                rbtnPageUpForGood.Size = new System.Drawing.Size(rbtnPageUpForGood.Width + 1, rbtnPageUpForGood.Height + 1);
                rbtnPageDownForGood.Size = new System.Drawing.Size(rbtnPageDownForGood.Width + 1, rbtnPageDownForGood.Height + 1);

                imgPageUpForCategory = MainModel.GetControlImage(rbtnPreviousPageForCategoty);
                imgPageDownForCagegory = MainModel.GetControlImage(rbtnNextPageForCategory);

                imgPageUpForGood = MainModel.GetControlImage(rbtnPageUpForGood);
                imgPageDownForGood = MainModel.GetControlImage(rbtnPageDownForGood);
                #endregion

                picMember.Location = lblTitle.Location;
                picMember.Size = new System.Drawing.Size(lblSearchShuiyin.Height*2,lblSearchShuiyin.Height);
                #region  自动加购 打码设置
                string WhetherAutoCart = INIManager.GetIni("CashierSet", "WhetherAutoCart", MainModel.IniPath);
                string WhetherPrint = INIManager.GetIni("CashierSet", "WhetherPrint", MainModel.IniPath);
                string WhetherAutoPrint = INIManager.GetIni("CashierSet", "WhetherAutoPrint", MainModel.IniPath);


                if (WhetherAutoCart == "1")
                {
                    MainModel.WhetherAutoCart = true;
                }
                else
                {
                    MainModel.WhetherAutoCart = false;                 
                }


                if (WhetherPrint == "1")
                {
                    MainModel.WhetherPrint = true;
                }
                else
                {
                    MainModel.WhetherPrint = false;                  
                }

                if (WhetherAutoPrint == "1")
                {
                    MainModel.WhetherAutoPrint = true;                  
                }
                else
                {
                    MainModel.WhetherAutoPrint = false;
                }
                #endregion

                //BackHelper.IniFormBackGround();
                //ConfirmHelper.IniFormConfirm();
                //NumberHelper.IniFormNumber();
                //ToastHelper.IniFormToast();
                //扫描数据处理线程

                if (string.IsNullOrEmpty(MainModel.TvShowPage1))
                {
                    Thread threadTV = new Thread(ServerDataUtil.LoadTVSkus);
                    threadTV.IsBackground = true;
                    threadTV.Start();
                }
             

            }
            catch (Exception ex)
            {
                ShowLog("加载基础信息异常",true);
            }
        }

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

                if (e.ColumnIndex == 0)
                {
                   // PurchaseLimitHelper.ShowPucehaseLimit();
                    if (!string.IsNullOrEmpty(pro.price.purchaselimitsubdesc))
                    {
                        PurchaseLimitHelper.ShowPucehaseLimit();
                       // ConfirmHelper.Confirm("温馨提示", "知道了"); 
                    }
                    return;
                }

                LastLstPro = new List<Product>();
                foreach (Product ppro in CurrentCart.products)
                {
                    LastLstPro.Add((Product)MainModel.Clone(ppro));
                }

                //增加标品
                if (e.ColumnIndex == 3 && pro.goodstagid == 0)
                {
                    for (int i = 0; i < CurrentCart.products.Count; i++)
                    {
                        if (CurrentCart.products[i].barcode == pro.barcode)
                        {
                            CurrentCart.products[i].num += 1;
                            break;
                        }
                    }

                    RefreshCart(LastLstPro);
                }
                //减少标品

                if (e.ColumnIndex == 2 && pro.goodstagid == 0)
                {
                    for (int i = 0; i < CurrentCart.products.Count; i++)
                    {
                        if (CurrentCart.products[i].barcode == pro.barcode)
                        {

                            if (CurrentCart.products[i].num == 1)
                            {
                                if (ZhuiZhi_Integral_Scale_UncleFruit.HelperUI.ConfirmHelper.Confirm("确认删除？",pro.title+pro.skucode))
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
                    RefreshCart(LastLstPro);
                }

                if (e.ColumnIndex == 5)
                {
                    if (!ZhuiZhi_Integral_Scale_UncleFruit.HelperUI.ConfirmHelper.Confirm("确认删除？", pro.title + pro.skucode))
                    {
                        return;
                    }


                    foreach (Product delpro in CurrentCart.products)
                    {
                        if (delpro.skucode == pro.skucode && delpro.specnum == pro.specnum)
                        {
                            ReceiptUtil.EditCancelSingle(delpro.num, delpro.price.origintotal);
                            CurrentCart.products.Remove(delpro);
                            break;
                        }
                    }
                    RefreshCart(LastLstPro);
                }

                dgvCart.ClearSelection();
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

        private void rbtnPay_ButtonClick(object sender, EventArgs e)
        {
            try
            {

                if (!IsEnable || !rbtnPay.WhetherEnable)
                {
                    return;
                }

                if (rbtnPay.ShowText == "完成")
                {
                    PayOK(); 
                }
                else
                {
                    int resultcode = PayHelper.ShowFormPay(CurrentCart);
                    if (resultcode == 1)
                    {

                        ClearMember();
                        ClearForm();
                        
                    }
                    else if (resultcode == 0)
                    {
                        RefreshCart();
                    }
                    else
                    {
                        CheckUserAndMember(resultcode, "");
                    }
                }
               
            }
            catch (Exception ex)
            {
                ShowLog("结算异常"+ex.Message,true);
            }
        }

        private void PayOK()
        {
            try
            {
                ShowLoading(true,false);
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
                    FormPaySuccess frmresult = new FormPaySuccess(orderresult.orderid);
                    frmresult.DataReceiveHandle += FormCashierResult_DataReceiveHandle;
                    frmresult.ShowDialog();
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
                ShowLoading(false,true);
            }
        }


        private void btnModifyPrice_Click(object sender, EventArgs e)
        {
            try
            {
                ShowLoading(true, false);
                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0 && CurrentCart.totalpayment > 0)
                {
                    decimal fixpricetaotal = ModifyPriceHelper.ShowForm(CurrentCart.totalpaymentbeforefix);
                    if (fixpricetaotal>0)
                    {
                        CurrentCart.fixpricetotal = fixpricetaotal;
                        RefreshCart();
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
        /// <summary>
        /// 定时获取电子秤数据 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerScale_Tick(object sender, EventArgs e)
        {
            try
            {
                timerScale.Enabled = false;
                CurrentScaleResult = ScaleGlobalHelper.GetWeight();

                if (CurrentScaleResult.WhetherSuccess)
                {
                    btnNetWeight.Text = CurrentScaleResult.NetWeight + "";
                    btnTareWeight.Text=CurrentScaleResult.TareWeight+"";

                    if (MainModel.WhetherAutoCart && CurrentScaleResult.WhetherStable && CurrentScaleResult.NetWeight>0 && SelectProduct!=null && SelectProduct.goodstagid!=0)
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
                                CurrentCart.products.Add(SelectProduct);

                                if (MainModel.WhetherPrint)
                                {
                                    
                                    LabelPrintHelper.LabelPrint(SelectProduct);
                                }

                                RefreshCart(LastLstPro);
                                SelectProduct = null;
                    }
                }
                else
                {
                    LogManager.WriteLog("SCALE","主界面获取电子秤重量信息错误" + CurrentScaleResult.Message);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("SCALE", "获取电子秤重量信息异常" + ex.Message);
            }
            finally
            {
                timerScale.Enabled = true;
            }
        }


        private void LoadPnlScale()
        {
            try
            {

                string ScaleName = INIManager.GetIni("Scale", "ScaleName", MainModel.IniPath);

                if (ScaleName == ScaleType.中科英泰.ToString() || ScaleName == ScaleType.托利多.ToString())
                {
                    pnlScale.Width = lblTareWeightStr.Left * 2; 
                    pnlScale.Left = pnlCategory.Width - pnlScale.Width - 10;
                }
                else if (ScaleName == ScaleType.爱宝.ToString() || ScaleName==ScaleType.易捷通.ToString())
                {
                    pnlScale.Width = lblTareWeightStr.Left; 
                    pnlScale.Left = pnlCategory.Width - pnlScale.Width - 10;
                }
               
            }
            catch { }
        }
        #endregion

        private void btnScan_Click(object sender, EventArgs e)
        {

            //List<string> lst = new List<string>();

            //lst.Add("4206842040000");
            //lst.Add("4510002");
            //string errormsg = "";
            //httputil.GetSkuMovePrice(MainModel.CurrentShopInfo.shopid,lst,ref errormsg);

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

        private void FormMain_Activated(object sender, EventArgs e)
        {
            try
            {
                if (this.WindowState != FormWindowState.Minimized)
                {
                    MainModel.HideTask();
                }
                //MainModel.HideTask();
            }
            catch { }
        }

        private void btnKboard_Click(object sender, EventArgs e)
        {
            keyBoard.Visible = !keyBoard.Visible;

            dgvGoodQuery.Visible = keyBoard.Visible;

            if (keyBoard.Visible)
            {

                keyBoard.Size = new System.Drawing.Size(dgvGood.Width, dgvGood.RowTemplate.Height * 3);
                txtSearch.Focus();

                LoadDgvQuery();
            }
            else
            {
                txtSearch.Clear();
            }

            UpdateQueryStatus();
        }


        private void UpdateQueryStatus()
        {
            if (keyBoard.Visible)
            {
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

                KeyValuePair<string, string> kv = new KeyValuePair<string, string>("-1", "全部");

                btnSelect.Text = kv.Value;
                Image img = MainModel.GetControlImage(btnSelect);
                img.Tag = kv;

                dgvCategory.Rows[0].Cells[0].Value = img;

            }
            else
            {
                LoadDgvCategory();
            }

          
        }

        string keyInput = "";
        private void keyBoardNew1_Press(object sender, KeyBoardNew.KeyboardArgs e)
        {
            TextBox focusing = txtSearch;
            keyInput = e.KeyCode;

            int startDel = 0;

            //退格
            if (keyInput ==keyBoard.KeyDelete )
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
                dgvGoodQuery.Visible = false;
                txtSearch.Clear();

                UpdateQueryStatus();
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

        private void dgvGoodQuery_CellClick(object sender, DataGridViewCellEventArgs e)
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

                ShowLoading(true, false);

                Image selectimg = (Image)dgvGoodQuery.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;




                if (LastSkuCode != "")
                {
                    //遍历单元格清空之前的选中状态
                    for (int i = 0; i < this.dgvGoodQuery.Rows.Count; i++)
                    {
                        for (int j = 0; j < this.dgvGoodQuery.Columns.Count; j++)
                        {
                            Image lastimg = (Image)dgvGoodQuery.Rows[i].Cells[j].Value;

                            if (lastimg.Tag != null && ((Product)lastimg.Tag).skucode == LastSkuCode)
                            {
                                dgvGoodQuery.Rows[i].Cells[j].Value = GetItemImg((Product)lastimg.Tag);
                                break;
                            }
                        }
                    }
                }

                if (selectimg == imgPageDownForGood)
                {
                    CurrentGoodQueryPage++;
                    UpdateDgvGoodByQuery();
                    ShowLoading(false, true);
                    return;
                }

                if (selectimg == imgPageUpForGood)
                {
                    CurrentGoodQueryPage--;
                    UpdateDgvGoodByQuery();
                    ShowLoading(false, true);
                    return;
                }


                if (selectimg.Tag == null)  //空白单元格（无商品）
                {
                    ShowLoading(false, true);
                    return;
                }



                Product pro = (Product)selectimg.Tag;
                pro.RowNum = 1;
                pnlGoodNotSelect.BackgroundImage = picGoodSelect.Image;

                dgvGoodQuery.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = GetItemImg(pro);
                pnlGoodNotSelect.BackgroundImage = picGoodNotSelect.Image;

                if (CurrentCart == null)
                {
                    CurrentCart = new Cart();
                }
                if (CurrentCart.products == null)
                {
                    List<Product> products = new List<Product>();
                    CurrentCart.products = products;
                }


                Console.WriteLine("刷新dgvgoodquery时间" + (DateTime.Now - starttime).TotalMilliseconds);

                LastLstPro = new List<Product>();
                foreach (Product ppro in CurrentCart.products)
                {
                    LastLstPro.Add((Product)MainModel.Clone(ppro));
                }

                SelectProduct = pro;
                LastSkuCode = pro.skucode;
                isCartRefresh = true;
                FlashSkuCode = pro.skucode;
                if (pro.goodstagid == 0) //标品
                {
                    pro.num = 1;
                    pro.specnum = 1;
                    CurrentCart.products.Add(pro);
                    RefreshCart(LastLstPro);



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
                        timerScale.Enabled = false;
                        if (ScaleHelper.ShowFormScale(pro))
                        {
                            CurrentCart.products.Add(pro);

                            RefreshCart(LastLstPro);

                            SelectProduct = null;
                        }

                        timerScale.Enabled = true;
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



    }
}
