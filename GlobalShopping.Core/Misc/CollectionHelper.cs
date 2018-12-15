using System;
using System.Collections.Specialized;
using System.Web;

namespace Zen.Framework.Misc
{
    public class CollectionHelper
    {
        public static NameValueCollection UpperParams(NameValueCollection nameValueCollection)
        {
            var newNameValueCollection = new NameValueCollection(nameValueCollection.Count);

            foreach (var item in nameValueCollection.AllKeys)
                newNameValueCollection.Add((item ?? String.Empty).ToUpper(), HttpContext.Current.Server.UrlDecode(nameValueCollection[item ?? String.Empty]));

            return newNameValueCollection;
        }
    }
}