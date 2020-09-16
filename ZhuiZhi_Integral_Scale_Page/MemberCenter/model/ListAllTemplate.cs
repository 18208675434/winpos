using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MemberCenter.model
{
 

    
public class ListAllTemplate
{
    /// <summary>
    /// 自定义金额传值
    /// </summary>
    public static string CustomMoney;
    /// <summary>
    /// 自定义赠送金额
    /// </summary>
    public static string ZengCustomMoney;
    /// <summary>
    /// 是否自定义金额
    /// </summary>
    public bool iszidingyi { get; set; }

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
    /// 
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
