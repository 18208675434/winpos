using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Model.Promotion
{
    public class ImplOfflineAdjsutPrice
    {

        
    public void adjustPrice(Product productBean) {
        try {
            if (productBean != null) {
                if (productBean.adjustpriceinfo != null) {
                    AdjustPriceDesc adjustPriceDesc = new AdjustPriceDesc();
                    decimal amt = productBean.adjustpriceinfo.amt;
                    int type = productBean.adjustpriceinfo.type;
                    adjustPriceDesc.type=type;
                   if ( type==1) {
                        //调价
                       if(productBean.price.saleprice>amt) {
                            adjustPriceDesc.amt=amt;
                            decimal newPrice = amt;
                            decimal newTotal = CommonConstant.ZERODECIMAL;
                            if (productBean.goodstagid == 0) {
                                decimal qty =productBean.num;;
                                newTotal = MoneyUtils.multiply(newPrice, qty);
                            } else {
                                decimal qty =productBean.specnum;
                                newTotal = MoneyUtils.multiply(newPrice, qty);
                            }
                            decimal tempdecimal = MoneyUtils.substract(productBean.price.total, newTotal);
                           adjustPriceDesc.amtdesc="已调价优惠：- ¥ "+tempdecimal.ToString("f2");
                           adjustPriceDesc.bannerdesc="¥"+productBean.adjustpriceinfo.amt.ToString("f2");
                            if (productBean.price != null) {
                                if (MoneyUtils.isBiggerThanZero(newPrice)) {
                                    productBean.price.originprice=productBean.price.saleprice;
                                    //1019 商品连续改价
                                    productBean.price.origintotal = productBean.price.total;
                                }
                                productBean.price.saleprice=amt;
                            }
                            if (productBean.price != null && newTotal != null) {
                                productBean.price.total = newTotal;
                                productBean.PaySubAmt = newTotal;
                            }
                            productBean.adjustpricedesc=adjustPriceDesc;
                        } else {
                            productBean.adjustpricedesc=null;
                        }
                    } else if (type ==2) {
                        //折扣
                        adjustPriceDesc.amt=amt;
                        decimal newPrice = MoneyUtils.multiply(productBean.price.saleprice, amt);
                        decimal newTotal = CommonConstant.ZERODECIMAL;
                        if (productBean.goodstagid == 0) {
                            decimal qty =productBean.num;  
                            newTotal = MoneyUtils.multiply(newPrice, qty);
                        } else {
                            decimal qty= productBean.specnum;
                            newTotal = MoneyUtils.multiply(newPrice, qty);
                        }
                        decimal tempdecimal = MoneyUtils.substract(productBean.price.total, newTotal);
                          adjustPriceDesc.amtdesc="已折扣优惠：- ¥ "+tempdecimal.ToString("f2");
                           adjustPriceDesc.bannerdesc="¥"+productBean.adjustpriceinfo.amt.ToString("f2");
                           String zhe = (productBean.adjustpriceinfo.amt * 10).ToString("f2");

                           adjustPriceDesc.bannerdesc = zhe + "折";
                        if (productBean.price != null) {
                            if (MoneyUtils.isBiggerThanZero(newPrice)) {
                                productBean.price.originprice = productBean.price.saleprice;

                                //1019 商品连续改价
                                productBean.price.origintotal = productBean.price.total;
                            }
                            productBean.price.saleprice = newPrice;
                        }
                        if (productBean.price != null && newTotal != null) {
                            productBean.price.total = newTotal;
                            productBean.PaySubAmt = newTotal;
                        }
                        productBean.adjustpricedesc = adjustPriceDesc;
                    }else if (type == 3)
                   {
                       //整单调价
                       if (productBean.price.total > amt)
                       {
                           adjustPriceDesc.amt = amt;

                           decimal tempdecimal = MoneyUtils.substract(productBean.price.total, amt);
                           adjustPriceDesc.amtdesc = "已调价优惠：- ¥ " + tempdecimal.ToString("f2");
                           adjustPriceDesc.bannerdesc = "¥" + productBean.adjustpriceinfo.amt.ToString("f2");

                           
                           if (productBean.price != null)
                           {
                               if (productBean.goodstagid == 0)
                           {
                               productBean.price.saleprice = Math.Round(amt/productBean.num, 2, MidpointRounding.AwayFromZero) ;
                           }else{
                               productBean.price.saleprice = Math.Round(amt / productBean.price.specnum, 2, MidpointRounding.AwayFromZero);
                               }
                             
                           }
                           productBean.price.total = amt;
                           productBean.PaySubAmt = amt;
                           productBean.adjustpricedesc = adjustPriceDesc;
                       }
                       else
                       {
                           productBean.adjustpricedesc = null;
                       }
                   } 
                   
                   else {
                        productBean.adjustpricedesc = null;
                    }
                }
            }
        } catch (Exception ex) {

                            LogManager.WriteLog("Error", "离线单品改价异常：" + ex.Message);
        }
    }


    }
}
