using FA.JustBlog.Core.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.Controllers
{
    public class PostController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public PostController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public IActionResult Index(int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            var sx = _unitOfWork.TagRepository.GetAll();
            var sortByLatest = _unitOfWork.PostRepository.GetLatestPost(5).ToList();
            ViewBag.SortByLatest = sortByLatest;
            var mostViewedPosts = _unitOfWork.PostRepository.GetMostViewedPost(5).ToList();
            ViewBag.MostViewedPosts = mostViewedPosts;
            var result = _unitOfWork.PostRepository.GetAll().Skip((page.Value-1)*3).Take(3).ToList();
            ViewBag.Count = (int)Math.Ceiling(_unitOfWork.PostRepository.GetAll().Count / (decimal)3);
            ViewBag.Title = "All Posts";
            ViewBag.PopularTags = _unitOfWork.TagRepository.GetAll().OrderByDescending(x => x.Count).ToList();
            return View(result);
        }

        public IActionResult LatestPost()
        {
            var sortByLatest = _unitOfWork.PostRepository.GetLatestPost(5).ToList();
            return PartialView("_ListPosts", sortByLatest);

        }

        public IActionResult MostViewedPosts()
        {
            var mostViewedPosts = _unitOfWork.PostRepository.GetMostViewedPost(5).ToList();
            return PartialView("_ListPosts",mostViewedPosts);
        }

        public IActionResult Category(string name)
        {
            var sx = _unitOfWork.TagRepository.GetAll();

            var postsByCategory = _unitOfWork.PostRepository.GetPostsByCategory(name).ToList();
            ViewBag.Count = (int)Math.Ceiling(postsByCategory.Count / (decimal)3);
            ViewBag.Title = $"All Posts In Category {name}";
            return View("Index", postsByCategory);
        }

        public IActionResult Tag(string name) {
            var sx = _unitOfWork.TagRepository.GetAll();
            var postsByTag = _unitOfWork.PostRepository.GetPostsByTag(name).ToList();
            ViewBag.Count = (int)Math.Ceiling(postsByTag.Count / (decimal)3);
            ViewBag.Title = $"All Posts With Tag \"{name}\"";
            return View("Index", postsByTag);
        }

        public IActionResult Details(int year, int month, string title)
        {
            var post = _unitOfWork.PostRepository.FindPost(year, month, title);
            ViewBag.Comments = _unitOfWork.CommentRepository.GetCommentsForPost(post.Id);
            if (post == null)
                return NotFound();

            return View(post);
        }
    }
}
