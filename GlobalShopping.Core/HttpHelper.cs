using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace GlobalShopping.Core
{
    class HttpHelper
    {
               
        public static T Get<T>(string url, bool mustEncoding = false)
        {
            var encoding = Encoding.UTF8;         
            var request = WebRequest.Create(url);
            return InternalPostData<T>(request, encoding, null, mustEncoding, "application/json", "GET");
        }

        public static T Post<T>(string url, object obj, string prefix = null, bool mustEncoding = false,
             string dataprefix = null)
        {
            var encoding = Encoding.UTF8;
            string data = JsonConvert.SerializeObject(obj);

            if (mustEncoding)
                data = HttpUtility.UrlEncode(data, encoding);

            if (!string.IsNullOrEmpty(dataprefix))
                data = dataprefix + data;

            if (!string.IsNullOrWhiteSpace(prefix))
                url = string.Format("{0}{1}{2}", url, url.Contains("?") ? "&" : "?", prefix);


            var request = WebRequest.Create(url) as HttpWebRequest;
            if (request == null)
                throw new NotSupportedException("只支持Http格式的url");

            byte[] bytes = encoding.GetBytes(data);
            return InternalPostData<T>(request, encoding, bytes, mustEncoding, "application/x-www-form-urlencoded");
        }

        private static T InternalPostData<T>(WebRequest request, Encoding encoding, byte[] bytes = null,
            bool mustEncoding = false, string contentType = "application/json", string method = "POST")
        {
            string data;
            request.Method = method;
            request.ContentType = contentType;

            if (bytes != null)
            {
                request.ContentLength = bytes.Length;
                request.GetRequestStream().Write(bytes, 0, bytes.Length);
            }
            using (var response = request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream(), encoding))
                    data = reader.ReadToEnd();
            }
            if (mustEncoding)
                data = HttpUtility.UrlDecode(data, encoding);
         
            return JsonConvert.DeserializeObject<T>(data);
        }     
    }
}
