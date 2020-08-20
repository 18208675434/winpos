using Maticsoft.BLL;
using Maticsoft.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.Resources;
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
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;
using ZhuiZhi_Integral_Scale_UncleFruit.MyControl;
using ZhuiZhi_Integral_Scale_UncleFruit.BrokenUI;

namespace ZhuiZhi_Integral_Scale_UncleFruit
{
    public partial class FormPanelGoodsForBroken : Form
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

        #region  页面初始化
        public FormPanelGoodsForBroken()
        {
            InitializeComponent();
            //使用委托的话frmmain界面会卡死
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void frmPanelGoods_Load(object sender, EventArgs e)
        {
            try
            {
                lblShopName.Text = MainModel.Titledata + "   " + MainModel.CurrentShopInfo.shopname;

                lblMenu.Text = MainModel.CurrentUser.nickname + ",你好";
                picMenu.Left = pnlMenu.Width - picMenu.Width - lblMenu.Width;
                lblMenu.Left = picMenu.Right;

                picLoading.Size = new Size(55, 55);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("加载报损面板异常"+ex.Message);
            }
        }

        private void frmPanelGoods_Shown(object sender, EventArgs e)
        {
            try
            {
                Application.DoEvents();
                keyBoard.Size = new System.Drawing.Size(dgvGood.Width, dgvGood.RowTemplate.Height * 3);


                imgPageUpForCategory = MainModel.GetControlImage(btnPreviousPageForCategoty);
                imgPageDownForCagegory = MainModel.GetControlImage(btnNextPageForCategory);

                imgPageUpForGood = MainModel.GetControlImage(btnPageUpForGood);

                imgPageDownForGood = MainModel.GetControlImage(btnPageDownForGood);

                LstAllProduct = CartUtil.LoadAllProduct(true);

                IniForm();
            }
            catch (Exception ex)
            {

            }
        }

    
       
        private void IniForm()
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

        #region  分类分页

        private Dictionary<string, string> sortCategory = new Dictionary<string,string>();
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
                if (page == 1)
                {
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
                        lastindex = startindex + 11;
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
                if (dgvCategory.Rows.Count > 0 && dgvGood.Rows.Count == 0)
                {
                    dgvCategory_CellClick(null, new DataGridViewCellEventArgs(0, 0));
                }


            }
            catch (Exception ex)
            {
               MainModel.ShowLog("加载分类异常" + ex.StackTrace, true);
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
                MainModel.ShowLog("选择分类异常" + ex.StackTrace, true);
            }
            finally
            {
                //btnScan.Select();
            }
        }

        #endregion


        #region 商品分页

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
                int lastindex = 29;
                int waitingcount = 0;

                bool havanextpage = false;
                bool havepreviousPage = false;
                if (page == 1)
                {
                    havepreviousPage = false;
                    startindex = 0;
                    if (AllCategoryPro.Count > 30)
                    {

                        lastindex = 28;
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
                    waitingcount = AllCategoryPro.Count - ((page - 1) * 28 + 1);  //第一页只有下一页  中间页都是上一页下一页 占用两个
                    startindex = (page - 1) * 28 + 1;

                    if (waitingcount > 29)
                    {
                        lastindex = startindex + 27;
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

                List<Product> lstLaodingPro = AllCategoryPro.GetRange(startindex, lastindex - startindex + 1);


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
                    List<Product> templstpro = httputil.GetPanelProductPrice(panelpara, ref ErrorMsg, ref resultcode);

                    Console.WriteLine("Good接口时间" + (DateTime.Now - starttime).TotalMilliseconds);
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


                int emptycount = 5 - lstshowimg.Count % 5;


                for (int i = 0; i < emptycount; i++)
                {
                    lstshowimg.Add(ResourcePos.empty);
                }

                int rowcount = lstshowimg.Count / 5;

                for (int i = 0; i < rowcount; i++)
                {
                    dgvGood.Rows.Add(lstshowimg[i * 5 + 0], lstshowimg[i * 5 + 1], lstshowimg[i * 5 + 2], lstshowimg[i * 5 + 3], lstshowimg[i * 5 + 4]);
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

            lblPriceDetail.Text = "/" + pro.saleunit;


            if (pro.price != null)
            {
                if (pro.price.saleprice == pro.price.originprice)
                {
                    lblPrice.Text = "￥" + pro.price.saleprice.ToString("f2");
                }
                else
                {
                    lblPrice.Text = "￥" + pro.price.saleprice.ToString("f2");
                }
            }
            else
            {
            }

            if (pro.price != null && pro.price.saleprice == pro.price.originprice)
            {
                lblPrice.Text = "￥" + pro.price.saleprice.ToString("f2");
            }
            else
            {
            }

            lblPriceDetail.Left = lblPrice.Left + lblPrice.Width - 3;


            Bitmap b = (Bitmap)MainModel.GetControlImage(pnlGoodNotSelect);
            b.Tag = pro;
            return b;
        }



        private void dgvGood_CellClick(object sender, DataGridViewCellEventArgs e)
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

                Bitmap bmp = (Bitmap)dgvGood.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;



                if (bmp == imgPageDownForGood)
                {
                    CurrentGoodPage++;
                    LoadDgvGood(false, false);
                    return;
                }

                if (bmp == imgPageUpForGood)
                {
                    CurrentGoodPage--;
                    LoadDgvGood(false, false);
                    return;
                }


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

                    decimal numbervalue = BrokenHelper.ShowBrokenScale(selectpro); //BrokenHelper.ShowBrokenNumber(selectpro.skuname);
                    if (numbervalue>0)
                    {
                        selectpro.specnum = numbervalue;
                        selectpro.price.specnum = numbervalue;
                        selectpro.num = 1;
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
            finally
            {
                IsEnable = true;
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
                int lastindex = 14;
                int waitingcount = 0;

                bool havanextpage = false;
                bool havepreviousPage = false;
                if (page == 1)
                {
                    havepreviousPage = false;
                    startindex = 0;
                    if (AllQueryPro.Count > 15)
                    {

                        lastindex = 13;
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
                    waitingcount = AllQueryPro.Count - ((page - 1) * 13 + 1);  //第一页只有下一页  中间页都是上一页下一页 占用两个
                    startindex = (page - 1) * 13 + 1;

                    if (waitingcount > 14)
                    {
                        lastindex = startindex + 12;
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
                    List<Product> templstpro = httputil.GetPanelProductPrice(panelpara, ref ErrorMsg, ref resultcode);

                    Console.WriteLine("Good接口时间" + (DateTime.Now - starttime).TotalMilliseconds);
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



                int emptycount = 5 - lstshowimg.Count % 5;


                for (int i = 0; i < emptycount; i++)
                {
                    lstshowimg.Add(ResourcePos.empty);
                }

                int rowcount = lstshowimg.Count / 5;

                for (int i = 0; i < rowcount; i++)
                {
                    dgvGoodQuery.Rows.Add(lstshowimg[i * 5 + 0], lstshowimg[i * 5 + 1], lstshowimg[i * 5 + 2], lstshowimg[i * 5 + 3], lstshowimg[i * 5 + 4]);
                }

                Console.WriteLine("Good画图时间" + (DateTime.Now - starttime).TotalMilliseconds);
                IsEnable = true;

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ERROR", "加载面板商品异常" + ex.Message);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
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
                    // btnScan.Select();
                    //ShowLog("请输入商品名称或商品条码", false);
                }
                else
                {
                    ShowLoading(true, false);
                    Application.DoEvents();

                    CurrentGoodQueryPage = 1;
                    LoadDgvQuery();

                    ShowLoading(false, true);
                }
            }
            catch (Exception ex)
            {
                ShowLoading(false, true);
                MainModel.ShowLog("查询面板商品异常" + ex.Message, true);
            }

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
                dgvGoodQuery.Visible = false;
                txtSearch.Clear();
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
                if (!IsEnable)
                {
                    return;
                }
                if (e.RowIndex < 0)
                    return;
                IsEnable = false;
                Bitmap bmp = (Bitmap)dgvGoodQuery.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                if (bmp == imgPageDownForGood) //下一页
                {
                    CurrentGoodQueryPage++;
                    LoadDgvQuery();
                    return;
                }

                if (bmp == imgPageUpForGood)//上一页
                {
                    CurrentGoodQueryPage--;
                    LoadDgvQuery();
                    return;
                }

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

                    decimal numbervalue = BrokenHelper.ShowBrokenScale(selectpro);// BrokenHelper.ShowBrokenNumber(selectpro.skuname);//broken NumberHelper.ShowFormNumber(selectpro.skuname, NumberType.ProWeight);
                    if (numbervalue > 0)
                    {
                        selectpro.specnum = numbervalue;
                        selectpro.price.specnum = numbervalue;
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

                    if (!ZhuiZhi_Integral_Scale_UncleFruit.HelperUI.ConfirmHelper.Confirm("确认返回", "返回后将清空已选商品，确认返回？"))
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
                        dgvCart.Rows.Clear();
                        CurrentCart.products = lastlstpro;
                        CheckUserAndMember(ResultCode, ErrorMsgCart);
                        MainModel.ShowLog(ErrorMsgCart, true);
                        return;
                    }
                    else
                    {
                        CurrentCart = cart;
                        LoadDgvCart();
                  
                    }
                }
                else
                {
                    dgvCart.Rows.Clear();

                    LoadDgvCart();
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

        private Bitmap GetCartRow(Product pro)
        {
            try
            {
                Image first;
                Image second;
                Image third;

                lblNmae.Text = pro.title;
                               
                lblSelectPrice.Text = "￥" + pro.price.total.ToString("f2");

                if (pro.goodstagid == 0)  //0是标品  1是称重
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

                    lblProNum.Text = pro.price.specnum + pro.price.unit;
                    lblProNum.Left = picMinus.Right;
                }


                Bitmap bmpPro = new Bitmap(pnlCartItem.Width, pnlCartItem.Height);
                bmpPro.Tag = pro;
                pnlCartItem.DrawToBitmap(bmpPro, new Rectangle(0, 0, pnlCartItem.Width, pnlCartItem.Height));

                bmpPro.MakeTransparent(Color.White);

                return bmpPro;
            }
            catch
            {
                return null;
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
                    this.Invoke(new InvokeHandler(delegate()
                    {
                        picLoading.Visible = showloading;

                    }));
                }
                else
                {
                    picLoading.Visible = showloading;
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
                Image bmp = (Image)dgvCart.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                Product pro = (Product)bmp.Tag;
                Point po = GlobalUtil.GetCursorPos();
                if (pro.goodstagid == 0 && po.X < (dgvCart.Left + picMinus.Right + 10) && po.X > (dgvCart.Left + picMinus.Left - 10)) //减少
                {
                    if (pro.num == 1)
                    {


                        if (!ZhuiZhi_Integral_Scale_UncleFruit.HelperUI.ConfirmHelper.Confirm("确认删除？"))
                        {
                            return;
                        }
                        else
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

                if (pro.goodstagid == 0 && po.X < (dgvCart.Left + picAdd.Right + 10) && po.X > (dgvCart.Left + picAdd.Left - 10))//增加
                {
                    pro.num += 1;
                    pro.specnum = 1;
                    pro.price.specnum = 1;
                    UpdateDgvSelect(CurrentCart.products);
                }

                if (po.X < (dgvCart.Left + picDelete.Right + 10) && po.X > (dgvCart.Left + picDelete.Left - 10))//删除
                {
                     
                    if (!ZhuiZhi_Integral_Scale_UncleFruit.HelperUI.ConfirmHelper.Confirm("确认删除？"))
                    {
                        return;
                    }
                    else
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

        private void frmPanelGoods_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsRun = false;
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
        }



        #region 购物车分页
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


        private void LoadDgvCart()
        {
               try
            {
                dgvCart.Rows.Clear();

                if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                {


                    pnlWaiting.Visible = false;

                    CurrentCart.products.Reverse();
                    rbtnPageUpForCart.WhetherEnable = CurrentCartPage > 1;

                    int selectcount = 0;

                    foreach (Product pro in CurrentCart.products)
                    {
                        selectcount += pro.num;
                    }


                    int startindex = (CurrentCartPage - 1) * 7;

                    int lastindex = Math.Min(CurrentCart.products.Count - 1, startindex + 6);

                    List<Product> lstLoadingPro = CurrentCart.products.GetRange(startindex, lastindex - startindex + 1);

                    foreach (Product por in lstLoadingPro)
                    {

                            dgvCart.Rows.Add( GetCartRow(por) );

                    }
                    CurrentCart.products.Reverse();


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

                    rbtnPageDownForCart.WhetherEnable = CurrentCart.products.Count > CurrentCartPage * 7;

                    Application.DoEvents();
                    dgvCart.ClearSelection();


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

        #endregion

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            keyBoard.Visible = true;

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
        }
    }

}
