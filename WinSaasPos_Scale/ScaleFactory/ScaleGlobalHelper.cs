using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using WinSaasPOS_Scale.Common;
using WinSaasPOS_Scale.Model;

namespace WinSaasPOS_Scale.ScaleFactory
{
   
   public class ScaleGlobalHelper
    {
       private static Scale_Action scaleaction = null;

       private static ScaleType currentscaletype = ScaleType.未指定;

//       [Scale]
//ComNo=COM1
//Baud=9600
//ScaleName=WINTEC
       public static void IniScale(bool needrefresh)
       {
           try
           {
               if (scaleaction == null || needrefresh)
               {
                   string ComNo = INIManager.GetIni("Scale", "ComNo", MainModel.IniPath);
                   Int32 Baud = Convert.ToInt32(INIManager.GetIni("Scale", "Baud", MainModel.IniPath));
                   string ScaleName = INIManager.GetIni("Scale", "ScaleName", MainModel.IniPath);

                   if (scaleaction != null)
                   {
                       scaleaction.Close();
                   }

                   if (ScaleName == ScaleType.中科英泰.ToString())
                   {
                       currentscaletype = ScaleType.中科英泰;
                       scaleaction = new Scale_Toledo();
                   }
                   else if (ScaleName == ScaleType.顶尖.ToString())
                   {
                       currentscaletype = ScaleType.顶尖;
                       scaleaction = new Scale_Aclas();
                   }

                   scaleaction.Open(ComNo, Baud);
                   //if (ScaleName == ScaleType.顶尖.ToString())
                   //{                      
                   //}
               }
           }
           catch (Exception ex)
           {
               LogManager.WriteLog("电子秤初始化异常" + ex.Message);
           }
       }
       public ScaleGlobalHelper(ScaleType scaletype)
       {
           try
           {
               switch (scaletype)
               {
                   case ScaleType.中科英泰: scaleaction = new Scale_Toledo(); break;
                   case ScaleType.顶尖: scaleaction = new Scale_Aclas(); break;
                   default: MainModel.ShowLog("未匹配到电子秤", true); break;
               }
           }
           catch (Exception ex)
           {
               LogManager.WriteLog("电子秤初始化异常"+ex.Message);
           }
       }


       /// <summary>
       /// 打开电子秤
       /// </summary>
       /// <param name="connum">串口号</param>
       /// <param name="baud">波特率 9600</param>
       /// <returns></returns>
       public static bool Open(string connum, int baud)
       {
           try
           {
               IniScale(false);
               return scaleaction.Open(connum, baud);
           }
           catch (Exception ex)
           {
               return false;
               LogManager.WriteLog("打开电子秤异常"+ex.Message);
           }
           
       }

       /// <summary>
       /// 关闭电子秤
       /// </summary>
       /// <returns></returns>
       public static  bool Close()
       {
           try
           {
               IniScale(false);
               return scaleaction.Close();
           }
           catch (Exception ex)
           {
               return false;
               LogManager.WriteLog("关闭电子秤异常" + ex.Message);
           }           
       }

       /// <summary>
       /// 获取重量
       /// </summary>
       /// <returns></returns>
       public static  ScaleResult GetWeight()
       {
           try
           {
               IniScale(false);

               if (!AscalOK())
               {
                   return null;
               }
               return scaleaction.GetWeight();
           }
           catch (Exception ex)
           {
               return null;
               LogManager.WriteLog("获取重量异常" + ex.Message);
           }
       }

       /// <summary>
       /// 清皮
       /// </summary>
       /// <returns></returns>
       public static  ScaleResult ClearTare()
       {

           try
           {
               IniScale(false);
               return scaleaction.ClearTare();
           }
           catch (Exception ex)
           {
               return null;
               LogManager.WriteLog("清皮异常" + ex.Message);
           }
       }

       /// <summary>
       /// 数字去皮
       /// </summary>
       /// <param name="num"> 去皮数字 单位是g  需要转换成kg</param>
       /// <returns></returns>
       public static  ScaleResult SetTareByNum(int num)
       {
           try
           {
               IniScale(false);
               return scaleaction.SetTareByNum(num);
           }
           catch (Exception ex)
           {
               return null;
               LogManager.WriteLog("数字去皮异常" + ex.Message);
           }
       }

       /// <summary>
       /// 称重去皮
       /// </summary>
       /// <returns></returns>
       public static  ScaleResult SetTare()
       {

           try
           {
               IniScale(false);
               return scaleaction.SetTare();
           }
           catch (Exception ex)
           {
               return null;
               LogManager.WriteLog("称重去皮异常" + ex.Message);
           }
       }

       /// <summary>
       /// 置零
       /// </summary>
       /// <returns></returns>
       public static  ScaleResult SetZero()
       {

           try
           {
               IniScale(false);
               return scaleaction.SetZero();
           }
           catch (Exception ex)
           {
               return null;
               LogManager.WriteLog("置零异常" + ex.Message);
           }
       }

       //不能用string 接收   否则没有连接秤的情况下会
       [DllImport("SensorDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
       private static extern byte __GetWeight();
       //顶尖电子秤未连接成功  或者没有电子秤 读取重量会直接退出  此函数判断是否顶尖电子秤或者是否打开
       private static bool AscalOK()
       {
           try
           {

               if (currentscaletype != ScaleType.顶尖)
               {
                   return true;
               }
               if (__GetWeight().ToString() != "37")
               {
                   return true;
               }
               else
               {
                   return false;
               }
           }
           catch
           {
               return false;
           }
       }


    }

    public enum ScaleType{
        未指定,
        中科英泰,
        顶尖
    }
}
