using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace AspNetCore.Csrf.Sample.Middleware
{
    public class SameSiteCookieMiddleware
    {
        private readonly RequestDelegate next;

        public SameSiteCookieMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public Task Invoke(HttpContext context)
        {
            context.Response.OnStarting(AddSameSiteToCookies, context);
            return next(context);
        }

        private static Task AddSameSiteToCookies(object state)
        {
            var context = (HttpContext) state;

            if (context.Response.Headers.TryGetValue("Set-Cookie", out var cookieValues))
            {
                var cookie = cookieValues.First();
                context.Response.Headers["Set-Cookie"] = new StringValues(cookie + "; SameSite=strict");
            }

            return Task.CompletedTask;
        }
    }
}