using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volvo.LAT.POLineDomain.DomainLayer.Entities;
using Volvo.NVS.Persistence.Repositories;

namespace Volvo.LAT.POLineDomain.DomainLayer.RepositoryInterfaces
{
    public interface ICostListRepositrory : IGenericRepository<CostList, long>
    {
        CostList FindCostListById(string number);

        IEnumerable<CostList> GetAllCostListByDate(DateTime date);

        IEnumerable<CostList> GetAllCostList();

        IEnumerable<CostList> GetAllCostListByPolineId(string poLineId);

        bool SaveCostList(List<List<CostList>> listOfCostListList);
        bool DeleteAndUpdateCostList(List<CostList> listOfCostListList, string poNumber, string poLineId);

        IEnumerable<CostList> GetAllCostListByPolineNumberAndId(string poLineId, string poNumber);
    }
}
