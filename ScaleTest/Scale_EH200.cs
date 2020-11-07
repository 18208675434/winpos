using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace ScaleTest
{
    public class Scale_EH200
    {

        static SerialPort ComScale = null;

        static byte[] CmdWeigth = new byte[] { 0x02, 0x81, 0x30, 0x30, 0x30, 0x03, 0x31, 0x41 };
        static byte[] CmdSetTare = new byte[] { 0x02, 0x81, 0x32, 0x30, 0x30, 0x03, 0x31, 0x42 };
        static byte[] CmdSetZero = new byte[] { 0x02, 0x81, 0x32, 0x30, 0x30, 0x03, 0x31, 0x43 };

        public  bool Open(string connum, int baud)
        {
            try
            {
                if (ComScale != null)
                {
                    Close();
                }
                ComScale = new SerialPort();

                bool openresult = SerialPortOperation.InitSerialPort(ComScale, connum, baud.ToString(), "8", "1", "None", "None");

                //LogManager.WriteLog("打开串口" + comNo + baud + ":" + openresult);
                return openresult;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public  bool Close()
        {
            try
            {
                ComScale.Dispose();
                ComScale.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public  ScaleResult GetScaleWeight()
        {
            ScaleResult result = new ScaleResult();
            result.WhetherSuccess = false;

            try
            {
                ComScale.Write(CmdWeigth,0,CmdWeigth.Length);



                int len = ComScale.BytesToRead;



                Byte[] readBuffer = new Byte[len];

                ComScale.Read(readBuffer, 0, len);

                Byte[] ThisByte = new Byte[6];
                Buffer.BlockCopy(readBuffer, 0, ThisByte, 5, 6);

                string data = ComScale.ReadExisting();

                result.WhetherSuccess = true;
                result.Message = data;

                return result;
            }
            catch (Exception ex)
            {
                result.WhetherSuccess = false;
                result.Message = "获取重量异常" + ex.Message;

                return result;
            }
        }




        public  ScaleResult SetTare()
        {
            ScaleResult result = new ScaleResult();
            result.WhetherSuccess = false;

            try
            {

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

        public  ScaleResult SetTareByNum(int num)
        {
            ScaleResult result = new ScaleResult();
            result.WhetherSuccess = false;
            result.Message = "爱宝设备无此功能";
            return result;
        }

        public  ScaleResult SetZero()
        {
            ScaleResult result = new ScaleResult();
            result.WhetherSuccess = false;

            try
            {
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


        public  ScaleResult ClearTare()
        {
            ScaleResult result = new ScaleResult();
            result.WhetherSuccess = false;
            result.Message = "爱宝设备无此功能";
            return result;
        }

        public static void SendCmd()
        {
            byte[] by = new byte[] { 0x12 };
        }

    }

    public class ScaleResult
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




    /// <summary>
    /// 串口操作类
    /// </summary>
    public class SerialPortOperation
    {
        /// <summary>
        /// 初始化串口
        /// </summary>
        /// <param name="ComProt">串口名称</param>
        /// <param name="strComNumber">设置串口号</param>
        /// <param name="strBaud">波特率</param>
        /// <param name="strDataBits">数据位</param>
        /// <param name="strStopBits">停止位</param>
        /// <param name="strParity">校验位</param>
        /// <param name="strHWFlow">流控制</param>
        /// <returns>设置成功返回 ＴＲＵＥ</returns>
        public static bool InitSerialPort(SerialPort ComProt, string strComNumber, string strBaud, string strDataBits,
                                   string strStopBits, string strParity, string strHWFlow)
        {

            try
            {
                if (ComProt.IsOpen)
                {
                    ComProt.Close();
                }

                if (strComNumber.ToUpper().Contains("COM"))
                {
                    ComProt.PortName = strComNumber.Trim().ToUpper();
                }
                else
                {
                    ComProt.PortName = "COM" + strComNumber.Trim();
                }

                ComProt.BaudRate = Convert.ToInt32(strBaud);
                ComProt.DataBits = Convert.ToInt32(strDataBits);

                if (strStopBits.Trim() == "2")
                {
                    ComProt.StopBits = StopBits.Two;
                }
                else if (strStopBits.Trim() == "1")
                {
                    ComProt.StopBits = StopBits.One;
                }
                else if (strStopBits.Trim() == "1.5")
                {
                    ComProt.StopBits = StopBits.OnePointFive;
                }
                else
                {
                    ComProt.StopBits = StopBits.None;
                }

                if (strParity.Trim().ToUpper() == "EVEN")
                {
                    ComProt.Parity = Parity.Even;
                }
                else if (strParity.Trim().ToUpper() == "MARK")
                {
                    ComProt.Parity = Parity.Mark;
                }
                else if (strParity.Trim().ToUpper() == "ODD")
                {
                    ComProt.Parity = Parity.Odd;
                }
                else if (strParity.Trim().ToUpper() == "SPACE")
                {
                    ComProt.Parity = Parity.Space;
                }
                else
                {
                    ComProt.Parity = Parity.None;
                }

                if (strHWFlow.ToUpper().Contains("RTS"))
                {
                    ComProt.RtsEnable = true;
                }

                if (strHWFlow.ToUpper().Contains("DTR"))
                {
                    ComProt.DtrEnable = true;
                }

                if (!ComProt.IsOpen)
                {
                    ComProt.Open();
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }


}
