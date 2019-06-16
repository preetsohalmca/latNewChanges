using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volvo.LAT.POLineDomain.DomainLayer.Entities;
using Volvo.LAT.POLineDomain.DomainLayer.RepositoryInterfaces;
using Volvo.NVS.Persistence.NHibernate.Repositories;

namespace Volvo.LAT.PartDomain.InfrastructureLayer.Repositories
{
    public class ContractTypeRepository : GenericRepository<ContractType>, IContractTypeRepository
    {
        public ContractType FindContractTypeById(long number)
        {
            throw new NotImplementedException();
        }

        public ContractType FindByNumber(long contractTypeId)
        {
            var test = this.Session.QueryOver<ContractType>().List();
            return test.FirstOrDefault();
        }

        public IEnumerable<ContractType> GetAllContractType(string contractTypeId = null)
        {
            var test = this.Session.QueryOver<ContractType>().List();
            return test;
        }
    }
}
