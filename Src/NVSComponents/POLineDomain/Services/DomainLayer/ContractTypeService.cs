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

    public class ContractTypeService : IContractTypeService
    {
        /// <summary>
        /// A Application repository used by the service.
        /// </summary>
        protected IContractTypeRepository contractTypeRepository { get; }

        public ContractTypeService(IContractTypeRepository contractTypeRepository)
        {
            this.contractTypeRepository = contractTypeRepository;
        }

        public IEnumerable<ContractType> GetContractTypes() => this.contractTypeRepository.GetAllContractType().OrderBy(x=>x.Name).ToList();
    }
}

#pragma warning restore SA1623 // Property summary documentation must match accessors