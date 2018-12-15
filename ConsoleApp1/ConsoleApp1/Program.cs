using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //var appkey = "25313721";
            //var secret = "7baeb2a26918600d6e3a2fa3209c9211";

            //var appkey = "12193480";
            //var secret = "3091a169a0106c59f0e2fb769939ca8a";

            var appkey = "23238713";
            var secret = "35bc06de312ac2999c0b3930ce8c1e56";

            //var appkey = "23346767";
            //var secret = "0e3e924ebec051da26438087571f242c";


            var url = "http://gw.api.taobao.com/router/rest";
            ITopClient client = new DefaultTopClient(url, appkey, secret);
            TimeGetRequest req = new TimeGetRequest();
            TimeGetResponse rsp = client.Execute(req);
            Console.WriteLine(rsp.Body);
        }
    }
}
