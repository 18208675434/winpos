using QiandamaPOS.Common;
using QiandamaPOS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QiandamaPOS
{
    public partial class frmMainMedia : Form
    {
        HttpUtil httputil = new HttpUtil();

        /// <summary>
        /// 按比例缩放页面及控件
        /// </summary>
        AutoSizeFormUtil asf = new AutoSizeFormUtil();

        /// <summary>
        /// 委托解决跨线程调用
        /// </summary>
        private delegate void InvokeHandler();
        public frmMainMedia()
        {
            InitializeComponent();

            Control.CheckForIllegalCrossThreadCalls = false;
            IniForm();
        }
        Thread threadIniExedate;
        public void IniForm()
        {

            //启动扫描处理线程
            threadIniExedate = new Thread(IniFormExe);
            threadIniExedate.IsBackground = true;
            threadIniExedate.Start();
           
        }

        private void IniFormExe()
        {
            try
            {
                tabControlMedia.SelectedIndex = 1;


                player.Visible = false;

                //test
                //string testurl = "https://pic.qdama.cn/Fjxb0w7cS11yuWKLt5vMBGN-O2Yq";
                //Image _imagetest = Image.FromStream(System.Net.WebRequest.Create(testurl).GetResponse().GetResponseStream());

                //tabPageAdvert.BackgroundImage = _imagetest;

                //tabPageAdvert.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;


                string ErrorMsg = "";
                MediaList posmedia = httputil.GetPosMedia(ref ErrorMsg);
                player.Visible = false;
                if (ErrorMsg != "" || posmedia == null)
                {
                    //获取异常  显示空白页
                }
                else
                {
                    foreach (Mediadetaildto media in posmedia.mediadetaildtos)
                    {
                        if (media.mediatype == 1) //图片
                        {
                            player.Visible = false;
                            string ImgUrl = media.content;

                            //test
                            //ImgUrl = "https://pic.qdama.cn/Fjxb0w7cS11yuWKLt5vMBGN-O2Yq";
                            Image _image = Image.FromStream(System.Net.WebRequest.Create(ImgUrl).GetResponse().GetResponseStream());

                            tabPageAdvert.BackgroundImage = _image;

                            tabPageAdvert.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;

                            Delay.Start(3000);
                        }
                        else if (media.mediatype == 2)
                        {
                            string PlayerUrl = media.content;
                            player.Visible = true;
                            player.URL = PlayerUrl;
                            this.player.Ctlcontrols.play();

                            while (!player.status.Contains("停止"))
                            {
                                Delay.Start(100);
                            }

                            player.close();
                            player.Visible = false;
                            //pictureBox1.BackgroundImage = _image;
                        }
                       
                    }
                }
            }
            catch (Exception ex)
            {
               // LogManager.WriteLog("ERROR", "初始化客屏信息异常：" + ex.Message);
            }
        }

        private Cart CurrentCart;
        private Member CurrentMember;


        public void UpdateForm(Cart cart,Member member)
        {
            try
            {

                pnlPayInfo.Visible = false;
                tabControlMedia.SelectedIndex = 0;
                CurrentCart = (Cart)cart.qianClone();
                CurrentMember = member;

                //启动扫描处理线程
                Thread threadItemExedate = new Thread(UpdateFormExe);
                threadItemExedate.IsBackground = true;
                threadItemExedate.Start();

                //UpdateFormExe();
                //UpdateFormExe(Cart,member);
                //Application.DoEvents();
            }
            catch (Exception ex)
            {

            }

           
        }

        private void UpdateFormExe()
        {
            try
            {
                try { player.close(); }
                catch { }

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
                                dgvOrderDetail.Rows.Add(CurrentCart.orderpricedetails[i].title, CurrentCart.orderpricedetails[i].amount);
                                dgvOrderDetail.ClearSelection();
                            }
                        }
                        lblPrice.Text = CurrentCart.totalpayment.ToString("f2");

                        int count = CurrentCart.products.Count;
                        lblGoodsCount.Text = count.ToString();
                        if (count > 0)
                        {
                            for (int i = 0; i < count; i++)
                            {
                                //TODO 下划线

                                Product pro = CurrentCart.products[i];

                                string barcode = "\r\n  " + pro.title + "\r\n  " + pro.barcode;
                                string price = "";
                                string jian = "";

                                string num = "";
                                string add = "";
                                string total = "";
                                switch (pro.pricetagid)
                                {
                                    case 1: barcode = "1" + pro.pricetag + "\r\n  " + pro.title + "\r\n  " + pro.barcode; break;
                                    case 2: barcode = "2" + pro.pricetag + "\r\n  " + pro.title + "\r\n  " + pro.barcode; break;
                                    case 3: barcode = "3" + pro.pricetag + "\r\n  " + pro.title + "\r\n  " + pro.barcode; break;
                                    case 4: barcode = "4" + pro.pricetag + "\r\n  " + pro.title + "\r\n  " + pro.barcode; break;
                                }

                                if (pro.price.saleprice == pro.price.originprice)
                                {
                                    price = pro.price.saleprice.ToString("f2");
                                }
                                else
                                {
                                    //price = "￥" + pro.price.saleprice.ToString() + "("+pro.price.salepricedesc+")" + "\r\n" + "￥" + pro.price.originprice + "("+pro.price.originpricedesc+")";

                                    price = pro.price.saleprice.ToString("f2");
                                    if (!string.IsNullOrWhiteSpace(pro.price.salepricedesc))
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

                                    if (!string.IsNullOrWhiteSpace(pro.price.originpricedesc))
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

                                    if (!string.IsNullOrWhiteSpace(pro.price.salepricedesc))
                                    {
                                        total += "(" + pro.price.salepricedesc + ")";
                                    }
                                    total += "\r\n" + pro.price.origintotal.ToString("f2");
                                    if (!string.IsNullOrWhiteSpace(pro.price.originpricedesc))
                                    {
                                        total += "(" + pro.price.originpricedesc + ")";
                                    }


                                }

                                dgvGood.Rows.Insert(0, new object[] { barcode, price, jian, num, add, total });
                            }
                            dgvGood.ClearSelection();


                        }
                    }


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
                        //picMemberCard.Visible = false;
                        //picBirthday1.Visible = true;
                        //picBirthday2.Visible = true;
                        //picBirthday3.Visible = true;
                        //picBirthday4.Visible = true;
                    }
                    else
                    {
                        lblMobil.Text = "";
                        lblWechartNickName.Text = "";

                        
                        string ErrorMsg = "";
                        string imgurl = httputil.GetMemberCard(ref ErrorMsg);
                        if (!string.IsNullOrWhiteSpace(httputil.GetMemberCard(ref ErrorMsg)) && ErrorMsg != null)
                        {
                            
                            Image _image = Image.FromStream(System.Net.WebRequest.Create(imgurl).GetResponse().GetResponseStream());
                            picMemberCard.BackgroundImage = _image;

                            int picwidth = Math.Min(pnlMemberCard.Width, pnlMemberCard.Height) * 4/ 5;
                            picMemberCard.Size = new System.Drawing.Size(picwidth, picwidth);

                            picMemberCard.Location = new System.Drawing.Point((pnlMemberCard.Width - picwidth) / 2, 10);
                            pnlMemberCard.Visible = true;
                        }



                    }
                

                Application.DoEvents();

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ERROR","显示客屏购物车异常："+ex.Message);
            }
        }

        public void ShowPayResult(object payinfo)
        {
            try
            {
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
                frmNumber frmnumber = new frmNumber("请输入会员号", true,false);

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
                LogManager.WriteLog("客屏输入会员号异常"+ex.Message);
            }

        }

        public void ShowPayInfo(string lblinfo)
        {
            try
            {
                //player.Visible = false;
                tabControlMedia.SelectedIndex = 0;


                // pnlMemberCard.Visible = true;

                pnlPayInfo.Visible = true;
                lblPayInfo.Text = lblinfo;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("客屏提示支付信息异常：" + ex.Message);
            }
        }

        private void frmMainMedia_SizeChanged(object sender, EventArgs e)
        {
          // asf.ControlAutoSize(this);
        }

        //播放状态是全屏   不是播放状态 此属性不可修改 （异常:灾难性错误）
        private void player_StatusChange(object sender, EventArgs e)
        {
            if (player.status.Contains("正在播放"))
            {
                if(!player.fullScreen)
                player.fullScreen = true;
            }
        }





        //重绘datagridview单元格
        private void dgvGood_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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


                Font fnt1 = new System.Drawing.Font("微软雅黑", 10F * Math.Min( MainModel.hScale,  MainModel.wScale));
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
                Font fnt2 = new System.Drawing.Font("微软雅黑", 12F * Math.Min( MainModel.hScale,  MainModel.wScale));
                SizeF size2 = this.CreateGraphics().MeasureString(strLine2, fnt2);

                intY = intY + (int)size1.Height;
                TextRenderer.DrawText(e.Graphics, strLine2, fnt2, new Rectangle(intX, intY, intWidth, (int)size2.Height),
                    Color.Black, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);

                //Font fnt20 = new System.Drawing.Font("微软雅黑", 9F, FontStyle.Strikeout);
                //TextRenderer.DrawText(e.Graphics, strLine2, fnt20, new Rectangle(intX + (int)size2.Width, intY, intWidth, (int)size2.Height),
                //    Color.Green, Color.Red, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);


                Font fnt3 = new System.Drawing.Font("微软雅黑", 12F * Math.Min( MainModel.hScale,  MainModel.wScale));
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

                dgvGood.ClearSelection();
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


                Font fnt1 = new System.Drawing.Font("微软雅黑", 12F * Math.Min( MainModel.hScale,  MainModel.wScale));
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

                    Font tempfont2 = new System.Drawing.Font("微软雅黑", 10F * Math.Min( MainModel.hScale,  MainModel.wScale));

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
                Font fnt2 = new System.Drawing.Font("微软雅黑", 10F * Math.Min( MainModel.hScale,  MainModel.wScale));
                bool isstrickout = false;
                if (strLine2.Contains("strikeout"))
                {
                    isstrickout = true;
                    fnt2 = new System.Drawing.Font("微软雅黑", 10F * Math.Min( MainModel.hScale,  MainModel.wScale), FontStyle.Strikeout);
                    strLine2 = strLine2.Replace("strikeout", "");
                }
                SizeF size2 = this.CreateGraphics().MeasureString(strLine2, fnt2);
                intY = intY + (int)size1.Height;

                if (strLine2.Contains("("))
                {
                    int index = strLine1.IndexOf("(");

                    string tempstrline21 = strLine2.Substring(0, index);
                    string tempstrline22 = strLine2.Substring(index);

                    SizeF siztemp1 = this.CreateGraphics().MeasureString(tempstrline21, fnt2);
                    SizeF sizetemp2 = this.CreateGraphics().MeasureString(tempstrline22, fnt2);

                    int pianyiX = (int)(e.CellBounds.Width - siztemp1.Width - sizetemp2.Width) / 2;
                    //第一行
                    TextRenderer.DrawText(e.Graphics, tempstrline21, fnt2, new Rectangle(intX + pianyiX, intY, intWidth, (int)siztemp1.Height),
                        Color.Black, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);

                    Font tempfont2 = new System.Drawing.Font("微软雅黑", 10F * Math.Min( MainModel.hScale,  MainModel.wScale));

                    TextRenderer.DrawText(e.Graphics, tempstrline22, fnt2, new Rectangle(intX + (int)siztemp1.Width + pianyiX, intY + 5, intWidth, (int)size2.Height),
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

                dgvGood.ClearSelection();
            }


        }




    }
}
