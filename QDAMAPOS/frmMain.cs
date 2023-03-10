using Maticsoft.BLL;
using Maticsoft.Model;
using Newtonsoft.Json;
using QDAMAPOS.Common;
using QDAMAPOS.Model;
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

namespace QDAMAPOS
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

        private Bitmap bmpdelete;
        private Bitmap bmpMinus;
        private Bitmap bmpAdd;
        private Bitmap bmpWhite;
        private Bitmap bmpEmpty;
        /// <summary>
        /// this.enable=false; 页面不可用页面不可控；  通过该标志控制页面是否可用
        /// </summary>
        private  bool IsEnable=true;
        #endregion

        #region  页面加载
        public frmMain(frmLogin frmlogin)
        {
            InitializeComponent();

            //使用委托的话frmmain界面会卡死
            Control.CheckForIllegalCrossThreadCalls = false;

            MainModel.wScale = (float)Screen.AllScreens[0].Bounds.Width / this.Width;
            MainModel.hScale = (float)Screen.AllScreens[0].Bounds.Height / this.Height;

            //ShowLoading(false);// LoadingHelper.CloseForm();
            //防止标品加减框变形
            btnIncrease.Size = new System.Drawing.Size(35, 35);
            btnNum.Size = new System.Drawing.Size(90, 35);
            btnNum.Left = pnlNum.Width - btnNum.Width + 3;
            CurrentFrmLogin = frmlogin;
            //Application.DoEvents();
            //frmlogin.Hide();
        }
        private void frmMain_Shown(object sender, EventArgs e)
        {
            MainModel.IsOffLine = false;
            CurrentFrmLogin.Hide();
            LoadingHelper.CloseForm();

            ParameterizedThreadStart Pts = new ParameterizedThreadStart(ChangeMQTT);
            Thread threadmqtt = new Thread(Pts);
            threadmqtt.IsBackground = true;
            threadmqtt.Start(true);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
              
                SetBtnPayStarus(false);

                timerNow.Interval = 1000;
                timerNow.Enabled = true;

                timerClearMemory.Interval = 2 * 60 * 1000;
                timerClearMemory.Enabled = true;

                lblShopName.Text = MainModel.CurrentShopInfo.shopname;

                btnOnLineType.Left = lblShopName.Left + lblShopName.Width + 10;
                //∨ 从右往左排列 被当成图形   从左向右 右侧间距太大
                btnMenu.Text = MainModel.CurrentUser.nickname + "，你好 ∨";

                picBirthday.Visible = false;
                

                //扫描数据处理线程
                Thread threadScanCode = new Thread(ScanCodeThread);
                threadScanCode.IsBackground = false;
                threadScanCode.Start();

                //刷新焦点线程  防止客屏播放视频抢走焦点
                Thread threadCheckActivate = new Thread(CheckActivate);
                threadCheckActivate.IsBackground = false;
                threadCheckActivate.Start();

                //启动全量商品同步线程
                Thread threadLoadAllProduct = new Thread(LoadAllProduct);
                threadLoadAllProduct.IsBackground = false;
                threadLoadAllProduct.Start();

                //启动全量商品同步线程
                Thread threadLoadPromotion = new Thread(serverutil.UpdatePromotion);
                threadLoadPromotion.IsBackground = false;
                threadLoadPromotion.Start();

                //启动电子秤同步信息线程
                Thread threadLoadScale = new Thread(LoadScale);
                threadLoadScale.IsBackground = false;
                threadLoadScale.Start();

                //启动电子秤同步信息线程
                Thread threadLoadFrmIni = new Thread(LoadFormIni);
                threadLoadFrmIni.IsBackground = false;
                threadLoadFrmIni.Start();


                //启动电视屏服务
                Thread threadServerStart = new Thread(HttpServerStart);
                threadServerStart.IsBackground = false;
                threadServerStart.Start();

                //启动电视屏服务
                Thread threadUploadOffLineDate = new Thread(UploadOffLineData);
                threadUploadOffLineDate.IsBackground = false;
                threadUploadOffLineDate.Start();


                Thread threadLoadExpense = new Thread(UpLoadExpenses);
                threadLoadExpense.IsBackground = false;
                threadLoadExpense.Start();
                timerGetIncrementProduct.Enabled = true;

                btnorderhangimage = new Bitmap(btnOrderHang.Image, 10, 10);
                UpdateOrderHang();


                //控制按钮图片大小，防止与按钮文字异常
                try
                {                   
                    int topsize = Convert.ToInt16(btnPayByCash.Height * (MainModel.hScale - 1) / 3 / MainModel.hScale);

                    btnPayByCash.Padding = new System.Windows.Forms.Padding(0, topsize, 0, topsize);
                    btnPayByBalance.Padding = new System.Windows.Forms.Padding(0, topsize, 0, topsize);
                    btnPayOnLine.Padding = new System.Windows.Forms.Padding(0, topsize, 0, topsize);
                    btnPayByCoupon.Padding = new System.Windows.Forms.Padding(0, topsize, 0, topsize);

                    bmpdelete = new Bitmap(picDelete.Image, dgvGood.RowTemplate.Height * 26 / 100, dgvGood.RowTemplate.Height * 26 / 100);

                    picLoading.Size = new Size(55, 55);
                    pnlPriceLine.Height = 1;
                    bmpAdd = new Bitmap(Resources.ResourcePos.add, dgvGood.RowTemplate.Height * 44 / 100, dgvGood.RowTemplate.Height * 44 / 100);
                    bmpEmpty = Resources.ResourcePos.empty;                  
                }
                catch { }
                Application.DoEvents();
                CheckPrint();
              
            }
            catch (Exception ex)
            {
                ShowLog("初始化页面异常" + ex.Message + ex.StackTrace, true);
            }
            this.Activate();
        }



        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {

            ChangeMQTT(false);
            timerGetIncrementProduct.Enabled = false;
            //MainModel.ShowTaskThread();

            //MainModel.frmmainmedia.Close();
            //MainModel.frmmainmedia = null;
            MainModel.ShowTask();
           // CurrentFrmLogin.Show();
        }

        
        bool isshowpic = false;
        //实时时间显示
        private void timerNow_Tick(object sender, EventArgs e)
        {

            ////button4_Click(null,null);
            ////button4.PerformClick();
            ////***否则点击完 dgv 监听键盘事件监听不到 return和0
            //btnScan.Select();
           // button4.Focus();
            //button4.Visible = false;
            lblTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            //isshowpic = !isshowpic;
            //LoadPicScreen(isshowpic);
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
                //this.Invoke(new InvokeHandler(delegate()
                //{
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
                        MainModel.Authorization = "";
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
                        ClearForm();
                        LoadPicScreen(false);

                        btnLoadPhone_Click(null, null);

                    }
                    //else if (resultcode == MainModel.DifferentMember)   //不是同一个会员 只提示不退出
                    //{
                    //    MainModel.ShowLog("非当前登录用户的付款码，请确认后重新支付", true);
                    //}
                    else
                    {
                        ShowLog(ErrorMsg, false);
                    }
               // }));

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
                MainModel.Authorization = "";
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
                    ReceiptUtil.EditCancelOrder(1, CurrentCart.totalpayment);
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
                        ShowLog("已挂单", false);

                        ClearForm();




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
            try
            {

            
            if (!IsEnable)
            {
                return;
            }

            frmToolMain frmtool = new frmToolMain();

            asf.AutoScaleControlTest(frmtool, 178, 413, Convert.ToInt32(MainModel.wScale * 178), Convert.ToInt32(MainModel.hScale * 413), true);
            frmtool.DataReceiveHandle += frmToolMain_DataReceiveHandle;
            frmtool.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width - frmtool.Width - 15, pnlHead.Height + 10);
            frmtool.Show();
            //if (MainModel.frmtoolmain == null)
            //{
            //    frmToolMain frmtool = new frmToolMain();

            //    asf.AutoScaleControlTest(frmtool, 178, 413, Convert.ToInt32(MainModel.wScale * 178), Convert.ToInt32(MainModel.hScale * 413), true);
            //    frmtool.DataReceiveHandle += frmToolMain_DataReceiveHandle;
            //    frmtool.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width - frmtool.Width - 15, pnlHead.Height + 10);
            //    frmtool.Show();
            //}
            //else
            //{

            //}
           
            }
            catch (Exception ex)
            {
                MainModel.frmtoolmain = null;
                MainModel.ShowLog("菜单窗体显示异常"+ex.Message,true);
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
               ShowLoading(true);// LoadingHelper.ShowLoadingScreen();
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

            if (frmchangemode.DialogResult == DialogResult.OK)
            {

                INIManager.SetIni("System", "POS-Authorization", "", MainModel.IniPath);
                MainModel.Authorization = "";
                this.Hide();
                if (MainModel.frmloginoffline == null)
                {

                    MainModel.frmloginoffline = new frmLoginOffLine();
                    MainModel.frmloginoffline.Show();
                    this.Close();
                }
                else
                {
                    MainModel.frmloginoffline.Show();
                    this.Close();
                }
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
                    string goodscode = frmnumberback.NumValue.ToString();
                    if (goodscode.Length == 13)
                    {
                        QueueScanCode.Enqueue(goodscode);
                    }
                    else
                    {

                        if (goodscode.Length > 5 && goodscode.Length != 8 && goodscode.Length != 13)
                        {
                            // ShowLog("请输入正确的手机号！",false);
                            //lblMsg.Text = "只允许输入1-5、8、13 位！";
                            MainModel.ShowLog("只允许输入1-5、8、13 位！",true);
                            return;
                        }
                        else if (goodscode.Length == 8)
                        {
                            goodscode = goodscode.Substring(2, 5);

                        }

                        InputShortCodeThread(goodscode); 
                    }
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
                isGoodRefresh = false;
                DateTime starttime = DateTime.Now;
                string ErrorMsgCart = "";
                int ResultCode = -1;

                
                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                {
                    // pnlPayType1.Enabled = true;
                    SetBtnPayStarus(true);
                    pnlPayType2.Enabled = true;
                    //CurrentCart.pointpayoption = 1;

                    CurrentCart.pointpayoption = btnCheckJF.BackgroundImage == picCheck.BackgroundImage ? 1 : 0;
                   ShowLoading(true);// LoadingHelper.ShowLoadingScreen();
                    IsEnable = false;

                    Cart cart = httputil.RefreshCart(CurrentCart, ref ErrorMsgCart, ref ResultCode);
                    Console.WriteLine("购物车访问时间" + (DateTime.Now - starttime).TotalMilliseconds);

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

                    if (btnCheckJF.BackgroundImage == picCheck.BackgroundImage)
                    {
                        if (MainModel.CurrentMember != null && CurrentCart != null && CurrentCart.pointinfo != null)
                        {
                            lblJF.Text = CurrentCart.pointinfo.totalpoints;
                            //lblJFUse.Visible = true;


                            btnJFUse.Text = "使用" + CurrentCart.pointinfo.availablepoints + "积分 抵用" + CurrentCart.pointinfo.availablepointsamount + "元";
                            btnJFUse.Visible = true;
                            // panel2.Visible = true;
                        }
                    }
                    else
                    {
                        btnJFUse.Text = "";
                        btnJFUse.Visible = false;
                        //panel2.Visible = false;
                    }


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




                    //btnPayByBalance.Enabled = CurrentCart.paymenttypes.balancepayenabled == 1;
                    //btnPayByCash.Enabled = CurrentCart.paymenttypes.cashenabled == 1;
                    //btnPayOnLine.Enabled = CurrentCart.paymenttypes.onlineenabled == 1;

                    return true;

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
            finally
            {
                IsEnable = true;
                
                ShowLoading(false);// LoadingHelper.CloseForm();
            }
        }


        private bool RefreshCart(List<Product> lstpro)
        {

          
                try
                {
                   
                    isGoodRefresh = true;
                    //ShowPicScreen = false;
                    IsEnable = false;
                   //ShowLoading(true);// LoadingHelper.ShowLoadingScreen();

                    ShowLoading(true);
                    DateTime starttime = DateTime.Now;
                    string ErrorMsgCart = "";
                    int ResultCode = 0;
                    if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                    {
                        SetBtnPayStarus(true);
                        pnlPayType2.Enabled = true;
                        //CurrentCart.pointpayoption = 1;

                        CurrentCart.pointpayoption = btnCheckJF.BackgroundImage == picCheck.BackgroundImage ? 1 : 0;
                        //增加或删除商品整单优惠重置
                        CurrentCart.fixpricetotal = 0;
                        Cart cart = httputil.RefreshCart(CurrentCart, ref ErrorMsgCart, ref ResultCode);
                        Console.WriteLine("购物车访问时间" + (DateTime.Now - starttime).TotalMilliseconds);

                        if (ErrorMsgCart != "" || cart == null) //商品不存在或异常
                        {
                            CurrentCart.products = lstpro;
                            CheckUserAndMember(ResultCode, ErrorMsgCart);
                            return false;
                        }
                        else
                        {
                            CurrentCart = cart;
                            //有商品增加或减少   fixpricetotal 置0
                            CurrentCart.fixpricetotal = 0;
                            UploadDgvGoods(cart);
                            Console.WriteLine("表格加载时间" + (DateTime.Now - starttime).TotalMilliseconds);

                            if (btnCheckJF.BackgroundImage == picCheck.BackgroundImage)
                            {
                                if (MainModel.CurrentMember != null && CurrentCart != null && CurrentCart.pointinfo != null)
                                {
                                    lblJF.Text = CurrentCart.pointinfo.totalpoints;
                                    //lblJFUse.Visible = true;
                                    btnJFUse.Text = "使用" + CurrentCart.pointinfo.availablepoints + "积分 抵用" + CurrentCart.pointinfo.availablepointsamount + "元";
                                    btnJFUse.Visible = true;
                                }
                            }
                            else
                            {
                                btnJFUse.Text = "";
                                btnJFUse.Visible = false;
                            }


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
                            //btnPayByBalance.Enabled = CurrentCart.paymenttypes.balancepayenabled == 1;
                            //btnPayByCash.Enabled = CurrentCart.paymenttypes.cashenabled == 1;
                            //btnPayOnLine.Enabled = CurrentCart.paymenttypes.onlineenabled == 1;

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
                    IsEnable = true;

                    ShowLog("刷新购物车异常：" + ex.Message, true);

                    return false;
                }
                finally
                {
                    IsEnable = true;
                    ShowLoading(false);
                    //ShowLoading(false);// LoadingHelper.CloseForm();

                }
            
        }

        private object thislockScanCode = new object();
        private void ScanCodeThread(object obj)
        {
            while (true)
            {

                //if (MainModel.IsPlayer==true)
                //{
                //    Delay.Start(500);
                //    MainModel.IsPlayer = false;
                //    this.Activate();

                   
                //    Application.DoEvents();
                //}
               
                if (QueueScanCode.Count > 0 && IsEnable)
                {
                    try
                    {
                        IsEnable = false;
                        ShowLoading(true);// LoadingHelper.ShowLoadingScreen();//显示

                        List<string> LstScanCode = new List<string>();

                        List<string> lstNotLocalCode = new List<string>();
                        while (QueueScanCode.Count > 0)
                        {
                            LstScanCode.Add(QueueScanCode.Dequeue());
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
                                    LoadMember(scancodemember.memberresponsevo);
                                }
                                else
                                {
                                    LstScancodemember.Add(scancodemember);    
                                }
                            }
                        }
                        ShowLoading(false);// LoadingHelper.CloseForm();


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

                        Thread.Sleep(1);
                    }
                    //}
                    catch (Exception ex)
                    {
                        ShowLoading(false);// LoadingHelper.CloseForm();//关闭
                        LogManager.WriteLog("ERROR", "扫描数据处理异常：" + ex.Message);
                    }
                    finally
                    {
                        IsEnable = true;
                        ShowLoading(false);// LoadingHelper.CloseForm();
                        //Application.DoEvents();
                    }
                }


                Thread.Sleep(100);
            }
        }

        private object thislockShortCode = new object();
        private void InputShortCodeThread(object obj)
        {
            lock (thislockShortCode)
            {

            
            try
            {
                List<scancodememberModel> LstScancodemember = new List<scancodememberModel>();
                string goodcode = obj.ToString();

                string ErrorMsg = "";
                int ResultCode = 0;

                DBPRODUCT_BEANMODEL dbpro = productbll.GetModelByGoodsID(goodcode.PadLeft(5,'0'),MainModel.URL);
                if (dbpro == null)
                {
                   ShowLoading(true);// LoadingHelper.ShowLoadingScreen();//显示
                    scancodememberModel scancodemember = httputil.GetSkuInfoByShortCode(goodcode, ref ErrorMsg, ref ResultCode);

                    ShowLoading(false);// LoadingHelper.CloseForm();

                    if (ErrorMsg != "" || scancodemember == null) //商品不存在或异常
                    {
                        CheckUserAndMember(ResultCode, ErrorMsg);
                        //ShowLog(ErrorMsg, false);
                    }
                    else
                    {
                        LstScancodemember.Add(scancodemember);
                        addcart(LstScancodemember);
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
                    if (dbpro.WEIGHTFLAG==0) //非标品数量为1
                    {
                        scancodemember.scancodedto.num = 1;
                        scancodemember.scancodedto.specnum = 1;
                    }

                    scancodemember.scancodedto.barcode = dbpro.BARCODE;
                    //3443
                    LstScancodemember.Add(scancodemember);
                    addcart(LstScancodemember);
                }               

                //ShowLoading(false);// LoadingHelper.CloseForm();//关闭
                
            }
            catch (Exception ex)
            {                
                 ShowLoading(false);// LoadingHelper.CloseForm();//关闭
                LogManager.WriteLog("ERROR", "扫描数据处理异常：" + ex.Message);
            }
           
            }
        }

        private scancodememberModel GetLocalPro(string barcode)
        {
            try
            {
                //(decimal)frmnumberback.NumValue / 1000;
                string skucode = "";
                int pronum = 0;
                if (barcode.Length == 13)
                {

                    if (checkEanCodeIsError(barcode, 13))
                    {
                        return null;
                    }
                    skucode = barcode.Substring(2, 5);
                    pronum = Convert.ToInt32(barcode.Substring(7, 5));
                }
                else if (barcode.Length == 8)
                {
                    skucode = barcode.Substring(2, 5);
                }
                else if (barcode.Length <= 5)
                {
                    skucode = barcode.PadLeft(5, '0');
                }
                else
                {
                    //MainModel.ShowLog("条码不正确", true);
                    return null;
                }

                DBPRODUCT_BEANMODEL dbpro = productbll.GetModelByGoodsID(skucode, MainModel.URL);
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
                    scancodemember.scancodedto.num = (int)dbpro.NUM;
                    scancodemember.scancodedto.specnum = dbpro.SPECNUM;
                    scancodemember.scancodedto.spectype = dbpro.SPECTYPE;
                    scancodemember.scancodedto.weightflag = dbpro.WEIGHTFLAG == 1 ? true : false;
                    if (dbpro.WEIGHTFLAG == 0) //非标品数量为1
                    {
                        scancodemember.scancodedto.num =pronum;
                        scancodemember.scancodedto.specnum = 1;
                    }
                    else
                    {
                        if (barcode.Length == 13)
                        {
                            scancodemember.scancodedto.num = 1;
                            scancodemember.scancodedto.specnum = (decimal)pronum / 1000;
                        }
                       
                    }

                    scancodemember.scancodedto.barcode = dbpro.BARCODE;

                    return scancodemember;
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("条码验证异常"+ex.Message ,true );
                return null;
            }
        }


        public static bool checkEanCodeIsError(String barCode, int num)
        {
            if (barCode.Length != num)
            {
                return true;
            }
            try
            {
                String code = barCode.Substring(0, num - 1);
                String checkBit = barCode.Substring(num - 1);
                int c1 = 0, c2 = 0;
                for (int i = 0; i < code.Length; i += 2)
                {
                    char c = code[i];
                    int n = c - '0';
                    c1 += n;
                }
                for (int i = 1; i < code.Length; i += 2)
                {
                    char c = code[i];
                    int n = c - '0';
                    c2 += n;
                }
                String check = (10 - (c1 + c2 * 3) % 10) % 10 + "";
                if (check == checkBit)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                return true;
            }
        }

        private void addcart( List<scancodememberModel> lstscancodemember)
        {



                try
                {
                    foreach (scancodememberModel scancodemember in lstscancodemember)
                    {

                   

                    if (scancodemember.scancodedto.weightflag && scancodemember.scancodedto.specnum == 0)
                    {
                        frmNumberBack frmnumberback = new frmNumberBack(scancodemember.scancodedto.skuname, NumberType.ProWeight, ShowLocation.Center);
                        
                        frmnumberback.Location = new Point(0, 0);
                        frmnumberback.ShowDialog();

                        if (frmnumberback.DialogResult == DialogResult.OK)
                        {
                            scancodemember.scancodedto.specnum = (decimal)frmnumberback.NumValue / 1000;
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

                    }

                    string ErrorMsgCart = "";
                    int ResultCode = 0;
                    bool IsExits = false;
                    DateTime starttime = DateTime.Now;
                    RefreshCart(LastLstPro);

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
            try
            {

                    MainModel.ShowLog(msg, iserror);               
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
                                if (temppro.goodstagid == 0 && temppro.skucode == FlashSkuCode)
                                {
                                    FlashIndex = count - i - 1;
                                }

                                List<Bitmap> lstbmp = GetDgvRow(temppro);
                                if (lstbmp != null && lstbmp.Count == 6)
                                {
                                    dgvGood.Rows.Insert(0, new object[] { lstbmp[0], lstbmp[1], lstbmp[2], lstbmp[3], lstbmp[4], lstbmp[5] });
                                }
                            }
                            try { dgvGood.FirstDisplayedScrollingRowIndex = oldrowindex; }
                            catch { }

                            Application.DoEvents();

                            ////ShowDgv();
                            Thread threadItemExedate = new Thread(ShowDgv);
                            threadItemExedate.IsBackground = false;
                            threadItemExedate.SetApartmentState(ApartmentState.STA);
                            threadItemExedate.Start();

                            dgvGood.ClearSelection();

                            if (cart.totalpayment == 0 && cart.products != null && cart.products.Count > 0)
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
                        pnlPayType2.Enabled = false;
                    }
                    //Console.WriteLine("积分前" + (DateTime.Now - starttime).TotalMilliseconds);
                    CurrentCart = cart;
                    if (MainModel.CurrentMember != null && btnCheckJF.BackgroundImage == picCheck.BackgroundImage && CurrentCart.pointinfo != null)
                    {
                        btnJFUse.Text = "使用" + CurrentCart.pointinfo.availablepoints + "积分 抵用" + CurrentCart.pointinfo.availablepointsamount + "元";
                        btnJFUse.Visible = true;
                    }


                    if (CurrentCart.availablecoupons != null && CurrentCart.availablecoupons.Length > 0)
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

                    Thread threadMember = new Thread(UploadMember);
                    threadMember.IsBackground = false;
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
                lblSkuCode.Text = pro.skucode;
                //第一行图片
                switch (pro.pricetagid)
                {
                    case 1: lblPriceTag.BackColor = ColorTranslator.FromHtml("#FF7D14"); lblPriceTag.Text = pro.pricetag; break;
                    case 2: lblPriceTag.BackColor = ColorTranslator.FromHtml("#209FD4"); lblPriceTag.Text = pro.pricetag; break;
                    case 3: lblPriceTag.BackColor = ColorTranslator.FromHtml("#D42031"); lblPriceTag.Text = pro.pricetag; break;
                    case 4: lblPriceTag.BackColor = ColorTranslator.FromHtml("#FF000"); lblPriceTag.Text = pro.pricetag; break;
                    default: lblPriceTag.Text = ""; break;
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
                    btnNum.Text = (pro.price.specnum + pro.price.unit).PadRight(5, ' ');
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


                bmpdelete = new Bitmap(picDelete.Image, dgvGood.RowTemplate.Height * 26 / 100, dgvGood.RowTemplate.Height * 26 / 100);
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
                MainModel.ShowLog("解析商品信息异常" + ex.Message, true);
                return null;
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
                        if (CurrentCart.memberpromo != null && CurrentCart.memberpromo > 0)
                        {
                            
                            btnMemberPromo.Text = "会员已优惠:￥" + CurrentCart.memberpromo.ToString("f2");

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


        private object thislockMember = new object();
        private void UploadMember()
        {
            lock (thislockMember)
            {
                try
                {
                    

                    if (pnlMember.Visible)
                    {
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

                //isCellPainting = false;
                dgvGood.Rows.Clear();

                //Application.DoEvents();
               // isCellPainting = true;

                ShowLoading(false);

                lblPrice.Text = "￥" + "0.00";
                lblGoodsCount.Text = "(0件商品)";

                btnMemberPromo.Visible = false;

                pnlWaiting.Show();

                dgvOrderDetail.Rows.Clear();


                pnlPayType1.Visible = true; ;
                pnlPayType2.Visible = false;

                SetBtnPayStarus(false);
                pnlPayType2.Enabled = false;

                
                UpdateOrderHang();

                ShowLoading(false);// LoadingHelper.CloseForm();
                MsgHelper.CloseForm();

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
                frmNumberBack frmnumberback = new frmNumberBack("请输入会员号", NumberType.MemberCode, ShowLocation.Right);
                
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
                        IsEnable = false;
                        pnlWaitingMember.Visible = false;
                        pnlMember.Visible = true;
                        //
                        lblMobil.Text = member.memberheaderresponsevo.mobile;
                        lblWechartNickName.Text = member.memberinformationresponsevo.wechatnickname;

                        MainModel.CurrentMember = member;

                        lblJF.Text = member.creditaccountrepvo.availablecredit.ToString();
                        btnCheckJF.BackgroundImage = picUncheck.BackgroundImage;
                        lblCoupon.Visible = false;
                        lblCouponStr.Visible = false;
                        Application.DoEvents();

                        //RefreshCart();
                        //购物车有商品的话刷新一次
                        if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                        {
                            RefreshCart();
                        }
                        else
                        {
                            //UploadMember();

                            Thread threadMember = new Thread(UploadMember);
                            threadMember.IsBackground = false;
                            threadMember.Start();
                        }
                        IsEnable = true;
    
                        
                        Application.DoEvents();

                        if (member.memberinformationresponsevo.onbirthday)
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
                RefreshCart();
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
                    lblJF.Text = "0";
                    btnJFUse.Text = "";
                    btnJFUse.Visible = false;
                    MainModel.CurrentMember = null;
                    MainModel.CurrentCouponCode = "";
                    //chkJF.Checked = false;
                    btnCheckJF.BackgroundImage = picUncheck.BackgroundImage;
                    pnlWaitingMember.Visible = true;
                    pnlMember.Visible = false;
                    picBirthday.Visible = false;

                    Thread threadMember = new Thread(UploadMember);
                    threadMember.IsBackground = false;
                    threadMember.Start();

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
                if (!IsEnable)
                {
                    return;
                }
                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                {
                    if (btnCheckJF.BackgroundImage == picCheck.BackgroundImage)
                    {
                        btnCheckJF.BackgroundImage = picUncheck.BackgroundImage;
                    }
                    else
                    {
                        btnCheckJF.BackgroundImage = picCheck.BackgroundImage;
                    }

                    RefreshCart();
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
                //frmcoupon.Opacity = 0.95d;

                frmcoupon.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                frmcoupon.ShowDialog();


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
                        frmPaySuccess frmresult = new frmPaySuccess(orderresult.orderid);
                        frmresult.DataReceiveHandle += FormCashierResult_DataReceiveHandle;
                        frmresult.ShowDialog();
                        ClearForm();
                        ClearMember();
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

        private void FormOrderHang_DataReceiveHandle(int type, Cart Cart, string phone)
        {
            try
            {

                this.Invoke(new InvokeHandler(delegate()
                {
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
                    CurrentCart = Cart;

                    RefreshCart(null);
                    //UploadDgvGoods(Cart);


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
                if (!IsEnable)
                {
                    return;
                }
                if (btnPayOnLine.Tag == null || btnPayOnLine.Tag.ToString() != "1")
                {
                    return;
                }

                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                {
                   ShowLoading(true);// LoadingHelper.ShowLoadingScreen("加载中...");
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

                            frmOnLinePayResultBack frmonlinepayresultback = new frmOnLinePayResultBack(orderresult.orderid, CurrentCart);
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
                            frmOnLinePayResultBack frmonlinepayresultback = new frmOnLinePayResultBack(orderid, frmcashpayback.CurrentCart);
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
                        MainModel.ShowLog(ErrorMsg, false);
                        return;
                    }
                    else if (lstCashCoupons == null || lstCashCoupons.Count == 0)
                    {
                        MainModel.ShowLog("没有可用的代金券", false);
                        return;
                    }


                    frmCashCouponBack frmcashcouponback = new frmCashCouponBack(CurrentCart, lstCashCoupons);
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

               ShowLoading(true);// LoadingHelper.ShowLoadingScreen();
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



        private void dgvGood_CellContentClick(object sender, DataGridViewCellEventArgs e)
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


                LastLstPro = new List<Product>();
                foreach (Product ppro in CurrentCart.products)
                {
                    LastLstPro.Add((Product)MainModel.Clone(ppro));
                }

                //string cellnames = dgvGood.Columns[0].Name;
                //string test = cellnames + "";

                //string proinfo = dgvGood.Rows[e.RowIndex].Cells["barcode"].Value.ToString();
                //string[] strs = proinfo.Replace("\r\n", "*").Split('*');
                //string skucode = strs[strs.Length - 2].Trim();
                //string proname = strs[strs.Length - 3].Trim();

                ////dgvGood.Rows[e.RowIndex].Cells[e.ColumnIndex].OwningColumn.Name = "num";
                //string pronum = dgvGood.Rows[e.RowIndex].Cells["num"].Value.ToString();
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
                    //UploadDgvGoods(CurrentCart);
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


                                FrmConfirmBack frmconfirmback = new FrmConfirmBack("是否确认删除商品？", pro.skuname, pro.skucode);

                                frmconfirmback.Location = new Point(0, 0);


                                if (frmconfirmback.ShowDialog() == DialogResult.OK)
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
                    RefreshCart(LastLstPro);
                }

                if (e.ColumnIndex == 5)
                {

                    FrmConfirmBack frmconfirmback = new FrmConfirmBack("是否确认删除商品？", pro.skuname, pro.skucode);

                    frmconfirmback.Location = new Point(0, 0);

                    if (frmconfirmback.ShowDialog() != DialogResult.OK)
                    {

                        return;
                    }

                    foreach (Product delpro in CurrentCart.products)
                    {
                        if (delpro.skucode == pro.skucode && delpro.specnum == pro.specnum)
                        {
                            CurrentCart.products.Remove(delpro);
                            break;
                        }
                    }
                    RefreshCart(LastLstPro);
                }
                //try { dgvGood.FirstDisplayedScrollingRowIndex = oldrowindex; }
                //catch { }




                dgvGood.ClearSelection();
            }
            catch (Exception ex)
            {

                MainModel.ShowLog("操作购物车商品异常" + ex.Message, true);
            }
            finally
            {
                Delay.Start(200);
                btnScan.Select();
                LoadPicScreen(false);
            }
        }

        //防止控件占用焦点后  按键无法捕获
        private void dgvGood_Click(object sender, EventArgs e)
        {
            Delay.Start(200);
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
                       // picScreen.BackgroundImage = picCheck.BackgroundImage;
                        picScreen.Size = new System.Drawing.Size(this.Width, this.Height);
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
                LogManager.WriteLog("修改主窗体背景图异常：" + ex.Message);
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
                LoadPicScreen(true);
                frmPrinterSetting frmprint = new frmPrinterSetting();
                frmprint.TopMost = true;
                asf.AutoScaleControlTest(frmprint, 536, 243, 536 * MainModel.wScale, 243 * MainModel.hScale, true);
                frmprint.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmprint.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmprint.Height) / 2);                

                //frmprint.StartPosition = FormStartPosition.CenterScreen;
                frmprint.ShowDialog();
                LoadPicScreen(false);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("打印机设置页面异常"+ex.Message);
            }
            finally
            {
                LoadPicScreen(false);
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
                        CurrentCart.products = frmpanel.SelectProducts;
                        RefreshCart(LastLstPro);
                    }
                    else
                    {
                        LastLstPro = new List<Product>();
                        foreach (Product ppro in CurrentCart.products)
                        {
                            LastLstPro.Add((Product)MainModel.Clone(ppro));
                        }

                        CurrentCart.products.AddRange(frmpanel.SelectProducts);
                        RefreshCart(LastLstPro);
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


        bool isCellPainting = true;
        //重绘datagridview单元格
        private void dgvGood_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

            try
            {
                if (e.ColumnIndex==0 && e.RowIndex >= 0 && e.Value != null && isCellPainting)//要进行重绘的单元格
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

                if ((e.ColumnIndex==1 || e.ColumnIndex == 4) && e.RowIndex >= 0 && e.Value != null && isCellPainting)//要进行重绘的单元格
                {

                    Graphics gpcEventArgs = e.Graphics;
                    Color clrBack = e.CellStyle.BackColor;
                    //Font fntText = e.CellStyle.Font;//获取单元格字体
                    //先使用背景颜色重画一遍背景
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
                        if (e.ColumnIndex == 4)
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
            finally
            {
                
            }

        }


        //bool isCellPainting = true;
        ////重绘datagridview单元格
        //private void dgvGood_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        //{

        //    try
        //    {
        //        if (e.ColumnIndex == 0 && e.RowIndex >= 0 && e.Value != null && isCellPainting)//要进行重绘的单元格
        //        {
        //            Graphics gpcEventArgs = e.Graphics;
        //            Color clrBack = e.CellStyle.BackColor;
        //            //Font fntText = e.CellStyle.Font;//获取单元格字体
        //            //先使用北京颜色重画一遍背景
        //            gpcEventArgs.FillRectangle(new SolidBrush(clrBack), e.CellBounds);
        //            //设置字体的颜色
        //            Color oneFore = System.Drawing.Color.Black;
        //            Color secFore = System.Drawing.Color.Red;
        //            //string strFirstLine = "黑色内容";
        //            //string strSecondLine = "红色内容";

        //            if (!e.Value.ToString().Contains("\r\n"))
        //            {
        //                return;
        //            }

        //            string tempstr = e.Value.ToString().Replace("\r\n", "*");
        //            string strLine1 = "";
        //            string strLine2 = "";
        //            string strLine3 = "";

        //            strLine1 = tempstr.Split('*')[0];
        //            strLine2 = tempstr.Split('*')[1];
        //            strLine3 = tempstr.Split('*')[2];
        //            string[] sts = tempstr.Split('*');
        //            //Size sizText = TextRenderer.MeasureText(e.Graphics, strFirstLine, fntText);
        //            int intX = e.CellBounds.Left + e.CellStyle.Padding.Left;
        //            int intY = e.CellBounds.Top + e.CellStyle.Padding.Top + 10;
        //            int intWidth = e.CellBounds.Width - (e.CellStyle.Padding.Left + e.CellStyle.Padding.Right);
        //            //int intHeight = sizText.Height + (e.CellStyle.Padding.Top + e.CellStyle.Padding.Bottom);


        //            Font fnt1 = new System.Drawing.Font("微软雅黑", 10F * Math.Min(MainModel.hScale, MainModel.wScale));
        //            //Graphics g = this.CreateGraphics(); //this是指所有control派生出来的类，这里是个form

        //            SizeF size1 = this.CreateGraphics().MeasureString(strLine1, fnt1);
        //            Color titlebackcolor = Color.Black;
        //            if (strLine1.Length > 0)
        //            {

        //                string typecolor = strLine1.Substring(0, 1);
        //                strLine1 = strLine1.Substring(1, strLine1.Length - 1);
        //                switch (typecolor)
        //                {
        //                    case "1": titlebackcolor = ColorTranslator.FromHtml("#FF7D14"); break;
        //                    case "2": titlebackcolor = ColorTranslator.FromHtml("#209FD4"); break;
        //                    case "3": titlebackcolor = ColorTranslator.FromHtml("#D42031"); break;
        //                    case "4": titlebackcolor = ColorTranslator.FromHtml("#FF000"); break;
        //                }
        //            }
        //            //第一行
        //            TextRenderer.DrawText(e.Graphics, strLine1, fnt1, new Rectangle(intX + 10, intY, intWidth, (int)size1.Height),
        //                Color.White, titlebackcolor, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);

        //            //另起一行
        //            Font fnt2 = new System.Drawing.Font("微软雅黑", 12F * Math.Min(MainModel.hScale, MainModel.wScale));
        //            SizeF size2 = this.CreateGraphics().MeasureString(strLine2, fnt2);

        //            intY = intY + (int)size1.Height;
        //            TextRenderer.DrawText(e.Graphics, strLine2, fnt2, new Rectangle(intX, intY, intWidth, (int)size2.Height),
        //                Color.Black, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);

        //            //Font fnt20 = new System.Drawing.Font("微软雅黑", 9F, FontStyle.Strikeout);
        //            //TextRenderer.DrawText(e.Graphics, strLine2, fnt20, new Rectangle(intX + (int)size2.Width, intY, intWidth, (int)size2.Height),
        //            //    Color.Green, Color.Red, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);

        //            Font fnt3 = new System.Drawing.Font("微软雅黑", 12F * Math.Min(MainModel.hScale, MainModel.wScale));
        //            intY = intY + (int)size2.Height;

        //            TextRenderer.DrawText(e.Graphics, strLine3, fnt3, new Rectangle(intX, intY, intWidth, (int)size2.Height), Color.Black, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);


        //            //int y = intY + (int)size2.Height + e.CellStyle.Padding.Top + e.CellStyle.Padding.Bottom + dgvGood.RowTemplate.Height;
        //            int y = (e.RowIndex + 1) * dgvGood.RowTemplate.Height + dgvGood.ColumnHeadersHeight - 1;

        //            //Point point1 = new Point(0, y);
        //            //Point point2 = new Point(e.CellBounds.Width, y);
        //            //Pen blackPen = new Pen(Color.Black, 1);
        //            //e.Graphics.DrawLine(blackPen, point1, point2);

        //            //Point point21 = new Point(10, 0);
        //            //Point point22 = new Point(10, intY + (int)size2.Height + e.CellStyle.Padding.Top + e.CellStyle.Padding.Bottom);
        //            //Pen blackPen2 = new Pen(Color.Black, 10);
        //            //e.Graphics.DrawLine(blackPen2, point21, point21);
        //            // e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        //            // dgv2.Rows[e.RowIndex].Height = (int)size1.Height+(int)size2.Height*2 + e.CellStyle.Padding.Top + e.CellStyle.Padding.Bottom+1;
        //            e.Handled = true;

        //            dgvGood.ClearSelection();
        //        }

        //        if ((e.ColumnIndex == 1 || e.ColumnIndex == 4) && e.RowIndex >= 0 && e.Value != null && isCellPainting)//要进行重绘的单元格
        //        {

        //            Graphics gpcEventArgs = e.Graphics;
        //            Color clrBack = e.CellStyle.BackColor;
        //            //Font fntText = e.CellStyle.Font;//获取单元格字体
        //            //先使用背景颜色重画一遍背景
        //            gpcEventArgs.FillRectangle(new SolidBrush(clrBack), e.CellBounds);
        //            //设置字体的颜色
        //            Color oneFore = System.Drawing.Color.Black;
        //            Color secFore = System.Drawing.Color.Red;
        //            //string strFirstLine = "黑色内容";
        //            //string strSecondLine = "红色内容";

        //            if (!e.Value.ToString().Contains("\r\n"))
        //            {
        //                return;
        //            }

        //            string tempstr = e.Value.ToString().Replace("\r\n", "*");
        //            string strLine1 = "";
        //            string strLine2 = "";


        //            strLine1 = tempstr.Split('*')[0];
        //            strLine2 = tempstr.Split('*')[1];

        //            string[] sts = tempstr.Split('*');
        //            //Size sizText = TextRenderer.MeasureText(e.Graphics, strFirstLine, fntText);
        //            int intX = e.CellBounds.Left + e.CellStyle.Padding.Left;
        //            int intY = e.CellBounds.Top + e.CellStyle.Padding.Top + 30;
        //            int intWidth = e.CellBounds.Width - (e.CellStyle.Padding.Left + e.CellStyle.Padding.Right);
        //            //int intHeight = sizText.Height + (e.CellStyle.Padding.Top + e.CellStyle.Padding.Bottom);


        //            Font fnt1 = new System.Drawing.Font("微软雅黑", 12F * Math.Min(MainModel.hScale, MainModel.wScale));
        //            //Graphics g = this.CreateGraphics(); //this是指所有control派生出来的类，这里是个form
        //            SizeF size1 = this.CreateGraphics().MeasureString(strLine1, fnt1);

        //            if (strLine1.Contains("("))
        //            {
        //                int index = strLine1.IndexOf("(");

        //                string tempstrline11 = strLine1.Substring(0, index);
        //                string tempstrline12 = strLine1.Substring(index);

        //                SizeF siztemp1 = this.CreateGraphics().MeasureString(tempstrline11, fnt1);
        //                SizeF sizetemp2 = this.CreateGraphics().MeasureString(tempstrline12, fnt1);

        //                int pianyiX = (int)(e.CellBounds.Width - siztemp1.Width - sizetemp2.Width) / 2;
        //                if (e.ColumnIndex == 5)
        //                {
        //                    TextRenderer.DrawText(e.Graphics, tempstrline11, fnt1, new Rectangle(intX + pianyiX, intY, intWidth, (int)siztemp1.Height),
        //                        Color.OrangeRed, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);

        //                }
        //                else
        //                {
        //                    TextRenderer.DrawText(e.Graphics, tempstrline11, fnt1, new Rectangle(intX + pianyiX, intY, intWidth, (int)siztemp1.Height),
        //                        Color.Black, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);

        //                }

        //                Font tempfont2 = new System.Drawing.Font("微软雅黑", 10F * Math.Min(MainModel.hScale, MainModel.wScale));

        //                TextRenderer.DrawText(e.Graphics, tempstrline12, tempfont2, new Rectangle(intX + (int)siztemp1.Width + pianyiX, intY, intWidth, (int)siztemp1.Height),
        //                  Color.DimGray, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);

        //            }
        //            else
        //            {
        //                if (e.ColumnIndex == 4)
        //                {
        //                    //第一行
        //                    TextRenderer.DrawText(e.Graphics, strLine1, fnt1, new Rectangle(intX + (int)(e.CellBounds.Width - size1.Width) / 2, intY, intWidth, (int)size1.Height),
        //                        Color.OrangeRed, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);
        //                }
        //                else
        //                {
        //                    //第一行
        //                    TextRenderer.DrawText(e.Graphics, strLine1, fnt1, new Rectangle(intX + (int)(e.CellBounds.Width - size1.Width) / 2, intY, intWidth, (int)size1.Height),
        //                        Color.Black, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);
        //                }

        //            }


        //            //第二行
        //            Font fnt2 = new System.Drawing.Font("微软雅黑", 10F * Math.Min(MainModel.hScale, MainModel.wScale));
        //            bool isstrickout = false;
        //            if (strLine2.Contains("strikeout"))
        //            {
        //                isstrickout = true;
        //                fnt2 = new System.Drawing.Font("微软雅黑", 10F * Math.Min(MainModel.hScale, MainModel.wScale), FontStyle.Strikeout);
        //                strLine2 = strLine2.Replace("strikeout", "");
        //            }
        //            SizeF size2 = this.CreateGraphics().MeasureString(strLine2, fnt2);
        //            intY = intY + (int)size1.Height;

        //            if (strLine2.Contains("("))
        //            {
        //                int index = strLine2.IndexOf("(");

        //                string tempstrline21 = strLine2.Substring(0, index);
        //                string tempstrline22 = strLine2.Substring(index);

        //                SizeF siztemp1 = this.CreateGraphics().MeasureString(tempstrline21, fnt2);
        //                SizeF sizetemp2 = this.CreateGraphics().MeasureString(tempstrline22, fnt2);

        //                int pianyiX = (int)(e.CellBounds.Width - siztemp1.Width - sizetemp2.Width) / 2;
        //                //第一行
        //                TextRenderer.DrawText(e.Graphics, tempstrline21, fnt2, new Rectangle(intX + pianyiX, intY, intWidth, (int)siztemp1.Height),
        //                    Color.DimGray, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);

        //                Font tempfont2 = new System.Drawing.Font("微软雅黑", 10F * Math.Min(MainModel.hScale, MainModel.wScale));

        //                TextRenderer.DrawText(e.Graphics, tempstrline22, fnt2, new Rectangle(intX + (int)siztemp1.Width + pianyiX, intY, intWidth, (int)size2.Height),
        //                Color.DimGray, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);

        //            }
        //            else
        //            {
        //                TextRenderer.DrawText(e.Graphics, strLine2, fnt2, new Rectangle(intX + (int)(e.CellBounds.Width - size2.Width) / 2, intY, intWidth, (int)size2.Height),
        //                Color.DimGray, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);
        //            }


        //            //int y = intY + (int)size2.Height + e.CellStyle.Padding.Top + e.CellStyle.Padding.Bottom + dgvGood.RowTemplate.Height;
        //            int y = (e.RowIndex + 1) * dgvGood.RowTemplate.Height + dgvGood.ColumnHeadersHeight - 1;

        //            //Point point1 = new Point(0, y);
        //            ////Point point2 = new Point(e.CellBounds.Width, y);
        //            //Point point2 = new Point(dgvGood.Width, y);
        //            //Pen blackPen = new Pen(Color.Black, 1);
        //            //e.Graphics.DrawLine(blackPen, point1, point2);


        //            // dgv2.Rows[e.RowIndex].Height = (int)size1.Height+(int)size2.Height*2 + e.CellStyle.Padding.Top + e.CellStyle.Padding.Bottom+1;
        //            e.Handled = true;

        //            dgvGood.ClearSelection();
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //        this.Activate();
        //    }
        //}


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
                if (keyData == Keys.Enter || keyData == Keys.Space || scancode.ToString().Length>=13)
                {
                    QueueScanCode.Enqueue(scancode.ToString());
                    scancode = new StringBuilder();
                    return false;
                }
                return false;
                // return base.ProcessDialogKey(keyData);
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

                    Delay.Start(200);
                    dataGridViewCellStyle1.BackColor = color;
                    dgvGood.Rows[FlashIndex].DefaultCellStyle = dataGridViewCellStyle1;


                    FlashIndex = 0;
                    FlashSkuCode = "";
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("突出新增商品异常" + ex.Message);
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

                productbll.AddProduct(GetAllProdcut(1, 200),MainModel.URL);

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
            //启动增量商品同步线程
            Thread threadLoadIncrementProduct = new Thread(LoadIncrementProduct);
            threadLoadIncrementProduct.IsBackground = false;
            threadLoadIncrementProduct.Start();

            //每10分钟刷新一次电视屏数据信息
               Thread threadLoadTVSkus = new Thread(LoadTVSkus);
            threadLoadTVSkus.IsBackground = false;
            threadLoadTVSkus.Start();

            Thread threadLoadExpense = new Thread(UpLoadExpenses);
            threadLoadExpense.IsBackground = false;
            threadLoadExpense.Start();

            //启动全量商品同步线程
            Thread threadLoadPromotion = new Thread(serverutil.UpdatePromotion);
            threadLoadPromotion.IsBackground = false;
            threadLoadPromotion.Start();
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
                List<DBPRODUCT_BEANMODEL> lstpro = GetIncrementProdcut(1, 100);
                if (lstpro != null && lstpro.Count > 0)
                {
                    productbll.AddProduct(lstpro, MainModel.URL);
                    LogManager.WriteLog("添加增量商品数量："+lstpro.Count);
                }
                

                

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("更新增量商品异常" + ex.Message);
            }

        }

        List<DBPRODUCT_BEANMODEL> lstIncrementproduct = new List<DBPRODUCT_BEANMODEL>();
        private List<DBPRODUCT_BEANMODEL> GetIncrementProdcut(int page, int size)
        {
            try
            {
                string errormesg = "";
                int ResultCode = -1;
                AllProduct allproduct = httputil.QuerySkushopIncrement(MainModel.CurrentShopInfo.shopid, page, size, ref errormesg,ref ResultCode);

                if (!string.IsNullOrEmpty(errormesg) || allproduct == null)
                {
                   
                    //代表超过增量更新上限，需要全量更新
                    if (ResultCode == 160046)
                    {                        
                        Thread threadLoadAllProduct = new Thread(LoadAllProduct);
                        threadLoadAllProduct.IsBackground = false;
                        threadLoadAllProduct.Start();
                    }
                    else
                    {
                        LogManager.WriteLog("更新增量商品异常： " + errormesg + ResultCode);

                    }

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

        private DBFEE_BEANBLL feebll = new DBFEE_BEANBLL();
        private DBEXPENSE_BEANBLL expensebll = new DBEXPENSE_BEANBLL();
        private void UpLoadExpenses()
        {

            try
            {
                string ErrorMsg = "";
                List<ExpenseType> lstexpensetype = httputil.GetExpenses(ref ErrorMsg);


                if (!string.IsNullOrEmpty(ErrorMsg) || lstexpensetype == null)
                {
                    MainModel.ShowLog(ErrorMsg, false);
                }
                else
                {
                    feebll.DeleteAll();
                    foreach (ExpenseType dat in lstexpensetype)
                    {

                        DBFEE_BEANMODEL dbfee = new DBFEE_BEANMODEL();
                        dbfee.FEE_ID = dat.id;
                        dbfee.DISCRIPTION = dat.description;
                        dbfee.TYPE = dat.type;
                        dbfee.VISIBLEFLAG = dat.visibleflag;
                        dbfee.INPUTFLAG = dat.inputflag;
                        dbfee.CREATE_URL_IP = MainModel.URL;

                        feebll.Add(dbfee);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("更新费用项目异常"+ex.StackTrace);
            }

            try
            {
                List<DBEXPENSE_BEANMODEL> LstExpense = expensebll.GetModelList(" TYPE = '0' and CREATEURLIP ='"+MainModel.URL+"'");

                if (LstExpense != null && LstExpense.Count > 0)
                {
                    foreach (DBEXPENSE_BEANMODEL expense in LstExpense)
                    {
                        string ErrorMsg = "";
                        bool result = false;
                        result = httputil.SaveExpense(expense.EXPENSEID,expense.EXPENSEFEE, expense.SHOPID, ref ErrorMsg);
                        if (result)
                        {
                            expense.TYPE = "1";
                            expense.SYNTIME = Convert.ToInt64(MainModel.getStampByDateTime(DateTime.Now));
                            expensebll.Update(expense);
                        }
                        else
                        {
                            LogManager.WriteLog("ERROR","提交离线营业外支出异常"+ErrorMsg);
                        }
                    }
                   
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("提交本地营业外支出异常" + ex.StackTrace);
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
                scalebll.AddScalse(GetScale(1, 100),MainModel.URL);

               // LogManager.WriteLog("添加电子秤数据时间" + (DateTime.Now - starttime).TotalMilliseconds);
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



        public void btnWindows_Click(object sender, EventArgs e)
        {
            if (!IsEnable)
            {
                return;
            }
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

                //if (isshow)
                //{
                //    LoadingHelper.ShowLoadingScreen();
                //}
                //else
                //{
                //    LoadingHelper.CloseForm();
                //}
                if (this.IsHandleCreated)
                {
                    this.Invoke(new InvokeHandler(delegate()
                     {
                         picLoading.Visible = isshow;
                        // lblLoading.Visible = isshow;
                     }));
                }
                else
                {
                    picLoading.Visible = isshow;
                   // lblLoading.Visible = isshow;
                }
                
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("显示等待异常" + ex.Message);
            }
        
        }



        #region 解决闪烁问题
        //protected override void WndProc(ref Message m)
        //{
        //    if (m.Msg == 0x0014) // 禁掉清除背景消息
        //        return;
        //    base.WndProc(ref m);
        //}

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
            while (true)
            {
                try
                {

                    if ( MainModel.IsPlayer && this.WindowState!=FormWindowState.Minimized && IsEnable)
                    {
                        MainModel.IsPlayer = false;
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


        //初始化弹窗页面，提前加载 防止加载资源影藏页面  防止加载时速度慢闪屏
        private void LoadFormIni()
        {
            try {
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




            }
            catch
            {
                LogManager.WriteLog("初始化静态页面异常");
            }
        }



        private void HttpServerStart()
        {
            try
            {
                LoadTVSkus();

                HttpServer httpServer;
                httpServer = new MyHttpServer(8080);
                Thread threadHttpServer = new Thread(new ThreadStart(httpServer.listen));
                threadHttpServer.IsBackground = false;
                threadHttpServer.Start();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("启动电视屏服务异常："+ex.Message);
            }
        }


        private void LoadTVSkus()
        {
            try
            {
                string ErrorMsg = "";
                if (string.IsNullOrEmpty(ErrorMsg))
                {
                    MainModel.PromotionJson = httputil.GetTVshowpageForPromotion(ref ErrorMsg);

                }

                if (string.IsNullOrEmpty(ErrorMsg))
                {
                    MainModel.PorkJson = httputil.GetTVshowpageForPork(ref ErrorMsg);

                }
                MainModel.TVPromotionSkus = httputil.GetPromotionSkus(ref ErrorMsg);
                MainModel.TVSingleActivesSku = httputil.GetActiveSkus(ref ErrorMsg);

                int activecount =60;
                string exitsSkucode = "";
                if (MainModel.TVSingleActivesSku != null && MainModel.TVSingleActivesSku.posactiveskudetails.Count != 0)
                {
                    activecount = 60 - MainModel.TVSingleActivesSku.posactiveskudetails.Count;
                    List<string> lstresult = MainModel.TVSingleActivesSku.posactiveskudetails.Select(t => t.skucode).ToList();
                    foreach (string str in lstresult)
                    {
                        exitsSkucode += "'" + str + "',";
                    }
                    exitsSkucode = exitsSkucode.TrimEnd(',');
                }
                else
                {
                    if (MainModel.TVSingleActivesSku == null)
                    {
                        MainModel.TVSingleActivesSku = new PosActivesSku();
                    }
                    MainModel.TVSingleActivesSku.posactiveskudetails = new List<TVProduct>();
                }

                string strwhere = " STATUS =1 and CREATE_URL_IP='" + MainModel.URL + "' and CATEGORYID like '010%' and skucode not in(" + exitsSkucode + ") ";

                String[] notlikes = { "结算专用", "(", "A", "B", "C", "D", "E", "F", "G", "H", "I", "G", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
                if (notlikes != null && notlikes.Length > 0)
                {
                    for (int i = 0; i < notlikes.Length; i++)
                    {
                            strwhere += " AND SKUNAME NOT LIKE '%" + notlikes[i] + "%'";
                    }
                }

                strwhere+=" order by saleprice desc limit "+activecount;
                List<DBPRODUCT_BEANMODEL> lstpro = productbll.GetModelList(strwhere);
                if(lstpro!=null && lstpro.Count>0){
                    foreach (DBPRODUCT_BEANMODEL dbpro in lstpro)
                    {  
                        TVProduct tvpro = new TVProduct();
                        tvpro.categoryid = dbpro.SKUCODE;
                        tvpro.originalprice = dbpro.ORIGINPRICE;
                        tvpro.promotionprice = dbpro.SALEPRICE;
                        tvpro.saleunit = dbpro.SALESUNIT;
                        tvpro.skucode = dbpro.SKUCODE;
                        tvpro.skuname = dbpro.SKUNAME;
                        tvpro.weightflag = dbpro.WEIGHTFLAG == 1 ? true : false;

                        MainModel.TVSingleActivesSku.posactiveskudetails.Add(tvpro);
                    }
                }

                string prokwhere = " STATUS =1 and CREATE_URL_IP='" + MainModel.URL + "' and CATEGORYID like '030%' ";
                String[] notlikespork = { "结算专用", "(", "A", "C", "D", "E", "F", "G", "H", "I", "G", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
                prokwhere = "CATEGORYID LIKE '030%'";
                if (notlikes != null && notlikespork.Length > 0)
                {
                    for (int i = 0; i < notlikespork.Length; i++)
                    {
                        prokwhere += " AND SKUNAME NOT LIKE '%" + notlikespork[i] + "%'";
                    }
                }
                prokwhere += " order by saleprice desc limit 30";
                List<DBPRODUCT_BEANMODEL> lstporkpro = productbll.GetModelList(prokwhere);
                if (lstporkpro != null && lstporkpro.Count > 0)
                {
                    MainModel.TVPorkSkus = new PosActivesSku();
                    MainModel.TVPorkSkus.posactiveskudetails = new List<TVProduct>();
                
                    foreach (DBPRODUCT_BEANMODEL dbpro in lstporkpro)
                    {

                        TVProduct tvpro = new TVProduct();
                        tvpro.categoryid = dbpro.SKUCODE;
                        tvpro.originalprice = dbpro.ORIGINPRICE;
                        tvpro.promotionprice = dbpro.SALEPRICE;
                        tvpro.saleunit = dbpro.SALESUNIT;
                        tvpro.skucode = dbpro.SKUCODE;
                        tvpro.skuname = dbpro.SKUNAME;
                        tvpro.weightflag = dbpro.WEIGHTFLAG == 1 ? true : false;

                        MainModel.TVPorkSkus.posactiveskudetails.Add(tvpro);
                    }
                }


            }
            catch (Exception ex)
            {
                LogManager.WriteLog("加载电视屏数据异常"+ex.Message);
            }
        }

        private void dgvGood_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                Console.WriteLine("滚动条");
                MainModel.frmmainmedia.UpDgvScorll(dgvGood.FirstDisplayedScrollingRowIndex);
            }
        }

        SYSTEM_USER_BEANBLL userbll = new SYSTEM_USER_BEANBLL();
        DBPROMOTION_CACHE_BEANBLL promotionbll = new DBPROMOTION_CACHE_BEANBLL();

        ServerDataUtil serverutil = new ServerDataUtil();
        private void button1_Click(object sender, EventArgs e)
        {

            serverutil.UpdatePromotion();
            //string refstr = "";
            //List<DBPROMOTION_CACHE_BEANMODEL> lsttest =  httputil.QueryPromotionAll(MainModel.CurrentShopInfo.shopid,0,50,ref refstr);

            //promotionbll.AddPromotion(lsttest,MainModel.URL);
            //string test = MainModel.getStampByDateTime(DateTime.Now);
            //MessageBox.Show(test);
            //Int64 testlong = Convert.ToInt64(test);

            //MessageBox.Show(testlong.ToString());
            //UploadPosUser();
            //UploadOffLineOrder();
            UploadOffLineReceiptr();
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

        private void UploadPosUser()
        {
            try
            {

                List<OffLineUser> lstoffuser = new List<OffLineUser>();
                List<SYSTEM_USER_BEANMODEL> lstuser = userbll.GetModelList(" CREATE_URL_IP = '"+MainModel.URL+"'");

                if (lstuser != null && lstuser.Count > 0)
                {
                    foreach (SYSTEM_USER_BEANMODEL user in lstuser)
                    {
                        OffLineUser offuser = new OffLineUser();
                        offuser.loginaccount = user.LOGINACCOUNT;
                        offuser.nickname = user.NICKNAME;
                        lstoffuser.Add(offuser);
                    }
                }
                else
                {
                  //  return;
                }
                string errormsg = "";
                List<OffLineUser> lstresultoffuser =  httputil.PosUser(lstoffuser, ref errormsg);

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
                LogManager.WriteLog("添加离线员工信异常"+ex.Message );
            }
        }

        DBORDER_BEANBLL orderbll = new DBORDER_BEANBLL();
        private void UploadOffLineOrder()
        {
            try
            {

                List<DBORDER_BEANMODEL> lstorder = orderbll.GetModelList(" SYN_TIME=null or SYN_TIME=0");

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
                            LogManager.WriteLog("离线订单同步完成"+order.OFFLINEORDERID);
                            order.SYN_TIME = Convert.ToInt64(MainModel.getStampByDateTime(DateTime.Now));
                            orderbll.Update(order);
                        }
                        else
                        {
                            LogManager.WriteLog("离线订单同步失败" + order.OFFLINEORDERID+"  "+errormsg);
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

                List<DBORDER_BEANMODEL> lstorder = orderbll.GetModelList(" REFUND_TIME> SYN_TIME");

                if (lstorder != null && lstorder.Count > 0)
                {
                    foreach (DBORDER_BEANMODEL order in lstorder)
                    {
                        OffLineOrder offlineorder = JsonConvert.DeserializeObject<OffLineOrder>(order.ORDER_JSON);

                        //if (order.ORDERSTATUSVALUE == 5)
                        //{
                        //    offlineorder.hasrefunded = 1;
                        //}

                        offlineorder.hasrefunded = 1;
                        string errormsg = "";
                        bool result = httputil.OffLineRefund(offlineorder, ref errormsg);

                        order.SYN_TIME = Convert.ToInt64(MainModel.getStampByDateTime(DateTime.Now));
                        orderbll.Update(order);

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


        DBRECEIPT_BEANBLL receiptbll = new DBRECEIPT_BEANBLL();
        private void UploadOffLineReceiptr()
        {
            try
            {

                List<DBRECEIPT_BEANMODEL> lstreceipt = receiptbll.GetModelList(" SYN_TIME=null or SYN_TIME=0");

                if (lstreceipt != null && lstreceipt.Count > 0)
                {
                    foreach (DBRECEIPT_BEANMODEL receipt in lstreceipt)
                    {
                        Receiptdetail  receiptdetail = JsonConvert.DeserializeObject<Receiptdetail>(receipt.RECEIPTDETAIL);

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


        private void ChangeMQTT( object obj)
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

    }
}
