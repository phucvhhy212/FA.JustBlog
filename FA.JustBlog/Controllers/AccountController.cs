using System.Security.Claims;
using System.Text;
using FA.JustBlog.Common;
using FA.JustBlog.Core.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using MimeKit;

namespace FA.JustBlog.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
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
                ModelState.AddModelError(string.Empty, "Account's email not confirmed!");
                return View();
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

            var user = new AppUser
            {
                UserName = username,
                Email = email,
                PhoneNumber = phone
            };
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                // phát sinh token theo thông tin user để xác nhận email
                // mỗi user dựa vào thông tin sẽ có một mã riêng, mã này nhúng vào link
                // trong email gửi đi để người dùng xác nhận
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                // callbackUrl = /Account/ConfirmEmail?userId=useridxx&code=codexxxx
                // Link trong email người dùng bấm vào, nó sẽ gọi Page: /Acount/ConfirmEmail để xác nhận
                var callbackUrl = Url.ActionLink("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Scheme);

                // Gửi email    
                await _emailSender.SendEmailAsync(email, "Confirm Your Just Blog Account Email Address",
                    $"To confirm your email address, please <a href='{callbackUrl}'>Click here</a>.");

                if (_userManager.Options.SignIn.RequireConfirmedEmail)
                {
                    // Nếu cấu hình phải xác thực email mới được đăng nhập thì chuyển hướng đến trang
                    // RegisterConfirmation - chỉ để hiện thông báo cho biết người dùng cần mở email xác nhận
                    return Redirect($"RegisterConfirmation?email={email}");
                }
                else
                {
                    // Không cần xác thực - đăng nhập luôn
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index","Home");
                }
                
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
                    string externalMail = info.Principal.FindFirstValue(ClaimTypes.Email);
                    var userWithexternalMail = (externalMail != null) ? (await _userManager.FindByEmailAsync(externalMail)) : null;
                    // Xử lý khi có thông tin về email từ info, đồng thời có user với email đó
                    // trường hợp này sẽ thực hiện liên kết tài khoản ngoài + xác thực email luôn     
                    if ((userWithexternalMail != null))
                    {
                        // xác nhận email luôn nếu chưa xác nhận
                        if (!userWithexternalMail.EmailConfirmed)
                        {
                            var codeactive = await _userManager.GenerateEmailConfirmationTokenAsync(userWithexternalMail);
                            await _userManager.ConfirmEmailAsync(userWithexternalMail, codeactive);
                        }
                        // Thực hiện liên kết info và user
                        var resultAdd = await _userManager.AddLoginAsync(userWithexternalMail, info);
                        if (resultAdd.Succeeded)
                        {
                            // Thực hiện login    
                            await _signInManager.SignInAsync(userWithexternalMail, isPersistent: false);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return Content("Something is wrong when atempting to link account");
                        }
                    }
                    var user = new AppUser { UserName = info.Principal.FindFirstValue(ClaimTypes.Email), Email = info.Principal.FindFirstValue(ClaimTypes.Email) };
                    var resultAddNew = await _userManager.CreateAsync(user);
                    if (resultAddNew.Succeeded)
                    {

                        // Liên kết tài khoản ngoài với tài khoản vừa tạo
                        resultAddNew = await _userManager.AddLoginAsync(user, info);
                        if (resultAddNew.Succeeded)
                        {
                            // Email tạo tài khoản và email từ info giống nhau -> xác thực email luôn
                            if (user.Email == externalMail)
                            {
                                var codeactive = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                                await _userManager.ConfirmEmailAsync(user, codeactive);
                                await _signInManager.SignInAsync(user, isPersistent: false, info.LoginProvider);
                                return RedirectToAction("Index", "Home");
                            }

                            
                        }
                    }
                    foreach (var error in resultAddNew.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

            }
            return RedirectToAction("Login");

        }
 

        public async Task<IActionResult> RegisterConfirmation(string email)
        {
            if (email == null)
            {
                return RedirectToAction("Login");
            }
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"Cannot find any user with email: '{email}'.");
            }
            if (user.EmailConfirmed)
            {
                // Tài khoản đã xác thực email
                return Content("Account's email already confirmed!");
            }
            return View("~/Views/Account/RegisterConfirmation.cshtml",email);
        }

        public async Task<IActionResult> ConfirmEmail(string userId,string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction("Login");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"User not exist - '{userId}'.");
            }
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            // Xác thực email
            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                return View();              
            }
            else
            {
                return Content("Something wrong in confirming email!");
            }
            
        }
    }
}
