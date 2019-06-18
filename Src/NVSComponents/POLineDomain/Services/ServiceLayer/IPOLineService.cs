using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Volvo.LAT.PartDomain.DomainLayer.Entities;
using Volvo.LAT.POLineDomain.DomainLayer.Entities;
using Volvo.LAT.POLineDomain.DomainLayer.Projections;
using Volvo.LAT.POLineDomain.ServiceLayer.Contracts;

namespace Volvo.LAT.POLineDomain.ServiceLayer
{
    /// <summary>
    /// Service interface for POLineDomain component
    /// </summary>
    [ContractClass(typeof(POLineServiceContract))]
    public interface IPOLineService
    {
        /// <summary>
        /// Gets a queryable instance over the POLine selection read-only projection.
        /// </summary>
        /// <returns>A queryable over the POLine selection projection.</returns>
        IQueryable<POLineSelection> FindPOLineSelectionAsQueryable();
        IQueryable<POLine> FindPOLineAsQueryable(bool flag = false);
        IQueryable<POLine> FindPOLineAsQueryable(int pageNumber,int pageSize, out int totalRecords);
        POLine GetById(Guid purchaseLineId);

        POLine GetPolineByEbdNumber(string ebdNumber);
        POLine GetPolineByEbdNumberPoline(string ebdNumber,int poLine);
        /// <summary>
        /// Gets a POLine with a given number.
        /// </summary>
        /// <param name="number">A POLine number.</param>
        /// <returns>A located POLine instance.</returns>
        POLine GetPOLine(long number);

        /// <summary>
        /// Updates POLine availability information.
        /// </summary>
        /// <param name="listOfPOLine">A collection </param>
        //void UpdatePOLineAvailability(IList<POLineNewAvailability> listOfPOLine);

        IEnumerable<POLine> FindPOLineAsQueryable1(string search, bool isAdvancedSearch, DateTime startDate, DateTime endDate,
            string applicationId, string ownerName, string requesterName, string wbs, string assignmentCode, string contractTypeId,
            bool isRenewalYes, bool isRenewalNo, bool isRenewalAll,int pagesize,int pageNuber,out int totalrecords);

        IEnumerable<string> FindAllOwner1();

        //IEnumerable<App> GetApplicationName();
        IEnumerable<string> FindAllWBSORAssignmentCode();
        IEnumerable<string> FindAllRequesterName();
        IEnumerable<Owner> GetAllOwners();
		IEnumerable<ContractType> GetAllContractTypes();
        IEnumerable<App> GetAllApplications();
        IEnumerable<ActivityType> GetAllActivityTypes();
        IEnumerable<StatusPo> GetAllStatus();
		IEnumerable<Product> GellAllProducts();
        IEnumerable<CostType> GellAllCostTypes();
        IEnumerable<CostCenter> GetAllCostCenter();
        IEnumerable<Currency> GetAllCurrency();
        POLine FindPolineByPurchaseOrderLineID(string purchaseOrderLineId);
        EmailRecipent GetPoLinebyEbdNumber(string ebdNumber);
        bool SaveUpdateDetail(POLine poLine, string comments);
        List<CustomModelSecondGrid> GetCustomModelSecondGridData(string poLineId);
        bool SaveUpdateMonthlyRate(List<Tuple<string, string, decimal>> tuppleListRec);
        IEnumerable<PurchaseOrder> GetAllPurchaseOrders();
        decimal GetPOlineSeekAmount(string currency, int year);
        IQueryable<POLine> FindPOLineAsQueryableNew();
        bool CheckRenewalOrderLineExist(string renewalOredrLine);
        bool InserBulk(List<POLine> polines);
        IEnumerable<WbsElement> GetAllWbs();
        StatusPo InsertStatusPo(StatusPo status);

    }
}
