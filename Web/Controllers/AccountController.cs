using System;
using Web.Models;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Linq;

namespace Web.Controllers
{
	[AllowAnonymous]
	public class AccountController : Controller
	{
		private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		public AccountController(Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager)
		{
			this._userManager = userManager;
			this._signInManager = signInManager;
		}
		public IActionResult Register()
		{
            return View();
		}
        public async Task<IActionResult> ProfileAsync()
		{
			
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            if(user == null)
			{
				return RedirectToAction("Login", "Account");
            }
            return View("Views/Home/Profile.cshtml", user);
        }

        [HttpPost]
		public IActionResult Register(UserViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = _userManager.CreateAsync
				(
					new ApplicationUser
					{
						Id = Guid.NewGuid().ToString(),
						Email = model.Email,
						UserName = model.UserName,
						EmailConfirmed = true,
						NormalizedUserName = model.UserName.ToUpper(),
						NormalizedEmail = model.Email.ToUpper(),
					}, 
					model.Password
				).Result;
				if (result.Succeeded)
					return RedirectToAction("Login");
				return View(model);
			}
			return View();
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = _userManager.FindByEmailAsync(model.Email).Result;
				if (user != null)
				{
					var result = _signInManager.PasswordSignInAsync(user, model.Password,
						true, false).Result;
					if (result.Succeeded)
						return RedirectToAction("Index", "Home");
				}
				return View(model);
			}
			return View();
		}


        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
        }
    }
}
