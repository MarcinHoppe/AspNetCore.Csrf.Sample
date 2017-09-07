using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;

namespace AspNetCore.Csrf.Sample.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string login, string password, string returnUrl = null)
        {
            ViewData["returnUrl"] = returnUrl;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || password != "s3cr3t")
                return View();

            var principal = CreatePrincipal(login);

            await HttpContext.SignInAsync("AspNetCore.Csrf.Sample", principal);

            return LocalRedirect(returnUrl ?? "/");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("AspNetCore.Csrf.Sample");
            return Redirect("/");
        }

        private ClaimsPrincipal CreatePrincipal(string login)
        {
            var claims = new List<Claim>
            {
                new Claim("username", login),
                new Claim("role", "user")
            };

            var identity = new ClaimsIdentity(claims, "password");

            return new ClaimsPrincipal(identity);
        }
    }
}