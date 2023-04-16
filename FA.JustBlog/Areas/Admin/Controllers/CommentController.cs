using FA.JustBlog.Core.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/{controller}/{action}")]
    public class CommentController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public CommentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var commentsList = _unitOfWork.CommentRepository.GetAll();
            return View(commentsList);
        }

        public IActionResult Delete(int id) {
            _unitOfWork.CommentRepository.Delete(id);
            _unitOfWork.SaveChange();
            return RedirectToAction("Index");
        }
    }
}
