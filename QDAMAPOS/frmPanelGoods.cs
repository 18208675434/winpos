using Maticsoft.BLL;
using Maticsoft.Model;
using QDAMAPOS.Common;
using QDAMAPOS.Model;
using QDAMAPOS.Model.Promotion;
using QDAMAPOS.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace QDAMAPOS
{
    public partial class frmPanelGoods : Form
    {
        /// <summary>
        /// 数据库所有可显示商品
        /// </summary>
        private List<Product> LstAllProduct = new List<Product>();

        private List<Product> CurrentLstProduct = new List<Product>();
        private DBPRODUCT_BEANBLL productbll = new DBPRODUCT_BEANBLL();
        private string  CurrentFirstCategoryid="";

        private HttpUtil httputil = new HttpUtil();

        private int dgvgoodwidth = 0;

        SortedDictionary<string, List<Product>> sortProductByFirstCategoryid = new SortedDictionary<string, List<Product>>();

        private ImplOfflineSingleCalculate singlecalculate = new ImplOfflineSingleCalculate(MainModel.CurrentShopInfo.tenantid, MainModel.CurrentShopInfo.shopid);



        //<summary>
        //按比例缩放页面及控件 
        //</summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();
        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();

        /// <summary>
        /// this.enable=false; 页面不可用页面不可控；  通过该标志控制页面是否可用
        /// </summary>
        private bool IsEnable = true;
        #region  页面初始化
        public frmPanelGoods()
        {
            InitializeComponent();
        }



        private void frmPanelGoods_Shown(object sender, EventArgs e)
        {
            try
            {
              
                Application.DoEvents();
                //LoadingHelper.ShowLoadingScreen("加载中");

                LoadProduct();

                IniForm();
                //Thread threadIniForm = new Thread(IniForm);
                //threadIniForm.IsBackground = true;
                //threadIniForm.Start();
                picLoading.Visible = false;

            }
            catch (Exception ex)
            {
                MainModel.ShowLog("页面加载异常"+ex.Message,true);
            }

        }



        private void frmPanelGoods_Load(object sender, EventArgs e)
        {
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
            btnMenu.Text = MainModel.CurrentUser.nickname + "，你好  ";
            timerNow.Interval = 1000;
            timerNow.Enabled = true;

            picLoading.Size = new Size(55, 55);
            dgvgoodwidth = dgvGoodPic.Width;

            dgvGoodPic.RowHeadersWidth = Convert.ToInt16(dgvGoodPic.RowHeadersWidth * MainModel.hScale);
        }
        private void LoadProduct()
        {
            DateTime starttime = DateTime.Now;
            List<DBPRODUCT_BEANMODEL> lstpro = productbll.GetModelList("PANELFLAG='1' and PANELSHOWFLAG=1 and STATUS =1 and CREATE_URL_IP='"+MainModel.URL+"' and SHOPID='"+MainModel.CurrentShopInfo.shopid+"' order by FIRSTCATEGORYID");
            Console.WriteLine("数据库访问时间" + (DateTime.Now - starttime).TotalMilliseconds);
            foreach (DBPRODUCT_BEANMODEL pro in lstpro)
            {
                Product product = new Product();
                product.mainimg = pro.MAINIMG;
                product.price = null;
                product.pricetag = pro.PRICETAG;
                product.pricetagid = pro.PRICETAGID;
                product.saleunit = pro.SALESUNIT;
                product.skucode = pro.SKUCODE;
                product.skuname = pro.SKUNAME;
                product.title = pro.SKUNAME;
                product.firstcategoryid = pro.FIRSTCATEGORYID;
                product.secondcategoryid = pro.SECONDCATEGORYID;
                pro.CATEGORYID = pro.CATEGORYID;
                product.firstcategoryname = pro.FIRSTCATEGORYNAME;
                product.barcode = pro.SKUCODE;
                product.weightflag = Convert.ToBoolean(pro.WEIGHTFLAG);
                product.shopid = pro.SHOPID;
                product.goodstagid = pro.WEIGHTFLAG;
                product.num = 1;

                if (MainModel.IsOffLine)
                {               
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
                }
                LstAllProduct.Add(product);
            }
            Console.WriteLine("数据转换时间" + (DateTime.Now - starttime).TotalMilliseconds);

        }


        private void IniForm()
        {
            SortedDictionary<string, string> sort = productbll.GetDiatinctCategory("PANELFLAG='1' and PANELSHOWFLAG=1 and STATUS =1 and CREATE_URL_IP='" + MainModel.URL +"' and SHOPID='"+MainModel.CurrentShopInfo.shopid+"'  order by FIRSTCATEGORYID");
            LoadSortProByFirstcategoryid(sort);
            LoadPnlCategory(sort);
        }

        private void LoadPnlCategory(SortedDictionary<string, string> sort)
        {
            foreach (KeyValuePair<string, string> kv in sort)
            {

                int count = pnlCategory.Controls.Count;
                Button btntemp = new Button();
                btntemp.Font = new System.Drawing.Font("微软雅黑", 11F * Math.Min(MainModel.hScale, MainModel.wScale), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                btntemp.Size = new System.Drawing.Size(pnlCategory.Width, 70);
                btntemp.Text = kv.Value;
                btntemp.Tag = kv.Key;
                btntemp.FlatStyle = FlatStyle.Flat;
                btntemp.FlatAppearance.BorderSize = 0;
              
                btntemp.FlatAppearance.MouseDownBackColor = Color.White;
                btntemp.FlatAppearance.MouseOverBackColor = Color.White;

                btntemp.Click += new System.EventHandler(btnCategory_Click);

                int inty = 0;
                int intx = 0;


                int left = 0;
                int top = count * (btntemp.Height);

            
             pnlCategory.Controls.Add(btntemp);
             btntemp.Location = new System.Drawing.Point(left, top);
             btntemp.Show();

            }

            if (pnlCategory.Controls.Count > 0)
            {

                if (this.IsHandleCreated)
                {
                        Button btn = (Button)pnlCategory.Controls[0];
                        if (btn.Enabled == true)
                        {
                            btnCategory_Click(btn, new EventArgs());
                        }
                    
                }
                else
                {
                    Button btn = (Button)pnlCategory.Controls[0];
                    if (btn.Enabled == true)
                    {
                        btnCategory_Click(btn, new EventArgs());
                    }
                }


            }
        }

        private void LoadSortProByFirstcategoryid(SortedDictionary<string, string> sort)
        {
            try
            {
                foreach (KeyValuePair<string, string> kv in sort)
                {
                    List<Product> lstpro =  LstAllProduct.Where(r => r.firstcategoryid == kv.Key).ToList();
                    if (lstpro != null && lstpro.Count > 0)
                    {
                        sortProductByFirstCategoryid.Add(kv.Key,lstpro);
                    }
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载分类面板商品异常"+ex.Message,false);
            }
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

                    this.Close();

                }
                else if (resultcode == MainModel.HttpMemberExpired)
                {
                    MainModel.CurrentMember = null;

                    ExpiredCode = ExpiredType.MemberExpired;

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
                this.Enabled = true;

                MainModel.ShowLog("面板商品验证用户/会员异常", true);

            }

        }
        #endregion


        #region  面板商品分类
        private void btnCategory_Click(object sender, EventArgs e)
        {

            if (!IsEnable)
            {
                return;
            }
            Button btn = (Button)sender;


            dgvGoodPic.Rows.Clear();

            foreach (Control con in pnlCategory.Controls)
            {
                con.BackColor = Color.Transparent;

                con.Font = new System.Drawing.Font("微软雅黑", 11F * Math.Min(MainModel.hScale, MainModel.wScale), FontStyle.Regular);
            }
          
            btn.BackColor = Color.White;
            btn.Font = new System.Drawing.Font("微软雅黑", 12F * Math.Min(MainModel.hScale, MainModel.wScale), FontStyle.Bold);

            CurrentFirstCategoryid = btn.Tag.ToString();

            UpdateDgvGood(false);

        }

      

        private void lblExit_Click(object sender, EventArgs e)
        {
            try
            {

                if (!IsEnable)
                {
                    return;
                }
                if (SelectProducts.Count > 0)
                {
                    FrmConfirmBack frmconfirmback = new FrmConfirmBack("确认返回", "返回后将清空已选商品，确认返回？", "");
                    frmconfirmback.Location = new Point(0, 0);

                    if (frmconfirmback.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    this.Close();
                }
                
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
            if (SelectProducts.Count > 0)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void dgvGoodPic_Scroll(object sender, ScrollEventArgs e)
        {



            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (e.NewValue + dgvGoodPic.DisplayedRowCount(false) == dgvGoodPic.Rows.Count)
                {

                    
                    if (sortProductByFirstCategoryid[CurrentFirstCategoryid].Count > 0 && sortProductByFirstCategoryid[CurrentFirstCategoryid].Count <= dgvGoodPic.Rows.Count * 4)
                    {
                        lblOver.Visible = true;
                    }
                    else
                    {
                        UpdateDgvGood(true);
                    }
                }
                else
                {
                    lblOver.Visible = false;
                }
            }

        }

        private void UpdateDgvGood(bool isnew)
        {
            try
            {
                int dgrowscount = dgvGoodPic.Rows.Count * 4;
                
                List<Product> templstprodcut = sortProductByFirstCategoryid[CurrentFirstCategoryid];
                List<Product> tempIsLoadlstprodcut = templstprodcut.Where(r => r.isLoadPanel == true).ToList();
                List<Product> tempNotLoadlstprodcut = templstprodcut.Where(r => r.isLoadPanel == false).ToList();

                if (templstprodcut.Count > dgrowscount)
                {
                  
                if ((tempIsLoadlstprodcut.Count == 0 || isnew) && tempNotLoadlstprodcut.Count>0)
                {
                    DateTime starttime = DateTime.Now;

                    IsEnable = false;
                    ShowLoading(true);

                    int newcount = Math.Min(tempNotLoadlstprodcut.Count, 20);
                    List<Product> lstNewProduct = tempNotLoadlstprodcut.GetRange(0, newcount);
                   
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
                    Console.WriteLine("面板商品价格访问时间 ：" + (DateTime.Now - starttime).TotalMilliseconds);
                    if (!string.IsNullOrEmpty(ErrorMsg) || templstpro == null)
                    {
                        CheckUserAndMember(resultcode, ErrorMsg);
                        //MainModel.ShowLog(ErrorMsg, true); 
                    }
                    else
                    {
                        IsEnable = true;
                        foreach (Product temppro in templstpro)
                        {

                    //                    this.BeginInvoke(new InvokeHandler(delegate()
                    //{
                        Product tempproduct = templstprodcut.Where(r => r.skucode == temppro.skucode).ToList()[0];

                        tempproduct.price = temppro.price;
                        tempproduct.pricetagid = temppro.pricetagid;
                        tempproduct.pricetag = temppro.pricetag;
                        tempproduct.isLoadPanel = true;
                        tempproduct.panelbmp = GetItemImg(tempproduct);

                        tempIsLoadlstprodcut.Add(tempproduct);
                   // }));

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
                    Application.DoEvents();
                    Console.WriteLine("面板商品加载时间：" + (DateTime.Now - starttime).TotalMilliseconds);
                }
                else
                {
                   
                    
                }


                IsEnable = true;
               // LoadingHelper.CloseForm();
                ShowLoading(false);
                }

                LoadDgv(tempIsLoadlstprodcut, 1, 1);
            }
            catch (Exception ex)
            {
                ShowLoading(false); 
                IsEnable = true;
                LogManager.WriteLog("ERROR","加载面板商品异常"+ex.Message);
            }
           
        }




        private void LoadDgv(List<Product> lstpro,int showItems,int addItems)
        {
            try
            {
                
                dgvGoodPic.Rows.Clear();

                int count = lstpro.Count;
                List<Bitmap> lstbmp = new List<Bitmap>();
                for (int i = 0; i < count; i++)
                {
                    if (MainModel.IsOffLine)
                    {
                        singlecalculate.calculate((lstpro[i]));
                    }
                    
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



            }
            catch (Exception ex)
            {
                ShowLoading(false);                //LoadingHelper.CloseForm();
            }
            finally
            {
                ShowLoading(false);
                //LoadingHelper.CloseForm();
            }
        }

        public List<Product> SelectProducts = new List<Product>();
        private void dgvGoodPic_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!IsEnable)
                {
                    return;
                }

                if (e.RowIndex < 0)
                    return;

                Bitmap bmp = (Bitmap)dgvGoodPic.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                if (bmp.Tag == null)  //空白单元格（无商品）
                {
                    return;
                }

                Product selepro = (Product)bmp.Tag;

                Product selectpro = new Product();
                selectpro = selepro.ThisClone();



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
                        SelectProducts.Add(selectpro);
                    }
                    else
                    {
                        return;
                    }
                    selectpro.price.total = Math.Round(selectpro.price.saleprice * selectpro.price.specnum, 2);
                    selectpro.price.origintotal = Math.Round(selectpro.price.originprice * selectpro.price.specnum, 2);
                    selectpro.PaySubAmt = Math.Round(selectpro.price.saleprice * selectpro.price.specnum, 2);

                }
                else
                {
                    bool isExits = false;
                    foreach (Product pro in SelectProducts)
                    {
                        if (pro.skucode == selectpro.skucode && !pro.weightflag)
                        {
                            pro.num += 1;
                            pro.specnum = 1;
                            pro.price.specnum = 1;
                            isExits = true;
                            pro.price.total = Math.Round(pro.num * pro.price.saleprice, 2);
                            pro.price.origintotal = Math.Round(pro.num * pro.price.originprice, 2);
                            pro.PaySubAmt = Math.Round(pro.num * pro.price.saleprice, 2);

                            break;
                        }
                    }
                    if (!isExits)
                    {
                        selectpro.specnum = 1;
                        selectpro.price.specnum = 1;
                        selectpro.price.total = Math.Round(selectpro.price.saleprice, 2);
                        selectpro.price.origintotal = Math.Round(selectpro.price.originprice, 2);
                        selectpro.PaySubAmt = Math.Round(selectpro.price.saleprice, 2);

                        SelectProducts.Add(selectpro);
                    }
                }


                UpdateDgvSelect();
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("选择商品异常" + ex.Message, true);
            }
            finally
            {
                LoadPicScreen(false);
            }
        }



        private List<Image> GetSelect(Product pro)
        {
            try
            {
                Image first;
                Image second;
                Image third;

                lblNmae.Text = pro.skuname;
                if (!pro.weightflag)  //0是标品  1是称重
                {
                    lblWeight.Visible = false;
                    btnSelectMinus.Visible = true;
                    btnSelectAdd.Visible = true;
                    lblSelectNum.Visible = true;
                    picDelect.Visible = false;

                    lblSelectNum.Text = pro.num.ToString();
                }
                else
                {
                    lblWeight.Visible = true;
                    btnSelectMinus.Visible = false;
                    btnSelectAdd.Visible = false;
                    lblSelectNum.Visible = false;
                    picDelect.Visible = true;

                    lblWeight.Text = pro.specnum.ToString() + pro.saleunit;
                    lblWeight.Left = lblNmae.Left + lblNmae.Width;
                }

                Bitmap bmptotal;
                bmptotal = new Bitmap(pnlPicSelectItem.Width, pnlPicSelectItem.Height);
                pnlPicSelectItem.DrawToBitmap(bmptotal, new Rectangle(0, 0, pnlPicSelectItem.Width, pnlPicSelectItem.Height));

                int fitstwidth =lblSelectNum.Left;
                int secondwidth = btnSelectAdd.Left-5;
                int totalwidth = pnlPicSelectItem.Width;
                int height=pnlPicSelectItem.Height;
                Point point1 = new Point(0,0);
                Point point2 = new Point(fitstwidth,0);
                Point point3 = new Point(secondwidth,0);


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



        private void UpdateDgvSelect() 
        {
            try
            {
                
               
                int selectcount = 0;
                dgvGoodSelect.Rows.Clear();
                for (int i = SelectProducts.Count; i > 0; i--)
                {

                    Product protemp = SelectProducts[i - 1];


                    List<Image> lstbmp = GetSelect(protemp);
                    if (lstbmp != null && lstbmp.Count == 3)
                    {

                        dgvGoodSelect.Rows.Insert(0, new object[] { lstbmp[0], lstbmp[1], lstbmp[2] });

                        singlecalculate.calculate(protemp);
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


                Application.DoEvents();
                if (selectcount > 0)
                {
                    btnOK.BackColor = Color.OrangeRed;
                    btnOK.Text = "确定("+selectcount+")";
                    pnlWaiting.Visible = false;
                }
                else
                {
                    btnOK.BackColor = Color.DarkGray;
                    btnOK.Text = "确定(" + selectcount + ")";
                    pnlWaiting.Visible = true;
                }

                foreach (Product pro in LstAllProduct)
                {
                    if (pro.panelSelectNum != 0)
                    {
                        pro.panelSelectNum = 0;
                        pro.panelbmp = GetItemImg(pro);
                    }
                }
                if (SelectProducts != null && SelectProducts.Count != null)
                {
                    foreach (Product selectpro in SelectProducts)
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
                    UpdateDgvGood(false);

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
                MainModel.ShowLog("加载选择商品异常"+ex.Message,true);
            }
        }
        #endregion


        #region OTHER
        private Bitmap GetItemImg(Product pro)
        {
            lblGoodName.Text = pro.skuname;
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
                case 4: lblPriceTag.Text = pro.pricetag; lblPriceTag.BackColor = ColorTranslator.FromHtml("#FF000"); break;
                default: lblPriceTag.Text = ""; break;
            }

            if (pro.price != null)
            {
                if (pro.price.saleprice == pro.price.originprice)
                {
                    lblPrice.Text ="￥"+ pro.price.saleprice.ToString("f2");
                    lblMemberPrice.Visible = false;
                }
                else
                {
                    lblPrice.Text = "￥" + pro.price.saleprice.ToString("f2");
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
                lblPrice.Text = "￥" + pro.price.saleprice.ToString("f2");
            }
            else
            {
            }

            lblPriceDetail.Left = lblPrice.Left + lblPrice.Width;

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
        /// <summary>
        /// 重绘商品容器 边框线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlItem_Paint(object sender, PaintEventArgs e)
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
                MainModel.ShowLog("请输入商品名称或商品条码",false );
            }
            else
            {
                UpdateDgvGoodByQuery();
            }
        }

        private void UpdateDgvGoodByQuery()
        {
            try
            {
                dgvGoodPicQuery.Rows.Clear();

                string strquery = txtQuery.Text;
                List<Product> templstprodcut = LstAllProduct.Where(r => r.skuname.Contains(strquery) || r.skucode.Substring(2, 5) == strquery.PadLeft(5, '0') || r.skucode==strquery).ToList();
                
                    LoadDgvQuery(templstprodcut);
               
            }
            catch (Exception ex)
            {
                IsEnable = true;
                this.Enabled = true;
                LogManager.WriteLog("ERROR", "加载面板商品异常" + ex.Message);
            }
        }

        private void LoadDgvQuery(List<Product> lstpro)
        {
            try
            {

                dgvGoodPicQuery.Rows.Clear();

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
                        dgvGoodPicQuery.Rows.Add(lstbmp[0], lstbmp[1], lstbmp[2], lstbmp[3]);
                        lstbmp = new List<Bitmap>();
                    }
                }

            }
            catch (Exception ex)
            {
                ShowLoading(false);               // LoadingHelper.CloseForm();
            }
            finally
            {
                ShowLoading(false);               // LoadingHelper.CloseForm();
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
            UpdateDgvGood(false);
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
                        pnlCategory.Visible = false;

                        pnlCategory.Visible = false;
                        dgvGoodPic.Visible = false;
                        dgvGoodPicQuery.Visible = true;

                        UpdateDgvGoodByQuery();
                    }
                    else
                    {
                        lblShuiyin.Visible = true;
                        pnlCategory.Visible = true;

                        pnlCategory.Visible = true;
                        dgvGoodPic.Visible = true;
                        dgvGoodPicQuery.Visible = false;
                    }

                    //Application.DoEvents();
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog("模糊查询异常" + ex.Message);
                }
            }
        }


        //private void dgvGoodSelect_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    try
        //    {
        //        if (!IsEnable)
        //        {
        //            return;
        //        }

        //        if (e.RowIndex < 0)
        //            return;

        //        string num = dgvGoodSelect.Rows[e.RowIndex].Cells["selectnum"].Value.ToString();
        //        int serialno = Convert.ToInt16(dgvGoodSelect.Rows[e.RowIndex].Cells["serialno"].Value.ToString());

        //        if (e.ColumnIndex == 2 && !string.IsNullOrEmpty(num))
        //        {
        //            if (SelectProducts[serialno].num == 1)
        //            {
        //                LoadPicScreen(true);
        //                frmDeleteGood frmdelete = new frmDeleteGood("是否确认删除商品？", SelectProducts[serialno].skuname, "");

        //                if (frmdelete.ShowDialog() == DialogResult.OK)
        //                {
        //                    SelectProducts.RemoveAt(serialno);
        //                }
        //                this.Enabled = true;
        //            }
        //            else
        //            {
        //                SelectProducts[serialno].num -= 1;
        //            }

        //            UpdateDgvSelect();
        //        }


        //        if (e.ColumnIndex == 4)
        //        {
        //            if (string.IsNullOrEmpty(num)) //删除
        //            {
        //                LoadPicScreen(true);
        //                frmDeleteGood frmdelete = new frmDeleteGood("是否确认删除商品？", SelectProducts[serialno].skuname, "");
        //                if (frmdelete.ShowDialog() != DialogResult.OK)
        //                {
        //                    return;
        //                }

        //                SelectProducts.RemoveAt(serialno);
        //            }
        //            else
        //            {
        //                SelectProducts[serialno].num += 1;
        //                SelectProducts[serialno].specnum = 1;
        //            }
        //            UpdateDgvSelect();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MainModel.ShowLog("操作面板已选择商品异常" + ex.Message, true);
        //    }
        //    finally
        //    {
        //        LoadPicScreen(false);
        //    }
        //}



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

                Bitmap bmp = (Bitmap)dgvGoodPicQuery.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                if (bmp.Tag == null)  //空白单元格（无商品）
                {
                    return;
                }

                Product selepro = (Product)bmp.Tag;

                Product selectpro = new Product();
                selectpro = selepro.ThisClone();


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
                        SelectProducts.Add(selectpro);
                    }
                    else
                    {
                        return;
                    }
                    selectpro.price.total = Math.Round(selectpro.price.saleprice * selectpro.price.specnum, 2);
                    selectpro.price.origintotal = Math.Round(selectpro.price.originprice * selectpro.price.specnum, 2);
                    selectpro.PaySubAmt = Math.Round(selectpro.price.saleprice * selectpro.price.specnum, 2);
                }
                else
                {
                    bool isExits = false;
                    foreach (Product pro in SelectProducts)
                    {
                        if (pro.skucode == selectpro.skucode && !pro.weightflag)
                        {
                            pro.num += 1;
                            pro.specnum = 1;
                            pro.price.specnum = 1;
                            isExits = true;
                            pro.price.total = Math.Round(pro.num * pro.price.saleprice, 2);
                            pro.price.origintotal = Math.Round(pro.num * pro.price.originprice, 2);
                            pro.PaySubAmt = Math.Round(pro.num * pro.price.saleprice, 2);
                            break;
                        }
                    }
                    if (!isExits)
                    {
                        selectpro.specnum = 1;
                        selectpro.price.specnum = 1;
                        selectpro.price.total = Math.Round(selectpro.price.saleprice, 2);
                        selectpro.price.origintotal = Math.Round(selectpro.price.originprice, 2);
                        selectpro.PaySubAmt = Math.Round(selectpro.price.saleprice, 2);
                        SelectProducts.Add(selectpro);
                    }
                }


                UpdateDgvSelect();
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("选择商品异常" + ex.Message, true);
            }
            finally
            {
                LoadPicScreen(false);
            }
        }

        #endregion

        private void lblShuiyin_Click(object sender, EventArgs e)
        {
            if (!IsEnable)
            {
                return;
            }
            txtQuery.Focus();
        }


        private void timerNow_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void btnWindows_Click(object sender, EventArgs e)
        {
            MainModel.ShowWindows();
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




        private Panel GetSelectPanel(Product pro)
        {
            Panel pnlResult = new Panel();
            Label lblSelectNameTemp = new Label();
            Label lblSelectWeightTemp = new Label();
            Label lblSelectNumTemp = new Label();
            Button btnSelectTempAdd = new Button();
            Button btnSelectTempMinus = new Button();
            PictureBox picSelectTempDel = new PictureBox();

            pnlResult.BackColor = System.Drawing.Color.White;
            pnlResult.Controls.Add(lblSelectNameTemp);
       


            pnlResult.Size = pnlPicSelectItem.Size;

            lblSelectNameTemp.AutoSize = lblNmae.AutoSize;
            lblSelectNameTemp.Font = lblNmae.Font;
            lblSelectNameTemp.ForeColor = lblNmae.ForeColor;
            lblSelectNameTemp.Location = lblNmae.Location;
            lblSelectNameTemp.Size = lblNmae.Size;
            lblSelectNameTemp.Text = pro.skuname;



              if (!pro.weightflag)  //0是标品  1是称重
                        {

                            

                             
            pnlResult.Controls.Add(btnSelectTempAdd);
            pnlResult.Controls.Add(btnSelectTempMinus);
            pnlResult.Controls.Add(lblSelectNumTemp);



         


            btnSelectTempMinus.FlatAppearance.BorderColor = btnSelectMinus.FlatAppearance.BorderColor;
            btnSelectTempMinus.FlatStyle = btnSelectMinus.FlatStyle;
            btnSelectTempMinus.Font = btnSelectMinus.Font;
            btnSelectTempMinus.Location = btnSelectMinus.Location;
            btnSelectTempMinus.Size = btnSelectMinus.Size;
            btnSelectTempMinus.Text = "一";
            btnSelectTempMinus.UseVisualStyleBackColor = btnSelectMinus.UseVisualStyleBackColor;
            btnSelectTempMinus.Click += btnMinus_Click;
            btnSelectTempMinus.Tag = pro;
            // 
            // btnSelectMinus
            // 
            btnSelectTempAdd.FlatAppearance.BorderColor = btnSelectAdd.FlatAppearance.BorderColor;
            btnSelectTempAdd.FlatStyle = btnSelectAdd.FlatStyle;
            btnSelectTempAdd.Font = btnSelectAdd.Font;
            btnSelectTempAdd.Location = btnSelectAdd.Location;
            btnSelectTempAdd.Size = btnSelectAdd.Size;
            btnSelectTempAdd.Text = "十";
            btnSelectTempAdd.UseVisualStyleBackColor = true;
            btnSelectTempAdd.Click += btnAdd_Click;
            btnSelectTempAdd.Tag = pro;

            lblSelectNumTemp.BorderStyle = lblSelectNum.BorderStyle;
            lblSelectNumTemp.Font = lblSelectNum.Font;
            lblSelectNumTemp.Location = btnSelectTempMinus.Location;
            lblSelectNumTemp.Size = new System.Drawing.Size(btnSelectAdd.Right - btnSelectMinus.Left, btnSelectMinus.Height);
            lblSelectNumTemp.Text = pro.num.ToString();
            lblSelectNumTemp.TextAlign = lblSelectNum.TextAlign;

                        }
                        else
                        {
                            pnlResult.Controls.Add(lblSelectWeightTemp);
                            pnlResult.Controls.Add(picSelectTempDel);
                            lblSelectWeightTemp.AutoSize = true;
                            lblSelectWeightTemp.Font = lblWeight.Font;
                            lblSelectWeightTemp.ForeColor = lblWeight.ForeColor;
                            lblSelectWeightTemp.Location = lblWeight.Location;
                            lblSelectWeightTemp.Size = lblWeight.Size;
                            lblSelectWeightTemp.Text = pro.specnum.ToString() + pro.saleunit;
                            lblSelectWeightTemp.Left = lblSelectNameTemp.Left + lblSelectNameTemp.Width;


                            picSelectTempDel.BackgroundImage = picDelect.BackgroundImage;
                            picSelectTempDel.BackgroundImageLayout = picDelect.BackgroundImageLayout;
                            picSelectTempDel.Location = picDelect.Location;
                            picSelectTempDel.Size = picDelect.Size;
                           picSelectTempDel.TabStop = false;
                           picSelectTempDel.Tag = pro;
                           picSelectTempDel.Click += btnDel_Click;
                        }


            
              Panel pnlLine = new Panel();
              pnlLine.Location = new Point(0, pnlPicSelectItem.Height - 2);
              pnlLine.Size = new Size(pnlPicSelectItem.Width+10, 1);
            
              pnlLine.BackColor = Color.Silver;
              pnlResult.Controls.Add(pnlLine);
          
            return pnlResult;

        }


        private void btnMinus_Click(object sender, EventArgs e)
        {
            try 
            {
                Button btn = (Button)sender;
                Product pro =(Product) btn.Tag;
                if (pro.num == 1)
                {

                    FrmConfirmBack frmconfirmback = new FrmConfirmBack("是否确认删除商品？", pro.skuname, "");
                    frmconfirmback.Location = new Point(0, 0);

                    if (frmconfirmback.ShowDialog() == DialogResult.OK)
                    {
                        SelectProducts.Remove(pro);
                    }
                    
                }
                else
                {
                    pro.num -= 1;
                }

                UpdateDgvSelect();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("减少商品异常"+ex.Message);
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
                Button btn = (Button)sender;
                Product pro = (Product)btn.Tag;

                pro.num += 1;
                pro.specnum = 1;
                pro.price.specnum = 1;
                UpdateDgvSelect();
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
                PictureBox pic = (PictureBox)sender;
                Product pro = (Product)pic.Tag;

                FrmConfirmBack frmconfirmback = new FrmConfirmBack("是否确认删除商品？", pro.skuname, "");
                frmconfirmback.Location = new Point(0, 0);
                if (frmconfirmback.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                SelectProducts.Remove(pro); 
                UpdateDgvSelect();
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

                if (e.ColumnIndex == 1 && !pro.weightflag) //减少
                {
                    if (pro.num == 1)
                    {

                        FrmConfirmBack frmconfirmback = new FrmConfirmBack("是否确认删除商品？", pro.skuname, "");
                        frmconfirmback.Location = new Point(0, 0);

                        if (frmconfirmback.ShowDialog() == DialogResult.OK)
                        {
                            SelectProducts.Remove(pro);
                        }

                    }
                    else
                    {
                        pro.num -= 1;
                    }
                    UpdateDgvSelect();
                }

                if (e.ColumnIndex == 2 && !pro.weightflag)//增加
                {
                    pro.num += 1;
                    pro.specnum = 1;
                    pro.price.specnum = 1;
                    UpdateDgvSelect();
                }

                if (e.ColumnIndex == 2 && pro.weightflag)//删除
                {
                    FrmConfirmBack frmconfirmback = new FrmConfirmBack("是否确认删除商品？", pro.skuname, "");
                    frmconfirmback.Location = new Point(0, 0);

                    if (frmconfirmback.ShowDialog() == DialogResult.OK)
                    {
                        SelectProducts.Remove(pro);

                        UpdateDgvSelect();
                    }

                  
                }

            }
            catch (Exception ex)
            {
                MainModel.ShowLog("操作面板商品购物车异常"+ex.Message,true);
            }
        }


        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;
        //        return cp;
        //    }
        //}

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
