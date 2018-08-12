namespace Sues.Models.Organization
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class OrganizationEditModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Необходимо для заполнения")]
        public string Name { get; set; }
        public string INN { get; set; }
        public int TeacherId { get; set; }

        public IEnumerable<SelectListItem> TeacherSelectList { get; set; }
        public string ReturnUrl { get; set; }

    }
}