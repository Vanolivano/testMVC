namespace Sues.Models.EmployeeInStudyGroup
{
    using Sues.Domain;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class EmployeeInStudyGroupEditModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int StudyGroupId { get; set; }

        public IEnumerable<SelectListItem> EmployeeSelectList { get; set; }
        public IEnumerable<SelectListItem> StudyGroupSelectList { get; set; }

        public string ReturnUrl { get; set; }
    }
}