using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScaleTest
{
    public abstract class Scale_Action
    {
        /// <summary>
        /// 打开电子秤
        /// </summary>
        /// <param name="connum">串口号</param>
        /// <param name="baud">波特率 9600</param>
        /// <returns></returns>
        public abstract bool Open(string connum,int baud);

        /// <summary>
        /// 关闭电子秤
        /// </summary>
        /// <returns></returns>
        public abstract bool Close();


        /// <summary>
        /// 获取重量
        /// </summary>
        /// <returns></returns>
        public abstract ScaleResult GetScaleWeight();


        /// <summary>
        /// 清皮
        /// </summary>
        /// <returns></returns>
        public abstract ScaleResult ClearTare();

       /// <summary>
       /// 数字去皮
       /// </summary>
       /// <param name="num"> 去皮数字 单位是g  需要转换成kg</param>
       /// <returns></returns>
        public abstract ScaleResult SetTareByNum(int  num);

        /// <summary>
        /// 称重去皮
        /// </summary>
        /// <returns></returns>
        public abstract ScaleResult SetTare();

        /// <summary>
        /// 置零
        /// </summary>
        /// <returns></returns>
        public abstract ScaleResult SetZero();

    }
}
