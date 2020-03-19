using WinSaasPOS.Common;
using WinSaasPOS.Model;
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
using System.Threading;
using System.Windows.Forms;
using Maticsoft.Model;
using Newtonsoft.Json;
using Maticsoft.BLL;

namespace WinSaasPOS
{
    public partial class frmMainMedia : Form
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

        Thread threadMedia;

        private Image imgmembercard = null;

        #endregion




        #region 页面加载与关闭
        public frmMainMedia()
        {
            InitializeComponent();

            //使用委托的话frmmain界面会卡死
            Control.CheckForIllegalCrossThreadCalls = false;
            // IniForm(null);


            ////线程优先级低 不能占用数据处理线程资源
            //threadMedia = new Thread(PlayerThread);
            //threadMedia.Priority = ThreadPriority.BelowNormal;
            //threadMedia.IsBackground = true;
            //threadMedia.Start();

            threadMedia = new Thread(PlayerThread);
            threadMedia.IsBackground = true;
            //thread.SetApartmentState(ApartmentState.Unknown);
            threadMedia.SetApartmentState(ApartmentState.STA);
            //thread.Priority = ThreadPriority.Lowest;
            threadMedia.Start();
         
          
            //PlayerThread();
        }
        private void frmMainMedia_Load(object sender, EventArgs e)
        {

        }

        private void frmMainMedia_Shown(object sender, EventArgs e)
        {
            threadIniExedate = new Thread(IniFormExe);
            threadIniExedate.IsBackground = true;
            //threadIniExedate.Priority = ThreadPriority.BelowNormal;
            threadIniExedate.Start();


            lblShopName.Text = "欢迎光临 "+ MainModel.CurrentShopInfo.shopname;
            timerNow.Enabled = true;

            timerMedia.Interval = 10 * 60 * 1000;
            timerMedia.Enabled = true;
        }

        #endregion




        #region 客屏媒体

        private SortedDictionary<int, Mediadetaildto> sortMedia = new SortedDictionary<int, Mediadetaildto>();

        int sortMediaCount = 0;
        Thread threadIniExedate;
        //每十分钟刷新一次客屏广告信息
        private void timerMedia_Tick(object sender, EventArgs e)
        {

            sortMedia.Clear();
            threadIniExedate = new Thread(IniFormExe);
            threadIniExedate.IsBackground = true;
            //threadIniExedate.Priority = ThreadPriority.BelowNormal;
            threadIniExedate.Start();
        }
        public void IniForm(object obj)
        {

            if ((threadMedia.ThreadState & ThreadState.Suspended)!=0)
            {
                threadMedia.Resume();
            }
            

            tabControlMedia.SelectedIndex = 1;

        }
        private JSON_BEANBLL jsonbll = new JSON_BEANBLL();
        private void IniFormExe(object obj)
        {
            try
            {
               // tabControlMedia.SelectedIndex = 1;

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

                        
                        //下载商户图片
                        //LoginLogo.bmp
                        try
                        {
                            Image _image = Image.FromStream(System.Net.WebRequest.Create(posmedia.tenantlogo).GetResponse().GetResponseStream());

                            _image.Save(MainModel.MediaPath + "LoginLogo.bmp");
                            //picItem.BackgroundImage = Image.FromFile(MainModel.ProductPicPath + imgname);
                        }
                        catch (Exception ex)
                        {
                           // LogManager.WriteLog("下载商户图标异常" + ex.Message);
                        }

                        CurrentMediadetaildtos = posmedia.mediadetaildtos;
                        foreach (Mediadetaildto media in posmedia.mediadetaildtos)
                        {
                            ParameterizedThreadStart Pts = new ParameterizedThreadStart(DownLoadFile);
                            Thread thread = new Thread(Pts);
                            thread.IsBackground = true;
                            thread.Start(media);
                        }
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
                //sortMedia.Clear();
                Mediadetaildto media = (Mediadetaildto)obj;

                string url = media.content;

                string remoteUri = System.IO.Path.GetDirectoryName(url);

                string fileName = System.IO.Path.GetFileName(url);
                string filePath = MainModel.MediaPath + fileName;
                if (File.Exists(filePath))
                {
                    sortMedia.Add(media.sortnum,media);
                }
                else
                {
                     

                WebClient myWebClient = new WebClient();

                myWebClient.DownloadFile(url, filePath);

                sortMedia.Add(media.sortnum, media);
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ERROR", "下载文件异常：" + ex.Message);
            }
        }


        private bool LoadMediaResult = false;
        private bool DownLoadImgResult = false;
        private bool DownLoadMp4Result = false;
        private List<Mediadetaildto> CurrentMediadetaildtos = new List<Mediadetaildto>();
        private void PlayerThread()
        {
            try
            {
                if (sortMedia.Count > 0)
                {                    

                    List<Mediadetaildto> lstmedia = new List<Mediadetaildto>();
                    foreach (KeyValuePair<int, Mediadetaildto> kv in sortMedia)
                    { 
                        lstmedia.Add((Mediadetaildto)MainModel.Clone(kv.Value));
                    }
                   // SortedDictionary<int, Mediadetaildto> sortTempMedia = (SortedDictionary<int, Mediadetaildto>)MainModel.Clone(sortMedia);
                    foreach (Mediadetaildto media in lstmedia)
                    {
                        //for (int i = 0; i < sortMediaCount;i++ )
                        //{
                        try
                        {
                            //Mediadetaildto media = kv.Value;

                            if (media.mediatype == 1) //图片
                            {
                                player.Visible = false;
                                string ImgUrl = media.content;

                                Image _image = Image.FromStream(System.Net.WebRequest.Create(ImgUrl).GetResponse().GetResponseStream());

                                string imgname = media.content.Substring(media.content.LastIndexOf("/") + 1); //URL 最后的值


                                if (File.Exists(MainModel.MediaPath + imgname))
                                {
                                    Image imgback = Image.FromFile(MainModel.MediaPath + imgname);
                                    imgback.Tag = imgname;

                                    //同一个图片不更换 以免屏幕闪动
                                    if (tabPageAdvert.BackgroundImage == null || tabPageAdvert.BackgroundImage.Tag.ToString() != imgname)
                                    {
                                        tabPageAdvert.BackgroundImage = imgback;
                                    }

                                    //Application.DoEvents();
                                    Delay.Start(3000);
                                }
                                else
                                {

                                }

                            }
                            else if (media.mediatype == 2) //视频
                            {
                                string playername = media.content.Substring(media.content.LastIndexOf("/") + 1); //URL 最后的值

                                if (File.Exists(MainModel.MediaPath + playername))
                                {
                                    try
                                    {
                                        player.Visible = true;
                                        player.URL = MainModel.MediaPath + playername;
                                        this.player.Ctlcontrols.play();


                                     
                                        //访问过于频繁会异常  消息筛选器显示应用程序正在使用中
                                        Delay.Start(100);
                                        //MainModel.IsPlayer = true;

                                        while (!player.status.Contains("停止") && player.Visible)
                                        {
                                            Delay.Start(500);
                                            //MainModel.IsPlayer = true;
                                        }

                                        player.Ctlcontrols.stop();
                                        player.Visible = false;

                                    }
                                    catch (Exception ex)
                                    {
                                        player.Visible = false;
                                       // LogManager.WriteLog("客屏视频播放异常" + ex.Message + ex.StackTrace);
                                    }
                                }
                                else
                                {
                                    player.Ctlcontrols.stop();

                                    player.Visible = false;
                                }

                                //pictureBox1.BackgroundImage = _image;
                            }
                        }
                        catch (Exception ex)
                        {
                            LogManager.WriteLog("客屏单个广告异常" + ex.Message + ex.StackTrace);
                        }
                    }
                }
            player.Visible = false;
            Delay.Start(1000);
            Application.DoEvents();
            PlayerThread();

                
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("客屏广告异常" + ex.Message + ex.StackTrace);
            }
        }

        //播放状态是全屏   不是播放状态 此属性不可修改 （异常:灾难性错误）
        private void player_StatusChange(object sender, EventArgs e)
        {

            MainModel.IsPlayer = true;
            if (player.status.Contains("正在播放"))
            {
                if (!player.fullScreen)
                    player.fullScreen = true;
            }
            
        }

        #endregion



        #region  购物车列表

        public void UpdateForm()
        {
            try
            {
                try
                {
                    threadMedia.Suspend();
                    player.Visible = false;
                    player.Ctlcontrols.stop();
                }
                catch { }

               // UpdateFormExe("");

                tlpnlRight.Left = pnlMemberCard.Left;

                UpdateFormExe("");
                ////////启动扫描处理线程
                //Thread threadItemExedate = new Thread(UpdateFormExe);
                //threadItemExedate.IsBackground = false;
                //threadItemExedate.Start();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("显示客屏购物车异常");
            }
        }


        bool isfirstmembercard = true;

        //增加线程锁  防止多线程操作datagridview 红叉情况
        private object thislock = new object();
        private void UpdateFormExe(object obj)
        {
            lock (thislock)
            {
                try
                {


                    dgvGood.Visible = true;
                    tableLayoutPanel1.Visible = true;

                    pnlPayInfo.Visible = false;
                    try
                    {
                        tabControlMedia.SelectedIndex = 0;
                    }
                    catch (Exception ex)
                    {
                        LogManager.WriteLog("切换窗体异常" + ex.Message);
                    }
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

                    //离线模式不展示会员相关信息
                    if (MainModel.IsOffLine)
                    {
                        picBirthday1.Visible = false;


                        picBirthday2.Visible = false;
                        picBirthday3.Visible = false;
                        picBirthday4.Visible = false;

                        this.tlpnlRight.RowStyles[0] = new RowStyle(SizeType.Percent, 0);
                        this.tlpnlRight.RowStyles[1] = new RowStyle(SizeType.Percent, 65);
                        this.tlpnlRight.RowStyles[2] = new RowStyle(SizeType.Percent, 35);

                        pnlMemberCard.Visible = false;
                    }

                    else if (MainModel.CurrentMember == null)
                    {

                        tabPageIni.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
                        picBirthday1.Visible = false;


                        picBirthday2.Visible = false;
                        picBirthday3.Visible = false;
                        picBirthday4.Visible = false;

                        this.tlpnlRight.RowStyles[0] = new RowStyle(SizeType.Percent, 0);
                        this.tlpnlRight.RowStyles[1] = new RowStyle(SizeType.Percent, 65);
                        this.tlpnlRight.RowStyles[2] = new RowStyle(SizeType.Percent, 35);

                        if (imgmembercard == null && isfirstmembercard)
                        {
                            isfirstmembercard = false;
                            string ErrorMsg = "";
                            string imgurl = httputil.GetMemberCard(ref ErrorMsg);
                            if (!string.IsNullOrEmpty(imgurl) && string.IsNullOrEmpty(ErrorMsg))
                            {
                                //this.Invoke(new InvokeHandler(delegate()
                                //{

                                Image _image = Image.FromStream(System.Net.WebRequest.Create(imgurl).GetResponse().GetResponseStream());
                                imgmembercard = _image;
                                picMemberCard.BackgroundImage = _image;

                                int picwidth = Math.Min(pnlMemberCard.Width, pnlMemberCard.Height) * 4 / 5;
                                picMemberCard.Size = new System.Drawing.Size(picwidth, picwidth);

                                picMemberCard.Location = new System.Drawing.Point((pnlMemberCard.Width - picwidth) / 2, 10);
                                pnlMemberCard.Visible = true;
                                //Application.DoEvents();
                            }

                        }
                        else
                        {
                            // Image _image = Image.FromStream(System.Net.WebRequest.Create(imgurl).GetResponse().GetResponseStream());
                            picMemberCard.BackgroundImage = imgmembercard;
                            int picwidth = Math.Min(pnlMemberCard.Width, pnlMemberCard.Height) * 4 / 5;
                            picMemberCard.Size = new System.Drawing.Size(picwidth, picwidth);

                            picMemberCard.Location = new System.Drawing.Point((pnlMemberCard.Width - picwidth) / 2, 10);
                            pnlMemberCard.Visible = true;
                        }
                    }
                    else
                    {

                        this.tlpnlRight.RowStyles[0] = new RowStyle(SizeType.Percent, 35);
                        this.tlpnlRight.RowStyles[1] = new RowStyle(SizeType.Percent, 65);
                        this.tlpnlRight.RowStyles[2] = new RowStyle(SizeType.Percent, 0);
                        pnlMemberCard.Visible = false;

                        if (CurrentMember.memberinformationresponsevo.onbirthday)
                        {
                            // pnlBirthday.Visible = true;
                            if (!lblWechartNickName.Text.Contains("生日快乐！"))
                            {
                                lblWechartNickName.Text += "生日快乐！";
                            }
                            picBirthday1.Visible = false;
                            picBirthday2.Visible = false;
                            picBirthday3.Visible = false;
                            picBirthday4.Visible = false;

                            tabPageIni.BackColor = Color.DarkSlateBlue;
                            Application.DoEvents();
                            picBirthday1.Visible = true;
                            picBirthday2.Visible = true;
                            picBirthday3.Visible = true;
                            picBirthday4.Visible = true;


                        }
                        else
                        {
                            tabPageIni.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
                            picBirthday1.Visible = false;
                            picBirthday2.Visible = false;
                            picBirthday3.Visible = false;
                            picBirthday4.Visible = false;

                            lblWechartNickName.Text = lblWechartNickName.Text.Replace("生日快乐！", "");
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
                   threadMedia.Suspend();
                //    player.Visible = false;
                
                ////ThreadPool.QueueUserWorkItem(new WaitCallback(IniFormExe));
                //////启动扫描处理线程
                Thread threadLoadMember = new Thread(LoadMemberThread);
                threadLoadMember.IsBackground = true;
                //threadIniExedate.Priority = ThreadPriority.BelowNormal;
                threadLoadMember.Start();

                //LoadMemberThread();
            
            }
            catch (Exception ex)
            {

            }
        }

        public void LoadMemberThread()
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
                    string mobil = CurrentMember.memberheaderresponsevo.mobile;
                    if (mobil.Length > 8)
                    {
                        mobil = mobil.Substring(0, mobil.Length - 8) + "****" + mobil.Substring(mobil.Length - 4);
                    }
                    lblMobil.Text = mobil;

                    if (!string.IsNullOrEmpty(CurrentMember.memberinformationresponsevo.nickname))
                    {
                        lblWechartNickName.Text = CurrentMember.memberinformationresponsevo.nickname + "  你好！";

                    }
                    else
                    {
                        lblWechartNickName.Text = CurrentMember.memberinformationresponsevo.wechatnickname + "  你好！";
                    }

                    pnlMemberCard.Visible = false;


                    this.tlpnlRight.RowStyles[0] = new RowStyle(SizeType.Percent, 35);
                    this.tlpnlRight.RowStyles[1] = new RowStyle(SizeType.Percent, 65);
                    this.tlpnlRight.RowStyles[2] = new RowStyle(SizeType.Percent, 0);

                    if (CurrentMember.memberinformationresponsevo.onbirthday)
                    {
                        // pnlBirthday.Visible = true;
                        if (!lblWechartNickName.Text.Contains("生日快乐！"))
                        {
                            lblWechartNickName.Text += "生日快乐！";
                        }
                        picBirthday1.Visible = false;
                        picBirthday2.Visible = false;
                        picBirthday3.Visible = false;
                        picBirthday4.Visible = false;

                        tabPageIni.BackColor = Color.DarkSlateBlue;
                        Application.DoEvents();
                        picBirthday1.Visible = true;
                        picBirthday2.Visible = true;
                        picBirthday3.Visible = true;
                        picBirthday4.Visible = true;


                    }
                    else
                    {
                        tabPageIni.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
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

                    tabPageIni.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
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

                        int picwidth = Math.Min(pnlMemberCard.Width, pnlMemberCard.Height) * 4 / 5;
                        picMemberCard.Size = new System.Drawing.Size(picwidth, picwidth);

                        picMemberCard.Location = new System.Drawing.Point((pnlMemberCard.Width - picwidth) / 2, 10);

                        pnlMemberCard.Visible = true;
                        //Application.DoEvents();
                    }

                }

                //frmMain.listener.Stop();

                //frmMain.listener.Start();

                Console.WriteLine("会员加载时间" + (DateTime.Now - starttime).TotalMilliseconds);
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
                
                    threadMedia.Suspend();
                    player.Visible = false;
                
               
                frmPaySuccessMedia frmresult = new frmPaySuccessMedia(payinfo.ToString());
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




        public void ShowNumber()
        {
            //try
            //{
               
            //        threadMedia.Suspend();
            //        player.Visible = false;


            //        frmNumber frmnumber = new frmNumber("请输入会员号", NumberType.MemberCode);

            //    frmnumber.frmNumber_SizeChanged(null, null);
            //    frmnumber.Size = new System.Drawing.Size(this.Width / 3, this.Height - 200);
            //    // frmnumber.Location = new System.Drawing.Point(this.Width - frmnumber.Width - 50, 100);

            //    frmnumber.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width + Screen.AllScreens[1].Bounds.Width - frmnumber.Width - 50, 100);

            //    //frmresult.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            //    frmnumber.Show();
            //    Application.DoEvents();
            //}
            //catch (Exception ex)
            //{
            //    LogManager.WriteLog("客屏输入会员号异常" + ex.Message);
            //}
        }

        public void ShowPayInfo(string lblinfo,bool isError)
        {
            try
            {
                
                threadMedia.Suspend();
                player.Visible = false;
                
                tabControlMedia.SelectedIndex = 0;

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
            threadMedia.Suspend();

            ParameterizedThreadStart Pts = new ParameterizedThreadStart(SowBalancePwdThread);
            Thread thread = new Thread(Pts);
            thread.IsBackground = true;
            thread.Start(showorclose);
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
                    if (!string.IsNullOrEmpty(pro.price.purchaselimitsubdesc))
                    {
                        btnPurchaseLimit.Text = pro.price.purchaselimitdesc + "?";
                    }
                    else
                    {
                        btnPurchaseLimit.Text = pro.price.purchaselimitdesc;
                    }
                }
                else
                {
                    btnPurchaseLimit.Visible = false;
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

        private void timerNow_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +GetWeek();
        }


        private string GetWeek()
        {
            try
            {
                string week = string.Empty;
                switch ((int)DateTime.Now.DayOfWeek)
                {
                    case 0:
                        week = "（星期日）";
                        break;
                    case 1:
                        week = "（星期一）";
                        break;
                    case 2:
                        week = "（星期二）";
                        break;
                    case 3:
                        week = "（星期三）";
                        break;
                    case 4:
                        week = "（星期四）";
                        break;
                    case 5:
                        week = "（星期五）";
                        break;
                    default:
                        week = "（星期六）";
                        break;
                }
                return week;
            }
            catch
            {
                return "";
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
   
    }
}
