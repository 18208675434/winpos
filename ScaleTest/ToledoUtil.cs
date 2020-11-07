using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ScaleTest
{
    public class ToledoUtil
    {
        [DllImport("pos_ad_dll.dll", CharSet = CharSet.Ansi, CallingConvention =CallingConvention.StdCall)]
        public static extern int read_standard_stdcall(byte[] str);
        /// <summary>
        /// 获取秤称重信息
        /// </summary>
        /// <returns></returns>
        public static ToledoResult ReadStdcall()
        {
            ToledoResult result = new ToledoResult();
            result.WhetherSuccess = false;

            int li_ret = 0;
            byte[] p = new byte[32];
            string s = "";
            try
            {
                li_ret = read_standard_stdcall(p);

                result.ResultCode = li_ret;

                /*
                * HS_OK 成功
                * -1 欠载
                * -2 过载
                */
                switch (li_ret)
                {
                    case -1:
                        {
                            result.WhetherSuccess = false;
                            result.Message = "欠载";
                            break;
                        }
                    case -2:
                        {
                            result.WhetherSuccess = false;
                            result.Message = "过载";
                            break;
                        }
                    case 0xF0:
                        {
                            result.WhetherSuccess = true;
                            //status 的 bit0(第一位)表示是否稳定，如为 1 则表示稳定
                            //status 的 bit1(第二位)表示是否在零位，如为 1 则表示零位
                            //status 的 bit2(第三位)表示是否有皮重，如为 1 则表示有皮重
                            byte status = p[0];
                            result.WhetherStable = (status & 0x01) == 0x01;
                            result.WhetherZero = (status & 0x02) == 0x02;
                            result.WhetherTare = (status & 0x04) == 0x04;

                            string strweight = Encoding.Default.GetString(p, 1, p.Length - 1);

                            result.NetWeight = Math.Round(Convert.ToDecimal(strweight.Split('P')[0]), 3);
                            result.TareWeight = Math.Round(Convert.ToDecimal(strweight.Split('P')[1]), 3);
                            result.TotalWeight = result.NetWeight + result.TareWeight;
                            break;
                        }
                    default:
                        {
                            result.WhetherSuccess = false;
                            result.Message = "失败";
                            break;
                        }
                }

                return result;
            }
            catch (Exception ex)
            {
                //LogManager.WriteLog("TOLEDO","读取秤信息异常" + ex.Message);
                result.WhetherSuccess = false;
                result.Message = "异常"+ex.Message;
                return result;
            }

        }


        [DllImport("pos_ad_dll.dll", CharSet = CharSet.Ansi, CallingConvention =CallingConvention.StdCall)]
        public static extern int send_pre_tare(byte[] str);
        /// <summary>
        /// 设置预置皮重，根据传入的皮重参数进行去皮（需要转换为KG单位）
        /// </summary>
        /// <param name="weight">单位：g</param>
        /// <returns></returns>
        public static ToledoResult SendPreTare(int weight)
        {
            ToledoResult result = new ToledoResult();
            result.WhetherSuccess = false;

            try
            {
                string tempweight = Math.Round((decimal)weight / 1000,3).ToString().PadLeft(6,'0');
               
                byte[] byteArray = System.Text.Encoding.Default.GetBytes(tempweight);
                int resultcode = send_pre_tare(byteArray);
                result.ResultCode = resultcode;
                switch (resultcode)
                {
                    case 1: result.WhetherSuccess = false; result.Message = "不合法规禁止操作"; break;
                    case 2: result.WhetherSuccess = false; result.Message = "欠载"; break;
                    case 3: result.WhetherSuccess = false; result.Message = "过载超出去皮范围"; break;
                    case 4: result.WhetherSuccess = false; result.Message = "参数异常"; break;
                    case 5: result.WhetherSuccess = false; result.Message = "数据处理异常"; break;
                    case -1: result.WhetherSuccess = false; result.Message = "IDNET_Service 与串口通讯异常"; break;
                    case -2: result.WhetherSuccess = false; result.Message = "IDNET_Service 与数传通讯异常"; break;
                    case -3: result.WhetherSuccess = false; result.Message = "IDNET_Service 与数传语法、逻辑/无法执行、内部函数错误"; break;
                    case -8: result.WhetherSuccess = false; result.Message = "与 IDNET_Service 连接失败"; break;
                    case -9: result.WhetherSuccess = false; result.Message = "与 IDNET_Service 数据通讯错误"; break;
                    case 240: result.WhetherSuccess = true; result.Message = "成功"; break;
                    default: result.WhetherSuccess = false; result.Message = "未知错误"; break;
                }


                return result;
            }
            catch (Exception ex)
            {
                result.WhetherSuccess = false;
                result.Message = "设置预置皮重异常"+ex.Message;
                return result;
            }
        }



        [DllImport("pos_ad_dll.dll", CharSet = CharSet.Ansi, CallingConvention =CallingConvention.StdCall)]
        public static extern int set_tare_bykey(byte[] str);
        /// <summary>
        /// 按键去皮，根据秤盘上当前重量进行自动去皮
        /// </summary>
        /// <param name="weight">单位：g</param>
        /// <returns></returns>
        public static ToledoResult SendTareByKey()
        {
            ToledoResult result = new ToledoResult();
            result.WhetherSuccess = false;

            try
            {
                byte[] p = new byte[128];
                int resultcode = set_tare_bykey(p);
                result.ResultCode = resultcode;
                
                //txtTareBykey.Text = Encoding.Default.GetString(p, 1, p.Length - 1);

               
                switch (resultcode)
                {
                    case 1: result.WhetherSuccess = false; result.Message = "不合法规禁止操作"; break;
                    case 2: result.WhetherSuccess = false; result.Message = "欠载"; break;
                    case 3: result.WhetherSuccess = false; result.Message = "过载超出去皮范围"; break;
                    case 4: result.WhetherSuccess = false; result.Message = "参数异常"; break;
                    case 5: result.WhetherSuccess = false; result.Message = "数据处理异常"; break;
                    case -1: result.WhetherSuccess = false; result.Message = "IDNET_Service 与串口通讯异常"; break;
                    case -2: result.WhetherSuccess = false; result.Message = "IDNET_Service 与数传通讯异常"; break;
                    case -3: result.WhetherSuccess = false; result.Message = "IDNET_Service 与数传语法、逻辑/无法执行、内部函数错误"; break;
                    case -8: result.WhetherSuccess = false; result.Message = "与 IDNET_Service 连接失败"; break;
                    case -9: result.WhetherSuccess = false; result.Message = "与 IDNET_Service 数据通讯错误"; break;
                    case 240: result.WhetherSuccess = true; result.Message = "成功"; result.TareWeight = Convert.ToDecimal(Encoding.Default.GetString(p, 1, p.Length - 1)); break;
                    default: result.WhetherSuccess = false; result.Message = "未知错误"; break;
                }


                return result;
            }
            catch (Exception ex)
            {
                result.WhetherSuccess = false;
                result.Message = "设置预置皮重异常" + ex.Message;
                return result;
            }
        }


        [DllImport("pos_ad_dll.dll", CharSet = CharSet.Ansi, CallingConvention =CallingConvention.StdCall)]
        public static extern int send_zero();
        /// <summary>
        /// 清零
        /// </summary>
        /// <param name="weight">单位：g</param>
        /// <returns></returns>
        public static ToledoResult SendZero()
        {
            ToledoResult result = new ToledoResult();
            result.WhetherSuccess = false;

            try
            {
                int resultcode = send_zero();
                result.ResultCode = resultcode;

                switch (resultcode)
                {
                    case 1: result.WhetherSuccess = false; result.Message = "非稳定状态且处于非欠载状态禁止清零"; break;
                    case 2: result.WhetherSuccess = false; result.Message = "负载超出去皮范围"; break;
                    case 3: result.WhetherSuccess = false; result.Message = "过载超出去皮范围"; break;                 
                    case -1: result.WhetherSuccess = false; result.Message = "异常"; break;
                    case -2: result.WhetherSuccess = false; result.Message = "数传通讯异常"; break;
                    case -3: result.WhetherSuccess = false; result.Message = "数传内：语法、逻辑/无法执行、内部函数错误"; break;                   
                    case 240: result.WhetherSuccess = true; result.Message = "成功"; break;
                    default: result.WhetherSuccess = false; result.Message = "未知错误"; break;
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



    }

    public class ToledoResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool WhetherSuccess { get; set; }

        /// <summary>
        /// 是否稳定
        /// </summary>
        public bool WhetherStable { get; set; }
        /// <summary>
        /// 是否零位
        /// </summary>
        public bool WhetherZero { get; set; }
        /// <summary>
        /// 是否有皮重
        /// </summary>
        public bool WhetherTare { get; set; }
        /// <summary>
        /// 返回code
        /// </summary>
        public int ResultCode { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 总重量
        /// </summary>
        public decimal TotalWeight { get; set; }
        /// <summary>
        /// 皮重
        /// </summary>
        public decimal TareWeight { get; set; }
        /// <summary>
        /// 净重
        /// </summary>
        public decimal NetWeight { get; set; }
    }
}