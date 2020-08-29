using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MenuUI
{
    public class MenuHelper
    {
        //<summary>
        //按比例缩放页面及控件
        //</summary>
        private static AutoSizeFormUtil asf = new AutoSizeFormUtil();


        public delegate void DataRecHandleDelegate(ToolType tooltype);
        /// <summary>
        /// 数据接收事件
        /// </summary>
        public event DataRecHandleDelegate DataReceiveHandle;

        private static FormToolMainScale frmtoolmainscale = null;

        public static void IniFormToolMainScale()
        {
            try
            {
                if (frmtoolmainscale != null)
                {
                    try
                    {
                        frmtoolmainscale.Dispose();
                    }
                    catch { }                  
                }

                frmtoolmainscale = new FormToolMainScale();
                asf.AutoScaleControlTest(frmtoolmainscale, 210, 180, 210 * MainModel.midScale, 480 * MainModel.midScale, true);
                frmtoolmainscale.Location = new System.Drawing.Point(Screen.AllScreens[0].Bounds.Width - frmtoolmainscale.Width - 15, Convert.ToInt16(60 * MainModel.midScale) + 10);
                frmtoolmainscale.TopMost = true;

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("初始化单独秤菜单页面异常"+ex.Message);
            }
        }

        public static void ShowFomrToolMainScale()
        {
            try
            {
                if (frmtoolmainscale == null)
                {
                    IniFormToolMainScale();
                }

                frmtoolmainscale.Show();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("显示单独秤菜单页面异常" + ex.Message);
            }
        }

        #region 订单查询
        public static void ShowFormOrderHangItem(Cart cart)
        {

            try
            {

                BackHelper.ShowFormBackGround();
                FormOrderHangItem frmitem = new FormOrderHangItem(cart);

                asf.AutoScaleControlTest(frmitem, 700, 500, 700 * MainModel.midScale, 500 * MainModel.midScale, true);
                frmitem.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmitem.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmitem.Height) / 2);
                    frmitem.TopMost = true;

                
                    frmitem.ShowDialog();

                    frmitem.Dispose();
                    Application.DoEvents();
                    BackHelper.HideFormBackGround();
            }
            catch
            {

            }

        }


        public static bool ShowFormRefundByAmt(Order order)
        {
            try
            {
                BackHelper.ShowFormBackGround();
                FormRefundByAmt frmitem = new FormRefundByAmt(order);

                asf.AutoScaleControlTest(frmitem, 600, 460, 600 * MainModel.midScale, 460 * MainModel.midScale, true);
                frmitem.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmitem.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmitem.Height) / 2);
                frmitem.TopMost = true;


                frmitem.ShowDialog();

                frmitem.Dispose();
                Application.DoEvents();
                BackHelper.HideFormBackGround();

                return frmitem.DialogResult == DialogResult.OK;
            }
            catch (Exception ex)
            {
                MainModel.ShowLog("指定金额退款窗体异常"+ex.Message,true);
                return false;
            }
        }

        /// <summary>
        /// 组合 退款 文案提示
        /// </summary>
        /// <returns></returns>
        public static string GetTotalPayInfo(Order order)
        {
            try
            {
                decimal AliPayAmt = order.alipayamt;
                decimal BalanceAmt = order.balanceamt;
                decimal CashPayAmt = order.cashpayamt;
                decimal WechatPayAmt = order.wechatpayamt;
                decimal YLPayAmt = order.ylpayamt;
                decimal PointPayAmt = order.pointpayamt;
                decimal CashCouponAmt = order.cashcouponamt;
                decimal OtherPayAmt = order.otherpayamt;

                string totalpay = "";
                if (AliPayAmt > 0)
                {
                    totalpay += "支付宝：" + AliPayAmt + " ";
                }
                if (BalanceAmt > 0)
                {
                    totalpay += "余额：" + BalanceAmt + " ";
                }
                if (CashPayAmt > 0)
                {
                    totalpay += "现金：" + CashPayAmt + " ";
                }
                if (WechatPayAmt > 0)
                {
                    totalpay += "微信：" + WechatPayAmt + " ";
                }
                if (YLPayAmt > 0)
                {
                    totalpay += "银联：" + YLPayAmt + " ";
                }

                if (PointPayAmt > 0)
                {
                    totalpay += "积分：" + PointPayAmt + " ";
                }

                if (CashCouponAmt > 0)
                {
                    totalpay += "代金券：" + CashCouponAmt + " ";
                }

                if (order.otherpaydetailinfos != null && order.otherpaydetailinfos.Count > 0)
                {
                    foreach (OtherPayDetailInfo otherpay in order.otherpaydetailinfos)
                    {
                        totalpay += otherpay.type + "：" + otherpay.amount + " ";
                    }
                }

                return totalpay;
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region  调价记录

        public static void ShowFormAdjustPrice()
        {
            try
            {
                FormAdjustRecord frmadjust = new FormAdjustRecord();
                asf.AutoScaleControlTest(frmadjust, 1178, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                frmadjust.Location = new System.Drawing.Point(0, 0);
                frmadjust.ShowDialog();
                frmadjust.Dispose();
                Application.DoEvents();

            }
            catch
            {

            }
        }
        #endregion
    }
}
