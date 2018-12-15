using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalShopping.Web.APIResponse
{
    public class JsonReponse
    {
        public bool Success { get; set; }

        public object Model { get; set; }

        public string Message { get; set; }
    }
}
