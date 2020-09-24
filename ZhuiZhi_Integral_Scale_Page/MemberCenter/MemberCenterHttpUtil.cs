using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Maticsoft.Model;
using Newtonsoft.Json;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using ZhuiZhi_Integral_Scale_UncleFruit.BrokenUI.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.Model.HalfOffLine;
using ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter
{
    public class MemberCenterHttpUtil
    {
        #region 修改密码&忘记密码接口
        /// <summary>
        /// 修改数字密码
        /// </summary>
        /// <param name="newdigitpaypassword">新密码</param>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public string UpdatePassWord(string newdigitpaypassword, string olddigitpaypassword, int resettype, ref string erromessage, ref int result)
        {
            try
            {
                string url = "/pos/member/balance/updatedigit";

                SortedDictionary<string, object> sort = new SortedDictionary<string, object>();
                sort.Add("newdigitpaypassword", newdigitpaypassword);
                sort.Add("olddigitpaypassword", olddigitpaypassword);
                sort.Add("resettype", resettype);
                sort.Add("memberid", MainModel.CurrentMember.memberid);
                sort.Add("tenantid", MainModel.CurrentShopInfo.tenantid);


                string testjson = JsonConvert.SerializeObject(sort);
                string json = HttpPOST(url, testjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                result = rd.code;

                if (rd.code == 0)
                {
                    return "true";
                }
                else
                {
                    try { LogManager.WriteLog("Error", "verifypassword:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "修改用户密码异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return "";
            }
        }
        /// <summary>
        /// 修改数字密码
        /// </summary>
        /// <param name="newdigitpaypassword">新密码</param>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public string ForgetSetPassWord(string newdigitpaypassword, int resettype, string smscode, ref string erromessage, ref int result)
        {
            try
            {
                string url = "/pos/member/balance/updatedigit";

                SortedDictionary<string, object> sort = new SortedDictionary<string, object>();
                sort.Add("newdigitpaypassword", newdigitpaypassword);
                sort.Add("resettype", resettype);
                sort.Add("memberid", MainModel.CurrentMember.memberid);
                sort.Add("tenantid", MainModel.CurrentShopInfo.tenantid);
                sort.Add("smscode", smscode);


                string testjson = JsonConvert.SerializeObject(sort);
                string json = HttpPOST(url, testjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                result = rd.code;

                if (rd.code == 0)
                {
                    return "true";
                }
                else
                {
                    try { LogManager.WriteLog("Error", "verifypassword:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "修改用户密码异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return "";
            }
        }
        /// <summary>
        /// 校验密码
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="authcode"></param>
        /// <param name="erromessage"></param>
        public VerifyBalancePwd VerifyBalancePwd(string paypassword, ref string erromessage, ref int resultcode,Member m)
        {
            try
            {


                string url = "/pos/member/balance/verifypassword";

                SortedDictionary<string, object> sort = new SortedDictionary<string, object>();
                sort.Add("paypasswordtype", 1);
                sort.Add("paypassword", paypassword);
                sort.Add("deviceid", MainModel.DeviceSN);
                sort.Add("authtoken", m.memberheaderresponsevo.token);
                sort.Add("memberid", m.memberid);
                string tempjson = JsonConvert.SerializeObject(sort);


                string json = HttpPOST(url, tempjson);

                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                resultcode = rd.code;

                if (rd.code == 0)
                {
                    VerifyBalancePwd verifyresult = JsonConvert.DeserializeObject<VerifyBalancePwd>(rd.data.ToString());

                    return verifyresult;

                }
                else
                {
                    try { LogManager.WriteLog("Error", "verifypassword:" + json); }
                    catch { }
                    erromessage = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "校验支付密码异常：" + ex.Message);
                erromessage = "网络连接异常，请检查网络连接";
                return null;
            }
        }

        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public string GetSendvalidateSmsCode(string memberid, ref string errormsg)
        {
            try
            {
                string url = "/pos/member/balance/sendvalidatesmscode";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                sort.Add("memberid", memberid);

                string json = HttpGET(url, sort);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    return rd.data.ToString();
                }
                else
                {
                    try { LogManager.WriteLog("Error", "GetSmsCode:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "获取短信验证码异常：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return null;
            }
        }
        /// <summary>
        /// 校验短信验证码
        /// </summary>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public string GetVerifysmscode(string smscode, ref string errormsg)
        {
            try
            {
                string url = "/pos/member/balance/verifysmscode";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                sort.Add("smscode", smscode);
                sort.Add("memberid", MainModel.CurrentMember.memberid);

                string json = HttpGET(url, sort);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                if (rd.code == 0)
                {
                    return "success";

                }
                else
                {
                    try { LogManager.WriteLog("Error", "GetVerifsms:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "短信验证码校验异常：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return null;
            }
        }
        #endregion

        /// <summary>
        /// 修改手机号码 新手机获取验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public int ChangeNumberGetSendsmscode(string phone, ref string errormsg)
        {
            try
            {
                string url = "/pos/member/sendsmscode";
                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                sort.Add("phone", phone);

                string json = HttpGET(url, sort);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);

                if (rd.code == 0)
                {
                    return 1;

                }
                else
                {
                    try { LogManager.WriteLog("Error", "NewPhoneGetsmsErr:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 修改手机号码 新手机验证码校验
        /// </summary>
        /// <returns></returns>
        public int ChangeNumberVerifysmscode(string newphonesmscode, ref string errormsg)
        {
            try
            {
                string url = "/pos/member/verifysmscode";
                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                sort.Add("authcode",newphonesmscode);
                sort.Add("mobile",MainModel.NewPhone);
                sort.Add("shopid",MainModel.CurrentShopInfo.shopid);
                string testjson = JsonConvert.SerializeObject(sort);
                string json = HttpPOST(url, testjson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    return 1;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "ChangeNumberVerifysmscode:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return 0;
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "验证码校验异常：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return 0;
            }
        }
        
        /// <summary>
        /// 修改手机号码 全局验证是否是会员
        /// </summary>
        /// <returns></returns>
        public bool GetCheckmember(string mobile, ref string errormsg)
        {
            try
            {
                string url = "/pos/member/memberheader/checkmember";
                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                sort.Add("mobile", mobile);

                string json = HttpGET(url, sort);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    if (rd.data.ToString()=="true")
                    {
                       return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    try { LogManager.WriteLog("Error", "GetCheckmembererr:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "验证是否是会员异常：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return false;
            }
        }
        /// <summary>
        /// 修改手机号码 更换会员手机号码
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public bool Updatemembermobile(string mobile, ref string errormsg)
        {
            try
            {
                string url = "/pos/member/memberheader/updatemembermobile";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                sort.Add("token", MainModel.CurrentMember.memberheaderresponsevo.token);
                sort.Add("mobile", MainModel.NewPhone);

                string json = HttpGET(url, sort);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    return true;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "Updatemembermobileerr:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "更换会员手机号异常：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                throw;
            }
        }
        /// <summary>
        /// 修改手机号码 合并会员
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public bool MergeMemberPhonenumber( ref string errormsg)
        {
            try
            {
                string url = "/pos/member/memberheader/merge";

                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                sort.Add("sourcetoken",MainModel.Sourcetoken );
                sort.Add("targettoken",MemberCenterHelper.member.memberheaderresponsevo.token);

                string json = HttpGET(url, sort);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    return true;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "checkmember:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "合并会员异常：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                throw;
            }
        }
        /// <summary>
        /// 更换手机号码 实体卡新卡验证
        /// </summary>
        /// <param name="cardid"></param>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public bool GetMactchCardNewCard(string cardid, ref string errormsg)
        {
            try
            {
                string url = "/pos/member/memberentitycard/mactchcard";
                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                sort.Add("cardid", cardid);
                sort.Add("memberid", MainModel.CurrentMember.memberid);

                string json = HttpGET(url, sort);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    return true;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "GetMactchCardNewCarderr:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "新卡验证异常：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                throw;
            }
        }
        /// <summary>
        /// 更换手机号码 实体卡旧卡验证
        /// </summary>
        /// <param name="cardid"></param>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public bool GetMactchCardOldCard(string oldcardid, ref string errormsg)
        {
            try
            {
                string url = "/pos/member/outentitycard/mactchcard";
                SortedDictionary<string, string> sort = new SortedDictionary<string, string>();
                sort.Add("oldcardid", oldcardid);
                sort.Add("memberid", MainModel.CurrentMember.memberid);

                string json = HttpGET(url, sort);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    return true;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "GetMactchCardNewCarderr:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "旧卡验证异常：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                throw;
            }
        }
        /// <summary>
        /// 自定义充值
        /// </summary>
        /// <returns></returns>
        
        public bool CreateMember(CreateMemberPara para, ref string errormsg)
        {
            try
            {
                string url = "/pos/member/memberheader/create/inactivemember";
               
                string parajson = JsonConvert.SerializeObject(para);
                string json = HttpPOST(url, parajson);
                ResultData rd = JsonConvert.DeserializeObject<ResultData>(json);
                if (rd.code == 0)
                {
                    return true;
                }
                else
                {
                    try { LogManager.WriteLog("Error", "inactivemember:" + json); }
                    catch { }
                    errormsg = rd.message;
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error", "创建会员异常：" + ex.Message);
                errormsg = "网络连接异常，请检查网络连接";
                return false;
            }
        }


        #region  访问服务端
        private HttpRequest httprequest = new HttpRequest();

        static object lockhttpget = new object();
        public string HttpGET(string Url, SortedDictionary<string, string> sortpara)
        {
            Other.CrearMemory();
            return httprequest.HttpGetJson(Url, sortpara);
        }

        static object lockhttppost = new object();
        public string HttpPOST(string Url, string bodyjson)
        {
            Other.CrearMemory();
            return httprequest.HttpPostJson(Url, bodyjson);
        }



        //public string HttpGET(string Url, SortedDictionary<string, string> sortpara)
        //{
        //    System.GC.Collect();
        //    string retString = "";

        //    string Timestamp = MainModel.getStampByDateTime(DateTime.Now);
        //    string nonce = MainModel.getNonce();

        //    sortpara.Add("nonce", nonce);
        //    //string signstr = "kVl55eO1n3DZhWC8Z7" + "devicesn" + devicesn + "nonce" + nonce + Timestamp;
        //    string signstr = MainModel.PrivateKey;
        //    foreach (KeyValuePair<string, string> keyvalue in sortpara)
        //    {
        //        signstr += keyvalue.Key + keyvalue.Value;
        //    }
        //    signstr += Timestamp;

        //    //string body = "{\"devicesn\":\"" + devicesn + "\"}";

        //    string postDataStr = "sign=" + MainModel.GetMD5(signstr);
        //    //Url += "?" + "devicesn=" + devicesn + "&" + postDataStr;

        //    Url = MainModel.URL + Url + "?";
        //    foreach (KeyValuePair<string, string> keyvalue in sortpara)
        //    {
        //        Url += keyvalue.Key + "=" + keyvalue.Value + "&";
        //    }
        //    Url += postDataStr;
        //    // //Console.Write(Url);
        //    try
        //    {
        //        ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);

        //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);

        //        request.Timeout = 60 * 1000;

        //        System.Net.ServicePointManager.Expect100Continue = false;

        //        if (Url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
        //        {
        //            //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
        //            request = WebRequest.Create(new Uri(Url)) as HttpWebRequest;
        //            request.ProtocolVersion = HttpVersion.Version10;
        //            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;
        //        }
        //        else
        //        {
        //            request = WebRequest.Create(new Uri(Url)) as HttpWebRequest;
        //        }


        //        request.Method = "GET";

        //        ////请求头
        //        request.Headers.Add("v", MainModel.Version);
        //        request.Headers.Add("X-ZZ-Device-Sn", MainModel.DeviceSN);
        //        request.Headers.Add("POS-Authorization", MainModel.Authorization);
        //        request.Headers.Add("X-ZZ-Timestamp", Timestamp);

        //        request.ContentType = "application/json;charset=UTF-8";

        //        request.KeepAlive = false;
        //        request.UseDefaultCredentials = true;
        //        request.ServicePoint.Expect100Continue = false;//important


        //        System.Net.ServicePointManager.DefaultConnectionLimit = 100;
        //        request.Timeout = 60 * 1000; //3秒钟无响应 网络有问题

        //        ParameterizedThreadStart Pts = new ParameterizedThreadStart(GetResponseResult);
        //        Thread thread = new Thread(Pts);
        //        thread.IsBackground = true;
        //        thread.Start(request);

        //        while (thread.IsAlive)
        //        {
        //            Delay.Start(100);
        //        }

        //        int id = thread.ManagedThreadId;

        //        try
        //        {
        //            retString = dicresult[id];
        //            dicresult.Remove(id);
        //        }
        //        catch (Exception ex)
        //        {
        //            LogManager.WriteLog("线程返回异常" + ex.Message);
        //        }

        //        try
        //        {
        //            request.Abort();
        //        }
        //        catch { }
        //        //if (CheckNet())
        //        //{
        //        //    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        //        //    {
        //        //        request.Timeout = 60 * 1000;
        //        //        Stream myResponseStream = response.GetResponseStream();
        //        //        StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
        //        //        retString = myStreamReader.ReadToEnd();

        //        //        myStreamReader.Close();
        //        //        myResponseStream.Close();

        //        //        try
        //        //        {
        //        //            response.Close();
        //        //        }
        //        //        catch { }
        //        //    }
        //        //    try
        //        //    {
        //        //        request.Abort();
        //        //    }
        //        //    catch { }
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show(ex.StackTrace);
        //        LogManager.WriteLog("访问服务器出错:" + ex.Message);
        //        MainModel.ShowLog("访问服务器出错,请检查网络连接！", false);

        //    }

        //    return retString;
        //}


        //public string HttpPOST(string Url, string bodyjson)
        //{

        //    string retString = "";

        //    string Timestamp = MainModel.getStampByDateTime(DateTime.Now);
        //    string nonce = MainModel.getNonce();
        //    string signstr = MainModel.PrivateKey + "nonce" + nonce + Timestamp + bodyjson;
        //    string postDataStr = "nonce=" + nonce + "&sign=" + MainModel.GetMD5(signstr);

        //    Url = MainModel.URL + Url + "?" + postDataStr;
        //    try
        //    {
        //        ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);

        //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);

        //        System.Net.ServicePointManager.Expect100Continue = false;

        //        if (Url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
        //        {
        //            //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
        //            request = WebRequest.Create(new Uri(Url)) as HttpWebRequest;
        //            request.ProtocolVersion = HttpVersion.Version10;
        //            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;
        //        }
        //        else
        //        {
        //            request = WebRequest.Create(new Uri(Url)) as HttpWebRequest;
        //        }

        //        request.Method = "POST";

        //        ////请求头
        //        request.Headers.Add("v", MainModel.Version);
        //        request.Headers.Add("X-ZZ-Device-Sn", MainModel.DeviceSN);
        //        request.Headers.Add("POS-Authorization", MainModel.Authorization);
        //        request.Headers.Add("X-ZZ-Timestamp", Timestamp);


        //        byte[] by = Encoding.GetEncoding("utf-8").GetBytes(bodyjson);   //请求参数转码
        //        request.ContentType = "application/json;charset=UTF-8";
        //        request.Timeout = 1000 * 60;
        //        request.ContentLength = by.Length;

        //        Stream stw = request.GetRequestStream();     //获取绑定相应流
        //        stw.Write(by, 0, by.Length);      //写入流
        //        stw.Close();    //关闭流

        //        System.Net.ServicePointManager.DefaultConnectionLimit = 100;

        //        ParameterizedThreadStart Pts = new ParameterizedThreadStart(GetResponseResult);
        //        Thread thread = new Thread(Pts);
        //        thread.IsBackground = true;
        //        thread.Start(request);

        //        while (thread.IsAlive)
        //        {
        //            Delay.Start(100);
        //        }

        //        int id = thread.ManagedThreadId;

        //        try
        //        {
        //            retString = dicresult[id];
        //            dicresult.Remove(id);
        //        }
        //        catch (Exception ex)
        //        {
        //            LogManager.WriteLog("线程返回异常" + ex.Message);
        //        }

        //        //request.Timeout = 60 * 1000;
        //        //using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        //        //{
        //        //    request.Timeout = 60 * 1000;
        //        //    Stream myResponseStream = response.GetResponseStream();
        //        //    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
        //        //    retString = myStreamReader.ReadToEnd();

        //        //    myStreamReader.Close();
        //        //    myResponseStream.Close();

        //        //    try
        //        //    {
        //        //        response.Close();
        //        //    }
        //        //    catch { }
        //        //}


        //        try
        //        {
        //            request.Abort();
        //        }
        //        catch { }


        //    }
        //    catch (Exception ex)
        //    {
        //        LogManager.WriteLog("访问服务器出错:" + ex.Message);
        //        //MainModel.ShowLog("访问服务器出错,请检查网络连接！", false);
        //    }

        //    return retString;
        //}

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
                // ShowLog("无法检测网络连接是否正常-" + ex.Message, true);
            }

            return var;
        }

        public Dictionary<int, string> dicresult = new Dictionary<int, string>();
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

                    dicresult.Add(Thread.CurrentThread.ManagedThreadId, result);
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

        ////当前时间戳
        //private string getStampByDateTime(DateTime datetime )
        //{

        //    //DateTime datetime = DateTime.Now;
        //    var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        //    var result = (long)(datetime - startTime).TotalMilliseconds;

        //    return result.ToString();
        //}

        //private DateTime  GetDateTimeByStamp(string stamp)
        //{
        //    try
        //    {
        //        long result = Convert.ToInt64(stamp);
        //        DateTime datetime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        //        datetime = datetime.AddMilliseconds(result);
        //        return datetime;
        //    }
        //    catch (Exception ex)
        //    {
        //        return DateTime.Now;
        //    }
        //}

        ////MD5 加密
        //private string GetMD5(string str)
        //{

        //    byte[] result = Encoding.Default.GetBytes(str);
        //    MD5 md5 = new MD5CryptoServiceProvider();
        //    byte[] output = md5.ComputeHash(result);
        //    string md5str = BitConverter.ToString(output).Replace("-", ""); 
        //    return md5str;
        //}

        ////获取20位随机码 nonce
        //private string getNonce()
        //{
        //    string randomstr = "0,1,2,3,4,5,6,7,8,9,0,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
        //    string[] strRandom = randomstr.Split(',');
        //    Random rd = new Random();
        //    string result = "";
        //    for (int i = 0; i < 20; i++)
        //    {
        //        int num = rd.Next(35);

        //        result += strRandom[num];
        //    }

        //    return result;
        //}

        ////键值对排序
        //private Dictionary<string,string> SortDictory(Dictionary<string,string> dictionary)
        //{
        //    System.Collections.ArrayList lst = new System.Collections.ArrayList(dictionary.Keys);
        //    lst.Sort();
        //    //lst.Reverse();  //反转排序
        //    Dictionary<string, string> dicresult = new Dictionary<string, string>();

        //    foreach (string key in lst)
        //    {
        //        dicresult.Add(key, dictionary[key]);
        //    }

        //    return dicresult;
        //}

        #endregion

    }
}
