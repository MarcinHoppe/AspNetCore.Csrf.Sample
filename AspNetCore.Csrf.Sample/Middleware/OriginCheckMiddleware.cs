using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AspNetCore.Csrf.Sample.Middleware
{
    public class OriginCheckMiddleware
    {
        private readonly RequestDelegate next;

        public OriginCheckMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public Task Invoke(HttpContext context)
        { 
            if (context.Request.Method == "POST")
            {
                string GetHost(string url)
                {
                    return new Uri(url).Host;
                }

                string origin = null;

                if (context.Request.Headers.TryGetValue("Origin", out var originValues))
                {
                    origin = GetHost(originValues.First());
                }
                else if (context.Request.Headers.TryGetValue("Referer", out var refererValues))
                {
                    origin = GetHost(refererValues.First());
                }
                
                if (string.IsNullOrWhiteSpace(origin) || origin != "web.local")
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                }
            }

            return next(context);
        }
    }
}