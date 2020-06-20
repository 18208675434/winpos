using Maticsoft.BLL;
using Maticsoft.Model;
using WinSaasPOS.Common;
using WinSaasPOS.Model;
using WinSaasPOS.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace WinSaasPOS
{
    public partial class frmPanelGoods : Form
    {

        #region 成员变量
        /// <summary>
        /// 数据库所有可显示商品
        /// </summary>
        private List<Product> LstAllProduct = new List<Product>();

        private DBPRODUCT_BEANBLL productbll = new DBPRODUCT_BEANBLL();

        private string CurrentFirstCategoryid = "";

        private HttpUtil httputil = new HttpUtil();

        private int dgvgoodwidth = 0;

        private bool IsRun = true;

        /// <summary>
        /// 当前页面商品 根据firsecategoryid 区分
        /// </summary>
        SortedDictionary<string, List<Product>> sortProductByFirstCategoryid = new SortedDictionary<string, List<Product>>();


        /// <summary>
        /// 当前页面购物车 根据firsecategoryid 区分
        /// </summary>
        SortedDictionary<string,Cart> sortCartByFirstCategoryid = new SortedDictionary<string, Cart>();


        //<summary>
        //按比例缩放页面及控件
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();
        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();

        public List<Product> SelectProducts = new List<Product>();

        public Cart CurrentCart = new Cart();

        public List<Product> LastLstPro = new List<Product>();

        public SortType querysorttype = SortType.SaleCount;

        /// <summary>
        /// this.enable=false; 页面不可用页面不可控；  通过该标志控制页面是否可用
        /// </summary>
        private bool IsEnable = true;
        #endregion

        #region  页面初始化
        public frmPanelGoods()
        {
            InitializeComponent();
            //使用委托的话frmmain界面会卡死
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void frmPanelGoods_Load(object sender, EventArgs e)
        {

            lblTime.Text = MainModel.Titledata;
            lblShopName.Text = MainModel.CurrentShopInfo.shopname;
            btnOnLineType.Left = lblShopName.Left + lblShopName.Width + 10;
            if (MainModel.IsOffLine)
            {
                btnOnLineType.BackgroundImage = Resources.ResourcePos.OffLineType; btnOnLineType.Text = "   离线";
            }
            else
            {
                btnOnLineType.BackgroundImage = Resources.ResourcePos.OnLineType; btnOnLineType.Text = "   在线";
            }                                                
            btnMenu.Text = MainModel.CurrentUser.nickname + "，你好   ";
            btnMenu.Left = Math.Max(pnlHead.Width - btnMenu.Width-10, btnCancle.Left + btnCancle.Width);
          

            picLoading.Size = new Size(55, 55);
            dgvgoodwidth = dgvGoodPic.Width;

            dgvGoodPic.RowHeadersWidth = Convert.ToInt16(dgvGoodPic.RowHeadersWidth * MainModel.hScale);

            TopLblGoodName = lblGoodName.Top;
            HeightLblGoodName = lblGoodName.Height;
          
        }

        private void frmPanelGoods_Shown(object sender, EventArgs e)
        {
            
            Application.DoEvents();
            Other.CrearMemory();
            imgSelect = btnOrderBySaleCount.BackgroundImage;
            imgNotSelect = btnOrderBySalePrice.BackgroundImage;

            LoadProduct();

            IniForm();
            //扫描数据处理线程
            Thread threadScanCode = new Thread(ScanCodeThread);
            threadScanCode.IsBackground = false;
            threadScanCode.Start();


        }

        private void LoadProduct()
        {
            try
            {
                List<DBPRODUCT_BEANMODEL> lstpro = productbll.GetModelList("PANELFLAG='1' and PANELSHOWFLAG=1 and STATUS =1 and CREATE_URL_IP='" + MainModel.URL + "' and SHOPID='" + MainModel.CurrentShopInfo.shopid + "' and SKUTYPE in (1,4) order by FIRSTCATEGORYID");
                foreach (DBPRODUCT_BEANMODEL pro in lstpro)
                {
                    Product product = new Product();
                    product.mainimg = pro.MAINIMG;
                    product.price = null;
                    product.pricetag = pro.PRICETAG;
                    product.pricetagid = (int)pro.PRICETAGID;
                    product.saleunit = pro.SALESUNIT;
                    product.skucode = pro.SKUCODE;
                    product.skuname = pro.SKUNAME;
                    product.title = pro.SKUNAME;
                    product.firstcategoryid = pro.FIRSTCATEGORYID;
                    product.firstcategoryname = pro.FIRSTCATEGORYNAME;
                    product.barcode = pro.SKUCODE;
                    product.secondcategoryid = pro.SECONDCATEGORYID;
                    
                    //product.isQueryBarcode = 0;
                    product.weightflag = Convert.ToBoolean(pro.WEIGHTFLAG);
                    product.shopid = pro.SHOPID;
                    product.goodstagid = (int)pro.WEIGHTFLAG;
                    product.num = 1;

                    product.salecount = (int)pro.SALECOUNT;
                    product.createdat = pro.CREATEDAT;

                   
                        Price price = new Price();
                        price.saleprice = pro.SALEPRICE;
                        price.originprice = pro.SALEPRICE;
                        price.specnum = pro.SPECNUM;
                        price.unit = pro.SALESUNIT;

                        product.price = price;
                        //product.pricetagid = "";
                        product.pricetag = "";
                        //product.isLoadPanel = true;
                        //product.panelbmp = GetItemImg(product);

                        //singlecalculate.calculate(product);
                    
                    //product.price = new Price();
                    LstAllProduct.Add(product);
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载面板商品数据异常"+ex.StackTrace,true);
            }
        }
       
        private void IniForm()
        {
            SortedDictionary<string, string> sort = productbll.GetDiatinctCategory("PANELFLAG='1' and PANELSHOWFLAG=1 and STATUS =1 and CREATE_URL_IP='" + MainModel.URL + "' and SHOPID='" + MainModel.CurrentShopInfo.shopid + "' and SKUTYPE in (1,4) order by FIRSTCATEGORYID");

            LoadSortProByFirstcategoryid(sort);
            LoadPnlCategory(sort);
        }
        private void LoadPnlCategory(SortedDictionary<string, string> sort)
        {
            try
            {
                dgvCategory.Rows.Clear();
                foreach (KeyValuePair<string, string> kv in sort)
                {
                    dgvCategory.Rows.Add(kv.Key,kv.Value);
                }

                if (dgvCategory.Rows.Count > 0)
                {
                    dgvCategory_CellClick(null, new DataGridViewCellEventArgs(0, 0));
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void LoadSortProByFirstcategoryid(SortedDictionary<string, string> sort)
        {
            try
            {
                foreach (KeyValuePair<string, string> kv in sort)
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
            catch (Exception ex)
            {
                MainModel.ShowLog("加载分类面板商品异常" + ex.Message, false);
            }
        }


        private void lblShuiyin_Click(object sender, EventArgs e)
        {

            txtQuery.Focus();
        }



        private void pnlQuery_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Control con = (Control)sender;

                // Draw(e.ClipRectangle, e.Graphics, 100, false, Color.FromArgb(113, 113, 113), Color.FromArgb(0, 0, 0));
                //base.OnPaint(e);
                Graphics g = e.Graphics;
                // g.DrawString("", new Font("微软雅黑", 9, FontStyle.Regular), new SolidBrush(Color.White), new PointF(10, 10));

                LinearGradientBrush myLinearGradientBrush = new LinearGradientBrush(e.ClipRectangle, Color.LightGray, Color.LightGray, LinearGradientMode.Vertical);
                //填充
                ////四边圆角
                GraphicsPath gp = new GraphicsPath();
                gp.AddArc(e.ClipRectangle.X, e.ClipRectangle.Y, con.Height, con.Height, 180, 90);
                gp.AddArc(e.ClipRectangle.Width - 2 - con.Height, e.ClipRectangle.Y, con.Height, con.Height, 270, 90);
                gp.AddArc(e.ClipRectangle.Width - 2 - con.Height, e.ClipRectangle.Height - 1 - con.Height, con.Height, con.Height, 0, 90);
                gp.AddArc(e.ClipRectangle.Y, e.ClipRectangle.Height - 1 - con.Height, con.Height, con.Height, 90, 90);
                gp.CloseAllFigures();

                g.FillPath(myLinearGradientBrush, gp);
            }
            catch { }
        }


        public ExpiredType ExpiredCode = ExpiredType.None;
        private void CheckUserAndMember(int resultcode, string ErrorMsg)
        {
            try
            {

                if (resultcode == MainModel.HttpUserExpired)
                {

                    MainModel.CurrentMember = null;

                    ExpiredCode = ExpiredType.UserExpired;
                    IsRun = false;
                    this.Close();

                }
                else if (resultcode == MainModel.HttpMemberExpired)
                {
                    MainModel.CurrentMember = null;

                    ExpiredCode = ExpiredType.MemberExpired;
                    IsRun = false;

                    this.Close();

                }
                else
                {
                    if (!string.IsNullOrEmpty(ErrorMsg))
                    {
                        MainModel.ShowLog(ErrorMsg, false);
                    }

                }

            }
            catch (Exception ex)
            {
                IsEnable = true;
                MainModel.ShowLog("面板商品验证用户/会员异常", true);

            }
        }

        #endregion



        #region  面板商品分类

        private void lblExit_Click(object sender, EventArgs e)
        {
            try
            {

                if (!IsEnable)
                {
                    return;
                }
                if (CurrentCart!=null && CurrentCart.products!=null && CurrentCart.products.Count>0)
                {
                    FrmConfirmBack frmconfirmback = new FrmConfirmBack("确认返回", "返回后将清空已选商品，确认返回？", "");
                    frmconfirmback.Location = new Point(0, 0);

                    if (frmconfirmback.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    IsRun = false;

                    this.Close();
                }
                IsRun = false;
                //this.Dispose();
                this.Close();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!IsEnable)
            {
                return;
            }
            Application.DoEvents();
            if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
            {
                this.DialogResult = DialogResult.OK;
                IsRun = false;

                this.Close();
            }
        }

        private void dgvGoodPic_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (e.NewValue + dgvGoodPic.DisplayedRowCount(false) == dgvGoodPic.Rows.Count)
                {
                   
                    if (sortCartByFirstCategoryid[CurrentFirstCategoryid].products.Count > 0 && sortCartByFirstCategoryid[CurrentFirstCategoryid].products.Count <= dgvGoodPic.Rows.Count * 4)
                    {
                        lblOver.Visible = true;
                    }
                    else
                    {
                        UpdateDgvGood(true,false);
                    }
                }
                else
                {
                    lblOver.Visible = false;
                }
            }
        }

        bool isnewType = false ;
        private void UpdateDgvGood(bool isnew,bool isnewType)
        {
            try
            {
                IsEnable = false;
                int dgrowscount = dgvGoodPic.Rows.Count * 4;

                LoadBtnSortStatus(sortCartByFirstCategoryid[CurrentFirstCategoryid].sorttype);
                List<Product> templstprodcut = sortCartByFirstCategoryid[CurrentFirstCategoryid].products;

                switch (sortCartByFirstCategoryid[CurrentFirstCategoryid].sorttype)
                {
                    case SortType.SaleCount: templstprodcut = templstprodcut.OrderByDescending(r => r.salecount).ToList(); break;
                    case SortType.CreateDate: templstprodcut = templstprodcut.OrderByDescending(r => r.createdat).ToList(); break;
                    case SortType.SalePriceAsc: templstprodcut = templstprodcut.OrderBy(r => r.price.saleprice).ToList(); break;
                    case SortType.SalePriceDesc: templstprodcut = templstprodcut.OrderByDescending(r => r.price.saleprice).ToList(); break;
                    default: templstprodcut.OrderBy(r => r.salecount); break;
                }

                List<Product> tempIsLoadlstprodcut = templstprodcut.Where(r => r.isLoadPanel == true).ToList();

                List<Product> tempNotLoadlstprodcut = templstprodcut.Where(r => r.isLoadPanel == false).ToList();

                if (isnewType)
                {
                    dgvGoodPic.Rows.Clear();
                }
                if (templstprodcut.Count > dgrowscount)
                {
                    if (isnewType)
                    {
                        isnewType = false;
                        dgvGoodPic.Rows.Clear();
                        isnew = true;
                        switch (sortCartByFirstCategoryid[CurrentFirstCategoryid].sorttype)
                        {
                            case SortType.SaleCount: templstprodcut = templstprodcut.OrderByDescending(r => r.salecount).ToList(); break;
                            case SortType.CreateDate: templstprodcut = templstprodcut.OrderByDescending(r => r.createdat).ToList(); break;
                            case SortType.SalePriceAsc: templstprodcut = templstprodcut.OrderBy(r => r.price.saleprice).ToList(); break;
                            case SortType.SalePriceDesc: templstprodcut = templstprodcut.OrderByDescending(r => r.price.saleprice).ToList(); break;
                            default: templstprodcut.OrderBy(r => r.salecount); break;
                        }
                        tempNotLoadlstprodcut = templstprodcut;

                        tempIsLoadlstprodcut = new List<Product>();
                    }

                    if ((tempIsLoadlstprodcut.Count == 0 || isnew) && tempNotLoadlstprodcut.Count > 0)
                    {
                        DateTime starttime = DateTime.Now;
                        
                        ShowLoading(true);
                        int newcount = Math.Min(tempNotLoadlstprodcut.Count, 20);
                        List<Product> lstNewProduct = tempNotLoadlstprodcut.GetRange(0, newcount);

                        //防止转换json  死循环   bmp.tag 是product
                        lstNewProduct.ForEach(r => r.panelbmp = null);
                        lstNewProduct.ForEach(r=> r.isLoadPanel=false);
                        if (!MainModel.IsOffLine) { 
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
                        List<Product> templstpro = httputil.GetPanelProductPrice(panelpara, ref ErrorMsg,ref resultcode);
                        if (!string.IsNullOrEmpty(ErrorMsg) || templstpro == null)
                        {
                            CheckUserAndMember(resultcode,ErrorMsg);
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
                IsEnable = true;
                LogManager.WriteLog("ERROR", "加载面板商品异常" + ex.Message);
            }
            finally
            {
                IsEnable = true;
                ShowLoading(false);

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


                dgvGoodPic.Rows.Clear();

                int count = lstpro.Count;
                List<Bitmap> lstbmp = new List<Bitmap>();
                for (int i = 0; i < count; i++)
                {
                    lstbmp.Add(lstpro[i].panelbmp);
                    if (lstbmp.Count >= 4 || i >= count - 1)
                    {
                        int addcount = 4 - lstbmp.Count;
                        for (int j = 0; j < addcount; j++)
                        {
                            lstbmp.Add(ResourcePos.White);
                        }
                        dgvGoodPic.Rows.Add(lstbmp[0], lstbmp[1], lstbmp[2], lstbmp[3]);
                        lstbmp = new List<Bitmap>();
                    }
                }

                if (dgvGoodPic.Rows.Count > 4)
                {
                    pnlDgvRight.Visible = true;
                }
                else
                {
                    pnlDgvRight.Visible = false;
                }


            }
            catch (Exception ex)
            {
                ShowLoading(false);

            }
            finally
            {
                ShowLoading(false);

            }
        }


        private void dgvGoodPic_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (!IsEnable)
                {
                    return;
                }

                IsEnable = false;
                if (e.RowIndex < 0)
                    return;

                Bitmap bmp = (Bitmap)dgvGoodPic.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                if (bmp.Tag == null)  //空白单元格（无商品）
                {
                    return;
                }

                Product selepro = (Product)bmp.Tag;

                Product selectpro = new Product();
                selectpro = (Product)MainModel.Clone(selepro);
               
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

                if (selectpro.weightflag)
                {
                    
                    frmNumberBack frmnumberback = new frmNumberBack(selectpro.skuname, NumberType.ProWeight, ShowLocation.Center);

                    frmnumberback.Location = new Point(0, 0);
                    frmnumberback.ShowDialog();


                    if (frmnumberback.DialogResult == DialogResult.OK)
                    {
                        selectpro.specnum = (decimal)frmnumberback.NumValue / 1000;
                        selectpro.price.specnum = (decimal)frmnumberback.NumValue / 1000;
                        selectpro.num = 1;
                        //SelectProducts.Add(selectpro);
                        CurrentCart.products.Add(selectpro);
                    }
                    else
                    {
                        IsEnable = true;
                        return;
                    }

                    IsEnable = true;

                    selectpro.price.total = Math.Round(selectpro.price.saleprice * selectpro.price.specnum, 2, MidpointRounding.AwayFromZero);
                    selectpro.price.origintotal = Math.Round(selectpro.price.originprice * selectpro.price.specnum, 2, MidpointRounding.AwayFromZero);
                    selectpro.PaySubAmt = Math.Round(selectpro.price.saleprice * selectpro.price.specnum, 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    bool isExits = false;
                    foreach (Product pro in CurrentCart.products)
                    {
                        if (pro.skucode == selectpro.skucode && !pro.weightflag)
                        {
                            pro.num += 1;
                            pro.specnum = 1;
                            pro.price.specnum = 1;
                            isExits = true;
                            pro.price.total = Math.Round(pro.num * pro.price.saleprice, 2, MidpointRounding.AwayFromZero);
                            pro.price.origintotal = Math.Round(pro.num * pro.price.originprice, 2, MidpointRounding.AwayFromZero);
                            pro.PaySubAmt = Math.Round(pro.num * pro.price.saleprice, 2, MidpointRounding.AwayFromZero);
                            break;
                        }
                    }
                    if (!isExits)
                    {
                        selectpro.specnum = 1;
                        selectpro.price.total = Math.Round(selectpro.price.saleprice, 2, MidpointRounding.AwayFromZero);
                        selectpro.price.origintotal = Math.Round(selectpro.price.originprice, 2, MidpointRounding.AwayFromZero);
                        selectpro.PaySubAmt = Math.Round(selectpro.price.saleprice, 2, MidpointRounding.AwayFromZero);

                        CurrentCart.products.Add(selectpro);
                    }
                }


                UpdateDgvSelect(LastLstPro);
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("选择商品异常" + ex.Message, true);
            }
            finally {
                IsEnable = true;           
            }
        }

        private void UpdateDgvSelect(List<Product> lastlstpro)
        {
            try
            {
                IsEnable = false;

                //先清空图片 减少或删除商品后 不再购物车里面 后面无法清空
                foreach (Product selectpro in lastlstpro)
                {
                    Product modifypro = LstAllProduct.Where(r => r.skucode == selectpro.skucode).ToList()[0];
                    if (selectpro.weightflag)
                    {

                        modifypro.panelSelectNum = 0;
                        modifypro.panelbmp = GetItemImg(modifypro);
                    }
                    else
                    {
                        modifypro.panelSelectNum = 0;
                        modifypro.panelbmp = GetItemImg(modifypro);
                    }
                }


               
                ShowLoading(true);

                int selectcount = 0;

                string ErrorMsgCart = "";
                int ResultCode = 0;
                


                if (!MainModel.IsOffLine)
                {

                    Cart cart = httputil.RefreshCart(CurrentCart, ref ErrorMsgCart, ref ResultCode);

                    if (ErrorMsgCart != "" || cart == null) //商品不存在或异常
                    {
                        dgvGoodSelect.Rows.Clear();
                        CurrentCart.products = lastlstpro;
                        CheckUserAndMember(ResultCode, ErrorMsgCart);
                        MainModel.ShowLog(ErrorMsgCart,true);
                        return;
                    }
                    else
                    {
                        CurrentCart = cart;
                        dgvGoodSelect.Rows.Clear();

                        foreach (Product protemp in CurrentCart.products)
                        {
                            List<Image> lstbmp = GetSelect(protemp);
                            if (lstbmp != null && lstbmp.Count == 3)
                            {
                                dgvGoodSelect.Rows.Insert(0, new object[] { lstbmp[0], lstbmp[1], lstbmp[2] });

                                //singlecalculate.calculate(protemp);
                                if (!protemp.weightflag)  //0是标品  1是称重
                                {
                                    selectcount += protemp.num;
                                }
                                else
                                {
                                    selectcount++;
                                }
                            }
                        }
                        //for (int i = CurrentCart.products.Count; i > 0; i--)
                        //{
                        //    Product protemp = CurrentCart.products[i - 1];

                        //    List<Image> lstbmp = GetSelect(protemp);
                        //    if (lstbmp != null && lstbmp.Count == 3)
                        //    {
                        //        dgvGoodSelect.Rows.Insert(0, new object[] { lstbmp[0], lstbmp[1], lstbmp[2] });

                        //       //singlecalculate.calculate(protemp);
                        //        if (!protemp.weightflag)  //0是标品  1是称重
                        //        {
                        //            selectcount += protemp.num;
                        //        }
                        //        else
                        //        {
                        //            selectcount++;
                        //        }
                        //    }
                        //}
                    }
                }
                else
                {
                    dgvGoodSelect.Rows.Clear();

                    foreach (Product protemp in CurrentCart.products)
                    {
                        List<Image> lstbmp = GetSelect(protemp);
                        if (lstbmp != null && lstbmp.Count == 3)
                        {
                            dgvGoodSelect.Rows.Insert(0, new object[] { lstbmp[0], lstbmp[1], lstbmp[2] });

                            //singlecalculate.calculate(protemp);
                            if (!protemp.weightflag)  //0是标品  1是称重
                            {
                                selectcount += protemp.num;
                            }
                            else
                            {
                                selectcount++;
                            }
                        }
                    }
                    //for (int i = CurrentCart.products.Count; i > 0; i--)
                    //{

                    //    Product protemp = CurrentCart.products[i - 1];


                    //    List<Image> lstbmp = GetSelect(protemp);
                    //    if (lstbmp != null && lstbmp.Count == 3)
                    //    {

                    //        dgvGoodSelect.Rows.Insert(0, new object[] { lstbmp[0], lstbmp[1], lstbmp[2] });

                    //        //singlecalculate.calculate(protemp);
                    //        if (!protemp.weightflag)  //0是标品  1是称重
                    //        {
                    //            selectcount += protemp.num;
                    //        }
                    //        else
                    //        {
                    //            selectcount++;
                    //        }
                    //    }
                    //}
                }
                
                if (selectcount > 0)
                {
                    btnOK.BackColor = Color.OrangeRed;
                    btnOK.Text = "确定(" + selectcount + ")";
                    pnlWaiting.Visible = false;
                }
                else
                {
                    btnOK.BackColor = Color.DarkGray;
                    btnOK.Text = "确定(" + selectcount + ")";
                    pnlWaiting.Visible = true;
                }


               // LstAllProduct.ForEach(r => r.panelSelectNum = 0);
                foreach (Product pro in LstAllProduct)
                {
                    if (pro.panelSelectNum != 0)
                    {
                        pro.panelSelectNum = 0;
                        pro.panelbmp = GetItemImg(pro);
                    }
                }
                if (CurrentCart != null && CurrentCart.products != null)
                {
                    foreach (Product selectpro in CurrentCart.products)
                    {
                        Product modifypro = LstAllProduct.Where(r => r.skucode == selectpro.skucode).ToList()[0];
                        if (selectpro.weightflag)
                        {                          
                           modifypro.panelSelectNum += 1;
                           modifypro.panelbmp = GetItemImg(modifypro);
                        }
                        else
                        {
                            modifypro.panelSelectNum += selectpro.num;
                            modifypro.panelbmp = GetItemImg(modifypro);
                        }
                    }
                    Application.DoEvents();
                }

                if (dgvGoodPic.Visible)
                {
                    int oldrowindex = dgvGoodPic.FirstDisplayedScrollingRowIndex;
                    UpdateDgvGood(false,false);

                    dgvGoodPic.FirstDisplayedScrollingRowIndex = oldrowindex;
                }
                
                if (dgvGoodPicQuery.Visible)
                {

                    int oldrowindex = dgvGoodPicQuery.FirstDisplayedScrollingRowIndex;
                    UpdateDgvGoodByQuery();
                    dgvGoodPicQuery.FirstDisplayedScrollingRowIndex = oldrowindex;
                }
               
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载选择商品异常" + ex.Message, true);
            }
            finally
            {
                IsEnable = true;
                ShowLoading(false);
            }
        }


        private List<Image> GetSelect(Product pro)
        {
            try
            {
                Image first;
                Image second;
                Image third;
                lblNmae.AutoSize = true;
                lblNmae.Text = pro.title;
                if (pro.goodstagid==0)  //0是标品  1是称重
                {
                    if (lblNmae.Width > (btnSelectNum.Left - lblNmae.Left))
                    {
                        lblNmae.AutoSize = false;
                        lblNmae.Size = new Size(btnSelectNum.Left - lblNmae.Left, lblSelectPrice.Top);
                        lblNmae.Top = 0;
                    }
                    else
                    {
                        lblNmae.Top = (lblSelectPrice.Top - lblNmae.Height) / 2;
                    }
                    lblWeight.Visible = false;
                    btnSelectMinus.Visible = true;
                    btnSelectAdd.Visible = true;
                    btnSelectNum.Visible = true;
                    picDelect.Visible = false;

                    btnSelectNum.Text = pro.num.ToString();
                }
                else
                {
                    lblWeight.Text = pro.specnum.ToString() + pro.price.unit;
                    if (lblNmae.Width > (btnSelectAdd.Left - lblNmae.Left))
                    {
                        lblNmae.AutoSize = false;
                        lblNmae.Size = new Size(btnSelectAdd.Left - lblNmae.Left, lblSelectPrice.Top);
                        lblNmae.Top = 0;
                        lblWeight.Visible = false;
                    }
                    else if (lblNmae.Width + lblWeight.Width > (btnSelectAdd.Left - lblNmae.Left))
                    {
                        lblNmae.Top = 0;
                        lblWeight.Left = lblNmae.Left;
                        lblWeight.Top = lblNmae.Top + lblNmae.Height;
                        lblWeight.Visible = true;
                    }
                    else
                    {
                        lblNmae.Top = (lblSelectPrice.Top- lblNmae.Height) / 2;
                        lblWeight.Left = lblNmae.Left + lblNmae.Width;
                        lblWeight.Top = lblNmae.Top;
                        lblWeight.Visible = true;
                    }

                    btnSelectMinus.Visible = false;
                    btnSelectAdd.Visible = false;
                    btnSelectNum.Visible = false;
                    picDelect.Visible = true;

                 
                   
                }
                lblSelectPrice.Text = "￥" + pro.price.total.ToString("f2");

                Bitmap bmptotal;
                bmptotal = new Bitmap(pnlPicSelectItem.Width, pnlPicSelectItem.Height);
                pnlPicSelectItem.DrawToBitmap(bmptotal, new Rectangle(0, 0, pnlPicSelectItem.Width, pnlPicSelectItem.Height));

                int fitstwidth = btnSelectNum.Left;
                int secondwidth = btnSelectAdd.Left;
                int totalwidth = pnlPicSelectItem.Width;
                int height = pnlPicSelectItem.Height;

                //if (!pro.weightflag)
                //{
                //     secondwidth = btnSelectAdd.Left-5;
                //}
                //else
                //{
                //    secondwidth = btnSelectAdd.Left-5;
                //}

                Point point1 = new Point(0, 0);
                Point point2 = new Point(fitstwidth, 0);
                Point point3 = new Point(secondwidth, 0);


                first = MainModel.cutImage(bmptotal, point1, fitstwidth, height); first.Tag = pro;

                second = MainModel.cutImage(bmptotal, point2, secondwidth - fitstwidth, height); second.Tag = pro;
                third = MainModel.cutImage(bmptotal, point3, totalwidth - secondwidth, height); third.Tag = pro;


                List<Image> lstbmp = new List<Image>();
                lstbmp.Add(first);
                lstbmp.Add(second);
                lstbmp.Add(third);
                return lstbmp;
            }
            catch
            {
                return null;
            }
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            try
            {

                if (!IsEnable)
                {
                    return;
                }
                Button btn = (Button)sender;
                Product pro = (Product)btn.Tag;

                LastLstPro = new List<Product>();
                foreach (Product ppro in CurrentCart.products)
                {
                    LastLstPro.Add((Product)MainModel.Clone(ppro));
                }

                foreach (Product tpro in CurrentCart.products)
                {
                    if (tpro.barcode == pro.barcode)
                    {
                        if (tpro.num == 1)
                        {
                            FrmConfirmBack frmconfirmback = new FrmConfirmBack("是否确认删除商品？", tpro.skuname, "");
                            frmconfirmback.Location = new Point(0, 0);

                            if (frmconfirmback.ShowDialog() == DialogResult.OK)
                            {
                                CurrentCart.products.Remove(tpro);
                            }
                        }
                        else
                        {
                            tpro.num -= 1;
                        }
                        break;
                    }
                }             

                UpdateDgvSelect(LastLstPro);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("减少商品异常" + ex.Message);
            }
            finally
            {
                LoadPicScreen(false);
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            try
            {

                if (!IsEnable)
                {
                    return;
                }
                LastLstPro = new List<Product>();
                foreach (Product ppro in CurrentCart.products)
                {
                    LastLstPro.Add((Product)MainModel.Clone(ppro));
                }

                Button btn = (Button)sender;
                Product pro = (Product)btn.Tag;

                foreach (Product tpro in CurrentCart.products)
                {
                    if (tpro.barcode == pro.barcode)
                    {
                        tpro.num += 1;
                        tpro.specnum = 1;
                    }
                }
             

                UpdateDgvSelect(LastLstPro);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("增加商品异常" + ex.Message);
            }

        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {

                if (!IsEnable)
                {
                    return;
                }

                LastLstPro = new List<Product>();
                foreach (Product ppro in CurrentCart.products)
                {
                    LastLstPro.Add((Product)MainModel.Clone(ppro));
                }

                PictureBox pic = (PictureBox)sender;
                Product pro = (Product)pic.Tag;

                FrmConfirmBack frmconfirmback = new FrmConfirmBack("是否确认删除商品？", pro.skuname, "");
                frmconfirmback.Location = new Point(0, 0);
                if (frmconfirmback.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                CurrentCart.products.Remove(pro);
                UpdateDgvSelect(LastLstPro);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("增加商品异常" + ex.Message);
            }
            finally
            {
                LoadPicScreen(false);
            }


        }



        private void LoadPicScreen(bool isShown)
        {
            try
            {
                if (isShown)
                {
                    picScreen.BackgroundImage = MainModel.GetWinformImage(this);
                    picScreen.Size = new System.Drawing.Size(this.Width, this.Height);
                    picScreen.Visible = true;
                }
                else
                {
                    //picScreen.Size = new System.Drawing.Size(0, 0);
                    picScreen.Visible = false;
                }

                Application.DoEvents();
            }
            catch (Exception ex)
            {
                picScreen.Visible = false;
                LogManager.WriteLog("修改面板窗体背景图异常：" + ex.Message);
            }
        }

        private void picScreen_Click(object sender, EventArgs e)
        {
            LoadPicScreen(false);
        }

        #endregion



        #region  搜索商品
        private void btnQuery_Click(object sender, EventArgs e)
        {

            if (!IsEnable)
            {
                return;
            }
            if (string.IsNullOrEmpty(txtQuery.Text))
            {
                MainModel.ShowLog("请输入商品名称或商品条码", false);
            }
            else
            {
                UpdateDgvGoodByQuery();

                if (dgvGoodPicQuery.Rows.Count == 0)
                {
                    MainModel.ShowLog("未查到商品",false);
                }
            }
        }

        private void UpdateDgvGoodByQuery()
        {
            try
            {

                IsEnable = false;
                ShowLoading(true);
                dgvGoodPicQuery.Rows.Clear();


                LoadBtnSortStatus(querysorttype);
                string strquery = txtQuery.Text;
                List<Product> templstprodcut = LstAllProduct.Where(r => r.skuname.Contains(strquery) || r.skucode.Contains(strquery)).ToList();

                try
                {
                    switch (querysorttype)
                    {
                        case SortType.SaleCount: templstprodcut = templstprodcut.OrderByDescending(r => r.salecount).ToList(); break;
                        case SortType.CreateDate: templstprodcut = templstprodcut.OrderByDescending(r => r.createdat).ToList(); break;
                        case SortType.SalePriceAsc: templstprodcut = templstprodcut.OrderBy(r => r.price.saleprice).ToList(); break;
                        case SortType.SalePriceDesc: templstprodcut = templstprodcut.OrderByDescending(r => r.price.saleprice).ToList(); break;
                        default: templstprodcut = templstprodcut.OrderBy(r => r.salecount).ToList(); break;
                    }
                }
                catch (Exception ex)
                {

                }

                //查询最多查询20个
                int newcount = Math.Min(templstprodcut.Count, 20);
                templstprodcut = templstprodcut.GetRange(0, newcount);

                IsEnable = true;
                LoadDgvQuery(templstprodcut);

                ShowLoading(false);
                IsEnable = true; 

            }
            catch (Exception ex)
            {
                
                LogManager.WriteLog("ERROR", "加载面板商品异常" + ex.Message);
            }
            finally
            {
                ShowLoading(false);
                IsEnable = true;
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
                dgvGoodPicQuery.Rows.Clear();

                int count = lstpro.Count;
                List<Bitmap> lstbmp = new List<Bitmap>();
                for (int i = 0; i < count; i++)
                {
                    if (lstpro[i].panelbmp == null)
                    {
                        lstpro[i].panelbmp = GetItemImg(lstpro[i]);
                    }
                    lstbmp.Add(lstpro[i].panelbmp);
                    if (lstbmp.Count >= 4 || i >= count - 1)
                    {
                        int addcount = 4 - lstbmp.Count;
                        for (int j = 0; j < addcount; j++)
                        {
                            lstbmp.Add(ResourcePos.White);
                        }
                        dgvGoodPicQuery.Rows.Add(lstbmp[0], lstbmp[1], lstbmp[2], lstbmp[3]);
                        lstbmp = new List<Bitmap>();
                    }
                }

                if (dgvGoodPicQuery.Rows.Count > 4)
                {
                    pnlDgvRight.Visible = true;
                }
                else
                {
                    pnlDgvRight.Visible = false;
                }

            }
            catch (Exception ex)
            {
                ShowLoading(false);
            }
            finally
            {
                IsEnable = true;

                ShowLoading(false);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

            if (!IsEnable)
            {
                return;
            }
            txtQuery.Text = "";
            dgvGoodPic.Rows.Clear();
            UpdateDgvGood(false,false);
        }


        private object lockTextChange = new object();
        private void txtQuery_TextChanged(object sender, EventArgs e)
        {
            lock (lockTextChange)
            {
                try
                {
                    if (txtQuery.Text.Length > 0)
                    {
                        lblShuiyin.Visible = false;
                        dgvCategory.Visible = false;
                        dgvGoodPic.Visible = false;
                        dgvGoodPicQuery.Visible = true;
                    }
                    else
                    {
                        lblShuiyin.Visible = true;
                        dgvCategory.Visible = true;
                        dgvGoodPic.Visible = true;
                        dgvGoodPicQuery.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    MainModel.ShowLog("面板商品查询异常" + ex.Message, true);
                }
                }
          
        }

        private void dgvGoodPicQuery_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (!IsEnable)
                {
                    return;
                }
                if (e.RowIndex < 0)
                    return;
                IsEnable = false;
                Bitmap bmp = (Bitmap)dgvGoodPicQuery.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                if (bmp.Tag == null)  //空白单元格（无商品）
                {
                    return;
                }

                Product selepro = (Product)bmp.Tag;

                Product selectpro = new Product();
                selectpro =(Product) MainModel.Clone(selepro);

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

                if (selectpro.weightflag)
                {

                    frmNumberBack frmnumberback = new frmNumberBack(selectpro.skuname, NumberType.ProWeight, ShowLocation.Center);

                    frmnumberback.Location = new Point(0, 0);
                    frmnumberback.ShowDialog();

                    if (frmnumberback.DialogResult == DialogResult.OK)
                    {
                        selectpro.specnum = (decimal)frmnumberback.NumValue / 1000;
                        selectpro.price.specnum = (decimal)frmnumberback.NumValue / 1000;
                        selectpro.num = 1;
                        CurrentCart.products.Add(selectpro);
                    }
                    else
                    {
                        return;
                    }
                    selectpro.price.total = Math.Round(selectpro.price.saleprice * selectpro.price.specnum, 2, MidpointRounding.AwayFromZero);
                    selectpro.price.origintotal = Math.Round(selectpro.price.originprice * selectpro.price.specnum, 2, MidpointRounding.AwayFromZero);
                    selectpro.PaySubAmt = Math.Round(selectpro.price.saleprice * selectpro.price.specnum, 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    bool isExits = false;
                    foreach (Product pro in CurrentCart.products)
                    {
                        if (pro.skucode == selectpro.skucode && !pro.weightflag)
                        {
                            pro.num += 1;
                            pro.specnum = 1;
                            isExits = true;

                            pro.price.total = Math.Round(pro.num * pro.price.saleprice, 2, MidpointRounding.AwayFromZero);
                            pro.price.origintotal = Math.Round(pro.num * pro.price.originprice, 2, MidpointRounding.AwayFromZero);
                            pro.PaySubAmt = Math.Round(pro.num * pro.price.saleprice, 2, MidpointRounding.AwayFromZero);
                            break;
                        }
                    }
                    if (!isExits)
                    {
                        selectpro.specnum = 1;
                        selectpro.price.total = Math.Round(selectpro.price.saleprice, 2, MidpointRounding.AwayFromZero);
                        selectpro.price.origintotal = Math.Round(selectpro.price.originprice, 2, MidpointRounding.AwayFromZero);
                        selectpro.PaySubAmt = Math.Round(selectpro.price.saleprice, 2, MidpointRounding.AwayFromZero);
                        CurrentCart.products.Add(selectpro);
                    }
                }

                UpdateDgvSelect(LastLstPro);
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("选择商品异常" + ex.Message, true);
            }
            finally
            {
                IsEnable = true;
            }
        }

        #endregion



        #region OTHER

        int TopLblGoodName = 0;
        int HeightLblGoodName = 0;
        private Bitmap GetItemImg(Product pro)
        {

            try
            {
                lblGoodName.AutoSize = true;
                lblGoodName.Text = pro.skuname;

                if (lblGoodName.Width > pnlItem.Width - picItem.Width - 15)
                {
                    lblGoodName.AutoSize = false;
                    lblGoodName.Width = pnlItem.Width - picItem.Width - 15;
                    lblGoodName.Height = HeightLblGoodName * 2;
                    lblGoodName.Top = lblSkucode.Top - lblGoodName.Height;

                }
                else
                {
                    lblGoodName.Top = TopLblGoodName;

                }
                lblSkucode.Text = pro.skucode;
                //lblPrice.Text = pro.SALEPRICE.ToString("f2");
                lblPriceDetail.Text = "/" + pro.saleunit;

                string imgurl = pro.mainimg;
                string imgname = imgurl.Substring(imgurl.LastIndexOf("/") + 1) + ".bmp"; //URL 最后的值



                switch (pro.pricetagid)
                {
                    case 1: lblPriceTag.Text = pro.pricetag; lblPriceTag.BackColor = ColorTranslator.FromHtml("#FF7D14"); break;
                    case 2: lblPriceTag.Text = pro.pricetag; lblPriceTag.BackColor = ColorTranslator.FromHtml("#209FD4"); break;
                    case 3: lblPriceTag.Text = pro.pricetag; lblPriceTag.BackColor = ColorTranslator.FromHtml("#D42031"); break;
                    case 4: lblPriceTag.Text = pro.pricetag; lblPriceTag.BackColor = ColorTranslator.FromHtml("#250D05"); break;
                    default: lblPriceTag.Text = ""; break;
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
                    picItem.BackgroundImage = Image.FromFile(MainModel.ProductPicPath + imgname);
                }
                else
                {
                    try
                    {
                        Image _image = Image.FromStream(System.Net.WebRequest.Create(imgurl).GetResponse().GetResponseStream());
                        _image.Save(MainModel.ProductPicPath + imgname);
                        picItem.BackgroundImage = Image.FromFile(MainModel.ProductPicPath + imgname);
                    }
                    catch { }
                }




                if (pro.panelSelectNum > 0)
                {
                    btnRed.Text = pro.panelSelectNum.ToString();
                    btnRed.Visible = true;
                }
                else
                {
                    btnRed.Visible = false;
                }

                //获取单元格图片内容
                Bitmap b = new Bitmap(pnlItem.Width, pnlItem.Height);

                b.Tag = pro;
                pnlItem.DrawToBitmap(b, new Rectangle(0, 0, pnlItem.Width, pnlItem.Height));

                return b;

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("绘制面板商品图片异常"+ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 重绘商品容器 边框线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlItem_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                ControlPaint.DrawBorder(e.Graphics,
                                    this.pnlItem.ClientRectangle,
                                    Color.Gainsboro,//7f9db9
                                    1,
                                    ButtonBorderStyle.Solid,
                                    Color.Gainsboro,
                                    1,
                                    ButtonBorderStyle.Solid,
                                    Color.Gainsboro,
                                    1,
                                    ButtonBorderStyle.Solid,
                                    Color.Gainsboro,
                                    1,
                                    ButtonBorderStyle.Solid);

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("重绘面板商品边框异常"+ex.Message);
            }
        }

        #endregion


        private Image imgSelect;
        private Image imgNotSelect;

        private void btnOrderBySaleCount_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }
                Other.CrearMemory();
                SortType thissorttype = SortType.SaleCount;

                //已选择该排序，不需要再刷新
                if (btnOrderBySaleCount.BackgroundImage == imgSelect)
                {
                    return;
                }

                if (dgvGoodPic.Visible)
                {
                    sortCartByFirstCategoryid[CurrentFirstCategoryid].sorttype = thissorttype;

                    UpdateDgvGood(false,true);
                }
                else
                {
                    querysorttype = thissorttype;
                    UpdateDgvGoodByQuery();
                }

                LoadBtnSortStatus(thissorttype);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("销量排序异常"+ex.Message);
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

                Other.CrearMemory();
                SortType thissorttype = SortType.CreateDate;
                //已选择该排序，不需要再刷新
                if (btnOrderByCreateDate.BackgroundImage == imgSelect)
                {
                    return;
                }

                if (dgvGoodPic.Visible)
                {
                    sortCartByFirstCategoryid[CurrentFirstCategoryid].sorttype = thissorttype;
                    UpdateDgvGood(false,true);
                }
                else
                {
                    querysorttype = thissorttype;
                    UpdateDgvGoodByQuery();
                }

                LoadBtnSortStatus(thissorttype);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("上新排序异常" + ex.Message);
            }
        }

        private void btnOrderBySalePrice_Click(object sender, EventArgs e)
        {
            try { 

            if (!IsEnable)
            {
                return;
            }
            Other.CrearMemory();
            SortType thissorttype;
            if (btnOrderBySalePrice.Text == "价格↓")
            {
                thissorttype = SortType.SalePriceAsc;
            }
            else
            {
                thissorttype = SortType.SalePriceDesc;
            }

            if (dgvGoodPic.Visible)
            {
                sortCartByFirstCategoryid[CurrentFirstCategoryid].sorttype = thissorttype;
                UpdateDgvGood(false,true);
            }
            else
            {
                querysorttype = thissorttype;
                UpdateDgvGoodByQuery();
            }

            LoadBtnSortStatus(thissorttype);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("价格排序异常" + ex.Message);
            }
        }

        private void LoadBtnSortStatus(SortType sorttype)
        {
            try
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
            catch (Exception ex)
            {
                LogManager.WriteLog("更新面板排序按钮状态异常"+ex.Message);
            }
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

        private void btnWindows_Click(object sender, EventArgs e)
        {
            MainModel.ShowWindows();
        }

        private string currenttext = "";
        private void ScanCodeThread(object obj)
        {
            while (IsRun)
            {

                if (currenttext!=txtQuery.Text)
                {
                    try
                    {

                        currenttext = txtQuery.Text;
                        if (this.IsHandleCreated)
                        {
                            this.Invoke(new InvokeHandler(delegate()
                            {
                                UpdateDgvGoodByQuery();

                            }));
                        }
                        else
                        {
                            UpdateDgvGoodByQuery();

                        }

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
                    }
                }

                Thread.Sleep(100);
            }
        }

        private void dgvGoodSelect_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
                Image bmp = (Image)dgvGoodSelect.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                Product pro = (Product)bmp.Tag;

                if (e.ColumnIndex == 1 && pro.goodstagid==0) //减少
                {
                    if (pro.num == 1)
                    {

                        FrmConfirmBack frmconfirmback = new FrmConfirmBack("是否确认删除商品？", pro.skuname, "");
                        frmconfirmback.Location = new Point(0, 0);

                        if (frmconfirmback.ShowDialog() == DialogResult.OK)
                        {
                            CurrentCart.products.Remove(pro);
                        }

                    }
                    else
                    {
                        pro.num -= 1;
                    }
                    UpdateDgvSelect(CurrentCart.products);
                }

                if (e.ColumnIndex == 2 && pro.goodstagid == 0)//增加
                {
                    pro.num += 1;
                    pro.specnum = 1;
                    pro.price.specnum = 1;
                    UpdateDgvSelect(CurrentCart.products);
                }

                if (e.ColumnIndex == 2 && pro.goodstagid != 0)//删除
                {
                    FrmConfirmBack frmconfirmback = new FrmConfirmBack("是否确认删除商品？", pro.skuname, "");
                    frmconfirmback.Location = new Point(0, 0);

                    if (frmconfirmback.ShowDialog() == DialogResult.OK)
                    {
                        CurrentCart.products.Remove(pro);
                        UpdateDgvSelect(CurrentCart.products);
                    }
                }

            }
            catch (Exception ex)
            {
                MainModel.ShowLog("操作面板商品购物车异常" + ex.Message, true);
            }
        }

        private void frmPanelGoods_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Other.CrearMemory();
                IsRun = false;
                GlobalUtil.CloseOSK();
              //  Dispose();
            }
            catch { }

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
                IsEnable = false;
               
                string categoryid = dgvCategory.Rows[e.RowIndex].Cells["FirstcategoryID"].Value.ToString();


                for (int i = 0; i < dgvCategory.Rows.Count; i++)
                {
                    if (i == e.RowIndex)
                    {
                        dgvCategory.Rows[i].DefaultCellStyle.Font = new Font("微软雅黑", 14*MainModel.midScale,FontStyle.Bold);
                    }
                    else
                    {
                        dgvCategory.Rows[i].DefaultCellStyle.Font = new Font("微软雅黑", 12*MainModel.midScale);
                    }
                }      
                dgvGoodPic.Rows.Clear();
                CurrentFirstCategoryid = categoryid;
                //说明是第一次加载
                if (sender == null)
                {
                    UpdateDgvGood(true, true);
                }
                else
                {
                    UpdateDgvGood(false, false);
                }
                IsEnable = true;
            }
            catch (Exception ex)
            {
                IsEnable = true;
                MainModel.ShowLog("选择分类异常"+ex.Message,true);
            }
        }

        private void txt_Enter(object sender, EventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                GlobalUtil.OpenOSK();
                txt.Focus();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("焦点打开键盘异常" + ex.Message);
            }
        }

        private void txt_Leave(object sender, EventArgs e)
        {
            try
            {
                
                    GlobalUtil.CloseOSK();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("失去焦点关闭键盘异常" + ex.Message);
            }
        }

       
    }


    public enum SortType
    {
        SaleCount,
        CreateDate,
        SalePriceAsc,
        SalePriceDesc
    }

    public enum ExpiredType
    {
        None,
        UserExpired,
        MemberExpired,
        DifferentMember
    }
}
