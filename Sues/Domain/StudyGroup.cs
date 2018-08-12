namespace Sues.Domain
{
    public class StudyGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }

        public string TeacherName { get; set; }
        public int CountOfStudents { get; set; }
    }
}