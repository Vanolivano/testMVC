namespace Sues.Controllers
{
    using Sues.Domain;
    using Sues.Models.Organization;
    using Sues.Repository;
    using Sues.Repository.sql;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class OrganizationController : Controller
    {
        IOrganizationRepository _organizationRepository;
        ITeacherRepository _teacherRepository;

        public OrganizationController()
        {
            _organizationRepository = new OrganizationRepository();
            _teacherRepository = new TeacherRepository();
        }
        [HttpGet]
        public ActionResult List()
        {
            var model = new OrganizationListModel
            {
                Items = _organizationRepository.Select(new Repository.Filter.OrganizationFilter { })
            };
            return View(model);
        }


        [HttpGet]
        public ActionResult Create(string returnUrl)
        {
            IEnumerable<Teacher> teacherList = _teacherRepository.Select(new Repository.Filter.TeacherFilter { });
            List<SelectListItem> teacherSelectList = new List<SelectListItem>();

            foreach (var item in teacherList)
            {
                var selectedElement = new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                };
                teacherSelectList.Add(selectedElement);
            }


            var model = new OrganizationEditModel
            {
                ReturnUrl = returnUrl,
                TeacherSelectList = teacherSelectList
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(OrganizationEditModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var entity = new Organization
            {
                Name = model.Name,
                INN = model.INN,
                TeacherId = model.TeacherId
            };

            _organizationRepository.Insert(entity);
            return Redirect(model.ReturnUrl);
        }

        //[HttpPost]
        public ActionResult Delete(int id)
        {
            _organizationRepository.Delete(id);
            return RedirectToAction("List");
        }

    }
}