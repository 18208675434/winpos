using QiandamaPOS.Common;
using QiandamaPOS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            tableLayoutPanel1.Visible = false;
            tableLayoutPanel2.Visible = false;
            pnlGoods.Visible = false;
            player.Visible = false;

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
                        Image _image = Image.FromStream(System.Net.WebRequest.Create(ImgUrl).GetResponse().GetResponseStream());

                        this.BackgroundImage = _image;
                        this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
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

        public void UpdateForm(Cart Cart,Member member)
        {

                  player.Visible = false;
                  tableLayoutPanel1.Visible = true;
                  tableLayoutPanel2.Visible = true;
                  pnlGoods.Visible = true;
 

            try
            {
                this.Invoke(new InvokeHandler(delegate()
                {

                    if (member != null&&member.memberinformationresponsevo!=null&&member.memberheaderresponsevo!=null)
                    {
                        string mobil = member.memberheaderresponsevo.mobile;
                        if (mobil.Length > 8)
                        {
                            mobil = mobil.Substring(0, mobil.Length - 8) + "****" + mobil.Substring(mobil.Length - 4);
                        }
                        lblMobil.Text = mobil;
                        lblWechartNickName.Text = member.memberinformationresponsevo.wechatnickname +"  你好！";

                    }
                    else
                    {
                        lblMobil.Text = "";
                        lblWechartNickName.Text = "会员登录";
                    }

                    pnlGoods.Controls.Clear();
                    pnlOrderDetail.Controls.Clear();
                    if (Cart != null)
                    {
                        // lblTotalPrice.Text = "￥" + cart.producttotalamt.ToString();
                        //lblPromoamt.Text = "-￥" + cart.promoamt.ToString();
                        int orderCount = Cart.orderpricedetails.Length;
                        if (orderCount == 0)
                        {
                            pnlOrderDetail.Controls.Add(pnlOrderIni);
                            pnlOrderIni.Show();
                        }
                        else
                        {
                            for (int i = 0; i < orderCount; i++)
                            {
                                frmOrderDetail frmorderdetail = new frmOrderDetail(Cart.orderpricedetails[i]);
                                frmorderdetail.TopLevel = false;
                                frmorderdetail.Width = pnlOrderDetail.Width;
                                frmorderdetail.Location = new System.Drawing.Point(0, i * frmorderdetail.Height);
                                pnlOrderDetail.Controls.Add(frmorderdetail);
                                frmorderdetail.Show();
                            }
                        }
                        lblPrice.Text = Cart.totalpayment.ToString("f2");

                        int count = Cart.products.Count;
                        lblGoodsCount.Text = count.ToString();
                        if (count == 0)
                        {
                            player.Visible = false;
                            tableLayoutPanel1.Visible = false;
                            tableLayoutPanel2.Visible = false;
                            pnlGoods.Visible = false;
                        }
                        else
                        {
                            for (int i = 0; i < count; i++)
                            {
                                frmGoodMedia frmgoodmedia = new frmGoodMedia(Cart.products[i]);
                                frmgoodmedia.TopLevel = false;

                                frmgoodmedia.frmGoodMedia_SizeChanged(null, null);
                                //frmnumber.WindowState = FormWindowState.Maximized;
                                frmgoodmedia.Location = new System.Drawing.Point(0, i * frmgoodmedia.Height);

                                pnlGoods.Controls.Add(frmgoodmedia);
                                frmgoodmedia.Width = pnlGoods.Width - 20;
                                frmgoodmedia.Show();
                            }
                        }
                    }

                }));

                Application.DoEvents();

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "客屏商品列表加载异常" + ex.Message);
                // ShowLog("更新显示列表异常" + ex.Message, false);
            }
        }

        public void ShowPayResult(object payinfo)
        {
            try
            {

                //frmCashierResultMedia frmresult = new frmCashierResultMedia(payinfo.ToString());
                //frmresult.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width, 0);

                //frmresult.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                //frmresult.Show();

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

            frmNumber frmnumber = new frmNumber("请输入会员号", true);

            frmnumber.frmNumber_SizeChanged(null, null);
            frmnumber.Size = new System.Drawing.Size(this.Width / 3, this.Height - 200);
           // frmnumber.Location = new System.Drawing.Point(this.Width - frmnumber.Width - 50, 100);

            frmnumber.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width + Screen.AllScreens[1].Bounds.Width - frmnumber.Width - 50, 100);

            //frmresult.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            frmnumber.Show();
            Application.DoEvents();


        }

        private void frmMainMedia_SizeChanged(object sender, EventArgs e)
        {
           asf.ControlAutoSize(this);
        }
    }
}
