namespace Volvo.LAT.POLineDomain.DomainLayer.RepositoryInterfaces
{
    using Microsoft.Practices.EnterpriseLibrary.Validation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Volvo.LAT.PartDomain.DomainLayer.Entities;
    using Volvo.LAT.POLineDomain.DomainLayer.Entities;
    using Volvo.NVS.Persistence.Repositories;

    /// <summary>
    /// Defines the interface for the repository of orders.
    /// </summary>
    public interface IDashboardRepository : IGenericRepository<POLine, long>
    {
    
        IEnumerable<DashboardNewOrders> FindNewPurchaseOrders(int pageZise, int pageNumber, string username, out int totalrecords);
        IEnumerable<POLine> FindRenewals(int pageZise, int pageNumber, string username,  out int totalrecords);
    }
}
