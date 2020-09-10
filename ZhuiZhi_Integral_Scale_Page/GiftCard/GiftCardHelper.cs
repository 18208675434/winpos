using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.GiftCard.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.GiftCard
{
    public class GiftCardHelper
    {
        //<summary>
        //按比例缩放页面及控件
        //</summary>
        private static AutoSizeFormUtil asf = new AutoSizeFormUtil();

        public static GiftCardHttp gifthttputil = new GiftCardHttp();
        public static bool BindingMember(CardProduct pro,string buyermemberid,string bindingphone)
        {
            try
            {
                string numbervalue = NumberHelper.ShowFormNumber("绑定礼品卡秘钥", NumberType.GiftCardPwd, true);
                if (!string.IsNullOrEmpty(numbervalue))
                {
                    string errormsg = "";
                    if (gifthttputil.ValidatePrepaidCard(pro.cardnumber, numbervalue, ref errormsg))
                    {

                        string errormsgbing = "";

                        BindingMemberPara para = new BindingMemberPara();
                        para.cardnoes = new List<string>() { pro.cardnumber};
                        para.memberid = buyermemberid;
                        para.phone = bindingphone;
                        para.tenantid = MainModel.CurrentShopInfo.tenantid;
                        BindingResult bindingresult =  gifthttputil.BindingMember(para,ref errormsgbing);
                        if (!string.IsNullOrEmpty(errormsgbing) || bindingresult == null)
                        {
                            MainModel.ShowLog(errormsgbing,false);
                            return false;
                        }
                        return true;
                    }
                    else
                    {
                        MainModel.ShowLog(errormsg, false);
                        return false;
                    }

                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("礼品卡绑定会员异常"+ex.Message);
                return false;
            }

        }

        public static bool ShowFormGiftCardByCash(CartAloneUpdate cart,out decimal cash)
        {
            try
            {
                BackHelper.ShowFormBackGround();

                FormGiftCardByCash frmpaybycash = new FormGiftCardByCash(cart);
                asf.AutoScaleControlTest(frmpaybycash, 380, 520, 380 * MainModel.midScale, 520 * MainModel.midScale, true);
                frmpaybycash.Location = new System.Drawing.Point((Screen.AllScreens[0].Bounds.Width - frmpaybycash.Width) / 2, (Screen.AllScreens[0].Bounds.Height - frmpaybycash.Height) / 2);
                frmpaybycash.TopMost = true;
                frmpaybycash.ShowDialog();
                frmpaybycash.Dispose();


                BackHelper.HideFormBackGround();
                Application.DoEvents();
                cash=frmpaybycash.RealCash;
                return frmpaybycash.DialogResult == DialogResult.OK;

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("礼品卡现金支付窗体异常：" + ex.Message);
                cash=0;
                return false;
            }
        }


        public static void ShowFormGiftCardRecord()
        {
            FormGiftCardRecord frmgiftcard = new FormGiftCardRecord();
            asf.AutoScaleControlTest(frmgiftcard, 1178, 760, Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height, true);
            frmgiftcard.Location = new System.Drawing.Point(0, 0);
            frmgiftcard.ShowDialog();
            frmgiftcard.Dispose();
            Application.DoEvents();
        }

    }
}
