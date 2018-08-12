using Sues.Domain;
using Sues.Repository.Filter;
using System.Collections.Generic;

namespace Sues.Repository
{
    public interface IEmployeeInStudyGroupRepository
    {
        IEnumerable<EmployeeInStudyGroup> Select(EmployeeInStudyGroupFilter filter);
        EmployeeInStudyGroup Get(int id);
        int Insert(EmployeeInStudyGroup entity);
        void Update(EmployeeInStudyGroup entity);
        void Delete(int id);
    }
}
