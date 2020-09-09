using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.GiftCard.Model;
using ZhuiZhi_Integral_Scale_UncleFruit.HelperUI;
using ZhuiZhi_Integral_Scale_UncleFruit.Model;

namespace ZhuiZhi_Integral_Scale_UncleFruit.GiftCard
{
    public class GiftCardHelper
    {
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

    }
}
