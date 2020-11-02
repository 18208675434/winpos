
using Maticsoft.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QDAMAPOS.Model.Promotion
{public class PromotionInfoUtils {

    /**
     * 校验当前折扣值是否合法
     *
     * @param discountNum 促销记录上的折扣值
     * @return 折扣优惠在(0, 1]范围, 返回true 即配置的为[0折, 10折)
     */
    public static bool isValidDiscountNum(Decimal discountNum) {
        return MoneyUtils.isFirstBiggerThanSecond(discountNum, 0) && MoneyUtils.isFirstBiggerThanOrEqualToSecond(1, discountNum);
    }

    public static Decimal getFixedScaleAmt(Decimal amt, int defaultScale) {
        return Math.Round(amt,defaultScale); //amt.setScale(defaultScale, RoundingMode.HALF_UP);
    }

    //促销排序
    public static void sortPromotion(List<DBPROMOTION_CACHE_BEANMODEL> list) {

          list.OrderBy(x => x.FROMOUTER).ThenByDescending(x => x.RANK);
        //if (Build.VERSION.SDK_INT >= 24) {
        //    list.sort((o1, o2) -> {
        //        //fromouter相同时按优先级、时间倒序
        //        if (o1.getFromouter().equals(o2.getFromouter())) {
        //            if (o2.getRank().equals(o1.getRank())) {//优先级相同,按时间排序
        //                return o2.getUpdatedat().compareTo(o1.getUpdatedat());
        //            } else {
        //                return o2.getRank().compareTo(o1.getRank());
        //            }
        //        } else {
        //            //fromouter优先
        //            if (o1.getFromouter()) {
        //                return -1;
        //            } else {
        //                return 1;
        //            }
        //        }
        //    });
       // }
    }

//判断是否在时间范围内
    public static bool isInTimeRange(DBPROMOTION_CACHE_BEANMODEL promotion) {
        //默认适用，当且仅当成对时间范围不为空，且不在该范围内时进行时间范围判断
        bool inTimeRange = true;
        try {
            String enabledTimeInfo = promotion.ENABLEDTIMEINFO;
            if (!string.IsNullOrEmpty(enabledTimeInfo)) {
                ActivityEnabledTimeInfo activityEnabledTimeInfo = JsonConvert.DeserializeObject<ActivityEnabledTimeInfo>(enabledTimeInfo);
                //周循环处理
                if (!string.IsNullOrEmpty(activityEnabledTimeInfo.weekcycleData)) {
                    String[] weekcycleData = activityEnabledTimeInfo.weekcycleData.Split(',');
                    if (1 != (int)DateTime.Now.DayOfWeek) {
                        return false;
                    }
                }
                long currentTime = Convert.ToInt64( MainModel.getStampByDateTime(DateTime.Now)); 
                if (!string.IsNullOrEmpty(activityEnabledTimeInfo.startTime1) && !string.IsNullOrEmpty(activityEnabledTimeInfo.endTime1)) {
                    long start = parseTime(activityEnabledTimeInfo.startTime1);
                    long end = parseTime(activityEnabledTimeInfo.endTime1);
                    inTimeRange = start <= currentTime && currentTime <= end;
                }
                if (!inTimeRange && !string.IsNullOrEmpty(activityEnabledTimeInfo.startTime2) && !string.IsNullOrEmpty(activityEnabledTimeInfo.endTime2)) {
                    long start = parseTime(activityEnabledTimeInfo.startTime2);
                    long end = parseTime(activityEnabledTimeInfo.endTime2);
                    inTimeRange = start <= currentTime && currentTime <= end;
                }
                if (!inTimeRange && !string.IsNullOrEmpty(activityEnabledTimeInfo.startTime3) && !string.IsNullOrEmpty(activityEnabledTimeInfo.endTime3)) {
                    long start = parseTime(activityEnabledTimeInfo.startTime3);
                    long end = parseTime(activityEnabledTimeInfo.endTime3);
                    inTimeRange = start <= currentTime && currentTime <= end;
                }
            }
        } catch (Exception e) {
        }
        return inTimeRange;
    }

    private static long parseTime(String time) {
        //        String shortPattern = "HH:mm";
        //        String longPattern = "HH:mm:ss";
        String[] split = time.Split(':');
        String currentTims = DateTime.Now.ToString("yyyy-MM-dd");
        String pattern = currentTims + " " + time + ":00";
        if (split.Length == 3)
        {
            pattern = currentTims + " " + time;
        }
        return Convert.ToInt64(MainModel.getStampByDateTime(Convert.ToDateTime(pattern)));
    }


    ///**
    // * 获取当前日期是星期几<br>
    // *
    // * @return 当前日期是星期几
    // */
    //public int getWeekOfDate()
    //{
    //    // String[] weekDays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
    //    int[] weekDays = { 6, 0, 1, 2, 3, 4, 5 };
    //    Calendar cal = Calendar.CurrentEra;
    //    cal.setTime(new Date());
    //    int w = cal.get(Calendar.DAY_OF_WEEK) - 1;
    //    if (w < 0)
    //        w = 0;
    //    return weekDays[w];
    //}
}

}
