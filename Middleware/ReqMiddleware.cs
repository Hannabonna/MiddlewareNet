using System.Collections.Immutable;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using System;
using System.IO;

namespace MiddlewareNet.Middleware
{
    public class Middleware
    {
        public readonly RequestDelegate _next;

        public Middleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var startTime = DateTime.Now;
            var met = context.Request.Method.ToString();
            var path = context.Request.Path.ToString();
            var host = context.Request.Host.ToString();
            
            if (context.Request.Host.ToString() == "localhost:5000")
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                var stat = context.Response.StatusCode.ToString();
                ILog.Logging (stat, met, path, host);
            }
            else
            {
                 await _next(context);
            }
        }
    }
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UsingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Middleware>();
        }
    }

    public class ILog
    {
        public static void Logging(string status, string a, string reqPath, string reqHost)
        {
            File.AppendAllText(@"/Users/gigaming/MiddlewareNet/App.log", $"{DateTime.Now} Started {a} {reqPath} for {reqHost} \n");
            File.AppendAllText(@"/Users/gigaming/MiddlewareNet/App.log", $"{DateTime.Now} Completed {status} {reqPath} for {reqPath} not allowed for {reqHost} \n");
        }
    }
}