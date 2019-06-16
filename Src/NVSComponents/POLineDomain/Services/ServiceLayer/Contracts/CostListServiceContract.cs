using NHibernate;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Volvo.LAT.POLineDomain.DomainLayer.Entities;
using Volvo.LAT.POLineDomain.DomainLayer.Projections;
using System;

namespace Volvo.LAT.POLineDomain.ServiceLayer.Contracts
{
    [ContractClassFor(typeof(ICostListService))]
    public abstract class CostListServiceContract : ICostListService
    {
        public IEnumerable<CostList> GetAllCostLists()=>default(IEnumerable<CostList>);

        public IEnumerable<CostList> GetAllCostListsByDate(DateTime date) => default(IEnumerable<CostList>);

        public CostList GetAllCostListsById(string costListId) => default(CostList);

        public IEnumerable<CostList> GetAllCostListsByPoLineId(string poLineID) => default(IEnumerable<CostList>);

        public bool SaveCostList(List<List<CostList>> listOfCostListList) => default(bool);

        public bool DeleteAndUpdateCostList(List<CostList> listOfCostListList, string poNumber, string poLineId) => default(bool);

        public IEnumerable<CostList> GetAllCostListByPolineNumberAndId(string poLineId, string poNumber) => default(IEnumerable<CostList>);
    }
}
