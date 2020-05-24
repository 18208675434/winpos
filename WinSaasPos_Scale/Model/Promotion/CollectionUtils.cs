using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinSaasPOS_Scale.Model.Promotion
{
    public  class CollectionUtils
    {

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
