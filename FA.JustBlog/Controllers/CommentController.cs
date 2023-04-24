using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.Controllers
{
    public class CommentController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public CommentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        public IActionResult Add()
        {
            Comment c = new Comment
            {
                Name = HttpContext.Request.Form["name"],
                CommentHeader = HttpContext.Request.Form["title"],
                CommentText = HttpContext.Request.Form["text"],
                CommentTime = DateTime.Now,
                Email = HttpContext.Request.Form["email"],
                PostId = int.Parse(HttpContext.Request.Form["postid"])
            };
            _unitOfWork.CommentRepository.Add(c);
            _unitOfWork.SaveChange();
            return View(c);
        }
    }
}
