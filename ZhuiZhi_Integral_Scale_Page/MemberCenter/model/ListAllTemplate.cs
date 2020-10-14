using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model
{
    public class CustomTemplateModel
    {
        
        
    }

public class ListAllTemplate
{
    public static decimal rewardmount = 0;
    /// <summary>
    /// 自定义金额传值
    /// </summary>
    public static bool iszidingyi;
    public static string zhifu = "";
    /// <summary>
    /// 自定义赠送金额
    /// </summary>
    public int CustomId { get; set; }
    public bool able { get; set; }

    public static bool enable = false;
    public static decimal mount = 0;
    public static decimal CustomMoney = 0;
    public static string Money;
    public static string ZengCustomMoney;
    /// <summary>
    /// 是否自定义金额
    /// </summary>

    /// <summary>
    /// 
    /// </summary>
    public decimal amount { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public decimal couponamount { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List <string > couponlist { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string couponname { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string createdat { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string createdby { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Depositrewardcoupondto depositrewardcoupondto { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool enabled { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int id { get; set; }
    /// <summary>
    /// 
    /// </summary>
  //  public string operator{ get; set; }  //TODO 关键字不能用
    /// <summary>
    /// 赠送金额
    /// </summary>
    public decimal rewardamount { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string tenantid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string updatedat { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string updatedby { get; set; }


}

public class RewardcoupondetaillistItem
{


    /// <summary>
    /// 
    /// </summary>
    public decimal count { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string promotioncode { get; set; }
}

public class Depositrewardcoupondto
{
    /// <summary>
    /// 
    /// </summary>
    public List <RewardcoupondetaillistItem > rewardcoupondetaillist { get; set; }
}




}
