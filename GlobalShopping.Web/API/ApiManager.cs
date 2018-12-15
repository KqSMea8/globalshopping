using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using GlobalShopping.Lib;
using GlobalShopping.Web.API;
using GlobalShopping.Web.Middleware;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace GlobalShopping.Web.API
{
    public static class ApiManager
    {
        public static Dictionary<string, ApiBase> List { get; private set; }

        static ApiManager()
        {
            var apiList = AssemblyLoader.LoadByInterface(typeof(ApiBase));
            List = new Dictionary<string, ApiBase>();
            foreach (var api in apiList)
            {
                var instance = Activator.CreateInstance(api) as ApiBase;
                List.Add(instance.Name.ToLower(), instance);
            }
        }

        public static object GetInstance(string name)
        {
            name = name.ToLower();
            if (List.ContainsKey(name))
                return List[name];
            return null;
        }

        public static MethodInfo GetMethod(object instance,string methodName)
        {
            var type = instance.GetType();
            var methods = type.GetMethods();
            foreach(var method in methods)
            {
                if(method.Name.ToLower()==methodName.ToLower())
                {
                    return method;
                }
            }
            return null;
        }

        private static object[] GetParams(MethodInfo method,ApiRequest request)
        {
            var pars = method.GetParameters();

            object[] obj = new object[pars.Length];
            var index = 0;

            var error = new StringBuilder();
            foreach(var param in pars)
            {
                object value = null;
                var requestValue =request.GetValue(param.Name);
                if (requestValue != null)
                {
                    if (param.ParameterType.IsValueType || param.ParameterType == typeof(string))
                    {
                        value = TypeHelper.ChangeType(requestValue, param.ParameterType);
                    }
                    else if(param.ParameterType.IsClass)
                    {
                        value = JsonConvert.DeserializeObject(requestValue, param.ParameterType);
                    }
                }
                //else
                //{
                //    if (param.ParameterType.IsClass)
                //    {
                //        var body = request.HttpRequest.Body;
                //        if (body != null && body.CanRead)
                //        {
                //            var memoryStream = new System.IO.MemoryStream();
                //            var task = body.CopyToAsync(memoryStream);
                //            task.Wait();
                //            var bytes = memoryStream.ToArray();
                //            var str = System.Text.Encoding.UTF8.GetString(bytes);
                //            str = System.Web.HttpUtility.UrlDecode(str);
                //            value = JsonConvert.DeserializeObject(str, param.ParameterType);
                //        }
                //        else
                //        {
                //            error.AppendFormat("param {0} not exist", param.Name);
                //        }

                //    }
                //    else
                //    {
                //        error.AppendFormat("param {0} not exist", param.Name);
                //    }


                //}
                obj[index] = value;
                index++;
            }
            if (error.Length > 0)
            {
                throw new Exception(error.ToString());
            }
            return obj;

        }

        //private static object GetModel(HttpRequest request)
        //{
        //    if()
        //}

        private static object GetValue(HttpRequest request,string name)
        {
            StringValues stringValue;
            if (request.Query.TryGetValue(name, out stringValue))
            {
                return stringValue.ToString();
            }
            
            return null;
        }
        public static object Execute(ApiClass apiClass,HttpRequest request)
        {
            var instance = GetInstance(apiClass.Class);
            if (instance == null)
                throw new Exception("class not exist");

            var requestInstance = instance as ApiBase;

            var apiRequest = new ApiRequest(request);

            requestInstance.Request = apiRequest;
            var method = GetMethod(instance, apiClass.Method);
            if (method == null)
            {
                throw new Exception("method not exist");
            }

            var pars = GetParams(method, apiRequest);
            var obj = method.Invoke(instance, pars);

            return obj;
        }
    }
}
