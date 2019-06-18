using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Volvo.LAT.PartDomain.DomainLayer.Entities;
using Volvo.LAT.POLineDomain.DomainLayer.Entities;
using Volvo.LAT.POLineDomain.DomainLayer.Projections;
using Volvo.LAT.POLineDomain.ServiceLayer.Contracts;

namespace Volvo.LAT.POLineDomain.ServiceLayer
{
    /// <summary>
    /// Service interface for POLineDomain component
    /// </summary>
    [ContractClass(typeof(DashboardServiceContract))]
    public interface IDashboardService
    {
        IEnumerable<DashboardNewOrders> FindNewPurchaseOrders(int pagesize, int pageNuber, string username, out int totalRecords);
        IEnumerable<POLine> FindRenewals(int pagesize, int pageNuber, string username, out int totalRecords);
    }
}
