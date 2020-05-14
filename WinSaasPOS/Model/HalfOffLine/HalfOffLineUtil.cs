using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinSaasPOS.Common;
using WinSaasPOS.Model.Promotion;

namespace WinSaasPOS.Model.HalfOffLine
{
    public class HalfOffLineUtil
    {
        private static HttpUtil httputil = new HttpUtil();
        private static string memberid = "";

        /// <summary>
        /// 当前会员标签信息
        /// </summary>
        public static List<long> listvalidatePromotionMemberTags =new List<long>();

        /// <summary>
        /// 会员等级商户设置
        /// </summary>
        public static MemberTenantmembergradeconfig  membertenantmembergradeconfig=null;

       /// <summary>
       /// 当前会员等级
       /// </summary>
        public static Gradesettinggetgrade gradesettinggetgrade=null;

        /// <summary>
        /// 积分规则
        /// </summary>
        public static TenantCreditConfig tenantCreditConfig = null;//积分规则
        /// <summary>
        /// 优惠券
        /// </summary>
        public static List<PromotionCoupon> listcoupon = null;//查询出优惠券

        public static void LoadMemberInfo()
        {
            try
            {
                memberid = MainModel.CurrentMember.memberheaderresponsevo.memberid;
                MemberOperationItem();
                GradesttingGetGrade();
                GetTenantCreditConfig();
                GetTenantMembergradeConfig();
                ListMemberCouponAvailable();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("加载会员信息异常"+ex.Message);
            }
        }
        public static void ClearMemberInfo()
        {
            try
            {
                listvalidatePromotionMemberTags = null;
                membertenantmembergradeconfig = null;
                gradesettinggetgrade = null;
                tenantCreditConfig = null;
                listcoupon = null;
                PromotionCache.getInstance().onDestory();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("清除会员信息异常" +ex.Message);
            }
        }


        #region 半离线会员信息

        /// <summary>
        /// 获取会员标签信息
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="errormsg"></param>
        public static void MemberOperationItem()
        {
            try
            {
                string ErrorMsg = "";
                List<long> objresult = httputil.MemberOperationItem(memberid, ref ErrorMsg);

                if (objresult == null || !string.IsNullOrEmpty(ErrorMsg))
                {
                    listvalidatePromotionMemberTags = null;
                    LogManager.WriteLog("获取会员标签信息失败" + ErrorMsg);
                }
                else
                {
                    listvalidatePromotionMemberTags = objresult;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("获取会员标签信息异常" + ex.Message);
            }
        }

        /// <summary>
        /// 当前会员等级
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="errormsg"></param>
        public static void GradesttingGetGrade()
        {
            try
            {
                string ErrorMsg = "";
                Gradesettinggetgrade objresult = httputil.GradesttingGetGrade(memberid, ref ErrorMsg);

                if (objresult == null || !string.IsNullOrEmpty(ErrorMsg))
                {
                    gradesettinggetgrade = null;
                    LogManager.WriteLog("获取当前会员等级失败" + ErrorMsg);
                }
                else
                {
                    gradesettinggetgrade = objresult;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("获取当前会员等级异常" + ex.Message);
            }
        }

        /// <summary>
        /// 当前会员等级商户设置
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="errormsg"></param>
        public static void GetTenantMembergradeConfig()
        {
            try
            {
                string ErrorMsg = "";
                MemberTenantmembergradeconfig objresult = httputil.GetTenantMembergradeConfig( ref ErrorMsg);

                if (objresult == null || !string.IsNullOrEmpty(ErrorMsg))
                {
                    membertenantmembergradeconfig = null;
                    LogManager.WriteLog("当前会员等级商户设置失败" + ErrorMsg);
                }
                else
                {
                    membertenantmembergradeconfig = objresult;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("当前会员等级商户设置异常" + ex.Message);
            }
        }

        /// <summary>
        /// 获取积分规则
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="errormsg"></param>
        public static void GetTenantCreditConfig()
        {
            try
            {
                string ErrorMsg = "";
                TenantCreditConfig objresult = httputil.GetTenantCreditConfig(ref ErrorMsg);

                if (objresult == null || !string.IsNullOrEmpty(ErrorMsg))
                {
                    tenantCreditConfig = null;
                    LogManager.WriteLog("获取积分规则失败" + ErrorMsg);
                }
                else
                {
                    tenantCreditConfig = objresult;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("获取积分规则异常" + ex.Message);
            }
        }

        /// <summary>
        /// 用户可用券列表
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="errormsg"></param>
        public static void ListMemberCouponAvailable()
        {
            try
            {
                string ErrorMsg = "";
                List<PromotionCoupon> objresult = httputil.ListMemberCouponAvailable(memberid,ref ErrorMsg);

                if (objresult == null || !string.IsNullOrEmpty(ErrorMsg))
                {
                    listcoupon = null;
                    LogManager.WriteLog("获取积分规则失败" + ErrorMsg);
                }
                else
                {
                    listcoupon = objresult;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("获取积分规则异常" + ex.Message);
            }
        }

        #endregion

    }
}
