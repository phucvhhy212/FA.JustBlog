using FA.JustBlog.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/{controller}/{action}")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            this._userManager = userManager;
        }

        public class UserAndRole : AppUser
        {
            public string RoleNames { get; set; }
        }
        public List<UserAndRole> users { set; get; }
        public async Task<IActionResult> Index()
        {
            var qr = _userManager.Users;
            var qr1 = qr
                .Select(u => new UserAndRole()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                });
            users = await qr1.ToListAsync();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                user.RoleNames = string.Join(",", roles);
            }
            return View(users);
        }
    }
}
