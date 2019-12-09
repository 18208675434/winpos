﻿using WinSaasPOS.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace WinSaasPOS.Model
{
    public class MainModel
    {
        /// <summary>
        /// INI目录
        /// </summary>
        public static string IniPath = AppDomain.CurrentDomain.BaseDirectory + "Config.ini";

        /// <summary>
        /// 程序根目录
        /// </summary>
        public static string ServerPath = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// 私钥
        /// </summary>
        public static string PrivateKey = INIManager.GetIni("System", "PrivateKey", MainModel.IniPath);
        /// <summary>
        /// 环境
        /// </summary>
        public static string URL = INIManager.GetIni("System", "URL", MainModel.IniPath);
        
        /// <summary>
        /// POS端 token
        /// </summary>
        public static string Authorization = INIManager.GetIni("System", "POS-Authorization", MainModel.IniPath);
        /// <summary>
        /// 设备号
        /// </summary>
        public static string DeviceSN = "";
        /// <summary>
        /// 版本号
        /// </summary>
        public static string Version = INIManager.GetIni("System", "Version", MainModel.IniPath);

        public static string TempFilePath = AppDomain.CurrentDomain.BaseDirectory + "TempFile" + "\\";

        ///// <summary>
        ///// 商店ID
        ///// </summary>
        //public static string ShopID = INIManager.GetIni("System", "ShopID", MainModel.IniPath);
        /// <summary>
        /// 商店名称
        /// </summary>
        public static string ShopName = INIManager.GetIni("System", "ShopName", MainModel.IniPath);

        /// <summary>
        /// 当前登录用户信息
        /// </summary>
        public static userModel CurrentUser;

        /// <summary>
        /// 当前登录会员
        /// </summary>
        public static Member CurrentMember = null;

        /// <summary>
        /// 接口返回code 100031;//店员登录失效
        /// </summary>
        public static int HttpUserExpired = 100031;
        /// <summary>
        /// 接口返回code 120014;//会员登录失效
        /// </summary>
        public static int HttpMemberExpired = 120014;

        /// <summary>
        /// 接口返回code 100011;//非当前登录用户付款码
        /// </summary>
        public static int DifferentMember = 100011;

        /// <summary>
        /// 小主，当前选择优惠券无效
        /// </summary>
        public static int Code_260011=260011;//小主，当前选择优惠券无效

        /// <summary>
        /// 商品价格或促销发生变化，请重新指定成交价    释放修改价格 重新刷新购物车
        /// </summary>
        public static int Code_260058 = 260058;//小主，当前选择优惠券无效

        public static string CurrentCouponCode ="";

        /// <summary>
        /// 当前店铺信息
        /// </summary>
        public static DeviceShopInfo CurrentShopInfo;





        /// <summary>
        /// 用于同步主屏和客屏 余额密码输入  （焦点问题）
        /// </summary>
        public static string BalancePwd = "";

        /// <summary>
        /// 用于同步主屏和客屏 回车键按钮
        /// </summary>
        public static bool BalanceEnter = false;

        /// <summary>
        /// 用于同步主屏和客屏 关闭页面
        /// </summary>
        public static bool BalanceClose = false;

        /// <summary>
        /// 密码支付 rsa加密
        /// </summary>
        public static string BalancePayPwd = "";

        /// <summary>
        /// 安全码  购物车需要用
        /// </summary>
        public static string BalanceSecuritycode = "";

        /// <summary>
        /// 安全码  购物车需要用
        /// </summary>
        public static bool BalanceClear = false;

        /// <summary>
        /// 余额密码验证错误吗
        /// </summary>

        public static int BalancePwdErrorCode = -1;







        /// <summary>
        /// 页面宽度缩放比例
        /// </summary>
        public static  float wScale = 1;
        /// <summary>
        /// 页面高度缩放比例
        /// </summary>
        public static  float hScale = 1;

        /// <summary>
        /// 获取全量商品接口时间戳，不是第一次调用的话需要使用上一次返回时间戳
        /// </summary>
        public static string LastQuerySkushopAllTimeStamp = "";


        /// <summary>
        /// 获取增量商品接口时间戳，不是第一次调用的话需要使用上一次返回时间戳
        /// </summary>
        public static string LastQuerySkushopCrementTimeStamp = "";

        public static string OrderPath
        {
            get
            {

                //判断默认文件夹是否存在，不存在就创建
                DirectoryInfo logDirectory = new DirectoryInfo(System.Windows.Forms.Application.StartupPath + "\\OrderPath\\");
                if (!logDirectory.Exists)
                    logDirectory.Create();

                return System.Windows.Forms.Application.StartupPath + "\\OrderPath\\";

            }
        }


        /// <summary>
        /// 面板商品图片路径
        /// </summary>
        public static string ProductPicPath
        {
            get
            {

                //判断默认文件夹是否存在，不存在就创建
                DirectoryInfo logDirectory = new DirectoryInfo(System.Windows.Forms.Application.StartupPath + "\\ProductPicPath\\");
                if (!logDirectory.Exists)
                    logDirectory.Create();

                return System.Windows.Forms.Application.StartupPath + "\\ProductPicPath\\";

            }
        }

        /// <summary>
        /// 客屏媒体路径
        /// </summary>
        public static string MediaPath
        {
            get
            {

                //判断默认文件夹是否存在，不存在就创建
                DirectoryInfo logDirectory = new DirectoryInfo(System.Windows.Forms.Application.StartupPath + "\\MediaPath\\");
                if (!logDirectory.Exists)
                    logDirectory.Create();

                return System.Windows.Forms.Application.StartupPath + "\\MediaPath\\";

            }
        }


        /// <summary>
        /// 客屏对象
        /// </summary>
        public static frmMainMedia frmmainmedia = null;


        public static Cart frmMainmediaCart = null;
        

        public static frmLoadingTop frmloading=null;

        //当前时间戳
        public static  string getStampByDateTime(DateTime datetime)
        {

            //DateTime datetime = DateTime.Now;
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            var result = (long)(datetime - startTime).TotalMilliseconds;

            return result.ToString();
        }

        public static DateTime GetDateTimeByStamp(string stamp)
        {
            try
            {
                long result = Convert.ToInt64(stamp);
                DateTime datetime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                datetime = datetime.AddMilliseconds(result);
                return datetime;
            }
            catch (Exception ex)
            {
                return DateTime.Now;
            }
        }

        //MD5 加密
        public static string GetMD5(string str)
        {

            //byte[] result = Encoding.Default.GetBytes(str);
            byte[] result = Encoding.UTF8.GetBytes(str);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            string md5str = BitConverter.ToString(output).Replace("-", "");
            return md5str;
        }

        //获取20位随机码 nonce
        public static string getNonce()
        {
            string randomstr = "0,1,2,3,4,5,6,7,8,9,0,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
            string[] strRandom = randomstr.Split(',');
            Random rd = new Random();
            string result = "";
            for (int i = 0; i < 20; i++)
            {
                int num = rd.Next(35);

                result += strRandom[num];
            }

            return result;
        }

        //键值对排序
        public static Dictionary<string, string> SortDictory(Dictionary<string, string> dictionary)
        {
            System.Collections.ArrayList lst = new System.Collections.ArrayList(dictionary.Keys);
            lst.Sort();
            //lst.Reverse();  //反转排序
            Dictionary<string, string> dicresult = new Dictionary<string, string>();

            foreach (string key in lst)
            {
                dicresult.Add(key, dictionary[key]);
            }

            return dicresult;
        }

        public static AutoSizeFormUtil AuroSizeUtil = new AutoSizeFormUtil();


        public static void ShowLog(string msg, bool iserror)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                MsgHelper.AutoShowForm(msg);
                LogManager.WriteLog(msg);
            }
         
        }



        public static  Image GetWinformImage(Form frm)
        {
            //获取当前屏幕的图像
            Bitmap b = new Bitmap(frm.Width, frm.Height);
            frm.DrawToBitmap(b, new Rectangle(0, 0, frm.Width, frm.Height));
           

           
            //b.Save(yourFileName);
            float opacity =(float) -0.5;
            
            //float[][] nArray ={ new float[] {1, 0, 0, 0, 0},
            //      new float[] {0,1, 0, 0, 0},
            //      new float[] {0, 0, 1, 0, 0},
            //      new float[] {0, 0, 0, opacity, 0},
            //      new float[] {0, 0, 0, 0, 1}};

            float[][] nArray = {new float[] {1,0,0,0,0},
                                                 new float[] {0,1,0,0,0},
                                                 new float[] {0,0,1,0,0},
                                                 new float[] {0,0,0,1,0},
                                                 new float[] {opacity,opacity,opacity,0,1}};


            ColorMatrix matrix = new ColorMatrix(nArray);
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            Bitmap resultImage = new Bitmap(b.Width, b.Height);
            Graphics g = Graphics.FromImage(resultImage);
            g.DrawImage(b, new Rectangle(0, 0, b.Width, b.Height), 0, 0, b.Width, b.Height, GraphicsUnit.Pixel, attributes);

            return resultImage;

            //return b;
        }


        public static Image TransparentImage(Image srcImage, float opacity)
        {
            float[][] nArray ={ new float[] {1, 0, 0, 0, 0},
                  new float[] {0, 1, 0, 0, 0},
                  new float[] {0, 0, 1, 0, 0},
                  new float[] {0, 0, 0, opacity, 0},
                  new float[] {0, 0, 0, 0, 1}};
            ColorMatrix matrix = new ColorMatrix(nArray);
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            Bitmap resultImage = new Bitmap(srcImage.Width, srcImage.Height);
            Graphics g = Graphics.FromImage(resultImage);
            g.DrawImage(srcImage, new Rectangle(0, 0, srcImage.Width, srcImage.Height), 0, 0, srcImage.Width, srcImage.Height, GraphicsUnit.Pixel, attributes);

            return resultImage;
        }




        public static string UTF8ToGB2312(string str)
        {
            try
            {   
                Encoding utf8 = Encoding.GetEncoding(65001);
                Encoding gb2312 = Encoding.GetEncoding("gb2312");//Encoding.Default ,936
                byte[] temp = utf8.GetBytes(str);
                byte[] temp1 = Encoding.Convert(utf8, gb2312, temp);
                string result = gb2312.GetString(temp1);
                return result;
            }
            catch (Exception ex)//(UnsupportedEncodingException ex)
           {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        public static  string GB2312ToUTF8(string str)
        {
            try
            {
                Encoding uft8 = Encoding.GetEncoding(65001);
                Encoding gb2312 = Encoding.GetEncoding("gb2312");
                byte[] temp = gb2312.GetBytes(str);
               // MessageBox.Show("gb2312的编码的字节个数：" + temp.Length);
                for (int i = 0; i < temp.Length; i++)
                {
                    //MessageBox.Show(Convert.ToUInt16(temp[i]).ToString());
                }   
                byte[] temp1 = Encoding.Convert(gb2312, uft8, temp);
                //MessageBox.Show("uft8的编码的字节个数：" + temp1.Length);
                for (int i = 0; i < temp1.Length; i++)
                {
                   // MessageBox.Show(Convert.ToUInt16(temp1[i]).ToString());
                }              
                string result = uft8.GetString(temp1);
                return result;
            }
            catch (Exception ex)//(UnsupportedEncodingException ex)
           {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }




        public static object Clone(object sampleObject)
        {
            try
            {
                Type t = sampleObject.GetType();
                PropertyInfo[] properties = t.GetProperties();
                object p = t.InvokeMember("", BindingFlags.CreateInstance, null, sampleObject, null);
                foreach (PropertyInfo pi in properties)
                {
                    if (pi.CanWrite)
                    { object value = pi.GetValue(sampleObject, null); pi.SetValue(p, value, null); }
                } return p;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ERROR", "克隆对象异常" + ex.Message);
                return sampleObject;
            }
        }




        /// <summary>
        /// 公钥加密
        /// </summary>
        /// <param name="xmlPublicKey"></param>
        /// <param name="content"></param>
        /// <returns></returns>
       public static string RSAEncrypt(string PublicKey, string content)
        {
            try
            {
                string xmlPublicKey = ToXmlPublicKey(PublicKey);
                string encryptedContent = string.Empty;
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.FromXmlString(xmlPublicKey);
                    byte[] encryptedData = rsa.Encrypt(Encoding.Default.GetBytes(content), false);
                    encryptedContent = Convert.ToBase64String(encryptedData);
                }
                return encryptedContent;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("计算RSA公钥异常"+PublicKey+":"+content+":"+ex.Message);
                return "";
            }
        }



        /// <summary>
        /// base64 public key string -> xml public key
        /// </summary>
        /// <param name="pubilcKey"></param>
        /// <returns></returns>
        public static string ToXmlPublicKey(string pubilcKey)
        {
            RsaKeyParameters p =
                PublicKeyFactory.CreateKey(Convert.FromBase64String(pubilcKey)) as RsaKeyParameters;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                RSAParameters rsaParams = new RSAParameters
                {
                    Modulus = p.Modulus.ToByteArrayUnsigned(),
                    Exponent = p.Exponent.ToByteArrayUnsigned()
                };
                rsa.ImportParameters(rsaParams);
                return rsa.ToXmlString(false);
            }
        }


    }
}
