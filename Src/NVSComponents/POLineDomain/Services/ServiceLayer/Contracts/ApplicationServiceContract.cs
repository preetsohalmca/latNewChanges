using NHibernate;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Volvo.LAT.POLineDomain.DomainLayer.Entities;
using Volvo.LAT.POLineDomain.DomainLayer.Projections;
using System;

namespace Volvo.LAT.POLineDomain.ServiceLayer.Contracts
{
    [ContractClassFor(typeof(IApplicationService))]
    public abstract class ApplicationServiceContract : IApplicationService
    {
        /// <summary>
        /// GetApplicationName
        /// </summary>
        /// <returns>App</returns>
        public IEnumerable<App> GetApplicationName() => default(IEnumerable<App>);

    }
}
