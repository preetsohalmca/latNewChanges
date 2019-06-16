namespace Volvo.LAT.POLineDomain.DomainLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
    using Volvo.NVS.Persistence.NHibernate.Entities;
    using System.ComponentModel.DataAnnotations;
    public class PurchaseOrderLineFromEbd : GenericEntity
    {
        public PurchaseOrderLineFromEbd() { } 
        public virtual System.Guid PurchaseOrderLineFromEbdId { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        [Required]
        public virtual DateTime TimeStamp { get; set; }
        public virtual string EbdReceiveStatusDescription { get; set; }
        [Required]
        public virtual string LineItemDescription { get; set; }
        [Required]
        public virtual decimal OrderAmount { get; set; }
        [Required]
        public virtual DateTime CreationDate { get; set; }
        public virtual string SpendType { get; set; }
        //public virtual string CostCenter { get; set; }
        public virtual string CompanyId { get; set; }
        public virtual string ShortDescription { get; set; }
        public virtual string GeographicalSite { get; set; }
        public virtual string LowestBorg { get; set; }
        public virtual string RequesterEmail { get; set; }
        public virtual string PurchaserName { get; set; }
        public virtual string FunctionalApproverName { get; set; }
        public virtual string ParmaNbr { get; set; }
        [Required]
        public virtual decimal OrderedQuantity { get; set; }
        [Required]
        public virtual decimal ReceivedQuantity { get; set; }
        [Required]
        public virtual DateTime ContractStartDate { get; set; }
        [Required]
        public virtual DateTime ContractEndDate { get; set; }
        public virtual string LicenseType { get; set; }
        public virtual string SoftwareName { get; set; }
        [Required]
        public virtual int PoLine { get; set; }
        //public virtual CostCenter CostCenter { get; set; }
    }
}
