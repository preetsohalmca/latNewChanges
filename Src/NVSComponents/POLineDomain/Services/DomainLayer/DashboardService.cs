namespace Volvo.LAT.POLineDomain.DomainLayer
{
    using UserDomain.ServiceLayer;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Volvo.LAT.POLineDomain.DomainLayer.Entities;
    using Volvo.LAT.POLineDomain.DomainLayer.ProjectionRepositoryInterfaces;
    using Volvo.LAT.POLineDomain.DomainLayer.Projections;
    using Volvo.LAT.POLineDomain.DomainLayer.RepositoryInterfaces;
    using Volvo.LAT.POLineDomain.ServiceLayer;
    using NHibernate;
    using Volvo.LAT.PartDomain.DomainLayer.Entities;

    public class DashboardService : IDashboardService
    {

        /// <summary>
        /// A POLine repository used by the service.
        /// </summary>

        protected IUserService UserService { get; }
        protected IDashboardRepository DashboardRepository { get; }
        protected IApplicationRepositrory AppRepository { get; }
        /// <summary>
        /// A POLine selection repository used by the service.
        /// </summary>
        protected IPOLineSelectionRepository POLineSelectionRepository { set; get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="POLineService"/> class using provided services.
        /// </summary>
        /// <param name="POLineRepository">A POLine repository.</param>
        /// <param name="POLineSelectionRepository">A POLine selection repository.</param>
        public DashboardService(IDashboardRepository dashboardRepository,IUserService userService)
        {
            DashboardRepository = dashboardRepository;
            
            UserService = userService;
           
        }
        public IEnumerable<DashboardNewOrders> FindNewPurchaseOrders(int pagesize, int pageNuber, string username, out int totalrecords)
        {
            totalrecords = 0;
          return  this.DashboardRepository.FindNewPurchaseOrders(pagesize, pageNuber,  username, out totalrecords); }

        public IEnumerable<POLine> FindRenewals(int pagesize, int pageNuber, string username, out int totalrecords)
        {
            totalrecords = 0;
            return this.DashboardRepository.FindRenewals(pagesize, pageNuber,  username, out totalrecords);
        }
    }
}

#pragma warning restore SA1623 // Property summary documentation must match accessors
