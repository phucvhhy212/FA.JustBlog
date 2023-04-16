using FA.JustBlog.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Route("Admin")]
    [Route("Admin/Home")]
    [Authorize]
	public class HomeController : Controller
	{
        [Authorize(Roles = $"{RoleUtils.BLOG_OWER}, {RoleUtils.CONTRIBUTOR}, {RoleUtils.USER}")]
        public IActionResult Index()
		{
			return View();
		}
	}
}
