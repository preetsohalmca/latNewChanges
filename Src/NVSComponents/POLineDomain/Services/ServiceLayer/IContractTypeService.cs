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
    [ContractClass(typeof(ContractTypeServiceContract))]
    public interface IContractTypeService
    {
        /// <summary>
        /// GetApplicationName
        /// </summary>
        /// <returns>WERWR</returns>
        IEnumerable<ContractType> GetContractTypes();
    }
}
