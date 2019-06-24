using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volvo.LAT.PartDomain.DomainLayer.Entities;

namespace Volvo.LAT.POLineDomain.DomainLayer.Entities
{

    public class CustomModel
    {
        public System.Guid PurchaseOrderLineId { get; set; }
        public DateTime TimeStamp { get; set; }
        public string EbdNumber { get; set; }
        public int PoLine { get; set; }
        public string ReplacedWithPo { get; set; }
        public string Software { get; set; }
        public string Remark { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? DelayedDate { get; set; }
        public string ContactPerson { get; set; }
        public string LastChangeByName { get; set; }
        public string AcOrWbs { get; set; }
        public decimal? SplitLineItemAmount { get; set; }
        //public  System.Nullable<System.Guid> ApprovedBy { get; set; }
        //public  System.Nullable<System.Guid> UnApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? UnApprovedDate { get; set; }
        public string OwnerName { get; set; }
        public System.Guid LastChangeBy { get; set; }
        public DateTime LastChangeDate { get; set; }
        public bool IsSplitted { get; set; }
        public string PurchaserName { get; set; }
        public string RequestorName { get; set; }
        public DateTime? EarlierPaymentDate { get; set; }
        public bool Renewal { get; set; }
        public int? ExchangeRateYear { get; set; }
        public string ProductNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceNumberHeader { get; set; }
        public bool IsNewOrder { get; set; }
        public string Activitytype { get; set; }
        public string WBS { get; set; }
        public string SoftwareName { get; set; }
        public string Lastmodifyname { get; set; }
        public DateTime Lastmodifydate { get; set; }
        public string CostCenter { get; set; }
        public string SDU { get; set; }
        public string OrderAmount { get; set; }
        public string OrderamountinSEK { get; set; }
        public decimal OrderamountAmount { get; set; }
        public decimal LeftTotalPoAmount { get; set; }
        public decimal SekRate { get; set; }
        public decimal ExchangeRateCurrencyRate { get; set; }
        public int Numberofchargableamount { get; set; }
        public string LineItemDescription { get; set; }
        public string ShortDescription { get; set; }
        public string APMID { get; set; }
        public string CostType { get; set; }
        public string Name { get; set; }
        public string PoNumber { get; set; }
        public string ContractStartDate { get; set; }
        public string ContractEndDate { get; set; }
        public string Owner_ID { get; set; }
        public string App_ID { get; set; }
        public string ContractTypeId { get; set; }
        public string CostTypeId { get; set; }
        public string ProductId { get; set; }
        public string VendorName { get; set; }
        public string Currency { get; set; }
        public string StatusPoid { get; set; }
        public string Comments { get; set; }
        public DateTime OrderDate { get; set; }
        public string ActivityTypeId { get; set; }
        public string PurchaserOrderName { get; set; }
        public int RenewalTotalDaysLeft { get; set; }
        public List<AssignmentCode> AssignmentCode { get; set; }
    public string RenewalOrderPurchaseLine { get; set; }
        public string CostCenterName { get; set; }
        public string CostCenterId { get; set; }
        public string MonthlyRate { get; set; }
        public IEnumerable<App> Applications  { get; set; }
        public IEnumerable<ContractType> ContractTypes { get; set; }
        public IEnumerable<Owner> Owners { get; set; }
        public IEnumerable<StatusPo> StatusPo { get; set; }
        public IEnumerable<ActivityType> ActivityTypes { get; set; }
        public SearchModel SearchModelDetail { get; set; }

    }

    public class CustomModelSecondGrid
    {
        public virtual string Wbs { get; set; }
        public virtual string ApplicationAPM1 { get; set; }
        public virtual string RechargeAmount { get; set; }
        public virtual string ContractStartDate { get; set; }
        public virtual string ContractEndDate { get; set; }
        public virtual string EarlierPaymentDate { get; set; }
        public virtual string DelayedPaymentDate { get; set; }
        public virtual string MonthlyRate { get; set; }
        public virtual string RequesterName { get; set; }
    }

    public class EmailTemplate
    {
        public string EmailTemplateString { get; set; }
        public decimal AllOrderAmount  { get; set; }
        public CustomModel CustomViewModel { get; set; }
        public string userName { get; set; }
        public string Name { get; set; }
        public EmailTemplate()
        {
            CustomViewModel = new CustomModel();
        }
    }
}
