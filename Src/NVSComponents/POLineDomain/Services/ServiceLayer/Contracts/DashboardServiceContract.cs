using NHibernate;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Volvo.LAT.POLineDomain.DomainLayer.Entities;
using Volvo.LAT.POLineDomain.DomainLayer.Projections;
using System;
using Volvo.LAT.PartDomain.DomainLayer.Entities;

namespace Volvo.LAT.POLineDomain.ServiceLayer.Contracts
{
    [ContractClassFor(typeof(IDashboardService))]
    public abstract class DashboardServiceContract : IDashboardService
    {
        public IEnumerable<DashboardNewOrders> FindNewPurchaseOrders(int pagesize, int pageNuber, string username, out int totalRecords) { totalRecords = 0; return default(IEnumerable<DashboardNewOrders>); }
        public IEnumerable<POLine> FindRenewals(int pagesize, int pageNuber, string username, out int totalRecords)
        {
            totalRecords = 0;
            return default(IEnumerable<POLine>);
        }
    }
}
