using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SJZ.OAuthService
{
    public class StaticGeneratorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _hostEnvironment;

        public StaticGeneratorMiddleware(RequestDelegate next, IWebHostEnvironment hostEnvironment)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _hostEnvironment = hostEnvironment ?? throw new ArgumentNullException(nameof(hostEnvironment));
        }

        public async Task Invoke(HttpContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (context.Request == null) throw new ArgumentNullException(nameof(context.Request));

            await _next(context);

            //if (context.Response?.ContentType?.Contains("text/html") == false && context.Response.StatusCode != 200)
            //{
            //    await _next(context);
            //    return;
            //}

            //var responseStream = context.Response.Body;
            //var buffer = new MemoryStream();
            //var reader = new StreamReader(buffer);
            //context.Response.Body = buffer;
            //try
            //{
            //    buffer.Seek(0, SeekOrigin.Begin);
            //    var responseBody = await reader.ReadToEndAsync();


            //}
            //finally
            //{
            //    context.Response.Body = responseStream;
            //}
        }
    }
}
