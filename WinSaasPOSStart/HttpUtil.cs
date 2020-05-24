
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
//using System.Threading.Tasks;

namespace WinSaasPosStart
{
    public class HttpUtil
    {

        public VersionInfo GetWinPosVersion(ref string erromessage)
        {
            try
            {

                string url = "/pos/common/appversion/winposdetail";
                //string url = "/appversion/winposdetail";
                //string url = "/pos/common/wechat/suncode/membercard";
                
                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();


                string json = HttpGET(url, sort);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                // return;
                if (rd.code == 0)
                {
                    erromessage = "";
                    VersionInfo versioninfo = JsonConvert.DeserializeObject<VersionInfo>(rd.data.ToString());
                    return versioninfo;
                }
                else
                {
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取winpos异常：" + ex.Message);
                erromessage = "获取winpos异常：" + ex.Message;
                return null;
            }
        }



        private HttpRequest httprequest = new HttpRequest();
        public string HttpGET(string Url, SortedDictionary<string, string> sortpara)
        {
            Other.CrearMemory();
            return httprequest.HttpGetJson(Url, sortpara);
        }
        public string HttpPOST(string Url, string bodyjson)
        {
            Other.CrearMemory();
            return httprequest.HttpPostJson(Url, bodyjson);
        }



       // //#region  访问服务端
       // public string HttpGET(string Url, SortedDictionary<string, string> sortpara)
       // {
       //     string retString = "";

       //     string Timestamp = MainModel.getStampByDateTime(DateTime.Now);
       //     string nonce = MainModel.getNonce();

       //     sortpara.Add("nonce", nonce);
       //     string signstr = MainModel.PrivateKey;
       //     foreach (KeyValuePair<string, string> keyvalue in sortpara)
       //     {
       //         signstr += keyvalue.Key + keyvalue.Value;
       //     }
       //     signstr += Timestamp;

       //     string postDataStr = "sign=" + MainModel.GetMD5(signstr);

       //     Url = MainModel.URL + Url + "?";
       //     foreach (KeyValuePair<string, string> keyvalue in sortpara)
       //     {
       //         Url += keyvalue.Key + "=" + keyvalue.Value + "&";
       //     }
       //     Url += postDataStr;
       //     // //Console.Write(Url);
       //     try
       //     {
       //         ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);

       //         HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);

       //         try
       //         {
       //             int timeout = 60000;
       //             if (timeout > 0)
       //                 request.Timeout = timeout;
       //         }
       //         catch { }

       //         System.Net.ServicePointManager.Expect100Continue = false;

       //         if (Url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
       //         {
       //             //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
       //             request = WebRequest.Create(new Uri(Url)) as HttpWebRequest;
       //             request.ProtocolVersion = HttpVersion.Version10;
       //             ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;
       //         }
       //         else
       //         {
       //             request = WebRequest.Create(new Uri(Url)) as HttpWebRequest;
       //         }

       //         request.Method = "GET";

       //         ////请求头
       //         request.Headers.Add("v", MainModel.Version);
       //         request.Headers.Add("X-ZZ-Device-Sn", MainModel.DeviceSN);
       //         //request.Headers.Add("POS-Authorization", MainModel.Authorization);
       //         request.Headers.Add("X-ZZ-Timestamp", Timestamp);

       //         request.ContentType = "application/json;charset=UTF-8";

       //         //byte[] by = Encoding.GetEncoding("utf-8").GetBytes(body);   //请求参数转码
       //         ////request.ContentType = "application/json;charset=UTF-8";
       //         //request.ContinueTimeout = 500000;
       //         //request.ContentLength = by.Length;

       //         //Stream stw = request.GetRequestStream();     //获取绑定相应流
       //         //stw.Write(by, 0, by.Length);      //写入流
       //         //stw.Close();    //关闭流

       //         HttpWebResponse response = (HttpWebResponse)request.GetResponse();

       //         Stream myResponseStream = response.GetResponseStream();
       //         StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
       //         retString = myStreamReader.ReadToEnd();
       //         myStreamReader.Close();
       //         myResponseStream.Close();
       //     }
       //     catch (Exception ex)
       //     {
       //        //MessageBox.Show("访问服务器出错,请检查网络连接！");
       //        LogManager.WriteLog("访问服务器出错:" + ex.Message);
       //        // MainModel.ShowLog("访问服务器出错,请检查网络连接！", false);

       //     }

       //     return retString;
       // }

       // internal bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
       // {
       //     // 总是接受  
       //     return true;
       // }

       // //public string HttpPOST(string Url, string bodyjson)
       // //{

       // //    string retString = "";

       // //    string Timestamp = MainModel.getStampByDateTime(DateTime.Now);
       // //    string nonce = MainModel.getNonce();
       // //    string signstr = MainModel.PrivateKey + "nonce" + nonce + Timestamp + bodyjson;
       // //    string postDataStr = "nonce=" + nonce + "&sign=" + MainModel.GetMD5(signstr);

       // //    Url = MainModel.URL + Url + "?" + postDataStr;
       // //    // Console.WriteLine(Url);
       // //    try
       // //    {
       // //        ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);

       // //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);

       // //        try
       // //        {
       // //            int timeout = 5000;
       // //            if (timeout > 0)
       // //                request.Timeout = timeout;
       // //        }
       // //        catch { }

       // //        System.Net.ServicePointManager.Expect100Continue = false;

       // //        if (Url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
       // //        {
       // //            //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
       // //            request = WebRequest.Create(new Uri(Url)) as HttpWebRequest;
       // //            request.ProtocolVersion = HttpVersion.Version10;
       // //            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
       // //        }
       // //        else
       // //        {
       // //            request = WebRequest.Create(new Uri(Url)) as HttpWebRequest;
       // //        }

       // //        request.Method = "POST";

       // //        ////请求头
       // //        request.Headers.Add("v", MainModel.Version);
       // //        request.Headers.Add("X-ZZ-Device-Sn", MainModel.DeviceSN);
       // //        request.Headers.Add("POS-Authorization", MainModel.Authorization);
       // //        request.Headers.Add("X-ZZ-Timestamp", Timestamp);


       // //        byte[] by = Encoding.GetEncoding("utf-8").GetBytes(bodyjson);   //请求参数转码
       // //        request.ContentType = "application/json;charset=UTF-8";
       // //        request.ContinueTimeout = 500000;
       // //        request.ContentLength = by.Length;

       // //        Stream stw = request.GetRequestStream();     //获取绑定相应流
       // //        stw.Write(by, 0, by.Length);      //写入流
       // //        stw.Close();    //关闭流

       // //        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

       // //        Stream myResponseStream = response.GetResponseStream();
       // //        StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
       // //        retString = myStreamReader.ReadToEnd();
       // //        myStreamReader.Close();
       // //        myResponseStream.Close();
       // //    }
       // //    catch (Exception ex)
       // //    {
       // //        LogManager.WriteLog("访问服务器出错:" + ex.Message);
       // //        MainModel.ShowLog("访问服务器出错,请检查网络连接！", false);
       // //    }

       // //    return retString;
       // //}

       // //internal bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
       // //{
       // //    // 总是接受  
       // //    return true;
       // //}
     
       //// #endregion
    }
}
