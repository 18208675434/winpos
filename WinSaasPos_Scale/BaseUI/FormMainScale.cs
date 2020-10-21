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
    public partial class FormMainScale : Form
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
        public FormMainScale()
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
            LoadBaseInfo();

            BaseUIHelper.ShowFormMainMedia();
            BaseUIHelper.IniFormMainMedia();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();

            ShowLoading(true,false);

           // BaseUIHelper.CloseFormMain();
            LstAllProduct = CartUtil.LoadAllProduct(true);

            if (LstAllProduct == null || LstAllProduct.Count == 0)
            {
                
                DataUtil.LoadAllProduct();
                LstAllProduct = CartUtil.LoadAllProduct(true);
            }
            else
            {
                //启动全量商品同步线程
                threadLoadAllProduct = new Thread(DataUtil.LoadAllProduct);
                threadLoadAllProduct.IsBackground = true;
                threadLoadAllProduct.Start();
            }
            IniForm();
            ShowLoading(false,true);
            timerScale.Enabled = true;           
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

            }
            //BaseUIHelper.CloseFormMainMedia();
        }
        #endregion


        #region 菜单栏功能按钮
        private void btnWindows_Click(object sender, EventArgs e)
        {
            MainModel.ShowWindows();
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

        private FormToolMainScale frmtoolmainscale = null;
        private void btnMenu_Click(object sender, EventArgs e)
        {
            try
            {

                if (!IsEnable)
                {
                    return;
                }

                if (frmtoolmainscale == null)
                {
                    frmtoolmainscale = new FormToolMainScale();

                    asf.AutoScaleControlTest(frmtoolmainscale, 178, 160, Convert.ToInt32(MainModel.wScale * 178), Convert.ToInt32(MainModel.hScale * 160), true);
                    frmtoolmainscale.DataReceiveHandle += frmToolMain_DataReceiveHandle;
                    frmtoolmainscale.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width - frmtoolmainscale.Width - 15, pnlHead.Height + 10);
                    frmtoolmainscale.TopMost = true;
                    frmtoolmainscale.Show();
                }
                else
                {
                    frmtoolmainscale.Show();
                }

            }
            catch (Exception ex)
            {
                frmtoolmainscale = null;
                MainModel.ShowLog("菜单窗体显示异常" + ex.Message, true);
            }
        }


        private void frmToolMain_DataReceiveHandle(ToolType tooltype)
        {
            try
            {
                
                if (tooltype == ToolType.Exit)
                {
                    this.Invoke(new InvokeHandler(delegate()
                    {
                        tsmExit_Click(null, null);

                    }));
                }


                if (tooltype == ToolType.ScaleModel)
                {
                    this.Invoke(new InvokeHandler(delegate()
                    {
                        ChangeScaleModel();

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
                BaseUIHelper.CloseFormMainScale();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("退出系统异常" + ex.Message);
            }            
        }

        private void ChangeScaleModel()
        {
            try
            {
                if (ChangeScaleModelHelper.Confirm(ChangeModel.OnlyScale))
                {
                    this.DialogResult = DialogResult.Retry;
                    this.Close();
                   ////BaseUIHelper.ShowFormMain();
                   // this.Close();
                   // //this.Dispose();
                   // FormMain frmmain = new FormMain();
                   // asf.AutoScaleControlTest(frmmain, 1180, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                   // frmmain.ShowDialog();
                   // frmmain.Dispose();

                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("切换秤模式异常" + ex.Message, true);
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
            
        }

        private void pbtnClearSearch_Click(object sender, EventArgs e)
        {
            try
            {
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

               
                    List<Product> lstNotHaveImg = lstpro.Where(r => r.panelbmp == null).ToList();
                    foreach (Product pro in lstNotHaveImg)
                    {
                        pro.panelbmp = GetItemImg(pro);
                    }                 
                    switch (querysorttype)
                    {
                        case SortType.SaleCount: lstpro = lstpro.OrderByDescending(r => r.salecount).ToList(); break;
                        case SortType.CreateDate: lstpro = lstpro.OrderByDescending(r => r.createdat).ToList(); break;
                        case SortType.SalePriceAsc: lstpro = lstpro.OrderBy(r => r.price.saleprice).ToList(); break;
                        case SortType.SalePriceDesc: lstpro = lstpro.OrderByDescending(r => r.price.saleprice).ToList(); break;
                        default: lstpro = lstpro.OrderBy(r => r.salecount).ToList(); break;
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
                ShowLoading(true, false);
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

                ShowLoading(true, false);
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

                ShowLoading(true, false);
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

                if (rowscount == 1)
                {
                    dgvCategory.Height = dgvCategory.RowTemplate.Height + 10;
                    tlpGood.RowStyles[0] = new RowStyle(SizeType.Absolute, dgvCategory.Height);
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
                MainModel.ShowLog("加载分类异常" + ex.StackTrace, true);
            }
        }

        private void PackDownCategory()
        {
            try
            {

                int whitecount = 7 - (sortCategory.Count + 1) % 7;
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


                dgvCategory.Height = rowscount * dgvCategory.RowTemplate.Height + 10;

                tlpGood.RowStyles[0] = new RowStyle(SizeType.Absolute, dgvCategory.Height);
                dgvGood.Height = pnlDgvGood.Height;

            }
            catch (Exception ex)
            {
                MainModel.ShowLog("展开分类异常" + ex.Message, true);
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

                dgvCategory.Height = 2 * dgvCategory.RowTemplate.Height + 10;

                tlpGood.RowStyles[0] = new RowStyle(SizeType.Absolute, dgvCategory.Height);
                dgvGood.Height = pnlDgvGood.Height;
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("收起分类异常" + ex.Message, true);
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
                    }

                    if ((tempIsLoadlstprodcut.Count == 0 || isnew) && tempNotLoadlstprodcut.Count > 0)
                    {
                        DateTime starttime = DateTime.Now;
                        int newcount = Math.Min(tempNotLoadlstprodcut.Count, 18);
                        List<Product> lstNewProduct = tempNotLoadlstprodcut.GetRange(0, newcount);

                        //防止转换json  死循环   bmp.tag 是product
                        lstNewProduct.ForEach(r => r.panelbmp = null);

                            foreach (Product pro in lstNewProduct)
                            {
                                if (!pro.isLoadPanel)
                                {
                                    pro.isLoadPanel = true;
                                    pro.panelbmp = GetItemImg(pro);

                                    tempIsLoadlstprodcut.Add(pro);
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
                List<Product> lstNotHaveImg = lstpro.Where(r => r.panelbmp == null).ToList();
                foreach (Product pro in lstNotHaveImg)
                {
                    pro.panelbmp = GetItemImg(pro);
                }
                switch (querysorttype)
                {
                    case SortType.SaleCount: lstpro = lstpro.OrderByDescending(r => r.salecount).ToList(); break;
                    case SortType.CreateDate: lstpro = lstpro.OrderByDescending(r => r.createdat).ToList(); break;
                    case SortType.SalePriceAsc: lstpro = lstpro.OrderBy(r => r.price.saleprice).ToList(); break;
                    case SortType.SalePriceDesc: lstpro = lstpro.OrderByDescending(r => r.price.saleprice).ToList(); break;
                    default: lstpro = lstpro.OrderBy(r => r.salecount).ToList(); break;
                }
                

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



        private void SelectProduct(Product pro)
        {
            try
            {
                //打开批量打码 不清空
                if (BatchPrint && pro == null)
                {
                    return;
                }

                CurrentSelectProduct = pro;

                if(pro==null){
                    ClearDgvGoodSelect();
                    ClearDgvGoodQuerySelect();

                    lblCurrentSkuName.Text="未选择商品";
                    lblItemPrice.Text="0.00";
                    lblTotalPrice.Text="0.00";
                }else{

                    LastSkuCode = pro.skucode;
                    CurrentSelectProduct.panelSelectNum += 1;
                    lblItemPrice.Text = pro.price.saleprice.ToString("f2");
                    if(pro.goodstagid==0){
                        lblCurrentSkuName.Text = "";

                        lblTotalPrice.Text=pro.price.saleprice.ToString("f2");
                        if (AutoPrint)
                        {
                            LabelPrintHelper.LabelPrint(pro);
                            SelectProduct(null);
                        }

                    }
                    else
                    {
                        lblCurrentSkuName.Text = pro.skuname;

                    }
                   
                    
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("解析选择商品异常" + ex.Message, true);
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
                LogManager.WriteLog("清除商品列表选择项异常"+ex.Message);
            }
        }


        private void ClearDgvGoodQuerySelect()
        {
            try
            {
                if (dgvGoodQuery.Rows.Count <= 0 || !dgvGoodQuery.Visible)
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

        private void ClearDgvCategorySelect()
        {
            try
            {
                DataGridViewCell dgc = dgvGoodQuery.CurrentCell;
                Image lastimg = (Image)dgc.Value;

                if (lastimg.Tag != null)
                {
                    btnNotSelect.Text = ((KeyValuePair<string, string>)lastimg.Tag).Value;
                    Image tempimg = MainModel.GetControlImage(btnNotSelect);
                    tempimg.Tag = (KeyValuePair<string, string>)lastimg.Tag;

                    dgc.Value = tempimg;
                    // break;
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("清除分类列表选择项异常" + ex.Message);
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

                        BaseUIHelper.CloseFormMainScale();
                       // this.Close();
                    }
                    else if (resultcode == MainModel.HttpMemberExpired || resultcode == MainModel.DifferentMember)
                    {
                        MainModel.CurrentMember = null;


                        if (!WinSaasPOS_Scale.HelperUI.ConfirmHelper.Confirm("会员登录已过期，请重新登录","",false))
                        {
                            IsEnable = true;
                            return;
                        }

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


        #region 购物车
        private Product CurrentSelectProduct = null;
        private string LastSkuCode = "";
        private void dgvGood_CellClick(object sender, DataGridViewCellEventArgs e)
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


                AllowPrint = true;
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


                Image selectimg = (Image)dgvGood.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                if (selectimg.Tag == null)  //空白单元格（无商品）
                {
                    return;
                }

                ShowLoading(true, false);

                Product pro = (Product)selectimg.Tag;

                pnlGoodNotSelect.BackgroundImage = picGoodSelect.Image;

                dgvGood.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = GetItemImg(pro);
                pnlGoodNotSelect.BackgroundImage = picGoodNotSelect.Image;

               
                    SelectProduct(pro);  
       

                ShowLoading(false, true);               
               
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
                AllowPrint = true;
                if (CurrentSelectProduct != null)
                {
                    //遍历单元格清空之前的选中状态
                    for (int i = 0; i < this.dgvGoodQuery.Rows.Count; i++)
                    {
                        for (int j = 0; j < this.dgvGoodQuery.Columns.Count; j++)
                        {
                            Image lastimg = (Image)dgvGoodQuery.Rows[i].Cells[j].Value;

                            if (lastimg.Tag != null && ((Product)lastimg.Tag).skucode == CurrentSelectProduct.skucode)
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

                dgvGoodQuery.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = GetItemImg(pro);
                pnlGoodNotSelect.BackgroundImage = picGoodNotSelect.Image;


              
                SelectProduct(pro);

            }
            catch (Exception ex)
            {
                MainModel.ShowLog("选择商品异常" + ex.StackTrace, true);
            }
            finally
            {
                btnScan.Select();
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

                }

                string ErrorMsgCart = "";
                int ResultCode = 0;
                bool IsExits = false;
                DateTime starttime = DateTime.Now;

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ERROR", "添加购物车商品异常:" + ex.Message);
            }
        }

#endregion

        private void LoadBaseInfo()
        {
            try
            {
                timerNow.Enabled = true;
                TopLblGoodName = lblGoodName.Top;
                HeightLblGoodName = lblGoodName.Height;

                lblShopName.Text = MainModel.CurrentShopInfo.shopname;
                btnMenu.Text = MainModel.CurrentUser.nickname + "，你好 ∨";
                btnMenu.Left = pnlHead.Width - btnMenu.Width - 10;

                picLoading.Size = new Size(55,55);

                KBoard.Size = new Size(pnlSet.Width, this.Height - cbtnSearch.Bottom-8);
                KBoard.Location = new Point(cbtnSearch.Left, cbtnSearch.Bottom + 5);

                cbtnSearch.roundradius = cbtnSearch.Height;
                INIManager.SetIni("System", "MainType","Scale", MainModel.IniPath); 

                #region 排序选择
                btnSortNortSelect.RoundRadius = btnSortNortSelect.Height;
                btnSortSelect.RoundRadius = btnSortSelect.Height;

                imgSelect = MainModel.GetControlImage(btnSortSelect);
                imgNotSelect = MainModel.GetControlImage(btnSortNortSelect);
                btnOrderBySaleCount.BackgroundImage = imgSelect;

                imgPackUp = MainModel.GetControlImage(pnlPackUp);
                imgPackDown = MainModel.GetControlImage(pnlPackDown);
                #endregion
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
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载基础信息异常",true);
            }
        }

        #region
        private ScaleResult CurrentScaleResult = null;
        private decimal LastNetWeight = 0;
        /// <summary>
        /// 是否允许打印  重量置零或者选择新商品时 允许  防止批量打印时有重量一直打印
        /// </summary>
        private bool AllowPrint = true;
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
                    lblTotalWeight.Text = CurrentScaleResult.TotalWeight.ToString("f3")+" >";
                    lblTareWeight.Text = CurrentScaleResult.TareWeight.ToString("f3")+" >";
                    lblNetWeight.Text = CurrentScaleResult.NetWeight.ToString("f3");

                    //称重商品才需要判断重量 和是否自动打印
                    if (CurrentSelectProduct != null && CurrentSelectProduct.goodstagid == 1)
                    {

                        lblTotalPrice.Text = Math.Round(CurrentSelectProduct.price.saleprice * CurrentScaleResult.NetWeight, 2, MidpointRounding.AwayFromZero) + "";

                        if (CurrentScaleResult.NetWeight <= 0 && LastNetWeight>0)
                        {
                            AllowPrint = true;
                            SelectProduct(null);

                        }
                        else if (AutoPrint && CurrentScaleResult.WhetherStable && CurrentScaleResult.NetWeight > 0  && AllowPrint)
                        {
                            CurrentSelectProduct.specnum = CurrentScaleResult.NetWeight;
                            AllowPrint = false;
                            LabelPrintHelper.LabelPrint(CurrentSelectProduct);

                            SelectProduct(null);
                        }

                    }
                    LastNetWeight = CurrentScaleResult.NetWeight;
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
         
            //KBoard.Size = new Size(300,400);
            //rbtnPay.Enabled = !rbtnPay.Enabled;
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
               // KBoard.Hide();
            }

            //其他键直接输入
            else
            {
                if (focusing.SelectedText != "")
                    focusing.SelectedText = keyInput;
                else
                    focusing.SelectedText += keyInput;
            }
        }


        private void timerNow_Tick(object sender, EventArgs e)
        {
           lblTime.Text = MainModel.Titledata;
        }


        private bool AutoPrint = false;
        private bool BatchPrint = false;
        private void picAutoPrint_Click(object sender, EventArgs e)
        {
            try
            {
                AutoPrint = !AutoPrint;

                if (AutoPrint)
                {
                    picAutoPrint.BackgroundImage = picChecked.Image;
                }
                else
                {
                    picAutoPrint.BackgroundImage = picNotChecked.Image;
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("更新自动打印异常"+ex.Message ,true);
            }
        }

        private void picBatchPrint_Click(object sender, EventArgs e)
        {
            try
            {
                BatchPrint = !BatchPrint;

                if (BatchPrint)
                {
                    picBatchPrint.BackgroundImage = picChecked.Image;
                }
                else
                {
                    picBatchPrint.BackgroundImage = picNotChecked.Image;
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("更新批量打印异常" + ex.Message, true);
            }
        }

        private void lblTotalWeight_Click(object sender, EventArgs e)
        {
            FormZero frmzero = new FormZero();
            frmzero.Location = new Point(lblTotalWeight.Left+lblTotalWeight.Width/2-frmzero.Width/2, pnlHead.Height + lblTotalWeight.Bottom);
            frmzero.Show();
        }

        private void lblTareWeight_Click(object sender, EventArgs e)
        {

            FormTare frmtare = new FormTare();
            frmtare.Location = new Point(lblTareWeight.Left+lblTareWeight.Width/2-frmtare.Width/2, pnlHead.Height + lblTareWeight.Bottom);
            frmtare.Show();
        }

        private void rbtnPrint_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (CurrentSelectProduct == null)
                {
                    MainModel.ShowLog("未选择商品",false);
                    return;
                }

                if (CurrentSelectProduct.goodstagid == 0)
                {
                    AllowPrint = false;
                    LabelPrintHelper.LabelPrint(CurrentSelectProduct);
                    SelectProduct(null);
                }
                else
                {
                    //未获取到稳定重量数据无效
                    if (CurrentScaleResult == null || !CurrentScaleResult.WhetherSuccess || !CurrentScaleResult.WhetherStable)
                    {
                        MainModel.ShowLog("当前称重还没有稳定", false);
                        return;
                    }
                    //重量不允许为0
                    if (CurrentScaleResult.NetWeight <= 0)
                    {
                        MainModel.ShowLog("当前没有重量", false);
                        return;
                    }

                   
                        CurrentSelectProduct.specnum = CurrentScaleResult.NetWeight;
                        AllowPrint = false;
                        LabelPrintHelper.LabelPrint(CurrentSelectProduct);
                       
                        SelectProduct(null);
                }
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("打码异常"+ex.Message,true);
            }
        }


    }
}
