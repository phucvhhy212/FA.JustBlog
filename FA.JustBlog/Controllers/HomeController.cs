using FA.JustBlog.Core.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.Controllers
{
    public class HomeController : Controller
    {
        
        private IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public IActionResult Index()
        {
            var postList = _unitOfWork.PostRepository.GetAll();
            return View(postList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult AboutCard()
        {
            return PartialView("_PartialAboutCard");
        }

        
    }
}