using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Model.Promotion
{
    public  class CollectionUtils
    {
        public static bool isEmpty<T>(List<T> products)
        {
            if (products == null)
            {
                return true;
            } 
            
            if (products.Count == 0)
            {
                return true;
            }

            return false;
        }

        public static bool isNotEmpty( List<Product> products)
        {
            if (products == null)
            {
                return false;
            } if (products.Count <= 0)
            {
                return false;
            }

            return true;
        }

        public static bool isNotEmpty(List<string> lststr)
        {
            if (lststr == null)
            {
                return false;
            } if (lststr.Count <= 0)
            {
                return false;
            }

            return true;
        }

    }
}
