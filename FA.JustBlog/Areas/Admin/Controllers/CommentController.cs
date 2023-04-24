using FA.JustBlog.Common;
using FA.JustBlog.Core.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/{controller}/{action}")]
    [Authorize]

    public class CommentController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public CommentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Authorize(Roles = $"{RoleUtils.BLOG_OWER}, {RoleUtils.CONTRIBUTOR}")]

        public IActionResult Index()
        {
            var commentsList = _unitOfWork.CommentRepository.GetAll().ToList();
            return View(commentsList);
        }
        [Authorize(Roles = $"{RoleUtils.BLOG_OWER}, {RoleUtils.CONTRIBUTOR}")]

        public IActionResult Delete(int id) {
            _unitOfWork.CommentRepository.Delete(id);
            _unitOfWork.SaveChange();
            return RedirectToAction("Index");
        }
    }
}
