#pragma warning disable SA1623 // Property summary documentation must match accessors

namespace Volvo.LAT.POLineDomain.DomainLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NHibernate;
    using UserDomain.ServiceLayer;
    using Volvo.LAT.POLineDomain.DomainLayer.Entities;
    using Volvo.LAT.POLineDomain.DomainLayer.ProjectionRepositoryInterfaces;
    using Volvo.LAT.POLineDomain.DomainLayer.Projections;
    using Volvo.LAT.POLineDomain.DomainLayer.RepositoryInterfaces;
    using Volvo.LAT.POLineDomain.ServiceLayer;

    public class ApplicationService : IApplicationService
    {
        /// <summary>
        /// A Application repository used by the service.
        /// </summary>
        protected IApplicationRepositrory AppRepository { get; }

        public ApplicationService(IApplicationRepositrory applicationRepository)
        {
            this.AppRepository = applicationRepository;
        }

        public IEnumerable<App> GetApplicationName() => this.AppRepository.GetAllApps().OrderBy(x=>x.Name).ToList();
    }
}

#pragma warning restore SA1623 // Property summary documentation must match accessors