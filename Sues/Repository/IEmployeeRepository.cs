namespace Sues.Repository
{
    using Sues.Domain;
    using Sues.Repository.Filter;
    using System.Collections.Generic;

    public interface IEmployeeRepository
    {
        IEnumerable<Employee> Select(EmployeeFilter filter);
        Employee Get(int id);
        int Insert(Employee entity);
        void Update(Employee entity);
        void Delete(int id);
    }
}
