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
    [ContractClass(typeof(CostListServiceContract))]
    public interface ICostListService
    {
        /// <summary>
        /// GetApplicationName
        /// </summary>
        /// <returns>WERWR</returns>
        IEnumerable<CostList> GetAllCostLists();
        IEnumerable<CostList> GetAllCostListsByDate(DateTime date);
        IEnumerable<CostList> GetAllCostListsByPoLineId(string poLineID);
        CostList GetAllCostListsById(string costListId);
        bool SaveCostList(List<List<CostList>> listOfCostListList);
        bool DeleteAndUpdateCostList(List<CostList> listOfCostListList, string poNumber, string poLineId);
        IEnumerable<CostList> GetAllCostListByPolineNumberAndId(string poLineId, string poNumber);
    }
}
