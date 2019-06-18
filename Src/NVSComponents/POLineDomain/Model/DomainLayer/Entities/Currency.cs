namespace Volvo.LAT.POLineDomain.DomainLayer.Entities
{
    using System;
    using Volvo.NVS.Persistence.NHibernate.Entities;
    using System.ComponentModel.DataAnnotations;
    public class Currency : GenericEntity
    {
        public Currency() { }
        public virtual System.Guid CurrencyID { get; set; }
        [Required]
        public virtual string Name { get; set; }
        [Required]
        public virtual decimal Rate { get; set; }
        [Required]
        public virtual int Year { get; set; }
        [Required]
        public virtual DateTime TimeStamp { get; set; }
    }
}