using NHibernate;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Volvo.LAT.POLineDomain.DomainLayer.Entities;
using Volvo.LAT.POLineDomain.DomainLayer.Projections;
using System;

namespace Volvo.LAT.POLineDomain.ServiceLayer.Contracts
{
    [ContractClassFor(typeof(IContractTypeService))]
    public abstract class ContractTypeServiceContract : IContractTypeService
    {
        /// <summary>
        /// GetContractType
        /// </summary>
        /// <returns>App</returns>
        public IEnumerable<ContractType> GetContractTypes() => default(IEnumerable<ContractType>);
    }
}
