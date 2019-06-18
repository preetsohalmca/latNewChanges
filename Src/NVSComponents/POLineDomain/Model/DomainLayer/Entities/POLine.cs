namespace Volvo.LAT.POLineDomain.DomainLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
    using Volvo.NVS.Persistence.NHibernate.Entities;
    using System.ComponentModel.DataAnnotations;
    using Volvo.LAT.PartDomain.DomainLayer.Entities;

    public class POLine : GenericEntity
    {
        public virtual System.Guid PurchaseOrderLineId { get; set; }
      
        [Required]
        public virtual DateTime TimeStamp { get; set; }
        [Required]
        public virtual string EbdNumber { get; set; }
        [Required]
        public virtual int PoLine { get; set; }
        public virtual string ReplacedWithPo { get; set; }
        public virtual string Software { get; set; }
        public virtual string Remark { get; set; }

        
        public virtual string AcOrWbs { get; set; }
        
        public virtual System.Nullable<System.Guid> ApprovedBy { get; set; }
        public virtual System.Nullable<System.Guid> UnApprovedBy { get; set; }
        public virtual DateTime? ApprovedDate { get; set; }
        public virtual DateTime? UnApprovedDate { get; set; }
        public virtual string OwnerName { get; set; }
        [Required]
        public virtual System.Guid LastChangeBy { get; set; }
        [Required]
        public virtual DateTime LastChangeDate { get; set; }
        [Required]
        public virtual bool IsSplitted { get; set; }
        public virtual string PurchaserName { get; set; }
      
       
        [Required]
        public virtual bool Renewal { get; set; }
        
        public virtual string ProductNumber { get; set; }
        public virtual string InvoiceNumber { get; set; }
        [Required]
        public virtual bool IsNewOrder { get; set; }
        public virtual string StatusPoID { get; set; }
        public virtual string ContractTypeId { get; set; }
        public virtual string AppId { get; set; }
        public virtual string CostTypeId { get; set; }
        public virtual string ProductId { get; set; }
        public virtual string OwnerId { get; set; }
        public virtual Guid PurchaseOrderLineFromEbd_ID { get; set; }
        public virtual ContractType ContractType { get; set; }
        public virtual App App { get; set; }
        public virtual StatusPo StatusPo { get; set; }
        public virtual ActivityType ActivityType { get; set; }
        public virtual CostType CostType { get; set; }
		public virtual string ActivityTypeId { get; set; }
        public virtual Product Product { get; set; }
        public virtual PurchaseOrderLineFromEbd PurchaseOrderLineFromEbd { get; set; }
        public virtual WbsElement WbsElement { get; set; }
        public virtual AssignmentCode AssignmentCode { get; set; }
        public virtual Currency Currency { get; set; }
        //public virtual string Comments { get; set; }
        public virtual string RenewalOrderPurchaseLine { get; set; }
        public virtual string CostCenterId { get; set; }
        public virtual CostCenter CostCenter { get; set; }
       
    }
    public class DashboardNewOrders
    {
        public virtual string EbdNumber { get; set; }
        public virtual string VendorName { get; set; }
        public virtual DateTime StartDate { get; set; }
    }
}
