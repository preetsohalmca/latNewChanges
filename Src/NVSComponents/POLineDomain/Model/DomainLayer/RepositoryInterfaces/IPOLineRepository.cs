namespace Volvo.LAT.POLineDomain.DomainLayer.RepositoryInterfaces
{
    using Microsoft.Practices.EnterpriseLibrary.Validation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Volvo.LAT.PartDomain.DomainLayer.Entities;
    using Volvo.LAT.POLineDomain.DomainLayer.Entities;
    using Volvo.NVS.Persistence.Repositories;

    /// <summary>
    /// Defines the interface for the repository of orders.
    /// </summary>
    public interface IPOLineRepository : IGenericRepository<POLine, long>
    {
        /// <summary>
        /// Finds a part by its number.
        /// </summary>
        /// <param name="number">A part number to be located.</param>
        /// <returns>A part number or null when not found.</returns>
        IEnumerable<AssignmentCode> GetAssignmentCode(string ponumber, int poline);
        POLine FindByNumber(long number);
        POLine GetById(Guid purchaseOrderLineId);
        StatusPo InserStatusPo(StatusPo status);
        IEnumerable<string> FindAllOwner(string ownerName = null);
        POLine FindPolineByPurchaseOrderLineID(string purchaseOrderLineId);
        IEnumerable<Owner> GetAllOwners();
        IEnumerable<StatusPo> GetAllStatus();
        IEnumerable<ContractType> GetAllContractTypes();
        IEnumerable<App> GetAllApplications();
        IEnumerable<ActivityType> GetAllActivityTypes();
        IEnumerable<Product> GellAllProducts();
        IEnumerable<CostType> GellAllCostTypes();
        bool SaveUpdateDetail(POLine pLine, string purchaseOrderComments);
        ValidationResults UpdateDetail(POLine pLine);
        List<POLine> FillOwnerNameByRelationship(IEnumerable<POLine> purchaseOrderList);
        IQueryable<POLine> FillOwnerNameByRelationshipQurable(IQueryable<POLine> purchaseOrderList);
        EmailRecipent GetPoLinebyEbdNumber(string ebdNumber);
        List<CustomModelSecondGrid> RetrieveDetailInnerGridData(string poLineId);
        bool SaveUpdateMonthlyRate(List<Tuple<string, string, decimal>> tuppleListRec);
        IEnumerable<PurchaseOrder> GellAllPurchaseOrder();
        decimal GetSeekAmount(string currency, int year);
        PurchaseOrderLineFromEbd GetPurchaseOrderLineFromEbdById(Guid purchaseOrderLineFromEbdId);
        bool CheckRenewalOrderLineExist(string renewalOrderLine);
        bool BulkInsert(List<POLine> polines);
        IEnumerable<CostCenter> GetAllCostCenter();
        IEnumerable<Currency> GetAllCurrency();
        POLine GetPolineByEbdNumber(string ebdNumber);
        POLine GetPolineByEbdNumberPoline(string ebdNumber,int poLine);
        IEnumerable<WbsElement> GetAllWbs();    
    }
}
