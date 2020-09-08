using Maticsoft.BLL;
using Maticsoft.Model;
using MQTTnet;
using MQTTnet.Core;
using MQTTnet.Core.Client;
using MQTTnet.Core.Packets;
using MQTTnet.Core.Protocol;
using MQTTnet.Core.Serializer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MQTTClient
{
    public partial class Form1 : Form
    {

        private MqttClient mqttClient = null;

        private JSON_BEANBLL jsonbll = new JSON_BEANBLL();
        private DBPROMOTION_CACHE_BEANBLL promotionbll = new DBPROMOTION_CACHE_BEANBLL();

        private DeviceShopInfo CurrentShopInfo;
        public Form1()
        {
            InitializeComponent();

           // Task.Run(async () => { await ConnectMqttServerAsync(); });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Hide();
            Application.DoEvents();

            Task.Run(async () => { await ConnectMqttServerAsync(); });
        }
        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <returns></returns>
        private async Task ConnectMqttServerAsync()
        {
            if (mqttClient == null) ;
            {
                mqttClient = new MqttClientFactory().CreateMqttClient() as MqttClient;
                mqttClient.ApplicationMessageReceived += MqttClient_ApplicationMessageReceived;
                mqttClient.Connected += MqttClient_Connected;
                mqttClient.Disconnected += MqttClient_Disconnected;                
            }

            try
            {
                string Server = INIManager.GetIni("MQTT", "Server", MainModel.IniPath);
                int Port =Convert.ToInt32(INIManager.GetIni("MQTT", "Port", MainModel.IniPath));
                string UserName = INIManager.GetIni("MQTT", "UserName", MainModel.IniPath);
                string PassWord = INIManager.GetIni("MQTT", "PassWord", MainModel.IniPath);
                string ClientId="";

                JSON_BEANMODEL jsonmodel = jsonbll.GetModel("SHOPINFO");
                if (jsonmodel != null && jsonmodel.JSON != null)
                {
                    CurrentShopInfo= JsonConvert.DeserializeObject<DeviceShopInfo>(jsonmodel.JSON);

                    ClientId = CurrentShopInfo.tenantid + "_" + CurrentShopInfo.shopid + "_" + CurrentShopInfo.deviceid;
                }
                else
                {
                    LogManager.WriteLog("未获取门店信息MQTT服务未开启");
                    return;
                }

                var options = new MqttClientTcpOptions
                {
                    Server = Server,
                    Port = Port,
                    ClientId = ClientId,
                    UserName = UserName,
                    Password = PassWord,
                    CleanSession=true,
                    KeepAlivePeriod=TimeSpan.FromSeconds(10),
                    DefaultCommunicationTimeout=TimeSpan.FromSeconds(10),
                };
                await mqttClient.ConnectAsync(options);
            
            }
            catch (Exception ex)
            {
                Invoke((new Action(() =>
                {
                   // txtReceiveMessage.AppendText("连接到MQTT服务器失败！" + Environment.NewLine + ex.Message + Environment.NewLine);
                    LogManager.WriteLog("MQTT", "连接到MQTT服务器失败！" + Environment.NewLine + ex.Message);
                })));
            }
        }

        /// <summary>
        /// 服务器连接成功
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MqttClient_Connected(object sender, EventArgs e)
        {
            Invoke((new Action(() =>
            {
                LogManager.WriteLog("MQTT","已连接到MQTT服务器！");
               // txtReceiveMessage.AppendText("已连接到MQTT服务器！" + Environment.NewLine);

                string topicpromo = "promo:change:" + CurrentShopInfo.tenantid + ":" + CurrentShopInfo.shopid;
                SubScribe(topicpromo);

                string topicprice = "sku:adjust:increment:" + CurrentShopInfo.tenantid + ":" + CurrentShopInfo.shopid;
                SubScribe(topicprice);
            })));
        }

        /// <summary>
        /// 断开服务器连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MqttClient_Disconnected(object sender, EventArgs e)
        {
            Invoke((new Action(() =>
            {
               // txtReceiveMessage.AppendText("已断开MQTT连接！" + Environment.NewLine);
                LogManager.WriteLog("MQTT", "已断开MQTT连接！");
            })));
        }

        /// <summary>
        /// 接收到消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MqttClient_ApplicationMessageReceived(object sender, MqttApplicationMessageReceivedEventArgs e)
        {
            Invoke((new Action(() =>
            {
                try
                {                   
                    string json = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

                    LogManager.WriteLog("MQTT", "接收到消息" + e.ApplicationMessage.Topic+"  " + json);

                    if (e.ApplicationMessage.Topic == "promo:change:" + CurrentShopInfo.tenantid + ":" + CurrentShopInfo.shopid)
                    {
                       INIManager.SetIni("MQTT", "ChangeType", "3",MainModel.IniPath);
                    }
                    else if (e.ApplicationMessage.Topic == "sku:adjust:increment:" + CurrentShopInfo.tenantid + ":" + CurrentShopInfo.shopid)
                    {
                        AdjustTypes type = JsonConvert.DeserializeObject<AdjustTypes>(json);

                        if (type != null)
                        {
                            INIManager.SetIni("MQTT", "ChangeType", type.adjustTypes.ToString(), MainModel.IniPath);
                        }
                    }

                    //DBPROMOTION_CACHE_BEANMODEL promotion = JsonConvert.DeserializeObject<DBPROMOTION_CACHE_BEANMODEL>(json);

                    //if (promotion != null)
                    //{

                    //    LogManager.WriteLog("MQTT", "接收到商品变更" + promotion.CODE);

                    //    INIManager.SetIni("MQTT", "IsChange","1", MainModel.IniPath);
                    //    if (promotionbll.ExistsByCode(promotion.CODE))
                    //    {
                    //        promotion.TENANTID = CurrentShopInfo.tenantid;
                    //        promotion.SHOPID = CurrentShopInfo.shopid;
                    //        promotion.CREATE_URL_IP = INIManager.GetIni("System", "URL", MainModel.IniPath);
                    //        promotionbll.UpdateByCode(promotion);
                    //    }
                    //    else
                    //    {
                    //        promotion.TENANTID = CurrentShopInfo.tenantid;
                    //        promotion.SHOPID = CurrentShopInfo.shopid;
                    //        promotion.CREATE_URL_IP = INIManager.GetIni("System", "URL", MainModel.IniPath);
                    //        promotionbll.Add(promotion);
                    //    }
                    //}
                    //else
                    //{
                    //    LogManager.WriteLog("MQTT", "接收到不正确信息" + json);
                    //}
                   
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog("MQTT","更新MQTT促销异常" + ex.Message + ex.StackTrace);
                }
                //txtReceiveMessage.AppendText( Encoding.UTF8.GetString(e.ApplicationMessage.Payload)+Environment.NewLine);
            })));
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSubscribe_ClickAsync(object sender, EventArgs e)
        {
            SubScribe(txtSubTopic.Text.Trim());
        }

        private void SubScribe(string topic)
        {
            try
            {
                if (string.IsNullOrEmpty(topic))
                {
                    //MessageBox.Show("订阅主题不能为空！");
                    return;
                }

                if (!mqttClient.IsConnected)
                {
                    //MessageBox.Show("MQTT客户端尚未连接！");
                    return;
                }

                mqttClient.SubscribeAsync(new List<TopicFilter> {
                new TopicFilter(topic, MqttQualityOfServiceLevel.AtMostOnce)
            });

                LogManager.WriteLog("MQTT", "已订阅[" + topic + "]主题");
                //txtReceiveMessage.AppendText("已订阅["+topic+"]主题" + Environment.NewLine);
                txtSubTopic.Enabled = false;
                btnSubscribe.Enabled = false;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("MQTT","订阅消息异常"+ex.Message);
            }
        }

        /// <summary>
        /// 发布主题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPublish_Click(object sender, EventArgs e)
        {
            string topic = txtPubTopic.Text.Trim();

            if (string.IsNullOrEmpty(topic))
            {
               // MessageBox.Show("发布主题不能为空！");
                return;
            }

            string inputString = txtSendMessage.Text.Trim();
            var appMsg = new MqttApplicationMessage(topic, Encoding.UTF8.GetBytes(inputString), MqttQualityOfServiceLevel.AtMostOnce, false);
            mqttClient.PublishAsync(appMsg);
        }

        private void FmMqttClient_Load(object sender, EventArgs e)
        {
           // button1_Click(null,null);
            //Dictionary<string, string> dic = new Dictionary<string, string>();
            //dic.Add("ClientId", "123");
            //dic.Add("Topic", "ttt");
            //dic.Add("Value", "ggyy");
            //dic.Add("ServiceLevel", "1");
            //TopicLogic.SaveTopic(dic);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string json = "{\"allowance\":\"00\",\"canBeCombined\":false,\"canMixCoupon\":true,\"code\":\"206362169292365824\",\"costCenterInfo\":\"{\\\"platform\\\":1.00}\",\"createdAt\":1583462975181,\"createdBy\":\"3401\",\"description\":\"测试店满额+所有商品\",\"districtScope\":\"\",\"eligibilityCondition\":\"{\\\"catalogsToInclude\\\":[],\\\"realmType\\\":\\\"all\\\",\\\"skuCodesToInclude\\\":[]}\",\"enabled\":true,\"enabledFrom\":1583424000000,\"enabledTimeInfo\":\"{\\\"endTime1\\\":\\\"\\\",\\\"startTime1\\\":\\\"\\\"}\",\"enabledTo\":1588176000000,\"fromOuter\":false,\"name\":\"测试店满额+所有商品\",\"orderSubType\":8,\"outerCode\":\"\",\"promoAction\":\"step.amount.off\",\"promoActionContext\":\"10.2:1;14.4:2;19.9:3;20.3:4\",\"promoConditionContext\":\"10.20\",\"promoConditionType\":\"order.amount.threshold\",\"promoSubType\":\"reduction\",\"promoType\":\"O\",\"rank\":500,\"saleChannel\":4095,\"shopScope\":\"508888\",\"tag\":\"step-reduction\",\"tenantScope\":\"0210000001\",\"updatedAt\":1583479904366,\"updatedBy\":\"3401\"}";
               
                DBPROMOTION_CACHE_BEANMODEL promotion = JsonConvert.DeserializeObject<DBPROMOTION_CACHE_BEANMODEL>(json);

                //if (promotionbll.ExistsByCode(promotion.CODE))
                //{
                //    promotion.TENANTID = CurrentShopInfo.tenantid;
                //    promotion.SHOPID = CurrentShopInfo.shopid;
                //    promotion.CREATE_URL_IP = INIManager.GetIni("System", "URL", MainModel.IniPath);
                //    promotionbll.UpdateByCode(promotion);
                //}
                //else
                //{
                //    promotion.TENANTID = CurrentShopInfo.tenantid;
                //    promotion.SHOPID = CurrentShopInfo.shopid;
                //    promotion.CREATE_URL_IP = INIManager.GetIni("System", "URL", MainModel.IniPath);
                //    promotionbll.Add(promotion);
                //}


            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.StackTrace);
            }
        }

    }
}
