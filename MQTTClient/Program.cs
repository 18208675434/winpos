using MQTTnet;
using MQTTnet.Core.Adapter;
using MQTTnet.Core.Diagnostics;
using MQTTnet.Core.Protocol;
using MQTTnet.Core.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace MQTTClient
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {


            //获取当前进程的一个伪句柄
            System.Diagnostics.Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();

            //获取包含当前进程的一个列表
            System.Diagnostics.Process[] processList = System.Diagnostics.Process.GetProcessesByName(currentProcess.ProcessName);

            //如果前进程已经存在
            if (processList.Length > 1)
            {
                //MessageBox.Show("自动打印程序已在运行！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //退出以下所有操作
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            //MqttNetTrace.TraceMessagePublished += MqttNetTrace_TraceMessagePublished;
            //new Thread(StartMqttServer).Start();
            Application.Run(new Form1());

            //Dictionary<string, string> dic = new Dictionary<string, string>();
            //dic.Add("ClientId", "123");
            //dic.Add("Topic", "ttt");
            //dic.Add("Value", "ggyy");
            //dic.Add("ServiceLevel", "1");
            //TopicLogic.SaveTopic(dic);


            //while (true)
            //{
            //    string result = Console.ReadLine();
            //    if (!string.IsNullOrEmpty(result))
            //    {
            //        var inputString = result.ToLower().Trim();

            //        if (inputString == "exit")
            //        {
            //            mqttServer.StopAsync();
            //            Console.WriteLine("MQTT服务已停止！");
            //            break;
            //        }
            //        else if (inputString == "clients")
            //        {
            //            foreach (var item in mqttServer.GetConnectedClients())
            //            {
            //                Console.WriteLine("客户端标识：{item.ClientId}，协议版本：{item.ProtocolVersion}");
            //            }
            //        }
            //        else
            //        {
            //            Console.WriteLine("命令[{inputString}]无效！");
            //        }
            //    }

            //}

            
        }
               private static MqttServer mqttServer = null;


        private static void StartMqttServer()
        {
            if (mqttServer == null)
            {
                try
                {
                    var options = new MqttServerOptions
                    {
                        ConnectionValidator = p =>
                        {
                            if (p.ClientId == "c001")
                            {
                                if (p.Username != "u001" || p.Password != "p001")
                                {
                                    return MqttConnectReturnCode.ConnectionRefusedBadUsernameOrPassword;
                                }
                            }

                            return MqttConnectReturnCode.ConnectionAccepted;
                        }

                    };

                    

                    mqttServer = new MqttServerFactory().CreateMqttServer(options) as MqttServer;

                    mqttServer.ApplicationMessageReceived += MqttServer_ApplicationMessageReceived;
                    mqttServer.ClientConnected += MqttServer_ClientConnected;
                    mqttServer.ClientDisconnected += MqttServer_ClientDisconnected;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
            }

            mqttServer.StartAsync();
            Console.WriteLine("MQTT服务启动成功！");
        }

        private static void MqttServer_ClientConnected(object sender, MqttClientConnectedEventArgs e)
        {
            Console.WriteLine("客户端[" + e.Client.ClientId + "{}]已连接，协议版本：{}" + e.Client.ProtocolVersion);
        }

        private static void MqttServer_ClientDisconnected(object sender, MqttClientDisconnectedEventArgs e)
        {
            Console.WriteLine("客户端[{e.Client.ClientId}]已断开连接！");
        }

        private static void MqttServer_ApplicationMessageReceived(object sender, MqttApplicationMessageReceivedEventArgs e)
        {
            Console.WriteLine("客户端[{e.ClientId}]>> 主题：{e.ApplicationMessage.Topic} 负荷：{Encoding.UTF8.GetString(e.ApplicationMessage.Payload)} Qos：{e.ApplicationMessage.QualityOfServiceLevel} 保留：{e.ApplicationMessage.Retain}");
            
        }

        private static void MqttNetTrace_TraceMessagePublished(object sender, MqttNetTraceMessagePublishedEventArgs e)
        {
            /*Console.WriteLine($">> 线程ID：{e.ThreadId} 来源：{e.Source} 跟踪级别：{e.Level} 消息: {e.Message}");

            if (e.Exception != null)
            {
                Console.WriteLine(e.Exception);
            }*/
        }
    }
}
