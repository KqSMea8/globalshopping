using System;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using System.Text;
using GlobalShopping.Web.Middleware;
using GlobalShopping.Lib;
using System.Diagnostics;


namespace GlobalShopping.Web
{
    class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<ApiMiddleware>();
            app.UseMiddleware<RenderMiddleware>();
            
        }
        static void Main(string[] args)
        {
            var port = GetPort();

            var host = new WebHostBuilder()
                .UseKestrel(options =>
                {
                    options.Listen(IPAddress.Loopback, port);
                }).
                UseStartup<Startup>()
                .Build();
            host.Run();

            //var defaultStartUrl = "http://127.0.0.1";
            //if (port != 80)
            //{
            //    defaultStartUrl += ":" + port;
            //}
            //Process.Start(defaultStartUrl);
        }
        
        public static int GetPort()
        {
            var port = 80;
            while (NetworkHelper.IsPortInUse(port) && port < 65535)
            {
                port += 1;
            }
            return port;
        }
        public static Options GetOptions()
        {
            var options = new Options()
            {
                ApiPrefox = "_api",
                StartPath = "_Admin",
                ViewFolder = "View",
                ScriptFolder = "Script",
                StyleFolder = "Style",
                ImageFolder = "Image"
            };
            return options;
        }
    }
}
