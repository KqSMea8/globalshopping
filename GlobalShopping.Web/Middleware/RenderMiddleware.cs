using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using GlobalShopping.Render;
using GlobalShopping.Lib;
using System.IO;
using System.Configuration;

namespace GlobalShopping.Web.Middleware
{
    public class RenderMiddleware
    {
        private readonly RequestDelegate _next;
        private Options Options;
        public RenderMiddleware(RequestDelegate next)
        {
            _next = next;
            Options = Startup.GetOptions();
        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;
            var path = request.Path.ToString();
            path = path.Trim('/');
            path = GetDefaultPath(path);
            if (!path.StartsWith(Options.StartPath, StringComparison.OrdinalIgnoreCase))
            {
                await _next.Invoke(context);
            }
            var contentType = RenderHelper.GetContentType(path);
            var content = GetContent(path);

            var response = context.Response;
            if (content == null)
            {
                response.StatusCode = 403;
                return;
            }
            
            response.ContentType = contentType;
            //content length is need,otherwise you can't download image or show full image
            response.ContentLength = content.Length;
            context.Response.StatusCode =200;
            await context.Response.Body.WriteAsync(content, 0, content.Length);
            //await _next.Invoke(context);

        }

        private string GetDefaultPath(string path)
        {
            if(string.IsNullOrEmpty(path))
            {
                var defaultPage = ConfigurationManager.AppSettings.Get("defaultPage");
                return Path.Combine(Options.StartPath, Options.ViewFolder, defaultPage);
            }
            return path;
        }

        private byte[] GetContent(string path)
        {
            var rootPath = PathSetting.RootPath;
            var filePath = PathHelper.CombinePath(rootPath,path);
            var extension = Path.GetExtension(filePath);
            if (string.IsNullOrEmpty(extension))
            {
                var fileInfo = new FileInfo(filePath);
                if (Directory.Exists(fileInfo.DirectoryName))
                {
                    var files = Directory.GetFiles(fileInfo.DirectoryName,fileInfo.Name+".*", SearchOption.TopDirectoryOnly);
                    if (files.Length > 0)
                    {
                        filePath += Path.GetExtension(files[0]);
                    }
                }
            }
            if (System.IO.File.Exists(filePath))
            {
                return File.ReadAllBytes(filePath);
            }
            
            return null;
        }
    }
}
