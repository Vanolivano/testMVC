namespace Sues.Domain
{
    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string INN { get; set; }
        public int TeacherId { get; set; }

        public string TeacherName { get; set; }
    }
}