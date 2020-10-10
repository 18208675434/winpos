using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.BrokenUI.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.PayUI;

namespace ZhuiZhi_Integral_Scale_UncleFruit.BatchSaleCardUI
{
    public class BatchSaleCardHelper
    {
        //<summary>
        //按比例缩放页面及控件
        //</summary>
        private static AutoSizeFormUtil asf = new AutoSizeFormUtil();
        private static FormBatchSaleCardCreate formBatchSaleCardCreate = null;

        public static void IniFormBatchSaleCardCreate()
        {
            try
            {
                if (formBatchSaleCardCreate != null)
                {
                    try
                    {
                        formBatchSaleCardCreate.Dispose();
                    }
                    catch { }
                }
                formBatchSaleCardCreate = new FormBatchSaleCardCreate();
                asf.AutoScaleControlTest(formBatchSaleCardCreate, 1180, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
                formBatchSaleCardCreate.Location = new System.Drawing.Point(0, 0);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("初始化报损页面异常" + ex.Message);
            }
        }

        public static void ShowFormBatchSaleCardCreate()
        {
            try
            {
                if (formBatchSaleCardCreate == null || formBatchSaleCardCreate.IsDisposed)
                {
                    IniFormBatchSaleCardCreate();
                }

                formBatchSaleCardCreate.ShowDialog();
                formBatchSaleCardCreate.Dispose();
            }
            catch (Exception ex)
            {
                formBatchSaleCardCreate = null;
                LogManager.WriteLog("显示批量售卡页面异常" + ex.Message);
            }
        }

        /// <summary>
        /// 实体卡卡号 不校验规则和长度
        /// </summary>
        /// <param name="cart"></param>
        /// <returns>返回录入的实体卡卡号</returns>
        public static string ShowFormVoucher()
        {
            try
            {
                BackHelper.ShowFormBackGround();

                FormVoucher frmvoucher = new FormVoucher();
                asf.AutoScaleControlTest(frmvoucher, 430, 240, 430 * MainModel.midScale, 240 * MainModel.midScale, true);
                frmvoucher.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmvoucher.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmvoucher.Height) / 3);
                frmvoucher.TopMost = true;

                frmvoucher.ShowDialog();

                BackHelper.HideFormBackGround();
                frmvoucher.Dispose();

                Application.DoEvents();
                return frmvoucher.CurrentPhone;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("会员号弹窗异常" + ex.Message);
                return "";
            }
        }
    }
}
