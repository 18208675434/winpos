using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinSaasPOS.Model.HalfOffLine
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
        private float discount;
        private String title;
        private String description;
        private bool unionpromotion;
        private String realmtype;
        private List<String> catalogstoinclude;
        private List<String> catalogstoexclude;
        private List<String> skucodestoexclude;
        private List<String> skucodestoinclude;

        private List<SkuResponseVo> skucodestoincludevo;
        private List<CatalogsResponseVo> catalogstoincludevo;
        private List<SkuResponseVo> skucodestoexcludevo;
        private List<CatalogsResponseVo> catalogstoexcludevo;
    }

    public class SkuResponseVo
    {
        private String skucode;
        private String skuname;
        private String spec;

    }

    public class CatalogsResponseVo
    {
        private String id;
        private String name;
    }
}
