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


    //public class HttpRequest
    //{
    //    public delegate string deleteHttpGet(string Url, SortedDictionary<string, string> sortpara);

    //    public bool WhetherGetOK = false;
    //    public string GetJson = "";


    //    Dictionary<string, string> dicResult = new Dictionary<string, string>();

    //    public string HttpGetJson(string Url, SortedDictionary<string, string> sortpara)
    //    {
    //        try
    //        {

    //            if (!IsConnectInternet())
    //            {
    //                return "";
    //            }

    //            AsyRequetCallback testback = new AsyRequetCallback(TestBack);


    //            string Timestamp = MainModel.getStampByDateTime(DateTime.Now);
    //            string nonce = MainModel.getNonce();

    //            sortpara.Add("nonce", nonce);
    //            //string signstr = "kVl55eO1n3DZhWC8Z7" + "devicesn" + devicesn + "nonce" + nonce + Timestamp;
    //            string signstr = MainModel.PrivateKey;
    //            foreach (KeyValuePair<string, string> keyvalue in sortpara)
    //            {
    //                signstr += keyvalue.Key + keyvalue.Value;
    //            }
    //            signstr += Timestamp;


    //            string postDataStr = "sign=" + MainModel.GetMD5(signstr);

    //            Url = MainModel.URL + Url + "?";
    //            foreach (KeyValuePair<string, string> keyvalue in sortpara)
    //            {
    //                Url += keyvalue.Key + "=" + keyvalue.Value + "&";
    //            }
    //            Url += postDataStr;

    //            //异步执行防止共用变量影响
    //            string thiscode = Guid.NewGuid().ToString();

    //            HttpAsyncRequest(Url, "GET", Timestamp, testback, thiscode, postDataStr);


    //            //接口访问完毕或 超时60
    //            long beginTime = DateTime.Now.Ticks;
    //            while (!WhetherGetOK)
    //            {
    //                long endTime = DateTime.Now.Ticks;

    //                TimeSpan elapsedSpan = new TimeSpan(endTime - beginTime);

    //                if ((elapsedSpan.TotalMilliseconds) > 60000 || dicResult.Keys.Contains(thiscode))
    //                    break;

    //                Delay.Start(20);
    //            }

    //            if (dicResult.Keys.Contains(thiscode))
    //            {
    //                string result = dicResult[thiscode];
    //                dicResult.Remove(thiscode);
    //                return result;
    //            }
    //            else
    //            {
    //                return "";
    //            }
    //            //return GetJson;
    //        }
    //        catch (Exception ex)
    //        {
    //            LogManager.WriteLog("HttpGetJson" + ex.Message);
    //            return "";
    //        }
    //    }

    //    public void GetCallbackHandler(IAsyncResult iar)
    //    {
    //        AsyncResult ar = (AsyncResult)iar;
    //        // 获取原委托对象。
    //        deleteHttpGet operation = (deleteHttpGet)ar.AsyncDelegate;
    //        // 结束委托调用。
    //        string json = operation.EndInvoke(iar);
    //        WhetherGetOK = true;
    //        GetJson = json;
    //    }


    //    public delegate string deleteHttpPost(string Url, string bodyjson);
    //    public bool WhetherPostOK = false;
    //    public string PostJson = "";

    //    public string HttpPostJson(string Url, string bodyjson)
    //    {
    //        try
    //        {

    //            if (!IsConnectInternet())
    //            {
    //                return "";
    //            }

    //            AsyRequetCallback testback = new AsyRequetCallback(TestBack);

    //            string Timestamp = MainModel.getStampByDateTime(DateTime.Now);
    //            string nonce = MainModel.getNonce();

    //            string signstr = MainModel.PrivateKey + "nonce" + nonce + Timestamp + bodyjson;
    //            string postDataStr = "nonce=" + nonce + "&sign=" + MainModel.GetMD5(signstr);

    //            Url = MainModel.URL + Url + "?" + postDataStr;


    //            //异步执行防止共用变量影响
    //            string thiscode = Guid.NewGuid().ToString();
    //            HttpAsyncRequest(Url, "POST", Timestamp, testback, thiscode, bodyjson);


    //            //接口访问完毕或 超时60
    //            long beginTime = DateTime.Now.Ticks;
    //            while (!WhetherGetOK)
    //            {
    //                long endTime = DateTime.Now.Ticks;

    //                TimeSpan elapsedSpan = new TimeSpan(endTime - beginTime);

    //                if ((elapsedSpan.TotalMilliseconds) > 60000 || dicResult.Keys.Contains(thiscode))
    //                    break;

    //                Delay.Start(20);
    //            }

    //            if (dicResult.Keys.Contains(thiscode))
    //            {
    //                string result = dicResult[thiscode];
    //                dicResult.Remove(thiscode);
    //                return result;
    //            }
    //            else
    //            {
    //                return "";
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            LogManager.WriteLog("HttpPostJson" + ex.Message);
    //            return "";
    //        }
    //    }

    //    public string HttpPostJson(string Url, SortedDictionary<string, string> sortpara, string bodyjson)
    //    {
    //        try
    //        {

    //            if (!IsConnectInternet())
    //            {
    //                return "";
    //            }
                
    //            AsyRequetCallback testback = new AsyRequetCallback(TestBack);

    //            string Timestamp = MainModel.getStampByDateTime(DateTime.Now);
    //            string nonce = MainModel.getNonce();

    //            string signstr = MainModel.PrivateKey;
    //            foreach (KeyValuePair<string, string> keyvalue in sortpara)//在nonce前加入get参数进行签名
    //            {
    //                signstr += keyvalue.Key + keyvalue.Value;
    //            }
    //            signstr = signstr + "nonce" + nonce + Timestamp + bodyjson;
    //            string postDataStr = "nonce=" + nonce + "&sign=" + MainModel.GetMD5(signstr);

    //            Url = MainModel.URL + Url + "?" ;
    //            foreach (KeyValuePair<string, string> keyvalue in sortpara)//post请求加入get参数
    //            {
    //                Url += keyvalue.Key + "=" + keyvalue.Value + "&";
    //            }
    //            Url = Url + postDataStr;

    //            //异步执行防止共用变量影响
    //            string thiscode = Guid.NewGuid().ToString();
    //            HttpAsyncRequest(Url, "POST", Timestamp, testback, thiscode, bodyjson);


    //            //接口访问完毕或 超时60
    //            long beginTime = DateTime.Now.Ticks;
    //            while (!WhetherGetOK)
    //            {
    //                long endTime = DateTime.Now.Ticks;

    //                TimeSpan elapsedSpan = new TimeSpan(endTime - beginTime);

    //                if ((elapsedSpan.TotalMilliseconds) > 60000 || dicResult.Keys.Contains(thiscode))
    //                    break;

    //                Delay.Start(20);
    //            }

    //            if (dicResult.Keys.Contains(thiscode))
    //            {
    //                string result = dicResult[thiscode];
    //                dicResult.Remove(thiscode);
    //                return result;
    //            }
    //            else
    //            {
    //                return "";
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            LogManager.WriteLog("HttpPostJson" + ex.Message);
    //            return "";
    //        }
    //    }

    //    public void PostGetCallbackHandler(IAsyncResult iar)
    //    {
    //        AsyncResult ar = (AsyncResult)iar;
    //        // 获取原委托对象。
    //        deleteHttpPost operation = (deleteHttpPost)ar.AsyncDelegate;
    //        // 结束委托调用。
    //        PostJson = operation.EndInvoke(iar);
    //        WhetherPostOK = true;
    //    }

    //    #region  访问服务端


    //    //private static MyHttp myhtttp = new MyHttp();
    //    //public string HttpGET(string Url, SortedDictionary<string, string> sortpara)
    //    //{

    //    //    //System.GC.Collect();

    //    //    string retString = "";

    //    //    string Timestamp = MainModel.getStampByDateTime(DateTime.Now);
    //    //    string nonce = MainModel.getNonce();

    //    //    sortpara.Add("nonce", nonce);
    //    //    string signstr = MainModel.PrivateKey;
    //    //    foreach (KeyValuePair<string, string> keyvalue in sortpara)
    //    //    {
    //    //        signstr += keyvalue.Key + keyvalue.Value;
    //    //    }
    //    //    signstr += Timestamp;

    //    //    string postDataStr = "sign=" + MainModel.GetMD5(signstr);

    //    //    Url = MainModel.URL + Url + "?";
    //    //    foreach (KeyValuePair<string, string> keyvalue in sortpara)
    //    //    {
    //    //        Url += keyvalue.Key + "=" + keyvalue.Value + "&";
    //    //    }
    //    //    Url += postDataStr;

    //    //    Response respon = MyHttp.HTTP("GET", Url, "", "");


    //    //    retString = respon.HTTPResponseText;

    //    //    return retString;
    //    //}


    //    //public string HttpPOST(string Url, string bodyjson)
    //    //{
    //    //    string retString = "";
    //    //    try
    //    //    {


    //    //        string Timestamp = MainModel.getStampByDateTime(DateTime.Now);
    //    //        string nonce = MainModel.getNonce();
    //    //        string signstr = MainModel.PrivateKey + "nonce" + nonce + Timestamp + bodyjson;
    //    //        string postDataStr = "nonce=" + nonce + "&sign=" + MainModel.GetMD5(signstr);

    //    //        Url = MainModel.URL + Url + "?" + postDataStr;


    //    //        Response respon = MyHttp.HTTP("POST", Url, "", bodyjson);


    //    //        retString = respon.HTTPResponseText;

    //    //    }
    //    //    catch (WebException ex)
    //    //    {
    //    //        LogManager.WriteLog("访问服务器出错:" + ex.Message + ex.StackTrace);
    //    //    }

    //    //    return retString;
    //    //}




    //    internal bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
    //    {
    //        // 总是接受  
    //        return true;
    //    }


    //    [System.Runtime.InteropServices.DllImport("wininet.dll")]
    //    private extern static bool InternetGetConnectedState(int Description, int ReservedValue);

    //    /// <summary>
    //    /// 用于检查网络是否可以连接互联网,true表示连接成功,false表示连接失败 
    //    /// </summary>
    //    /// <returns></returns>
    //    public static bool IsConnectInternet()
    //    {
    //        int Description = 0;
    //        return InternetGetConnectedState(Description, 0);
    //    }

    //    #endregion

    //    object lockdic = new object();
    //    public void TestBack(object asyObj, string respStr, int statusCode, WebException webEx)
    //    {
    //        try
    //        {
    //            lock (lockdic)
    //            {
    //                dicResult.Add(asyObj.ToString(), respStr);
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            LogManager.WriteLog("testback异常" + ex.Message + respStr);
    //        }
    //    }
    //    /// <summary>
    //    /// http异步请求
    //    /// </summary>
    //    /// <param name="url">url</param>
    //    /// <param name="reqMethod">请求方法 GET、POST</param>
    //    /// <param name="Timestamp">请求头与签名时间戳需要一致  做参数传递</param>
    //    /// <param name="callback">回调函数</param>
    //    /// <param name="ob">回传对象</param>
    //    /// <param name="postData">post数据</param>
    //    public void HttpAsyncRequest(string Url, string reqMethod, string Timestamp, AsyRequetCallback callback, object ob = null, string postData = "")
    //    {
    //        Stream requestStream = null;
    //        try
    //        {
    //            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);

    //            HttpWebRequest request;

    //            System.Net.ServicePointManager.Expect100Continue = false;

    //            if (Url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
    //            {
    //                request = WebRequest.Create(new Uri(Url)) as HttpWebRequest;
    //                request.ProtocolVersion = HttpVersion.Version10;
    //                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;
    //            }
    //            else
    //            {
    //                request = WebRequest.Create(new Uri(Url)) as HttpWebRequest;
    //            }


    //            request.Method = reqMethod;

    //            ////请求头
    //            request.Headers.Add("v", MainModel.Version.Replace(".", ""));
    //            request.Headers.Add("X-ZZ-Device-Sn", MainModel.DeviceSN);
    //            request.Headers.Add("POS-Authorization", MainModel.Authorization);
    //            request.Headers.Add("X-ZZ-Timestamp", Timestamp);

    //            request.ContentType = "application/json;charset=UTF-8";

    //            request.KeepAlive = false;
    //            request.UseDefaultCredentials = true;
    //            request.ServicePoint.Expect100Continue = false;//important


    //            System.Net.ServicePointManager.DefaultConnectionLimit = 100;
    //            request.Timeout = 60 * 1000;



    //            if (reqMethod.ToUpper() == "POST")
    //            {
    //                byte[] bytes = Encoding.UTF8.GetBytes(postData);
    //                request.ContentLength = bytes.Length;
    //                requestStream = request.GetRequestStream();
    //                requestStream.Write(bytes, 0, bytes.Length);
    //            }
    //            //开始调用异步请求 
    //            //AsyResultTag 是自定义类 用于传递调用时信息 其中HttpWebRequest 是必须传递对象。
    //            //因为回调需要用HttpWebRequest来获取HttpWebResponse 
    //            request.BeginGetResponse(new AsyncCallback(HttpCallback), new AsyResultTag() { obj = ob, callback = callback, req = request });

    //        }
    //        catch (Exception ex)
    //        {
    //            LogManager.WriteLog("http请求异常" + ex.Message);
    //            //throw ex;
    //        }
    //        finally
    //        {
    //            if (requestStream != null)
    //            {
    //                requestStream.Close();
    //            }
    //        }
    //    }


    //    private void HttpAsyncRequestThread(QuestPara para)
    //    {
    //        Stream requestStream = null;
    //        try
    //        {

    //            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);

    //            HttpWebRequest request;

    //            System.Net.ServicePointManager.Expect100Continue = false;

    //            if (para.Url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
    //            {
    //                request = WebRequest.Create(new Uri(para.Url)) as HttpWebRequest;
    //                request.ProtocolVersion = HttpVersion.Version10;
    //                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;
    //            }
    //            else
    //            {
    //                request = WebRequest.Create(new Uri(para.Url)) as HttpWebRequest;
    //            }


    //            request.Method = para.reqMethod;

    //            ////请求头
    //            request.Headers.Add("v", MainModel.Version.Replace(".", ""));
    //            request.Headers.Add("X-ZZ-Device-Sn", MainModel.DeviceSN);
    //            request.Headers.Add("POS-Authorization", MainModel.Authorization);
    //            request.Headers.Add("X-ZZ-Timestamp", para.Timestamp);
    //            request.ContentType = "application/json;charset=UTF-8";

    //            request.KeepAlive = false;
    //            request.UseDefaultCredentials = true;
    //            request.ServicePoint.Expect100Continue = false;//important


    //            System.Net.ServicePointManager.DefaultConnectionLimit = 100;
    //            request.Timeout = 60 * 1000;



    //            if (para.reqMethod.ToUpper() == "POST")
    //            {
    //                byte[] bytes = Encoding.UTF8.GetBytes(para.postData);
    //                request.ContentLength = bytes.Length;
    //                requestStream = request.GetRequestStream();
    //                requestStream.Write(bytes, 0, bytes.Length);
    //            }
    //            //开始调用异步请求 
    //            //AsyResultTag 是自定义类 用于传递调用时信息 其中HttpWebRequest 是必须传递对象。
    //            //因为回调需要用HttpWebRequest来获取HttpWebResponse 
    //            request.BeginGetResponse(new AsyncCallback(HttpCallback), new AsyResultTag() { obj = para.ob, callback = para.callback, req = request });

    //        }
    //        catch (Exception ex)
    //        {
    //            LogManager.WriteLog("http请求异常" + ex.Message);
    //            //throw ex;
    //        }
    //        finally
    //        {
    //            if (requestStream != null)
    //            {
    //                requestStream.Close();
    //            }
    //        }
    //    }




    //    /// <summary>
    //    /// http请求回调 由.net内部调用 参数必须为IAsyncResult
    //    /// </summary>
    //    /// <param name="asynchronousResult">http回调时回传对象</param>
    //    private void HttpCallback(IAsyncResult asynchronousResult)
    //    {

    //        int statusCode = 0;
    //        string retString = "";
    //        AsyResultTag tag = new AsyResultTag();
    //        WebException webEx = null;
    //        try
    //        {
    //            //获取请求时传递的对象
    //            tag = asynchronousResult.AsyncState as AsyResultTag;
    //            HttpWebRequest req = tag.req;
    //            //获取异步返回的http结果
    //            HttpWebResponse response = req.EndGetResponse(asynchronousResult) as HttpWebResponse;
    //            Stream myResponseStream = response.GetResponseStream();
    //            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
    //            retString = myStreamReader.ReadToEnd();
    //            myStreamReader.Close();
    //            myResponseStream.Close();
    //            statusCode = ((int)response.StatusCode);



    //        }
    //        catch (WebException ex)
    //        {
    //            if ((HttpWebResponse)ex.Response != null)
    //            {
    //                statusCode = ((int)((HttpWebResponse)ex.Response).StatusCode);
    //            }

    //            webEx = ex;
    //        }
    //        //调用外部回调 即最外层的回调
    //        tag.callback(tag.obj, retString, statusCode, webEx);

    //    }


    //    /// <summary>
    //    /// 异步请求回调委托
    //    /// </summary>
    //    /// <param name="asyObj">回传对象</param>
    //    /// <param name="resStr">http响应结果</param>
    //    /// <param name="statusCode">http状态码</param>
    //    /// <param name="webEx">异常</param>
    //    public delegate void AsyRequetCallback(object asyObj, string respStr, int statusCode, WebException webEx);

    //    /// <summary>
    //    /// 异步返回对象
    //    /// </summary>
    //    class AsyResultTag
    //    {
    //        /// <summary>
    //        /// 回传对象
    //        /// </summary>
    //        public object obj { get; set; }
    //        /// <summary>
    //        /// 当前httpRequest请求实例
    //        /// </summary>
    //        public HttpWebRequest req { get; set; }
    //        /// <summary>
    //        /// 回调函数委托
    //        /// </summary>
    //        public AsyRequetCallback callback { get; set; }
    //    }


    //    class QuestPara
    //    {
    //        public string Url { get; set; }
    //        public string reqMethod { get; set; }
    //        public string Timestamp { get; set; }
    //        public AsyRequetCallback callback { get; set; }
    //        public object ob { get; set; }
    //        public string postData { get; set; }

    //    }

    //}

    public class HttpRequest
    {

        public bool WhetherGetOK = false;


        Dictionary<string, string> dicResult = new Dictionary<string, string>();

        public string HttpGetJson(string Url, SortedDictionary<string, string> sortpara)
        {
            try
            {
                if (!IsConnectInternet())
                {
                    return "";
                }
                AsyRequetCallback testback = new AsyRequetCallback(TestBack);


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


                string postDataStr = "sign=" + MainModel.GetMD5(signstr);

                Url = MainModel.URL + Url + "?";
                foreach (KeyValuePair<string, string> keyvalue in sortpara)
                {
                    Url += keyvalue.Key + "=" + keyvalue.Value + "&";
                }
                Url += postDataStr;

                //异步执行防止共用变量影响
                string thiscode = Guid.NewGuid().ToString();

                QuestPara para = new QuestPara();
                para.Url = Url;
                para.ob = thiscode;
                para.postData = postDataStr;
                para.reqMethod = "GET";
                para.Timestamp = Timestamp;
                para.callback = testback;
                System.ComponentModel.BackgroundWorker bk = new System.ComponentModel.BackgroundWorker();
                bk.DoWork += HttpAsyncRequest_DoWork;
                bk.RunWorkerAsync(para);

                // HttpAsyncRequest(Url, "GET", Timestamp, testback, thiscode, postDataStr);

                //接口访问完毕或 超时60
                long beginTime = DateTime.Now.Ticks;
                while (!WhetherGetOK)
                {
                    long endTime = DateTime.Now.Ticks;

                    TimeSpan elapsedSpan = new TimeSpan(endTime - beginTime);

                    if ((elapsedSpan.TotalMilliseconds) > 60000 || dicResult.Keys.Contains(thiscode))
                        break;

                    Delay.Start(20);
                }

                if (dicResult.Keys.Contains(thiscode))
                {
                    string result = dicResult[thiscode];
                    dicResult.Remove(thiscode);
                    return result;
                }
                else
                {
                    return "";
                }
                //return GetJson;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("HttpGetJson" + ex.Message);
                return "";
            }
        }



        public bool WhetherPostOK = false;

        public string HttpPostJson(string Url, string bodyjson)
        {
            try
            {
                if (!IsConnectInternet())
                {
                    return "";
                }

                AsyRequetCallback testback = new AsyRequetCallback(TestBack);
                string Timestamp = MainModel.getStampByDateTime(DateTime.Now);
                string nonce = MainModel.getNonce();

                string signstr = MainModel.PrivateKey + "nonce" + nonce + Timestamp + bodyjson;
                string postDataStr = "nonce=" + nonce + "&sign=" + MainModel.GetMD5(signstr);

                Url = MainModel.URL + Url + "?" + postDataStr;

                //异步执行防止共用变量影响
                string thiscode = Guid.NewGuid().ToString();

                QuestPara para = new QuestPara();
                para.Url = Url;
                para.ob = thiscode;
                para.postData = bodyjson;
                para.reqMethod = "POST";
                para.Timestamp = Timestamp;
                para.callback = testback;
                System.ComponentModel.BackgroundWorker bk = new System.ComponentModel.BackgroundWorker();
                bk.DoWork += HttpAsyncRequest_DoWork;
                bk.RunWorkerAsync(para);

                //接口访问完毕或 超时60
                long beginTime = DateTime.Now.Ticks;
                while (!WhetherGetOK)
                {
                    long endTime = DateTime.Now.Ticks;

                    TimeSpan elapsedSpan = new TimeSpan(endTime - beginTime);

                    if ((elapsedSpan.TotalMilliseconds) > 60000 || dicResult.Keys.Contains(thiscode))
                        break;

                    Delay.Start(20);
                }

                if (dicResult.Keys.Contains(thiscode))
                {
                    string result = dicResult[thiscode];
                    dicResult.Remove(thiscode);
                    return result;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("HttpPostJson" + ex.Message);
                return "";
            }
        }


        public string HttpPostJson(string Url, SortedDictionary<string, string> sortpara, string bodyjson)
        {
            try
            {

                if (!IsConnectInternet())
                {
                    return "";
                }

                AsyRequetCallback testback = new AsyRequetCallback(TestBack);

                string Timestamp = MainModel.getStampByDateTime(DateTime.Now);
                string nonce = MainModel.getNonce();

                string signstr = MainModel.PrivateKey;
                foreach (KeyValuePair<string, string> keyvalue in sortpara)//在nonce前加入get参数进行签名
                {
                    signstr += keyvalue.Key + keyvalue.Value;
                }
                signstr = signstr + "nonce" + nonce + Timestamp + bodyjson;
                string postDataStr = "nonce=" + nonce + "&sign=" + MainModel.GetMD5(signstr);

                Url = MainModel.URL + Url + "?";
                foreach (KeyValuePair<string, string> keyvalue in sortpara)//post请求加入get参数
                {
                    Url += keyvalue.Key + "=" + keyvalue.Value + "&";
                }
                Url = Url + postDataStr;


                //异步执行防止共用变量影响
                string thiscode = Guid.NewGuid().ToString();

                QuestPara para = new QuestPara();
                para.Url = Url;
                para.ob = thiscode;
                para.postData = bodyjson;
                para.reqMethod = "POST";
                para.Timestamp = Timestamp;
                para.callback = testback;
                System.ComponentModel.BackgroundWorker bk = new System.ComponentModel.BackgroundWorker();
                bk.DoWork += HttpAsyncRequest_DoWork;
                bk.RunWorkerAsync(para);

                //接口访问完毕或 超时60
                long beginTime = DateTime.Now.Ticks;
                while (!WhetherGetOK)
                {
                    long endTime = DateTime.Now.Ticks;

                    TimeSpan elapsedSpan = new TimeSpan(endTime - beginTime);

                    if ((elapsedSpan.TotalMilliseconds) > 60000 || dicResult.Keys.Contains(thiscode))
                        break;

                    Delay.Start(20);
                }

                if (dicResult.Keys.Contains(thiscode))
                {
                    string result = dicResult[thiscode];
                    dicResult.Remove(thiscode);
                    return result;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("HttpPostJson" + ex.Message);
                return "";
            }
        }


        #region  访问服务端


        internal bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            // 总是接受  
            return true;
        }


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

        object lockdic = new object();
        public void TestBack(object asyObj, string respStr, int statusCode, WebException webEx)
        {
            try
            {
                lock (lockdic)
                {
                    dicResult.Add(asyObj.ToString(), respStr);
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("testback异常" + ex.Message + respStr);
            }
        }

        /// <summary>
        /// http异步请求
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="reqMethod">请求方法 GET、POST</param>
        /// <param name="Timestamp">请求头与签名时间戳需要一致  做参数传递</param>
        /// <param name="callback">回调函数</param>
        /// <param name="ob">回传对象</param>
        /// <param name="postData">post数据</param>
        public void HttpAsyncRequest_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Stream requestStream = null;
            string retString = "";
            try
            {

                QuestPara para = e.Argument as QuestPara;
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);

                HttpWebRequest request;

                System.Net.ServicePointManager.Expect100Continue = false;

                if (para.Url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    request = WebRequest.Create(new Uri(para.Url)) as HttpWebRequest;
                    request.ProtocolVersion = HttpVersion.Version10;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;
                }
                else
                {
                    request = WebRequest.Create(new Uri(para.Url)) as HttpWebRequest;
                }


                request.Method = para.reqMethod;

                ////请求头
                request.Headers.Add("v", MainModel.Version.Replace(".", ""));
                request.Headers.Add("X-ZZ-Device-Sn", MainModel.DeviceSN);
                request.Headers.Add("POS-Authorization", MainModel.Authorization);
                request.Headers.Add("X-ZZ-Timestamp", para.Timestamp);

                request.ContentType = "application/json;charset=UTF-8";

                request.KeepAlive = false;
                request.UseDefaultCredentials = true;
                request.ServicePoint.Expect100Continue = false;//important

                System.Net.ServicePointManager.DefaultConnectionLimit = 100;
                request.Timeout = 60 * 1000;


                if (para.reqMethod.ToUpper() == "POST")
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(para.postData);
                    request.ContentLength = bytes.Length;
                    requestStream = request.GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                }
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
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

                dicResult.Add(para.ob.ToString(), retString);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("http请求异常" + ex.Message);
                //throw ex;
            }
            finally
            {
                if (requestStream != null)
                {
                    requestStream.Close();
                }
            }
        }

        /// <summary>
        /// 异步请求回调委托
        /// </summary>
        /// <param name="asyObj">回传对象</param>
        /// <param name="resStr">http响应结果</param>
        /// <param name="statusCode">http状态码</param>
        /// <param name="webEx">异常</param>
        public delegate void AsyRequetCallback(object asyObj, string respStr, int statusCode, WebException webEx);

        public class QuestPara
        {
            public string Url { get; set; }
            public string reqMethod { get; set; }
            public string Timestamp { get; set; }
            public AsyRequetCallback callback { get; set; }
            public object ob { get; set; }
            public string postData { get; set; }

        }

    }
}
