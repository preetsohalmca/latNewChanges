namespace Volvo.LAT.POLineDomain.DomainLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
    using Volvo.NVS.Persistence.NHibernate.Entities;
    using System.ComponentModel.DataAnnotations;
    public class CostList : GenericEntity
    {
        public CostList() { }
        public virtual System.Guid CostListId { get; set; }
        [Required]
        public virtual decimal Cost { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual bool IsDeleted { get; set; }
        [Required]
        public virtual string PoLineId { get; set; }
        public virtual string PoNumber { get; set; }
    }
}