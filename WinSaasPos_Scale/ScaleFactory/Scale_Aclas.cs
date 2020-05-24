using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace WinSaasPOS_Scale.ScaleFactory
{
    /// <summary>
    /// 顶尖电子秤
    /// </summary>
    public class Scale_Aclas : Scale_Action
    {

        [DllImport("SensorDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]

        public static extern bool __Open(string CommName, int BaudRate);

        public override bool Open(string connum, int baud)
        {
            try
            {
                return __Open(connum,baud);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [DllImport("SensorDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]

        public static extern bool __Close();
        public override bool Close()
        {
            try
            {
                return __Close();
            }
            catch (Exception ex)
            {
                return false;
            }
        }




        //不能用string 接收   否则没有连接秤的情况下会
        [DllImport("SensorDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern string __GetWeight();
        public override ScaleResult GetWeight()
        {
            ScaleResult result = new ScaleResult();
            result.WhetherSuccess = false;

            try
            {
                string strresult = __GetWeight().ToString();
                string[] strs = strresult.Split(',');

                result.WhetherStable = strs[0] == "S";

                decimal weight = 0;
                string strweight= strs[1]+strs[2];
                if (decimal.TryParse(strweight, out weight))
                {
                    result.WhetherSuccess = true;
                    result.NetWeight = Math.Round(weight, 3); 
                    result.TareWeight = 0.000M;
                    result.TotalWeight = Math.Round(weight, 3);
                }
                else
                {
                    result.WhetherSuccess = false;
                }

               
                return result;
            }
            catch (Exception ex)
            {
                result.WhetherSuccess = false;
                result.Message = "获取重量异常" + ex.Message;

                return result;
            }
        }
       


        [DllImport("SensorDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]

        public static extern void __udeTare();

        public override ScaleResult SetTare()
        {
            ScaleResult result = new ScaleResult();
            result.WhetherSuccess = false;

            try
            {

                __udeTare();
                result.WhetherSuccess = true;
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
            result.Message = "顶尖设备无此功能";
            return result;
        }


        [DllImport("SensorDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]

        public static extern void __uCleart();
        public override ScaleResult SetZero()
        {
            ScaleResult result = new ScaleResult();
            result.WhetherSuccess = false;

            try
            {
                __uCleart();
                result.WhetherSuccess = true;
                return result;
            }
            catch (Exception ex)
            {
                result.WhetherSuccess = false;
                result.Message = "设置皮重异常" + ex.Message;
                return result;
            }
        }


        public override ScaleResult ClearTare()
        {
            ScaleResult result = new ScaleResult();
            result.WhetherSuccess = false;
            result.Message = "顶尖设备无此功能";
            return result;
        }


    }
}
