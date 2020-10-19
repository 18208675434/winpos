using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhuiZhi_Integral_Scale_UncleFruit.Common;
using ZhuiZhi_Integral_Scale_UncleFruit.Model.Promotion;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Model.HalfOffLine
{
    public class HalfOffLineUtil
    {
        private static HttpUtil httputil = new HttpUtil();
        private static string memberid = "";

        /// <summary>
        /// 当前会员标签信息
        /// </summary>
        public static List<long> listvalidatePromotionMemberTags = new List<long>();

        /// <summary>
        /// 会员等级商户设置
        /// </summary>
        public static MemberTenantmembergradeconfig membertenantmembergradeconfig = null;

        /// <summary>
        /// 当前会员等级
        /// </summary>
        public static Gradesettinggetgrade gradesettinggetgrade = null;

        /// <summary>
        /// 积分规则
        /// </summary>
        public static TenantCreditConfig tenantCreditConfig = null;//积分规则
        /// <summary>
        /// 优惠券
        /// </summary>
        public static List<PromotionCoupon> listcoupon = null;//查询出优惠券


        /// <summary>
        /// 是否能享受会员权益
        /// </summary>
        public static bool enjoymemberrights = false;

        /// <summary>
        /// 会员权益配置
        /// </summary>
        public static MemberrightsItem memberrightsitem = null;

        //委托异步调用接口
        public delegate void deleteHttpMember();


        public static void LoadMemberInfo()
        {
            try
            {
                memberid = MainModel.CurrentMember.memberheaderresponsevo.memberid;


                //deleteHttpMember operationMemberOperationItem = new deleteHttpMember(MemberOperationItem);
                //operationMemberOperationItem.BeginInvoke(null, null);

                //deleteHttpMember operationGradesttingGetGrade = new deleteHttpMember(GradesttingGetGrade);
                //operationGradesttingGetGrade.BeginInvoke(null, null);

                //deleteHttpMember operationGetTenantCreditConfig = new deleteHttpMember(GetTenantCreditConfig);
                //operationGetTenantCreditConfig.BeginInvoke(null, null);

                //deleteHttpMember operationGetTenantMembergradeConfig = new deleteHttpMember(GetTenantMembergradeConfig);
                //operationGetTenantMembergradeConfig.BeginInvoke(null, null);

                //deleteHttpMember operationListMemberCouponAvailable = new deleteHttpMember(ListMemberCouponAvailable);
                //operationListMemberCouponAvailable.BeginInvoke(null, null);

                //deleteHttpMember operationEnjoyMemberRights = new deleteHttpMember(EnjoyMemberRights);
                //operationEnjoyMemberRights.BeginInvoke(null, null);

                //deleteHttpMember operationGetTenantMemberRightsConfigUsing = new deleteHttpMember(GetTenantMemberRightsConfigUsing);
                //operationGetTenantMemberRightsConfigUsing.BeginInvoke(null, null);

                //deleteHttpMember operationGetMemberTenantItem = new deleteHttpMember(GetMemberTenantItem);
                //operationGetMemberTenantItem.BeginInvoke(null, null);
                
                MemberOperationItem();
                GradesttingGetGrade();
                GetTenantCreditConfig();
                GetTenantMembergradeConfig();
                ListMemberCouponAvailable();
                EnjoyMemberRights();
                GetTenantMemberRightsConfigUsing();
                GetMemberTenantItem();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("加载会员信息异常" + ex.Message);
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
                enjoymemberrights = false;
                memberrightsitem = null;

                PromotionCache.getInstance().onDestory();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("清除会员信息异常" + ex.Message);
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
                Memberoperationitem objresult= httputil.MemberOperationItem(memberid, ref ErrorMsg);
                if (objresult == null || !string.IsNullOrEmpty(ErrorMsg))
                {
                    listvalidatePromotionMemberTags = null;
                    LogManager.WriteLog("获取会员标签信息失败" + ErrorMsg);
                }
                else
                {
                    listvalidatePromotionMemberTags = objresult.tagids;
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
                MemberTenantmembergradeconfig objresult = httputil.GetTenantMembergradeConfig(ref ErrorMsg);

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
                List<PromotionCoupon> objresult = httputil.ListMemberCouponAvailable(memberid, ref ErrorMsg);

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

        public static void EnjoyMemberRights()
        {
            try
            {
                string ErrorMsg = "";
                bool objresult = httputil.EnjoyMemberRights(memberid, ref ErrorMsg);

                if (objresult == null || !string.IsNullOrEmpty(ErrorMsg))
                {
                    enjoymemberrights = false;
                    LogManager.WriteLog("是否能享受会员权益失败" + ErrorMsg);
                }
                else
                {
                    enjoymemberrights = objresult;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("是否能享受会员权益异常" + ex.Message);
            }
        }

        public static void GetTenantMemberRightsConfigUsing()
        {
            try
            {
                string ErrorMsg = "";
                MemberrightsItem objresult = httputil.GetTenantMemberRightsConfigUsing(memberid, ref ErrorMsg);

                if (objresult == null || !string.IsNullOrEmpty(ErrorMsg))
                {
                    memberrightsitem = null;
                    LogManager.WriteLog("会员权益配置获取失败" + ErrorMsg);
                }
                else
                {
                    memberrightsitem = objresult;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("会员权益配置获取异常" + ex.Message);
            }
        }

        #endregion


        public static Paymenttypes paymenttypes = null;
        public static void GetAvailablePaymentTypes()
        {
            try
            {
                string ErrorMsg = "";
                Paymenttypes objresult = httputil.GetAvailablePaymentTypes(MainModel.CurrentShopInfo.shopid, ref ErrorMsg);
                if (objresult == null || !string.IsNullOrEmpty(ErrorMsg))
                {
                    paymenttypes = null;
                    LogManager.WriteLog("GetAvailablePaymentTypes失败" + ErrorMsg);
                }
                else
                {
                    paymenttypes = objresult;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetAvailablePaymentTypes异常" + ex.Message);
            }
        }


        public static MemberTenantItem membertenantitem = null;
        public static void GetMemberTenantItem()
        {
            try
            {
                string ErrorMsg = "";
                MemberTenantItem objresult = httputil.GetmemberTenantItem(memberid, ref ErrorMsg);
                if (objresult == null || !string.IsNullOrEmpty(ErrorMsg))
                {
                    membertenantitem = null;
                    LogManager.WriteLog("GetAvailablePaymentTypes失败" + ErrorMsg);
                }
                else
                {
                    membertenantitem = objresult;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetMemberTenantItem异常" + ex.Message);
            }
        }

    }
}
