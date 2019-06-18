namespace Volvo.LAT.POLineDomain.DomainLayer.ProjectionRepositoryInterfaces
{
    using Volvo.LAT.POLineDomain.DomainLayer.Projections;
    using Volvo.NVS.Persistence.Repositories;

    /// <summary>
    /// Defines a contract for a part selection, read-only repository.
    /// </summary>
    public interface IPOLineSelectionRepository : IReadOnlyRepository<POLineSelection, long>
    {
    }
}
