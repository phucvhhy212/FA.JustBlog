using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FA.JustBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/{controller}/{action}")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        [HttpPost]
        public async Task<IActionResult> Edit()
        {
            var foundRole = await _roleManager.FindByIdAsync(HttpContext.Request.Form["id"]);
            foundRole.Name = HttpContext.Request.Form["name"];
            await _roleManager.UpdateAsync(foundRole);
            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Add()
        {
            var newRole = new IdentityRole(HttpContext.Request.Form["name"]);
            await _roleManager.CreateAsync(newRole);
            return RedirectToAction("Index");
        }
    }
}
