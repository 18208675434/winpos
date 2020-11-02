using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.ScaleFactory
{
   
   public class ScaleGlobalHelper
    {
       private static Scale_Action scaleaction = null;

       public static ScaleType currentscaletype = ScaleType.未指定;

       public static ScaleResult CurrentScaleResult = null;

       #region 异步获取重量 全局用
       private static  System.ComponentModel.BackgroundWorker bk;// = new System.ComponentModel.BackgroundWorker();

       public static void BeginReadWeight()
       {
           //顶尖os2xapi开启端口 放到异步无效***
           IniScale(true);
           if (bk != null)
           {
               bk.Dispose();
           }

           bk = new System.ComponentModel.BackgroundWorker();
           bk.DoWork += backgroundWorker_DoWork;
           bk.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;

           bk.RunWorkerAsync();
       }

       private static  void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
       {
           try
           {
               IniScale(false);

               if (AscalOK())
               {
                   CurrentScaleResult = scaleaction.GetScaleWeight();

                   if (!CurrentScaleResult.WhetherSuccess)
                   {
                       CurrentScaleResult.NetWeight = (decimal)0.000;
                       CurrentScaleResult.TareWeight = (decimal)0.000;
                       CurrentScaleResult.TotalWeight = (decimal)0.000;
                   }
               }
               System.Threading.Thread.Sleep(150);
           }
           catch { }
       }

       private static  void backgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
       {
           bk.RunWorkerAsync();
       }
       #endregion

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
                       scaleaction = new Scale_Wintec();
                   }
                   else if (ScaleName == ScaleType.爱宝.ToString())
                   {
                       currentscaletype = ScaleType.爱宝;
                       scaleaction = new Scale_Aclas();
                   }
                   else if (ScaleName == ScaleType.托利多.ToString())
                   {
                       currentscaletype = ScaleType.托利多;
                       scaleaction = new Scale_Toledo();
                   }
                   else if (ScaleName == ScaleType.易捷通.ToString())
                   {
                       currentscaletype = ScaleType.易捷通;
                       scaleaction = new Scale_Aclas_PS1X();
                   }
                   else if (ScaleName == ScaleType.易衡.ToString())
                   {
                       currentscaletype = ScaleType.易衡;
                       scaleaction = new Scale_EH200();
                   }
                    else if (ScaleName == ScaleType.龙飞.ToString())
                    {
                        currentscaletype = ScaleType.龙飞;
                        scaleaction = new Scale_LF();
                    }

                    Action aa =   new Action(() =>
                  {
                      scaleaction.Open(ComNo, Baud);
                  });

                aa.Invoke();
               }
           }
           catch (Exception ex)
           {
               LogManager.WriteLog("电子秤初始化异常" + ex.Message);
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
              // IniScale(false);
               scaleaction = null;
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
           //if (currentscaletype != ScaleType.爱宝)
           //{
           //    return CurrentScaleResult;
           //}
           return CurrentScaleResult;
           //try
           //{
           //    IniScale(false);

           //    if (!AscalOK())
           //    {
           //        return null;
           //    }

           //    return scaleaction.GetScaleWeight();
           //}
           //catch (Exception ex)
           //{
           //    return null;
           //    LogManager.WriteLog("获取重量异常" + ex.Message);
           //}
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
       //爱宝电子秤未连接成功  或者没有电子秤 读取重量会直接退出  此函数判断是否爱宝电子秤或者是否打开
       private static bool AscalOK()
       {
           try
           {
               if (currentscaletype != ScaleType.爱宝)
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

       private static void IniSetType()
       {
           try
           {
              string setted =  INIManager.GetIni("Scale", "Setted", MainModel.IniPath);

              if (string.IsNullOrEmpty(setted))
              {
                  
              }
           }
           catch (Exception ex)
           {

           }
       }
    }

    public enum ScaleType{
        未指定,
        中科英泰,
        托利多,
        爱宝,
        易捷通,
        易衡,
        龙飞
    }
}
