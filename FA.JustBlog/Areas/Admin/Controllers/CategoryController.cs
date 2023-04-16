using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("Admin/{controller}/{action}")]
	public class CategoryController : Controller
	{
		private IUnitOfWork _unitOfWork;

		public CategoryController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public IActionResult Index()
		{
			var categories = _unitOfWork.CategoryRepository.GetAll();
			return View("~/Areas/Admin/Views/Category/Index.cshtml",categories);
		}
		[HttpPost]
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
