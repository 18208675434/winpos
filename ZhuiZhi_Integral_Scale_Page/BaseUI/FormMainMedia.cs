using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Maticsoft.Model;
using Newtonsoft.Json;
using Maticsoft.BLL;
using ZhuiZhi_Integral_Scale_UncleFruit.PayUI;

namespace ZhuiZhi_Integral_Scale_UncleFruit
{
    public partial class FormMainMedia : Form
    {
        #region 成员变量
        /// <summary>
        /// 接口访问类
        /// </summary>
        HttpUtil httputil = new HttpUtil();

        /// <summary>
        /// 按比例缩放页面及控件
        /// </summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();

        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();

        /// <summary>
        /// 当前购物车对象
        /// </summary>
        private Cart CurrentCart;


        /// <summary>
        /// 当前会员对象
        /// </summary>
        private Member CurrentMember;

        private bool isplayer = false;

        private Image imgmembercard = null;

        private bool PlayerOpen = false;



        #endregion

        #region 页面加载与关闭
        public FormMainMedia()
        {
            InitializeComponent();

            //使用委托的话frmmain界面会卡死
            Control.CheckForIllegalCrossThreadCalls = false;

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.  
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲  


        }
        private void frmMainMedia_Load(object sender, EventArgs e)
        {

        }

        private void frmMainMedia_Shown(object sender, EventArgs e)
        {
            threadIniExedate = new Thread(IniFormExe);
            threadIniExedate.IsBackground = false;
            //threadIniExedate.Priority = ThreadPriority.BelowNormal;
            threadIniExedate.Start();

            lblTime.Text = MainModel.Titledata;
            lblShopName.Text = "欢迎光临 "+ MainModel.CurrentShopInfo.shopname;

            timerMedia.Interval = 15 * 60 * 1000;
            timerMedia.Enabled = true;

            timerImage.Interval = 5 * 1000;
            timerImage.Enabled = true;

            tlpnlRight.Width = pnlMemberCard.Width;

            pnlMember.Width = tlpnlRight.Width;
            
            panel2.Width = tlpnlRight.Width;
            pnlMember.Height = Convert.ToInt16(tlpnlRight.Height * 35 / 100);
            panel2.Height =Convert.ToInt16(tlpnlRight.Height*65/100);

            picBirthday1.Visible = false;
            picBirthday2.Visible = false;
            picBirthday3.Visible = false;
            picBirthday4.Visible = false;

            bgwLoadMemberCard.RunWorkerAsync();
        }

        #endregion

        #region 客屏媒体

        private SortedDictionary<int, Mediadetaildto> sortMedia = new SortedDictionary<int, Mediadetaildto>();

        int sortMediaCount = 0;
        Thread threadIniExedate;
        //每十分钟刷新一次客屏广告信息
        private void timerMedia_Tick(object sender, EventArgs e)
        {
            try
            {
                sortMedia.Clear();
                threadIniExedate = new Thread(IniFormExe);
                threadIniExedate.IsBackground = true;
                //threadIniExedate.Priority = ThreadPriority.BelowNormal;
                threadIniExedate.Start();
            }
            catch { }
        }
        public void IniForm(object obj)
        {


            SelectTlp(1);

            timerImage.Enabled = true;

            //if (PlayerOpen)
            //{
            //    player.uiMode = "none";
            //    player.Ctlcontrols.play();
            //}

        }
        private JSON_BEANBLL jsonbll = new JSON_BEANBLL();
        private void IniFormExe(object obj)
        {
            try
            {
                player.Visible = false;
                

                if (MainModel.IsOffLine)
                {
                    JSON_BEANMODEL jsonmodel = jsonbll.GetModel("MEDIA");
                    if (jsonmodel != null && jsonmodel.JSON != null)
                    {
                        MediaList medialist = JsonConvert.DeserializeObject<MediaList>(jsonmodel.JSON);


                        foreach (Mediadetaildto media in medialist.mediadetaildtos)
                        {

                            string url = media.content;
                            string remoteUri = System.IO.Path.GetDirectoryName(url);

                            string fileName = System.IO.Path.GetFileName(url);
                            string filePath = MainModel.MediaPath + fileName;
                            if (File.Exists(filePath))
                            {
                                sortMedia.Add(media.sortnum, media);
                            }
                        }
                    }
                }
                else
                {
                    string ErrorMsg = "";
                    MediaList posmedia = httputil.GetPosMedia(ref ErrorMsg);

                    if (ErrorMsg != "" || posmedia == null)
                    {
                        //获取异常  显示空白页
                    }
                    else
                    {
                        //保存本地
                        jsonbll.Delete("MEDIA");
                        JSON_BEANMODEL jsonmodel = new JSON_BEANMODEL();
                        jsonmodel.CONDITION = "MEDIA";
                        jsonmodel.CREATE_TIME = DateTime.Now.ToString("yyyyMMddHHmmss");
                        jsonmodel.DEVICESN = MainModel.DeviceSN;
                        jsonmodel.CREATE_URL_IP = MainModel.URL;
                        jsonmodel.JSON = JsonConvert.SerializeObject(posmedia);
                        jsonbll.Add(jsonmodel);

                        CurrentMediadetaildtos = posmedia.mediadetaildtos;

                            ParameterizedThreadStart Pts = new ParameterizedThreadStart(DownLoadFile);
                            Thread thread = new Thread(Pts);
                            thread.IsBackground = true;
                            thread.Start(posmedia);                        
                    }
                }
            }
            catch (Exception ex)
            {
                player.Visible = false;
                // LogManager.WriteLog("ERROR", "初始化客屏信息异常：" + ex.Message);
            }
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url"></param>
        private void DownLoadFile(object obj)
        {
            try
            {
                lstPlayerUrl = new List<string>();
                lstShowImg = new List<Image>();

                MediaList medialist = (MediaList)obj;

                foreach (Mediadetaildto media in medialist.mediadetaildtos)
                {
                    try
                    {
                        string url = media.content;
                        string remoteUri = System.IO.Path.GetDirectoryName(url);

                        string fileName = System.IO.Path.GetFileName(url);
                        //图片没有后缀名  player控件加载无法播放
                        if (media.mediatype == 1)
                        {
                            if (!fileName.Contains("."))
                            {
                                fileName += ".png";
                            }

                            string filePath = MainModel.MediaPath + fileName;
                            if (File.Exists(filePath))
                            {
                                sortMedia.Add(media.sortnum, media);

                                lstShowImg.Add(Image.FromFile(filePath));
                            }
                            else
                            {
                                WebClient myWebClient = new WebClient();

                                myWebClient.DownloadFile(url, filePath);

                                sortMedia.Add(media.sortnum, media);

                                Image img = Image.FromFile(filePath);
                                lstShowImg.Add(img);

                                if (pnlAdvertising.BackgroundImage == null)
                                {
                                    pnlAdvertising.BackgroundImage = img;
                                }
                            }
                        }
                        else if (media.mediatype == 2)
                        {
                            string filePath = MainModel.MediaPath + fileName;
                            if (File.Exists(filePath))
                            {
                                sortMedia.Add(media.sortnum, media);

                                lstPlayerUrl.Add(filePath);
                            }
                            else
                            {
                                WebClient myWebClient = new WebClient();

                                myWebClient.DownloadFile(url, filePath);

                                sortMedia.Add(media.sortnum, media);
                                lstPlayerUrl.Add(filePath);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogManager.WriteLog("ERROR", "文件加载异常：" + ex.Message);
                    }

                }

                //if (CurrentSelectMode == 1)
                //{
                //    PlayerThread();
                //}
               
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ERROR", "下载文件异常：" + ex.Message);
            }
        }

        private List<Mediadetaildto> CurrentMediadetaildtos = new List<Mediadetaildto>();
        private void PlayerThread()
        {
            try
            {

               // PlayerOpen = false;
                if (lstPlayerUrl!=null && lstPlayerUrl.Count>0)
                {
                   
                        PlayerOpen = true;
                        player.currentPlaylist.clear();

                        foreach (string playerurl in lstPlayerUrl)
                        {
                            string playername = playerurl; //URL 最后的值

                            if (File.Exists(playerurl))
                            {
                                player.currentPlaylist.appendItem(player.newMedia(playername));
                            }
                        }
                        

                        if (player.currentPlaylist.count > 0)
                        {

                            //player.Visible = true;
                            player.settings.setMode("loop", false);
                            player.Ctlcontrols.play();
                        }
                    
                }
            }
            catch (Exception ex)
            {
                Invoke((new Action(() =>
                {
                    timerImage.Enabled = true;
                })));  
                LogManager.WriteLog("客屏广告异常" + ex.Message + ex.StackTrace);
            }
        }

        //播放状态是全屏   不是播放状态 此属性不可修改 （异常:灾难性错误）
        private void player_StatusChange(object sender, EventArgs e)
        {
            try
            {
                MainModel.IsPlayer = true;
                if (player.status.Contains("正在播放"))
                {
                    if (!player.fullScreen)
                        player.fullScreen = true;
                }
                else if (player.status == "停止" || player.status == "已完成")
                {
                    try
                    {
                        player.Ctlcontrols.stop();
                        player.Visible = false;
                        PlayerOpen = false;
                    }
                    catch { }

                    //Invoke((new Action(() =>
                    //{
                    //    timerImage.Enabled = true;
                    //}))); 
                    
                }
            }
            catch { }
            
        }

        #endregion

        #region  购物车列表

        public void UpdateForm()
        {
            try
            {
                try
                {
                    if (PlayerOpen)
                    {
                        player.Ctlcontrols.stop();
                        player.Visible = false;
                    }
                }
                catch { }

                this.BeginInvoke(new EventHandler(delegate
                {
                    UpdateFormExe();

                }));

                //System.ComponentModel.BackgroundWorker bk = new System.ComponentModel.BackgroundWorker();
                //bk.DoWork += UpdateFormExe;
                //bk.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("显示客屏购物车异常");
            }
        }

        //增加线程锁  防止多线程操作datagridview 红叉情况
        private object thislock = new object();
        private void UpdateFormExe()
        {
            lock (thislock)
            {
                try
                {

                    dgvGood.Visible = true;
                    tableLayoutPanel1.Visible = true;

                    pnlPayInfo.Visible = false;

                    SelectTlp(0);

                    CurrentCart = MainModel.frmMainmediaCart;
                    CurrentMember = MainModel.CurrentMember;
                    player.Visible = false;
                    try
                    { //player.close(); 
                    }
                    catch
                    {
                        LogManager.WriteLog("关闭视频异常");
                    }

                    try { threadIniExedate.Abort(); }
                    catch { }
                    //pnlPayInfo.Visible = false;
                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new InvokeHandler(delegate()
                        {
                            dgvGood.Rows.Clear();
                        }));
                    }
                    dgvOrderDetail.Rows.Clear();
                    if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                    {
                        int orderCount = CurrentCart.orderpricedetails.Count;
                        if (orderCount == 0)
                        {

                        }
                        else
                        {
                            for (int i = 0; i < orderCount; i++)
                            {

                                try
                                {
                                    if (this.IsHandleCreated)
                                    {
                                        this.Invoke(new InvokeHandler(delegate()
                                        {
                                            dgvOrderDetail.Rows.Add(CurrentCart.orderpricedetails[i].title, CurrentCart.orderpricedetails[i].amount);
                                        }));
                                    }
                                }
                                catch
                                {
                                    dgvOrderDetail.Refresh();
                                }


                            }

                            if (MainModel.CurrentMember != null)
                            {
                                if (CurrentCart.memberpromo != null && CurrentCart.memberpromo > 0)
                                {

                                    btnMemberPromo.Text = "会员已优惠:￥" + CurrentCart.memberpromo.ToString("f2");

                                    btnMemberPromo.Visible = true;
                                }
                                else
                                {
                                    btnMemberPromo.Visible = false;
                                }

                            }
                            else
                            {
                                if (CurrentCart.memberpromo != null && CurrentCart.memberpromo > 0)
                                {
                                    btnMemberPromo.Text = "会员可优惠:￥" + CurrentCart.memberpromo.ToString("f2");
                                    btnMemberPromo.Visible = true;
                                }
                                else
                                {
                                    btnMemberPromo.Visible = false;
                                }
                            }

                            if (CurrentCart.cashpayamt != null && CurrentCart.cashpayamt > 0)
                            {
                                if (this.IsHandleCreated)
                                {
                                    this.Invoke(new InvokeHandler(delegate()
                                    {
                                        dgvOrderDetail.Rows.Add("已付现金", "￥" + CurrentCart.cashpayamt.ToString("f2"));
                                    }));
                                }
                            }

                            if (CurrentCart.otherpayamt > 0)
                            {
                               
                                string strotherpaytype = "";
                                try
                                {
                                    strotherpaytype = CurrentCart.paymenttypes.otherpaytypeinfo.Where(r => r.code == CurrentCart.otherpaytype).ToList()[0].name;
                                }
                                catch
                                {
                                }

                                dgvOrderDetail.Rows.Add(strotherpaytype, "￥" + CurrentCart.otherpayamt.ToString("f2"));
                            }

                            dgvOrderDetail.ClearSelection();
                        }

                        if (CurrentCart.cashchangeamt != null && CurrentCart.cashchangeamt > 0)
                        {
                            lblPriceContent.Text = "找零:";
                            lblPrice.Text = "￥" + CurrentCart.cashchangeamt.ToString("f2");
                        }

                        else
                        {
                            if (dgvOrderDetail.Rows.Count > 1)
                            {
                                lblPriceContent.Text = "还需支付:";
                            }
                            else
                            {
                                lblPriceContent.Text = "应付:";
                            }
                            lblPrice.Text = "￥" + CurrentCart.totalpayment.ToString("f2");
                        }


                        int count = CurrentCart.products.Count;
                        int goodscount = 0;
                        foreach (Product pro in CurrentCart.products)
                        {
                            goodscount += pro.num;
                        }

                        lblGoodsCount.Text = "(" + goodscount.ToString() + "件商品)";
                        if (count > 0)
                        {
                            this.Enabled = true;
                            for (int i = 0; i < count; i++)
                            {
                                Product temppro = CurrentCart.products[i].ThisClone();

                                string pronum = "";
                                if (temppro.goodstagid == 0)
                                {
                                    pronum = temppro.num.ToString();
                                }
                                else
                                {
                                    pronum = temppro.price.specnum + temppro.price.unit;
                                }
                                if (this.IsHandleCreated)
                                {
                                    this.Invoke(new InvokeHandler(delegate()
                                    {
                                        List<Bitmap> lstbmp = GetDgvRow(temppro);
                                        if (lstbmp != null && lstbmp.Count == 3)
                                        {
                                            dgvGood.Rows.Insert(0, new object[] { lstbmp[0], lstbmp[1], "", pronum, "", lstbmp[2] });
                                        }
                                    }));
                                }
                            }
                            Application.DoEvents();
                        }
                    }
                    this.Enabled = true;
                }
                catch (Exception ex)
                {
                    try
                    {
                        dgvGood.Refresh();
                        dgvOrderDetail.Refresh();
                    }
                    catch
                    {

                    }
                    LogManager.WriteLog("ERROR", "显示客屏购物车异常：" + ex.Message + ex.StackTrace);
                }
            }
        }


        public void UpDgvScorll(int value)
        {
            try
            {
                dgvGood.FirstDisplayedScrollingRowIndex = value;

            }catch{
            }
            
        }


        public void UpdateDgvOrderDetail(Dictionary<string,string> orderdetail, string pricecontent,string price)
        {
            try
            {
                
                    dgvOrderDetail.Rows.Clear();
                    foreach (KeyValuePair<string, string> keyvalue in orderdetail)
                    {
                        dgvOrderDetail.Rows.Add(keyvalue.Key, keyvalue.Value);
                    }
                    lblPriceContent.Text = pricecontent;
                    lblPrice.Text = price;

                    dgvOrderDetail.ClearSelection();
                    
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("更新客屏订单详情异常"+ex.Message);
            }

        }
        #endregion

        #region 加载会员
        public void LoadMember()
        {
            try
            {
                if (PlayerOpen)
                {
                    player.Ctlcontrols.stop();
                    player.Visible = false;
                }
                //Thread threadLoadMember = new Thread(LoadMemberThread);
                //threadLoadMember.IsBackground = true;
                //threadLoadMember.Start();

                System.ComponentModel.BackgroundWorker bk = new System.ComponentModel.BackgroundWorker();
                bk.DoWork += LoadMemberThread;
                bk.RunWorkerAsync();

            }
            catch (Exception ex)
            {

            }
        }

        public void LoadMemberThread(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                if (MainModel.IsOffLine)
                {
                    return;
                }
                DateTime starttime = DateTime.Now;
                //isplayer = false;

                CurrentMember = MainModel.CurrentMember;

                if (CurrentMember != null && CurrentMember.memberinformationresponsevo != null && CurrentMember.memberheaderresponsevo != null)
                {
                    //string mobil = CurrentMember.memberheaderresponsevo.mobile;
                    //if (mobil.Length > 8)
                    //{
                    //    mobil = mobil.Substring(0, mobil.Length - 8) + "****" + mobil.Substring(mobil.Length - 4);
                    //}
                    lblMobil.Text = CurrentMember.entrancecode;

                    lblWechartNickName.Text =  "  你好！";

                    pnlMemberCard.Visible = false;

                    this.tlpnlRight.RowStyles[0] = new RowStyle(SizeType.Percent, 35);
                    this.tlpnlRight.RowStyles[1] = new RowStyle(SizeType.Percent, 65);
                    this.tlpnlRight.RowStyles[2] = new RowStyle(SizeType.Percent, 0);

                    if (CurrentMember.membertenantresponsevo.onbirthday)
                    {
                        // pnlBirthday.Visible = true;
                        if (!lblWechartNickName.Text.Contains("生日快乐！"))
                        {
                            lblWechartNickName.Text += "生日快乐！";
                        }
                        //picBirthday1.Visible = false;
                        //picBirthday2.Visible = false;
                        //picBirthday3.Visible = false;
                        //picBirthday4.Visible = false;

                        pnlCart.BackColor = Color.DarkSlateBlue;
                        Application.DoEvents();
                        picBirthday1.Visible = true;
                        picBirthday2.Visible = true;
                        picBirthday3.Visible = true;
                        picBirthday4.Visible = true;


                    }
                    else
                    {
                        pnlCart.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
                        picBirthday1.Visible = false;
                        picBirthday2.Visible = false;
                        picBirthday3.Visible = false;
                        picBirthday4.Visible = false;

                        lblWechartNickName.Text = lblWechartNickName.Text.Replace("生日快乐！", "");
                    }
                }
                else
                {
                    this.tlpnlRight.RowStyles[0] = new RowStyle(SizeType.Percent, 0);
                    this.tlpnlRight.RowStyles[1] = new RowStyle(SizeType.Percent, 65);
                    this.tlpnlRight.RowStyles[2] = new RowStyle(SizeType.Percent, 35);

                    lblMobil.Text = "";
                    lblWechartNickName.Text = "";

                    pnlCart.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
                    picBirthday1.Visible = false;
                    picBirthday2.Visible = false;
                    picBirthday3.Visible = false;
                    picBirthday4.Visible = false;

                    string ErrorMsg = "";
                    string imgurl = httputil.GetMemberCard(ref ErrorMsg);
                    if (!string.IsNullOrEmpty(imgurl) && string.IsNullOrEmpty(ErrorMsg))
                    {
                        //this.Invoke(new InvokeHandler(delegate()
                        //{

                        Image _image = Image.FromStream(System.Net.WebRequest.Create(imgurl).GetResponse().GetResponseStream());
                        picMemberCard.BackgroundImage = _image;
                        pnlMemberCard.Visible = true;
                    }

                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("加载会员异常" + ex.Message);
            }
        }
        #endregion

        #region  支付提示/结果
        public void ShowPayResult(object payinfo)
        {
            try
            {
                if (PlayerOpen)
                {
                    player.Ctlcontrols.stop();
                    player.Visible = false;
                }

                FormPaySuccessMedia frmresult = new FormPaySuccessMedia(payinfo.ToString());
                frmresult.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width, 0);

                frmresult.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                frmresult.Show();
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("客屏展示收银成功异常" + ex.Message);
            }
        }

      
        public void ShowPayInfo(string lblinfo,bool isError)
        {
            try
            {
                if (PlayerOpen)
                {
                    player.Ctlcontrols.stop();
                    player.Visible = false;
                }

                SelectTlp(0);
                dgvGood.Visible = false;
                tableLayoutPanel1.Visible = false;

                pnlPayInfo.Visible = true;

                if (isError)
                {
                    picPayInfo.BackgroundImage = picPayError.BackgroundImage;
                    lblPayInfo1.Text = "亲，付款失败";
                    lblPayInfo2.Text = "店员正在处理，请稍等";
                    lblPayInfo2.Visible = true ;
                }
                else
                {
                    picPayInfo.BackgroundImage = picShowPay.BackgroundImage;
                    lblPayInfo1.Text = lblinfo;
                    lblPayInfo2.Visible = false;
                }
               
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("客屏提示支付信息异常：" + ex.Message);
            }
        }

        frmBalancePwdGuest frmbalancepwdguest = null;
        public void ShowBalancePwd(bool showorclose)
        {
            if (PlayerOpen)
            {
                player.Ctlcontrols.stop();
                player.Visible = false;
            }
            ParameterizedThreadStart Pts = new ParameterizedThreadStart(SowBalancePwdThread);
            Thread thread = new Thread(Pts);
            thread.IsBackground = true;
            thread.Start(showorclose);
        }

        public void ShowBalancePwdLog(string msg)
        {
            try
            {
                if (frmbalancepwdguest != null)
                {
                    frmbalancepwdguest.ShowLog(msg,false);
                }
            }
            catch { }
        }
        private void SowBalancePwdThread(object obj)
        {
            try
            {
               
                bool showorclose = (bool)obj;
                if (showorclose) //打开
                {
                    this.Enabled = false;
                    player.Visible = false;
                    if (frmbalancepwdguest == null)
                    {
                        frmbalancepwdguest = new frmBalancePwdGuest();
                        frmbalancepwdguest.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width + pnlPayInfo.Location.X, pnlPayInfo.Location.Y);
                        frmbalancepwdguest.TopMost = true;
                        asf.AutoScaleControlTest(frmbalancepwdguest,704,699, pnlPayInfo.Width, pnlPayInfo.Height, true);
                    }

                    frmbalancepwdguest.ShowDialog();
                    Application.DoEvents();
                }
                else //关闭
                {
                    this.Enabled = true;
                    frmbalancepwdguest.Close();

                    frmbalancepwdguest = null;
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                LogManager.WriteLog("客屏余额输入密码显示异常" + ex.Message);
            }
        }

        #endregion



                /// <summary>
        /// 数据处理委托方法
        /// </summary>
        /// <param name="type">0：返回  1：确认数字</param>
        public delegate void DataRecHandleDelegate(string scancode);
        /// <summary>
        /// 数据接收事件
        /// </summary>
        public event DataRecHandleDelegate DataReceiveHandle;


        private List<Bitmap> GetDgvRow(Product pro)
        {
            try
            {
                Bitmap bmpbarcode;
                Bitmap bmpPrice;

                Bitmap bmpTotal;



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
                //test
                //barcode += "测试限购商品?";
                if (!string.IsNullOrEmpty(pro.price.purchaselimitdesc))
                {
                    btnPurchaseLimit.Visible = true;
                    if (!string.IsNullOrEmpty(pro.price.purchaselimitsubdesc))
                    {
                        btnPurchaseLimit.Text = pro.price.purchaselimitdesc.Split('，')[0] + "?";
                    }
                    else
                    {
                        btnPurchaseLimit.Text = pro.price.purchaselimitdesc.Split('，')[0];
                    }
                }
                else
                {
                    btnPurchaseLimit.Visible = false;
                }

                if (pro.adjustpriceinfo != null && pro.adjustpricedesc != null)
                {
                    lblAdjust.Visible = true;
                    lblAdjust.Text = pro.adjustpricedesc.amtdesc;
                    lblAdjust.Width = lblAdjust.Width + 5;
                    lblAdjust.Left = (pnlTotal.Width - lblAdjust.Width)/2;

                }
                else
                {
                    lblAdjust.Visible = false;
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


              


                if (pro.price.total == pro.price.origintotal)
                {
                    lblTotal.Text = pro.price.total.ToString("f2");

                    lblTotalDesc.Text = "";
                    lblOriginTotal.Text = "";

                    lblTotal.Left = (pnlTotal.Width - lblTotal.Width) / 2;
                }
                else
                {
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


                List<Bitmap> lstbmp = new List<Bitmap>();
                lstbmp.Add(bmpbarcode);
                lstbmp.Add(bmpPrice);
                lstbmp.Add(bmpTotal);


                return lstbmp;
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("解析商品信息异常" + ex.Message, true);
                return null;
            }
        }


        private void frmMainMedia_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                player.Ctlcontrols.stop();
                this.Dispose();
            }
            catch (Exception ex)
            {

            }
        }

        int showcount = 0;
        List<Image> lstShowImg = new List<Image>();

        List<string> lstPlayerUrl = new List<string>(); 
        private void timerImage_Tick(object sender, EventArgs e)
        {
            try
            {
                if (lstShowImg.Count > 0 && !player.status.Contains("正在播放"))
                {
                   
                    if (showcount < lstShowImg.Count)
                    {

                        Invoke((new Action(() =>
                        {
                            player.Visible = false;
                            pnlAdvertising.BackgroundImage = lstShowImg[showcount];
                        })));
                        //player.Visible = false;
                        //pnlAdvertising.BackgroundImage = lstShowImg[showcount];
                    }
                    else
                    {                      
                            showcount = 0;
                            if (CurrentSelectMode == 1 && lstPlayerUrl.Count > 0)
                            {
                                player.Visible = true;
                                PlayerThread();

                            }
                    }

                    showcount += 1;
                }
            }
            catch (Exception ex)
            {
                showcount = 0;

                LogManager.WriteLog("客屏广告定时刷新异常"+ex.StackTrace);
            }
        }


        private int CurrentSelectMode = 0;
        private void SelectTlp(int index)
        {
            try
            {
                CurrentSelectMode = index;
                for(int i=0;i<tlpMedia.ColumnCount;i++){
                    if (i == index)
                    {
                        tlpMedia.ColumnStyles[i] = new ColumnStyle(SizeType.Percent, 100);
                    }
                    else
                    {
                        tlpMedia.ColumnStyles[i] = new ColumnStyle(SizeType.Percent, 0);
                    }
                }
            }
            catch { }
        }

        private void bgwLoadMemberCard_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string ErrorMsg = "";
                string imgurl = httputil.GetMemberCard(ref ErrorMsg);
                if (!string.IsNullOrEmpty(imgurl) && string.IsNullOrEmpty(ErrorMsg))
                {
                    //this.Invoke(new InvokeHandler(delegate()
                    //{

                    Image _image = Image.FromStream(System.Net.WebRequest.Create(imgurl).GetResponse().GetResponseStream());
                    imgmembercard = _image;

                    picMemberCard.BackgroundImage = _image;
                    pnlMemberCard.Visible = true;
                }
            }
            catch { }
        }
    }
}
