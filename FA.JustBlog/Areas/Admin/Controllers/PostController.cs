using FA.JustBlog.Common;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/{controller}/{action}")]
    [Authorize]
    public class PostController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public PostController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Authorize(Roles = $"{RoleUtils.BLOG_OWER}, {RoleUtils.CONTRIBUTOR}")]

        public IActionResult Index()
        {
            ViewBag.categories = _unitOfWork.CategoryRepository.GetAll().ToList();
            ViewBag.message = "Manage Post";
            var postList = _unitOfWork.PostRepository.GetAll().ToList();
            return View(postList);
        }

        [HttpPost]
        [Authorize(Roles = $"{RoleUtils.BLOG_OWER}, {RoleUtils.CONTRIBUTOR}")]

        public IActionResult Edit()
        {
            Post p = new Post
            {
                Id = int.Parse(HttpContext.Request.Form["id"]),
                Title = HttpContext.Request.Form["title"],
                ShortDescription = HttpContext.Request.Form["description"],
                PostContent = HttpContext.Request.Form["content"],
                PostedOn = DateTime.Parse(HttpContext.Request.Form["postedDate"]),
                UrlSlug = HttpContext.Request.Form["url"],
                Published = bool.Parse(HttpContext.Request.Form["published"]),
                CategoryId = int.Parse(HttpContext.Request.Form["category"]),
                ViewCount = int.Parse(HttpContext.Request.Form["viewcount"]),
                RateCount = int.Parse(HttpContext.Request.Form["ratecount"]),
                TotalRate = int.Parse(HttpContext.Request.Form["totalrate"])
            };
            _unitOfWork.PostRepository.Update(p);
            _unitOfWork.SaveChange();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = $"{RoleUtils.BLOG_OWER}, {RoleUtils.CONTRIBUTOR}")]

        public IActionResult Add()
        {
            Post p = new Post
            {
                Title = HttpContext.Request.Form["title"],
                ShortDescription = HttpContext.Request.Form["description"],
                PostContent = HttpContext.Request.Form["content"],
                PostedOn = DateTime.Parse(HttpContext.Request.Form["postedDate"]),
                UrlSlug = HttpContext.Request.Form["url"],
                Published = bool.Parse(HttpContext.Request.Form["published"]),
                CategoryId = int.Parse(HttpContext.Request.Form["category"]),
                ViewCount = int.Parse(HttpContext.Request.Form["viewcount"]),
                RateCount = int.Parse(HttpContext.Request.Form["ratecount"]),
                TotalRate = int.Parse(HttpContext.Request.Form["totalrate"])
            };
            _unitOfWork.PostRepository.Add(p);
            _unitOfWork.SaveChange();
            return RedirectToAction("Index");

        }
        [Authorize(Roles = $"{RoleUtils.BLOG_OWER}, {RoleUtils.CONTRIBUTOR}")]

        public IActionResult Latest()
        {
            ViewBag.categories = _unitOfWork.CategoryRepository.GetAll();
            ViewBag.message = "Latest Posts";
            var latestList = _unitOfWork.PostRepository.GetLatestPost(_unitOfWork.PostRepository.GetAll().Count).ToList();
            return View("Index",latestList);
        }
        [Authorize(Roles = $"{RoleUtils.BLOG_OWER}, {RoleUtils.CONTRIBUTOR}")]

        public IActionResult MostViewed()
        {
            ViewBag.categories = _unitOfWork.CategoryRepository.GetAll();
            ViewBag.message = "Most Viewed Posts";
            var latestList = _unitOfWork.PostRepository.GetMostViewedPost(_unitOfWork.PostRepository.GetAll().Count).ToList();
            return View("Index", latestList);
        }
        [Authorize(Roles = $"{RoleUtils.BLOG_OWER}, {RoleUtils.CONTRIBUTOR}")]

        public IActionResult Published()
        {
            ViewBag.categories = _unitOfWork.CategoryRepository.GetAll();
            ViewBag.message = "Published Posts";
            var latestList = _unitOfWork.PostRepository.GetPublishedPosts();
            return View("Index", latestList);
        }
        [Authorize(Roles = $"{RoleUtils.BLOG_OWER}, {RoleUtils.CONTRIBUTOR}")]

        public IActionResult Unpublished()
        {
            ViewBag.categories = _unitOfWork.CategoryRepository.GetAll();
            ViewBag.message = "Unpublished Posts";
            var latestList = _unitOfWork.PostRepository.GetUnpublishedPosts().ToList();
            return View("Index", latestList);
        }

    }
}
