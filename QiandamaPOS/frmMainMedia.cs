using QiandamaPOS.Common;
using QiandamaPOS.Model;
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

namespace QiandamaPOS
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

        #endregion




        #region 页面加载与关闭
        public frmMainMedia()
        {
            InitializeComponent();

            //使用委托的话frmmain界面会卡死
            Control.CheckForIllegalCrossThreadCalls = false;
            // IniForm(null);

            threadMedia = new Thread(PlayerThread);
            threadMedia.IsBackground = true;
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
          

            timerMedia.Interval = 10 * 60 * 1000;
            timerMedia.Enabled = true;
        }

        private void frmMainMedia_SizeChanged(object sender, EventArgs e)
        {
            // asf.ControlAutoSize(this);
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
            //isplayer = true;
            ////ThreadPool.QueueUserWorkItem(new WaitCallback(IniFormExe));
            //////启动扫描处理线程
            //threadIniExedate = new Thread(IniFormExe);
            //threadIniExedate.IsBackground = true;
            ////threadIniExedate.Priority = ThreadPriority.BelowNormal;
            //threadIniExedate.Start();
        }

        private void IniFormExe(object obj)
        {
            try
            {
                tabControlMedia.SelectedIndex = 1;

                player.Visible = false;

                string ErrorMsg = "";
                MediaList posmedia = httputil.GetPosMedia(ref ErrorMsg);

                if (ErrorMsg != "" || posmedia == null)
                {
                    //获取异常  显示空白页
                }
                else
                {
                    CurrentMediadetaildtos =posmedia.mediadetaildtos;
                    foreach (Mediadetaildto media in posmedia.mediadetaildtos)
                    {
                        ParameterizedThreadStart Pts = new ParameterizedThreadStart(DownLoadFile);
                        Thread thread = new Thread(Pts);
                        thread.IsBackground = true;
                        thread.Start(media);
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
                sortMediaCount = sortMedia.Count;
               // foreach (KeyValuePair<int, Mediadetaildto> keval in sortMedia)
                    for (int i = 0; i < sortMediaCount;i++ )
                    //foreach (Mediadetaildto media in CurrentMediadetaildtos)
                    {
                        try
                        {
                            Mediadetaildto media = sortMedia[i];

                            if (media.mediatype == 1) //图片
                            {
                                player.Visible = false;
                                string ImgUrl = media.content;

                                Image _image = Image.FromStream(System.Net.WebRequest.Create(ImgUrl).GetResponse().GetResponseStream());

                                string imgname = media.content.Substring(media.content.LastIndexOf("/") + 1); //URL 最后的值

                                tabPageAdvert.BackgroundImage = _image;

                                if (File.Exists(MainModel.MediaPath + imgname))
                                {

                                    tabPageAdvert.BackgroundImage = Image.FromFile(MainModel.MediaPath + imgname);
                                    Application.DoEvents();
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

                                        while (!player.status.Contains("停止") && player.Visible)
                                        {
                                            Delay.Start(100);
                                        }

                                        //player.close();
                                        player.Visible = false;

                                    }
                                    catch (Exception ex)
                                    {
                                        //player.close();
                                        player.Visible = false;
                                        LogManager.WriteLog("客屏视频播放异常" + ex.Message);
                                    }
                                }
                                else
                                {
                                    //player.close();
                                    player.Visible = false;
                                }

                                //pictureBox1.BackgroundImage = _image;
                            }
                        }
                        catch (Exception ex)
                        {
                            LogManager.WriteLog("客屏单个广告异常" + ex.Message+ex.StackTrace);
                        }
                    }
                    player.Visible = false;
            Delay.Start(1000);
                //Thread.Sleep(100);
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
            if (player.status.Contains("正在播放"))
            {
                if (!player.fullScreen)
                    player.fullScreen = true;
            }

            //player.
        }


        #endregion




        #region  购物车列表

        public void UpdateForm()
        {
            try
            {
              
                    threadMedia.Suspend();
                    player.Visible = false;
                    player.close();
                
                //////启动扫描处理线程
                Thread threadItemExedate = new Thread(UpdateFormExe);
                threadItemExedate.IsBackground = false;
                //threadIniExedate.Priority = ThreadPriority.BelowNormal;
                threadItemExedate.Start();
                //isplayer = false;
                //ThreadPool.QueueUserWorkItem(new WaitCallback(UpdateFormExe));
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("显示客屏购物车异常");
            }
        }

        //增加线程锁  防止多线程操作datagridview 红叉情况
        private object thislock = new object();
        private void UpdateFormExe(object obj)
        {
            lock (thislock)
            {
                try
                {
                    DateTime starttime = DateTime.Now;


                    dgvGood.Visible = true;
                    tableLayoutPanel1.Visible = true;

                    pnlPayInfo.Visible = false;
                    try
                    {
                        tabControlMedia.SelectedIndex = 0;
                    }
                    catch (Exception ex)
                    {
                        LogManager.WriteLog("切换窗体异常" +ex.Message);
                    }
                    CurrentCart = MainModel.frmMainmediaCart;
                    CurrentMember = MainModel.CurrentMember;
                    player.Visible = false;
                    try { //player.close(); 
                    }
                    catch {
                        LogManager.WriteLog("关闭视频异常");
                    }

                    try { threadIniExedate.Abort(); }
                    catch { }
                    //pnlPayInfo.Visible = false;
                    dgvGood.Rows.Clear();
                    dgvOrderDetail.Rows.Clear();
                    if (CurrentCart != null && CurrentCart.products != null && CurrentCart.products.Count > 0)
                    {
                        int orderCount = CurrentCart.orderpricedetails.Length;
                        if (orderCount == 0)
                        {

                        }
                        else
                        {
                            for (int i = 0; i < orderCount; i++)
                            {
                                try
                                {
                                    dgvOrderDetail.Rows.Add(CurrentCart.orderpricedetails[i].title, CurrentCart.orderpricedetails[i].amount);
                                }
                                catch { }

                            }
                            dgvOrderDetail.ClearSelection();
                        }
                        lblPrice.Text = "￥" + CurrentCart.totalpayment.ToString("f2");

                        int count = CurrentCart.products.Count;
                        lblGoodsCount.Text = "(" + count.ToString() + "件商品)";
                        if (count > 0)
                        {
                            for (int i = 0; i < count; i++)
                            {
                                //TODO 下划线

                                Product pro = CurrentCart.products[i];

                                string barcode = "\r\n  " + pro.title + "\r\n  " + pro.skucode;
                                string price = "";
                                string jian = "";

                                string num = "";
                                string add = "";
                                string total = "";
                                switch (pro.pricetagid)
                                {
                                    case 1: barcode = "1" + pro.pricetag + "\r\n  " + pro.title + "\r\n  " + pro.skucode; break;
                                    case 2: barcode = "2" + pro.pricetag + "\r\n  " + pro.title + "\r\n  " + pro.skucode; break;
                                    case 3: barcode = "3" + pro.pricetag + "\r\n  " + pro.title + "\r\n  " + pro.skucode; break;
                                    case 4: barcode = "4" + pro.pricetag + "\r\n  " + pro.title + "\r\n  " + pro.skucode; break;
                                }

                                if (pro.price.saleprice == pro.price.originprice)
                                {
                                    price = pro.price.saleprice.ToString("f2");
                                }
                                else
                                {
                                    //price = "￥" + pro.price.saleprice.ToString() + "("+pro.price.salepricedesc+")" + "\r\n" + "￥" + pro.price.originprice + "("+pro.price.originpricedesc+")";

                                    price = pro.price.saleprice.ToString("f2");
                                    if (!string.IsNullOrEmpty(pro.price.salepricedesc))
                                    {
                                        price += "(" + pro.price.salepricedesc + ")";
                                    }

                                    if (pro.price.strikeout == 1)
                                    {
                                        price += "\r\n" + "strikeout" + pro.price.originprice.ToString("f2");
                                    }
                                    else
                                    {
                                        price += "\r\n" + pro.price.originprice.ToString("f2");
                                    }

                                    if (!string.IsNullOrEmpty(pro.price.originpricedesc))
                                    {
                                        price += "(" + pro.price.originpricedesc + ")";
                                    }
                                }

                                if (pro.goodstagid == 0)  //0是标品  1是称重
                                {

                                    num = pro.num.ToString();
                                }
                                else
                                {
                                    add = "";
                                    jian = "";
                                    num = pro.price.specnum + pro.price.unit;
                                }

                                if (pro.price.total == pro.price.origintotal)
                                {
                                    total = pro.price.total.ToString("f2");
                                }
                                else
                                {
                                    //total = "￥" + pro.price.total.ToString() + "("+pro.price.salepricedesc+")" + "\r\n" + "￥" + pro.price.origintotal + "("+pro.price.originpricedesc+")";

                                    total = pro.price.total.ToString("f2");

                                    if (!string.IsNullOrEmpty(pro.price.salepricedesc))
                                    {
                                        total += "(" + pro.price.salepricedesc + ")";
                                    }
                                    total += "\r\n" + pro.price.origintotal.ToString("f2");
                                    if (!string.IsNullOrEmpty(pro.price.originpricedesc))
                                    {
                                        total += "(" + pro.price.originpricedesc + ")";
                                    }
                                }
                                try
                                {
                                    dgvGood.Rows.Insert(0, new object[] { barcode, price, jian, num, add, total });
                                }
                                catch { }
                            }
                            //dgvGood.ClearSelection();
                            //Application.DoEvents();

                        }
                    }

                    Console.WriteLine("客屏加载时间" + (DateTime.Now - starttime).TotalMilliseconds);

                }
                catch (Exception ex)
                {
                    //frmMain.listener.Start();
                    LogManager.WriteLog("ERROR", "显示客屏购物车异常：" + ex.Message + ex.StackTrace);
                }
            }
        }

        //重绘datagridview单元格
        private void dgvGood_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0 && e.RowIndex >= 0 && e.Value != null)//要进行重绘的单元格
                {

                    Graphics gpcEventArgs = e.Graphics;
                    Color clrBack = e.CellStyle.BackColor;
                    //Font fntText = e.CellStyle.Font;//获取单元格字体
                    //先使用北京颜色重画一遍背景
                    gpcEventArgs.FillRectangle(new SolidBrush(clrBack), e.CellBounds);
                    //设置字体的颜色
                    Color oneFore = System.Drawing.Color.Black;
                    Color secFore = System.Drawing.Color.Red;
                    //string strFirstLine = "黑色内容";
                    //string strSecondLine = "红色内容";

                    if (!e.Value.ToString().Contains("\r\n"))
                    {
                        return;
                    }

                    string tempstr = e.Value.ToString().Replace("\r\n", "*");
                    string strLine1 = "";
                    string strLine2 = "";
                    string strLine3 = "";

                    strLine1 = tempstr.Split('*')[0];
                    strLine2 = tempstr.Split('*')[1];
                    strLine3 = tempstr.Split('*')[2];
                    string[] sts = tempstr.Split('*');
                    //Size sizText = TextRenderer.MeasureText(e.Graphics, strFirstLine, fntText);
                    int intX = e.CellBounds.Left + e.CellStyle.Padding.Left;
                    int intY = e.CellBounds.Top + e.CellStyle.Padding.Top + 10;
                    int intWidth = e.CellBounds.Width - (e.CellStyle.Padding.Left + e.CellStyle.Padding.Right);
                    //int intHeight = sizText.Height + (e.CellStyle.Padding.Top + e.CellStyle.Padding.Bottom);


                    Font fnt1 = new System.Drawing.Font("微软雅黑", 10F * Math.Min(MainModel.hScale, MainModel.wScale));
                    //Graphics g = this.CreateGraphics(); //this是指所有control派生出来的类，这里是个form

                    SizeF size1 = this.CreateGraphics().MeasureString(strLine1, fnt1);
                    Color titlebackcolor = Color.Black;
                    if (strLine1.Length > 0)
                    {
                        string typecolor = strLine1.Substring(0, 1);
                        strLine1 = strLine1.Substring(1, strLine1.Length - 1);
                        switch (typecolor)
                        {
                            case "1": titlebackcolor = ColorTranslator.FromHtml("#FF7D14"); break;
                            case "2": titlebackcolor = ColorTranslator.FromHtml("#209FD4"); break;
                            case "3": titlebackcolor = ColorTranslator.FromHtml("#D42031"); break;
                            case "4": titlebackcolor = ColorTranslator.FromHtml("#FF000"); break;
                        }
                    }
                    //第一行
                    TextRenderer.DrawText(e.Graphics, strLine1, fnt1, new Rectangle(intX + 10, intY, intWidth, (int)size1.Height),
                        Color.White, titlebackcolor, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);

                    //另起一行
                    Font fnt2 = new System.Drawing.Font("微软雅黑", 12F * Math.Min(MainModel.hScale, MainModel.wScale));
                    SizeF size2 = this.CreateGraphics().MeasureString(strLine2, fnt2);

                    intY = intY + (int)size1.Height;
                    TextRenderer.DrawText(e.Graphics, strLine2, fnt2, new Rectangle(intX, intY, intWidth, (int)size2.Height),
                        Color.Black, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);

                    //Font fnt20 = new System.Drawing.Font("微软雅黑", 9F, FontStyle.Strikeout);
                    //TextRenderer.DrawText(e.Graphics, strLine2, fnt20, new Rectangle(intX + (int)size2.Width, intY, intWidth, (int)size2.Height),
                    //    Color.Green, Color.Red, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);


                    Font fnt3 = new System.Drawing.Font("微软雅黑", 12F * Math.Min(MainModel.hScale, MainModel.wScale));
                    intY = intY + (int)size2.Height;

                    TextRenderer.DrawText(e.Graphics, strLine3, fnt3, new Rectangle(intX, intY, intWidth, (int)size2.Height), Color.Black, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);


                    //int y = intY + (int)size2.Height + e.CellStyle.Padding.Top + e.CellStyle.Padding.Bottom + dgvGood.RowTemplate.Height;
                    int y = (e.RowIndex + 1) * dgvGood.RowTemplate.Height + dgvGood.ColumnHeadersHeight - 1;

                    //Point point1 = new Point(0, y);
                    //Point point2 = new Point(e.CellBounds.Width, y);
                    //Pen blackPen = new Pen(Color.Black, 1);
                    //e.Graphics.DrawLine(blackPen, point1, point2);

                    //Point point21 = new Point(10, 0);
                    //Point point22 = new Point(10, intY + (int)size2.Height + e.CellStyle.Padding.Top + e.CellStyle.Padding.Bottom);
                    //Pen blackPen2 = new Pen(Color.Black, 10);
                    //e.Graphics.DrawLine(blackPen2, point21, point21);
                    // e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    // dgv2.Rows[e.RowIndex].Height = (int)size1.Height+(int)size2.Height*2 + e.CellStyle.Padding.Top + e.CellStyle.Padding.Bottom+1;
                    e.Handled = true;

                    // dgvGood.ClearSelection();
                }

                if ((e.ColumnIndex == 1 || e.ColumnIndex == 5) && e.RowIndex >= 0 && e.Value != null)//要进行重绘的单元格
                {

                    Graphics gpcEventArgs = e.Graphics;
                    Color clrBack = e.CellStyle.BackColor;
                    //Font fntText = e.CellStyle.Font;//获取单元格字体
                    //先使用北京颜色重画一遍背景
                    gpcEventArgs.FillRectangle(new SolidBrush(clrBack), e.CellBounds);
                    //设置字体的颜色
                    Color oneFore = System.Drawing.Color.Black;
                    Color secFore = System.Drawing.Color.Red;
                    //string strFirstLine = "黑色内容";
                    //string strSecondLine = "红色内容";

                    if (!e.Value.ToString().Contains("\r\n"))
                    {
                        return;
                    }

                    string tempstr = e.Value.ToString().Replace("\r\n", "*");
                    string strLine1 = "";
                    string strLine2 = "";


                    strLine1 = tempstr.Split('*')[0];
                    strLine2 = tempstr.Split('*')[1];

                    string[] sts = tempstr.Split('*');
                    //Size sizText = TextRenderer.MeasureText(e.Graphics, strFirstLine, fntText);
                    int intX = e.CellBounds.Left + e.CellStyle.Padding.Left;
                    int intY = e.CellBounds.Top + e.CellStyle.Padding.Top + 30;
                    int intWidth = e.CellBounds.Width - (e.CellStyle.Padding.Left + e.CellStyle.Padding.Right);
                    //int intHeight = sizText.Height + (e.CellStyle.Padding.Top + e.CellStyle.Padding.Bottom);


                    Font fnt1 = new System.Drawing.Font("微软雅黑", 12F * Math.Min(MainModel.hScale, MainModel.wScale));
                    //Graphics g = this.CreateGraphics(); //this是指所有control派生出来的类，这里是个form
                    SizeF size1 = this.CreateGraphics().MeasureString(strLine1, fnt1);

                    if (strLine1.Contains("("))
                    {
                        int index = strLine1.IndexOf("(");

                        string tempstrline11 = strLine1.Substring(0, index);
                        string tempstrline12 = strLine1.Substring(index);

                        SizeF siztemp1 = this.CreateGraphics().MeasureString(tempstrline11, fnt1);
                        SizeF sizetemp2 = this.CreateGraphics().MeasureString(tempstrline12, fnt1);

                        int pianyiX = (int)(e.CellBounds.Width - siztemp1.Width - sizetemp2.Width) / 2;
                        if (e.ColumnIndex == 5)
                        {
                            TextRenderer.DrawText(e.Graphics, tempstrline11, fnt1, new Rectangle(intX + pianyiX, intY, intWidth, (int)siztemp1.Height),
                                Color.OrangeRed, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);

                        }
                        else
                        {
                            TextRenderer.DrawText(e.Graphics, tempstrline11, fnt1, new Rectangle(intX + pianyiX, intY, intWidth, (int)siztemp1.Height),
                                Color.Black, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);

                        }

                        Font tempfont2 = new System.Drawing.Font("微软雅黑", 10F * Math.Min(MainModel.hScale, MainModel.wScale));

                        TextRenderer.DrawText(e.Graphics, tempstrline12, tempfont2, new Rectangle(intX + (int)siztemp1.Width + pianyiX, intY + 5, intWidth, (int)siztemp1.Height),
                          Color.DimGray, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);

                    }
                    else
                    {
                        if (e.ColumnIndex == 5)
                        {
                            //第一行
                            TextRenderer.DrawText(e.Graphics, strLine1, fnt1, new Rectangle(intX + (int)(e.CellBounds.Width - size1.Width) / 2, intY, intWidth, (int)size1.Height),
                                Color.OrangeRed, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);
                        }
                        else
                        {
                            //第一行
                            TextRenderer.DrawText(e.Graphics, strLine1, fnt1, new Rectangle(intX + (int)(e.CellBounds.Width - size1.Width) / 2, intY, intWidth, (int)size1.Height),
                                Color.Black, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);
                        }

                    }


                    //第二行
                    Font fnt2 = new System.Drawing.Font("微软雅黑", 10F * Math.Min(MainModel.hScale, MainModel.wScale));
                    bool isstrickout = false;
                    if (strLine2.Contains("strikeout"))
                    {
                        isstrickout = true;
                        fnt2 = new System.Drawing.Font("微软雅黑", 10F * Math.Min(MainModel.hScale, MainModel.wScale), FontStyle.Strikeout);
                        strLine2 = strLine2.Replace("strikeout", "");
                    }
                    SizeF size2 = this.CreateGraphics().MeasureString(strLine2, fnt2);
                    intY = intY + (int)size1.Height;

                    if (strLine2.Contains("("))
                    {
                        int index = strLine2.IndexOf("(");

                        string tempstrline21 = strLine2.Substring(0, index);
                        string tempstrline22 = strLine2.Substring(index);

                        SizeF siztemp1 = this.CreateGraphics().MeasureString(tempstrline21, fnt2);
                        SizeF sizetemp2 = this.CreateGraphics().MeasureString(tempstrline22, fnt2);

                        int pianyiX = (int)(e.CellBounds.Width - siztemp1.Width - sizetemp2.Width) / 2;
                        //第一行
                        TextRenderer.DrawText(e.Graphics, tempstrline21, fnt2, new Rectangle(intX + pianyiX, intY, intWidth, (int)siztemp1.Height),
                            Color.DimGray, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);

                        Font tempfont2 = new System.Drawing.Font("微软雅黑", 10F * Math.Min(MainModel.hScale, MainModel.wScale));

                        TextRenderer.DrawText(e.Graphics, tempstrline22, fnt2, new Rectangle(intX + (int)siztemp1.Width + pianyiX, intY, intWidth, (int)size2.Height),
                        Color.DimGray, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);

                    }
                    else
                    {

                        TextRenderer.DrawText(e.Graphics, strLine2, fnt2, new Rectangle(intX + (int)(e.CellBounds.Width - size2.Width) / 2, intY, intWidth, (int)size2.Height),
                        Color.DimGray, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);
                    }


                    //int y = intY + (int)size2.Height + e.CellStyle.Padding.Top + e.CellStyle.Padding.Bottom + dgvGood.RowTemplate.Height;
                    int y = (e.RowIndex + 1) * dgvGood.RowTemplate.Height + dgvGood.ColumnHeadersHeight - 1;

                    //Point point1 = new Point(0, y);
                    ////Point point2 = new Point(e.CellBounds.Width, y);
                    //Point point2 = new Point(dgvGood.Width, y);
                    //Pen blackPen = new Pen(Color.Black, 1);
                    //e.Graphics.DrawLine(blackPen, point1, point2);


                    // dgv2.Rows[e.RowIndex].Height = (int)size1.Height+(int)size2.Height*2 + e.CellStyle.Padding.Top + e.CellStyle.Padding.Bottom+1;
                    e.Handled = true;

                    // dgvGood.ClearSelection();
                }
            }
            catch (Exception ex)
            {

            }

        }
        #endregion



        #region 加载会员
        public void LoadMember()
        {
            try
            {

               
                    threadMedia.Suspend();
                    player.Visible = false;
                
                //ThreadPool.QueueUserWorkItem(new WaitCallback(IniFormExe));
                ////启动扫描处理线程
                Thread threadLoadMember = new Thread(LoadMemberThread);
                threadLoadMember.IsBackground = true;
                //threadIniExedate.Priority = ThreadPriority.BelowNormal;
                threadLoadMember.Start();
            
            }
            catch (Exception ex)
            {

            }
        }

        public void LoadMemberThread()
        {
            try
            {
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
                    lblWechartNickName.Text = CurrentMember.memberinformationresponsevo.wechatnickname + "  你好！";

                    pnlMemberCard.Visible = false;


                    if (CurrentMember.memberinformationresponsevo.onbirthday)
                    {
                        // pnlBirthday.Visible = true;
                        picBirthday1.Visible = true;


                        picBirthday2.Visible = true;
                        picBirthday3.Visible = true;
                        picBirthday4.Visible = true;
                    }
                    else
                    {
                        picBirthday1.Visible = false;
                        picBirthday2.Visible = false;
                        picBirthday3.Visible = false;
                        picBirthday4.Visible = false;
                    }
                }
                else
                {
                    lblMobil.Text = "";
                    lblWechartNickName.Text = "";

                    string ErrorMsg = "";
                    string imgurl = httputil.GetMemberCard(ref ErrorMsg);
                    if (!string.IsNullOrEmpty(httputil.GetMemberCard(ref ErrorMsg)) && ErrorMsg != null)
                    {

                        Image _image = Image.FromStream(System.Net.WebRequest.Create(imgurl).GetResponse().GetResponseStream());
                        picMemberCard.BackgroundImage = _image;

                        int picwidth = Math.Min(pnlMemberCard.Width, pnlMemberCard.Height) * 4 / 5;
                        picMemberCard.Size = new System.Drawing.Size(picwidth, picwidth);

                        picMemberCard.Location = new System.Drawing.Point((pnlMemberCard.Width - picwidth) / 2, 10);

                        pnlMemberCard.Visible = true;
                    }

                    picBirthday1.Visible = false;
                    picBirthday2.Visible = false;
                    picBirthday3.Visible = false;
                    picBirthday4.Visible = false;
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
                
               
                frmCashierResultMedia frmresult = new frmCashierResultMedia(payinfo.ToString());
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
            try
            {
               
                    threadMedia.Suspend();
                    player.Visible = false;
                
                
                frmNumber frmnumber = new frmNumber("请输入会员号", true, false);

                frmnumber.frmNumber_SizeChanged(null, null);
                frmnumber.Size = new System.Drawing.Size(this.Width / 3, this.Height - 200);
                // frmnumber.Location = new System.Drawing.Point(this.Width - frmnumber.Width - 50, 100);

                frmnumber.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width + Screen.AllScreens[1].Bounds.Width - frmnumber.Width - 50, 100);

                //frmresult.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                frmnumber.Show();
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("客屏输入会员号异常" + ex.Message);
            }
        }

        public void ShowPayInfo(string lblinfo)
        {
            try
            {
                
                    threadMedia.Suspend();
                    player.Visible = false;
                
                //player.Visible = false;
                tabControlMedia.SelectedIndex = 0;

                // pnlMemberCard.Visible = true;
                dgvGood.Visible = false;
                tableLayoutPanel1.Visible = false;

                pnlPayInfo.Visible = true;
                lblPayInfo.Text = lblinfo;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("客屏提示支付信息异常：" + ex.Message);
            }
        }

        #endregion

     

   
    }
}
