using System.Security.Claims;
using FA.JustBlog.Common;
using FA.JustBlog.Core.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        //public IList<AuthenticationScheme> ExternalLogins { get; set; }
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
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(account.UserName, account.Password, account.RememberMe, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(account.UserName);
                if (await _userManager.IsInRoleAsync(user, RoleUtils.CONTRIBUTOR) || await _userManager.IsInRoleAsync(user, RoleUtils.BLOG_OWER))
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult Register()
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
        public async Task<IActionResult> Register(string username, string email, string phone, string password)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _userManager.CreateAsync(new AppUser
            {
                UserName = username,
                Email = email,
                PhoneNumber = phone,
            }, password);
            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }

            result.Errors.ToList().ForEach(error =>
            {
                ModelState.AddModelError(string.Empty, error.Description);
            });
            return View();

        }
        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            //var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl });

            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, Url.Action("Callback", "Account"));

            return new ChallengeResult(provider, properties);
        }


        public async Task<IActionResult> Callback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty,$"Error from external provider {remoteError}");
                return RedirectToAction("Login");
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info == null)
            {
                ModelState.AddModelError(string.Empty, $"Error loading external login information");
                return RedirectToAction("Login");
            }
            
            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey,
                isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Co tk chua lien ket => Lien ket vs dv ngoai va dang nhap
                // Chua co tk => tao tk
                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    var user = new AppUser
                    {
                        UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                        Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                    };
                    var resultCreate = await _userManager.CreateAsync(user);
                    if (resultCreate.Succeeded)
                    {
                        resultCreate = await _userManager.AddLoginAsync(user, info);
                        if (resultCreate.Succeeded)
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false, info.LoginProvider);
                        }
                        return RedirectToAction("Index", "Home");
                    }

                    foreach (var item in resultCreate.Errors)
                    {
                        ModelState.AddModelError(string.Empty,item.Description);
                    }
                }

            }
            return RedirectToAction("Login");

        }
    }
}
