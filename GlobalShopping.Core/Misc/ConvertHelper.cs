using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using Zen.Framework.Logging;

namespace Zen.Framework.Misc
{
    public class ConvertHelper
    {
        public static Int64 ToInt64(Object original, Int64 defaultValue)
        {
            if (original == null) return defaultValue;

            if (original.ToString().Trim() == String.Empty) return defaultValue;

            try
            {
                return Int64.Parse(original.ToString());
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     四舍五入
        /// </summary>
        /// <param name="value"> </param>
        /// <returns> </returns>
        public static double Rounding(double value)
        {
            if (value < 0) return -RoundingPostive(-value);
            return RoundingPostive(value);
        }

        /// <summary>
        ///     四舍五入
        /// </summary>
        /// <param name="value"> </param>
        /// <returns> </returns>
        public static Decimal Rounding(Decimal value)
        {
            if (value < 0) return -RoundingPostive(-value);
            return RoundingPostive(value);
        }

        /// <summary>
        ///     正数四舍五入
        /// </summary>
        /// <param name="value"> </param>
        /// <returns> </returns>
        private static double RoundingPostive(double value)
        {
            if (value < 0) throw new Exception("方法只能格式化正数。");
            double vt = Math.Pow(10, 2);
            double vx = value * vt;

            vx += 0.5;
            return (Math.Floor(vx) / vt);
        }

        /// <summary>
        ///     正数四舍五入
        /// </summary>
        /// <param name="value"> </param>
        /// <returns> </returns>
        private static decimal RoundingPostive(decimal value)
        {
            if (value < 0) throw new Exception("方法只能格式化正数。");
            double vt = Math.Pow(10, 2);
            decimal vx = value * (decimal)vt;

            vx += 0.5M;
            return (Math.Floor(vx) / (decimal)vt);
        }

        public static Int32 ToInt32(String value, String errorMessage = @"Input value has invalid Int32 format.")
        {
            if (String.IsNullOrEmpty(value)) throw new ArgumentNullException();

            Int32 temp = 0;

            if (Int32.TryParse(value, out temp) == false) throw new InvalidCastException(errorMessage);

            return temp;
        }

        public static Int32 ToInt32(String value, Int32 defaultValue)
        {
            if (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value)) return defaultValue;

            Int32 temp = 0;

            if (Int32.TryParse(value, out temp) == false) return defaultValue;

            return temp;
        }

        public static Decimal ToDecimal(Object valueObject, String errorMessage = @"Input value has invalid decimal format.",
                                         Boolean needToRounding = false)
        {
            if (valueObject == null) throw new ArgumentNullException();

            string value = valueObject.ToString();

            if (String.IsNullOrEmpty(value)) throw new ArgumentNullException();

            Decimal temp = 0M;

            if (Decimal.TryParse(value, out temp) == false) throw new InvalidCastException(errorMessage);

            return needToRounding ? Rounding(temp) : temp;
        }

        public static Decimal ToMoney(Object valueObject, Int32 precision, Boolean needRunding = true)
        {
            if (valueObject == null) throw new ArgumentNullException();

            String value = valueObject.ToString();

            if (String.IsNullOrEmpty(value)) throw new ArgumentNullException();

            Decimal temp = 0M;

            if (Decimal.TryParse(value, NumberStyles.Currency, null, out temp) == false) return temp;

            return needRunding ? decimal.Round(temp, 2) : temp;
        }

        public static Decimal ToSgExchange(Object obj, Decimal exchange) { return ConvertExchange(obj, "SG", exchange); }

        public static Decimal ToRmbExchange(Object obj, Decimal exchange) { return ConvertExchange(obj, "RMB", exchange); }

        private static Decimal ConvertExchange(Object obj, String destination, Decimal exchange)
        {
            if (obj == null) return default(Decimal);

            Decimal original = default(Decimal);
            Decimal result = default(Decimal);

            if (Decimal.TryParse(obj.ToString(), out original) == false)
                throw new InvalidDataException("Object has invalid decimal format to be converted.");

            switch (destination)
            {
                case "SG":
                    result = original / exchange;
                    break;
                case "RMB":
                    result = original * exchange;
                    break;
                default:
                    result = original;
                    break;
            }

            return Decimal.Round(result, 2, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        ///     格式化为美国格式的日期
        /// </summary>
        /// <param name="dateTime"> </param>
        /// <returns> </returns>
        public static string ToUsaDateString(DateTime? dateTime)
        {
            if (!dateTime.HasValue) return string.Empty;
            DateTime value = dateTime.Value;
            string month = value.Month < 10 ? "0" + value.Month : value.Month.ToString();
            string day = value.Day < 10 ? "0" + value.Day : value.Day.ToString();
            return string.Format("{0}/{1}/{2}", month, day, value.Year);
        }

        /// <summary>
        ///     Convert the date time to Singapore date.
        /// </summary>
        /// <param name="dateTime"> </param>
        /// <param name="pattern"> </param>
        /// <returns> </returns>
        public static String ToSgDateString(DateTime? dateTime, String pattern = "dd-MM-yyyy") { return dateTime.HasValue == false ? String.Empty : dateTime.Value.ToString(pattern); }

        public static Boolean ToBoolean(String value)
        {
            if (String.IsNullOrEmpty(value)) return false;

            return value.Replace(" ", String.Empty).Trim().ToUpper() == "Y";
        }

        public static DateTime ToDateTime(Object obj, String cultureInfo = "zh-sg")
        {
            if (obj == null) throw new ArgumentNullException("Parameter 'obj' is null.");

            string dateTimeText = obj.ToString();

            if (String.IsNullOrWhiteSpace(dateTimeText)) throw new ArgumentException("Original object is empty string.");

            DateTime tempValue;
            if (DateTime.TryParse(dateTimeText, new CultureInfo(cultureInfo), DateTimeStyles.AssumeLocal, out tempValue))
                return tempValue;

            throw new ArgumentException("Cannot convert the string to date time value.");
        }

        public static DateTime? ToDateTimeIfNull(Object obj, String cultureInfo = "zh-sg")
        {
            if (obj == null) return null;
            if (String.IsNullOrWhiteSpace(obj.ToString())) return null;

            return ToDateTime(obj, cultureInfo);
        }

        public static String ToString(DateTime dateTime, Boolean returnShortTime = false)
        {
            if (dateTime == default(DateTime)) return String.Empty;

            return returnShortTime ? dateTime.ToString("dd/MM/yyyy") : dateTime.ToString("dd/MM/yyyy hh:mm:ss");
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

        /// <summary>
        ///     删除分隔符
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveSplit(object value)
        {
            if (value == null)
                return "";
            string newValue = value.ToString().Trim();
            newValue = newValue.Replace("-", "");
            newValue = newValue.Replace("/", "");
            newValue = newValue.Replace("(", "");
            newValue = newValue.Replace(")", "");
            return newValue;
        }

        /// <summary>
        ///     日期格式化
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DateTimeMmddyyyy(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return "";
            else
                return DateTime.Parse(value.ToString()).ToString("MM/dd/yyyy");
        }

        /// <summary>
        ///     dd/MM/yyyy
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DateTimeDdmmyyyy(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return "";
            else
                return DateTime.Parse(value.ToString()).ToString("dd/MM/yyyy");
        }

        /// <summary>
        ///     HTML JQ DateControl (dd/MM/yyyy) 转为 Date (MM/dd/yyyy)
        /// </summary>
        /// <returns></returns>
        public static string DateTimeStringForMy(string myCulturDate)
        {
            try
            {
                DateTime dt = DateTime.ParseExact(myCulturDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                return dt.ToString("MM/dd/yyyy");
            }
            catch (Exception)
            {
                var culture = new CultureInfo("zh-SG");
                return Convert.ToDateTime(myCulturDate.Trim(), culture).ToString("MM/dd/yyyy");
            }
        }

        /// <summary>
        ///     dd/MM/yyyy hh:mm
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DateAndTimeString(string value)
        {
            if (value == null || string.IsNullOrEmpty(value))
                return "";
            else
                return DateTime.Parse(value).ToString("dd/MM/yyyy HH:mm");
        }

        /// <summary>
        ///     格式化为美国格式的日期
        /// </summary>
        /// <param name="dateString"></param>
        /// <returns></returns>
        public static string ToUsaDateString(string dateString)
        {
            if ((dateString.IndexOf("/") == -1) && (dateString.Length == 8) && (dateString.IndexOf('-') == -1))
                dateString = dateString.Substring(0, 2) + "/" + dateString.Substring(2, 2) + "/" + dateString.Substring(4, 4);
            return dateString;
        }

        /// <summary>
        ///     获取指定长度的字符串
        /// </summary>
        /// <param name="content"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Substring(string content, int startIndex, int length)
        {
            if (string.IsNullOrEmpty(content)) return content;
            if (content.Length > length) return content.Substring(startIndex, length) + "...";
            return content;
        }

        /// <summary>
        ///     中英字符截取。
        /// </summary>
        /// <param name="s"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        public static string SubstringForZhEn(string s, int l)
        {
            string temp = s;
            if (Regex.Replace(temp, "[\u4e00-\u9fa5]", "zz", RegexOptions.IgnoreCase).Length <= l)
                return temp;
            for (int i = temp.Length; i >= 0; i--)
            {
                temp = temp.Substring(0, i);
                if (Regex.Replace(temp, "[\u4e00-\u9fa5]", "zz", RegexOptions.IgnoreCase).Length <= l - 3)
                    return temp + "...";
            }
            return "";
        }

        [Obsolete("Please use method [ToDateTimeExtract] instead of this.")]
        public static DateTime? ToDateTime(string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                    return null;

                return Convert.ToDateTime(value);
            }
            catch (Exception ex)
            {
                LogContent logContent = new LogContent
                {
                    SourceType = "ConvertHelper",
                    Action = "ToDateTime",
                    CreateBy = "ConvertHelper",
                    Message = ex.Message,
                    RequestUrl = string.Format("Convert {0} to datetime object failed.", value),
                    StackTrace = ex.ToString()
                };
                LogUnity.Error(logContent);

            }

            try
            {
                return ToDateTime(value, "zh-sg");
            }
            catch (Exception ex)
            {
                LogContent logContent = new LogContent
                {
                    SourceType = "ConvertHelper",
                    Action = "ToDateTime",
                    CreateBy = "ConvertHelper",
                    Message = ex.Message,
                    RequestUrl = string.Format("Convert {0} to datetime object failed, by cultrue zh-sg.", value),
                    StackTrace = ex.ToString()
                };
                LogUnity.Error(logContent);

            }

            return null;
        }

        public static DateTime ToDateTimeExtract(String value, String format = "dd/MM/yyyy")
        {
            try
            {
                return DateTime.ParseExact(value, format, CultureInfo.InvariantCulture);
            }
            catch (Exception exception)
            {
                LogContent logContent = new LogContent
                {
                    SourceType = "ConvertHelper",
                    Action = "ToDateTimeExtract",
                    CreateBy = "ConvertHelper",
                    Message = exception.Message,
                    RequestUrl = string.Format("Convert {0} to datetime object failed.", value),
                    StackTrace = exception.ToString()
                };
                LogUnity.Error(logContent);

                throw;
            }
        }

        /// <summary>
        ///     格式化HTML格式
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string FormatTitle(object content)
        {
            if (content == null) return string.Empty;
            if (string.IsNullOrEmpty(content.ToString())) return content.ToString();
            return HttpUtility.HtmlEncode(content.ToString());
        }

        /// <summary>
        ///     比较指定时间和现在的距离
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string CompareToNow(string dateTime)
        {
            if (!IsDateTimeFormat(dateTime)) return string.Empty;
            DateTime now = DateTime.Now;
            DateTime? compareTime = ToDateTime(dateTime);
            TimeSpan timeSpan = now.Subtract(compareTime.Value);
            int minutes = timeSpan.Minutes;
            if (minutes < 60) return string.Format("{0} 分钟前", minutes);
            return string.Format("{0} 小时前", minutes / 60);
        }

        /// <summary>
        ///     比较两个日期之间的天数间隔
        /// </summary>
        /// <param name="value1">小的日期</param>
        /// <param name="value2">大的日期</param>
        /// <returns></returns>
        public static int Compare(DateTime value1, DateTime value2)
        {
            var timeSpan1 = new TimeSpan(value1.Ticks);
            var timeSpan2 = new TimeSpan(value2.Ticks);
            TimeSpan duration = timeSpan2.Subtract(timeSpan1);
            return duration.Days;
        }

        /// <summary>
        ///     是否比今天早
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static bool BeforeToday(string dateTime)
        {
            if (string.IsNullOrEmpty(dateTime)) return false;
            if (!IsDateTimeFormat(dateTime)) return false;
            DateTime compareTime = ToDateTime(dateTime).Value;
            if (DateTime.Today.CompareTo(compareTime) > 0) return true;
            return false;
        }

        /// <summary>
        ///     保留2位小数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToFloat(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString())) return string.Empty;
            return String.Format("{0:F}", value);
        }

        /// <summary>
        ///     删除字符中的HTML内容
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveHtml(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            var regex1 = new Regex(@"<script[\s\S]+</script *>", RegexOptions.IgnoreCase);
            var regex2 = new Regex(@" href *= *[\s\S]*script *:", RegexOptions.IgnoreCase);
            var regex3 = new Regex(@" no[\s\S]*=", RegexOptions.IgnoreCase);
            var regex4 = new Regex(@"<iframe[\s\S]+</iframe *>", RegexOptions.IgnoreCase);
            var regex5 = new Regex(@"<frameset[\s\S]+</frameset *>", RegexOptions.IgnoreCase);
            var regex6 = new Regex(@"\<img[^\>]+\>", RegexOptions.IgnoreCase);
            var regex7 = new Regex(@"</p>", RegexOptions.IgnoreCase);
            var regex8 = new Regex(@"<p>", RegexOptions.IgnoreCase);
            var regex9 = new Regex(@"<[^>]*>", RegexOptions.IgnoreCase);
            var regex10 = new Regex(@"<.*?>", RegexOptions.IgnoreCase);
            value = regex1.Replace(value, ""); //过滤<script></script>标记 
            value = regex2.Replace(value, ""); //过滤href=javascript: (<A>) 属性 
            value = regex3.Replace(value, " _disibledevent="); //过滤其它控件的on...事件 
            value = regex4.Replace(value, ""); //过滤iframe 
            value = regex5.Replace(value, ""); //过滤frameset 
            value = regex6.Replace(value, ""); //过滤frameset 
            value = regex7.Replace(value, ""); //过滤frameset 
            value = regex8.Replace(value, ""); //过滤frameset 
            value = regex9.Replace(value, "");
            value = regex10.Replace(value, "");
            value = value.Replace("</strong>", "");
            value = value.Replace("<strong>", "");
            value = value.Trim();
            return value;
        }
    }
}