using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.HelperUI
{
    public class NumberHelper
    {
        //<summary>
        //按比例缩放页面及控件
        //</summary>
        private static AutoSizeFormUtil asf = new AutoSizeFormUtil();
        private static FormNumber frmnumber = null;

        public static void IniFormNumber()
        {
            try
            {
                if (frmnumber != null)
                {
                    try
                    {
                        frmnumber.Close();
                        frmnumber.Dispose();
                    }
                    catch (Exception ex)
                    {
                    }                   
                }

                frmnumber = new FormNumber();
                asf.AutoScaleControlTest(frmnumber, 380, 480, 380 * MainModel.midScale, 480 * MainModel.midScale, true);
                frmnumber.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmnumber.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmnumber.Height) / 2);
                frmnumber.TopMost = true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("初始化确认弹窗异常" + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="numbertype">数字类型</param>
        /// <param name="Right">是否居右  默认居中</param>
        /// <returns></returns>
        public static string ShowFormNumber(string title,NumberType numbertype,bool Right = false)
        {
              try
            {
                BackHelper.ShowFormBackGround();
                if (frmnumber == null || frmnumber.IsDisposed)
                {
                    IniFormNumber();
                }
                if (Right)
                {
                    frmnumber.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmnumber.Width)*95 / 100, (Screen.AllScreens[0].Bounds.Height - frmnumber.Height) / 2);
                }
                else
                {
                    frmnumber.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmnumber.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmnumber.Height) / 2);
                }

                frmnumber.UpInfo(title,numbertype);
                frmnumber.ShowDialog();

                BackHelper.HideFormBackGround();
                Application.DoEvents();
                return frmnumber.NumberValue;
            }
            catch (Exception ex)
            {
                BackHelper.HideFormBackGround();
                frmnumber = null;
                LogManager.WriteLog("数字弹窗出现异常"+ex.Message);
                return "";
            }
        }
    }


    public enum NumberType
    {
        None,
        /// <summary>
        /// 条码
        /// </summary>
        BarCode,
        /// <summary>
        /// 会员号
        /// </summary>
        MemberCode,
        /// <summary>
        /// 商品重量
        /// </summary>
        ProWeight,
        /// <summary>
        /// 称重
        /// </summary>
        TareWeight,
        /// <summary>
        /// 商品数量 
        /// </summary>
        ProNum,
        /// <summary>
        /// 礼品卡卡号
        /// </summary>
        GiftCardNo,
        /// <summary>
        /// 绑定会员
        /// </summary>
        BindingMember,
        /// <summary>
        /// 礼品卡秘钥
        /// </summary>
        GiftCardPwd,
        /// <summary>
        /// 绑定实体卡
        /// </summary>
        BindingEntryCard
    }
}
