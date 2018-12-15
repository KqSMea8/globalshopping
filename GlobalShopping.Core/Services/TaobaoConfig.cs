using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalShopping.Core.Services
{
   public class TaobaoConfig
    {
        public static string Url
        {
            get
            {
                return "http://gw.api.taobao.com/router/rest";
            }
        }

        public static string AppKey
        {
            get
            {
                return "23346767";
            }
        }

        public static string AppSecret
        {
            get
            {
                return "0e3e924ebec051da26438087571f242c";
            }
        }
    }
}
