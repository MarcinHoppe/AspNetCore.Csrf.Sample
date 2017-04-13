using AspNetCore.Csrf.Sample.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Csrf.Sample.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IProfileRepository profiles;

        public ProfileController(IProfileRepository profiles)
        {
            this.profiles = profiles;
        }

        public IActionResult Index()
        {
            var login = User.FindFirst(c => c.Type == "username").Value;
            var profile = profiles.GetProfileForUser(login);
            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Profile newProfile)
        {
            var login = User.FindFirst(c => c.Type == "username").Value;
            profiles.UpdateProfileForUser(login, newProfile);
            return RedirectToAction("Index");
        }
    }
}