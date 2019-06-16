namespace Volvo.LAT.POLineDomain.DomainLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
    using Volvo.NVS.Persistence.NHibernate.Entities;
    using System.ComponentModel.DataAnnotations;
    public class PurchaseOrder : GenericEntity
    {
		public PurchaseOrder() { }
        public virtual System.Guid PurchaseOrderId { get; set; }
        public virtual Owner Owner { get; set; }
        [Required]
        public virtual DateTime TimeStamp { get; set; }
        [Required]
        public virtual string PoNumber { get; set; }
        [Required]
        public virtual string Currency { get; set; }
        public virtual string VendorName { get; set; }
        [Required]
        public virtual string PurchaseOrderName { get; set; }
        [Required]
        public virtual DateTime OrderDate { get; set; }
        public virtual string PurchaserName { get; set; }
        public virtual string Comments { get; set; }
        public virtual string InvoiceNumber { get; set; }
        //public virtual PurchaseOrderLineFromEbd PurchaseOrderLineFromEbd { get; set; }
    }
}