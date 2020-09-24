using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.ChangePriceUI
{
    public class ChangePriceHelper
    {
        private static  AutoSizeFormUtil asf = new AutoSizeFormUtil();

        public static AdjustPriceResult ShowFormPricing(Product pro)
        {
            AdjustPriceResult adjustresult = new AdjustPriceResult();
            try
            {
                BackHelper.ShowFormBackGround();

                FormPricing frmpricing = new FormPricing(pro);
                asf.AutoScaleControlTest(frmpricing, 420, 560, 420 * MainModel.midScale, 560 * MainModel.midScale, true);
                frmpricing.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmpricing.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmpricing.Height) / 2);
                frmpricing.TopMost = true;

                if (frmpricing.ShowDialog() == DialogResult.OK)
                {
                    adjustresult.WhetherAdjust = true;
                    adjustresult.adjustpriceinfo = frmpricing.adjustpriceinfo;
                }
                else
                {
                    adjustresult.WhetherAdjust = false;
                    adjustresult.adjustpriceinfo = null ;
                }
                
                frmpricing.Dispose();
                BackHelper.HideFormBackGround();
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                BackHelper.HideFormBackGround();
                LogManager.WriteLog("调价页面出现异常" + ex.Message);

                adjustresult.WhetherAdjust = false;
                adjustresult.adjustpriceinfo = null;
            }

            return adjustresult;
        }

        public static AdjustPriceResult ShowFormDiscount(Product pro)
        {
            AdjustPriceResult adjustresult = new AdjustPriceResult();

            try
            {
                BackHelper.ShowFormBackGround();

                FormDiscount frmdiscount = new FormDiscount(pro);
                asf.AutoScaleControlTest(frmdiscount, 420, 560, 420 * MainModel.midScale, 560 * MainModel.midScale, true);
                frmdiscount.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmdiscount.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmdiscount.Height) / 2);
                frmdiscount.TopMost = true;
                if (frmdiscount.ShowDialog() == DialogResult.OK)
                {
                    adjustresult.WhetherAdjust = true;
                    adjustresult.adjustpriceinfo = frmdiscount.adjustpriceinfo;
                }
                else
                {
                    adjustresult.WhetherAdjust = false;
                    adjustresult.adjustpriceinfo = null;
                }
                frmdiscount.Dispose();
                BackHelper.HideFormBackGround();
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                BackHelper.HideFormBackGround();
                LogManager.WriteLog("折扣页面出现异常" + ex.Message);

                adjustresult.WhetherAdjust = false;
                adjustresult.adjustpriceinfo = null;
            }

            return adjustresult;
        }


        public static decimal ShowFormOrderPricing(decimal beforefixtotal)
        {
            try
            {
               
                FormOrderPricing frmorderpricing = new FormOrderPricing(beforefixtotal);
                asf.AutoScaleControlTest(frmorderpricing, 500, 600, 500 * MainModel.midScale, 600 * MainModel.midScale, true);
                frmorderpricing.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmorderpricing.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmorderpricing.Height) / 2);
                frmorderpricing.TopMost = true;
                 BackHelper.ShowFormBackGround();
                frmorderpricing.ShowDialog();
                frmorderpricing.Dispose();

                Application.DoEvents();
                BackHelper.HideFormBackGround();
                return frmorderpricing.fixpricetotal;
            }
            catch (Exception ex)
            {
                BackHelper.HideFormBackGround();
                LogManager.WriteLog("整单改价页面出现异常" + ex.Message);
                return 0;
            }
        }
    }

    public class AdjustPriceResult
    {
        public bool WhetherAdjust { get; set; }

        public AdjustPriceInfo adjustpriceinfo { get; set; }
    }

    public enum ChangeType
    {
        unitprice,
        totalprice,
        totaldiscount
    }
}
