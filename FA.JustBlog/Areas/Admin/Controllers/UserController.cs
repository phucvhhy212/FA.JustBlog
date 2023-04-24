using FA.JustBlog.Common;
using FA.JustBlog.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/{controller}/{action}")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JustBlogContext _context;

        public UserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, JustBlogContext context)
        {
            this._userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public class UserAndRole : AppUser
        {

            public string RoleNames { get; set; }
        }
        public List<UserAndRole> users { set; get; }
        [Authorize(Roles = $"{RoleUtils.BLOG_OWER}, {RoleUtils.CONTRIBUTOR}")]

        public async Task<IActionResult> Index()
        {
            var qr = _userManager.Users;
            var qr1 = qr
                .Select(u => new UserAndRole()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    EmailConfirmed = u.EmailConfirmed,
                    PhoneNumberConfirmed = u.PhoneNumberConfirmed
                });
            users = await qr1.ToListAsync();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                user.RoleNames = string.Join(",", roles);
            }

            ViewBag.Roles = _roleManager.Roles.ToList();
            return View(users);
        }
        [HttpPost]
        [Authorize(Roles = $"{RoleUtils.BLOG_OWER}, {RoleUtils.CONTRIBUTOR}")]

        public async Task<IActionResult> Edit()
        {
            var selectedRoles = HttpContext.Request.Form["roles"];
            var user = await _userManager.FindByIdAsync(HttpContext.Request.Form["id"]);
            var OldRoleNames = (await _userManager.GetRolesAsync(user)).ToArray();
            var deleteRoles = OldRoleNames.Where(r => !selectedRoles.Contains(r));
            var addRoles = selectedRoles.Where(r => !OldRoleNames.Contains(r));


            List<string> roleNames = await _roleManager.Roles.Select(r => r.Name).ToListAsync();

            await _userManager.RemoveFromRolesAsync(user, deleteRoles);
            await _userManager.AddToRolesAsync(user, addRoles);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = $"{RoleUtils.BLOG_OWER}, {RoleUtils.CONTRIBUTOR}")]

        public async Task<IActionResult> Add()
        {
            var result = await _userManager.CreateAsync(new AppUser
            {
                UserName = HttpContext.Request.Form["username"],
                Email = HttpContext.Request.Form["email"],
                PhoneNumber = HttpContext.Request.Form["phone"],
            }, HttpContext.Request.Form["password"]);
            if (result.Succeeded)
            {
                AppUser createdUser =
                    _context.AppUsers.FirstOrDefault(u =>
                        u.UserName == HttpContext.Request.Form["username"].ToString());

                foreach (var roleItem in HttpContext.Request.Form["roles"].ToList())
                {
                    _userManager.AddToRoleAsync(createdUser, roleItem).GetAwaiter().GetResult();
                }
            }

            return RedirectToAction("Index");

        }
    }
}
