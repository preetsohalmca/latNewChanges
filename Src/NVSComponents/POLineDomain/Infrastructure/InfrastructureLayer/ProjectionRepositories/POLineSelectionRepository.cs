using Volvo.NVS.Persistence.NHibernate.Repositories;
using Volvo.LAT.POLineDomain.DomainLayer.ProjectionRepositoryInterfaces;
using Volvo.LAT.POLineDomain.DomainLayer.Projections;

namespace Volvo.LAT.POLineDomain.InfrastructureLayer.ProjectionRepositories
{
    /// <summary>
    /// Provides POLine selection specific projections.
    /// </summary>
    public class POLineSelectionRepository : ReadOnlyRepository<POLineSelection>, IPOLineSelectionRepository
    {
    }
}
