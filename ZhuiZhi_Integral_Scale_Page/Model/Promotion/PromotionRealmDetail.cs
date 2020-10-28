using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Model.Promotion
{

    public class PromotionRealmDetail
    {
        // "适用规则标题")
        public String title { get; set; }
        // "适用规则描述")
        public String description { get; set; }
        public string realmType { get; set; }


        public List<String> catalogsToInclude{get;set;}
        // "排除的分类")
        public List<String> catalogsToExclude { get; set; }
        //"排除的商品sku"{get;set;}
        public List<String> skuCodesToExclude { get; set; }
        //"包含的商品")
        public List<String> skuCodesToInclude { get; set; }

        //商品包含数量信息
        public List<PromotionSku> skuAmtInfo;
    }

    //此方法 set get 方法无法解析json
    public class PromotionRealmDetailbak
    {

        // "适用规则标题")
        private String title;
        // "适用规则描述")
        private String description;
        //"适用规则类型,eg.all 全场、catalog 分类、goods 多商品")
        private String realmType;
        //"包含的分类")
        private List<String> catalogsToInclude;
        // "排除的分类")
        private List<String> catalogsToExclude;
        //"排除的商品sku")
        private List<String> skuCodesToExclude;
        //"包含的商品")
        private List<String> skuCodesToInclude;

        public String getTitle()
        {
            return title;
        }

        public void setTitle(String title)
        {
            this.title = title;
        }

        public String getDescription()
        {
            return description;
        }

        public void setDescription(String description)
        {
            this.description = description;
        }

        public String getRealmType()
        {
            return realmType;
        }

        public void setRealmType(String realmType)
        {
            this.realmType = realmType;
        }

        public List<String> getCatalogsToInclude()
        {
            return catalogsToInclude;
        }

        public void setCatalogsToInclude(List<String> catalogsToInclude)
        {
            this.catalogsToInclude = catalogsToInclude;
        }

        public List<String> getCatalogsToExclude()
        {
            return catalogsToExclude;
        }

        public void setCatalogsToExclude(List<String> catalogsToExclude)
        {
            this.catalogsToExclude = catalogsToExclude;
        }

        public List<String> getSkuCodesToExclude()
        {
            return skuCodesToExclude;
        }

        public void setSkuCodesToExclude(List<String> skuCodesToExclude)
        {
            this.skuCodesToExclude = skuCodesToExclude;
        }

        public List<String> getSkuCodesToInclude()
        {
            return skuCodesToInclude;
        }

        public void setSkuCodesToInclude(List<String> skuCodesToInclude)
        {
            this.skuCodesToInclude = skuCodesToInclude;
        }

    }

        public  class PromotionSku {
            public static long serialVersionUID = 1967167540454037862L;
            public String skuCode;
            public String skuName;
            public Decimal amt;
    }
}
