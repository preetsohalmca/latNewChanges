namespace Volvo.LAT.POLineDomain.DomainLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
    using Volvo.NVS.Persistence.NHibernate.Entities;
    using System.ComponentModel.DataAnnotations;
    using UserDomain.DomainLayer.Entities;

    public class InvoicingReport : GenericEntity
    {
        public InvoicingReport() { }
        public virtual System.Guid InvoicingReportID { get; set; }
        [Required]
        public virtual DateTime TimeStamp { get; set; }
        [Required]
        public virtual int Year { get; set; }
        [Required]
        public virtual int Month { get; set; }
        [Required]
        public virtual string Xml { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual string InvoicingReportToDelete { get; set; }
        public virtual User CreatedUser { get; set; }
       


    }

    public class InvoiceReportRecordDTO
    {
        public string AcOrWbs { get; set; }
        public string CostCenter { get; set; }
        public string ActivityType { get; set; }
        public string Volume { get; set; }
        public string PoName { get; set; }
        public string EbdNumber { get; set; }
        public string InvoiceMasterId { get; set; }
        public virtual Guid UserID { get; set; }
    }
}