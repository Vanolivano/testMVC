namespace Sues.Models.Course
{
    using System.ComponentModel.DataAnnotations;

    public class CourseEditModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Необходимо для заполнения")]
        public string Name { get; set; }

        public string ReturnUrl { get; set; }
    }
}