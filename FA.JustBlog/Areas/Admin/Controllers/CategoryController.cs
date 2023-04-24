using FA.JustBlog.Common;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize]
	[Route("Admin/{controller}/{action}")]
    public class CategoryController : Controller
	{
		private IUnitOfWork _unitOfWork;

		public CategoryController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
        [Authorize(Roles = $"{RoleUtils.BLOG_OWER}, {RoleUtils.CONTRIBUTOR}")]

        public IActionResult Index()
		{
			var categories = _unitOfWork.CategoryRepository.GetAll();
			return View("~/Areas/Admin/Views/Category/Index.cshtml",categories);
		}
		[HttpPost]
        [Authorize(Roles = $"{RoleUtils.BLOG_OWER}, {RoleUtils.CONTRIBUTOR}")]

        public IActionResult Edit() {
			Category c = new Category
			{
				Id = int.Parse(HttpContext.Request.Form["id"]),
                Name = HttpContext.Request.Form["name"],
				UrlSlug = HttpContext.Request.Form["url"],
				Description = HttpContext.Request.Form["description"]
            };
			_unitOfWork.CategoryRepository.Update(c);
			_unitOfWork.SaveChange();
			return RedirectToAction("Index");
        }


		[HttpPost]
        [Authorize(Roles = $"{RoleUtils.BLOG_OWER}, {RoleUtils.CONTRIBUTOR}")]

        public IActionResult Add() {
			Category c = new Category
			{
				Name = HttpContext.Request.Form["name"],
				UrlSlug = HttpContext.Request.Form["url"],
				Description = HttpContext.Request.Form["description"]
			};
			_unitOfWork.CategoryRepository.Add(c);
			_unitOfWork.SaveChange();
            return RedirectToAction("Index");
        }


    }
}
