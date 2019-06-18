namespace Volvo.LAT.POLineDomain.InfrastructureLayer.Repositories
{
    using System;
    using System.Collections.Generic;
    using Volvo.LAT.POLineDomain.DomainLayer.Entities;
    using Volvo.LAT.POLineDomain.DomainLayer.RepositoryInterfaces;
    using Volvo.NVS.Persistence.NHibernate.Repositories;

    /// <summary>
    /// The POLine repository.
    /// </summary>
    public class DashboardRepository : GenericRepository<POLine>, IDashboardRepository
    {
        public IEnumerable<DashboardNewOrders> FindNewPurchaseOrders(int pageZise, int pagesSkip, string username, out int totalRecords)
        {

            var count = this.Session.CreateSQLQuery("exec GetTotalRenewalCount :username").SetParameter("username", username);
            totalRecords = count.UniqueResult<int>();
            var query = this.Session.CreateSQLQuery("exec GetNewPurchaseOrders :skip, :take,:username").AddEntity(typeof(DashboardNewOrders)).SetParameter("skip", pagesSkip)
                    .SetParameter("take", pageZise).SetParameter("username", username).List<DashboardNewOrders>();


            return query;
        }

        public IEnumerable<POLine> FindRenewals(int pageZise, int pagesSkip, string username, out int totalRecords)
        {
            var count = this.Session.CreateSQLQuery("exec GetTotalMyRenewalCount :username").SetParameter("username", username);
            totalRecords = count.UniqueResult<int>();
            var query = this.Session.CreateSQLQuery("exec GetMyrenewals :skip, :take, :username").AddEntity(typeof(POLine)).SetParameter("skip", pagesSkip)
                   .SetParameter("take", pageZise).SetParameter("username", username).List<POLine>();
            return query;
        }
    }
}
