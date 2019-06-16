namespace Volvo.LAT.POLineDomain.DomainLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
    using Volvo.NVS.Persistence.NHibernate.Entities;
    using System.ComponentModel.DataAnnotations;
    public class ActivityType : GenericEntity
    {
        public ActivityType() { }
        public virtual System.Guid ActivityTypeId { get; set; }
        [Required]
        public virtual string Name { get; set; }
        [Required]
        public virtual bool IsDefault { get; set; }
        [Required]
        public virtual DateTime TimeStamp { get; set; }
    }
}