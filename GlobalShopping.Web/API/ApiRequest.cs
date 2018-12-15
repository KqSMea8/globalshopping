using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using GlobalShopping.Web.Request;

namespace GlobalShopping.Web.API
{
    public class ApiRequest
    {
        private HttpRequest _httpRequest;

        public ApiRequest(HttpRequest request)
        {
            _httpRequest = request;
        }
        public IFormFileCollection Files
        {
            get
            {
                return _httpRequest.Form.Files;
            }
        }

        public byte[] GetBytes(IFormFile file)
        {
            var ms = new System.IO.MemoryStream();
            file.CopyTo(ms);
            return ms.ToArray();
        }
        public T GetValue<T>(string name)
        {
            var type = typeof(T);
            string value = GetValue(name);
            if (string.IsNullOrEmpty(value))
            {
                return default(T);
            }
            if (type == typeof(Guid))
            {
                Guid id = default(Guid);
                System.Guid.TryParse(value, out id);
                return (T)Convert.ChangeType(id, type);
            }
            else if (type == typeof(bool))
            {
                bool ok = false;
                bool.TryParse(value, out ok);
                return (T)Convert.ChangeType(ok, type);
            }
            else if (type == typeof(int))
            {
                int intvalue = 0;
                int.TryParse(value, out intvalue);
                return (T)Convert.ChangeType(intvalue, type);
            }
            else if (type == typeof(long))
            {
                long longvalue = 0;
                long.TryParse(value, out longvalue);
                return (T)Convert.ChangeType(longvalue, type);
            }
            else if (type == typeof(string))
            {
                return (T)Convert.ChangeType(value, type);
            }
            else
            {
                throw new Exception("type of not supported");
            }

        }

        public string GetValue(string name)
        {
            return RequestManager.GetValue(_httpRequest, name);
        }
    }
}
