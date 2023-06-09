﻿using FA.JustBlog.Common;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/{controller}/{action}")]
    [Authorize]
    public class TagController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public TagController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        [Authorize(Roles = $"{RoleUtils.BLOG_OWER}, {RoleUtils.CONTRIBUTOR}")]

        public IActionResult Index()
        {
            var tagList = _unitOfWork.TagRepository.GetAll().ToList();
            return View(tagList);
        }

        [HttpPost]
        [Authorize(Roles = $"{RoleUtils.BLOG_OWER}, {RoleUtils.CONTRIBUTOR}")]

        public IActionResult Add()
        {
            Tag t = new Tag
            {
                Name = HttpContext.Request.Form["name"],
                UrlSlug = HttpContext.Request.Form["url"],
                Description = HttpContext.Request.Form["description"],
                Count = int.Parse(HttpContext.Request.Form["count"])
            };
            _unitOfWork.TagRepository.Add(t);
            _unitOfWork.SaveChange();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = $"{RoleUtils.BLOG_OWER}, {RoleUtils.CONTRIBUTOR}")]

        public IActionResult Edit()
        {
            Tag t = new Tag
            {
                Id = int.Parse(HttpContext.Request.Form["id"]),
                Name = HttpContext.Request.Form["name"],
                UrlSlug = HttpContext.Request.Form["url"],
                Description = HttpContext.Request.Form["description"],
                Count = int.Parse(HttpContext.Request.Form["count"])
            };
            _unitOfWork.TagRepository.Update(t);
            _unitOfWork.SaveChange();
            return RedirectToAction("Index");
        }
    }
}
