namespace Volvo.LAT.POLineDomain.ServiceLayer
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using Volvo.LAT.POLineDomain.DomainLayer.Entities;
    using Volvo.LAT.POLineDomain.DomainLayer.Projections;
    using Volvo.LAT.POLineDomain.ServiceLayer.Contracts;

    /// <summary>
    /// Service interface for POLineDomain component
    /// </summary>
    [ContractClass(typeof(ApplicationServiceContract))]
    public interface IApplicationService
    {
        /// <summary>
        /// GetApplicationName
        /// </summary>
        /// <returns>WERWR</returns>
        IEnumerable<App> GetApplicationName();
    }
}
