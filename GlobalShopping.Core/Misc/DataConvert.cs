using System;
using System.Globalization;
using System.Text;

namespace GlobalShopping.Core.Misc
{
    /// <summary>
    /// todo refactor
    /// </summary>
    public class DataConvert
    {
        public static DateTime? ToDateTime(string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value)) return null;
                return Convert.ToDateTime(value);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DateTime? ToNullableDateTime(object value)
        {
            if (value == null) return null;
            DateTime dateTime;
            if (DateTime.TryParse(value.ToString(), out dateTime)) return dateTime;
            return null;
        }

        public static int ToInt32(string value)
        {
            int num;
            if (int.TryParse(value, out num)) return num;
            return 0;
        }

        public static int ToInt32(int? value)
        {
            if (string.IsNullOrEmpty(value.ToString())) return 0;
            return Convert.ToInt32(value);
        }

        public static long ToInt64(string value)
        {
            long num;
            if (long.TryParse(value, out num)) return num;
            return 0;
        }

        public static decimal ToDecimal(string value)
        {
            if (string.IsNullOrEmpty(value)) return 0;
            return ToNullableDecimal(value).Value;
        }

        public static double? ToNullableDouble(string value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            return Convert.ToDouble(value);
        }

        public static decimal? ToNullableDecimal(string value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            return Convert.ToDecimal(value);
        }

        public static double ToDouble(string value)
        {
            if (string.IsNullOrEmpty(value)) return 0;
            return ToNullableDouble(value).Value;
        }

        public static double ToDouble(double? value)
        {
            if (value.HasValue)
                return value.Value;
            return 0;
        }

        public static int ToInt(string value)
        {
            if (string.IsNullOrEmpty(value)) return 0;
            return Convert.ToInt32(value);
        }

        public static int ToZeroInt(object value)
        {
            if (value == null) return 0;
            if (string.IsNullOrEmpty(value.ToString())) return 0;
            return Convert.ToInt32(value);
        }

        public static int ToInt32(object value)
        {
            if (value == null)
                return -1;
            if (string.IsNullOrEmpty(value.ToString())) return -1;
            return Convert.ToInt32(value);
        }

        public static string ToString(DateTime value)
        {
            if (value == DateTime.MinValue) return "";
            return value.ToShortDateString();
        }

        public static string ToString(object value)
        {
            if (value == null) return "";
            return value.ToString();
        }

        /// <summary>
        ///     dd/MM/yyyy
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToDateString(DateTime? value)
        {
            if (value == null || value == DateTime.MinValue) return "";
            var culture = new CultureInfo("ms-MY");
            return Convert.ToDateTime(value).ToString("dd/MM/yyyy", culture);
        }

        /// <summary>
        ///     dd/MM/yyyy
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToDateString(string value)
        {
            if (string.IsNullOrEmpty(value)) return "";
            var culture = new CultureInfo("ms-MY");
            return Convert.ToDateTime(value).ToString("dd/MM/yyyy", culture);
        }

        public static DateTime NullToDateNow(string value)
        {
            if (string.IsNullOrEmpty(value)) return DateTime.Now;
            return Convert.ToDateTime(value);
        }

        public static bool ToBoolean(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (value.ToUpper().Equals("Y")) return true;
                if (value.ToUpper().Equals("TRUE")) return true;
                if (value.ToUpper().Equals("1")) return true;
            }
            return false;
        }

        public static bool ToBoolean(object value)
        {
            if (value == null) return false;
            if (value.ToString().ToUpper() == "Y") return true;
            if (value.ToString().ToUpper() == "TRUE") return true;
            if (value.ToString().ToUpper() == "1") return true;
            return false;
        }

        public static string ToBooleanString(bool value)
        {
            if (value) return "Y";
            return "N";
        }

        public static string ToBooleanString(string value)
        {
            if (string.IsNullOrEmpty(value)) return "N";
            if (value == "Y" || value == "N") return value;
            try
            {
                if (Convert.ToBoolean(value)) return "Y";
                return "N";
            }
            catch
            {
                return "N";
            }
        }

        public static string ToString(int? value)
        {
            if (value == null) return "";
            return value.ToString();
        }

        public static string ToString(int value)
        {
            if (value == null)
                return "";
            return value.ToString();
        }

        public static string ToString(decimal value)
        {
            if (value == 0) return "";
            return value.ToString();
        }

        public static string ToString(char value)
        {
            if (value == ' ' || value == '\0') return "";
            return value.ToString();
        }

        public static string ToString(decimal? value)
        {
            if (value == null) return "";
            return value.ToString();
        }

        public static char? ToChar(string value)
        {
            if (value == null || value.Equals("")) return null;
            return value[0];
        }

        public static int? ToNullableInt32(object value)
        {
            if (value == null) return null;
            if (string.IsNullOrEmpty(value.ToString())) return null;
            return Convert.ToInt32(value, CultureInfo.CurrentCulture);
        }

        public static bool? ToNullableBoolean(string value)
        {
            if (value == null) return null;
            return ToBoolean(value);
        }

        public static string ToNullableBooleanString(bool? value)
        {
            if (value == null) return null;
            return ToBooleanString(value.Value);
        }

        public static int? ToNullableInt32(string value)
        {
            if (value == null) return null;
            if (string.IsNullOrEmpty(value)) return null;
            return Convert.ToInt32(value, CultureInfo.CurrentCulture);
        }


        public static string ConvertToNull(string value)
        {
            if (value.Trim() == "") return null;
            return value.Trim();
        }

        public static bool IsDateTimeFormat(string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value)) return false;
                Convert.ToDateTime(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsDecimal(string value)
        {
            try
            {
                Convert.ToDecimal(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsInteger(string value)
        {
            try
            {
                Convert.ToInt32(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsLong(string value)
        {
            try
            {
                Convert.ToInt64(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static double GetMaxValue(double value1, double value2) { return (value1 > value2) ? value1 : value2; }

        public static decimal Rounding(double value)
        {
            if (value < 0) return -RoundingPostive(-value);
            return RoundingPostive(value);
        }

        /// <summary>
        ///     正数四舍五入
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static decimal RoundingPostive(double value)
        {
            if (value < 0) throw new Exception("方法只能格式化正数。");
            double vt = Math.Pow(10, 2);
            double vx = value * vt;

            vx += 0.5;
            return ToDecimal((Math.Floor(vx) / vt).ToString());
        }

        public static decimal Rounding(decimal value) { return Convert.ToDecimal(Rounding(Convert.ToDouble(value))); }

        public static decimal Rounding(decimal? value)
        {
            return Convert.ToDecimal(Rounding(Convert.ToDouble(value)));
        }

        #region 对外提供的decimal处理入口
        /// <summary>
        /// 针对人民币不做国家处理。只保留两位小数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static decimal EzDecimalCNY(decimal input)
        {
            return Transfer2Decimal(input);
        }
        /// <summary>
        /// 针对人民币不做国家处理。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static decimal EzDecimalViewCNY(decimal input)
        {
            return Transfer2Decimal(input);
        }

        ///// <summary>
        ///// 由于一些数据仍旧使用dynamic的形式传入，导致异常的，采用方法重载的形式规避掉。
        ///// </summary>
        //public static decimal EzDecimal(dynamic input, bool isAll)
        //{
        //    return EzDecimal((decimal)input, isAll: isAll);
        //}
        ///// <summary>
        ///// 根据特定国家来进行decimal的格式化
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //public static decimal EzDecimal(decimal input, Area area = Area.NA, bool isAll = false)
        //{
        //    if (area == Area.NA)
        //    {
        //        area = WebSetting.DefaultArea;
        //    }
        //    switch (area)
        //    {
        //        case Area.ID:
        //            input = ConvertToId(input, isAll);
        //            break;
        //        default:
        //            input = Transfer2Decimal(input);
        //            break;
        //    }
        //    return input;
        //}

        ///// <summary>
        ///// 由于一些数据仍旧使用dynamic的形式传入，导致异常的，采用方法重载的形式规避掉。
        ///// </summary>
        ///// <param name="input"></param>
        ///// <param name="isAll">印尼是否显示全部，千位以下也显示</param>
        ///// <returns></returns>
        //public static string EzDecimalView(dynamic input, bool isAll = false)
        //{
        //    try
        //    {
        //        return EzDecimalView(ToDecimal(input.ToString()), isAll);
        //    }
        //    catch (Exception ex)
        //    {
        //        return input;
        //    }
        //}
        ///// <summary>
        ///// 根据特定国家来进行decimal string的格式化
        ///// </summary>
        ///// <param name="input"></param>
        ///// <param name="isAll">印尼是否显示全部，千位以下也显示</param>
        ///// <returns></returns>
        //public static string EzDecimalView(decimal input, bool isAll = false)
        //{
        //    var result = string.Empty;
        //    switch (WebSetting.DefaultArea)
        //    {
        //        case Area.ID:
        //            result = ConvertToIdWithDot(input.ToString(), isAll);
        //            break;
        //        case Area.TH:
        //            result = ConvertToThWithComma(input.ToString());
        //            break;
        //        default:
        //            result = Transfer2Decimal(input).ToString();
        //            break;
        //    }
        //    return result;
        //} 
        #endregion

        /// <summary>
        /// 将字符串转成Id专用格式
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        private static decimal ConvertToId(string price, bool isAll)
        {
            return ConvertToId(ToDecimal(price), isAll);
        }

        /// <summary>
        /// 将decimal转成Id专用格式
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        private static decimal ConvertToId(decimal price, bool isAll)
        {
            if (price / 1000 > 0 && !isAll)
            {
                return Math.Ceiling(price / 1000) * 1000;
            }
            else
            {
                return price;
            }
        }

        /// <summary>
        /// 将string转成带dot的印尼专用格式
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        private static string ConvertToIdWithDot(string price, bool isAll = false)
        {
            return ConvertToIdWithDot(Transfer2Decimal(price), isAll);
        }
        /// <summary>
        /// 将decimal转成带dot的印尼专用格式
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        private static string ConvertToIdWithDot(decimal price, bool isAll)
        {
            var result = string.Empty;
            var isMinus = false;
            if (price >= 0 && price <= 1000)
            {
                return Transfer2Decimal(price).ToString();
            }

            if (price < 0)
            {
                price = Math.Abs(price);
                isMinus = true;
            }
            if (!isAll)
            {
                price = Math.Ceiling(price / 1000);
            }
            while ((price > 1000 && !isAll) || (price > 1000 && isAll))
            {
                var priceTemp = Math.Floor(price) % 1000;
                if (priceTemp < 10)
                {
                    result = string.Format(".00{0}{1}", priceTemp, result);
                }
                else if (priceTemp < 100)
                {
                    result = string.Format(".0{0}{1}", priceTemp, result);
                }
                else
                {
                    result = string.Format(".{0}{1}", priceTemp, result);
                }
                price = price / 1000;
            }
            if (isMinus)
            {
                result = string.Format("-{0}{1}", Math.Floor(price), result);
            }
            else
            {
                result = string.Format("{0}{1}", Math.Floor(price), result);
            }
            if (!isAll)
            {
                result = string.Format("{0}.000", result);
            }
            return result;
        }

        /// <summary>
        /// 将string转成带Comma的泰国专用格式
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        private static string ConvertToThWithComma(string price)
        {
            return ConvertToThWithComma(Transfer2Decimal(price));
        }
        /// <summary>
        /// 将decimal转成带Comma的泰国专用格式
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        private static string ConvertToThWithComma(decimal price)
        {
            var result = string.Empty;
            var isMinus = false;
            if (price >= 0 && price <= 1000)
            {
                return Transfer2Decimal(price).ToString();
            }

            if (price < 0)
            {
                price = Math.Abs(price);
                isMinus = true;
            }
            var fraction = price % 1;
            while (price > 1000)
            {
                var priceTemp = Math.Floor(price) % 1000;
                if (priceTemp < 10)
                {
                    result = string.Format(",00{0}{1}", priceTemp, result);
                }
                else if (priceTemp < 100)
                {
                    result = string.Format(",0{0}{1}", priceTemp, result);
                }
                else
                {
                    result = string.Format(",{0}{1}", priceTemp, result);
                }
                price = price / 1000;
            }
            if (isMinus)
            {
                result = string.Format("-{0}{1}.{2}{3}", Math.Floor(price), result, fraction < 0.1m ? "0" : "", (int)(Transfer2Decimal(fraction) * 100));
            }
            else
            {
                result = string.Format("{0}{1}.{2}{3}", Math.Floor(price), result, fraction < 0.1m ? "0" : "", (int)(Transfer2Decimal(fraction) * 100));
            }
            return result;
        }

        /// <summary>
        /// 保留两位小数 string重载
        /// </summary>
        /// <returns></returns>
        private static decimal Transfer2Decimal(string originData)
        {
            return string.IsNullOrEmpty(originData) ? 0 : Transfer2Decimal(Convert.ToDecimal(originData));
        }
        /// <summary>
        /// 保留两位小数
        /// </summary>
        /// <returns></returns>
        private static decimal Transfer2Decimal(decimal originData)
        {
            if (originData < 0)
            {
                return -Transfer2Decimal(-originData);
            }
            if (originData < 0)
            {
                throw new Exception("方法只能格式化正数。");
            }
            var vt = Math.Pow(10, 2);
            var vx = originData * (decimal)vt;

            vx += 0.5M;
            return (Math.Floor(vx) / (decimal)vt);
        }

        /// <summary>
        /// 将时间格式化成int格式 年月日， 2016-05-21 ===》 20160521 TODO 可以考虑公用，但目前不需要
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static int ToDateInt(DateTime dateTime)
        {
            int result = dateTime.Year * 10000;
            result += dateTime.Month * 100;
            result += dateTime.Day;
            return result;
        }
    }
}