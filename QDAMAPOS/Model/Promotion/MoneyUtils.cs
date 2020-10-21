using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QDAMAPOS.Model.Promotion
{
    public class MoneyUtils
    {

        private MoneyUtils()
        {
            // prevent instantiation
        }

        //public static Decimal newQty(Decimal qty) {
        //    return qty.setScale(CommonConstant.DEFAULT_QTY_SCALE, CommonConstant.DEFAULT_ROUND);
        //}

        //public static Decimal newMoneyFromLong(long number) {
        //    Decimal bd = new Decimal(number);
        //    return setScale(bd);
        //}

        public static Decimal newMoney(String number)
        {
            if (!string.IsNullOrEmpty(number))
            {
                number = number.Replace(",", "");
            }
            // Decimal bd = new Decimal(number);
            return Convert.ToDecimal(number);
        }


        //public static Decimal newMoneyFromFloat(float number) {
        //    return newMoneyFromDouble((double) number);
        //}

        //public static Decimal newMoneyFromDouble(double number) {
        //    Decimal bd = new Decimal(number);
        //    return setScale(bd);
        //}

        /**
         * 从int初始化金额Decimal
         *
         * @param number 数字
         * @return Decimal
         */
        public static Decimal newMoneyFromInt(int number)
        {

            //TODO
            Decimal bd = new Decimal(number);
            return number;// setScale(bd);
        }

        //public static Decimal setScale(Decimal bd) {
        //    return bd.setScale(CommonConstant.DEFAULT_SCALE, CommonConstant.DEFAULT_ROUND);
        //}

        //public static int displayInt(Decimal bd) throws Exception {
        //    return MoneyUtils.multiply(bd, CommonConstant.HUNDREDDECIMAL).intValue();
        //}

        //public static Decimal fromDisplayInt(int bd) throws Exception {
        //    return divide(newMoneyFromDouble(bd), CommonConstant.HUNDREDDECIMAL);
        //}

        //public static String displayMoney(Decimal bd) {
        //    String template = "\u00A5 %s";
        //    return String.format(template, bd.toString());
        //}

        public static Decimal add( Decimal first,  Decimal second) {
            return Math.Round(first+second,CommonConstant.DEFAULT_SCALE); 
        }

        public static Decimal add( Decimal first,  Decimal second, int defaultScale) {
            if (first == null || first.Equals(Decimal.Zero)) {
                return second;
            } else if (second == null || second.Equals(Decimal.Zero)) {
                return first;
            } else {
                
                //if (first.scale() == CommonConstant.DEFAULT_QTY_SCALE || second.scale() == CommonConstant.DEFAULT_QTY_SCALE) {
                //    return first.add(second).setScale(CommonConstant.DEFAULT_QTY_SCALE, CommonConstant.DEFAULT_ROUND);
                //}
                return Math.Round(first+second,CommonConstant.DEFAULT_SCALE); //first.add(second).setScale(getSafeScale(defaultScale), CommonConstant.DEFAULT_ROUND);
            }
        }

        //public static Decimal sum(Decimal... bds) {
        //    Decimal sum = CommonConstant.ZERODECIMAL;
        //    if (bds == null) {
        //        return sum;
        //    }
        //    for (Decimal bd : bds) {
        //        sum = MoneyUtils.add(bd, sum);
        //    }
        //    return sum;
        //}

        public static Decimal substract(Decimal first, Decimal second)
        {
            return substract(first, second, CommonConstant.DEFAULT_SCALE);
        }

        public static Decimal substract(Decimal first, Decimal second, int defaultScale)
        {
            if (first == null)
            {
                first = Decimal.Zero;
            }
            if (second == null)
            {
                second = Decimal.Zero;
            }
            //if (first.scale() == CommonConstant.DEFAULT_QTY_SCALE || second.scale() == CommonConstant.DEFAULT_QTY_SCALE)
            //{
            //    return first.subtract(second).setScale(CommonConstant.DEFAULT_QTY_SCALE, CommonConstant.DEFAULT_ROUND);
            //}
            return Math.Round(first-second,CommonConstant.DEFAULT_ROUND); //first.subtract(second).setScale(getSafeScale(defaultScale), CommonConstant.DEFAULT_ROUND);
        }

        public static Decimal multiply(Decimal first, Decimal second)
        {
            return multiply(first, second, 2);
        }

        public static Decimal multiply(Decimal first, Decimal second, int defaultScale)
        {
            if (first == null || second == null)
            {
                throw new Exception("can't multiply null values");
            }

            return Math.Round(first * second, 2);
            // return first.multiply(second).setScale(getSafeScale(defaultScale), CommonConstant.DEFAULT_ROUND);
        }

        //public static Decimal multiplyQty(Decimal first, Decimal second) throws Exception {
        //    return multiplyQty(first, second, CommonConstant.DEFAULT_SCALE);
        //}

        //public static Decimal multiplyQty(Decimal first, Decimal second, Integer defaultScale) throws Exception {
        //    if (first == null || second == null) {
        //        throw new Exception("can't multiply null values");
        //    }
        //    if (first.scale() == CommonConstant.DEFAULT_QTY_SCALE || second.scale() == CommonConstant.DEFAULT_QTY_SCALE) {
        //        return first.multiply(second).setScale(CommonConstant.DEFAULT_QTY_SCALE, CommonConstant.DEFAULT_ROUND);
        //    }
        //    return first.multiply(second).setScale(getSafeScale(defaultScale), CommonConstant.DEFAULT_ROUND);
        //}

        public static Decimal divide(Decimal first, Decimal second) {
            return  Math.Round(first/second,2);
        }

        public static Decimal divide(Decimal first, Decimal second, int defaultScale){
            if (first == null || second == null) {
                throw new Exception("can't divide null values");
            }

            return Math.Round(first / second, getSafeScale(defaultScale));
          //  return first.divide(second, getSafeScale(defaultScale), CommonConstant.DEFAULT_ROUND);
        }

        private static int getSafeScale(int scale)
        {
            if (scale == null)
            {
                return CommonConstant.DEFAULT_SCALE;
            }
            return scale;
        }

        ///**
        // * Get max value from given values.
        // *
        // * @param first  first given
        // * @param second second value
        // * @return max value.
        // */
        //public static Decimal max(final Decimal first, final Decimal second) {
        //    if (isFirstBiggerThanSecond(
        //            notNull(first),
        //            notNull(second))) {
        //        return notNull(first);
        //    }
        //    return notNull(second);
        //}

        ///**
        // * Get minimal, but greater than 0 value from given values.
        // *
        // * @param first  first given
        // * @param second second value
        // * @return max value.
        // */
        //public static Decimal minPositive(final Decimal first, final Decimal second) {
        //    if (first == null || notNull(first).equals(Decimal.Zero)) {
        //        return notNull(second);
        //    } else if (second == null || notNull(second).equals(Decimal.Zero)) {
        //        return notNull(first);
        //    } else if (isFirstBiggerThanSecond(
        //            notNull(first),
        //            notNull(second))) {
        //        return notNull(second);
        //    }
        //    return notNull(first);
        //}

        ///**
        // * @param value value to check
        // * @return value if it not null, otherwise Decimal.Zero
        // */
        //public static Decimal notNull(final Decimal value) {
        //    return notNull(value, null);
        //}

        ///**
        // * @param value  value to check
        // * @param ifNull value to return if value to check is null
        // * @return value or ifNull if value is null. if ifNull is null returns Decimal.Zero.
        // */
        //public static Decimal notNull(final Decimal value, final Decimal ifNull) {
        //    if (value == null) {
        //        if (ifNull == null) {
        //            return Decimal.Zero;
        //        }
        //        return ifNull;
        //    }
        //    return value;
        //}


        /**
         * @param first  value
         * @param second value
         * @return true if first is greater than second (null safe)
         */
        public static bool isFirstBiggerThanSecond(Decimal first, Decimal second)
        {

            if (first == null && second == null)
            {
                return false;
            }
            else if (second == null)
            {
                return true;
            }
            else if (first == null)
            {
                return false;
            }
            return first - second > 0;

        }

        public static bool isZero( Decimal first) {
            if (first == null) {
                return false;
            }

            return isFirstEqualToSecond(first, CommonConstant.ZERODECIMAL);

        }

        public static bool isBiggerThanZero(Decimal first)
        {
            return first != null && isFirstBiggerThanSecond(first, 0);
        }

        //public static bool isFirstBiggerThanOrEqualToZero(final Decimal first) {
        //    return first != null && isFirstBiggerThanOrEqualToSecond(first, CommonConstant.ZERODECIMAL);
        //}

        /**
         * @param first  value
         * @param second value
         * @return true if first is greater than or equal to second (null safe)
         */
        public static bool isFirstBiggerThanOrEqualToSecond(Decimal first, Decimal second)
        {
            if (first == null && second == null)
            {
                return false;
            }
            else if (second == null)
            {
                return true;
            }
            else if (first == null)
            {
                return false;
            }
            return first >= second;
        }
            //    return first.compareTo(second) >= 0;
            //}

            /**
             * @param first  value
             * @param second value
             * @return true if first is equal to second (null safe)
             */
            public static bool isFirstEqualToSecond( Decimal first,  Decimal second) {
                if (first == null && second == null) {
                    return false;
                } else if (second == null) {
                    return false;
                } else if (first == null) {
                    return false;
                }

                return first == second;// first.compareTo(second) == 0;
            }

            /**
             * @param first  value
             * @param second value
             * @param scale  scale
             * @return true if first is equal to second (null safe)
             */
            public static bool isFirstEqualToSecond( Decimal first,  Decimal second,  int scale) {
                if (first == null && second == null) {
                    return false;
                } else if (second == null) {
                    return false;
                } else if (first == null) {
                    return false;
                }
                return first==second;

            }

            ///**
            // * Get discount as percentage.
            // * E.g. original 100.00, discounted 80.0 - the result will be 80 (%)
            // * E.g. original 100.00, discounted 80.99 - the result will be 80 (%)
            // *
            // * @param original   original price
            // * @param discounted discounted price
            // * @return discount in percent
            // */
            //public static Decimal getDiscountDisplayValue(final Decimal original, final Decimal discounted) {
            //    if (original == null || discounted == null) {
            //        return Decimal.Zero;
            //    }
            //    return discounted.multiply(CommonConstant.HUNDREDDECIMAL)
            //            .divide(original, CommonConstant.DEFAULT_ROUND).setScale(0, CommonConstant.DEFAULT_ROUND);
            //}

            //public static Decimal newMoneyQty(String qty) throws Exception {
            //    if (StringUtils.checkNull(qty)) {
            //        throw new Exception("qty can't be null value");
            //    }
            //    Decimal bd = new Decimal(qty);
            //    return bd.setScale(CommonConstant.DEFAULT_QTY_SCALE, CommonConstant.DEFAULT_ROUND);
            //}

            //public static Decimal displayQty(Decimal qty) throws Exception {
            //    if (qty == null) {
            //        throw new Exception("can't multiply null value");
            //    }
            //    return qty.multiply(CommonConstant.HUNDREDDECIMAL).setScale(CommonConstant.DEFAULT_SCALE, CommonConstant.DEFAULT_ROUND);
            //}

            //public static Decimal fromDisplayQty(String qty) throws Exception {
            //    Decimal newQty = newMoneyQty(qty);
            //    return newQty.divide(CommonConstant.HUNDREDDECIMAL, CommonConstant.DEFAULT_QTY_SCALE, CommonConstant.DEFAULT_ROUND);
            //}

            ///**
            // * 将数字转换为取出所有末尾0的自然数表达的文字，比如 2.00 -> 2 2.10 -> 2.1
            // * 如果数字为null，返回空字符串
            // *
            // * @param amount 数字
            // * @return 字符串表达
            // */
            //public static String toNoTrailingZerosString(Decimal amount) {
            //    if (amount == null) {
            //        return "";
            //    }

            //    return amount.stripTrailingZeros().toPlainString();
            //}
       // }
        }
    }

