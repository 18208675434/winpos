using Maticsoft.BLL;
using Maticsoft.Model;
using Newtonsoft.Json;
using WinSaasPOS.Common;
using WinSaasPOS.Model;
using WinSaasPOS.Model.Promotion;
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

namespace WinSaasPOS
{
    public partial class frmMainOffLine : Form
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
      //  HttpUtil httputil = new HttpUtil();

        //第三方支付页面
        frmOnLinePayResult frmonlinepayresult = null;

        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();

        private Bitmap btnorderhangimage;

        private frmLoginOffLine CurrentFrmLoginOffLine;

        private Bitmap bmpdelete;
        private Bitmap bmpMinus;
        private Bitmap bmpAdd;
        private Bitmap bmpWhite;
        private Bitmap bmpEmpty;
        /// <summary>
        /// this.enable=false; 页面不可用页面不可控；  通过该标志控制页面是否可用
        /// </summary>
        private  bool IsEnable=true;
        DBPRODUCT_BEANBLL productbll = new DBPRODUCT_BEANBLL();

        private ImplOfflineSingleCalculateNew singlecalculate = new ImplOfflineSingleCalculateNew(MainModel.CurrentShopInfo.tenantid,MainModel.CurrentShopInfo.shopid);


        private ImplOfflineOrderPromotion ordercalculate = new ImplOfflineOrderPromotion(MainModel.CurrentShopInfo.tenantid, MainModel.CurrentShopInfo.shopid);



        //扫描数据处理线程
        Thread threadScanCode;


        //刷新焦点线程  防止客屏播放视频抢走焦点
        Thread threadCheckActivate;

        //启动电子秤同步信息线程
        Thread threadLoadFrmIni;


        //启动电视屏服务
        Thread threadServerStart;

        /// <summary>
        /// 用于终止监听线程
        /// </summary>
        bool IsRun = true;
        #endregion

        #region  页面加载
        public frmMainOffLine(frmLoginOffLine frmlogin)
        {
            InitializeComponent();

            //使用委托的话frmmain界面会卡死
            Control.CheckForIllegalCrossThreadCalls = false;

            MainModel.wScale = (float)Screen.AllScreens[0].Bounds.Width / this.Width;
            MainModel.hScale = (float)Screen.AllScreens[0].Bounds.Height / this.Height;
            MainModel.midScale = (MainModel.wScale + MainModel.hScale) / 2;
            //ShowLoading(false);// LoadingHelper.CloseForm();

            //防止标品加减框变形
            btnIncrease.Size = new System.Drawing.Size(35,35);
            btnNum.Size = new System.Drawing.Size(90,35);
            btnNum.Left = pnlNum.Width - btnNum.Width + 3;
            
            //btnIncrease.Width = btnIncrease.Height;

            CurrentFrmLoginOffLine = frmlogin;

            LoadingHelper.ShowLoadingScreen("页面初始化...");

        }
        private void frmMain_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            MainModel.IsOffLine = true;
            CurrentFrmLoginOffLine.Hide();
            LoadingHelper.CloseForm();

            LoadCart();
            string offlinestarttime= INIManager.GetIni("OffLine","StartTime",MainModel.IniPath);
            if (string.IsNullOrEmpty(offlinestarttime))
            {
                INIManager.SetIni("OffLine", "StartTime",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), MainModel.IniPath);
            }
           
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
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
              
                SetBtnPayStarus(false);


                timerClearMemory.Interval = 8 * 60 * 1000;
                timerClearMemory.Enabled = true;

                lblTime.Text = MainModel.Titledata;
                lblShopName.Text = MainModel.CurrentShopInfo.shopname;
                btnOnLineType.Left = lblShopName.Left + lblShopName.Width + 10;
                //∨ 从右往左排列 被当成图形   从左向右 右侧间距太大
                btnMenu.Text = MainModel.CurrentUser.nickname + "，你好 ∨";

                btnMenu.Left = Math.Max(pnlHead.Width - btnMenu.Width - 10, btnOrderQuery.Left + btnOrderQuery.Width);


                //扫描数据处理线程
                threadScanCode = new Thread(ScanCodeThread);
                threadScanCode.IsBackground = true;
                threadScanCode.Start();

                //刷新焦点线程  防止客屏播放视频抢走焦点
                threadCheckActivate = new Thread(CheckActivate);
                threadCheckActivate.IsBackground = true;
                threadCheckActivate.Start();

                //启动电子秤同步信息线程
                threadLoadFrmIni = new Thread(LoadFormIni);
                threadLoadFrmIni.IsBackground = true;
                threadLoadFrmIni.Start();


                //启动电视屏服务
                threadServerStart = new Thread(HttpServerStart);
                threadServerStart.IsBackground = true;
                threadServerStart.Start();


                btnorderhangimage = new Bitmap(btnOrderHang.Image, 10, 10);
                UpdateOrderHang();


               // Application.DoEvents();
                CheckPrint();

                //控制按钮图片大小，防止与按钮文字异常
                try
                {                  
                    int topsize = Convert.ToInt16(btnPayByCash.Height * (MainModel.hScale - 1) / 3 / MainModel.hScale);

                    btnPayByCash.Padding = new System.Windows.Forms.Padding(0, topsize, 0, topsize);                   
                    bmpdelete = new Bitmap(picDelete.Image, dgvGood.RowTemplate.Height * 26 / 100, dgvGood.RowTemplate.Height * 26 / 100);

                    picLoading.Size = new Size(55, 55);
                    pnlPriceLine.Height = 1;
                    bmpAdd = new Bitmap(Resources.ResourcePos.add, dgvGood.RowTemplate.Height * 44 / 100, dgvGood.RowTemplate.Height * 44 / 100);
                    bmpEmpty = Resources.ResourcePos.empty;                  
                }
                catch { }
              
              
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
            SaveCart();
            try { threadScanCode.Abort(); }
            catch { }

            try { threadCheckActivate.Abort(); }
            catch { }

            try { threadLoadFrmIni.Abort(); }
            catch { }

            try { threadServerStart.Abort(); }
            catch { }


            try { MainModel.frmnumber.Dispose(); }
            catch { } MainModel.frmnumber = null; 
            try { MainModel.frmcashpay.Dispose(); }
            catch { } MainModel.frmcashpay = null;
            try { MainModel.frmcashpayoffline.Dispose(); }
            catch { } MainModel.frmcashpayoffline = null;
            try { MainModel.frmcashcoupon.Dispose();  }
            catch { } MainModel.frmcashcoupon = null;
            try { MainModel.frmtoolmain.Dispose(); }
            catch { } MainModel.frmtoolmain = null; 
            try { MainModel.frmmodifyprice.Dispose();  }
            catch { } MainModel.frmmodifyprice = null;
            try { MainModel.frmprintersetting.Dispose();  }
            catch { } MainModel.frmprintersetting = null;
            try { MainModel.frmloading.Dispose();  }
            catch { } MainModel.frmloading = null;

            try
            {
                MainModel.frmmainmedia.Close();

            }
            catch { }
            MainModel.frmmainmedia = null;
            MainModel.ShowTask();
           // CurrentFrmLoginOffLine.Show();

            this.Dispose();
        }


        JSON_BEANBLL jsonbll = new JSON_BEANBLL();
        private void SaveCart()
        {
            try
            {
                jsonbll.Delete(ConditionType.CurrentCart);
                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                {

                  
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
                LogManager.WriteLog("保存购物车信息异常" + ex.Message);
            }
        }

        private void LoadCart()
        {
            try
            {              
                    JSON_BEANMODEL jsonbmodel = jsonbll.GetModel(ConditionType.CurrentCart);
                    jsonbll.Delete(ConditionType.CurrentCart);
                    if (jsonbmodel != null && jsonbmodel.CREATE_URL_IP == MainModel.URL)
                    {
                        Cart lastCart = JsonConvert.DeserializeObject<Cart>(jsonbmodel.JSON);
                        lastCart.promoamt = 0;
                        lastCart.fixpricepromoamt = 0;
                        foreach (Product pro in lastCart.products)
                        {
                            pro.pricetagid = 0;
                            pro.price.saleprice = pro.price.originprice;
                        }

                        CurrentCart = lastCart;
                        //CurrentCart = lastCart;
                        UploadDgvGoods(CurrentCart);
                    }
                
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("加载购物车信息异常" + ex.Message);
            }
        }

        
        bool isshowpic = false;

        //定时清理内存
        private void timerClearMemory_Tick(object sender, EventArgs e)
        {
            
            try { Other.CrearMemory(); }
            catch(Exception ex) {
                LogManager.WriteLog("清理内存异常"+ex.Message);
            }
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

                        CurrentFrmLoginOffLine.Show();
                        this.Close();
                        LoadPicScreen(false);
                    }
                    else if (resultcode == MainModel.HttpMemberExpired || resultcode == MainModel.DifferentMember)
                    {
                        ShowLoading(false);// LoadingHelper.CloseForm();
                        MainModel.CurrentMember = null;

                       
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
                CurrentFrmLoginOffLine.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("退出系统异常"+ex.Message);
            }
            finally
            {
               // LoadPicScreen(false);
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
                    ReceiptUtil.EditCancelOrder(1, CurrentCart.origintotal);
                }
                catch (Exception ex) { }


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

                        ClearForm();


                        ShowLog("已挂单", false);






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


                        UploadDgvGoods(CurrentCart);
                    //foreach (Product pro in frmorderhang.CurrentCart.products)
                    //{
                    //    scancodememberModel tempscancode = new scancodememberModel();

                    //    Scancodedto tempscancodedto = new Scancodedto();

                    //    tempscancodedto.skucode = pro.skucode;
                    //    tempscancodedto.num = pro.num;
                    //    tempscancodedto.specnum = pro.specnum;
                    //    tempscancodedto.spectype = pro.spectype;
                    //    tempscancodedto.weightflag = Convert.ToBoolean(pro.goodstagid);
                    //    tempscancodedto.barcode = pro.barcode;

                    //    tempscancode.scancodedto = tempscancodedto;
                    //    //QueueScanCode.Enqueue(pro.barcode);
                    //}
                   
                    }

                    //RefreshCart();
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
                        orderpath = MainModel.OffLineOrderPath + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + MainModel.CurrentMember.memberheaderresponsevo.mobile + ".order";
                    }
                    else
                    {
                        orderpath = MainModel.OffLineOrderPath + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + "" + ".order";
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
                    DirectoryInfo di = new DirectoryInfo(MainModel.OffLineOrderPath);
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

            asf.AutoScaleControlTest(frmtool, 178, 370, Convert.ToInt32(MainModel.wScale * 178), Convert.ToInt32(MainModel.hScale * 370), true);
            frmtool.DataReceiveHandle += frmToolMain_DataReceiveHandle;
            frmtool.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width - frmtool.Width - 15, pnlHead.Height + 10);
            frmtool.Show();     
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

        /// <summary>
        /// 本地订单表操作类
        /// </summary>
        private DBORDER_BEANBLL orderbll = new DBORDER_BEANBLL();

        private DBRECEIPT_BEANBLL receiptbll = new DBRECEIPT_BEANBLL(); 
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

                


                IsEnable = false;
                Receiptdetail receiptdetail = ReceiptUtil.GetReceiptDetailOffLine();
                IsEnable = true;
                //if (receiptdetail == null)
                //{
                //    MainModel.ShowLog("交班失败",true);
                //    return;
                //}

               FrmConfirmReceiptBack frmconfirmreceiptback = new FrmConfirmReceiptBack(receiptdetail);
               frmconfirmreceiptback.Location = new Point(0, 0);
               frmconfirmreceiptback.ShowDialog();

               CurrentFrmLoginOffLine.Show();
               CurrentCart = null;
               this.Close();

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("交班出现异常"+ex.Message);
                
            }
            finally
            {
               // LoadPicScreen(false);
                IsEnable = true;
                ShowLoading(false);// LoadingHelper.CloseForm();
            }
        }


        private string GetOffLineReceiptID(Receiptdetail receiptdetail)
        {
            try
            {
                //订单号：取当时时间戳+设备SN+门店ID+登录离线店员手机号+该订单总价+现金支付价+找零价+抹分价+实际订单对象hashcode+订单对象hashcode，生成 后的订单hashcode+4位随机数,生成后的订单号去掉"-"为本次生成的离线订单号

                string strorder = MainModel.getStampByDateTime(DateTime.Now) + MainModel.DeviceSN + MainModel.CurrentUserPhone + receiptdetail.GetHashCode();
                return strorder.GetHashCode().ToString().Replace("-", "") + Getrandom(4);
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("获取离线订单号异常" + ex.Message, true);
            }
            return "";
        }
        private string Getrandom(int num)
        {
            Random rd = new Random();
            string result = "";
            for (int i = 0; i < num; i++)
            {
                result += rd.Next(10).ToString();
            }
            return result;
        }

        private OrderPriceDetail getOrderpriceDetail(string amount,string title,string subtitle){
            OrderPriceDetail orderprice = new OrderPriceDetail();
            orderprice.amount = amount;
            orderprice.title = title;
            orderprice.subtitle = subtitle;

            return orderprice;
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
                //this.Hide();
                if (MainModel.frmlogin != null)
            {
                try
                {
                    MainModel.frmlogin.Dispose();
                }
                catch { }
            }
            MainModel.frmlogin = new frmLogin();
            MainModel.frmlogin.Show();

            CurrentCart = null;
            this.Close();
                
                //tsmReceipt_Click(null,null);
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


        private object thislockScanCode = new object();
        private void ScanCodeThread(object obj)
        {
            while (IsRun)
            {
               
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

                        List<DBPRODUCT_BEANMODEL> lstpro = new List<DBPRODUCT_BEANMODEL>();
                        foreach (string scancode in LstScanCode)
                        {
                            DBPRODUCT_BEANMODEL dbpro = GetLocalPro(scancode);
                            if (dbpro != null)
                            {
                                lstpro.Add(dbpro);
                                
                            }
                            else
                            {
                                MainModel.ShowLog("条码不正确", false);
                                lstNotLocalCode.Add(scancode);
                            }
                        }

                       
                        ShowLoading(false);// LoadingHelper.CloseForm();


                        if (this.IsHandleCreated)
                        {
                            this.Invoke(new InvokeHandler(delegate()
                            {
                                addcart(lstpro);
                            }));
                        }
                        else
                        {
                            addcart(lstpro);
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
                List<DBPRODUCT_BEANMODEL> lstpro = new List<DBPRODUCT_BEANMODEL>();
                string goodcode = obj.ToString();

                string ErrorMsg = "";
                int ResultCode = 0;

                DBPRODUCT_BEANMODEL dbpro = productbll.GetModelByGoodsID(goodcode.PadLeft(5,'0'),MainModel.URL);
                if (dbpro == null)
                {
                    MainModel.ShowLog("条码不正确", false);
                }
                else
                {
                    dbpro.NUM = 1;
                    lstpro.Add(dbpro);
                    addcart(lstpro);
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


        private DBPRODUCT_BEANMODEL GetLocalPro(string goodcode)
        {
            try
            {

                DBPRODUCT_BEANMODEL dbpro = null;

                bool isINNERBARCODE = false;
                string skucode = "";
                int pronum = 0;
                if (goodcode.Length == 18 && !WinSaasPOS.BaseUI.MainHelper.checkEanCodeIsError(goodcode, 18) && (goodcode.Substring(0, 2) == "25" || goodcode.Substring(0, 2) == "26"))
                {
                    List<DBPRODUCT_BEANMODEL> lstdbpro = productbll.GetModelList(" INNERBARCODE='" + goodcode.Substring(2, 10) + "' " + " and CREATE_URL_IP='" + MainModel.URL + "' ");
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
                    List<DBPRODUCT_BEANMODEL> lstdbpro = productbll.GetModelList(" BARCODE='" + goodcode + "'" + " and CREATE_URL_IP='" + MainModel.URL + "' ");
                    if (lstdbpro != null && lstdbpro.Count > 0)
                    {
                        dbpro = lstdbpro[0];
                    }
                }

                if (dbpro != null)
                {


                    if (goodcode.Length == 18 && !WinSaasPOS.BaseUI.MainHelper.checkEanCodeIsError(goodcode, 18))
                {

                    if (dbpro.WEIGHTFLAG == 1)
                    {
                        if (isINNERBARCODE)
                        {
                            int num = Convert.ToInt32(goodcode.Substring(goodcode.Length - 6, 5));
                            decimal decimalnum = (decimal)num / 1000;

                            dbpro.SPECNUM = decimalnum;
                            dbpro.NUM = 1;
                        }
                        else
                        {
                            dbpro.NUM = 1;
                        }

                    }
                    else
                    {
                        if (isINNERBARCODE)
                        {
                            int num = Convert.ToInt32(goodcode.Substring(goodcode.Length - 6, 5));

                            dbpro.NUM = num;
                        }
                        else
                        {
                            dbpro.NUM = 1;
                        }

                        dbpro.SPECNUM = 1;
                    }
                }
                else
                {
                    dbpro.NUM = 1;
                }

                }

                //防止重量为0的条码
                if (dbpro != null && dbpro.WEIGHTFLAG == 1 && dbpro.SPECNUM == 0)
                {
                    dbpro = null;
                }
                return dbpro;

              
            }
            catch (Exception ex)
            {
               // MainModel.ShowLog("条码验证异常"+ex.Message ,true );
                LogManager.WriteLog("条码验证异常" + ex.Message);
                return null;
            }
        }



        private void addcart( List<DBPRODUCT_BEANMODEL> lstpro)
        {

                try
                {
                    foreach (DBPRODUCT_BEANMODEL dbpro in lstpro)
                    {


                    if (dbpro.WEIGHTFLAG==1 && dbpro.SPECNUM==0)
                    {
                        frmNumberBack frmnumberback = new frmNumberBack(dbpro.SKUNAME, NumberType.ProWeight, ShowLocation.Center);
                        
                        frmnumberback.Location = new Point(0, 0);
                        frmnumberback.ShowDialog();

                        if (frmnumberback.DialogResult == DialogResult.OK)
                        {
                            dbpro.SPECNUM = (decimal)frmnumberback.NumValue / 1000;
                            dbpro.NUM = 1;
                        }
                        else
                        {
                            Application.DoEvents();
                            return;
                        }
                        Application.DoEvents();
                    }

                    Product pro = new Product();
                    pro.title = dbpro.SKUNAME;
                    pro.skucode = dbpro.SKUCODE;
                    pro.num = (int)dbpro.NUM;
                    pro.specnum = dbpro.SPECNUM;
                    pro.spectype = dbpro.SPECTYPE;
                    pro.goodstagid = dbpro.WEIGHTFLAG;
                    pro.barcode = dbpro.BARCODE;
                    pro.weightflag = Convert.ToBoolean(dbpro.WEIGHTFLAG);
                    pro.firstcategoryid = dbpro.FIRSTCATEGORYID;
                    pro.secondcategoryid = dbpro.SECONDCATEGORYID;
                    pro.categoryid = dbpro.SECONDCATEGORYID;
                    Price price = new Price();
                    price.saleprice = dbpro.SALEPRICE;
                    price.originprice = dbpro.SALEPRICE;
                    price.specnum = dbpro.SPECNUM;
                    price.unit = dbpro.SALESUNIT; 

                    pro.price = price;


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
                    UploadDgvGoods(CurrentCart);

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


        private object thislockDgvGood = new object();

        private void UploadDgvGoods(Cart cart)
        {
            lock (thislockDgvGood)
            {
                try
                {
                    //初始化 防止优惠过的商品 增减重新计算

                    if (cart != null && cart.products != null && cart.products.Count > 0)
                    {
                        foreach (Product singlepro in cart.products)
                        {

                            singlepro.offlinepromos = new List<OffLinePromos>();
                            if (!singlepro.weightflag)
                            {
                                singlepro.price.total = Math.Round(singlepro.num * singlepro.price.saleprice, 2, MidpointRounding.AwayFromZero);
                                singlepro.price.origintotal = Math.Round(singlepro.num * singlepro.price.originprice, 2, MidpointRounding.AwayFromZero);
                                singlepro.PaySubAmt = Math.Round(singlepro.num * singlepro.price.saleprice, 2, MidpointRounding.AwayFromZero);
                            }
                            else
                            {
                                singlepro.price.total = Math.Round(singlepro.price.saleprice * singlepro.price.specnum, 2, MidpointRounding.AwayFromZero);
                                singlepro.price.origintotal = Math.Round(singlepro.price.originprice * singlepro.price.specnum, 2, MidpointRounding.AwayFromZero);
                                singlepro.PaySubAmt = Math.Round(singlepro.price.saleprice * singlepro.price.specnum, 2, MidpointRounding.AwayFromZero);

                            }

                            singlepro.PromoSubAmt = 0;
                            singlepro.offlinepromos = new List<OffLinePromos>();
                            
                            //singlecalculate.calculate(singlepro);
                        }

                    }

                    CurrentCart.fixpricetotal = 0;
                    CurrentCart.fixpricepromoamt = 0;
                    //ordercalculate.doAction(CurrentCart.products);

                    UploadOrderDetail();


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
                                if (temppro.goodstagid == 0 && temppro.skucode == FlashSkuCode)
                                {
                                    FlashIndex = count - i - 1;
                                }

                                List<Bitmap> lstbmp = GetDgvRow(temppro);
                                if (lstbmp != null && lstbmp.Count == 6)
                                {
                                    dgvGood.Rows.Insert(0, new object[] {(1+ i).ToString(), lstbmp[0], lstbmp[1], lstbmp[2], lstbmp[3], lstbmp[4], lstbmp[5] });
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

                            if (cart.totalpayment == 0 && cart.products != null && cart.products.Count > 0)
                            {
                                btnPayByCash.Visible = false;
                                btnPayOK.Visible = true;
                               
                               
                            }
                            else
                            {
                                btnPayByCash.Visible = true;
                                btnPayOK.Visible = false;
                            }
                        }

                        if (cart.totalpayment > 0)
                        {
                            SetBtnPayStarus(true);
                        }
                    }
                    else
                    {
                        pnlWaiting.Show();
                        SetBtnPayStarus(false);
                       // pnlPayType2.Enabled = false;
                    }
                    //Console.WriteLine("积分前" + (DateTime.Now - starttime).TotalMilliseconds);
                    CurrentCart = cart;
                  
               

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
                    btnNum.Text = (pro.price.specnum + pro.price.unit).PadRight(5,' ');
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
                    decimal cartoriginaltotal = 0;
                    decimal carttotal = 0;
                    decimal cartpromoamt = 0;
                    foreach (Product pro in CurrentCart.products)
                    {
                        cartoriginaltotal += pro.price.origintotal;
                        carttotal += pro.PaySubAmt;

                        if (pro.offlinepromos != null && pro.offlinepromos.Count > 0)
                        {
                            foreach (OffLinePromos offlinepromo in pro.offlinepromos)
                            {
                                cartpromoamt += offlinepromo.promoamt;
                            }
                        }
                        
                    }

                    CurrentCart.origintotal = Math.Round(cartoriginaltotal, 2, MidpointRounding.AwayFromZero);
                    CurrentCart.totalpayment = Math.Round(carttotal, 2, MidpointRounding.AwayFromZero);

                    //提供客屏展示信息
                    CurrentCart.orderpricedetails = new List<OrderPriceDetail>();
                    OrderPriceDetail orderdetail = new OrderPriceDetail();
                    orderdetail.title = "商品金额";
                    orderdetail.amount ="￥"+ cartoriginaltotal.ToString("f2");
                    CurrentCart.orderpricedetails.Add(orderdetail);


                    if (cartpromoamt > 0)
                    {
                        OrderPriceDetail orderdetailpro = new OrderPriceDetail();
                        orderdetailpro.title = "活动优惠";
                        orderdetailpro.amount = "￥" + cartpromoamt.ToString("f2");
                        CurrentCart.orderpricedetails.Add(orderdetailpro);

                        CurrentCart.promoamt = Math.Round(cartpromoamt, 2, MidpointRounding.AwayFromZero);
                    }
                    dgvOrderDetail.Rows.Clear();



                    dgvOrderDetail.Rows.Add("商品金额", "￥" + cartoriginaltotal.ToString("f2"));
                    if (cartpromoamt > 0)
                    {
                        dgvOrderDetail.Rows.Add("活动优惠", "￥" + cartpromoamt.ToString("f2"));
                    }


                    dgvOrderDetail.ClearSelection();


                    
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

                btnModifyPrice.BackgroundImage = Resources.ResourcePos.FixDisenable;
                btnModifyPrice.ForeColor = Color.White;


                btnMemberPromo.Visible = false;

                pnlWaiting.Show();

                dgvOrderDetail.Rows.Clear();

                btnPayByCash.Visible = true;
                btnPayOK.Visible = false;

                SetBtnPayStarus(false);
               // pnlPayType2.Enabled = false;

                
                UpdateOrderHang();

                ShowLoading(false);// LoadingHelper.CloseForm();
                Application.DoEvents();

                MainModel.frmmainmedia.IniForm(null);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("清空主界面异常" + ex.Message);
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

                    //RefreshCart();
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
                  
                    frmCashPayOffLineBack frmCashPayOffLineBack = new frmCashPayOffLineBack(CurrentCart);
                    frmCashPayOffLineBack.ShowDialog();

                    int type = frmCashPayOffLineBack.cashpaytype;
                    string orderid = frmCashPayOffLineBack.cashpayorderid;
                    if (type == 0)
                    {
                       
                    }
                    else if (type == 1) //支付完成
                    {
                        this.Invoke(new InvokeHandler(delegate()
                        {
                            frmPaySuccess frmresult = new frmPaySuccess(orderid);
                            frmresult.DataReceiveHandle += FormCashierResult_DataReceiveHandle;
                            frmresult.ShowDialog();

                            ClearForm();
                           
                        }));
                    }
                    else if (type == MainModel.HttpUserExpired || type == MainModel.HttpMemberExpired)
                    {
                        //CheckUserAndMember(type, "");
                    }

                    if (CurrentCart != null)
                    {
                        CurrentCart.cashpayoption = 0;
                        CurrentCart.cashpayamt = 0;
                    }


                    //RefreshCart();
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


        //金额为0使用
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
                string offlineorderid = "";

                if (!OrderUtil.CreaterOrder(CurrentCart, 0, ref offlineorderid))
                {
                    return;
                }


                frmPaySuccess frmresult = new frmPaySuccess(offlineorderid);
                    frmresult.DataReceiveHandle += FormCashierResult_DataReceiveHandle;
                    frmresult.ShowDialog();
                    ClearForm();

                
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("完成支付异常" + ex.Message);
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
                if (e.ColumnIndex == 4 && pro.goodstagid == 0)
                {
                    for (int i = 0; i < CurrentCart.products.Count; i++)
                    {
                        if (CurrentCart.products[i].skucode == pro.skucode)
                        {
                            CurrentCart.products[i].num += 1;
                            CurrentCart.products[i].price.total = Math.Round(CurrentCart.products[i].num * CurrentCart.products[i].price.saleprice, 2, MidpointRounding.AwayFromZero);
                            CurrentCart.products[i].price.origintotal = Math.Round(CurrentCart.products[i].num * CurrentCart.products[i].price.originprice, 2, MidpointRounding.AwayFromZero);
                            // CurrentCart.products[i].PaySubAmt = Math.Round(CurrentCart.products[i].num * CurrentCart.products[i].price.saleprice, 2, MidpointRounding.AwayFromZero);

                            break;
                        }
                    }

                    //RefreshCart();
                    UploadDgvGoods(CurrentCart);
                }
                //减少标品

                if (e.ColumnIndex == 3 && pro.goodstagid == 0)
                {
                    for (int i = 0; i < CurrentCart.products.Count; i++)
                    {
                        if (CurrentCart.products[i].skucode == pro.skucode)
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
                                else { return; }

                            }
                            else
                            {
                                ReceiptUtil.EditCancelSingle(1, CurrentCart.products[i].price.origintotal);
                                CurrentCart.products[i].num -= 1;
                                CurrentCart.products[i].price.total = Math.Round(CurrentCart.products[i].num * CurrentCart.products[i].price.saleprice, 2, MidpointRounding.AwayFromZero);
                                CurrentCart.products[i].price.origintotal = Math.Round(CurrentCart.products[i].num * CurrentCart.products[i].price.originprice, 2, MidpointRounding.AwayFromZero);
                                // CurrentCart.products[i].PaySubAmt = Math.Round(CurrentCart.products[i].num * CurrentCart.products[i].price.saleprice, 2, MidpointRounding.AwayFromZero);

                            }
                            break;
                        }
                    }
                    UploadDgvGoods(CurrentCart);
                }

                if (e.ColumnIndex == 6)
                {

                    FrmConfirmBack frmconfirmback = new FrmConfirmBack("确认删除", pro.title, pro.skucode);

                    frmconfirmback.Location = new Point(0, 0);

                    if (frmconfirmback.ShowDialog() != DialogResult.OK)
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
                    UploadDgvGoods(CurrentCart);
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
                        picScreen.Size = new System.Drawing.Size(this.Width, this.Height);
                        picScreen.Visible = true;
                    }                   
                }
                else
                {
                    picScreen.Visible = false;
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


                frmpanel.Dispose();
                //Application.DoEvents();

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
                    //防止序列化死循环
                    foreach (Product proclear in frmpanel.CurrentCart.products)
                    {
                        proclear.panelbmp = null;
                    }

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
                        UploadDgvGoods(CurrentCart);
                    }
                    else
                    {
                        //防止序列化死循环
                        foreach (Product pro in frmpanel.CurrentCart.products)
                        {
                            if (pro.weightflag)
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
                        }

                        //CurrentCart.products.AddRange(frmpanel.SelectProducts);
                        UploadDgvGoods(CurrentCart);
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

                btnPayByCash.BackColor = Color.DarkOrange;
                btnPayByCash.Tag = 1;
            }
            else
            {
                btnPayByCash.BackColor = Color.Silver;
                btnPayByCash.Tag = 0;
            }
        }

        private void pnlPayType1_EnabledChanged(object sender, EventArgs e)
        {

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
                if (keyData == Keys.Enter || keyData == Keys.Space || scancode.ToString().Length>=18)
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
                //LoadPicScreen(false);
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
            while (IsRun)
            {
                try
                {

                    if ( MainModel.IsPlayer && this.WindowState!=FormWindowState.Minimized && IsEnable)
                    {
                        MainModel.IsPlayer = false;
                       // Delay.Start(500);
                        Thread.Sleep(500);
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
                if (MainModel.frmnumber != null)
                {
                    try
                    {
                        MainModel.frmnumber.Close();
                        MainModel.frmnumber.Dispose();
                    }
                    catch { }
                }
                //数字弹窗初始化
                MainModel.frmnumber = new frmNumber();
                asf.AutoScaleControlTest(MainModel.frmnumber, 380, 540, this.Width * 36 / 100, this.Height * 70 / 100, true);
                MainModel.frmnumber.TopMost = true;


                if (MainModel.frmcashpay != null)
                {
                    try
                    {
                        MainModel.frmcashpay.Close();
                        MainModel.frmcashpay.Dispose();
                    }
                    catch { }
                }
                //现金支付窗体
                MainModel.frmcashpay = new frmCashPay();
                asf.AutoScaleControlTest(MainModel.frmcashpay, 380, 520, Screen.AllScreens[0].Bounds.Width * 36 / 100, Screen.AllScreens[0].Bounds.Height * 70 / 100, true);
                MainModel.frmcashpay.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width - MainModel.frmcashpay.Width - 40, Screen.AllScreens[0].Bounds.Height * 15 / 100);
                MainModel.frmcashpay.TopMost = true;


                if (MainModel.frmcashcoupon != null)
                {
                    try
                    {
                        MainModel.frmcashcoupon.Close();
                        MainModel.frmcashcoupon.Dispose();
                    }
                    catch { }
                }
                //现金券弹窗
                MainModel.frmcashcoupon = new frmCashCoupon();
                asf.AutoScaleControlTest(MainModel.frmcashcoupon, 380, 480, Screen.AllScreens[0].Bounds.Width * 36 / 100, Screen.AllScreens[0].Bounds.Height * 70 / 100, true);
                MainModel.frmcashcoupon.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width - MainModel.frmcashpay.Width - 40, Screen.AllScreens[0].Bounds.Height * 15 / 100);
                MainModel.frmcashcoupon.TopMost = true;



                if (MainModel.frmmodifyprice != null)
                {
                    try
                    {
                        MainModel.frmmodifyprice.Close();
                        MainModel.frmmodifyprice.Dispose();
                    }
                    catch { }
                }
                //修改订单金额弹窗
                MainModel.frmmodifyprice = new frmModifyPrice();
                asf.AutoScaleControlTest(MainModel.frmmodifyprice, 380, 520, Screen.AllScreens[0].Bounds.Width * 36 / 100, Screen.AllScreens[0].Bounds.Height * 70 / 100, true);
                MainModel.frmmodifyprice.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width - MainModel.frmmodifyprice.Width - 40, Screen.AllScreens[0].Bounds.Height * 15 / 100);
                MainModel.frmmodifyprice.TopMost = true;
            }
            catch
            {
                LogManager.WriteLog("初始化静态页面异常");
            }
        }


        HttpServer httpServer;
        private void HttpServerStart()
        {
            try
            {
                LoadTVSkus();
                httpServer = new MyHttpServer(8080);
                Thread threadHttpServer = new Thread(new ThreadStart(httpServer.listen));
                threadHttpServer.IsBackground = true;
                threadHttpServer.Start();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("启动电视屏服务异常："+ex.Message);
            }
        }


        private void LoadTVSkus()
        {
            //try
            //{
            //    string ErrorMsg = "";
            //    if (string.IsNullOrEmpty(ErrorMsg))
            //    {
            //        MainModel.PromotionJson = httputil.GetTVshowpageForPromotion(ref ErrorMsg);

            //    }

            //    if (string.IsNullOrEmpty(ErrorMsg))
            //    {
            //        MainModel.PorkJson = httputil.GetTVshowpageForPork(ref ErrorMsg);

            //    }
            //    MainModel.TVPromotionSkus = httputil.GetPromotionSkus(ref ErrorMsg);
            //    MainModel.TVSingleActivesSku = httputil.GetActiveSkus(ref ErrorMsg);

            //    int activecount =60;
            //    string exitsSkucode = "";
            //    if (MainModel.TVSingleActivesSku != null && MainModel.TVSingleActivesSku.posactiveskudetails.Count != 0)
            //    {
            //        activecount = 60 - MainModel.TVSingleActivesSku.posactiveskudetails.Count;
            //        List<string> lstresult = MainModel.TVSingleActivesSku.posactiveskudetails.Select(t => t.skucode).ToList();
            //        foreach (string str in lstresult)
            //        {
            //            exitsSkucode += "'" + str + "',";
            //        }
            //        exitsSkucode = exitsSkucode.TrimEnd(',');
            //    }
            //    else
            //    {
            //        if (MainModel.TVSingleActivesSku == null)
            //        {
            //            MainModel.TVSingleActivesSku = new PosActivesSku();
            //        }
            //        MainModel.TVSingleActivesSku.posactiveskudetails = new List<TVProduct>();
            //    }

            //    string strwhere = " STATUS =1 and CREATE_URL_IP='" + MainModel.URL + "' and CATEGORYID like '010%' and skucode not in(" + exitsSkucode + ") ";

            //    String[] notlikes = { "结算专用", "(", "A", "B", "C", "D", "E", "F", "G", "H", "I", "G", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            //    if (notlikes != null && notlikes.Length > 0)
            //    {
            //        for (int i = 0; i < notlikes.Length; i++)
            //        {
            //                strwhere += " AND SKUNAME NOT LIKE '%" + notlikes[i] + "%'";
            //        }
            //    }

            //    strwhere+=" order by saleprice desc limit "+activecount;
            //    List<DBPRODUCT_BEANMODEL> lstpro = productbll.GetModelList(strwhere);
            //    if(lstpro!=null && lstpro.Count>0){
            //        foreach (DBPRODUCT_BEANMODEL dbpro in lstpro)
            //        {  
            //            TVProduct tvpro = new TVProduct();
            //            tvpro.categoryid = dbpro.SKUCODE;
            //            tvpro.originalprice = dbpro.ORIGINPRICE;
            //            tvpro.promotionprice = dbpro.SALEPRICE;
            //            tvpro.saleunit = dbpro.SALESUNIT;
            //            tvpro.skucode = dbpro.SKUCODE;
            //            tvpro.skuname = dbpro.SKUNAME;
            //            tvpro.weightflag = dbpro.WEIGHTFLAG == 1 ? true : false;

            //            MainModel.TVSingleActivesSku.posactiveskudetails.Add(tvpro);
            //        }
            //    }

            //    string prokwhere = " STATUS =1 and CREATE_URL_IP='" + MainModel.URL + "' and CATEGORYID like '030%' ";
            //    String[] notlikespork = { "结算专用", "(", "A", "C", "D", "E", "F", "G", "H", "I", "G", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            //    prokwhere = "CATEGORYID LIKE '030%'";
            //    if (notlikes != null && notlikespork.Length > 0)
            //    {
            //        for (int i = 0; i < notlikespork.Length; i++)
            //        {
            //            prokwhere += " AND SKUNAME NOT LIKE '%" + notlikespork[i] + "%'";
            //        }
            //    }
            //    prokwhere += " order by saleprice desc limit 30";
            //    List<DBPRODUCT_BEANMODEL> lstporkpro = productbll.GetModelList(prokwhere);
            //    if (lstporkpro != null && lstporkpro.Count > 0)
            //    {
            //        MainModel.TVPorkSkus = new PosActivesSku();
            //        MainModel.TVPorkSkus.posactiveskudetails = new List<TVProduct>();
                
            //        foreach (DBPRODUCT_BEANMODEL dbpro in lstporkpro)
            //        {

            //            TVProduct tvpro = new TVProduct();
            //            tvpro.categoryid = dbpro.SKUCODE;
            //            tvpro.originalprice = dbpro.ORIGINPRICE;
            //            tvpro.promotionprice = dbpro.SALEPRICE;
            //            tvpro.saleunit = dbpro.SALESUNIT;
            //            tvpro.skucode = dbpro.SKUCODE;
            //            tvpro.skuname = dbpro.SKUNAME;
            //            tvpro.weightflag = dbpro.WEIGHTFLAG == 1 ? true : false;

            //            MainModel.TVPorkSkus.posactiveskudetails.Add(tvpro);
            //        }
            //    }


            //}
            //catch (Exception ex)
            //{
            //    LogManager.WriteLog("加载电视屏数据异常"+ex.Message);
            //}
        }

        private void dgvGood_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                Console.WriteLine("滚动条");
                MainModel.frmmainmedia.UpDgvScorll(dgvGood.FirstDisplayedScrollingRowIndex);
            }
        }



        private void ChangeMQTT(object obj )
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

        private void btnModifyPrice_Click(object sender, EventArgs e)
        {
            try
            {

                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0 && CurrentCart.totalpayment > 0)
                {

                    CurrentCart.totalpaymentbeforefix = CurrentCart.origintotal;
                    frmModifyPriceBack frmmodefypriceback = new frmModifyPriceBack(CurrentCart);
                    frmmodefypriceback.Location = new Point(0, 0);
                    frmmodefypriceback.ShowDialog();

                    this.Enabled = true;
                    Application.DoEvents();

                    if (frmmodefypriceback.DialogResult == DialogResult.OK)
                    {
                        CurrentCart.fixpricetotal = frmmodefypriceback.fixpricetotal;
                        CurrentCart.fixpricepromoamt = CurrentCart.origintotal - CurrentCart.fixpricetotal;
                        CurrentCart.totalpayment = CurrentCart.fixpricetotal;

                        //提供客屏展示信息
                        CurrentCart.orderpricedetails = new List<OrderPriceDetail>();
                        OrderPriceDetail orderdetail = new OrderPriceDetail();
                        orderdetail.title = "商品金额";
                        orderdetail.amount = "￥" + CurrentCart.origintotal.ToString("f2");
                        CurrentCart.orderpricedetails.Add(orderdetail);


                       
                            OrderPriceDetail orderdetailpro = new OrderPriceDetail();
                            orderdetailpro.title = "整单优惠";
                            orderdetailpro.amount = "-" + "￥" + CurrentCart.fixpricepromoamt.ToString("f2");
                            CurrentCart.orderpricedetails.Add(orderdetailpro);

                            // CurrentCart.fixpricepromoamt = Math.Round(CurrentCart.fixpricepromoamt, 2, MidpointRounding.AwayFromZero);
                        
                        dgvOrderDetail.Rows.Clear();



                        dgvOrderDetail.Rows.Add("商品金额", "￥" + CurrentCart.origintotal.ToString("f2"));
                        dgvOrderDetail.Rows.Add("整单优惠", "-"+"￥" + CurrentCart.fixpricepromoamt.ToString("f2"));
                        //if (cartpromoamt > 0)
                        //{
                        //    dgvOrderDetail.Rows.Add("活动优惠", "￥" + cartpromoamt.ToString("f2"));
                        //}


                        dgvOrderDetail.ClearSelection();

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

                        MainModel.frmMainmediaCart = CurrentCart;
                        MainModel.frmmainmedia.UpdateForm();
                        this.Activate();
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

        private void frmMainOffLine_Activated(object sender, EventArgs e)
        {
            try
            {
                if (this.WindowState != FormWindowState.Minimized)
                {
                    MainModel.HideTask();
                }
            }
            catch { }
        }

    }
}
