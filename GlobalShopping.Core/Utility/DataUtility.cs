using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using GlobalShopping.Core.Misc;
using System.Net.Http;

namespace GlobalShopping.Core.Utility
{
    public class DataUtility
    {
        public static bool IsDouble(string value)
        {
            try
            {
                var result = Convert.ToDouble(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string GetHtml(string url, string cookie = "")
        {
            var http = new HttpUtil();
            var request = new HttpItem();

            var header = new System.Net.WebHeaderCollection
            {
                { "Cookie",cookie},
                {"Cache-Control", "max-age=0"},
                {"Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7"},
                {"Accept-Language", "en-US,en;q=0.8,zh-CN;q=0.6,zh;q=0.4"},
                {"Accept-Encoding", "gzip,deflate"},
                {"Pragma", ""}
            };

            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.125 Safari/537.36";
            request.Timeout = 10000;
            request.Allowautoredirect = true;
            request.Header = header;
            request.URL = url;
            var result = http.GetHtml(request);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
                return result.Html;
            else
                return string.Empty;
        }

        public static string GetHtmlByHttpClient(string url)
        {
            var httpClient = new System.Net.Http.HttpClient();
            httpClient.DefaultRequestHeaders.Add("user-agent",
                "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");
            var response = httpClient.GetAsync(new Uri(url)).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        public static string GetHtmlByHttpClientForMobile(string url)
        {
            var httpClient = new System.Net.Http.HttpClient();
            httpClient.DefaultRequestHeaders.Add("user-agent",
                "Mozilla/5.0 (iPhone; CPU iPhone OS 8_0 like Mac OS X) AppleWebKit/600.1.3 (KHTML, like Gecko) Version/8.0 Mobile/12A4345d Safari/600.1.4");
            var response = httpClient.GetAsync(new Uri(url)).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        public static string GetPageContent(string productUrl, Encoding encoding)
        {
            try
            {
                //change HttpClient to WebClient
                var webClient = new System.Net.WebClient();
                // 模拟 IE 6.0
                // 部分网站检测浏览器
                webClient.Headers.Add("Accept", "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, */*");
                webClient.Headers.Add("Accept-Language", "zh-CN");
                webClient.Headers.Add("User-Agent",
                    "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)");

                var pageData = webClient.DownloadData(productUrl.Replace("&pfrom=transfer", ""));
                string pageContent;

                if (encoding == null)
                {
                    pageContent = Encoding.Default.GetString(pageData);
                    encoding = GetEncoding(pageContent);

                    if (encoding != null && encoding != Encoding.Default)
                    {
                        pageContent = encoding.GetString(pageData);
                    }
                }
                else
                {
                    pageContent = encoding.GetString(pageData);
                }

                pageContent = pageContent.Replace("\r\n", "");
                pageContent = pageContent.Replace("\t", "");
                pageContent = pageContent.Replace("\r", "");
                pageContent = pageContent.Replace("\n", "");

                return pageContent;
            }
            catch (Exception e)
            {
                return string.Format("远程访问页面错误，由于{0}", e.Message);
            }
        }

        // 根据网页的HTML内容提取网页的Encoding
        private static Encoding GetEncoding(string html)
        {
            var pattern = @"(?i)\bcharset=(?<charset>[-a-zA-Z_0-9]+)";
            var charset = Regex.Match(html, pattern).Groups["charset"].Value;

            try
            {
                // TMD，凡客诚品竟然写错了
                if (charset == "uft-8")
                {
                    charset = "utf-8";
                }
                if (string.IsNullOrEmpty(charset))
                {
                    //使用默认的encoding
                    return null;
                }
                return Encoding.GetEncoding(charset);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }


        /// <summary>
        ///     从文件中反序列化对象并返回它的引用。
        /// </summary>
        /// <param name="filename">源文件名。</param>
        /// <param name="objectType">对象类型。</param>
        /// <param name="binarySerialization">是否采用二进制序列化。</param>
        /// <returns>反序列化出来的对象。</returns>
        public static object DeSerializeObject(string filename, Type objectType, bool binarySerialization)
        {
            object Instance = null;

            if (!binarySerialization)
            {
                XmlReader reader = null;
                XmlSerializer serializer = null;
                FileStream fs = null;
                try
                {
                    serializer = new XmlSerializer(objectType);

                    fs = new FileStream(filename, FileMode.Open);
                    reader = new XmlTextReader(fs);

                    Instance = serializer.Deserialize(reader);
                }
                catch
                {
                    return null;
                }
                finally
                {
                    if (fs != null)
                        fs.Close();

                    if (reader != null)
                        reader.Close();
                }
            }
            else
            {
                BinaryFormatter serializer = null;
                FileStream fs = null;

                try
                {
                    serializer = new BinaryFormatter();
                    fs = new FileStream(filename, FileMode.Open);
                    Instance = serializer.Deserialize(fs);
                }
                catch
                {
                    return null;
                }
                finally
                {
                    if (fs != null)
                        fs.Close();
                }
            }

            return Instance;
        }

        public static bool IsEmptyContent(string content)
        {
            if (string.IsNullOrEmpty(content) || content.Length < 1000) return true;

            return false;
        }

        public static string RemoveHtml(string value)
        {
            return DataFormat.RemoveHtml(value).HtmlDecode();
        }

        public static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        public static string GetRefId(string url)
        {
            var uri = new Uri(url);
            if (uri.Host.IndexOf("taobao.com") > -1 || uri.Host.IndexOf("tmall.com") > -1)
            {
                var query = System.Web.HttpUtility.ParseQueryString(uri.Query);
                string id = query["id"];
                if (!string.IsNullOrEmpty(id))
                {
                    return id;
                }

                var end = uri.LocalPath.IndexOf(".htm");
                if (end > -1)
                {
                    var start = uri.LocalPath.LastIndexOf("/");
                    var key = uri.LocalPath.Substring(start + 1, end - start - 1);
                    if (IsDigitsOnly(key))
                    {
                        return key;
                    }
                }
            }
            //else if (url.Contains("ezbuy=ezbuy"))
            //{
            //    return "ezbuy:" + EzbuyProductWapper.GetId(url, SpikeProductManager.indentifier);
            //}
            return url;
        }
    }
}