namespace Sues.Repository
{
    using Sues.Domain;
    using Sues.Repository.Filter;
    using System.Collections.Generic;

    public interface ITeacherRepository
    {
        IEnumerable<Teacher> Select(TeacherFilter filter);
        Teacher Get(int id);
        int Insert(Teacher entity);
        void Update(Teacher entity);
        void Delete(int id);
    }
}
