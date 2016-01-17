using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using TheWorld.Models;
using TheWorld.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TheWorld.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<WorldUser> _sinInManager;

        public AuthController(SignInManager<WorldUser> sinInManager)
        {
            _sinInManager = sinInManager;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Trips", "App");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl)
        {
            if (!ModelState.IsValid) return View();
            var signInResult = await _sinInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password,
                true, false);
            if (signInResult.Succeeded)
            {
                if (string.IsNullOrWhiteSpace(returnUrl))
                {
                    return RedirectToAction("Trips", "App");
                }
                return Redirect(returnUrl);
            }
            ModelState.AddModelError("", "Username or password incorrect");
            return View();
        }
    }
}
