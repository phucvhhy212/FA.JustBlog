using FA.JustBlog.Common;
using FA.JustBlog.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace FA.JustBlog.Core.DbInitializer
{
    public class DbInitializer:IDbInitializer
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JustBlogContext _context;

        public DbInitializer(
                UserManager<AppUser> userManager,
                RoleManager<IdentityRole> roleManager,
                JustBlogContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public void Initialize()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                    _context.Database.Migrate();
            }
            catch
            {

            }

            if (!_roleManager.RoleExistsAsync(RoleUtils.BLOG_OWER).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(RoleUtils.BLOG_OWER)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(RoleUtils.CONTRIBUTOR)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(RoleUtils.USER)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(RoleUtils.OTHER)).GetAwaiter().GetResult();

                _userManager.CreateAsync(new AppUser
                {
                    UserName = "toantv",
                    Email = "toan@gmail.com",
                    PhoneNumber = "0123456789",
                }, "Toan123*").GetAwaiter().GetResult();
                _userManager.CreateAsync(new AppUser
                {
                    UserName = "sondh",
                    Email = "sondh@gmail.com",
                    PhoneNumber = "0156456789",
                }, "Son123*").GetAwaiter().GetResult();
                _userManager.CreateAsync(new AppUser
                {
                    UserName = "kienvv",
                    Email = "kienvv@gmail.com",
                    PhoneNumber = "0123436789",
                }, "Kien123*").GetAwaiter().GetResult();
                _userManager.CreateAsync(new AppUser
                {
                    UserName = "trangdt",
                    Email = "trangdt@gmail.com",
                    PhoneNumber = "0123436789",
                }, "Trang123*").GetAwaiter().GetResult();


                AppUser userBO = _context.AppUsers.FirstOrDefault(u => u.UserName == "toantv");
                _userManager.AddToRoleAsync(userBO, RoleUtils.BLOG_OWER).GetAwaiter().GetResult();

                AppUser userC = _context.AppUsers.FirstOrDefault(u => u.UserName == "sondh");
                _userManager.AddToRoleAsync(userC, RoleUtils.CONTRIBUTOR).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(userC, RoleUtils.USER).GetAwaiter().GetResult();

                AppUser userU = _context.AppUsers.FirstOrDefault(u => u.UserName == "kienvv");
                _userManager.AddToRoleAsync(userU, RoleUtils.USER).GetAwaiter().GetResult();

                AppUser userO = _context.AppUsers.FirstOrDefault(u => u.UserName == "trangdt");
                _userManager.AddToRoleAsync(userO, RoleUtils.OTHER).GetAwaiter().GetResult();



            }


        }

    }
}
