namespace Sues.Controllers
{
    using Sues.Domain;
    using Sues.Models.Course;
    using Sues.Repository;
    using Sues.Repository.sql;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class CourseController : Controller
    {
        ICourseRepository _courseRepository;

        public CourseController()
        {
            _courseRepository = new CourseRepository();
        }
        [HttpGet]
        public ActionResult List()
        {
            var model = new CourseListModel
            {
                Items = _courseRepository.Select(new Repository.Filter.CourseFilter { })
            };
            return View(model);
        }


        [HttpGet]
        public ActionResult Create(string returnUrl)
        {
            var model = new CourseEditModel
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CourseEditModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var entity = new Course
            {
                Name = model.Name
            };

            _courseRepository.Insert(entity);
            return Redirect(model.ReturnUrl);
        }

        //[HttpPost]
        public ActionResult Delete(int id)
        {
            _courseRepository.Delete(id);
            return RedirectToAction("List");
        }

    }
}