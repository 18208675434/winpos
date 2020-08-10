using Maticsoft.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Model.Promotion
{
    public class MemberRightsForGradeBean
    {
        private bool memberRightsForGrade;
        private bool gradeMember;
        private DBPROMOTION_CACHE_BEANMODEL memberGradeDiscountPricePromo;

        public bool isMemberRightsForGrade()
        {
            return memberRightsForGrade;
        }

        public void setMemberRightsForGrade(bool memberRightsForGrade)
        {
            this.memberRightsForGrade = memberRightsForGrade;
        }

        public bool isGradeMember()
        {
            return gradeMember;
        }

        public void setGradeMember(bool gradeMember)
        {
            this.gradeMember = gradeMember;
        }

        public DBPROMOTION_CACHE_BEANMODEL getMemberGradeDiscountPricePromo()
        {
            return memberGradeDiscountPricePromo;
        }

        public void setMemberGradeDiscountPricePromo(DBPROMOTION_CACHE_BEANMODEL memberGradeDiscountPricePromo)
        {
            this.memberGradeDiscountPricePromo = memberGradeDiscountPricePromo;
        }
    }

}
