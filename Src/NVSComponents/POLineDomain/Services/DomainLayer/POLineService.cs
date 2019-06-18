namespace Volvo.LAT.POLineDomain.DomainLayer
{
    using UserDomain.ServiceLayer;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Volvo.LAT.POLineDomain.DomainLayer.Entities;
    using Volvo.LAT.POLineDomain.DomainLayer.ProjectionRepositoryInterfaces;
    using Volvo.LAT.POLineDomain.DomainLayer.Projections;
    using Volvo.LAT.POLineDomain.DomainLayer.RepositoryInterfaces;
    using Volvo.LAT.POLineDomain.ServiceLayer;
    using NHibernate;
    using Volvo.LAT.PartDomain.DomainLayer.Entities;

    public class POLineService : IPOLineService
    {
        /// <summary>
        /// A POLine repository used by the service.
        /// </summary>

        protected IUserService UserService { get; }
        protected IPOLineRepository POLineRepository { get; }
        protected IApplicationRepositrory AppRepository { get; }
        /// <summary>
        /// A POLine selection repository used by the service.
        /// </summary>
        protected IPOLineSelectionRepository POLineSelectionRepository { set; get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="POLineService"/> class using provided services.
        /// </summary>
        /// <param name="POLineRepository">A POLine repository.</param>
        /// <param name="POLineSelectionRepository">A POLine selection repository.</param>
        public POLineService(IPOLineRepository pOLineRepository, IPOLineSelectionRepository pOLineSelectionRepository,
       IUserService userService, IApplicationRepositrory applicationRepository)
        {
            POLineRepository = pOLineRepository;
            POLineSelectionRepository = pOLineSelectionRepository;
            UserService = userService;
            AppRepository = applicationRepository;
        }

        /// <summary>
        /// Gets a POLine with a given number.
        /// </summary>
        /// <param name="number">A POLine number.</param>
        /// <returns>A located POLine instance.</returns>
        public POLine GetPOLine(long number) => this.POLineRepository.FindByNumber(number);

        /// <summary>
        /// Gets a queryable instance over the POLine selection read-only projection.
        /// </summary>
        /// <returns>A queryable over the POLine selection projection.</returns>
        public virtual IQueryable<POLineSelection> FindPOLineSelectionAsQueryable() => this.POLineSelectionRepository.Find();
        //public virtual IQueryable<POLine> FindPOLineAsQueryable() => this.POLineRepository.Find();
        public virtual IQueryable<POLine> FindPOLineAsQueryable(bool flag = false)
        {
            var quearbleRsltSetWithoutOwnerName = this.POLineRepository.Find();
            if (flag)
            {
                var resultSetWithFilledOwnerName = this.POLineRepository.FillOwnerNameByRelationshipQurable(quearbleRsltSetWithoutOwnerName);
                return resultSetWithFilledOwnerName;
            }
            else
            {
                return quearbleRsltSetWithoutOwnerName;
            }
            //result = this.POLineServices.FindPOLineAsQueryable()
            //    .ToDataSourceResult(request);
        }

        public virtual IQueryable<POLine> FindPOLineAsQueryableNew()
        {
            var quearbleRsltSetWithoutOwnerName = this.POLineRepository.Find();
            return quearbleRsltSetWithoutOwnerName;
        }

        public virtual IQueryable<POLine> FindPOLineAsQueryable(int pageNumber, int pageSize, out int totalRecords)
        {
            totalRecords = this.POLineRepository.Count();
            var quearbleRsltSetWithoutOwnerName = this.POLineRepository.Find().Take(pageSize).Skip(pageNumber);
            var resultSetWithFilledOwnerName = this.POLineRepository.FillOwnerNameByRelationshipQurable(quearbleRsltSetWithoutOwnerName);
            return resultSetWithFilledOwnerName;
            //result = this.POLineServices.FindPOLineAsQueryable()
            //    .ToDataSourceResult(request);
        }

        public virtual IEnumerable<string> FindAllOwner() => this.POLineRepository.Find().Select(x => x.OwnerName).Distinct();

        public IEnumerable<string> FindAllOwner1() => this.POLineRepository.Find().Select(x => x.OwnerName).Distinct().ToList();

        public IEnumerable<POLine> FindPOLineAsQueryable1(string search, bool isAdvancedSearch, DateTime startDate, DateTime endDate,
            string applicationId, string ownerName, string requesterName, string wbs, string assignmentCode, string contractTypeId, 
            bool isRenewalYes, bool isRenewalNo, bool isRenewalAll,int pagesize,int pageNumber,out int totalRecords)
        {
            int poLine = 0;
            int.TryParse(search,out poLine);
			bool isFilterExists = false;
            List<POLine> resultSet1 = new List<POLine>();
            List<POLine> resultSet2 = new List<POLine>();

            IEnumerable<POLine> query = null;


            if(startDate != DateTime.MinValue)
            {
                query = this.POLineRepository.Find().Where(x => (x.StartDate != null
                                               && x.StartDate.Value.Year >= startDate.Year
                                               && x.StartDate.Value.Month >= startDate.Month));
                isFilterExists = true;
            }

            if (endDate != DateTime.MinValue)
            {
                if (query != null)
                {
                    query = query.Where(x => (x.EndDate != null
                                                    && x.EndDate.Value.Year <= endDate.Year
                                                    && x.EndDate.Value.Month <= endDate.Month));
                }
                else
                {
                    query = this.POLineRepository.Find().Where(x => (x.EndDate != null
                                                    && x.EndDate.Value.Year <= endDate.Year
                                                    && x.EndDate.Value.Month <= endDate.Month));
                }
                isFilterExists = true;
            }

            if (!string.IsNullOrEmpty(ownerName))
            {
                if (query != null)
                {
                    query = query.Where(x => x.PurchaseOrderLineFromEbd.PurchaseOrder.Owner.Name.Trim().ToLower() == ownerName.Trim().ToLower());
                }
                else
                {
                    query = this.POLineRepository.Find().Where(x => x.PurchaseOrderLineFromEbd.PurchaseOrder.Owner.Name.Trim().ToLower() == ownerName.Trim().ToLower());
                }

                isFilterExists = true;
            }

            if (!string.IsNullOrEmpty(requesterName))
            {
                if (query != null)
                {
                    ////query = query.Where(x => x.RequestorName.Trim().ToLower() == requesterName.Trim().ToLower());
                    //query = query.Where(x => String.Equals(x.RequestorName, requesterName, StringComparison.OrdinalIgnoreCase));
                    query = query.Where(x => x.RequestorName.Equals(requesterName));
                }
                else
                {
                    ////query = this.POLineRepository.Find().Where(x => x.RequestorName.Trim().ToLower() == requesterName.Trim().ToLower());
                    //query = this.POLineRepository.Find().Where(x => String.Equals(x.RequestorName, requesterName, StringComparison.OrdinalIgnoreCase));
                    query = this.POLineRepository.Find().Where(x => x.RequestorName.Equals(requesterName));
                }
                isFilterExists = true;
            }

            if (!string.IsNullOrEmpty(wbs))
            {
                if (query != null)
                {
                    //query = query.Where(x => x.AcOrWbs.Trim().ToLower() == wbs.Trim().ToLower());
                    query = query.Where(x => x.WbsElement.Name.Trim().ToLower() == wbs.Trim().ToLower());
                }
                else
                {
                    //query = this.POLineRepository.Find().Where(x => x.AcOrWbs.Trim().ToLower() == wbs.Trim().ToLower());
                    query = this.POLineRepository.Find().Where(x => x.WbsElement.Name.Trim().ToLower() == wbs.Trim().ToLower());
                }
                isFilterExists = true;
            }

            if (!string.IsNullOrEmpty(assignmentCode))
            {
                if (query != null)
                {
                    query = query.Where(x => x.AcOrWbs!=null);
                    query = query.Where(x => x.AcOrWbs.Trim().ToLower() == assignmentCode.Trim().ToLower());
                }
                else
                {
                    query = this.POLineRepository.Find().Where(x => x.AcOrWbs.Trim().ToLower() == assignmentCode.Trim().ToLower());
                }
                isFilterExists = true;
            }

            if (!string.IsNullOrEmpty(applicationId))
            {
                if (query != null)
                {
                    query = query.Where(x =>
                    x.App.AppId.ToString() == applicationId);
                }
                else
                {
                    query = this.POLineRepository.Find().Where(x =>
                                       x.App.AppId.ToString() == applicationId);
                }
                isFilterExists = true;
            }

            if (!string.IsNullOrEmpty(contractTypeId))
            {
                if (query != null)
                {
                    query = query.Where(x =>
                    x.ContractType.ContractTypeId.ToString() == contractTypeId);
                }
                else
                {
                    query = this.POLineRepository.Find().Where(x =>
                                        x.ContractType.ContractTypeId.ToString() == contractTypeId);
                }
                isFilterExists = true;
            }

            if (isRenewalYes && isRenewalNo)
            {
                if (query != null)
                {
                    query = query.Where(x => x.Renewal == true || x.Renewal == false);
                }
                else
                {
                    query = this.POLineRepository.Find().Where(x => x.Renewal == true || x.Renewal == false);
                }
                isFilterExists = true;
            }
            else if (isRenewalYes)
            {
                if (query != null)
                {
                    query = query.Where(x => x.Renewal == true);
                }
                else
                {
                    query = this.POLineRepository.Find().Where(x => x.Renewal == true);
                }
                isFilterExists = true;
            }
            else if (isRenewalNo)
            {
                if (query != null)
                {
                    query = query.Where(x => x.Renewal == false);
                }
                else
                {
                    query = this.POLineRepository.Find().Where(x => x.Renewal == false);
                }
                isFilterExists = true;
            }
            if (query == null)
            {
                totalRecords = this.POLineRepository.Find().Count();
                query = this.POLineRepository.Find();//.OrderBy(x => x.StartDate).OrderBy(x => x.EndDate);//.Skip(pageNumber).Take(pagesize);
            }
            else
            {
                totalRecords = query.Count();
                //query = query.OrderBy(x => x.StartDate).OrderBy(x => x.EndDate);//.Skip(pageNumber).Take(pagesize);
            }

            if (!string.IsNullOrEmpty(search))
            {
              
                resultSet2 = this.POLineRepository.Find().Where(x => (x.Software != null && x.Software.Contains(search))
                                                                || (x.ContactPerson != null && x.ContactPerson.Contains(search))
                                                                || (x.ProductNumber != null && x.ProductNumber.Contains(search))
                                                                || (x.PurchaseOrderLineFromEbd.PurchaseOrder.Owner.Name != null && x.PurchaseOrderLineFromEbd.PurchaseOrder.Owner.Name.Contains(search))//x.OwnerName.Contains(search))
                                                                || (x.RequestorName != null && x.RequestorName.Contains(search))
                                                                || (x.EbdNumber != null && x.EbdNumber.Contains(search))
                                                                || (x.AcOrWbs != null && x.AcOrWbs.Contains(search))
                                                                || (poLine > 0 && x.PoLine == poLine)
                                                                ).ToList();
               

            }

            List<POLine> result = new List<POLine>();

            if (isFilterExists)
            {
                resultSet1 = query.ToList();
            }

            if (!isFilterExists && string.IsNullOrEmpty(search))
            {
                resultSet1 = query.ToList();
            }

            if (resultSet1.Count > 0 && !string.IsNullOrEmpty(search))
            {
                result = resultSet1.Where(x => (!string.IsNullOrEmpty(x.Software) && x.Software.Contains(search))
                                                                || (!string.IsNullOrEmpty(x.ContactPerson) && x.ContactPerson.Contains(search))
                                                                || (!string.IsNullOrEmpty(x.ProductNumber) && x.ProductNumber.Contains(search))
                                                                //|| (!string.IsNullOrEmpty(x.OwnerName) && x.OwnerName.Contains(search))
                                                                || (x.PurchaseOrderLineFromEbd.PurchaseOrder.Owner.Name != null && x.PurchaseOrderLineFromEbd.PurchaseOrder.Owner.Name.Contains(search))
                                                                || (!string.IsNullOrEmpty(x.RequestorName) && x.RequestorName.Contains(search))
                                                                || (!string.IsNullOrEmpty(x.EbdNumber) && x.EbdNumber.Contains(search))
                                                                || (!string.IsNullOrEmpty(x.AcOrWbs) && x.AcOrWbs.Contains(search))
                                                                || (poLine > 0 && x.PoLine == poLine)
                                                                ).ToList();
                totalRecords = result.Count;
            }
            else
            {
                result = resultSet1.Union(resultSet2).ToList();
                totalRecords = totalRecords+ resultSet2.Count;
            }



            if (result.Count > 0)
            {
                result = this.POLineRepository.FillOwnerNameByRelationship(result);

                
            }

            

            //var result = resultSet1.Union(resultSet2).ToList();

            //var result = this.POLineRepository.Find().Where(x => x.Software.Contains(search)
            //                                                || x.ContactPerson.Contains(search)
            //                                                || x.ProductNumber.Contains(search)
            //                                                || x.OwnerName.Contains(search)
            //                                                || x.RequestorName.Contains(search)
            //                                                || x.EbdNumber.Contains(search)
            //                                                || x.AcOrWbs.Contains(search) 
            //                                                || (x.StartDate.HasValue && (x.StartDate == startDate || startDate == DateTime.MinValue))
            //                                                || (x.EndDate.HasValue && (x.EndDate == endDate || endDate == DateTime.MinValue))
            //                                                || ((x.OwnerName != null && x.OwnerName != "") && (x.OwnerName == ownerName || ownerName == ""))
            //                                                || ((x.AcOrWbs != null && x.AcOrWbs != "") && (x.AcOrWbs == assignmentCode || assignmentCode == ""))
            //                                                || ((x.AcOrWbs != null && x.AcOrWbs != "") && (x.AcOrWbs == wbs || wbs == ""))
            //                                                || (poLine > 0 && x.PoLine == poLine)).ToList();
           

            //foreach (var item in result)
            //{
            //    var pb = this.POLineRepository.GetPurchaseOrderLineFromEbdById(item.PurchaseOrderLineFromEbd.PurchaseOrderLineFromEbdId);
            //    if (pb!=null)
            //    item.CostCenter = pb.CostCenter;
            //}

            return result;
            // return default(IEnumerable<POLine>).Where(x => !string.IsNullOrEmpty(x.Software) && x.Software.Contains(search)).ToList();
        }

        public IEnumerable<App> GetApplicationName() => this.AppRepository.GetAllApps().ToList();
      public virtual IEnumerable<string> FindAllWBSORAssignmentCode() => this.POLineRepository.Find().OrderBy(x=>x.AcOrWbs).Select(x => x.AcOrWbs).Distinct().ToList();

        public virtual IEnumerable<string> FindAllRequesterName() => this.POLineRepository.Find().OrderBy(x => x.RequestorName).Select(x => x.RequestorName).Distinct().ToList();


        public virtual IEnumerable<Owner> GetAllOwners() => this.POLineRepository.GetAllOwners().OrderBy(s=>s.Name);
        public virtual IEnumerable<StatusPo> GetAllStatus() => this.POLineRepository.GetAllStatus().OrderBy(s => s.Name);

        public IEnumerable<ContractType> GetAllContractTypes() => this.POLineRepository.GetAllContractTypes().OrderBy(s => s.Name);

        public IEnumerable<App> GetAllApplications() => this.POLineRepository.GetAllApplications().OrderBy(s=>s.Name);

        public IEnumerable<ActivityType> GetAllActivityTypes() => this.POLineRepository.GetAllActivityTypes().OrderBy(s => s.Name);
        public IEnumerable<Product> GellAllProducts() => this.POLineRepository.GellAllProducts().OrderBy(s => s.Name);
        public IEnumerable<CostType> GellAllCostTypes() => this.POLineRepository.GellAllCostTypes().OrderBy(s => s.Name);
        public POLine FindPolineByPurchaseOrderLineID(string purchaseOrderLineId) => this.POLineRepository.FindPolineByPurchaseOrderLineID(purchaseOrderLineId);
        public bool SaveUpdateDetail(POLine poLine, string comments) => this.POLineRepository.SaveUpdateDetail(poLine, comments);
        public List<CustomModelSecondGrid> GetCustomModelSecondGridData(string poLineId)
        {
            var customModelSecondGridData = this.POLineRepository.RetrieveDetailInnerGridData(poLineId);
            return customModelSecondGridData;
        }
        public POLine GetById(Guid purchaseLineId) => this.POLineRepository.GetById(purchaseLineId);
        public EmailRecipent GetPoLinebyEbdNumber(string ebdNumber) => this.POLineRepository.GetPoLinebyEbdNumber(ebdNumber);
        public bool SaveUpdateMonthlyRate(List<Tuple<string, string, decimal>> tuppleListRec) => this.POLineRepository.SaveUpdateMonthlyRate(tuppleListRec);

        public IEnumerable<PurchaseOrder> GetAllPurchaseOrders() => this.POLineRepository.GellAllPurchaseOrder();

        public decimal GetPOlineSeekAmount(string currency, int year) => this.POLineRepository.GetSeekAmount(currency, year);
        public bool CheckRenewalOrderLineExist(string renewalOredrLine) => this.POLineRepository.CheckRenewalOrderLineExist(renewalOredrLine);

        public bool InserBulk(List<POLine> polines) => this.POLineRepository.BulkInsert(polines);

        public IEnumerable<CostCenter> GetAllCostCenter() => this.POLineRepository.GetAllCostCenter();

        public IEnumerable<Currency> GetAllCurrency() => this.POLineRepository.GetAllCurrency();

        public POLine GetPolineByEbdNumber(string ebdNumber) => this.POLineRepository.GetPolineByEbdNumber(ebdNumber);
        public POLine GetPolineByEbdNumberPoline(string ebdNumber,int poLine) => this.POLineRepository.GetPolineByEbdNumberPoline(ebdNumber, poLine);

        public IEnumerable<WbsElement> GetAllWbs() => this.POLineRepository.GetAllWbs().OrderBy(x=>x.Name);

        public StatusPo InsertStatusPo(StatusPo status) => this.POLineRepository.InserStatusPo(status);
    }    
}

#pragma warning restore SA1623 // Property summary documentation must match accessors
