using AspNetCore.Csrf.Sample.Middleware;
using AspNetCore.Csrf.Sample.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AspNetCore.Csrf.Sample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IProfileRepository, InMemoryProfileRepository>();
            services.AddAntiforgery(options =>
            {
                options.CookieDomain = "web.local";
                options.CookieName = "AspNetCore.Csrf.Sample.CsrfCookie";
                options.FormFieldName = "AspNetCore.Csrf.Sample.CsrfToken";
            });
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "AspNetCore.Csrf.Sample",
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                CookieName = "AspNetCore.Csrf.Sample.AuthCookie",
                CookieDomain = "web.local",
                LoginPath = new PathString("/Account/Login/"),
                LogoutPath = new PathString("/Account/Logout/"),
                AccessDeniedPath = new PathString("/Account/AccessDenied/")
            });

            app.UseMiddleware<SameSiteCookieMiddleware>();
            app.UseMiddleware<OriginCheckMiddleware>();

            app.UseMvcWithDefaultRoute();
        }
    }
}