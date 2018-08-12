namespace Sues.Repository
{
    using Sues.Domain;
    using Sues.Repository.Filter;
    using System.Collections.Generic;

    public interface ICourseRepository
    {
        IEnumerable<Course> Select(CourseFilter filter);
        Course Get(int id);
        int Insert(Course entity);
        void Update(Course entity);
        void Delete(int id);
    }
}
