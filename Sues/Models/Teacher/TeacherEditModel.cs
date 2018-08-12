namespace Sues.Models.Teacher
{
    using System.ComponentModel.DataAnnotations;

    public class TeacherEditModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Необходимо для заполнения")]
        public string Name { get; set; }
        public string Email { get; set; }

        public string ReturnUrl { get; set; }

    }
}