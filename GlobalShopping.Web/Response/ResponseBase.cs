using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace GlobalShopping.Web.Response
{
    public class ResponseBase
    {
        public virtual string ContentType { get; set; } = "text/plain";
        public HttpContext Context { get; set; }
        public object Content { get; set; }

        public void Write()
        {
            var response = Context.Response;
            response.StatusCode = 200;
            response.ContentType = ContentType;

        }
    }
}
