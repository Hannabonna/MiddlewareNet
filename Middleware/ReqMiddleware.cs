using System.Collections.Immutable;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;

namespace MiddlewareNet.Middleware
{
    public class CustomAuthMiddleware
    {
        public readonly RequestDelegate _next;

        public CustomAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers["Authorization"] == "hello")
            {
                await _next(context);
            }
            else
            {
                var text = "Not Authorized";
            
                var data = System.Text.Encoding.UTF8.GetBytes(text);
                await context.Response.Body.WriteAsync(data, 0, data.Length);
            }
           
        }
    }
    public static class AuthMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomAuthMiddleware>();
        }
    }
}