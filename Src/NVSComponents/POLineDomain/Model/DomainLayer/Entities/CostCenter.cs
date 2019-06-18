namespace Volvo.LAT.POLineDomain.DomainLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
    using Volvo.NVS.Persistence.NHibernate.Entities;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;

    public class CostCenter : GenericEntity
    {
        public CostCenter() { }
        public virtual System.Guid CostCenterId { get; set; }
        [Required]
        public virtual string Name { get; set; }
        [Required]
        public virtual string FullName { get; set; }
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Date")]
        [Required]
        public virtual DateTime TimeStamp { get; set; }
        [Required]
        public virtual string BaseCurrency { get; set; }
        public virtual string CountryCode { get; set; }
    }
}
