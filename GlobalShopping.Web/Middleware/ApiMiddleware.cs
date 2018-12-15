using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using GlobalShopping.Web.API;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using GlobalShopping.Web.APIResponse;

namespace GlobalShopping.Web.Middleware
{
    public class ApiMiddleware
    {
        private readonly RequestDelegate _next;
        private Options Options;
        public ApiMiddleware(RequestDelegate next)
        {
            _next = next;
            Options = Startup.GetOptions();
        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;
            var path = request.Path.ToString();
            path = path.Trim('/');
            if(!path.StartsWith(Options.ApiPrefox,StringComparison.OrdinalIgnoreCase))
            {
                await _next.Invoke(context);
            }
            var apiClass = GetApiClass(path);

            var response = new JsonReponse();
            try
            {
                var result = ApiManager.Execute(apiClass, context.Request);
                response.Success = true;
                response.Model = result;
                response.Message = string.Empty;
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            

            context.Response.ContentType = "application/json";
            var content = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));
            await context.Response.Body.WriteAsync(content,0,content.Length);
        }

        public ApiClass GetApiClass(string path)
        {
            path = path.Trim('/');
            var parts = path.Split("/");
            if (parts.Length < 3)
            {
                throw new Exception("api method is not exist.");
            }
            var classPart = parts[1];
            var method = parts[2];
            return new ApiClass()
            {
                Class = classPart,
                Method = method
            };
        }


    }

    public class ApiClass
    {
        public string Class { get; set; }

        public string Method { get; set; }
    }
}
