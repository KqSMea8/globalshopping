using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Web;

namespace GlobalShopping.Core.Utility
{
    /// <summary>
    ///     string 类型扩展
    /// </summary>
    public static class StringExtensions
    {
        public static string RemoveHTML(this string str)
        {
            if (!string.IsNullOrEmpty(str))
                return Regex.Replace(str, "<[^>]*>", "").Trim();
            return null;
        }

        public static string GetMatchContent(this string content, string regexExpression)
        {
            var result = string.Empty;

            var regContent = new Regex(regexExpression, RegexOptions.Compiled);
            var matchContent = regContent.Match(content);
            if (matchContent.Success)
            {
                result = matchContent.Value;
            }

            return result;
        }

        public static string GetMatchContent(this string content, string regexStart, string regexEnd)
        {
            var result = string.Empty;

            var regContent = new Regex(string.Format("(?<={0})(.|\n)+?(?={1})", regexStart, regexEnd),
                RegexOptions.Compiled);
            var matchContent = regContent.Match(content);
            if (matchContent.Success)
            {
                result = matchContent.Value.Trim().HtmlDecode().Trim(',');
            }

            return result;
        }

        public static string Format(this string input, object p)
        {
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(p))
                input = input.Replace("{" + prop.Name + "}", (prop.GetValue(p) ?? "(null)").ToString());

            return input;
        }

        public static string GetPhotoBySize(this string input, int size)
        {
            var result = Regex.Replace(input, @"\d+?x\d+\.", string.Format("{0}x{0}.", size));

            return result;
        }

        public static string GetQueryValue(this string url, string key)
        {
            return HttpUtility.ParseQueryString(new Uri(url).Query).Get(key);
        }

        /// <summary>
        /// 美国的网站通常在meta里都有OG这个信息，是提供给第三方网站用的
        /// </summary>
        /// <param name="input"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetOGMetaInfo(this string input, string name)
        {
            string regexStart = string.Format("<meta.*?property=\"og:{0}\".*?content=(\"|')", name);
            return input.GetMatchContent(regexStart, "(\"|')");
        }

        public static string GetTwitterMetaInfo(this string input, string name)
        {
            string regexStart = string.Format("<meta.*?name=\"twitter:{0}\".*?content=(\"|')", name);
            return input.GetMatchContent(regexStart, "(\"|')");
        }

        public static string GetFacebookMetaInfo(this string input, string name)
        {
            string regexStart = string.Format("<meta.*?property=\"fb:{0}\".*?content=(\"|')", name);
            return input.GetMatchContent(regexStart, "(\"|')");
        }

        public static string GetMetaValue(this string input, string name)
        {
            string regexStart = string.Format("<meta.*?name=\"{0}\".*?content=(\"|')", name);
            return input.GetMatchContent(regexStart, "\"");
        }

        public static string GetMetaPropertyValue(this string input, string name)
        {
            string regexStart = string.Format("<meta.*?property=\"{0}\".*?content=(\"|')", name);
            return input.GetMatchContent(regexStart, "\"");
        }
    }
}