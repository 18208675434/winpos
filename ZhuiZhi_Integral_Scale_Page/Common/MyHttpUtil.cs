using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Common
{
    class MyHttpUtil
    {
    }




    // Need class with TlsClient in inheritance chain  

    class MyTlsClient : DefaultTlsClient
    {

        public override TlsAuthentication GetAuthentication()
        {

            return new MyTlsAuthentication();

        }

    }





    // Need class to handle certificate auth  

    class MyTlsAuthentication : TlsAuthentication
    {

        public TlsCredentials GetClientCredentials(CertificateRequest certificateRequest)
        {

            // return client certificate  

            return null;

        }





        public void NotifyServerCertificate(Certificate serverCertificate)
        {

            // validate server certificate  

        }

    }





    public class MyHttp
    {

        public static Response HTTP(string _type, string _url, String _auth = "", string _postdata = "", string _cookie = "")
        {

            Encoding _responseEncode = Encoding.UTF8;

            int port = 80;


            try
            {

                if (_url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    port = 443;
                }

                else

                { }


                Uri URI = new Uri(_url);


                TcpClient tcpClient = new TcpClient(URI.Host, port);

                TlsClientProtocol protocol = new TlsClientProtocol(tcpClient.GetStream(), new Org.BouncyCastle.Security.SecureRandom());

                MyTlsClient client = new MyTlsClient();

                protocol.Connect(client);


                //utf8编码

                byte[] bs = UTF8Encoding.UTF8.GetBytes(_postdata);

                StringBuilder RequestHeaders = new StringBuilder();

                RequestHeaders.Append(_type + " " + URI.PathAndQuery + " HTTP/1.1\r\n");

                RequestHeaders.Append("Content-Type: application/json;charset=UTF-8\r\n");

                //RequestHeaders.Append("User-Agent: Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.64 Safari/537.11\r\n");

                //RequestHeaders.Append("Cookie: " + _cookie + "\r\n");

                //RequestHeaders.Append("Accept: */*\r\n");

                RequestHeaders.Append("Host:" + URI.Host + "\r\n");

                RequestHeaders.Append("Content-Length:" + bs.Length + "\r\n");

                //RequestHeaders.Append("Connection: close\r\n");


                string Timestamp = MainModel.getStampByDateTime(DateTime.Now);
                ////请求头
                RequestHeaders.Append("v:" + MainModel.Version.Replace(".", "") + "\r\n");
                RequestHeaders.Append("X-ZZ-Device-Sn:" + MainModel.DeviceSN + "\r\n");
                RequestHeaders.Append("POS-Authorization:" + MainModel.Authorization + "\r\n");
                RequestHeaders.Append("X-ZZ-Timestamp:" + Timestamp + "\r\n");

                if (!String.IsNullOrEmpty(_auth))
                {
                    RequestHeaders.Append("Authorization: " + "Basic " + _auth + "\r\n");
                }

                RequestHeaders.Append("\r\n");

                byte[] request = Encoding.UTF8.GetBytes(RequestHeaders.ToString() + _postdata);


                //发送http请求  
                protocol.Stream.Write(request, 0, request.Length);
                protocol.Stream.Flush();

                //读取返回内容  

                MemoryStream ms = new MemoryStream();


                byte[] buffer = new byte[409600];

                int actual = 0;

                //先保存到内存流MemoryStream中

                try
                {
                    if ((actual = protocol.Stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, actual);

                    }

                }

                catch { }

                ms.Position = 0;

                byte[] bImageBytes = ms.ToArray();

                string result = Encoding.UTF8.GetString(bImageBytes);
               // LogManager.WriteLog(result);



                int headerIndex = result.IndexOf("\r\n\r\n");

                string[] headerStr = result.Substring(0, headerIndex).Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                Dictionary<string, string> responseHeader = new Dictionary<string, string>();

                for (int i = 0; i < headerStr.Length; i++)
                {

                    string[] temp = headerStr[i].Split(new string[] { ": " }, StringSplitOptions.RemoveEmptyEntries);

                    if (temp.Length == 2)
                    {

                        if (responseHeader.ContainsKey(temp[0]))
                        {

                            responseHeader[temp[0]] = temp[1];

                        }

                        else
                        {

                            responseHeader.Add(temp[0], temp[1]);

                        }

                    }

                }

                Response response = new Response();

                response.HTTPResponseHeader = responseHeader;

                string[] httpstatus = headerStr[0].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                if (httpstatus.Length > 2)
                {

                    response.HTTPStatusCode = httpstatus[1];

                }

                else
                {

                    response.HTTPStatusCode = "400";

                }

                response.HTTPResponseText = _responseEncode.GetString(Encoding.UTF8.GetBytes(result.Substring(headerIndex + 4)));

                return response;
            }

            catch (Exception ex)
            {
                LogManager.WriteLog("teste" + ex.Message + ex.StackTrace);
                return null;
            }
        }

    }





    public class Response
    {





        string hTTPStatusCode;

        ///   

        /// http状态代码  

        ///   

        public string HTTPStatusCode
        {

            get { return hTTPStatusCode; }

            set { hTTPStatusCode = value; }

        }









        Dictionary<string, string> hTTPResponseHeader;

        ///   

        /// Response的header  

        ///   

        public Dictionary<string, string> HTTPResponseHeader
        {

            get { return hTTPResponseHeader; }

            set { hTTPResponseHeader = value; }

        }





        string hTTPResponseText;

        ///   

        /// html代码  

        ///   

        public string HTTPResponseText
        {

            get { return hTTPResponseText; }

            set { hTTPResponseText = value; }

        }


    }



}
