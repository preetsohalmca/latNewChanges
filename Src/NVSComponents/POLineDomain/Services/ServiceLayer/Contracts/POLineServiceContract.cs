using NHibernate;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Volvo.LAT.POLineDomain.DomainLayer.Entities;
using Volvo.LAT.POLineDomain.DomainLayer.Projections;
using System;
using Volvo.LAT.PartDomain.DomainLayer.Entities;

namespace Volvo.LAT.POLineDomain.ServiceLayer.Contracts
{
    [ContractClassFor(typeof(IPOLineService))]
    public abstract class POLineServiceContract : IPOLineService
    {
        public IQueryable<POLineSelection> FindPOLineSelectionAsQueryable() => default(IQueryable<POLineSelection>);
        public IQueryable<POLine> FindPOLineAsQueryable(bool flag = false) => default(IQueryable<POLine>);

        public IEnumerable<string> FindAllOwner() => default(IEnumerable<POLine>).Select(x => x.OwnerName).Distinct();
        public POLine GetPOLine(long number)
        {
            Contract.Requires(number > 0);
            return default(POLine);
        }
        public IEnumerable<string> FindAllOwner1() => default(IEnumerable<POLine>).OrderBy(x => x.OwnerName).OrderBy(x => x.OwnerName).Select(x => x.OwnerName).Distinct().ToList();
        public IEnumerable<POLine> FindPOLineAsQueryable1(string search, bool isAdvancedSearch, DateTime startDate, DateTime endDate,
            string applicationId, string ownerName, string requesterName, string wbs, string assignmentCode, string contractTypeId, 
            bool isRenewalYes, bool isRenewalNo, bool isRenewalAll, int pageSize, int pageNumber, out int totalrecords)
        {
            totalrecords = 0;
            return default(IEnumerable<POLine>)
                .Where(x =>
                        x.ContactPerson.Contains(search) ||
                        x.ProductNumber.Contains(search) ||
                        x.OwnerName.Contains(search)
                        || (x.StartDate.HasValue && (x.StartDate == startDate || startDate == DateTime.MinValue))
                        || (x.EndDate.HasValue && (x.EndDate == endDate || endDate == DateTime.MinValue))
                        || (!string.IsNullOrEmpty(x.OwnerName) && (x.OwnerName == ownerName || ownerName == null))
                        || (!string.IsNullOrEmpty(x.AcOrWbs) && (x.AcOrWbs == assignmentCode || assignmentCode == null))
                        || (!string.IsNullOrEmpty(x.AcOrWbs) && (x.AcOrWbs == wbs || wbs == null))
                        ).Take(pageSize).Skip(pageNumber).ToList();
        }

        public IQueryable<POLine> FindPOLineAsQueryable2(string search, bool isAdvancedSearch, DateTime startDate, DateTime endDate,
            string applicationId, string ownerName, string requesterName, string wbs, string assignmentCode, string contractTypeId, 
            bool isRenewalYes, bool isRenewalNo, bool isRenewalAll, int pageSize, int pageNumber, out int totalrecords)
        {
            totalrecords = 0;
            return default(IQueryable<POLine>)
                .Where(x =>
                        x.ContactPerson.Contains(search) ||
                        x.ProductNumber.Contains(search) ||
                        x.OwnerName.Contains(search)
                        || (x.StartDate.HasValue && (x.StartDate == startDate || startDate == DateTime.MinValue))
                        || (x.EndDate.HasValue && (x.EndDate == endDate || endDate == DateTime.MinValue))
                        || (!string.IsNullOrEmpty(x.OwnerName) && (x.OwnerName == ownerName || ownerName == null))
                        || (!string.IsNullOrEmpty(x.AcOrWbs) && (x.AcOrWbs == assignmentCode || assignmentCode == null))
                        || (!string.IsNullOrEmpty(x.AcOrWbs) && (x.AcOrWbs == wbs || wbs == null))
                        );
        }

        ////public IEnumerable<string> GetApplicationName() => default(IEnumerable<App>).Select(x => x.DeliveryManager).Distinct().ToList();
        //public IEnumerable<App> GetApplicationName() => default(IEnumerable<App>);

        public IEnumerable<string> FindAllWBSORAssignmentCode() => default(IEnumerable<POLine>).OrderBy(x => x.AcOrWbs).Select(x => x.AcOrWbs).Distinct().ToList();
        public IEnumerable<string> FindAllRequesterName() => default(IEnumerable<POLine>).OrderBy(x => x.RequestorName).Select(x => x.RequestorName).Distinct().ToList();
        //public void UpdatePOLineAvailability(IList<POLineNewAvailability> listOfPOLine) => Contract.Requires(listOfPOLine != null);
        public IEnumerable<Owner> GetAllOwners() => default(IEnumerable<Owner>);
        public IEnumerable<StatusPo> GetAllStatus() => default(IEnumerable<StatusPo>);
        public IEnumerable<ContractType> GetAllContractTypes() => default(IEnumerable<ContractType>);
        public IEnumerable<App> GetAllApplications() => default(IEnumerable<App>);
        public IEnumerable<ActivityType> GetAllActivityTypes() => default(IEnumerable<ActivityType>);
        public IEnumerable<Product> GellAllProducts() => default(IEnumerable<Product>);
        public IEnumerable<CostType> GellAllCostTypes() => default(IEnumerable<CostType>);
        public POLine FindPolineByPurchaseOrderLineID(string purchaseOrderLineId) => default(POLine);
        public bool SaveUpdateDetail(POLine poLine, string comments) => default(bool);
        public List<CustomModelSecondGrid> GetCustomModelSecondGridData(string poLineId) => default(List<CustomModelSecondGrid>);
        public POLine GetById(Guid purchaseLineId) => default(POLine);
        public EmailRecipent GetPoLinebyEbdNumber(string ebdNumber) => default(EmailRecipent);
        public bool SaveUpdateMonthlyRate(List<Tuple<string, string, decimal>> tuppleListRec) => default(bool);
        public IEnumerable<PurchaseOrder> GetAllPurchaseOrders() => default(IEnumerable<PurchaseOrder>);

        public IQueryable<POLine> FindPOLineAsQueryable(int pageNumber, int pageSize, out int totalRecords)
        {
            totalRecords = 0;
            return default(IQueryable<POLine>).Take(pageSize).Skip(pageNumber);
        }

        public IQueryable<POLine> FindPOLineAsQueryableWithoutTakeSkip()
        {
            return default(IQueryable<POLine>);
        }

        public decimal GetPOlineSeekAmount(string currency, int year) => default(decimal);

        public IQueryable<POLine> FindPOLineAsQueryableNew() => default(IQueryable<POLine>);

        public bool CheckRenewalOrderLineExist(string renewalOredrLine) => default(bool);

        public bool InserBulk(List<POLine> polines) => default(bool);

        public IEnumerable<CostCenter> GetAllCostCenter() => default(List<CostCenter>);

        public IEnumerable<Currency> GetAllCurrency() => default(IEnumerable<Currency>);

        public POLine GetPolineByEbdNumber(string ebdNumber) => default(POLine);

        public POLine GetPolineByEbdNumberPoline(string ebdNumber, int poLine)
        {
          POLine final =   default(IEnumerable<POLine>).Where(x => x.PoLine == poLine) .First();
            return final;


        }

        public IEnumerable<WbsElement> GetAllWbs() => default(IEnumerable<WbsElement>);

        public StatusPo InsertStatusPo(StatusPo status) => default(StatusPo);
    }
}
