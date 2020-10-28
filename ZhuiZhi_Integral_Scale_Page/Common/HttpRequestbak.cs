using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Security;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Common
{
    public class HttpRequestbak
    {
        public delegate string deleteHttpGet(string Url, SortedDictionary<string, string> sortpara);

        public bool WhetherGetOK = false;
        public string GetJson = "";

        public string HttpGetJson(string Url, SortedDictionary<string, string> sortpara)
        {
            try
            {

                if (!IsConnectInternet())
                {
                    return "";
                }

                GetJson = "";
                WhetherGetOK = false;
                deleteHttpGet operation = new deleteHttpGet(HttpGET);

                operation.BeginInvoke(Url, sortpara, new System.AsyncCallback(GetCallbackHandler), "Async parameter");

                //接口访问完毕或 超时60
                long beginTime = DateTime.Now.Ticks;
                while (!WhetherGetOK)
                {
                    long endTime = DateTime.Now.Ticks;

                    TimeSpan elapsedSpan = new TimeSpan(endTime - beginTime);

                    if ((elapsedSpan.TotalMilliseconds) > 60000 || WhetherGetOK)
                        break;

                    Delay.Start(20);
                }

                return GetJson;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("HttpGetJson" + ex.Message);
                return "";
            }
        }

        public void GetCallbackHandler(IAsyncResult iar)
        {
            AsyncResult ar = (AsyncResult)iar;
            // 获取原委托对象。
            deleteHttpGet operation = (deleteHttpGet)ar.AsyncDelegate;
            // 结束委托调用。
            string json = operation.EndInvoke(iar);
            WhetherGetOK = true;
            GetJson = json;
        }


        public delegate string deleteHttpPost(string Url, string bodyjson);
        public bool WhetherPostOK = false;
        public string PostJson = "";

        public string HttpPostJson(string Url, string bodyjson)
        {
            try
            {

               
                if (!IsConnectInternet())
                {
                    return "";
                }
                PostJson = "";
                WhetherPostOK = false;
                deleteHttpPost operation = new deleteHttpPost(HttpPOST);

                operation.BeginInvoke(Url, bodyjson, new System.AsyncCallback(PostGetCallbackHandler), "Async parameter");

                //接口访问完毕或 超时60
                long beginTime = DateTime.Now.Ticks;
                while (!WhetherPostOK)
                {
                    long endTime = DateTime.Now.Ticks;

                    TimeSpan elapsedSpan = new TimeSpan(endTime - beginTime);

                    if ((elapsedSpan.TotalMilliseconds) > 60000 || WhetherPostOK)
                        break;

                    Delay.Start(20);
                }

                return PostJson;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("HttpPostJson" + ex.Message);
                return "";
            }
        }

        public void PostGetCallbackHandler(IAsyncResult iar)
        {
            AsyncResult ar = (AsyncResult)iar;
            // 获取原委托对象。
            deleteHttpPost operation = (deleteHttpPost)ar.AsyncDelegate;
            // 结束委托调用。
            PostJson = operation.EndInvoke(iar);
            WhetherPostOK = true;
        }

        #region  访问服务端


        //private static MyHttp myhtttp = new MyHttp();
        //public string HttpGET(string Url, SortedDictionary<string, string> sortpara)
        //{

        //    //System.GC.Collect();

        //    string retString = "";

        //    string Timestamp = MainModel.getStampByDateTime(DateTime.Now);
        //    string nonce = MainModel.getNonce();

        //    sortpara.Add("nonce", nonce);
        //    string signstr = MainModel.PrivateKey;
        //    foreach (KeyValuePair<string, string> keyvalue in sortpara)
        //    {
        //        signstr += keyvalue.Key + keyvalue.Value;
        //    }
        //    signstr += Timestamp;

        //    string postDataStr = "sign=" + MainModel.GetMD5(signstr);

        //    Url = MainModel.URL + Url + "?";
        //    foreach (KeyValuePair<string, string> keyvalue in sortpara)
        //    {
        //        Url += keyvalue.Key + "=" + keyvalue.Value + "&";
        //    }
        //    Url += postDataStr;

        //    Response respon = MyHttp.HTTP("GET", Url, "", "");


        //    retString = respon.HTTPResponseText;

        //    return retString;
        //}


        //public string HttpPOST(string Url, string bodyjson)
        //{
        //    string retString = "";
        //    try
        //    {


        //        string Timestamp = MainModel.getStampByDateTime(DateTime.Now);
        //        string nonce = MainModel.getNonce();
        //        string signstr = MainModel.PrivateKey + "nonce" + nonce + Timestamp + bodyjson;
        //        string postDataStr = "nonce=" + nonce + "&sign=" + MainModel.GetMD5(signstr);

        //        Url = MainModel.URL + Url + "?" + postDataStr;


        //        Response respon = MyHttp.HTTP("POST", Url, "", bodyjson);


        //        retString = respon.HTTPResponseText;

        //    }
        //    catch (WebException ex)
        //    {
        //        LogManager.WriteLog("访问服务器出错:" + ex.Message + ex.StackTrace);
        //    }

        //    return retString;
        //}


        #region  （旧  不兼容XP）
        public string HttpGET(string Url, SortedDictionary<string, string> sortpara)
        {

            //System.GC.Collect();

            string retString = "";

            string Timestamp = MainModel.getStampByDateTime(DateTime.Now);
            string nonce = MainModel.getNonce();

            sortpara.Add("nonce", nonce);
            //string signstr = "kVl55eO1n3DZhWC8Z7" + "devicesn" + devicesn + "nonce" + nonce + Timestamp;
            string signstr = MainModel.PrivateKey;
            foreach (KeyValuePair<string, string> keyvalue in sortpara)
            {
                signstr += keyvalue.Key + keyvalue.Value;
            }
            signstr += Timestamp;

            //string body = "{\"devicesn\":\"" + devicesn + "\"}";

            string postDataStr = "sign=" + MainModel.GetMD5(signstr);
            //Url += "?" + "devicesn=" + devicesn + "&" + postDataStr;

            Url = MainModel.URL + Url + "?";
            foreach (KeyValuePair<string, string> keyvalue in sortpara)
            {
                Url += keyvalue.Key + "=" + keyvalue.Value + "&";
            }
            Url += postDataStr;
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);

                request.Timeout = 60 * 1000;

                System.Net.ServicePointManager.Expect100Continue = false;

                if (Url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    request = WebRequest.Create(new Uri(Url)) as HttpWebRequest;
                    request.ProtocolVersion = HttpVersion.Version10;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;
                }
                else
                {
                    request = WebRequest.Create(new Uri(Url)) as HttpWebRequest;
                }


                request.Method = "GET";

                ////请求头
                request.Headers.Add("v", MainModel.Version.Replace(".",""));
                request.Headers.Add("X-ZZ-Device-Sn", MainModel.DeviceSN);
                request.Headers.Add("POS-Authorization", MainModel.Authorization);
                request.Headers.Add("X-ZZ-Timestamp", Timestamp);

                request.ContentType = "application/json;charset=UTF-8";

                request.KeepAlive = false;
                request.UseDefaultCredentials = true;
                request.ServicePoint.Expect100Continue = false;//important


                System.Net.ServicePointManager.DefaultConnectionLimit = 100;
                request.Timeout = 60 * 1000; //3秒钟无响应 网络有问题

                //ParameterizedThreadStart Pts = new ParameterizedThreadStart(GetResponseResult);
                //Thread thread = new Thread(Pts);
                //thread.IsBackground = true;
                //thread.Start(request);

                //    while (thread.IsAlive)
                //    {
                //        Delay.Start(10);
                //    }

                //int id = thread.ManagedThreadId;

                //try
                //{
                //    retString = dicresult[id];
                //    dicresult.Remove(id);
                //}
                //catch (Exception ex)
                //{
                //    LogManager.WriteLog("线程返回异常" + ex.Message);
                //}



                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    request.Timeout = 60 * 1000;
                    Stream myResponseStream = response.GetResponseStream();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                    retString = myStreamReader.ReadToEnd();

                    myStreamReader.Close();
                    myResponseStream.Close();

                    try
                    {
                        response.Close();
                    }
                    catch { }
                }
                try
                {
                    request.Abort();
                }
                catch { }

            }
            catch (WebException ex)
            {
                //MessageBox.Show(ex.StackTrace);
                LogManager.WriteLog("访问服务器出错:" + ex.Message);
                MainModel.ShowLog("访问服务器出错,请检查网络连接！", false);

            }

            return retString;
        }





        public string HttpPOST(string Url, string bodyjson)
        {

            string retString = "";

            string Timestamp = MainModel.getStampByDateTime(DateTime.Now);
            string nonce = MainModel.getNonce();
            string signstr = MainModel.PrivateKey + "nonce" + nonce + Timestamp + bodyjson;
            string postDataStr = "nonce=" + nonce + "&sign=" + MainModel.GetMD5(signstr);

            Url = MainModel.URL + Url + "?" + postDataStr;
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);

                System.Net.ServicePointManager.Expect100Continue = false;

                if (Url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    request = WebRequest.Create(new Uri(Url)) as HttpWebRequest;
                    request.ProtocolVersion = HttpVersion.Version10;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;
                }
                else
                {
                    request = WebRequest.Create(new Uri(Url)) as HttpWebRequest;
                }

                request.Method = "POST";

                ////请求头
                request.Headers.Add("v", MainModel.Version.Replace(".", ""));
                request.Headers.Add("X-ZZ-Device-Sn", MainModel.DeviceSN);
                request.Headers.Add("POS-Authorization", MainModel.Authorization);
                request.Headers.Add("X-ZZ-Timestamp", Timestamp);


                byte[] by = Encoding.GetEncoding("utf-8").GetBytes(bodyjson);   //请求参数转码
                request.ContentType = "application/json;charset=UTF-8";
                request.Timeout = 1000 * 60;
                request.ContentLength = by.Length;

                Stream stw = request.GetRequestStream();     //获取绑定相应流
                stw.Write(by, 0, by.Length);      //写入流
                stw.Close();    //关闭流

                System.Net.ServicePointManager.DefaultConnectionLimit = 100;


                request.Timeout = 60 * 1000;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    request.Timeout = 60 * 1000;
                    Stream myResponseStream = response.GetResponseStream();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                    retString = myStreamReader.ReadToEnd();

                    myStreamReader.Close();
                    myResponseStream.Close();

                    try
                    {
                        response.Close();
                    }
                    catch { }
                }


                try
                {
                    request.Abort();
                }
                catch { }

            }
            catch (WebException ex)
            {
                LogManager.WriteLog("访问服务器出错:" + ex.Message);
            }

            return retString;
        }

        internal bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            // 总是接受  
            return true;
        }

        //检测IP连接
        bool CheckNet()
        {
            return true;
            bool var = false;

            try
            {
                string ip = "www.baidu.com";
                Ping pingSender = new Ping();

                PingOptions pingOption = new PingOptions();
                pingOption.DontFragment = true;
                string data = "0";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 1000;
                PingReply reply = pingSender.Send(ip, timeout, buffer);
                if (reply.Status == IPStatus.Success)
                    var = true;
                else
                    var = false;
            }
            catch (Exception ex)
            {

                return true;
                 //ShowLog("无法检测网络连接是否正常-" + ex.Message, true);
            }

            return var;
        }

        private void GetResponseResult(object obj)
        {
            try
            {
                WebRequest request = (WebRequest)obj;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    request.Timeout = 60 * 1000;
                    Stream myResponseStream = response.GetResponseStream();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                    string result = myStreamReader.ReadToEnd();

                    myStreamReader.Close();
                    myResponseStream.Close();

                    try
                    {
                        response.Close();
                    }
                    catch { }

                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("接口访问异常" + ex.Message + ex.StackTrace);
            }
        }

#endregion

        [System.Runtime.InteropServices.DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(int Description, int ReservedValue);

        /// <summary>
        /// 用于检查网络是否可以连接互联网,true表示连接成功,false表示连接失败 
        /// </summary>
        /// <returns></returns>
        public static bool IsConnectInternet()
        {
            int Description = 0;
            return InternetGetConnectedState(Description, 0);
        }

        #endregion
    }
}
