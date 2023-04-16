using FA.JustBlog.Core.Repositories;
using FA.JustBlog.Core.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.Controllers
{
    public class TagController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public TagController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult PopularTags()
        {
            var popularTags = _unitOfWork.TagRepository.GetAll().OrderByDescending(x=>x.Count).Take(2);
            return PartialView(popularTags);
        }
    }
}
