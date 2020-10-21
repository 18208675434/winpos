using Maticsoft.BLL;
using Maticsoft.Model;
using WinSaasPOS_Scale.Common;
using WinSaasPOS_Scale.Model;
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
using WinSaasPOS_Scale.HelperUI;
using WinSaasPOS_Scale.BrokenUI.Model;
using WinSaasPOS_Scale.BrokenUI;

namespace WinSaasPOS_Scale
{
    public partial class FormBrokenCreate : Form
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

        ///// <summary>
        ///// 当前购物车对象
        ///// </summary>
        //private Cart CurrentCart = new Cart();

        /// <summary>
        /// 当前购物车商品
        /// </summary>
        private List<BrokenProduct> CurrentProducts = new List<BrokenProduct>();

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
        private  bool IsEnable=true;

           //扫描数据处理线程
           Thread threadScanCode;

           //刷新焦点线程  防止客屏播放视频抢走焦点
           Thread threadCheckActivate ;

           private bool IsRun = true;

           private DBPRODUCT_BEANBLL productbll = new DBPRODUCT_BEANBLL();
        #endregion

        #region  页面加载
        public FormBrokenCreate()
        {
            InitializeComponent();

            //使用委托的话frmmain界面会卡死
            Control.CheckForIllegalCrossThreadCalls = false;

            MainModel.wScale = (float)Screen.AllScreens[0].Bounds.Width / this.Width;
            MainModel.hScale = (float)Screen.AllScreens[0].Bounds.Height / this.Height;
            
            //防止标品加减框变形
            btnIncrease.Size = new System.Drawing.Size(35, 35);
            btnNum.Size = new System.Drawing.Size(90, 35);
            btnNum.Left = pnlNum.Width - btnNum.Width + 3;

        }
        private void frmMain_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            btnScan.Select();
           
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                lblShopName.Text = MainModel.CurrentShopInfo.shopname;
                btnOnLineType.Left = lblShopName.Left + lblShopName.Width + 10;

                //∨ 从右往左排列 被当成图形   从左向右 右侧间距太大
                btnMenu.Text = MainModel.CurrentUser.nickname + "，你好";
                btnMenu.Left = Math.Max(pnlHead.Width - btnMenu.Width - 10, btnCancle.Left + btnCancle.Width);

                //扫描数据处理线程
                threadScanCode = new Thread(ScanCodeThread);
                threadScanCode.IsBackground = true;
                threadScanCode.Start();

                //刷新焦点线程  防止客屏播放视频抢走焦点
                threadCheckActivate = new Thread(CheckActivate);
                threadCheckActivate.IsBackground = true;
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
            try { threadScanCode.Abort(); }
            catch { }

            try { threadCheckActivate.Abort(); }
            catch { }

        }

        JSON_BEANBLL jsonbll = new JSON_BEANBLL();


        private void CheckUserAndMember(int resultcode, string ErrorMsg)
        {
            try
            {
                this.Invoke(new InvokeHandler(delegate()
                {
                    if (resultcode == MainModel.HttpUserExpired)
                    {
                        ShowLoading(false);
                        LoadPicScreen(true);
                        MainModel.CurrentMember = null;
                        frmUserExpired frmuserexpired = new frmUserExpired();
                        frmuserexpired.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmuserexpired.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmuserexpired.Height) / 2);
                        frmuserexpired.TopMost = true;
                        frmuserexpired.ShowDialog();

                        INIManager.SetIni("System", "POS-Authorization", "", MainModel.IniPath);

                        this.Close();
                        LoadPicScreen(false);
                    }
                    else if (resultcode == MainModel.HttpMemberExpired || resultcode == MainModel.DifferentMember)
                    {
                        ShowLoading(false);// LoadingHelper.CloseForm();
                        MainModel.CurrentMember = null;

                        if (!ConfirmHelper.Confirm("会员登录已过期，请重新登录"))
                        {
                            IsEnable = true;
                            return;
                        }
                       
                        IsEnable = true;
                        ClearForm();
                        LoadPicScreen(false);


                    }
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

        #region 购物车
        private void btnLoadBarCode_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }


                string numbervalue = NumberHelper.ShowFormNumber("请输入商品条码", NumberType.BarCode);
                if (!string.IsNullOrEmpty(numbervalue))
                {
                    QueueScanCode.Enqueue(numbervalue);
                }
                else
                {
                    Application.DoEvents();
                    return;
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


        /// <summary>
        /// 更新商品列表
        /// </summary>
        /// <param name="type"> -2 删除商品  -1减去商品   0刷新   1增加商品</param>
        /// <param name="pro"></param>
        /// <returns></returns>
        private bool RefreshCart()
        {

                try
                {

                    if (CurrentProducts == null || CurrentProducts.Count == 0)
                    {
                        dgvGood.Rows.Clear();
                        lblSkuAmount.Text = "0";
                        lblTotalPrice.Text = "￥0.00";
                        rbtnOK.WhetherEnable = false;
                        return true;
                    }
                    else
                    {
                        rbtnOK.WhetherEnable = true;
                    }
                    IsEnable = false;

                    ShowLoading(true);

                    BrokenResult result = BrokenHelper.GetSkuMovePrice(CurrentProducts);

                    if (result.WhetherSuccess)
                    {
                        UploadDgvGoods();
                    }
                    else
                    {
                        MainModel.ShowLog(result.message,false);
                    }

                    return true;
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
                                MainModel.ShowLog("条码不正确",false);
                                //lstNotLocalCode.Add(scancode);
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
                                }
                                else
                                {
                                    LstScancodemember.Add(scancodemember);
                                }
                            }
                        }
                        //ShowLoading(false);// LoadingHelper.CloseForm();

                        if (LstScancodemember.Count > 0)
                        {
                            isGoodRefresh = true;
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

        private scancodememberModel GetLocalPro(string goodcode)
        {
            try
            {
                DBPRODUCT_BEANMODEL dbpro = null;

                bool isINNERBARCODE = false;

                if (goodcode.Length == 18 && !checkEanCodeIsError(goodcode, 18) && (goodcode.Substring(0, 2) == "25" || goodcode.Substring(0, 2) == "26"))
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
                    List<DBPRODUCT_BEANMODEL> lstdbpro = productbll.GetModelList(" BARCODE='" + goodcode + "'" + " and CREATE_URL_IP='" + MainModel.URL + "' "+ " and SHOPID='" + MainModel.CurrentShopInfo.shopid + "' ");
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
                    scancodemember.scancodedto.salesunit = dbpro.SALESUNIT;

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



                        BrokenProduct bpro = new BrokenProduct();
                        bpro.skucode = scancodemember.scancodedto.skucode;
                        bpro.skuname = scancodemember.scancodedto.skuname;
                        bpro.title = scancodemember.scancodedto.skuname;
                        bpro.weightflag = scancodemember.scancodedto.weightflag;
                        bpro.num = scancodemember.scancodedto.num;
                        bpro.specnum = scancodemember.scancodedto.specnum;
                        bpro.unit = scancodemember.scancodedto.salesunit;
                        bpro.saleprice = 0;



                        if (!bpro.weightflag)
                    {
                        List<BrokenProduct> lstbroken = CurrentProducts.Where(r => r.skucode == bpro.skucode).ToList();

                        if (lstbroken != null && lstbroken.Count > 0)
                        {
                            lstbroken[0].num += 1;
                        }
                        else
                        {
                            CurrentProducts.Add(bpro);
                        }
                        
                    }else{
                        CurrentProducts.Add(bpro);
                    }

                }
                    RefreshCart();
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

        private void UploadDgvGoods()
        {
            lock (thislockDgvGood)
            {
                try
                {


                    decimal totalprice = 0;

                    int oldrowindex = dgvGood.FirstDisplayedScrollingRowIndex;
                    dgvGood.Rows.Clear();

                 int count = CurrentProducts.Count;
                            for (int i = 0; i < count; i++)
                            {
                                List<Bitmap> lstbmp = GetDgvRow(CurrentProducts[i]);
                                if (lstbmp != null && lstbmp.Count == 6)
                                {
                                    dgvGood.Rows.Insert(0, new object[] { lstbmp[0], lstbmp[1], lstbmp[4], lstbmp[2], lstbmp[3], lstbmp[5] });

                                    if (!CurrentProducts[i].weightflag)
                                    {
                                        totalprice += CurrentProducts[i].num * CurrentProducts[i].deliveryprice;

                                    }
                                    else
                                    {
                                        totalprice += CurrentProducts[i].specnum * CurrentProducts[i].deliveryprice;

                                    }
                                }
                            }

                            try { dgvGood.FirstDisplayedScrollingRowIndex = oldrowindex; }
                            catch { }


                            lblSkuAmount.Text = CurrentProducts.Count.ToString();
                            lblTotalPrice.Text = "￥"+totalprice.ToString("f2");
                            lblUserName.Text = MainModel.CurrentUser.nickname;
                            Application.DoEvents();

                            dgvGood.ClearSelection();
                     this.Activate();
                    }
                 


                   
                  catch (Exception ex)
                {
                    dgvGood.Refresh();
                    ShowLoading(false);// LoadingHelper.CloseForm();
                    LogManager.WriteLog("更新显示列表异常" + ex.Message + ex.StackTrace);
                }
                }
        }



        private void ClearForm()
        {
            try
            {
                CurrentProducts = new List<BrokenProduct>();
                dgvGood.Rows.Clear();

                ShowLoading(false);

                ShowLoading(false);// LoadingHelper.CloseForm();
                MsgHelper.CloseForm();

                Application.DoEvents();

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("清空主界面异常" + ex.Message);
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

                Bitmap bmp = (Bitmap)dgvGood.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                BrokenProduct pro = (BrokenProduct)bmp.Tag;

            
                //增加标品
                if (e.ColumnIndex == 4 && !pro.weightflag)
                {
                    CurrentProducts.ForEach(r => r.num = r.skucode == pro.skucode ? r.num + 1 : r.num);

                    RefreshCart();
                }


                //减少标品 或修改非标品重量
                if (e.ColumnIndex == 3)
                {
                    if (!pro.weightflag)
                    {
                        BrokenProduct brokenpro = CurrentProducts.Where(r => r.skucode == pro.skucode).ToList()[0];

                        if (brokenpro.num == 1)
                        {
                            if (ConfirmHelper.Confirm("是否确认删除商品？", pro.skuname + pro.skucode))
                            {
                                CurrentProducts.Remove(pro);
                            }
                        }
                        else
                        {
                            brokenpro.num -= 1;
                        }
                       
                        RefreshCart();
                    }
                    else
                    {
                        string numbervalue = NumberHelper.ShowFormNumber(pro.skuname, NumberType.ProWeight);
                        if (!string.IsNullOrEmpty(numbervalue))
                        {
                            pro.specnum = Convert.ToDecimal(numbervalue) / 1000;
                            pro.num = 1;
                            RefreshCart();
                        }
                    }
                }

                if (e.ColumnIndex == 5)
                {
                    if (ConfirmHelper.Confirm("是否确认删除商品？", pro.skuname + pro.skucode))
                    {
                        CurrentProducts.Remove(pro);
                        RefreshCart();
                    }
                  
                }

                dgvGood.ClearSelection();
            }
            catch (Exception ex)
            {

                MainModel.ShowLog("操作购物车商品异常" + ex.Message, true);
            }
            finally
            {
                btnScan.Select();
                LoadPicScreen(false);
            }
        }

        //防止控件占用焦点后  按键无法捕获
        private void dgvGood_Click(object sender, EventArgs e)
        {
            btnScan.Select();
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
           
        }

        private void btnMianban_Click(object sender, EventArgs e)
        {

            if (!IsEnable)
            {
                return;
            }
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
                if (frmpanel.DialogResult == DialogResult.OK)
                {
                    foreach (Product pro in frmpanel.CurrentCart.products)
                    {
                        if (pro.weightflag)
                        {
                            CurrentProducts.Add(BrokenHelper.ProductToBrokenProduct(pro));
                        }
                        else
                        {
                            List<BrokenProduct> lstbrokenpro = CurrentProducts.Where(r => r.skucode == pro.skucode).ToList();

                            if (lstbrokenpro == null || lstbrokenpro.Count == 0)
                            {
                                CurrentProducts.Add(BrokenHelper.ProductToBrokenProduct(pro));
                            }
                            else
                            {
                                lstbrokenpro[0].num += pro.num;
                            }
                        }
                       
                    }
                   

                    RefreshCart();
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

        StringBuilder scancode = new StringBuilder();
        protected override bool ProcessDialogKey(Keys keyData)
        {
            try
            {

                LogManager.WriteLog("kboard"+keyData.ToString());
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
                };
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

                    Delay.Start(200);
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
                    if ( MainModel.IsPlayer)
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




        private List<Bitmap> GetDgvRow(BrokenProduct pro)
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
               
                bmpbarcode = new Bitmap(pnlBarCode.Width, pnlBarCode.Height);
                bmpbarcode.Tag = pro;
                pnlBarCode.DrawToBitmap(bmpbarcode, new Rectangle(0, 0, pnlBarCode.Width, pnlBarCode.Height));


   
                    lblSinglePrice.Text =pro.saleprice==0 ? "--": pro.saleprice.ToString("f2");

                    lblSinglePrice.Left = (pnlSinglePrice.Width - lblSinglePrice.Width) / 2;

               
                bmpPrice = new Bitmap(pnlSinglePrice.Width, pnlSinglePrice.Height);
                bmpPrice.Tag = pro;
                pnlSinglePrice.DrawToBitmap(bmpPrice, new Rectangle(0, 0, pnlSinglePrice.Width, pnlSinglePrice.Height));



                //第三 四列图片
                if (!pro.weightflag)  //0是标品  1是称重
                {
                    btnNum.Text =  pro.num.ToString() + "    ";

                    btnNum.BackgroundImage  = Resources.ResourcePos.border_minus;

                    btnIncrease.Text = "";
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
                    btnNum.BackgroundImage = Resources.ResourcePos.border_weight;
                    btnIncrease.BackgroundImage = Resources.ResourcePos.White;
                    btnNum.Text = pro.specnum.ToString().PadRight(6,' ');
                    btnIncrease.Text = pro.unit;
                    bmpNum = new Bitmap(pnlNum.Width, pnlNum.Height);
                    bmpNum.Tag = pro;
                    pnlNum.DrawToBitmap(bmpNum, new Rectangle(0, 0, pnlNum.Width, pnlNum.Height));

                    bmpAdd = new Bitmap(pnlAdd.Width, pnlAdd.Height);
                    bmpAdd.Tag = pro;
                    pnlAdd.DrawToBitmap(bmpAdd, new Rectangle(0, 0, pnlAdd.Width, pnlAdd.Height));
                }




                    lblTotal.Text =pro.deliveryprice==0? "--": pro.deliveryprice.ToString("f2");

                    lblTotal.Left = (pnlTotal.Width - lblTotal.Width) / 2;



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
                MainModel.ShowLog("解析商品信息异常"+ex.Message,true);
                return null;
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
           
        }

        private void dgvGood_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                Console.WriteLine("滚动条");
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbtnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (!rbtnOK.WhetherEnable)
                {
                    return;
                }

                string errormsg = "";
                CreateBrokenResult result =  httputil.CreateBroken(BrokenHelper.GetParaCreateBroken(CurrentProducts),ref errormsg);

                if (result == null || !string.IsNullOrEmpty(errormsg))
                {
                    MainModel.ShowLog(errormsg,false); 
                }
                else
                {
                    string errormsg1 = "";
                    PrintUtil.PrintBroken(result,ref errormsg1);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("确认报损异常"+ex.Message,true);
            }
        }
    }
}
