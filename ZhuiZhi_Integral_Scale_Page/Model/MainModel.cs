using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Model
{
    public class MainModel
    {
        //判断客屏是否播放视屏  是的话把焦点还给主界面
        public static bool IsPlayer = false;

        /// <summary>
        /// INI目录
        /// </summary>
        public static string IniPath = AppDomain.CurrentDomain.BaseDirectory + "Config.ini";

        /// <summary>
        /// StartINI目录
        /// </summary>
        public static string StartIniPath = AppDomain.CurrentDomain.BaseDirectory + "StartConfig.ini";

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
        ///  //当前离线登录手机号
        /// </summary>
        public static string CurrentUserPhone = "";

        /// <summary>
        /// 当前是否是离线状态
        /// </summary>
        public static bool IsOffLine = false;

        /// <summary>
        /// 在线模式想 半离线是否开启
        /// </summary>
        public static bool WhetherHalfOffLine = true;

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
        public static int Code_260011 = 260011;//小主，当前选择优惠券无效

        /// <summary>
        /// 商品价格或促销发生变化，请重新指定成交价    释放修改价格 重新刷新购物车
        /// </summary>
        public static int Code_260058 = 260058;//小主，当前选择优惠券无效

        public static string CurrentCouponCode = "";

        public static OrderCouponVo Currentavailabecoupno = null;

        /// <summary>
        /// 当前店铺信息
        /// </summary>
        public static DeviceShopInfo CurrentShopInfo;


        public static string Titledata = DateTime.Now.ToString("yyyy-MM-dd ")+ GlobalUtil.GetWeek();

        /// <summary>
        /// 是否自动加购
        /// </summary>
        public static bool WhetherAutoCart = false;
        /// <summary>
        /// 是否开启打码
        /// </summary>
        public static bool WhetherPrint = false;
        /// <summary>
        /// 是否自动打码
        /// </summary>
        public static bool WhetherAutoPrint = false;


        public static decimal CurrentTareWeight = (decimal)0.00;

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
        /// 电视屏1
        /// </summary>
        public static string TvShowPage1 = "";

        /// <summary>
        /// 电视屏2
        /// </summary>
        public static string TvShowPage2 = "";


        /// <summary>
        /// 促销商品1
        /// </summary>
        public static PosActivesSku TVActivesSku1 = null;

        /// <summary>
        /// 促销商品2
        /// </summary>
        public static PosActivesSku TVActivesSku2 = null;



        
        /// <summary>
        /// 页面宽度缩放比例
        /// </summary>
        public static float wScale = 1;
        /// <summary>
        /// 页面高度缩放比例
        /// </summary>
        public static float hScale = 1;

        /// <summary>
        /// 页面宽高比例中间值
        /// </summary>
        public static float midScale = 1;

        /// <summary>
        /// 获取全量商品接口时间戳，不是第一次调用的话需要使用上一次返回时间戳
        /// </summary>
        public static string LastQuerySkushopAllTimeStamp = "";


        /// <summary>
        /// 获取增量商品接口时间戳，不是第一次调用的话需要使用上一次返回时间戳
        /// </summary>
        public static string LastQuerySkushopCrementTimeStamp = "";

        public static string LastScaleTimeStamp = "";

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
        /// 离线挂单文件路径
        /// </summary>
        public static string OffLineOrderPath
        {
            get
            {

                //判断默认文件夹是否存在，不存在就创建
                DirectoryInfo logDirectory = new DirectoryInfo(System.Windows.Forms.Application.StartupPath + "\\OffLineOrderPath\\");
                if (!logDirectory.Exists)
                    logDirectory.Create();

                return System.Windows.Forms.Application.StartupPath + "\\OffLineOrderPath\\";

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
       // public static FormMainMedia frmmainmedia = null;



        /// <summary>
        /// 现金支付窗体页面
        /// </summary>
        public static frmCashPay frmcashpay = null;


        /// <summary>
        /// 现金券窗体页面
        /// </summary>
        public static FormPayByCashCoupon frmcashcoupon = null;




        public static Cart frmMainmediaCart = null;

        public static frmLoadingTop frmloading = null;


        /// <summary>
        /// 在线登录窗体
        /// </summary>
        public static frmLogin frmlogin = null;

        /// <summary>
        /// 离线登录窗体
        /// </summary>
        public static frmLoginOffLine frmloginoffline = null;

        /// <summary>
        /// 称重页面
        /// </summary>
        public static FormScale frmscale = null;

        //当前时间戳
        public static string getStampByDateTime(DateTime datetime)
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

                ToastHelper.AutoToast(msg,1200);
                if (iserror)
                {
                    LogManager.WriteLog(msg);

                }
            }

        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="con"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Image GetControlImage(Control con,object obj=null)
        {
            try
            {
                //获取单元格图片内容
                Bitmap b = new Bitmap(con.Width, con.Height);

                con.DrawToBitmap(b, new Rectangle(0, 0, con.Width, con.Height));

                if (obj != null)
                {
                    b.Tag = obj;
                }
                return b;
            }
            catch (Exception ex)
            {
                return new Bitmap(con.Width, con.Height);
            }
        }


        /// <summary>
        /// 截取图片区域，返回所截取的图片  注意超出范围是黑色
        /// </summary>
        /// <param name="SrcImage"></param>
        /// <param name="pos"></param>
        /// <param name="cutWidth"></param>
        /// <param name="cutHeight"></param>
        /// <returns></returns>
        public static Image cutImage(Image SrcImage, Point pos, int cutWidth, int cutHeight)
        {

            Image cutedImage = null;

            //先初始化一个位图对象，来存储截取后的图像
            Bitmap bmpDest = new Bitmap(cutWidth, cutHeight, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            Graphics g = Graphics.FromImage(bmpDest);

            //矩形定义,将要在被截取的图像上要截取的图像区域的左顶点位置和截取的大小
            Rectangle rectSource = new Rectangle(pos.X, pos.Y, cutWidth, cutHeight);


            //矩形定义,将要把 截取的图像区域 绘制到初始化的位图的位置和大小
            //rectDest说明，将把截取的区域，从位图左顶点开始绘制，绘制截取的区域原来大小
            Rectangle rectDest = new Rectangle(0, 0, cutWidth, cutHeight);

            //第一个参数就是加载你要截取的图像对象，第二个和第三个参数及如上所说定义截取和绘制图像过程中的相关属性，第四个属性定义了属性值所使用的度量单位
            g.DrawImage(SrcImage, rectDest, rectSource, GraphicsUnit.Pixel);

            //在GUI上显示被截取的图像
            cutedImage = (Image)bmpDest;

            g.Dispose();

            return cutedImage;

        }

        public static Image GetWinformImage(Form frm)
        {
            try
            {
                //获取当前屏幕的图像
                Bitmap b = new Bitmap(frm.Width, frm.Height);
                frm.DrawToBitmap(b, new Rectangle(0, 0, frm.Width, frm.Height));



                //b.Save(yourFileName);
                float opacity = (float)-0.5;

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
            catch (Exception ex)
            {
                LogManager.WriteLog("获取页面截图异常"+ex.Message);
                return new Bitmap(frm.Width, frm.Height);
            }
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
        public static string GB2312ToUTF8(string str)
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
        /// rsa加密公钥
        /// </summary>
        public static string RSAPrivateKey
        {
            get
            {
                //TODO
                if (URL.Contains("https://pos.a72hongjie.com"))
                {
                    return "MFwwDQYJKoZIhvcNAQEBBQADSwAwSAJBAIWSVenS4KZRSQB5S4dt25JRy/wX33do4nqK7CHzr+v6cNAsT0icoIFpFksmoIM5KNmvnsF3uRzr2rvg6IlV8gUCAwEAAQ==";
                }
                else
                {
                    return "MFwwDQYJKoZIhvcNAQEBBQADSwAwSAJBAN2nINiXBXIzzC6LMqS7/cyXLtEpqa+e2WcyHQoyXytWabBNRH8Vno/d/sDXCZm81LIJJwralJHYUciMMTEkqeMCAwEAAQ==";
                }

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
                LogManager.WriteLog("计算RSA公钥异常" + PublicKey + ":" + content + ":" + ex.Message);
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



        public static bool TaskIsShow = true;

        //Win+D    页面FormBoardStyle  属性不能为none 否则返回windows页面只要有焦点事件就会打开程序

        [DllImport("User32.dll")]
        public static extern void keybd_event(Byte bVk, Byte bScan, Int32 dwFlags, Int32 dwExtraInfo);

        public static void ShowWindows()
        {
            try
            {
                //try
                //{

                //    ShowWindow(FindWindow("Shell_TrayWnd", null), (int)SW_SHOW);
                //}
                //catch (Exception ex)
                //{
                //    LogManager.WriteLog("显示任务栏异常" + ex.Message);
                //}
                ShowTask();
                keybd_event(0x5b, 0, 0, 0); //0x5b是left win的代码，这一句使key按下，下一句使key释放。 
                keybd_event(68, 0, 0, 0);
                keybd_event(0x5b, 0, 0x2, 0);
                keybd_event(68, 0, 0x2, 0);
                //this.WindowState = FormWindowState.Minimized;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("显示windows桌面异常" + ex.Message);
            }
        }


        public static void ShowTaskThread()
        {
            TaskIsShow = true;
            Thread thread = new Thread(ShowTask);
            thread.IsBackground = true;
            thread.Start();
        }

        public static  void ShowTask()
        {
            //try
            //{
            //    TaskIsShow = true;
            //    ShowWindow(FindWindow("Shell_TrayWnd", null), (int)SW_SHOW);
            //}
            //catch(Exception ex) {
            //    LogManager.WriteLog("显示任务栏异常"+ex.Message);
            //}
        }

        public static void HideTaskThread()
        {
           
            if (TaskIsShow)
            {
                Thread thread = new Thread(HideTask);
                thread.IsBackground = true;
                thread.Start();
            }
          
        }
        public static void HideTask()
        {
        //    try
        //    {
        //        if (TaskIsShow)
        //        {
        //            ShowWindow(FindWindow("Shell_TrayWnd", null), (int)SW_HIDE);
        //            TaskIsShow = false;
        //        }
                
        //    }
        //    catch (Exception ex)
        //    {
        //        LogManager.WriteLog("隐藏任务栏异常" + ex.Message);
        //    }
            
        }


        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;
        [DllImport("user32.dll")]
        internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        internal static extern int ShowWindow(IntPtr hWin, int nCmdShow);




    }
}
