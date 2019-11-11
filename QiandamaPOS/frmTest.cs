using Newtonsoft.Json;
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
using System.Windows.Forms;

namespace QiandamaPOS
{
    public partial class frmTest : Form
    {
        public frmTest()
        {
            InitializeComponent();
        }
        HttpUtil httputil = new HttpUtil();
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "/pos/account/sysuser/item/user";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();

                string json = httputil.HttpGET(url, sort);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    string data = rd.data.ToString();
                    userModel currentuser = JsonConvert.DeserializeObject<userModel>(rd.data.ToString());
                }
                else
                {
                    // return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取用户信息异常：" + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "/pos/product/pos/getdeviceshopinfovo";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                sort.Add("devicesn", MainModel.DeviceSN);

                string json = httputil.HttpGET(url, sort);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    DeviceShopInfo deviceshop = JsonConvert.DeserializeObject<DeviceShopInfo>(rd.data.ToString());
                    string test = deviceshop.address;
                }
                else
                {
                    // return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取门店信息异常：" + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "/pos/account/signin";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                sort.Add("username", "18101891345");
                sort.Add("password", "666666");

                string testjson = JsonConvert.SerializeObject(sort);

                string json = httputil.HttpPOST(url, testjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {

                    string data = rd.data.ToString();

                    INIManager.SetIni("System", "POS-Authorization", data, MainModel.IniPath);

                    MainModel.DeviceSN = data;

                }
                else
                {
                    // return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "验证用户信息异常：" + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "/pos/product/scancode/getscancodeskumemberdto";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                sort.Add("shopid", MainModel.CurrentShopInfo.shopid);
                sort.Add("barcode", "2500033001008");

                string json = httputil.HttpGET(url, sort);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                txtMsg.Text = rd.data.ToString();
                if (rd.code == 0)
                {
                    scancodememberModel scancode = JsonConvert.DeserializeObject<scancodememberModel>(rd.data.ToString());
                    string test = scancode.skubarcodeflag.ToString();

                    //string data = rd.data.ToString();

                    //INIManager.SetIni("System", "DeviceSN", data, MainModel.IniPath);

                    //MainModel.DeviceSN = data;

                }
                else
                {
                    // return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "条码或会员识别异常：" + ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "/pos/product/pos/getposmedialistwithoutlogin";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();


                string json = httputil.HttpGET(url, sort);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                string jsonkk = rd.data.ToString();

                if (rd.code == 0)
                {
                    MediaList media = JsonConvert.DeserializeObject<MediaList>(rd.data.ToString());
                    string test = media.img[0];
                }
                else
                {
                    // return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "条码或会员识别异常：" + ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

            try
            {
                string url = "/pos/order/pos/cart";


                product pro = new product();
                pro.skucode = "20000332";
                pro.num = 1;
                pro.specnum="0.1";
                pro.spectype = 2;
                pro.goodstagid = 1;
                pro.barcode = "2500033001008";

                product[] lstpro=new product[1];
                lstpro[0] = pro;

                CartPara cart = new CartPara();
                cart.ordersubtype = "pos";
                cart.products = lstpro;
                cart.shopid = MainModel.CurrentShopInfo.shopid;

                string tempjson = JsonConvert.SerializeObject(cart);

                string json = httputil.HttpPOST(url, tempjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                txtMsg.Text = rd.data.ToString();
               // return;
                if (rd.code == 0)
                {
                    Cart carttemp = JsonConvert.DeserializeObject<Cart>(rd.data.ToString());
                    string test = carttemp.title;
                   
                }
                else
                {
                    // return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "条码或会员识别异常：" + ex.Message);
            }
        }



        private void button7_Click(object sender, EventArgs e)
        {

            Image _image = Image.FromStream(System.Net.WebRequest.Create("https://pic.qdama.cn/Fjxb0w7cS11yuWKLt5vMBGN-O2Yq").GetResponse().GetResponseStream());


            pictureBox1.BackgroundImage = _image;

        //System.Net.WebClient wc = new System.Net.WebClient();
        //wc.DownloadFile("https://pic.qdama.cn/Fjxb0w7cS11yuWKLt5vMBGN-O2Yq", "D:\temp.png");
        ////wc.Dispose();
        ////这下图片就下载到本地了。
        ////然后你在你的Winform里加个picbox控件:
        //pictureBox1.Image = Image.FromFile( "D:\temp.png");
            
        }

        private void button8_Click(object sender, EventArgs e)
        {

            try
            {
                string url = "/pos/order/pos/create";


                product pro = new product();
                pro.skucode = "20000332";
                pro.num = 1;
                pro.specnum = "0.1";
                pro.spectype = 2;
                pro.goodstagid = 1;
                pro.barcode = "2500033001008";

                product[] lstpro = new product[1];
                lstpro[0] = pro;

                CreateOrderPara order = new CreateOrderPara();

                order.ordersubtype = "pos";
                order.products = lstpro;
                order.pricetotal =(decimal) 0.00;
                order.shopid = MainModel.CurrentShopInfo.shopid;
                order.orderplaceid = "145122450751889408";
                order.cashpayoption = 1;
                order.cashpayamt = (decimal)10;
                string tempjson = JsonConvert.SerializeObject(order);

                string json = httputil.HttpPOST(url, tempjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                txtMsg.Text = rd.data.ToString();
                // return;
                if (rd.code == 0)
                {
                    CreateOrderResult cr = JsonConvert.DeserializeObject<CreateOrderResult>(rd.data.ToString());

                }
                else
                {
                    // return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "创建订单异常：" + ex.Message);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {

            try
            {
                string url = "/pos/order/pos/querystatus";

                string orderid = "8150660100075090";

                string tempjson = JsonConvert.SerializeObject(orderid);
                tempjson = "{\"orderid\":\"" + orderid + "\"}";
                string json = httputil.HttpPOST(url, tempjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                txtMsg.Text = rd.data.ToString();
                // return;
                if (rd.code == 0)
                {
                    string testresult = rd.data.ToString();

                }
                else
                {
                    // return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "创建订单异常：" + ex.Message);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "/pos/order/pos/detail";

                string orderid = "8150660100075090";

                string tempjson = JsonConvert.SerializeObject(orderid);
                tempjson = "{\"orderid\":\"" + orderid + "\"}";
                string json = httputil.HttpPOST(url, tempjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                txtMsg.Text = rd.data.ToString();
                // return;
                if (rd.code == 0)
                {
                    string testresult = rd.data.ToString();

                }
                else
                {
                    // return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "创建订单异常：" + ex.Message);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {

            player.Visible = false;
            player.Visible = true;

            //WMPLib.IWMPMedia mediaInfo = player.newMedia(txtPlayerURL.Text);


            //MessageBox.Show(mediaInfo.duration.ToString());

            this.player.URL = txtPlayerURL.Text;
            //player.fullScreen = true;
            player.uiMode = "full";
            this.player.Ctlcontrols.play();

           
            
            while (!player.status.Contains("停止"))
            {
                
                Delay.Start(100);
            }
            player.close();
          //  MessageBox.Show("播放完成");
          

        }

        private void player_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
           // player.statu
        }

        private void player_StatusChange(object sender, EventArgs e)
        {

            if (player.status.Contains("正在播放"))
            {
                player.fullScreen = true;
            }
            txtMsg.Text += DateTime.Now.ToString("ssfff") + player.status + "\r\n";
        }

    }
}
