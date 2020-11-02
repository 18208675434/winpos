using Newtonsoft.Json;
using QDAMAPOS.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace QDAMAPOS.Common
{
    public class HttpProcessor
    {
        public TcpClient socket;
        public HttpServer srv;

        private Stream inputStream;
        public StreamWriter outputStream;

        public String http_method;
        public String http_url;
        public String http_protocol_versionstring;
        public Hashtable httpHeaders = new Hashtable();

        

        private static int MAX_POST_SIZE = 10 * 1024 * 1024; // 10MB

        public HttpProcessor(TcpClient s, HttpServer srv)
        {
            this.socket = s;
            this.srv = srv;
        }


        private string streamReadLine(Stream inputStream)
        {
            int next_char;
            string data = "";
            while (true)
            {
                next_char = inputStream.ReadByte();
                if (next_char == '\n') { break; }
                if (next_char == '\r') { continue; }
                if (next_char == -1) { Thread.Sleep(1); continue; };
                data += Convert.ToChar(next_char);
            }
            return data;
        }
        public void process()
        {
            try
            {
                // we can't use a StreamReader for input, because it buffers up extra data on us inside it's
                // "processed" view of the world, and we want the data raw after the headers
                inputStream = new BufferedStream(socket.GetStream());

                // we probably shouldn't be using a streamwriter for all output from handlers either
                outputStream = new StreamWriter(new BufferedStream(socket.GetStream()), Encoding.UTF8);
                try
                {
                    parseRequest();
                    readHeaders();
                    if (http_method.Equals("GET"))
                    {
                        handleGETRequest();
                    }
                    else if (http_method.Equals("POST"))
                    {
                        handlePOSTRequest();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.ToString());
                    writeFailure();
                }
                outputStream.Flush();
                // bs.Flush(); // flush any remaining output
                inputStream = null; outputStream = null; // bs = null;            
                socket.Close();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("TVLOG" + "process异常" + ex.Message);
            }
        }

        public void parseRequest()
        {
            String request = streamReadLine(inputStream);
            string[] tokens = request.Split(' ');
            if (tokens.Length != 3)
            {
                throw new Exception("invalid http request line");
            }
            http_method = tokens[0].ToUpper();
            http_url = tokens[1];
            http_protocol_versionstring = tokens[2];

            Console.WriteLine("starting: " + request);
        }

        public void readHeaders()
        {
            Console.WriteLine("readHeaders()");
            String line;
            while ((line = streamReadLine(inputStream)) != null)
            {
                if (line.Equals(""))
                {
                    Console.WriteLine("got headers");
                    return;
                }

                int separator = line.IndexOf(':');
                if (separator == -1)
                {
                    throw new Exception("invalid http header line: " + line);
                }
                String name = line.Substring(0, separator);
                int pos = separator + 1;
                while ((pos < line.Length) && (line[pos] == ' '))
                {
                    pos++; // strip any spaces
                }

                string value = line.Substring(pos, line.Length - pos);
                Console.WriteLine("header: {0}:{1}", name, value);
                httpHeaders[name] = value;
            }
        }

        public void handleGETRequest()
        {
            srv.handleGETRequest(this);
        }

        private const int BUF_SIZE = 4096;
        public void handlePOSTRequest()
        {
            try { 
            // this post data processing just reads everything into a memory stream.
            // this is fine for smallish things, but for large stuff we should really
            // hand an input stream to the request processor. However, the input stream 
            // we hand him needs to let him see the "end of the stream" at this content 
            // length, because otherwise he won't know when he's seen it all! 

            Console.WriteLine("get post data start");
            int content_len = 0;
            MemoryStream ms = new MemoryStream();
            if (this.httpHeaders.ContainsKey("Content-Length"))
            {
                content_len = Convert.ToInt32(this.httpHeaders["Content-Length"]);
                if (content_len > MAX_POST_SIZE)
                {
                    throw new Exception(
                        String.Format("POST Content-Length({0}) too big for this simple server",
                          content_len));
                }
                byte[] buf = new byte[BUF_SIZE];
                int to_read = content_len;
                while (to_read > 0)
                {
                    Console.WriteLine("starting Read, to_read={0}", to_read);

                    int numread = this.inputStream.Read(buf, 0, Math.Min(BUF_SIZE, to_read));
                    Console.WriteLine("read finished, numread={0}", numread);
                    if (numread == 0)
                    {
                        if (to_read == 0)
                        {
                            break;
                        }
                        else
                        {
                            throw new Exception("client disconnected during post");
                        }
                    }
                    to_read -= numread;
                    ms.Write(buf, 0, numread);
                }
                ms.Seek(0, SeekOrigin.Begin);
            }
            Console.WriteLine("get post data end");
            srv.handlePOSTRequest(this, new StreamReader(ms));
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("TVLOG" + "handlePOSTRequest" + ex.Message);
            }
        }

        public void writeSuccess()
        {
            try
            {
                outputStream.WriteLine("HTTP/1.0 200 OK");
                outputStream.WriteLine("Content-Type: text/html; charset=utf-8");
                outputStream.WriteLine("Connection: close");
                outputStream.WriteLine("");
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("TVLOG" + "writeSuccess"+ex.Message);
            }
        }

        public void writeFailure()
        {
            try
            {
                outputStream.WriteLine("HTTP/1.0 404 File not found");
                outputStream.WriteLine("Connection: close");
                outputStream.WriteLine("");
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("TVLOG" + "writeFailure" + ex.Message);
            }
        }
    }

    public abstract class HttpServer
    {

        protected int port;
        TcpListener listener;
        bool is_active = true;

        public HttpServer(int port)
        {
            this.port = port;
        }

        public void listen()
        {
            try { 
            //检查端口是否被占用
            System.Net.NetworkInformation.IPGlobalProperties ipProperties = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();
            

            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                {
                    LogManager.WriteLog("TVLOG" + "端口被占用" + port);
                    return;
                }
            }

            listener = new TcpListener(port);
            listener.Start();
            while (is_active)
            {
                TcpClient s = listener.AcceptTcpClient();
                HttpProcessor processor = new HttpProcessor(s, this);
                Thread thread = new Thread(new ThreadStart(processor.process));
                thread.Start();
                Thread.Sleep(1);
            }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("TVLOG" + "服务监听异常" + ex.Message);
            }
        }

        public abstract void handleGETRequest(HttpProcessor p);
        public abstract void handlePOSTRequest(HttpProcessor p, StreamReader inputData);
    }

    public class MyHttpServer : HttpServer
    {
        public MyHttpServer(int port)
            : base(port)
        {
        }
        public override void handleGETRequest(HttpProcessor p)
        {

            try
            {
                string url = p.http_url;
                Console.WriteLine("request: {0}", p.http_url);

                TVShowSkus tvshow = new TVShowSkus();
                tvshow.code = -1;
                tvshow.message = "";
                tvshow.data = null;

                if (url == "/static/html/televisionscreen")  //电视屏蔬菜促销页面
                {
                    p.writeSuccess();
                    p.outputStream.WriteLine(MainModel.PromotionJson);
                }
                else if (url == "/static/html/televisionscreen.html")  //电视屏蔬菜促销页面
                {
                    p.writeSuccess();

                    string file = "D:\\televisionscreen.html";
                    StreamReader sr = new StreamReader(file, Encoding.UTF8);
                    string result = sr.ReadToEnd();

                    p.outputStream.WriteLine(result);
                }
                else if (url == "/static/html/televisionscreenbypork") //电视屏猪肉促销页面
                {
                    p.writeSuccess();
                    p.outputStream.WriteLine(MainModel.PorkJson);
                }
                else if (url == "/static/html/televisionscreenbypork.html")  //电视屏蔬菜促销页面
                {
                    p.writeSuccess();

                    string file = "D:\\televisionscreenbypork.html";
                    StreamReader sr = new StreamReader(file, Encoding.UTF8);
                    string result = sr.ReadToEnd();

                    p.outputStream.WriteLine(result);
                }
                else if (url == "/api/getactiveskus")  //60个蔬菜+30个猪肉
                {

                    List<TVProduct> lstpro = new List<TVProduct>();
                    if (MainModel.TVSingleActivesSku != null && MainModel.TVSingleActivesSku.posactiveskudetails != null && MainModel.TVSingleActivesSku.posactiveskudetails.Count > 0)
                    {

                        lstpro.AddRange( MainModel.TVSingleActivesSku.posactiveskudetails);
                    }
                    if (MainModel.TVPorkSkus != null && MainModel.TVPorkSkus.posactiveskudetails != null && MainModel.TVPorkSkus.posactiveskudetails.Count > 0)
                    {
                        lstpro.AddRange(MainModel.TVPorkSkus.posactiveskudetails);
                    }

                    if (lstpro.Count > 0)
                    {
                        tvshow.code = 0;
                        tvshow.data = lstpro;
                    }
                    else
                    {
                        tvshow.code = -1;
                        tvshow.message = "暂无数据";
                    }

                    p.writeSuccess();
                    p.outputStream.WriteLine(get_uft8( JsonConvert.SerializeObject(tvshow)));
                }
                else if (url == "/api/getsingleactiveskus") //60个蔬菜
                {
                    if (MainModel.TVSingleActivesSku != null && MainModel.TVSingleActivesSku.posactiveskudetails != null && MainModel.TVSingleActivesSku.posactiveskudetails.Count > 0)
                    {
                        tvshow.code = 0;
                        tvshow.data = MainModel.TVSingleActivesSku.posactiveskudetails;
                    }
                    else
                    {
                        tvshow.message = "暂无数据";
                    }
                    p.writeSuccess();
                    p.outputStream.WriteLine( JsonConvert.SerializeObject(tvshow));
                }
                else if (url == "/api/getpromotionskus")  //今日促销商品
                {
                    if (MainModel.TVPromotionSkus != null && MainModel.TVPromotionSkus.posactiveskudetails != null && MainModel.TVPromotionSkus.posactiveskudetails.Count > 0)
                    {
                        tvshow.code = 0;
                        tvshow.data = MainModel.TVPromotionSkus.posactiveskudetails;
                    }
                    else
                    {
                        tvshow.message = "暂无数据";
                    }
                    p.writeSuccess();
                    p.outputStream.WriteLine(JsonConvert.SerializeObject(tvshow));
                }
                else if (url == "/api/getactiveskusbypork") //30个猪肉
                {

                    if (MainModel.TVPorkSkus != null && MainModel.TVPorkSkus.posactiveskudetails != null && MainModel.TVPorkSkus.posactiveskudetails.Count > 0)
                    {
                        tvshow.code = 0;
                        tvshow.data = MainModel.TVPorkSkus.posactiveskudetails;
                    }
                    else
                    {
                        tvshow.message = "暂无数据";
                    }
                    p.writeSuccess();
                    p.outputStream.WriteLine(JsonConvert.SerializeObject(tvshow));
                }
                else
                {
                    p.writeSuccess();
                    p.outputStream.WriteLine(JsonConvert.SerializeObject(tvshow));
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("TVLOG" + "响应请求异常" + ex.Message);
                try
                {
                    TVShowSkus tvshow = new TVShowSkus();
                    tvshow.code = 500;
                    tvshow.message = ex.Message;
                    tvshow.data = null;
                    p.writeSuccess();
                    p.outputStream.WriteLine(JsonConvert.SerializeObject(tvshow));
                }
                catch (Exception ex1)
                {
                    LogManager.WriteLog("TVLOG" + "响应异常结果错误" + ex1.Message);
                }
            }
        }

        public override void handlePOSTRequest(HttpProcessor p, StreamReader inputData)
        {
            Console.WriteLine("POST request: {0}", p.http_url);
            string data = inputData.ReadToEnd();

            p.outputStream.WriteLine("<html><body><h1>test server</h1>");
            p.outputStream.WriteLine("<a href=/test>return</a><p>");
            p.outputStream.WriteLine("postbody: <pre>{0}</pre>", data);
        }

        public static string get_uft8(string unicodeString)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            Byte[] encodedBytes = utf8.GetBytes(unicodeString);
            String decodedString = utf8.GetString(encodedBytes);
            return decodedString;
        }
    }
}
