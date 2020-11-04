using Maticsoft.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QDAMAPOS.Model.Promotion
{
   public class PromotionContextConvertUtils {

    public static List<KeyValuePair<Decimal, Decimal>> convertActionContext(String promoactioncontext) {

        List<KeyValuePair<Decimal, Decimal>> list = new List<KeyValuePair<decimal,decimal>>();
        if (!string.IsNullOrEmpty(promoactioncontext)) {
            //如:100:10;200:25;300:40
            String[] ctxArr = promoactioncontext.Split(';');
            if (ctxArr != null && ctxArr.Length > 0) {
                foreach (String s in ctxArr) {
                    //100:10
                    String[] str = s.Split(':');
                    //如果出现没有:正常分隔的情况直接
                    if (str.Length == 1) {
                        continue;
                    }
                    Decimal stepThreshold = MoneyUtils.newMoney(str[0]);
                    Decimal stepDiscount = MoneyUtils.newMoney(str[1]);
                    list.Add(new KeyValuePair<Decimal, Decimal>(stepThreshold, stepDiscount));
                }

                list = SortMyObjectAscending(list);
                //list.Sort();
                    //Collections.sort(list, new ImplComparator());
            }
        }
        return list;
    }

    //public static class ImplComparator implements Comparator<Pair<Decimal, Decimal>> {

    //    @Override
    //    public int compare(Pair<Decimal, Decimal> o1, Pair<Decimal, Decimal> o2) {
    //        return o2.first.compareTo(o1.first);
    //    }
    //}

    public static bool isSingleLinePromotion(DBPROMOTION_CACHE_BEANMODEL promotion)
    {
        //第N件N折 第N件N元
        return (promotion.PROMOACTION== EnumPromotionType.DESIGNATED_DISCOUNT_OFF_ACTION)
                ||(promotion.PROMOACTION== EnumPromotionType.DESIGNATED_PRICE_ACTION)
                || (promotion.PROMOACTION== EnumPromotionType.DYNAMIC_DESIGNATED_PRICE_ACTION)
                || (promotion.PROMOACTION== EnumPromotionType.CYCLE_DESIGNATED_DISCOUNT_OFF_ACTION)
                ;
    }


       
public static List <KeyValuePair<Decimal, Decimal>> SortMyObjectAscending (List <KeyValuePair<Decimal, Decimal>> myObjectList )
{
    int minObjectIndexId = 0;
    List <KeyValuePair<Decimal, Decimal>> sortedMyObjectList = new List <KeyValuePair<Decimal, Decimal>>();

    int count = myObjectList.Count;
    for (int indexA = count; indexA > 0; indexA--)
    {
      for ( int indexB = 0; indexB < myObjectList.Count ; indexB ++)
      {
          KeyValuePair<Decimal, Decimal> objecttemp = myObjectList[indexB];
          if (objecttemp.Key <= minObjectIndexId)
          {
                minObjectIndexId = indexB;
          }
      }
      sortedMyObjectList.Add(myObjectList[minObjectIndexId]);
      myObjectList.RemoveAt( minObjectIndexId );
      minObjectIndexId = 0;
     // indexA = 0;
    }
    sortedMyObjectList.Reverse();//反转倒叙
    return sortedMyObjectList;
} 
}

}
