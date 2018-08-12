namespace Sues.Repository
{
    using Sues.Domain;
    using Sues.Repository.Filter;
    using System.Collections.Generic;

    public interface IStudyGroupRepository
    {
        IEnumerable<StudyGroup> Select(StudyGroupFilter filter);
        StudyGroup Get(int id);
        int Insert(StudyGroup entity);
        void Update(StudyGroup entity);
        void Delete(int id);
    }
}
