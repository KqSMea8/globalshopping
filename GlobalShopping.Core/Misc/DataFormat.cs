using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;

namespace GlobalShopping.Core.Misc
{
    public class DataFormat
    {
        /// <summary>
        ///     ɾ���ָ���
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
        ///     ���ڸ�ʽ��
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
        ///     HTML JQ DateControl (dd/MM/yyyy) תΪ Date (MM/dd/yyyy)
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
        ///     ��ʽ��Ϊ������ʽ������
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
        ///     ��ʽ��Ϊ������ʽ������
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
        ///     ��ȡָ�����ȵ��ַ���
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
        ///     ��Ӣ�ַ���ȡ��
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
        ///     ��ʽ��HTML��ʽ
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
        ///     �Ƚ�ָ��ʱ������ڵľ���
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
            if (minutes < 60) return string.Format("{0} ����ǰ", minutes);
            return string.Format("{0} Сʱǰ", minutes / 60);
        }

        /// <summary>
        ///     �Ƿ�Ƚ�����
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
        ///     ����2λС��
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToFloat(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString())) return string.Empty;
            return String.Format("{0:F}", value);
        }

        /// <summary>
        ///     ɾ���ַ��е�HTML����
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
            value = regex1.Replace(value, ""); //����<script></script>��� 
            value = regex2.Replace(value, ""); //����href=javascript: (<A>) ���� 
            value = regex3.Replace(value, " _disibledevent="); //���������ؼ���on...�¼� 
            value = regex4.Replace(value, ""); //����iframe 
            value = regex5.Replace(value, ""); //����frameset 
            value = regex6.Replace(value, ""); //����frameset 
            value = regex7.Replace(value, ""); //����frameset 
            value = regex8.Replace(value, ""); //����frameset 
            value = regex9.Replace(value, "");
            value = regex10.Replace(value, "");
            value = value.Replace("</strong>", "");
            value = value.Replace("<strong>", "");
            value = value.Trim();
            return value;
        }
    }
}