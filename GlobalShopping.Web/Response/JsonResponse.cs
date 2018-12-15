using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalShopping.Web.Response
{
    public class JsonResponse:ResponseBase
    {
        public override string ContentType { get; set; } = "application/json";

    }
}
