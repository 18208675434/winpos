using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhuiZhi_Integral_Scale_UncleFruit.Model.HalfOffLine
{
    public class Tenantmemberrightsdiscountconfig
    {
        //"discount": 8.8,
        //    "title": "专享折扣",
        //            "description": "专享超低折扣，轻松赚回月费",
        //            "unionpromotion": true,
        //            "realmtype": "catalog",
        //            "catalogstoinclude": ["10100000"],
        //            "catalogstoincludevo": [{
        //        "id": "10100000",
        //                "name": "飘柔"
        //    }],
        //            "catalogstoexclude": [],
        //            "catalogstoexcludevo": [],
        //            "skucodestoexclude": [],
        //            "skucodestoexcludevo": [],
        //            "skucodestoinclude": [],
        //            "skucodestoincludevo": []
        public float discount;
        public String title;
        public String description;
        public bool unionpromotion;
        public String realmtype;
        public List<String> catalogstoinclude;
        public List<String> catalogstoexclude;
        public List<String> skucodestoexclude;
        public List<String> skucodestoinclude;

        public List<SkuResponseVo> skucodestoincludevo;
        public List<CatalogsResponseVo> catalogstoincludevo;
        public List<SkuResponseVo> skucodestoexcludevo;
        public List<CatalogsResponseVo> catalogstoexcludevo;
    }

    public class SkuResponseVo
    {
        public String skucode;
        public String skuname;
        public String spec;

    }

    public class CatalogsResponseVo
    {
        public String id;
        public String name;
    }
}
