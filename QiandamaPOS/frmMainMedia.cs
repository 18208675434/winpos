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

        public void IniForm()
        {

            //启动扫描处理线程
            Thread threadItemExedate = new Thread(IniFormExe);
            threadItemExedate.IsBackground = true;
            threadItemExedate.Start();
           
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
                            string ImgUrl = media.content;

                            //test
                            //ImgUrl = "https://pic.qdama.cn/Fjxb0w7cS11yuWKLt5vMBGN-O2Yq";
                            Image _image = Image.FromStream(System.Net.WebRequest.Create(ImgUrl).GetResponse().GetResponseStream());

                            tabPageAdvert.BackgroundImage = _image;

                            tabPageAdvert.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                        }
                        else if (media.mediatype == 2)
                        {
                            string PlayerUrl = media.content;
                            player.Visible = true;
                            player.URL = PlayerUrl;
                            this.player.Ctlcontrols.play();

                            //pictureBox1.BackgroundImage = _image;
                        }
                        Delay.Start(3000);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ERROR", "初始化客屏信息异常：" + ex.Message);
            }
        }

        private Cart CurrentCart;
        private Member CurrentMember;


        public void UpdateForm(Cart cart,Member member)
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

        private void UpdateFormExe()
        {
            try
            {               
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

                                string barcode = pro.title + "\r\n" + pro.barcode;
                                string price = "";

                                string num = "";
 
                                string total = "";
                                switch (pro.pricetagid)
                                {
                                    case 1: barcode = "会员价" + "\r\n" + pro.title + "\r\n" + pro.barcode; break;
                                    case 2: barcode = "折扣" + "\r\n" + pro.title + "\r\n" + pro.barcode; break;
                                    case 3: barcode = "直降" + "\r\n" + pro.title + "\r\n" + pro.barcode; break;
                                }

                                if (pro.price.saleprice == pro.price.originprice)
                                {
                                    price = "￥" + pro.price.saleprice.ToString();
                                }
                                else
                                {
                                    price = "￥" + pro.price.saleprice.ToString() + "(" + pro.price.salepricedesc + ")" + "\r\n" + "￥" + pro.price.originprice + "(" + pro.price.originpricedesc + ")";
                                }                       

                                if (pro.price.total == pro.price.origintotal)
                                {
                                    total = "￥" + pro.price.total.ToString();
                                }
                                else
                                {
                                    total = "￥" + pro.price.total.ToString() + "(" + pro.price.salepricedesc + ")" + "\r\n" + "￥" + pro.price.origintotal + "(" + pro.price.originpricedesc + ")";
                                }

                                dgvGood.Rows.Insert(0, new object[] { barcode, price, num, total });
                                //dgvGood.Rows.Add(barcode, price, num, total);

                                dgvGood.ClearSelection();

                            }
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
                    }
                    else
                    {
                        lblMobil.Text = "";
                        lblWechartNickName.Text = "会员登录";

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
            //player.Visible = false;
            tabControlMedia.SelectedIndex = 0;

           
           // pnlMemberCard.Visible = true;

            pnlPayInfo.Visible = true;
            lblPayInfo.Text = lblinfo;
        }

        private void frmMainMedia_SizeChanged(object sender, EventArgs e)
        {
          // asf.ControlAutoSize(this);
        }
    }
}
