using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace GlobalShopping.Web.Request
{
    public class RequestManager
    {
        public static string GetValue(HttpRequest request, string key)
        {
            return TryGetValue(request, key);
        }

        private static string TryGetValue(HttpRequest request, string name)
        {
            StringValues stringValues;
            if (request.Query.TryGetValue(name,out stringValues))
            {
                return System.Net.WebUtility.UrlDecode(stringValues.ToString());
            }
            if(request.Form.TryGetValue(name,out stringValues))
            {
                return stringValues.ToString();
            }

            if(request.Headers.TryGetValue(name,out stringValues))
            {
                return stringValues.ToString();
            }

            if (request.Cookies.ContainsKey(name))
            {
                return request.Cookies[name];
            }
            //todo get model
            return null;
        }
    }
}
