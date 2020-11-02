using Maticsoft.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinSaasPOS.Model.Promotion
{
    public class TripletBean
    {
        //售价
        private Decimal originprice;
        //促销类型
        private String priceKind;

        private DBPROMOTION_CACHE_BEANMODEL promoTriplet;
        private int pricetagid;
        private String pricetag;

        public TripletBean(Decimal originprice, String priceKind, DBPROMOTION_CACHE_BEANMODEL promoTriplet, int pricetagid, String pricetag)
        {
            this.originprice = originprice;
            this.priceKind = priceKind;
            this.promoTriplet = promoTriplet;
            this.pricetagid = pricetagid;
            this.pricetag = pricetag;
        }

        public Decimal getOriginprice()
        {
            return originprice;
        }

        public void setOriginprice(Decimal originprice)
        {
            this.originprice = originprice;
        }

        public String getPriceKind()
        {
            return priceKind;
        }

        public void setPriceKind(String priceKind)
        {
            this.priceKind = priceKind;
        }

        public DBPROMOTION_CACHE_BEANMODEL getPromoTriplet()
        {
            return promoTriplet;
        }

        public void setPromoTriplet(DBPROMOTION_CACHE_BEANMODEL promoTriplet)
        {
            this.promoTriplet = promoTriplet;
        }

        public int getPricetagid()
        {
            return pricetagid;
        }

        public void setPricetagid(int pricetagid)
        {
            this.pricetagid = pricetagid;
        }

        public String getPricetag()
        {
            return pricetag;
        }

        public void setPricetag(String pricetag)
        {
            this.pricetag = pricetag;
        }
    }

}
