using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinSaasPOS.Model.Promotion
{
    public class EvaluateScopePromotion
    {
        private Decimal promotionItemTotalAmt;  //TODO 全部改成引用 promotionItemTotalPayAmt
        private Decimal promotionItemTotalCount;
        private Decimal promotionItemTotalPayAmt;
        private List<Product> list;
        private Dictionary<int, KeyValuePair<Product, Decimal>> var_package_selling_item_info;


        public Decimal getPromotionItemTotalAmt()
        {
            return promotionItemTotalPayAmt;
           // return promotionItemTotalAmt;
        }

        public void setPromotionItemTotalAmt(Decimal promotionItemTotalAmt)
        {
            this.promotionItemTotalAmt = promotionItemTotalAmt;
        }

        public Decimal getPromotionItemTotalCount()
        {
            return promotionItemTotalCount;
        }

        public void setPromotionItemTotalCount(Decimal promotionItemTotalCount)
        {
            this.promotionItemTotalCount = promotionItemTotalCount;
        }

        public Decimal getPromotionItemTotalPayAmt()
        {
            return promotionItemTotalPayAmt;
        }

        public void setPromotionItemTotalPayAmt(Decimal promotionItemTotalPayAmt)
        {
            this.promotionItemTotalPayAmt = promotionItemTotalPayAmt;
        }

        public List<Product> getList()
        {
            return list;
        }

        public void setList(List<Product> list)
        {
            this.list = list;
        }

        public void setVar_package_selling_item_info(Dictionary<int, KeyValuePair<Product, Decimal>> var_package_selling_item_info)
        {
            this.var_package_selling_item_info = var_package_selling_item_info;
        }

        public Dictionary<int, KeyValuePair<Product, Decimal>> getVar_package_selling_item_info()
        {
            return var_package_selling_item_info;
        }
    }
}
