using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.ScaleFactory
{
    /// <summary>
    /// 顶尖电子秤
    /// </summary>
    public class Scale_EH200 : Scale_Action
    {

        private IntPtr ptr = new IntPtr(1);

        [DllImport("PosWeighInterface.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenCom(IntPtr obj, string port);


        [DllImport("PosWeighInterface.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern bool CloseCom(IntPtr obj);


        [DllImport("PosWeighInterface.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern bool SetTare(IntPtr obj);


        [DllImport("PosWeighInterface.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern bool SetZeno(IntPtr obj);


        [DllImport("PosWeighInterface.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern string CurWeight(IntPtr obj);



        public override bool Open(string connum, int baud)
        {
            try
            {
                Close();
                return OpenCom(ptr, connum);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public override bool Close()
        {
            try
            {
                return CloseCom(ptr);
            }
            catch (Exception ex)
            {
                return false;
            }
        }


//        返回值字符串格式  
//名称	起始位	长度	说明
//稳定位	1	1	1 表示稳定 ；0表示未稳定
//零点位	2	1	1 表示零位；0表示未在零位
//去皮位	3	1	1 表示去皮；0表示未去皮
//重量值	4	6	重量值
//皮重值	10	5	皮重值

        public override ScaleResult GetScaleWeight()
        {
            ScaleResult result = new ScaleResult();
            result.WhetherSuccess = false;

            try
            {
                string strresult = CurWeight(ptr);

                if (string.IsNullOrEmpty(strresult) || strresult.Length < 15)
                {
                    return result;
                }


                result.WhetherStable = strresult.Substring(0, 1) == "1";

                    result.NetWeight =Convert.ToDecimal( strresult.Substring(3,6));
                    result.TareWeight = Convert.ToDecimal(strresult.Substring(9, 5));
                    result.TotalWeight = result.NetWeight + result.TareWeight;

                    result.WhetherSuccess = true;
               
                return result;
            }
            catch (Exception ex)
            {
                result.WhetherSuccess = false;
                result.Message = "获取重量异常" + ex.Message;

                return result;
            }
        }
       



        public override ScaleResult SetTare()
        {
            ScaleResult result = new ScaleResult();
            result.WhetherSuccess = false;

            try
            {


                result.WhetherSuccess = SetTare(ptr);
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


        public override ScaleResult SetZero()
        {
            ScaleResult result = new ScaleResult(); 
            result.WhetherSuccess = false;

            try
            {
                result.WhetherSuccess = SetZeno(ptr);
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
