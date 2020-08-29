using Maticsoft.BLL;
using Maticsoft.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;
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
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;
using ZhuiZhi_Integral_Scale_UncleFruit.BrokenUI.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.BrokenUI;

namespace ZhuiZhi_Integral_Scale_UncleFruit
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
        }
        private void frmMain_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
      
            LoadDgvType();
            btnScan.Select();
           
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                lblShopName.Text = MainModel.Titledata + "   " + MainModel.CurrentShopInfo.shopname;
                lblMenu.Text = MainModel.CurrentUser.nickname + ",你好";
                picMenu.Left = pnlMenu.Width - picMenu.Width - lblMenu.Width;
                lblMenu.Left = picMenu.Right;

                lblUserName.Text = MainModel.CurrentUser.nickname;
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
                    else if (resultcode == MainModel.HttpMemberExpired)
                    {
                        ShowLoading(false);// LoadingHelper.CloseForm();
                        MainModel.CurrentMember = null;

                        //if (!ConfirmHelper.Confirm("会员登录已过期，请重新登录"))
                        //{
                        //    IsEnable = true;
                        //    return;
                        //}
                       
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
                dgvType.Visible = false;
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
                        btnOK.Tag = 0;
                        btnOK.BackColor = Color.Silver;
                        btnBatchBroken.Tag = 0;
                        btnBatchBroken.BackColor = Color.Silver;

                        return true;
                    }
                    else
                    {
                        btnOK.Tag = 1;
                        btnOK.BackColor = Color.OrangeRed;

                        btnBatchBroken.Tag = 1;
                        btnBatchBroken.BackColor = Color.FromArgb(18, 159, 255);
                    }
                    IsEnable = false;

                    ShowLoading(true);

                    BrokenResult result = BrokenHelper.GetSkuMovePrice(CurrentProducts);

                    if (result.WhetherSuccess)
                    {
                        //UploadDgvGoods();

                        CurrentPage = 1;
                        LoadDgvCart();
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

                                MainModel.ShowLog(ErrorMsg,false);
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

                if (goodcode.Length == 18 && !CartUtil.checkEanCodeIsError(goodcode, 18) && (goodcode.Substring(0, 2) == "25" || goodcode.Substring(0, 2) == "26"))
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
                    List<DBPRODUCT_BEANMODEL> lstdbpro = productbll.GetModelList(" BARCODE='" + goodcode.Substring(2, 5) + "'" + " and CREATE_URL_IP='" + MainModel.URL + "' " + " and SHOPID='" + MainModel.CurrentShopInfo.shopid + "' ");
                    if (lstdbpro != null && lstdbpro.Count > 0)
                    {
                        dbpro = lstdbpro[0];
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

               
                if(dbpro!=null && dbpro.STATUS==1)
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


                    scancodemember.scancodedto.skuname = dbpro.SKUNAME;

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
                else
                {
                    return null;
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


                        List<BrokenProduct> lstbrokenpro = CurrentProducts.Where(r => r.skucode == bpro.skucode).ToList();

                        if (lstbrokenpro == null || lstbrokenpro.Count == 0)
                        {
                            CurrentProducts.Add(bpro);
                        }
                        else
                        {
                            if (!bpro.weightflag)
                            {
                                lstbrokenpro[0].num += bpro.num;
                            }
                            else
                            {
                                lstbrokenpro[0].specnum += bpro.specnum;
                            }


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


        private void ClearForm()
        {
            try
            {
                CurrentProducts = new List<BrokenProduct>();
                dgvGood.Rows.Clear();

                ShowLoading(false);// LoadingHelper.CloseForm();
                ToastHelper.HideFormToast();
                Application.DoEvents();

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("清空主界面异常" + ex.Message);
            }
        }
        #endregion


        private BrokenProduct SelectBrokenPro = null;

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

                Point po = GlobalUtil.GetCursorPos();
                //增加标品
                if (po.X < (dgvGood.Left + picAdd.Right + 10) && po.X > (dgvGood.Left + picAdd.Left - 10) && !pro.weightflag)
                {
                    CurrentProducts.ForEach(r => r.num = r.skucode == pro.skucode ? r.num + 1 : r.num);

                    RefreshCart();
                }


                //减少标品 或修改非标品重量
                 if (po.X < (dgvGood.Left + picMinus.Right + 10) && po.X > (dgvGood.Left + picMinus.Left - 10) && !pro.weightflag)
                {

                        BrokenProduct brokenpro = CurrentProducts.Where(r => r.skucode == pro.skucode).ToList()[0];

                        if (brokenpro.num == 1)
                        {
                            if (ConfirmHelper.Confirm("确认删除？", pro.skuname + pro.skucode))
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

                //修改散称重量
                if (po.X < (dgvGood.Left + lblProNum.Right + 10) && po.X > (dgvGood.Left + lblProNum.Left - 10) && pro.weightflag)
                {
                    decimal numbervalue = BrokenHelper.ShowBrokenNumber(pro.skuname);
                    if (numbervalue > 0)
                    {
                        pro.specnum = numbervalue;
                        pro.num = 1;
                        RefreshCart();
                    }
                }

                if (po.X < (dgvGood.Left + btnType.Right + 10) && po.X > (dgvGood.Left + btnType.Left - 10))
                {

                    dgvType.Visible = true;

                    if(e.RowIndex<3){
                        dgvType.Top = dgvGood.Top + dgvGood.RowTemplate.Height * e.RowIndex + btnType.Bottom ;
                    }
                    else
                    {
                        dgvType.Top = dgvGood.Top + dgvGood.RowTemplate.Height * e.RowIndex-dgvType.Height+btnType.Top ;
                    }

                    dgvType.Left = dgvGood.Left + btnType.Left;
                    SelectBrokenPro = pro;
                }
                else
                {
                    dgvType.Visible = false;
                    SelectBrokenPro = null;
                }

                if (po.X < (dgvGood.Left + picDelete.Right + 10) && po.X > (dgvGood.Left + picDelete.Left - 10))
                {
                    if (ConfirmHelper.Confirm("确认删除？", pro.skuname + pro.skucode))
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
                dgvType.Visible = false;
                IsEnable = false;
                FormPanelGoodsForBroken frmpanel = new FormPanelGoodsForBroken();
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
                       
                            List<BrokenProduct> lstbrokenpro = CurrentProducts.Where(r => r.skucode == pro.skucode).ToList();

                            if (lstbrokenpro == null || lstbrokenpro.Count == 0)
                            {
                                CurrentProducts.Add(BrokenHelper.ProductToBrokenProduct(pro));
                            }
                            else
                            {
                                if (pro.goodstagid == 0)
                                {
                                    lstbrokenpro[0].num += pro.num;
                                }
                                else
                                {
                                    lstbrokenpro[0].specnum += pro.specnum;                                   
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




        private Bitmap GetDgvRow(BrokenProduct pro)
        {
            try
            {

                lblTitle.Text = string.IsNullOrEmpty(pro.title) ? pro.skuname : pro.title;
                lblSkuCode.Text = pro.skucode;
   
                lblSinglePrice.Text =pro.saleprice==0 ? "--": pro.saleprice.ToString("f2");




                //第三 四列图片
                if (!pro.weightflag)  //0是标品  1是称重
                {
                    picAdd.Visible = true;
                    picMinus.Visible = true;

                    lblProNum.Text = pro.num.ToString();
                    lblProNum.Left = (picAdd.Left - picMinus.Right - lblProNum.Width) / 2 + picMinus.Right;
                }
                else
                {
                    picAdd.Visible = false;
                    picMinus.Visible = false;

                    //lblProNum.Text = pro.specnum + pro.unit;
                    lblProNum.Text = pro.specnum + "KG";
                    lblProNum.Left = picMinus.Right; 
                }


                if (!string.IsNullOrEmpty(pro.brokentypevalue))
                {
                    btnType.Text = pro.brokentypevalue;
                }else{
                    if (BrokenHelper.GetBrokenType(false) != null && BrokenHelper.GetBrokenType(false).Count > 0)
                    {
                        pro.brokentypekey = BrokenHelper.GetBrokenType(false)[0].key;
                        pro.brokentypevalue = BrokenHelper.GetBrokenType(false)[0].value;

                        btnType.Text = pro.brokentypevalue;
                    }
                }


                    lblTotal.Text =pro.deliveryprice==0? "--": pro.deliveryprice.ToString("f2");

                    Bitmap bmpPro = new Bitmap(pnlCartItem.Width, pnlCartItem.Height);
                    bmpPro.Tag = pro;
                    pnlCartItem.DrawToBitmap(bmpPro, new Rectangle(0, 0, pnlCartItem.Width, pnlCartItem.Height));
                    bmpPro.MakeTransparent(Color.White);

                    return bmpPro;
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

        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

              
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                dgvType.Visible = false;
                if (btnOK.Tag == null || btnOK.Tag.ToString() == "0" || !IsEnable)
                {
                    return;
                }

                if (!ZhuiZhi_Integral_Scale_UncleFruit.HelperUI.ConfirmHelper.Confirm("确认报损"))
                {
                    return;
                }

                IsEnable = false;
                string errormsg = "";
                CreateBrokenResult result = httputil.CreateBroken(BrokenHelper.GetParaCreateBroken(CurrentProducts), ref errormsg);
               
                if (result == null || !string.IsNullOrEmpty(errormsg))
                {
                    MainModel.ShowLog(errormsg, false);
                    IsEnable = true;
                }
                else
                {
                    string errormsg1 = "";
                    PrintUtil.PrintBroken(result, ref errormsg1);
                    this.Close();
                    IsEnable = true;
                }
            }
            catch (Exception ex)
            {
                IsEnable = true;
                MainModel.ShowLog("确认报损异常" + ex.Message, true);
            }
        }

        private void btnBatchBroken_Click(object sender, EventArgs e)
        {
            try
            {
                dgvType.Visible = false;
                if (btnBatchBroken.Tag == null || btnBatchBroken.Tag.ToString() == "0" || !IsEnable)
                {
                    return;
                }
                int brokentype =BrokenHelper.ShowFormBrokenBatch();
                
              if(  brokentype>=0){
                  CurrentProducts.ForEach(r => { r.brokentypekey = brokentype; r.brokentypevalue = BrokenHelper.GetBrokenTypeName(brokentype); });

                  LoadDgvCart();
              }

            }
            catch { }
        }

        #region
        private int CurrentPage = 1;
        private void rbtnPageUp_ButtonClick(object sender, EventArgs e)
        {
            dgvType.Visible = false;
            if (!rbtnPageUp.WhetherEnable)
            {
                return;
            }
            CurrentPage--;
            LoadDgvCart();
        }

        private void rbtnPageDown_ButtonClick(object sender, EventArgs e)
        {
            dgvType.Visible = false;
            if (!rbtnPageDown.WhetherEnable)
            {
                return;
            }
            CurrentPage++;
            LoadDgvCart();
        }


        private void LoadDgvCart()
        {
            try
            {
                dgvGood.Rows.Clear();
                if(CurrentProducts ==null || CurrentProducts.Count==0){
                    return;
                }

                CurrentProducts.Reverse();
              
                    rbtnPageUp.WhetherEnable = CurrentPage > 1;
               


                decimal totalprice = 0;


                int startindex = (CurrentPage - 1) * 6;

                int lastindex = Math.Min(CurrentProducts.Count - 1, startindex + 5);


                List<BrokenProduct> lstLoadingPro = CurrentProducts.GetRange(startindex, lastindex - startindex + 1);

                foreach (BrokenProduct pro in lstLoadingPro)
                {
                    
                       // dgvGood.Rows.Insert(0, GetDgvRow(pro));

                        dgvGood.Rows.Add(GetDgvRow(pro));
 
                    
                }
                //每页只显示6条记录，总金额要计算所有的
                foreach (BrokenProduct pro in CurrentProducts)
                {
                      if (!pro.weightflag)
                        {
                            totalprice += pro.num * pro.deliveryprice;

                        }
                        else
                        {
                            totalprice += pro.specnum * pro.deliveryprice;
                        }
                }

                rbtnPageDown.WhetherEnable = CurrentProducts.Count > CurrentPage * 6;
                CurrentProducts.Reverse();

                lblSkuAmount.Text = CurrentProducts.Count.ToString();
                lblTotalPrice.Text = "￥" + totalprice.ToString("f2");
                lblUserName.Text = MainModel.CurrentUser.nickname;
                Application.DoEvents();

                dgvGood.ClearSelection();
                this.Activate();

            }
            catch (Exception ex)
            {
               
                LogManager.WriteLog("加载购物车列表异常" + ex.Message);
            }
        }


        private void LoadDgvType()
        {
            try
            {
               

                dgvType.Rows.Clear();

                foreach (BrokenType typeitem in BrokenHelper.GetBrokenType(false))
                {
                    dgvType.Rows.Add(typeitem.key,typeitem.value);
                }

                if (dgvType.Rows.Count > 5)
                {
                    dgvType.Height = dgvType.RowTemplate.Height * 5+5;
                }
                else
                {
                    dgvType.Height = dgvType.RowTemplate.Height*dgvType.Rows.Count+5;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dgvType_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                 if (!IsEnable)
                {
                    return;
                }

                if (e.RowIndex < 0)
                    return;

                if (SelectBrokenPro == null)
                {
                    return;
                }

                BrokenProduct OperationPro = CurrentProducts.FirstOrDefault(r=> r.skucode==SelectBrokenPro.skucode);
                OperationPro.brokentypekey =(int) dgvType.Rows[e.RowIndex].Cells[0].Value;
                OperationPro.brokentypevalue = dgvType.Rows[e.RowIndex].Cells[1].Value.ToString();

                dgvType.Visible = false;
                LoadDgvCart();

            }
            catch
            {
                
            }
        }
        #endregion

        private void FormBrokenCreate_MouseDown(object sender, MouseEventArgs e)
        {
            dgvType.Visible = false;
        }

        private void pnlHead_MouseDown(object sender, MouseEventArgs e)
        {
            dgvType.Visible = false;
        }

      

    }
}
