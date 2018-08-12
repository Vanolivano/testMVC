namespace Sues.Models.StudyGroup
{
    using Sues.Domain;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class StudyGroupEditModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Необходимо для заполнения")]
        public string Name { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }

        public string TeacherName { get; set; }
        public string ReturnUrl { get; set; }

        public IEnumerable<Employee> StudentList { get; set; }
        public IEnumerable<SelectListItem> TeacherSelectList { get; set; }
        //public int CourseName { get; set; }
    }
}