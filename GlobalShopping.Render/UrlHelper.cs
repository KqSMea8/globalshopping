using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalShopping.Render
{
    public class UrlHelper
    {
        public static string FileExtension(string fullurl)
        {
            if (String.IsNullOrWhiteSpace(fullurl))
            {
                return String.Empty;
            }

            if (fullurl.IndexOf("?") > -1)
            {
                var index = fullurl.IndexOf("?");
                fullurl = fullurl.Substring(0, index);
            }

            fullurl = fullurl.Trim('\\', '/');

            return System.IO.Path.GetExtension(fullurl.ToLower());
        }
    }
}
