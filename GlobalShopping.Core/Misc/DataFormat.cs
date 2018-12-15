using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;

namespace GlobalShopping.Core.Misc
{
    public class DataFormat
    {
        /// <summary>
        ///     删除分隔符
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveSplit(object value)
        {
            if (value == null) return "";
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
        public static string DateTimeMMDDYYYY(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString())) return "";
            else return DateTime.Parse(value.ToString()).ToString("MM/dd/yyyy");
        }

        /// <summary>
        ///     dd/MM/yyyy
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DateTimeDDMMYYYY(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString())) return "";
            else return DateTime.Parse(value.ToString()).ToString("dd/MM/yyyy");
        }

        /// <summary>
        ///     HTML JQ DateControl (dd/MM/yyyy) 转为 Date (MM/dd/yyyy)
        /// </summary>
        /// <param name="jqDateTControlValue"></param>
        /// <returns></returns>
        public static string DateTimeStringForMY(string MYCulturDate)
        {
            try
            {
                var culture = new CultureInfo("ms-MY");
                return Convert.ToDateTime(MYCulturDate.Trim(), culture).ToString("MM/dd/yyyy");
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        ///     dd/MM/yyyy hh:mm
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DateAndTimeString(string value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString())) return "";
            else return DateTime.Parse(value.ToString()).ToString("dd/MM/yyyy HH:mm");
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
        ///     格式化为美国格式的日期
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToUsaDateString(DateTime? dateTime)
        {
            if (!dateTime.HasValue) return string.Empty;
            DateTime value = dateTime.Value;
            string month = value.Month < 10 ? "0" + value.Month : value.Month.ToString();
            string day = value.Day < 10 ? "0" + value.Day : value.Day.ToString();
            return string.Format("{0}/{1}/{2}", month, day, value.Year);
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
            var value = new StringInfo(content);
            if (value.LengthInTextElements > length) return value.SubstringByTextElements(startIndex, length) + "...";
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
            if (Regex.Replace(temp, "[\u4e00-\u9fa5]", "zz", RegexOptions.IgnoreCase).Length <= l) return temp;
            for (int i = temp.Length; i >= 0; i--)
            {
                temp = temp.Substring(0, i);
                if (Regex.Replace(temp, "[\u4e00-\u9fa5]", "zz", RegexOptions.IgnoreCase).Length <= l - 3) return temp + "...";
            }
            return "";
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
            if (!DataConvert.IsDateTimeFormat(dateTime)) return string.Empty;
            DateTime now = DateTime.Now;
            DateTime? compareTime = DataConvert.ToDateTime(dateTime);
            TimeSpan timeSpan = now.Subtract(compareTime.Value);
            int minutes = timeSpan.Minutes;
            if (minutes < 60) return string.Format("{0} 分钟前", minutes);
            return string.Format("{0} 小时前", minutes / 60);
        }

        /// <summary>
        ///     是否比今天早
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static bool BeforeToday(string dateTime)
        {
            if (string.IsNullOrEmpty(dateTime)) return false;
            if (!DataConvert.IsDateTimeFormat(dateTime)) return false;
            DateTime compareTime = DataConvert.ToDateTime(dateTime).Value;
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