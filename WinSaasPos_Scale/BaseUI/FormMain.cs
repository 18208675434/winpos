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
using WinSaasPOS_Scale.BaseUI;
using WinSaasPOS_Scale.BrokenUI;
using WinSaasPOS_Scale.Common;
using WinSaasPOS_Scale.HelperUI;
using WinSaasPOS_Scale.Model;
using WinSaasPOS_Scale.MyControl;
using WinSaasPOS_Scale.PayUI;
using WinSaasPOS_Scale.Resources;
using WinSaasPOS_Scale.ScaleFactory;
using WinSaasPOS_Scale.ScaleUI;

namespace WinSaasPOS_Scale
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

        //第三方支付页面
        FormPayByOnLine frmonlinepayresult = null;

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
        //启动全量商品同步线程
        Thread threadLoadAllProduct;

        //启动电视屏服务
        Thread threadServerStart;

        //更新离线数据
        Thread threadUploadOffLineDate;

        private bool IsRun = true;
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
                lblTime.Text = MainModel.Titledata;
                LoadBaseInfo();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("称重收银页面加载异常" + ex.Message);

            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            try
            {
                Application.DoEvents();


                ShowLoading(true, false);

                //扫描数据处理线程
                threadScanCode = new Thread(ScanCodeThread);
                threadScanCode.IsBackground = true;
                threadScanCode.Start();

                //刷新焦点线程  防止客屏播放视频抢走焦点
                threadCheckActivate = new Thread(CheckActivate);
                threadCheckActivate.IsBackground = true;
                threadCheckActivate.Start();


                LstAllProduct = CartUtil.LoadAllProduct(false);

                if (LstAllProduct == null || LstAllProduct.Count == 0)
                {

                    DataUtil.LoadAllProduct();
                    LstAllProduct = CartUtil.LoadAllProduct(false);
                }
                else
                {
                    //启动全量商品同步线程
                    threadLoadAllProduct = new Thread(DataUtil.LoadAllProduct);
                    threadLoadAllProduct.IsBackground = true;
                    threadLoadAllProduct.Start();
                }
                IniForm();
                ShowLoading(false, true);
                timerScale.Enabled = true;
                Application.DoEvents();
                BaseUIHelper.ShowFormMainMedia();
                BaseUIHelper.IniFormMainMedia();
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
            MainModel.ShowWindows();
        }

        private void btnNetWeight_Click(object sender, EventArgs e)
        {
            FormZero frmzero = new FormZero();
            frmzero.Location = new Point(pnlPanelGood.Left + pnlScale.Left + btnNetWeight.Left+btnNetWeight.Width/2-frmzero.Width/2, pnlHead.Height + btnNetWeight.Bottom);
            frmzero.Show();
        }

        private void btnTareWeight_Click(object sender, EventArgs e)
        {

            FormTare frmtare = new FormTare();
            frmtare.Location = new Point(pnlPanelGood.Left + pnlScale.Left + btnTareWeight.Left + btnTareWeight.Width / 2 - frmtare.Width / 2, pnlHead.Height + btnTareWeight.Bottom);
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

                if (!ConfirmHelper.Confirm("是否确认取消订单？"))
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

                        if (!WinSaasPOS_Scale.HelperUI.ConfirmHelper.Confirm("确认挂单？"))
                        {
                            return;
                        }

                        SerializeOrder(CurrentCart);

                        if (MainModel.CurrentMember != null)
                        {
                            ClearMember();
                        }
                        MainModel.ShowLog("已挂单", false);

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
                                MainModel.ShowLog(ErrorMsgMember, false);
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
                MainModel.ShowLog("挂单异常", true);
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

                    asf.AutoScaleControlTest(frmtoolmain, 178, 470, Convert.ToInt32(MainModel.wScale * 178), Convert.ToInt32(MainModel.hScale * 470), true);
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
                MainModel.ShowLog("菜单窗体显示异常" + ex.Message, true);
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



                if (!WinSaasPOS_Scale.HelperUI.ConfirmHelper.Confirm("是否确认退出系统？"))
                {
                    return;
                }

                INIManager.SetIni("System", "POS-Authorization", "", MainModel.IniPath);
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


                if (!WinSaasPOS_Scale.HelperUI.ConfirmHelper.Confirm("确认交班","点击确认后，收银机将自动打印交班表单"))
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
                string ErrorMsg = "";
                Receiptdetail receipt = httputil.Receipt(receiptpara, ref ErrorMsg);

                IsEnable = true;
                if (ErrorMsg != "" || receipt == null) //商品不存在或异常
                {
                    MainModel.ShowLog(ErrorMsg, false);
                }
                else
                {
                    string ErrorMsgReceipt = "";
                    bool receiptresult = PrintUtil.ReceiptPrint(receipt, ref ErrorMsgReceipt);

                    if (receiptresult)
                    { }
                    else
                    {
                        MainModel.ShowLog(ErrorMsgReceipt, true);
                    }
                    ReceiptUtil.ClearReceipt();


                    INIManager.SetIni("System", "POS-Authorization", "", MainModel.IniPath);
                    MainModel.Authorization = "";

                    FrmConfirmReceiptBack frmconfirmreceiptback = new FrmConfirmReceiptBack(receipt);
                    frmconfirmreceiptback.Location = new Point(0, 0);
                    frmconfirmreceiptback.ShowDialog();

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
                MainModel.ShowLog("暂未开通", false);
                //frmScale frmscal = new frmScale();

                //asf.AutoScaleControlTest(frmscal, 1178, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                //frmscal.Location = new System.Drawing.Point(0, 0);

                //frmscal.ShowDialog();
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
                MainModel.ShowLog("切换秤模式异常"+ex.Message,true);
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
                MainModel.ShowLog("切换秤模式异常" + ex.Message, true);
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
                string numbervalue = NumberHelper.ShowFormNumber("请输入会员手机号", NumberType.MemberCode);
                 if (!string.IsNullOrEmpty(numbervalue))
                 {
                     string ErrorMsgMember = "";
                     Member member = httputil.GetMember(numbervalue, ref ErrorMsgMember);

                     if (ErrorMsgMember != "" || member == null) //会员不存在
                     {
                         MainModel.ShowLog(ErrorMsgMember, false);

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

                    btnCoupon.Visible = false;
                    btnCredit.Visible = false;

                    BaseUIHelper.LoadMember();
                }));
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("清空会员异常",true);
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
                        if (member.memberinformationresponsevo != null)
                        {
                            lblMemberPhone.Text += string.IsNullOrEmpty(member.memberinformationresponsevo.nickname) ? "(" + member.memberinformationresponsevo.wechatnickname + ")" : "(" + member.memberinformationresponsevo.nickname + ")";
                        }

                        if (member.barcoderecognitionresponse != null)
                        {
                            lblMemberPhone.Text += "        余额:￥" + member.barcoderecognitionresponse.balance;
                        }

                        MainModel.CurrentMember = member;
                        lblCredit.Text = "积分" + member.creditaccountrepvo.availablecredit.ToString();
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
                        // MainModel.frmmainmedia.LoadMember();
                    }));
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载会员异常"+ex.StackTrace,true);
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

                if (!WinSaasPOS_Scale.HelperUI.ConfirmHelper.Confirm("是否确认退出会员？"))
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
                        MainModel.ShowLog("需要继续支付", true);
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
                MainModel.ShowLog("积分处理异常" + ex.Message, true);
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
        /// 商品分类 收起
        /// </summary>
        private Image imgPackUp;
        /// <summary>
        /// 商品分类展开
        /// </summary>
        private Image imgPackDown;


        #region  查询和排序
        private void lblSearchShuiyin_Click(object sender, EventArgs e)
        {
            txtSearch.Focus();

          
            //KBoard.Show();
            
        }

        private void pbtnClearSearch_Click(object sender, EventArgs e)
        {
            try
            {

                if (KBoard.Visible)
                {
                    KBoard.Hide();
                }

                if (!IsEnable || txtSearch.Text == "")
                {
                    return;
                }

             

                txtSearch.Text = "";
                dgvGood.Rows.Clear();
                UpdateDgvGood(false, false);
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("清空查询异常" + ex.StackTrace, true);
            }
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
                dgvGood.Visible = true;
                dgvGoodQuery.Visible = false;
                btnScan.Select();
                //MainModel.ShowLog("请输入商品名称或商品条码", false);
            }
            else
            {
                ShowLoading(true,false);
                dgvGood.Visible = false;
                dgvGoodQuery.Visible = true;
                UpdateDgvGoodByQuery();

                if (dgvGoodQuery.Rows.Count == 0)
                {
                    MainModel.ShowLog("未查到商品", false);
                }
                ShowLoading(false, true);
            }
            }catch(Exception ex){
                ShowLoading(false, true);
                MainModel.ShowLog("查询面板商品异常"+ex.Message,true);
            }

        }
        private void txtSearch_Enter(object sender, EventArgs e)
        {
            KBoard.Show();
        }


        private void UpdateDgvGoodByQuery()
        {
            try
            {

                ShowLoading(true,false);

                dgvGoodQuery.Rows.Clear();


                LoadBtnSortStatus(querysorttype);
                string strquery = txtSearch.Text.ToUpper();
                List<Product> templstprodcut = LstAllProduct.Where(r => r.AllFirstLetter.Contains(strquery) || r.skucode.Contains(strquery)).ToList();

                //查询最多查询20个
                int newcount = Math.Min(templstprodcut.Count, 18);
                templstprodcut = templstprodcut.GetRange(0, newcount);

                IsEnable = true;
                LoadDgvQuery(templstprodcut);

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

        private void LoadDgvQuery(List<Product> lstpro)
        {
            try
            {
                IsEnable = false;

                if (!MainModel.IsOffLine)
                {
                    List<Product> lstNotHaveprice = lstpro.Where(r => r.price == null).ToList();
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
                        List<Product> templstpro = httputil.GetPanelProductPrice(panelpara, ref ErrorMsg, ref resultcode);
                        if (!string.IsNullOrEmpty(ErrorMsg) || templstpro == null)
                        {
                            CheckUserAndMember(resultcode, ErrorMsg);
                            // MainModel.ShowLog(ErrorMsg, true);
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

                    switch (querysorttype)
                    {
                        case SortType.SaleCount: lstpro = lstpro.OrderByDescending(r => r.salecount).ToList(); break;
                        case SortType.CreateDate: lstpro = lstpro.OrderByDescending(r => r.createdat).ToList(); break;
                        case SortType.SalePriceAsc: lstpro = lstpro.OrderBy(r => r.price.saleprice).ToList(); break;
                        case SortType.SalePriceDesc: lstpro = lstpro.OrderByDescending(r => r.price.saleprice).ToList(); break;
                        default: lstpro = lstpro.OrderBy(r => r.salecount).ToList(); break;
                    }
                }
                dgvGoodQuery.Rows.Clear();

                int count = lstpro.Count;
                List<Bitmap> lstbmp = new List<Bitmap>();
                for (int i = 0; i < count; i++)
                {
                    if (lstpro[i].panelbmp == null)
                    {
                        lstpro[i].panelbmp = GetItemImg(lstpro[i]);
                    }
                    lstbmp.Add(lstpro[i].panelbmp);
                    if (lstbmp.Count >= 3 || i >= count - 1)
                    {
                        int addcount = 3 - lstbmp.Count;
                        for (int j = 0; j < addcount; j++)
                        {
                            lstbmp.Add(ResourcePos.empty);
                        }
                        dgvGoodQuery.Rows.Add(lstbmp[0], lstbmp[1], lstbmp[2]);
                        lstbmp = new List<Bitmap>();
                    }
                }

            }
            catch (Exception ex)
            {
                ShowLoading(false,true);
            }
            finally
            {
                IsEnable = true;
                ShowLoading(false,true);
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

                    UpdateDgvGood(false, true);
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
                    UpdateDgvGood(false, true);
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
                    UpdateDgvGood(false, false);
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

        private SortedDictionary<string, string> sortCategory;
        private void IniForm()
        {
            try
            {
                sortCategory = productbll.GetDiatinctCategory("STATUS =1 and CREATE_URL_IP='" + MainModel.URL + "' and SHOPID='" + MainModel.CurrentShopInfo.shopid + "' and SKUTYPE in (1,4) order by FIRSTCATEGORYID");
                sortCartByFirstCategoryid.Clear();

                foreach (KeyValuePair<string, string> kv in sortCategory)
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
                LoadPnlCategory();
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("初始化商品列表异常" + ex.StackTrace, true);
            }
        }


        private void LoadPnlCategory()
        {
            try
            {

                int whitecount = 7 - sortCategory.Count % 7;
                int rowscount = (int)Math.Ceiling((decimal)sortCategory.Count / 7);

                string firstCategoryID = "";
                if (sortCategory.Count > 14)
                {

                     List<Image> lstimg = new List<Image>();

                     foreach (KeyValuePair<string, string> kv in sortCategory)
                     {
                         if (firstCategoryID == "")
                         {
                             firstCategoryID = kv.Key;
                         }
                         btnNotSelect.Text = kv.Value;
                         Image img = MainModel.GetControlImage(btnNotSelect);
                         img.Tag = kv;

                         lstimg.Add(img);
                         //超过14个 只加载13个 最后一个补充展开按钮
                         if (lstimg.Count >= 13)
                         {
                             break;
                         }
                     }
                     lstimg.Add(imgPackDown);

                     dgvCategory.Rows.Clear();
                     for (int i = 0; i < 2; i++)
                     {
                         int temp = 7 * i;
                         dgvCategory.Rows.Add(lstimg[temp + 0], lstimg[temp + 1], lstimg[temp + 2], lstimg[temp + 3], lstimg[temp + 4], lstimg[temp + 5], lstimg[temp + 6]);
                     }

                     IsEnable = true;                
                }
                else
                {
                   
                    List<Image> lstimg = new List<Image>();
                    foreach (KeyValuePair<string, string> kv in sortCategory)
                    {
                        if (firstCategoryID == "")
                        {
                            firstCategoryID = kv.Key;
                        }
                        btnNotSelect.Text = kv.Value;
                        Image img = MainModel.GetControlImage(btnNotSelect);
                        img.Tag = kv;

                        lstimg.Add(img);
                    }

                    for (int i = 0; i < whitecount; i++)
                    {
                        lstimg.Add(ResourcePos.empty);
                    }
                    dgvCategory.Rows.Clear();
                    for (int i = 0; i < rowscount; i++)
                    {
                        int temp = 7 * i;
                        dgvCategory.Rows.Add(lstimg[temp + 0], lstimg[temp + 1], lstimg[temp + 2], lstimg[temp + 3], lstimg[temp + 4], lstimg[temp + 5], lstimg[temp + 6]);
                    }                  
                }

                //第一次加载只有一行和两行 不会存在其他情况
                if (rowscount == 1)
                {
                    dgvCategory.Height =  dgvCategory.RowTemplate.Height+10;
                    tlpGood.RowStyles[0] = new RowStyle(SizeType.Absolute, dgvCategory.Height + dgvCategory.Top);
                    dgvGood.Height = pnlDgvGood.Height;
                }
                else
                {
                    dgvCategory.Height = dgvCategory.RowTemplate.Height*2 + 10;
                    tlpGood.RowStyles[0] = new RowStyle(SizeType.Absolute, dgvCategory.Height + dgvCategory.Top);
                    dgvGood.Height = pnlDgvGood.Height;
                }               


                IsEnable = true;
                if (dgvCategory.Rows.Count > 0)
                {
                    dgvCategory_CellClick(null, new DataGridViewCellEventArgs(0, 0));
                }
              

            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载分类异常"+ex.StackTrace,true);
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

                //IsEnable = false;
                if (e.RowIndex < 0)
                    return;

                if (KBoard.Visible)
                {
                    KBoard.Hide();
                }

                Image selectimg = (Image)dgvCategory.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                //展开
                if (selectimg == imgPackDown)
                {
                    PackDownCategory();
                    return;
                }
                //收起
                if (selectimg == imgPackUp)
                {
                    PackUpCategory();
                    return;
                }
                if (selectimg.Tag == null)  //空白单元格（无商品）
                {
                    return;
                }

                ShowLoading(true,false);
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

                //说明是第一次加载
                if (sender == null)
                {
                    UpdateDgvGood(true, true);

                }
                else
                {
                    UpdateDgvGood(false, false);

                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("选择分类异常"+ex.StackTrace,true);
            }
            finally
            {
                ShowLoading(false,true);
                btnScan.Select();
            }
        }

        private void PackDownCategory()
        {
            try
            {

                int whitecount = 7 - (sortCategory.Count+1) % 7;
                int rowscount = (int)Math.Ceiling((decimal)(sortCategory.Count+1) / 7);

                List<Image> lstimg = new List<Image>();

                foreach (KeyValuePair<string, string> kv in sortCategory)
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

                    lstimg.Add(img);
                }
                lstimg.Add(imgPackUp);

                dgvCategory.Rows.Clear();

                for (int i = 0; i < whitecount; i++)
                {
                    lstimg.Add(ResourcePos.empty);
                }

                dgvCategory.Rows.Clear();
                for (int i = 0; i < rowscount; i++)
                {
                    int temp = 7 * i;
                    dgvCategory.Rows.Add(lstimg[temp + 0], lstimg[temp + 1], lstimg[temp + 2], lstimg[temp + 3], lstimg[temp + 4], lstimg[temp + 5], lstimg[temp + 6]);
                }


                dgvCategory.Height = rowscount * dgvCategory.RowTemplate.Height+10;

                tlpGood.RowStyles[0] = new RowStyle(SizeType.Absolute, dgvCategory.Height + dgvCategory.Top);
                dgvGood.Height = pnlDgvGood.Height;
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("展开分类异常"+ex.Message ,true);
            }
        }

        private void PackUpCategory()
        {
            try
            {

                List<Image> lstimg = new List<Image>();

                foreach (KeyValuePair<string, string> kv in sortCategory)
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
                   

                    lstimg.Add(img);
                    //超过14个 只加载13个 最后一个补充展开按钮
                    if (lstimg.Count >= 13)
                    {
                        break;
                    }
                }
                lstimg.Add(imgPackDown);

                dgvCategory.Rows.Clear();
                for (int i = 0; i < 2; i++)
                {
                    int temp = 7 * i;
                    dgvCategory.Rows.Add(lstimg[temp + 0], lstimg[temp + 1], lstimg[temp + 2], lstimg[temp + 3], lstimg[temp + 4], lstimg[temp + 5], lstimg[temp + 6]);
                }

                dgvCategory.Height = 2 * dgvCategory.RowTemplate.Height+10;

                tlpGood.RowStyles[0] = new RowStyle(SizeType.Absolute, dgvCategory.Height + dgvCategory.Top);
                dgvGood.Height = pnlDgvGood.Height;
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("收起分类异常" + ex.Message, true);
            }
        }

        bool isnewType = false;
        private void UpdateDgvGood(bool isnew, bool isnewType)
        {
            try
            {
                int dgrowscount = dgvGood.Rows.Count * 3;
                LoadBtnSortStatus(sortCartByFirstCategoryid[CurrentFirstCategoryid].sorttype);
                List<Product> templstprodcut = sortCartByFirstCategoryid[CurrentFirstCategoryid].products;

                List<Product> tempIsLoadlstprodcut = templstprodcut.Where(r => r.isLoadPanel == true).ToList();

                List<Product> tempNotLoadlstprodcut = templstprodcut.Where(r => r.isLoadPanel == false).ToList();

                if (templstprodcut.Count > dgrowscount)
                {
                    if (isnewType)
                    {
                        isnewType = false;
                        dgvGood.Rows.Clear();
                        //isnew = true;
                        switch (sortCartByFirstCategoryid[CurrentFirstCategoryid].sorttype)
                        {
                            case SortType.SaleCount: templstprodcut = templstprodcut.OrderByDescending(r => r.salecount).ToList(); break;
                            case SortType.CreateDate: templstprodcut = templstprodcut.OrderByDescending(r => r.createdat).ToList(); break;
                            case SortType.SalePriceAsc: templstprodcut = templstprodcut.OrderBy(r => r.price.saleprice).ToList(); break;
                            case SortType.SalePriceDesc: templstprodcut = templstprodcut.OrderByDescending(r => r.price.saleprice).ToList(); break;
                            default: templstprodcut.OrderBy(r => r.salecount); break;
                        }
                        tempNotLoadlstprodcut = templstprodcut;
                    }

                    if ((tempIsLoadlstprodcut.Count == 0 || isnew) && tempNotLoadlstprodcut.Count > 0)
                    {
                        DateTime starttime = DateTime.Now;
                        int newcount = Math.Min(tempNotLoadlstprodcut.Count, 18);
                        List<Product> lstNewProduct = tempNotLoadlstprodcut.GetRange(0, newcount);

                        //防止转换json  死循环   bmp.tag 是product
                        lstNewProduct.ForEach(r => r.panelbmp = null);

                        if (!MainModel.IsOffLine)
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

                            panelpara.products = lstNewProduct;
                            string ErrorMsg = "";
                            int resultcode = -1;
                            List<Product> templstpro = httputil.GetPanelProductPrice(panelpara, ref ErrorMsg, ref resultcode);
                            if (!string.IsNullOrEmpty(ErrorMsg) || templstpro == null)
                            {
                                CheckUserAndMember(resultcode, ErrorMsg);
                                //MainModel.ShowLog(ErrorMsg, true); 
                            }
                            else
                            {
                                foreach (Product temppro in templstpro)
                                {
                                    templstprodcut.Where(r => r.skucode == temppro.skucode).ToList()[0].price = temppro.price;
                                    templstprodcut.Where(r => r.skucode == temppro.skucode).ToList()[0].price = temppro.price;
                                    templstprodcut.Where(r => r.skucode == temppro.skucode).ToList()[0].pricetagid = temppro.pricetagid;
                                    templstprodcut.Where(r => r.skucode == temppro.skucode).ToList()[0].pricetag = temppro.pricetag;
                                    templstprodcut.Where(r => r.skucode == temppro.skucode).ToList()[0].isLoadPanel = true;
                                    templstprodcut.Where(r => r.skucode == temppro.skucode).ToList()[0].panelbmp = GetItemImg(templstprodcut.Where(r => r.skucode == temppro.skucode).ToList()[0]);

                                    tempIsLoadlstprodcut.Add(templstprodcut.Where(r => r.skucode == temppro.skucode).ToList()[0]);
                                }
                            }
                        }
                        else
                        {
                            foreach (Product pro in lstNewProduct)
                            {
                                if (!pro.isLoadPanel)
                                {
                                    pro.isLoadPanel = true;
                                    pro.panelbmp = GetItemImg(pro);

                                    tempIsLoadlstprodcut.Add(pro);
                                }
                            }

                        }
                        Console.WriteLine("面板商品加载时间：" + (DateTime.Now - starttime).TotalMilliseconds);
                    }
                    else
                    {

                    }

                }
                LoadDgv(tempIsLoadlstprodcut);

             
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ERROR", "加载面板商品异常" + ex.Message);
            }
           
        }


        private void LoadDgv(List<Product> lstpro)
        {
            try
            {

                switch (sortCartByFirstCategoryid[CurrentFirstCategoryid].sorttype)
                {
                    case SortType.SaleCount: lstpro = lstpro.OrderByDescending(r => r.salecount).ToList(); break;
                    case SortType.CreateDate: lstpro = lstpro.OrderByDescending(r => r.createdat).ToList(); break;
                    case SortType.SalePriceAsc: lstpro = lstpro.OrderBy(r => r.price.saleprice).ToList(); break;
                    case SortType.SalePriceDesc: lstpro = lstpro.OrderByDescending(r => r.price.saleprice).ToList(); break;
                    default: lstpro.OrderBy(r => r.salecount); break;
                }


                dgvGood.Rows.Clear();

                int count = lstpro.Count;
                List<Bitmap> lstbmp = new List<Bitmap>();
                for (int i = 0; i < count; i++)
                {
                    lstbmp.Add(lstpro[i].panelbmp);
                    if (lstbmp.Count >= 3 || i >= count - 1)
                    {
                        int addcount = 3 - lstbmp.Count;
                        for (int j = 0; j < addcount; j++)
                        {
                            lstbmp.Add(ResourcePos.empty);
                        }
                        dgvGood.Rows.Add(lstbmp[0], lstbmp[1], lstbmp[2]);
                        lstbmp = new List<Bitmap>();
                    }
                }


            }
            catch (Exception ex)
            {
                //ShowLoading(false);

            }

        }

        private void dgvGood_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
               
                if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
                {
                    if (e.NewValue + dgvGood.DisplayedRowCount(false) == dgvGood.Rows.Count)
                    {

                        if (sortCartByFirstCategoryid[CurrentFirstCategoryid].products.Count > 0 && sortCartByFirstCategoryid[CurrentFirstCategoryid].products.Count <= dgvGood.Rows.Count * 3)
                        {
                            lblOver.Visible = true;
                        }
                        else
                        {
                            ShowLoading(true,false);
                           
                            UpdateDgvGood(true, false);

                            ShowLoading(false,true);

                        }
                    }
                    else
                    {
                        lblOver.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("刷新商品列表异常",true);
            }
        }




        private Bitmap GetItemImg(Product pro)
        {


            lblGoodName.AutoSize = true;
            lblGoodName.Text = pro.skuname;

            if (lblGoodName.Width > pnlGoodNotSelect.Width - pnlpicItem.Width - 25)
            {
                lblGoodName.AutoSize = false;
                lblGoodName.Width = pnlGoodNotSelect.Width - pnlpicItem.Width - 25;
                lblGoodName.Height = HeightLblGoodName * 2;
                lblGoodName.Top = 5;
            }
            else
            {
                lblGoodName.Top = TopLblGoodName;
            }

            lblPromotion.Top = lblGoodName.Top + lblGoodName.Height;
            lblPromotion.Text = pro.pricetag;
            lblPriceDetail.Text = "/" + pro.saleunit;

            string imgurl = pro.mainimg;
            string imgname = imgurl.Substring(imgurl.LastIndexOf("/") + 1) + ".bmp"; //URL 最后的值

            switch (pro.pricetagid)
            {

                case 1: picGoodPricetag.Visible = true; picGoodPricetag.BackgroundImage = pic会员.Image; break;
                case 2: picGoodPricetag.Visible = true; picGoodPricetag.BackgroundImage = pic折扣.Image; break;
                case 3: picGoodPricetag.Visible = true; picGoodPricetag.BackgroundImage = pic直降.Image; break;
                case 4: picGoodPricetag.Visible = true; picGoodPricetag.BackgroundImage = pic会员.Image; break;
                default: picGoodPricetag.Visible = false; break;          
            }

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
                    if (!string.IsNullOrEmpty(pro.price.salepricedesc))
                    {
                        lblPriceDetail.Text += "(" + pro.price.salepricedesc + ")";
                    }

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

                    if (!string.IsNullOrEmpty(pro.price.originpricedesc))
                    {
                        lblMemberPrice.Text += "(" + pro.price.originpricedesc + ")";
                    }
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

            if (File.Exists(MainModel.ProductPicPath + imgname))
            {
                pnlpicItem.BackgroundImage = Image.FromFile(MainModel.ProductPicPath + imgname);
            }
            else
            {
                try
                {
                    Image _image = Image.FromStream(System.Net.WebRequest.Create(imgurl).GetResponse().GetResponseStream());
                    _image.Save(MainModel.ProductPicPath + imgname);
                    pnlpicItem.BackgroundImage = Image.FromFile(MainModel.ProductPicPath + imgname);
                }
                catch { }
            }




            //if (pro.panelSelectNum > 0)
            //{
            //    btnRed.Text = pro.panelSelectNum.ToString();
            //    btnRed.Visible = true;
            //}
            //else
            //{
            //    btnRed.Visible = false;
            //}

            ////获取单元格图片内容
            //Bitmap b = new Bitmap(pnlItem.Width, pnlItem.Height);

            //b.Tag = pro;
            //pnlItem.DrawToBitmap(b, new Rectangle(0, 0, pnlItem.Width, pnlItem.Height));

            Bitmap b =(Bitmap) MainModel.GetControlImage(pnlGoodNotSelect);
            b.Tag = pro;
            return b;
        }
        #endregion


        #region 公用
        /// <summary>
        /// 
        /// </summary>
        /// <param name="showloading">显示等待框</param>
        /// <param name="isenable">页面是否可操作</param>
        private void ShowLoading(bool showloading,bool isenable)
        {
            try
            {

                IsEnable = isenable;
                if (this.IsHandleCreated)
                {
                    this.Invoke(new InvokeHandler(delegate()
                    {
                        picLoading.Visible = showloading;

                    }));
                }
                else
                {
                    picLoading.Visible = showloading;
                }
                btnScan.Select();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("显示等待异常" + ex.Message);
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
                       // LoadPicScreen(true);
                        MainModel.CurrentMember = null;
                        frmUserExpired frmuserexpired = new frmUserExpired();
                        frmuserexpired.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmuserexpired.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmuserexpired.Height) / 2);
                        frmuserexpired.TopMost = true;
                        frmuserexpired.ShowDialog();

                        INIManager.SetIni("System", "POS-Authorization", "", MainModel.IniPath);

                       
                        BaseUIHelper.CloseFormMain();

                    }
                    else if (resultcode == MainModel.HttpMemberExpired || resultcode == MainModel.DifferentMember)
                    {
                        ClearMember();
                        if (!WinSaasPOS_Scale.HelperUI.ConfirmHelper.Confirm("会员登录已过期，请重新登录","",false))
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
                            MainModel.ShowLog("条码不正确",true);
                        }
                        Thread.Sleep(100);
                    }
                    //}
                    catch (Exception ex)
                    {
                        ShowLoading(false,true);// LoadingHelper.CloseForm();//关闭
                        LogManager.WriteLog("ERROR", "扫描数据处理异常：" + ex.Message);
                    }
                    finally
                    {
                        IsEnable = true;
                        ShowLoading(false,true);// LoadingHelper.CloseForm();
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
                else
                {

                    isINNERBARCODE = false;
                    List<DBPRODUCT_BEANMODEL> lstdbpro = productbll.GetModelList(" BARCODE='" + goodcode + "'" + " and CREATE_URL_IP='" + MainModel.URL + "' " + " and SHOPID='" + MainModel.CurrentShopInfo.shopid + "' ");
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
                    return;
                }

                //IsEnable = false;
                if (e.RowIndex < 0)
                    return;


                if (KBoard.Visible)
                {
                    KBoard.Hide();
                }

                Image selectimg = (Image)dgvGood.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                if (selectimg.Tag == null)  //空白单元格（无商品）
                {
                    return;
                }

                ShowLoading(true, false);
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

   

                Product pro = (Product)selectimg.Tag;

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

                   SelectProduct = null;

                   if (MainModel.WhetherPrint)
                   {
                      // pro.price.saleprice = pro.price.saleprice; //打印用 不等访问购物车接口节省时间
                       LabelPrintHelper.LabelPrint(pro);
                   }
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
                MainModel.ShowLog("选择商品异常"+ex.StackTrace,true);
            }
            finally
            {
                btnScan.Select();
            }
        }


        private void dgvGoodQuery_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }

                //IsEnable = false;
                if (e.RowIndex < 0)
                    return;


                if (KBoard.Visible)
                {
                    KBoard.Hide();
                }


                if (SelectProduct != null)
                {
                    //遍历单元格清空之前的选中状态
                    for (int i = 0; i < this.dgvGoodQuery.Rows.Count; i++)
                    {
                        for (int j = 0; j < this.dgvGoodQuery.Columns.Count; j++)
                        {
                            Image lastimg = (Image)dgvGoodQuery.Rows[i].Cells[j].Value;

                            if (lastimg.Tag != null && ((Product)lastimg.Tag).skucode == SelectProduct.skucode)
                            {

                                dgvGoodQuery.Rows[i].Cells[j].Value = GetItemImg((Product)lastimg.Tag);
                                break;
                            }
                        }
                    }
                }


                Image selectimg = (Image)dgvGoodQuery.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                if (selectimg.Tag == null)  //空白单元格（无商品）
                {
                    return;
                }

                Product pro = (Product)selectimg.Tag;


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

                    SelectProduct = null;

                    if (MainModel.WhetherPrint)
                    {
                        // pro.price.saleprice = pro.price.saleprice; //打印用 不等访问购物车接口节省时间
                        LabelPrintHelper.LabelPrint(pro);
                    }
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

                ShowLoading(false, true);

            }
            catch (Exception ex)
            {
                ShowLoading(false, true);
                MainModel.ShowLog("选择商品异常" + ex.StackTrace, true);
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


        private void ClearDgvGoodQuerySelect()
        {
            try
            {
                if (dgvGoodQuery.Rows.Count <= 0 ||  !dgvGoodQuery.Visible)
                {
                    return;
                }
                DataGridViewCell dgc = dgvGoodQuery.CurrentCell;
                if (dgc != null)
                {
                    Image lastimg = (Image)dgc.Value;

                    if (lastimg.Tag != null)
                    {
                        dgc.Value = GetItemImg((Product)lastimg.Tag);
                    }
                }
                
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("清除查询商品列表选择项异常" + ex.Message);
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
                    IsEnable = false;

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
                                MainModel.ShowLog(ErrorMsgCart, false);
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

               MainModel.ShowLog("刷新购物车异常：" + ex.Message, true);
                return false;
            }
            finally
            {
                IsEnable = true;
            }
        }


        private bool RefreshCart(List<Product> lstpro)
        {

            try
            {

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

                        MainModel.ShowLog(ErrorMsgCart,false);
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

                MainModel.ShowLog("刷新购物车异常：" + ex.StackTrace, true);

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
                   


                    int oldrowindex = dgvCart.FirstDisplayedScrollingRowIndex;
                    dgvCart.Rows.Clear();
                    if (cart != null && cart.products != null && cart.products.Count > 0)
                    {

                        int count = cart.products.Count;
                        int goodscount = 0;
                        foreach (Product pro in cart.products)
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
                            for (int i = 0; i < count; i++)
                            {

                                Product temppro = cart.products[i].ThisClone();

                                //标品之前有的话不会改变位置，所以要记录标品行数 实现动画效果
                                if (temppro.goodstagid == 0 && temppro.skucode == FlashSkuCode)
                                {
                                    FlashIndex = count - i - 1;
                                }

                                List<Bitmap> lstbmp = GetDgvRow(temppro);
                                if (lstbmp != null && lstbmp.Count == 6)
                                {
                                    dgvCart.Rows.Insert(0, new object[] { lstbmp[0], lstbmp[1], lstbmp[2], lstbmp[3], lstbmp[4],lstbmp[5] });
                                }
                            }
                            try { dgvCart.FirstDisplayedScrollingRowIndex = oldrowindex; }
                            catch { }

                            Application.DoEvents();

                            ////ShowDgv();
                            Thread threadItemExedate = new Thread(ShowDgv);
                            threadItemExedate.IsBackground = true;
                            threadItemExedate.SetApartmentState(ApartmentState.STA);
                            threadItemExedate.Start();

                            dgvCart.ClearSelection();

                        }
                    }

                    CurrentCart = cart;

                    if (CurrentCart.availablecoupons != null && CurrentCart.availablecoupons.Length > 0)
                    {

                        btnCoupon.Visible = true;

                        if (CurrentCart.couponpromoamt > 0)
                        {
                            btnCoupon.Text = "-￥" + CurrentCart.couponpromoamt;
                        }
                        else
                        {
                            MainModel.CurrentCouponCode = "";
                           btnCoupon.Text = CurrentCart.availablecoupons.Length + "张";
                        }
                    }
                    else
                    {
                        MainModel.CurrentCouponCode = "";
                        btnCoupon.Visible = false;
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
                    ClearDgvGoodQuerySelect();
                    this.Activate();

                }
                catch (Exception ex)
                {
                    dgvGood.Refresh();
                    LogManager.WriteLog("更新显示列表异常" + ex.Message + ex.StackTrace);
                }
            }
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

                            ////只显示优惠，不显示
                            //if (!orderprice.title.Contains("商品"))
                            //{
                            //    lstcartdetail.Add(orderprice.title+":" + orderprice.amount);

                            //}

                            lstcartdetail.Add(orderprice.title + ":" + orderprice.amount);
                            // dgvCartDetail.Rows.Add(orderprice.title, orderprice.amount);                           
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
                // btnOrderCancle.Visible = false;
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
                    case 4: picMember.Visible = true; picMember.BackgroundImage = pic会员.Image; picMember.Location = lblTitle.Location; lblTitle.Text = "          " + lblTitle.Text; break;
                    default: picMember.Visible=false; break;
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
                    btnNum.Text = pro.num.ToString() + "    ";

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
                //return null;
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("解析商品信息异常" + ex.Message, true);
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
                btnCoupon.Visible = false;

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

                lblShopName.Text = MainModel.CurrentShopInfo.shopname;
                btnMenu.Text = MainModel.CurrentUser.nickname + ",你好 ∨";
                btnMenu.Left = Math.Max(pnlHead.Width - btnMenu.Width - 10, btnOrderQuery.Right);

                picLoading.Size = new Size(55,55);

                KBoard.Size = new Size(pnlPanelGood.Left,this.Height-pnlHead.Height);
                KBoard.Location = new Point(0, pnlHead.Height);

                INIManager.SetIni("System", "MainType", "Main", MainModel.IniPath); 

                #region 排序选择
                btnSortNortSelect.RoundRadius = btnSortNortSelect.Height;
                btnSortSelect.RoundRadius = btnSortSelect.Height;

                imgSelect = MainModel.GetControlImage(btnSortSelect);
                imgNotSelect = MainModel.GetControlImage(btnSortNortSelect);
                btnOrderBySaleCount.BackgroundImage = imgSelect;


                imgPackUp = MainModel.GetControlImage(pnlPackUp);
                imgPackDown = MainModel.GetControlImage(pnlPackDown);
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
                    Thread threadTV = new Thread(DataUtil.LoadTVSkus);
                    threadTV.IsBackground = true;
                    threadTV.Start();
                }
             

            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载基础信息异常",true);
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
                    if (!string.IsNullOrEmpty(pro.price.purchaselimitsubdesc))
                    {
                        ConfirmHelper.Confirm("温馨提示", "知道了"); 
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
                                if (WinSaasPOS_Scale.HelperUI.ConfirmHelper.Confirm("是否确认删除商品？",pro.title+pro.skucode))
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
                    if (!WinSaasPOS_Scale.HelperUI.ConfirmHelper.Confirm("是否确认删除商品？", pro.title + pro.skucode))
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
                MainModel.ShowLog("操作购物车商品异常" + ex.Message, true);
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
                        ClearForm();
                        ClearMember();
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
                MainModel.ShowLog("结算异常"+ex.Message,true);
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
                    MainModel.ShowLog("需要继续支付", true);
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
                MainModel.ShowLog("修改订单金额异常：" + ex.Message, true);
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

                    if (MainModel.WhetherAutoCart && CurrentScaleResult.WhetherStable && SelectProduct!=null)
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

        #endregion

        private void btnScan_Click(object sender, EventArgs e)
        {

            //List<string> lst = new List<string>();

            //lst.Add("4206842040000");
            //lst.Add("4510002");
            //string errormsg = "";
            //httputil.GetSkuMovePrice(MainModel.CurrentShopInfo.shopid,lst,ref errormsg);

        }



        string keyInput = "";
        private void MiniKeyboardHandler(object sender, WinSaasPOS_Scale.MyControl.KeyBoard.KeyboardArgs e)
        {
            TextBox focusing = txtSearch;
            keyInput = e.KeyCode;

            int startDel = 0;

            //退格
            if (keyInput == KBoard.KeyDelete)
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
            else if (keyInput == KBoard.KeyEnter)
            {
                //TOOD querendong
            }
            else if (keyInput == KBoard.KeyClear)
            {
                focusing.Text = "";
            }
            else if (keyInput == KBoard.KeyHide)
            {
                KBoard.Hide();
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

        private void dgvCart_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                BaseUIHelper.UpDgvScorll(dgvCart.FirstDisplayedScrollingRowIndex);
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (pnlHead.ContainsFocus || dgvGood.ContainsFocus || dgvGoodQuery.ContainsFocus)
            {
                KBoard.Hide();
            }
            //if(!KBoard.ContainsFocus)
            //KBoard.Hide();
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


    }
}
