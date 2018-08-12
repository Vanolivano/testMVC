namespace Sues.Repository
{
    using Sues.Domain;
    using Sues.Repository.Filter;
    using System.Collections.Generic;

    public interface IOrganizationRepository
    {
        IEnumerable<Organization> Select(OrganizationFilter filter);
        Organization Get(int id);
        int Insert(Organization entity);
        void Update(Organization entity);
        void Delete(int id);
    }
}
