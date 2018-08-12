namespace Sues.Controllers
{
    using Sues.Domain;
    using Sues.Models.Teacher;
    using Sues.Repository;
    using Sues.Repository.sql;
    using System.Web.Mvc;

    public class TeacherController : Controller
    {
        ITeacherRepository _teacherRepository;

        public TeacherController()
        {
            _teacherRepository = new TeacherRepository();
        }
        [HttpGet]
        public ActionResult List()
        {
            var model = new TeacherListModel
            {
                Items = _teacherRepository.Select(new Repository.Filter.TeacherFilter { })
            };
            return View(model);
        }


        [HttpGet]
        public ActionResult Create(string returnUrl)
        {
            var model = new TeacherEditModel
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(TeacherEditModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var entity = new Teacher
            {
                Name = model.Name,
                Email = model.Email
            };

            _teacherRepository.Insert(entity);
            return Redirect(model.ReturnUrl);
        }

        public ActionResult Delete(int id)
        {
            _teacherRepository.Delete(id);
            return RedirectToAction("List");
        }

    }
}