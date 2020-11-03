/*
 * 果叔 易捷通—爱宝ps1X 设备   串口外接
 * 
 * pa1dll 创建对象和释放对象必须一对一执行，否则会无法使用 ！！！！！
 * 
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;

namespace ZhuiZhi_Integral_Scale_UncleFruit.ScaleFactory
{
    public class Scale_LF : Scale_Action
    {
        #region api
        //#define HS_OK 0xf0 //正常
        //#define HS_ERROR 0xff //异常

        /*功能	清零
        函数名	int send_zero(void)
        Int send_zero_stdcall (void)
        参数	
        返回值	HS_OK
        HS_ERROR 
        */
        [DllImport("lf_pos_dll.dll", EntryPoint = "send_zero_stdcall", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int send_zero_stdcall();


        /*功能	设置皮重
        函数名	int send_tare(char *buf)
        int send_tare_stdcall (char *buf)
        参数	buf 的长度必须大于等于 10 字节。
        buf 中的值为空字符串或"0"或"0.000"，有两种函义：
        1，如果有皮重，如毛重=0，则清除皮重，否则如净重大于 0，则按键去皮
        2，如果没有皮重，则按键去皮
        buf 为皮重字符串则表示预置去皮,如:需要设置的皮重值的字符串,如 20 克，则参数为字符串"0.020"。
        返回值	HS_OK
        HS_ERROR
        实例	 
        备注	返回值：
        HS_OK 成功,此时 buf 中存放当前皮重
        HS_ERROR  失败
        当返回 HS_OK 时，表明当前次皮重操作成功，操作后实际的皮重值会存放在 buf 中，如皮重操作
        后皮重值为 20 克，则 buf 中的内容为"00.0200"。
        //设置预置皮重，根据传入的皮重参数进行去皮
        //参数 buf 既作为传入参数，又作为返回皮重使用，长度要大于等于 10 字节
        */
        [DllImport("lf_pos_dll.dll", EntryPoint = "send_tare_stdcall", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int send_tare_stdcall(byte[] buf);

        /*功能	清皮
        函数名	int clear_tare (char *buf)
        int clear_tare_stdcall (char *buf)
        参数	buf 的长度必须大于等于 10 字节。
        buf 中的值为空字符串或"0"或"0.000"，有两种函义：
        1，如果有皮重，如毛重=0，则清除皮重，否则如净重大于 0，则按键去皮
        2，如果没有皮重，则按键去皮
        buf 为皮重字符串则表示预置去皮,如:需要设置的皮重值的字符串,如 20 克，则参数为字符串"0.020"。
        返回值	HS_OK
        HS_ERROR
        实例	 
        备注	返回值：
        HS_OK 成功,此时 buf 中存放当前皮重
        HS_ERROR  失败
        当返回 HS_OK 时，表明当前次皮重操作成功，操作后实际的皮重值会存放在 buf 中，如皮重操作
        后皮重值为 20 克，则 buf 中的内容为"00.0200"。
        //设置预置皮重，根据传入的皮重参数进行去皮
        //参数 buf 既作为传入参数，又作为返回皮重使用，长度要大于等于 10 字节
        */
        [DllImport("lf_pos_dll.dll", EntryPoint = "clear_tare_stdcall", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int clear_tare_stdcall(byte[] buf);

        /*功能	读取重量
        函数名	int read_standard(char *buf)
        int read_standard_stdcall (char *buf)
        参数	buf 长度要大于等于 18 字节
        如返回 HS_OK,则成功取得重量信息,数据结构如下
        char status;
        //status 的 bit0(第一位)表示是否稳定，如为 1 则表示稳定
        //status 的 bit1(第二位)表示是否在零位，如为 1 则表示零位
        //status 的 bit2(第三位)表示是否有皮重，如为 1 则表示有皮重
        char net_weight[7];
        注： 如净重<=-10.000 时， 小数点后取 3 位， 其它时候为 4 位。 如 9.9980， -10.124
        char FixSeparator;//固定为"P"
        char tare_weight[7];
        当处于欠载状态时，net_weight中的数据为"┗━┛"，返回值为-1 
        当处于过载状态时，net_weight中的数据为"┏━┓"，返回值为-2
        建议：如在欠载过载状态称重时、会导致数据与实物不准确。建议软件做相应警示提示
        在对接读取重量时，建议采用线程的方式，其次也可以采用定时器的方式，读取重量建议每秒 3-5
        次左右，两次调用重量接口之间间隔 200-300ms 左右，在采用定时器时要注意，进入定时器事件处理后，应先关闭定时器，然后读取并处理数据，最后再重新启动定时器。
        示例：如稳定的净重量为 1234g，皮重 10g，则返回数据如下
        status 的值为 5
        net_weight 中的数据为： "01.2340"
        FixSeparator 的值为字符:'P'
        tare_weight 中的数据为： "00.0100"
        如稳定的净重量为-10468g，皮重 10468g，则返回数据如下
        status 的值为 5
        net_weight 中的数据为： "-10.468"
        FixSeparator 的值为字符:'P'
        tare_weight 中的数据为： "10.4680"
        C#、 Delphi 取重量的示例参考本文档最后一部分内容，对于个别开发语言不支持位操作时，可以用
        下面的方法来判断稳定&零位&皮重标志：
        bit2~~bit0 对应十进制数 皮重状态 零位状态 稳定状态
        000 0 无皮重 非零位 非稳定
        001 1 无皮重 非零位 稳定
        010 2 无皮重 零位 非稳定
        011 3 无皮重 零位 稳定
        100 4 有皮重 非零位 非稳定
        101 5 有皮重 非零位 稳定
        110 6 有皮重 零位 非稳定
        111 7 有皮重 零位 稳定
        将 status 转为十进制数， 然后判断对应的十进制值，以确定状态。
        [注意] 第 1 个表示状态的字节可能值为 0x00，所以不可能使用字符串的方式直接处理，否则可能在
        首字节为 0x00 时，因 0x00 表示字符串的结束符而取不到任何信息
        返回值	HS_OK
        HS_ERROR
        -2:超载
        -3:0点末标定
        -4：任一点未标定
        -5：开机自动清零失败！
        实例	 
        备注	返回值：
        HS_OK 成功
        HS_ERROR  失败
        */
        [DllImport("lf_pos_dll.dll", EntryPoint = "read_standard_stdcall", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int read_standard_stdcall(byte[] buf);

        #endregion

        private const int HS_OK = 240;
        public override bool Open(string connum, int baud)
        {
            return true;
        }

        public override bool Close()
        {
            return true;
        }

        // 易捷通  没有稳定状态 根据时间判断 ，连续三次获取重量一致则判定为稳定
        public decimal lastweight = 0;
        public int OKnum = 0;
        /// <summary>
        /// 获取秤称重信息
        /// </summary>
        /// <returns></returns>
        public override ScaleResult GetScaleWeight()
        {
            ScaleResult result = new ScaleResult();
            result.WhetherSuccess = false;

            try
            {
                int li_ret = 0;
                byte[] ls_buf_temp = new byte[32];
                li_ret = read_standard_stdcall(ls_buf_temp);
                if (li_ret == HS_OK || li_ret == -1 || li_ret == -2)
                {
                    string fixSeparator = "P";//正常:P 欠载:┗━┛
                    if (li_ret != HS_OK)
                    {
                        fixSeparator = "┗━┛";
                    }
                    string ls_buf = Encoding.Default.GetString(ls_buf_temp, 0, ls_buf_temp.Length);
                    if (!string.IsNullOrEmpty(ls_buf))  //ls_buf   100.1600P00.0000
                    {
                        int status = Convert.ToInt32(ls_buf.Substring(0, 1));
                        string[] weight = System.Text.RegularExpressions.Regex.Split(ls_buf.Substring(1), fixSeparator);
                        result.WhetherStable = (status == 1 || status == 3 || status == 5 || status == 7);
                        result.NetWeight = Convert.ToDecimal(weight[0]) * 1000;
                        result.TareWeight = Convert.ToDecimal(weight[1]) * 1000;
                        result.TotalWeight = result.NetWeight + result.TareWeight;
                        result.WhetherSuccess = true;
                    }
                    else// 首字节为 0x00 时，因 0x00 表示字符串的结束符而取不到任何信息
                    {
                        result.WhetherSuccess = false;
                    }
                }
                else
                {
                    result.WhetherSuccess = false;
                    result.Message = "获取重量异常:" + li_ret;
                }
                return result;
            }
            catch (Exception ex)
            {
                result.WhetherSuccess = false;
                result.Message = "异常" + ex.Message;
                return result;
            }
        }

        public override ScaleResult SetTare()
        {
            ScaleResult result = new ScaleResult();
            result.WhetherSuccess = false;

            try
            {
                byte[] buf = Encoding.Default.GetBytes("0");
                int li_ret = send_tare_stdcall(buf);

                result.WhetherSuccess = li_ret == HS_OK;
                if (li_ret != HS_OK)
                {
                    result.Message = li_ret + "";
                }
                return result;
            }
            catch (Exception ex)
            {
                result.WhetherSuccess = false;
                result.Message = "设置皮重异常" + ex.Message;
                return result;
            }
        }

        public override ScaleResult SetTareByNum(int num)
        {
            ScaleResult result = new ScaleResult();
            result.WhetherSuccess = false;

            try
            {
                byte[] buf = Encoding.Default.GetBytes(Math.Round(num / 1000d, 3) + "");
                int li_ret = send_tare_stdcall(buf);

                result.WhetherSuccess = li_ret == HS_OK;
                if (li_ret != HS_OK)
                {
                    result.Message = li_ret + "";
                }
                return result;
            }
            catch (Exception ex)
            {
                result.WhetherSuccess = false;
                result.Message = "设置皮重异常" + ex.Message;
                return result;
            }
        }

        public override ScaleResult SetZero()
        {
            ScaleResult result = new ScaleResult();
            result.WhetherSuccess = false;

            try
            {
                int li_ret = send_zero_stdcall();
                result.WhetherSuccess = li_ret == HS_OK;
                if (li_ret != HS_OK)
                {
                    result.Message = li_ret + "";
                }
                return result;
            }
            catch (Exception ex)
            {
                result.WhetherSuccess = false;
                result.Message = "清零异常" + ex.Message;
                return result;
            }
        }


        public override ScaleResult ClearTare()
        {
            ScaleResult result = new ScaleResult();
            result.WhetherSuccess = false;
            try
            {
                byte[] buf = Encoding.Default.GetBytes("0");
                int li_ret = clear_tare_stdcall(buf);
                result.WhetherSuccess = li_ret == HS_OK;
                if (li_ret != HS_OK)
                {
                    result.Message = li_ret + "";
                }
                return result;
            }
            catch (Exception ex)
            {
                result.WhetherSuccess = false;
                result.Message = "清皮异常" + ex.Message;
                return result;
            }
        }
    }
}