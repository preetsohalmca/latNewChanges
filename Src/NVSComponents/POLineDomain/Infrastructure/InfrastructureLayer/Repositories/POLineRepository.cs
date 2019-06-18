using System.Linq;
using Volvo.NVS.Persistence.NHibernate.Repositories;
using Volvo.LAT.POLineDomain.DomainLayer.Entities;
using Volvo.LAT.POLineDomain.DomainLayer.RepositoryInterfaces;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System;
using Volvo.LAT.PartDomain.DomainLayer.Entities;
using NHibernate;
using NHibernate.Engine;
using NHibernate.Type;

namespace Volvo.LAT.POLineDomain.InfrastructureLayer.Repositories
{
    /// <summary>
    /// The POLine repository.
    /// </summary>
    public class POLineRepository : GenericRepository<POLine>, IPOLineRepository
    {
        /// <summary>
        /// Finds a POLine by its number.
        /// </summary>
        /// <param name="number">A POLine number to be located.</param>
        /// <returns>A POLine number or null when not found.</returns>
        /// public POLine FindByNumber(long number) =>
        ///  Session.QueryOver<POLine>().Where(POLine => POLine.PoLine == number).List().FirstOrDefault();
        public POLine FindByNumber(long number)
        {
            //return FindAll().First();
            var test = this.Session.QueryOver<POLine>().List();
            return test.FirstOrDefault();
         }
        public IEnumerable<string> FindAllOwner(string ownerName = null)
        {
            //var test = this.Session.QueryOver<POLine>().Select;
            var test = this.Session.QueryOver<POLine>().List() ;
 
            return test.Select(x => x.OwnerName).Distinct();
        }
        public IEnumerable<Owner> GetAllOwners()
        {
            return this.Session.QueryOver<Owner>().List();
        }
        public IEnumerable<StatusPo> GetAllStatus()
        {
            return this.Session.QueryOver<StatusPo>().List();
        }
        public IEnumerable<ContractType> GetAllContractTypes()
        {
            return this.Session.QueryOver<ContractType>().List();
        }
        public IEnumerable<CostCenter> GetAllCostCenter()
        {
            return this.Session.QueryOver<CostCenter>().List();
        }
        public IEnumerable<Currency> GetAllCurrency()
        {
            return this.Session.QueryOver<Currency>().List();
        }

        public IEnumerable<App> GetAllApplications()
        {
            return this.Session.QueryOver<App>().List();
        }
        public IEnumerable<ActivityType> GetAllActivityTypes()
        {
            return this.Session.QueryOver<ActivityType>().List();
        }
        public IEnumerable<Product> GellAllProducts()
        {
            return this.Session.QueryOver<Product>().List();
        }
        public IEnumerable<CostType> GellAllCostTypes()
        {
            return this.Session.QueryOver<CostType>().List();
        }
        public bool SaveUpdateDetail(POLine pLine, string purchaseOrderComment)
        {
            using (var transaction = this.Session.BeginTransaction())
            {
                try
                {
                    if (!string.IsNullOrEmpty(pLine.ContractTypeId))
                    {
                        pLine.ContractType = this.Session.Load<ContractType>(new Guid(pLine.ContractTypeId));
                    }
                    if (!string.IsNullOrEmpty(pLine.ProductId))
                    {
                        pLine.Product = this.Session.Load<Product>(new Guid(pLine.ProductId));
                    }
                    if (!string.IsNullOrEmpty(pLine.AppId))
                    {
                        pLine.App = this.Session.Load<App>(new Guid(pLine.AppId));
                    }
                    if (!string.IsNullOrEmpty(pLine.CostTypeId))
                    {
                        pLine.CostType = this.Session.Load<CostType>(new Guid(pLine.CostTypeId));
                    }
                    if (!string.IsNullOrEmpty(pLine.ActivityTypeId))
                    {
                        pLine.ActivityType = this.Session.Load<ActivityType>(new Guid(pLine.ActivityTypeId));
                    }
                    if (!string.IsNullOrEmpty(pLine.StatusPoID))
                    {
                        pLine.StatusPo = this.Session.Load<StatusPo>(new Guid(pLine.StatusPoID));
                    }
                    if (!string.IsNullOrEmpty(pLine.OwnerId))
                    {
                        pLine.PurchaseOrderLineFromEbd.PurchaseOrder.Owner = this.Session.Load<Owner>(new Guid(pLine.OwnerId));
                    }
                        pLine.PurchaseOrderLineFromEbd.PurchaseOrder.Comments = purchaseOrderComment;
                   
                    this.Session.Update("PoLine", pLine);
                    transaction.Commit();
                }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
                {

                    throw;
                }
            }
            return true;
        }
        public ValidationResults UpdateDetail(POLine pLine)
        {
            throw new System.NotImplementedException();
        }
        public POLine FindPolineByPurchaseOrderLineID(string purchaseOrderLineId)
        {
            try
            {
                Guid guid = new Guid(purchaseOrderLineId);
                return this.Session.QueryOver<POLine>().Where(x => x.PurchaseOrderLineId == guid).SingleOrDefault();
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                throw;
            }
        }
        public List<POLine> FillOwnerNameByRelationship(IEnumerable<POLine> purchaseOrderList)
        {
            //var purchaseOrderData = this.Session.QueryOver<PurchaseOrder>().List();
           // var OWNERData = this.Session.QueryOver<Owner>().List();
            //var PurchaseOrderLineFromEbdData = this.Session.QueryOver<PurchaseOrderLineFromEbd>().List();

            foreach (var item in purchaseOrderList)
            {
                var edbData = this.Session.QueryOver<PurchaseOrderLineFromEbd>().Where(x => x.PurchaseOrderLineFromEbdId == item.PurchaseOrderLineFromEbd.PurchaseOrderLineFromEbdId).List().FirstOrDefault();
                if (edbData != null)
                {
                    var pOrderData = this.Session.QueryOver<PurchaseOrder>().Where(x => x.PurchaseOrderId == edbData.PurchaseOrder.PurchaseOrderId).List().FirstOrDefault();
                    if (pOrderData != null)
                    {
                        var ownerData = this.Session.QueryOver<Owner>().Where(x => x.OwnerId == pOrderData.Owner.OwnerId).List().FirstOrDefault();
                        if (ownerData != null)
                        {
                            item.OwnerName = ownerData.Name;
                        }
                    }
                }
            }
            return purchaseOrderList.ToList();
        }
        public IQueryable<POLine> FillOwnerNameByRelationshipQurable(IQueryable<POLine> purchaseOrderList)
        {
     
            foreach (var item in purchaseOrderList)
            {
                var edbData = this.Session.QueryOver<PurchaseOrderLineFromEbd>().Where(x => x.PurchaseOrderLineFromEbdId == item.PurchaseOrderLineFromEbd.PurchaseOrderLineFromEbdId).List().FirstOrDefault();
                if (edbData != null)
                {
                    var pOrderData = this.Session.QueryOver<PurchaseOrder>().Where(x => x.PurchaseOrderId == edbData.PurchaseOrder.PurchaseOrderId).List().FirstOrDefault();
                    if (pOrderData != null)
                    {
                        var ownerData = this.Session.QueryOver<Owner>().Where(x => x.OwnerId == pOrderData.Owner.OwnerId).List().FirstOrDefault();
                        if (ownerData != null)
                        {
                            item.OwnerName = ownerData.Name;
                        }
                    }
                   

                }
            }
            return purchaseOrderList.ToList().AsQueryable();
        }
        public List<CustomModelSecondGrid> RetrieveDetailInnerGridData(string poLineId)
        {
            var poLineData = this.Session.QueryOver<POLine>().Where(i => i.PurchaseOrderLineId == new Guid(poLineId)).List();
            if (poLineData.Count() > 0)
            {
                 var poLineList = poLineData ;

                List<CustomModelSecondGrid> customModelSecondGridlist = new List<CustomModelSecondGrid>();

                foreach (var item in poLineList)
                {
                    CustomModelSecondGrid obj = new CustomModelSecondGrid();
                    obj.EarlierPaymentDate = (item.EarlierPaymentDate.HasValue) ? item.EarlierPaymentDate.Value.ToString("yyyy-MM") : string.Empty;
                    obj.DelayedPaymentDate = (item.DelayedDate.HasValue) ? item.DelayedDate.Value.ToString("yyyy-MM") : string.Empty;
                    obj.RechargeAmount = (item.SplitLineItemAmount.HasValue) ? item.SplitLineItemAmount.Value.ToString() : string.Empty;
                    obj.ContractStartDate = (item.StartDate.HasValue) ? item.StartDate.Value.ToString("yyyy-MM") : string.Empty;
                    obj.ContractEndDate = (item.EndDate.HasValue) ? item.EndDate.Value.ToString("yyyy-MM") : string.Empty;
                    obj.RequesterName = item.RequestorName;

                    var wbsDta = this.Session.QueryOver<WbsElement>().Where(x => x.AssignmentCode == item.AcOrWbs).List().FirstOrDefault();
                    if (wbsDta != null)
                    {
                        obj.Wbs = wbsDta.Name;
                        var sqlData = this.Session.QueryOver<App>().Where(x => x.ApmId == Convert.ToInt32(wbsDta.ApmId)).List().FirstOrDefault();
                        if (sqlData != null)
                        {
                            obj.ApplicationAPM1 = sqlData.Name;
                        }
                    }
                    customModelSecondGridlist.Add(obj);
                }
                return customModelSecondGridlist;
            }
            return new List<CustomModelSecondGrid>();
        }

        public class AppData
        {
            public string WBS { get; set; }
            public string Application { get; set; }

        }
        public POLine GetById(Guid purchaseOrderLineId)
        {
            return this.Session.Load<POLine>(purchaseOrderLineId);
        }

        public EmailRecipent GetPoLinebyEbdNumber(string ebdNumber)
        {
            try
            {

                var result = this.Session.QueryOver<POLine>().Where(x => x.EbdNumber == ebdNumber).List();
                if (result != null && result.Any())
                {
                    var recipents = new EmailRecipent();
                    recipents.RequestorEmail = new List<RequestorEmail>();
                    recipents.ContactPersonEmail = new List<ContactPersonEmail>();
                    foreach (var item in result)
                    {
                        recipents.RequestorEmail.Add(new RequestorEmail() { EbdNumber = item.EbdNumber, RequestorEmailId = item.RequestorName, PoLineId = item.PoLine });
                        recipents.ContactPersonEmail.Add(new ContactPersonEmail() { EbdNumber = item.EbdNumber, ContactPersonEmailId = item.ContactPerson, PoLineId = item.PoLine });

                    }

                    recipents.EbdNumber = ebdNumber;
                    return recipents;
                }

                return new EmailRecipent();
            }

            catch (Exception ex)

            {

                throw;
            }
        }
        public bool SaveUpdateMonthlyRate(List<Tuple<string, string, decimal>> tuppleListRec)
        {
            if (tuppleListRec != null && tuppleListRec.Count > 0)
            {
                foreach (var item in tuppleListRec)
                {
                    if (!string.IsNullOrEmpty(item.Item1) && !string.IsNullOrEmpty(item.Item2))
                    {
                        Guid guid = new Guid(item.Item2);
                        var poline = GetById(guid);
                        if (poline != null && item.Item3 > 0)
                        {
                            using (var transaction = this.Session.BeginTransaction())
                            {
                                try
                                {
                                    //var itt = Math.Round(item.Item3, 0, MidpointRounding.AwayFromZero);
                                    //IType decimalType = TypeFactory.Basic("Decimal(18,2)");
                                    IQuery query = this.Session.CreateQuery("update POLine set MonthlyRate = :NewMonthlyRateValue" + " where PurchaseOrderLine_ID = :PolineGuId");
                                    //query.SetDecimal("NewMonthlyRateValue", item.Item3);//, decimalType);
                                    query.SetParameter("NewMonthlyRateValue", item.Item3, TypeFactory.Basic("Decimal(18,2)"));
                                    query.SetParameter("PolineGuId", guid.ToString());
                                    int res = query.ExecuteUpdate();
                                    this.Session.Flush();
                                    transaction.Commit();
                                }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                                catch (Exception ex) { }
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
                            }
                        }
                    }
                }
            }

            return true;
        }


        public IEnumerable<PurchaseOrder> GellAllPurchaseOrder()
        {
            return this.Session.QueryOver<PurchaseOrder>().List();
        }

        public decimal GetSeekAmount(string currency, int year)
        {
            var cuurency = this.Session.QueryOver<Currency>().Where(x => x.Name == currency && x.Year == year).List().FirstOrDefault();//&& year == year).List().fir.FirstOrDefault();
            if (cuurency != null)
            {
                return cuurency.Rate;
            }
            return 0;
        }

        public PurchaseOrderLineFromEbd GetPurchaseOrderLineFromEbdById(Guid purchaseOrderLineFromEbdId)
        {
            return this.Session.QueryOver<PurchaseOrderLineFromEbd>().Where(x => x.PurchaseOrderLineFromEbdId == purchaseOrderLineFromEbdId).List().FirstOrDefault();
        }

        public bool CheckRenewalOrderLineExist(string renewalOrderLine)
        {
            var splitRenwal = renewalOrderLine.Split('_');
            if(splitRenwal.Count()==1)
            {
                return false;
            }
            var result = this.Session.QueryOver<POLine>().Where(x => x.EbdNumber == splitRenwal[0].ToString() && x.PoLine == int.Parse(splitRenwal[1])).List();
            if (result.Count() > 0)
            {
                return true;
            }
            return false;
        }

        public bool BulkInsert(List<POLine> polines)
        {
            using (var transaction = this.Session.BeginTransaction())
            {
                try
                {
                    this.Session.SetBatchSize(2000);
                    foreach (var item in polines)
                    {
                        this.Session.SaveOrUpdate(item);
                    }

                    transaction.Commit();
                    return true;

                }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
                {
                    return false;
                }
               
            }
        }

        public POLine GetPolineByEbdNumber(string ebdNumber)
        {
            return this.Session.QueryOver<POLine>().Where(x => x.EbdNumber == ebdNumber).Take(1).List().FirstOrDefault();
        }
        public POLine GetPolineByEbdNumberPoline(string ebdNumber,  int poLine)
        {
            var final = this.Session.QueryOver<POLine>().Where(x => x.EbdNumber == ebdNumber);
            POLine polinefinal = final.Where(x => x.PoLine == poLine).Take(1).List().FirstOrDefault();
            return polinefinal;
        }

        public IEnumerable<WbsElement> GetAllWbs()
        {
            return this.Session.QueryOver<WbsElement>().List();
        }

        public StatusPo InserStatusPo(StatusPo status)
        {
            using (var transaction = this.Session.BeginTransaction())
            {
                {
                    try
                    {
                        this.Session.Save(status);
                        transaction.Commit();
                        return status;
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }

                }
            }
        }
    }
}
