using System.Linq;
using Volvo.NVS.Persistence.NHibernate.Repositories;
using Volvo.LAT.UserDomain.DomainLayer.ProjectionRepositoryInterfaces;
using Volvo.LAT.UserDomain.DomainLayer.Projections.Management;

namespace Volvo.LAT.UserDomain.InfrastructureLayer.ProjectionRepositories
{
    /// <summary>
    /// Provides user management specific projections.
    /// </summary>
    public class UserManagementRepository : ReadOnlyRepository<UserManagement>, IUserManagementRepository
    {
        /// <summary>
        /// Get the queryable over the non-deleted users.
        /// </summary>
        /// <returns>A queryable over a user management projection.</returns>
        public IQueryable<UserManagement> FindNonDeleted()
        {
            return Find()/*Where(user => !user.IsDeleted)*/;
        }
    }
}
