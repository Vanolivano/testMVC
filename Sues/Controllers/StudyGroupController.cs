namespace Sues.Controllers
{
    using Sues.Domain;
    using Sues.Models.StudyGroup;
    using Sues.Repository;
    using Sues.Repository.Filter;
    using Sues.Repository.sql;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class StudyGroupController : Controller
    {
        IStudyGroupRepository _studyGroupRepository;
        ITeacherRepository _teacherRepository;
        IEmployeeInStudyGroupRepository _employeeInStudyGroupRepository;
        IEmployeeRepository _employeeRepository;
        IOrganizationRepository _organizationRepository;

        public StudyGroupController()
        {
            _studyGroupRepository = new StudyGroupRepository();
            _teacherRepository = new TeacherRepository();
            _employeeInStudyGroupRepository = new EmployeeInStudyGroupRepository();
            _employeeRepository = new EmployeeRepository();
            _organizationRepository = new OrganizationRepository();
        }

        [HttpGet]
        public ActionResult List(StudyGroupFilterModel filter)
        {

            IEnumerable<EmployeeInStudyGroup> studentList = _employeeInStudyGroupRepository.Select(new EmployeeInStudyGroupFilter { });
            IEnumerable<StudyGroup> studyGroupList = _studyGroupRepository.Select(new StudyGroupFilter { });

            foreach (var item in studyGroupList)
            {
                item.TeacherName = _teacherRepository.Get(item.TeacherId).Name;
                item.CountOfStudents = studentList.Where(x => x.StudyGroupId == item.Id).Count();
            }
            var model = new StudyGroupListModel
            {
                Items = studyGroupList
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id, string returnUrl)
        {
            StudyGroup studyGroup = _studyGroupRepository.Get(id);

            IEnumerable<EmployeeInStudyGroup> employeeInStudyGroup = _employeeInStudyGroupRepository.Select(new EmployeeInStudyGroupFilter { StudyGroupId = id });
            IEnumerable<Employee> employeeList = _employeeRepository.Select(new EmployeeFilter { });
            IEnumerable<Employee> demoStudentList = employeeList.Join(employeeInStudyGroup, x => x.Id, y => y.EmployeeId, (x, y) => new Employee { Id = x.Id, Name = x.Name, OrganizationId = x.OrganizationId });
            List<Employee> studentList = new List<Employee>();

            foreach (var item in demoStudentList)
            {
                Employee entity = new Employee
                {
                    Id = item.Id,
                    Name = item.Name,
                    OrganizationId = item.OrganizationId,
                    OrganizationName = _organizationRepository.Get(item.OrganizationId).Name
                };
                studentList.Add(entity);
            }

            var model = new StudyGroupEditModel
            {
                Id = studyGroup.Id,
                Name = studyGroup.Name,
                TeacherId = studyGroup.TeacherId,
                CourseId = studyGroup.CourseId,
                TeacherName = _teacherRepository.Get(studyGroup.TeacherId).Name,
                StudentList = studentList,
                ReturnUrl = returnUrl

            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(StudyGroupEditModel model)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<EmployeeInStudyGroup> employeeInStudyGroup = _employeeInStudyGroupRepository.Select(new EmployeeInStudyGroupFilter { StudyGroupId = model.Id });
                IEnumerable<Employee> employeeList = _employeeRepository.Select(new EmployeeFilter { });
                IEnumerable<Employee> demoStudentList = employeeList.Join(employeeInStudyGroup, x => x.Id, y => y.EmployeeId, (x, y) => new Employee { Id = x.Id, Name = x.Name, OrganizationId = x.OrganizationId });
                List<Employee> studentList = new List<Employee>();

                foreach (var item in demoStudentList)
                {
                    studentList.Add(new Employee
                    {
                        Id = item.Id,
                        Name = item.Name,
                        OrganizationId = item.OrganizationId,
                        OrganizationName = _organizationRepository.Get(item.OrganizationId).Name
                    });
                }
                model.StudentList = studentList;
                return View(model);
            }
            StudyGroup entity = new StudyGroup
            {
                Id = model.Id,
                Name = model.Name,
                TeacherId = model.TeacherId,
                CourseId = model.CourseId
            };
            _studyGroupRepository.Update(entity);
            return Redirect(model.ReturnUrl);
        }

        [HttpGet]
        public ActionResult Create(string returnUrl)
        {
            IEnumerable<Teacher> teacherList = _teacherRepository.Select(new TeacherFilter { });
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

            var model = new StudyGroupEditModel
            {
                TeacherSelectList = teacherSelectList,
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(StudyGroupEditModel model)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<Teacher> teacherList = _teacherRepository.Select(new TeacherFilter { });
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
                model.TeacherSelectList = teacherSelectList;
                return View(model);
            }

            var entity = new StudyGroup
            {
                Name = model.Name,
                TeacherId = model.TeacherId,
                CourseId = 1
            };

            return RedirectToAction("Edit", new { id = _studyGroupRepository.Insert(entity), returnUrl = model.ReturnUrl });
        }
        public ActionResult Delete(int id)
        {
            _studyGroupRepository.Delete(id);
            return RedirectToAction("List");
        }

        //public ActionResult DeleteStudent(int id, string returnUrl)
        //{
        //    IEnumerable<EmployeeInStudyGroup> students = _employeeInStudyGroupRepository.Select(new EmployeeInStudyGroupFilter { EmployeeId = id });
        //    EmployeeInStudyGroup student = students.FirstOrDefault(x => x.EmployeeId == id);
        //    _employeeInStudyGroupRepository.Delete(student.Id);
        //    return Redirect(returnUrl);
        //}
    }
}