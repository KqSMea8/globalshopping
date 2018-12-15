using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalShopping.Web.API
{
    public abstract class ApiBase
    {
        public virtual string Name { get; set; }

        public ApiRequest Request { get; set; }
    }
}
