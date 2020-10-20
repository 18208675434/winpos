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
    public class Scale_Aclas_PS1X : Scale_Action
    {
        [DllImport("PS1DLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern void CreateComm();

        [DllImport("PS1DLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern void destoryComm();

        [DllImport("PS1DLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern bool openport(string ICOM, int Ibaud);

        [DllImport("PS1DLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern bool closeport();

        [DllImport("PS1DLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern string GetWeight();

        [DllImport("PS1DLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern string GetAllValue();

        [DllImport("PS1DLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern bool Peelskin();

        [DllImport("PS1DLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClearZero();



        private bool WhetherCreate = false;

        private bool WhetherDestory = false;

        public override bool Open(string connum, int baud)
        {
            try
            {
                if (!WhetherCreate) //初始化资源重复调用会导致不能用 
                {
                    CreateComm();
                    WhetherCreate = true;

                }

                bool result = openport(connum, baud);
                return result;

            }
            catch (Exception ex)
            {
                return false;
            }
            //throw new NotImplementedException();
        }

        public override bool Close()
        {
            try
            {
                closeport();  //关闭成功返回false  所以不判断返回值 直接返回true
                // throw new NotImplementedException();
                //清理资源

                if (!WhetherDestory) //初始化资源重复调用会导致不能用 
                {
                    destoryComm();
                    WhetherDestory = true;

                }
                //destoryComm();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
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
                string weight = GetWeight();

               // LogManager.WriteLog("weight",weight);

                if (string.IsNullOrEmpty(weight))
                {
                    return result;
                }
                //Delay.Start(50); //没有稳定标识 通过实践判断 两次读取一直标志位稳定
                //string weight2 = GetWeight();

                //result.WhetherStable = weight == weight2;

                result.WhetherStable = true;
                weight = weight.Replace("k", "").Replace("g", "").Replace("K", "").Replace("G", "").Replace(" ", "").Trim();  //易捷通 返回信息有KG 需要过滤   重量为负值时 -号后面有空格也需要过滤
                result.NetWeight = Math.Round(Convert.ToDecimal(weight), 3);
                result.TareWeight = 0; //无皮重信息 默认给0值
                result.TotalWeight = result.NetWeight + result.TareWeight;
       
                lastweight = result.NetWeight;

                result.WhetherSuccess = true;
                return result;
            }
            catch (Exception ex)
            {
                result.WhetherSuccess = false;
                result.Message = "异常" + ex.Message;
                //LogManager.WriteLog("weight",ex.Message);
                return result;
            }
        }

        public override ScaleResult SetTare()
        {
            ScaleResult result = new ScaleResult();
            result.WhetherSuccess = false;

            try
            {

                result.WhetherSuccess = Peelskin();
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
            result.Message = "爱宝设备无此功能";
            return result;
        }

        public override ScaleResult SetZero()
        {
            ScaleResult result = new ScaleResult();
            result.WhetherSuccess = false;

            try
            {
                result.WhetherSuccess = ClearZero();
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
            result.Message = "爱宝设备无此功能";
            return result;
        }


    }
}
