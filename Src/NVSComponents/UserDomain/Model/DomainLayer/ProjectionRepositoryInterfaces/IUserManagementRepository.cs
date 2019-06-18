using System.Linq;
using Volvo.NVS.Persistence.Repositories;
using Volvo.LAT.UserDomain.DomainLayer.Projections.Management;

namespace Volvo.LAT.UserDomain.DomainLayer.ProjectionRepositoryInterfaces
{
    /// <summary>
    /// Defines a contract for the <see cref="UserManagement"/> read-only repository.
    /// </summary>
    public interface IUserManagementRepository : IReadOnlyRepository<UserManagement, long>
    {
        /// <summary>
        /// Get the queryable over the non-deleted users.
        /// </summary>
        /// <returns>A queryable over a user management projection.</returns>
        IQueryable<UserManagement> FindNonDeleted();
    }
}
