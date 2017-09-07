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

            services.AddAuthentication()
                .AddCookie("AspNetCore.Csrf.Sample", options => {
                    options.Cookie.Name = "AspNetCore.Csrf.Sample.AuthCookie";
                    options.Cookie.Domain = "web.local";
                    options.LoginPath = new PathString("/Account/Login/");
                    options.LogoutPath = new PathString("/Account/Logout/");
                    options.AccessDeniedPath = new PathString("/Account/AccessDenied/");
                });

            services.AddAntiforgery(options =>
            {
                options.Cookie.Domain = "web.local";
                options.Cookie.Name = "AspNetCore.Csrf.Sample.CsrfCookie";
                options.FormFieldName = "AspNetCore.Csrf.Sample.CsrfToken";
            });
            
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMiddleware<SameSiteCookieMiddleware>();
            app.UseMiddleware<OriginCheckMiddleware>();

            app.UseMvcWithDefaultRoute();
        }
    }
}