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
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.MyControl;
using ZhuiZhi_Integral_Scale_UncleFruit.PayUI;
using ZhuiZhi_Integral_Scale_UncleFruit.Resources;
using ZhuiZhi_Integral_Scale_UncleFruit.ScaleFactory;
using ZhuiZhi_Integral_Scale_UncleFruit.ScaleUI;

namespace ZhuiZhi_Integral_Scale_UncleFruit
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


        /// <summary>
        /// 当前展示分类页数
        /// </summary>
        private int CurrentCategoryPage = 1;

        /// <summary>
        /// 当前展示二级分类页数
        /// </summary>
        private int CurrentSecondCategoryPage = 1;

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
            try
            {
                LoadBaseInfo();

                BaseUIHelper.ShowFormMainMedia();
                BaseUIHelper.IniFormMainMedia();
            }
            catch (Exception ex)
            {

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

                lblToast.Visible = false;
                ShowLoading(true, false);

                // BaseUIHelper.CloseFormMain();
                LstAllProduct = CartUtil.LoadAllProduct(true);

                if (LstAllProduct == null || LstAllProduct.Count == 0)
                {

                    ServerDataUtil.LoadAllProduct();
                    LstAllProduct = CartUtil.LoadAllProduct(true);
                }
                else
                {
                    //启动全量商品同步线程
                    threadLoadAllProduct = new Thread(ServerDataUtil.LoadAllProduct);
                    threadLoadAllProduct.IsBackground = true;
                    threadLoadAllProduct.Start();
                }
                IniForm();
                ShowLoading(false, true);
                timerScale.Enabled = true;
                txtSearch.Focus();
            }
            catch { }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsRun = false;
            try
            {
                //ScaleGlobalHelper.Close();
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

                    asf.AutoScaleControlTest(frmtoolmainscale, 210, 180, Convert.ToInt32(MainModel.wScale * 210), Convert.ToInt32(MainModel.hScale * 180), true);
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
                ShowLog("菜单窗体显示异常" + ex.Message, true);
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



                if (!ZhuiZhi_Integral_Scale_UncleFruit.HelperUI.ConfirmHelper.Confirm("退出登录"))
                {
                    return;
                }

                INIManager.SetIni("System", "POS-Authorization", "", MainModel.IniPath);

                try { MainModel.frmlogin.Show(); }
                catch { }
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

                }
            }
            catch (Exception ex)
            {
                ShowLog("切换秤模式异常" + ex.Message, true);
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

                ShowLoading(true,false);
                LoadDgvGood(false,false);

                ShowLoading(false, true);
            
            }catch(Exception ex){
                ShowLoading(false, true);
                ShowLog("查询面板商品异常"+ex.Message,true);
            }
            finally
            {
                txtSearch.Select();
            }
        }



        #endregion


        #region  分类
        private Dictionary<string, string> sortCategory = new Dictionary<string, string>();

        /// <summary>
        /// 当前页面购物车 根据firsecategoryid 区分
        /// </summary>
        SortedDictionary<string, Cart> sortCartByFirstCategoryid = new SortedDictionary<string, Cart>();
        private void IniForm()
        {
            try
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
                int lastindex = 6;
                int waitingcount = 0;

                bool havanextpage = false;
                bool havepreviousPage = false;
                if (page == 1)
                {
                    havepreviousPage = false;
                    startindex = 0;
                    if (sortCategory.Count > 7)
                    {
                        lastindex = 5;
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
                    waitingcount = sortCategory.Count - ((page - 1) * 5 + 1);  //第一页只有下一页  中间页都是上一页下一页 占用两个
                    startindex = (page - 1) * 5 + 1;

                    if (waitingcount > 6)
                    {
                        lastindex = startindex + 4;
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

                int emptyimgcount = 7 - loadingcount;

                for (int i = 0; i < emptyimgcount; i++)
                {
                    lstshowimg.Add(ResourcePos.empty);
                }
                dgvCategory.Rows.Clear();
                for (int i = 0; i < 1; i++)
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
                ShowLog("加载分类异常" + ex.StackTrace, true);
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
                CurrentSecondCategoryPage = 1;
                LoadSecondDgvCategory();

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


        private void dgvSecondCategory_CellClick(object sender, DataGridViewCellEventArgs e)
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
                Image selectimg = (Image)dgvSecondCategory.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                //展开
                if (selectimg == imgPageDownForCagegory)
                {
                    CurrentSecondCategoryPage++;
                    LoadSecondDgvCategory();
                    return;
                }
                //收起
                if (selectimg == imgPageUpForCategory)
                {
                    CurrentSecondCategoryPage--;
                    LoadSecondDgvCategory();
                    return;
                }
                if (selectimg.Tag == null)  //空白单元格（无商品）
                {
                    return;
                }

                //遍历单元格清空之前的选中状态
                for (int i = 0; i < this.dgvSecondCategory.Rows.Count; i++)
                {
                    for (int j = 0; j < this.dgvSecondCategory.Columns.Count; j++)
                    {
                        Image lastimg = (Image)dgvSecondCategory.Rows[i].Cells[j].Value;

                        if (lastimg.Tag != null && ((KeyValuePair<string, string>)lastimg.Tag).Key == CurrentFirstCategoryid)
                        {
                            btnNotSelect.Text = ((KeyValuePair<string, string>)lastimg.Tag).Value;
                            Image tempimg = MainModel.GetControlImage(btnNotSelect);
                            tempimg.Tag = (KeyValuePair<string, string>)lastimg.Tag;

                            dgvSecondCategory.Rows[i].Cells[j].Value = tempimg;
                            // break;
                        }
                    }
                }


                KeyValuePair<string, string> kv = (KeyValuePair<string, string>)selectimg.Tag;

                btnSecondSelect.Text = kv.Value;
                Image img = MainModel.GetControlImage(btnSecondSelect);
                img.Tag = kv;

                dgvSecondCategory.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = img;

                sortCartByFirstCategoryid[CurrentFirstCategoryid].SelectSecondCategoryid = kv.Key;

                LoadSecondDgvCategory();
                //dgvGood.Rows.Clear();

                //CurrentGoodPage = 1;
                ////说明是第一次加载
                //if (sender == null)
                //{
                //    LoadDgvGood(true, true);
                //}
                //else
                //{
                //    LoadDgvGood(false, false);
                //}
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("选择分类异常" + ex.StackTrace, true);
            }

        }


        private void LoadSecondDgvCategory()
        {
            try
            {

                if (CurrentFirstCategoryid == "-1")
                {
                    //TODO  隐藏二级分类 显示无分类信息
                    dgvSecondCategory.Rows.Clear();
                    CurrentGoodPage = 1;
                    LoadDgvGood(false, false);
                    return;
                }

                List<Product> AllCategoryPro = sortCartByFirstCategoryid[CurrentFirstCategoryid].products;

                List<string> list_name = AllCategoryPro.Select(t => t.secondcategoryid).Distinct().ToList();

                Dictionary<string, string> currentsecond = new Dictionary<string, string>();
                foreach (string str in list_name)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        currentsecond.Add(str, AllCategoryPro.FirstOrDefault(r => r.secondcategoryid == str).secondcategoryname);
                    }
                }

                int page = CurrentSecondCategoryPage;
                int startindex = 0;
                int lastindex = 6;
                int waitingcount = 0;

                bool havanextpage = false;
                bool havepreviousPage = false;
                if (page == 1)
                {
                    havepreviousPage = false;
                    startindex = 0;
                    if (currentsecond.Count > 7)
                    {
                        lastindex = 5;
                        havanextpage = true;
                    }
                    else
                    {
                        lastindex = currentsecond.Count - 1;
                        havanextpage = false;
                    }
                }
                else
                {
                    havepreviousPage = true;
                    waitingcount = currentsecond.Count - ((page - 1) * 5 + 1);  //第一页只有下一页  中间页都是上一页下一页 占用两个
                    startindex = (page - 1) * 5 + 1;

                    if (waitingcount > 6)
                    {
                        lastindex = startindex + 4;
                        havanextpage = true;
                    }
                    else
                    {
                        lastindex = currentsecond.Count - 1;
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
                foreach (KeyValuePair<string, string> kv in currentsecond)
                {
                    if (tempcount >= startindex && tempcount <= lastindex)
                    {
                        Image img;
                        if (sortCartByFirstCategoryid[CurrentFirstCategoryid].SelectSecondCategoryid == kv.Key)
                        {
                            btnSecondSelect.Text = kv.Value;
                            img = MainModel.GetControlImage(btnSecondSelect);
                            img.Tag = kv;
                        }
                        else
                        {
                            btnSecondNotSelect.Text = kv.Value;
                            img = MainModel.GetControlImage(btnSecondNotSelect);
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

                int emptyimgcount = 7 - loadingcount;

                for (int i = 0; i < emptyimgcount; i++)
                {
                    lstshowimg.Add(ResourcePos.empty);
                }

                dgvSecondCategory.Rows.Clear();
                for (int i = 0; i < 1; i++)
                {
                    int temp = 7 * i;
                    dgvSecondCategory.Rows.Add(lstshowimg[temp + 0], lstshowimg[temp + 1], lstshowimg[temp + 2], lstshowimg[temp + 3], lstshowimg[temp + 4], lstshowimg[temp + 5], lstshowimg[temp + 6]);
                }

                IsEnable = true;
                if (dgvSecondCategory.Rows.Count > 0 && sortCartByFirstCategoryid[CurrentFirstCategoryid].SelectSecondCategoryid == "-1")
                {
                    dgvSecondCategory_CellClick(null, new DataGridViewCellEventArgs(0, 0));
                    // dgvSecondCategory(null, new DataGridViewCellEventArgs(0, 0));
                }
                else
                {
                    CurrentGoodPage = 1;
                    LoadDgvGood(false, false);
                }


            }
            catch (Exception ex)
            {
                MainModel.ShowLog("加载分类异常" + ex.StackTrace, true);
            }
        }

        #endregion


        #region   商品表

        private void LoadDgvGood(bool isnew, bool isnewType)
        {
            try
            {
                Other.CrearMemory();
                List<Product> AllCategoryPro = new List<Product>();

                if (CurrentFirstCategoryid == "-1")
                {
                    AllCategoryPro = sortCartByFirstCategoryid[CurrentFirstCategoryid].products;

                }
                else
                {
                    AllCategoryPro = sortCartByFirstCategoryid[CurrentFirstCategoryid].products.Where(r => r.secondcategoryid == sortCartByFirstCategoryid[CurrentFirstCategoryid].SelectSecondCategoryid).ToList();

                }


                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    string strquery = txtSearch.Text.ToUpper();
                    AllCategoryPro = AllCategoryPro.Where(r => r.AllFirstLetter.Contains(strquery) || r.skucode.Contains(strquery)).ToList();

                }

                if (AllCategoryPro == null || AllCategoryPro.Count == 0)
                {
                    dgvGood.Rows.Clear();
                    return;
                }


                Paging paging = CartUtil.GetPaging(CurrentGoodPage, 25, AllCategoryPro.Count, 5);
                

                if (!paging.success)
                {
                    MainModel.ShowLog("分页出现异常，请重试", true);
                    dgvGood.Rows.Clear();
                    CurrentGoodPage = 1;
                    return;
                }

                List<Image> lstshowimg = new List<Image>();
                if (paging.haveuppage)
                {
                    lstshowimg.Add(imgPageUpForGood);
                }

                List<Product> lstLaodingPro = AllCategoryPro.GetRange(paging.startindex, paging.endindex - paging.startindex + 1);


                List<Product> lstNotHaveprice = lstLaodingPro.Where(r => r.panelbmp == null).ToList();
                //防止转换json  死循环   bmp.tag 是product
                lstNotHaveprice.ForEach(r => r.panelbmp = null);
                if (lstNotHaveprice != null && lstNotHaveprice.Count > 0)
                {
                    ZhuiZhi_Integral_Scale_UncleFruit.BaseUI.MainHelper.SingleCalculate(lstNotHaveprice);

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

                if (paging.havedownpage)
                {
                    lstshowimg.Add(imgPageDownForGood);
                }



                for (int i = 0; i < paging.makeupcount; i++)
                {
                    lstshowimg.Add(ResourcePos.empty);
                }

                int rowcount = lstshowimg.Count / 5;

                for (int i = 0; i < rowcount; i++)
                {
                    dgvGood.Rows.Add(lstshowimg[i * 5 + 0], lstshowimg[i * 5 + 1], lstshowimg[i * 5 + 2], lstshowimg[i * 5 + 3], lstshowimg[i * 5 + 4]);
                }

                IsEnable = true;

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ERROR", "加载面板商品异常" + ex.Message);
            }

        }


        #endregion

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
                ShowLog("解析选择商品异常" + ex.Message, true);
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

               // lblToast.Left = (this.Width - lblToast.Width) / 2;
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
                LogManager.WriteLog("显示秤模式toast异常"+ex.Message);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="showloading">显示等待框</param>
        /// <param name="isenable">页面是否可操作</param>
        private void ShowLoading(bool showloading, bool isenable)
        {
            //try
            //{
            //    IsEnable = isenable;
            //    if (this.IsHandleCreated)
            //    {
            //        this.Invoke(new InvokeHandler(delegate()
            //        {
            //            pnlLoading.Visible = showloading;

            //        }));
            //    }
            //    else
            //    {
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

                        BaseUIHelper.CloseFormMainScale();
                       // this.Close();
                    }
                    else if (resultcode == MainModel.HttpMemberExpired )
                    {
                        MainModel.CurrentMember = null;


                        //if (!ZhuiZhi_Integral_Scale_UncleFruit.HelperUI.ConfirmHelper.Confirm("会员登录已过期，请重新登录","",false))
                        //{
                        //    IsEnable = true;
                        //    return;
                        //}

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
                ShowLog("选择商品异常"+ex.StackTrace,true);
            }
            finally
            {
                btnScan.Select();
            }
        }

#endregion

        private void LoadBaseInfo()
        {
            try
            {
                TopLblGoodName = lblGoodName.Top;
                HeightLblGoodName = lblGoodName.Height;

                lblShopName.Text = MainModel.Titledata + "   " + MainModel.CurrentShopInfo.shopname;

                lblMenu.Text = MainModel.CurrentUser.nickname + ",你好 ∨";
                picMenu.Left = pnlMenu.Width - picMenu.Width - lblMenu.Width;
                lblMenu.Left = picMenu.Right;


                if (MainModel.URL.Contains("pos-qa"))
                {
                    lblUrl.Visible = true;
                    lblUrl.Text = "测试环境（QA）";
                }
                else if (MainModel.URL.Contains("pos-stage"))
                {
                    lblUrl.Visible = true;
                    lblUrl.Text = "测试环境（stage）";
                }
                else
                {
                    lblUrl.Visible = false;
                }

                pnlLoading.Size = new Size(55,55);

                KBoard.Size = new Size(pnlSet.Width, this.Height - txtSearch.Bottom - 8);
                KBoard.Location = new Point(txtSearch.Left, txtSearch.Bottom + 5);

                INIManager.SetIni("System", "MainType","Scale", MainModel.IniPath); 

                #region 排序选择

                imgPageUpForCategory = MainModel.GetControlImage(btnPreviousPageForCategoty);
                imgPageDownForCagegory = MainModel.GetControlImage(btnNextPageForCategory);

                imgPageUpForGood = MainModel.GetControlImage(btnPageUpForGood);
                imgPageDownForGood = MainModel.GetControlImage(btnPageDownForGood);
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
                ShowLog("加载基础信息异常",true);
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
                LogManager.WriteLog("SCALE", "获取秤界面电子秤重量信息异常" + ex.StackTrace);
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
        private void MiniKeyboardHandler(object sender, ZhuiZhi_Integral_Scale_UncleFruit.MyControl.KeyBoard.KeyboardArgs e)
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
                ShowLog("更新自动打印异常"+ex.Message ,true);
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
                ShowLog("更新批量打印异常" + ex.Message, true);
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentSelectProduct == null)
                {
                    ShowLog("未选择商品", false);
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
                        ShowLog("当前称重还没有稳定", false);
                        return;
                    }
                    //重量不允许为0
                    if (CurrentScaleResult.NetWeight <= 0)
                    {
                        ShowLog("当前没有重量", false);
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
                ShowLog("打码异常" + ex.Message, true);
            }
        }


        private void FormMainScale_Activated(object sender, EventArgs e)
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

        private void lblSearchShuiyin_Click_1(object sender, EventArgs e)
        {
            txtSearch.Focus();
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            txtSearch.Focus();
        }

    


    }
}
