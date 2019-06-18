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

    public class CostListService : ICostListService
    {
        /// <summary>
        /// A Application repository used by the service.
        /// </summary>
        protected ICostListRepositrory CostListRepository { get; }

        public CostListService(ICostListRepositrory costListRepository)
        {
            this.CostListRepository = costListRepository;
        }

        public IEnumerable<CostList> GetAllCostListsByDate(DateTime date) => this.CostListRepository.GetAllCostListByDate(date).OrderBy(x=>x.Date).ToList();

        public IEnumerable<CostList> GetAllCostListsByPoLineId(string poLineID) => this.CostListRepository.GetAllCostListByPolineId(poLineID).OrderBy(x => x.Date).ToList();

        public CostList GetAllCostListsById(string costListId) => this.CostListRepository.FindCostListById(costListId);

        public IEnumerable<CostList> GetAllCostLists() => this.CostListRepository.GetAllCostList();

        public bool SaveCostList(List<List<CostList>> listOfCostListList) => this.CostListRepository.SaveCostList(listOfCostListList);

        public bool DeleteAndUpdateCostList(List<CostList> listOfCostListList, string poNumber, string poLineId) => this.CostListRepository.DeleteAndUpdateCostList(listOfCostListList, poNumber, poLineId);
        public IEnumerable<CostList> GetAllCostListByPolineNumberAndId(string poLineId, string poNumber) => this.CostListRepository.GetAllCostListByPolineNumberAndId(poLineId, poNumber);

    }
}

#pragma warning restore SA1623 // Property summary documentation must match accessors