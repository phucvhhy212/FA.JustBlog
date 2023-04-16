using FA.JustBlog.Common;
using FA.JustBlog.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Account account)
        {
            if (!ModelState.IsValid) { 
                return View(); 
            }
            var result = await _signInManager.PasswordSignInAsync(account.UserName, account.Password, account.RememberMe, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(account.UserName);
                if (await _userManager.IsInRoleAsync(user, RoleUtils.CONTRIBUTOR) || await _userManager.IsInRoleAsync(user, RoleUtils.BLOG_OWER) || await _userManager.IsInRoleAsync(user, RoleUtils.USER))
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
            }
            return RedirectToAction("Index","Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
